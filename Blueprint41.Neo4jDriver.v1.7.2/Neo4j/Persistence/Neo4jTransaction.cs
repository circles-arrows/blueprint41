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
        internal Neo4jTransaction(neo4j.IDriver driver, bool withTransaction, TransactionLogger? logger) : base(withTransaction, logger)
        {
            Driver = driver;
            Session = driver.Session();
            StatementRunner = Session;
            if (withTransaction)
            {
                Transaction = Session.BeginTransaction();
                StatementRunner = Transaction;
            }
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
            if (Logger != null)
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
            if (Logger != null)
            {
                results.Peek();
                Logger.Stop(cypher, parameters: parameters, callerInfo: new List<string>() { memberName, sourceFilePath, sourceLineNumber.ToString() });
            }
#endif

            return new Neo4jRawResult(results);
        }

        public neo4j.IDriver Driver { get; set; }
        public neo4j.ISession? Session { get; set; }
        public neo4j.ITransaction? Transaction { get; set; }
        public neo4j.IStatementRunner? StatementRunner { get; set; }

        protected override void OnCommit()
        {
            if (Session is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            if (WithTransaction)
            {
                if (Transaction != null)
                {
                    Transaction.Success();
                    Transaction.Dispose();
                }
            }

            if (!(Session is null))
                Session.Dispose();

            Transaction = null;
            StatementRunner = null;
            Session = null;
        }
        protected override void OnRollback()
        {
            if (Session is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            if (WithTransaction)
                Transaction?.Failure();

            if (!(Session is null))
                Session.Dispose();

            StatementRunner = null;
            Session = null;
        }

        protected override void FlushPrivate()
        {
            if (Session is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            if (!WithTransaction)
            {
                Transaction = Session.BeginTransaction();
                StatementRunner = Transaction;
                WithTransaction = true;
            }

            base.FlushPrivate();
        }

        protected override void ApplyFunctionalId(FunctionalId functionalId)
        {
            if (functionalId == null)
                return;

            if (functionalId.wasApplied || functionalId.highestSeenId == -1)
                return;

            lock (functionalId)
            {
                string getFidQuery = $"CALL blueprint41.functionalid.current('{functionalId.Label}')";
                RawResult result = Run(getFidQuery);
                long? currentFid = result.FirstOrDefault()?.Values["Sequence"].As<long?>();
                if (currentFid.HasValue)
                    functionalId.SeenUid(currentFid.Value);

                string setFidQuery = $"CALL blueprint41.functionalid.setSequenceNumber('{functionalId.Label}', {functionalId.highestSeenId}, {(functionalId.Format == IdFormat.Numeric).ToString().ToLowerInvariant()})";
                Run(setFidQuery);
                functionalId.wasApplied = true;
                functionalId.highestSeenId = -1;
            }
        }
    }
}
