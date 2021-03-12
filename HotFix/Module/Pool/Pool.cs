using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix
{
    /// <summary>
    /// 用于创建单个池对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pool<T> where T : class, new()
    {
        private readonly Stack<T> pool = new Stack<T>();
        //对象数量
        private int liveCount = 0;

        public T Get()
        {
            ++liveCount;
            return (pool.Count > 0) ? (pool.Pop() as T) : new T();
        }

        public void Release(T obj)
        {
            --liveCount;
            pool.Push(obj);
        }

        public void Clear()
        {
            pool.Clear();
        }

        public string Report() { return string.Format("{0}/{1}", liveCount, pool.Count); }
    }
    /// <summary>
    /// 用于创建多个池对象
    /// </summary>
    /// <typeparam name="I"></typeparam>
    public class TypedPool<I>
    {
        private readonly Dictionary<Type, Stack<I>> pools = new Dictionary<Type, Stack<I>>();
        //处于活动状态的实例总数
        private int liveCount = 0;
        public T Get<T>() where T : class, I, new()
        {
            ++liveCount;
            var pool = GetPool(typeof(T));
            return (pool.Count > 0) ? (pool.Pop() as T) : new T();
        }
        public void Release(I obj)
        {
            --liveCount;
            var pool = GetPool(obj.GetType());
            pool.Push(obj);
        }

        public void Clear()
        {
            foreach (var item in pools)
            {
                item.Value.Clear();
            }
            pools.Clear();
        }
        public string Report()
        {
            var c = 0;
            foreach (var t in pools.Values)
            {
                c += t.Count;
            }
            return string.Format("{0}/{1}", liveCount, c);
        }

        private Stack<I> GetPool(Type type)
        {
            if (!pools.ContainsKey(type))
            {
                var pool = new Stack<I>();
                pools[type] = pool;
                return pool;
            }
            return pools[type];
        }
    }
}
