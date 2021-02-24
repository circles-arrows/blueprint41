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
                            driver = GraphDatabase.Driver(Uri, AuthTokens.Basic(Username, Password));

                            if (Database is not null)
                                using (Transaction.Begin())
                                    Transaction.RunningTransaction.Run($":use {Database}");
                        }
                    }
                }
                return driver;
            }
        }

        public bool? RewriteOldParams { get; private set; }

        private Neo4jPersistenceProvider() : this(null, null, null, false) { }
        public Neo4jPersistenceProvider(string? uri, string? username, string? password, bool withLogging = false, bool rewriteOldParams = false) : this(uri, username, password, null, withLogging, rewriteOldParams) { }
        public Neo4jPersistenceProvider(string? uri, string? username, string? password, string? database, bool withLogging = false, bool rewriteOldParams = false) : base(uri, username, password, database, withLogging)
        {
            RewriteOldParams = rewriteOldParams;

            Core.ExtensionMethods.RegisterAsConversion(typeof(Neo4jPersistenceProvider), typeof(RawNode),         from => from is INode         item ? new Neo4jRawNode(item)         : null);
            Core.ExtensionMethods.RegisterAsConversion(typeof(Neo4jPersistenceProvider), typeof(RawRelationship), from => from is IRelationship item ? new Neo4jRawRelationship(item) : null);
        }

        private protected override NodePersistenceProvider GetNodePersistenceProvider()
        {
            return new Neo4jNodePersistenceProvider(this);
        }
        private protected override RelationshipPersistenceProvider GetRelationshipPersistenceProvider()
        {
            return new Neo4jRelationshipPersistenceProvider(this);
        }
        public override Transaction NewTransaction(bool withTransaction)
        {
            return new Neo4jTransaction(Driver, withTransaction, RewriteOldParams ?? false, TransactionLogger);
        }

        protected override QueryTranslator GetTranslator() => new Neo4jQueryTranslator();
        internal override SchemaInfo GetSchemaInfo(DatastoreModel datastoreModel) => new Schema.v4.SchemaInfo_v4(datastoreModel);
    }
}
