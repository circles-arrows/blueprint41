using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Neo4j.Driver;

using Blueprint41.Core;
using Blueprint41.Log;
using Blueprint41.Neo4j.Model;
using Blueprint41.Neo4j.Schema;

namespace Blueprint41.Neo4j.Persistence.Driver.v4
{
    public partial class Neo4jPersistenceProvider : Void.Neo4jPersistenceProvider
    {
        private const int MAX_CONNECTION_POOL_SIZE = 400;
        private const int DEFAULT_READWRITESIZE = 65536;
        private const int DEFAULT_READWRITESIZE_MAX = 655360;
        private IDriver? driver = null;
        public IDriver Driver
        {
            get
            {
                if (driver is null)
                {
                    lock (typeof(Neo4jPersistenceProvider))
                    {
                        if (driver is null)
                        {
                            driver = GraphDatabase.Driver(Uri, AuthTokens.Basic(Username, Password), 
                                o =>
                                {
                                    o.WithFetchSize(Config.Infinite);
                                    o.WithMaxConnectionPoolSize(MAX_CONNECTION_POOL_SIZE);
                                    o.WithDefaultReadBufferSize(DEFAULT_READWRITESIZE);
                                    o.WithDefaultWriteBufferSize(DEFAULT_READWRITESIZE);
                                    o.WithMaxReadBufferSize(DEFAULT_READWRITESIZE_MAX);
                                    o.WithMaxWriteBufferSize(DEFAULT_READWRITESIZE_MAX);
                                    o.WithMaxTransactionRetryTime(TimeSpan.Zero);
                                }
                            );

                            if (Database is not null)
                                using (Transaction.Begin())
                                    Transaction.RunningTransaction.Run($":use {Database}");
                        }
                    }
                }
                return driver;
            }
        }

        private Neo4jPersistenceProvider() : this(null, null, null, false) { }
        public Neo4jPersistenceProvider(string? uri, string? username, string? password, bool withLogging = false) : this(uri, username, password, null, withLogging) { }
        public Neo4jPersistenceProvider(string? uri, string? username, string? password, string? database, bool withLogging = false) : base(uri, username, password, database, withLogging)
        {
            Core.ExtensionMethods.RegisterAsConversion(typeof(Neo4jPersistenceProvider), typeof(RawNode),         from => from is INode         item ? new Neo4jRawNode(item)         : null);
            Core.ExtensionMethods.RegisterAsConversion(typeof(Neo4jPersistenceProvider), typeof(RawRelationship), from => from is IRelationship item ? new Neo4jRawRelationship(item) : null);
        }
        public Neo4jPersistenceProvider(string? uri, string? username, string? password, Action<string> logger) : this(uri, username, password, null, logger) { }
        public Neo4jPersistenceProvider(string? uri, string? username, string? password, string? database, Action<string> logger) : base(uri, username, password, database, logger)
        {
            Core.ExtensionMethods.RegisterAsConversion(typeof(Neo4jPersistenceProvider), typeof(RawNode), from => from is INode item ? new Neo4jRawNode(item) : null);
            Core.ExtensionMethods.RegisterAsConversion(typeof(Neo4jPersistenceProvider), typeof(RawRelationship), from => from is IRelationship item ? new Neo4jRawRelationship(item) : null);
        }


        public override Transaction NewTransaction(bool withTransaction)
        {
            return new Neo4jTransaction(this, withTransaction, TransactionLogger);
        }

        internal CustomTaskScheduler TaskScheduler
        {
            get
            {
                if (taskScheduler is null)
                {
                    lock (sync)
                    {
                        if (taskScheduler is null)
                        {
                            CustomTaskQueueOptions main = new CustomTaskQueueOptions(10, 50);
                            CustomTaskQueueOptions sub = new CustomTaskQueueOptions(20, 100);
                            taskScheduler = new CustomThreadSafeTaskScheduler(main, sub);
                        }
                    }
                }

                return taskScheduler;
            }
        }
        private CustomTaskScheduler? taskScheduler = null;
        private static object sync = new object();

        public Neo4jPersistenceProvider ConfigureTaskScheduler(CustomTaskQueueOptions mainQueue) => ConfigureTaskScheduler(mainQueue, CustomTaskQueueOptions.Disabled);
        public Neo4jPersistenceProvider ConfigureTaskScheduler(CustomTaskQueueOptions mainQueue, CustomTaskQueueOptions subQueue)
        {
            lock (sync)
            {
                if (taskScheduler is not null)
                    throw new InvalidOperationException("You can only configure the TaskScheduler during initialization of Blueprint41.");

                taskScheduler = new CustomThreadSafeTaskScheduler(mainQueue, subQueue);
            }

            return this;
        }
    }
}
