using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Blueprint41.Core;
using Blueprint41.DatastoreTemplates;
using Blueprint41.Query;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.UnitTest.Helper;
using Blueprint41.UnitTest.Mocks;

using Datastore.Manipulation;
using Datastore.Query;

using NUnit.Framework;

using node = Datastore.Query.Node;

namespace Blueprint41.UnitTest.Tests
{
    [TestFixture]
    public class TestGeneratedClasses
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            MockNeo4jPersistenceProvider persistenceProvider = new MockNeo4jPersistenceProvider(DatabaseConnectionSettings.URI, DatabaseConnectionSettings.USER_NAME, DatabaseConnectionSettings.PASSWORD);
            PersistenceProvider.CurrentPersistenceProvider = persistenceProvider;

            TearDown();
        }

        [SetUp]
        public void Setup()
        {
            // Run mock model every time because the FunctionalId is wiped out by cleanup and needs to be recreated!
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
#if NEO4J
            using (Transaction.Begin())
            {
                string clearSchema = "CALL apoc.schema.assert({},{},true) YIELD label, key RETURN *";
                Transaction.RunningTransaction.Run(clearSchema);
                Transaction.Commit();
            }
#elif MEMGRAPH
            string clearSchema = "CALL schema.assert({},{}) YIELD label, key RETURN *";
            PersistenceProvider.CurrentPersistenceProvider.Run(clearSchema);
#endif
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

                output.AssertNodeCreated("Person");
                output.AssertNodeCreated("City");
                output.AssertTimeDependentRelationshipCreated("Person", "LIVES_IN", "City");

                Assert.IsInstanceOf<OGMImpl>(a);
                Assert.AreEqual(a.Name, "Joe Smith");
                Assert.AreEqual(a.City.Name, "New York");

                // Database assigned a valid Uid
                string key = GetAndCheckKey(a);

                // Without transaction
                Assert.Throws<InvalidOperationException>(() => Person.Load(key));

                Person b;
                using (Transaction.Begin(true))
                {
                    // Load
                    b = Person.Load(key);
                    Assert.AreEqual(a, b);

                    // Update
                    b.Name = "Jaden Smith";
                    Transaction.Commit();
                }

                Assert.AreEqual(b.Name, "Jaden Smith");
                output.AssertNodeUpdated("Person");

                Person c;
                using (Transaction.Begin(true))
                {
                    c = Person.Load(key);

                    c.Delete();
                    Transaction.Commit();
                }

                output.AssertNodeDeleted("Person");
                output.AssertTimeDependentRelationshipDeleted("Person", "LIVES_IN", "City");
                output.AssertRelationshipDeleted("Person", "EATS_AT", "Restaurant");

                Person d;
                using (Transaction.Begin())
                {
                    // Load
                    d = Person.Load(key);
                    Assert.IsNull(d);
                }
            }
        }

        [Test]
        public void OGMImplCRUDWithRelationship()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {

                Person p1, p2;
                City c1, c2;
                Restaurant r1, r2;

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

                    Transaction.Flush();

                    Assert.AreEqual(p1.City, c1);
                    Assert.AreEqual(r1.City, c1);
                    Assert.AreEqual(p1.Restaurants[0], r1);

                    Transaction.Commit();
                }


                output.AssertNodeCreated("Person");
                output.AssertNodeCreated("City");
                output.AssertNodeCreated("Restaurant");
                output.AssertTimeDependentRelationshipCreated("Person", "LIVES_IN", "City");
                output.AssertRelationshipCreated("Restaurant", "LOCATED_AT", "City");
                output.AssertRelationshipCreated("Person", "EATS_AT", "Restaurant");

                // Database assigned a valid Uids
                string key2 = GetAndCheckKey(p1); 
                string key3 = GetAndCheckKey(c1); 
                string key4 = GetAndCheckKey(r1);


                using (Transaction.Begin(true))
                {
                    p2 = new Person()
                    {
                        Name = "Jane Smith",
                    };

                    c2 = new City()
                    {
                        Name = "San Francisco"
                    };

                    r2 = new Restaurant
                    {
                        Name = "Tadich Grill",
                    };

                    p2.City = c2;
                    r2.City = c2;
                    p2.Restaurants.Add(r2);

                    Transaction.Commit();
                }

                output.AssertNodeCreated("Person");
                output.AssertNodeCreated("City");
                output.AssertNodeCreated("Restaurant");
                output.AssertTimeDependentRelationshipCreated("Person", "LIVES_IN", "City");
                output.AssertRelationshipCreated("Restaurant", "LOCATED_AT", "City");
                output.AssertRelationshipCreated("Person", "EATS_AT", "Restaurant");

                // Database assigned a valid Uids
                string key5 = GetAndCheckKey(p2);
                string key6 = GetAndCheckKey(c2);
                string key7 = GetAndCheckKey(r2);


                // Update
                using (Transaction.Begin(true))
                {

                    Person person = Person.Load(key5);
                    person.Name = "Janice Smith";
                    person.City.Name = "California";
                    person.Restaurants[0].Name = "Shakeys Pizza";

                    City city = person.City;
                    Restaurant restaurant = person.Restaurants[0];

                    Transaction.Commit();
                }

                output.AssertNodeLoaded("Person");
                output.AssertRelationshipLoaded("Person", "LIVES_IN", "City");
                output.AssertRelationshipLoaded("Person", "EATS_AT", "Restaurant");
                output.AssertNodeUpdated("Person");
                output.AssertNodeUpdated("City");
                output.AssertNodeUpdated("Restaurant");

                // Check properties are updated after reloading
                using (Transaction.Begin())
                {
                    Person p = Person.Load(key5);
                    City c = City.Load(key6);
                    Restaurant r = Restaurant.Load(key7);

                    Assert.AreEqual(p.Name, "Janice Smith");
                    Assert.AreEqual(c.Name, "California");
                    Assert.AreEqual(r.Name, "Shakeys Pizza");
                }

                // Removing relationships by setting
                using (Transaction.Begin(true))
                {
                    Person p = Person.Load(key5);
                    p.City = null;
                    p.Restaurants.Clear();

                    Transaction.Flush();

                    Assert.IsNull(p.City);
                    Assert.IsTrue(p.Restaurants.Count == 0);

                    Transaction.Rollback();
                }

                output.AssertTimeDependentRelationshipDeleted("Person", "LIVES_IN", "City");
                output.AssertRelationshipDeleted("Person", "EATS_AT", "Restaurant");


                // Removing relationships via properties
                using (Transaction.Begin(true))
                {
                    Person p = Person.Load(key5);
                    City c = p.City; // Side-effect Person is lazy-loaded here, because one of it's properties is accessed.
                    Restaurant r = p.Restaurants[0];

                    p.City = null;
                    p.Restaurants.Remove(r);

                    Transaction.Flush();

                    Assert.IsTrue(c.PersistenceState == PersistenceState.Loaded);
                    Assert.IsTrue(r.PersistenceState == PersistenceState.Loaded);
 
                    Assert.IsNull(p.City);
                    Assert.True(p.Restaurants.Count == 0);

                    Transaction.Rollback();
                }

                output.AssertTimeDependentRelationshipDeleted("Person", "LIVES_IN", "City");
                output.AssertRelationshipDeleted("Person", "EATS_AT", "Restaurant");


                // Removing relationships and nodes via properties
                using (Transaction.Begin(true))
                {
                    Person p = Person.Load(key5);
                    City c = p.City; // Side-effect Person is lazy-loaded here, because one of it's properties is accessed.
                    Restaurant r = p.Restaurants[0];

                    p.City = null;
                    p.Restaurants.Delete(r);

                    c.Delete();

                    Transaction.Flush();

                    Assert.IsTrue(c.PersistenceState == PersistenceState.Deleted);
                    Assert.Throws<InvalidOperationException>(() => c.Name = "New Name", "The object has been deleted, you cannot make changes to it anymore.");

                    Assert.IsTrue(r.PersistenceState == PersistenceState.Deleted);
                    Assert.Throws<InvalidOperationException>(() => r.Name = "New Name", "The object has been deleted, you cannot make changes to it anymore.");

                    Assert.IsNull(p.City);
                    //Assert.True(p.Restaurants.Count == 0); //TODO: Expected 0?

                    Transaction.Rollback();
                }

                output.AssertTimeDependentRelationshipDeleted("Person", "LIVES_IN", "City");
                output.AssertRelationshipDeleted("Person", "EATS_AT", "Restaurant");
                output.AssertRelationshipDeleted("Restaurant", "LOCATED_AT", "City");

                output.AssertNodeDeleted("City");
                output.AssertNodeDeleted("Restaurant");


                // Removing node with existing relationship
                using (Transaction.Begin(true))
                {
                    //load before deleting
                    Person p = Person.Load(key5);

                    City.Load(key6).ForceDelete(); // Side-effect Person NOT lazy loaded here yet, because it's properties were never accessed.
                    Transaction.Flush(); // Persist in DB & change PersistenceState from Delete to Deleted

                    //load after deleting
                    Restaurant r = Restaurant.Load(key7);

                    Assert.IsNull(p.City);
                    Assert.IsNull(r.City);

                    Transaction.Rollback();
                }

                output.AssertTimeDependentRelationshipDeleted("Person", "LIVES_IN", "City");
                output.AssertRelationshipDeleted("Restaurant", "LOCATED_AT", "City");
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

                    p1.City.Restaurants.Add(new Restaurant { Name = "Mcdonalds" });
                    p1.City.Restaurants.Add(new Restaurant { Name = "Shakeys" });
                    p1.City.Restaurants.Add(new Restaurant { Name = "Starbucks" });
                    p1.City.Restaurants.Add(new Restaurant { Name = "Bo's Coffee" });
                    p1.City.Restaurants.Add(new Restaurant { Name = "Chattime" });

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

                    p2.City.Restaurants.Add(new Restaurant { Name = "Pink's Hot Dogs" });
                    p2.City.Restaurants.Add(new Restaurant { Name = "World Famous" });
                    p2.City.Restaurants.Add(new Restaurant { Name = "Barone's" });
                    p2.City.Restaurants.Add(new Restaurant { Name = "Providence" });
                    p2.City.Restaurants.Add(new Restaurant { Name = "La Taqueria" });

                    p1.Restaurants.AddRange(p1.City.Restaurants);
                    p2.Restaurants.AddRange(p2.City.Restaurants);
                    p3.Restaurants.AddRange(p1.City.Restaurants);

                    Transaction.Commit();
                }
            }

            using (Transaction.Begin())
            {
                ICompiled compiled;
                IReturnQuery query = Transaction.CompiledQuery
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
                    .Where(Functions.ExistsSubquery(sq => sq.Match(p.In.PERSON_EATS_AT.Out.Restaurant), Transaction.CompiledQuery) == true)
                    .Return(p, restaurants);
#if NEO4J
                compiled = query.Compile();

                var result = compiled.GetExecutionContext().Execute();
                List<Person> searchResult = Person.LoadWhere(compiled);
                Assert.Greater(searchResult.Count, 0);

                Assert.AreEqual(
                    """
                    MATCH (n0:Person)
                    WITH DISTINCT n0, COLLECT{MATCH (n0)-[:EATS_AT]->(n1:Restaurant)
                    WHERE (COUNT{MATCH (n0)-[:EATS_AT]->(:Restaurant)} = $param0)
                    RETURN DISTINCT n1.Name AS Column1} AS restaurants
                    WHERE (EXISTS{MATCH (n0)-[:EATS_AT]->(:Restaurant)} = $param1)
                    RETURN DISTINCT n0 AS Column1, restaurants AS Column2
                    """,
                    compiled.CompiledQuery.QueryText);

                compiled = Transaction.CompiledQuery
                    .Match(node.Person.Alias(out PersonAlias pWithLimit))
                    .Where(pWithLimit.Name.Contains("Smith"))
                    .Return(pWithLimit)
                    .Limit(1)
                    .Compile();

                searchResult = Person.LoadWhere(compiled);
                Assert.AreEqual(1, searchResult.Count);

                Assert.AreEqual(
                    """
                    MATCH (n0:Person)
                    WHERE (n0.Name CONTAINS $param0)
                    RETURN DISTINCT n0 AS Column1
                    LIMIT $param1
                    """,
                    compiled.CompiledQuery.QueryText);

                compiled = Transaction.CompiledQuery
                    .Match(node.Person.Alias(out var pR).In.PERSON_EATS_AT.Out.Restaurant.Alias(out var rP))
                    .Where(rP.Name == "Shakeys")
                    .Return(pR)
                    .OrderBy(pR.Name)
                    .Compile();

                searchResult = Person.LoadWhere(compiled);
                Assert.AreEqual(2, searchResult.Count);

                Assert.AreEqual("Bob Smith", searchResult[0].Name);
                Assert.AreEqual("Joe Smith", searchResult[1].Name);

                Assert.AreEqual(
                    """
                    MATCH (n0:Person)-[:EATS_AT]->(n1:Restaurant)
                    WHERE (n1.Name = $param0)
                    RETURN DISTINCT n0 AS Column1
                    ORDER BY n0.Name
                    """,
                    compiled.CompiledQuery.QueryText);
#elif MEMGRAPH
                Exception ex = Assert.Throws<NotSupportedException>(() => query.Compile());
                Assert.That(() => ex.Message.Contains("Memgraph does not support Collect subqueries"));
#endif               
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

                    output.AssertQuery(
                        """
                        MATCH (n0:Person)
                        WHERE (n0.Name CONTAINS $param0)
                        OPTIONAL MATCH (n1:Movie)
                        RETURN DISTINCT n1.Title AS Column1
                        ORDER BY n1.Title
                        """);

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

                    output.AssertQuery(
                        """
                        MATCH (n0:Person)
                        WHERE (n0.Name CONTAINS $param0)
                        OPTIONAL MATCH (n1:Movie)
                        RETURN DISTINCT n1.Title AS Column1
                        ORDER BY n1.Title
                        MATCH (n0:Person)
                        WHERE (n0.Name CONTAINS $param0)
                        OPTIONAL MATCH (n0)-[:DIRECTED_BY]->(n1:Movie)
                        RETURN DISTINCT n0.Name AS Column1, n1.Title AS Column2
                        ORDER BY n1.Title
                        """);

                    compiled = Transaction.CompiledQuery
                                .Match(node.Person.Alias(out PersonAlias pap).In.PERSON_DIRECTED.Out.Movie.Alias(out MovieAlias mam))
                                .Where(pap.Name.Contains("Martin Sheen"))
                                .Return(mam.Title)
                                .OrderBy(mam.Title)
                                .Compile();

                    result = compiled.GetExecutionContext().Execute();
                    Assert.Zero(result.Count);

                    output.AssertQuery(
                        """
                        MATCH (n0:Person)
                        WHERE (n0.Name CONTAINS $param0)
                        OPTIONAL MATCH (n1:Movie)
                        RETURN DISTINCT n1.Title AS Column1
                        ORDER BY n1.Title
                        MATCH (n0:Person)
                        WHERE (n0.Name CONTAINS $param0)
                        OPTIONAL MATCH (n0)-[:DIRECTED_BY]->(n1:Movie)
                        RETURN DISTINCT n0.Name AS Column1, n1.Title AS Column2
                        ORDER BY n1.Title
                        MATCH (n0:Person)-[:DIRECTED_BY]->(n1:Movie)
                        WHERE (n0.Name CONTAINS $param0)
                        RETURN DISTINCT n1.Title AS Column1
                        ORDER BY n1.Title
                        """);
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

                    output.AssertQuery(
                        """
                        MATCH (n0:Movie)
                        USING INDEX n0:Movie(Title)
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1
                        """);

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

                    output.AssertQuery(
                        """
                        MATCH (n0:Movie)
                        USING INDEX n0:Movie(Title)
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1
                        MATCH (n0:Movie)<-[:DIRECTED_BY]-(n1:Person)
                        USING INDEX n0:Movie(Title)
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1, n1.Name AS Column2
                        """);

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

                    output.AssertQuery(
                        """
                        MATCH (n0:Movie)
                        USING INDEX n0:Movie(Title)
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1
                        MATCH (n0:Movie)<-[:DIRECTED_BY]-(n1:Person)
                        USING INDEX n0:Movie(Title)
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1, n1.Name AS Column2
                        MATCH (n0:Movie)
                        USING SCAN n0:Movie
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1
                        """);

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

                    output.AssertQuery(
                        """
                        MATCH (n0:Movie)
                        USING INDEX n0:Movie(Title)
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1
                        MATCH (n0:Movie)<-[:DIRECTED_BY]-(n1:Person)
                        USING INDEX n0:Movie(Title)
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1, n1.Name AS Column2
                        MATCH (n0:Movie)
                        USING SCAN n0:Movie
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1
                        MATCH (n0:Movie)<-[:DIRECTED_BY]-(n1:Person)
                        USING SCAN n0:Movie
                        USING SCAN n1:Person
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1, n1.Name AS Column2
                        """);

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

                    output.AssertQuery(
                        """
                        MATCH (n0:Movie)
                        USING INDEX n0:Movie(Title)
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1
                        MATCH (n0:Movie)<-[:DIRECTED_BY]-(n1:Person)
                        USING INDEX n0:Movie(Title)
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1, n1.Name AS Column2
                        MATCH (n0:Movie)
                        USING SCAN n0:Movie
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1
                        MATCH (n0:Movie)<-[:DIRECTED_BY]-(n1:Person)
                        USING SCAN n0:Movie
                        USING SCAN n1:Person
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1, n1.Name AS Column2
                        MATCH (n0:Movie)<-[:DIRECTED_BY]-(n1:Person)
                        USING INDEX n0:Movie(Title)
                        USING SCAN n1:Person
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1, n1.Name AS Column2
                        """);
                }
            }
        }

        private static string GetAndCheckKey<T>(T a)
            where T : OGM
        {
            string key = a.GetKey()?.ToString();
            Assert.IsNotNull(key);
            Assert.IsNotEmpty(key);
#if NEO4J
            Assert.DoesNotThrow(() => int.Parse(key));
#elif MEMGRAPH
            Assert.DoesNotThrow(() => Guid.Parse(key));
#endif
            return key;
        }
    }
}
