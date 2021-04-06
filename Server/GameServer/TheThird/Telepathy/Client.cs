using System;
using System.Net.Sockets;
using System.Threading;

namespace Telepathy
{
    // 可以安全地传递给ReceiveThread的客户端状态对象。
    // => 允许我们在每次连接并启动接收线程时创建一个新对象。
    // => 完美地保护我们免受数据争夺。 修复了所有不稳定的测试，在这些测试中，死线程仍会使用.Connecting或.client，而尝试将其用于新的连接尝试等。
    // => 每次都创建一个新的客户端状态是抵制数据争夺的最佳解决方案！
    class ClientConnectionState : ConnectionState
    {
        public Thread receiveThread;

        // TcpClient.Connected doesn't check if socket != null, 如果关闭连接，则导致NullReferenceExceptions。
        // -> 让我们手动检查一下
        public bool Connected => client != null &&
                                 client.Client != null &&
                                 client.Client.Connected;

        //  TcpClient没有要检查的“connecting”状态。我们需要手动跟踪。 
        // -> 仅检查'thread.IsAlive &&！Connected'是不够的，因为在断开连接后的一小段时间内，
        //    该线程仍处于活动状态且连接为false，因此会导致争用情况。
        // -> 我们使用线程安全的布尔包装器，以便线程功能可以保持静态（它需要一个公共锁）
        // => 从此处的第一个Connect()调用开始，直到线程TcpClient.Connect()返回，连接才为true。 简单明了。
        // => bools are atomic according to
        //    https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/variables
        //    made volatile so the compiler does not reorder access to it
        public volatile bool Connecting;

        // 用于接收消息的线程安全管道
        // => 内部客户端连接状态，以便我们每次连接时都可以创建一个新状态
        //    (与服务器不同的是，该服务器具有一个用于所有连接的接收管道)
        public readonly MagnificentReceivePipe receivePipe;

        // 构造函数始终为客户端连接创建新的TcpClient！
        public ClientConnectionState(int MaxMessageSize) : base(new TcpClient(), MaxMessageSize)
        {
            // 创建具有最大消息大小的接收管道以进行池化
            receivePipe = new MagnificentReceivePipe(MaxMessageSize);
        }

        // dispose all the state safely
        public void Dispose()
        {
            // close client
            client.Close();

            // 等到线程完成。 这是确保断开连接后立即可以再次调用Connect（）的唯一方法
            // -> calling .Join would sometimes wait forever, e.g. when
            //    calling Disconnect while trying to connect to a dead end
            receiveThread?.Interrupt();

            // 我们中断了接收线程，因此我们无法保证连接已重置。 让我们手动进行。
            Connecting = false;

            // 清除发送管道。 无需保留任何元素。.
            // (与receiveQueue不同，后者仍然需要处理最新的Disconnected消息等。)
            sendPipe.Clear();

            // 重要：请勿清除接收管。
            // 我们仍然想在Tick（）中处理断开连接消息!

            // 完全放开这个客户。 线程结束，没有人再使用它，因此Connected立即再次为false。
            client = null;
        }
    }

    public class Client : Common
    {
        // events to hook into
        // =>OnData使用ArraySegment进行稍后的免费分配接收
        public Action OnConnected;
        public Action<ArraySegment<byte>> OnData;
        public Action OnDisconnected;

        // 如果发送队列太大，则断开连接。
        // -> 如果网络速度比输入速度慢，可避免增加队列内存
        // -> 断开连接非常适合负载平衡。 断开一个连接比冒每个连接/整个服务器的风险更好
        // -> 庞大的队列无论如何都会带来数秒的延迟
        //
        // Mirror / DOTSNET使用MaxMessageSize批处理，因此最大大小为16kb：
        //   limit =  1,000 means  16 MB of memory/connection
        //   limit = 10,000 means 160 MB of memory/connection
        public int SendQueueLimit = 10000;
        public int ReceiveQueueLimit = 10000;

        // 所有客户端状态都包装到一个对象中，该对象传递给ReceiveThread
        // => 每次连接时，我们都会创建一个新对象，以避免与旧的死线程仍在使用前一个对象的数据争用！
        ClientConnectionState state;

        // Connected & Connecting
        public bool Connected => state != null && state.Connected;
        public bool Connecting => state != null && state.Connecting;

        // 管道数量，对于调试/基准测试很有用
        public int ReceivePipeCount => state != null ? state.receivePipe.TotalCount : 0;

        // 构造函数
        public Client(int MaxMessageSize) : base(MaxMessageSize) {}

        // 线程功能
        // STATIC避免共享状态！!
        // => 传递ClientState对象。 为每个新线程创建一个新线程！
        // => 避免旧的线程可能仍会修改当前线程状态的数据争用 :/
        static void ReceiveThreadFunction(ClientConnectionState state, string ip, int port, int MaxMessageSize, bool NoDelay, int SendTimeout, int ReceiveTimeout, int ReceiveQueueLimit)

        {
            Thread sendThread = null;

            // 绝对必须用try / catch换行，否则线程异常是静默的
            try
            {
                // connect (blocking)
                state.client.Connect(ip, port);
                state.Connecting = false; // volatile!

                // set socket options after the socket was created in Connect()
                // (不是在构造函数之后，因为我们清除了那里的套接字)
                state.client.NoDelay = NoDelay;
                state.client.SendTimeout = SendTimeout;
                state.client.ReceiveTimeout = ReceiveTimeout;

                //连接后才开始发送线程
                // 重要提示：请勿跨多个线程共享状态！
                sendThread = new Thread(() => { ThreadFunctions.SendLoop(0, state.client, state.sendPipe, state.sendPending); });
                sendThread.IsBackground = true;
                sendThread.Start();

                // 运行接收循环
                // (接收管道在所有循环中共享)
                ThreadFunctions.ReceiveLoop(0, state.client, MaxMessageSize, state.receivePipe, ReceiveQueueLimit);
            }
            catch (SocketException exception)
            {
                // 如果（例如）IP地址正确但在该IP /端口上没有服务器在运行，则会发生这种情况
                Log.Info("Client Recv: failed to connect to ip=" + ip + " port=" + port + " reason=" + exception);

                // 添加“断开连接”事件以接收管道，以便调用者知道连接失败。 否则他们永远不会知道
                state.receivePipe.Enqueue(0, EventType.Disconnected, default);
            }
            catch (ThreadInterruptedException)
            {
                // expected if Disconnect() aborts it
            }
            catch (ThreadAbortException)
            {
                // expected if Disconnect() aborts it
            }
            catch (ObjectDisposedException)
            {
                // expected if Disconnect() aborts it and disposed the client
                // while ReceiveThread is in a blocking Connect() call
            }
            catch (Exception exception)
            {
                // 出问题了。 可能很重要。
                Log.Error("Client Recv Exception: " + exception);
            }

            // sendthread可能正在等待ManualResetEvent，因此请确保在连接关闭的情况下结束它。
            // 否则，发送线程只会在以下情况下结束在关闭连接时实际发送数据。
            sendThread?.Interrupt();

            // 连接可能已失败。 线程可能已关闭。
            // 无论如何，让我们重置连接状态。
            state.Connecting = false;

            // 如果我们到达这里，那么我们就完成了。 ReceiveLoop已经清理完毕，
            //但是如果连接失败，我们可能永远都收不到。 所以我们也来这里打扫一下。
            state.client?.Close();
        }

        public void Connect(string ip, int port)
        {
            // not if already started
            if (Connecting || Connected)
            {
                Log.Warning("Telepathy Client can not create connection because an existing connection is connecting or connected");
                return;
            }

            // 覆盖旧线程的状态对象。 创建一个新的以避免数据争用，旧的死线程可能仍会修改当前状态！ 修复所有片状测试！
            state = new ClientConnectionState(MaxMessageSize);

            // 从现在开始连接，直到连接成功或失败
            state.Connecting = true;

            // 创建具有完善的IPv4，IPv6和主机名解析支持的TcpClient。
            // * TcpClient(hostname, port): works but would connect (and block)
            //   already
            // * TcpClient(AddressFamily.InterNetworkV6): 接受Ipv4和IPv6地址，但仅连接到IPv6服务器（例如，心灵感应）。 
            //即使启用了DualMode，也不会连接到IPv4服务器（例如Mirror Booster）。
            // * TcpClient(): 在内部创建IPv4套接字，这将迫使Connect（）仅使用IPv4套接字。
            //
            // => 技巧是清除内部IPv4套接字，以便Connect解析主机名并根据需要创建IPv4或IPv6套接字（请参阅TcpClient源代码）
            state.client.Client = null; // clear internal IPv4 socket until Connect()

            // client.Connect(ip, port) is blocking（阻塞）. 让我们在线程中调用它并立即返回。
            // -> 这样，如果连接时间太长，应用程序不会挂起30秒，这在游戏中尤其有用
            // -> 这样我们就不会异步client.BeginConnect，如果我们以太快的速度连接太多的客户端，有时似乎会失败
            state.receiveThread = new Thread(() => {
                ReceiveThreadFunction(state, ip, port, MaxMessageSize, NoDelay, SendTimeout, ReceiveTimeout, ReceiveQueueLimit);
            });
            state.receiveThread.IsBackground = true;
            state.receiveThread.Start();
        }

        public void Disconnect()
        {
            // only if started
            if (Connecting || Connected)
            {
                // dispose all the state safely
                state.Dispose();

                //重要提示：请勿将state设置为null！
                //我们仍然要处理管道的断开连接消息等！
            }
        }

        // 使用套接字连接将消息发送到服务器。 用于免费分配的arraysegment稍后发送。
        // -> the segment's array is only used until Send() returns!
        public bool Send(ArraySegment<byte> message)
        {
            if (Connected)
            {
                // 尊重最大消息大小，以避免分配攻击。
                if (message.Count <= MaxMessageSize)
                {
                    // check send pipe limit
                    if (state.sendPipe.Count < SendQueueLimit)
                    {
                        //添加到线程安全发送管道并立即返回。 呼叫“发送到此处”会被阻止（如果其他侧滞或电线断开，则有时会很长时间）
                        state.sendPipe.Enqueue(message);
                        state.sendPending.Set(); // interrupt SendThread WaitOne()
                        return true;
                    }
                    // 如果发送队列太大，则断开连接。
                    // -> 如果网络速度比输入速度慢，可避免增加队列内存
                    // -> 也避免了日益增长的延迟
                    //
                    // note: 尽管SendThread始终会立即抓住整个发送队列，但发送块可能仍然存在很长时间，
                    //以至于发送队列变得太大。 有限制-安全胜过遗憾。
                    else
                    {
                        // log the reason
                        Log.Warning($"Client.Send: sendPipe reached limit of {SendQueueLimit}. This can happen if we call send faster than the network can process messages. Disconnecting to avoid ever growing memory & latency.");

                        // just close it. send thread will take care of the rest.
                        state.client.Close();
                        return false;
                    }
                }
                Log.Error("Client.Send: message too big: " + message.Count + ". Limit: " + MaxMessageSize);
                return false;
            }
            Log.Warning("Client.Send: not connected!");
            return false;
        }

        // tick: 处理最多“限制”消息
        // => limit参数，以避免死锁/如果服务器或客户端太慢而无法处理网络负载，则冻结时间太长
        // => 无论如何，Mirror＆DOTSNET需要有一个过程限制。 不妨在这里做，让生活更轻松。
        // => 返回要处理的剩余消息量，因此呼叫者可以根据需要（或限制）多次调用tick
        // Tick（）可能处理多个消息，但是如果场景更改消息到达，Mirror需要一种方法来立即停止处理。
        // 在场景更改期间，Mirror无法处理任何其他消息。
        // (可能对其他人也有用)
        // => 确保在运输中仅分配一次lambda
        public int Tick(int processLimit, Func<bool> checkEnabled = null)
        {
            // only if state was created yet (after connect())
            // note: 我们不会选中“仅在已连接的情况下”，因为我们也想在以后继续处理“断开连接”消息！
            if (state == null)
                return 0;

            // process up to 'processLimit' messages
            for (int i = 0; i < processLimit; ++i)
            {
                // 如果启用了“镜像”场景消息，则启用此检查
                if (checkEnabled != null && !checkEnabled())
                    break;

                // 先偷看。 允许我们处理第一个排队的条目，同时通过不删除任何内容来保持池中的byte []有效。
                if (state.receivePipe.TryPeek(out int _, out EventType eventType, out ArraySegment<byte> message))
                {
                    switch (eventType)
                    {
                        case EventType.Connected:
                            OnConnected?.Invoke();
                            break;
                        case EventType.Data:
                            OnData?.Invoke(message);
                            break;
                        case EventType.Disconnected:
                            OnDisconnected?.Invoke();
                            break;
                    }

                    //重要说明：现在，在完成处理事件之后，将队列从队列中取出并返回到池中。
                    state.receivePipe.TryDequeue();
                }
                // no more messages. stop the loop.
                else break;
            }

            // 返回下一次要处理的内容
            return state.receivePipe.TotalCount;
        }
    }
}
