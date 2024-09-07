using Blueprint41.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using persistence = Blueprint41.Persistence;

namespace Blueprint41.Core
{
    public abstract class OGMImpl : OgmClass
    {
        protected OGMImpl()
        {
        }

        protected override void SetChanged()
        {
            if (PersistenceState == PersistenceState.New || PersistenceState == PersistenceState.NewAndChanged)
                PersistenceState = PersistenceState.NewAndChanged;
            else if (PersistenceState == PersistenceState.Loaded || PersistenceState == PersistenceState.LoadedAndChanged)
                PersistenceState = PersistenceState.LoadedAndChanged;
            else
                throw new InvalidOperationException(string.Format("To be able to call this method, the {0} PersistenceState must either be 'New' or 'Loaded'", GetEntity().Name));
        }
        public override void Save()
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
        protected bool RelationshipExists(EntityProperty foreignProperty, OGM instance)
        {
            return PersistenceProvider.NodePersistenceProvider.RelationshipExists(foreignProperty, instance);
        }

        public void ForceDelete()
        {
            switch (PersistenceState)
            {
                case PersistenceState.New:
                case PersistenceState.NewAndChanged:
                    GetEntity().RaiseOnDelete(this, Transaction.RunningTransaction);
                    PersistenceState = PersistenceState.Deleted;
                    ExecuteAction(new persistence.ClearRelationshipsAction(RunningTransaction.RelationshipPersistenceProvider, this));
                    break;
                case PersistenceState.HasUid:
                case PersistenceState.Loaded:
                case PersistenceState.Persisted:
                case PersistenceState.LoadedAndChanged:
                    GetEntity().RaiseOnDelete(this, Transaction.RunningTransaction);
                    PersistenceState = PersistenceState.ForceDelete;
                    ExecuteAction(new persistence.ClearRelationshipsAction(RunningTransaction.RelationshipPersistenceProvider, this));
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
                    ExecuteAction(new persistence.ClearRelationshipsAction(RunningTransaction.RelationshipPersistenceProvider, this));
                    break;
                case PersistenceState.HasUid:
                case PersistenceState.Loaded:
                case PersistenceState.Persisted:
                case PersistenceState.LoadedAndChanged:
                    GetEntity().RaiseOnDelete(this, Transaction.RunningTransaction);
                    PersistenceState = PersistenceState.Delete;
                    ExecuteAction(new persistence.ClearRelationshipsAction(RunningTransaction.RelationshipPersistenceProvider, this));
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
        public override void Delete(bool force)
        {
            if (force)
                ForceDelete();
            else
                Delete();
        }

        private protected void ExecuteAction(persistence.ClearRelationshipsAction action)
        {
            if (Transaction is not null)
            {
                Transaction.Register(action);
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

        //protected override IDictionary<string, object?> GetData()
        //{
        //    return this.GetData().MapTo();
        //}
        //protected override void SetData(IReadOnlyDictionary<string, object?> data)
        //{
        //    this.GetData().MapFrom(data);
        //    AfterSetData();
        //}

        protected override void SetRowVersion(DateTime? value)
        {
            Entity entity = GetEntity();
            if (entity.RowVersion is null)
                throw new InvalidOperationException($"The entity '{entity.Name}' does not have a row version field set.");

            entity.RowVersion.SetValue(this, value ?? DateTime.MinValue);
        }
        protected override DateTime GetRowVersion()
        {
            Entity entity = GetEntity();
            if (entity.RowVersion is null)
                throw new InvalidOperationException($"The entity '{entity.Name}' does not have a row version field set.");

            return (DateTime?)entity.RowVersion.GetValue(this) ?? DateTime.MinValue;
        }

        public override persistence.PersistenceProvider PersistenceProvider
        {
            get
            {
                if (persistenceProvider is null)
                    persistenceProvider = GetEntity().Parent.PersistenceProvider;

                return persistenceProvider;
            }
        }
        private persistence.PersistenceProvider? persistenceProvider = null;

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
}
