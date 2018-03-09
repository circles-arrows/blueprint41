using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Core
{
    public class FastDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private Dictionary<TKey, TValue> dict;
        private SortedList<TKey, TValue> list;

        public FastDictionary()
        {
            dict = new Dictionary<TKey, TValue>();
            list = new SortedList<TKey, TValue>();
        }
        public FastDictionary(IDictionary<TKey, TValue> dictionary)
        {
            dict = new Dictionary<TKey, TValue>(dictionary);
            list = new SortedList<TKey, TValue>(dictionary);
        }

        public TValue this[TKey key]
        {
            get { return dict[key]; }
            set { dict[key] = value; list [key] = value; }
        }
        public ICollection<TKey> Keys => list.Keys;
        public ICollection<TValue> Values => list.Values;
        public int Count => list.Count;
        public bool IsReadOnly => false;
        public void Add(TKey key, TValue value)
        {
            dict.Add(key,value);
            list.Add(key, value);
        }
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            dict.Add(item.Key, item.Value);
            list.Add(item.Key, item.Value);
        }
        public void Clear()
        {
            dict.Clear();
            list.Clear();
        }
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            TValue value;
            if (!dict.TryGetValue(item.Key, out value))
                return false;

            if ((object)item.Value == null && (object)value == null)
                return true;

            if ((object)item.Value == null || (object)value == null)
                return false;

            return item.Value.Equals(value);
        }
        public bool ContainsKey(TKey key)
        {
            return dict.ContainsKey(key);
        }
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return list.GetEnumerator();
        }
        public bool Remove(TKey key)
        {
            return dict.Remove(key) | list.Remove(key);
        }
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (Contains(item))
                return Remove(item.Key);
            else
                return false;
        }
        public bool TryGetValue(TKey key, out TValue value)
        {
            return dict.TryGetValue(key, out value);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
