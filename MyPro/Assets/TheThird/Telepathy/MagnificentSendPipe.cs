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
        // 消息队列
        // 并发队列分配. lock{} instead.
        // -> 字节数组始终为MaxMessageSize
        // -> ArraySegment指示实际的消息内容
        // IMPORTANT: lock{} all usages!
        readonly Queue<ArraySegment<byte>> queue = new Queue<ArraySegment<byte>>();

        // byte []池以避免分配
        // Take＆Return精美地封装在管道中。 外部无需担心任何事情，并且可以轻松进行测试。
        // IMPORTANT: lock{} all usages!
        Pool<byte[]> pool;

        // constructor
        public MagnificentSendPipe(int MaxMessageSize)
        {
            // 初始化池以每次创建最大消息大小字节
            pool = new Pool<byte[]>(() => new byte[MaxMessageSize]);
        }

        // 用于统计。 不要调用Count并假设在调用后它是相同的。
        public int Count
        {
            get { lock (this) { return queue.Count; } }
        }

        // pool count for testing
        public int PoolCount
        {
            get { lock (this) { return pool.Count(); } }
        }

        // 入队消息
        // 用于免费分配的arraysegment稍后发送。
        // -> the segment's array is only used until Enqueue() returns!
        public void Enqueue(ArraySegment<byte> message)
        {
            // pool & queue usage always needs to be locked
            lock (this)
            {
                // ArraySegment数组仅在返回之前才有效，因此将其复制到byte []中，我们可以安全地将其排队。
                // 首先从池中获取一个以避免分配
                byte[] bytes = pool.Take();

                // copy into it
                Buffer.BlockCopy(message.Array, message.Offset, bytes, 0, message.Count);

                // indicate which part is the message
                ArraySegment<byte> segment = new ArraySegment<byte>(bytes, 0, message.Count);

                // now enqueue it
                queue.Enqueue(segment);
            }
        }

        // 发送线程需要使每个byte []出队并将其写入 socket
        // -> 使一个字节[]另一个队列出队列工作，但是比立即使所有字节出队列慢（仅锁定一次）
        //    lock {}和DequeueAll比ConcurrentQueue快得多，并且一个接一个地出队：
        //
        //      uMMORPG 450 CCU
        //        SafeQueue:       900-1440ms latency
        //        ConcurrentQueue:     2000ms latency
        //
        // -> 最明显的解决方案是只返回一个包含所有byte []（分配）的列表，然后将每个列表写入套接字
        // -> 一种更快的解决方案是将每个序列化到一个有效负载缓冲区中，并将其仅传递给套接字一次。 更少的套接字调用总能带来更好的CPU性能（！）
        // -> 为了避免每次分配新的条目列表，我们已经在这里简单地将所有条目序列化到有效负载中
        // => 将所有这些复杂性内置到管道中使测试和修改算法超级容易！
        //
        // IMPORTANT: 在这里进行序列化将使我们稍后可以将byte []条目返回到池中，以完全避免分配！
        public bool DequeueAndSerializeAll(ref byte[] payload, out int packetSize)
        {
            // pool & queue usage always needs to be locked
            lock (this)
            {
                // do nothing if empty
                packetSize = 0;
                if (queue.Count == 0)
                    return false;

                // 我们可能有多个待处理邮件。 合并为一个数据包，以避免TCP开销并提高性能。
                //
                // IMPORTANT: Mirror & DOTSNET already batch into MaxMessageSize
                //            chunks, but we STILL pack all pending messages
                //            into one large payload so we only give it to TCP
                //            ONCE. This is HUGE for performance so we keep it!
                packetSize = 0;
                foreach (ArraySegment<byte> message in queue)
                    packetSize += 4 + message.Count; // header + content

                // 如果尚未创建有效负载缓冲区，或者前一个缓冲区太小，则创建它
                // IMPORTANT: payload.Length might be > packetSize! don't use it!
                if (payload == null || payload.Length < packetSize)
                    payload = new byte[packetSize];

                // dequeue all byte[] messages and serialize into the packet
                int position = 0;
                while (queue.Count > 0)
                {
                    // dequeue
                    ArraySegment<byte> message = queue.Dequeue();

                    // 将标头（大小）写入缓冲区中的位置（增加Size）
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