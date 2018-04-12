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
        internal Relationship(DatastoreModel parent, string name, string neo4JRelationshipType, Entity inEntity, Entity outEntity)
        {
            Parent = parent;
            RelationshipType =  RelationshipType.None;
            Name = ComputeAliasName(name, neo4JRelationshipType, OutProperty);
            Neo4JRelationshipType = ComputeNeo4JName(name, neo4JRelationshipType, OutProperty);
            InEntity = inEntity;
            InProperty = null;
            OutEntity = outEntity;
            OutProperty = null;
            Guid = parent.GenerateGuid(name);
        }

        #region Properties

        public DatastoreModel   Parent                { get; private set; }
        public string           Name                  { get; private set; }
        public string           Neo4JRelationshipType { get; private set; }
        public RelationshipType RelationshipType      { get; private set; }

        public Entity           InEntity              { get; private set; }
        public Property         InProperty            { get; private set; }
        public Entity           OutEntity             { get; private set; }
        public Property         OutProperty           { get; private set; }

        public string           CreationDate { get { return "CreationDate"; } }

        public string           StartDate             { get { return "StartDate"; } }
        public string           EndDate               { get { return "EndDate";  } }
        public bool             IsTimeDependent       { get; private set; }

        public Guid Guid { get; private set; }

        #endregion

        #region Modeling Actions

        internal void Rename(string newName, string newNeo4JRelationshipType = null)
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
            if (InProperty != null)
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
            if (OutProperty != null)
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
 
        void IRefactorRelationship.Rename(string newName, string alias)
        {
            Parent.EnsureSchemaMigration();

            if (string.IsNullOrEmpty(newName))
                throw new ArgumentNullException("neo4JRelationshipType");

            if (alias == null)
                alias = newName;

            string oldName = Neo4JRelationshipType;
            Rename(newName, alias);

            Parser.ExecuteBatched<RenameRelationship>(delegate (RenameRelationship template)
            {
                template.Relationship = this;
                template.OldName = oldName;
                template.NewName = Neo4JRelationshipType;
            });
        }

        void IRefactorRelationship.SetInEntity(Entity target)
        {
            Parent.EnsureSchemaMigration();

            if (!InEntity.IsSubsclassOf(target))
                throw new ArgumentException(string.Format("Target {0} is not a base type of {1}", target.Name, InEntity.Name), "baseType");

            // No changes in DB needed for this action!!!

            if (InProperty != null)
            {
                InEntity.Properties.Remove(InProperty.Name);
                target.Properties.Add(InProperty.Name, InProperty);
                InProperty.SetParentEntity(target);
            }
            if (OutProperty != null)
                OutProperty.SetReturnTypeEntity(target);

            InEntity = target;
        }
        void IRefactorRelationship.SetOutEntity(Entity target)
        {
            Parent.EnsureSchemaMigration();

            if (!OutEntity.IsSubsclassOf(target))
                throw new ArgumentException(string.Format("Target {0} is not a base type of {1}", target.Name, OutEntity.Name), "baseType");

            // No changes in DB needed for this action!!!

            if (OutProperty != null)
            {
                OutEntity.Properties.Remove(OutProperty.Name);
                target.Properties.Add(OutProperty.Name, OutProperty);
                OutProperty.SetParentEntity(target);
            }
            if (InProperty != null)
                InProperty.SetReturnTypeEntity(target);

            OutEntity = target;
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

            Parser.ExecuteBatched<RemoveRelationship>(delegate (RemoveRelationship template)
            {
                template.InEntity = InEntity.Label.Name;
                template.Relation = Neo4JRelationshipType;
                template.OutEntity = OutEntity.Label.Name;
            });

            if (this.InProperty != null)
                this.InEntity.Properties.Remove(this.InProperty.Name);

            if (this.OutProperty != null)
                this.OutEntity.Properties.Remove(this.OutProperty.Name);

            Parent.Relations.Remove(Name);
        }

        #endregion

        #region Event Handlers

        public Core.IRelationshipEvents Events { get { return this; } }

        internal void RaiseOnRelationCreate(RelationshipEventArgs args)
        {
            EventHandler<RelationshipEventArgs> handler = onRelationCreate;
            if ((object)handler != null)
                handler.Invoke(this, args);
        }
        bool IRelationshipEvents.HasRegisteredOnRelationCreateHandlers { get { return onRelationCreate != null; } }
        private event EventHandler<RelationshipEventArgs> onRelationCreate;
        event EventHandler<RelationshipEventArgs> IRelationshipEvents.OnRelationCreate
        {
            add { onRelationCreate += value; }
            remove { onRelationCreate -= value; }
        }

        internal void RaiseOnRelationCreated(RelationshipEventArgs args)
        {
            EventHandler<RelationshipEventArgs> handler = onRelationCreated;
            if ((object)handler != null)
                handler.Invoke(this, args);
        }
        bool IRelationshipEvents.HasRegisteredOnRelationCreatedHandlers { get { return onRelationCreated != null; } }
        private event EventHandler<RelationshipEventArgs> onRelationCreated;
        event EventHandler<RelationshipEventArgs> IRelationshipEvents.OnRelationCreated
        {
            add { onRelationCreated += value; }
            remove { onRelationCreated -= value; }
        }

        internal void RaiseOnRelationDelete(RelationshipEventArgs args)
        {
            EventHandler<RelationshipEventArgs> handler = onRelationDelete;
            if ((object)handler != null)
                handler.Invoke(this, args);
        }
        bool IRelationshipEvents.HasRegisteredOnRelationDeleteHandlers { get { return onRelationDelete != null; } }
        private event EventHandler<RelationshipEventArgs> onRelationDelete;
        event EventHandler<RelationshipEventArgs> IRelationshipEvents.OnRelationDelete
        {
            add { onRelationDelete += value; }
            remove { onRelationDelete -= value; }
        }

        internal void RaiseOnRelationDeleted(RelationshipEventArgs args)
        {
            EventHandler<RelationshipEventArgs> handler = onRelationDeleted;
            if ((object)handler != null)
                handler.Invoke(this, args);
        }
        bool IRelationshipEvents.HasRegisteredOnRelationDeletedHandlers { get { return onRelationDeleted != null; } }
        private event EventHandler<RelationshipEventArgs> onRelationDeleted;
        event EventHandler<RelationshipEventArgs> IRelationshipEvents.OnRelationDeleted
        {
            add { onRelationDeleted += value; }
            remove { onRelationDeleted -= value; }
        }

        #endregion

        private string ComputeAliasName(string name, string neo4JRelationshipType, Property outProperty)
        {
            if (name != null)
                return name;

            return ComputeNeo4JName(name, neo4JRelationshipType, outProperty);
        }
        private string ComputeNeo4JName(string name, string neo4JRelationshipType, Property outProperty)
        {
            if (neo4JRelationshipType != null)
                return neo4JRelationshipType;

            if (name != null)
                return name;

            return string.Format("{0}_{1}", InEntity.Name.ToUpperInvariant(), OutEntity.Name.ToUpperInvariant());
       }
    }
}
