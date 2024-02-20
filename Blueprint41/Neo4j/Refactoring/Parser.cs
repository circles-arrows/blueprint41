#nullable disable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.Neo4j.Refactoring.Templates;

namespace Blueprint41.Neo4j.Refactoring
{
    internal static class Parser
    {
        #region Parser Logic

        private static RawResult PrivateExecute(string cypher, Dictionary<string, object> parameters)
        {
            IStatementRunner runner = Session.Current as IStatementRunner ?? Transaction.Current ?? throw new InvalidOperationException("Either a Session or an Transaction should be started.");

            if (parameters is null || parameters.Count == 0)
                return runner.Run(cypher);
            else
                return runner.Run(cypher, parameters);
        }

        internal static void Execute(string cypher, Dictionary<string, object> parameters, bool withTransaction = true, Action<RawResult> logic = null)
        {
            if (!ShouldExecute)
                return;

            RawResult result;

            if (withTransaction)
            {
                using (Transaction.Begin(withTransaction))
                {
                    result = PrivateExecute(cypher, parameters);
                    logic?.Invoke(result);
                    Transaction.Commit();
                }
            }
            else
            {
                using (Session.Begin(withTransaction))
                {
                    result = PrivateExecute(cypher, parameters);
                    logic?.Invoke(result);
                }
            }
        }
        internal static void ExecuteBatched(string cypher, Dictionary<string, object> parameters)
        {
            if (!ShouldExecute)
                return;

            RawResultStatistics counters;
            do
            {
                using (Transaction.Begin(true))
                {
                    RawResult result = Parser.PrivateExecute(cypher, parameters);
                    Transaction.Commit();

                    counters = result.Statistics();
                }
            }
            while (counters.ContainsUpdates);
        }

        internal static bool LogToDebugger { get; set; } = true;
        internal static bool LogToConsole { get; set; } = false;

        internal static void Log(string message, params object[] args)
        {
            if (LogToDebugger)
                Debug.WriteLine(message, args);

            if (LogToConsole)
                Console.WriteLine(message, args);
        }

        #endregion

        #region Neo4j Access Logic

        public static bool HasScript(DatastoreModel.UpgradeScript script)
        {
            // the HasScriptPrivate method doesn't set hasScript = true
            hasScript = Transaction.RunningTransaction.PersistenceProviderFactory.Translator.HasScript(script);
            return hasScript; 
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
            Transaction.RunningTransaction.PersistenceProviderFactory.Translator.CommitScript(script);
            hasScript = true;
        }

        private static bool hasScript = true;
        internal static bool ShouldExecute { get { return !hasScript; } }

        internal static bool ShouldRefreshFunctionalIds()
        {
            bool shouldRefresh = Transaction.RunningTransaction.PersistenceProviderFactory.Translator.ShouldRefreshFunctionalIds();
            hasScript = !shouldRefresh;
            return shouldRefresh;
        }


        public static void SetLastRun()
        {
            Transaction.RunningTransaction.PersistenceProviderFactory.Translator.SetLastRun();
            hasScript = true;
        }

        #endregion
    }
}
