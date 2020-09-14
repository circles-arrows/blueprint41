using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.UnitTest.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.UnitTest.Tests
{
    [TestFixture]
    public abstract class TestBase
    {
        [SetUp]
        public void Setup()
        {
            MockNeo4JPersistenceProvider persistenceProvider = new MockNeo4JPersistenceProvider("bolt://localhost:7687", "neo4j", "neo");
            PersistenceProvider.CurrentPersistenceProvider = persistenceProvider;

            TearDown();
        }


        [TearDown]
        public void TearDown()
        {
            using (Transaction.Begin())
            {
                string reset = "Match (n) detach delete n";
                Transaction.RunningTransaction.Run(reset);
            }
        }
    }
}
