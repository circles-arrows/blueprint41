using Blueprint41.Neo4j.Refactoring;
using Blueprint41.Neo4j.Refactoring.Templates;
using Templates = Blueprint41.Neo4j.Refactoring.Templates;
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
using System.Diagnostics.CodeAnalysis;

namespace Blueprint41
{
    [DebuggerDisplay("{Parent.Name}.{Name}")]
    public abstract partial class Property : IRefactorProperty, IPropertyCondition, IPropertyEvents
    {
        private protected Property(IEntity parent, PropertyType storage, string name, Entity entityType, bool nullable, IndexType indexType)
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
            Enumeration = null;
        }
        private protected Property(IEntity parent, PropertyType storage, string name, EntityProperty reference)
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
            Enumeration = null;
        }
        private protected Property(IEntity parent, PropertyType storage, string name, Type systemType, bool nullable, IndexType indexType, string[]? enumeration = null)
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
            Enumeration = (enumeration is null || enumeration.Length == 0) ? null : new Enumeration(this, enumeration);
        }
        private protected Property(IEntity parent, PropertyType storage, string name, Type systemType, bool nullable, IndexType indexType, Enumeration enumeration)
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
            Enumeration = enumeration;
            Enumeration.PropertyReference = this;
        }

        #region Properties

        public IEntity Parent { get; protected internal set; }
        public PropertyType PropertyType { get; protected internal set; }
        public string Name { get; private set; }
        public IReadOnlyList<EnumerationValue>? EnumValues
        {
            get
            {
                if (Enumeration is null || Enumeration.Values.Count == 0)
                    return null;

                return Enumeration.Values;
            }
        }
        public Enumeration? Enumeration { get; private set; }
        public Type? SystemReturnType { get; private set; }
        public Type? SystemReturnTypeWithNullability
        {
            get
            {
                if (SystemReturnType is null)
                    return null;

                if (!Nullable)
                    return SystemReturnType;

                if (!SystemReturnType.IsValueType)
                    return SystemReturnType;

                return typeof(Nullable<>).MakeGenericType(SystemReturnType);  //Caching this type?...
            }
        }
        public Entity? EntityReturnType { get; private set; }
        public bool Nullable { get; protected internal set; }
        public IndexType IndexType { get; private set; }
        public bool IsKey { get; internal set; }
        public bool IsNodeType { get; internal set; }
        public bool IsRowVersion { get; internal set; }
        internal bool HideSetter { get; set; }
        public Property? Reference { get; private set; }
        public Guid Guid { get; private set; }

        private string? innerReturnType = null;
        public string InnerReturnType
        {
            get
            {
                if (innerReturnType is null)
                {

                    StringBuilder sb = new StringBuilder();

                    if (PropertyType == PropertyType.Collection || PropertyType == PropertyType.Lookup)
                    {
                        if (Relationship?.IsTimeDependent ?? false)
                            sb.Append("EntityTimeCollection<");
                        else
                            sb.Append("EntityCollection<");
                    }

                    if (SystemReturnType is not null)
                        sb.Append(SystemReturnType.ToCSharp() + (this.Nullable && this.SystemReturnType.IsValueType ? "?" : ""));
                    else if (EntityReturnType is not null)
                        sb.Append(EntityReturnType.ClassName);
                    else
                        sb.Append(Reference!.InnerReturnType);

                    if (PropertyType == PropertyType.Collection || PropertyType == PropertyType.Lookup)
                        sb.Append(">");

                    innerReturnType = sb.ToString();
                }

                return innerReturnType;
            }
        }

        private string? outerReturnType = null;
        public string OuterReturnType
        {
            get
            {
                if (outerReturnType is null)
                {

                    StringBuilder sb = new StringBuilder();

                    if (PropertyType == PropertyType.Collection)
                    {
                        if (Relationship?.IsTimeDependent ?? false)
                            sb.Append("EntityTimeCollection<");
                        else
                            sb.Append("EntityCollection<");
                    }

                    if (SystemReturnType == typeof(string) && EnumValues is not null)
                        sb.Append($"{this.Parent.Name}.{this.Name}Enum" + (this.Nullable ? "?" : ""));
                    else if (SystemReturnType is not null)
                        sb.Append(SystemReturnType.ToCSharp() + (this.Nullable && this.SystemReturnType.IsValueType ? "?" : ""));
                    else if (EntityReturnType is not null)
                        sb.Append(EntityReturnType.ClassName);
                    else
                        sb.Append(Reference!.OuterReturnType);

                    if (PropertyType == PropertyType.Collection)
                        sb.Append(">");

                    outerReturnType = sb.ToString();
                }

                return outerReturnType;
            }
        }

        private string? outerReturnTypeReadOnly = null;
        public string OuterReturnTypeReadOnly
        {
            get
            {
                if (outerReturnTypeReadOnly is null)
                {

                    StringBuilder sb = new StringBuilder();

                    if (PropertyType == PropertyType.Collection)
                    {
                        sb.Append("IEnumerable<");
                    }

                    if (SystemReturnType == typeof(string) && EnumValues is not null)
                        sb.Append($"{this.Parent.Name}.{this.Name}Enum" + (this.Nullable ? "?" : ""));
                    else if (SystemReturnType is not null)
                        sb.Append(SystemReturnType.ToCSharp() + (this.Nullable && this.SystemReturnType.IsValueType ? "?" : ""));
                    else if (EntityReturnType is not null)
                        sb.Append(EntityReturnType.ClassName);
                    else
                        sb.Append(Reference!.OuterReturnTypeReadOnly);

                    if (PropertyType == PropertyType.Collection)
                        sb.Append(">");

                    outerReturnTypeReadOnly = sb.ToString();
                }

                return outerReturnTypeReadOnly;
            }
        }

        public Relationship? Relationship { get; internal set; } = null;
        public DirectionEnum Direction { get; internal set; } = DirectionEnum.None;

        public Entity? ForeignEntity
        {
            get
            {
                if (Relationship is null)
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
        public EntityProperty? ForeignProperty
        {
            get
            {
                if (Relationship is null)
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

        internal void ConvertToAdhocEnum()
        {
            if (Enumeration is null)
                return;

            if (Enumeration.Values.Count == 0)
                Enumeration = null;
            else
                Enumeration = new Enumeration(this, Enumeration.Values.Select(item => item.Name));
        }

        private object? m_MandatoryDefaultValue;

        public object? MandatoryDefaultValue
        {
            get
            {
                if (!Nullable)
                    return m_MandatoryDefaultValue;

                return null;
            }
            set
            {
                m_MandatoryDefaultValue = value;
            }
        }

        #endregion

        #region Refactor Actions

        /// <summary>
        /// Removes the property from the data model, but leaves the current dat in the database.
        /// (Use Refactor.Deprecate() if you also want the data to be deleted)
        /// </summary>
        public void Remove()
        {

            if (!Parent.Properties.Any(item => item.Name == Name))
                throw new NotSupportedException($"Property to remove with name '{Name}' does not exist.");
            // No changes in DB needed for this action!!!
            Parent.Properties.Remove(Name);
        }

        void IRefactorProperty.Rename(string newName)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentNullException(nameof(newName));

            List<string> propertyNames = Parent.GetBaseTypesAndSelf().SelectMany(item => item.Properties.Select(p => p.Name)).ToList();
            if (propertyNames.Any(item => item == newName))
                throw new InvalidOperationException($"There is an existing property with name '{newName}'.");


            Parent.Parent.EnsureSchemaMigration();

            if (PropertyType == PropertyType.Attribute)
            {
                RemoveIndexesAndContraints();

                foreach (var entity in Parent.GetConcreteClasses())
                {
                    Parent.Parent.Templates.RenameProperty(template =>
                    {
                        template.Caller = entity; // ConcreteParent
                        template.From = this;
                        template.To = newName;
                    }).RunBatched();
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
                using (Transaction.Begin(true))
                {
                    Nullable = true;
                    IndexType = IndexType.None;

                    foreach (var entity in Parent.GetSubclassesOrSelf())
                    {
                        ApplyConstraintEntity applyConstraint = Parent.Parent.GetSchema().NewApplyConstraintEntity(entity);
                        foreach (var action in applyConstraint.Actions.Where(c => c.Property == Name))
                        {
                            foreach (string query in action.ToCypher())
                            {
                                Parser.Execute(query, null);
                            }
                        }
                    }
                    Transaction.Commit();
                }
            }
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

            MergeProperty(target, mergeAlgorithm);

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

        protected internal void Reroute(string pattern, string newPropertyName, string newRelationshipName, string newNeo4jRelationshipType, bool strict)
        {
            Parent.Parent.EnsureSchemaMigration();

            if (Relationship is null || EntityReturnType is null)
                throw new NotSupportedException("This operation can only be done on an relationship property");

            if (string.IsNullOrWhiteSpace(pattern))
                throw new FormatException("The pattern cannot be empty.");

            string oldname = Name;

            NodePattern decoded = new NodePattern(Parent.Parent, pattern);
            decoded.Validate(EntityReturnType, strict);

            Entity original = (Entity)Parent;
            Entity? target = decoded.GetToEntity();

            string left = (Direction == DirectionEnum.Out) ? "<-" : "-";
            string right = (Direction == DirectionEnum.In) ? "->" : "-";
            string cypher = $"MATCH (anchor:{original.Label.Name}){left}[{Relationship.Neo4JRelationshipType}]{right}{decoded.Compile()} MERGE (anchor){left}[:{newNeo4jRelationshipType}]{right}(to)";

            Parser.Execute(cypher, null);

            Entity? inEntity = Relationship.InEntity;
            Property? inProperty = Relationship.InProperty;
            Entity? outEntity = Relationship.OutEntity;
            Property? outProperty = Relationship.OutProperty;
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
            if (inProperty is not null)
                rel.SetInProperty(inProperty.Name, inProperty.PropertyType, inProperty.Nullable);
            if (outProperty is not null)
                rel.SetOutProperty(outProperty.Name, outProperty.PropertyType, outProperty.Nullable);

            // StaticData
            Property? targetProperty = null;
            switch (Direction)
            {
                case DirectionEnum.In:
                    targetProperty = rel.OutProperty;
                    break;
                case DirectionEnum.Out:
                    targetProperty = rel.InProperty;
                    break;
            }
            original.DynamicEntityPropertyRerouted(oldname, target, targetProperty!);
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

            if (mergeAlgorithm == MergeAlgorithm.ThrowOnConflict)
            {
                foreach (var entity in Parent.GetConcreteClasses())
                {
                    long conflicts = Parent.Parent.Templates.MergeProperty(template =>
                    {
                        template.From = this;
                        template.To = target;
                        template.Caller = entity; // ConcreteParent
                        template.MergeAlgorithm = mergeAlgorithm;
                    }).Run();

                    if (conflicts > 0)
                        throw new InvalidOperationException($"MergeProperty detected {conflicts} conflicts.");
                }
                mergeAlgorithm = MergeAlgorithm.NotApplicable;
            }

            foreach (var entity in Parent.GetConcreteClasses())
            {
                Parent.Parent.Templates.MergeProperty(template =>
                {
                    template.From = this;
                    template.To = target;
                    template.Caller = entity; // ConcreteParent
                    template.MergeAlgorithm = mergeAlgorithm;
                }).RunBatched();
            }
        }

        void IRefactorProperty.ToCompressedString(int batchSize)
        {
            Parent.Parent.EnsureSchemaMigration();

            if (PropertyType != PropertyType.Attribute || SystemReturnType != typeof(string))
                throw new NotSupportedException("Only string properties can be converted to compressed string.");

            if (Parser.ShouldExecute)
            {

                foreach (IEntity iEntity in Parent.GetConcreteClasses())
                {
                    string cypherRead, cypherWrite;
                    List<object> list;

                    if (iEntity is Entity entity)
                    {
                        cypherRead = $"""
                            MATCH (node:{entity.Label.Name})
                            WHERE [x IN node.`{Name}` | x] <> node.`{Name}`
                            RETURN DISTINCT node.`{Name}` as Text
                            LIMIT {batchSize}
                            """;
                        cypherWrite = $"""
                            UNWIND $Batch as map
                            MATCH (node:{entity.Label.Name})
                            WHERE node.`{Name}` = map['Text']
                            SET node.`{Name}` = map['Blob']
                            """;
                    }
                    else if (iEntity is Relationship relationship)
                    {
                        cypherRead = $"""
                            MATCH (:{relationship.InEntity.Label.Name})-[rel:{relationship.Neo4JRelationshipType}]->(:{relationship.OutEntity.Label.Name})
                            WHERE [x IN rel.`{Name}` | x] <> rel.`{Name}`
                            RETURN DISTINCT rel.`{Name}` as Text
                            LIMIT {batchSize}
                            """;
                        cypherWrite = $"""
                            UNWIND $Batch as map
                            MATCH (:{relationship.InEntity.Label.Name})-[rel:{relationship.Neo4JRelationshipType}]->(:{relationship.OutEntity.Label.Name})
                            WHERE rel.`{Name}` = map['Text']
                            SET rel.`{Name}` = map['Blob']
                            """;
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }

                    do
                    {
                        list = new List<object>();

                        Parser.Execute(cypherRead, null, true, delegate (RawResult result)
                        {
                            foreach (RawRecord item in result)
                            {
                                string text = item["Text"].As<string>();

                                Dictionary<string, object?> map = new Dictionary<string, object?>();
                                map.Add("Text", text);
                                map.Add("Blob", (byte[]?)(CompressedString?)text);
                                list.Add(map);
                            }
                        });

                        if (list.Count > 0)
                        {
                            Dictionary<string, object?> batch = new Dictionary<string, object?>();
                            batch.Add("Batch", list);

                            Parser.Execute(cypherWrite, batch, true);
                        }
                    }
                    while (list.Count > 0);
                }
            }

            // StaticData
            Parent.DynamicEntityPropertyConverted(this, typeof(CompressedString));

            this.SystemReturnType = typeof(CompressedString);
        }
        void IRefactorProperty.Convert(Type target, bool skipConvertionLogic)
        {
            Parent.Parent.EnsureSchemaMigration();

            if (PropertyType != PropertyType.Attribute || SystemReturnType is null)
                throw new NotSupportedException("Only primitive properties can have their return type converted.");

            Type from = SystemReturnType;
            Type to = target;

            if (from == to)
                throw new NotSupportedException("The property is already of this type.");

            if (!skipConvertionLogic)
            {
                Type? fromDb = Parent.Parent.PersistenceProvider.GetStoredType(from);
                Type? toDb = Parent.Parent.PersistenceProvider.GetStoredType(to);

                string chkScript, convScript;
                (_, _, chkScript, convScript) = specificConvertTabel.FirstOrDefault(item => item.fromType == from && (item.toType is null || item.toType == to));
                if (chkScript is null || convScript is null)
                    (_, _, chkScript, convScript) = genericConvertTabel.FirstOrDefault(item => item.fromType == fromDb && (item.toType is null || item.toType == toDb));

                if (chkScript is null || string.IsNullOrEmpty(convScript) || chkScript == NOT_SUPPORTED || convScript == NOT_SUPPORTED)
                    throw new NotSupportedException($"A refactor conversion from '{from.Name}' to '{to.Name}' is not supported.");

                if (chkScript != NO_SCRIPT && convScript != NO_SCRIPT)
                {
                    foreach (var entity in Parent.GetConcreteClasses())
                    {
                        Parent.Parent.Templates.Convert(template =>
                        {
                            template.Caller = entity;
                            template.Property = this;
                            template.WhereScript = chkScript;
                            template.AssignScript = convScript;
                        }).RunBatched();
                    }
                }
            }

            // StaticData
            Parent.DynamicEntityPropertyConverted(this, target);

            this.SystemReturnType = to;
        }
        private const string NOT_SUPPORTED = nameof(NOT_SUPPORTED);
        private const string NO_SCRIPT     = nameof(NO_SCRIPT);
        private const string TO_BOOL       = "toBoolean({0})";
        private const string TO_LONG       = "toInteger({0})";
        private const string TO_DOUBLE     = "toFloat({0})";
        private const string TO_STRING     = "toString({0})";

        private static readonly (Type fromType, Type? toType, string typeCheck, string typeConv)[] specificConvertTabel = new[] {
            (typeof(DateTime), typeof(long),  NO_SCRIPT,     NO_SCRIPT),
            (typeof(DateTime), typeof(ulong), NO_SCRIPT,     NO_SCRIPT),
            (typeof(DateTime), null,          NOT_SUPPORTED, NOT_SUPPORTED),
            (typeof(Guid),     null,          NOT_SUPPORTED, NOT_SUPPORTED),
        };
        private static readonly (Type fromType, Type toType, string typeCheck, string typeConv)[] genericConvertTabel = new[] {
            // bool
            (typeof(bool),   typeof(long),   TO_BOOL, "CASE WHEN {0} THEN toInteger(1) ELSE toInteger(0) END"),
            (typeof(bool),   typeof(double), TO_BOOL, "CASE WHEN {0} THEN toFloat(1) ELSE toFloat(0) END"),
            (typeof(bool),   typeof(string), TO_BOOL, TO_STRING),

            // long
            (typeof(long),   typeof(bool),   TO_LONG, "toInteger({0}) <> toInteger(0)"),
            (typeof(long),   typeof(double), TO_LONG, TO_DOUBLE),
            (typeof(long),   typeof(string), TO_LONG, TO_STRING),

            // double
            (typeof(double), typeof(bool),   TO_DOUBLE, "toInteger({0}) <> toInteger(0)"),
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

            if (PropertyType == PropertyType.Attribute || Relationship is null)
            {
                RemoveIndexesAndContraints();
                Parent.RemoveFullTextProperty(Name);

                foreach (var entity in Parent.GetConcreteClasses())
                {
                    Parent.Parent.Templates.RemoveProperty(template =>
                    {
                        template.Name = Name;
                        template.Caller = entity; // ConcreteParent
                    }).RunBatched();
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
                if (Parent is Entity entity)
                {
                    string cypher = $"MATCH (n:{entity.Label.Name}) WHERE n.{Name} IS NULL RETURN count(n) as count";
                    RawRecord record = Parser.Execute(cypher, null, false).FirstOrDefault();
                    bool hasNullProperty = record["count"].As<long>() > 0;
                    if (hasNullProperty)
                        throw new NotSupportedException(string.Format("Some nodes in the database contains null values for {0}.{1}.", entity.Name, Name));
                }
                else if (Parent is Relationship relationship)
                {
                    string cypher = $"MATCH (:{relationship.InEntity.Label.Name})-[r:{relationship.Neo4JRelationshipType}]->(:{relationship.OutEntity.Label.Name}) WHERE r.{Name} IS NULL RETURN count(r) as count";
                    RawRecord record = Parser.Execute(cypher, null, false).FirstOrDefault();
                    bool hasNullProperty = record["count"].As<long>() > 0;
                    if (hasNullProperty)
                        throw new NotSupportedException(string.Format("Some nodes in the database contains null values for {0}.{1}.", relationship.Name, Name));
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }
        void IRefactorProperty.MakeMandatory(object defaultValue)
        {
            Parent.Parent.EnsureSchemaMigration();

            if (Nullable == false)
                throw new NotSupportedException("The property is already mandatory.");

            if (PropertyType == PropertyType.Attribute && !(SystemReturnType is null))
            {
                Nullable = false;
                foreach (var entity in Parent.GetConcreteClasses())
                {
                    Parent.Parent.Templates.SetDefaultConstantValue(template =>
                    {
                        template.Caller = entity;
                        template.Property = this;
                        template.Value = Parent.Parent.PersistenceProvider.ConvertToStoredType(SystemReturnType, defaultValue);
                    }).RunBatched();

                    MandatoryDefaultValue = defaultValue;
                }
            }
            else
            {
                if (PropertyType == PropertyType.Collection || ForeignEntity is null)
                    throw new NotSupportedException("A collection cannot be made mandatory.");

                if (defaultValue is null || defaultValue.GetType() != ForeignEntity.Key?.SystemReturnType)
                    throw new ArgumentException("The supplied default value does not match the type of the entity its primary key field.");

                Nullable = false;

                Parent.Parent.Templates.SetDefaultLookupValue(template =>
                {
                    template.Caller = Parent;
                    template.Property = this;
                    template.Value = (string)defaultValue;
                }).RunBatched();

                MandatoryDefaultValue = defaultValue;
            }
        }
        void IRefactorProperty.SetDefaultValue(object defaultValue)
        {
            Parent.Parent.EnsureSchemaMigration();

            if (PropertyType == PropertyType.Attribute && !(SystemReturnType is null))
            {
                foreach (var entity in Parent.GetConcreteClasses())
                {
                    Parent.Parent.Templates.SetDefaultConstantValue(template =>
                    {
                        template.Caller = entity;
                        template.Property = this;
                        template.Value = Parent.Parent.PersistenceProvider.ConvertToStoredType(SystemReturnType, defaultValue);
                    }).RunBatched();
                }
            }
            else
            {
                if (PropertyType == PropertyType.Collection || ForeignEntity is null)
                    throw new NotSupportedException("A collection cannot have default value.");

                if (defaultValue is null || defaultValue.GetType() != ForeignEntity.Key?.SystemReturnType)
                    throw new ArgumentException("The supplied default value does not match the type of the entity its primary key field.");

                Parent.Parent.Templates.SetDefaultLookupValue(template =>
                {
                    template.Property = this;
                    template.Value = (string)defaultValue;
                }).RunBatched();
            }
        }

        private class NodePattern
        {
            #region Construction

            public NodePattern(DatastoreModel model, string pattern)
            {
                Model = model;

                int pOpen = pattern.IndexOf("(");
                int pClose = pattern.IndexOf(")");
                if (pOpen == -1 || pClose == -1 || pClose < pOpen)
                    throw new FormatException("A node was expected (node)");

                Pattern = pattern.Substring(pOpen, pClose - pOpen + 1);

                string next = pattern.Substring(pClose + 1);

                string[] innerPattern = Pattern.TrimStart(new[] { '(' }).TrimEnd(new[] { ')' }).Split(new[] { ':' });
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

                Entity = model.Entities.FirstOrDefault(item => item.Name == EntityName);

                if (Entity is null)
                    throw new FormatException($"The node '{EntityName}' does not seem to exist");

                if (!string.IsNullOrWhiteSpace(next))
                    Next = new RelationshipPattern(model, next);
            }

            #endregion

            #region Properties

            private DatastoreModel Model { get; set; }
            private string Pattern { get; set; }
            public string EntityAlias { get; private set; }
            public string EntityName { get; private set; }
            public Entity Entity { get; private set; }

            public RelationshipPattern? Next { get; set; }

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

                 if (EntityAlias == "from" && Entity != fromEntity)
                    throw new FormatException($"The property return type is '{fromEntity.Name}' while the pattern is (from:{EntityName})");

                if (Next is not null)
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
                if (Entity is null)
                {
                    if (Next is null)
                        return $"({EntityAlias})";

                    return $"({EntityAlias})";
                }
                else
                {
                    if (Next is null)
                        return $"({EntityAlias}:{Entity.Label.Name})";

                    return $"({EntityAlias}:{Entity.Label.Name}){Next.Compile()}";
                }
            }
            public Entity GetToEntity()
            {
                if (EntityAlias == "to")
                    return Entity;

                return Next?.GetToEntity() ?? throw new FormatException("The pattern must have a 'to' node specified.");
            }
        }
        private class RelationshipPattern
        {
            #region Construction

            public RelationshipPattern(DatastoreModel model, string pattern)
            {
                Model = model;

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

                string next = pattern.Substring(dClose + plus);

                RelationDirection = DirectionPattern.Unknown;
                if (Pattern.StartsWith("<"))
                    RelationDirection = DirectionPattern.Left;
                else if (Pattern.EndsWith(">"))
                    RelationDirection = DirectionPattern.Right;

                string[] innerPattern = Pattern.TrimStart(new[] { '<', '-', '[' }).TrimEnd(new[] { '>', '-', ']' }).Split(new[] { ':' });
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

                Relation = model.Relations.FirstOrDefault(item => item.Name == RelationName);

                if (!string.IsNullOrWhiteSpace(next))
                    Next = new NodePattern(model, next);
            }

            #endregion

            #region Properties

            private DatastoreModel Model { get; set; }
            private string Pattern { get; set; }
            public DirectionPattern RelationDirection { get; private set; }
            public string RelationAlias { get; private set; }
            public string RelationName { get; private set; }
            public Relationship? Relation { get; private set; }

            public NodePattern? Next { get; set; }

            #endregion

            #region Validation

            internal void ValidateRecursive(Entity fromEntity, NodePattern node, bool strict)
            {
                if (RelationAlias != "")
                    throw new FormatException("A relationship cannot have an alias");

                if (Next is null)
                    throw new FormatException($"The pattern cannot end in a relationship");

                if (Relation is null)
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
                return Next?.HasAlias(alias) ?? 0;
            }

            #endregion
            public string Compile()
            {
                if (Next is null)
                    throw new FormatException($"The pattern cannot end in a relationship");

                return $"{(RelationDirection == DirectionPattern.Left ? "<" : "")}-[{Relation?.Neo4JRelationshipType ?? ""}]-{(RelationDirection == DirectionPattern.Right ? ">" : "")}{Next.Compile()}";
            }
            public Entity GetToEntity()
            {
                if (Next is null)
                    throw new FormatException($"The pattern cannot end in a relationship");

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
            if (Parent is Entity entity)
            {
                if (PropertyType == PropertyType.Lookup && (Relationship?.IsTimeDependent ?? false))
                {
                    if (getValueWithMoment is null)
                    {
                        lock (this)
                        {
                            if (getValueWithMoment is null)
                            {
                                string name = string.Concat("Get", Name);
                                MethodInfo? method = entity.RuntimeReturnType!.GetMethods().FirstOrDefault(item => item.Name == name);
                                if (method is null)
                                    throw new NotSupportedException("No get accessor exists");

                                getValueWithMoment = DelegateHelper.CreateOpenInstanceDelegate<Func<OGM, DateTime?, object>>(method, DelegateHelper.CreateOptions.Downcasting);
                            }
                        }
                    }
                    return getValueWithMoment.Invoke(instance, moment ?? DateTime.UtcNow);
                }
                else
                {
                    if (getValue is null)
                    {
                        lock (this)
                        {
                            if (getValue is null)
                            {
                                MethodInfo? method = entity.RuntimeReturnType!.GetProperties().FirstOrDefault(item => item.Name == Name)?.GetGetMethod();
                                if (method is null)
                                    throw new NotSupportedException("No get accessor exists");

                                getValue = DelegateHelper.CreateOpenInstanceDelegate<Func<OGM, object>>(method, DelegateHelper.CreateOptions.Downcasting);
                            }
                        }
                    }
                    return getValue.Invoke(instance);
                }
            }
            else
            {
                throw new NotSupportedException("There is no OGM mapping generated for relationships or their properties.");
            }
        }
        private Func<OGM, object>? getValue = null;
        private Func<OGM, DateTime?, object>? getValueWithMoment = null;

        public void SetValue(OGM instance, object? value, DateTime? moment = null)
        {
            if (Parent is Entity entity)
            {
                if (PropertyType == PropertyType.Lookup && (Relationship?.IsTimeDependent ?? false))
                {
                    if (setValueWithMoment is null)
                    {
                        lock (this)
                        {
                            if (setValueWithMoment is null)
                            {
                                string name = string.Concat("Set", Name);
                                MethodInfo? method = entity.RuntimeReturnType!.GetMethods().FirstOrDefault(item => item.Name == name);
                                if (method is null)
                                    throw new NotSupportedException("No set accessor exists");

                                setValueWithMoment = DelegateHelper.CreateOpenInstanceDelegate<Action<OGM, object?, DateTime?>>(method, DelegateHelper.CreateOptions.Downcasting);
                            }
                        }
                    }
                    setValueWithMoment.Invoke(instance, value, moment);
                }
                else
                {
                    if (setValue is null)
                    {
                        lock (this)
                        {
                            if (setValue is null)
                            {
                                MethodInfo? method = entity.RuntimeReturnType!.GetProperties().First(item => item.Name == Name).GetSetMethod(true);
                                if (method is null)
                                {
                                    if (IsRowVersion)
                                        method = typeof(OGM).GetMethod("SetRowVersion");

                                    if (method is null)
                                        throw new NotSupportedException("No set accessor exists");
                                }
                                setValue = DelegateHelper.CreateOpenInstanceDelegate<Action<OGM, object?>>(method, DelegateHelper.CreateOptions.Downcasting);
                            }
                        }
                    }
                    setValue.Invoke(instance, value);
                }
            }
            else
            {
                throw new NotSupportedException("There is no OGM mapping generated for relationships or their properties.");
            }
        }
        private Action<OGM, object?>? setValue = null;
        private Action<OGM, object?, DateTime?>? setValueWithMoment = null;

        internal void ClearLookup(OGM instance, DateTime? moment = null)
        {
            Type type = instance.GetType();
            Action<OGM, DateTime?>? clearLookup;

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

        internal bool RaiseOnChange<T>(OGMImpl sender, [AllowNull] T previousValue, [AllowNull] T assignedValue, DateTime? moment, OperationEnum operation)
        {
            Transaction trans = Transaction.RunningTransaction;
            if (!trans.FireEntityEvents)
                return false;

            OGMImpl? otherSender;
            bool canceled = false;

            if (ForeignProperty is not null)
            {
                otherSender = previousValue as OGMImpl;

                if (operation == OperationEnum.Set || operation == OperationEnum.Remove)
                    if (RaiseOther(otherSender, ForeignProperty, sender, null, OperationEnum.Remove))
                        canceled = true;

                if (assignedValue is not null)
                {
                    otherSender = ForeignProperty.GetValue((OGM)assignedValue, moment) as OGMImpl;
                    if (operation == OperationEnum.Set || operation == OperationEnum.Remove)
                        if (RaiseOther(otherSender, this, assignedValue, null, OperationEnum.Remove))
                            canceled = true;
                }

                if (RaiseMain(sender, previousValue!, assignedValue!, operation))
                    canceled = true;

                otherSender = assignedValue as OGMImpl;

                if (operation == OperationEnum.Set || operation == OperationEnum.Add)
                    if (RaiseOther(otherSender, ForeignProperty, null, sender, OperationEnum.Add))
                        canceled = true;
            }
            else
            {
                if (RaiseMain(sender, previousValue!, assignedValue!, operation))
                    canceled = true;
            }

            return canceled;

            bool RaiseMain(OGMImpl target, T oldValue, T newValue, OperationEnum op)
            {
                if (target is null)
                    return false;

                PropertyEventArgs args = PropertyEventArgs.CreateInstance(EventTypeEnum.OnPropertyChange, target, this, oldValue, newValue, moment ?? trans.TransactionDate, op, trans);
                onChange?.Invoke(target, args);

                args.Lock();
                return args.Canceled;
            }
            bool RaiseOther(OGMImpl? target, Property? property, object? oldValue, object? newValue, OperationEnum op)
            {
                if (target is null || property is null)
                    return false;

                if (property.PropertyType == PropertyType.Lookup)
                {
                    op = OperationEnum.Set;
                    if (oldValue is null)
                        oldValue = property.GetValue(target, moment);
                }

                PropertyEventArgs args = PropertyEventArgs.CreateInstance(EventTypeEnum.OnPropertyChange, target, property, oldValue, newValue, moment ?? trans.TransactionDate, op, trans);
                property.onChange?.Invoke(target, args);

                args.Lock();
                return args.Canceled;
            }
        }
        bool IPropertyEvents.HasRegisteredChangeHandlers
        {
            get
            {
                return onChange is not null || ForeignProperty?.onChange is not null;
            }
        }
        private EventHandler<PropertyEventArgs>? onChange;
        event EventHandler<PropertyEventArgs> IPropertyEvents.OnChange
        {
            add { onChange += value; }
            remove { onChange -= value; }
        }

        internal Type GetPropertyEventArgsType(Type senderType)
        {
            Type type;
            if (!propertyEventArgsType.TryGetValue(senderType.Name, out type))
            {
                lock (this)
                {
                    if (!propertyEventArgsType.TryGetValue(senderType.Name, out type))
                    {
                        type = typeof(PropertyEventArgs<,>).MakeGenericType(senderType, SystemReturnTypeWithNullability ?? EntityReturnType!.RuntimeReturnType!);
                        propertyEventArgsType.Add(senderType.Name, type);
                    }
                }
            }
            return type;
        }

        private Dictionary<string, Type> propertyEventArgsType = new Dictionary<string, Type>();

        #endregion
    }

    public class EntityProperty : Property, IRefactorEntityProperty
    {
        public IRefactorEntityProperty Refactor { get { return this; } }

        #region Refactoring Entity Properties

        void IRefactorEntityProperty.Reroute(string pattern, string newPropertyName, bool strict)
        {
            if (Relationship is null || EntityReturnType is null)
                throw new NotSupportedException("This operation can only be done on an relationship property");

            Reroute(pattern, newPropertyName, Relationship.Name, Relationship.Neo4JRelationshipType, strict);
        }
        void IRefactorEntityProperty.Reroute(string pattern, string newPropertyName, string newRelationshipName, string newNeo4jRelationshipType, bool strict)
        {
            Reroute(pattern, newPropertyName, newRelationshipName, newNeo4jRelationshipType ?? newRelationshipName, strict);
        }

        void IRefactorEntityProperty.Move(Entity target)
        {
            Parent.Parent.EnsureSchemaMigration();

            if (PropertyType != PropertyType.Attribute)
                throw new NotSupportedException("Consider using the refactor action 'SetInEntity' or 'SetOutEntity' on the relationship.");

            if (Parent.IsSubsclassOf(target) && !this.Nullable)
                throw new ArgumentException("If we move the property to a base type, it should be checked the property is at least nullable.");

            if (!Parent.IsSubsclassOf(target) && !target.IsSubsclassOf(Parent))
                throw new ArgumentException(string.Format("Target {0} is not a base-type or sub-type of {1}", target.Name, Parent.Name), "baseType");

            if (target.IsSubsclassOf(Parent))
            {
                foreach (var subClass in Parent.GetSubclasses())
                {
                    if (subClass == target)
                        continue;

                    subClass.Properties.Remove(Name);
                }
            }

            Parent.Properties.Remove(Name);
            target.Properties.Add(Name, this);
            base.Parent = target;
        }
        void IRefactorEntityProperty.MoveToSubClasses()
        {
            Parent.Parent.EnsureSchemaMigration();

            if (PropertyType != PropertyType.Attribute)
                throw new NotSupportedException("Consider using the refactor action 'SetInEntity' or 'SetOutEntity' on the relationship.");

            Parent.Properties.Remove(Name);
            foreach (var subClass in Parent.GetSubclasses())
            {
                subClass.Properties.Add(Name, this);
                base.Parent = subClass;
            }
        }

        void IRefactorEntityProperty.ConvertToCollection()
        {
            Parent.Parent.EnsureSchemaMigration();

            PropertyType = PropertyType.Collection;
            Nullable = true;
        }

        void IRefactorEntityProperty.ConvertToCollection(string newName)
        {
            Refactor.ConvertToCollection();
            Refactor.Rename(newName);
        }
        void IRefactorEntityProperty.ConvertToLookup(ConvertAlgorithm conversionAlgorithm)
        {
            Parent.Parent.EnsureSchemaMigration();

            PropertyType = PropertyType.Lookup;
            Nullable = true;

            throw new NotImplementedException("Apply the conversion algorithm");
        }
        void IRefactorEntityProperty.ConvertToLookup(string newName, ConvertAlgorithm conversionAlgorithm)
        {
            Refactor.ConvertToLookup(conversionAlgorithm);
            Refactor.Rename(newName);
        }

        void IRefactorEntityProperty.MakeMandatory(DynamicEntity defaultValue)
        {
            Parent.Parent.EnsureSchemaMigration();

            if (Nullable == false)
                throw new NotSupportedException("The property is already mandatory.");

            if (PropertyType == PropertyType.Collection)
                throw new NotSupportedException("A collection cannot be made mandatory.");

            if (PropertyType == PropertyType.Attribute || ForeignEntity is null)
                throw new ArgumentException("The property type does not match the type of the supplied 'defaultValue'.");

            if (defaultValue.GetEntity().Name != ForeignEntity.Name)
                throw new ArgumentException("The supplied default value does not match the entity type of the property.");

            Nullable = false;

            Parent.Parent.Templates.SetDefaultLookupValue(template =>
            {
                template.Caller = Parent;
                template.Property = this;
                template.Value = (string?)defaultValue.GetKey();
            }).RunBatched();
        }

        #endregion

        internal EntityProperty(Entity parent, PropertyType storage, string name, Entity entityType, bool nullable, IndexType indexType) 
            : base(parent, storage, name, entityType, nullable, indexType)
        {
        }
        internal EntityProperty(Entity parent, PropertyType storage, string name, EntityProperty reference) 
            : base(parent, storage, name, reference)
        {
        }
        internal EntityProperty(Entity parent, PropertyType storage, string name, Type systemType, bool nullable, IndexType indexType, string[]? enumeration = null) 
            : base (parent, storage, name, systemType, nullable, indexType, enumeration)
        {
        }
        internal EntityProperty(Entity parent, PropertyType storage, string name, Type systemType, bool nullable, IndexType indexType, Enumeration enumeration) 
            : base(parent, storage, name, systemType, nullable, indexType, enumeration)
        {
        }

        new public Entity Parent => (Entity)base.Parent;
    }
    public class RelationshipProperty : Property
    {
        public IRefactorProperty Refactor { get { return this; } }

        internal RelationshipProperty(Relationship parent, PropertyType storage, string name, Entity entityType, bool nullable, IndexType indexType)
            : base(parent, storage, name, entityType, nullable, indexType)
        {
        }
        internal RelationshipProperty(Relationship parent, PropertyType storage, string name, EntityProperty reference)
            : base(parent, storage, name, reference)
        {
        }
        internal RelationshipProperty(Relationship parent, PropertyType storage, string name, Type systemType, bool nullable, IndexType indexType, string[]? enumeration = null)
            : base(parent, storage, name, systemType, nullable, indexType, enumeration)
        {
        }
        internal RelationshipProperty(Relationship parent, PropertyType storage, string name, Type systemType, bool nullable, IndexType indexType, Enumeration enumeration)
            : base(parent, storage, name, systemType, nullable, indexType, enumeration)
        {
        }

        new public Relationship Parent => (Relationship)base.Parent;
    }

    public interface IPropertyCondition
    {
        string Contains(string text);
    }
}
