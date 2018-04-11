using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = Blueprint41.Neo4j.Model;
using persistence = Blueprint41.Neo4j.Persistence;

namespace Blueprint41.Core
{
    public class EntityCollection<TEntity> : EntityCollectionBase<TEntity>
        where TEntity : OGM
    {
        public EntityCollection(OGM parent, Property property, Action<TEntity> eagerLoadLogic = null) : base(parent, property, eagerLoadLogic) { }

        #region Manipulation

        public TEntity this[int index]
        {
            get
            {
                LazyLoad();
                return InnerData[index].Item;
            }
        }
        private protected override int CountInternal { get { return Count; } }
        public int Count { get { LazyLoad(); return InnerData.Count; } }
        internal sealed override void Add(TEntity item, bool fireEvents)
        {
            LazyLoad();
            LazySet();

            if (item != null && EagerLoadLogic != null)
                EagerLoadLogic.Invoke(item);

            if (fireEvents)
                if (ParentProperty.RaiseOnChange((OGMImpl)Parent, default(TEntity), item, null, OperationEnum.Add))
                    return;

            Transaction?.Register(AddAction(item, null));
        }
        public override bool Contains(TEntity item)
        {
            LazyLoad();
            return InnerData.Contains(new CollectionItem<TEntity>(this.Parent, item));
        }
        internal sealed override bool Remove(TEntity item, bool fireEvents)
        {
            if (ForeignProperty != null && ForeignProperty.PropertyType == PropertyType.Lookup && !ForeignProperty.Nullable)
                throw new PersistenceException(string.Format("Due to a nullability constraint, you cannot delete {0} relationships directly. Consider removing the {1} objects instead.", ParentProperty.Relationship.Neo4JRelationshipType, ForeignEntity.Name));

            LazyLoad();
            LazySet();

            if (item != null && EagerLoadLogic != null)
                EagerLoadLogic.Invoke(item);

            if (fireEvents)
            {
                bool cancel = false;
                ForEach(delegate (int index, CollectionItem current)
                {
                    if (current.Item.Equals(item))
                        if (ParentProperty.RaiseOnChange<OGM>((OGMImpl)Parent, current.Item, default(TEntity), null, OperationEnum.Remove))
                            cancel = true;
                });
                if (cancel)
                    return false;
            }

            LinkedList<RelationshipAction> actions = new LinkedList<RelationshipAction>();

            ForEach(delegate (int index, CollectionItem current)
            {
                if (current.Item.Equals(item))
                    actions.AddLast(RemoveAction(current, null));
            });

            Transaction.Register(actions);

            return (actions.Count > 0);
        }
        internal sealed override void Clear(bool fireEvents)
        {
            if (ForeignProperty != null && ForeignProperty.PropertyType == PropertyType.Lookup && !ForeignProperty.Nullable)
                throw new PersistenceException(string.Format("Due to a nullability constraint, you cannot delete {0} relationships directly. Consider removing the {1} objects instead.", ParentProperty.Relationship.Neo4JRelationshipType, ForeignEntity.Name));

            LazyLoad();
            LazySet();

            if (fireEvents)
            {
                HashSet<CollectionItem> cancel = new HashSet<CollectionItem>();
                ForEach(delegate (int index, CollectionItem item)
                {
                    if (item != null && EagerLoadLogic != null)
                        EagerLoadLogic.Invoke((TEntity)item.Item);

                    if (ParentProperty.RaiseOnChange((OGMImpl)(OGMImpl)Parent, (OGM)item.Item, (OGM)default(TEntity), null , (OperationEnum)OperationEnum.Remove))
                        cancel.Add((CollectionItem)item);
                });

                if (cancel.Count != 0)
                {
                    LinkedList<RelationshipAction> actions = new LinkedList<RelationshipAction>();

                    ForEach(delegate (int index, CollectionItem item)
                    {
                        if (cancel.Contains(item))
                            return;

                        actions.AddLast(RemoveAction(item, null));
                    });

                    Transaction.Register(actions);
                    return;
                }
            }

            Transaction.Register(ClearAction(null));
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

            if (InnerData.Count == 0)
                return default(TEntity);

            return InnerData[0].Item;
        }
        protected override TEntity GetOriginalItem(DateTime? moment)
        {
            LazyLoad();

            if (LoadedData.Count() == 0)
                return default(TEntity);

            return LoadedData.First().Item;
        }
        protected override void SetItem(TEntity item, DateTime? moment)
        {
            LazyLoad();
            LazySet();

            if (item != null && EagerLoadLogic != null)
                EagerLoadLogic.Invoke(item);

            List<CollectionItem<TEntity>> currentItem = InnerData.ToList();

            if (!currentItem.FirstOrDefault()?.Item?.Equals(item) ?? !ReferenceEquals(item, null))
            {
                if (ForeignProperty != null && ForeignProperty.PropertyType == PropertyType.Lookup)
                {
                    OGM oldForeignValue = (item == null) ? null : (OGM)ForeignProperty.GetValue(item, moment);
                    if (oldForeignValue != null)
                        ParentProperty.ClearLookup(oldForeignValue, null);
                
                    foreach (TEntity entity in currentItem.Select(iitem => iitem.Item).Distinct())
                        ForeignProperty.ClearLookup(entity, null);
                }

                if (item == null)
                {
                    if (currentItem.Count != 0)
                        Clear(false);
                }
                else
                {
                    if (currentItem.Count == 1 && currentItem[0].Item.Equals(item))
                        return;

                    if (currentItem.Count > 0)
                        Clear(false);

                    if (Count == 0)
                        Add(item, false);
                }
            }            
        }
        protected override bool IsNull(bool isUpdate)
        {
            if(!isUpdate && !IsLoaded)
                return true;
            
            return ((InnerData?.Count ?? 1) == 0);
        }
        protected override void ClearLookup(DateTime? moment)
        {
            LazyLoad();
            LazySet();

            OGM inItem = (Direction == DirectionEnum.In) ? Parent : null;
            OGM outItem = (Direction == DirectionEnum.Out) ? Parent : null;
            Transaction.Register(new ClearRelationshipsAction(PersistenceProvider, Relationship, inItem, outItem));
        }

        #endregion

        internal override CollectionItem NewCollectionItem(OGM parent, OGM item, DateTime? startDate, DateTime? endDate)
        {
            return new CollectionItem<TEntity>(parent, (TEntity)item, startDate, endDate);
        }
        internal override RelationshipAction RemoveAction(CollectionItem item, DateTime? moment)
        {
            return new RemoveRelationshipAction(PersistenceProvider, Relationship, InItem(item), OutItem(item));
        }
        internal override RelationshipAction AddAction(OGM item, DateTime? moment)
        {
            return new AddRelationshipAction(PersistenceProvider, Relationship, InItem(item), OutItem(item));
        }
        internal override RelationshipAction ClearAction(DateTime? moment)
        {
            OGM inItem = (Direction == DirectionEnum.In) ? Parent : null;
            OGM outItem = (Direction == DirectionEnum.Out) ? Parent : null;
            return new ClearRelationshipsAction(PersistenceProvider, Relationship, inItem, outItem);
        }
    }
}
