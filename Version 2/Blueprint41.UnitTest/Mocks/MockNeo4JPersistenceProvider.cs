﻿#pragma warning disable CS8981

using System;
using System.Linq;

using neo4j = Neo4j.Driver;

using Blueprint41.Core;
using Blueprint41.Persistence;
using System.Threading.Tasks;

namespace Blueprint41.UnitTest.Mocks
{
    public class MockNeo4jPersistenceProvider : PersistenceProvider
    {
        internal protected MockNeo4jPersistenceProvider(DatastoreModel model, Uri? uri, AuthToken? authToken, string? database, AdvancedConfig? advancedConfig = null) 
            : base(model, uri, authToken, database, advancedConfig)
        {
        }

        //#if NEO4J
        //        public MockNeo4jPersistenceProvider(string uri, string username, string pass) : base(uri, username, pass, "unittest") { }
        //#elif MEMGRAPH
        //        public MockNeo4jPersistenceProvider(string uri, string username, string pass) : base(uri, username, pass) { }
        //#endif

        //public override Transaction NewTransaction(bool readWriteMode) => new MockNeo4jTransaction(this, readWriteMode, TransactionLogger);
    }

    public class MockNeo4jTransaction : Transaction
    {
        protected MockNeo4jTransaction(PersistenceProvider provider, ReadWriteMode readwrite, OptimizeFor optimize, TransactionLogger? logger)
            : base(provider, readwrite, optimize, logger)
        {
        }

        //protected override void Initialize()
        //{
        //    neo4j.AccessMode accessMode = (ReadWriteMode) ? neo4j.AccessMode.Write : neo4j.AccessMode.Read;

        //    if ((Provider ?? PersistenceProvider.CurrentPersistenceProvider) is not MockNeo4jPersistenceProvider provider)
        //        throw new InvalidOperationException("CurrentPersistenceProvider is null");

        //    Session = new MockSession(provider.Driver.AsyncSession(c =>
        //    {
        //        if (provider.Database is not null)
        //            c.WithDatabase(provider.Database);

        //        c.WithFetchSize(neo4j.Config.Infinite);
        //        c.WithDefaultAccessMode(accessMode);

        //        if (Consistency is not null)
        //            c.WithBookmarks(Consistency.Select(item => item.ToBookmark()).ToArray());
        //    }));

        //    Transaction = provider.TaskScheduler.RunBlocking(() => Session.BeginTransactionAsync(), "Begin Transaction");

        //    StatementRunner = Transaction;
        //}
    }
}