using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using persistence = Blueprint41.Neo4j.Persistence;


namespace Blueprint41.Core
{
    public abstract class OGMImpl : OGM
    {
        protected const string Param0 = "P0";
        protected const string Param1 = "P1";
        protected const string Param2 = "P2";
        protected const string Param3 = "P3";
        protected const string Param4 = "P4";
        protected const string Param5 = "P5";
        protected const string Param6 = "P6";
        protected const string Param7 = "P7";
        protected const string Param8 = "P8";
        protected const string Param9 = "P9";

        protected OGMImpl()
        {
            if (!GetEntity().Parent.IsUpgraded)
                throw new InvalidOperationException("You cannot use entity inside the upgrade script.");

            Transaction.RunningTransaction.Register(this);
            PersistenceProvider = Transaction.Current.NodePersistenceProvider;
        }
        
        void OGM.Save()
        {
            switch (PersistenceState)
            {
                case PersistenceState.New:
                case PersistenceState.HasUid:
                case PersistenceState.Loaded:
                    break;
                case PersistenceState.NewAndChanged:
                    PersistenceProvider.Insert(this);
                    PersistenceState = PersistenceState.Persisted;
                    break;
                case PersistenceState.LoadedAndChanged:
                    PersistenceProvider.Update(this);
                    PersistenceState = PersistenceState.Persisted;
                    break;
                case PersistenceState.Persisted:
                    break;
                case PersistenceState.Deleted:
                    PersistenceProvider.Delete(this);
                    PersistenceState = PersistenceState.Persisted;
                    return;
                case PersistenceState.ForceDeleted:
                    PersistenceProvider.ForceDelete(this);
                    PersistenceState = PersistenceState.Persisted;
                    return;
                case PersistenceState.OutOfScope:
                case PersistenceState.Error:
                    throw new InvalidOperationException(string.Format("The object with key '{0}' cannot be saved because it's state was {1}.", GetKey() ?? "<null>", PersistenceState.ToString()));
                case PersistenceState.DoesntExist:
                    throw new InvalidOperationException($"{GetEntity().Name} with key {GetKey().ToString()} couldn't be loaded from the database.");
                default:
                    throw new NotImplementedException(string.Format("The object with key '{0}' has an invalid/unknown state {1}.", GetKey() ?? "<null>", PersistenceState.ToString()));
            }
        }
        void OGM.ValidateSave()
        {
            ValidateSave();
        }
        void OGM.ValidateDelete()
        {
            ValidateDelete();
        }
        protected virtual void ValidateSave() { }

        protected virtual void ValidateDelete() { }
        protected bool RelationshipExists(Property foreignProperty, OGM instance)
        {
            return PersistenceProvider.RelationshipExists(foreignProperty, instance);
        }

        void OGM.Delete(bool force)
        {
            GetEntity().RaiseOnDelete(this, Transaction.RunningTransaction);

            if (force)
                ForceDelete();
            else
                Delete();
        }

        public void ForceDelete()
        {
            switch (PersistenceState)
            {
                case PersistenceState.New:
                case PersistenceState.NewAndChanged:
                    PersistenceState = PersistenceState.Persisted;
                    DbTransaction.Register(new ClearRelationshipsAction(DbTransaction.RelationshipPersistenceProvider, null, this, this));
                    break;
                case PersistenceState.HasUid:
                case PersistenceState.Loaded:
                case PersistenceState.LoadedAndChanged:
                    PersistenceState = PersistenceState.ForceDeleted;
                    DbTransaction.Register(new ClearRelationshipsAction(DbTransaction.RelationshipPersistenceProvider, null, this, this));
                    break;
                case PersistenceState.Deleted:
                case PersistenceState.ForceDeleted:
                    break;
                case PersistenceState.OutOfScope:
                    throw new InvalidOperationException("The transaction for this object has already ended.");
                case PersistenceState.Persisted:
                    throw new InvalidOperationException("This object was already flushed to the data store.");
                case PersistenceState.Error:
                    throw new InvalidOperationException("The object suffered an unexpected failure.");
                case PersistenceState.DoesntExist:
                    throw new InvalidOperationException($"{GetEntity().Name} with key {GetKey().ToString()} couldn't be loaded from the database.");
                default:
                    throw new NotImplementedException(string.Format("The PersistenceState '{0}' is not yet implemented.", PersistenceState.ToString()));
            }
        }

        public void Delete()
        {
            switch (PersistenceState)
            {
                case PersistenceState.New:
                case PersistenceState.NewAndChanged:
                    PersistenceState = PersistenceState.Persisted;
                    DbTransaction.Register(new ClearRelationshipsAction(DbTransaction.RelationshipPersistenceProvider, null, this, this));
                    break;
                case PersistenceState.HasUid:
                case PersistenceState.Loaded:
                case PersistenceState.LoadedAndChanged:
                    PersistenceState = PersistenceState.Deleted;
                    DbTransaction.Register(new ClearRelationshipsAction(DbTransaction.RelationshipPersistenceProvider, null, this, this));
                    break;
                case PersistenceState.Deleted:
                case PersistenceState.ForceDeleted:
                    break;
                case PersistenceState.OutOfScope:
                    throw new InvalidOperationException("The transaction for this object has already ended.");
                case PersistenceState.Persisted:
                    throw new InvalidOperationException("This object was already flushed to the data store.");
                case PersistenceState.Error:
                    throw new InvalidOperationException("The object suffered an unexpected failure.");
                case PersistenceState.DoesntExist:
                    throw new InvalidOperationException($"{GetEntity().Name} with key {GetKey().ToString()} couldn't be loaded from the database.");
                default:
                    throw new NotImplementedException(string.Format("The PersistenceState '{0}' is not yet implemented.", PersistenceState.ToString()));
            }
        }

        #region Explicit OGM

        object OGM.GetKey() { return this.GetKey(); }

        void OGM.SetKey(object key)
        {
            this.GetData().SetKey(key);
        }

        IDictionary<string, object> OGM.GetData()
        {
            return this.GetData().MapTo();
        }
        void OGM.SetData(IReadOnlyDictionary<string, object> data)
        {
            this.GetData().MapFrom(data);
        }

        PersistenceState OGM.PersistenceState { get { return this.PersistenceState; } set { this.PersistenceState = value; } }
        Transaction OGM.Transaction { get { return this.DbTransaction; } set { this.DbTransaction = value; } }

        #endregion

        internal abstract object GetKey();
        internal abstract Data GetData();

        public abstract PersistenceState PersistenceState { get; internal set; }
        internal Transaction DbTransaction { get; set; }

        internal NodePersistenceProvider PersistenceProvider { get; set; }

        public abstract Entity GetEntity();

        public override int GetHashCode()
        {
            object key = GetKey();
            if (key == null)
                return base.GetHashCode();

            return GetEntity().Name.GetHashCode() ^ key.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            OGM other = obj as OGM;
            if ((object)other == null)
                return false;

            if (GetEntity().Name != other.GetEntity().Name)
                return false;

            object key = GetKey();
            if (key == null)
                return base.Equals(other);

            object otherKey = other.GetKey();
            if (otherKey == null)
                return base.Equals(other);

            return key.Equals(otherKey);
        }

        internal protected abstract void LazyGet();
        internal protected abstract void LazySet();
        internal protected virtual bool LazySet<T>(Property property, T previousValue, T assignValue, DateTime? moment = null)
        {
            return property.RaiseOnChange<T>(this, previousValue, assignValue, moment, OperationEnum.Set);
        }

        #region Events

        private Dictionary<string, object> customState;
        internal IDictionary<string, object> CustomState
        {
            get
            {
                if (customState == null)
                {
                    lock (this)
                    {
                        if (customState == null)
                            customState = new Dictionary<string, object>();
                    }
                }
                return customState;
            }
        }

        public IReadOnlyList<EntityEventArgs> EventHistory
        {
            get
            {
                return eventHistory;
            }
        }
        internal void AppendEventHistory(EntityEventArgs args)
        {
            eventHistory.Add(args);
        }
        private List<EntityEventArgs> eventHistory = new List<EntityEventArgs>(16);

        #endregion
    }

    public interface ISetRuntimeType
    {
        void SetRuntimeTypes(Type returnType, Type classType);
    }
}
