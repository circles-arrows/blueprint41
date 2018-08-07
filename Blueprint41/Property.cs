using Blueprint41.Neo4j.Refactoring;
using Blueprint41.Neo4j.Refactoring.Templates;
using Templates = Blueprint41.Neo4j.Refactoring.Templates;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Blueprint41.Core;
using System.Reflection;
using Blueprint41.Dynamic;
using Blueprint41.Neo4j.Schema;

namespace Blueprint41
{
    [DebuggerDisplay("{Parent.Name}.{Name}")]
    public class Property : IRefactorProperty, IPropertyCondition, IPropertyEvents
    {
        internal Property(Entity parent, PropertyType storage, string name, Type systemType, bool nullable, IndexType indexType)
        {
            Parent = parent;
            PropertyType = storage;
            Name = name;
            SystemReturnType = systemType;
            EntityReturnType = null;
            Nullable = nullable;
            IndexType = indexType;
            Reference = null;
            Guid = parent.Parent.GenerateGuid(string.Concat(parent.Guid, ".", name));
        }
        internal Property(Entity parent, PropertyType storage, string name, Entity entityType, bool nullable, IndexType indexType)
        {
            Parent = parent;
            PropertyType = storage;
            Name = name;
            SystemReturnType = null;
            EntityReturnType = entityType;
            Nullable = nullable;
            IndexType = indexType;
            Reference = null;
            Guid = parent.Parent.GenerateGuid(string.Concat(parent.Guid, ".", name));
        }
        internal Property(Entity parent, PropertyType storage, string name, Property reference)
        {
            Parent = parent;
            PropertyType = storage;
            Name = name;
            SystemReturnType = null;
            EntityReturnType = null;
            Nullable = true;
            IndexType = IndexType.None;
            Reference = reference;
            Guid = parent.Parent.GenerateGuid(string.Concat(parent.Guid, ".", name));
        }

        #region Properties

        public Entity Parent { get; private set; }
        public PropertyType PropertyType { get; private set; }
        public string Name { get; private set; }
        public Type SystemReturnType { get; private set; }
        public Type SystemReturnTypeWithNullability
        {
            get
            {
                if (SystemReturnType == null)
                    return null;

                if (!Nullable)
                    return SystemReturnType;

                if (!SystemReturnType.IsValueType)
                    return SystemReturnType;

                return typeof(Nullable<>).MakeGenericType(SystemReturnType);  //Caching this type?...
            }
        }
        public Entity EntityReturnType { get; private set; }
        public bool Nullable { get; private set; }
        public IndexType IndexType { get; private set; }
        public bool IsKey { get; internal set; }
        public bool IsNodeType { get; internal set; }
        public bool IsRowVersion { get; internal set; }
        internal bool HideSetter { get; set; }
        public Property Reference { get; private set; }
        public Guid Guid { get; private set; }

        private string innerReturnType = null;
        public string InnerReturnType
        {
            get
            {
                if (innerReturnType == null)
                {

                    StringBuilder sb = new StringBuilder();

                    if (PropertyType == PropertyType.Collection || PropertyType == PropertyType.Lookup)
                    {
                        if (Relationship.IsTimeDependent)
                            sb.Append("EntityTimeCollection<");
                        else
                            sb.Append("EntityCollection<");
                    }

                    if (SystemReturnType != null)
                        sb.Append(SystemReturnType.ToCSharp() + (this.Nullable && this.SystemReturnType.IsValueType ? "?" : ""));
                    else if (EntityReturnType != null)
                        sb.Append(EntityReturnType.ClassName);
                    else
                        sb.Append(Reference.InnerReturnType);

                    if (PropertyType == PropertyType.Collection || PropertyType == PropertyType.Lookup)
                        sb.Append(">");

                    innerReturnType = sb.ToString();
                }

                return innerReturnType;
            }
        }

        private string outerReturnType = null;
        public string OuterReturnType
        {
            get
            {
                if (outerReturnType == null)
                {

                    StringBuilder sb = new StringBuilder();

                    if (PropertyType == PropertyType.Collection)
                    {
                        if (Relationship.IsTimeDependent)
                            sb.Append("EntityTimeCollection<");
                        else
                            sb.Append("EntityCollection<");
                    }

                    if (SystemReturnType != null)
                        sb.Append(SystemReturnType.ToCSharp() + (this.Nullable && this.SystemReturnType.IsValueType ? "?" : ""));
                    else if (EntityReturnType != null)
                        sb.Append(EntityReturnType.ClassName);
                    else
                        sb.Append(Reference.OuterReturnType);

                    if (PropertyType == PropertyType.Collection)
                        sb.Append(">");

                    outerReturnType = sb.ToString();
                }

                return outerReturnType;
            }
        }

        private string outerReturnTypeReadOnly = null;
        public string OuterReturnTypeReadOnly
        {
            get
            {
                if (outerReturnTypeReadOnly == null)
                {

                    StringBuilder sb = new StringBuilder();

                    if (PropertyType == PropertyType.Collection)
                    {
                        sb.Append("IEnumerable<");
                    }

                    if (SystemReturnType != null)
                        sb.Append(SystemReturnType.ToCSharp() + (this.Nullable && this.SystemReturnType.IsValueType ? "?" : ""));
                    else if (EntityReturnType != null)
                        sb.Append(EntityReturnType.ClassName);
                    else
                        sb.Append(Reference.OuterReturnTypeReadOnly);

                    if (PropertyType == PropertyType.Collection)
                        sb.Append(">");

                    outerReturnTypeReadOnly = sb.ToString();
                }

                return outerReturnTypeReadOnly;
            }
        }

        public Relationship Relationship { get; internal set; } = null;
        public DirectionEnum Direction { get; internal set; } = DirectionEnum.None;

        public Entity ForeignEntity
        {
            get
            {
                if (Relationship == null)
                    return null;

                switch (Direction)
                {
                    case DirectionEnum.In:
                        return Relationship.OutEntity;
                    case DirectionEnum.Out:
                        return Relationship.InEntity;
                    default:
                        return null;
                }
            }
        }
        public Property ForeignProperty
        {
            get
            {
                if (Relationship == null)
                    return null;

                switch (Direction)
                {
                    case DirectionEnum.In:
                        return Relationship.OutProperty;
                    case DirectionEnum.Out:
                        return Relationship.InProperty;
                    default:
                        return null;
                }
            }
        }

        internal void SetParentEntity(Entity parent)
        {
            Parent = parent;
        }
        internal void SetReturnTypeEntity(Entity returnType)
        {
            EntityReturnType = returnType; 
        }

        #endregion

        #region Refactor Actions

        public IRefactorProperty Refactor { get { return this; } }

        void IRefactorProperty.Rename(string newName)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentNullException(nameof(newName));

            Parent.Parent.EnsureSchemaMigration();

            if (PropertyType == PropertyType.Attribute)
            {
                RemoveIndexesAndContraints();

                foreach (var entity in Parent.GetConcreteClasses())
                {
                    Parser.ExecuteBatched<RenameProperty>(delegate (RenameProperty template)
                    {
                        template.ConcreteParent = entity;
                        template.From = this;
                        template.To = newName;
                    });
                }
            }

            string oldName = Name;

            Parent.Properties.Remove(oldName);
            Parent.Properties.Add(newName, this);
            Name = newName;

            // StaticData
            Parent.DynamicEntityPropertyRenamed(oldName, this);
        }

        private void RemoveIndexesAndContraints()
        {
            Parent.Parent.EnsureSchemaMigration();

            if (Parser.ShouldExecute && (Nullable == false || IndexType != IndexType.None))
            {
                Nullable = true;
                IndexType = IndexType.None;

                foreach (var entity in Parent.GetConcreteClasses())
                {
                    ApplyConstraintEntity applyConstraint = new ApplyConstraintEntity(Parent.Parent.GetSchema(), entity);
                    foreach (var action in applyConstraint.Actions.Where(c => c.Property == Name))
                    {
                        foreach (string query in action.ToCypher())
                        {
                            Parser.Execute(query, null);
                        }
                    }
                }
            }
        }

        void IRefactorProperty.Move(Entity target)
        {
            Parent.Parent.EnsureSchemaMigration();

            if (PropertyType != PropertyType.Attribute)
                throw new NotSupportedException("Consider using the refactor action 'SetInEntity' or 'SetOutEntity' on the relationship.");

            if (!Parent.IsSubsclassOf(target))
                throw new ArgumentException(string.Format("Target {0} is not a base type of {1}", target.Name, Parent.Name), "baseType");

            // No changes in DB needed for this action!!!

            Parent.Properties.Remove(Name);
            target.Properties.Add(Name, this);
            this.Parent = target;
        }
        //void IRefactorProperty.Move(string pattern, string newPropertyName)
        //{
        //    throw new NotImplementedException();
        //}

        void IRefactorProperty.Merge(Property target, MergeAlgorithm mergeAlgorithm)
        {
            Parent.Parent.EnsureSchemaMigration();

            if (PropertyType != PropertyType.Attribute)
                throw new NotImplementedException();

            if (!Parent.IsSelfOrSubclassOf(target.Parent))
                throw new ArgumentException(string.Format("Target entity {0} is not a base type of {1}", target.Parent.Name, Parent.Name), "baseType");

            if (PropertyType != target.PropertyType)
                throw new NotSupportedException("Merging is not supported for properties that have a different StorageModel.");

            Parent.Properties.Remove(Name); // remove this property

            if (target.PropertyType == PropertyType.Collection || target.PropertyType == PropertyType.Lookup)
            {
                MergeRelationship(target, mergeAlgorithm);
            }
            else
            {
                MergeProperty(target, mergeAlgorithm);
            }

            //switch (target.StorageModel)
            //{
            //    case StorageModel.Lookup:
            //    case StorageModel.Collection:
            //        string format = string.Format("MATCH (in:{0})-[rel:{1}]-(out:{2}) MERGE (in)-[:LANGUAGE_FOR]->(out) DELETE rel", 
            //            this.Relationship.InEntity.Label.Name,
            //            this.Relationship.Name,
            //            this.Relationship.OutEntity.Label.Name,

            //            )
            //        break;
            //    case StorageModel.Attribute:
            //        break;
            //    case StorageModel.Blob:
            //        throw new NotImplementedException();
            //    case StorageModel.Cloned:
            //        throw new NotImplementedException();
            //    default:
            //        throw new NotImplementedException();
            //}

            // StaticData
            Parent.DynamicEntityPropertyRenamed(Name, target, mergeAlgorithm);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="baseProperty"></param>
        ///// <param name="mergeAlgorithm">(source, target) => source ?? target</param>
        //void IRefactorProperty.Merge(Property target, Func<object, object, object> mergeAlgorithm)
        //{
        //    if (PropertyType != PropertyType.Attribute)
        //        throw new NotImplementedException();

        //    throw new NotSupportedException();

        //    //// StaticData
        //    //Parent.DynamicEntityPropertyRenamed(Name, target, mergeAlgorithm);
        //}

        private void MergeProperty(Property target, MergeAlgorithm mergeAlgorithm)
        {
            Parent.Parent.EnsureSchemaMigration();

            if (PropertyType != PropertyType.Attribute)
                throw new NotImplementedException();

            foreach (var entity in Parent.GetConcreteClasses())
            {
                Parser.ExecuteBatched<MergeProperty>(delegate (MergeProperty template)
                {
                    template.From = this;
                    template.To = target;
                    template.ConcreteParent = entity;
                    template.MergeAlgorithm = mergeAlgorithm;
                });
            }
        }

        private void MergeRelationship(Property target, MergeAlgorithm mergeAlgorithm)
        {
            Parent.Parent.EnsureSchemaMigration();

            if (PropertyType != PropertyType.Attribute)
                throw new NotImplementedException();

            if (!Relationship.RelationshipType.IsCompatible(target.Relationship.RelationshipType))
                throw new NotSupportedException(string.Format("Merging is not supported between relationships of type {0} and {1}.", this.Relationship.RelationshipType.ToString(), target.Relationship.RelationshipType.ToString()));

            if (Relationship.RelationshipType.IsImplicitLookup() || target.Relationship.RelationshipType.IsImplicitLookup())
                if (mergeAlgorithm == MergeAlgorithm.NotApplicable)
                    throw new NotSupportedException("You cannot ignore conflicts on lookup properties.");

            Parser.ExecuteBatched<MergeRelationship>(delegate (MergeRelationship template)
            {
                template.From = this.Relationship;
                template.To = target.Relationship;
                template.MergeAlgorithm = mergeAlgorithm;
            });
        }

        void IRefactorProperty.Convert(Type target)
        {
            Parent.Parent.EnsureSchemaMigration();

            if (PropertyType != PropertyType.Attribute)
                throw new NotSupportedException("Only primitive properties can have their return type converted.");

            Type from = this.SystemReturnType;
            Type to = target;

            if (from == to)
                throw new NotSupportedException("The property is already of this type.");

            Type fromDb = Transaction.Current.GetStoredType(from);
            Type toDb = Transaction.Current.GetStoredType(from);

            string chkScript, convScript;
            (_, _, chkScript, convScript) = specificConvertTabel.FirstOrDefault(item => item.fromType == from && (item.toType == null || item.toType == to));
            if (chkScript == null || convScript == null)
                (_, _, chkScript, convScript) = genericConvertTabel.FirstOrDefault(item => item.fromType == fromDb && (item.toType == null || item.toType == toDb));

            if (chkScript == null || convScript == null || chkScript == NOT_SUPPORTED || convScript == NOT_SUPPORTED)
                throw new NotSupportedException($"A refactor conversion from '{from.Name}' to '{to.Name}' is not supported.");
                
            if (chkScript != NO_SCRIPT && convScript != NO_SCRIPT)
            {
                foreach (var entity in Parent.GetConcreteClasses())
                {
                    Parser.ExecuteBatched<Templates.Convert>(delegate (Templates.Convert template)
                    {
                        template.Entity = entity;
                        template.Property = this;
                        template.WhereScript = chkScript;
                        template.AssignScript = convScript;
                    });
                }
            }

            // StaticData
            Parent.DynamicEntityPropertyConverted(this, target);

            this.SystemReturnType = to;
        }
        private const string NOT_SUPPORTED = nameof(NOT_SUPPORTED);
        private const string NO_SCRIPT     = nameof(NO_SCRIPT);
        private const string TO_BOOL       = "ToBoolean({0})";
        private const string TO_LONG       = "ToInt({0})";
        private const string TO_DOUBLE     = "ToFloat({0})";
        private const string TO_STRING     = "ToString({0})";

        private static readonly (Type fromType, Type toType, string typeCheck, string typeConv)[] specificConvertTabel = new[] {
            (typeof(DateTime), typeof(long),  NO_SCRIPT,     NO_SCRIPT),
            (typeof(DateTime), typeof(ulong), NO_SCRIPT,     NO_SCRIPT),
            (typeof(DateTime), null,          NOT_SUPPORTED, NOT_SUPPORTED),
            (typeof(Guid),     null,          NOT_SUPPORTED, NOT_SUPPORTED),
        };
        private static readonly (Type fromType, Type toType, string typeCheck, string typeConv)[] genericConvertTabel = new[] {
            // bool
            (typeof(bool),   typeof(long),   TO_BOOL, "CASE WHEN {0} THEN ToInt(1) ELSE ToInt(0) END"),
            (typeof(bool),   typeof(double), TO_BOOL, "CASE WHEN {0} THEN ToFloat(1) ELSE ToFloat(0) END"),
            (typeof(bool),   typeof(string), TO_BOOL, TO_STRING),

            // long
            (typeof(long),   typeof(bool),   TO_LONG, "ToInt({0}) <> ToInt(0)"),
            (typeof(long),   typeof(double), TO_LONG, TO_DOUBLE),
            (typeof(long),   typeof(string), TO_LONG, TO_STRING),

            // double
            (typeof(double), typeof(bool),   TO_DOUBLE, "ToInt({0}) <> ToInt(0)"),
            (typeof(double), typeof(long),   TO_DOUBLE, TO_LONG),
            (typeof(double), typeof(string), TO_DOUBLE, TO_STRING),

            // string
            (typeof(string), typeof(bool),   TO_STRING, ""),
            (typeof(string), typeof(long),   TO_STRING, TO_LONG),
            (typeof(string), typeof(double), TO_STRING, TO_DOUBLE),
        };

        void IRefactorProperty.Deprecate()
        {
            Parent.Parent.EnsureSchemaMigration();

            if (PropertyType == PropertyType.Attribute)
            {
                RemoveIndexesAndContraints();
                Parent.RemoveFullTextProperty(Name);

                foreach (var entity in Parent.GetConcreteClasses())
                {
                    Parser.ExecuteBatched<RemoveProperty>(delegate (RemoveProperty template)
                    {
                        template.Name = Name;
                        template.ConcreteParent = entity;
                    });
                }
            }
            else
            {
                Relationship.ResetProperty(Direction);
            }
            Parent.Properties.Remove(Name);

            // StaticData
            Parent.DynamicEntityPropertyRemoved(this);
        }

        void IRefactorProperty.SetIndexType(IndexType indexType)
        {
            Parent.Parent.EnsureSchemaMigration();

            if (PropertyType != PropertyType.Attribute)
                throw new NotImplementedException();

            if (IndexType == indexType)
                return;

            IndexType = indexType;
            // commit to db is automatic after any script ran during upgrade...
        }

        
        void IRefactorProperty.Reroute(string pattern, string newPropertyName, bool strict)
        {
            Reroute(pattern, newPropertyName, Relationship.Name, Relationship.Neo4JRelationshipType, strict);
        }
        void IRefactorProperty.Reroute(string pattern, string newPropertyName, string newRelationshipName, string newNeo4jRelationshipType, bool strict)
        {
            Reroute(pattern, newPropertyName, newRelationshipName, newNeo4jRelationshipType ?? newRelationshipName, strict);
        }
        private void Reroute(string pattern, string newPropertyName, string newRelationshipName, string newNeo4jRelationshipType, bool strict)
        {
            Parent.Parent.EnsureSchemaMigration();

            if (Relationship == null)
                throw new NotSupportedException("This operation can only be done on an relationship property");

            if (string.IsNullOrWhiteSpace(pattern))
                throw new FormatException("The pattern cannot be empty.");

            string oldname = Name;

            NodePattern decoded = new NodePattern(Parent.Parent, pattern);
            decoded.Validate(EntityReturnType, strict);

            Entity original = Parent;
            Entity target = decoded.GetToEntity();

            string left = (Direction == DirectionEnum.Out) ? "<-" : "-";
            string right = (Direction == DirectionEnum.In) ? "->" : "-";
            string cypher = $"MATCH (anchor:{Parent.Label.Name}){left}[{Relationship.Neo4JRelationshipType}]{right}{decoded.Compile()} MERGE (anchor){left}[:{newNeo4jRelationshipType}]{right}(to)";

            Parser.Execute(cypher, null);

            Entity inEntity = Relationship.InEntity;
            Property inProperty = Relationship.InProperty;
            Entity outEntity = Relationship.OutEntity;
            Property outProperty = Relationship.OutProperty;
            switch (Direction)
            {
                case DirectionEnum.In:
                    outEntity = target;
                    break;
                case DirectionEnum.Out:
                    inEntity = target;
                    break;
            }

            Relationship.Refactor.Deprecate();

            this.Name = newPropertyName;

            Relationship rel = Parent.Parent.Relations.New(inEntity, outEntity, newRelationshipName, newNeo4jRelationshipType ?? newRelationshipName);
            if (inProperty != null)
                rel.SetInProperty(inProperty.Name, inProperty.PropertyType, inProperty.Nullable);
            if (outProperty != null)
                rel.SetOutProperty(outProperty.Name, outProperty.PropertyType, outProperty.Nullable);

            // StaticData
            Property targetProperty = null;
            switch (Direction)
            {
                case DirectionEnum.In:
                    targetProperty = rel.OutProperty;
                    break;
                case DirectionEnum.Out:
                    targetProperty = rel.InProperty;
                    break;
            }
            original.DynamicEntityPropertyRerouted(oldname, target, targetProperty);
        }

        void IRefactorProperty.ConvertToCollection()
        {
            Parent.Parent.EnsureSchemaMigration();

            PropertyType = PropertyType.Collection;
            Nullable = true;
        }
        void IRefactorProperty.ConvertToLookup(ConvertAlgorithm conversionAlgorithm)
        {
            Parent.Parent.EnsureSchemaMigration();

            PropertyType = PropertyType.Lookup;
            Nullable = true;

            throw new NotImplementedException("Apply the conversion algorithm");
        }

        void IRefactorProperty.MakeNullable()
        {
            Parent.Parent.EnsureSchemaMigration();

            if (PropertyType == PropertyType.Collection)
                throw new NotSupportedException("A collection cannot be made nullable, it already is nullable by default.");

            if (Nullable == true)
                throw new NotSupportedException("The property is already nullable.");

            Nullable = true;
        }
        void IRefactorProperty.MakeMandatory()
        {
            Parent.Parent.EnsureSchemaMigration();

            if (PropertyType == PropertyType.Collection)
                throw new NotSupportedException("A collection cannot be made mandatory.");

            if (Nullable == false)
                throw new NotSupportedException("The property is already mandatory.");

            Nullable = false;

            if (Parser.ShouldExecute)
            {
                string cypher = string.Format("MATCH (n:{0}) WHERE n.{1} IS NULL RETURN count(n) as count", Parent.Label.Name, Name);
                IRecord record = Parser.Execute(cypher, null, false).FirstOrDefault();
                bool hasNullProperty = record["count"].As<long>() > 0;
                if (hasNullProperty)
                    throw new NotSupportedException(string.Format("Some nodes in the database contains null values for {0}.{1}.", Parent.Name, Name));
            }
        }
        void IRefactorProperty.MakeMandatory(object defaultValue)
        {
            Parent.Parent.EnsureSchemaMigration();

            if (Nullable == false)
                throw new NotSupportedException("The property is already mandatory.");

            if (PropertyType == PropertyType.Attribute)
            {
                Nullable = false;
                foreach (var entity in Parent.GetConcreteClasses())
                {
                    Parser.ExecuteBatched<SetDefaultConstantValue>(delegate (SetDefaultConstantValue template)
                    {
                        template.Entity = entity;
                        template.Property = this;
                        template.Value = Transaction.Current.ConvertToStoredType(SystemReturnType, defaultValue);
                    });
                }
            }
            else
            {
                if (PropertyType == PropertyType.Collection)
                    throw new NotSupportedException("A collection cannot be made mandatory.");

                if (defaultValue == null || defaultValue.GetType() != ForeignEntity.Key?.SystemReturnType)
                    throw new ArgumentException("The supplied default value does not match the type of the entity its primary key field.");

                Nullable = false;

                Parser.ExecuteBatched<SetDefaultLookupValue>(delegate (SetDefaultLookupValue template)
                {
                    template.Property = this;
                    template.Value = (string)defaultValue;
                });
            }
        }
        void IRefactorProperty.MakeMandatory(DynamicEntity defaultValue)
        {
            Parent.Parent.EnsureSchemaMigration();

            if (Nullable == false)
                throw new NotSupportedException("The property is already mandatory.");

            if (PropertyType == PropertyType.Collection)
                throw new NotSupportedException("A collection cannot be made mandatory.");

            if (PropertyType == PropertyType.Attribute)
                throw new ArgumentException("The property type does not match the type of the supplied 'defaultValue'.");

            if(defaultValue.GetEntity().Name != ForeignEntity.Name)
                throw new ArgumentException("The supplied default value does not match the entity type of the property.");

            Nullable = false;

            Parser.ExecuteBatched<SetDefaultLookupValue>(delegate (SetDefaultLookupValue template)
            {
                template.Property = this;
                template.Value = (string)defaultValue.GetKey();
            });
        }

        private class NodePattern
        {
            #region Construction

            public NodePattern(DatastoreModel model, string pattern)
            {
                Model = model;

                string next = InterpretPattern(pattern);
                InterpretNode();

                if (EntityName != "")
                    Entity = model.Entities.FirstOrDefault(item => item.Name == EntityName);

                if (!string.IsNullOrWhiteSpace(next))
                    Next = new RelationshipPattern(model, next);
            }
            private string InterpretPattern(string pattern)
            {
                int pOpen = pattern.IndexOf("(");
                int pClose = pattern.IndexOf(")");
                if (pOpen == -1 || pClose == -1 || pClose < pOpen)
                    throw new FormatException("A node was expected (node)");

                Pattern = pattern.Substring(pOpen, pClose - pOpen + 1);

                string rest = pattern.Substring(pClose + 1);
                return rest;
            }
            private void InterpretNode()
            {
                string[] innerPattern = Pattern.TrimStart('(').TrimEnd(')').Split(':');
                if (innerPattern.Length == 0)
                {
                    EntityAlias = "";
                    EntityName = "";
                }
                else if (innerPattern.Length == 1)
                {
                    EntityAlias = "";
                    EntityName = innerPattern[0].Trim();
                }
                else if (innerPattern.Length == 2)
                {
                    EntityAlias = innerPattern[0].Trim().ToLowerInvariant();
                    EntityName = innerPattern[1].Trim();
                }
                else
                    throw new FormatException("The node cannot have multiple aliases");
            }

            #endregion

            #region Properties

            private DatastoreModel Model { get; set; }
            private string Pattern { get; set; }
            public string EntityAlias { get; private set; }
            public string EntityName { get; private set; }
            public Entity Entity { get; private set; }

            public RelationshipPattern Next { get; set; }

            #endregion

            #region Validation

            public void Validate(Entity fromEntity, bool strict)
            {
                ValidateRecursive(fromEntity, strict);

                if (EntityAlias !=  "from")
                    throw new FormatException("The pattern should start with the 'from' entity");

                if (HasAlias("from") != 1 || HasAlias("to") != 1)
                    throw new FormatException("The pattern needs to have 1 'from' and 1 'to' node alias");
            }
            internal void ValidateRecursive(Entity fromEntity, bool strict)
            {
                if (EntityAlias != "" && EntityAlias != "from" && EntityAlias != "to")
                    throw new FormatException("The pattern can only have a 'from' and 'to' node alias");

                if (EntityName == "")
                    throw new FormatException("The node name must be specified");

                if (Entity == null)
                    throw new FormatException($"The node '{EntityName}' does not seem to exist");

                if (EntityAlias == "from" && Entity != fromEntity)
                    throw new FormatException($"The property return type is '{fromEntity.Name}' while the pattern is (from:{EntityName})");

                if (Next != null)
                    Next.ValidateRecursive(fromEntity, this, strict);
            }
            internal int HasAlias(string alias)
            {
                int count = Next?.HasAlias(alias) ?? 0;

                if (EntityAlias == alias)
                    count++;

                return count;
            }

            #endregion

            public string Compile()
            {
                if (Next == null)
                    return $"({EntityAlias}:{Entity.Label.Name})";

                return $"({EntityAlias}:{Entity.Label.Name}){Next.Compile()}";
            }
            public Entity GetToEntity()
            {
                if (EntityAlias == "to")
                    return Entity;

                return Next?.GetToEntity();
            }
        }
        private class RelationshipPattern
        {
            #region Construction

            public RelationshipPattern(DatastoreModel model, string pattern)
            {
                Model = model;

                string next = InterpretPattern(pattern);
                InterpretDirection();
                InterpretRelation();

                if (RelationName != "")
                    Relation = model.Relations.FirstOrDefault(item => item.Name == RelationName);

                if (!string.IsNullOrWhiteSpace(next))
                    Next = new NodePattern(model, next);
            }
            private string InterpretPattern(string pattern)
            {
                int dOpen = pattern.IndexOf("<-[");
                if (dOpen == -1)
                    dOpen = pattern.IndexOf("-[");

                int plus = 3;
                int dClose = pattern.IndexOf("]->");
                if (dClose == -1)
                {
                    plus = 2;
                    dClose = pattern.IndexOf("]-");
                }

                if (dOpen == -1 || dClose == -1 || dClose < dOpen)
                    throw new FormatException("A relationship was expected [relationship]");

                Pattern = pattern.Substring(dOpen, dClose - dOpen + plus);

                string rest = pattern.Substring(dClose + plus);
                return rest;
            }
            private void InterpretDirection()
            {
                RelationDirection = DirectionPattern.Unknown;
                if (Pattern.StartsWith("<"))
                    RelationDirection = DirectionPattern.Left;
                else if (Pattern.EndsWith(">"))
                    RelationDirection = DirectionPattern.Right;
            }
            private void InterpretRelation()
            {
                string[] innerPattern = Pattern.TrimStart('<', '-', '[').TrimEnd('>', '-', ']').Split(':');
                if (innerPattern.Length == 0)
                {
                    RelationAlias = "";
                    RelationName = "";
                }
                else if (innerPattern.Length == 1)
                {
                    RelationAlias = "";
                    RelationName = innerPattern[0].Trim();
                }
                else if (innerPattern.Length == 2)
                {
                    RelationAlias = innerPattern[0].Trim().ToLowerInvariant();
                    RelationName = innerPattern[1].Trim();
                }
                else
                    throw new FormatException("The node cannot have multiple aliases");
            }

            #endregion

            #region Properties

            private DatastoreModel Model { get; set; }
            private string Pattern { get; set; }
            public DirectionPattern RelationDirection { get; private set; }
            public string RelationAlias { get; private set; }
            public string RelationName { get; private set; }
            public Relationship Relation { get; private set; }

            public NodePattern Next { get; set; }

            #endregion

            #region Validation

            internal void ValidateRecursive(Entity fromEntity, NodePattern node, bool strict)
            {
                if (RelationAlias != "")
                    throw new FormatException("A relationship cannot have an alias");

                if (RelationName != "" && Relation == null)
                    throw new FormatException($"The relationship '{RelationName}' does not exist");

                if (Next == null)
                    throw new FormatException($"The pattern cannot end in a relationship");

                if (Relation == null)
                {
                    bool found = false;
                    foreach (Relationship relation in node.Entity.GetRelationshipsRecursive())
                    {
                        if (RelationDirection.Test(relation, node.Entity, Next.Entity, strict))
                            found = true;
                    }

                    if (!found)
                        if (strict)
                            throw new FormatException($"When applying the pattern '({node.EntityName}){(RelationDirection == DirectionPattern.Left ? "<" : "")}-[]-{(RelationDirection == DirectionPattern.Right ? ">" : "")}({Next.EntityName})' it cannot be guarenteed that you will lose information. If you are sure about this pattern, set the 'strict' argument to 'false' to override this warning.");
                        else
                            throw new FormatException($"The relationship '({node.EntityName}){(RelationDirection == DirectionPattern.Left ? "<" : "")}-[]-{(RelationDirection == DirectionPattern.Right ? ">" : "")}({Next.EntityName})' does not exist");
                }
                else
                {
                    if (!RelationDirection.Test(Relation, node.Entity, Next.Entity, strict))
                        if (strict)
                            throw new FormatException($"When applying the pattern '({node.EntityName}){(RelationDirection == DirectionPattern.Left ? "<" : "")}-[{Relation.Name}]-{(RelationDirection == DirectionPattern.Right ? ">" : "")}({Next.EntityName})' it cannot be guarenteed that you will lose information. If you are sure about this pattern, set the 'strict' argument to 'false' to override this warning.");
                        else
                            throw new FormatException($"The relationship '{Relation.Name}' does not attach to the specified node types");
                }

                Next.ValidateRecursive(fromEntity, strict);
            }
            internal int HasAlias(string alias)
            {
                return Next.HasAlias(alias);
            }

            #endregion
            public string Compile()
            {
                return $"{(RelationDirection == DirectionPattern.Left ? "<" : "")}-[{Relation?.Neo4JRelationshipType ?? ""}]-{(RelationDirection == DirectionPattern.Right ? ">" : "")}{Next.Compile()}";
            }
            public Entity GetToEntity()
            {
                return Next.GetToEntity();
            }
        }
        private abstract class DirectionPattern
        {
            public static readonly DirectionPattern Unknown = new DirectionUnknown();
            public static readonly DirectionPattern Left = new DirectionLeft();
            public static readonly DirectionPattern Right = new DirectionRight();

            public abstract bool Test(Relationship relation, Entity left, Entity right, bool strict);

            private sealed class DirectionUnknown : DirectionPattern
            {
                public override bool Test(Relationship relation, Entity left, Entity right, bool strict)
                {
                    if (Left.Test(relation, left, right, strict) || Right.Test(relation, left, right, strict))
                        return true;

                    return false;
                }
            }
            private sealed class DirectionLeft : DirectionPattern
            {
                public override bool Test(Relationship relation, Entity left, Entity right, bool strict)
                {
                    bool leave = right.IsSelfOrSubclassOf(relation.InEntity);
                    bool enter = relation.OutEntity.IsSelfOrSubclassOf(left);

                    if (!strict)
                    {
                        leave = leave || relation.InEntity.IsSelfOrSubclassOf(right);
                        enter = enter || left.IsSelfOrSubclassOf(relation.OutEntity);
                    }

                    return (leave && enter);
                }
            }
            private sealed class DirectionRight : DirectionPattern
            {
                public override bool Test(Relationship relation, Entity left, Entity right, bool strict)
                {
                    bool leave = left.IsSelfOrSubclassOf(relation.InEntity);
                    bool enter = relation.OutEntity.IsSelfOrSubclassOf(right);

                    if (!strict)
                    {
                        leave = leave || relation.InEntity.IsSelfOrSubclassOf(left);
                        enter = enter || right.IsSelfOrSubclassOf(relation.OutEntity);
                    }

                    return (leave && enter);
                }
            }
        }

        // (from:PCSModel)<-[rel:XYZ]-(to:SERVICECATEGORY)

        #endregion

        #region Conditions

        public IPropertyCondition Condition { get { return this; } }

        string IPropertyCondition.Contains(string text)
        {
            return string.Format("{{0}}.{0} CONTAINS '{1}'", Name, text);
        }

        #endregion

        #region Reflection

        public object GetValue(OGM instance, DateTime? moment = null)
        {
            if (PropertyType == PropertyType.Lookup && Relationship.IsTimeDependent)
            {
                if (getValueWithMoment == null)
                {
                    lock (this)
                    {
                        if (getValueWithMoment == null)
                        {
                            string name = string.Concat("Get", Name);
                            MethodInfo method = Parent.RuntimeReturnType.GetMethods().First(item => item.Name == name);
                            getValueWithMoment = DelegateHelper.CreateOpenInstanceDelegate<Func<OGM, DateTime?, object>>(method, DelegateHelper.CreateOptions.Downcasting);
                        }
                    }
                }
                return getValueWithMoment.Invoke(instance, moment ?? DateTime.UtcNow);
            }
            else
            {
                if (getValue == null)
                {
                    lock (this)
                    {
                        if (getValue == null)
                        {
                            MethodInfo method = Parent.RuntimeReturnType.GetProperties().First(item => item.Name == Name).GetGetMethod();
                            getValue = DelegateHelper.CreateOpenInstanceDelegate<Func<OGM, object>>(method, DelegateHelper.CreateOptions.Downcasting);
                        }
                    }
                }
                return getValue.Invoke(instance);
            }
        }
        private Func<OGM, object> getValue = null;
        private Func<OGM, DateTime?, object> getValueWithMoment = null;

        public void SetValue(OGM instance, object value, DateTime? moment = null)
        {
            if (PropertyType == PropertyType.Lookup && Relationship.IsTimeDependent)
            {
                if (setValueWithMoment == null)
                {
                    lock (this)
                    {
                        if (setValueWithMoment == null)
                        {
                            string name = string.Concat("Set", Name);
                            MethodInfo method = Parent.RuntimeReturnType.GetMethods().First(item => item.Name == name);
                            setValueWithMoment = DelegateHelper.CreateOpenInstanceDelegate<Action<OGM, object, DateTime?>>(method, DelegateHelper.CreateOptions.Downcasting);
                        }
                    }
                }
                setValueWithMoment.Invoke(instance, value, moment);
            }
            else
            {
                if (setValue == null)
                {
                    lock (this)
                    {
                        if (setValue == null)
                        {
                            MethodInfo method = Parent.RuntimeReturnType.GetProperties().First(item => item.Name == Name).GetSetMethod(true);
                            if (method == null && IsRowVersion)
                                method = typeof(OGM).GetMethod("SetRowVersion");

                            setValue = DelegateHelper.CreateOpenInstanceDelegate<Action<OGM, object>>(method, DelegateHelper.CreateOptions.Downcasting);
                        }
                    }
                }
                setValue.Invoke(instance, value);
            }
        }
        private Action<OGM, object> setValue = null;
        private Action<OGM, object, DateTime?> setValueWithMoment = null;

        internal void ClearLookup(OGM instance, DateTime? moment = null)
        {
            Type type = instance.GetType();
            Action<OGM, DateTime?> clearLookup;

            if (PropertyType == PropertyType.Lookup)
            {
                if (!clearLookupCache.TryGetValue(type, out clearLookup))
                {
                    lock (this)
                    {
                        if (!clearLookupCache.TryGetValue(type, out clearLookup))
                        {
                            string name = string.Concat("Clear", Name);
                            MethodInfo method = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod).First(item => item.Name == name);
                            clearLookup = DelegateHelper.CreateOpenInstanceDelegate<Action<OGM, DateTime?>>(method, DelegateHelper.CreateOptions.Downcasting);
                            clearLookupCache.Add(type, clearLookup);
                        }
                    }
                }
                clearLookup.Invoke(instance, moment);
            }
        }
        private Dictionary<Type, Action<OGM, DateTime?>> clearLookupCache = new Dictionary<Type, Action<OGM, DateTime?>>();

        #endregion

        #region Event Handlers

        public Core.IPropertyEvents Events { get { return this; } }

        internal bool RaiseOnChange<T>(OGMImpl sender, T previousValue, T assignedValue, DateTime? moment, OperationEnum operation)
        {
            Transaction trans = Transaction.RunningTransaction;
            if (!trans.FireEntityEvents)
                return false;

            OGMImpl otherSender;
            bool canceled = false;

            if (ForeignProperty != null)
            {
                otherSender = previousValue as OGMImpl;

                if (operation == OperationEnum.Set || operation == OperationEnum.Remove)
                    if (RaiseOther(otherSender, ForeignProperty, sender, null, OperationEnum.Remove))
                        canceled = true;

                if (assignedValue != null)
                {
                    otherSender = ForeignProperty.GetValue((OGM)assignedValue, moment) as OGMImpl;
                    if (operation == OperationEnum.Set || operation == OperationEnum.Remove)
                        if (RaiseOther(otherSender, this, assignedValue, null, OperationEnum.Remove))
                            canceled = true;
                }

                if (RaiseMain(sender, previousValue, assignedValue, operation))
                    canceled = true;

                otherSender = assignedValue as OGMImpl;

                if (operation == OperationEnum.Set || operation == OperationEnum.Add)
                    if (RaiseOther(otherSender, ForeignProperty, null, sender, OperationEnum.Add))
                        canceled = true;
            }
            else
            {
                if (RaiseMain(sender, previousValue, assignedValue, operation))
                    canceled = true;
            }

            return canceled;

            bool RaiseMain(OGMImpl target, T oldValue, T newValue, OperationEnum op)
            {
                if (target == null)
                    return false;

                PropertyEventArgs args = PropertyEventArgs.CreateInstance(EventTypeEnum.OnPropertyChange, target, this, oldValue, newValue, moment ?? trans.TransactionDate, op, trans);
                EventHandler<PropertyEventArgs> handler = onChange;
                if ((object)handler != null)
                    handler.Invoke(target, args);

                args.Lock();
                return args.Canceled;
            }
            bool RaiseOther(OGMImpl target, Property property, object oldValue, object newValue, OperationEnum op)
            {
                if (target == null || property == null)
                    return false;

                if (property.PropertyType == PropertyType.Lookup)
                {
                    op = OperationEnum.Set;
                    if (oldValue == null)
                        oldValue = property.GetValue(target, moment);
                }

                PropertyEventArgs args = PropertyEventArgs.CreateInstance(EventTypeEnum.OnPropertyChange, target, property, oldValue, newValue, moment ?? trans.TransactionDate, op, trans);
                EventHandler<PropertyEventArgs> handler = property.onChange;
                if ((object)handler != null)
                    handler.Invoke(target, args);

                args.Lock();
                return args.Canceled;
            }
        }
        bool IPropertyEvents.HasRegisteredChangeHandlers
        {
            get
            {
                return onChange != null || ForeignProperty?.onChange != null;
            }
        }
        private EventHandler<PropertyEventArgs> onChange;
        event EventHandler<PropertyEventArgs> IPropertyEvents.OnChange
        {
            add { onChange += value; }
            remove { onChange -= value; }
        }

        internal Type GetPropertyEventArgsType(Type senderType)
        {
            Type type;
            if (!propertyEventArgsType.TryGetValue(senderType, out type))
            {
                lock (this)
                {
                    if (!propertyEventArgsType.TryGetValue(senderType, out type))
                    {
                        type = typeof(PropertyEventArgs<,>).MakeGenericType(senderType, SystemReturnTypeWithNullability ?? EntityReturnType.RuntimeReturnType);
                        propertyEventArgsType.Add(senderType, type);
                    }
                }
            }
            return type;
        }

        private Dictionary<Type, Type> propertyEventArgsType = new Dictionary<Type, Type>();

        #endregion
    }

    public interface IPropertyCondition
    {
        string Contains(string text);
    }
}
