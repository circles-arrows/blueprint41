using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41
{
    public sealed class ObservableList<T> : IList<T>, INotifyCollectionChanged
    {
        private List<T> InnerList;
        public event NotifyCollectionChangedEventHandler BeforeCollectionChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

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
                if ((object)InnerList[index] != (object)value)
                {
                    NotifyCollectionChangedEventArgs eventArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, (object)InnerList[index], index);
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
            NotifyCollectionChangedEventArgs eventArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item);
            BeforeCollectionChanged?.Invoke(this, eventArgs);
            InnerList.Add(item);
            CollectionChanged?.Invoke(this, eventArgs);
        }

        public void Clear()
        {
            NotifyCollectionChangedEventArgs eventArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
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
            NotifyCollectionChangedEventArgs eventArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index);
            BeforeCollectionChanged?.Invoke(this, eventArgs);
            InnerList.Insert(index, item);
            CollectionChanged?.Invoke(this, eventArgs);
        }

        public bool Remove(T item)
        {
            NotifyCollectionChangedEventArgs eventArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item);
            BeforeCollectionChanged?.Invoke(this, eventArgs);
            bool result = InnerList.Remove(item);
            CollectionChanged?.Invoke(this, eventArgs);
            return result;
        }

        public void RemoveAt(int index)
        {
            NotifyCollectionChangedEventArgs eventArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, InnerList[index], index);
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
