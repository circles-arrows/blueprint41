﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using Blueprint41.Core;
using Blueprint41.Persistence;

namespace Blueprint41
{
    public class Session : DisposableScope<Session>, IStatementRunner
    {
        internal Session(PersistenceProvider provider, ReadWriteMode readwrite, OptimizeFor optimize, TransactionLogger? logger)
        {
            Logger = logger;
            OptimizeFor = optimize;
            ReadWriteMode = readwrite;
            //DisableForeignKeyChecks = false;

            PersistenceProvider = provider;
        }
        private protected TransactionLogger? Logger { get; private set; }
        public static void Log(string message) => RunningSession.Logger?.Log(message);

        public DateTime TransactionDate { get; private set; }
        public OptimizeFor OptimizeFor { get; private set; }
        public ReadWriteMode ReadWriteMode { get; private set; }

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

        public virtual RawResult Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (PersistenceProvider.IsVoidProvider)
            {
#if DEBUG
                Logger?.Start();
                if (Logger is not null)
                    Logger.Stop(cypher, null, memberName, sourceFilePath, sourceLineNumber);
#endif
                return new RawResult(0);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public virtual RawResult Run(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (PersistenceProvider.IsVoidProvider)
            {
#if DEBUG
                Logger?.Start();
                if (Logger is not null)
                    Logger.Stop(cypher, null, memberName, sourceFilePath, sourceLineNumber);
#endif
                return new RawResult(0);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

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

        public PersistenceProvider PersistenceProvider { get; private set; }
        internal NodePersistenceProvider NodePersistenceProvider => PersistenceProvider.NodePersistenceProvider;
        internal RelationshipPersistenceProvider RelationshipPersistenceProvider => PersistenceProvider.RelationshipPersistenceProvider;

        #endregion

        protected override void Cleanup() => CloseSession();
        protected virtual void CloseSession() { }
    }
}
