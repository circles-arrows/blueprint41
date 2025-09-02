using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Blueprint41.Dynamic;
using Blueprint41.Persistence;

namespace Blueprint41.Core
{
    public abstract class EntityCollectionAsyncBase : IItteratable<CollectionItem>, IInternalListAccess, IReplayableCollection
    {
        protected EntityCollectionAsyncBase(OGM parent, Property property)
        {
            if (property.Relationship is null)
                throw new NotSupportedException("The property is not a relationship property.");

            Parent = parent;
            Relationship = property.Relationship;
            Direction = property.Direction;
            Transaction = parent.Transaction;

            switch (Direction)
            {
                case DirectionEnum.In:
                    ParentEntity = Relationship.InEntity;
                    ParentProperty = Relationship.InProperty;
                    ForeignEntity = Relationship.OutEntity;
                    ForeignProperty = Relationship.OutProperty;
                    break;
                case DirectionEnum.Out:
                    ParentEntity = Relationship.OutEntity;
                    ParentProperty = Relationship.OutProperty;
                    ForeignEntity = Relationship.InEntity;
                    ForeignProperty = Relationship.InProperty;
                    break;
                case DirectionEnum.None:
                    throw new NotSupportedException("You cannot initialize a collection without a direction.");
                default:
                    throw new NotImplementedException();
            }

            IsLoaded = false;

            if (parent is OgmClass || (parent is DynamicEntity && ((DynamicEntity)parent).ShouldExecute))
                Transaction?.Register(this);
        }

        #region Properties

        public OGM Parent { get; private set; }
        public Entity ParentEntity { get; private set; }
        public Property? ParentProperty { get; private set; }
        public Entity ForeignEntity { get; private set; }
        public Property? ForeignProperty { get; private set; }

        public Relationship Relationship { get; private set; }
        public DirectionEnum Direction { get; private set; }

        #endregion

        #region Relationship Action Helpers

        internal abstract void ForEach(Action<int, CollectionItem> action);
        void IItteratable<CollectionItem>.ForEach(Action<int, CollectionItem> action)
        {
            ForEach(action);
        }

        internal abstract void EnsureLoaded();
        internal abstract void Add(CollectionItem item);
        internal abstract CollectionItem? GetItem(int index);
        internal abstract void SetItem(int index, CollectionItem item);
        internal abstract void RemoveAt(int index);
        internal abstract int[] IndexOf(OGM item);

        void IInternalListAccess.EnsureLoaded()
        {
            EnsureLoaded();
        }
        void IInternalListAccess.Add(CollectionItem item)
        {
            Add(item);
        }
        CollectionItem? IInternalListAccess.GetItem(int index)
        {
            return GetItem(index);
        }
        void IInternalListAccess.SetItem(int index, CollectionItem item)
        {
            SetItem(index, item);
        }
        void IInternalListAccess.RemoveAt(int index)
        {
            RemoveAt(index);
        }
        int[] IInternalListAccess.IndexOf(OGM item)
        {
            return IndexOf(item);
        }

        internal OGM InItem(CollectionItem item)
        {
            if (ForeignProperty is not null && ForeignProperty.Direction == DirectionEnum.In)
                return item.Item as OGM;

            return Parent;
        }
        OGM IReplayableCollection.InItem(CollectionItem item) => InItem(item);
        internal OGM InItem(OGM foreign)
        {
            if (ForeignProperty is not null && ForeignProperty.Direction == DirectionEnum.In)
                return foreign;

            return Parent;
        }
        OGM IReplayableCollection.InItem(OGM foreign) => InItem(foreign);
        internal OGM OutItem(CollectionItem item)
        {
            if (ForeignProperty is null || ForeignProperty.Direction == DirectionEnum.Out)
                return item.Item as OGM;

            return Parent;
        }
        OGM IReplayableCollection.OutItem(CollectionItem item) => OutItem(item);
        internal OGM OutItem(OGM foreign)
        {
            if (ForeignProperty is null || ForeignProperty.Direction == DirectionEnum.Out)
                return foreign;

            return Parent;
        }
        OGM IReplayableCollection.OutItem(OGM foreign) => OutItem(foreign);
        internal OGM? ParentItem(RelationshipAction action)
        {
            if (ForeignProperty is null || ForeignProperty.Direction == DirectionEnum.Out)
                return action.InItem;

            return action.OutItem;
        }
        OGM? IReplayableCollection.ParentItem(RelationshipAction action) => ParentItem(action);
        internal OGM? ForeignItem(RelationshipAction action)
        {
            if (ForeignProperty is not null && ForeignProperty.Direction == DirectionEnum.In)
                return action.InItem;

            return action.OutItem;
        }
        OGM? IReplayableCollection.ForeignItem(RelationshipAction action) => ForeignItem(action);

        internal abstract RelationshipAction RemoveAction(OGM item, DateTime? moment);
        internal abstract RelationshipAction AddAction(OGM item, DateTime? moment, Dictionary<string, object>? properties);
        internal abstract RelationshipAction ClearAction(DateTime? moment);

        #endregion

        #region Persistence

        internal abstract CollectionItem NewCollectionItem(OGM parent, OGM item, DateTime? startDate, DateTime? endDate);
        CollectionItem IReplayableCollection.NewCollectionItem(OGM parent, OGM item, DateTime? startDate, DateTime? endDate)
        {
            return NewCollectionItem(parent, item, startDate, endDate);
        }

        private static readonly List<CollectionItem> empty = new List<CollectionItem>();

        protected void LazyLoad()
        {
            if (IsLoaded)
                return;

            if (Parent.PersistenceState == PersistenceState.New || Parent.PersistenceState == PersistenceState.NewAndChanged)
            {
                InitialLoad(empty);
                return;
            }

            Transaction trans = Transaction.RunningTransaction;
            if (trans.OptimizeFor == OptimizeFor.RecursiveSubGraphAccess)
            {
                // TODO: This should be awaited, but that would require all calling methods to be async as well.
                Task.Run(() => trans.LoadAllAsync(this)).Wait();
                return;
            }

            // TODO: This should be awaited, but that would require all calling methods to be async as well.
            IEnumerable<CollectionItem> items = Task.Run(() => RelationshipPersistenceProvider.LoadAsync(Parent, this)).Result;
            InitialLoad(items);
        }
        protected virtual async Task LazyLoadAsync()
        {
            if (IsLoaded)
                return;

            if (Parent.PersistenceState == PersistenceState.New || Parent.PersistenceState == PersistenceState.NewAndChanged)
            {
                InitialLoad(empty);
                return;
            }

            Transaction trans = Transaction.RunningTransaction;
            if (trans.OptimizeFor == OptimizeFor.RecursiveSubGraphAccess)
            {
                await trans.LoadAllAsync(this);
                return;
            }

            IEnumerable<CollectionItem> items = await RelationshipPersistenceProvider.LoadAsync(Parent, this);
            InitialLoad(items);
        }

        internal protected abstract void AfterFlush();
        internal abstract void InitialLoad(IEnumerable<CollectionItem> items);
        void IReplayableCollection.InitialLoad(IEnumerable<CollectionItem> items)
        {
            InitialLoad(items);
        }

        public bool IsLoaded { get; internal set; }

        #endregion

        public Transaction? Transaction { get; internal set; }
        Transaction? IReplayableCollection.Transaction { get => Transaction; set => Transaction = value; }

        protected private Transaction RunningTransaction
        {
            get
            {
                Transaction? trans = Transaction;

                if (trans is null)
                    throw new InvalidOperationException("There is no transaction, you should create one first -> using (Transaction.Begin()) { ... Transaction.Commit(); }");

                if (!trans.InTransaction)
                    throw new InvalidOperationException("The transaction was already committed or rolled back.");

                return trans;
            }
        }
        internal RelationshipPersistenceProvider RelationshipPersistenceProvider => RunningTransaction.RelationshipPersistenceProvider;

        private protected void ExecuteAction(RelationshipAction action)
        {
            if (Parent is OgmClass || (Parent is DynamicEntity && ((DynamicEntity)Parent).ShouldExecute))
                Transaction?.Register(action);
        }
        private protected void ExecuteAction(LinkedList<RelationshipAction> actions)
        {
            foreach (var action in actions)
                ExecuteAction(action);
        }
    }
}
