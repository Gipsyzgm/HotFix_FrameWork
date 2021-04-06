// IMPORTANT
// ǿ�������̹߳���Ϊ��̬��
// => Common.Send / ReceiveLoop�ǳ�Σ�գ���Ϊ�����������߳�֮�����⹲��Common״̬��
// => ���̹߳��ܴӾ�̬����Ϊ�Ǿ�̬��ͷ����������Ч���ص����⹲����һ��
// => C�������Զ�������ݾ����� ��õİ취�ǽ������̴߳������뾲̬������������״̬���ݸ�����
// ʹ��size�ж����������ԣ�����ճ���ְ����⡣
// let's even keep them in a STATIC CLASS so it's 100% obvious that this should
// NOT EVER be changed to non static!
using System;
using System.Net.Sockets;
using System.Threading;

namespace Telepathy
{
    public static class ThreadFunctions
    {
        // ʹ��<size��content>��Ϣ�ṹ������Ϣ��ͨ���������˹�����ʱ��������
        // (���� ���ĳ���и��ӳٻ���߱��ж�)
        // -> ��Ч�غ��Ƕ��<< size��content��size��content��...>����
        public static bool SendMessagesBlocking(NetworkStream stream, byte[] payload, int packetSize)
        {
            // stream.Write���ڿͻ��˷���Ƶ�ʺܸ��ҷ�����ֹͣʱ�׳��쳣
            try
            {
                // write the whole thing
                stream.Write(payload, 0, packetSize);
                return true;
            }
            catch (Exception exception)
            {
                // ��¼Ϊ������Ϣ����Ϊ��������ʱ��ر�
                Log.Info("Send: stream.Write exception: " + exception);
                return false;
            }
        }
        // ��ȡ��Ϣ��ͨ��������ֹ�� д��byte []������Ϊ��������д����ֽڡ�
        public static bool ReadMessageBlocking(NetworkStream stream, int MaxMessageSize, byte[] headerBuffer, byte[] payloadBuffer, out int size)
        {
            size = 0;

            // ����������ΪHeader + MaxMessageSize
            if (payloadBuffer.Length != 4 + MaxMessageSize)
            {
                Log.Error($"ReadMessageBlocking: payloadBuffer needs to be of size 4 + MaxMessageSize = {4 + MaxMessageSize} instead of {payloadBuffer.Length}");
                return false;
            }

            // ��ȡ����4���ֽڵı�ͷ��������
            if (!stream.ReadExactly(headerBuffer, 4))
                return false;

            // convert to int
            size = Utils.BytesToIntBigEndian(headerBuffer);

            // ��ֹ���乥���� �����߿����������Ͷ��α��ġ� 2GB��ͷ�����ݰ����Ӷ����·�����������2GB�ֽ����鲢�ľ��ڴ档
            // �����Է�ֹ�ߴ�<= 0��������
            if (size > 0 && size <= MaxMessageSize)
            {
                // ׼ȷ��ȡ���ݵġ���С���ֽڣ�������
                return stream.ReadExactly(payloadBuffer, size);
            }
            Log.Warning("ReadMessageBlocking: possible header attack with a header of: " + size + " bytes.");
            return false;
        }

        // �ͻ��˺ͷ������Ŀͻ��˵��߳̽��չ�����ͬ
        public static void ReceiveLoop(int connectionId, TcpClient client, int MaxMessageSize, MagnificentReceivePipe receivePipe, int QueueLimit)
        {
            // get NetworkStream from client
            NetworkStream stream = client.GetStream();

            // ÿ������ѭ������Ҫ���Լ���HeaderSize + MaxMessageSize���ջ��������Ա�������ʱ���䡣
            // IMPORTANT: DO NOT make this a member, otherwise every connection
            //            on the server would use the same buffer simulatenously
            byte[] receiveBuffer = new byte[4 + MaxMessageSize];

            // ����ͷ�ļ�[4]����
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

                // ��������̸̸��ȡ���ݡ�
                // -> ͨ�����ǻ��Ķ������ܶ�����ݣ�Ȼ����ȡ������յ���һ�����<size��content>��<size��content>��Ϣ�� ��ȷʵ�ܸ��Ӷ��Ұ���
                // -> �෴������ʹ��һ�����ɣ�
                //      Read(2) -> size
                //        Read(size) -> content
                //      repeat
                //    ��ȡ��������״̬�������޹ؽ�Ҫ����Ϊ�ڵȴ���������Ϣ����֮ǰ��õİ취���ǵȴ���
                // => ���������ţ����ݵĽ��������
                //    + no resizing
                //    + no extra allocations, just one for the content
                //    + no crazy extraction logic
                while (true)
                {
                    // �Ķ���һ����Ϣ����������������رգ���ֹͣ
                    if (!ReadMessageBlocking(stream, MaxMessageSize, headerBuffer, receiveBuffer, out int size))
                        // �ж϶����Ƿ��أ��������ر���Ȼ������
                        break;

                    // Ϊ��ȡ����Ϣ����arraysegment
                    ArraySegment<byte> message = new ArraySegment<byte>(receiveBuffer, 0, size);

                    // send to main thread via pipe
                    // -> �������ڲ�������Ϣ��������ǿ��Խ����ջ���������������һ�ζ�ȡ��
                    receivePipe.Enqueue(connectionId, EventType.Data, message);

                    // ������չܵ����ڴ�connectionId̫����Ͽ����ӡ�
                    // -> ��������ٶȱ������ٶ������ɱ������Ӷ����ڴ�
                    // -> �Ͽ����ӷǳ��ʺϸ���ƽ�⡣ �Ͽ�һ�����ӱ�ðÿ������/�����������ķ��ո���
                    if (receivePipe.Count(connectionId) >= QueueLimit)
                    {
                        // log the reason
                        Log.Warning($"receivePipe reached limit of {QueueLimit} for connectionId {connectionId}. This can happen if network messages come in way faster than we manage to process them. Disconnecting this connection for load balancing.");

                        // IMPORTANT: ��������������С� ���Ƕ���������ʹ��һ�����С�
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

                // ��ȷ�Ͽ����Ӻ���ӡ��Ͽ����ӡ���Ϣ��
                // -> ʼ���ڹر���֮����⾺�����������������£�
                //�Ͽ�->�������ӽ��������ã���Ϊ�ڹر���֮ǰ��һС��ʱ���ڣ�Connected��Ϊtrue��
                receivePipe.Enqueue(connectionId, EventType.Disconnected, default);
            }
        }
        // thread send function
        // note: ����ȷʵȷʵ��Ҫÿ������һ������ˣ����һ������������������Խ�������ȡ����
        public static void SendLoop(int connectionId, TcpClient client, MagnificentSendPipe sendPipe, ManualResetEvent sendPending)
        {
            // get NetworkStream from client
            NetworkStream stream = client.GetStream();

            // ������Ч����[packetSize]���䡣 ��С�����������Ҫ��̬���ӡ�
            //
            // IMPORTANT: DO NOT make this a member, otherwise every connection
            //            on the server would use the same buffer simulatenously
            byte[] payload = null;

            try
            {
                while (client.Connected) // try this. client will get closed eventually.
                {
                    // ��ִ�������κβ���֮ǰ��������ManualResetEvent�� ������û�б��������� ����ڴ��ڼ��ٴε���Send���������´ν�������ȷ��⵽��
                    // -> ���򣬿����ڳ��Ӻ���.Reset֮ǰ��������Send���⽫��ȫ��������ֱ����һ��Send����Ϊֹ��
                    sendPending.Reset(); // WaitOne() blocks until .Set() again

                    // ���Ӳ����л�����
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
                // ��������߳��жϷ����̻߳ᷢ����
            }
            catch (Exception exception)
            {
                // �������ˡ� �߳��жϻ����ӹرգ��������ǹر����Լ������ӣ�����...
                // -> �������ַ�ʽ�����Ƕ�Ӧ�����ŵ�ͣ����
                Log.Info("SendLoop Exception: connectionId=" + connectionId + " reason: " + exception);
            }
            finally
            {
                // ���������δ����Ӧ�������۷���ʲô���ݶ����յ�SocketExceptions��
                // ������-����������£�����Ӧ�ùر����ӣ���ᵼ��ReceiveLoop����������Disconnected��Ϣ��
                // ����ʹ�����޷��ٷ��ͣ�����Ҳ����Զ���ֻ״̬��
                stream.Close();
                client.Close();
            }
        }
    }
}