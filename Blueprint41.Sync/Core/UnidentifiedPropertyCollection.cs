using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Sync.Core
{
    public class UnidentifiedPropertyCollection : IDictionary<string, object?>
    {
        internal UnidentifiedPropertyCollection(OGM parent)
        {
            Parent = (OGMImpl)parent;
        }

        public UnidentifiedPropertyCollection(UnidentifiedPropertyCollection source)
        {
            Parent = source.Parent;
            foreach (KeyValuePair<string, object?> item in source.UnidentifiedBacking)
                AddInternal(item.Key, item.Value);
        }

        private Dictionary<string, object?> UnidentifiedBacking = new Dictionary<string, object?>();
        private OGMImpl Parent;

        internal void ForEachInternal(Action<KeyValuePair<string, object?>> action)
        {
            foreach (KeyValuePair<string, object?> item in UnidentifiedBacking)
                action.Invoke(item);
        }
        internal void AddInternal(string key, object? value)
        {
            UnidentifiedBacking.Add(key, value);
        }
        internal void ClearInternal()
        {
            UnidentifiedBacking.Clear();
        }

        private object? RepairList(object? value)
        {
            if (value is IList list)
            {
                for (int index = list.Count - 1; index >= 0; index--)
                {
                    if (list[index] is null)
                        list.RemoveAt(index);
                }
            }

            return value;
        }

        #region IDictionary<string, object>

        public object? this[string key]
        {
            get
            {
                Parent.LazyGet();
                return UnidentifiedBacking[key];
            }

            set
            {
                Parent.LazySet();
                UnidentifiedBacking[key] = RepairList(value);
            }
        }

        public int Count
        {
            get
            {
                Parent.LazyGet();
                return UnidentifiedBacking.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                Parent.LazyGet();
                return UnidentifiedBacking.Keys;
            }
        }

        public ICollection<object?> Values
        {
            get
            {
                Parent.LazyGet();
                return UnidentifiedBacking.Values;
            }
        }

        public void Add(KeyValuePair<string, object?> item)
        {
            Parent.LazySet();
            UnidentifiedBacking.Add(item.Key, RepairList(item.Value));
        }

        public void Add(string key, object? value)
        {
            Parent.LazySet();
            UnidentifiedBacking.Add(key, RepairList(value));
        }

        public void Clear()
        {
            Parent.LazySet();
            UnidentifiedBacking.Clear();
        }

        public bool Contains(KeyValuePair<string, object?> item)
        {
            return UnidentifiedBacking.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            Parent.LazyGet();
            return UnidentifiedBacking.ContainsKey(key);
        }

        [Obsolete("This method is not supported.", true)]
        public void CopyTo(KeyValuePair<string, object?>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, object?>> GetEnumerator()
        {
            Parent.LazyGet();
            return UnidentifiedBacking.GetEnumerator();
        }

        [Obsolete("Please use \"public bool Remove(string key)\" instead.", true)]
        public bool Remove(KeyValuePair<string, object?> item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            Parent.LazySet();
            return UnidentifiedBacking.Remove(key);
        }

        public bool TryGetValue(string key, out object? value)
        {
            Parent.LazyGet();
            return UnidentifiedBacking.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Parent.LazyGet();
            return UnidentifiedBacking.GetEnumerator();
        }

        #endregion
    }
}
