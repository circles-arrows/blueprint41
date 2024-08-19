using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Blueprint41.Core;

namespace Blueprint41
{
    public abstract class Session : DisposableScope<Session>, IStatementRunner
    {
        protected Session(PersistenceProvider provider)
        {
            PersistenceProviderFactory = provider;
        }

        #region Session Logic

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
