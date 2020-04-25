using Blueprint41.Dynamic;
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
    public abstract class EntityCollectionBase : IItteratable<CollectionItem>, IInternalListAccess
    {
        public EntityCollectionBase(OGM parent, Property property)
        {
            if (property.Relationship is null)
                throw new NotSupportedException("The property is not a relationship property.");

            Parent = parent;
            Relationship = property.Relationship;
            Direction = property.Direction;
            DbTransaction = Transaction.Current;

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

            if (parent is OGMImpl || (parent is DynamicEntity && ((DynamicEntity)parent).ShouldExecute))
                DbTransaction?.Register(this);
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

        internal abstract void Add(CollectionItem item);
        internal abstract CollectionItem GetItem(int index);
        internal abstract void SetItem(int index, CollectionItem item);
        internal abstract void RemoveAt(int index);

        void IInternalListAccess.Add(CollectionItem item)
        {
            Add(item);
        }
        CollectionItem IInternalListAccess.GetItem(int index)
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

        internal OGM InItem(CollectionItem item)
        {
            if (ForeignProperty != null && ForeignProperty.Direction == DirectionEnum.In)
                return item.Item as OGM;

            return Parent;
        }
        internal OGM InItem(OGM foreign)
        {
            if (ForeignProperty != null && ForeignProperty.Direction == DirectionEnum.In)
                return foreign;

            return Parent;
        }
        internal OGM OutItem(CollectionItem item)
        {
            if (ForeignProperty == null || ForeignProperty.Direction == DirectionEnum.Out)
                return item.Item as OGM;

            return Parent;
        }
        internal OGM OutItem(OGM foreign)
        {
            if (ForeignProperty == null || ForeignProperty.Direction == DirectionEnum.Out)
                return foreign;

            return Parent;
        }
        internal OGM ParentItem(RelationshipAction action)
        {
            if (ForeignProperty == null || ForeignProperty.Direction == DirectionEnum.Out)
                return action.InItem;

            return action.OutItem;
        }
        internal OGM ForeignItem(RelationshipAction action)
        {
            if (ForeignProperty != null && ForeignProperty.Direction == DirectionEnum.In)
                return action.InItem;

            return action.OutItem;
        }

        internal abstract RelationshipAction RemoveAction(CollectionItem item, DateTime? moment);
        internal abstract RelationshipAction AddAction(OGM item, DateTime? moment);
        internal abstract RelationshipAction ClearAction(DateTime? moment);

        #endregion

        #region Persistence

        internal abstract CollectionItem NewCollectionItem(OGM parent, OGM item, DateTime? startDate, DateTime? endDate);


        static readonly List<CollectionItem> empty = new List<CollectionItem>();
        protected virtual void LazyLoad()
        {
            if (IsLoaded)
                return;

            if (Parent.PersistenceState == PersistenceState.New || Parent.PersistenceState == PersistenceState.NewAndChanged)
            {
                InitialLoad(empty);
                return;
            }

            Transaction trans = Transaction.RunningTransaction;
            if (trans.Mode == OptimizeFor.RecursiveSubGraphAccess)
            {
                trans.LoadAll(this);
                return;
            }

            IEnumerable<CollectionItem> items = PersistenceProvider.Load(Parent, this);
            InitialLoad(items);
        }
        internal abstract void InitialLoad(IEnumerable<CollectionItem> items);

        public bool IsLoaded { get; internal set; }

        #endregion

        public Transaction? DbTransaction { get; internal set; }
        protected private Transaction RunningTransaction
        {
            get
            {
                Transaction? trans = DbTransaction;

                if (trans is null)
                    throw new InvalidOperationException("There is no transaction, you should create one first -> using (Transaction.Begin()) { ... Transaction.Commit(); } or you loaded this object in another transaction then you are using now.");

                if (!trans.InTransaction)
                    throw new InvalidOperationException("The transaction was already committed or rolled back.");

                return trans;
            }
        }

        internal RelationshipPersistenceProvider PersistenceProvider => RunningTransaction.RelationshipPersistenceProvider;
    }
}
