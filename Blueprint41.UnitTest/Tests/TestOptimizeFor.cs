using Blueprint41.Core;
using Blueprint41.DatastoreTemplates;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.Neo4j.Persistence.Driver.v5;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.UnitTest.Helper;
using Blueprint41.UnitTest.Mocks;
using Datastore.Manipulation;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace Blueprint41.UnitTest.Tests
{
    [TestFixture]
    internal class TestOptimizeFor
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            MockNeo4JPersistenceProvider persistenceProvider = new MockNeo4JPersistenceProvider("bolt://localhost:7689", "neo4j", "neo");
            PersistenceProvider.CurrentPersistenceProvider = persistenceProvider;

            TearDown();

            string projectFolder = Environment.CurrentDirectory + "\\..\\..\\..\\";
            GeneratorSettings settings = new GeneratorSettings(projectFolder);
            GeneratorResult result = Generator.Execute<MockModel>(settings);

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
        }


        [Test]
        public void TestOptimize()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
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
                }

                using (Transaction.Begin(OptimizeFor.RecursiveSubGraphAccess))
                {
                    Person p = Person.Load("2");
                    Assert.Zero(p.DirectedMovies.Count);
                    Assert.Greater(p.ActedInMovies.Count, 0);
                    Assert.Greater(p.ActedInMovies[0].Actors.Count, 0);

                    outputConsole = output.GetOuput();

                    Assert.IsTrue(Regex.IsMatch(outputConsole, @"(MATCH \(node:Person\) WHERE node.Uid = ""2"" RETURN node)"));
                    Assert.IsTrue(Regex.IsMatch(outputConsole, @"(MATCH \(node:Person\)-\[rel:DIRECTED_BY\]->\(out:Movie\) WHERE node\.Uid in \(\[""2""\]\) RETURN out, rel, node\.Uid as ParentKey)"));
                    Assert.IsTrue(Regex.IsMatch(outputConsole, @"(MATCH \(node:Person\)-\[rel:ACTORS\]->\(out:Movie\) WHERE node\.Uid in \(\[""2""\]\) RETURN out, rel, node\.Uid as ParentKey)"));
                    Assert.IsTrue(Regex.IsMatch(outputConsole, @"(MATCH \(node:Movie\)<-\[rel:ACTORS\]-\(out:Person\) WHERE node\.Uid in \(\["")\d+("","")\d+(""\]\) RETURN out, rel, node\.Uid as ParentKey)"));
                }

                using (Transaction.Begin(OptimizeFor.PartialSubGraphAccess))
                {
                    Person p = Person.Load("2");
                    Assert.Zero(p.DirectedMovies.Count);
                    Assert.Greater(p.ActedInMovies.Count, 0);
                    Assert.Greater(p.ActedInMovies[0].Actors.Count, 0);

                    outputConsole = output.GetOuput();

                    Assert.IsTrue(Regex.IsMatch(outputConsole, @"(MATCH \(node:Person\) WHERE node.Uid = ""2"" RETURN node)"));
                    Assert.IsTrue(Regex.IsMatch(outputConsole, @"(MATCH \(node:Person\)-\[rel:DIRECTED_BY\]->\(out:Movie\) WHERE node\.Uid = ""2"" RETURN out, rel)"));
                    Assert.IsTrue(Regex.IsMatch(outputConsole, @"(MATCH \(node:Person\)-\[rel:ACTORS\]->\(out:Movie\) WHERE node\.Uid = ""2"" RETURN out, rel)"));
                    Assert.IsTrue(Regex.IsMatch(outputConsole, @"(MATCH \(node:Movie\)<-\[rel:ACTORS\]-\(out:Person\) WHERE node\.Uid = "")\d("" RETURN out, rel)"));
                }
            }
        }
    }
}
