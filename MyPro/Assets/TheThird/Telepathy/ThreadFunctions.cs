// IMPORTANT
// 强制所有线程功能为静态。
// => Common.Send / ReceiveLoop非常危险，因为它很容易在线程之间意外共享Common状态。
// => 将线程功能从静态更改为非静态后，头缓冲区，有效负载等意外共享了一次
// => C＃不会自动检测数据竞争。 最好的办法是将所有线程代码移入静态函数并将所有状态传递给它们
// 使用size判定数据完整性，避免粘包分包问题。
// let's even keep them in a STATIC CLASS so it's 100% obvious that this should
// NOT EVER be changed to non static!
using System;
using System.Net.Sockets;
using System.Threading;

namespace Telepathy
{
    public static class ThreadFunctions
    {
        // 使用<size，content>消息结构发送消息（通过流），此功能有时会阻塞！
        // (例如 如果某人有高延迟或电线被切断)
        // -> 有效载荷是多个<< size，content，size，content，...>部分
        public static bool SendMessagesBlocking(NetworkStream stream, byte[] payload, int packetSize)
        {
            // stream.Write会在客户端发送频率很高且服务器停止时抛出异常
            try
            {
                // write the whole thing
                stream.Write(payload, 0, packetSize);
                return true;
            }
            catch (Exception exception)
            {
                // 记录为常规消息，因为服务器有时会关闭
                Log.Info("Send: stream.Write exception: " + exception);
                return false;
            }
        }
        // 读取消息（通过流）阻止。 写入byte []并返回为避免分配而写入的字节。
        public static bool ReadMessageBlocking(NetworkStream stream, int MaxMessageSize, byte[] headerBuffer, byte[] payloadBuffer, out int size)
        {
            size = 0;

            // 缓冲区必须为Header + MaxMessageSize
            if (payloadBuffer.Length != 4 + MaxMessageSize)
            {
                Log.Error($"ReadMessageBlocking: payloadBuffer needs to be of size 4 + MaxMessageSize = {4 + MaxMessageSize} instead of {payloadBuffer.Length}");
                return false;
            }

            // 读取正好4个字节的标头（阻塞）
            if (!stream.ReadExactly(headerBuffer, 4))
                return false;

            // convert to int
            size = Utils.BytesToIntBigEndian(headerBuffer);

            // 防止分配攻击。 攻击者可能连续发送多个伪造的“ 2GB标头”数据包，从而导致服务器分配多个2GB字节数组并耗尽内存。
            // 还可以防止尺寸<= 0引起问题
            if (size > 0 && size <= MaxMessageSize)
            {
                // 准确读取内容的“大小”字节（阻塞）
                return stream.ReadExactly(payloadBuffer, size);
            }
            Log.Warning("ReadMessageBlocking: possible header attack with a header of: " + size + " bytes.");
            return false;
        }

        // 客户端和服务器的客户端的线程接收功能相同
        public static void ReceiveLoop(int connectionId, TcpClient client, int MaxMessageSize, MagnificentReceivePipe receivePipe, int QueueLimit)
        {
            // get NetworkStream from client
            NetworkStream stream = client.GetStream();

            // 每个接收循环都需要它自己的HeaderSize + MaxMessageSize接收缓冲区，以避免运行时分配。
            // IMPORTANT: DO NOT make this a member, otherwise every connection
            //            on the server would use the same buffer simulatenously
            byte[] receiveBuffer = new byte[4 + MaxMessageSize];

            // 避免头文件[4]分配
            //
            // IMPORTANT: DO NOT make this a member, otherwise every connection
            //            on the server would use the same buffer simulatenously
            byte[] headerBuffer = new byte[4];

            // absolutely must wrap with try/catch, otherwise thread exceptions
            // are silent
            try
            {
                // add connected event to pipe
                receivePipe.Enqueue(connectionId, EventType.Connected, default);

                // 让我们来谈谈读取数据。
                // -> 通常我们会阅读尽可能多的内容，然后提取与这次收到的一样多的<size，content>，<size，content>消息。 这确实很复杂而且昂贵
                // -> 相反，我们使用一个技巧：
                //      Read(2) -> size
                //        Read(size) -> content
                //      repeat
                //    读取处于阻塞状态，但这无关紧要，因为在等待完整的消息到达之前最好的办法就是等待。
                // => 这是最优雅，最快捷的解决方案。
                //    + no resizing
                //    + no extra allocations, just one for the content
                //    + no crazy extraction logic
                while (true)
                {
                    // 阅读下一条消息（阻塞）；如果流关闭，则停止
                    if (!ReadMessageBlocking(stream, MaxMessageSize, headerBuffer, receiveBuffer, out int size))
                        // 中断而不是返回，所以流关闭仍然发生！
                        break;

                    // 为读取的消息创建arraysegment
                    ArraySegment<byte> message = new ArraySegment<byte>(receiveBuffer, 0, size);

                    // send to main thread via pipe
                    // -> 它将在内部复制消息，因此我们可以将接收缓冲区重新用于下一次读取！
                    receivePipe.Enqueue(connectionId, EventType.Data, message);

                    // 如果接收管道对于此connectionId太大，则断开连接。
                    // -> 如果网络速度比输入速度慢，可避免增加队列内存
                    // -> 断开连接非常适合负载平衡。 断开一个连接比冒每个连接/整个服务器的风险更好
                    if (receivePipe.Count(connectionId) >= QueueLimit)
                    {
                        // log the reason
                        Log.Warning($"receivePipe reached limit of {QueueLimit} for connectionId {connectionId}. This can happen if network messages come in way faster than we manage to process them. Disconnecting this connection for load balancing.");

                        // IMPORTANT: 请勿清除整个队列。 我们对所有连接使用一个队列。
                        //receivePipe.Clear();

                        // just break. the finally{} will close everything.
                        break;
                    }
                }
            }
            catch (Exception exception)
            {
                // something went wrong. the thread was interrupted or the
                // connection closed or we closed our own connection or ...
                // -> either way we should stop gracefully
                Log.Info("ReceiveLoop: finished receive function for connectionId=" + connectionId + " reason: " + exception);
            }
            finally
            {
                // clean up no matter what
                stream.Close();
                client.Close();

                // 正确断开连接后，添加“断开连接”消息。
                // -> 始终在关闭流之后避免竞争情况，在这种情况下，
                //断开->重新连接将不起作用，因为在关闭流之前的一小段时间内，Connected仍为true。
                receivePipe.Enqueue(connectionId, EventType.Disconnected, default);
            }
        }
        // thread send function
        // note: 我们确实确实需要每个连接一个，因此，如果一个连接阻塞，其余的仍将继续获取发送
        public static void SendLoop(int connectionId, TcpClient client, MagnificentSendPipe sendPipe, ManualResetEvent sendPending)
        {
            // get NetworkStream from client
            NetworkStream stream = client.GetStream();

            // 避免有效负载[packetSize]分配。 大小会根据批次需要动态增加。
            //
            // IMPORTANT: DO NOT make this a member, otherwise every connection
            //            on the server would use the same buffer simulatenously
            byte[] payload = null;

            try
            {
                while (client.Connected) // try this. client will get closed eventually.
                {
                    // 在执行其他任何操作之前，请重置ManualResetEvent。 这样就没有比赛条件。 如果在此期间再次调用Send（），则下次将可以正确检测到它
                    // -> 否则，可能在出队后但在.Reset之前立即调用Send，这将完全忽略它，直到下一个Send调用为止。
                    sendPending.Reset(); // WaitOne() blocks until .Set() again

                    // 出队并序列化所有
                    // a locked{} TryDequeueAll is twice as fast as
                    // ConcurrentQueue, see SafeQueue.cs!
                    if (sendPipe.DequeueAndSerializeAll(ref payload, out int packetSize))
                    {
                        // send messages (blocking) or stop if stream is closed
                        if (!SendMessagesBlocking(stream, payload, packetSize))
                            // break instead of return so stream close still happens!
                            break;
                    }

                    // don't choke up the CPU: wait until queue not empty anymore
                    sendPending.WaitOne();
                }
            }
            catch (ThreadAbortException)
            {
                // happens on stop. don't log anything.
            }
            catch (ThreadInterruptedException)
            {
                // 如果接收线程中断发送线程会发生。
            }
            catch (Exception exception)
            {
                // 出问题了。 线程中断或连接关闭，或者我们关闭了自己的连接，或者...
                // -> 无论哪种方式，我们都应该优雅地停下来
                Log.Info("SendLoop Exception: connectionId=" + connectionId + " reason: " + exception);
            }
            finally
            {
                // 如果“主机未能响应”，无论发送什么内容都会收到SocketExceptions，
                // 请清理-在这种情况下，我们应该关闭连接，这会导致ReceiveLoop结束并触发Disconnected消息。
                // 否则即使我们无法再发送，连接也将永远保持活动状态。
                stream.Close();
                client.Close();
            }
        }
    }
}