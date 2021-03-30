// Net 4.X具有ConcurrentQueue，但是ConcurrentQueue没有TryDequeueAll方法，这使SafeQueue的发送线程速度提高了一倍。
// uMMORPG 450 CCU
//   SafeQueue:       900-1440ms latency
//   ConcurrentQueue:     2000ms latency
// 在LoadTest项目中也很明显，该项目几乎无法使用ConcurrentQueue处理300个CCU！
using System.Collections.Generic;

namespace Telepathy
{
    public class SafeQueue<T>
    {
        readonly Queue<T> queue = new Queue<T>();

        // 用于统计。 不要调用Count并假设在调用后它是相同的。
        public int Count
        {
            get
            {
                lock (queue)
                {
                    return queue.Count;
                }
            }
        }

        public void Enqueue(T item)
        {
            lock (queue)
            {
                queue.Enqueue(item);
            }
        }

        // 不能在做Dequeue之前检查.Count，因为它之间可能会发生变化，因此我们需要一个TryDequeue
        public bool TryDequeue(out T result)
        {
            lock (queue)
            {
                result = default(T);
                if (queue.Count > 0)
                {
                    result = queue.Dequeue();
                    return true;
                }
                return false;
            }
        }

        // 当我们想要出队并立即将其全部删除而无需锁定每个TryDequeue时。
        public bool TryDequeueAll(out T[] result)
        {
            lock (queue)
            {
                result = queue.ToArray();
                queue.Clear();
                return result.Length > 0;
            }
        }

        public void Clear()
        {
            lock (queue)
            {
                queue.Clear();
            }
        }
    }
}
