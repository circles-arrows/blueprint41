using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.Neo4j.Refactoring.Templates;
using Blueprint41.Response;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Neo4j.Refactoring
{
    internal class Parser
    {
        #region Parser Logic

        //internal static IStatementResult ExecuteSelect(string cypher, Dictionary<string, object> parameters)
        //{
        //    if (!ShouldExecute)
        //        return null;

        //    using (Transaction.Begin(false))
        //    {
        //        if (parameters == null || parameters.Count == 0)
        //            return Neo4jTransaction.Run(cypher);
        //        else
        //            return Neo4jTransaction.Run(cypher, parameters);
        //    }
        //}

        private static IGraphResponse PrivateExecute<T>(Action<T> setup)
            where T : TemplateBase, new()
        {
            if (!ShouldExecute)
                return null;

            T template = new T();

            if (PersistenceProvider.TargetFeatures.SupportsFeature(template) == false)
                return null;

            if (setup != null)
                setup.Invoke(template);

            string cypher = template.TransformText();

            if (template.OutputParameters.Count == 0)
                return Transaction.Run(cypher);
            else
                return Transaction.Run(cypher, template.OutputParameters);
        }

        internal static void Execute<T>(Action<T> setup, bool withTransaction = true)
            where T : TemplateBase, new()
        {
            if (!ShouldExecute)
                return;

            using (Transaction.Begin(withTransaction))
            {
                IGraphResponse result = Parser.PrivateExecute<T>(setup);
                Transaction.Commit();
            }
        }

        internal static IGraphResponse Execute(string cypher, Dictionary<string, object> parameters, bool withTransaction = true)
        {
            if (!ShouldExecute)
                return null;

            IGraphResponse result;

            using (Transaction.Begin(withTransaction))
            {
                if (parameters == null || parameters.Count == 0)
                    result = Transaction.Run(cypher);
                else
                    result = Transaction.Run(cypher, parameters);
                Transaction.Commit();
            }

            return result;
        }

        // TODO: Execute Batches
        internal static void ExecuteBatched<T>(Action<T> setup, bool withTransaction = true)
            where T : TemplateBase, new()
        {
            if (!ShouldExecute)
                return;

            ICounters counters = null;
            do
            {
                using (Transaction.Begin(withTransaction))
                {
                    IGraphResponse result = Parser.PrivateExecute<T>(setup);
                    Transaction.Commit();

                    if (result.Result is IStatementResult statementResult)
                        counters = statementResult.Consume().Counters;
                }
            }
            while (counters != null && counters.ContainsUpdates);
        }

        internal static void ExecuteBatched(string cypher, Dictionary<string, object> parameters, bool withTransaction = true)
        {
            if (!ShouldExecute)
                return;

            ICounters counters = null;
            do
            {
                using (Transaction.Begin(withTransaction))
                {
                    IGraphResponse result;
                    if (parameters == null || parameters.Count == 0)
                        result = Transaction.Run(cypher);
                    else
                        result = Transaction.Run(cypher, parameters);

                    Transaction.Commit();

                    if (result.Result is IStatementResult statementResult)
                        counters = statementResult.Consume().Counters;
                }
            }
            while (counters != null && counters.ContainsUpdates);
        }

        #endregion

        #region Neo4j Access Logic

        public static bool HasScript(DatastoreModel.UpgradeScript script)
        {
            // the HasScriptPrivate method doesn't set hasScript = true
            hasScript = HasScriptPrivate(script);
            return hasScript;
        }
        private static bool HasScriptPrivate(DatastoreModel.UpgradeScript script)
        {
            string query = "MATCH (version:RefactorVersion) RETURN version;";
            var result = Transaction.Run(query);

            IRecord record = result.FirstOrDefault();
            if (record == null)
                return false;

            INode node = record["version"].As<INode>();
            DatastoreModel.UpgradeScript databaseVersion = new DatastoreModel.UpgradeScript(node);

            if (databaseVersion.Major < script.Major)
                return false;

            if (databaseVersion.Major > script.Major)
                return true;

            if (databaseVersion.Minor < script.Minor)
                return false;

            if (databaseVersion.Minor > script.Minor)
                return true;

            if (databaseVersion.Patch < script.Patch)
                return false;

            return true;
        }
        internal static void ForceScript(Action action)
        {
            bool tmp = hasScript;
            hasScript = false;
            try
            {
                action.Invoke();
            }
            finally
            {
                hasScript = tmp;
            }
        }
        public static void CommitScript(DatastoreModel.UpgradeScript script)
        {
            // write version nr
            string create = "MERGE (n:RefactorVersion {node})";

            Dictionary<string, object> node = new Dictionary<string, object>();
            node.Add("Major", script.Major);
            node.Add("Minor", script.Minor);
            node.Add("Patch", script.Patch);
            node.Add("LastRun", Conversion<DateTime, long>.Convert(DateTime.UtcNow));

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("node", node);

            Transaction.Run(create, parameters);
            Transaction.Commit();

            hasScript = true;
        }

        private static bool hasScript = true;
        internal static bool ShouldExecute { get { return !hasScript; } }

        internal static bool ShouldRefreshFunctionalIds()
        {
            bool shouldRefresh = ShouldRefreshFunctionalIdsPrivate();
            hasScript = !shouldRefresh;
            return shouldRefresh;
        }
        private static bool ShouldRefreshFunctionalIdsPrivate()
        {
            string query = "MATCH (version:RefactorVersion) RETURN version.LastRun as LastRun";
            var result = Transaction.Run(query);

            IRecord record = result.FirstOrDefault();
            if (record == null)
                return true;

            DateTime? lastRun = Conversion<long?, DateTime?>.Convert(record["LastRun"].As<long?>());
            if (lastRun == null)
                return true;

            if (DateTime.UtcNow.Subtract(lastRun.Value).TotalHours >= 12)
                return true;

            return false;
        }

        public static void SetLastRun()
        {
            // write version nr
            string query = "MATCH (n:RefactorVersion) SET n.LastRun = {LastRun}";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("LastRun", Conversion<DateTime, long>.Convert(DateTime.UtcNow));

            Transaction.Run(query, parameters);

            hasScript = true;
        }

        #endregion
    }
}
