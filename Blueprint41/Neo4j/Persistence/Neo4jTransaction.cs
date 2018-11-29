using Blueprint41.Gremlin;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blueprint41;
using Blueprint41.Response;

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

        protected override IGraphResponse RunPrivate(string cypher)
        {
            Neo4jTransaction trans = RunningTransaction as Neo4jTransaction;
            if (trans == null)
                throw new InvalidOperationException("The current transaction is not a Neo4j transaction.");

            //Console.WriteLine("===========Neo4j Transaction=====================");
            //Console.WriteLine(cypher);
            //Console.WriteLine("===================================================");

            //Console.WriteLine();

            //Console.WriteLine("===========Gremlin Translation=====================");
            //Console.WriteLine(Translate.ToCosmos(cypher.Replace(Environment.NewLine, " ")) ?? "No translation");
            //Console.WriteLine("====================================================");

            //Console.WriteLine("#####################################################");
            //Console.WriteLine("#####################################################");
            //Console.WriteLine();

            IGraphResponse results = trans.StatementRunner.RunCypher(cypher);
            return results;
        }
        protected override IGraphResponse RunPrivate(string cypher, Dictionary<string, object> parameters)
        {
            Neo4jTransaction trans = RunningTransaction as Neo4jTransaction;
            if (trans == null)
                throw new InvalidOperationException("The current transaction is not a Neo4j transaction.");

            //Console.WriteLine("===========Neo4j Transaction=====================");
            //Console.WriteLine(cypher);
            //Console.WriteLine("===================================================");

            //Console.WriteLine();

            //Console.WriteLine("===========Gremlin Translation=====================");
            //Console.WriteLine(Translate.ToCosmos(cypher.ToCypherString(parameters).Replace(Environment.NewLine, " ")) ?? "No translation");
            //Console.WriteLine("====================================================");

            //Console.WriteLine("#####################################################");
            //Console.WriteLine("#####################################################");
            //Console.WriteLine();


            IGraphResponse results = trans.StatementRunner.RunCypher(cypher, parameters);
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
                string query = $"CALL blueprint41.functionalid.setSequenceNumber('{functionalId.Label}', {functionalId.highestSeenId}, {(functionalId.Format == IdFormat.Numeric).ToString().ToLowerInvariant()})";
                RunPrivate(query);
                functionalId.wasApplied = true;
                functionalId.highestSeenId = -1;
            }
        }
    }
}
