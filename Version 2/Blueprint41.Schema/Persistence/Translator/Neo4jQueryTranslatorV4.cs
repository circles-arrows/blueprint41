using System;
using System.Collections.Generic;
using System.Linq;

namespace Blueprint41.Persistence.Translator
{
    internal class Neo4jQueryTranslatorV4 : QueryTranslator
    {
        internal Neo4jQueryTranslatorV4(DatastoreModel datastoreModel) : base(datastoreModel)
        {
        }

        //#region Compile Query Parts

        //protected internal override void SearchTranslation(q.Query query, CompileState state)
        //{
        //    string search = $"replace(trim(replace(replace(replace({state.Preview(query.SearchWords!.Compile, state)}, 'AND', '\"AND\"'), 'OR', '\"OR\"'), '  ', ' ')), ' ', ' {query.SearchOperator!.ToString().ToUpperInvariant()} ')";
        //    search = string.Format("replace({0}, '{1}', '{2}')", search, "]", @"\\]");
        //    search = string.Format("replace({0}, '{1}', '{2}')", search, "[", @"\\[");
        //    search = string.Format("replace({0}, '{1}', '{2}')", search, "-", $" {query.SearchOperator!.ToString().ToUpperInvariant()} ");
        //    Node node = query.Patterns.First();
        //    AliasResult alias = node.NodeAlias!;
        //    AliasResult? weight = query.Aliases?.FirstOrDefault();
        //    FieldResult[] fields = query.Fields!;

        //    List<string> queries = new List<string>();
        //    foreach (var property in fields)
        //        queries.Add(string.Format("({0}:' + {1} + ')", property.FieldName, search));

        //    state.Text.Append(string.Format(FtiSearch, string.Join(" OR ", queries), state.Preview(alias.Compile, state)));
        //    if ((object?)weight is not null)
        //    {
        //        state.Text.Append(string.Format(FtiWeight, state.Preview(weight.Compile, state)));
        //    }
        //    state.Text.Append(" WHERE (");
        //    Compile(alias, state);
        //    state.Text.Append(":");
        //    state.Text.Append(node.Neo4jLabel);
        //    state.Text.Append(")");
        //}

        //#endregion

        #region Compile Functions

        public override string FnToUpper => "toUpper({base})";
        public override string FnToLower => "toLower({base})";
        public override string FnIgnoreCase => "toLower({0})";
        public override string FnListExtract => "[item in {base} | {0}]";

        #endregion

        #region Full Text Indexes

        public override string FtiSearch => "CALL db.index.fulltext.queryNodes(\"fts\", '{0}') YIELD node AS {1}";
        public override string FtiWeight => ", score AS {0}";
        public override string FtiCreate => "CALL db.index.fulltext.createNodeIndex(\"fts\", [ {0} ], [ {1} ])";
        public override string FtiEntity => "\"{0}\"";
        public override string FtiProperty => "\"{0}\"";
        public override string FtiSeparator => ", ";
        public override string FtiRemove => "CALL db.index.fulltext.drop(\"fts\")";
        public override string FtiList => "SHOW FULLTEXT INDEXES";
        internal override void ApplyFullTextSearchIndexes(IEnumerable<Entity> entities)
        {
            if (HasFullTextSearchIndexes())
            {
                try
                {
                    using (DatastoreModel.PersistenceProvider.NewTransaction(ReadWriteMode.ReadWrite))
                    {
                        Transaction.RunningTransaction.Run(FtiRemove);
                        Transaction.Commit();
                    }
                }
                catch { }
            }

            using (DatastoreModel.PersistenceProvider.NewTransaction(ReadWriteMode.ReadWrite))
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
    }
}