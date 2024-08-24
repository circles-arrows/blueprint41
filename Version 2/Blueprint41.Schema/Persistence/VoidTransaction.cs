using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

using Blueprint41.Core;
using Blueprint41.Persistence.Provider;

namespace Blueprint41.Persistence
{

    public class VoidTransaction : Transaction
    {
        internal VoidTransaction(PersistenceProvider provider, ReadWriteMode readwrite, OptimizeFor optimize, TransactionLogger? logger) : base(provider, readwrite, optimize)
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

        protected override void CommitInternal()
        {
            RaiseOnCommit();
        }
        protected override void RollbackInternal()
        {
        }
        protected override void RetryInternal()
        {
        }

        public static void Log(string message)
        {
            VoidTransaction? trans = RunningTransaction as VoidTransaction;
            if (trans is null)
                throw new InvalidOperationException("The current transaction is not a Neo4j transaction.");

            trans.Logger?.Log(message);
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
                long? currentFid = result.FirstOrDefault()?["Sequence"].As<long?>();
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
