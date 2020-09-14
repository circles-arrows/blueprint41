using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence.Driver.v3;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Blueprint41.UnitTest.Mocks
{
    public class MockNeo4JPersistenceProvider : Neo4jPersistenceProvider
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
        public override RawResult Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            throw new NotImplementedException();
        }

        public override RawResult Run(string cypher, Dictionary<string, object> parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            throw new NotImplementedException();
        }

        protected override void ApplyFunctionalId(FunctionalId functionalId)
        {

        }
    }
}
