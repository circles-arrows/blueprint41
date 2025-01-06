#pragma warning disable CS8981 // Names should not be lower type only

using System;
using System.Linq;

using NUnit.Framework;

using Blueprint41.Core;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.Persistence;

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
                Transaction.Run("CREATE (n:Person { name: 'Address', title: 'Developer' })");
            });

            Assert.That(exception.Message, Contains.Substring("There is no transaction, you should create one first -> using (MockModel.BeginTransaction()) { ... Transaction.Commit(); }"));
        }

        [Test]
        public void EnsureRunningTransactionIsNeo4jTransaction()
        {
            using (MockModel.BeginTransaction())
                Assert.IsInstanceOf<Transaction>(Transaction.RunningTransaction);
        }

        [Test]
        public void EnsureNotAbleToTransactAfterCommit()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                using (MockModel.BeginTransaction(ReadWriteMode.ReadWrite))
                {
                    // Let us try to create an entity
                    Transaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");

                    Transaction.Commit();

                    // This statement should throw invalid operation exception
                    ResultCursor result = Transaction.Run("Match (n:SampleEntity) Return n");
                    Record record = result.First();
                    NodeResult loaded = record["n"].As<NodeResult>();

                    Assert.AreEqual(loaded.Properties["name"], "Address");
                    Assert.AreEqual(loaded.Properties["title"], "Developer");
                }
            });

            Assert.That(exception.Message, Contains.Substring("The transaction was already committed or rolled back."));
        }

        [Test]
        public void EnsureCanCreateAnEntity()
        {
            using (MockModel.BeginTransaction(ReadWriteMode.ReadWrite))
            {
                // Let us try to create an entity
                Transaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");

                ResultCursor result = Transaction.Run("Match (n:SampleEntity) Return n");
                Record record = result.First();
                NodeResult loaded = record["n"].As<NodeResult>();

                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");

                Transaction.Commit();
            }
        }

        [Test]
        public void EnsureEntityShouldNotBeAddedAfterRollback()
        {
            using (MockModel.BeginTransaction(ReadWriteMode.ReadWrite))
            {
                // Let us try to create an entity
                Transaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");

                ResultCursor result = Transaction.Run("Match (n:SampleEntity) Return n");
                Record record = result.First();
                NodeResult loaded = record["n"].As<NodeResult>();

                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");

                Transaction.Rollback();
            }

            using (MockModel.BeginTransaction())
            {
                ResultCursor result = Transaction.Run("Match (n:SampleEntity) Return n");
                Record record = result.FirstOrDefault()!;
                Assert.IsNull(record);
            }
        }

        [Test]
        public void EnsureEntityShouldNotBeRollbackedAfterCommitedAndViceVersa()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                using (MockModel.BeginTransaction(ReadWriteMode.ReadWrite))
                {
                    // Let us try to create an entity
                    Transaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");

                    ResultCursor result = Transaction.Run("Match (n:SampleEntity) Return n");
                    Record record = result.First();
                    NodeResult loaded = record["n"].As<NodeResult>();

                    Assert.AreEqual(loaded.Properties["name"], "Address");
                    Assert.AreEqual(loaded.Properties["title"], "Developer");

                    Transaction.Commit();
                    Transaction.Rollback();
                }
            });

            Assert.That(exception.Message, Contains.Substring("The transaction was already committed or rolled back."));

            InvalidOperationException exception2 = Assert.Throws<InvalidOperationException>(() =>
            {
                using (MockModel.BeginTransaction(ReadWriteMode.ReadWrite))
                {
                    // Let us try to create an entity
                    Transaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");

                    ResultCursor result = Transaction.Run("Match (n:SampleEntity) Return n");
                    Record record = result.First();
                    NodeResult loaded = record["n"].As<NodeResult>();

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
                using (MockModel.BeginTransaction())
                {
                    // Let us try to create an entity
                    Transaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");
                    Transaction.Commit();
                    throw new Exception();
                }
            });

            using (MockModel.BeginTransaction())
            {
                ResultCursor result = Transaction.Run("Match (n:SampleEntity) Return n");
                Record record = result.First();
                NodeResult loaded = record["n"].As<NodeResult>();

                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");
            }
        }

        [Test]
        public void EnsureEntityIsRolledbackWhenExceptionIsThrown()
        {
            Assert.Throws<Exception>(() =>
            {
                using (MockModel.BeginTransaction(ReadWriteMode.ReadWrite))
                {
                    // Let us try to create an entity
                    Transaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");
                    throw new Exception();
                }
            });

            using (MockModel.BeginTransaction())
            {
                ResultCursor result = Transaction.Run("Match (n:SampleEntity) Return n");
                Record record = result.FirstOrDefault()!;
                Assert.IsNull(record);
            }
        }

        [Test]
        public void EnsureEntityIsFlushedAfterTransaction()
        {
            using (MockModel.BeginTransaction(ReadWriteMode.ReadWrite))
            {
                Transaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");
                ResultCursor result = Transaction.Run("Match (n:SampleEntity) Return n");
                Record record = result.First();

                NodeResult loaded = record["n"].As<NodeResult>();
                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");

                Transaction.Flush();
            }

            using (MockModel.BeginTransaction())
            {
                ResultCursor result = Transaction.Run("Match (n:SampleEntity) Return n");
                Record record = result.FirstOrDefault()!;
                Assert.IsNull(record);
            }
        }

        [Test]
        public void EnsureEntityIsCreatedEvenFlushedWithoutTransaction()
        {
            using (MockModel.BeginTransaction())
            {
                Transaction.Run("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");
                ResultCursor result = Transaction.Run("Match (n:SampleEntity) Return n");
                Record record = result.First();

                NodeResult loaded = record["n"].As<NodeResult>();
                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");

                Transaction.Commit();
            }

            using (MockModel.BeginTransaction())
            {
                ResultCursor result = Transaction.Run("Match (n:SampleEntity) Return n");
                Record record = result.First();

                NodeResult loaded = record["n"].As<NodeResult>();
                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");
            }
        }

    }
}
