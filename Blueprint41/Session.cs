using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Blueprint41.Core.RelationshipPersistenceProvider;
using persistence = Blueprint41.Neo4j.Persistence;

namespace Blueprint41
{
    public abstract class Session : DisposableScope<Session>, IStatementRunner
    {
        protected Session()
        {
            PersistenceProvider factory = PersistenceProvider.CurrentPersistenceProvider;
            PersistenceProviderFactory = factory;
        }

        #region Session Logic

        public static Session Begin()
        {
            return Begin(true, OptimizeFor.PartialSubGraphAccess);
        }
        public static Session Begin(bool readWriteMode)
        {
            return Begin(readWriteMode, OptimizeFor.PartialSubGraphAccess);
        }
        public static Session Begin(OptimizeFor mode)
        {
            return Begin(true, mode);
        }
        public static Session Begin(bool readWriteMode, OptimizeFor mode)
        {
            if (PersistenceProvider.CurrentPersistenceProvider is null)
                throw new InvalidOperationException("PersistenceProviderFactory should be set before you start doing transactions.");

            Session trans = PersistenceProvider.CurrentPersistenceProvider.NewSession(readWriteMode);
            trans.Attach();

            return trans;
        }

        public virtual Session WithConsistency(Bookmark consistency)
        {
            return this;
        }
        public virtual Session WithConsistency(string consistencyToken)
        {
            return this;
        }
        public Session WithConsistency(params Bookmark[] consistency)
        {
            if (consistency is not null)
            {
                foreach (Bookmark item in consistency)
                    WithConsistency(item);
            }

            return this;
        }
        public Session WithConsistency(params string[] consistencyTokens)
        {
            if (consistencyTokens is not null)
            {
                foreach (string item in consistencyTokens)
                    WithConsistency(item);
            }

            return this;
        }

        public abstract RawResult Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
        public abstract RawResult Run(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);

        public static Session RunningSession
        {
            get
            {
                Session? session = Current;

                if (session is null)
                    throw new InvalidOperationException("There is no session, you should create one first -> using (Session.Begin()) { ... }");

                return session;
            }
        }

        public virtual Bookmark GetConsistency() => NullConsistency;
        private static readonly Bookmark NullConsistency = new Bookmark();

        #endregion

        #region PersistenceProviderFactory

        public PersistenceProvider PersistenceProviderFactory { get; private set; }

        #endregion

        protected override void Cleanup() => CloseSession();
        protected abstract void CloseSession();
    }
}
