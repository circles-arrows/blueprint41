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
        }

        public void SetChanged()
        {
            if (PersistenceState == PersistenceState.New || PersistenceState == PersistenceState.NewAndChanged)
                PersistenceState = PersistenceState.NewAndChanged;
            else if (PersistenceState == PersistenceState.Loaded || PersistenceState == PersistenceState.LoadedAndChanged)
                PersistenceState = PersistenceState.LoadedAndChanged;
            else
                throw new InvalidOperationException(string.Format("To be able to call this method, the {0} PersistenceState must either be 'New' or 'Loaded'", GetEntity().Name));
        }
        void OGM.Save()
        {
            switch (PersistenceState)
            {
                case PersistenceState.New:
                    throw new NotSupportedException(string.Format("You created an instance of {0}, but inside this transaction but did not set any properties. If you did this intentionally, you can call the method 'SetChanged' on the instance before committing the transaction.", GetEntity().Name));
                case PersistenceState.HasUid:
                case PersistenceState.Loaded:
                    break;
                case PersistenceState.NewAndChanged:
                    PersistenceProvider.NodePersistenceProvider.Insert(this);
                    PersistenceState = PersistenceState.Persisted;
                    break;
                case PersistenceState.LoadedAndChanged:
                    PersistenceProvider.NodePersistenceProvider.Update(this);
                    PersistenceState = PersistenceState.Persisted;
                    break;
                case PersistenceState.Persisted:
                    break;
                case PersistenceState.Delete:
                    PersistenceProvider.NodePersistenceProvider.Delete(this);
                    PersistenceState = PersistenceState.Deleted;
                    return;
                case PersistenceState.ForceDelete:
                    PersistenceProvider.NodePersistenceProvider.ForceDelete(this);
                    PersistenceState = PersistenceState.Deleted;
                    return;
                case PersistenceState.OutOfScope:
                case PersistenceState.Error:
                    throw new InvalidOperationException(string.Format("The {0} with key '{1}' cannot be saved because it's state was {2}.", GetEntity().Name, GetKey() ?? "<null>", PersistenceState.ToString()));
                case PersistenceState.DoesntExist:
                    throw new InvalidOperationException($"{GetEntity().Name} with key {GetKey()?.ToString() ?? "<NULL>"} couldn't be loaded from the database.");
                default:
                    throw new NotImplementedException(string.Format("The {0} with key '{1}' has an invalid/unknown state {2}.", GetEntity().Name, GetKey() ?? "<null>", PersistenceState.ToString()));
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
        protected bool RelationshipExists(EntityProperty foreignProperty, OGM instance)
        {
            return PersistenceProvider.NodePersistenceProvider.RelationshipExists(foreignProperty, instance);
        }

        void OGM.Delete(bool force)
        {
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
                    GetEntity().RaiseOnDelete(this, Transaction.RunningTransaction);
                    PersistenceState = PersistenceState.Deleted;
                    ExecuteAction(new ClearRelationshipsAction(RunningTransaction.RelationshipPersistenceProvider, this));
                    break;
                case PersistenceState.HasUid:
                case PersistenceState.Loaded:
                case PersistenceState.Persisted:
                case PersistenceState.LoadedAndChanged:
                    GetEntity().RaiseOnDelete(this, Transaction.RunningTransaction);
                    PersistenceState = PersistenceState.ForceDelete;
                    ExecuteAction(new ClearRelationshipsAction(RunningTransaction.RelationshipPersistenceProvider, this));
                    break;
                case PersistenceState.Delete:
                case PersistenceState.ForceDelete:
                    break;
                case PersistenceState.OutOfScope:
                    throw new InvalidOperationException("The transaction for this object has already ended.");
                case PersistenceState.Error:
                    throw new InvalidOperationException("The object suffered an unexpected failure.");
                case PersistenceState.DoesntExist:
                    throw new InvalidOperationException($"{GetEntity().Name} with key {GetKey()?.ToString() ?? "<NULL>"} couldn't be loaded from the database.");
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
                    GetEntity().RaiseOnDelete(this, Transaction.RunningTransaction);
                    PersistenceState = PersistenceState.Deleted;
                    ExecuteAction(new ClearRelationshipsAction(RunningTransaction.RelationshipPersistenceProvider, this));
                    break;
                case PersistenceState.HasUid:
                case PersistenceState.Loaded:
                case PersistenceState.Persisted:
                case PersistenceState.LoadedAndChanged:
                    GetEntity().RaiseOnDelete(this, Transaction.RunningTransaction);
                    PersistenceState = PersistenceState.Delete;
                    ExecuteAction(new ClearRelationshipsAction(RunningTransaction.RelationshipPersistenceProvider, this));
                    break;
                case PersistenceState.Delete:
                case PersistenceState.ForceDelete:
                    break;
                case PersistenceState.OutOfScope:
                    throw new InvalidOperationException("The transaction for this object has already ended.");
                case PersistenceState.Error:
                    throw new InvalidOperationException("The object suffered an unexpected failure.");
                case PersistenceState.DoesntExist:
                    throw new InvalidOperationException($"{GetEntity().Name} with key {GetKey()?.ToString() ?? "<NULL>"} couldn't be loaded from the database.");
                default:
                    throw new NotImplementedException(string.Format("The PersistenceState '{0}' is not yet implemented.", PersistenceState.ToString()));
            }
        }
        private protected void ExecuteAction(ClearRelationshipsAction action)
        {
            if (DbTransaction is not null)
            {
                DbTransaction?.Register(action);
            }
            else
            {
                foreach (Property property in GetEntity().Properties.Where(item => item.PropertyType != PropertyType.Attribute))
                {
                    if (property.ForeignProperty is null)
                        continue;

                    IEnumerable<OGM> instances;
                    object value = property.GetValue(this, null);
                    if (property.PropertyType == PropertyType.Lookup)
                        instances = new OGM[] { (OGM)value };
                    else
                        instances = (IEnumerable<OGM>)value;

                    foreach (OGM instance in instances)
                    {
                        if (property.ForeignProperty.PropertyType == PropertyType.Lookup)
                        {
                            property.ForeignProperty.SetValue(instance, null);
                        }
                        else
                        {
                            EntityCollectionBase collection = (EntityCollectionBase)property.ForeignProperty.GetValue(instance, null);
                            action.ExecuteInMemory(collection);
                        }
                    }
                    property.SetValue(this, null);
                }
            }
        }

        #region Explicit OGM

        object? OGM.GetKey() { return this.GetKey(); }
        void OGM.SetKey(object key)
        {
            this.GetData().SetKey(key);
        }

        DateTime OGM.GetRowVersion() { return this.GetRowVersion(); }

        IDictionary<string, object?> OGM.GetData()
        {
            return this.GetData().MapTo();
        }
        void OGM.SetData(IReadOnlyDictionary<string, object?> data)
        {
            this.GetData().MapFrom(data);
            AfterSetData();
        }

        PersistenceState OGM.PersistenceState { get { return this.PersistenceState; } set { this.PersistenceState = value; } }
        PersistenceState OGM.OriginalPersistenceState { get { return this.OriginalPersistenceState; } set { this.OriginalPersistenceState = value; } }
        Transaction? OGM.Transaction { get { return this.DbTransaction; } set { this.DbTransaction = value; } }

        #endregion

        internal abstract object? GetKey();
        internal abstract Data GetData();

        public virtual void SetRowVersion(DateTime? value)
        {
            Entity entity = GetEntity();
            if (entity.RowVersion is null)
                throw new InvalidOperationException($"The entity '{entity.Name}' does not have a row version field set.");

            entity.RowVersion.SetValue(this, value ?? DateTime.MinValue);
        }
        internal protected virtual DateTime GetRowVersion()
        {
            Entity entity = GetEntity();
            if (entity.RowVersion is null)
                throw new InvalidOperationException($"The entity '{entity.Name}' does not have a row version field set.");

            return (DateTime?)entity.RowVersion.GetValue(this) ?? DateTime.MinValue;
        }

        public abstract PersistenceState PersistenceState { get; internal set; }
        public abstract PersistenceState OriginalPersistenceState { get; internal set; }
        internal Transaction? DbTransaction { get; set; }
        protected private Transaction RunningTransaction
        {
            get
            {
                Transaction? trans = DbTransaction;

                if (trans is null)
                    throw new InvalidOperationException("There is no transaction, you should create one first -> using (Transaction.Begin()) { ... Transaction.Commit(); }");

                if (!trans.InTransaction)
                    throw new InvalidOperationException("The transaction was already committed or rolled back.");

                return trans;
            }
        }

        public abstract Entity GetEntity();

        public PersistenceProvider PersistenceProvider
        {
            get
            {
                if (persistenceProvider is null)
                    persistenceProvider = GetEntity().Parent.PersistenceProvider;

                return persistenceProvider;
            }
        }
        private PersistenceProvider? persistenceProvider = null;

        public override int GetHashCode()
        {
            object? key = GetKey();
            if (key is null)
                return base.GetHashCode();

            return HashCode.Combine(GetEntity().Name, key);
        }
        public override bool Equals(object? obj)
        {
            OGM? other = obj as OGM;
            if (other is null)
                return false;

            if (GetEntity().Name != other.GetEntity().Name)
                return false;

            object? key = GetKey();
            if (key is null)
                return object.ReferenceEquals(this, other);

            object? otherKey = other.GetKey();
            if (otherKey is null)
                return object.ReferenceEquals(this, other);

            return key.Equals(otherKey);
        }

        internal protected abstract void LazyGet(bool locked = false);
        internal protected abstract void LazySet();
        internal protected abstract void AfterSetData();
        internal protected virtual bool LazySet<T>(Property property, T previousValue, T assignValue, DateTime? moment)
        {
            return property.RaiseOnChange<T>(this, previousValue, assignValue, moment, OperationEnum.Set);
        }
        internal protected virtual bool LazySet<T>(Property property, IEnumerable<CollectionItem<T>> previousValues, T assignValue, DateTime? moment)
            where T : OGM
        {
            CollectionItem<T>? prevColItem = previousValues.FirstOrDefault(item => item.Overlaps(moment ?? Conversion.MinDateTime));
            T? previousValue = (prevColItem is null) ? default : prevColItem.Item;

            return property.RaiseOnChange<T>(this, previousValue, assignValue, moment, OperationEnum.Set);
        }

        #region Events

        private Dictionary<string, object?>? customState = null;
        internal IDictionary<string, object?> CustomState
        {
            get
            {
                if (customState is null)
                {
                    lock (this)
                    {
                        if (customState is null)
                            customState = new Dictionary<string, object?>();
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

        protected TEnum? Parse<TEnum>(string self)
            where TEnum : struct
        {
            if (self is null)
                return null;

            TEnum result;
            if (!Enum.TryParse<TEnum>(self, out result))
                return null;

            return result;
        }
    }

    public interface ISetRuntimeType
    {
        [Obsolete("This method is reserved for internal use by the generated code", true)]
        void SetRuntimeTypes(Type returnType, Type classType);
    }
    public interface IEntityAdvancedFeatures
    {
        /// <summary>
        /// Create a new node
        /// </summary>
        /// <param name="eventOptions">Which events should be fired during the creation</param>
        /// <returns>The new node</returns>
        OGM Activator(EventOptions eventOptions = EventOptions.GraphEvents);

        /// <summary>
        /// Map a node loaded via a query into a new node instance
        /// </summary>
        /// <param name="node">The raw cypher node</param>
        /// <param name="mappingMode">The node mapping mode</param>
        /// <returns>The new node</returns>
        OGM? Map(RawNode node, NodeMapping mappingMode);

        /// <summary>
        /// Map a node loaded via a query into a new node instance
        /// </summary>
        /// <param name="node">The raw cypher node</param>
        /// <param name="cypher">The cypher query</param>
        /// <param name="parameters">The cypher query parameters</param>
        /// <param name="mappingMode">The node mapping mode</param>
        /// <returns>The new node</returns>
        OGM? Map(RawNode node, string cypher, Dictionary<string, object?>? parameters, NodeMapping mappingMode);

        /// <summary>
        /// Load an existing node
        /// </summary>
        /// <param name="key">The primary key value for the node to be loaded</param>
        /// <param name="locked">If the node should be locked</param>
        /// <returns>The node</returns>
        OGM? Load(object? key, bool locked = false);

        /// <summary>
        /// Load an existing node from the transaction cache
        /// </summary>
        /// <param name="key">The primary key value for the node to be loaded</param>
        /// <returns>The node</returns>
        OGM? Lookup(object? key);

        /// <summary>
        /// Force-delete an existing node
        /// </summary>
        /// <param name="key">The primary key value for the node to be deleted</param>
        void ForceDelete(object key);

        /// <summary>
        /// Delete an existing node
        /// </summary>
        /// <param name="key">The primary key value for the node to be deleted</param>
        void Delete(object key);
    }
}
