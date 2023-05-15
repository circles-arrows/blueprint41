using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.Neo4j.Persistence.Driver.v5;
using Blueprint41.Neo4j.Schema;
using Blueprint41.Query;
using Blueprint41.UnitTest.Mocks;
using NUnit.Framework;
using System;
using System.Linq;

namespace Blueprint41.UnitTest.Tests
{
    /// <summary>
    /// Before running test, be sure to back up the exisiting neo4j database. 
    /// </summary>
    [TestFixture]
    internal class TestNeo4jTransaction : TestBase
    {
        [Test]
        public void EnsureThereShouldBeATransactionWhenRunningNeo4jCypher()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                Transaction.RunningTransaction.Run("CREATE (n:Person { name: 'Address', title: 'Developer' })");
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
                using (Transaction.Begin(true))
                {
                    // Let us try to create a person entity
                    Transaction.RunningTransaction.Run("CREATE (n:Person { name: 'Address', title: 'Developer' })");
                    Transaction.Commit();
                }
            });

            Assert.That(exception.Message, Contains.Substring("The current transaction is not a Neo4j transaction."));

            // Revert back for future transaction
            provider.NotNeo4jTransaction = false;
        }

        [Test]
        public void EnsureNotAbleToTransactAfterCommit()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                using (Transaction.Begin(true))
                {
                    // Let us try to create an entity
                    Transaction.RunningTransaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");

                    Transaction.Commit();

                    // This statement should throw invalid operation exception
                    RawResult result = Transaction.RunningTransaction.Run("Match (n:SampleEntity) Return n");
                    RawRecord record = result.FirstOrDefault();
                    RawNode loaded = record["n"].As<RawNode>();

                    Assert.AreEqual(loaded.Properties["name"], "Address");
                    Assert.AreEqual(loaded.Properties["title"], "Developer");
                }
            });

            Assert.That(exception.Message, Contains.Substring("The transaction was already committed or rolled back."));
        }

        [Test]
        public void EnsureCanCreateAnEntity()
        {
            using (Transaction.Begin(true))
            {
                // Let us try to create an entity
                Transaction.RunningTransaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");

                RawResult result = Transaction.RunningTransaction.Run("Match (n:SampleEntity) Return n");
                RawRecord record = result.FirstOrDefault();
                RawNode loaded = record["n"].As<RawNode>();

                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");

                Transaction.Commit();
            }
        }

        [Test]
        public void EnsureEntityShouldNotBeAddedAfterRollback()
        {
            using (Transaction.Begin(true))
            {
                // Let us try to create an entity
                Transaction.RunningTransaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");

                RawResult result = Transaction.RunningTransaction.Run("Match (n:SampleEntity) Return n");
                RawRecord record = result.FirstOrDefault();
                RawNode loaded = record["n"].As<RawNode>();

                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");

                Transaction.Rollback();
            }

            using (Transaction.Begin())
            {
                RawResult result = Transaction.RunningTransaction.Run("Match (n:SampleEntity) Return n");
                RawRecord record = result.FirstOrDefault();
                Assert.IsNull(record);
            }
        }

        [Test]
        public void EnsureEntityShouldNotBeRollbackedAfterCommitedAndViceVersa()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                using (Transaction.Begin(true))
                {
                    // Let us try to create an entity
                    Transaction.RunningTransaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");

                    RawResult result = Transaction.RunningTransaction.Run("Match (n:SampleEntity) Return n");
                    RawRecord record = result.FirstOrDefault();
                    RawNode loaded = record["n"].As<RawNode>();

                    Assert.AreEqual(loaded.Properties["name"], "Address");
                    Assert.AreEqual(loaded.Properties["title"], "Developer");

                    Transaction.Commit();
                    Transaction.Rollback();
                }
            });

            Assert.That(exception.Message, Contains.Substring("The transaction was already committed or rolled back."));

            InvalidOperationException exception2 = Assert.Throws<InvalidOperationException>(() =>
            {
                using (Transaction.Begin(true))
                {
                    // Let us try to create an entity
                    Transaction.RunningTransaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");

                    RawResult result = Transaction.RunningTransaction.Run("Match (n:SampleEntity) Return n");
                    RawRecord record = result.FirstOrDefault();
                    RawNode loaded = record["n"].As<RawNode>();

                    Assert.AreEqual(loaded.Properties["name"], "Address");
                    Assert.AreEqual(loaded.Properties["title"], "Developer");

                    Transaction.Rollback();
                    Transaction.Commit();
                }
            });

            Assert.That(exception2.Message, Contains.Substring("The transaction was already committed or rolled back."));
        }

        [Test]
        public void EnsureEntityIsCreatedRegardlessAnExceptionIsThrown()
        {
            Assert.Throws<Exception>(() =>
            {
                using (Transaction.Begin())
                {
                    // Let us try to create an entity
                    Transaction.RunningTransaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");
                    Transaction.Commit();
                    throw new Exception();
                }
            });

            using (Transaction.Begin())
            {
                RawResult result = Transaction.RunningTransaction.Run("Match (n:SampleEntity) Return n");
                RawRecord record = result.FirstOrDefault();
                RawNode loaded = record["n"].As<RawNode>();

                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");
            }
        }

        [Test]
        public void EnsureEntityIsRolledbackWhenExceptionIsThrown()
        {
            Assert.Throws<Exception>(() =>
            {
                using (Transaction.Begin(true))
                {
                    // Let us try to create an entity
                    Transaction.RunningTransaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");
                    throw new Exception();
                }
            });

            using (Transaction.Begin())
            {
                RawResult result = Transaction.RunningTransaction.Run("Match (n:SampleEntity) Return n");
                RawRecord record = result.FirstOrDefault();
                Assert.IsNull(record);
            }
        }

        [Test]
        public void EnsureEntityIsFlushedAfterTransaction()
        {
            using (Transaction.Begin(true))
            {
                Transaction.RunningTransaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");
                RawResult result = Transaction.RunningTransaction.Run("Match (n:SampleEntity) Return n");
                RawRecord record = result.FirstOrDefault();

                RawNode loaded = record["n"].As<RawNode>();
                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");

                Transaction.Flush();
            }

            using (Transaction.Begin())
            {
                RawResult result = Transaction.RunningTransaction.Run("Match (n:SampleEntity) Return n");
                RawRecord record = result.FirstOrDefault();
                Assert.IsNull(record);
            }
        }

        [Test]
        public void EnsureEntityIsCreatedEvenFlushedWithoutTransaction()
        {
            using (Transaction.Begin())
            {
                Transaction.RunningTransaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");
                RawResult result = Transaction.RunningTransaction.Run("Match (n:SampleEntity) Return n");
                RawRecord record = result.FirstOrDefault();

                RawNode loaded = record["n"].As<RawNode>();
                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");

                Transaction.Commit();
            }

            using (Transaction.Begin())
            {
                RawResult result = Transaction.RunningTransaction.Run("Match (n:SampleEntity) Return n");
                RawRecord record = result.FirstOrDefault();

                RawNode loaded = record["n"].As<RawNode>();
                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");
            }
        }

    }
}
