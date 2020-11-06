using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Neo4j.Persistence
{
    internal class Neo4jQueryTranslator : QueryTranslator
    {
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
