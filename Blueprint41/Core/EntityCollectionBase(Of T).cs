using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Dynamic;
using model = Blueprint41.Neo4j.Model;
using persistence = Blueprint41.Neo4j.Persistence;

namespace Blueprint41.Core
{
    public abstract class EntityCollectionBase<TEntity> : EntityCollectionBase, ICollection<TEntity>, ILookupHelper<TEntity>, IInnerDataCol<TEntity>
        where TEntity : class, OGM
    {
        protected EntityCollectionBase(OGM parent, Property property, Action<TEntity>? eagerLoadLogic) : base(parent, property)
        {
            EagerLoadLogic = eagerLoadLogic;
        }

        protected Action<TEntity>? EagerLoadLogic;

        protected private IObservableList<TEntity>? innerData = null;
        protected IObservableList<TEntity> InnerData
        {
            get
            {
                if (innerData is null)
                    throw new InvalidOperationException("The collection is not initialized, please call InitialLoad first.");

                return innerData;
            }
        }
        private IEnumerable<CollectionItem<TEntity>>? loadedData = null;
        protected IEnumerable<CollectionItem<TEntity>> LoadedData
        {
            get
            {
                if (loadedData is null)
                    throw new InvalidOperationException("The collection is not initialized, please call InitialLoad first.");

                return loadedData;
            }
        }
        public IEnumerable<TEntity> OriginalData { get { LazyLoad(); return LoadedData.Select(item => item.Item); } }
        sealed internal override void InitialLoad(IEnumerable<CollectionItem> items)
        {
            InitialLoad(items.Cast<CollectionItem<TEntity>>());
        }
        protected void InitialLoad(IEnumerable<CollectionItem<TEntity>> items)
        {
            if (IsLoaded)
                return;

            ObservableList<TEntity> tmp = new ObservableList<TEntity>(items);
            tmp.BeforeCollectionChanged += BeforeCollectionChanged;
            innerData = tmp;
            loadedData = tmp;

            IsLoaded = true;
            if (Parent is OGMImpl || (Parent is DynamicEntity && ((DynamicEntity)Parent).ShouldExecute))
                DbTransaction?.Replay(this);
        }
        private void BeforeCollectionChanged(object sender, EventArgs args)
        {
            ObservableList<TEntity> tmp = (ObservableList<TEntity>)InnerData;
            tmp.BeforeCollectionChanged -= BeforeCollectionChanged;
            if (ReferenceEquals(InnerData, LoadedData))
                loadedData = new List<CollectionItem<TEntity>>(InnerData);
        }
        protected virtual void LazySet()
        {
            if (Parent.PersistenceState == PersistenceState.Persisted && DbTransaction != Transaction.RunningTransaction)
                throw new InvalidOperationException("This object was already flushed to the data store.");
            else if (Parent.PersistenceState == PersistenceState.OutOfScope)
                throw new InvalidOperationException("The transaction for this object has already ended.");

            LazyLoad();

            if (Parent.PersistenceState == PersistenceState.New)
                Parent.PersistenceState = PersistenceState.NewAndChanged;
            else if (Parent.PersistenceState == PersistenceState.Loaded)
                Parent.PersistenceState = PersistenceState.LoadedAndChanged;
        }
        internal protected override void AfterFlush()
        {
            loadedData = innerData;
        }

        int ICollection<TEntity>.Count { get { return CountInternal; } }
        private protected abstract int CountInternal { get; }
        public void Add(TEntity item)
        {
            Add(item, typeof(TEntity) != typeof(Dynamic.DynamicEntity));
        }
        internal abstract void Add(TEntity item, bool fireEvents);
        public void AddRange(IEnumerable<TEntity> items)
        {
            AddRange(items, typeof(TEntity) != typeof(Dynamic.DynamicEntity));
        }
        internal abstract void AddRange(IEnumerable<TEntity> items, bool fireEvents);
        public abstract bool Contains(TEntity item);
        public void Remove(TEntity item)
        {
            Remove(item, typeof(TEntity) != typeof(Dynamic.DynamicEntity));
        }
        internal abstract void Remove(TEntity item, bool fireEvents);
        public void RemoveRange(IEnumerable<TEntity> items)
        {
            RemoveRange(items, typeof(TEntity) != typeof(Dynamic.DynamicEntity));
        }
        internal abstract void RemoveRange(IEnumerable<TEntity> items, bool fireEvents);
        public void Clear()
        {
            Clear(typeof(TEntity) != typeof(Dynamic.DynamicEntity));
        }
        internal abstract void Clear(bool fireEvents);
        public void Delete(TEntity item, bool force = false)
        {
            LazyLoad();
            LazySet();

            item.Delete(force);
            ForeignProperty?.ClearLookup(item);
        }
        public void DeleteDeep(bool force = false)
        {
            //if (ForeignProperty is not null && ForeignProperty.PropertyType == PropertyType.Lookup && !ForeignProperty.Nullable)
            //    throw new PersistenceException(string.Format("Due to a nullability constraint, you cannot delete {0} relationships directly. Consider removing the {1} objects instead.", ParentProperty.Relationship.Neo4JRelationshipType, ForeignEntity.Name));

            LazyLoad();
            LazySet();
            ForEach(delegate (int index, CollectionItem item)
            {
                item.Item.Delete(force);
                ForeignProperty?.ClearLookup(item.Item);
            });
        }

        protected abstract IEnumerator<TEntity> GetEnumeratorInternal();

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return GetEnumeratorInternal();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumeratorInternal();
        }

        bool ICollection<TEntity>.IsReadOnly { get { return false; } }

        IEnumerable<TEntity> IInnerDataCol<TEntity>.InnerData => InnerData.Select(item => item.Item);

        IEnumerable<OGM> IInnerDataCol.InnerData => InnerData.Select(item => item.Item);

        IEnumerable<OGM> IInnerDataCol.OriginalData => OriginalData;

        OGM? IInnerDataCol.InnerDataLookup => InnerData.FirstOrDefault()?.Item;

        OGM? IInnerDataCol.OriginalDataLookup => OriginalData.FirstOrDefault();

        public TEntity? InnerDataLookup => InnerData.FirstOrDefault()?.Item;

        public TEntity? OriginalDataLookup => OriginalData.FirstOrDefault();

        void ICollection<TEntity>.CopyTo(TEntity[] array, int arrayIndex)
        {
            foreach (TEntity item in this)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        protected abstract TEntity? GetOriginalItem(DateTime? moment);
        protected abstract IEnumerable<CollectionItem<TEntity>> GetItems(DateTime? from, DateTime? till);
        protected abstract TEntity? GetItem(DateTime? moment);
        protected abstract void SetItem(TEntity? item, DateTime? moment);
        protected abstract bool IsNull(bool isUpdate);
        protected abstract void ClearLookup(DateTime? moment);

        TEntity? ILookupHelper<TEntity>.GetOriginalItem(DateTime? moment)
        {
            return GetOriginalItem(moment);
        }

        TEntity? ILookupHelper<TEntity>.GetItem(DateTime? moment)
        {
            return GetItem(moment);
        }

        IEnumerable<CollectionItem<TEntity>> ILookupHelper<TEntity>.GetItems(DateTime? from, DateTime? till)
        {
            return GetItems(from, till);
        }

        void ILookupHelper<TEntity>.SetItem(TEntity? item, DateTime? moment)
        {
            SetItem(item, moment);
        }

        bool ILookupHelper<TEntity>.IsNull(bool isUpdate)
        {
            return IsNull(isUpdate);
        }

        void ILookupHelper<TEntity>.ClearLookup(DateTime? moment)
        {
            ClearLookup(moment);
        }

        bool ICollection<TEntity>.Remove(TEntity item)
        {
            Remove(item);
            return true;
        }
    }

    public interface IInnerDataCol
    {
        IEnumerable<OGM> InnerData { get; }
        IEnumerable<OGM> OriginalData { get; }
        OGM? InnerDataLookup { get; }
        OGM? OriginalDataLookup { get; }
    }
    public interface IInnerDataCol<TEntity> : IInnerDataCol
    {
        new IEnumerable<TEntity> InnerData { get; }
        new IEnumerable<TEntity> OriginalData { get; }
        new TEntity? InnerDataLookup { get; }
        new TEntity? OriginalDataLookup { get; }
    }
}
