#pragma warning disable CS8981 // Names should not be lower type only

using System;
using System.Linq;

using NUnit.Framework;

using Blueprint41.Core;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.Persistence;
using System.Threading.Tasks;

namespace Blueprint41.UnitTest.Tests.Async
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
            InvalidOperationException exception = Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return Transaction.RunAsync("CREATE (n:Person { name: 'Address', title: 'Developer' })");
            });

            Assert.That(exception.Message, Contains.Substring("There is no transaction, you should create one first -> using (DatastoreModel.BeginTransaction()) { ... Transaction.Commit(); }"));
        }

        [Test]
        public async Task EnsureRunningTransactionIsNeo4jTransaction()
        {
            await using (MockModel.BeginTransactionAsync())
                Assert.IsInstanceOf<Transaction>(Transaction.RunningTransaction);
        }

        [Test]
        public void EnsureNotAbleToTransactAfterCommit()
        {
            InvalidOperationException exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
                {
                    // Let us try to create an entity
                    await Transaction.RunAsync("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");

                    await Transaction.CommitAsync();

                    // This statement should throw invalid operation exception
                    ResultCursor result = await Transaction.RunAsync("Match (n:SampleEntity) Return n");
                    Record record = await result.FirstAsync();
                    NodeResult loaded = record["n"].As<NodeResult>();

                    Assert.AreEqual(loaded.Properties["name"], "Address");
                    Assert.AreEqual(loaded.Properties["title"], "Developer");
                }
            });

            Assert.That(exception.Message, Contains.Substring("The transaction was already committed or rolled back."));
        }

        [Test]
        public async Task EnsureCanCreateAnEntity()
        {
            await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
            {
                // Let us try to create an entity
                await Transaction.RunAsync("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");

                ResultCursor result = await Transaction.RunAsync("Match (n:SampleEntity) Return n");
                Record record = await result.FirstAsync();
                NodeResult loaded = record["n"].As<NodeResult>();

                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");

                await Transaction.CommitAsync();
            }
        }

        [Test]
        public async Task EnsureEntityShouldNotBeAddedAfterRollback()
        {
            await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
            {
                // Let us try to create an entity
                await Transaction.RunAsync("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");

                ResultCursor result = await Transaction.RunAsync("Match (n:SampleEntity) Return n");
                Record record = await result.FirstAsync();
                NodeResult loaded = record["n"].As<NodeResult>();

                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");

                await Transaction.RollbackAsync();
            }

            await using (MockModel.BeginTransactionAsync())
            {
                ResultCursor result = await Transaction.RunAsync("Match (n:SampleEntity) Return n");
                Record? record = await result.FirstOrDefaultAsync();
                Assert.IsNull(record);
            }
        }

        [Test]
        public void EnsureEntityShouldNotBeRollbackedAfterCommitedAndViceVersa()
        {
            InvalidOperationException exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
                {
                    // Let us try to create an entity
                    await Transaction.RunAsync("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");

                    ResultCursor result = await Transaction.RunAsync("Match (n:SampleEntity) Return n");
                    Record record = await result.FirstAsync();
                    NodeResult loaded = record["n"].As<NodeResult>();

                    Assert.AreEqual(loaded.Properties["name"], "Address");
                    Assert.AreEqual(loaded.Properties["title"], "Developer");

                    await Transaction.CommitAsync();
                    await Transaction.RollbackAsync();
                }
            });

            Assert.That(exception.Message, Contains.Substring("The transaction was already committed or rolled back."));

            InvalidOperationException exception2 = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
                {
                    // Let us try to create an entity
                    await Transaction.RunAsync("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");

                    ResultCursor result = await Transaction.RunAsync("Match (n:SampleEntity) Return n");
                    Record record = await result.FirstAsync();
                    NodeResult loaded = record["n"].As<NodeResult>();

                    Assert.AreEqual(loaded.Properties["name"], "Address");
                    Assert.AreEqual(loaded.Properties["title"], "Developer");

                    await Transaction.RollbackAsync();
                    await Transaction.CommitAsync();
                }
            });

            Assert.That(exception2.Message, Contains.Substring("The transaction was already committed or rolled back."));
        }

        [Test]
        public async Task EnsureEntityIsCreatedRegardlessAnExceptionIsThrown()
        {
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await using (MockModel.BeginTransactionAsync())
                {
                    // Let us try to create an entity
                    await Transaction.RunAsync("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");
                    await Transaction.CommitAsync();
                    throw new Exception();
                }
            });

            await using (MockModel.BeginTransactionAsync())
            {
                ResultCursor result = await Transaction.RunAsync("Match (n:SampleEntity) Return n");
                Record record = await result.FirstAsync();
                NodeResult loaded = record["n"].As<NodeResult>();

                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");
            }
        }

        [Test]
        public async Task EnsureEntityIsRolledbackWhenExceptionIsThrown()
        {
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
                {
                    // Let us try to create an entity
                    await Transaction.RunAsync("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");
                    throw new Exception();
                }
            });

            await using (MockModel.BeginTransactionAsync())
            {
                ResultCursor result = await Transaction.RunAsync("Match (n:SampleEntity) Return n");
                Record? record = await result.FirstOrDefaultAsync();
                Assert.IsNull(record);
            }
        }

        [Test]
        public async Task EnsureEntityIsFlushedAfterTransaction()
        {
            await using (MockModel.BeginTransactionAsync (ReadWriteMode.ReadWrite))
            {
                await Transaction.RunAsync("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");
                ResultCursor result = await Transaction.RunAsync("Match (n:SampleEntity) Return n");
                Record record = await result.FirstAsync();

                NodeResult loaded = record["n"].As<NodeResult>();
                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");

                await Transaction.FlushAsync();
            }

            await using (MockModel.BeginTransactionAsync())
            {
                ResultCursor result = await Transaction.RunAsync("Match (n:SampleEntity) Return n");
                Record? record = await result.FirstOrDefaultAsync();
                Assert.IsNull(record);
            }
        }

        [Test]
        public async Task EnsureEntityIsCreatedEvenFlushedWithoutTransaction()
        {
            await using (MockModel.BeginTransactionAsync())
            {
                await Transaction.RunAsync("CREATE (n:SampleEntity { name: 'Address', title: 'Developer' })");
                ResultCursor result = await Transaction.RunAsync("Match (n:SampleEntity) Return n");
                Record record = await result.FirstAsync();

                NodeResult loaded = record["n"].As<NodeResult>();
                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");

                await Transaction.CommitAsync();
            }

            await using (MockModel.BeginTransactionAsync())
            {
                ResultCursor result = await Transaction.RunAsync("Match (n:SampleEntity) Return n");
                Record record = await result.FirstAsync();

                NodeResult loaded = record["n"].As<NodeResult>();
                Assert.AreEqual(loaded.Properties["name"], "Address");
                Assert.AreEqual(loaded.Properties["title"], "Developer");
            }
        }
    }
}
