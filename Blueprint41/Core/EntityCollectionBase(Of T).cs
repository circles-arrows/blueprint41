using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = Blueprint41.Neo4j.Model;
using persistence = Blueprint41.Neo4j.Persistence;

namespace Blueprint41.Core
{
    public abstract class EntityCollectionBase<TEntity> : EntityCollectionBase, ICollection<TEntity>, ILookupHelper<TEntity>
        where TEntity : OGM
    {
        public EntityCollectionBase(OGM parent, Property property, Action<TEntity> eagerLoadLogic) : base(parent, property)
        {
            EagerLoadLogic = eagerLoadLogic;
        }

        protected Action<TEntity> EagerLoadLogic;

        protected IList<CollectionItem<TEntity>> InnerData = null;
        protected IList<CollectionItem<TEntity>> LoadedData { get; private set; }
        public IEnumerable<TEntity> OriginalData { get { LazyLoad(); return LoadedData.Select(item => item.Item); } }
        sealed internal override void InitialLoad(IEnumerable<CollectionItem> items)
        {
            InitialLoad(items.Cast<CollectionItem<TEntity>>());
        }
        protected void InitialLoad(IEnumerable<CollectionItem<TEntity>> items)
        {
            if (IsLoaded)
                return;

            ObservableList<CollectionItem<TEntity>> tmp = new ObservableList<CollectionItem<TEntity>>(items);
            tmp.BeforeCollectionChanged += BeforeCollectionChanged;
            InnerData = tmp;
            LoadedData = tmp;

            IsLoaded = true;
            Transaction?.Replay(this);
        }
        private void BeforeCollectionChanged(object sender, EventArgs args)
        {
            ObservableList<CollectionItem<TEntity>> tmp = InnerData as ObservableList<CollectionItem<TEntity>>;
            tmp.BeforeCollectionChanged -= BeforeCollectionChanged;
            if (ReferenceEquals(InnerData, LoadedData))
                LoadedData = new List<CollectionItem<TEntity>>(InnerData);
        }
        protected virtual void LazySet()
        {

        }

        int ICollection<TEntity>.Count { get { return CountInternal; } }
        private protected abstract int CountInternal { get; }

        public void Add(TEntity item)
        {
            Add(item, typeof(TEntity) != typeof(Dynamic.DynamicEntity));
        }
        internal abstract void Add(TEntity item, bool fireEvent);
        public abstract bool Contains(TEntity item);
        public bool Remove(TEntity item)
        {
            return Remove(item, typeof(TEntity) != typeof(Dynamic.DynamicEntity));
        }
        internal abstract bool Remove(TEntity item, bool fireEvent);
        public void Clear()
        {
            Clear(typeof(TEntity) != typeof(Dynamic.DynamicEntity));
        }
        internal abstract void Clear(bool fireEvent);
        public void Delete(TEntity item, bool force = false)
        {
            LazyLoad();
            LazySet();

            item.Delete(force);
            ForeignProperty.ClearLookup(item);
        }
        public void DeleteDeep(bool force = false)
        {
            //if (ForeignProperty != null && ForeignProperty.PropertyType == PropertyType.Lookup && !ForeignProperty.Nullable)
            //    throw new PersistenceException(string.Format("Due to a nullability constraint, you cannot delete {0} relationships directly. Consider removing the {1} objects instead.", ParentProperty.Relationship.Neo4JRelationshipType, ForeignEntity.Name));

            LazyLoad();
            LazySet();
            ForEach(delegate (int index, CollectionItem item)
            {
                item.Item.Delete(force);
                ForeignProperty.ClearLookup(item.Item);
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
        void ICollection<TEntity>.CopyTo(TEntity[] array, int arrayIndex)
        {
            foreach (TEntity item in this)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        protected abstract TEntity GetOriginalItem(DateTime? moment);
        protected abstract TEntity GetItem(DateTime? moment);
        protected abstract void SetItem(TEntity item, DateTime? moment);
        protected abstract bool IsNull(bool isUpdate);
        protected abstract void ClearLookup(DateTime? moment);

        TEntity ILookupHelper<TEntity>.GetOriginalItem(DateTime? moment)
        {
            return GetOriginalItem(moment);
        }

        TEntity ILookupHelper<TEntity>.GetItem(DateTime? moment)
        {
            return GetItem(moment);
        }

        void ILookupHelper<TEntity>.SetItem(TEntity item, DateTime? moment)
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
    }
}
