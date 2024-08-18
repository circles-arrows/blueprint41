using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

using Blueprint41.Async.Core;
using Blueprint41.Async.Log;


namespace Blueprint41.Async.Neo4j.Persistence.Void
{

    public class Neo4jSession : Session
    {
        internal Neo4jSession(bool readWriteMode, TransactionLogger? logger)
        {
            Logger = logger;
            ReadWriteMode = readWriteMode;
        }

        public override RawResult Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
#if DEBUG
            Logger?.Start();
            if (Logger is not null)
                Logger.Stop(cypher, null, memberName, sourceFilePath, sourceLineNumber);
#endif

            return new Neo4jRawResult();
        }
        public override RawResult Run(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
#if DEBUG
            Logger?.Start();
            if (Logger is not null)
            {
                Logger.Stop(cypher, parameters: parameters, memberName, sourceFilePath, sourceLineNumber);
            }
#endif

            return new Neo4jRawResult();
        }

        protected internal TransactionLogger? Logger { get; private set; }

        protected bool ReadWriteMode { get; set; }

        public static void Log(string message)
        {
            Neo4jSession? session = RunningSession as Neo4jSession;
            if (session is null)
                throw new InvalidOperationException("The current transaction is not a Neo4j transaction.");

            session.Logger?.Log(message);
        }

        protected override void CloseSession() { }
    }
}
