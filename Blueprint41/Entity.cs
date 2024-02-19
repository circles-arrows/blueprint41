﻿using Blueprint41.Dynamic;
using Blueprint41.Neo4j.Refactoring;
using Blueprint41.Neo4j.Refactoring.Templates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = Blueprint41.Neo4j.Model;
using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using System.Linq.Expressions;
using Blueprint41.Query;
using System.Reflection;

namespace Blueprint41
{
    [DebuggerDisplay("Entity: {Name}")]
    public partial class Entity : IRefactorEntity, IEntityEvents, ISetRuntimeType, IEntityAdvancedFeatures
    {
        internal Entity(DatastoreModel parent, string name, string label, string? prefix, Entity? inherits)
            : this(parent, name, label, GetFunctionalId(parent, name, label, prefix), inherits)
        {
        }
        internal Entity(DatastoreModel parent, string name, string label, FunctionalId? functionalId, Entity? inherits)
        {
            _properties = new PropertyCollection(this);
            Properties = new EntityPropertyCollection<EntityProperty, Entity>(this, _properties);

            Parent = parent;
            Name = name;
            IsAbstract = false;
            IsVirtual = false;
            Guid = parent.GenerateGuid(name);

            Inherits = inherits;

            Parent.Labels.New(label);
            Label = Parent.Labels[label];

            if (inherits?.FunctionalId is not null && functionalId is not null)
                throw new InvalidOperationException($"The entity '{name}' already inherited a functional id '{(inherits?.FunctionalId ?? functionalId).Prefix} ({(inherits?.FunctionalId ?? functionalId).Label})', you cannot assign another one.");

            this.functionalId = functionalId;

            key = new Lazy<EntityProperty>(delegate () { return GetPropertiesOfBaseTypesAndSelf().SingleOrDefault(x => x.IsKey); }, true);
            nodeType = new Lazy<EntityProperty>(delegate () { return GetPropertiesOfBaseTypesAndSelf().SingleOrDefault(x => x.IsNodeType); }, true);
            rowVersion = new Lazy<EntityProperty>(delegate () { return GetPropertiesOfBaseTypesAndSelf().SingleOrDefault(x => x.IsRowVersion); }, true);

            Parent.SubModels["Main"].AddEntityInternal(this);
        }

        static private FunctionalId? GetFunctionalId(DatastoreModel parent, string name, string label, string? prefix)
        {
            FunctionalId? functionalId = null;
            if (prefix is not null)
            {
                if (string.IsNullOrWhiteSpace(prefix))
                    throw new NotSupportedException($"The prefix cannot be empty or blank for entity '{name}'.");

                functionalId = parent.FunctionalIds.FirstOrDefault(item => item.Label == label);
                if (functionalId is null)
                    functionalId = parent.FunctionalIds.New(label, prefix);
            }
            return functionalId;
        }

        #region Properties

        public DatastoreModel Parent { get; private set; }
        public string Name { get; private set; }

        public string ClassName
        {
            get
            {
                if (IsAbstract)
                    return string.Concat("I", Name);

                return Name;
            }
        }
        public string? Description { get; private set; }
        public Guid Guid { get; private set; }

        private FunctionalId? functionalId = null;
        public FunctionalId? FunctionalId
        {
            get
            {
                if (functionalId is null && !IsAbstract)
                    functionalId = FindFunctionalId();

                return functionalId;

                FunctionalId? FindFunctionalId()
                {
                    Entity? entity = this.Inherits;
                    while(entity is not null)
                    {
                        if (entity.functionalId is not null)
                            return entity.functionalId;

                        entity = entity.Inherits;
                    }
                    return Parent.FunctionalIds.Default;
                };
            }
        }

        public bool IsAbstract { get; private set; }
        public bool IsVirtual { get; private set; }

        private bool containsStaticData = false;
        public bool ContainsStaticData { get { return (containsStaticData || staticData.Count != 0); } }
        public Entity? Inherits { get; private set; }

        public model.Label Label { get; private set; }

        public bool HaveRelationship
        {
            get
            {
                return Parent.Relations.Any(relationship => relationship.InEntity == this || relationship.OutEntity == this);
            }
        }
        public IEnumerable<Relationship> GetRelationships()
        {
            return Parent.Relations.Where(relationship => relationship.InEntity == this || relationship.OutEntity == this);
        }
        public IEnumerable<Relationship> GetRelationshipsRecursive()
        {
            return Parent.Relations.Where(relationship => IsSelfOrSubclassOf(relationship.InEntity) || IsSelfOrSubclassOf(relationship.OutEntity));
        }

        #endregion

        public Entity SetFullTextProperty(string propertyName)
        {
            EntityProperty? property = Search(propertyName);
            if (property is null)
                throw new NotSupportedException("Property does not exist.");

            fullTextIndexProperties.Add(property);
            return this;
        }
        public Entity RemoveFullTextProperty(string propertyName)
        {
            EntityProperty? property = Search(propertyName);
            if (property is null)
                throw new NotSupportedException("Property does not exist.");

            fullTextIndexProperties.Remove(property);
            return this;
        }


        public Entity AddProperty(string name, Type type)
        {
            return AddProperty(name, type, true, IndexType.None);
        }
        public Entity AddProperty(string name, string[] enumeration, bool nullable = true, IndexType indexType = IndexType.None)
        {
            VerifyFromInheritedProperties(name);

            EntityProperty value = new EntityProperty(this, PropertyType.Attribute, name, typeof(string), nullable, indexType, enumeration);
            Properties.Add(name, value);

            // StaticData
            DynamicEntityPropertyAdded(value);

            return this;
        }
        public Entity AddProperty(string name, Enumeration enumeration, bool nullable = true, IndexType indexType = IndexType.None)
        {
            VerifyFromInheritedProperties(name);

            EntityProperty value = new EntityProperty(this, PropertyType.Attribute, name, typeof(string), nullable, indexType, enumeration);
            Properties.Add(name, value);

            // StaticData
            DynamicEntityPropertyAdded(value);

            return this;
        }
        public Entity AddProperty(string name, Type type, IndexType indexType)
        {
            return AddProperty(name, type, true, indexType);
        }
        public Entity AddProperty(string name, Type type, bool nullable, IndexType indexType = IndexType.None)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                throw new ArgumentException(string.Format("The type argument does not support the 'Nullable<{0}>' type. All types are considered nullable by default, but you can also set the 'nullable' argument explicitly.", type.GenericTypeArguments[0].Name));

            VerifyFromInheritedProperties(name);

            EntityProperty value = new EntityProperty(this, PropertyType.Attribute, name, type, nullable, indexType);
            Properties.Add(name, value);

            // StaticData
            DynamicEntityPropertyAdded(value);

            return this;
        }
        public Entity AddUnidentifiedProperties(string name)
        {
            UnidentifiedProperties = name;

            return this;
        }
        public Entity RemoveUnidentifiedProperties()
        {
            UnidentifiedProperties = null;

            return this;
        }
        internal Entity AddLookup(string name, Entity type, bool nullable = true)
        {
            EntityProperty value = new EntityProperty(this, PropertyType.Lookup, name, type, nullable, IndexType.None);
            Properties.Add(name, value);

            return this;
        }
        internal Entity AddCollection(string name, Entity type)
        {
            EntityProperty value = new EntityProperty(this, PropertyType.Collection, name, type, true, IndexType.None);
            Properties.Add(name, value);

            return this;
        }

        public IReadOnlyList<EntityProperty> FullTextIndexProperties
        {
            get { return fullTextIndexProperties; }
        }
        private List<EntityProperty> fullTextIndexProperties = new List<EntityProperty>();

        public EntityPropertyCollection<EntityProperty, Entity> Properties { get; private set; }
        private readonly PropertyCollection _properties;

        public string? UnidentifiedProperties { get; private set; }

        public Entity? InheritedUnidentifiedProperties()
        {
            if (!string.IsNullOrEmpty(UnidentifiedProperties))
                return this;

            return Inherits?.InheritedUnidentifiedProperties();
        }


        private Lazy<EntityProperty> key;
        public EntityProperty Key
        {
            get
            {
                if (Parent.IsUpgraded)
                    return key.Value;

                return GetPropertiesOfBaseTypesAndSelf().SingleOrDefault(x => x.IsKey);
            }
        }
        private Lazy<EntityProperty> nodeType;
        public EntityProperty NodeType
        {
            get
            {
                if (Parent.IsUpgraded)
                    return nodeType.Value;

                return GetPropertiesOfBaseTypesAndSelf().SingleOrDefault(x => x.IsNodeType);
            }
        }
        public string NodeTypeName
        {
            get
            {
                return (NodeType is null) ? "NodeType" : NodeType.Name;
            }
        }
        private Lazy<EntityProperty> rowVersion;
        public EntityProperty RowVersion
        {
            get
            {
                if (Parent.IsUpgraded)
                    return rowVersion.Value;

                return GetPropertiesOfBaseTypesAndSelf().SingleOrDefault(x => x.IsRowVersion);
            }
        }

        public Entity Abstract(bool isAbstract = true)
        {
            IsAbstract = isAbstract;

            return this;
        }
        public Entity Virtual(bool isVirtual = true)
        {
            IsVirtual = isVirtual;

            return this;
        }
        public Entity SetKey(string property, bool autoGenerate)
        {
            IReadOnlyList<Property> properties = GetPropertiesOfBaseTypesAndSelf();
            if (properties.Where(x => x.Name != property && x.IsKey).Count() > 0)
                throw new NotSupportedException("Multiple key not allowed.");

            if (Properties[property].IndexType != IndexType.Unique)
                throw new NotSupportedException("You cannot make a non-unique field the key.");

            Properties[property].IsKey = true;
            return this;
        }
        public Entity SetNodeType(string property)
        {
            IReadOnlyList<Property> properties = GetPropertiesOfBaseTypesAndSelf();
            if (properties.Where(x => x.Name != property && x.IsNodeType).Count() > 0)
                throw new NotSupportedException("Multiple node type not allowed.");

            Properties[property].IsNodeType = true;
            return this;
        }
        public Entity SetRowVersionField(string property, bool hideSetter = true)
        {
            IReadOnlyList<Property> properties = GetPropertiesOfBaseTypesAndSelf();
            if (properties.Where(x => x.Name != property && x.IsRowVersion).Count() > 0)
                throw new NotSupportedException("Multiple row version fields are not allowed.");

            if (Properties[property].SystemReturnType != typeof(DateTime))
                throw new NotSupportedException("You cannot make a non-datetime field the row version.");

            if (Properties[property].Nullable)
                Properties[property].Refactor.MakeMandatory(DateTime.MinValue);

            Properties[property].IsRowVersion = true;
            Properties[property].HideSetter = hideSetter;
            return this;
        }

        private Dictionary<object, DynamicEntity> staticData = new Dictionary<object, DynamicEntity>();
        public ICollection<DynamicEntity> StaticData { get { return staticData.Values; } }

        private string? summary = null;
        public Entity Summary(string summary)
        {
            this.summary = AddDot(summary);

            SetDescription();

            return this;
        }

        private string? example = null;
        public Entity Example(string example)
        {
            this.example = AddDot(example);

            SetDescription();

            return this;
        }

        public Entity HasStaticData(bool containsStaticData = true)
        {
            this.containsStaticData = containsStaticData;
            if (!containsStaticData)
                staticData.Clear();

            return this;
        }

        internal string[] GetDbNames(string alias)
        {
            if (IsVirtual)
                return GetNearestNonVirtualSubclass().Select(item => string.Concat(alias, ":", item.Label.Name)).ToArray();
            else
                return new string[] { string.Concat(alias, ":", Label.Name) };
        }

        private string AddDot(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            // "as is"
            if (text.Length < 5 && !text.Contains(" "))
                return text;

            char last = text[text.Length - 1];
            if ((last >= '0' && last <= '9') ||
                (last >= 'A' && last <= 'Z') ||
                (last >= 'a' && last <= 'z'))
                return string.Concat(text, ".");

            return text;
        }
        private void SetDescription()
        {
            if (string.IsNullOrEmpty(summary) && string.IsNullOrEmpty(example))
                Description = null;
            else if (string.IsNullOrEmpty(summary))
                Description = "Example: " + example;
            else if (string.IsNullOrEmpty(example))
                Description = summary;
            else
                Description = summary + "\r\nExample: " + example;
        }

        private void VerifyFromInheritedProperties(string propertyName, bool excludeThis = false)
        {
            Entity? item = this;
            while (item is not null && item.Name != "Neo4jBase")
            {
                if (excludeThis && item == this)
                {
                    item = item.Inherits;
                    continue;
                }

                if (item.Properties.Contains(propertyName))
                {
                    if (item == this)
                        throw new NotSupportedException(string.Format("Property with the name {0} already exists on Entity {1}", propertyName, item.Name));
                    else
                        throw new NotSupportedException(string.Format("Property with the name {0} already exists on base class Entity {1}", propertyName, item.Name));
                }
                item = item.Inherits;
            }
        }

        private void CheckProperties()
        {
            foreach (Property prop in Properties)
                VerifyFromInheritedProperties(prop.Name, true);
        }

        public Type? RuntimeReturnType { get; private set; }
        public Type? RuntimeClassType { get; private set; }
        void ISetRuntimeType.SetRuntimeTypes(Type returnType, Type classType)
        {
            RuntimeReturnType = returnType;
            RuntimeClassType = classType;

            Parent.TypesRegistered = true;
        }

        #region Refactor Actions

        public IRefactorEntity Refactor { get { return this; } }


        void IRefactorEntity.Rename(string newName)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentNullException(nameof(newName));

            Parent.EnsureSchemaMigration();

            string oldLabelName = Label.Name;
            Parent.Labels.Remove(Label.Name);
            Label.Name = newName;
            Parent.Labels.Add(newName, Label);

            Parent.Entities.Remove(Name);
            Name = newName;
            Parent.Entities.Add(Name, this);

            Parent.Templates.RenameEntity(template =>
            {
                template.OldName = oldLabelName;
                template.NewName = newName;
            }).RunBatched();
        }

        void IRefactorEntity.SetDefaultValue(Action<dynamic> values)
        {
            Parent.EnsureSchemaMigration();

            if (!Parser.ShouldExecute)
                return;

            dynamic tmp = new ExpandoObject();
            values.Invoke(tmp);
            IDictionary<string, object?> fields = (IDictionary<string, object?>)tmp;

            foreach (string key in fields.Keys)
            {
                Property property = GetPropertiesOfBaseTypesAndSelf().FirstOrDefault(item => item.Name == key);
                if (property is null)
                    throw new ArgumentException(string.Format("The field '{0}' was not present on entity '{1}'. ", key, Name));

                Parent.Templates.SetDefaultConstantValue(template =>
                {
                    template.Caller = this;
                    template.Property = property;
                    template.Value = fields[key];
                }).RunBatched();
            }
        }
        void IRefactorEntity.CopyValue(string sourceProperty, string targetProperty)
        {
            Parent.EnsureSchemaMigration();

            if (GetConcreteClasses().Any(item => item.ContainsStaticData))
                throw new NotSupportedException("You cannot copy (potentially dynamic) data to a static data node.");

            Property? source = Search(sourceProperty);
            if (source is null)
                throw new NotSupportedException(string.Format("The source property '{0}' was not found on entity '{1}', you cannot copy data outside of the entity with this refactor action.", sourceProperty, Name));

            Property? target = Search(targetProperty);
            if (target is null)
                throw new NotSupportedException(string.Format("The target property '{0}' was not found on entity '{1}', you cannot copy data outside of the entity with this refactor action.", targetProperty, Name));

            if (!Parser.ShouldExecute)
                return;

            foreach (Entity subClass in GetConcreteClasses())
            {
                Parent.Templates.CopyProperty(template =>
                {
                    template.Caller = subClass;
                    template.From = source.Name;
                    template.To = target.Name;
                }).RunBatched();
            }
        }

        void IRefactorEntity.ApplyLabels()
        {
            Parent.EnsureSchemaMigration();

            if (!Parser.ShouldExecute)
                return;

            foreach (Entity baseClass in this.GetBaseTypes())
            {
                if (baseClass.IsVirtual) continue;

                Parent.Templates.SetLabel(template =>
                {
                    template.Caller = this;
                    template.Label = baseClass.Label.Name;
                }).RunBatched();
            }
        }

        dynamic IRefactorEntity.CreateNode(object node)
        {
            //Parent.EnsureSchemaMigration();

            DynamicEntity entity = new DynamicEntity(this, Parser.ShouldExecute, node);

            object? key = entity.GetKey();
            if (key is null)
                return entity;

            if (!Parent.IsDataMigration)
            {
                if (staticData.ContainsKey(key))
                    throw new PersistenceException(string.Format("A static entity with the same key already exists."));

                staticData.Add(key, entity);
            }

            return entity;
        }
        dynamic? IRefactorEntity.MatchNode(object key)
        {
            //Parent.EnsureSchemaMigration();

            if (key is null)
                throw new ArgumentNullException("key");

            if (!Key.SystemReturnType!.IsAssignableFrom(key.GetType()))
                throw new InvalidCastException(string.Format("The key for entity '{0}' is of type '{1}', but the supplied key is of type '{2}'.", Name, Key.SystemReturnType.Name, key.GetType().Name));

            if (Parent.IsDataMigration)
                return DynamicEntity.Load(this, key);

            DynamicEntity value;
            if (!staticData.TryGetValue(key, out value))
                throw new ArgumentOutOfRangeException($"Only statically created data (via the upgrade script) can be loaded here.");

            if (Parser.ShouldExecute)
                return DynamicEntity.Load(this, key);
            else
                return value;
        }
        void IRefactorEntity.DeleteNode(object key)
        {
            //Parent.EnsureSchemaMigration();

            if (key is null)
                throw new ArgumentNullException("key");

            if (!Key.SystemReturnType!.IsAssignableFrom(key.GetType()))
                throw new InvalidCastException(string.Format("The key for entity '{0}' is of type '{1}', but the supplied key is of type '{2}'.", Name, Key.SystemReturnType.Name, key.GetType().Name));

            if (Parent.IsDataMigration)
                DynamicEntity.Delete(this, key);

            if (!staticData.Remove(key))
                throw new ArgumentOutOfRangeException($"Only statically created data (via the upgrade script) can be deleted here.");

            if (Parser.ShouldExecute)
                DynamicEntity.Delete(this, key);
        }

        void IRefactorEntity.Deprecate()
        {
            Parent.EnsureSchemaMigration();

            foreach (var entity in GetSubclassesOrSelf())
            {
                if (!entity.IsAbstract)
                {
                    Parent.Templates.RemoveEntity(template =>
                    {
                        template.Name = entity.Label.Name;
                    }).RunBatched();
                }

                foreach (var relation in Parent.Relations.Where(item => item.InEntity == entity || item.OutEntity == entity).ToList())
                    relation.Refactor.Deprecate();

                if (entity.FunctionalId is not null && !Parent.Entities.Any(item => item != entity && item.FunctionalId == entity.FunctionalId))
                    Parent.FunctionalIds.Remove(entity.FunctionalId!.Label);

                foreach (Interface iface in Parent.Interfaces)
                    iface.Refactor.RemoveEntity(entity);

                foreach (SubModel model in Parent.SubModels)
                    model.RemoveEntityInternal(entity);

                Parent.Entities.Remove(entity.Name);
                Parent.Labels.Remove(entity.Label.Name);
            }
        }

        void IRefactorEntity.ChangeInheritance(Entity newParentEntity)
        {
            Parent.EnsureSchemaMigration();

            Entity? originalParent = this.Inherits;
            if (originalParent is not null)
            {
                bool hasMandatoryProperty = false;
                Entity? newParent = newParentEntity;
                while (newParent is not null && newParent != originalParent)
                {
                    if (newParent.Properties.Any(prop => !prop.Nullable))
                        hasMandatoryProperty = true;

                    newParent = newParent.Inherits;
                }

                if (newParent is null)
                    throw new NotSupportedException();

                if (hasMandatoryProperty)
                    throw new NotSupportedException();
            }

            Inherits = newParentEntity;
            CheckProperties();
        }

        void IRefactorEntity.ResetFunctionalId()
        {
            Parent.EnsureSchemaMigration();

            if (Inherits?.FunctionalId is not null)
                throw new InvalidOperationException($"The entity '{Name}' does not have a functional id, but inherited it '{Inherits.FunctionalId.Prefix} ({Inherits.FunctionalId.Label})', you cannot assign another one.");

            this.functionalId = null;
            foreach (Entity subclass in this.GetSubclasses())
                subclass.functionalId = null;
        }
        void IRefactorEntity.SetFunctionalId(FunctionalId functionalId, ApplyAlgorithm algorithm)
        {
            Parent.EnsureSchemaMigration();

            if (Inherits?.FunctionalId is not null)
                throw new InvalidOperationException($"The entity '{Name}' already inherited a functional id '{Inherits.FunctionalId.Prefix} ({Inherits.FunctionalId.Label})', you cannot assign another one.");

            this.functionalId = functionalId;
            foreach (Entity subclass in this.GetSubclasses())
                subclass.functionalId = null;

            if (algorithm == ApplyAlgorithm.DoNotApply || !Parser.ShouldExecute)
                return;

            foreach (Entity baseClass in this.GetConcreteClasses())
            {
                Parent.Templates.ApplyFunctionalId(template =>
                {
                    template.Caller = this;
                    template.FunctionalId = functionalId;
                    template.Full = (algorithm == ApplyAlgorithm.ReapplyAll);
                }).RunBatched();
            }
        }

        internal void DynamicEntityPropertyAdded(Property property)
        {
            foreach (Entity entity in GetConcreteClasses())
            {
                foreach (KeyValuePair<object, DynamicEntity> item in entity.staticData)
                    item.Value.RefactorActionPropertyAdded(property);
            }
        }
        internal void DynamicEntityPropertyRenamed(string oldname, Property property, MergeAlgorithm mergeAlgorithm = MergeAlgorithm.NotApplicable)
        {
            foreach (Entity entity in GetConcreteClasses())
            {
                foreach (KeyValuePair<object, DynamicEntity> item in entity.staticData)
                    item.Value.RefactorActionPropertyRenamed(oldname, property, mergeAlgorithm);
            }
        }
        internal void DynamicEntityPropertyConverted(Property property, Type target)
        {
            foreach (Entity entity in GetConcreteClasses())
            {
                foreach (KeyValuePair<object, DynamicEntity> item in entity.staticData)
                    item.Value.RefactorActionPropertyConverted(property, target);
            }
        }
        internal void DynamicEntityPropertyRerouted(string oldname, Entity to, Property property)
        {
            foreach (Entity entity in GetConcreteClasses())
            {
                foreach (KeyValuePair<object, DynamicEntity> item in entity.staticData)
                    item.Value.RefactorActionPropertyRerouted(oldname, to, property);
            }
        }
        internal void DynamicEntityPropertyRemoved(Property property)
        {
            foreach (Entity entity in GetConcreteClasses())
            {
                foreach (KeyValuePair<object, DynamicEntity> item in entity.staticData)
                    item.Value.RefactorActionPropertyRemoved(property);
            }
        }

        #endregion

        #region Event Handlers

        public Core.IEntityEvents Events { get { return this; } }

        internal Type EntityEventArgsType
        {
            get
            {
                if (entityEventArgsType is null)
                {
                    lock (this)
                    {
                        if (entityEventArgsType is null)
                            entityEventArgsType = typeof(EntityEventArgs<>).MakeGenericType(RuntimeReturnType!);
                    }
                }
                return entityEventArgsType;
            }
        }
        private Type? entityEventArgsType = null;

        internal void RaiseOnNew(OGMImpl sender, Transaction trans)
        {
            EntityEventArgs args = EntityEventArgs.CreateInstance(EventTypeEnum.OnNew, sender, trans, true);
            if (!trans.FireEntityEvents)
                return;

            onNew?.Invoke(sender, args);
        }
        bool IEntityEvents.HasRegisteredOnNewHandlers { get { return onNew is not null; } }
        private EventHandler<EntityEventArgs>? onNew;
        event EventHandler<EntityEventArgs> IEntityEvents.OnNew
        {
            add { onNew += value; }
            remove { onNew -= value; }
        }

        internal void RaiseOnSave(OGMImpl sender, Transaction trans)
        {
            EntityEventArgs args = EntityEventArgs.CreateInstance(EventTypeEnum.OnSave, sender, trans);
            if (!trans.FireEntityEvents)
                return;

            onSave?.Invoke(sender, args);
        }
        bool IEntityEvents.HasRegisteredOnSaveHandlers { get { return onSave is not null; } }
        private EventHandler<EntityEventArgs>? onSave;
        event EventHandler<EntityEventArgs> IEntityEvents.OnSave
        {
            add { onSave += value; }
            remove { onSave -= value; }
        }

        internal void RaiseOnAfterSave(OGMImpl sender, Transaction trans)
        {
            EntityEventArgs args = EntityEventArgs.CreateInstance(EventTypeEnum.OnAfterSave, sender, trans);
            if (!trans.FireEntityEvents)
                return;

            onAfterSave?.Invoke(sender, args);
        }
        bool IEntityEvents.HasRegisteredOnAfterSaveHandlers { get { return onAfterSave is not null; } }
        private EventHandler<EntityEventArgs>? onAfterSave;
        event EventHandler<EntityEventArgs> IEntityEvents.OnAfterSave
        {
            add { onAfterSave += value; }
            remove { onAfterSave -= value; }
        }

        internal void RaiseOnDelete(OGMImpl sender, Transaction trans)
        {
            EntityEventArgs args = EntityEventArgs.CreateInstance(EventTypeEnum.OnDelete, sender, trans);
            if (!trans.FireEntityEvents)
                return;

            onDelete?.Invoke(sender, args);
        }
        bool IEntityEvents.HasRegisteredOnDeleteHandlers { get { return onDelete is not null; } }
        private EventHandler<EntityEventArgs>? onDelete;
        event EventHandler<EntityEventArgs> IEntityEvents.OnDelete
        {
            add { onDelete += value; }
            remove { onDelete -= value; }
        }

        internal NodeEventArgs RaiseOnNodeLoading(Transaction trans, OGM? sender, string cypher, Dictionary<string, object?>? parameters, ref Dictionary<string, object?>? customState)
        {
            NodeEventArgs args = new NodeEventArgs(EventTypeEnum.OnNodeLoading, trans, sender, cypher, parameters, customState);
            if (!trans.FireGraphEvents)
                return args;

            onNodeLoading?.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnNodeLoadingHandlers { get { return onNodeLoading is not null; } }
        private EventHandler<NodeEventArgs>? onNodeLoading;
        event EventHandler<NodeEventArgs> IEntityEvents.OnNodeLoading
        {
            add { onNodeLoading += value; }
            remove { onNodeLoading -= value; }
        }

        internal NodeEventArgs RaiseOnNodeLoaded(Transaction trans, NodeEventArgs previousArgs, long id, IReadOnlyList<string> labels, Dictionary<string, object?> properties)
        {
            NodeEventArgs args = new NodeEventArgs(EventTypeEnum.OnNodeLoaded, previousArgs, id, labels, properties);
            if (!trans.FireGraphEvents)
                return args;

            onNodeLoaded?.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnNodeLoadedHandlers { get { return onNodeLoaded is not null; } }
        private EventHandler<NodeEventArgs>? onNodeLoaded;
        event EventHandler<NodeEventArgs> IEntityEvents.OnNodeLoaded
        {
            add { onNodeLoaded += value; }
            remove { onNodeLoaded -= value; }
        }

        internal NodeEventArgs RaiseOnBatchFinished(Transaction trans, NodeEventArgs previousArgs)
        {
            NodeEventArgs args = new NodeEventArgs(EventTypeEnum.OnBatchFinished, previousArgs);
            if (!trans.FireGraphEvents)
                return args;

            onBatchFinished?.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnBatchFinishedHandlers { get { return onBatchFinished is not null; } }
        private EventHandler<NodeEventArgs>? onBatchFinished;
        event EventHandler<NodeEventArgs> IEntityEvents.OnBatchFinished
        {
            add { onBatchFinished += value; }
            remove { onBatchFinished -= value; }
        }

        internal NodeEventArgs RaiseOnNodeCreate(Transaction trans, OGM sender, string cypher, Dictionary<string, object?> parameters, ref Dictionary<string, object?>? customState)
        {
            NodeEventArgs args = new NodeEventArgs(EventTypeEnum.OnNodeCreate, trans, sender, cypher, parameters, customState);
            if (!trans.FireGraphEvents)
                return args;

            onNodeCreate?.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnNodeCreateHandlers { get { return onNodeCreate is not null; } }
        private EventHandler<NodeEventArgs>? onNodeCreate;
        event EventHandler<NodeEventArgs> IEntityEvents.OnNodeCreate
        {
            add { onNodeCreate += value; }
            remove { onNodeCreate -= value; }
        }

        internal NodeEventArgs RaiseOnNodeCreated(Transaction trans, NodeEventArgs previousArgs, long id, IReadOnlyList<string> labels, Dictionary<string, object?> properties)
        {
            NodeEventArgs args = new NodeEventArgs(EventTypeEnum.OnNodeCreated, previousArgs, id, labels, properties);
            if (!trans.FireGraphEvents)
                return args;

            onNodeCreated?.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnNodeCreatedHandlers { get { return onNodeCreated is not null; } }
        private EventHandler<NodeEventArgs>? onNodeCreated;
        event EventHandler<NodeEventArgs> IEntityEvents.OnNodeCreated
        {
            add { onNodeCreated += value; }
            remove { onNodeCreated -= value; }
        }

        internal NodeEventArgs RaiseOnNodeUpdate(Transaction trans, OGM sender, string cypher, Dictionary<string, object?> parameters, ref Dictionary<string, object?>? customState)
        {
            NodeEventArgs args = new NodeEventArgs(EventTypeEnum.OnNodeUpdate, trans, sender, cypher, parameters, customState);
            if (!trans.FireGraphEvents)
                return args;

            onNodeUpdate?.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnNodeUpdateHandlers { get { return onNodeUpdate is not null; } }
        private EventHandler<NodeEventArgs>? onNodeUpdate;
        event EventHandler<NodeEventArgs> IEntityEvents.OnNodeUpdate
        {
            add { onNodeUpdate += value; }
            remove { onNodeUpdate -= value; }
        }

        internal NodeEventArgs RaiseOnNodeUpdated(Transaction trans, NodeEventArgs previousArgs)
        {
            NodeEventArgs args = new NodeEventArgs(EventTypeEnum.OnNodeUpdated, previousArgs);
            if (!trans.FireGraphEvents)
                return args;

            onNodeUpdated?.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnNodeUpdatedHandlers { get { return onNodeUpdated is not null; } }
        private EventHandler<NodeEventArgs>? onNodeUpdated;
        event EventHandler<NodeEventArgs> IEntityEvents.OnNodeUpdated
        {
            add { onNodeUpdated += value; }
            remove { onNodeUpdated -= value; }
        }

        internal NodeEventArgs RaiseOnNodeDelete(Transaction trans, OGM sender, string cypher, Dictionary<string, object?> parameters, ref Dictionary<string, object?>? customState)
        {
            NodeEventArgs args = new NodeEventArgs(EventTypeEnum.OnNodeDelete, trans, sender, cypher, parameters, customState);
            if (!trans.FireGraphEvents)
                return args;

            onNodeDelete?.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnNodeDeleteHandlers { get { return onNodeDelete is not null; } }
        private EventHandler<NodeEventArgs>? onNodeDelete;
        event EventHandler<NodeEventArgs> IEntityEvents.OnNodeDelete
        {
            add { onNodeDelete += value; }
            remove { onNodeDelete -= value; }
        }

        internal NodeEventArgs RaiseOnNodeDeleted(Transaction trans, NodeEventArgs previousArgs)
        {
            NodeEventArgs args = new NodeEventArgs(EventTypeEnum.OnNodeDeleted, previousArgs);
            if (!trans.FireGraphEvents)
                return args;

            onNodeDeleted?.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnNodeDeletedHandlers { get { return onNodeDeleted is not null; } }
        private EventHandler<NodeEventArgs>? onNodeDeleted;
        event EventHandler<NodeEventArgs> IEntityEvents.OnNodeDeleted
        {
            add { onNodeDeleted += value; }
            remove { onNodeDeleted -= value; }
        }

        #endregion

        #region Helper Methods

        public bool IsSubsclassOf(IEntity baseType)
        {
            Entity? baseEntity = baseType as Entity;
            if (baseEntity is null)
                return false;

            bool found = false;

            WalkBaseTypes(Inherits, delegate (Entity item)
            {
                if (item == baseEntity)
                {
                    found = true;
                    return true; // cancel walking
                }
                return false;
            });

            return found;
        }
        public bool IsSelfOrSubclassOf(IEntity baseType)
        {
            Entity? baseEntity = baseType as Entity;
            if (baseEntity is null)
                return (baseType == this);

            bool found = false;

            WalkBaseTypes(this, delegate (Entity item)
            {
                if (item.Name == baseEntity.Name)
                {
                    found = true;
                    return true; // cancel walking
                }
                return false;
            });

            return found;
        }

        private static void WalkBaseTypes(Entity? item, Action<Entity> action)
        {
            while (item is not null)
            {
                action.Invoke(item);
                item = item.Inherits;
            }
        }
        private static void WalkBaseTypes(Entity? item, Func<Entity, bool> action)
        {
            while (item is not null)
            {
                if (action.Invoke(item))
                    return;

                item = item.Inherits;
            }
        }



        private List<Entity>? baseTypes = null;
        public IReadOnlyList<Entity> GetBaseTypes()
        {
            return InitSubList(ref baseTypes, delegate (List<Entity> result)
            {
                WalkBaseTypes(Inherits, delegate (Entity item)
                {
                    if (!result.Contains(item))
                        result.Add(item);
                });
            });
        }

        private List<Entity>? baseTypesAndSelf = null;
        public IReadOnlyList<Entity> GetBaseTypesAndSelf()
        {
            return InitSubList(ref baseTypesAndSelf, delegate (List<Entity> result)
            {
                WalkBaseTypes(this, delegate (Entity item)
                {
                    if (!result.Contains(item))
                        result.Add(item);
                });
            });
        }

        private List<Entity>? subclassesOrSelf = null;
        public IReadOnlyList<Entity> GetSubclassesOrSelf()
        {
            return InitSubList(ref subclassesOrSelf, delegate (List<Entity> classes)
            {
                foreach (Entity item in Parent.Entities)
                {
                    if (item.IsSelfOrSubclassOf(this))
                        if (!classes.Contains(item))
                            classes.Add(item);
                }
            });
        }

        private List<Entity>? subclasses = null;
        public IReadOnlyList<Entity> GetSubclasses()
        {
            return InitSubList(ref subclasses, delegate (List<Entity> classes)
            {
                foreach (Entity item in Parent.Entities)
                {
                    if (item.IsSubsclassOf(this))
                        if (!classes.Contains(item))
                            classes.Add(item);
                }
            });
        }

        private List<Entity>? directSubclasses = null;
        public IReadOnlyList<Entity> GetDirectSubclasses()
        {
            return InitSubList(ref directSubclasses, delegate (List<Entity> classes)
            {
                foreach (Entity item in Parent.Entities)
                {
                    if (item.Inherits == this)
                    {
                        if (!classes.Contains(item))
                        {
                            classes.Add(item);
                        }
                    }
                }
            });
        }

        private List<Entity>? nearestNonVirtualSubclasses = null;
        public IReadOnlyList<Entity> GetNearestNonVirtualSubclass()
        {
            return InitSubList(ref nearestNonVirtualSubclasses, delegate (List<Entity> classes)
            {
                foreach (Entity item in GetDirectSubclasses())
                {
                    if (!classes.Contains(item))
                    {
                        if (item.IsVirtual)
                            classes.AddRange(item.GetNearestNonVirtualSubclass());
                        else
                            classes.Add(item);
                    }
                }
            });
        }

        private List<EntityProperty>? propertiesOfBaseTypesAndSelf = null;
        public IReadOnlyList<EntityProperty> GetPropertiesOfBaseTypesAndSelf()
        {
            return InitSubList(ref propertiesOfBaseTypesAndSelf, delegate (List<EntityProperty> allProperties)
            {
                foreach (Entity entity in this.GetBaseTypesAndSelf())
                {
                    allProperties.AddRange(entity.Properties);
                }
            });
        }

        private List<Entity>? concreteClasses = null;
        public IReadOnlyList<Entity> GetConcreteClasses()
        {
            return InitSubList(ref concreteClasses, delegate (List<Entity> classes)
            {
                foreach (Entity item in Parent.Entities)
                {
                    if (item.IsAbstract)
                        continue;

                    if (item.IsSelfOrSubclassOf(this))
                        if (!classes.Contains(item))
                            classes.Add(item);
                }
            });
        }

        private List<T> InitSubList<T>(ref List<T>? listReference, Action<List<T>> initialize)
        {
            if (listReference is null)
            {
                lock (this)
                {
                    if (listReference is null)
                    {
                        List<T> list = new List<T>();

                        initialize.Invoke(list);

                        if (Parent.IsUpgraded)
                            listReference = list;
                        else
                            return list;
                    }
                }
            }

            return listReference;
        }

        private IReadOnlyDictionary<string, EntityProperty>? namedPropertiesOfBaseTypesAndSelf = null;
        public EntityProperty? Search(string name)
        {
            if (Parent.IsUpgraded)
            {
                if (namedPropertiesOfBaseTypesAndSelf is null)
                    namedPropertiesOfBaseTypesAndSelf = GetPropertiesOfBaseTypesAndSelf().Cast<EntityProperty>().ToDictionary(key => key.Name.ToLower(), value => value);

                EntityProperty? foundProperty;
                namedPropertiesOfBaseTypesAndSelf.TryGetValue(name.ToLower(), out foundProperty);
                return foundProperty;
            }
            else
            {
                return GetPropertiesOfBaseTypesAndSelf().Cast<EntityProperty>().FirstOrDefault(item => item.Name.ToLower() == name.ToLower());
            }
        }


        internal static Entity? FindCommonBaseClass(List<Entity> entities)
        {
            if (entities.Count == 0)
                return null;

            if (entities.Count == 1)
                return entities.First();

            Entity? e = null;
            List<Entity>? shared = null;
            foreach (Entity entity in entities)
            {
                List<Entity> inheritChain = new List<Entity>();
                e = entity;
                while (e is not null)
                {
                    inheritChain.Add(e);
                    e = e.Inherits;
                }

                if (shared is null)
                {
                    shared = inheritChain;
                }
                else
                {
                    HashSet<string> inheritedEntities = new HashSet<string>(inheritChain.Select(item => item.Name));

                    for (int index = shared.Count - 1; index >= 0; index--)
                    {
                        if (!inheritedEntities.Contains(shared[index].Name))
                            shared.RemoveAt(index);
                    }
                }

                if (shared.Count == 0)
                    break;
            }

            if (shared is null || shared.Count == 0)
                return null;

            return shared.First();
        }
        internal static AliasResult? FindCommonBaseClass(List<AliasResult> aliases)
        {
            if (aliases.Count == 0)
                return null;

            if (aliases.Count == 1)
                return aliases.First();

            AliasResultInfo? e;
            List<AliasResultInfo>? shared = null;
            foreach (AliasResultInfo info in aliases.Select(item => new AliasResultInfo(item)))
            {
                List<AliasResultInfo> inheritChain = new List<AliasResultInfo>();
                e = info;
                while (e is not null)
                {
                    inheritChain.Add(e);
                    e = e.Inherits;
                }

                if (shared is null)
                {
                    shared = inheritChain;
                }
                else
                {
                    if (shared.Last().Distance > inheritChain.Last().Distance)
                        (shared, inheritChain) = (inheritChain, shared);

                    HashSet<string> inheritedEntities = new HashSet<string>(inheritChain.Select(item => item.Entity.Name));
                    for (int index = shared.Count - 1; index >= 0; index--)
                    {
                        if (!inheritedEntities.Contains(shared[index].Entity.Name))
                        {
                            shared.RemoveAt(index);
                        }
                    }
                }

                if (shared.Count == 0)
                    break;
            }

            if (shared is null || shared.Count == 0)
                return null;

            return shared.First().AliasResult;
        }
        private class AliasResultInfo
        {
            public AliasResultInfo(AliasResult aliasResult)
            {
                AliasResult = aliasResult;
                Entity = aliasResult.Entity!;
                Distance = 0;
                Inherits = (aliasResult.Entity?.Inherits is null) ? null : new AliasResultInfo(this);
            }
            private AliasResultInfo(AliasResultInfo info)
            {
                AliasResult = info.AliasResult;
                Entity = info.Entity.Inherits!;
                Distance = info.Distance + 1;
            }

            public AliasResult AliasResult { get; set; }
            public Entity Entity { get; set; }
            public int Distance { get; set; }

            public AliasResultInfo? Inherits { get; set; }
        }

        internal OGM Activator(EventOptions eventOptions = EventOptions.GraphEvents)
        {
            if (IsAbstract)
                throw new NotSupportedException($"You cannot instantiate the abstract entity {Name}.");

            if (activator is null)
            {
                lock(this)
                {
                    if (activator is null)
                        activator = Expression.Lambda<Func<OGM>>(Expression.New(RuntimeReturnType)).Compile();
                }
            }

            return Transaction.Execute(() => activator.Invoke(), eventOptions);
        }
        private Func<OGM>? activator = null;

        internal OGM? Load(object? key, bool locked = false)
        {
            if (key is null)
                return null;

            OGM? item = Lookup(key);
            if (item is null)
                return null;

            if (locked || item.PersistenceState != PersistenceState.DoesntExist)
                Transaction.RunningTransaction.NodePersistenceProvider.Load(item, locked);

            if (item.PersistenceState != PersistenceState.New && item.PersistenceState != PersistenceState.DoesntExist)
                return item;
            else
                return null;
        }
        internal OGM? Lookup(object? key)
        {
            if (key is null)
                return null;

            OGM? instance = (OGM?)Transaction.RunningTransaction.GetEntityByKey(Name, key);
            if (instance is not null)
                return instance;

            OGM item = Activator();
            item.SetKey(key);

            return item;
        }
        internal void ForceDelete(object key)
        {
            OGM? item = Load(key);
            if (item is null || (Parser.ShouldExecute && ContainsStaticData))
                return;

            item.Delete(true);
        }
        internal void Delete(object key)
        {
            OGM? item = Load(key);
            if (item is null || (Parser.ShouldExecute && ContainsStaticData))
                return;

            item.Delete(false);
        }

        internal OGM? Map(RawNode node, NodeMapping mappingMode)
        {
            return Map(node, null!, null!, mappingMode);
        }

        internal OGM? Map(RawNode node, string cypher, Dictionary<string, object?>? parameters, NodeMapping mappingMode)
        {
            if(mapMethod is null)
            {
                lock (this)
                {
                    if (mapMethod is null)
                    {
                        MethodInfo? method = RuntimeClassType!.GetMethod("Map", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy, null, new Type[] { typeof(RawNode), typeof(string), typeof(Dictionary<string, object>), typeof(NodeMapping) }, null);
                        mapMethod = (method is null) ? null : (Func<RawNode, string, Dictionary<string, object?>?, NodeMapping, OGM?>)Delegate.CreateDelegate(typeof(Func<RawNode, string, Dictionary<string, object?>?, NodeMapping, OGM?>), method, true);
                    }
                }
            }

            return mapMethod?.Invoke(node, cypher, parameters, mappingMode);
        }
        private Func<RawNode, string, Dictionary<string, object?>?, NodeMapping, OGM?>? mapMethod = null;

        #region IEntityAdvancedFeatures


        OGM? IEntityAdvancedFeatures.Load(object? key, bool locked) => Load(key, locked);
        OGM? IEntityAdvancedFeatures.Lookup(object? key) => Lookup(key);
        void IEntityAdvancedFeatures.ForceDelete(object key) => ForceDelete(key);
        void IEntityAdvancedFeatures.Delete(object key) => Delete(key);

        OGM IEntityAdvancedFeatures.Activator(EventOptions eventOptions) => Activator(eventOptions);
        OGM? IEntityAdvancedFeatures.Map(RawNode node, NodeMapping mappingMode) => Map(node, mappingMode);
        OGM? IEntityAdvancedFeatures.Map(RawNode node, string cypher, Dictionary<string, object?>? parameters, NodeMapping mappingMode) => Map(node, cypher, parameters, mappingMode);
        #endregion

        #endregion
    }
}
