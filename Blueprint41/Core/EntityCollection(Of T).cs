﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Dynamic;
using model = Blueprint41.Neo4j.Model;
using persistence = Blueprint41.Neo4j.Persistence;

namespace Blueprint41.Core
{
    public partial class EntityCollection<TEntity> : EntityCollectionBase<TEntity>
        where TEntity : class, OGM
    {
        public EntityCollection(OGM parent, Property property, Action<TEntity>? eagerLoadLogic = null)
            : base(parent, property, eagerLoadLogic) { }

        #region Manipulation

        public TEntity? this[int index]
        {
            get
            {
                LazyLoad();
                return InnerData[index]?.Item;
            }
        }
        private protected override int CountInternal { get { return Count; } }
        public int Count { get { LazyLoad(); return InnerData.Count; } }

        internal sealed override void Add(TEntity item, bool fireEvents, Dictionary<string, object>? properties)
        {
            if (item is null)
                return;

            LazySet();

            if (EagerLoadLogic is not null)
                EagerLoadLogic.Invoke(item);

            if (fireEvents)
                if (ParentProperty?.RaiseOnChange((OGMImpl)Parent, default(TEntity), item, null, OperationEnum.Add) ?? false)
                    return;

            ExecuteAction(AddAction(item, null, properties));
        }
        internal sealed override void AddRange(IEnumerable<TEntity> items, bool fireEvents, Dictionary<string, object>? properties)
        {
            LazySet();

            LinkedList<RelationshipAction> actions = new LinkedList<RelationshipAction>();
            foreach (TEntity? item in items)
            {
                if (item is null)
                    continue;

                if (EagerLoadLogic is not null)
                    EagerLoadLogic.Invoke(item);

                if (fireEvents)
                    if (ParentProperty?.RaiseOnChange((OGMImpl)Parent, default(TEntity), item, null, OperationEnum.Add) ?? false)
                        return;

                actions.AddLast(AddAction(item, null, properties));
            }

            ExecuteAction(actions);
        }
        public override bool Contains(TEntity item)
        {
            LazyLoad();
            return InnerData.Contains(new CollectionItem<TEntity>(this.Parent, item));
        }
        internal sealed override void Remove(TEntity item, bool fireEvents)
        {
            if (ForeignProperty is not null && ForeignProperty.PropertyType == PropertyType.Lookup && !ForeignProperty.Nullable)
                throw new PersistenceException(string.Format("Due to a nullability constraint, you cannot delete {0} relationships directly. Consider removing the {1} objects instead.", ParentProperty?.Relationship?.Neo4JRelationshipType, ForeignEntity.Name));

            if (item is null)
                return;

            LazySet();

            if (EagerLoadLogic is not null)
                EagerLoadLogic.Invoke(item);

            if (fireEvents)
                if (ParentProperty?.RaiseOnChange<OGM>((OGMImpl)Parent, item, default(TEntity), null, OperationEnum.Remove) ?? false)
                    return;

            ExecuteAction(RemoveAction(item, null));
        }
        internal sealed override void Clear(bool fireEvents)
        {
            if (ForeignProperty is not null && ForeignProperty.PropertyType == PropertyType.Lookup && !ForeignProperty.Nullable)
                throw new PersistenceException(string.Format("Due to a nullability constraint, you cannot delete {0} relationships directly. Consider removing the {1} objects instead.", ParentProperty?.Relationship?.Neo4JRelationshipType, ForeignEntity.Name));

            LazySet();
            
            if (InnerData.Count == 0)
                return;

            if (fireEvents)
            {
                HashSet<CollectionItem> cancel = new HashSet<CollectionItem>();
                ForEach(delegate (int index, CollectionItem item)
                {
                    if (item is null)
                        return;

                    if (EagerLoadLogic is not null)
                        EagerLoadLogic.Invoke((TEntity)item.Item);

                    if (ParentProperty?.RaiseOnChange((OGMImpl)Parent, item.Item, default(TEntity), null , (OperationEnum)OperationEnum.Remove) ?? false)
                        cancel.Add(item);
                });

                if (cancel.Count != 0)
                {
                    LinkedList<RelationshipAction> actions = new LinkedList<RelationshipAction>();

                    ForEach(delegate (int index, CollectionItem item)
                    {
                        if (cancel.Contains(item))
                            return;

                        actions.AddLast(RemoveAction(item.Item, null));
                    });

                    ExecuteAction(actions);

                    return;
                }
            }

            ExecuteAction(ClearAction(null));
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

                    if (!(ParentProperty?.RaiseOnChange<OGM>((OGMImpl)Parent, item, default(TEntity), null, OperationEnum.Remove) ?? false))
                        ExecuteAction(RemoveAction(item, null));
                }
                else
                {
                    ExecuteAction(RemoveAction(item, null));
                }
            }
        }


        sealed protected override IEnumerator<TEntity> GetEnumeratorInternal()
        {
            LazyLoad();
            return new Enumerator(InnerData.GetEnumerator());
        }
        public IEnumerator<TEntity> GetEnumerator()
        {
            LazyLoad();
            return new Enumerator(InnerData.GetEnumerator());
        }

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

            if (InnerData.Count == 0)
                return default(TEntity);

            return InnerData.First().Item;
        }
        protected override IEnumerable<CollectionItem<TEntity>> GetItems(DateTime? from, DateTime? till)
        {
            return InnerData;
        }
        protected override TEntity? GetOriginalItem(DateTime? moment)
        {
            if (ParentProperty?.PropertyType != PropertyType.Lookup)
                throw new NotSupportedException("You cannot use GetOriginalItem on a property thats not a lookup.");

            LazyLoad();

            if (LoadedData.Count() == 0)
                return default(TEntity);

            return LoadedData.First().Item;
        }
        protected override void AddItem(TEntity item, DateTime? moment, Dictionary<string, object>? properties)
        {
            Add(item, true, properties);
        }
        protected override void SetItem(TEntity? item, DateTime? moment, Dictionary<string, object>? properties)
        {
            if (ParentProperty?.PropertyType != PropertyType.Lookup)
                throw new NotSupportedException("You cannot use SetItem on a property thats not a lookup.");

            LazySet();

            if (item is not null && EagerLoadLogic is not null)
                EagerLoadLogic.Invoke(item);

            List<CollectionItem<TEntity>> currentItem = InnerData.ToList();
            if (NeedsToAssign(ParentProperty?.Relationship) || (!currentItem.FirstOrDefault()?.Item?.Equals(item) ?? !ReferenceEquals(item, null)))
            {
                if (ForeignProperty is not null && ForeignProperty.PropertyType == PropertyType.Lookup)
                {
                    OGM? oldForeignValue = (item is null) ? null : (OGM)ForeignProperty.GetValue(item, moment);
                    if (oldForeignValue is not null)
                        ParentProperty?.ClearLookup(oldForeignValue, null);

                    foreach (TEntity entity in currentItem.Select(iitem => iitem.Item).Distinct())
                        ForeignProperty.ClearLookup(entity, null);
                }

                if (item is null)
                {
                    if (currentItem.Count > 0)
                    {
                        if (ParentProperty?.PropertyType == PropertyType.Lookup)
                            Remove(currentItem[0].Item);

                        if (Count > 0)
                            Clear(false); // Clear should not be called here as this is for lookup.
                    }
                }
                else
                {
                    if (currentItem.Count == 1 && currentItem[0].Item.Equals(item))
                        return;

                    if (currentItem.Count > 0)
                    {
                        if (ParentProperty?.PropertyType == PropertyType.Lookup)
                            Remove(currentItem[0].Item);

                        if (Count > 0)
                            Clear(false); // Clear should not be called here as this is for lookup.
                    }

                    if (Count == 0)
                        Add(item, false, properties);
                }
            }

            static bool NeedsToAssign(Relationship? relationship)
            {
                if (relationship is null || (relationship.Properties.Count - relationship.ExcludedProperties().Count) > 0)
                    return true;

                return false;
            }
        }
        protected override bool IsNull(bool isUpdate)
        {
            if(!isUpdate && !IsLoaded)
                return true;

            return ((innerData?.Count ?? 1) == 0);
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
        internal override RelationshipAction RemoveAction(OGM item, DateTime? moment)
        {
            return new RemoveRelationshipAction(RelationshipPersistenceProvider, Relationship, InItem(item), OutItem(item));
        }
        internal override RelationshipAction AddAction(OGM item, DateTime? moment, Dictionary<string, object>? properties)
        {
            return new AddRelationshipAction(RelationshipPersistenceProvider, Relationship, InItem(item), OutItem(item), properties);
        }
        internal override RelationshipAction ClearAction(DateTime? moment)
        {
            OGM? inItem = (Direction == DirectionEnum.In) ? Parent : null;
            OGM? outItem = (Direction == DirectionEnum.Out) ? Parent : null;

            // ClearRelationshipsAction removes ALL relationships from an Entity, we only need to remove all relationships from a collection.
            // Difference being ALL relationships in ALL directions vs. 1 relationship in 1 direction!!!
            return new RemoveRelationshipAction(RelationshipPersistenceProvider, Relationship, inItem, outItem);
        }
    }
}
