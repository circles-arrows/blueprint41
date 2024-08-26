using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Blueprint41.Events;
using Blueprint41.Persistence;

namespace Blueprint41.Core
{
    public partial class EntityTimeCollection<TEntity> : EntityCollectionBase<TEntity>
        where TEntity : class, OGM
    {
        public EntityTimeCollection(OGM parent, Property property, Action<TEntity>? eagerLoadLogic = null) : base(parent, property, eagerLoadLogic) { }

        #region Manipulation

        public CollectionItem<TEntity>? this[int index]
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

        internal sealed override void Add(TEntity item, bool fireEvents, Dictionary<string, object>? properties)
        {
            Add(item, RunningTransaction.TransactionDate, fireEvents, properties);
        }
        public void Add(TEntity item, DateTime? moment)
        {
            ForeignProperty?.ClearLookup(item, moment);
            Add(item, moment, true, null);
        }
        internal void Add(TEntity item, DateTime? moment, bool fireEvents, Dictionary<string, object>? properties)
        {
            if (item is null)
                return;

            LazyLoad();
            LazySet();

            if (EagerLoadLogic is not null)
                EagerLoadLogic.Invoke(item);

            if (fireEvents)
                if (ParentProperty?.RaiseOnChange((OGMImpl)Parent, default(TEntity), item, moment, OperationEnum.Add) ?? false)
                    return;

            ExecuteAction(AddAction(item, moment, properties));
        }
        internal sealed override void AddRange(IEnumerable<TEntity> items, bool fireEvents, Dictionary<string, object>? properties)
        {
            AddRange(items, RunningTransaction.TransactionDate, fireEvents, properties);
        }
        internal void AddRange(IEnumerable<TEntity> items, DateTime? moment, bool fireEvents, Dictionary<string, object>? properties)
        {
            LazyLoad();
            LazySet();

            LinkedList<RelationshipAction> actions = new LinkedList<RelationshipAction>();
            foreach (var item in items)
            {
                if (item is null)
                    continue;

                if (EagerLoadLogic is not null)
                    EagerLoadLogic.Invoke(item);

                if (fireEvents)
                    if (ParentProperty?.RaiseOnChange((OGMImpl)Parent, default(TEntity), item, moment, OperationEnum.Add) ?? false)
                        continue;

                actions.AddLast(AddAction(item, moment, properties));
            }

            ExecuteAction(actions);
        }
        public void AddUnmanaged(TEntity item, DateTime? startDate, DateTime? endDate, Dictionary<string, object>? properties, bool fullyUnmanaged = false)
        {
            RunningTransaction.RelationshipPersistenceProvider.AddUnmanaged(Relationship, InItem(item), OutItem(item), startDate, endDate, properties, fullyUnmanaged);
        }
        public sealed override bool Contains(TEntity item)
        {
            return Contains(item, Transaction.Current?.TransactionDate ?? DateTime.UtcNow);
        }
        public bool Contains(TEntity item, DateTime moment)
        {
            LazyLoad();

            for (int index = 0; index < InnerData.TotalCount; index++)
                if (InnerData[index] is not null)
                    if (InnerData[index]!.Item.Equals(item) && InnerData[index]!.Overlaps(moment))
                            return true;

            return false;
        }
        internal sealed override void Remove(TEntity item, bool fireEvents)
        {
            Remove(item, RunningTransaction.TransactionDate, fireEvents);
        }
        internal sealed override void RemoveRange(IEnumerable<TEntity> items, bool fireEvents)
        {
            RemoveRange(items, RunningTransaction.TransactionDate, fireEvents);
        }
        internal void RemoveRange(IEnumerable<TEntity> items, DateTime? moment, bool fireEvents)
        {
            if (items.Count() == 0)
                return;

            if (ForeignProperty is not null && ForeignProperty.PropertyType == PropertyType.Lookup && !ForeignProperty.Nullable)
                throw new PersistenceException(string.Format("Due to a nullability constraint, you cannot delete {0} relationships directly. Consider removing the {1} objects instead.", ParentProperty?.Relationship?.Neo4JRelationshipType, ForeignEntity.Name));

            LazySet();

            foreach (TEntity? item in items)
            {
                if (item is null)
                    continue;

                if (fireEvents)
                {

                    if (!(ParentProperty?.RaiseOnChange<OGM>((OGMImpl)Parent, item, default(TEntity), moment, OperationEnum.Remove) ?? false))
                        ExecuteAction(RemoveAction(item, moment));
                }
                else
                {
                    ExecuteAction(RemoveAction(item, moment));
                }
            }
        }
        public void Remove(TEntity item, DateTime? moment)
        {
            Remove(item, moment, true);
        }
        internal void Remove(TEntity item, DateTime? moment, bool fireEvents)
        {
            if (ForeignProperty is not null && ForeignProperty.PropertyType == PropertyType.Lookup && !ForeignProperty.Nullable)
                throw new PersistenceException(string.Format("Due to a nullability constraint, you cannot delete {0} relationships directly. Consider removing the {1} objects instead.", ParentProperty?.Relationship?.Neo4JRelationshipType, ForeignEntity.Name));

            if (item is null)
                return;

            LazyLoad();

            if (EagerLoadLogic is not null)
                EagerLoadLogic.Invoke(item);

            if (fireEvents)
                if (ParentProperty?.RaiseOnChange<OGM>((OGMImpl)Parent, item, default(TEntity), moment, OperationEnum.Remove) ?? false)
                    return;

            ExecuteAction(RemoveAction(item, moment));
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
            if (ForeignProperty is not null && ForeignProperty.PropertyType == PropertyType.Lookup && !ForeignProperty.Nullable)
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

                    if (EagerLoadLogic is not null)
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

                        actions.AddLast(RemoveAction(item.Item, moment));
                    });

                    ExecuteAction(actions);

                    return;
                }
            }

            ExecuteAction(ClearAction(moment));
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

        public IEnumerable<TEntity> Items => GetItems(Transaction.Current?.TransactionDate ?? DateTime.UtcNow);
        public IEnumerable<TEntity> GetItems(DateTime moment)
        {
            LazyLoad();

            return InnerData.Where(item => item.Overlaps(moment)).Select(item => item.Item).Distinct();
        }


        internal class Enumerator : IEnumerator<TEntity>
        {
            internal Enumerator(IEnumerator<CollectionItem<TEntity>> enumerator)
            {
                this.enumerator = enumerator;
            }

            private readonly IEnumerator<CollectionItem<TEntity>> enumerator;

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

        internal override void EnsureLoaded()
        {
            LazyLoad();
        }
        internal override void ForEach(Action<int, CollectionItem> action)
        {
            EnsureLoaded();

            for (int index = InnerData.TotalCount - 1; index >= 0; index--)
                if (InnerData[index] is not null)
                    action.Invoke(index, InnerData[index]!);
        }
        internal override void Add(CollectionItem item)
        {
            InnerData.Add((CollectionItem<TEntity>)item);
        }
        internal override CollectionItem? GetItem(int index)
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
        internal override int[] IndexOf(OGM item)
        {
            return InnerData.IndexOf((TEntity)item);
        }

        protected override TEntity? GetItem(DateTime? moment)
        {
            if (ParentProperty?.PropertyType != PropertyType.Lookup)
                throw new NotSupportedException("You cannot use GetItem on a property thats not a lookup.");

            LazyLoad();
            if (!moment.HasValue)
                moment = RunningTransaction.TransactionDate;

            return InnerData.Where(item => item.Overlaps(moment.Value)).Select(item => item.Item).FirstOrDefault();
        }
        protected override IEnumerable<CollectionItem<TEntity>> GetItems(DateTime? from, DateTime? till)
        {
            LazyLoad();

            if (from is null && till is null)
                return InnerData;

            return InnerData.Where(item => item.Overlaps(from, till));
        }
        protected override TEntity? GetOriginalItem(DateTime? moment)
        {
            if (ParentProperty?.PropertyType != PropertyType.Lookup)
                throw new NotSupportedException("You cannot use GetOriginalItem on a property thats not a lookup.");

            LazyLoad();
            if (!moment.HasValue)
                moment = RunningTransaction.TransactionDate;

            return LoadedData.Where(item => item.Overlaps(moment.Value)).Select(item => item.Item).FirstOrDefault();
        }
        protected override void AddItem(TEntity item, DateTime? moment, Dictionary<string, object>? properties)
        {
            Add(item, moment, true, properties);
        }
        protected override void SetItem(TEntity? item, DateTime? moment, Dictionary<string, object>? properties)
        {
            if (ParentProperty?.PropertyType != PropertyType.Lookup)
                throw new NotSupportedException("You cannot use SetItem on a property thats not a lookup.");

            LazyLoad();
            LazySet();

            //if (!moment.HasValue)
            //    moment = RunningTransaction.TransactionDate;

            if (item is not null && EagerLoadLogic is not null)
                EagerLoadLogic.Invoke(item);

            List<CollectionItem<TEntity>> currentItem = InnerData.Where(e => e.Overlaps(moment, null)).ToList();
            if (NeedsToAssign(ParentProperty?.Relationship, currentItem, item, moment, properties))
            {
                if (ForeignProperty is not null && ForeignProperty.PropertyType == PropertyType.Lookup)
                {
                    OGM? oldForeignValue = (item is null) ? null : (OGM)ForeignProperty.GetValue(item, moment);
                    if (oldForeignValue is not null)
                        ParentProperty?.ClearLookup(oldForeignValue, moment);

                    foreach (TEntity entity in currentItem.Select(iitem => iitem.Item).Distinct())
                        ForeignProperty.ClearLookup(entity, moment);
                }

                if (item is null)
                {
                    if (currentItem.Count > 0)
                    {
                        if (ParentProperty?.PropertyType == PropertyType.Lookup)
                            foreach (CollectionItem<TEntity> current in currentItem)
                                if (current?.Item is not null)
                                    Remove(current.Item, moment);

                        if (Count() > 0)
                            Clear(moment, false); // Clear should not be called here as this is for lookup.
                    }
                }
                else
                {
                    if (currentItem.Count > 0)
                    {
                        if (ParentProperty?.PropertyType == PropertyType.Lookup)
                            foreach (CollectionItem<TEntity> current in currentItem)
                                if (current?.Item is not null)
                                    Remove(current.Item, moment);

                        if (Count() > 0)
                            Clear(moment, false); // Clear should not be called here as this is for lookup.
                    }

                    if (Count() == 0)
                        Add(item, moment, false, properties);
                }
            }


            static bool NeedsToAssign(Relationship? relationship, List< CollectionItem<TEntity>> currentItem, TEntity? assignValue, DateTime? moment, Dictionary<string, object>? properties)
            {
                if (relationship is null || (relationship.Properties.Count - relationship.ExcludedProperties().Count) > 0)
                    return true;

                if (!currentItem.Any() && assignValue is null)
                    return false;

                if (currentItem.Take(2).Count() == 1 && currentItem.First().Item.Equals(assignValue) && (currentItem.First().StartDate.IsMin() || currentItem.First().StartDate <= moment) && currentItem.First().EndDate.IsMax())
                    return false;

                return true;
            }

            int Count() =>  InnerData.Where(e => e.Overlaps(moment, null)).Count();
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
            return !InnerData.Where(item => item.Overlaps(RunningTransaction.TransactionDate)).Select(item => item.Item).Any(item => item is not null);
        }
        protected override void ClearLookup(DateTime? moment)
        {
            LazySet();
            ExecuteAction(ClearAction(moment));
        }

        #endregion


        internal override CollectionItem NewCollectionItem(OGM parent, OGM item, DateTime? startDate, DateTime? endDate)
        {
            return new CollectionItem<TEntity>(parent, (TEntity)item, startDate, endDate);
        }
        internal override RelationshipAction AddAction(OGM item, DateTime? moment, Dictionary<string, object>? properties)
        {
            return new TimeDependentAddRelationshipAction(RelationshipPersistenceProvider, Relationship, InItem(item), OutItem(item), moment, properties);
        }
        internal override RelationshipAction RemoveAction(OGM item, DateTime? moment)
        {
            return new TimeDependentRemoveRelationshipAction(RelationshipPersistenceProvider, Relationship, InItem(item), OutItem(item), moment);
        }
        internal override RelationshipAction ClearAction(DateTime? moment)
        {
            OGM? inItem = (Direction == DirectionEnum.In) ? Parent : null;
            OGM? outItem = (Direction == DirectionEnum.Out) ? Parent : null;

            // ClearRelationshipsAction removes ALL relationships from an Entity, we only need to remove all relationships from a collection.
            // Difference being ALL relationships in ALL directions vs. 1 relationship in 1 direction!!!
            return new TimeDependentRemoveRelationshipAction(RelationshipPersistenceProvider, Relationship, inItem, outItem, moment);
        }
        internal RelationshipAction RemoveUnmanagedAction(OGM item, DateTime? startDate)
        {
            return new TimeDependentRemoveUnmanagedRelationshipAction(RelationshipPersistenceProvider, Relationship, InItem(item), OutItem(item), startDate);
        }
        internal RelationshipAction AddUnmanagedAction(OGM item, Dictionary<string, object>? properties, DateTime? startDate, DateTime? endDate)
        {
            return new TimeDependentAddUnmanagedRelationshipAction(RelationshipPersistenceProvider, Relationship, InItem(item), OutItem(item), startDate, endDate, properties);
        }
    }
}
