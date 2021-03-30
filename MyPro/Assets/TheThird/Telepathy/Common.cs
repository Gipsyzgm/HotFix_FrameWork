// common code used by server and client
using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;

namespace Telepathy
{
    public abstract class Common
    {
        // 通用代码 /////////////////////////////////////////////////////////
        // <connectionId，消息>的传入消息队列
        //（不是HashSet，因为一个连接可以包含多个新消息）
        protected ConcurrentQueue<Message> receiveQueue = new ConcurrentQueue<Message>();

        // 队列计数，对于调试/基准测试很有用
        public int ReceiveQueueCount => receiveQueue.Count;

        // 如果消息队列太大，则发出警告
        // 如果平均消息约为20个字节，则：
        // -   1k messages are   20KB
        // -  10k messages are  200KB
        // - 100k messages are 1.95MB
        // 2MB不是很多，但是如果调用者进程不能比传入消息更快地调用“GetNextMessage”，那么这是一个不好的信号。
        public static int messageQueueSizeWarning = 100000;

        // 从消息队列中删除并返回最旧的消息。
        // （可能要调用它，直到它不再返回任何内容为止）
        // -> 连接，数据，断开连接事件均在此处添加
        // -> 布尔返回使同时（GetMessage（out Message））更容易！
        // -> 没有“is client connected”检查，因为我们仍然想在断开连接后阅读已断开连接的消息
        public bool GetNextMessage(out Message message)
        {
            return receiveQueue.TryDequeue(out message);
        }

        //NoDelay禁用nagle算法。 降低CPU％和延迟，但增加带宽
        public bool NoDelay = true;

        // 防止分配攻击。 每个数据包都以一个长度报头作为前缀，
        // 因此攻击者可以发送一个长度为2GB的虚假数据包，从而导致服务器分配2GB并迅速耗尽内存。
        // -> 如果您要发送更大的文件，只需增加最大数据包大小即可！
        // -> 每个Message16KB应该足够了。
        public int MaxMessageSize = 100 * 1024;

        //如果在发送过程中网络断开，发送将永远停止，因此我们需要超时（以毫秒为单位）
        public int SendTimeout = 5000;

        // 避免分配header [4]，但不要为所有线程使用一个缓冲区
        [ThreadStatic] static byte[] header;

        // 避免有效负载[packetSize]分配，但不要对所有线程使用一个缓冲区
        [ThreadStatic] static byte[] payload;

        // static helper functions /////////////////////////////////////////////
        // 使用<size，content>消息结构发送消息（通过流），此功能有时会阻塞！
        // （例如，如果有人等待时间较长或电线被切断）
        protected static bool SendMessagesBlocking(NetworkStream stream, byte[][] messages)
        {
            // 如果客户端发送频率很高并且服务器停止，则stream.Write会引发异常
            try
            {
                // 我们可能有多个待处理邮件。 合并为一个数据包，以避免TCP开销并提高性能。
                // 运算符sizeof是用来求得某种类型（例如sizeof(double))或某个变量在内存中占有的字节数
                int packetSize = 0;
                for (int i = 0; i < messages.Length; ++i)
                    packetSize += sizeof(int) + messages[i].Length; // header + content

                // 如果尚未创建有效负载缓冲区，或者前一个缓冲区太小，则创建它
                // IMPORTANT: payload.Length might be > packetSize! don't use it!
                if (payload == null || payload.Length < packetSize)
                    payload = new byte[packetSize];

                // 创建数据包
                int position = 0;
                for (int i = 0; i < messages.Length; ++i)
                {
                    // 如果尚未创建，则创建头缓冲区
                    if (header == null)
                        header = new byte[4];

                    // construct header (size)
                    //Utils.IntToBytesBigEndianNonAlloc(messages[i].Length, header);
                    header = BitConverter.GetBytes(messages[i].Length);                
                    // 将标头+消息复制到缓冲区
                    Array.Copy(header, 0, payload, position, header.Length);
                    //例如： String[] arr = { "A", "B", "C", "D", "E", "F" };
                    //System.arraycopy(arr, 3, arr, 2, 2);
                    //从下标为3的位置开始复制，复制的长度为2(复制D、E)，从下标为2的位置开始替换为D、E
                    //复制后的数组为: String[] arr = { "A", "B", "D", "E", "E", "F" };
                    Array.Copy(messages[i], 0, payload, position + header.Length, messages[i].Length);
                    position += header.Length + messages[i].Length;
                }
                // write the whole thing
                stream.Write(payload, 0, packetSize);

                return true;
            }
            catch (Exception exception)
            {
                //记录为常规消息，因为服务器有时会关闭
                Logger.Log("Send: stream.Write exception: " + exception);
                return false;
            }
        }
        // 使用<size，content>消息结构读取消息（通过流）
        protected static bool ReadMessageBlocking(NetworkStream stream, int MaxMessageSize, out byte[] content)
        {
            content = null;

            // 如果尚未创建，则创建头缓冲区
            if (header == null)
                header = new byte[4];

            // 读取正好4个字节的标头（阻塞）
            if (!stream.ReadExactly(header, 4))
                return false;

            //转换为int
            //int size = Utils.BytesToIntBigEndian(header);
            int size = BitConverter.ToInt32(header,0);

            // 防止分配攻击。 攻击者可能连续发送多个伪造的“ 2GB标头”数据包，从而导致服务器分配多个2GB字节数组并耗尽内存。
            if (size <= MaxMessageSize)
            {
                // read exactly 'size' bytes for content (blocking)
                content = new byte[size];
                return stream.ReadExactly(content, size);
            }
            Logger.LogWarning("ReadMessageBlocking: possible allocation attack with a header of: " + size + " bytes.");
            return false;
        }

        // 客户端和服务器的客户端的线程接收功能相同
        // （静态以减少状态以获得最大的可靠性）
        protected static void ReceiveLoop(int connectionId, TcpClient client, ConcurrentQueue<Message> receiveQueue, int MaxMessageSize)
        {
            // 从客户端获取NetworkStream
            NetworkStream stream = client.GetStream();

            // 跟踪最后一条消息队列警告
            DateTime messageQueueLastWarning = DateTime.Now;

            // 绝对必须用try / catch换行，否则线程异常是静默的
            try
            {
                // 将连接的事件添加到以IP地址作为数据的队列，以备不时之需
                receiveQueue.Enqueue(new Message(connectionId, EventType.Connected, null));

                // let's talk about reading data.
                // -> 通常我们会阅读尽可能多的内容，然后提取与这次收到的一样多的<size，content>，
                //<size，content>消息。 这确实很复杂而且昂贵
                // -> 相反，我们使用一个技巧：
                //      Read(2) -> size
                //        Read(size) -> content
                //      repeat
                //    读取处于阻塞状态，但这并不重要，因为最好等到完整的消息到达后再等待。
                // => 这是最优雅，最快捷的解决方案。
                //    + 不可调整大小
                //    + 没有额外的分配，只分配一个内容
                //    + 没有疯狂的提取逻辑
                while (true)
                {
                    // 阅读下一条消息（阻塞）；如果流关闭，则停止
                    byte[] content;
                    if (!ReadMessageBlocking(stream, MaxMessageSize, out content))
                        break; // 中断而不是返回，所以流关闭仍然发生！

                    // 排队
                    receiveQueue.Enqueue(new Message(connectionId, EventType.Data, content));

                    // 如果队列太大，则显示警告
                    // -> 我们不想每次都显示警告，因为这样会浪费大量处理能力来记录日志，这会使队列堆积得更多。
                    // -> 相反，我们每10秒钟显示一次，以便系统可以使用其最大的处理能力来希望对其进行处理。
                    if (receiveQueue.Count > messageQueueSizeWarning)
                    {
                        TimeSpan elapsed = DateTime.Now - messageQueueLastWarning;
                        if (elapsed.TotalSeconds > 10)
                        {
                            Logger.LogWarning("ReceiveLoop: messageQueue is getting big(" + receiveQueue.Count + "), try calling GetNextMessage more often. You can call it more than once per frame!");
                            messageQueueLastWarning = DateTime.Now;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                // 出了些问题。 线程被中断或连接被关闭，或者我们关闭了自己的连接或...
                // -> 无论哪种方式，我们都应该优雅地停下来
                Logger.Log("ReceiveLoop: finished receive function for connectionId=" + connectionId + " reason: " + exception);
            }
            finally
            {
                // clean up no matter what
                stream.Close();
                client.Close();

                // 正确断开连接后，添加“断开连接”消息。
                // -> 始终在关闭流之后避免竞争情况，在这种情况下，断开->重新连接将不起作用，
                // 因为在关闭流之前的一小段时间内，Connected仍为true。
                receiveQueue.Enqueue(new Message(connectionId, EventType.Disconnected, null));
            }
        }

        // 线程发送功能
        // 注意：我们真的需要每个连接一个，所以如果一个连接阻塞，其余的仍将继续获取发送
        protected static void SendLoop(int connectionId, TcpClient client, SafeQueue<byte[]> sendQueue, ManualResetEvent sendPending)
        {
            // 从客户端获取NetworkStream
            NetworkStream stream = client.GetStream();

            try
            {
                while (client.Connected) // 试试这个。 客户最终将被关闭。
                {
                    // 在执行其他任何操作之前，请重置ManualResetEvent。 这样就没有比赛条件。 
                    // 如果在此期间再次调用Send（），则下次将可以正确检测到它
                    // -> 否则，可能在出队后但在.Reset之前立即调用Send，这将完全忽略它，直到下一个Send调用为止。
                    sendPending.Reset(); // WaitOne() blocks until .Set() again

                    // 全部出队
                    // SafeQueue.TryDequeueAll is twice as fast as
                    // ConcurrentQueue, see SafeQueue.cs!
                    byte[][] messages;
                    if (sendQueue.TryDequeueAll(out messages))
                    {
                        // 发送消息（阻塞），或者如果流关闭则停止
                        if (!SendMessagesBlocking(stream, messages))
                            break; // 中断而不是返回，所以流关闭仍然发生！
                    }

                    // 不要阻塞CPU：等到队列不再为空
                    sendPending.WaitOne();
                }
            }
            catch (ThreadAbortException)
            {
                // 发生在停止。 不要记录任何东西。
            }
            catch (ThreadInterruptedException)
            {
                // 如果接收线程中断发送线程会发生。
            }
            catch (Exception exception)
            {
                // 出了些问题。 线程被中断或连接被关闭，或者我们关闭了自己的连接或...
                // -> 无论哪种方式，我们都应该优雅地停下来
                Logger.Log("SendLoop Exception: connectionId=" + connectionId + " reason: " + exception);
            }
            finally
            {
                // clean up no matter what
                // 如果“主机未能响应”，则在发送时我们可能会收到SocketExceptions-在这种情况下，
                // 我们应该关闭连接，这会导致ReceiveLoop结束并触发Disconnected消息。 
                // 否则即使我们无法再发送，连接也将永远保持活动状态。
                stream.Close();
                client.Close();
            }
        }
    }
}
