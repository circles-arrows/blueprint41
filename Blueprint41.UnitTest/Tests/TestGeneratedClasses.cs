using Blueprint41.Core;
using Blueprint41.DatastoreTemplates;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.Query;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.UnitTest.Helper;
using Blueprint41.UnitTest.Mocks;
using Datastore.Manipulation;
using Datastore.Query;
using node = Datastore.Query.Node;
using Neo4j.Driver;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Dynamic;

namespace Blueprint41.UnitTest.Tests
{
    [TestFixture]
    public class TestGeneratedClasses
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            MockNeo4JPersistenceProvider persistenceProvider = new MockNeo4JPersistenceProvider("bolt://localhost:7687", "neo4j", "neo4j");
            PersistenceProvider.CurrentPersistenceProvider = persistenceProvider;

            TearDown();

            string projectFolder = Environment.CurrentDirectory + "\\..\\..\\..\\";
            GeneratorSettings settings = new GeneratorSettings(projectFolder);
            GeneratorResult result = Generator.Execute<MockModel>(settings);

            MockModel model = new MockModel()
            {
                LogToConsole = true
            };
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
            using (Transaction.Begin())
            {
                string clearSchema = "CALL apoc.schema.assert({},{},true) YIELD label, key RETURN *";
                Transaction.RunningTransaction.Run(clearSchema);
                Transaction.Commit();
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
                Assert.That(consoleOutput, Contains.Substring(@"MATCH (in:Person {Uid:""2"" })"));
                Assert.That(consoleOutput, Contains.Substring(@"MATCH (out:City {Uid:""3"" })"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MERGE \(in\)-\[outr:LIVES_IN\]->\(out\) ON CREATE SET outr \+= {""CreationDate"":)\d+(\})"));

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

                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(in:Person \{Uid:""2"" \}\))[^a-zA-Z,0-9]*(MATCH \(out:City \{Uid:""3"" \}\))"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MERGE \(in\)-\[outr:LIVES_IN\]->\(out\) ON CREATE SET outr \+= \{""CreationDate"":)\d+(\})"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(in:Restaurant \{Uid:""4"" \}\))[^a-zA-Z,0-9]*(MATCH \(out:City \{Uid:""3"" \}\))"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MERGE \(in\)-\[outr:LOCATED_AT\]->\(out\) ON CREATE SET outr \+= \{""CreationDate"":)\d+(\})"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(in:Person \{Uid:""2"" \}\))[^a-zA-Z,0-9]*(MATCH \(out:Restaurant \{Uid:""4"" \}\))"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MERGE \(in\)-\[outr:EATS_AT\]->\(out\) ON CREATE SET outr \+= \{""CreationDate"":)\d+(\})"));

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

                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(in:Person \{Uid:""5"" \}\))[^a-zA-Z,0-9]*(MATCH \(out:City \{Uid:""6"" \}\))"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MERGE \(in\)-\[outr:LIVES_IN\]->\(out\) ON CREATE SET outr \+= \{""CreationDate"":)\d+(\})"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(in:Restaurant \{Uid:""7"" \}\))[^a-zA-Z,0-9]*(MATCH \(out:City \{Uid:""6"" \}\))"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MERGE \(in\)-\[outr:LOCATED_AT\]->\(out\) ON CREATE SET outr \+= \{""CreationDate"":)\d+(\})"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(in:Person \{Uid:""5"" \}\))[^a-zA-Z,0-9]*(MATCH \(out:Restaurant \{Uid:""7"" \}\))"));
                Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MERGE \(in\)-\[outr:EATS_AT\]->\(out\) ON CREATE SET outr \+= \{""CreationDate"":)\d+(\})"));

                // Update

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

                // Removing relationships by setting
                using (Transaction.Begin(true))
                {
                    Person p = Person.Load("5");
                    p.City = null;
                    p.Restaurants.Clear();

                    Transaction.Flush();

                    Assert.IsNull(p.City);
                    Assert.IsTrue(p.Restaurants.Count == 0);

                    consoleOutput = output.GetOuput();

                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(item:Person\)-\[r:LIVES_IN\]->\(useless\) WHERE item.Uid = ""5"" DELETE r)"));
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(item:Person\)-\[r:EATS_AT\]->\(useless\) WHERE item.Uid = ""5"" DELETE r)"));

                    Transaction.Rollback();
                }

                // Removing relationships via properties
                using (Transaction.Begin(true))
                {
                    Person p = Person.Load("5");
                    p.City.Delete();
                    p.Restaurants[0].Delete();

                    Transaction.Flush();

                    consoleOutput = output.GetOuput();

                    Assert.IsNull(p.City);
                    Assert.IsTrue(p.Restaurants.Count == 0);

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

                // Removing node with existing relationship
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

        [Test]
        public void OGMImplQuery()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                using (Transaction.Begin(true))
                {
                    Person p1 = new Person
                    {
                        Name = "Joe Smith",
                        City = new City() { Name = "New York" }
                    };

                    p1.City.Restraurants.Add(new Restaurant { Name = "Mcdonalds" });
                    p1.City.Restraurants.Add(new Restaurant { Name = "Shakeys" });
                    p1.City.Restraurants.Add(new Restaurant { Name = "Starbucks" });
                    p1.City.Restraurants.Add(new Restaurant { Name = "Bo's Coffee" });
                    p1.City.Restraurants.Add(new Restaurant { Name = "Chattime" });

                    Person p2 = new Person
                    {
                        Name = "Jane Smith",
                        City = new City() { Name = "California" }
                    };

                    Person p3 = new Person
                    {
                        Name = "Bob Smith",
                        City = p1.City
                    };

                    p2.City.Restraurants.Add(new Restaurant { Name = "Pink's Hot Dogs" });
                    p2.City.Restraurants.Add(new Restaurant { Name = "World Famous" });
                    p2.City.Restraurants.Add(new Restaurant { Name = "Barone's" });
                    p2.City.Restraurants.Add(new Restaurant { Name = "Providence" });
                    p2.City.Restraurants.Add(new Restaurant { Name = "La Taqueria" });

                    p1.Restaurants.Add(p1.City.Restraurants[0]);
                    p1.Restaurants.Add(p1.City.Restraurants[1]);
                    p1.Restaurants.Add(p1.City.Restraurants[2]);
                    p1.Restaurants.Add(p1.City.Restraurants[3]);
                    p1.Restaurants.Add(p1.City.Restraurants[4]);

                    //p1.Restaurants.Add(p2.City.Restraurants[0]);
                    //p1.Restaurants.Add(p2.City.Restraurants[1]);
                    //p1.Restaurants.Add(p2.City.Restraurants[2]);
                    //p1.Restaurants.Add(p2.City.Restraurants[3]);
                    //p1.Restaurants.Add(p2.City.Restraurants[4]);

                    p2.Restaurants.Add(p1.City.Restraurants[0]);
                    p2.Restaurants.Add(p1.City.Restraurants[1]);
                    p2.Restaurants.Add(p1.City.Restraurants[2]);
                    p2.Restaurants.Add(p1.City.Restraurants[3]);
                    p2.Restaurants.Add(p1.City.Restraurants[4]);

                    p2.Restaurants.Add(p2.City.Restraurants[0]);
                    p2.Restaurants.Add(p2.City.Restraurants[1]);
                    p2.Restaurants.Add(p2.City.Restraurants[2]);
                    p2.Restaurants.Add(p2.City.Restraurants[2]);
                    p2.Restaurants.Add(p2.City.Restraurants[3]);
                    p2.Restaurants.Add(p2.City.Restraurants[4]);


                    Transaction.Commit();
                }
            }
            string queryString;
            using (Transaction.Begin())
            {
                ICompiled compiled = Transaction.CompiledQuery
                    .Match(node.Person.Alias(out PersonAlias p))
                    .With(p, Functions.CollectSubquery<StringListResult>(sq => 
                        sq.Match
                        (
                            p
                                .In.PERSON_EATS_AT.Out.
                            Restaurant.Alias(out var restaurantAlias)
                        )
                        .Where(Functions.CountSubquery(sq => sq.Match(p.In.PERSON_EATS_AT.Out.Restaurant), Transaction.CompiledQuery) == 5)
                        .Return(restaurantAlias.Name), Transaction.CompiledQuery)
                        .As("restaurants", out var restaurants)
                    )
                    //.WhereExistsSubQuery(sq => sq.Match(p.In.PERSON_EATS_AT.Out.Restaurant))
                    //.Where(Functions.ExistsSubquery(sq => sq.Match(p.In.PERSON_EATS_AT.Out.Restaurant), Transaction.CompiledQuery) == true)
                    .Return(p, restaurants)
                    .Compile();
                var result = compiled.GetExecutionContext().Execute();
                List<Person> searchResult = Person.LoadWhere(compiled);
                Assert.Greater(searchResult.Count, 0);

                queryString = compiled.ToString();

                Assert.IsTrue(Regex.IsMatch(queryString, @"(MATCH \(n0:Person\))[^a-zA-Z,0-9]*(WHERE \(n0\.Name CONTAINS 'Smith'\)[^a-zA-Z,0-9]*RETURN DISTINCT n0 AS Column1)"));

                compiled = Transaction.CompiledQuery
                    .Match(node.Person.Alias(out PersonAlias pWithLimit))
                    .Where(pWithLimit.Name.Contains("Smith"))
                    .Return(pWithLimit)
                    .Limit(1)
                    .Compile();

                searchResult = Person.LoadWhere(compiled);
                Assert.AreEqual(searchResult.Count, 1);

                queryString = compiled.ToString();

                Assert.IsTrue(Regex.IsMatch(queryString, @"(MATCH \(n0:Person\))[^a-zA-Z,0-9]*(WHERE \(n0.Name CONTAINS 'Smith'\)[^a-zA-Z,0-9]*RETURN DISTINCT n0 AS Column1)[^a-zA-Z,0-9]*(LIMIT 1)"));

                compiled = Transaction.CompiledQuery
                    .Match(node.Person.Alias(out var pR).In.PERSON_EATS_AT.Out.Restaurant.Alias(out var rP))
                    .Where(rP.Name == "Shakeys")
                    .Return(pR)
                    .OrderBy(pR.Name)
                    .Compile();

                searchResult = Person.LoadWhere(compiled);
                Assert.AreEqual(searchResult.Count, 2);

                Assert.AreEqual(searchResult[0].Name, "Jane Smith");
                Assert.AreEqual(searchResult[1].Name, "Joe Smith");

                queryString = compiled.ToString();

                Assert.IsTrue(Regex.IsMatch(queryString, @"(MATCH \(n0:Person\)-\[:EATS_AT\]->\(n1:Restaurant\))[^a-zA-Z,0-9]*(WHERE \(n1\.Name = 'Shakeys'\))[^a-zA-Z,0-9]*(RETURN DISTINCT n0 AS Column1)[^a-zA-Z,0-9]*(ORDER BY n0.Name)"));
            }
        }

        [Test]
        public void OGMImplQueryOptionalMatch()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
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

                    p1.ActedInMovies.Add(tap);
                    p1.ActedInMovies.Add(wallstreet);

                    p2.ActedInMovies.Add(tap);
                    p2.ActedInMovies.Add(wallstreet);

                    p3.DirectedMovies.Add(wallstreet);
                    p4.DirectedMovies.Add(tap);

                    Transaction.Commit();
                }
            }

            using (ConsoleOutput output = new ConsoleOutput())
            {
                string outputConsole;
                using (Transaction.Begin())
                {
                    ICompiled compiled = Transaction.CompiledQuery
                                .Match(node.Person.Alias(out PersonAlias p))
                                .Where(p.Name.Contains("Martin Sheen"))
                                .OptionalMatch(node.Movie.Alias(out MovieAlias m))
                                .Return(m.Title)
                                .OrderBy(m.Title)
                                .Compile();

                    var result = compiled.GetExecutionContext().Execute();

                    var a = result[0] as IDictionary<string, object>;
                    var b = result[1] as IDictionary<string, object>;

                    Assert.AreEqual(a["Column1"], "The American President");
                    Assert.AreEqual(b["Column1"], "Wall Street");

                    outputConsole = output.GetOuput();

                    Assert.IsTrue(Regex.IsMatch(outputConsole, @"(MATCH \(n0:Person\))[^a-zA-Z,0-9]*(WHERE \(n0\.Name CONTAINS ""Martin Sheen""\))[^a-zA-Z,0-9]*(OPTIONAL MATCH \(n1:Movie\))[^a-zA-Z,0-9]*(RETURN DISTINCT n1\.Title AS Column1)[^a-zA-Z,0-9]*(ORDER BY n1\.Title)"));

                    compiled = Transaction.CompiledQuery
                            .Match(node.Person.Alias(out PersonAlias pa))
                            .Where(pa.Name.Contains("Martin Sheen"))
                            .OptionalMatch(pa.In.PERSON_DIRECTED.Out.Movie.Alias(out MovieAlias ma))
                            .Return(pa.Name, ma.Title)
                            .OrderBy(ma.Title)
                            .Compile();

                    result = compiled.GetExecutionContext().Execute();

                    a = result[0] as IDictionary<string, object>;
                    Assert.AreEqual(a["Column1"], "Martin Sheen");
                    Assert.IsNull(a["Column2"]);

                    outputConsole = output.GetOuput();
                    Assert.IsTrue(Regex.IsMatch(outputConsole, @"(MATCH \(n0:Person\))[^a-zA-Z,0-9]*(WHERE \(n0\.Name CONTAINS ""Martin Sheen""\))[^a-zA-Z,0-9]*(OPTIONAL MATCH \(n0\)-\[\:DIRECTED_BY\]\-\>\(n1:Movie\))[^a-zA-Z,0-9]*(RETURN DISTINCT n0\.Name AS Column1, n1\.Title AS Column2)[^a-zA-Z,0-9]*(ORDER BY n1\.Title)"));

                    compiled = Transaction.CompiledQuery
                                .Match(node.Person.Alias(out PersonAlias pap).In.PERSON_DIRECTED.Out.Movie.Alias(out MovieAlias mam))
                                .Where(pap.Name.Contains("Martin Sheen"))
                                .Return(mam.Title)
                                .OrderBy(mam.Title)
                                .Compile();

                    result = compiled.GetExecutionContext().Execute();
                    Assert.Zero(result.Count);

                    outputConsole = output.GetOuput();
                    Assert.IsTrue(Regex.IsMatch(outputConsole, @"(MATCH \(n0:Person\)-\[\:DIRECTED_BY\]\-\>\(n1:Movie\))[^a-zA-Z,0-9]*(WHERE \(n0\.Name CONTAINS ""Martin Sheen""\))[^a-zA-Z,0-9]*(RETURN DISTINCT n1\.Title AS Column1)[^a-zA-Z,0-9]*(ORDER BY n1\.Title)"));
                }
            }
        }

        [Test]
        public void OGMImplPlannerHitsUsing()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
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

                    p1.ActedInMovies.Add(tap);
                    p1.ActedInMovies.Add(wallstreet);

                    p2.ActedInMovies.Add(tap);
                    p2.ActedInMovies.Add(wallstreet);

                    p3.DirectedMovies.Add(wallstreet);
                    p4.DirectedMovies.Add(tap);

                    Transaction.Commit();
                }
            }

            using (ConsoleOutput output = new ConsoleOutput())
            {
                string consoleOutput;
                using (Transaction.Begin())
                {
                    // Force to use index
                    ICompiled compiled = Transaction.CompiledQuery
                            .Match(node.Movie.Alias(out MovieAlias m))
                            .UsingIndex(m.Title)
                            .Where(m.Title == "Wall Street")
                            .Return(m.Title)
                            .Compile();

                    var result = compiled.GetExecutionContext().Execute();

                    var a = result[0] as IDictionary<string, object>;
                    Assert.AreEqual(a["Column1"], "Wall Street");

                    consoleOutput = output.GetOuput();
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(n0:Movie\))[^a-zA-Z,0-9]*(USING INDEX n0:Movie\(Title\))[^a-zA-Z,0-9]*(WHERE \(n0.Title = ""Wall Street""\))[^a-zA-Z,0-9]*(RETURN DISTINCT n0.Title AS Column1)"));

                    // With relationship
                    compiled = Transaction.CompiledQuery
                                .Match(node.Movie.Alias(out MovieAlias ma).Out.PERSON_DIRECTED.In.Person.Alias(out PersonAlias p))
                                .UsingIndex(ma.Title)
                                .Where(ma.Title == "Wall Street")
                                .Return(ma.Title, p.Name)
                                .Compile();

                    result = compiled.GetExecutionContext().Execute();

                    a = result[0] as IDictionary<string, object>;
                    Assert.AreEqual(a["Column1"], "Wall Street");
                    Assert.AreEqual(a["Column2"], "Oliver Stone");

                    consoleOutput = output.GetOuput();
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(n0:Movie\)<-\[:DIRECTED_BY\]-\(n1:Person\))[^a-zA-Z,0-9]*(USING INDEX n0:Movie\(Title\))[^a-zA-Z,0-9]*(WHERE \(n0.Title = ""Wall Street""\))[^a-zA-Z,0-9]*(RETURN DISTINCT n0.Title AS Column1, n1.Name AS Column2)"));

                    // Use label scan
                    compiled = Transaction.CompiledQuery
                            .Match(node.Movie.Alias(out MovieAlias mas))
                            .UsingScan(mas)
                            .Where(mas.Title == "Wall Street")
                            .Return(mas.Title)
                            .Compile();

                    result = compiled.GetExecutionContext().Execute();

                    a = result[0] as IDictionary<string, object>;
                    Assert.AreEqual(a["Column1"], "Wall Street");

                    consoleOutput = output.GetOuput();
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(n0:Movie\))[^a-zA-Z,0-9]*(USING SCAN n0:Movie)[^a-zA-Z,0-9]*(WHERE \(n0.Title = ""Wall Street""\))[^a-zA-Z,0-9]*(RETURN DISTINCT n0.Title AS Column1)"));

                    // use label scan with relationship
                    compiled = Transaction.CompiledQuery
                            .Match(node.Movie.Alias(out MovieAlias mar).Out.PERSON_DIRECTED.In.Person.Alias(out PersonAlias par))
                            .UsingScan(mar)
                            .UsingScan(par)
                            .Where(mar.Title == "Wall Street")
                            .Return(mar.Title, par.Name)
                            .Compile();

                    result = compiled.GetExecutionContext().Execute();

                    a = result[0] as IDictionary<string, object>;
                    Assert.AreEqual(a["Column1"], "Wall Street");
                    Assert.AreEqual(a["Column2"], "Oliver Stone");

                    consoleOutput = output.GetOuput();
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(n0:Movie\)<-\[:DIRECTED_BY\]-\(n1:Person\))[^a-zA-Z,0-9]*(USING SCAN n0:Movie)[^a-zA-Z,0-9]*(USING SCAN n1:Person)[^a-zA-Z,0-9]*(WHERE \(n0.Title = ""Wall Street""\))[^a-zA-Z,0-9]*(RETURN DISTINCT n0.Title AS Column1, n1.Name AS Column2)"));

                    // use label scan and index
                    compiled = Transaction.CompiledQuery
                            .Match(node.Movie.Alias(out MovieAlias msi).Out.PERSON_DIRECTED.In.Person.Alias(out PersonAlias psi))
                            .UsingIndex(msi.Title)
                            .UsingScan(psi)
                            .Where(msi.Title == "Wall Street")
                            .Return(msi.Title, psi.Name)
                            .Compile();

                    result = compiled.GetExecutionContext().Execute();

                    a = result[0] as IDictionary<string, object>;
                    Assert.AreEqual(a["Column1"], "Wall Street");
                    Assert.AreEqual(a["Column2"], "Oliver Stone");

                    consoleOutput = output.GetOuput();
                    Assert.IsTrue(Regex.IsMatch(consoleOutput, @"(MATCH \(n0:Movie\)<-\[:DIRECTED_BY\]-\(n1:Person\))[^a-zA-Z,0-9]*(USING INDEX n0:Movie\(Title\))[^a-zA-Z,0-9]*(USING SCAN n1:Person)[^a-zA-Z,0-9]*(WHERE \(n0.Title = ""Wall Street""\))[^a-zA-Z,0-9]*(RETURN DISTINCT n0.Title AS Column1, n1.Name AS Column2)"));
                }
            }
        }
    }
}
