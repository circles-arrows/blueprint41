using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

using neo4j = Neo4j.Driver.V1;

using Blueprint41.Core;
using Blueprint41.Log;

namespace Blueprint41.Neo4j.Persistence.Driver.v3
{

    public class Neo4jTransaction : Void.Neo4jTransaction
    {
        internal Neo4jTransaction(Neo4jPersistenceProvider provider, bool readWriteMode, TransactionLogger? logger) : base(readWriteMode, logger)
        {
            Provider = provider;
            Initialize();
        }

        public override RawResult Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (StatementRunner is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

#if DEBUG
            Logger?.Start();
#endif
            neo4j.IStatementResult results = StatementRunner.Run(cypher);
#if DEBUG
            if (Logger is not null)
            {
                results.Peek();
                Logger.Stop(cypher, callerInfo: new List<string>() { memberName, sourceFilePath, sourceLineNumber.ToString() });
            }
#endif

            return new Neo4jRawResult(results);
        }
        public override RawResult Run(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (StatementRunner is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

#if DEBUG
            Logger?.Start();
#endif
            neo4j.IStatementResult results = StatementRunner.Run(cypher, parameters);

#if DEBUG
            if (Logger is not null)
            {
                results.Peek();
                Logger.Stop(cypher, parameters: parameters, callerInfo: new List<string>() { memberName, sourceFilePath, sourceLineNumber.ToString() });
            }
#endif

            return new Neo4jRawResult(results);
        }

        public Neo4jPersistenceProvider Provider { get; set; }
        public neo4j.ISession? Session { get; set; }
        public neo4j.ITransaction? Transaction { get; set; }
        public neo4j.IStatementRunner? StatementRunner { get; set; }

        protected override void Initialize()
        {
            neo4j.AccessMode accessMode = (ReadWriteMode) ? neo4j.AccessMode.Write : neo4j.AccessMode.Read;

            Session = Provider.Driver.Session(accessMode);
            Transaction = Session.BeginTransaction();
            StatementRunner = Transaction;
            base.Initialize();
        }

        protected override void OnCommit()
        {
            if (Session is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            if (Transaction is not null)
            {
                Transaction.Success();
                Transaction.Dispose();
            }

            CloseSession();
        }
        protected override void OnRollback()
        {
            if (Session is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            if (Transaction is not null)
                Transaction?.Failure();

            CloseSession();
        }
        protected override void OnRetry()
        {
            OnRollback();
            Initialize();
        }
        private void CloseSession()
        {
            if (Session is not null)
                Session.Dispose();

            Transaction = null;
            StatementRunner = null;
            Session = null;
        }
        protected override void FlushPrivate()
        {
            if (Session is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            if (!ReadWriteMode)
            {
                ReadWriteMode = true;
                OnCommit();
                Initialize();
            }

            base.FlushPrivate();
        }
    }
}
