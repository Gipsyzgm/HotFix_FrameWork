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
        // listener
        public TcpListener listener;
        Thread listenerThread;

        // 客户的所有数据进行分类。 我们将其称为Token，以与异步套接字方法保持一致。
        class ClientToken
        {
            public TcpClient client;

            // send queue
            // SafeQueue is twice as fast as ConcurrentQueue, see SafeQueue.cs!
            public SafeQueue<byte[]> sendQueue = new SafeQueue<byte[]>();

            // ManualResetEvent唤醒发送线程。 比Thread.Sleep更好
            // -> call Set() 如果所有内容都已发送
            // -> call Reset() 如果有东西要再次发送
            // -> call WaitOne() 阻止直到调用Reset
            public ManualResetEvent sendPending = new ManualResetEvent(false);

            public ClientToken(TcpClient client)
            {
                this.client = client;
            }
        }

        // 具有<connectionId，ClientData>的客户端
        readonly ConcurrentDictionary<int, ClientToken> clients = new ConcurrentDictionary<int, ClientToken>();

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

        // the listener thread's listen function
        // 注意：没有maxConnections参数。 高级API应该可以解决这个问题。 （无论如何，运输无法发送“太满”消息）
        void Listen(int port)
        {
            // 绝对必须用try / catch换行，否则线程异常是静默的
            try
            {
                // 通过.Create在所有IPv4和IPv6地址上启动侦听器
                listener = TcpListener.Create(port);
                listener.Server.NoDelay = NoDelay;
                listener.Server.SendTimeout = SendTimeout;
                listener.Start();
                Logger.Log("Server: listening port=" + port);

                // 继续接受新客户
                while (true)
                {
                    // 等待并接受新客户
                    // 注意：“using”在这里很烂，因为它将在线程启动后尝试处理，但是我们仍然需要在线程中使用它
                    TcpClient client = listener.AcceptTcpClient();

                    // set socket options
                    client.NoDelay = NoDelay;
                    client.SendTimeout = SendTimeout;

                    // 生成下一个连接ID（安全地线程化）
                    int connectionId = NextConnectionId();

                    // 立即添加到字典
                    ClientToken token = new ClientToken(client);
                    clients[connectionId] = token;

                    // 为每个客户端生成一个发送线程
                    Thread sendThread = new Thread(() =>
                    {
                        // 包装在try-catch中，否则线程异常是静默的
                        try
                        {
                            // 运行发送循环
                            SendLoop(connectionId, client, token.sendQueue, token.sendPending);
                        }
                        catch (ThreadAbortException)
                        {
                            // happens on stop. don't log anything.
                            //（我们也在SendLoop中捕获了它，但是中止时它仍然可以到达此处。不显示错误。）
                        }
                        catch (Exception exception)
                        {
                            Logger.LogError("Server send thread exception: " + exception);
                        }
                    });
                    sendThread.IsBackground = true;
                    sendThread.Start();

                    // 为每个客户端生成一个接收线程
                    Thread receiveThread = new Thread(() =>
                    {
                        // 包装在try-catch中，否则线程异常是静默的
                        try
                        {
                            // run the receive loop
                            ReceiveLoop(connectionId, client, receiveQueue, MaxMessageSize);

                            // 之后从客户字典中删除客户
                            clients.TryRemove(connectionId, out ClientToken _);

                            // sendthread可能正在等待ManualResetEvent，因此请确保在连接关闭的情况下结束它。
                            // 否则，只有在关闭连接时实际发送数据时，发送线程才会结束。
                            sendThread.Interrupt();
                        }
                        catch (Exception exception)
                        {
                            Logger.LogError("Server client thread exception: " + exception);
                        }
                    });
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                }
            }
            catch (ThreadAbortException exception)
            {
                // 如果下次再次按下播放时线程仍在运行，则UnityEditor会导致AbortException。 没关系。
                Logger.Log("Server thread aborted. That's okay. " + exception);
            }
            catch (SocketException exception)
            {
                // 调用StopServer将使用“ SocketException：已中断”。 没关系。
                Logger.Log("Server Thread stopped. That's okay. " + exception);
            }
            catch (Exception exception)
            {
                // something went wrong. probably important.
                Logger.LogError("Server Exception: " + exception);
            }
        }

        // 开始在后台线程中侦听新连接，并为每个线程生成一个新线程。
        public bool Start(int port)
        {
            // not if already started
            if (Active) return false;

            // 清除队列中的旧邮件，只是要确保呼叫者没有收到上次的数据并且不同步。
            // -> 在“停止”中调用此方法并不明智，因为呼叫者之后可能仍想处理所有最新消息
            receiveQueue = new ConcurrentQueue<Message>();

            // start the listener thread
            // （低优先级。如果主线程太忙，那么接受更多客户端没有太大价值）
            Logger.Log("Server: Start port=" + port);
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

            Logger.Log("Server: stopping...");

            // 停止监听连接，以便在我们关闭客户端连接时没有人可以连接
            //（如果我们在“启动”后如此快地调用“停止”，以至于在创建监听器之前就已中断了线程，则可能为null）
            listener?.Stop();

            // 不惜一切代价杀死监听线程。 唯一可以确保.Active在Stop之后立即为false的方法。
            // -> calling .Join would sometimes wait forever
            listenerThread?.Interrupt();
            listenerThread = null;

            // 关闭所有客户端连接
            foreach (KeyValuePair<int, ClientToken> kvp in clients)
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

        // 使用套接字连接向客户端发送消息。
        public bool Send(int connectionId, byte[] data)
        {
            // respect max message size to avoid allocation attacks.
            if (data.Length <= MaxMessageSize)
            {
                // find the connection
                ClientToken token;
                if (clients.TryGetValue(connectionId, out token))
                {
                    // 添加发送队列并立即返回。
                    // 呼叫“发送到此处”将被阻止（如果其他侧滞或电线断开，则有时会很长一段时间）
                    token.sendQueue.Enqueue(data);
                    token.sendPending.Set(); // 中断SendThread WaitOne()
                    return true;
                }
                // 有时会发送到无效的connectionId。
                // 例如，如果客户端断开连接，则服务器可能仍尝试发送一帧，
                // 然后再次调用GetNextMessages并意识到断开连接已发生。
                // 因此，我们不要在日志中向控制台发送垃圾邮件。
                // Logger.Log("Server.Send: invalid connectionId: " + connectionId);
                return false;
            }
            Logger.LogError("Client.Send: message too big: " + data.Length + ". Limit: " + MaxMessageSize);
            return false;
        }

        // 服务器有时需要客户端的ip，例如 禁令ns
        public string GetClientAddress(int connectionId)
        {
            // find the connection
            ClientToken token;
            if (clients.TryGetValue(connectionId, out token))
            {
                return ((IPEndPoint)token.client.Client.RemoteEndPoint).Address.ToString();
            }
            return "";
        }

        // 断开（踢）客户端
        public bool Disconnect(int connectionId)
        {
            // find the connection
            ClientToken token;
            if (clients.TryGetValue(connectionId, out token))
            {
                // just close it. client thread will take care of the rest.
                token.client.Close();
                Logger.Log("Server.Disconnect connectionId:" + connectionId);
                return true;
            }
            return false;
        }
    }
}
