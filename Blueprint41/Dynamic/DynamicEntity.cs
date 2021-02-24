using Blueprint41.Core;
using Blueprint41.Neo4j.Refactoring;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Dynamic
{
    public class DynamicEntity : DynamicObject, OGM
    {
        internal DynamicEntity(Entity entity, bool shouldExecute, object? initialize = null)
        {
            ShouldExecute = shouldExecute;
            Guid = entity.Parent.GenerateGuid(entity.Name);

            if (entity.Parent.IsUpgraded)
                throw new InvalidOperationException("You cannot use dynamic entity outside of the upgrade script.");

            DynamicEntityType = entity;

            DbTransaction = Transaction.Current;
            if (ShouldExecute)
                RunningTransaction.Register(this);

            foreach (Property item in entity.GetPropertiesOfBaseTypesAndSelf())
                RefactorActionPropertyAdded(item);

            if (initialize is null)
                return;

            Dictionary<string, object?> values = initialize.GetType().GetProperties().ToDictionary(x => x.Name, x => (object?)x.GetValue(initialize, null));
            foreach (KeyValuePair<string, object?> init in values)
            {
                Property? property = DynamicEntityType.Search(init.Key);
                if (property is null)
                    throw new ArgumentException(string.Format("The property '{0}' is not contained within entity '{1}'.", init.Key, entity.Name));


                if (property.PropertyType != PropertyType.Collection)
                {
                    if (!TrySetMember(init.Key, init.Value))
                        throw new ArgumentException(string.Format("The property '{0}' is not contained within entity '{1}'.", init.Key, entity.Name));
                }
                else
                {
                    if (init.Value is null)
                        continue; // or throw that you cannot delete the collection and should call the Clear method instead?

                    IEnumerable<DynamicEntity>? input = init.Value as IEnumerable<DynamicEntity>;
                    if (input is null)
                        input = (init.Value as IEnumerable<object>)?.Cast<DynamicEntity>();
                    if (input is null)
                        throw new InvalidCastException("should be a collection of DynamicEntity");

                    object? member;
                    if (!TryGetMember(init.Key, out member))
                        throw new ArgumentException(string.Format("The property '{0}' is not contained within entity '{1}'.", init.Key, entity.Name));

                    EntityCollection<DynamicEntity>? collection = member as EntityCollection<DynamicEntity>;
                    if (collection is null)
                        throw new ArgumentException(string.Format("The property '{0}' is not contained within entity '{1}'.", init.Key, entity.Name));

                    foreach (DynamicEntity item in input)
                    {
                        if (item.GetEntity() != property.EntityReturnType)
                            throw new InvalidCastException("Wrong type of object inserted in collection...");

                        collection.Add(item, false);
                    }
                }
            }
        }

        internal void RefactorActionPropertyAdded(Property item)
        {
            if (item.PropertyType == PropertyType.Attribute || item.Relationship is null)
                return;

            if (item.Relationship.IsTimeDependent)
                DynamicEntityLinks.Add(item.Name, new EntityTimeCollection<DynamicEntity>(this, item));
            else
                DynamicEntityLinks.Add(item.Name, new EntityCollection<DynamicEntity>(this, item));
        }
        internal void RefactorActionPropertyRenamed(string oldname, Property item, MergeAlgorithm mergeAlgorithm = MergeAlgorithm.NotApplicable)
        {
            if (item.PropertyType == PropertyType.Attribute)
            {
                switch (mergeAlgorithm)
                {
                    case MergeAlgorithm.NotApplicable:
                    case MergeAlgorithm.ThrowOnConflict:
                        {
                            object? oldvalue;
                            if (DynamicEntityValues.TryGetValue(oldname, out oldvalue))
                            {
                                DynamicEntityValues.Add(item.Name, oldvalue);
                                DynamicEntityValues.Remove(oldname);
                            }
                            break;
                        }
                    case MergeAlgorithm.PreferSource:
                        {
                            object? oldvalue;
                            if (DynamicEntityValues.TryGetValue(oldname, out oldvalue))
                            {
                                if (DynamicEntityValues.ContainsKey(item.Name))
                                    DynamicEntityValues[item.Name] = oldvalue;
                                else
                                    DynamicEntityValues.Add(item.Name, oldvalue);
                                DynamicEntityValues.Remove(oldname);
                            }
                            break;
                        }
                    case MergeAlgorithm.PreferTarget:
                        {
                            if (!DynamicEntityValues.ContainsKey(item.Name))
                            {
                                object? oldvalue;
                                if (DynamicEntityValues.TryGetValue(oldname, out oldvalue))
                                {
                                    DynamicEntityValues.Add(item.Name, oldvalue);
                                    DynamicEntityValues.Remove(oldname);
                                }
                            }
                            break;
                        }
                    default:
                        throw new NotImplementedException();
                }
            }
            else
            {
                switch (mergeAlgorithm)
                {
                    case MergeAlgorithm.NotApplicable:
                    case MergeAlgorithm.ThrowOnConflict:
                        {
                            EntityCollectionBase<DynamicEntity>? oldvalue;
                            if (DynamicEntityLinks.TryGetValue(oldname, out oldvalue))
                            {
                                DynamicEntityLinks.Add(item.Name, oldvalue);
                                DynamicEntityLinks.Remove(oldname);
                            }
                            break;
                        }
                    case MergeAlgorithm.PreferSource:
                        {
                            EntityCollectionBase<DynamicEntity>? oldvalue;
                            if (DynamicEntityLinks.TryGetValue(oldname, out oldvalue))
                            {
                                if (DynamicEntityLinks.ContainsKey(item.Name))
                                    DynamicEntityLinks[item.Name] = oldvalue;
                                else
                                    DynamicEntityLinks.Add(item.Name, oldvalue);
                                DynamicEntityLinks.Remove(oldname);
                            }
                            break;
                        }
                    case MergeAlgorithm.PreferTarget:
                        {
                            if (!DynamicEntityLinks.ContainsKey(item.Name))
                            {
                                EntityCollectionBase<DynamicEntity>? oldvalue;
                                if (DynamicEntityLinks.TryGetValue(oldname, out oldvalue))
                                {
                                    DynamicEntityLinks.Add(item.Name, oldvalue);
                                    DynamicEntityLinks.Remove(oldname);
                                }
                            }
                            break;
                        }
                    default:
                        throw new NotImplementedException();
                }
            }
        }
        internal void RefactorActionPropertyConverted(Property item, Type target)
        {
            if (item.PropertyType != PropertyType.Attribute || item.SystemReturnType is null)
                throw new NotSupportedException("Only primitive properties can have their return type converted.");

            object? oldValue;
            if (DynamicEntityValues.TryGetValue(item.Name, out oldValue))
            {
                object? newValue = Conversion.Convert(item.SystemReturnType, target, oldValue);
                DynamicEntityValues[item.Name] = newValue;
            }
        }
        internal void RefactorActionPropertyRerouted(string oldname, Entity to, Property item)
        {
            throw new NotSupportedException("Rerouting of static data is not yet supported.");
        }
        internal void RefactorActionPropertyRemoved(Property item)
        {
            if (item.PropertyType == PropertyType.Attribute)
                DynamicEntityValues.Remove(item.Name);
            else
                DynamicEntityLinks.Remove(item.Name);
        }

        private readonly Entity DynamicEntityType;
        private readonly Dictionary<string, object?> DynamicEntityValues = new Dictionary<string, object?>();
        private readonly Dictionary<string, EntityCollectionBase<DynamicEntity>> DynamicEntityLinks = new Dictionary<string, EntityCollectionBase<DynamicEntity>>();
        public Guid Guid { get; private set; }
        public IReadOnlyDictionary<string, object?> GetDynamicEntityValues()
        {
            return this.DynamicEntityValues;
        }
        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            return TryGetMember(binder.Name, out result);
        }

        public bool TryGetMember(string propertyName, out object? result)
        {
            result = null;
            Property? property = DynamicEntityType.Search(propertyName);
            if (property is null)
                return false;

            LazyGet();

            switch (property.PropertyType)
            {
                case PropertyType.Attribute:
                    {
                        DynamicEntityValues.TryGetValue(propertyName, out result);
                        break;
                    }
                case PropertyType.Collection:
                    {
                        EntityCollectionBase<DynamicEntity>? collection;
                        DynamicEntityLinks.TryGetValue(propertyName, out collection);
                        result = collection;
                        break;
                    }
                case PropertyType.Lookup:
                    {
                        EntityCollectionBase<DynamicEntity>? collection;
                        DynamicEntityLinks.TryGetValue(propertyName, out collection);
                        result = (collection is null) ? null : ((ILookupHelper<DynamicEntity>)collection).GetItem(null);
                        break;
                    }
            }
            return true;
        }
        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            return TrySetMember(binder.Name, value);
        }
        public bool TrySetMember(string name, object? value)
        {
            Property? property = DynamicEntityType.Search(name);
            if (property is null)
                return false;

            if (property.IsKey)
                KeyCheck();
            else
                LazySet();

            if (value is null)
            {
                if (DynamicEntityValues.ContainsKey(name))
                    DynamicEntityValues.Remove(name);
            }
            else
            {
                Type type = value.GetType();
                switch (property.PropertyType)
                {
                    case PropertyType.Attribute:
                        {
                            try
                            {
                                if (type != property.SystemReturnType)
                                    value = Convert.ChangeType(value, property.SystemReturnType!);
                            }
                            catch
                            {
                                throw new InvalidCastException($"Cannot cast type '{type.Name}' to '{property.SystemReturnType?.Name ?? "Unknown"}'");
                            }

                            if (DynamicEntityValues.ContainsKey(name))
                                DynamicEntityValues[name] = value;
                            else
                                DynamicEntityValues.Add(name, value);

                            break;
                        }
                    case PropertyType.Collection:
                        {
                            object? tmp;
                            TryGetMember(name, out tmp);
                            EntityCollectionBase<DynamicEntity>? collection = tmp as EntityCollectionBase<DynamicEntity>;
                            // TODO: add nice error for tmp not being a EntityCollectionBase<DynamicEntity>

                            // TODO: add nice error for value not being a IEnumerable<DynamicEntity>
                            foreach (DynamicEntity item in (IEnumerable<DynamicEntity>)value)
                                collection?.Add(item, false);

                            break;
                        }
                    case PropertyType.Lookup:
                        {
                            if (!typeof(DynamicEntity).IsAssignableFrom(type))
                                throw new InvalidCastException($"Cannot cast type '{type.Name}' to 'DynamicEntity'.");

                            DynamicEntity? node = value as DynamicEntity;
                            if (!(node is null) && node.GetEntity() != property.EntityReturnType)
                                return false;

                            EntityCollectionBase<DynamicEntity>? collection;
                            DynamicEntityLinks.TryGetValue(name, out collection);
                            ((ILookupHelper<DynamicEntity>?)collection)?.SetItem(node, null);

                            break;
                        }
                }
            }

            if (property.IsKey)
                KeySet();

            return true;
        }

        internal bool ShouldExecute = false;

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
                    RunningTransaction.NodePersistenceProvider.Insert(this);
                    PersistenceState = PersistenceState.Persisted;
                    break;
                case PersistenceState.LoadedAndChanged:
                    RunningTransaction.NodePersistenceProvider.Update(this);
                    PersistenceState = PersistenceState.Persisted;
                    break;
                case PersistenceState.Persisted:
                    break;
                case PersistenceState.Delete:
                    RunningTransaction.NodePersistenceProvider.Delete(this);
                    PersistenceState = PersistenceState.Deleted;
                    return;
                case PersistenceState.ForceDelete:
                    RunningTransaction.NodePersistenceProvider.ForceDelete(this);
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
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

            object? key = GetKey();
            //TODO:: Validation here
            foreach (Entity inherited in DynamicEntityType.GetBaseTypesAndSelf())
            {
                foreach (Property attr in inherited.Properties)
                {
                    if (attr.IsNodeType)
                        continue;

                    if (attr.IsRowVersion)
                        continue;

                    if (attr.IsKey && DynamicEntityType.FunctionalId is not null)
                        continue;

                    if (attr.Nullable == false && attr.PropertyType == PropertyType.Attribute)
                    {
                        object? value;
                        if (!TryGetMember(attr.Name, out value) || value is null)
                            throw new PersistenceException(string.Format("Cannot save {0} with key '{1}' because the {2} cannot be null.", DynamicEntityType.Name, key, attr.Name));
                    }

                    if (attr.Nullable == false && attr.PropertyType == PropertyType.Lookup)
                    {
                        object? value;
                        if (!TryGetMember(attr.Name, out value) || value is null)
                            throw new PersistenceException(string.Format("Cannot save {0} with key '{1}' because the {2} cannot be null.", DynamicEntityType.Name, key, attr.Name));
                    }
                }
            }
        }

        void OGM.ValidateDelete()
        {
            //TODO:: Validation here
            object? key = GetKey();
            foreach (var relationship in DynamicEntityType.Parent.Relations)
            {
                if (DynamicEntityType.IsSelfOrSubclassOf(relationship.InEntity) && relationship.OutProperty is not null && relationship.OutProperty.Nullable == false)
                {
                    if (RelationshipExists(relationship.OutProperty, this))
                        throw new PersistenceException(string.Format("Cannot delete {0} with key '{1}' because it is participating in a {2} relationship.", DynamicEntityType.Name, key, relationship.Neo4JRelationshipType));
                }
                if (DynamicEntityType.IsSelfOrSubclassOf(relationship.OutEntity) && relationship.InProperty is not null && relationship.InProperty.Nullable == false)
                {
                    if (RelationshipExists(relationship.InProperty, this))
                        throw new PersistenceException(string.Format("Cannot delete {0} with key '{1}' because it is participating in a {2} relationship.", DynamicEntityType.Name, key, relationship.Neo4JRelationshipType));
                }
            }
        }

        internal object? GetKey()
        {
            return ((OGM)this).GetKey();
        }

        object? OGM.GetKey()
        {
            if (DynamicEntityType.Key is null)
                throw new MissingMemberException($"No key exists of entity '{DynamicEntityType.Name}'");

            DynamicEntityValues.TryGetValue(DynamicEntityType.Key.Name, out object? key);

            if (DbTransaction is null)
            {
                Conversion? converter;
                if (!PersistenceProvider.ConvertToStoredTypeCache.TryGetValue(DynamicEntityType.Key.SystemReturnType!, out converter))
                    return key;

                if (converter is null)
                    return key;

                return converter.Convert(key);
            }

            return PersistenceProvider.ConvertToStoredType(DynamicEntityType.Key.SystemReturnType!, key);
        }

        void OGM.SetKey(object key)
        {
            object? convertedKey = PersistenceProvider.ConvertFromStoredType(DynamicEntityType.Key.SystemReturnType!, key);
            if (DynamicEntityValues.ContainsKey(DynamicEntityType.Key.Name))
                DynamicEntityValues[DynamicEntityType.Key.Name] = convertedKey;
            else
                DynamicEntityValues.Add(DynamicEntityType.Key.Name, convertedKey);

            if (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged)
                return;

            PersistenceState = PersistenceState.HasUid;

            if (ShouldExecute)
                Transaction.RunningTransaction.Register(DynamicEntityType.Name, this);
        }
        void OGM.SetRowVersion(DateTime? value)
        {
            Entity entity = GetEntity();
            if (entity.RowVersion is null)
                throw new InvalidOperationException($"The entity '{entity.Name}' does not have a row version field set.");

            if (!TrySetMember(entity.RowVersion.Name, value ?? DateTime.MinValue))
                throw new InvalidOperationException($"Unable to set the row version field '{entity.RowVersion.Name}'.");
        }
        DateTime OGM.GetRowVersion()
        {
            Entity entity = GetEntity();
            if (entity.RowVersion is null)
                throw new InvalidOperationException($"The entity '{entity.Name}' does not have a row version field set.");

            object? result;
            if (!TryGetMember(entity.RowVersion.Name, out result))
                return DateTime.MinValue;

            return (DateTime?)result ?? DateTime.MinValue;
        }
        private void KeyCheck()
        {
            if (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged)
                throw new InvalidOperationException("You cannot set the key unless if the object is a newly created one.");

            // this check is not desired because we want to be able to update PK value during script and it's a bit slow on top of that...
            //if (HasKey)
            //    throw new InvalidOperationException("You cannot set the key multiple times.");
        }
        private void KeySet()
        {
            if (ShouldExecute)
                RunningTransaction.Register(DynamicEntityType.Name, this);
        }

        private void LazySet()
        {
            if (PersistenceState == PersistenceState.Persisted && DbTransaction != Transaction.RunningTransaction)
                throw new InvalidOperationException("This object was already flushed to the data store.");
            else if (PersistenceState == PersistenceState.OutOfScope)
                throw new InvalidOperationException("The transaction for this object has already ended.");

            LazyGet();

            if (PersistenceState == PersistenceState.New)
                PersistenceState = PersistenceState.NewAndChanged;
            else if (PersistenceState == PersistenceState.Loaded)
                PersistenceState = PersistenceState.LoadedAndChanged;
        }

        private void LazyGet()
        {
            switch (PersistenceState)
            {
                case PersistenceState.New:
                case PersistenceState.NewAndChanged:
                    break;
                case PersistenceState.HasUid:
                    RunningTransaction.NodePersistenceProvider.Load(this);
                    break;
                case PersistenceState.Loaded:
                case PersistenceState.LoadedAndChanged:
                case PersistenceState.OutOfScope:
                case PersistenceState.Persisted:
                case PersistenceState.Delete:
                case PersistenceState.ForceDelete:
                    break;
                case PersistenceState.Deleted:
                    throw new InvalidOperationException("The object has been deleted, you cannot make changes to it anymore.");
                case PersistenceState.Error:
                    throw new InvalidOperationException("The object suffered an unexpected failure.");
                case PersistenceState.DoesntExist:
                    throw new InvalidOperationException($"{GetEntity().Name} with key {GetKey()?.ToString() ?? "<NULL>"} couldn't be loaded from the database.");
                default:
                    throw new NotImplementedException(string.Format("The PersistenceState '{0}' is not yet implemented.", PersistenceState.ToString()));
            }
        }

        IDictionary<string, object?> OGM.GetData()
        {
            return DynamicEntityValues.ToDictionary(item => item.Key, item => PersistenceProvider.ConvertToStoredType(DynamicEntityType.Search(item.Key)!.SystemReturnType!, item.Value));
        }

        void OGM.SetData(IReadOnlyDictionary<string, object?> data)
        {
            DynamicEntityValues.Clear();
            foreach (KeyValuePair<string, object?> item in data)
            {
                DynamicEntityValues.Add(item.Key, PersistenceProvider.ConvertFromStoredType(DynamicEntityType.Search(item.Key)!.SystemReturnType!, item.Value));
            }
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
                    PersistenceState = PersistenceState.Deleted;
                    ExecuteAction(new ClearRelationshipsAction(RunningTransaction.RelationshipPersistenceProvider, null, this, this));
                    break;
                case PersistenceState.HasUid:
                case PersistenceState.Loaded:
                case PersistenceState.LoadedAndChanged:
                    PersistenceState = PersistenceState.ForceDelete;
                    ExecuteAction(new ClearRelationshipsAction(RunningTransaction.RelationshipPersistenceProvider, null, this, this));
                    break;
                case PersistenceState.Delete:
                case PersistenceState.ForceDelete:
                    break;
                case PersistenceState.OutOfScope:
                    throw new InvalidOperationException("The transaction for this object has already ended.");
                case PersistenceState.Persisted:
                    if (DbTransaction != Transaction.RunningTransaction)
                        throw new InvalidOperationException("This object was already flushed to the data store.");
                    break;
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
                    PersistenceState = PersistenceState.Deleted;
                    ExecuteAction(new ClearRelationshipsAction(RunningTransaction.RelationshipPersistenceProvider, null, this, this));
                    break;
                case PersistenceState.HasUid:
                case PersistenceState.Loaded:
                case PersistenceState.LoadedAndChanged:
                    PersistenceState = PersistenceState.Delete;
                    ExecuteAction(new ClearRelationshipsAction(RunningTransaction.RelationshipPersistenceProvider, null, this, this));
                    break;
                case PersistenceState.Delete:
                case PersistenceState.ForceDelete:
                    break;
                case PersistenceState.OutOfScope:
                    throw new InvalidOperationException("The transaction for this object has already ended.");
                case PersistenceState.Persisted:
                    if (DbTransaction != Transaction.RunningTransaction)
                        throw new InvalidOperationException("This object was already flushed to the data store.");
                    break;
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

        public Entity GetEntity()
        {
            return DynamicEntityType;
        }

        public PersistenceProvider PersistenceProvider
        {
            get
            {
                return GetEntity().Parent.PersistenceProvider;
            }
        }

        public PersistenceState PersistenceState
        {
            get
            {
                return ((OGM)this).PersistenceState;
            }
            internal set
            {
                ((OGM)this).PersistenceState = value;
            }
        }

        PersistenceState OGM.PersistenceState { get; set; }

        public Transaction? DbTransaction
        {
            get
            {
                return ((OGM)this).Transaction;
            }
            internal set
            {
                ((OGM)this).Transaction = value;
            }
        }
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
        Transaction? OGM.Transaction { get; set; }

        public bool HasKey
        {
            get
            {
                object? key = ((OGM)this).GetKey();
                return (key is not null);
            }
        }

        protected bool RelationshipExists(Property foreignProperty, OGM instance)
        {
            return RunningTransaction.NodePersistenceProvider.RelationshipExists(foreignProperty, instance);
        }

        public static dynamic? Load(Entity entity, object? key)
        {
            if (key is null)
                return null;

            DynamicEntity? item = Lookup(entity, key);
            if (item is null)
                return null;

            item.LazyGet();

            if (item.PersistenceState != PersistenceState.New && item.PersistenceState != PersistenceState.DoesntExist)
                return item;
            else
                return null;
        }
        public static dynamic? Lookup(Entity entity, object? key)
        {
            if (key is null)
                return null;

            DynamicEntity? instance = (DynamicEntity?)Transaction.RunningTransaction.GetEntityByKey(entity.Name, key);
            if (instance is not null)
                return instance;

            DynamicEntity item = new DynamicEntity(entity, Parser.ShouldExecute);
            ((OGM)item).SetKey(key);

            return item;
        }
        public static void Delete(Entity entity, object key)
        {
            DynamicEntity? item = Load(entity, key);
            if (item is null || (Parser.ShouldExecute && entity.ContainsStaticData))
                return;

            item.Delete();
        }
    }
}
