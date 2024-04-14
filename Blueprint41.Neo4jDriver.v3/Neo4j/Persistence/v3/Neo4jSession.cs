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

    public class Neo4jSession : Void.Neo4jSession
    {
        internal Neo4jSession(Neo4jPersistenceProvider provider, bool readWriteMode, TransactionLogger? logger) : base(provider, readWriteMode, logger)
        {
            Provider = provider;
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
                Logger.Stop(cypher, null, memberName, sourceFilePath, sourceLineNumber);
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
                Logger.Stop(cypher, parameters, memberName, sourceFilePath, sourceLineNumber);
            }
#endif

            return new Neo4jRawResult(results);
        }

        public Neo4jPersistenceProvider Provider { get; set; }
        public neo4j.ISession? Session { get; set; }
        public neo4j.IStatementRunner? StatementRunner { get; set; }

        protected override void Initialize()
        {
            neo4j.AccessMode accessMode = (ReadWriteMode) ? neo4j.AccessMode.Write : neo4j.AccessMode.Read;

            Session = Provider.Driver.Session(accessMode);
            StatementRunner = Session;
            base.Initialize();
        }

        protected override void CloseSession()
        {
            if (Session is not null)
                Session.Dispose();

            StatementRunner = null;
            Session = null;
        }
    }
}
