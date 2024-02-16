using Blueprint41.Core;
using Blueprint41.DatastoreTemplates;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.Neo4j.Persistence.Driver.Memgraph;
using Blueprint41.UnitTest.Memgraph.DataStore;
using Blueprint41.UnitTest.Memgraph.Helper;
using Blueprint41.UnitTest.Memgraph.Mocks;
using Datastore.Manipulation;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace Blueprint41.UnitTest.Memgraph.Tests
{
    [TestFixture]
    internal class TestOptimizeFor
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            MockNeo4jPersistenceProvider persistenceProvider = new MockNeo4jPersistenceProvider(DatabaseConnectionSettings.URI, DatabaseConnectionSettings.USER_NAME, DatabaseConnectionSettings.PASSWORD);
            PersistenceProvider.CurrentPersistenceProvider = persistenceProvider;

            TearDown();

            MockModel model = new MockModel();
            model.Execute(true);
        }

        [TearDown]
        public void TearDown()
        {
            using (Transaction.Begin())
            {
                string reset = "Match (n) detach delete n";
                Transaction.RunningTransaction.Run(reset);

                Transaction.Commit();
            }
            //using (Transaction.Begin())
            //{
            //    string clearSchema = "CALL apoc.schema.assert({},{},true) YIELD label, key RETURN *";
            //    Transaction.RunningTransaction.Run(clearSchema);
            //    Transaction.Commit();
            //}
        }


        [Test]
        public void TestOptimize()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                string key = null;

                string outputConsole;
                using (Transaction.Begin(true))
                {
                    Person p1 = new Person
                    {
                        Name = "Martin Sheen",
                    };

                    Person p2 = new Person
                    {
                        Name = "Michael Douglas",
                    };

                    Person p3 = new Person
                    {
                        Name = "Oliver Stone",
                    };

                    Person p4 = new Person
                    {
                        Name = "Rob Reiner",
                    };

                    Movie wallstreet = new Movie
                    {
                        Title = "Wall Street"
                    };

                    Movie tap = new Movie
                    {
                        Title = "The American President"
                    };

                    Movie st = new Movie
                    {
                        Title = "Starwars"
                    };

                    p1.ActedInMovies.Add(tap);
                    p1.ActedInMovies.Add(wallstreet);

                    p2.ActedInMovies.Add(tap);
                    p2.ActedInMovies.Add(wallstreet);

                    p3.DirectedMovies.Add(wallstreet);
                    p4.DirectedMovies.Add(tap);

                    Transaction.Commit();

                    key = p2.Uid;
                }

                using (Transaction.Begin(OptimizeFor.RecursiveSubGraphAccess))
                {
                    Person p = Person.Load(key);
                    Assert.Zero(p.DirectedMovies.Count);
                    Assert.Greater(p.ActedInMovies.Count, 0);
                    Assert.Greater(p.ActedInMovies[0].Actors.Count, 0);

                    outputConsole = output.GetOutput();

                    Assert.IsTrue(outputConsole.Contains(@"MATCH (node:Person) WHERE node.Uid = $key RETURN node"));
                    Assert.IsTrue(outputConsole.Contains(@"MATCH (node:Person)-[rel:DIRECTED_BY]->(out:Movie) WHERE node.Uid in ($keys)  RETURN node as Parent, out as Item"));
                    Assert.IsTrue(outputConsole.Contains(@"MATCH (node:Person)-[rel:ACTORS]->(out:Movie) WHERE node.Uid in ($keys)  RETURN node as Parent, out as Item"));
                    Assert.IsTrue(outputConsole.Contains(@"MATCH (node:Movie)<-[rel:ACTORS]-(out:Person) WHERE node.Uid in ($keys)  RETURN node as Parent, out as Item"));
                }

                using (Transaction.Begin(OptimizeFor.PartialSubGraphAccess))
                {
                    Person p = Person.Load(key);
                    Assert.Zero(p.DirectedMovies.Count);
                    Assert.Greater(p.ActedInMovies.Count, 0);
                    Assert.Greater(p.ActedInMovies[0].Actors.Count, 0);

                    outputConsole = output.GetOutput();

                    Assert.IsTrue(outputConsole.Contains(@"MATCH (node:Person) WHERE node.Uid = $key RETURN node"));
                    Assert.IsTrue(outputConsole.Contains(@"MATCH (node:Person)-[rel:DIRECTED_BY]->(out:Movie) WHERE node.Uid = $key RETURN out, rel"));
                    Assert.IsTrue(outputConsole.Contains(@"MATCH (node:Person)-[rel:ACTORS]->(out:Movie) WHERE node.Uid = $key RETURN out, rel"));
                    Assert.IsTrue(outputConsole.Contains(@"MATCH (node:Movie)<-[rel:ACTORS]-(out:Person) WHERE node.Uid = $key RETURN out, rel"));
                }
            }
        }
    }
}
