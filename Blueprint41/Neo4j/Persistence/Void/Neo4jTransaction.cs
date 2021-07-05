using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

using Blueprint41.Core;
using Blueprint41.Log;


namespace Blueprint41.Neo4j.Persistence.Void
{

    public class Neo4jTransaction : Transaction
    {
        internal Neo4jTransaction(bool readWriteMode, TransactionLogger? logger)
        {
            Logger = logger;
            ReadWriteMode = readWriteMode;
        }

        public override RawResult Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
#if DEBUG
            Logger?.Start();
            if (Logger is not null)
                Logger.Stop(cypher, callerInfo: new List<string>() { memberName, sourceFilePath, sourceLineNumber.ToString() });
#endif

            return new Neo4jRawResult();
        }
        public override RawResult Run(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
#if DEBUG
            Logger?.Start();
            if (Logger is not null)
            {
                Logger.Stop(cypher, parameters: parameters, callerInfo: new List<string>() { memberName, sourceFilePath, sourceLineNumber.ToString() });
            }
#endif

            return new Neo4jRawResult();
        }

        protected internal TransactionLogger? Logger { get; private set; }

        protected bool ReadWriteMode { get; set; }

        protected override void Initialize()
        {
            isDisposed = false;
        }
        protected override void OnCommit()
        {
        }
        protected override void OnRollback()
        {
        }

        protected override void FlushPrivate()
        {
            base.FlushPrivate();
        }

        public static void Log(string message)
        {
            Neo4jTransaction? trans = RunningTransaction as Neo4jTransaction;
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
