// a magnificent send pipe to shield us from all of life's complexities.
// safely sends messages from main thread to send thread.
// -> thread safety built in
// -> byte[] pooling coming in the future
//
// => hides all the complexity from telepathy
// => easy to switch between stack/queue/concurrentqueue/etc.
// => easy to test

using System;
using System.Collections.Generic;

namespace Telepathy
{
    public class MagnificentSendPipe
    {
        // ��Ϣ����
        // �������з���. lock{} instead.
        // -> �ֽ�����ʼ��ΪMaxMessageSize
        // -> ArraySegmentָʾʵ�ʵ���Ϣ����
        // IMPORTANT: lock{} all usages!
        readonly Queue<ArraySegment<byte>> queue = new Queue<ArraySegment<byte>>();

        // byte []���Ա������
        // Take��Return�����ط�װ�ڹܵ��С� �ⲿ���赣���κ����飬���ҿ������ɽ��в��ԡ�
        // IMPORTANT: lock{} all usages!
        Pool<byte[]> pool;

        // constructor
        public MagnificentSendPipe(int MaxMessageSize)
        {
            // ��ʼ������ÿ�δ��������Ϣ��С�ֽ�
            pool = new Pool<byte[]>(() => new byte[MaxMessageSize]);
        }

        // ����ͳ�ơ� ��Ҫ����Count�������ڵ��ú�������ͬ�ġ�
        public int Count
        {
            get { lock (this) { return queue.Count; } }
        }

        // pool count for testing
        public int PoolCount
        {
            get { lock (this) { return pool.Count(); } }
        }

        // �����Ϣ
        // ������ѷ����arraysegment�Ժ��͡�
        // -> the segment's array is only used until Enqueue() returns!
        public void Enqueue(ArraySegment<byte> message)
        {
            // pool & queue usage always needs to be locked
            lock (this)
            {
                // ArraySegment������ڷ���֮ǰ����Ч����˽��临�Ƶ�byte []�У����ǿ��԰�ȫ�ؽ����Ŷӡ�
                // ���ȴӳ��л�ȡһ���Ա������
                byte[] bytes = pool.Take();

                // copy into it
                Buffer.BlockCopy(message.Array, message.Offset, bytes, 0, message.Count);

                // indicate which part is the message
                ArraySegment<byte> segment = new ArraySegment<byte>(bytes, 0, message.Count);

                // now enqueue it
                queue.Enqueue(segment);
            }
        }

        // �����߳���Ҫʹÿ��byte []���Ӳ�����д�� socket
        // -> ʹһ���ֽ�[]��һ�����г����й��������Ǳ�����ʹ�����ֽڳ���������������һ�Σ�
        //    lock {}��DequeueAll��ConcurrentQueue��ö࣬����һ����һ���س��ӣ�
        //
        //      uMMORPG 450 CCU
        //        SafeQueue:       900-1440ms latency
        //        ConcurrentQueue:     2000ms latency
        //
        // -> �����ԵĽ��������ֻ����һ����������byte []�����䣩���б�Ȼ��ÿ���б�д���׽���
        // -> һ�ָ���Ľ�������ǽ�ÿ�����л���һ����Ч���ػ������У�����������ݸ��׽���һ�Ρ� ���ٵ��׽��ֵ������ܴ������õ�CPU���ܣ�����
        // -> Ϊ�˱���ÿ�η����µ���Ŀ�б������Ѿ�������򵥵ؽ�������Ŀ���л�����Ч������
        // => ��������Щ���������õ��ܵ���ʹ���Ժ��޸��㷨�������ף�
        //
        // IMPORTANT: ������������л���ʹ�����Ժ���Խ�byte []��Ŀ���ص����У�����ȫ������䣡
        public bool DequeueAndSerializeAll(ref byte[] payload, out int packetSize)
        {
            // pool & queue usage always needs to be locked
            lock (this)
            {
                // do nothing if empty
                packetSize = 0;
                if (queue.Count == 0)
                    return false;

                // ���ǿ����ж���������ʼ��� �ϲ�Ϊһ�����ݰ����Ա���TCP������������ܡ�
                //
                // IMPORTANT: Mirror & DOTSNET already batch into MaxMessageSize
                //            chunks, but we STILL pack all pending messages
                //            into one large payload so we only give it to TCP
                //            ONCE. This is HUGE for performance so we keep it!
                packetSize = 0;
                foreach (ArraySegment<byte> message in queue)
                    packetSize += 4 + message.Count; // header + content

                // �����δ������Ч���ػ�����������ǰһ��������̫С���򴴽���
                // IMPORTANT: payload.Length might be > packetSize! don't use it!
                if (payload == null || payload.Length < packetSize)
                    payload = new byte[packetSize];

                // dequeue all byte[] messages and serialize into the packet
                int position = 0;
                while (queue.Count > 0)
                {
                    // dequeue
                    ArraySegment<byte> message = queue.Dequeue();

                    // ����ͷ����С��д�뻺�����е�λ�ã�����Size��
                    Utils.IntToBytesBigEndianNonAlloc(message.Count, payload, position);
                    position += 4;

                    // copy message into payload at position
                    Buffer.BlockCopy(message.Array, message.Offset, payload, position, message.Count);
                    position += message.Count;

                    // return to pool so it can be reused (avoids allocations!)
                    pool.Return(message.Array);
                }

                // we did serialize something
                return true;
            }
        }

        public void Clear()
        {
            // pool & queue usage always needs to be locked
            lock (this)
            {
                // clear queue, but via dequeue to return each byte[] to pool
                while (queue.Count > 0)
                {
                    pool.Return(queue.Dequeue().Array);
                }
            }
        }
    }
}