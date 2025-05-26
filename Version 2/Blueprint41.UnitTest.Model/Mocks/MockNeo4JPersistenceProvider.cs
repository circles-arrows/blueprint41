#pragma warning disable CS8981

using System;
using System.Linq;
using System.Threading.Tasks;

using Blueprint41.Core;
using Blueprint41.Persistence;
using Blueprint41.Config;

using driver = Neo4j.Driver;
using Blueprint41.Events;

namespace Blueprint41.UnitTest.Mocks
{
    public class MockNeo4jPersistenceProvider : PersistenceProvider
    {
        internal MockNeo4jPersistenceProvider(DatastoreModel model, Uri? uri, AuthToken? authToken, string? database, AdvancedConfig? advancedConfig = null) 
            : base(model, uri, authToken, database, advancedConfig)
        {
        }

        public override Transaction NewTransaction(ReadWriteMode mode, OptimizeFor optimize = OptimizeFor.PartialSubGraphAccess)
        {
            return MockNeo4jTransaction.Get(DatastoreModel, mode, optimize, AdvancedConfig?.GetLogger());
        }
    }

    public class MockNeo4jTransaction : Transaction
    {
        private MockNeo4jTransaction(DatastoreModel model, ReadWriteMode readwrite, OptimizeFor optimize, TransactionLogger? logger)
            : base(model, readwrite, optimize, logger)
        {
        }

        static internal Transaction Get(DatastoreModel model, ReadWriteMode readwrite, OptimizeFor optimize, TransactionLogger? logger)
        {
            MockNeo4jTransaction transaction = new MockNeo4jTransaction(model, readwrite, optimize, logger);
            transaction.Attach();
            transaction.TransactionDate = DateTime.UtcNow;
            transaction.FireEvents = EventOptions.AllEvents;

            return transaction;
        }

        protected override void Initialize()
        {
            AccessMode accessMode = (ReadWriteMode == ReadWriteMode.ReadWrite) ? AccessMode.Write : AccessMode.Read;

            DriverSession = Swap(PersistenceProvider.Driver.Session(c =>
            {
                if (PersistenceProvider.Database is not null)
                    c.WithDatabase(PersistenceProvider.Database);

                c.WithFetchSize(ConfigBuilder.Infinite);
                c.WithDefaultAccessMode(accessMode);

                if (Consistency is not null)
                    c.WithBookmarks(Consistency);
            }));
        }

        public virtual DriverTransaction? GetDriverTransaction()
        {
            if (!InTransaction)
                return null;

            if (_driverTransaction is null && DriverSession is not null)
                _driverTransaction = Swap(DriverSession.BeginTransaction());

            return _driverTransaction;
        }
        public virtual async Task<DriverTransaction?> GetDriverTransactionAsync()
        {
            if (!InTransaction)
                return null;

            if (_driverTransaction is null && DriverSession is not null)
                _driverTransaction = Swap(await DriverSession.BeginTransactionAsync());

            return _driverTransaction;
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
