#pragma warning disable CS8981

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

        public override Transaction NewTransaction(ReadWriteMode mode, OptimizeFor optimize = OptimizeFor.PartialSubGraphAccess)
        {
            return MockNeo4jTransaction.Get(DatastoreModel.PersistenceProvider, mode, optimize, AdvancedConfig?.GetLogger());
        }
        public override Task<Transaction> NewTransactionAsync(ReadWriteMode mode, OptimizeFor optimize = OptimizeFor.PartialSubGraphAccess)
        {
            return MockNeo4jTransaction.GetAsync(DatastoreModel.PersistenceProvider, mode, optimize, AdvancedConfig?.GetLogger());
        }
    }

    public class MockNeo4jTransaction : Transaction
    {
        protected MockNeo4jTransaction(PersistenceProvider provider, ReadWriteMode readwrite, OptimizeFor optimize, TransactionLogger? logger)
            : base(provider, readwrite, optimize, logger)
        {
        }

        static internal Transaction Get(PersistenceProvider provider, ReadWriteMode readwrite, OptimizeFor optimize, TransactionLogger? logger)
        {
            MockNeo4jTransaction transaction = new MockNeo4jTransaction(provider, readwrite, optimize, logger);
            transaction.InitializeDriver();
            return transaction;
        }
        static internal async Task<Transaction> GetAsync(PersistenceProvider provider, ReadWriteMode readwrite, OptimizeFor optimize, TransactionLogger? logger)
        {
            MockNeo4jTransaction transaction = new MockNeo4jTransaction(provider, readwrite, optimize, logger);
            await transaction.InitializeDriverAsync();
            return transaction;
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
