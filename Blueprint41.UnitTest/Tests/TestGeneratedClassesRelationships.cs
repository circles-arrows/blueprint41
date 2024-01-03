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
    public class TestGeneratedClassesRelationships
    {
        // Lookup with properties
        // Collection with properties
        // Time Dependent Lookup with properties
        // Time Dependent Collection with properties


        /*

        1       2       3       4       5
        <-------|-------|-------|------->

                                |------->
                        |-------|
                        |--------------->


        00000
        00001
        00010
      
         
         
         */

        [Test]
        public void TimeDependentRelationshipCRUD()
        {
            



        }


        private void CleanupRelations(Relationship relationship)
        {
            string cypher = $"""
                MATCH (:{relationship.InEntity.Label.Name})-[r:{relationship.Neo4JRelationshipType}]->(:{relationship.OutEntity.Label.Name})
                DELETE r
                """;

            using (Transaction.Begin())
            {
                Transaction.RunningTransaction.Run(cypher);
                Transaction.Commit();
            }
        }

        private void WriteRelation(OGM @in, Relationship relationship, OGM @out, DateTime? from, DateTime? till)
        {

            string cypher = $"""
                MATCH (in:{relationship.InEntity.Label.Name}), (out:{relationship.OutEntity.Label.Name})
                WHERE in.{@in.GetEntity().Key.Name} = $in AND out.{@out.GetEntity().Key.Name} = $out
                CREATE (in)-[r:{relationship.Neo4JRelationshipType}]->(out)
                SET r.CreationDate = $now
                    r.StartDate = $from,
                    r.EndDate = $till,
                """;

            var parameters = new Dictionary<string, object>()
            {
                { "in", @in.GetKey() },
                { "out", @out.GetKey() },
                { "now", PersistenceProvider.CurrentPersistenceProvider.ConvertToStoredType(DateTime.UtcNow) },
                { "from", PersistenceProvider.CurrentPersistenceProvider.ConvertToStoredType(from) },
                { "till", PersistenceProvider.CurrentPersistenceProvider.ConvertToStoredType(till) },
            };

            using (Transaction.Begin())
            {
                Transaction.RunningTransaction.Run(cypher, parameters);
                Transaction.Commit();
            }
        }
    }
}

#region Sample Interface

//// Get all EATS_AT relations for the given person
//List<PERSON_EATS_AT> all = person.RestaurantRelations();
//// And set their 'Weight' & 'LastModifiedOn' properties
//all.Assign(Score: "Good", CreationDate: DateTime.UtcNow);

//// Get a sub-set of EATS_AT relations for the given person
//List<PERSON_EATS_AT> subset = person.RestaurantsWhere(alias => alias.Restaurant(restaurant) & alias.Score != "Bad");
//// And use LINQ to query restaurants
//IEnumerable<Restaurant> restaurants = subset.Select(rel => rel.Restaurant);

////// Get EATS_AT relations based on a JSON notated expression
//List<PERSON_EATS_AT> relations = PERSON_EATS_AT.Where(InNode: person, OutNode: restaurant);

//// Get EATS_AT relations based on a Bp41 notated expression
//List<PERSON_EATS_AT> relations2 = PERSON_EATS_AT.Where(alias => alias.Restaurants(restaurants) & alias.Person(person) & alias.Score != "Bad");

//// Get EATS_AT relations based on Bp41 notated expression, and set their 'Weight' property
//PERSON_EATS_AT.Where(alias => alias.Restaurants(restaurants)).Assign(Score: "Good");

//// Get a sub-set of EATS_AT relations for the given person, and set their 'Weight' property
//person.RestaurantsWhere(alias => alias.Score != "Bad").Assign(Score: "Good");

//// Lookup: Query LIVES_IN relation for the city OR null, depending on the condition
////         And potentially assign new values
//person.CityIf(alias => alias.Street == "San Nicolas Street" & alias.HouseNr == 8)?.Assign(HouseNr: 6);

//// Set city 
//person.SetCity(city, CreationDate: DateTime.UtcNow, Street: "San Nicolas Street", HouseNr: 6);

//// Add restaurant
//person.AddRestaurant(restaurant, CreationDate: DateTime.UtcNow, Score: "Good");

#endregion
