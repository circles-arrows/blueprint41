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
    public class EntityTimeCollection<TEntity> : EntityCollectionBase<TEntity>
        where TEntity : class, OGM
    {
        public EntityTimeCollection(OGM parent, Property property, Action<TEntity>? eagerLoadLogic = null) : base(parent, property, eagerLoadLogic) { }

        #region Manipulation

        public CollectionItem<TEntity> this[int index]
        {
            get
            {
                LazyLoad();
                return InnerData[index];
            }
        }

        private protected override int CountInternal { get { return CountAt(null); } }
        public int CountAt(DateTime? moment)
        {
            LazyLoad();

            int count = 0;
            if (!moment.HasValue)
                moment = RunningTransaction.TransactionDate;

            foreach (CollectionItem item in InnerData)
                if (item.Overlaps(moment.Value))
                    count++;

            return count;
        }
        public int CountAll
        {
            get
            {
                LazyLoad();
                return InnerData.Count;
            }
        }
        internal sealed override void Add(TEntity item, bool fireEvents)
        {
            Add(item, RunningTransaction.TransactionDate, fireEvents);
        }
        internal sealed override void AddRange(IEnumerable<TEntity> items, bool fireEvents)
        {
            AddRange(items, RunningTransaction.TransactionDate, fireEvents);
        }
        public void Add(TEntity item, DateTime? moment)
        {
            Add(item, moment, true);
        }
        internal void Add(TEntity item, DateTime? moment, bool fireEvents)
        {
            if (item is null)
                return;

            LazyLoad();
            LazySet();

            if (EagerLoadLogic != null)
                EagerLoadLogic.Invoke(item);

            if (fireEvents)
                if (ParentProperty?.RaiseOnChange((OGMImpl)Parent, default(TEntity), item, moment, OperationEnum.Add) ?? false)
                    return;

            if (Parent is OGMImpl || (Parent is DynamicEntity && ((DynamicEntity)Parent).ShouldExecute))
                DbTransaction?.Register(AddAction(item, moment));
        }
        internal void AddRange(IEnumerable<TEntity> items, DateTime? moment, bool fireEvents)
        {
            LazyLoad();
            LazySet();

            LinkedList<RelationshipAction> actions = new LinkedList<RelationshipAction>();
            foreach (var item in items)
            {
                if (item is null)
                    continue;

                if (EagerLoadLogic != null)
                    EagerLoadLogic.Invoke(item);

                if (fireEvents)
                    if (ParentProperty?.RaiseOnChange((OGMImpl)Parent, default(TEntity), item, moment, OperationEnum.Add) ?? false)
                        continue;

                actions.AddLast(AddAction(item, moment));
            }

            if (Parent is OGMImpl || (Parent is DynamicEntity && ((DynamicEntity)Parent).ShouldExecute))
                DbTransaction?.Register(actions);
        }
        public void AddUnmanaged(TEntity item, DateTime? startDate, DateTime? endDate, bool fullyUnmanaged = false)
        {
            RunningTransaction.RelationshipPersistenceProvider.AddUnmanaged(Relationship, InItem(item), OutItem(item), startDate, endDate, fullyUnmanaged);
        }
        public sealed override bool Contains(TEntity item)
        {
            return Contains(item, RunningTransaction.TransactionDate);
        }
        public bool Contains(TEntity item, DateTime? moment)
        {
            LazyLoad();

            for (int index = 0; index < InnerData.Count; index++)
                if (InnerData[index].Item.Equals(item) && (!moment.HasValue || InnerData[index].Overlaps(moment.Value)))
                    return true;

            return false;
        }
        internal sealed override bool Remove(TEntity item, bool fireEvents)
        {
            return Remove(item, RunningTransaction.TransactionDate, fireEvents);
        }
        internal sealed override bool RemoveRange(IEnumerable<TEntity> items, bool fireEvents)
        {
            return RemoveRange(items, RunningTransaction.TransactionDate, fireEvents);
        }
        internal bool RemoveRange(IEnumerable<TEntity> items, DateTime? moment, bool fireEvents)
        {
            if (ForeignProperty != null && ForeignProperty.PropertyType == PropertyType.Lookup && !ForeignProperty.Nullable)
                throw new PersistenceException(string.Format("Due to a nullability constraint, you cannot delete {0} relationships directly. Consider removing the {1} objects instead.", ParentProperty?.Relationship?.Neo4JRelationshipType, ForeignEntity.Name));

            LazyLoad();

            LinkedList<RelationshipAction> actions = new LinkedList<RelationshipAction>();

            foreach (var item in items)
            {
                if (item != null && EagerLoadLogic != null)
                    EagerLoadLogic.Invoke(item);

                if (fireEvents)
                {
                    bool cancel = false;
                    ForEach(delegate (int index, CollectionItem current)
                    {
                        if (current.Item.Equals(item) && (!moment.HasValue || current.EndDate > moment.Value))
                        {
                            if (current.Item.Equals(item))
                                if (ParentProperty?.RaiseOnChange<OGM>((OGMImpl)Parent, current.Item, default(TEntity), moment, OperationEnum.Remove) ?? false)
                                    cancel = true;
                        }
                    });
                    if (cancel)
                        return false;
                }

                ForEach(delegate (int index, CollectionItem current)
                {
                    if (current.Item.Equals(item) && (!moment.HasValue || current.EndDate > moment.Value))
                    {
                        ParentProperty?.RaiseOnChange<OGM>((OGMImpl)Parent, current.Item, default(TEntity), moment, OperationEnum.Remove);
                        actions.AddLast(RemoveAction(current, moment));
                    }
                });
            }

            if (actions.Count > 0)
            {
                if (Parent is OGMImpl || (Parent is DynamicEntity && ((DynamicEntity)Parent).ShouldExecute))
                    DbTransaction?.Register(actions);

                LazySet();
            }
            return (actions.Count > 0);
        }
        public bool Remove(TEntity item, DateTime? moment)
        {
            return Remove(item, moment, true);
        }
        internal bool Remove(TEntity item, DateTime? moment, bool fireEvents)
        {
            if (ForeignProperty != null && ForeignProperty.PropertyType == PropertyType.Lookup && !ForeignProperty.Nullable)
                throw new PersistenceException(string.Format("Due to a nullability constraint, you cannot delete {0} relationships directly. Consider removing the {1} objects instead.", ParentProperty?.Relationship?.Neo4JRelationshipType, ForeignEntity.Name));

            LazyLoad();

            if (item != null && EagerLoadLogic != null)
                EagerLoadLogic.Invoke(item);

            if (fireEvents)
            {
                bool cancel = false;
	            ForEach(delegate (int index, CollectionItem current)
                {
	                if (current.Item.Equals(item) && (!moment.HasValue || current.EndDate > moment.Value))
    	            {
        	            if (current.Item.Equals(item))
            	            if (ParentProperty?.RaiseOnChange<OGM>((OGMImpl)Parent, current.Item, default(TEntity), moment, OperationEnum.Remove) ?? false)
                	            cancel = true;
					}
                });
                if (cancel)
                    return false;
            }

            LinkedList<RelationshipAction> actions = new LinkedList<RelationshipAction>();

            ForEach(delegate (int index, CollectionItem current)
            {
                if (current.Item.Equals(item) && (!moment.HasValue || current.EndDate > moment.Value))
                {
                    ParentProperty?.RaiseOnChange<OGM>((OGMImpl)Parent, current.Item, default(TEntity), moment, OperationEnum.Remove);
                    actions.AddLast(RemoveAction(current, moment));
                }
            });

            if (actions.Count > 0)
            {
                if (Parent is OGMImpl || (Parent is DynamicEntity && ((DynamicEntity)Parent).ShouldExecute))
                    DbTransaction?.Register(actions);

                LazySet();
            }

            return (actions.Count > 0);
        }
        public void RemoveUmanaged(TEntity item, DateTime? startDate)
        {
            RunningTransaction.RelationshipPersistenceProvider.RemoveUnmanaged(Relationship, InItem(item), OutItem(item), startDate);
        }
        internal sealed override void Clear(bool fireEvents)
        {
            Clear(RunningTransaction.TransactionDate);
        }
        public void Clear(DateTime? moment)
        {
            Clear(moment, true);
        }
        internal void Clear(DateTime? moment, bool fireEvents)
        {
            if (ForeignProperty != null && ForeignProperty.PropertyType == PropertyType.Lookup && !ForeignProperty.Nullable)
                throw new PersistenceException(string.Format("Due to a nullability constraint, you cannot delete {0} relationships directly. Consider removing the {1} objects instead.", ParentProperty?.Relationship?.Neo4JRelationshipType, ForeignEntity.Name));

            LazyLoad();

            if (InnerData.Count == 0)
                return;

            LazySet();

            if (fireEvents)
            {
                HashSet<CollectionItem> cancel = new HashSet<CollectionItem>();
                ForEach(delegate (int index, CollectionItem item)
                {
                    if (item is null)
                        return;

	                if (EagerLoadLogic != null)
    	                EagerLoadLogic.Invoke((TEntity)item.Item);

                    if (ParentProperty?.RaiseOnChange((OGMImpl)Parent, item.Item, default(TEntity), moment, (OperationEnum)OperationEnum.Remove) ?? false)
                        cancel.Add((CollectionItem)item);
                });

                if (cancel.Count != 0)
                {
                    LinkedList<RelationshipAction> actions = new LinkedList<RelationshipAction>();

                    ForEach(delegate (int index, CollectionItem item)
                    {
                        if (cancel.Contains(item))
                            return;

                        actions.AddLast(RemoveAction(item, moment));
                    });

                    if (Parent is OGMImpl || (Parent is DynamicEntity && ((DynamicEntity)Parent).ShouldExecute))
                        DbTransaction?.Register(actions);

                    return;
                }
            }

            if (Parent is OGMImpl || (Parent is DynamicEntity && ((DynamicEntity)Parent).ShouldExecute))
                DbTransaction?.Register(ClearAction(moment));
        }

        sealed protected override IEnumerator<TEntity> GetEnumeratorInternal()
        {
            LazyLoad();
            return new Enumerator(InnerData.GetEnumerator());
        }

        public IEnumerator<CollectionItem<TEntity>> GetEnumerator()
        {
            LazyLoad();
            return InnerData.GetEnumerator();
        }

        public IEnumerable<TEntity> Items { get { return InnerData.Select(item => item.Item).Distinct(); } }

        internal class Enumerator : IEnumerator<TEntity>
        {
            internal Enumerator(IEnumerator<CollectionItem<TEntity>> enumerator)
            {
                this.enumerator = enumerator;
            }

            private IEnumerator<CollectionItem<TEntity>> enumerator;

            public TEntity Current
            {
                get
                {
                    return enumerator.Current.Item;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            public void Reset()
            {
                enumerator.Reset();
            }

            void IDisposable.Dispose()
            {
                enumerator.Dispose();
            }
        }

        #endregion

        #region Relationship Action Helpers

        internal override void ForEach(Action<int, CollectionItem> action)
        {
            LazyLoad();

            for (int index = InnerData.Count - 1; index >= 0; index--)
                action.Invoke(index, InnerData[index]);
        }
        internal override void Add(CollectionItem item)
        {
            InnerData.Add((CollectionItem<TEntity>)item);
        }
        internal override CollectionItem GetItem(int index)
        {
            return InnerData[index];
        }
        internal override void SetItem(int index, CollectionItem item)
        {
            InnerData[index] = (CollectionItem<TEntity>)item;
        }
        internal override void RemoveAt(int index)
        {
            InnerData.RemoveAt(index);
        }

        protected override TEntity GetItem(DateTime? moment)
        {
            LazyLoad();
            if (!moment.HasValue)
                moment = RunningTransaction.TransactionDate;

            return InnerData.Where(item => item.Overlaps(moment.Value)).Select(item => item.Item).FirstOrDefault();
        }
        protected override IEnumerable<CollectionItem<TEntity>> GetItems(DateTime? from, DateTime? till)
        {
            LazyLoad();

            if (from == null && till == null)
                return InnerData;

            return InnerData.Where(item => item.Overlaps(from, till));
        }
        protected override TEntity GetOriginalItem(DateTime? moment)
        {
            LazyLoad();
            if (!moment.HasValue)
                moment = RunningTransaction.TransactionDate;

            return LoadedData.Where(item => item.Overlaps(moment.Value)).Select(item => item.Item).FirstOrDefault();
        }
        protected override void SetItem(TEntity? item, DateTime? moment)
        {
            LazyLoad();
            LazySet();

            if (item != null && EagerLoadLogic != null)
                EagerLoadLogic.Invoke(item);

            List<CollectionItem<TEntity>> currentItem = InnerData.Where(e => e.Overlaps(moment, null)).ToList();

            if (!currentItem.FirstOrDefault()?.Item?.Equals(item) ?? !ReferenceEquals(item, null))
            {
                if (ForeignProperty != null && ForeignProperty.PropertyType == PropertyType.Lookup)
                {
                    OGM? oldForeignValue = (item is null) ? null : (OGM)ForeignProperty.GetValue(item, moment);
                    if (oldForeignValue != null)
                        ParentProperty?.ClearLookup(oldForeignValue, null);

                    foreach (TEntity entity in currentItem.Select(iitem => iitem.Item).Distinct())
                        ForeignProperty.ClearLookup(entity, moment);
                }

                if (item == null)
                {
                    if (currentItem.Count != 0)
                        Clear(moment, false);
                }
                else
                {
                    if (currentItem.Count == 1 && currentItem[0].Item.Equals(item))
                        return;

                    if (currentItem.Count > 0)
                        Clear(moment, false);

                    if (CountAt(moment) == 0)
                        Add(item, moment, false);
                }
            }
        }
        protected override bool IsNull(bool isUpdate)
        {
            if (!IsLoaded)
            {
                if (isUpdate)
                    return false;
                else
                    return true;
            }
            return !InnerData.Where(item => item.Overlaps(RunningTransaction.TransactionDate)).Select(item => item.Item).Any();
        }
        protected override void ClearLookup(DateTime? moment)
        {
            LazyLoad();
            LazySet();

            OGM? inItem = (Direction == DirectionEnum.In) ? Parent : null;
            OGM? outItem = (Direction == DirectionEnum.Out) ? Parent : null;
            if (Parent is OGMImpl || (Parent is DynamicEntity && ((DynamicEntity)Parent).ShouldExecute))
                DbTransaction?.Register(new TimeDependentClearRelationshipsAction(PersistenceProvider, Relationship, inItem, outItem, moment));
        }

        #endregion


        internal override CollectionItem NewCollectionItem(OGM parent, OGM item, DateTime? startDate, DateTime? endDate)
        {
            return new CollectionItem<TEntity>(parent, (TEntity)item, startDate, endDate);
        }
        internal override RelationshipAction AddAction(OGM item, DateTime? moment)
        {
            return new TimeDependentAddRelationshipAction(PersistenceProvider, Relationship, InItem(item), OutItem(item), moment);
        }
        internal override RelationshipAction RemoveAction(CollectionItem item, DateTime? moment)
        {
            return new TimeDependentRemoveRelationshipAction(PersistenceProvider, Relationship, InItem(item), OutItem(item), moment);
        }
        internal override RelationshipAction ClearAction(DateTime? moment)
        {
            OGM? inItem = (Direction == DirectionEnum.In) ? Parent : null;
            OGM? outItem = (Direction == DirectionEnum.Out) ? Parent : null;
            return new TimeDependentClearRelationshipsAction(PersistenceProvider, Relationship, inItem, outItem, moment);
        }
        internal RelationshipAction RemoveUnmanagedAction(CollectionItem item, DateTime? startDate)
        {
            return new TimeDependentRemoveUnmanagedRelationshipAction(PersistenceProvider, Relationship, InItem(item), OutItem(item), startDate);
        }
        internal RelationshipAction AddUnmanagedAction(OGM item, DateTime? startDate, DateTime? endDate)
        {
            return new TimeDependentAddUnmanagedRelationshipAction(PersistenceProvider, Relationship, InItem(item), OutItem(item), startDate, endDate);
        }
    }
}
