using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Neo4j.Schema;
using Blueprint41.Query;

using q = Blueprint41.Query;

namespace Blueprint41.Neo4j.Persistence.v5
{
    internal class Neo4jQueryTranslator : v4.Neo4jQueryTranslator
    {
        internal Neo4jQueryTranslator(PersistenceProvider persistenceProvider) : base(persistenceProvider) { }

        #region

        internal override NodePersistenceProvider GetNodePersistenceProvider() => new v3.Neo4jNodePersistenceProvider(PersistenceProvider);
        internal override RelationshipPersistenceProvider GetRelationshipPersistenceProvider() => new v3.Neo4jRelationshipPersistenceProvider(PersistenceProvider);
        internal override RefactorTemplates GetTemplates() => new RefactorTemplates_v5();
        internal override SchemaInfo GetSchemaInfo(DatastoreModel datastoreModel) => new Schema.v5.SchemaInfo_v5(datastoreModel);

        #endregion

        #region Compile Functions

        public override string FnExists    => "{base} IS NOT NULL";
        public override string FnNotExists => "{base} is NULL";

        #endregion
    }
}
