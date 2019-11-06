using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41
{
    public sealed class ObservableList<T> : IList<T>, INotifyChanged<T>
    {
        private List<T> InnerList;
        public event NotifyChangedEventHandler<T>? BeforeCollectionChanged;
        public event NotifyChangedEventHandler<T>? CollectionChanged;

        public ObservableList()
        {
            InnerList = new List<T>();
        }
        public ObservableList(IEnumerable<T> items)
        {
            InnerList = new List<T>(items);
        }

        public T this[int index]
        {
            get
            {
                return InnerList[index];
            }
            set
            {
                T item = InnerList[index];

                if (item is null && value is null)
                    return;

                if (item is null || value is null || !item.Equals(value))
                {
                    NotifyChangedEventArgs<T> eventArgs = new NotifyChangedEventArgs<T>(NotifyCollectionChangedAction.Replace, value, InnerList[index], index);
                    BeforeCollectionChanged?.Invoke(this, eventArgs);
                    InnerList[index] = value;
                    CollectionChanged?.Invoke(this, eventArgs);
                }
            }
        }
        public int Count => InnerList.Count;
        public bool IsReadOnly => false;
        public void Add(T item)
        {
            NotifyChangedEventArgs<T> eventArgs = new NotifyChangedEventArgs<T>(NotifyCollectionChangedAction.Add, item);
            BeforeCollectionChanged?.Invoke(this, eventArgs);
            InnerList.Add(item);
            CollectionChanged?.Invoke(this, eventArgs);
        }
        public void Clear()
        {
            NotifyChangedEventArgs<T> eventArgs = new NotifyChangedEventArgs<T>(NotifyCollectionChangedAction.Reset);
            BeforeCollectionChanged?.Invoke(this, eventArgs);
            InnerList.Clear();
            CollectionChanged?.Invoke(this, eventArgs);
        }
        public bool Contains(T item)
        {
            return InnerList.Contains(item);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            InnerList.CopyTo(array, arrayIndex);
        }
        public IEnumerator<T> GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }
        public int IndexOf(T item)
        {
            return InnerList.IndexOf(item);
        }
        public void Insert(int index, T item)
        {
            NotifyChangedEventArgs<T> eventArgs = new NotifyChangedEventArgs<T>(NotifyCollectionChangedAction.Add, item, index);
            BeforeCollectionChanged?.Invoke(this, eventArgs);
            InnerList.Insert(index, item);
            CollectionChanged?.Invoke(this, eventArgs);
        }
        public bool Remove(T item)
        {
            NotifyChangedEventArgs<T> eventArgs = new NotifyChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, item);
            BeforeCollectionChanged?.Invoke(this, eventArgs);
            bool result = InnerList.Remove(item);
            CollectionChanged?.Invoke(this, eventArgs);
            return result;
        }
        public void RemoveAt(int index)
        {
            NotifyChangedEventArgs<T> eventArgs = new NotifyChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, InnerList[index], index);
            BeforeCollectionChanged?.Invoke(this, eventArgs);
            InnerList.RemoveAt(index);
            CollectionChanged?.Invoke(this, eventArgs);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }
    }
}
