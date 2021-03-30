using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;

namespace Telepathy
{
    //https://github.com/vis2k/Telepathy
    public class Client : Common
    {
        public TcpClient client;
        Thread receiveThread;
        Thread sendThread;

        // TcpClient.Connected doesn't check if socket != null,
        // 如果连接已关闭，则会导致NullReferenceExceptions。
        // 让我们手动检查
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
        volatile bool _Connecting;
        public bool Connecting => _Connecting;

        // 发送队列
        // => SafeQueue is twice as fast as ConcurrentQueue, see SafeQueue.cs!
        SafeQueue<byte[]> sendQueue = new SafeQueue<byte[]>();

        // ManualResetEvent唤醒发送线程。 比Thread.Sleep更好
        // -> call Set() 如果所有内容都已发送
        // -> call Reset() 如果有东西要再次发送
        // -> call WaitOne() 阻止直到调用Reset
        ManualResetEvent sendPending = new ManualResetEvent(false);

        // the thread function
        void ReceiveThreadFunction(string ip, int port)
        {
            // 绝对必须用try / catch换行，否则线程异常是静默的
            try
            {
                // 连接（阻止）
                client.Connect(ip, port);
                _Connecting = false;

                // set socket options after the socket was created in Connect()
                // (not after the constructor because we clear the socket there)
                client.NoDelay = NoDelay;
                client.SendTimeout = SendTimeout;

                // 连接后才开始发送线程
                sendThread = new Thread(() => { SendLoop(0, client, sendQueue, sendPending); });
                sendThread.IsBackground = true;
                sendThread.Start();

                // 运行接收循环
                ReceiveLoop(0, client, receiveQueue, MaxMessageSize);
            }
            catch (SocketException exception)
            {
                // 如果（例如）IP地址正确但在该IP /端口上没有服务器在运行，则会发生这种情况
                Logger.Log("Client Recv: failed to connect to ip=" + ip + " port=" + port + " reason=" + exception);

                // 将“Disconnected(已断开连接)”事件添加到消息队列中，以便呼叫者知道连接失败。 否则他们永远不会知道
                receiveQueue.Enqueue(new Message(0, EventType.Disconnected, null));
            }
            catch (ThreadInterruptedException)
            {
                // expected if Disconnect() aborts it
            }
            catch (ThreadAbortException)
            {
                // expected if Disconnect() aborts it
            }
            catch (Exception exception)
            {
                // something went wrong. probably important.
                Logger.LogError("Client Recv Exception: " + exception);
            }

            // sendthread可能正在等待ManualResetEvent，因此请确保在连接关闭的情况下结束它。
            // 否则，只有在关闭连接时实际发送数据时，发送线程才会结束。
            sendThread?.Interrupt();

            // 连接可能已失败。 线程可能已关闭。 无论如何，让我们重置连接状态。
            _Connecting = false;

            // 如果我们到达这里，那么我们就完成了。 ReceiveLoop已经清理完毕，但是如果连接失败，
            // 我们可能永远都收不到。 所以我们也来这里打扫一下。
            client?.Close();
        }

        public void Connect(string ip, int port)
        {
            // not if already started
            if (Connecting || Connected)
            {
                Logger.LogWarning("Telepathy Client can not create connection because an existing connection is connecting or connected");
                return;
            }

            // 从现在开始连接，直到连接成功或失败
            _Connecting = true;

            // 创建具有完善的IPv4，IPv6和主机名解析支持的TcpClient。
            // * TcpClient（主机名，端口）：可以，但是已经连接（和阻止）了
            // * TcpClient（AddressFamily.InterNetworkV6）：使用Ipv4和IPv6地址，
            // 但仅连接到IPv6服务器（例如Telepathy）。 即使启用了DualMode，
            // 也无法连接到IPv4服务器（例如Mirror Booster）。
            // * TcpClient（）：在内部创建IPv4套接字，这将强制Connect（）仅使用IPv4套接字。
            // => 技巧是清除内部IPv4套接字，以便Connect解析主机名并根据需要创建IPv4或IPv6套接字（请参阅TcpClient源代码）
            client = new TcpClient(); // creates IPv4 socket
            client.Client = null; // 清除内部IPv4套接字，直到Connect（）

            // 清除队列中的旧邮件，只是要确保呼叫者没有收到上次的数据并且不同步。
            // -> 在“断开连接”中调用此方法并不明智，因为呼叫者之后可能仍想处理所有最新消息
            receiveQueue = new ConcurrentQueue<Message>();
            sendQueue.Clear();

            // client.Connect（ip，port）被阻止。 让我们在线程中调用它并立即返回。
            // -> 这样，如果连接时间太长，应用程序不会挂起30秒，这在游戏中尤其有用
            // -> 这样我们就不会异步client.BeginConnect，如果我们以太快的速度连接太多的客户端，有时似乎会失败
            receiveThread = new Thread(() => { ReceiveThreadFunction(ip, port); });
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        public void Disconnect()
        {
            // only if started
            if (Connecting || Connected)
            {
                // close client
                client.Close();

                // 等到线程完成。 这是确保断开连接后立即可以再次调用Connect（）的唯一方法
                // -> 加入有时会永远等待，例如 尝试连接到dead end同时调用“断开连接”时
                receiveThread?.Interrupt();

                // 我们中断了接收线程，因此我们无法保证连接已重置。 让我们手动进行。
                _Connecting = false;

                // 清除发送队列。 无需坚持。
                // （与receiveQueue不同，后者仍然需要处理最新的Disconnected消息等）
                sendQueue.Clear();

                // 完全放开这个。 线程结束，没有人再使用它，因此Connected立即再次为false。
                client = null;
            }
        }

        public bool Send(byte[] data)
        {
            if (Connected)
            {
                // 尊重最大消息大小，以避免分配攻击。
                if (data.Length <= MaxMessageSize)
                {
                    // 添加发送队列并立即返回。
                    // 呼叫“发送到此处”将被阻止（如果其他侧滞或电线断开，则有时会很长一段时间）
                    sendQueue.Enqueue(data);
                    sendPending.Set(); // 中断SendThread WaitOne()
                    return true;
                }
                Logger.LogError("Client.Send: message too big: " + data.Length + ". Limit: " + MaxMessageSize);
                return false;
            }
            Logger.LogWarning("Client.Send: not connected!");
            return false;
        }
    }
}
