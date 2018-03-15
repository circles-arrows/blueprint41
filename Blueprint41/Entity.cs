using Blueprint41.Dynamic;
using Blueprint41.Neo4j.Refactoring;
using Blueprint41.Neo4j.Refactoring.Templates;
using Neo4j.Driver.V1;
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

namespace Blueprint41
{
    [DebuggerDisplay("Entity: {Name}")]
    public partial class Entity : IRefactorEntity, IEntityEvents, ISetRuntimeType
    {
        internal Entity(DatastoreModel parent, string name, string label, string prefix, Entity inherits)
            : this(parent, name, label, GetFunctionalId(parent, name, label, prefix), inherits)
        {
        }
        internal Entity(DatastoreModel parent, string name, string label, FunctionalId functionalId, Entity inherits)
        {
            Properties = new PropertyCollection(this);

            Parent = parent;
            Name = name;
            IsAbstract = false;
            IsVirtual = false;
            Guid = parent.GenerateGuid(name);

            Inherits = inherits;

            Parent.Labels.New(label);
            Label = Parent.Labels[label];

            if (inherits?.FunctionalId != null && functionalId != null)
                throw new InvalidOperationException($"The entity '{name}' already inherited a functional id '{(inherits?.FunctionalId ?? functionalId).Prefix} ({(inherits?.FunctionalId ?? functionalId).Label})', you cannot assign another one.");

            this.functionalId = functionalId;

            FullTextIndexProperties = new List<Blueprint41.Property>();

            key = new Lazy<Property>(delegate () { return GetPropertiesOfBaseTypesAndSelf().SingleOrDefault(x => x.IsKey); }, true);
            nodeType = new Lazy<Property>(delegate () { return GetPropertiesOfBaseTypesAndSelf().SingleOrDefault(x => x.IsNodeType); }, true);
        }

        static private FunctionalId GetFunctionalId(DatastoreModel parent, string name, string label, string prefix)
        {
            FunctionalId functionalId = null;
            if (prefix != null)
            {
                if (string.IsNullOrWhiteSpace(prefix))
                    throw new NotSupportedException($"The prefix cannot be empty or blank for entity '{name}'.");

                functionalId = parent.FunctionalIds.FirstOrDefault(item => item.Label == label);
                if (functionalId == null)
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
        public string Description { get; private set; }
        public Guid Guid { get; private set; }

        private FunctionalId functionalId = null;
        public FunctionalId FunctionalId
        {
            get
            {
                if (functionalId == null && !IsAbstract)
                    functionalId = Inherits?.FunctionalId ?? Parent.FunctionalIds.Default;

                return functionalId;
            }
        }

        public bool IsAbstract { get; private set; }
        public bool IsVirtual { get; private set; }

        private bool containsStaticData = false;
        public bool ContainsStaticData { get { return (containsStaticData || staticData.Count != 0); } }
        public Entity Inherits { get; private set; }

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
            Property property = Search(propertyName);
            if (property == null)
                throw new NotSupportedException("Property does not exist.");

            FullTextIndexProperties.Add(property);
            return this;
        }

        public Entity AddProperty(string name, Type type)
        {
            return AddProperty(name, type, true, IndexType.None);
        }
        public Entity AddProperty(string name, Type type, IndexType indexType)
        {
            return AddProperty(name, type, true, indexType);
        }
        public Entity AddProperty(string name, Type type, bool nullable, IndexType indexType = IndexType.None)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                throw new ArgumentException(string.Format("The type argument does not support the 'Nullable<{0}>' type. All types are considered nullable by default, but you can also set the 'nullable' argument explicitly.", type.GenericTypeArguments[0].Name));

            Property value = new Property(this, PropertyType.Attribute, name, type, nullable, indexType);
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
        internal Entity AddLookup(string name, Entity type, bool nullable = true)
        {
            Property value = new Property(this, PropertyType.Lookup, name, type, nullable, IndexType.None);
            Properties.Add(name, value);

            return this;
        }
        internal Entity AddCollection(string name, Entity type)
        {
            Property value = new Property(this, PropertyType.Collection, name, type, true, IndexType.None);
            Properties.Add(name, value);

            return this;
        }

        public List<Property> FullTextIndexProperties { get; private set; }
        public PropertyCollection Properties { get; private set; }
        public string UnidentifiedProperties { get; private set; }


        private Lazy<Property> key;
        public Property Key
        {
            get
            {
                if (Parent.IsUpgraded)
                    return key.Value;

                return GetPropertiesOfBaseTypesAndSelf().SingleOrDefault(x => x.IsKey);
            }
        }
        private Lazy<Property> nodeType;
        public Property NodeType
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
                return ((object)NodeType == null) ? "NodeType" : NodeType.Name;
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
            List<Property> properties = GetPropertiesOfBaseTypesAndSelf();
            if (properties.Where(x => x.Name != property && x.IsKey).Count() > 0)
                throw new NotSupportedException("Multiple key not allowed.");

            if (Properties[property].IndexType != IndexType.Unique)
                throw new NotSupportedException("You cannot make a non-unique field the key.");

            Properties[property].IsKey = true;
            return this;
        }
        public Entity SetNodeType(string property)
        {
            List<Property> properties = GetPropertiesOfBaseTypesAndSelf();
            if (properties.Where(x => x.Name != property && x.IsNodeType).Count() > 0)
                throw new NotSupportedException("Multiple node type not allowed.");

            Properties[property].IsNodeType = true;
            return this;
        }

        private FastDictionary<object, DynamicEntity> staticData = new FastDictionary<object, DynamicEntity>();
        public ICollection<DynamicEntity> StaticData { get { return staticData.Values; } }

        private string summary = null;
        public Entity Summary(string summary)
        {
            this.summary = AddDot(summary);

            SetDescription();

            return this;
        }

        private string example = null;
        public Entity Example(string example)
        {
            this.example = AddDot(example);

            SetDescription();

            return this;
        }

        public Entity HasStaticData(bool containsStaticData = true)
        {
            this.containsStaticData = containsStaticData;
            return this;
        }

        internal string GetDbName(string alias)
        {
            return IsVirtual ? alias : string.Concat(alias, ":", Label.Name);
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

        public Type RuntimeReturnType { get; private set; }
        public Type RuntimeClassType { get; private set; }
        void ISetRuntimeType.SetRuntimeTypes(Type returnType, Type classType)
        {
            RuntimeReturnType = returnType;
            RuntimeClassType = classType;
        }

        #region Refactor Actions

        public IRefactorEntity Refactor { get { return this; } }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="newName"></param>
        void IRefactorEntity.Rename(string newName)
        {
            string oldLabelName = Label.Name;
            Parent.Labels.Remove(Label.Name);
            Label.Name = newName;
            Parent.Labels.Add(newName, Label);

            Parent.Entities.Remove(Name);
            Name = newName;
            Parent.Entities.Add(Name, this);

            Parser.ExecuteBatched<RenameEntity>(delegate (RenameEntity template)
            {
                template.OldName = oldLabelName;
                template.NewName = newName;
            });

            if (this.IsAbstract == false)
                Refactor.ApplyConstraints();
        }

        void IRefactorEntity.ApplyConstraints()
        {
            foreach (var property in GetPropertiesOfBaseTypesAndSelf())
            {
                if (property.IndexType == IndexType.Unique && !IsAbstract)
                {
                    Parser.Execute<CreateUniqueConstraint>(delegate (CreateUniqueConstraint template)
                    {
                        template.Entity = this;
                        template.Property = property;
                    }, false);
                }
                else if (property.IndexType == IndexType.Indexed || (property.IndexType == IndexType.Unique && IsAbstract))
                {
                    Parser.Execute<CreateIndex>(delegate (CreateIndex template)
                    {
                        template.Entity = this;
                        template.Property = property;
                    }, false);
                }
                else if (property.IndexType == IndexType.None && property.SystemReturnType != null)
                {
                    // Check if constraint exists
                    // if so, remove constraint
                }
            }
        }

        void IRefactorEntity.SetDefaultValue(Action<dynamic> values)
        {
            if (!Parser.ShouldExecute)
                return;

            dynamic tmp = new ExpandoObject();
            values.Invoke(tmp);
            IDictionary<string, object> fields = tmp as IDictionary<string, object>;

            foreach (string key in fields.Keys)
            {
                Property property = GetPropertiesOfBaseTypesAndSelf().FirstOrDefault(item => item.Name == key);
                if (property == null)
                    throw new ArgumentException(string.Format("The field '{0}' was not present on entity '{1}'. ", key, Name));

                Parser.ExecuteBatched<SetDefaultConstantValue>(delegate (SetDefaultConstantValue template)
                {
                    template.Entity = this;
                    template.Property = property;
                    template.Value = fields[key];
                });
            }
        }
        void IRefactorEntity.CopyValue(string sourceProperty, string targetProperty)
        {
            if (GetConcreteClasses().Any(item => item.ContainsStaticData))
                throw new NotSupportedException("You cannot copy (potentially dynamic) data to a static data node.");

            Property source = Search(sourceProperty);
            if (source == null)
                throw new NotSupportedException(string.Format("The source property '{0}' was not found on entity '{1}', you cannot copy data outside of the entity with this refactor action.", sourceProperty, Name));

            Property target = Search(targetProperty);
            if (target == null)
                throw new NotSupportedException(string.Format("The target property '{0}' was not found on entity '{1}', you cannot copy data outside of the entity with this refactor action.", targetProperty, Name));

            if (!Parser.ShouldExecute)
                return;

            foreach (Entity subClass in GetConcreteClasses())
            {
                Parser.ExecuteBatched<CopyProperty>(delegate (CopyProperty template)
                {
                    template.Entity = subClass;
                    template.From = source.Name;
                    template.To = target.Name;
                });
            }
        }

        void IRefactorEntity.ApplyLabels()
        {
            if (!Parser.ShouldExecute)
                return;

            foreach (Entity baseClass in this.GetBaseTypes())
            {
                if (baseClass.IsVirtual) continue;

                Parser.ExecuteBatched<SetLabel>(delegate (SetLabel template)
                {
                    template.Entity = this;
                    template.Label = baseClass.Label.Name;
                });
            }
        }

        void IRefactorEntity.CreateIndexes()
        {
            foreach (var property in GetPropertiesOfBaseTypesAndSelf())
            {
                if (property.IndexType == IndexType.Indexed || (property.IndexType == IndexType.Unique && property.Parent.IsAbstract))
                {
                    Parser.Execute<CreateIndex>(delegate (CreateIndex template)
                    {
                        template.Entity = this;
                        template.Property = property;
                    }, false);
                }
            }
        }

        dynamic IRefactorEntity.CreateNode(object node)
        {
            DynamicEntity entity = new DynamicEntity(this, Parser.ShouldExecute, node);

            object key = entity.GetKey();

            if (staticData.ContainsKey(key))
                throw new PersistenceException(string.Format("An static entity with the same key already exists."));

            staticData.Add(key, entity);

            return entity;
        }
        dynamic IRefactorEntity.MatchNode(object key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (!Key.SystemReturnType.IsAssignableFrom(key.GetType()))
                throw new InvalidCastException(string.Format("The key for entity '{0}' is of type '{1}', but the supplied key is of type '{2}'.", Name, Key.SystemReturnType.Name, key.GetType().Name));

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
            if (key == null)
                throw new ArgumentNullException("key");

            if (!Key.SystemReturnType.IsAssignableFrom(key.GetType()))
                throw new InvalidCastException(string.Format("The key for entity '{0}' is of type '{1}', but the supplied key is of type '{2}'.", Name, Key.SystemReturnType.Name, key.GetType().Name));

            if (!staticData.Remove(key))
                throw new ArgumentOutOfRangeException($"Only statically created data (via the upgrade script) can be deleted here.");

            if (Parser.ShouldExecute)
                DynamicEntity.Delete(this, key);
        }

        void IRefactorEntity.Deprecate()
        {
            foreach (var entity in GetSubclassesOrSelf())
            {
                if (!entity.IsAbstract)
                {
                    Parser.ExecuteBatched(delegate (RemoveEntity template)
                    {
                        template.Name = entity.Label.Name;
                    });
                }

                foreach (var relation in Parent.Relations.Where(item => item.InEntity == entity || item.OutEntity == entity).ToList())
                    relation.Refactor.Deprecate();

                if (!Parent.Entities.Any(item => item != entity && item.FunctionalId == entity.FunctionalId))
                    Parent.FunctionalIds.Remove(entity.FunctionalId.Label);

                Parent.Entities.Remove(entity.Name);
                Parent.Labels.Remove(entity.Label.Name);
            }
        }

        void IRefactorEntity.ResetFunctionalId()
        {
            if (Inherits?.FunctionalId != null)
                throw new InvalidOperationException($"The entity '{Name}' does not have a functional id, but inherited it '{Inherits.FunctionalId.Prefix} ({Inherits.FunctionalId.Label})', you cannot assign another one.");

            this.functionalId = null;
            foreach (Entity subclass in this.GetSubclasses())
                subclass.functionalId = null;
        }
        void IRefactorEntity.SetFunctionalId(FunctionalId functionalId, ApplyAlgorithm algorithm)
        {
            if (Inherits?.FunctionalId != null)
                throw new InvalidOperationException($"The entity '{Name}' already inherited a functional id '{Inherits.FunctionalId.Prefix} ({Inherits.FunctionalId.Label})', you cannot assign another one.");

            this.functionalId = functionalId;
            foreach (Entity subclass in this.GetSubclasses())
                subclass.functionalId = null;

            if (algorithm == ApplyAlgorithm.DoNotApply || !Parser.ShouldExecute)
                return;

            foreach (Entity baseClass in this.GetConcreteClasses())
            {
                Parser.ExecuteBatched(delegate (ApplyFunctionalId template)
                {
                    template.Entity = this;
                    template.FunctionalId = functionalId;
                    template.Full = (algorithm == ApplyAlgorithm.ReapplyAll);
                });
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
                if (entityEventArgsType == null)
                {
                    lock (this)
                    {
                        if (entityEventArgsType == null)
                            entityEventArgsType = typeof(EntityEventArgs<>).MakeGenericType(RuntimeReturnType);
                    }
                }
                return entityEventArgsType;
            }
        }
        private Type entityEventArgsType;

        internal void RaiseOnNew(OGMImpl sender, Transaction trans)
        {
            EntityEventArgs args = EntityEventArgs.CreateInstance(EventTypeEnum.OnNew, sender, trans, true);
            if (!trans.FireEntityEvents)
                return;

            EventHandler<EntityEventArgs> handler = onNew;
            if ((object)handler != null)
                handler.Invoke(sender, args);
        }
        bool IEntityEvents.HasRegisteredOnNewHandlers { get { return onNew != null; } }
        private EventHandler<EntityEventArgs> onNew;
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

            EventHandler<EntityEventArgs> handler = onSave;
            if ((object)handler != null)
                handler.Invoke(sender, args);
        }
        bool IEntityEvents.HasRegisteredOnSaveHandlers { get { return onSave != null; } }
        private EventHandler<EntityEventArgs> onSave;
        event EventHandler<EntityEventArgs> IEntityEvents.OnSave
        {
            add { onSave += value; }
            remove { onSave -= value; }
        }

        internal void RaiseOnDelete(OGMImpl sender, Transaction trans)
        {
            EntityEventArgs args = EntityEventArgs.CreateInstance(EventTypeEnum.OnDelete, sender, trans);
            if (!trans.FireEntityEvents)
                return;

            EventHandler<EntityEventArgs> handler = onDelete;
            if ((object)handler != null)
                handler.Invoke(sender, args);
        }
        bool IEntityEvents.HasRegisteredOnDeleteHandlers { get { return onDelete != null; } }
        private EventHandler<EntityEventArgs> onDelete;
        event EventHandler<EntityEventArgs> IEntityEvents.OnDelete
        {
            add { onDelete += value; }
            remove { onDelete -= value; }
        }

        internal NodeEventArgs RaiseOnNodeLoading(Transaction trans, OGM sender, string cypher, Dictionary<string, object> parameters, ref Dictionary<string, object> customState)
        {
            NodeEventArgs args = new NodeEventArgs(EventTypeEnum.OnNodeLoading, trans, sender, cypher, parameters, customState);
            if (!trans.FireGraphEvents)
                return args;

            EventHandler<NodeEventArgs> handler = onNodeLoading;
            if ((object)handler != null)
                handler.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnNodeLoadingHandlers { get { return onNodeLoading != null; } }
        private EventHandler<NodeEventArgs> onNodeLoading;
        event EventHandler<NodeEventArgs> IEntityEvents.OnNodeLoading
        {
            add { onNodeLoading += value; }
            remove { onNodeLoading -= value; }
        }

        internal NodeEventArgs RaiseOnNodeLoaded(Transaction trans, NodeEventArgs previousArgs, long id, IReadOnlyList<string> labels, Dictionary<string, object> properties)
        {
            NodeEventArgs args = new NodeEventArgs(EventTypeEnum.OnNodeLoaded, previousArgs, id, labels, properties);
            if (!trans.FireGraphEvents)
                return args;

            EventHandler<NodeEventArgs> handler = onNodeLoaded;
            if ((object)handler != null)
                handler.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnNodeLoadedHandlers { get { return onNodeLoaded != null; } }
        private EventHandler<NodeEventArgs> onNodeLoaded;
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

            EventHandler<NodeEventArgs> handler = onBatchFinished;
            if ((object)handler != null)
                handler.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnBatchFinishedHandlers { get { return onBatchFinished != null; } }
        private EventHandler<NodeEventArgs> onBatchFinished;
        event EventHandler<NodeEventArgs> IEntityEvents.OnBatchFinished
        {
            add { onBatchFinished += value; }
            remove { onBatchFinished -= value; }
        }

        internal NodeEventArgs RaiseOnNodeCreate(Transaction trans, OGM sender, string cypher, Dictionary<string, object> parameters, ref Dictionary<string, object> customState)
        {
            NodeEventArgs args = new NodeEventArgs(EventTypeEnum.OnNodeCreate, trans, sender, cypher, parameters, customState);
            if (!trans.FireGraphEvents)
                return args;

            EventHandler<NodeEventArgs> handler = onNodeCreate;
            if ((object)handler != null)
                handler.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnNodeCreateHandlers { get { return onNodeCreate != null; } }
        private EventHandler<NodeEventArgs> onNodeCreate;
        event EventHandler<NodeEventArgs> IEntityEvents.OnNodeCreate
        {
            add { onNodeCreate += value; }
            remove { onNodeCreate -= value; }
        }

        internal NodeEventArgs RaiseOnNodeCreated(Transaction trans, NodeEventArgs previousArgs, long id, IReadOnlyList<string> labels, Dictionary<string, object> properties)
        {
            NodeEventArgs args = new NodeEventArgs(EventTypeEnum.OnNodeCreated, previousArgs, id, labels, properties);
            if (!trans.FireGraphEvents)
                return args;

            EventHandler<NodeEventArgs> handler = onNodeCreated;
            if ((object)handler != null)
                handler.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnNodeCreatedHandlers { get { return onNodeCreated != null; } }
        private EventHandler<NodeEventArgs> onNodeCreated;
        event EventHandler<NodeEventArgs> IEntityEvents.OnNodeCreated
        {
            add { onNodeCreated += value; }
            remove { onNodeCreated -= value; }
        }

        internal NodeEventArgs RaiseOnNodeUpdate(Transaction trans, OGM sender, string cypher, Dictionary<string, object> parameters, ref Dictionary<string, object> customState)
        {
            NodeEventArgs args = new NodeEventArgs(EventTypeEnum.OnNodeUpdate, trans, sender, cypher, parameters, customState);
            if (!trans.FireGraphEvents)
                return args;

            EventHandler<NodeEventArgs> handler = onNodeUpdate;
            if ((object)handler != null)
                handler.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnNodeUpdateHandlers { get { return onNodeUpdate != null; } }
        private EventHandler<NodeEventArgs> onNodeUpdate;
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

            EventHandler<NodeEventArgs> handler = onNodeUpdated;
            if ((object)handler != null)
                handler.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnNodeUpdatedHandlers { get { return onNodeUpdated != null; } }
        private EventHandler<NodeEventArgs> onNodeUpdated;
        event EventHandler<NodeEventArgs> IEntityEvents.OnNodeUpdated
        {
            add { onNodeUpdated += value; }
            remove { onNodeUpdated -= value; }
        }

        internal NodeEventArgs RaiseOnNodeDelete(Transaction trans, OGM sender, string cypher, Dictionary<string, object> parameters, ref Dictionary<string, object> customState)
        {
            NodeEventArgs args = new NodeEventArgs(EventTypeEnum.OnNodeDelete, trans, sender, cypher, parameters, customState);
            if (!trans.FireGraphEvents)
                return args;

            EventHandler<NodeEventArgs> handler = onNodeDelete;
            if ((object)handler != null)
                handler.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnNodeDeleteHandlers { get { return onNodeDelete != null; } }
        private EventHandler<NodeEventArgs> onNodeDelete;
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

            EventHandler<NodeEventArgs> handler = onNodeDeleted;
            if ((object)handler != null)
                handler.Invoke(this, args);

            return args;
        }
        bool IEntityEvents.HasRegisteredOnNodeDeletedHandlers { get { return onNodeDeleted != null; } }
        private EventHandler<NodeEventArgs> onNodeDeleted;
        event EventHandler<NodeEventArgs> IEntityEvents.OnNodeDeleted
        {
            add { onNodeDeleted += value; }
            remove { onNodeDeleted -= value; }
        }

        #endregion

        #region Helper Methods

        public bool IsSubsclassOf(Entity baseType)
        {
            bool found = false;

            WalkBaseTypes(Inherits, delegate (Entity item)
            {
                if (item == baseType)
                {
                    found = true;
                    return true; // cancel walking
                }
                return false;
            });

            return found;
        }
        public bool IsSelfOrSubclassOf(Entity baseType)
        {
            bool found = false;

            WalkBaseTypes(this, delegate (Entity item)
            {
                if (item.Name == baseType.Name)
                {
                    found = true;
                    return true; // cancel walking
                }
                return false;
            });

            return found;
        }

        private static void WalkBaseTypes(Entity item, Action<Entity> action)
        {
            while (item != null)
            {
                action.Invoke(item);
                item = item.Inherits;
            }
        }
        private static void WalkBaseTypes(Entity item, Func<Entity, bool> action)
        {
            while (item != null)
            {
                if (action.Invoke(item))
                    return;

                item = item.Inherits;
            }
        }



        private List<Entity> baseTypes = null;
        public List<Entity> GetBaseTypes()
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

        private List<Entity> baseTypesAndSelf = null;
        public List<Entity> GetBaseTypesAndSelf()
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

        private List<Entity> subclassesOrSelf = null;
        public List<Entity> GetSubclassesOrSelf()
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

        private List<Entity> subclasses = null;
        public List<Entity> GetSubclasses()
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

        private List<Property> propertiesOfBaseTypesAndSelf = null;
        public List<Property> GetPropertiesOfBaseTypesAndSelf()
        {
            return InitSubList(ref propertiesOfBaseTypesAndSelf, delegate (List<Property> allProperties)
            {
                foreach (Entity entity in this.GetBaseTypesAndSelf())
                {
                    allProperties.AddRange(entity.Properties);
                }
            });
        }

        private List<Entity> concreteClasses = null;
        public List<Entity> GetConcreteClasses()
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

        private List<T> InitSubList<T>(ref List<T> listReference, Action<List<T>> initialize)
        {
            if (listReference == null)
            {
                lock (this)
                {
                    if (listReference == null)
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

        private IReadOnlyDictionary<string, Property> namedPropertiesOfBaseTypesAndSelf = null;
        public Property Search(string name)
        {
            if (Parent.IsUpgraded)
            {
                if (namedPropertiesOfBaseTypesAndSelf == null)
                    namedPropertiesOfBaseTypesAndSelf = GetPropertiesOfBaseTypesAndSelf().ToDictionary(key => key.Name, value => value);

                Property foundProperty;
                namedPropertiesOfBaseTypesAndSelf.TryGetValue(name, out foundProperty);
                return foundProperty;
            }
            else
            {
                return GetPropertiesOfBaseTypesAndSelf().FirstOrDefault(item => item.Name == name);
            }
        }

        #endregion
    }
}
