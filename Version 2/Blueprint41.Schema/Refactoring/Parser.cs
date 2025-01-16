using System;
using System.Diagnostics;
using System.Collections.Generic;
using driver = Blueprint41.Persistence;

namespace Blueprint41.Refactoring
{
    internal class Parser
    {
        internal Parser(DatastoreModel model)
        {
            Model = model;
        }

        private readonly DatastoreModel Model;

        #region Parser Logic

        private driver.ResultCursor PrivateExecute(IStatementRunner runner, string cypher, Dictionary<string, object?>? parameters)
        {
            if (parameters is null || parameters.Count == 0)
                return runner.Run(cypher);
            else
                return runner.Run(cypher, parameters);
        }

        internal void Execute(string cypher, Dictionary<string, object?>? parameters, bool withTransaction = true, Action<driver.ResultCursor>? logic = null)
        {
            if (!ShouldExecute)
                return;

            if (withTransaction)
            {
                using (IStatementRunner runner = Model.PersistenceProvider.NewTransaction(ReadWriteMode.ReadWrite))
                {
                    driver.ResultCursor result = PrivateExecute(runner, cypher, parameters);
                    logic?.Invoke(result);
                    Transaction.Commit();
                }
            }
            else
            {
                using (IStatementRunner runner = Model.PersistenceProvider.NewSession(ReadWriteMode.ReadWrite))
                {
                    driver.ResultCursor result = PrivateExecute(runner, cypher, parameters);
                    logic?.Invoke(result);
                }
            }
        }
        internal void ExecuteBatched(string cypher, Dictionary<string, object?>? parameters)
        {
            if (!ShouldExecute)
                return;

            driver.Counters counters;
            do
            {
                using (IStatementRunner runner = Model.PersistenceProvider.NewTransaction(ReadWriteMode.ReadWrite))
                {
                    driver.ResultCursor result = PrivateExecute(runner, cypher, parameters);
                    Transaction.Commit();

                    counters = result.Statistics();
                }
            }
            while (counters.ContainsUpdates);
        }

        internal bool LogToDebugger { get; set; } = true;
        internal bool LogToConsole { get; set; } = false;

        internal void Log(string message, params object[] args)
        {
            if (LogToDebugger)
                Debug.WriteLine(message, args);

            if (LogToConsole)
                Console.WriteLine(message, args);
        }

        #endregion

        #region Neo4j Access Logic

        public bool HasScript(DatastoreModel.UpgradeScript script)
        {
            return WithStatementRunner(delegate (IStatementRunner runner)
            {
                // the HasScriptPrivate method doesn't set hasScript = true
                hasScript = runner.PersistenceProvider.Translator.HasScript(script);
                return hasScript;
            });
        }
        internal void ForceScript(Action action)
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
        public void CommitScript(DatastoreModel.UpgradeScript script)
        {
            WithStatementRunner(delegate (IStatementRunner runner)
            {
                runner.PersistenceProvider.Translator.CommitScript(script);
                hasScript = true;
            });
        }

        private bool hasScript = true;
        internal bool ShouldExecute { get { return !hasScript; } }

        internal bool ShouldRefreshFunctionalIds()
        {
            return WithStatementRunner(delegate (IStatementRunner runner)
            {
                bool shouldRefresh = runner.PersistenceProvider.Translator.ShouldRefreshFunctionalIds();
                hasScript = !shouldRefresh;
                return shouldRefresh;
            });
        }


        public void SetLastRun()
        {
            WithStatementRunner(delegate (IStatementRunner runner)
            {
                runner.PersistenceProvider.Translator.SetLastRun();
                hasScript = true;
            });
        }

        private void WithStatementRunner(Action<IStatementRunner> action)
        {
            IStatementRunner? runner = Transaction.Current as IStatementRunner ?? Session.Current;
            if (runner is not null)
            {
                action(runner);
            }
            else
            {
                using (runner = Model.PersistenceProvider.NewSession(ReadWriteMode.ReadWrite))
                {
                    action(runner);
                }
            }
        }
        private T WithStatementRunner<T>(Func<IStatementRunner, T> action)
        {
            IStatementRunner? runner = Transaction.Current as IStatementRunner ?? Session.Current;
            if (runner is not null)
            {
                return action(runner);
            }
            else
            {
                using (runner = Model.PersistenceProvider.NewSession(ReadWriteMode.ReadWrite))
                {
                    return action(runner);
                }
            }
        }

        #endregion
    }
}
