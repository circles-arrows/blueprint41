#pragma warning disable CS8981

using System;
using System.Linq;
using System.Threading.Tasks;

using Blueprint41.Core;
using Blueprint41.Persistence;
using Blueprint41.Config;

using driver = Neo4j.Driver; 

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

        protected override void InitializeDriver()
        {
            DriverSession = Swap(InitializeDriverSession());
            DriverTransaction = Swap(DriverSession.BeginTransaction());
        }

        protected override async Task InitializeDriverAsync()
        {
            DriverSession = Swap(InitializeDriverSession());
            DriverTransaction = Swap(await DriverSession.BeginTransactionAsync().ConfigureAwait(false));
        }

        private DriverSession Swap(DriverSession session)
        {
            driver.IAsyncSession neo4jSession = (driver.IAsyncSession)session._instance;

            return new DriverSession(new MockSession(neo4jSession));
        }
        private DriverTransaction Swap(DriverTransaction transaction)
        {
            driver.IAsyncTransaction neo4jTransaction = (driver.IAsyncTransaction)transaction._instance;

            return new DriverTransaction(new MockTransaction(neo4jTransaction));
        }
    }
}
