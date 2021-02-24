using Blueprint41.Neo4j.Refactoring;
using Blueprint41.Neo4j.Refactoring.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using persistence = Blueprint41.Neo4j.Persistence;
using Blueprint41.Core;
using System.Diagnostics;

namespace Blueprint41
{
    [DebuggerDisplay("Relationship: {Name}")]
    public class Relationship : IRefactorRelationship, Core.IRelationshipEvents
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

            Parent = parent;
            RelationshipType      = RelationshipType.None;
            Name                  = ComputeAliasName(name, neo4JRelationshipType, OutProperty);
            Neo4JRelationshipType = ComputeNeo4JName(name, neo4JRelationshipType, OutProperty);
            InInterface           = inInterface ?? new Interface(inEntity!);
            InProperty            = null;
            OutInterface          = outInterface ?? new Interface(outEntity!);
            OutProperty           = null;
            Guid                  = parent.GenerateGuid(name);
        }

        #region Properties

        public DatastoreModel   Parent                { get; private set; }
        public string           Name                  { get; private set; }
        public string           Neo4JRelationshipType { get; private set; }
        public RelationshipType RelationshipType      { get; private set; }

        public Entity           InEntity              { get { return InInterface.BaseEntity; } }
        public Interface        InInterface           { get; private set; }
        public Property?        InProperty            { get; private set; }
        public Entity           OutEntity             { get { return OutInterface.BaseEntity; } }
        public Interface        OutInterface          { get; private set; }
        public Property?        OutProperty           { get; private set; }

        public string           CreationDate { get { return "CreationDate"; } }

        public string           StartDate             { get { return "StartDate"; } }
        public string           EndDate               { get { return "EndDate";  } }
        public bool             IsTimeDependent       { get; private set; }

        public Guid Guid { get; private set; }

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
        public Relationship AddTimeDependance()
        {
            if (IsTimeDependent)
                throw new NotSupportedException(string.Format("The relationship type '{0}' already has time dependence support.", Name));

            IsTimeDependent = true;

            return this;
        }

        public Relationship SetInProperty(string name, PropertyType type, bool nullable = true)
        {
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
        public Relationship SetOutProperty(string name, PropertyType type, bool nullable = true)
        {
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

        internal void ResetProperty(DirectionEnum direction)
        {
            if (direction == DirectionEnum.In)
                InProperty = null;

            if (direction == DirectionEnum.Out)
                OutProperty = null;
        }



        public Relationship CascadedDelete(params Entity[] nodes)
        {
            return this;
        }
 
        #endregion

        #region Refactor Actions

        public IRefactorRelationship Refactor { get { return this; } }
 
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
                Parent.Templates.RenameRelationship(template =>
                {
                    template.Relationship = this;
                    template.OldName = oldName;
                    template.NewName = Neo4JRelationshipType;
                }).RunBatched();
            }
        }

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
                    Parent.Templates.RemoveRelationship(template =>
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
                    Parent.Templates.RemoveRelationship(template =>
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

        void IRefactorRelationship.RemoveTimeDependance()
        {
            Parent.EnsureSchemaMigration();

            if (!IsTimeDependent)
                throw new NotSupportedException(string.Format("The relationship type '{0}' has no time dependence support yet.", Name));

            IsTimeDependent = false;

            throw new NotImplementedException("Apply logic to neo4j db...");
        }
        void IRefactorRelationship.Merge(Relationship target)
        {
            Parent.EnsureSchemaMigration();

            if (this.IsTimeDependent != target.IsTimeDependent)
                throw new InvalidOperationException("You cannot merge 2 relationships with different time dependence settings.");

            if (this.IsTimeDependent)
                throw new NotImplementedException("Merging time dependent relationships is not implemented because I found it annoying to program... You add it yourself if you really want it :oP");

            //TODO: implement...
        }
        void IRefactorRelationship.Deprecate()
        {
            Parent.EnsureSchemaMigration();

            Parent.Templates.RemoveRelationship(template =>
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

        public Core.IRelationshipEvents Events { get { return this; } }

        internal RelationshipEventArgs RaiseOnRelationCreate(Transaction trans)
        {
            RelationshipEventArgs args = new RelationshipEventArgs(EventTypeEnum.OnRelationCreate, trans);
            if (!trans.FireGraphEvents)
                return args;

            onRelationCreate?.Invoke(this, args);

            return args;
        }
        bool IRelationshipEvents.HasRegisteredOnRelationCreateHandlers { get { return onRelationCreate is not null; } }
        private event EventHandler<RelationshipEventArgs>? onRelationCreate;
        event EventHandler<RelationshipEventArgs> IRelationshipEvents.OnRelationCreate
        {
            add { onRelationCreate += value; }
            remove { onRelationCreate -= value; }
        }

        internal RelationshipEventArgs RaiseOnRelationCreated(Transaction trans)
        {
            RelationshipEventArgs args = new RelationshipEventArgs(EventTypeEnum.OnRelationCreated, trans);
            if (!trans.FireGraphEvents)
                return args;

            onRelationCreated?.Invoke(this, args);

            return args;
        }
        bool IRelationshipEvents.HasRegisteredOnRelationCreatedHandlers { get { return onRelationCreated is not null; } }
        private event EventHandler<RelationshipEventArgs>? onRelationCreated;
        event EventHandler<RelationshipEventArgs> IRelationshipEvents.OnRelationCreated
        {
            add { onRelationCreated += value; }
            remove { onRelationCreated -= value; }
        }

        internal RelationshipEventArgs RaiseOnRelationDelete(Transaction trans)
        {
            RelationshipEventArgs args = new RelationshipEventArgs(EventTypeEnum.OnRelationDelete, trans);
            if (!trans.FireGraphEvents)
                return args;

            onRelationDelete?.Invoke(this, args);

            return args;
        }
        bool IRelationshipEvents.HasRegisteredOnRelationDeleteHandlers { get { return onRelationDelete is not null; } }
        private event EventHandler<RelationshipEventArgs>? onRelationDelete;
        event EventHandler<RelationshipEventArgs> IRelationshipEvents.OnRelationDelete
        {
            add { onRelationDelete += value; }
            remove { onRelationDelete -= value; }
        }

        internal RelationshipEventArgs RaiseOnRelationDeleted(Transaction trans)
        {
            RelationshipEventArgs args = new RelationshipEventArgs(EventTypeEnum.OnRelationDelete, trans);
            if (!trans.FireGraphEvents)
                return args;

            onRelationDeleted?.Invoke(this, args);
      
            return args;
        }
        bool IRelationshipEvents.HasRegisteredOnRelationDeletedHandlers { get { return onRelationDeleted is not null; } }
        private event EventHandler<RelationshipEventArgs>? onRelationDeleted;
        event EventHandler<RelationshipEventArgs> IRelationshipEvents.OnRelationDeleted
        {
            add { onRelationDeleted += value; }
            remove { onRelationDeleted -= value; }
        }

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
