using System;
using System.Diagnostics;
using System.Collections.Generic;
using driver = Blueprint41.Persistence;

namespace Blueprint41.Refactoring
{
    internal static class Parser
    {
        #region Parser Logic

        private static driver.ResultCursor PrivateExecute(IStatementRunner runner, string cypher, Dictionary<string, object?>? parameters)
        {
            if (parameters is null || parameters.Count == 0)
                return runner.Run(cypher);
            else
                return runner.Run(cypher, parameters);
        }

        internal static void Execute(DatastoreModel model, string cypher, Dictionary<string, object?>? parameters, bool withTransaction = true, Action<driver.ResultCursor>? logic = null)
        {
            if (!ShouldExecute)
                return;

            if (withTransaction)
            {
                using (IStatementRunner runner = model.PersistenceProvider.NewTransaction(ReadWriteMode.ReadWrite))
                {
                    driver.ResultCursor result = PrivateExecute(runner, cypher, parameters);
                    logic?.Invoke(result);
                    Transaction.Commit();
                }
            }
            else
            {
                using (IStatementRunner runner = model.PersistenceProvider.NewSession(ReadWriteMode.ReadWrite))
                {
                    driver.ResultCursor result = PrivateExecute(runner, cypher, parameters);
                    logic?.Invoke(result);
                }
            }
        }
        internal static void ExecuteBatched(DatastoreModel model, string cypher, Dictionary<string, object?>? parameters)
        {
            if (!ShouldExecute)
                return;

            driver.Counters counters;
            do
            {
                using (IStatementRunner runner = model.PersistenceProvider.NewTransaction(ReadWriteMode.ReadWrite))
                {
                    driver.ResultCursor result = Parser.PrivateExecute(runner, cypher, parameters);
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

        public static bool HasScript(DatastoreModel model, DatastoreModel.UpgradeScript script)
        {
            return WithStatementRunner(model, delegate (IStatementRunner runner)
            {
                // the HasScriptPrivate method doesn't set hasScript = true
                hasScript = runner.PersistenceProvider.Translator.HasScript(script);
                return hasScript;
            });
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
        public static void CommitScript(DatastoreModel model, DatastoreModel.UpgradeScript script)
        {
            WithStatementRunner(model, delegate (IStatementRunner runner)
            {
                runner.PersistenceProvider.Translator.CommitScript(script);
                hasScript = true;
            });
        }

        private static bool hasScript = true;
        internal static bool ShouldExecute { get { return !hasScript; } }

        internal static bool ShouldRefreshFunctionalIds(DatastoreModel model)
        {
            return WithStatementRunner(model, delegate (IStatementRunner runner)
            {
                bool shouldRefresh = runner.PersistenceProvider.Translator.ShouldRefreshFunctionalIds();
                hasScript = !shouldRefresh;
                return shouldRefresh;
            });
        }


        public static void SetLastRun(DatastoreModel model)
        {
            WithStatementRunner(model, delegate (IStatementRunner runner)
            {
                runner.PersistenceProvider.Translator.SetLastRun();
                hasScript = true;
            });
        }

        private static void WithStatementRunner(DatastoreModel model, Action<IStatementRunner> action)
        {
            IStatementRunner? runner = Transaction.Current as IStatementRunner ?? Session.Current;
            if (runner is not null)
            {
                action(runner);
            }
            else
            {
                using (runner = model.PersistenceProvider.NewSession(ReadWriteMode.ReadWrite))
                {
                    action(runner);
                }
            }
        }
        private static T WithStatementRunner<T>(DatastoreModel model, Func<IStatementRunner, T> action)
        {
            IStatementRunner? runner = Transaction.Current as IStatementRunner ?? Session.Current;
            if (runner is not null)
            {
                return action(runner);
            }
            else
            {
                using (runner = model.PersistenceProvider.NewSession(ReadWriteMode.ReadWrite))
                {
                    return action(runner);
                }
            }
        }

        #endregion
    }
}
