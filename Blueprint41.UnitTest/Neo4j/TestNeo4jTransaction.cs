using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.Neo4j.Schema;
using Blueprint41.UnitTest.Mocks;
using Neo4j.Driver.V1;
using NUnit.Framework;
using System;
using System.Linq;

namespace Blueprint41.UnitTest.Neo4j
{
    /// <summary>
    /// Before running test, be sure to back up the exisiting neo4j database. 
    /// </summary>
    [TestFixture]
    public class TestNeo4jTransaction
    {
        [SetUp]
        public void Setup()
        {            
            MockNeo4JPersistenceProvider persistenceProvider = new MockNeo4JPersistenceProvider("bolt://localhost:7687", "neo4j", "neo");
            PersistenceProvider.CurrentPersistenceProvider = persistenceProvider;
        }

        [Test]
        public void EnsureThereShouldBeATransactionWhenRunningNeo4jCypher()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                Neo4jTransaction.Run("CREATE (n:Person { name: 'Address', title: 'Developer' })");
            });

            Assert.That(exception.Message, Contains.Substring("There is no transaction, you should create one first -> using (Transaction.Begin()) { ... Transaction.Commit(); }"));
        }

        [Test]
        public void EnsureRunningTransactionIsNeo4jTransaction()
        {
            using (Transaction.Begin())
                Assert.IsInstanceOf<Neo4jTransaction>(Transaction.RunningTransaction);
        }

        [Test]
        public void EnsureRunningTransactionIsNeo4jTransactionWithException()
        {
            var provider = PersistenceProvider.CurrentPersistenceProvider as MockNeo4JPersistenceProvider;
            provider.NotNeo4jTransaction = true;

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                using (Transaction.Begin())
                {
                    // Let us try to create a person entity
                    Neo4jTransaction.Run("CREATE (n:Person { name: 'Address', title: 'Developer' })");
                    Transaction.Commit();
                }
            });

            Assert.That(exception.Message, Contains.Substring("The current transaction is not a Neo4j transaction."));

            // Revert back for future transaction
            provider.NotNeo4jTransaction = false;
        }

        [Test]
        public void EnsureCanCreateAnEntity()
        {
            using (Transaction.Begin())
            {
                // Let us try to create a person entity
                Neo4jTransaction.Run("CREATE (n:Person { name: 'Address', title: 'Developer' })");                
                Transaction.Commit();
            }

            // Load the newly added entity
            using (Transaction.Begin())
            {
                IStatementResult result = Neo4jTransaction.Run("Match (n:Person) Return n");
                IRecord record = result.FirstOrDefault();
                INode loaded = record["n"].As<INode>();

                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (Transaction.Begin())
            {
                string reset = "Match (n) detach delete n";
                Neo4jTransaction.Run(reset);
                Transaction.Commit();
            }
        }
    }
}
