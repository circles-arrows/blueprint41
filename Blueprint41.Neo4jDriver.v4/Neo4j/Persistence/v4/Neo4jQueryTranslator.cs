using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Query;
using Neo4j.Driver;

namespace Blueprint41.Neo4j.Persistence
{
    internal class Neo4jQueryTranslator : QueryTranslator
    {
        #region Full Text Indexes

        public override string FtiSearch    => "CALL db.index.fulltext.queryNodes(\"fts\", \"{0}\") YIELD node AS {1}";
        public override string FtiWeight    => ", score AS {0}";
        public override string FtiCreate    => "CALL db.index.fulltext.createNodeIndex(\"fts\", [ {0} ], [ {1} ])";
        public override string FtiEntity    => "\"{0}\"";
        public override string FtiProperty  => "\"{0}\"";
        public override string FtiSeparator => ", ";
        public override string FtiRemove    => "CALL db.index.fulltext.drop(\"fts\")";

        internal override void ApplyFullTextSearchIndexes(IEnumerable<Entity> entities)
        {
            using (Transaction.Begin(true))
            {
                try
                {
                    Transaction.RunningTransaction.Run(FtiRemove);
                    Transaction.Commit();
                }
                catch (ClientException ex)
                {
                    if (!ex.Message.StartsWith("Failed to invoke procedure `db.index.fulltext.drop`: Caused by: java.lang.IllegalArgumentException: No index found with the name"))
                        throw;
                    // there's no procedure to check for full text indexes
                }
            }

            using (Transaction.Begin(true))
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

        internal override void Compile(Parameter parameter, CompileState state)
        {
            if (!state.Parameters.Contains(parameter))
                state.Parameters.Add(parameter);

            if (parameter.HasValue && !state.Values.Contains(parameter))
                state.Values.Add(parameter);

            if (parameter.Name == Parameter.CONSTANT_NAME)
            {
                parameter.Name = $"param{state.paramSeq}";
                state.paramSeq++;
            }
            state.Text.Append("$");
            state.Text.Append(parameter.Name);
        }
        internal override void CommitScript(DatastoreModel.UpgradeScript script)
        {
            // write version nr
            string create = "MERGE (n:RefactorVersion) ON CREATE SET n = $node ON MATCH SET n = $node";

            Dictionary<string, object> node = new Dictionary<string, object>();
            node.Add("Major", script.Major);
            node.Add("Minor", script.Minor);
            node.Add("Patch", script.Patch);
            node.Add("LastRun", Conversion<DateTime, long>.Convert(DateTime.UtcNow));

            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("node", node);

            Transaction.RunningTransaction.Run(create, parameters);
            Transaction.Commit();
        }
        internal override void SetLastRun()
        {
            // write version nr
            string query = "MATCH (n:RefactorVersion) SET n.LastRun = $LastRun";

            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("LastRun", Conversion<DateTime, long>.Convert(DateTime.UtcNow));

            Transaction.RunningTransaction.Run(query, parameters);
        }
    }
}
