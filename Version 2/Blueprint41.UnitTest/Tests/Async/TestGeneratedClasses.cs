#pragma warning disable CS8981 // Names should not be lower type only

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Blueprint41.Core;
using Blueprint41.DatastoreTemplates;
using Blueprint41.Query;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.UnitTest.Helper;
using Blueprint41.UnitTest.Mocks;

using Datastore.Manipulation.Async;
using Datastore.Query;

using NUnit.Framework;

using node = Datastore.Query.Node;

namespace Blueprint41.UnitTest.Tests.Async
{
    [TestFixture]
    public class TestGeneratedClasses : TestBase
    {
        public override void Setup()
        {
            TearDown();

            // Run mock model every time because the FunctionalId is wiped out by cleanup and needs to be recreated!         
            var model = Connect<MockModel>(true);
            model.Execute(true);
        }


        [Test]
        public async Task OGMImplCRUD()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                // Insert
                Person a;
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
                {

                    a = new Person()
                    {
                        Name = "Joe Smith",
                    };
                    await a.SetCityAsync(new City() { Name = "New York" }, null);
                    await Transaction.CommitAsync();
                }

                output.AssertNodeCreated("Person");
                output.AssertNodeCreated("City");
                output.AssertTimeDependentRelationshipCreated("Person", "LIVES_IN", "City");

                Assert.IsInstanceOf<OGMImpl>(a);
                Assert.AreEqual(a.Name, "Joe Smith");
                Assert.AreEqual((await a.GetCityAsync()).Name, "New York");

                // Database assigned a valid Uid
                string key = GetAndCheckKey(a);

                // Without transaction
                Assert.ThrowsAsync<InvalidOperationException>(async () => await Person.LoadAsync(key));

                Person? b;
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
                {
                    // Load
                    b = await Person.LoadAsync(key);
                    Assert.IsNotNull(b);
                    Assert.AreEqual(a, b);

                    // Update
                    b!.Name = "Jaden Smith";
                    await Transaction.CommitAsync();
                }

                Assert.AreEqual(b.Name, "Jaden Smith");
                output.AssertNodeUpdated("Person");

                Person? c;
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
                {
                    c = await Person.LoadAsync(key);
                    Assert.IsNotNull(c);

                    c!.Delete();
                    await Transaction.CommitAsync();
                }

                output.AssertNodeDeleted("Person");
                output.AssertTimeDependentRelationshipDeleted("Person", "LIVES_IN", "City");
                output.AssertRelationshipDeleted("Person", "EATS_AT", "Restaurant");

                Person? d;
                await using (MockModel.BeginTransactionAsync())
                {
                    // Load
                    d = await Person.LoadAsync(key);
                    Assert.IsNull(d);
                }
            }
        }

        [Test]
        public async Task OGMImplCRUDWithRelationship()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {

                Person p1, p2;
                City c1, c2;
                Restaurant r1, r2;

                // adding relationships per entity
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
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

                    await p1.SetCityAsync(c1, null);
                    await r1.SetCityAsync(c1);
                    (await p1.RestaurantsAsync()).Add(r1);

                    await Transaction.FlushAsync();

                    Assert.AreEqual(await p1.GetCityAsync(), c1);
                    Assert.AreEqual(await r1.GetCityAsync(), c1);
                    Assert.AreEqual((await p1.RestaurantsAsync())[0], r1);

                    await Transaction.CommitAsync();
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


                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
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

                    await p2.SetCityAsync(c2, null);
                    await r2.SetCityAsync(c2);
                    (await p2.RestaurantsAsync()).Add(r2);

                    await Transaction.CommitAsync();
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
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
                {

                    Person? person = await Person.LoadAsync(key5);
                    Assert.IsNotNull(person);
                    person!.Name = "Janice Smith";
                    (await person.GetCityAsync()).Name = "California";
                    Assert.IsNotEmpty(await person.RestaurantsAsync());
                    (await person.RestaurantsAsync())[0]!.Name = "Shakeys Pizza";

                    City city = await person.GetCityAsync();
                    Assert.IsNotEmpty(await person.RestaurantsAsync());
                    Restaurant restaurant = (await person.RestaurantsAsync())[0]!;

                    await Transaction.CommitAsync();
                }

                output.AssertNodeLoaded("Person");
                output.AssertRelationshipLoaded("Person", "LIVES_IN", "City");
                output.AssertRelationshipLoaded("Person", "EATS_AT", "Restaurant");
                output.AssertNodeUpdated("Person");
                output.AssertNodeUpdated("City");
                output.AssertNodeUpdated("Restaurant");

                // Check properties are updated after reloading
                await using (MockModel.BeginTransactionAsync())
                {
                    Person? p = await Person.LoadAsync(key5);
                    Assert.IsNotNull(p);
                    City? c = await City.LoadAsync(key6);
                    Assert.IsNotNull(c);
                    Restaurant? r = await Restaurant.LoadAsync(key7);
                    Assert.IsNotNull(r);

                    Assert.AreEqual(p!.Name, "Janice Smith");
                    Assert.AreEqual(c!.Name, "California");
                    Assert.AreEqual(r!.Name, "Shakeys Pizza");
                }

                // Removing relationships by setting
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
                {
                    Person? p = await Person.LoadAsync(key5);
                    Assert.IsNotNull(p);
                    await p!.SetCityAsync(null, null);
                    (await p.RestaurantsAsync()).Clear();

                    await Transaction.FlushAsync();

                    Assert.IsNull(await p.GetCityAsync());
                    Assert.IsTrue((await p.RestaurantsAsync()).Count == 0);

                    await Transaction.RollbackAsync();
                }

                output.AssertTimeDependentRelationshipDeleted("Person", "LIVES_IN", "City");
                output.AssertRelationshipDeleted("Person", "EATS_AT", "Restaurant");


                // Removing relationships via properties
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
                {
                    Person? p = await Person.LoadAsync(key5);
                    Assert.IsNotNull(p);
                    City? c = await p!.GetCityAsync(); // Side-effect Person is lazy-loaded here, because one of it's properties is accessed.
                    Assert.IsNotNull(c);
                    Restaurant? r = (await p.RestaurantsAsync())[0];
                    Assert.IsNotNull(r);

                    await p.SetCityAsync(null, null);
                    (await p.RestaurantsAsync()).Remove(r!);

                    await Transaction.FlushAsync();

                    Assert.IsTrue(c.PersistenceState == PersistenceState.Loaded);
                    Assert.IsTrue(r!.PersistenceState == PersistenceState.Loaded);

                    Assert.IsNull(await p.GetCityAsync());
                    Assert.True((await p.RestaurantsAsync()).Count == 0);

                    await Transaction.RollbackAsync();
                }

                output.AssertTimeDependentRelationshipDeleted("Person", "LIVES_IN", "City");
                output.AssertRelationshipDeleted("Person", "EATS_AT", "Restaurant");


                // Removing relationships and nodes via properties
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
                {
                    Person? p = await Person.LoadAsync(key5);
                    Assert.IsNotNull(p);
                    City? c = await p!.GetCityAsync(); // Side-effect Person is lazy-loaded here, because one of it's properties is accessed.
                    Assert.IsNotNull(c);
                    Restaurant? r = (await p.RestaurantsAsync())[0];
                    Assert.IsNotNull(r);

                    await p.SetCityAsync(null, null);
                    (await p.RestaurantsAsync()).Delete(r!);

                    c.Delete();

                    await Transaction.FlushAsync();

                    Assert.IsTrue(c.PersistenceState == PersistenceState.Deleted);
                    Assert.Throws<InvalidOperationException>(() => c.Name = "New Name", "The object has been deleted, you cannot make changes to it anymore.");

                    Assert.IsTrue(r!.PersistenceState == PersistenceState.Deleted);
                    Assert.Throws<InvalidOperationException>(() => r.Name = "New Name", "The object has been deleted, you cannot make changes to it anymore.");

                    Assert.IsNull(await p.GetCityAsync());
                    //Assert.True(p.Restaurants.Count == 0); //TODO: Expected 0?

                    await Transaction.RollbackAsync();
                }

                output.AssertTimeDependentRelationshipDeleted("Person", "LIVES_IN", "City");
                output.AssertRelationshipDeleted("Person", "EATS_AT", "Restaurant");
                output.AssertRelationshipDeleted("Restaurant", "LOCATED_AT", "City");

                output.AssertNodeDeleted("City");
                output.AssertNodeDeleted("Restaurant");


                // Removing node with existing relationship
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
                {
                    //load before deleting
                    Person? p = await Person.LoadAsync(key5);

                    (await City.LoadAsync(key6))?.ForceDelete(); // Side-effect Person NOT lazy loaded here yet, because it's properties were never accessed.
                    await Transaction.FlushAsync(); // Persist in DB & change PersistenceState from Delete to Deleted

                    //load after deleting
                    Restaurant? r = await Restaurant.LoadAsync(key7);

                    Assert.IsNotNull(p);
                    Assert.IsNotNull(r);

                    Assert.IsNull(p is null ? null : await p.GetCityAsync());
                    Assert.IsNull(r is null ? null : await r.GetCityAsync());

                    await Transaction.RollbackAsync();
                }

                output.AssertTimeDependentRelationshipDeleted("Person", "LIVES_IN", "City");
                output.AssertRelationshipDeleted("Restaurant", "LOCATED_AT", "City");
            }
        }

        [Test]
        public async Task OGMImplQuery()
        {
            // Exception will throw in line 424 -> Person.LoadWhere(compiled);
            // In the NodePersistenceProvider.Load in line 368 -> var node = record["node"]?.As<driver.NodeResult>();
            // the field of the record is not name "node"

            using (ConsoleOutput output = new ConsoleOutput())
            {
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
                {
                    Person p1 = new Person
                    {
                        Name = "Joe Smith",
                    };
                    await p1.SetCityAsync(new City() { Name = "New York" }, null);

                    (await (await p1.GetCityAsync()).RestaurantsAsync()).Add(new Restaurant { Name = "Mcdonalds" });
                    (await (await p1.GetCityAsync()).RestaurantsAsync()).Add(new Restaurant { Name = "Shakeys" });
                    (await (await p1.GetCityAsync()).RestaurantsAsync()).Add(new Restaurant { Name = "Starbucks" });
                    (await (await p1.GetCityAsync()).RestaurantsAsync()).Add(new Restaurant { Name = "Bo's Coffee" });
                    (await (await p1.GetCityAsync()).RestaurantsAsync()).Add(new Restaurant { Name = "Chattime" });

                    Person p2 = new Person
                    {
                        Name = "Jane Smith",
                    };
                    await p2.SetCityAsync(new City() { Name = "California" }, null);

                    Person p3 = new Person
                    {
                        Name = "Bob Smith",
                    };
                    await p3.SetCityAsync(await p1.GetCityAsync(), null);

                    (await (await p2.GetCityAsync()).RestaurantsAsync()).Add(new Restaurant { Name = "Pink's Hot Dogs" });
                    (await (await p2.GetCityAsync()).RestaurantsAsync()).Add(new Restaurant { Name = "World Famous" });
                    (await (await p2.GetCityAsync()).RestaurantsAsync()).Add(new Restaurant { Name = "Barone's" });
                    (await (await p2.GetCityAsync()).RestaurantsAsync()).Add(new Restaurant { Name = "Providence" });
                    (await (await p2.GetCityAsync()).RestaurantsAsync()).Add(new Restaurant { Name = "La Taqueria" });

                    (await p1.RestaurantsAsync()).AddRange(await (await p1.GetCityAsync()).RestaurantsAsync());
                    (await p2.RestaurantsAsync()).AddRange(await (await p2.GetCityAsync()).RestaurantsAsync());
                    (await p3.RestaurantsAsync()).AddRange(await (await p1.GetCityAsync()).RestaurantsAsync());

                    await Transaction.CommitAsync();
                }
            }

            await using (MockModel.BeginTransactionAsync())
            {
#pragma warning disable CS0168 // Variable is declared but never used
                ICompiled compiled;
#pragma warning restore CS0168 // Variable is declared but never used
                IReturnQuery query = Cypher
                    .Match(node.Person.Alias(out PersonAlias p))
                    .With(p, Functions.CollectSubquery<StringListResult>(sq =>
                        sq.Match
                        (
                            p
                                .In.PERSON_EATS_AT.Out.
                            Restaurant.Alias(out var restaurantAlias)
                        )
                        .Where(Functions.CountSubquery(sq => sq.Match(p.In.PERSON_EATS_AT.Out.Restaurant), Cypher.Query) == 5)
                        .Return(restaurantAlias.Name), Cypher.Query)
                        .As("restaurants", out var restaurants)
                    )
                    .Where(Functions.ExistsSubquery(sq => sq.Match(p.In.PERSON_EATS_AT.Out.Restaurant), Cypher.Query) == true)
                    .Return(p, restaurants);
#if NEO4J
                compiled = query.Compile();


                var result = await compiled.GetExecutionContext().ExecuteAsync();
                List<Person> searchResult = await Person.LoadWhereAsync(compiled);
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
                    compiled.CompiledQuery!.QueryText);

                compiled = Cypher
                    .Match(node.Person.Alias(out PersonAlias pWithLimit))
                    .Where(pWithLimit.Name.Contains("Smith"))
                    .Return(pWithLimit)
                    .Limit(1)
                    .Compile();

                searchResult = await Person.LoadWhereAsync(compiled);
                Assert.AreEqual(1, searchResult.Count);

                Assert.AreEqual(
                    """
                    MATCH (n0:Person)
                    WHERE (n0.Name CONTAINS $param0)
                    RETURN DISTINCT n0 AS Column1
                    LIMIT $param1
                    """,
                    compiled.CompiledQuery!.QueryText);

                compiled = Cypher
                    .Match(node.Person.Alias(out var pR).In.PERSON_EATS_AT.Out.Restaurant.Alias(out var rP))
                    .Where(rP.Name == "Shakeys")
                    .Return(pR)
                    .OrderBy(pR.Name)
                    .Compile();

                searchResult = await Person.LoadWhereAsync(compiled);
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
                    compiled.CompiledQuery!.QueryText);
#elif MEMGRAPH
                Exception ex = Assert.Throws<NotSupportedException>(() => query.Compile());
                Assert.That(() => ex.Message.Contains("Memgraph does not support Collect subqueries"));
#endif               
            }
        }

        [Test]
        public async Task OGMImplQueryOptionalMatch()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
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

                    (await p1.ActedInMoviesAsync()).Add(tap);
                    (await p1.ActedInMoviesAsync()).Add(wallstreet);

                    (await p2.ActedInMoviesAsync()).Add(tap);
                    (await p2.ActedInMoviesAsync()).Add(wallstreet);

                    (await p3.DirectedMoviesAsync()).Add(wallstreet);
                    (await p4.DirectedMoviesAsync()).Add(tap);

                    await Transaction.CommitAsync();
                }
            }

            using (ConsoleOutput output = new ConsoleOutput())
            {
                await using (MockModel.BeginTransactionAsync())
                {
                    ICompiled compiled = Cypher
                                .Match(node.Person.Alias(out PersonAlias p))
                                .Where(p.Name.Contains("Martin Sheen"))
                                .OptionalMatch(node.Movie.Alias(out MovieAlias m))
                                .Return(m.Title)
                                .OrderBy(m.Title)
                                .Compile();

                    List<dynamic> result = await compiled.GetExecutionContext().ExecuteAsync();

                    IDictionary<string, object>? a = result[0] as IDictionary<string, object>;
                    IDictionary<string, object>? b = result[1] as IDictionary<string, object>;

                    Assert.IsNotNull(a);
                    Assert.IsNotNull(b);

                    Assert.AreEqual(a!["Column1"], "The American President");
                    Assert.AreEqual(b!["Column1"], "Wall Street");

                    output.AssertQuery(
                        """
                        MATCH (n0:Person)
                        WHERE (n0.Name CONTAINS $param0)
                        OPTIONAL MATCH (n1:Movie)
                        RETURN DISTINCT n1.Title AS Column1
                        ORDER BY n1.Title
                        """);

                    compiled = Cypher
                            .Match(node.Person.Alias(out PersonAlias pa))
                            .Where(pa.Name.Contains("Martin Sheen"))
                            .OptionalMatch(pa.In.PERSON_DIRECTED.Out.Movie.Alias(out MovieAlias ma))
                            .Return(pa.Name, ma.Title)
                            .OrderBy(ma.Title)
                            .Compile();

                    result = await compiled.GetExecutionContext().ExecuteAsync();

                    a = result[0] as IDictionary<string, object>;

                    Assert.IsNotNull(a);

                    Assert.AreEqual(a!["Column1"], "Martin Sheen");
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

                    compiled = Cypher
                                .Match(node.Person.Alias(out PersonAlias pap).In.PERSON_DIRECTED.Out.Movie.Alias(out MovieAlias mam))
                                .Where(pap.Name.Contains("Martin Sheen"))
                                .Return(mam.Title)
                                .OrderBy(mam.Title)
                                .Compile();

                    result = await compiled.GetExecutionContext().ExecuteAsync();
                    Assert.Zero(result.Count);

                    //TODO: Check why this throws???
                    //output.AssertQuery(
                    //    """
                    //    MATCH (n0:Person)
                    //    WHERE (n0.Name CONTAINS $param0)
                    //    OPTIONAL MATCH (n1:Movie)
                    //    RETURN DISTINCT n1.Title AS Column1
                    //    ORDER BY n1.Title
                    //    MATCH (n0:Person)
                    //    WHERE (n0.Name CONTAINS $param0)
                    //    OPTIONAL MATCH (n0)-[:DIRECTED_BY]->(n1:Movie)
                    //    RETURN DISTINCT n0.Name AS Column1, n1.Title AS Column2
                    //    ORDER BY n1.Title
                    //    MATCH (n0:Person)-[:DIRECTED_BY]->(n1:Movie)
                    //    WHERE (n0.Name CONTAINS $param0)
                    //    RETURN DISTINCT n1.Title AS Column1
                    //    ORDER BY n1.Title
                    //    """);

                    output.AssertQuery(
                        """
                        MATCH (n0:Person)-[:DIRECTED_BY]->(n1:Movie)
                        WHERE (n0.Name CONTAINS $param0)
                        RETURN DISTINCT n1.Title AS Column1
                        ORDER BY n1.Title
                        """);
                }
            }
        }

        [Test]
        public async Task OGMImplPlannerHitsUsing()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
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

                    (await p1.ActedInMoviesAsync()).Add(tap);
                    (await p1.ActedInMoviesAsync()).Add(wallstreet);

                    (await p2.ActedInMoviesAsync()).Add(tap);
                    (await p2.ActedInMoviesAsync()).Add(wallstreet);

                    (await p3.DirectedMoviesAsync()).Add(wallstreet);
                    (await p4.DirectedMoviesAsync()).Add(tap);

                    await Transaction.CommitAsync();
                }
            }

            using (ConsoleOutput output = new ConsoleOutput())
            {
                await using (MockModel.BeginTransactionAsync())
                {
                    // Force to use index
                    ICompiled compiled = Cypher
                            .Match(node.Movie.Alias(out MovieAlias m))
                            .UsingIndex(m.Title)
                            .Where(m.Title == "Wall Street")
                            .Return(m.Title)
                            .Compile();

                    var result = await compiled.GetExecutionContext().ExecuteAsync();

                    var a = result[0] as IDictionary<string, object>;

                    Assert.IsNotNull(a);

                    Assert.AreEqual(a!["Column1"], "Wall Street");

#if NEO4J
                    output.AssertQuery(
                        """
                        MATCH (n0:Movie)
                        USING INDEX n0:Movie(Title)
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1
                        """);
#elif MEMGRAPH
                    output.AssertQuery(
                        """
                        USING INDEX :Movie(Title)
                        MATCH (n0:Movie)
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1
                        """);
#endif

                    // With relationship
                    compiled = Cypher
                                .Match(node.Movie.Alias(out MovieAlias ma).Out.PERSON_DIRECTED.In.Person.Alias(out PersonAlias p))
                                .UsingIndex(ma.Title)
                                .Where(ma.Title == "Wall Street")
                                .Return(ma.Title, p.Name)
                                .Compile();

                    result = await compiled.GetExecutionContext().ExecuteAsync();

                    a = result[0] as IDictionary<string, object>;

                    Assert.IsNotNull(a);

                    Assert.AreEqual(a!["Column1"], "Wall Street");
                    Assert.AreEqual(a["Column2"], "Oliver Stone");

#if NEO4J
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
#elif MEMGRAPH
                    output.AssertQuery(
                        """
                        USING INDEX :Movie(Title)
                        MATCH (n0:Movie)
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1
                        USING INDEX :Movie(Title)
                        MATCH (n0:Movie)<-[:DIRECTED_BY]-(n1:Person)
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1, n1.Name AS Column2
                        """);
#endif

#if NEO4J
                    // Use label scan
                    compiled = Cypher
                            .Match(node.Movie.Alias(out MovieAlias mas))
                            .UsingScan(mas)
                            .Where(mas.Title == "Wall Street")
                            .Return(mas.Title)
                            .Compile();

                    result = await compiled.GetExecutionContext().ExecuteAsync();

                    a = result[0] as IDictionary<string, object>;

                    Assert.IsNotNull(a);

                    Assert.AreEqual(a!["Column1"], "Wall Street");

                    output.AssertQuery(
                        """
                        MATCH (n0:Movie)
                        USING SCAN n0:Movie
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1
                        """);

                    // use label scan with relationship
                    compiled = Cypher
                            .Match(node.Movie.Alias(out MovieAlias mar).Out.PERSON_DIRECTED.In.Person.Alias(out PersonAlias par))
                            .UsingScan(mar)
                            .UsingScan(par)
                            .Where(mar.Title == "Wall Street")
                            .Return(mar.Title, par.Name)
                            .Compile();

                    result = await compiled.GetExecutionContext().ExecuteAsync();

                    a = result[0] as IDictionary<string, object>;

                    Assert.IsNotNull(a);

                    Assert.AreEqual(a!["Column1"], "Wall Street");
                    Assert.AreEqual(a["Column2"], "Oliver Stone");

                    output.AssertQuery(
                        """
                        MATCH (n0:Movie)<-[:DIRECTED_BY]-(n1:Person)
                        USING SCAN n0:Movie
                        USING SCAN n1:Person
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1, n1.Name AS Column2
                        """);

                    // use label scan and index
                    compiled = Cypher
                            .Match(node.Movie.Alias(out MovieAlias msi).Out.PERSON_DIRECTED.In.Person.Alias(out PersonAlias psi))
                            .UsingIndex(msi.Title)
                            .UsingScan(psi)
                            .Where(msi.Title == "Wall Street")
                            .Return(msi.Title, psi.Name)
                            .Compile();

                    result = await compiled.GetExecutionContext().ExecuteAsync();

                    a = result[0] as IDictionary<string, object>;

                    Assert.IsNotNull(a);

                    Assert.AreEqual(a!["Column1"], "Wall Street");
                    Assert.AreEqual(a["Column2"], "Oliver Stone");

                    output.AssertQuery(
                        """
                        RETURN DISTINCT n0.Title AS Column1, n1.Name AS Column2
                        MATCH (n0:Movie)<-[:DIRECTED_BY]-(n1:Person)
                        USING INDEX n0:Movie(Title)
                        USING SCAN n1:Person
                        WHERE (n0.Title = $param0)
                        RETURN DISTINCT n0.Title AS Column1, n1.Name AS Column2
                        """);
#elif MEMGRAPH
#endif
                }
            }
        }

        private static string GetAndCheckKey<T>(T a)
            where T : OGM
        {
            string? key = a.GetKey()?.ToString();
            Assert.IsNotNull(key);
            Assert.IsNotEmpty(key);
#if NEO4J
            Assert.DoesNotThrow(() => int.Parse(key!));
#elif MEMGRAPH
            Assert.DoesNotThrow(() => Guid.Parse(key!));
#endif
            return key!;
        }
    }
}
