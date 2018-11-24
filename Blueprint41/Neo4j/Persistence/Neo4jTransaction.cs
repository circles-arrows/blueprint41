using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Neo4j.Persistence
{
    
    public class Neo4jTransaction : Transaction
    {
        internal Neo4jTransaction(IDriver driver, bool withTransaction)
        {
            Driver = driver;
            Session = driver.Session();
            WithTransaction = withTransaction;
            StatementRunner = Session;
            if (withTransaction)
            {
                Transaction = Session.BeginTransaction();
                StatementRunner = Transaction;
            }
        }

        public static IStatementResult Run(string cypher)
        {
            Neo4jTransaction trans = RunningTransaction as Neo4jTransaction;
            if (trans == null)
                throw new InvalidOperationException("The current transaction is not a Neo4j transaction.");

            IStatementResult results = trans.StatementRunner.Run(cypher);
            return results;
        }
        public static IStatementResult Run(string cypher, Dictionary<string, object> parameters)
        {
            Neo4jTransaction trans = RunningTransaction as Neo4jTransaction;
            if (trans == null)
                throw new InvalidOperationException("The current transaction is not a Neo4j transaction.");

            IStatementResult results = trans.StatementRunner.Run(cypher, parameters);
            return results;
        }

        public IDriver Driver { get; set; }
        public ISession Session { get; set; }
        public ITransaction Transaction { get; set; }
        public IStatementRunner StatementRunner { get; set; }

        private bool WithTransaction;

        protected override void OnCommit()
        {
            if (WithTransaction)
            {
                if (Transaction != null)
                {
                    Transaction.Success();
                    Transaction.Dispose();
                }   
            }

            if (Session != null)
                Session.Dispose();

            Transaction = null;
            StatementRunner = null;
            Session = null;
        }
        protected override void OnRollback()
        {
            if (WithTransaction)
                Transaction.Failure();
            Session.Dispose();
        }

        protected override void FlushPrivate()
        {
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
                IStatementResult result = Run(getFidQuery);
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
