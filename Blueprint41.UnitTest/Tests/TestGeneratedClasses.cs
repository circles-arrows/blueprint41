using Blueprint41.Core;
using Blueprint41.DatastoreTemplates;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.Query;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.UnitTest.Helper;
using Blueprint41.UnitTest.Mocks;
using Datastore.Manipulation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Blueprint41.UnitTest.Tests
{
    [TestFixture]
    public class TestGeneratedClasses
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            MockNeo4JPersistenceProvider persistenceProvider = new MockNeo4JPersistenceProvider("bolt://localhost:7687", "neo4j", "neo");
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
                Neo4jTransaction.Run(reset);
            }
        }

        [Test]
        public void OGMImplCRUD()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                // Insert
                Person a;
                using (Transaction.Begin(true))
                {
                    a = new Person()
                    {
                        Name = "Joe Smith",
                        City = new City() { Name = "New York" }
                    };

                    Transaction.Commit();
                }

                string consoleOutput = output.GetOuput();

                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(CREATE \(inserted:Person {""Name"":""Joe Smith"",""LastModifiedOn"":)\d+}(\) SET inserted.Uid = key Return inserted)"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(CREATE \(inserted:City {""Name"":""New York"",""LastModifiedOn"":)\d+}(\) SET inserted.Uid = key Return inserted)"));
                Assert.That(consoleOutput, Contains.Substring(@"MATCH (in:Person {Uid:""2"" }), (out:City {Uid:""3"" })"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(CREATE UNIQUE \(in\)-\[:LIVES_IN {""CreationDate"":)\d+(\}\]->\(out\))"));

                Assert.IsInstanceOf<OGMImpl>(a);
                Assert.AreEqual(a.Name, "Joe Smith");
                Assert.AreEqual(a.City.Name, "New York");

                // Without transaction
                Assert.Throws<InvalidOperationException>(() => Person.Load("2"));

                Person b;
                using (Transaction.Begin(true))
                {
                    // Load
                    b = Person.Load("2");
                    Assert.AreEqual(a, b);

                    // Update
                    b.Name = "Jaden Smith";
                    Transaction.Commit();
                }

                consoleOutput = output.GetOuput();

                Assert.AreEqual(b.Name, "Jaden Smith");
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(node:Person\) WHERE node.Uid = ""2"" AND node.LastModifiedOn = )\d+( SET node = \{""Name"":""Jaden Smith"",""Uid"":""2"",""LastModifiedOn"":)\d+(\})"));

                Person c;
                using (Transaction.Begin(true))
                {
                    c = Person.Load("2");

                    c.Delete();
                    Transaction.Commit();
                }

                consoleOutput = output.GetOuput();

                Assert.That(consoleOutput, Contains.Substring(@"MATCH (item:Person)-[r:LIVES_IN]->(useless) WHERE item.Uid = ""2"" DELETE r"));
                Assert.That(consoleOutput, Contains.Substring(@"MATCH (item:Person)-[r:EATS_AT]->(useless) WHERE item.Uid = ""2"" DELETE r"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(node:Person\) WHERE node.Uid = ""2"" AND node.LastModifiedOn = )\d+( DELETE node)"));

                Person d;
                using (Transaction.Begin())
                {
                    // Load
                    d = Person.Load("2");
                    Assert.IsNull(d);
                }
            }
        }

        [Test]
        public void OGMImplCRUDWithRelationship()
        {
            string consoleOutput;
            using (ConsoleOutput output = new ConsoleOutput())
            {

                Person p1;
                City c1;
                Restaurant r1;

                using (Transaction.Begin(true))
                {
                    p1 = new Person()
                    {
                        Name = "Joe Smith"
                    };

                    c1 = new City()
                    {
                        Name = "New York"
                    };

                    r1 = new Restaurant()
                    {
                        Name = "Pizza House Inc."
                    };

                    p1.City = c1;
                    r1.City = c1;
                    p1.Restaurants.Add(r1);

                    Transaction.Commit();
                }

                Assert.AreEqual(p1.City, c1);
                Assert.AreEqual(r1.City, c1);
                Assert.AreEqual(p1.Restaurants[0], r1);

                consoleOutput = output.GetOuput();

                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(CREATE \(inserted:Person \{""Name"":""Joe Smith"",""LastModifiedOn"":)\d+(\}\) SET inserted.Uid = key Return inserted)"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(CREATE \(inserted:City \{""Name"":""New York"",""LastModifiedOn"":)\d+(\}\) SET inserted.Uid = key Return inserted)"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(CREATE \(inserted:Restaurant \{""Name"":""Pizza House Inc."",""LastModifiedOn"":)\d+(\}\) SET inserted.Uid = key Return inserted)"));

                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(in:Person \{Uid:""2"" \}\), \(out:City \{Uid:""3"" \}\))"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(CREATE UNIQUE \(in\)-\[:LIVES_IN \{""CreationDate"":)\d+(\}\]->\(out\))"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(in:Restaurant \{Uid:""4"" \}\), \(out:City \{Uid:""3"" \}\))"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(CREATE UNIQUE \(in\)-\[:LOCATED_AT \{""CreationDate"":)\d+(\}\]->\(out\))"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(in:Person \{Uid:""2"" \}\), \(out:Restaurant \{Uid:""4"" \}\))"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(CREATE UNIQUE \(in\)-\[:EATS_AT \{""CreationDate"":)\d+(\}\]->\(out\))"));
            }

            using (ConsoleOutput output = new ConsoleOutput())
            {
                using (Transaction.Begin(true))
                {
                    Person p2 = new Person()
                    {
                        Name = "Jane Smith",
                        City = new City { Name = "San Francisco" }
                    };
                    p2.Restaurants.Add(new Restaurant { Name = "Tadich Grill", City = p2.City });

                    Transaction.Commit();
                }

                consoleOutput = output.GetOuput();

                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(CREATE \(inserted:Person \{""Name"":""Jane Smith"",""LastModifiedOn"":)\d+(\}\) SET inserted.Uid = key Return inserted)"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(CREATE \(inserted:City \{""Name"":""San Francisco"",""LastModifiedOn"":)\d+(\}\) SET inserted.Uid = key Return inserted)"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(CREATE \(inserted:Restaurant \{""Name"":""Tadich Grill"",""LastModifiedOn"":)\d+(\}\) SET inserted.Uid = key Return inserted)"));

                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(in:Person \{Uid:""5"" \}\), \(out:City \{Uid:""6"" \}\))"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(CREATE UNIQUE \(in\)-\[:LIVES_IN \{""CreationDate"":)\d+(\}\]->\(out\))"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(in:Restaurant \{Uid:""7"" \}\), \(out:City \{Uid:""6"" \}\))"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(CREATE UNIQUE \(in\)-\[:LOCATED_AT \{""CreationDate"":)\d+(\}\]->\(out\))"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(in:Person \{Uid:""5"" \}\), \(out:Restaurant \{Uid:""7"" \}\))"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(CREATE UNIQUE \(in\)-\[:EATS_AT \{""CreationDate"":)\d+(\}\]->\(out\))"));
            }
        }
    }
}
