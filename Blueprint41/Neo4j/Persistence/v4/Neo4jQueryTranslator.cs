using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Neo4j.Persistence.Void;
using Blueprint41.Neo4j.Schema;
using Blueprint41.Query;

using q = Blueprint41.Query;

namespace Blueprint41.Neo4j.Persistence.v4
{
    internal class Neo4jQueryTranslator : QueryTranslator
    {
        internal Neo4jQueryTranslator(Neo4jPersistenceProvider persistenceProvider) : base(persistenceProvider) { }

        #region Compile Query Parts

        protected internal override void SearchTranslation(q.Query query, CompileState state)
        {
            string search = $"replace(trim(replace(replace(replace({state.Preview(query.SearchWords!.Compile, state)}, 'AND', '\"AND\"'), 'OR', '\"OR\"'), '  ', ' ')), ' ', ' {query.SearchOperator!.ToString().ToUpperInvariant()} ')";
            search = string.Format("replace({0}, '{1}', '{2}')", search, "]", @"\\]");
            search = string.Format("replace({0}, '{1}', '{2}')", search, "[", @"\\[");
            search = string.Format("replace({0}, '{1}', '{2}')", search, "-", $" {query.SearchOperator!.ToString().ToUpperInvariant()} ");
            Node node = query.Patterns.First();
            AliasResult alias = node.NodeAlias!;
            AliasResult? weight = query.Aliases?.FirstOrDefault();
            FieldResult[] fields = query.Fields!;

            List<string> queries = new List<string>();
            foreach (var property in fields)
                queries.Add(string.Format("({0}:' + {1} + ')", property.FieldName, search));

            state.Text.Append(string.Format(FtiSearch, string.Join(" OR ", queries), state.Preview(alias.Compile, state)));
            if ((object?)weight is not null)
            {
                state.Text.Append(string.Format(FtiWeight, state.Preview(weight.Compile, state)));
            }
            state.Text.Append(" WHERE (");
            Compile(alias, state);
            state.Text.Append(":");
            state.Text.Append(node.Neo4jLabel);
            state.Text.Append(")");
        }

        #endregion

        #region Compile Functions

        public override string FnToUpper     => "toUpper({base})";
        public override string FnToLower     => "toLower({base})";
        public override string FnIgnoreCase  => "toLower({0})";
        public override string FnListExtract => "[item in {base} | {0}]";

        #endregion

        #region Full Text Indexes

        public override string FtiSearch    => "CALL db.index.fulltext.queryNodes(\"fts\", '{0}') YIELD node AS {1}";
        public override string FtiWeight    => ", score AS {0}";
        public override string FtiCreate    => "CALL db.index.fulltext.createNodeIndex(\"fts\", [ {0} ], [ {1} ])";
        public override string FtiEntity    => "\"{0}\"";
        public override string FtiProperty  => "\"{0}\"";
        public override string FtiSeparator => ", ";
        public override string FtiRemove    => "CALL db.index.fulltext.drop(\"fts\")";
        public override string FtiList      => "SHOW FULLTEXT INDEXES";
        internal override void ApplyFullTextSearchIndexes(IEnumerable<Entity> entities)
        {
            if (HasFullTextSearchIndexes())
            {
                try
                {
                    using (Transaction.Begin())
                    {
                        Transaction.RunningTransaction.Run(FtiRemove);
                        Transaction.Commit();
                    }
                }
                catch { }
            }

            using (Transaction.Begin())
            {
                string e = string.Join(
                        FtiSeparator,
                        entities.Where(entity => entity.FullTextIndexProperties.Count > 0).Select(entity =>
                            string.Format(
                                FtiEntity,
                                entity.Label.Name
                            )
                        )
                    );
                string p = string.Join(
                        FtiSeparator,
                        entities.Where(entity => entity.FullTextIndexProperties.Count > 0).SelectMany(entity => entity.FullTextIndexProperties).Select(property =>
                            string.Format(
                                FtiProperty,
                                property.Name
                            )
                        ).Distinct()
                    );
                string query = string.Format(FtiCreate, e, p);

                Transaction.RunningTransaction.Run(query);
                Transaction.Commit();
            }
        }

        #endregion

        #region

        internal override NodePersistenceProvider GetNodePersistenceProvider() => new v3.Neo4jNodePersistenceProvider(PersistenceProvider);
        internal override RelationshipPersistenceProvider GetRelationshipPersistenceProvider() => new v3.Neo4jRelationshipPersistenceProvider(PersistenceProvider);
        internal override RefactorTemplates GetTemplates() => new RefactorTemplates_v4();
        internal override SchemaInfo GetSchemaInfo(DatastoreModel datastoreModel) => new Schema.v4.SchemaInfo_v4(datastoreModel, PersistenceProvider);

        #endregion
    }
}
