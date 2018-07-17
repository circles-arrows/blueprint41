using Blueprint41.Core;
using Blueprint41.DatastoreTemplates;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.Query;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.UnitTest.Helper;
using Blueprint41.UnitTest.Mocks;
using Datastore.Manipulation;
using Neo4j.Driver.V1;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

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

                // adding relationships per entity
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

            // adding relationships directly
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

            // Update
            using (ConsoleOutput output = new ConsoleOutput())
            {
                using (Transaction.Begin(true))
                {

                    Person p = Person.Load("5");
                    p.Name = "Janice Smith";
                    p.City.Name = "California";
                    p.Restaurants[0].Name = "Shakeys Pizza";

                    Transaction.Commit();

                    consoleOutput = output.GetOuput();

                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(node:Person\) WHERE node.Uid = ""5"" RETURN node)"));
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(node:Person\)-\[rel:LIVES_IN\]->\(out:City\) WHERE node.Uid = ""5"" RETURN out, rel)"));
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(node:Person\)-\[rel:EATS_AT\]->\(out:Restaurant\) WHERE node.Uid = ""5"" RETURN out, rel)"));
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(node:Person\) WHERE node.Uid = ""5"" AND node.LastModifiedOn = )\d+( SET node = \{""Name"":""Janice Smith"",""Uid"":""5"",""LastModifiedOn"":)\d+(\})"));
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(node:City\) WHERE node.Uid = ""6"" AND node.LastModifiedOn = )\d+( SET node = \{""Name"":""California"",""Uid"":""6"",""LastModifiedOn"":)\d+(\})"));
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(node:Restaurant\) WHERE node.Uid = ""7"" AND node.LastModifiedOn = )\d+( SET node = \{""Name"":""Shakeys Pizza"",""Uid"":""7"",""LastModifiedOn"":)\d+(\})"));
                }

                using (Transaction.Begin())
                {
                    Person p = Person.Load("5");
                    City c = City.Load("6");
                    Restaurant r = Restaurant.Load("7");

                    Assert.AreEqual(p.Name, "Janice Smith");
                    Assert.AreEqual(c.Name, "California");
                    Assert.AreEqual(r.Name, "Shakeys Pizza");
                }
            }

            // Removing relationships
            using (ConsoleOutput output = new ConsoleOutput())
            {
                using (Transaction.Begin(true))
                {
                    Person p = Person.Load("5");
                    p.City = null;
                    p.Restaurants.Clear();

                    Transaction.Flush();

                    consoleOutput = output.GetOuput();

                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(item:Person\)-\[r:LIVES_IN\]->\(useless\) WHERE item.Uid = ""5"" DELETE r)"));
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(item:Person\)-\[r:EATS_AT\]->\(useless\) WHERE item.Uid = ""5"" DELETE r)"));

                    Transaction.Rollback();
                }
            }

            // Removing relationships via properties
            using (ConsoleOutput output = new ConsoleOutput())
            {
                using (Transaction.Begin(true))
                {
                    Person p = Person.Load("5");
                    p.City.Delete();
                    p.Restaurants[0].Delete();

                    Transaction.Flush();

                    consoleOutput = output.GetOuput();

                    // Removing Person -> City Relationship
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(item:City\)-\[r:LIVES_IN\]->\(useless\) WHERE item.Uid = ""6"" DELETE r)"));
                    // Removing City -> Restaurant Relationship
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(item:City\)-\[r:LOCATED_AT\]->\(useless\) WHERE item.Uid = ""6"" DELETE r)"));

                    // Removing Restaurant -> City Relationship
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(item:Restaurant\)-\[r:LOCATED_AT\]->\(useless\) WHERE item.Uid = ""7"" DELETE r)"));
                    // Removing Person -> Restaurant Relationship
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(item:Restaurant\)-\[r:EATS_AT\]->\(useless\) WHERE item.Uid = ""7"" DELETE r)"));

                    // Deleting the node
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(node:City\) WHERE node.Uid = ""6"" AND node.LastModifiedOn = )\d+( DELETE node)"));
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(node:City\) WHERE node.Uid = ""6"" AND node.LastModifiedOn = )\d+( DELETE node)"));

                    Transaction.Rollback();
                }
            }

            // Removing node with existing relationship
            using (ConsoleOutput output = new ConsoleOutput())
            {
                using (Transaction.Begin(true))
                {
                    //load before deleting
                    Person p = Person.Load("5");

                    City.Load("6").Delete();

                    //load after deleting
                    Restaurant r = Restaurant.Load("7");

                    Assert.IsNull(p.City);
                    Assert.IsNull(r.City);

                    Transaction.Flush();

                    consoleOutput = output.GetOuput();

                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(node:City\) WHERE node.Uid = ""6"" RETURN node)"));
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(item:City\)-\[r:LIVES_IN\]->\(useless\) WHERE item.Uid = ""6"" DELETE r)"));
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(item:City\)-\[r:LOCATED_AT\]->\(useless\) WHERE item.Uid = ""6"" DELETE r)"));
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(node:City\) WHERE node.Uid = ""6"" AND node.LastModifiedOn = )\d+( DELETE node)"));

                    Transaction.Rollback();
                }
            }
        }
    }
}
