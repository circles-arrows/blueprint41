using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

using Blueprint41.Core;
using Blueprint41.Persistence.Provider;

namespace Blueprint41.Persistence
{

    public class VoidSession : Session
    {
        internal VoidSession(PersistenceProvider provider, ReadWriteMode readwrite, OptimizeFor optimize, TransactionLogger? logger) : base(provider, readwrite, optimize)
        {
            Logger = logger;
        }

        public override RawResult Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
#if DEBUG
            Logger?.Start();
            if (Logger is not null)
                Logger.Stop(cypher, null, memberName, sourceFilePath, sourceLineNumber);
#endif

            return new RawResult();
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

            return new RawResult();
        }

        protected internal TransactionLogger? Logger { get; private set; }

        public static void Log(string message)
        {
            VoidSession? session = RunningSession as VoidSession;
            if (session is null)
                throw new InvalidOperationException("The current transaction is not a Neo4j transaction.");

            session.Logger?.Log(message);
        }

        protected override void CloseSession() { }
    }
}
