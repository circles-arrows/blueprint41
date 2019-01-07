using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.Response;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.UnitTest.Mocks
{
    public class MockNeo4JPersistenceProvider : Neo4JPersistenceProvider
    {
        public bool NotNeo4jTransaction { get; set; }

        public MockNeo4JPersistenceProvider(string uri, string username, string pass) : base(uri, username, pass)
        {

        }

        public override Transaction NewTransaction(bool withTransaction)
        {
            if (NotNeo4jTransaction)
                return new NotNeo4jTransaction();

            Neo4jTransaction transaction = base.NewTransaction(withTransaction) as Neo4jTransaction;
            ISession session = transaction.Session;
            transaction.Session = new MockSession(session);
            transaction.StatementRunner = transaction.Session;
            transaction.Transaction?.Dispose();
            transaction.Transaction = null;

            if (withTransaction)
            {
                transaction.Transaction = transaction.Session.BeginTransaction();
                transaction.StatementRunner = transaction.Transaction;
            }

            return transaction;
        }
    }

    public class NotNeo4jTransaction : Transaction
    {
        protected override void ApplyFunctionalId(FunctionalId functionalId)
        {

        }

        protected override IGraphResponse RunPrivate(string cypher)
        {
            throw new NotImplementedException();
        }

        protected override IGraphResponse RunPrivate(string cypher, Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
