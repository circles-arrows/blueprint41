using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Blueprint41.Core
{
    public class AtomicDictionary<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : notnull
    {
        private object sync = new object();
        private IDictionary<TKey, TValue> dict;

        public AtomicDictionary()
        {
            dict = new Dictionary<TKey, TValue>();
        }
        public AtomicDictionary(IEqualityComparer<TKey> comparer)
        {
            dict = new Dictionary<TKey, TValue>(comparer);
        }
        public AtomicDictionary(IDictionary<TKey, TValue> dictionary)
        {
            dict = new Dictionary<TKey, TValue>(dictionary);
        }
        public AtomicDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            dict = new Dictionary<TKey, TValue>(dictionary, comparer);
        }

        private void Write(Action<IDictionary<TKey, TValue>> logic)
        {
            lock (sync)
            {
                IDictionary<TKey, TValue> newDict = new Dictionary<TKey, TValue>(dict);
                logic.Invoke(newDict);

                Interlocked.Exchange(ref dict, newDict);
            }
        }
        private T Write<T>(Func<IDictionary<TKey, TValue>, T> logic)
        {
            lock (sync)
            {
                IDictionary<TKey, TValue> newDict = new Dictionary<TKey, TValue>(dict);
                T retval = logic.Invoke(newDict);

                Interlocked.Exchange(ref dict, newDict);

                return retval;
            }
        }

        public TValue this[TKey key]
        {
            get => dict[key];
            set => Write(dict => dict[key] = value);
        }
        public ICollection<TKey> Keys => dict.Keys;
        public ICollection<TValue> Values => dict.Values;
        public int Count => dict.Count;
        public bool IsReadOnly => false;
        public void Add(TKey key, TValue value) => Write(dict => dict.Add(key, value));
        public void Add(KeyValuePair<TKey, TValue> item) => Write(dict => dict.Add(item));
        public bool Contains(KeyValuePair<TKey, TValue> item) => dict.Contains(item);
        public bool ContainsKey(TKey key) => dict.ContainsKey(key);
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => dict.CopyTo(array, arrayIndex);
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => dict.GetEnumerator();
        public bool Remove(TKey key) => Write(dict => dict.Remove(key));
        public bool Remove(KeyValuePair<TKey, TValue> item) => Write(dict => dict.Remove(item));
        public bool TryGetValue(TKey key, out TValue value) => dict.TryGetValue(key, out value);
        IEnumerator IEnumerable.GetEnumerator() => dict.GetEnumerator();

        public void Clear()
        {
            lock (sync)
            {
                IDictionary<TKey, TValue> newDict = new Dictionary<TKey, TValue>(((Dictionary<TKey, TValue>)dict).Comparer);
                Interlocked.Exchange(ref dict, newDict);
            }
        }
        private void Add(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            lock (sync)
            {
                IDictionary<TKey, TValue> newDict = new Dictionary<TKey, TValue>(dict);
                foreach (KeyValuePair<TKey, TValue> kvp in items)
                    if (!newDict.ContainsKey(kvp.Key))
                        newDict.Add(kvp.Key, kvp.Value);

                Interlocked.Exchange(ref dict, newDict);
            }
        }
        public void Remove(IEnumerable<TKey> keys)
        {
            lock (sync)
            {
                IDictionary<TKey, TValue> newDict = new Dictionary<TKey, TValue>(dict);
                foreach (TKey key in keys)
                    if (!newDict.ContainsKey(key))
                        newDict.Remove(key);

                Interlocked.Exchange(ref dict, newDict);
            }
        }

        public TValue TryGetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            TValue value;
            if (!dict.TryGetValue(key, out value))
            {
                lock (sync)
                {
                    if (!dict.TryGetValue(key, out value))
                    {
                        IDictionary<TKey, TValue> newDict = new Dictionary<TKey, TValue>(dict);
                        value = valueFactory.Invoke(key);
                        newDict.Add(key, value);

                        Interlocked.Exchange(ref dict, newDict);
                    }
                }
            }
            return value;
        }
        public TValue TryGetOrAdd(TKey key, Func<TKey, IEnumerable<KeyValuePair<TKey, TValue>>> valueFactory)
        {
            TValue value;
            if (!dict.TryGetValue(key, out value))
            {
                lock (sync)
                {
                    if (!dict.TryGetValue(key, out value))
                    {
                        IDictionary<TKey, TValue> newDict = new Dictionary<TKey, TValue>(dict);
                        foreach (KeyValuePair<TKey, TValue> kvp in valueFactory.Invoke(key))
                            if (!newDict.ContainsKey(kvp.Key))
                                newDict.Add(kvp.Key, kvp.Value);

                        Interlocked.Exchange(ref dict, newDict);

                        return newDict[key];
                    }
                }
            }
            return value;
        }
    }
}
