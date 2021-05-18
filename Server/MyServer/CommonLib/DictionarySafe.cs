using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CommonLib
{
    public class DictionarySafe<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, TValue> d = new ConcurrentDictionary<TKey, TValue>();
        public void Add(TKey key, TValue value)
        {
            d.TryAdd(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return d.ContainsKey(key);
        }

        public ICollection<TKey> Keys => d.Keys;

        public bool Remove(TKey key)
        {
            TValue val;
            return d.TryRemove(key, out val);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return d.TryGetValue(key, out value);
        }

        public ICollection<TValue> Values => d.Values;


        public TValue this[TKey key]
        {
            get => d[key];
            set => d[key] = value;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)d).Add(item);
        }

        public void Clear()
        {
            d.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)d).Contains(item);
        }


        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)d).CopyTo(array, arrayIndex);
        }

        public int Count => d.Count;

        public bool IsReadOnly => false;

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)d).Remove(item);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)d).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)d).GetEnumerator();
        }

        public void AddOrUpdate(TKey key, TValue value)
        {
            d.AddOrUpdate(key, value, (k, v) => value);
        }
    }
}