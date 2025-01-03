using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Blueprint41.Core;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.UnitTest.Helper;
using Blueprint41.UnitTest.Mocks;

using Datastore.Manipulation;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ClientException = Neo4j.Driver.ClientException;

namespace Blueprint41.UnitTest.Tests
{
    public partial class TestRelationships : TestBase
    {
        [Test]
        public void LookupSetLegacy()
        {
            #region Set Movie Certification

            using (MockModel.BeginTransaction())
            {
                Rating? rating = Rating.Load(DatabaseUids.Ratings.PG);
                Assert.IsNotNull(rating);

                CleanupRelations(MOVIE_CERTIFICATION.Relationship);

                foreach (var certification in DatabaseUids.Movies.Movies)
                {
                    Debug.WriteLine($"Set {certification.movie.Title} certification to {certification.rating.Name}");
                    certification.movie.Certification = rating;
                }

                Transaction.Flush();

                foreach (var certification in DatabaseUids.Movies.Movies)
                {
                    certification.movie.Certification = certification.rating;
                }

                Transaction.Commit();
            }

            using (MockModel.BeginTransaction())
            {
                foreach (var certification in DatabaseUids.Movies.Movies)
                {
                    Assert.IsTrue(certification.movie.Certification == certification.rating);
                }
            }

            #endregion

            #region Set NULL


            using (MockModel.BeginTransaction())
            {
                foreach (var certification in DatabaseUids.Movies.Movies)
                {
                    Debug.WriteLine($"Set {certification.movie.Title} certification to NULL");
                    certification.movie.Certification = null;
                }

                Transaction.Commit();
            }

            using (MockModel.BeginTransaction())
            {
                foreach (var certification in DatabaseUids.Movies.Movies)
                {
                    Assert.IsTrue(certification.movie.Certification is null);
                }
            }

            #endregion
        }

        [Test]
        public void CollAddAndRemoveLegacy()
        {
            #region Add Watched Movie

#if NEO4J
            using (MockModel.BeginTransaction())
            {
                CleanupRelations(WATCHED_MOVIE.Relationship);

                var watched = SampleDataWatchedMovies().First();
                watched.person.WatchedMovies.Add(watched.movie);

                Exception ex = Assert.Throws<AggregateException>(() => Transaction.Commit());
#if NET5_0_OR_GREATER
                Assert.That(() => ex.Message.Contains("`WATCHED` must have the property `MinutesWatched`"));
#else
                Assert.That(() => ex.InnerException.Message.Contains("`WATCHED` must have the property `MinutesWatched`"));
#endif
            }

            #region Strip Constraint from MinutesWatched

            Execute(MakeMinutesWatchedNullable);

            #endregion
#endif

            using (MockModel.BeginTransaction())
            {
                CleanupRelations(WATCHED_MOVIE.Relationship);

                foreach (var watched in SampleDataWatchedMovies())
                {
                    Debug.WriteLine($"Add Watched Movie {watched.movie.Title} for {watched.person.Name}");

                    watched.person.WatchedMovies.Add(watched.movie);
                }

                Transaction.Commit();
            }

            using (MockModel.BeginTransaction())
            {
                foreach (var watched in SampleDataWatchedMovies().GroupBy(item => item.person).Select(item => (person: item.Key, watchedMovies: item.ToList())))
                {
                    List<string> excpected = watched.watchedMovies.Select(item => item.movie.Title).ToList();
                    List<string> actual = watched.person.WatchedMovies.Select(item => item.Title).ToList();

                    Assert.AreEqual(excpected.Count, actual.Count);
                    Assert.AreEqual(0, excpected.Except(actual).Count());
                    Assert.AreEqual(0, actual.Except(excpected).Count());
                }

                Transaction.Commit();
            }

#endregion

            #region Remove Watched Movie

            using (MockModel.BeginTransaction())
            {
                foreach (var notWatched in SampleDataWatchedMovies().GroupBy(item => item.person).Select(item => item.First()))
                {
                    Debug.WriteLine($"Remove Watched Movie {notWatched.movie} for {notWatched.person.Name}");

                    notWatched.person.WatchedMovies.Remove(notWatched.movie);
                }

                Transaction.Commit();
            }

            using (MockModel.BeginTransaction())
            {
                foreach (var watched in SampleDataWatchedMovies().GroupBy(item => item.person).Select(item => (person: item.Key, watchedMovies: item.Skip(1).ToList())))
                {
                    List<string> excpected = watched.watchedMovies.Select(item => item.movie.Title).ToList();
                    List<string> actual = watched.person.WatchedMovies.Select(item => item.Title).ToList();

                    Assert.AreEqual(excpected.Count, actual.Count);
                    Assert.AreEqual(0, excpected.Except(actual).Count());
                    Assert.AreEqual(0, actual.Except(excpected).Count());
                }

                Transaction.Commit();
            }

            #endregion
        }

        [Test]
        public void LookupSetWithProperties()
        {
            #region Set Movie Certification

            using (MockModel.BeginTransaction())
            {
                Rating? rating = Rating.Load(DatabaseUids.Ratings.PG);
                Assert.IsNotNull(rating);

                CleanupRelations(MOVIE_CERTIFICATION.Relationship);

                foreach (var certification in DatabaseUids.Movies.Movies)
                {
                    Debug.WriteLine($"Set {certification.movie.Title} certification to {certification.rating.Name}");
                    certification.movie.SetCertification(
                        rating,
                        FrighteningIntense: RatingComponent.None,
                        Profanity: RatingComponent.None,
                        SexAndNudity: RatingComponent.None,
                        Substances: RatingComponent.None,
                        ViolenceGore: RatingComponent.None);
                }

                Transaction.Flush();

                foreach (var certification in DatabaseUids.Movies.Movies)
                {
                    certification.movie.SetCertification(
                        certification.rating,
                        FrighteningIntense: certification.frighteningIntense,
                        Profanity: certification.profanity,
                        SexAndNudity: certification.sexAndNudity,
                        Substances: certification.substances,
                        ViolenceGore: certification.violenceGore);
                }

                Transaction.Commit();
            }

            using (MockModel.BeginTransaction())
            {
                foreach (var certification in DatabaseUids.Movies.Movies)
                {
                    var details = ReadRelationsWithProperties(certification.movie, MOVIE_CERTIFICATION.Relationship, certification.rating);
                    Assert.AreEqual(1, details.Count);
                    Assert.AreEqual(Conversion.MinDateTime, details[0].from);
                    Assert.AreEqual(Conversion.MaxDateTime, details[0].till);
                    Assert.AreEqual(5, details[0].properties.Count);
                    Assert.AreEqual(certification.frighteningIntense.ToString(), details[0].properties["FrighteningIntense"]);
                    Assert.AreEqual(certification.profanity.ToString(), details[0].properties["Profanity"]);
                    Assert.AreEqual(certification.sexAndNudity.ToString(), details[0].properties["SexAndNudity"]);
                    Assert.AreEqual(certification.substances.ToString(), details[0].properties["Substances"]);
                    Assert.AreEqual(certification.violenceGore.ToString(), details[0].properties["ViolenceGore"]);
                }
            }

            #endregion

            #region Set NULL


            using (MockModel.BeginTransaction())
            {
                foreach (var certification in DatabaseUids.Movies.Movies)
                {
                    Debug.WriteLine($"Set {certification.movie.Title} certification to NULL");
                    certification.movie.SetCertification(
                        null,
                        FrighteningIntense: certification.frighteningIntense,
                        Profanity: certification.profanity,
                        SexAndNudity: certification.sexAndNudity,
                        Substances: certification.substances,
                        ViolenceGore: certification.violenceGore);
                }

                Transaction.Commit();
            }

            using (MockModel.BeginTransaction())
            {
                foreach (var certification in DatabaseUids.Movies.Movies)
                {
                    Assert.IsTrue(certification.movie.Certification is null);
                }
            }

            #endregion
        }

        [Test]
        public void CollAddAndRemoveWithProperties()
        {
            #region Add Watched Movie

            using (MockModel.BeginTransaction())
            {
                CleanupRelations(WATCHED_MOVIE.Relationship);

                foreach (var watched in SampleDataWatchedMovies())
                {
                    Debug.WriteLine($"Add Watched Movie {watched.movie.Title} for {watched.person.Name}");

                    watched.person.AddWatchedMovie(watched.movie, MinutesWatched: watched.minutes);
                }

                Transaction.Commit();
            }

            using (MockModel.BeginTransaction())
            {
                foreach (var watched in SampleDataWatchedMovies().GroupBy(item => item.person).Select(item => (person: item.Key, watchedMovies: item.ToList())))
                {
                    List<string> excpected = watched.watchedMovies.Select(item => item.movie.Title).ToList();
                    List<string> actual = watched.person.WatchedMovies.Select(item => item.Title).ToList();

                    Assert.AreEqual(excpected.Count, actual.Count);
                    Assert.AreEqual(0, excpected.Except(actual).Count());
                    Assert.AreEqual(0, actual.Except(excpected).Count());

                    foreach (var watchedMovie in watched.watchedMovies)
                    {
                        var relations = ReadRelationsWithProperties(watchedMovie.person, WATCHED_MOVIE.Relationship, watchedMovie.movie);
                        Assert.AreEqual(1, relations.Count);
                        Assert.That(relations.First().properties.ContainsKey("MinutesWatched"));
                        Assert.AreEqual(watchedMovie.minutes, relations.First().properties["MinutesWatched"]);
                    }
                }

                Transaction.Commit();
            }

            #endregion

            #region Remove Watched Movie

            using (MockModel.BeginTransaction())
            {
                foreach (var notWatched in SampleDataWatchedMovies().GroupBy(item => item.person).Select(item => item.First()))
                {
                    Debug.WriteLine($"Remove Watched Movie {notWatched.movie} for {notWatched.person.Name}");

                    notWatched.person.WatchedMovies.Remove(notWatched.movie);
                }

                Transaction.Commit();
            }

            using (MockModel.BeginTransaction())
            {
                foreach (var watched in SampleDataWatchedMovies().GroupBy(item => item.person).Select(item => (person: item.Key, watchedMovies: item.Skip(1).ToList())))
                {
                    List<string> excpected = watched.watchedMovies.Select(item => item.movie.Title).ToList();
                    List<string> actual = watched.person.WatchedMovies.Select(item => item.Title).ToList();

                    Assert.AreEqual(excpected.Count, actual.Count);
                    Assert.AreEqual(0, excpected.Except(actual).Count());
                    Assert.AreEqual(0, actual.Except(excpected).Count());

                    foreach (var watchedMovie in watched.watchedMovies)
                    {
                        var relations = ReadRelationsWithProperties(watchedMovie.person, WATCHED_MOVIE.Relationship, watchedMovie.movie);
                        Assert.AreEqual(1, relations.Count);
                        Assert.That(relations.First().properties.ContainsKey("MinutesWatched"));
                        Assert.AreEqual(watchedMovie.minutes, relations.First().properties["MinutesWatched"]);
                    }
                }

                Transaction.Commit();
            }

            #endregion

            #region Mutate Watched Movie

            using (MockModel.BeginTransaction())
            {
                foreach (var mutate in SampleDataWatchedMoviesMutations())
                {
                    Debug.WriteLine($"Mutate Watched Movie {mutate.movie} for {mutate.person.Name}");

                    var relations = WATCHED_MOVIE.Where(InNode: mutate.person, OutNode: mutate.movie);
                    Assert.AreEqual(1, relations.Count);

                    mutate.person.AddWatchedMovie(mutate.movie, MinutesWatched: relations.First().MinutesWatched + mutate.minutes);

                    Transaction.Flush();
                }

                Transaction.Commit();
            }

            using (MockModel.BeginTransaction())
            {
                foreach (var watched in SampleDataWatchedMovies().GroupBy(item => item.person).Select(item => (person: item.Key, watchedMovies: item.Skip(1).ToList())))
                {
                    List<string> excpected = watched.watchedMovies.Select(item => item.movie.Title).ToList();
                    List<string> actual = watched.person.WatchedMovies.Select(item => item.Title).ToList();

                    Assert.AreEqual(excpected.Count, actual.Count);
                    Assert.AreEqual(0, excpected.Except(actual).Count());
                    Assert.AreEqual(0, actual.Except(excpected).Count());

                    foreach (var watchedMovie in watched.watchedMovies)
                    {
                        var relations = ReadRelationsWithProperties(watchedMovie.person, WATCHED_MOVIE.Relationship, watchedMovie.movie);
                        Assert.AreEqual(1, relations.Count);
                        Assert.That(relations.First().properties.ContainsKey("MinutesWatched"));
                        Assert.AreEqual(watchedMovie.total, relations.First().properties["MinutesWatched"]);
                    }
                }

                Transaction.Commit();
            }

            #endregion
        }

        [Test]
        public void TimeDepLookupSetLegacy()
        {
            #region Set Same City

            List<TestScenario> scenariosAdd = TestScenario.Get(TestAction.AddSame);

            foreach (TestScenario scenario in scenariosAdd)
            {
                Debug.WriteLine($"Set City: {scenario}");

                using (MockModel.BeginTransaction())
                {
                    Person? person = Person.Load(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);
                    
                    City? city = City.Load(DatabaseUids.Cities.Metropolis);
                    Assert.IsNotNull(city);

                    CleanupRelations(PERSON_LIVES_IN.Relationship);

                    foreach (var relation in scenario.Initial)
                    {
                        WriteRelation(person!, PERSON_LIVES_IN.Relationship, city!, relation.from, relation.till);
                    }

                    person!.SetCity(city, scenario.Moment);

                    Transaction.Flush();

                    scenario.SetActual(ReadRelations(person, PERSON_LIVES_IN.Relationship, city!));

                    Transaction.Commit();
                }
            }

            scenariosAdd.AssertSuccess();

            #endregion

            #region Set NULL

            List<TestScenario> scenariosRemove = TestScenario.Get(TestAction.Remove);

            foreach (TestScenario scenario in scenariosRemove)
            {
                Debug.WriteLine($"Set NULL: {scenario}");

                using (MockModel.BeginTransaction())
                {
                    Person? person = Person.Load(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    City? city = City.Load(DatabaseUids.Cities.Metropolis);
                    Assert.IsNotNull(city);

                    CleanupRelations(PERSON_LIVES_IN.Relationship);

                    foreach (var relation in scenario.Initial)
                    {
                        WriteRelation(person!, PERSON_LIVES_IN.Relationship, city!, relation.from, relation.till);
                    }

                    person!.SetCity(null, scenario.Moment);

                    Transaction.Flush();

                    scenario.SetActual(ReadRelations(person, PERSON_LIVES_IN.Relationship, city!));

                    Transaction.Commit();
                }
            }

            scenariosRemove.AssertSuccess();

            #endregion
        }

        [Test]
        public void TimeDepCollAddAndRemoveLegacy()
        {
            #region Add Same Streaming Service

#if NEO4J
            using (MockModel.BeginTransaction())
            {
                CleanupRelations(SUBSCRIBED_TO_STREAMING_SERVICE.Relationship);

                Person? person = Person.Load(DatabaseUids.Persons.LinusTorvalds);
                Assert.IsNotNull(person);

                StreamingService? netflix = StreamingService.Load(DatabaseUids.StreamingServices.Netflix);
                Assert.IsNotNull(netflix);

                person!.StreamingServiceSubscriptions.Add(netflix!, DateTime.UtcNow);

                Exception ex = Assert.Throws<AggregateException>(() => Transaction.Commit());
#if NET5_0_OR_GREATER
                Assert.That(() => ex.Message.Contains("`SUBSCRIBED_TO` must have the property `MonthlyFee`"));
#else
                Assert.That(() => ex.InnerException.Message.Contains("`SUBSCRIBED_TO` must have the property `MonthlyFee`"));
#endif
            }

            #region Strip Constraint from MonthlyFee

            Execute(MakeMonthlyFeeNullable);

            #endregion
#endif

            List<TestScenario> scenariosAdd = TestScenario.Get(TestAction.AddSame);

            foreach (TestScenario scenario in scenariosAdd)
            {
                Debug.WriteLine($"Add Streaming Service: {scenario}");

                using (MockModel.BeginTransaction())
                {
                    Person? person = Person.Load(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    StreamingService? netflix = StreamingService.Load(DatabaseUids.StreamingServices.Netflix);
                    Assert.IsNotNull(netflix);

                    var initial = GetSubscribedToState(scenario.Initial, netflix!);
                    var expected = GetSubscribedToState(scenario.Expected, netflix!);

                    CleanupRelations(SUBSCRIBED_TO_STREAMING_SERVICE.Relationship);

                    foreach (var state in initial)
                    {
                        foreach (var relation in state.relations)
                            WriteRelation(person!, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target, relation.from, relation.till);
                    }

                    person!.StreamingServiceSubscriptions.Add(netflix!, scenario.Moment);

                    Transaction.Flush();

                    foreach (var state in expected.Skip(1))
                    {
                        var actual = ReadRelations(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target);
                        var expectedAsciiArt = TestScenario.DrawAsciiArtState(state.relations);
                        var actualAsciiArt = TestScenario.DrawAsciiArtState(actual);
                        Assert.AreEqual(expectedAsciiArt, actualAsciiArt);
                    }
                    scenario.SetActual(ReadRelations(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, netflix!));

                    Transaction.Commit();
                }
            }

            scenariosAdd.AssertSuccess();

#endregion

            #region Remove Same Streaming Service

            List<TestScenario> scenariosRemove = TestScenario.Get(TestAction.Remove);

            foreach (TestScenario scenario in scenariosRemove)
            {
                Debug.WriteLine($"Remove Streaming Service: {scenario}");

                using (MockModel.BeginTransaction())
                {
                    Person? person = Person.Load(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    StreamingService? netflix = StreamingService.Load(DatabaseUids.StreamingServices.Netflix);
                    Assert.IsNotNull(netflix);

                    var initial = GetSubscribedToState(scenario.Initial, netflix!);
                    var expected = GetSubscribedToState(scenario.Expected, netflix!);

                    CleanupRelations(SUBSCRIBED_TO_STREAMING_SERVICE.Relationship);

                    foreach (var state in initial)
                    {
                        foreach (var relation in state.relations)
                            WriteRelation(person!, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target, relation.from, relation.till);
                    }

                    person!.StreamingServiceSubscriptions.Remove(netflix!, scenario.Moment);

                    Transaction.Flush();

                    foreach (var state in expected.Skip(1))
                    {
                        var actual = ReadRelations(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target);
                        var expectedAsciiArt = TestScenario.DrawAsciiArtState(state.relations);
                        var actualAsciiArt = TestScenario.DrawAsciiArtState(actual);
                        Assert.AreEqual(expectedAsciiArt, actualAsciiArt);
                    }
                    scenario.SetActual(ReadRelations(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, netflix!));

                    Transaction.Commit();
                }
            }

            scenariosRemove.AssertSuccess();

            #endregion
        }

        [Test]
        public void TimeDepLookupSetWithProperties()
        {
            #region Set Same City

            List<TestScenario> scenariosAdd = TestScenario.Get(TestAction.AddSame);

            foreach (TestScenario scenario in scenariosAdd)
            {
                Debug.WriteLine($"Set City: {scenario}");

                using (MockModel.BeginTransaction())
                {
                    Person? person = Person.Load(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    City? city = City.Load(DatabaseUids.Cities.Metropolis);
                    Assert.IsNotNull(city);

                    string addr1 = CityUids.AddressLines.Metropolis.ClarkKent_Earlier[0];
                    string addr2 = CityUids.AddressLines.Metropolis.ClarkKent_Earlier[1];
                    Dictionary<string, object> properties = new Dictionary<string, object>()
                    {
                        { nameof(PERSON_LIVES_IN.AddressLine1), addr1 },
                        { nameof(PERSON_LIVES_IN.AddressLine2), addr2 },
                    };

                    CleanupRelations(PERSON_LIVES_IN.Relationship);

                    foreach (var relation in scenario.Initial)
                    {
                        WriteRelation(person!, PERSON_LIVES_IN.Relationship, city!, relation.from, relation.till, properties);
                    }

                    person!.SetCity(city, scenario.Moment, AddressLine1: addr1, AddressLine2: addr2);

                    Transaction.Flush();

                    var relationsWithProperties = ReadRelationsWithProperties(person, PERSON_LIVES_IN.Relationship, city!);
                    scenario.SetActual(relationsWithProperties.Select(item => (item.from, item.till)).ToList());

                    foreach (Dictionary<string, object>? actual in relationsWithProperties.Select(item => item.properties))
                    {
                        Assert.IsNotNull(actual);
                        Assert.AreEqual(properties.Count, actual.Count);
                        foreach (var value in properties)
                            Assert.AreEqual(value.Value, actual!.GetValue(value.Key));
                    }

                    Transaction.Commit();
                }
            }

            scenariosAdd.AssertSuccess();

            #endregion

            #region Set Same City with Different Properties

            scenariosAdd = TestScenario.Get(TestAction.AddDiff);

            foreach (TestScenario scenario in scenariosAdd)
            {
                Debug.WriteLine($"Set City: {scenario}");

                using (MockModel.BeginTransaction())
                {
                    Person? person = Person.Load(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    City? city = City.Load(DatabaseUids.Cities.Metropolis);
                    Assert.IsNotNull(city);

                    string addr1 = CityUids.AddressLines.Metropolis.ClarkKent_Earlier[0];
                    string addr2 = CityUids.AddressLines.Metropolis.ClarkKent_Earlier[1];
                    Dictionary<string, object> properties = new Dictionary<string, object>()
                    {
                        { nameof(PERSON_LIVES_IN.AddressLine1), addr1 },
                        { nameof(PERSON_LIVES_IN.AddressLine2), addr2 },
                    };

                    CleanupRelations(PERSON_LIVES_IN.Relationship);

                    foreach (var relation in scenario.Initial)
                    {
                        WriteRelation(person!, PERSON_LIVES_IN.Relationship, city!, relation.from, relation.till, properties);
                    }

                    var addr3 = CityUids.AddressLines.Metropolis.ClarkKent_Later[0];
                    var properties2 = new Dictionary<string, object>()
                    {
                        { nameof(PERSON_LIVES_IN.AddressLine1), addr3 },
                    };

                    person!.SetCity(city, scenario.Moment, AddressLine1: addr3);

                    Transaction.Flush();

                    var relationsWithProperties = ReadRelationsWithProperties(person, PERSON_LIVES_IN.Relationship, city!);
                    scenario.SetActual(relationsWithProperties.Select(item => (item.from, item.till)).ToList());

                    foreach ((DateTime from, DateTime till, Dictionary<string, object> properties) actual in relationsWithProperties)
                    {
                        var p = scenario.TestSet(actual.from, actual.till) switch
                        {
                            PropertySet.Before => properties,
                            PropertySet.After => properties2,
                            _ => throw new NotSupportedException(),
                        };

                        Assert.AreEqual(p.Count, actual.properties.Count);
                        foreach (var value in p)
                            Assert.AreEqual(value.Value, actual.properties!.GetValue(value.Key));
                    }

                    Transaction.Commit();
                }
            }

            scenariosAdd.AssertSuccess();

            #endregion

            #region Set NULL

            List<TestScenario> scenariosRemove = TestScenario.Get(TestAction.Remove);

            foreach (TestScenario scenario in scenariosRemove)
            {
                Debug.WriteLine($"Set NULL: {scenario}");

                using (MockModel.BeginTransaction())
                {
                    Person? person = Person.Load(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    City? city = City.Load(DatabaseUids.Cities.Metropolis);
                    Assert.IsNotNull(city);

                    string addr1 = CityUids.AddressLines.Metropolis.ClarkKent_Earlier[0];
                    string addr2 = CityUids.AddressLines.Metropolis.ClarkKent_Earlier[1];
                    Dictionary<string, object> properties = new Dictionary<string, object>()
                    {
                        { nameof(PERSON_LIVES_IN.AddressLine1), addr1 },
                        { nameof(PERSON_LIVES_IN.AddressLine2), addr2 },
                    };

                    CleanupRelations(PERSON_LIVES_IN.Relationship);

                    foreach (var relation in scenario.Initial)
                    {
                        WriteRelation(person!, PERSON_LIVES_IN.Relationship, city!, relation.from, relation.till, properties);
                    }

                    // person.SetCity(null, scenario.Moment); // We could use this overload, in theory it should do the same as below. However, it's already tested in the Legacy tests.
                    person!.SetCity(null, scenario.Moment, AddressLine1: addr1, AddressLine2: addr2);

                    Transaction.Flush();

                    var relationsWithProperties = ReadRelationsWithProperties(person, PERSON_LIVES_IN.Relationship, city!);
                    scenario.SetActual(relationsWithProperties.Select(item => (item.from, item.till)).ToList());

                    foreach (Dictionary<string, object>? actual in relationsWithProperties.Select(item => item.properties))
                    {
                        Assert.IsNotNull(actual);
                        Assert.AreEqual(properties.Count, actual.Count);
                        foreach (var value in properties)
                            Assert.AreEqual(value.Value, actual!.GetValue(value.Key));
                    }

                    Transaction.Commit();
                }
            }

            scenariosRemove.AssertSuccess();

            #endregion
        }

        [Test]
        public void TimeDepCollAddAndRemoveWithProperties()
        {
            #region Add Same Streaming Service

            List<TestScenario> scenariosAdd = TestScenario.Get(TestAction.AddSame);

            foreach (TestScenario scenario in scenariosAdd)
            {
                Debug.WriteLine($"Add Streaming Service: {scenario}");

                using (MockModel.BeginTransaction())
                {
                    Person? person = Person.Load(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    StreamingService? netflix = StreamingService.Load(DatabaseUids.StreamingServices.Netflix);
                    Assert.IsNotNull(netflix);

                    decimal price = StreamingServiceUids.Rates.Netflix;
                    Dictionary<string, object> properties = new Dictionary<string, object>()
                    {
                        { nameof(SUBSCRIBED_TO_STREAMING_SERVICE.MonthlyFee), price },
                    };

                    var initial = GetSubscribedToState(scenario.Initial, netflix!, price);
                    var expected = GetSubscribedToState(scenario.Expected, netflix!, price);

                    CleanupRelations(SUBSCRIBED_TO_STREAMING_SERVICE.Relationship);

                    foreach (var state in initial)
                    {
                        foreach (var relation in state.relations)
                        {
                            WriteRelation(person!, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target, relation.from, relation.till, new Dictionary<string, object>()
                            {
                                { nameof(SUBSCRIBED_TO_STREAMING_SERVICE.MonthlyFee), state.price },
                            });
                        }
                    }

                    person!.AddStreamingServiceSubscription(netflix, scenario.Moment, MonthlyFee: price);

                    Transaction.Flush();

                    foreach (var state in expected.Skip(1))
                    {
                        var actual = ReadRelations(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target);
                        var expectedAsciiArt = TestScenario.DrawAsciiArtState(state.relations);
                        var actualAsciiArt = TestScenario.DrawAsciiArtState(actual);
                        Assert.AreEqual(expectedAsciiArt, actualAsciiArt);
                    }

                    var relationsWithProperties = ReadRelationsWithProperties(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, netflix!);
                    scenario.SetActual(relationsWithProperties.Select(item => (item.from, item.till)).ToList());

                    foreach (Dictionary<string, object>? actual in relationsWithProperties.Select(item => item.properties))
                    {
                        Assert.IsNotNull(actual);
                        Assert.AreEqual(properties.Count, actual.Count);
                        foreach (var value in properties)
                            Assert.AreEqual(value.Value, actual!.GetValue(value.Key));
                    }

                    Transaction.Commit();
                }
            }

            scenariosAdd.AssertSuccess();

            #endregion

            #region Add Same Streaming Service with Different Properties

            scenariosAdd = TestScenario.Get(TestAction.AddDiff);

            foreach (TestScenario scenario in scenariosAdd)
            {
                Debug.WriteLine($"Add Streaming Service: {scenario}");

                using (MockModel.BeginTransaction())
                {
                    Person? person = Person.Load(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    StreamingService? netflix = StreamingService.Load(DatabaseUids.StreamingServices.Netflix);
                    Assert.IsNotNull(netflix);

                    decimal price = StreamingServiceUids.Rates.Hulu;
                    Dictionary<string, object> properties = new Dictionary<string, object>()
                    {
                        { nameof(SUBSCRIBED_TO_STREAMING_SERVICE.MonthlyFee), price },
                    };

                    var initial = GetSubscribedToState(scenario.Initial, netflix!, price);
                    var expected = GetSubscribedToState(scenario.Expected, netflix!, price);

                    CleanupRelations(SUBSCRIBED_TO_STREAMING_SERVICE.Relationship);

                    foreach (var state in initial)
                    {
                        foreach (var relation in state.relations)
                        {
                            WriteRelation(person!, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target, relation.from, relation.till, new Dictionary<string, object>()
                            {
                                { nameof(SUBSCRIBED_TO_STREAMING_SERVICE.MonthlyFee), state.price },
                            });
                        }
                    }
                    var price2 = StreamingServiceUids.Rates.HuluAdFree;
                    var properties2 = new Dictionary<string, object>()
                    {
                        { nameof(SUBSCRIBED_TO_STREAMING_SERVICE.MonthlyFee), price2 },
                    };

                    person!.AddStreamingServiceSubscription(netflix, scenario.Moment, MonthlyFee: price2);

                    Transaction.Flush();

                    foreach (var state in expected.Skip(1))
                    {
                        var actual = ReadRelations(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target);
                        var expectedAsciiArt = TestScenario.DrawAsciiArtState(state.relations);
                        var actualAsciiArt = TestScenario.DrawAsciiArtState(actual);
                        Assert.AreEqual(expectedAsciiArt, actualAsciiArt);
                    }

                    var relationsWithProperties = ReadRelationsWithProperties(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, netflix!);
                    scenario.SetActual(relationsWithProperties.Select(item => (item.from, item.till)).ToList());

                    foreach ((DateTime from, DateTime till, Dictionary<string, object> properties) actual in relationsWithProperties)
                    {
                        var p = scenario.TestSet(actual.from, actual.till) switch
                        {
                            PropertySet.Before => properties,
                            PropertySet.After => properties2,
                            _ => throw new NotSupportedException(),
                        };

                        Assert.AreEqual(p.Count, actual.properties.Count);
                        foreach (var value in p)
                            Assert.AreEqual(value.Value, actual.properties!.GetValue(value.Key));
                    }
                    Transaction.Commit();
                }
            }

            scenariosAdd.AssertSuccess();

            #endregion

            #region Remove Same Streaming Service

            List<TestScenario> scenariosRemove = TestScenario.Get(TestAction.Remove);

            foreach (TestScenario scenario in scenariosRemove)
            {
                Debug.WriteLine($"Remove Streaming Service: {scenario}");

                using (MockModel.BeginTransaction())
                {
                    Person? person = Person.Load(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    StreamingService? netflix = StreamingService.Load(DatabaseUids.StreamingServices.Netflix);
                    Assert.IsNotNull(netflix);

                    decimal price = StreamingServiceUids.Rates.Netflix;
                    Dictionary<string, object> properties = new Dictionary<string, object>()
                    {
                        { nameof(SUBSCRIBED_TO_STREAMING_SERVICE.MonthlyFee), price },
                    };

                    var initial = GetSubscribedToState(scenario.Initial, netflix!, price);
                    var expected = GetSubscribedToState(scenario.Expected, netflix!, price);

                    CleanupRelations(SUBSCRIBED_TO_STREAMING_SERVICE.Relationship);

                    foreach (var state in initial)
                    {
                        foreach (var relation in state.relations)
                            WriteRelation(person!, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target, relation.from, relation.till, properties);
                    }

                    person!.RemoveStreamingServiceSubscription(netflix, scenario.Moment);

                    Transaction.Flush();

                    foreach (var state in expected.Skip(1))
                    {
                        var actual = ReadRelations(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target);
                        var expectedAsciiArt = TestScenario.DrawAsciiArtState(state.relations);
                        var actualAsciiArt = TestScenario.DrawAsciiArtState(actual);
                        Assert.AreEqual(expectedAsciiArt, actualAsciiArt);
                    }

                    var relationsWithProperties = ReadRelationsWithProperties(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, netflix!);
                    scenario.SetActual(relationsWithProperties.Select(item => (item.from, item.till)).ToList());

                    foreach (Dictionary<string, object>? actual in relationsWithProperties.Select(item => item.properties))
                    {
                        Assert.IsNotNull(actual);
                        Assert.AreEqual(properties.Count, actual.Count);
                        foreach (var value in properties)
                            Assert.AreEqual(value.Value, actual!.GetValue(value.Key));
                    }

                    Transaction.Commit();
                }
            }

            scenariosRemove.AssertSuccess();

            #endregion
        }
    }
}
