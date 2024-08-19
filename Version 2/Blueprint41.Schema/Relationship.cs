using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Blueprint41.Events;
using Blueprint41.Refactoring;

namespace Blueprint41
{
    [DebuggerDisplay("Relationship: {Name}")]
    public partial class Relationship : IRefactorRelationship, IRelationshipEvents
    {
        internal Relationship(DatastoreModel parent, string name, string? neo4JRelationshipType, Entity? inEntity, Interface? inInterface, Entity? outEntity, Interface? outInterface)
        {
            if (inEntity is null && inInterface is null)
                throw new ArgumentNullException("You cannot have both the inEntity and the inInterface be empty.");

            if (outEntity is null && outInterface is null)
                throw new ArgumentNullException("You cannot have both the outEntity and the outInterface be empty.");

            if (inEntity is not null && inInterface is not null)
                throw new ArgumentException("You cannot have both the inEntity and the inInterface set at the same time.");

            if (outEntity is not null && outInterface is not null)
                throw new ArgumentException("You cannot have both the outEntity and the outInterface set at the same time.");

            if ((inEntity?.IsVirtual ?? false) && (outEntity?.IsVirtual ?? false))
                throw new InvalidOperationException($"In and Out sides of the relation '{name}' cannot be virtual at the same time.");

            _self = new IEntity[] { this };
            _properties = new PropertyCollection(this);
            Properties = new EntityPropertyCollection<RelationshipProperty, Relationship>(this, _properties);

            Parent = parent;
            RelationshipType      = RelationshipType.None;
            Name                  = ComputeAliasName(name, neo4JRelationshipType, OutProperty);
            Neo4JRelationshipType = ComputeNeo4JName(name, neo4JRelationshipType, OutProperty);
            InInterface           = inInterface ?? new Interface(inEntity!);
            InProperty            = null;
            OutInterface          = outInterface ?? new Interface(outEntity!);
            OutProperty           = null;
            Guid                  = parent.GenerateGuid(name);

            _properties.Add(CreationDate, new RelationshipProperty(this, PropertyType.Attribute, CreationDate, typeof(DateTime), true, IndexType.None));
        }

        #region Properties

        public DatastoreModel   Parent                { get; private set; }
        public string           Name                  { get; private set; }
        public string           Neo4JRelationshipType { get; private set; }
        public RelationshipType RelationshipType      { get; private set; }

        public Entity           InEntity              { get { return InInterface.BaseEntity; } }
        public Interface        InInterface           { get; private set; }
        public EntityProperty?  InProperty            { get; private set; }
        public Entity           OutEntity             { get { return OutInterface.BaseEntity; } }
        public Interface        OutInterface          { get; private set; }
        public EntityProperty?  OutProperty           { get; private set; }

        public string           CreationDate          { get { return "CreationDate"; } }

        public string           StartDate             { get { return "StartDate"; } }
        public string           EndDate               { get { return "EndDate";  } }
        public bool             IsTimeDependent       { get; private set; }

        internal string InEntityName
        {
            get
            {
                if (InEntity.Name == OutEntity.Name)
                    return "In" + InEntity.Name;

                return InEntity.Name;
            }
        }
        internal string OutEntityName
        {
            get
            {
                if (InEntity.Name == OutEntity.Name)
                    return "Out" + OutEntity.Name;

                return OutEntity.Name;
            }
        }


        public Guid Guid { get; private set; }

        public EntityPropertyCollection<RelationshipProperty, Relationship> Properties { get; private set; }
        private readonly PropertyCollection _properties;

        public Relationship SetFullTextProperty(string propertyName)
        {
            RelationshipProperty? property = Search(propertyName);
            if (property is null)
                throw new NotSupportedException("Property does not exist.");

            fullTextIndexProperties.Add(property);
            return this;
        }
        public Relationship RemoveFullTextProperty(string propertyName)
        {
            RelationshipProperty? property = Search(propertyName);
            if (property is null)
                throw new NotSupportedException("Property does not exist.");

            fullTextIndexProperties.Remove(property);
            return this;
        }
        public IReadOnlyList<RelationshipProperty> FullTextIndexProperties
        {
            get { return fullTextIndexProperties; }
        }
        private List<RelationshipProperty> fullTextIndexProperties = new List<RelationshipProperty>();

        #endregion

        #region Modeling Actions

        internal void Rename(string newName, string? newNeo4JRelationshipType = null)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentNullException("newName");

            Parent.Relations.Remove(Name);
            Name = newName;
            Parent.Relations.Add(Name, this);

            Neo4JRelationshipType = newNeo4JRelationshipType ?? newName;
        }

        /// <summary>
        /// Add time dependence support
        /// </summary>
        public Relationship AddTimeDependence()
        {
            if (IsTimeDependent)
                throw new NotSupportedException(string.Format("The relationship type '{0}' already has time dependence support.", Name));

            IsTimeDependent = true;

            _properties.Add(StartDate, new RelationshipProperty(this, PropertyType.Attribute, StartDate, typeof(DateTime), true, IndexType.None));
            _properties.Add(EndDate, new RelationshipProperty(this, PropertyType.Attribute, EndDate, typeof(DateTime), true, IndexType.None));

            return this;
        }

        /// <summary>
        /// Set the in-property
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="type">The type of the property</param>
        /// <param name="nullable">Whether the property can be null</param>
        public Relationship SetInProperty(string name, PropertyType type, bool nullable = true)
        {
            Parent.EnsureSchemaMigration();

            if (InProperty is not null)
                throw new InvalidOperationException("There is already an in property defined.");

            switch (type)
            {
                case PropertyType.Lookup:
                    InEntity.AddLookup(name, OutEntity, nullable);
                    break;
                case PropertyType.Collection:
                    InEntity.AddCollection(name, OutEntity);
                    break;
                default:
                    throw new NotSupportedException();
            }
            InProperty = InEntity.Properties[name];
            InProperty.Relationship = this;
            InProperty.Direction = DirectionEnum.In;

            // StaticData
            InEntity.DynamicEntityPropertyAdded(InProperty);

            return this;
        }

        /// <summary>
        /// Set the out-property
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="type">The type of the property</param>
        /// <param name="nullable">Whether the property can be null</param>
        public Relationship SetOutProperty(string name, PropertyType type, bool nullable = true)
        {
            Parent.EnsureSchemaMigration();

            if (OutProperty is not null)
                throw new InvalidOperationException("There is already an in property defined.");

            switch (type)
            {
                case PropertyType.Lookup:
                    OutEntity.AddLookup(name, InEntity, nullable);
                    break;
                case PropertyType.Collection:
                    OutEntity.AddCollection(name, InEntity);
                    break;
                default:
                    throw new NotSupportedException();
            }
            OutProperty = OutEntity.Properties[name];
            OutProperty.Relationship = this;
            OutProperty.Direction = DirectionEnum.Out;

            // StaticData
            OutEntity.DynamicEntityPropertyAdded(OutProperty);

            return this;
        }

        /// <summary>
        /// Add a new property
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="type">The type of the property</param>
        /// <returns></returns>
        public Relationship AddProperty(string name, Type type)
        {
            return AddProperty(name, type, true, IndexType.None);
        }

        /// <summary>
        /// Add a new property
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="enumeration">The possible values the property can have</param>
        /// <param name="nullable">Whether the property can be null</param>
        /// <param name="indexType">The type of the index</param>
        /// <returns></returns>
        public Relationship AddProperty(string name, string[] enumeration, bool nullable = true, IndexType indexType = IndexType.None)
        {
            VerifyPropertiesCanBeAdded();

            RelationshipProperty value = new RelationshipProperty(this, PropertyType.Attribute, name, typeof(string), nullable, indexType, enumeration);
            _properties.Add(name, value);

            return this;
        }

        /// <summary>
        /// Add a new property
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="enumeration">The possible values the property can have</param>
        /// <param name="nullable">Whether the property can be null</param>
        /// <param name="indexType">The type of the index</param>
        /// <returns></returns>
        public Relationship AddProperty(string name, Enumeration enumeration, bool nullable = true, IndexType indexType = IndexType.None)
        {
            VerifyPropertiesCanBeAdded();

            RelationshipProperty value = new RelationshipProperty(this, PropertyType.Attribute, name, typeof(string), nullable, indexType, enumeration);
            _properties.Add(name, value);

            return this;
        }

        /// <summary>
        /// Add a new property
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="type">The type of the property</param>
        /// <param name="indexType">The type of the index</param>
        /// <returns></returns>
        public Relationship AddProperty(string name, Type type, IndexType indexType)
        {
            return AddProperty(name, type, true, indexType);
        }

        /// <summary>
        /// Add a new property
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="type">The type of the property</param>
        /// <param name="nullable">Whether the property can be null</param>
        /// <param name="indexType">The type of the index</param>
        /// <returns></returns>
        public Relationship AddProperty(string name, Type type, bool nullable, IndexType indexType = IndexType.None)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                throw new ArgumentException(string.Format("The type argument does not support the 'Nullable<{0}>' type. All types are considered nullable by default, but you can also set the 'nullable' argument explicitly.", type.GenericTypeArguments[0].Name));

            VerifyPropertiesCanBeAdded();

            RelationshipProperty value = new RelationshipProperty(this, PropertyType.Attribute, name, type, nullable, indexType);
            _properties.Add(name, value);

            return this;
        }
        private void VerifyPropertiesCanBeAdded()
        {
            Parent.EnsureSchemaMigration();

            if (InProperty is null && OutProperty is null)
                throw new InvalidOperationException("At least 1 in or out property needs to be set before primitive properties can be added.");

            //if (IsTimeDependent)
            //    throw new NotSupportedException(string.Format("Primitive properties cannot be added since the relationship type '{0}' already has time dependence enabled.", Name));
        }

        internal void ResetProperty(DirectionEnum direction)
        {
            Parent.EnsureSchemaMigration();

            if (direction == DirectionEnum.In)
                InProperty = null;

            if (direction == DirectionEnum.Out)
                OutProperty = null;
        }

        #endregion

        #region Refactor Actions

        /// <summary>
        /// The refactor actions
        /// </summary>
        public IRefactorRelationship Refactor { get { return this; } }

        /// <summary>
        /// Renames the relationship
        /// </summary>
        /// <param name="newName">The new name</param>
        /// <param name="newNeo4JRelationshipType">The new neo4j relationship type</param>
        void IRefactorRelationship.Rename(string newName, string newNeo4JRelationshipType)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentNullException(nameof(newName));

            Parent.EnsureSchemaMigration();

            if (newNeo4JRelationshipType is null)
                newNeo4JRelationshipType = newName;

            string oldName = Neo4JRelationshipType;
            Rename(newName, newNeo4JRelationshipType);

            if (oldName != Neo4JRelationshipType)
            {
                Parent.PersistenceProvider.Templates.RenameRelationship(template =>
                {
                    template.Caller = this;
                    template.OldName = oldName;
                    template.NewName = Neo4JRelationshipType;
                }).RunBatched();
            }
        }

        /// <summary>
        /// Sets the in-entity
        /// </summary>
        /// <param name="target">The new in-entity</param>
        /// <param name="allowLosingData">Whether to allow losing data</param>
        void IRefactorRelationship.SetInEntity(Entity target, bool allowLosingData)
        {
            Parent.EnsureSchemaMigration();

            if (target.IsSubsclassOf(InEntity))
            {
                if(!allowLosingData)
                    throw new ArgumentException(string.Format("Target {0} is a sub type of {1}, you could lose data. If you want to do this anyway, you can set allowLosingData to true.", target.Name, InEntity.Name), "baseType");

                foreach (var item in InEntity.GetSubclasses())
                {
                    if (item.IsAbstract || item.IsVirtual)
                        continue;

                    if (item.IsSelfOrSubclassOf(target))
                        continue;

                    //delete relations
                    Parent.PersistenceProvider.Templates.RemoveRelationship(template =>
                    {
                        template.InEntity = item.Label.Name;
                        template.Relation = Neo4JRelationshipType;
                        template.OutEntity = OutEntity.Label.Name;
                    }).RunBatched();
                }
            }
            else if (!InEntity.IsSubsclassOf(target))
                throw new ArgumentException(string.Format("Target {0} is not a base or sub type of {1}. Consider using 'Reroute'.", target.Name, InEntity.Name), "baseType");

            if (InProperty is not null)
            {
                InEntity.Properties.Remove(InProperty.Name);
                target.Properties.Add(InProperty.Name, InProperty);
                InProperty.SetParentEntity(target);
            }
            if (OutProperty is not null)
                OutProperty.SetReturnTypeEntity(target);

            InInterface = new Interface(target);
        }

        /// <summary>
        /// Sets the out-entity
        /// </summary>
        /// <param name="target">The new out-entity</param>
        /// <param name="allowLosingData">Whether to allow losing data</param>
        void IRefactorRelationship.SetOutEntity(Entity target, bool allowLosingData)
        {
            Parent.EnsureSchemaMigration();

            if (target.IsSubsclassOf(OutEntity))
            {
                if (!allowLosingData)
                    throw new ArgumentException(string.Format("Target {0} is a sub type of {1}, you could lose data. If you want to do this anyway, you can set allowLosingData to true.", target.Name, OutEntity.Name), "baseType");

                foreach (var item in OutEntity.GetSubclasses())
                {
                    if (item.IsAbstract || item.IsVirtual)
                        continue;

                    if (item.IsSelfOrSubclassOf(target))
                        continue;

                    //delete relations
                    Parent.PersistenceProvider.Templates.RemoveRelationship(template =>
                    {
                        template.InEntity = InEntity.Label.Name;
                        template.Relation = Neo4JRelationshipType;
                        template.OutEntity = item.Label.Name;
                    }).RunBatched();
                }
            }
            else if (!OutEntity.IsSubsclassOf(target))
                throw new ArgumentException(string.Format("Target {0} is not a base or sub type of {1}. Consider using 'Reroute'.", target.Name, OutEntity.Name), "baseType");

            if (OutProperty is not null)
            {
                OutEntity.Properties.Remove(OutProperty.Name);
                target.Properties.Add(OutProperty.Name, OutProperty);
                OutProperty.SetParentEntity(target);
            }
            if (InProperty is not null)
                InProperty.SetReturnTypeEntity(target);

            OutInterface = new Interface(target);
        }

        /// <summary>
        /// Removes the time dependence
        /// </summary>
        void IRefactorRelationship.RemoveTimeDependence()
        {
            Parent.EnsureSchemaMigration();

            if (!IsTimeDependent)
                throw new NotSupportedException(string.Format("The relationship type '{0}' has no time dependence set.", Name));

            IsTimeDependent = false;

            _properties.Remove(StartDate); 
            _properties.Remove(EndDate);

            throw new NotImplementedException("Apply logic to neo4j db...");
        }

        /// <summary>
        /// Deprecates the relationship
        /// </summary>
        void IRefactorRelationship.Deprecate()
        {
            Parent.EnsureSchemaMigration();

            Parent.PersistenceProvider.Templates.RemoveRelationship(template =>
            {
                template.InEntity = InEntity.Label.Name;
                template.Relation = Neo4JRelationshipType;
                template.OutEntity = OutEntity.Label.Name;
            }).RunBatched();

            if (this.InProperty is not null)
                this.InEntity.Properties.Remove(this.InProperty.Name);

            if (this.OutProperty is not null)
                this.OutEntity.Properties.Remove(this.OutProperty.Name);

            Parent.Relations.Remove(Name);
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// The relationship events
        /// </summary>
        public IRelationshipEvents Events { get { return this; } }

        /// <summary>
        /// This event fires during the commit stage, when a new relationship is about to be created
        /// </summary>
        event EventHandler<RelationshipEventArgs> IRelationshipEvents.OnRelationCreate
        {
            add { onRelationCreate += value; }
            remove { onRelationCreate -= value; }
        }
        /// <summary>
        /// True when a OnRelationCreate event is registered
        /// </summary>
        bool IRelationshipEvents.HasRegisteredOnRelationCreateHandlers { get { return onRelationCreate is not null; } }
        internal RelationshipEventArgs RaiseOnRelationCreate(Transaction trans)
        {
            RelationshipEventArgs args = new RelationshipEventArgs(EventTypeEnum.OnRelationCreate, trans);
            if (!trans.FireGraphEvents)
                return args;

            onRelationCreate?.Invoke(this, args);

            return args;
        }
        private event EventHandler<RelationshipEventArgs>? onRelationCreate;

        /// <summary>
        /// This event fires during the commit stage, after the relationship was created. The transaction is read-only at this moment.
        /// </summary>
        event EventHandler<RelationshipEventArgs> IRelationshipEvents.OnRelationCreated
        {
            add { onRelationCreated += value; }
            remove { onRelationCreated -= value; }
        }
        /// <summary>
        /// True when a OnRelationCreated event is registered
        /// </summary>
        bool IRelationshipEvents.HasRegisteredOnRelationCreatedHandlers { get { return onRelationCreated is not null; } }
        internal RelationshipEventArgs RaiseOnRelationCreated(Transaction trans)
        {
            RelationshipEventArgs args = new RelationshipEventArgs(EventTypeEnum.OnRelationCreated, trans);
            if (!trans.FireGraphEvents)
                return args;

            onRelationCreated?.Invoke(this, args);

            return args;
        }
        private event EventHandler<RelationshipEventArgs>? onRelationCreated;

        /// <summary>
        /// This event fires during the commit stage, when an existing relationship is about to be deleted
        /// </summary>>
        event EventHandler<RelationshipEventArgs> IRelationshipEvents.OnRelationDelete
        {
            add { onRelationDelete += value; }
            remove { onRelationDelete -= value; }
        }
        /// <summary>
        /// True when a OnRelationDelete event is registered
        /// </summary>
        bool IRelationshipEvents.HasRegisteredOnRelationDeleteHandlers { get { return onRelationDelete is not null; } }
        internal RelationshipEventArgs RaiseOnRelationDelete(Transaction trans)
        {
            RelationshipEventArgs args = new RelationshipEventArgs(EventTypeEnum.OnRelationDelete, trans);
            if (!trans.FireGraphEvents)
                return args;

            onRelationDelete?.Invoke(this, args);

            return args;
        }
        private event EventHandler<RelationshipEventArgs>? onRelationDelete;

        /// <summary>
        /// This event fires during the commit stage, when an existing relationship was deleted. The transaction is read-only at this moment.
        /// </summary>
        event EventHandler<RelationshipEventArgs> IRelationshipEvents.OnRelationDeleted
        {
            add { onRelationDeleted += value; }
            remove { onRelationDeleted -= value; }
        }
        /// <summary>
        /// True when a OnRelationDeleted event is registered
        /// </summary>
        bool IRelationshipEvents.HasRegisteredOnRelationDeletedHandlers { get { return onRelationDeleted is not null; } }
        internal RelationshipEventArgs RaiseOnRelationDeleted(Transaction trans)
        {
            RelationshipEventArgs args = new RelationshipEventArgs(EventTypeEnum.OnRelationDelete, trans);
            if (!trans.FireGraphEvents)
                return args;

            onRelationDeleted?.Invoke(this, args);
      
            return args;
        }
        private event EventHandler<RelationshipEventArgs>? onRelationDeleted;

        #endregion

        #region Helper Methods

        /// <summary>
        /// Search for a property
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <returns>The property</returns>
        public RelationshipProperty? Search(string name)
        {
            if (Parent.IsUpgraded)
            {
                if (namedProperties is null)
                    namedProperties = Properties.ToDictionary(key => key.Name.ToLower(), value => value);

                RelationshipProperty? foundProperty;
                namedProperties.TryGetValue(name.ToLower(), out foundProperty);
                return foundProperty;
            }
            else
            {
                return Properties.FirstOrDefault(item => item.Name.ToLower() == name.ToLower());
            }
        }
        private IReadOnlyDictionary<string, RelationshipProperty>? namedProperties = null;

        internal IReadOnlyList<string> ExcludedProperties()
        {
            if (Parent.IsUpgraded)
            {
                if (excludedProperties is null)
                    excludedProperties = Get();

                return excludedProperties;
            }
            else
            {
                return Get();
            }

            List<string> Get()
            {
                List<string> excl = new List<string>();
                
                if (!string.IsNullOrEmpty(StartDate))
                    excl.Add(StartDate);
                
                if (!string.IsNullOrEmpty(EndDate))
                    excl.Add(EndDate);
                
                if (!string.IsNullOrEmpty(CreationDate))
                    excl.Add(CreationDate);
                
                return excl;
            }
        }
        private List<string>? excludedProperties = null;

        #endregion

        private string ComputeAliasName(string? name, string? neo4JRelationshipType, Property? outProperty)
        {
            if (!(name is null))
                return name;

            return ComputeNeo4JName(name, neo4JRelationshipType, outProperty);
        }
        private string ComputeNeo4JName(string? name, string? neo4JRelationshipType, Property? outProperty)
        {
            if (!(neo4JRelationshipType is null))
                return neo4JRelationshipType;

            if (!(name is null))
                return name;

            return string.Format("{0}_{1}", InEntity.Name.ToUpperInvariant(), OutEntity.Name.ToUpperInvariant());
       }

        internal DirectionEnum ComputeDirection(Entity entity)
        {
            return entity.IsSelfOrSubclassOf(InEntity) ? DirectionEnum.In : DirectionEnum.Out;
        }
    }
}
