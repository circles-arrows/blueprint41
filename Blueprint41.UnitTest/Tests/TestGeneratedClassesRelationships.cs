//#define CHECK_INITIAL_STATE_AND_EXPECTED_END_STATE

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
            DateTime[] dates = new[]
            {
                DateTime.MinValue,
                new DateTime(1981, 4, 12, 12, 0, 4, DateTimeKind.Utc), // The first orbital flight of the space shuttle, NASA's reusable space vehicle.
                new DateTime(1990, 4, 24, 12, 33, 51, DateTimeKind.Utc), // Apr 25, 1990 - Hubble Space Telescope launched into space.
                new DateTime(2012, 8, 25, 0, 0, 0, DateTimeKind.Utc), // Aug 25, 2012 - Voyager 1 becomes the first spacecraft to reach interstellar space.
                DateTime.MaxValue,
            };
            int bits = dates.Length - 1;
            int count = (1 << bits);

#if CHECK_INITIAL_STATE_AND_EXPECTED_END_STATE

            var test = new List<(int mask, string dbState, string moment, string action, string expected)>();

            for (int mask = 0; mask < count; mask++)
            {
                var relations = RelationsFromMask(mask);

                foreach (DateTime moment in dates)
                {
                    var expected = RelationsFromMask(AdjustMaskForAdd(mask, moment));
                    test.Add((mask, DrawAsciiArtState(relations), DrawAsciiArtMoment(moment), "Add same", DrawAsciiArtState(expected)));
                }

                foreach (DateTime moment in dates)
                {
                    var expected = RelationsFromMask(AdjustMaskForRemove(mask, moment)).Union(RelationsFromMask(AdjustMaskForAdd(0, moment))).ToList();
                    test.Add((mask, DrawAsciiArtState(relations), DrawAsciiArtMoment(moment), "Add diff", DrawAsciiArtState(expected)));
                }

                foreach (DateTime moment in dates)
                {
                    var expected = RelationsFromMask(AdjustMaskForRemove(mask, moment));
                    test.Add((mask, DrawAsciiArtState(relations), DrawAsciiArtMoment(moment), "Remove", DrawAsciiArtState(expected)));
                }
            }

            string DrawAsciiArtState(List<(DateTime from, DateTime till)> relations)
            {
                char[] asciiArt = new char[(bits << 2) + 1];
                for (int index = 0; index < asciiArt.Length; index++)
                    asciiArt[index] = ' ';

                foreach ((DateTime from, DateTime till) in relations)
                {
                    int fromIdx = Array.IndexOf(dates, from);
                    int tillIdx = Array.IndexOf(dates, till);

                    int fromPos = fromIdx << 2;
                    int tillPos = tillIdx << 2;

                    char fromChr = (fromIdx == 0) ? '<' : '|';
                    char tillChr = (tillIdx == bits) ? '>' : '|';

                    for (int pos = fromPos; pos <= tillPos; pos++)
                    {
                        if (pos == fromPos)
                            asciiArt[pos] = fromChr;
                        else if (pos == tillPos)
                            asciiArt[pos] = tillChr;
                        else
                            asciiArt[pos] = '-';
                    }
                }

                return new string(asciiArt);
            }
            string DrawAsciiArtMoment(DateTime moment)
            {
                char[] asciiArt = new char[(bits << 2) + 1];
                for (int index = 0; index < asciiArt.Length; index++)
                    asciiArt[index] = ' ';

                int idx = Array.IndexOf(dates, moment);
                int pos = idx << 2;

                char chr = '|';
                if (idx == 0) chr = '<';
                if (idx == bits) chr = '>';

                asciiArt[pos] = chr;

                return new string(asciiArt);
            }

            return;

#endif

            var prerequisites = new List<(List<(DateTime from, DateTime till)> initial, string key)>()
            {
                (RelationsFromMask(0b0100), "2"),
                (RelationsFromMask(0b0010), "3"),
                (RelationsFromMask(0b0101), "4"),
                (RelationsFromMask(0b1010), "5"),
                (RelationsFromMask(0b1001), "6"),
                (RelationsFromMask(0b1111), "7"),
            };

            for (int mask = 0; mask < count; mask++)
            {
                var relations = RelationsFromMask(mask);

                foreach (DateTime moment in dates)
                {
                    TestLookupAdd(relations, moment, RelationsFromMask(AdjustMaskForAdd(mask, moment)), false);
                    TestLookupAdd(relations, moment, RelationsFromMask(AdjustMaskForRemove(mask, moment)).Union(RelationsFromMask(AdjustMaskForAdd(0, moment))).ToList(), true);
                    TestLookupRemove(relations, moment, RelationsFromMask(AdjustMaskForRemove(mask, moment)));

                    TestCollectionAdd(relations, moment, RelationsFromMask(AdjustMaskForAdd(mask, moment)), false);
                    TestCollectionAdd(relations, moment, RelationsFromMask(AdjustMaskForRemove(mask, moment)).Union(RelationsFromMask(AdjustMaskForAdd(0, moment))).ToList(), true);
                    TestCollectionRemove(relations, moment, RelationsFromMask(AdjustMaskForRemove(mask, moment)));
                }
            }

            void TestLookupAdd(List<(DateTime from, DateTime till)> initial, DateTime moment, List< (DateTime from, DateTime till)> expected, bool differentProperties)
            {
            }

            void TestLookupRemove(List<(DateTime from, DateTime till)> initial, DateTime moment, List<(DateTime from, DateTime till)> expected)
            {
            }

            void TestCollectionAdd(List<(DateTime from, DateTime till)> initial, DateTime moment, List<(DateTime from, DateTime till)> expected, bool differentProperties)
            {
            }

            void TestCollectionRemove(List<(DateTime from, DateTime till)> initial, DateTime moment, List<(DateTime from, DateTime till)> expected)
            {
            }

            List<(DateTime from, DateTime till)> RelationsFromMask(int mask)
            {
                DateTime? from = null;
                var relations = new List<(DateTime from, DateTime till)>();

                for (int pos = 0; pos <= bits; pos++)
                {
                    bool bit = ((1 << pos) & mask) != 0;
                    if (bit && from is null)
                    {
                        from = dates[pos];
                    }
                    else if (!bit && from is not null)
                    {
                        relations.Add((from.Value, dates[pos]));
                        from = null;
                    }
                }

                return relations;
            }
            int AdjustMaskForAdd(int mask, DateTime moment)
            {
                int retval = mask;

                int idx = Array.IndexOf(dates, moment);
                while (idx < bits)
                {
                    retval |= (1 << idx);
                    idx++;
                }

                return retval;
            }
            int AdjustMaskForRemove(int mask, DateTime moment)
            {
                int retval = mask;

                int idx = Array.IndexOf(dates, moment);
                while (idx < bits)
                {
                    retval &= ~(1 << idx);
                    idx++;
                }

                return retval;
            }
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
