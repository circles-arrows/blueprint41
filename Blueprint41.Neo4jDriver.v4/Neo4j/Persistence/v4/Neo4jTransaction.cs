using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

using neo4j = Neo4j.Driver;
using static Neo4j.Driver.DriverExtensions;

using Blueprint41.Core;
using Blueprint41.Log;
using System.Diagnostics;

namespace Blueprint41.Neo4j.Persistence.Driver.v4
{

    public class Neo4jTransaction : Void.Neo4jTransaction
    {
        internal Neo4jTransaction(neo4j.IDriver driver, bool withTransaction, bool withRewrite, TransactionLogger? logger) : base(withTransaction, logger)
        {
            Driver = driver;
            Session = driver.Session();
            StatementRunner = Session;
            if (withTransaction)
            {
                Transaction = Session.BeginTransaction();
                StatementRunner = Transaction;
            }
            WithRewrite = withRewrite;
        }

        public override RawResult Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (StatementRunner is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

#if DEBUG
            Logger?.Start();
#endif
            neo4j.IResult results;
            if (WithRewrite)
                results = StatementRunner.Run(RewriteOldParams(cypher, null));
            else
                results = StatementRunner.Run(cypher);

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
            neo4j.IResult results;
            if (WithRewrite)
                results = StatementRunner.Run(RewriteOldParams(cypher, parameters), parameters);
            else
                results = StatementRunner.Run(cypher, parameters);

#if DEBUG
            if (Logger is not null)
            {
                results.Peek();
                Logger.Stop(cypher, parameters: parameters, callerInfo: new List<string>() { memberName, sourceFilePath, sourceLineNumber.ToString() });
            }
#endif

            return new Neo4jRawResult(results);
        }

        public string RewriteOldParams(string cypher, Dictionary<string, object?>? parameters)
        {
            string rewritten = cypher;

            if (parameters is not null && cypher.Contains("{"))
                foreach (string name in parameters.Keys)
                    rewritten = rewritten.Replace(string.Concat("{",name ,"}"), string.Concat("$", name));

            if (rewritten != cypher)
            {
                Debug.WriteLine("##### OLD STYLE CYPHER QUERY #####");
                Debug.WriteLine(cypher);
                Debug.WriteLine(new StackTrace().ToString());
            }
            else if (cypher.Contains("{"))
            {
                Debug.WriteLine("##### REWRITE OF CYPHER QUERY POSSIBLY FAILED, DID YOU FORGET TO SEND THE PARAMETERS? #####");
                Debug.WriteLine(cypher);
                Debug.WriteLine(new StackTrace().ToString());
            }

            return rewritten;
        }

        public neo4j.IDriver Driver { get; set; }
        public neo4j.ISession? Session { get; set; }
        public neo4j.ITransaction? Transaction { get; set; }
        public neo4j.IQueryRunner? StatementRunner { get; set; }
        public bool WithRewrite { get; set; }

        protected override void OnCommit()
        {
            if (Session is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            if (WithTransaction)
            {
                if (Transaction is not null)
                {
                    Transaction.Commit();
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
                Transaction?.Rollback();

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
            if (functionalId is null)
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
