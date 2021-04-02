using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Telepathy
{
    public class Server : Common
    {
        // events to hook into
        // => OnData uses ArraySegment for allocation free receives later
        public Action<int> OnConnected;
        public Action<int, ArraySegment<byte>> OnData;
        public Action<int> OnDisconnected;

        // listener
        public TcpListener listener;
        Thread listenerThread;

        // disconnect if send queue gets too big.
        // -> avoids ever growing queue memory if network is slower than input
        // -> disconnecting is great for load balancing. better to disconnect
        //    one connection than risking every connection / the whole server
        // -> huge queue would introduce multiple seconds of latency anyway
        //
        // Mirror/DOTSNET use MaxMessageSize batching, so for a 16kb max size:
        //   limit =  1,000 means  16 MB of memory/connection
        //   limit = 10,000 means 160 MB of memory/connection
        public int SendQueueLimit = 10000;
        public int ReceiveQueueLimit = 10000;

        // thread safe pipe for received messages
        // IMPORTANT: unfortunately using one pipe per connection is way slower
        //            when testing 150 CCU. we need to use one pipe for all
        //            connections. this scales beautifully.
        protected MagnificentReceivePipe receivePipe;

        // pipe count, useful for debugging / benchmarks
        public int ReceivePipeTotalCount => receivePipe.TotalCount;

        // clients with <connectionId, ConnectionState>
        readonly ConcurrentDictionary<int, ConnectionState> clients = new ConcurrentDictionary<int, ConnectionState>();

        // connectionId计数器
        int counter;

        // 公共下一个ID函数，以防有人需要保留ID
        //（例如，如果hostMode应该始终具有0个连接，而外部连接应从1开始，依此类推）
        public int NextConnectionId()
        {
            int id = Interlocked.Increment(ref counter);

            // 我们很难达到20亿的单位上限，即使每秒连接1个新连接也需要68年。
            // -> 但是，如果发生这种情况，那么我们应该抛出一个异常，因为调用者可能应该停止接受客户端。
            // -> 在这种情况下，几乎不值得使用“ bool Next（out id）”，因为它不太可能。
            if (id == int.MaxValue)
            {
                throw new Exception("connection id limit reached: " + id);
            }

            return id;
        }

        // 检查服务器是否正在运行
        public bool Active => listenerThread != null && listenerThread.IsAlive;

        // constructor
        public Server(int MaxMessageSize) : base(MaxMessageSize) {}

        // the listener thread's listen function
        // note: no maxConnections parameter. high level API should handle that.
        //       (Transport can't send a 'too full' message anyway)
        void Listen(int port)
        {
            // 绝对必须用try / catch换行，否则线程异常是静默的

            try
            {
                // 通过.Create在所有IPv4和IPv6地址上启动侦听器
                listener = TcpListener.Create(port);
                listener.Server.NoDelay = NoDelay;
                listener.Server.SendTimeout = SendTimeout;
                listener.Server.ReceiveTimeout = ReceiveTimeout;
                listener.Start();
                Log.Info("Server: listening port=" + port);

                // 继续接受新客户
                while (true)
                {
                    // 等待并接受新客户
                    // 注意：“using”在这里很烂，因为它将在线程启动后尝试处理，但是我们仍然需要在线程中使用它
                    TcpClient client = listener.AcceptTcpClient();

                    // set socket options
                    client.NoDelay = NoDelay;
                    client.SendTimeout = SendTimeout;
                    client.ReceiveTimeout = ReceiveTimeout;

                    // 生成下一个连接ID（安全地线程化）
                    int connectionId = NextConnectionId();

                    // add to dict immediately
                    ConnectionState connection = new ConnectionState(client, MaxMessageSize);
                    clients[connectionId] = connection;

                    // 为每个客户端生成一个发送线程
                    Thread sendThread = new Thread(() =>
                    {
                        // 包装在try-catch中，否则线程异常是静默的

                        try
                        {
                            // run the send loop
                            // IMPORTANT: DO NOT SHARE STATE ACROSS MULTIPLE THREADS!
                            ThreadFunctions.SendLoop(connectionId, client, connection.sendPipe, connection.sendPending);
                        }
                        catch (ThreadAbortException)
                        {
                            // happens on stop. don't log anything.
                            // (we catch it in SendLoop too, but it still gets
                            //  through to here when aborting. don't show an
                            //  error.)
                        }
                        catch (Exception exception)
                        {
                            Log.Error("Server send thread exception: " + exception);
                        }
                    });
                    sendThread.IsBackground = true;
                    sendThread.Start();

                    // 为每个客户端生成一个接收线程
                    Thread receiveThread = new Thread(() =>
                    {
                        // wrap in try-catch, otherwise Thread exceptions
                        // are silent
                        try
                        {
                            // run the receive loop
                            // (receive pipe is shared across all loops)
                            ThreadFunctions.ReceiveLoop(connectionId, client, MaxMessageSize, receivePipe, ReceiveQueueLimit);

                            // IMPORTANT: do NOT remove from clients after the
                            // thread ends. need to do it in Tick() so that the
                            // disconnect event in the pipe is still processed.
                            // (removing client immediately would mean that the
                            //  pipe is lost and the disconnect event is never
                            //  processed)

                            // sendthread可能正在等待ManualResetEvent，因此请确保在连接关闭的情况下结束它。
                            // 否则，只有在关闭连接时实际发送数据时，发送线程才会结束。
                            sendThread.Interrupt();
                        }
                        catch (Exception exception)
                        {
                            Log.Error("Server client thread exception: " + exception);
                        }
                    });
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                }
            }
            catch (ThreadAbortException exception)
            {
                // UnityEditor causes AbortException if thread is still
                // running when we press Play again next time. that's okay.
                Log.Info("Server thread aborted. That's okay. " + exception);
            }
            catch (SocketException exception)
            {
                // calling StopServer will interrupt this thread with a
                // 'SocketException: interrupted'. that's okay.
                Log.Info("Server Thread stopped. That's okay. " + exception);
            }
            catch (Exception exception)
            {
                // something went wrong. probably important.
                Log.Error("Server Exception: " + exception);
            }
        }

        // 开始在后台线程中侦听新连接，并为每个线程生成一个新线程。
        public bool Start(int port)
        {
            // not if already started
            if (Active) return false;

            // create receive pipe with max message size for pooling
            // => create new pipes every time!
            //    if an old receive thread is still finishing up, it might still
            //    be using the old pipes. we don't want to risk any old data for
            //    our new start here.
            receivePipe = new MagnificentReceivePipe(MaxMessageSize);

            // start the listener thread
            // （低优先级。如果主线程太忙，那么接受更多客户端没有太大价值）
            Log.Info("Server: Start port=" + port);
            listenerThread = new Thread(() => { Listen(port); });
            listenerThread.IsBackground = true;
            listenerThread.Priority = ThreadPriority.BelowNormal;
            listenerThread.Start();
            return true;
        }

        public void Stop()
        {
            // only if started
            if (!Active) return;

            Log.Info("Server: stopping...");

            // 停止监听连接，以便在我们关闭客户端连接时没有人可以连接
            //（如果我们在“启动”后如此快地调用“停止”，以至于在创建监听器之前就已中断了线程
            listener?.Stop();

            // 不惜一切代价杀死监听线程。 唯一可以确保.Active在Stop之后立即为false的方法。
            // -> calling .Join would sometimes wait forever
            listenerThread?.Interrupt();
            listenerThread = null;

            // close all client connections
            foreach (KeyValuePair<int, ConnectionState> kvp in clients)
            {
                TcpClient client = kvp.Value.client;
                // 如果尚未关闭，请关闭流。 它可能已经被断开连接关闭了，所以请使用try / catch
                try { client.GetStream().Close(); } catch {}
                client.Close();
            }

            // clear clients list
            clients.Clear();

            // 重置计数器，以防我们再次启动，以便客户端获得从1开始的连接ID
            counter = 0;
        }

        // send message to client using socket connection.
        // arraysegment for allocation free sends later.
        // -> the segment's array is only used until Send() returns!
        public bool Send(int connectionId, ArraySegment<byte> message)
        {
            // respect max message size to avoid allocation attacks.
            if (message.Count <= MaxMessageSize)
            {
                // find the connection
                if (clients.TryGetValue(connectionId, out ConnectionState connection))
                {
                    // check send pipe limit
                    if (connection.sendPipe.Count < SendQueueLimit)
                    {
                        // add to thread safe send pipe and return immediately.
                        // calling Send here would be blocking (sometimes for long
                        // times if other side lags or wire was disconnected)
                        connection.sendPipe.Enqueue(message);
                        connection.sendPending.Set(); // interrupt SendThread WaitOne()
                        return true;
                    }
                    // disconnect if send queue gets too big.
                    // -> avoids ever growing queue memory if network is slower
                    //    than input
                    // -> disconnecting is great for load balancing. better to
                    //    disconnect one connection than risking every
                    //    connection / the whole server
                    //
                    // note: while SendThread always grabs the WHOLE send queue
                    //       immediately, it's still possible that the sending
                    //       blocks for so long that the send queue just gets
                    //       way too big. have a limit - better safe than sorry.
                    else
                    {
                        // log the reason
                        Log.Warning($"Server.Send: sendPipe for connection {connectionId} reached limit of {SendQueueLimit}. This can happen if we call send faster than the network can process messages. Disconnecting this connection for load balancing.");

                        // just close it. send thread will take care of the rest.
                        connection.client.Close();
                        return false;
                    }
                }

                // 有时会发送到无效的connectionId。
                // 例如，如果客户端断开连接，则服务器可能仍尝试发送一帧，
                // 然后再次调用GetNextMessages并意识到断开连接已发生。
                // 因此，我们不要在日志中向控制台发送垃圾邮件。
                // Logger.Log("Server.Send: invalid connectionId: " + connectionId);
                return false;
            }
            Log.Error("Server.Send: message too big: " + message.Count + ". Limit: " + MaxMessageSize);
            return false;
        }

        // client's ip is sometimes needed by the server, e.g. for bans
        public string GetClientAddress(int connectionId)
        {
            // find the connection
            if (clients.TryGetValue(connectionId, out ConnectionState connection))
            {
                return ((IPEndPoint)connection.client.Client.RemoteEndPoint).Address.ToString();
            }
            return "";
        }

        // disconnect (kick) a client
        public bool Disconnect(int connectionId)
        {
            // find the connection
            if (clients.TryGetValue(connectionId, out ConnectionState connection))
            {
                // just close it. send thread will take care of the rest.
                connection.client.Close();
                Log.Info("Server.Disconnect connectionId:" + connectionId);
                return true;
            }
            return false;
        }

        // tick: processes up to 'limit' messages for each connection
        // => limit parameter to avoid deadlocks / too long freezes if server or
        //    client is too slow to process network load
        // => Mirror & DOTSNET need to have a process limit anyway.
        //    might as well do it here and make life easier.
        // => returns amount of remaining messages to process, so the caller
        //    can call tick again as many times as needed (or up to a limit)
        //
        // Tick() may process multiple messages, but Mirror needs a way to stop
        // processing immediately if a scene change messages arrives. Mirror
        // can't process any other messages during a scene change.
        // (could be useful for others too)
        // => make sure to allocate the lambda only once in transports
        public int Tick(int processLimit, Func<bool> checkEnabled = null)
        {
            // only if pipe was created yet (after start())
            if (receivePipe == null)
                return 0;

            // process up to 'processLimit' messages for this connection
            for (int i = 0; i < processLimit; ++i)
            {
                // check enabled in case a Mirror scene message arrived
                if (checkEnabled != null && !checkEnabled())
                    break;

                // peek first. allows us to process the first queued entry while
                // still keeping the pooled byte[] alive by not removing anything.
                if (receivePipe.TryPeek(out int connectionId, out EventType eventType, out ArraySegment<byte> message))
                {
                    switch (eventType)
                    {
                        case EventType.Connected:
                            OnConnected?.Invoke(connectionId);
                            break;
                        case EventType.Data:
                            OnData?.Invoke(connectionId, message);
                            break;
                        case EventType.Disconnected:
                            OnDisconnected?.Invoke(connectionId);
                            // remove disconnected connection now that the final
                            // disconnected message was processed.
                            clients.TryRemove(connectionId, out ConnectionState _);
                            break;
                    }

                    // IMPORTANT: now dequeue and return it to pool AFTER we are
                    //            done processing the event.
                    receivePipe.TryDequeue();
                }
                // no more messages. stop the loop.
                else break;
            }

            // return what's left to process for next time
            return receivePipe.TotalCount;
        }
    }
}
