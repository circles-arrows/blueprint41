using Blueprint41.Core;
using Blueprint41.Log;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;


namespace Blueprint41.Neo4j.Persistence.Void
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
        public override Task<RawResult> RunAsync(string cypher, CancellationToken cancellationToken = default, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult<RawResult>(Run(cypher, memberName, sourceFilePath, sourceLineNumber));
        }
        public override Task<RawResult> RunAsync(string cypher, Dictionary<string, object?>? parameters, CancellationToken cancellationToken = default, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult<RawResult>(Run(cypher, parameters, memberName, sourceFilePath, sourceLineNumber));
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
