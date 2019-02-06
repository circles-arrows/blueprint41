using Blueprint41.Gremlin;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.Neo4j.Refactoring.Templates;
using Schema = Blueprint41.Neo4j.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Blueprint41.Gremlin.Persistence;

namespace Blueprint41
{
    /// <summary>
    /// This is a class wherein what type of graph features is supports. Defaults to true
    /// </summary>
    public class GraphFeatures
    {
        private GraphFeatures() { }

        public static readonly GraphFeatures Neo4j = new GraphFeatures()
        {
            PersistenceProviderType = typeof(Neo4JPersistenceProvider),

        }
        .SetTemplateFeatures()
        .SetSchemaFeatures();

        public static readonly GraphFeatures Gremlin = new GraphFeatures()
        {
            Cypher = false,
            GremlinFlavor = GremlinFlavor.Default,
            FunctionalId = false,
            CreateIndex = false,
            CreateUniqueConstraint = false,
            DropExistConstraints = false,
            ApplyConstraintEntity = false,
            RenameEntity = false,
            RenameRelationship = false,
            ToCompressedString = false,
            PersistenceProviderType = typeof(GremlinPersistenceProvider),

        }
        .SetTemplateFeatures()
        .SetSchemaFeatures();

        public static readonly GraphFeatures Cosmos = new GraphFeatures()
        {
            Cypher = false,
            GremlinFlavor = GremlinFlavor.Cosmos,
            FunctionalId = false,
            CopyProperty = false,
            CreateIndex = false,
            CreateUniqueConstraint = false,
            DropExistConstraints = false,
            ApplyConstraintEntity = false,
            MergeProperty = false,
            RenameEntity = false,
            RenameProperty = false,
            RenameRelationship = false,
            ToCompressedString = false,
            PersistenceProviderType = typeof(GremlinPersistenceProvider),

        }
        .SetTemplateFeatures()
        .SetSchemaFeatures();

        //public static readonly GraphFeatures Neptune = new GraphFeatures()
        //{
        //    Cypher = false,
        //    GremlinFlavor = GremlinFlavor.Neptune,
        //    FunctionalId = false,
        //    CreateIndex = false,
        //    CreateUniqueConstraint = false,
        //    DropExistConstraints = false,
        //    ApplyConstraintEntity = false,
        //    RenameEntity = false,
        //    RenameRelationship = false,
        //    PersistenceProviderType = typeof(GremlinPersistenceProvider),
        //}
        //.SetTemplateFeatures()
        //.SetSchemaFeatures();

        private Dictionary<Type, bool> templateFeatures;
        GraphFeatures SetTemplateFeatures()
        {
            templateFeatures = new Dictionary<Type, bool>();

            templateFeatures.Add(typeof(ApplyFunctionalId), FunctionalId);
            templateFeatures.Add(typeof(Neo4j.Refactoring.Templates.Convert), Convert);
            templateFeatures.Add(typeof(CopyProperty), CopyProperty);
            templateFeatures.Add(typeof(CreateIndex), CreateIndex);
            templateFeatures.Add(typeof(CreateUniqueConstraint), CreateUniqueConstraint);
            templateFeatures.Add(typeof(DropExistConstraint), DropExistConstraints);
            templateFeatures.Add(typeof(MergeProperty), MergeProperty);
            templateFeatures.Add(typeof(MergeRelationship), MergeRelationship);
            templateFeatures.Add(typeof(RemoveEntity), RemoveEntity);
            templateFeatures.Add(typeof(RemoveProperty), RemoveProperty);
            templateFeatures.Add(typeof(RemoveRelationship), RemoveRelationship);
            templateFeatures.Add(typeof(RenameEntity), RenameEntity);
            templateFeatures.Add(typeof(RenameProperty), RenameProperty);
            templateFeatures.Add(typeof(RenameRelationship), RenameRelationship);
            templateFeatures.Add(typeof(SetDefaultConstantValue), SetDefaultConstantValue);
            templateFeatures.Add(typeof(SetDefaultLookupValue), SetDefaultLookupValue);
            templateFeatures.Add(typeof(SetLabel), SetLabel);
            templateFeatures.Add(typeof(SetRelationshipPropertyValue), SetRelationshipPropertyValue);

            return this;
        }

        private Dictionary<Type, bool> schemaFeatures;
        GraphFeatures SetSchemaFeatures()
        {
            schemaFeatures = new Dictionary<Type, bool>();
            schemaFeatures.Add(typeof(Schema.ApplyConstraintEntity), ApplyConstraintEntity);

            return this;
        }

        private GremlinFlavor gremlinFlavor;
        public GremlinFlavor GremlinFlavor
        {
            get
            {
                if (Cypher)
                    throw new NotSupportedException("The current graph db does not support this property");

                return gremlinFlavor;
            }
            set { gremlinFlavor = value; }
        }

        public bool Cypher { get; private set; } = true;
        public bool FunctionalId { get; private set; } = true;
        public bool Convert { get; private set; } = true;
        public bool CopyProperty { get; private set; } = true;
        public bool CreateIndex { get; private set; } = true;
        public bool CreateUniqueConstraint { get; private set; } = true;
        public bool DropExistConstraints { get; private set; } = true;
        public bool MergeProperty { get; private set; } = true;
        public bool MergeRelationship { get; private set; } = true;
        public bool RemoveEntity { get; private set; } = true;
        public bool RemoveProperty { get; private set; } = true;
        public bool RemoveRelationship { get; private set; } = true;
        public bool RenameEntity { get; private set; } = true;
        public bool RenameProperty { get; private set; } = true;
        public bool RenameRelationship { get; private set; } = true;
        public bool SetDefaultConstantValue { get; private set; } = true;
        public bool SetDefaultLookupValue { get; private set; } = true;
        public bool SetLabel { get; private set; } = true;
        public bool SetRelationshipPropertyValue { get; private set; } = true;
        public bool ToCompressedString { get; private set; } = true;


        // Schema
        public bool ApplyConstraintEntity { get; private set; } = true;

        internal Type PersistenceProviderType { get; private set; }

        internal bool SupportsFeature<T>(T feature)
        {
            Type featureType = typeof(T);
            return SupportsFeature(featureType);
        }

        internal bool SupportsFeature(Type feature)
        {
            if (templateFeatures.ContainsKey(feature))
                return templateFeatures[feature];

            if (schemaFeatures.ContainsKey(feature))
                return schemaFeatures[feature];

            // Default: all template features are supported
            return true;
        }
    }
}
