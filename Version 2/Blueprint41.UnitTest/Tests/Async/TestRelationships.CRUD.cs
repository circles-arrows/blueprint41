using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Blueprint41.Core;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.UnitTest.Helper;
using Blueprint41.UnitTest.Mocks;

using Datastore.Manipulation.Async;

using NUnit.Framework;
using NUnit.Framework.Internal;

using ClientException = Neo4j.Driver.ClientException;

namespace Blueprint41.UnitTest.Tests.Async
{
    public partial class TestRelationships : TestBase
    {
        [Test]
        public async Task LookupSetLegacy()
        {
            #region Set Movie Certification

            await using (MockModel.BeginTransactionAsync())
            {
                Rating? rating = await Rating.LoadAsync(DatabaseUids.Ratings.PG);
                Assert.IsNotNull(rating);

                await CleanupRelationsAsync(MOVIE_CERTIFICATION.Relationship);

                foreach (var certification in await DatabaseUids.Movies.MoviesAsync())
                {
                    Debug.WriteLine($"Set {certification.movie.Title} certification to {certification.rating.Name}");
                    await certification.movie.SetCertificationAsync(rating);
                }

                await Transaction.FlushAsync();

                foreach (var certification in await DatabaseUids.Movies.MoviesAsync())
                {
                    await certification.movie.SetCertificationAsync(certification.rating);
                }

                await Transaction.CommitAsync();
            }

            await using (MockModel.BeginTransactionAsync())
            {
                foreach (var certification in await DatabaseUids.Movies.MoviesAsync())
                {
                    Assert.IsTrue(await certification.movie.GetCertificationAsync() == certification.rating);
                }
            }

            #endregion

            #region Set NULL


            await using (MockModel.BeginTransactionAsync())
            {
                foreach (var certification in await DatabaseUids.Movies.MoviesAsync())
                {
                    Debug.WriteLine($"Set {certification.movie.Title} certification to NULL");
                    await certification.movie.SetCertificationAsync(null);
                }

                await Transaction.CommitAsync();
            }

            await using (MockModel.BeginTransactionAsync())
            {
                foreach (var certification in await DatabaseUids.Movies.MoviesAsync())
                {
                    Assert.IsTrue(await certification.movie.GetCertificationAsync() is null);
                }
            }

            #endregion
        }

        [Test]
        public async Task CollAddAndRemoveLegacy()
        {
            #region Add Watched Movie

#if NEO4J
            await using (MockModel.BeginTransactionAsync())
            {
                await CleanupRelationsAsync(WATCHED_MOVIE.Relationship);

                var watched = (await SampleDataWatchedMoviesAsync()).First();
                (await watched.person.WatchedMoviesAsync()).Add(watched.movie);

                Exception ex = Assert.ThrowsAsync<AggregateException>(async () => await Transaction.CommitAsync());
#if NET5_0_OR_GREATER
                Assert.That(() => ex.Message.Contains("`WATCHED` must have the property `MinutesWatched`"));
#else
                Assert.That(() => ex.InnerException.Message.Contains("`WATCHED` must have the property `MinutesWatched`"));
#endif
            }

            #region Strip Constraint from MinutesWatched

            await ExecuteAsync(Blocking.TestRelationships.MakeMinutesWatchedNullable);

            #endregion
#endif

            await using (MockModel.BeginTransactionAsync())
            {
                await CleanupRelationsAsync(WATCHED_MOVIE.Relationship);

                foreach (var watched in await SampleDataWatchedMoviesAsync())
                {
                    Debug.WriteLine($"Add Watched Movie {watched.movie.Title} for {watched.person.Name}");

                    (await watched.person.WatchedMoviesAsync()).Add(watched.movie);
                }

                await Transaction.CommitAsync();
            }

            await using (MockModel.BeginTransactionAsync())
            {
                foreach (var watched in (await SampleDataWatchedMoviesAsync()).GroupBy(item => item.person).Select(item => (person: item.Key, watchedMovies: item.ToList())))
                {
                    List<string> excpected = watched.watchedMovies.Select(item => item.movie.Title).ToList();
                    List<string> actual = (await watched.person.WatchedMoviesAsync()).Select(item => item.Title).ToList();

                    Assert.AreEqual(excpected.Count, actual.Count);
                    Assert.AreEqual(0, excpected.Except(actual).Count());
                    Assert.AreEqual(0, actual.Except(excpected).Count());
                }

                await Transaction.CommitAsync();
            }

            #endregion

            #region Remove Watched Movie

            await using (MockModel.BeginTransactionAsync())
            {
                foreach (var notWatched in (await SampleDataWatchedMoviesAsync()).GroupBy(item => item.person).Select(item => item.First()))
                {
                    Debug.WriteLine($"Remove Watched Movie {notWatched.movie} for {notWatched.person.Name}");

                    (await notWatched.person.WatchedMoviesAsync()).Remove(notWatched.movie);
                }

                await Transaction.CommitAsync();
            }

            await using (MockModel.BeginTransactionAsync())
            {
                foreach (var watched in (await SampleDataWatchedMoviesAsync()).GroupBy(item => item.person).Select(item => (person: item.Key, watchedMovies: item.Skip(1).ToList())))
                {
                    List<string> excpected = watched.watchedMovies.Select(item => item.movie.Title).ToList();
                    List<string> actual = (await watched.person.WatchedMoviesAsync()).Select(item => item.Title).ToList();

                    Assert.AreEqual(excpected.Count, actual.Count);
                    Assert.AreEqual(0, excpected.Except(actual).Count());
                    Assert.AreEqual(0, actual.Except(excpected).Count());
                }

                await Transaction.CommitAsync();
            }

            #endregion
        }

        [Test]
        public async Task LookupSetWithProperties()
        {
            #region Set Movie Certification

            await using (MockModel.BeginTransactionAsync())
            {
                Rating? rating = await Rating.LoadAsync(DatabaseUids.Ratings.PG);
                Assert.IsNotNull(rating);

                await CleanupRelationsAsync(MOVIE_CERTIFICATION.Relationship);

                foreach (var certification in await DatabaseUids.Movies.MoviesAsync())
                {
                    Debug.WriteLine($"Set {certification.movie.Title} certification to {certification.rating.Name}");
                    await certification.movie.SetCertificationAsync(
                        rating,
                        FrighteningIntense: RatingComponent.None,
                        Profanity: RatingComponent.None,
                        SexAndNudity: RatingComponent.None,
                        Substances: RatingComponent.None,
                        ViolenceGore: RatingComponent.None);
                }

                await Transaction.FlushAsync();

                foreach (var certification in await DatabaseUids.Movies.MoviesAsync())
                {
                    await certification.movie.SetCertificationAsync(
                        certification.rating,
                        FrighteningIntense: certification.frighteningIntense,
                        Profanity: certification.profanity,
                        SexAndNudity: certification.sexAndNudity,
                        Substances: certification.substances,
                        ViolenceGore: certification.violenceGore);
                }

                await Transaction.CommitAsync();
            }

            await using (MockModel.BeginTransactionAsync())
            {
                foreach (var certification in await DatabaseUids.Movies.MoviesAsync())
                {
                    var details = await ReadRelationsWithPropertiesAsync(certification.movie, MOVIE_CERTIFICATION.Relationship, certification.rating);
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


            await using (MockModel.BeginTransactionAsync())
            {
                foreach (var certification in await DatabaseUids.Movies.MoviesAsync())
                {
                    Debug.WriteLine($"Set {certification.movie.Title} certification to NULL");
                    await certification.movie.SetCertificationAsync(
                        null,
                        FrighteningIntense: certification.frighteningIntense,
                        Profanity: certification.profanity,
                        SexAndNudity: certification.sexAndNudity,
                        Substances: certification.substances,
                        ViolenceGore: certification.violenceGore);
                }

                await Transaction.CommitAsync();
            }

            await using (MockModel.BeginTransactionAsync())
            {
                foreach (var certification in await DatabaseUids.Movies.MoviesAsync())
                {
                    Assert.IsTrue(await certification.movie.GetCertificationAsync() is null);
                }
            }

            #endregion
        }

        [Test]
        public async Task CollAddAndRemoveWithProperties()
        {
            #region Add Watched Movie

            await using (MockModel.BeginTransactionAsync())
            {
                await CleanupRelationsAsync(WATCHED_MOVIE.Relationship);

                foreach (var watched in await SampleDataWatchedMoviesAsync())
                {
                    Debug.WriteLine($"Add Watched Movie {watched.movie.Title} for {watched.person.Name}");

                    await watched.person.AddWatchedMovieAsync(watched.movie, MinutesWatched: watched.minutes);
                }

                await Transaction.CommitAsync();
            }

            await using (MockModel.BeginTransactionAsync())
            {
                foreach (var watched in (await SampleDataWatchedMoviesAsync()).GroupBy(item => item.person).Select(item => (person: item.Key, watchedMovies: item.ToList())))
                {
                    List<string> excpected = watched.watchedMovies.Select(item => item.movie.Title).ToList();
                    List<string> actual = (await watched.person.WatchedMoviesAsync()).Select(item => item.Title).ToList();

                    Assert.AreEqual(excpected.Count, actual.Count);
                    Assert.AreEqual(0, excpected.Except(actual).Count());
                    Assert.AreEqual(0, actual.Except(excpected).Count());

                    foreach (var watchedMovie in watched.watchedMovies)
                    {
                        var relations = await ReadRelationsWithPropertiesAsync(watchedMovie.person, WATCHED_MOVIE.Relationship, watchedMovie.movie);
                        Assert.AreEqual(1, relations.Count);
                        Assert.That(relations.First().properties.ContainsKey("MinutesWatched"));
                        Assert.AreEqual(watchedMovie.minutes, relations.First().properties["MinutesWatched"]);
                    }
                }

                await Transaction.CommitAsync();
            }

            #endregion

            #region Remove Watched Movie

            await using (MockModel.BeginTransactionAsync())
            {
                foreach (var notWatched in (await SampleDataWatchedMoviesAsync()).GroupBy(item => item.person).Select(item => item.First()))
                {
                    Debug.WriteLine($"Remove Watched Movie {notWatched.movie} for {notWatched.person.Name}");

                    (await notWatched.person.WatchedMoviesAsync()).Remove(notWatched.movie);
                }

                await Transaction.CommitAsync();
            }

            await using (MockModel.BeginTransactionAsync())
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
                        var relations = await ReadRelationsWithPropertiesAsync(watchedMovie.person, WATCHED_MOVIE.Relationship, watchedMovie.movie);
                        Assert.AreEqual(1, relations.Count);
                        Assert.That(relations.First().properties.ContainsKey("MinutesWatched"));
                        Assert.AreEqual(watchedMovie.minutes, relations.First().properties["MinutesWatched"]);
                    }
                }

                await Transaction.CommitAsync();
            }

            #endregion

            #region Mutate Watched Movie

            await using (MockModel.BeginTransactionAsync())
            {
                foreach (var mutate in await SampleDataWatchedMoviesMutationsAsync())
                {
                    Debug.WriteLine($"Mutate Watched Movie {mutate.movie} for {mutate.person.Name}");

                    var relations = await WATCHED_MOVIE.WhereAsync(InNode: mutate.person, OutNode: mutate.movie);
                    Assert.AreEqual(1, relations.Count);

                    await mutate.person.AddWatchedMovieAsync(mutate.movie, MinutesWatched: relations.First().MinutesWatched + mutate.minutes);

                    await Transaction.FlushAsync();
                }

                await Transaction.CommitAsync();
            }

            await using (MockModel.BeginTransactionAsync())
            {
                foreach (var watched in (await SampleDataWatchedMoviesAsync()).GroupBy(item => item.person).Select(item => (person: item.Key, watchedMovies: item.Skip(1).ToList())))
                {
                    List<string> excpected = watched.watchedMovies.Select(item => item.movie.Title).ToList();
                    List<string> actual = (await watched.person.WatchedMoviesAsync()).Select(item => item.Title).ToList();

                    Assert.AreEqual(excpected.Count, actual.Count);
                    Assert.AreEqual(0, excpected.Except(actual).Count());
                    Assert.AreEqual(0, actual.Except(excpected).Count());

                    foreach (var watchedMovie in watched.watchedMovies)
                    {
                        var relations = await ReadRelationsWithPropertiesAsync(watchedMovie.person, WATCHED_MOVIE.Relationship, watchedMovie.movie);
                        Assert.AreEqual(1, relations.Count);
                        Assert.That(relations.First().properties.ContainsKey("MinutesWatched"));
                        Assert.AreEqual(watchedMovie.total, relations.First().properties["MinutesWatched"]);
                    }
                }

                await Transaction.CommitAsync();
            }

            #endregion
        }

        [Test]
        public async Task TimeDepLookupSetLegacy()
        {
            #region Set Same City

            List<TestScenario> scenariosAdd = TestScenario.Get(TestAction.AddSame);

            foreach (TestScenario scenario in scenariosAdd)
            {
                Debug.WriteLine($"Set City: {scenario}");

                await using (MockModel.BeginTransactionAsync())
                {
                    Person? person = await Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    City? city = await City.LoadAsync(DatabaseUids.Cities.Metropolis);
                    Assert.IsNotNull(city);

                    await CleanupRelationsAsync(PERSON_LIVES_IN.Relationship);

                    foreach (var relation in scenario.Initial)
                    {
                        await WriteRelationAsync(person!, PERSON_LIVES_IN.Relationship, city!, relation.from, relation.till);
                    }

                    await person!.SetCityAsync(city, scenario.Moment);

                    await Transaction.FlushAsync();

                    scenario.SetActual(ReadRelations(person, PERSON_LIVES_IN.Relationship, city!));

                    await Transaction.CommitAsync();
                }
            }

            scenariosAdd.AssertSuccess();

            #endregion

            #region Set NULL

            List<TestScenario> scenariosRemove = TestScenario.Get(TestAction.Remove);

            foreach (TestScenario scenario in scenariosRemove)
            {
                Debug.WriteLine($"Set NULL: {scenario}");

                await using (MockModel.BeginTransactionAsync())
                {
                    Person? person = await Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    City? city = await City.LoadAsync(DatabaseUids.Cities.Metropolis);
                    Assert.IsNotNull(city);

                    await CleanupRelationsAsync(PERSON_LIVES_IN.Relationship);

                    foreach (var relation in scenario.Initial)
                    {
                        await WriteRelationAsync(person!, PERSON_LIVES_IN.Relationship, city!, relation.from, relation.till);
                    }

                    await person!.SetCityAsync(null, scenario.Moment);

                    await Transaction.FlushAsync();

                    scenario.SetActual(ReadRelations(person, PERSON_LIVES_IN.Relationship, city!));

                    await Transaction.CommitAsync();
                }
            }

            scenariosRemove.AssertSuccess();

            #endregion
        }

        [Test]
        public async Task TimeDepCollAddAndRemoveLegacy()
        {
            #region Add Same Streaming Service

#if NEO4J
            await using (MockModel.BeginTransactionAsync())
            {
                await CleanupRelationsAsync(SUBSCRIBED_TO_STREAMING_SERVICE.Relationship);

                Person? person = await Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds);
                Assert.IsNotNull(person);

                StreamingService? netflix = await StreamingService.LoadAsync(DatabaseUids.StreamingServices.Netflix);
                Assert.IsNotNull(netflix);

                (await person!.StreamingServiceSubscriptionsAsync()).Add(netflix!, DateTime.UtcNow);

                Exception ex = Assert.ThrowsAsync<AggregateException>(async () => await Transaction.CommitAsync());
#if NET5_0_OR_GREATER
                Assert.That(() => ex.Message.Contains("`SUBSCRIBED_TO` must have the property `MonthlyFee`"));
#else
                Assert.That(() => ex.InnerException.Message.Contains("`SUBSCRIBED_TO` must have the property `MonthlyFee`"));
#endif
            }

            #region Strip Constraint from MonthlyFee

            await ExecuteAsync(Blocking.TestRelationships.MakeMonthlyFeeNullable);

            #endregion
#endif

            List<TestScenario> scenariosAdd = TestScenario.Get(TestAction.AddSame);

            foreach (TestScenario scenario in scenariosAdd)
            {
                Debug.WriteLine($"Add Streaming Service: {scenario}");

                await using (MockModel.BeginTransactionAsync())
                {
                    Person? person = await Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    StreamingService? netflix = await StreamingService.LoadAsync(DatabaseUids.StreamingServices.Netflix);
                    Assert.IsNotNull(netflix);

                    var initial = await GetSubscribedToStateAsync(scenario.Initial, netflix!);
                    var expected = await GetSubscribedToStateAsync(scenario.Expected, netflix!);

                    await CleanupRelationsAsync(SUBSCRIBED_TO_STREAMING_SERVICE.Relationship);

                    foreach (var state in initial)
                    {
                        foreach (var relation in state.relations)
                            WriteRelation(person!, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target, relation.from, relation.till);
                    }

                    (await person!.StreamingServiceSubscriptionsAsync()).Add(netflix!, scenario.Moment);

                    await Transaction.FlushAsync();

                    foreach (var state in expected.Skip(1))
                    {
                        var actual = ReadRelations(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target);
                        var expectedAsciiArt = TestScenario.DrawAsciiArtState(state.relations);
                        var actualAsciiArt = TestScenario.DrawAsciiArtState(actual);
                        Assert.AreEqual(expectedAsciiArt, actualAsciiArt);
                    }
                    scenario.SetActual(ReadRelations(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, netflix!));

                    await Transaction.CommitAsync();
                }
            }

            scenariosAdd.AssertSuccess();

            #endregion

            #region Remove Same Streaming Service

            List<TestScenario> scenariosRemove = TestScenario.Get(TestAction.Remove);

            foreach (TestScenario scenario in scenariosRemove)
            {
                Debug.WriteLine($"Remove Streaming Service: {scenario}");

                await using (MockModel.BeginTransactionAsync())
                {
                    Person? person = await Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    StreamingService? netflix = await StreamingService.LoadAsync(DatabaseUids.StreamingServices.Netflix);
                    Assert.IsNotNull(netflix);

                    var initial = await GetSubscribedToStateAsync(scenario.Initial, netflix!);
                    var expected = await GetSubscribedToStateAsync(scenario.Expected, netflix!);

                    await CleanupRelationsAsync(SUBSCRIBED_TO_STREAMING_SERVICE.Relationship);

                    foreach (var state in initial)
                    {
                        foreach (var relation in state.relations)
                            WriteRelation(person!, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target, relation.from, relation.till);
                    }

                    (await person!.StreamingServiceSubscriptionsAsync()).Remove(netflix!, scenario.Moment);

                    await Transaction.FlushAsync();

                    foreach (var state in expected.Skip(1))
                    {
                        var actual = await ReadRelationsAsync(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target);
                        var expectedAsciiArt = TestScenario.DrawAsciiArtState(state.relations);
                        var actualAsciiArt = TestScenario.DrawAsciiArtState(actual);
                        Assert.AreEqual(expectedAsciiArt, actualAsciiArt);
                    }
                    scenario.SetActual(await ReadRelationsAsync(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, netflix!));

                    await Transaction.CommitAsync();
                }
            }

            scenariosRemove.AssertSuccess();

            #endregion
        }

        [Test]
        public async Task TimeDepLookupSetWithProperties()
        {
            #region Set Same City

            List<TestScenario> scenariosAdd = TestScenario.Get(TestAction.AddSame);

            foreach (TestScenario scenario in scenariosAdd)
            {
                Debug.WriteLine($"Set City: {scenario}");

                await using (MockModel.BeginTransactionAsync())
                {
                    Person? person = await Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    City? city = await City.LoadAsync(DatabaseUids.Cities.Metropolis);
                    Assert.IsNotNull(city);

                    string addr1 = CityUids.AddressLines.Metropolis.ClarkKent_Earlier[0];
                    string addr2 = CityUids.AddressLines.Metropolis.ClarkKent_Earlier[1];
                    Dictionary<string, object> properties = new Dictionary<string, object>()
                    {
                        { nameof(PERSON_LIVES_IN.AddressLine1), addr1 },
                        { nameof(PERSON_LIVES_IN.AddressLine2), addr2 },
                    };

                    await CleanupRelationsAsync(PERSON_LIVES_IN.Relationship);

                    foreach (var relation in scenario.Initial)
                    {
                        WriteRelation(person!, PERSON_LIVES_IN.Relationship, city!, relation.from, relation.till, properties);
                    }

                    await person!.SetCityAsync(city, scenario.Moment, AddressLine1: addr1, AddressLine2: addr2);

                    await Transaction.FlushAsync();

                    var relationsWithProperties = await ReadRelationsWithPropertiesAsync(person, PERSON_LIVES_IN.Relationship, city!);
                    scenario.SetActual(relationsWithProperties.Select(item => (item.from, item.till)).ToList());

                    foreach (Dictionary<string, object>? actual in relationsWithProperties.Select(item => item.properties))
                    {
                        Assert.IsNotNull(actual);
                        Assert.AreEqual(properties.Count, actual.Count);
                        foreach (var value in properties)
                            Assert.AreEqual(value.Value, actual!.GetValue(value.Key));
                    }

                    await Transaction.CommitAsync();
                }
            }

            scenariosAdd.AssertSuccess();

            #endregion

            #region Set Same City with Different Properties

            scenariosAdd = TestScenario.Get(TestAction.AddDiff);

            foreach (TestScenario scenario in scenariosAdd)
            {
                Debug.WriteLine($"Set City: {scenario}");

                await using (MockModel.BeginTransactionAsync())
                {
                    Person? person = await Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    City? city = await City.LoadAsync(DatabaseUids.Cities.Metropolis);
                    Assert.IsNotNull(city);

                    string addr1 = CityUids.AddressLines.Metropolis.ClarkKent_Earlier[0];
                    string addr2 = CityUids.AddressLines.Metropolis.ClarkKent_Earlier[1];
                    Dictionary<string, object> properties = new Dictionary<string, object>()
                    {
                        { nameof(PERSON_LIVES_IN.AddressLine1), addr1 },
                        { nameof(PERSON_LIVES_IN.AddressLine2), addr2 },
                    };

                    await CleanupRelationsAsync(PERSON_LIVES_IN.Relationship);

                    foreach (var relation in scenario.Initial)
                    {
                        await WriteRelationAsync(person!, PERSON_LIVES_IN.Relationship, city!, relation.from, relation.till, properties);
                    }

                    var addr3 = CityUids.AddressLines.Metropolis.ClarkKent_Later[0];
                    var properties2 = new Dictionary<string, object>()
                    {
                        { nameof(PERSON_LIVES_IN.AddressLine1), addr3 },
                    };

                    await person!.SetCityAsync(city, scenario.Moment, AddressLine1: addr3);

                    await Transaction.FlushAsync();

                    var relationsWithProperties = await ReadRelationsWithPropertiesAsync(person, PERSON_LIVES_IN.Relationship, city!);
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

                    await Transaction.CommitAsync();
                }
            }

            scenariosAdd.AssertSuccess();

            #endregion

            #region Set NULL

            List<TestScenario> scenariosRemove = TestScenario.Get(TestAction.Remove);

            foreach (TestScenario scenario in scenariosRemove)
            {
                Debug.WriteLine($"Set NULL: {scenario}");

                await using (MockModel.BeginTransactionAsync())
                {
                    Person? person = await Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    City? city = await City.LoadAsync(DatabaseUids.Cities.Metropolis);
                    Assert.IsNotNull(city);

                    string addr1 = CityUids.AddressLines.Metropolis.ClarkKent_Earlier[0];
                    string addr2 = CityUids.AddressLines.Metropolis.ClarkKent_Earlier[1];
                    Dictionary<string, object> properties = new Dictionary<string, object>()
                    {
                        { nameof(PERSON_LIVES_IN.AddressLine1), addr1 },
                        { nameof(PERSON_LIVES_IN.AddressLine2), addr2 },
                    };

                    await CleanupRelationsAsync(PERSON_LIVES_IN.Relationship);

                    foreach (var relation in scenario.Initial)
                    {
                        WriteRelation(person!, PERSON_LIVES_IN.Relationship, city!, relation.from, relation.till, properties);
                    }

                    // person.SetCity(null, scenario.Moment); // We could use this overload, in theory it should do the same as below. However, it's already tested in the Legacy tests.
                    await person!.SetCityAsync(null, scenario.Moment, AddressLine1: addr1, AddressLine2: addr2);

                    await Transaction.FlushAsync();

                    var relationsWithProperties = await ReadRelationsWithPropertiesAsync(person, PERSON_LIVES_IN.Relationship, city!);
                    scenario.SetActual(relationsWithProperties.Select(item => (item.from, item.till)).ToList());

                    foreach (Dictionary<string, object>? actual in relationsWithProperties.Select(item => item.properties))
                    {
                        Assert.IsNotNull(actual);
                        Assert.AreEqual(properties.Count, actual.Count);
                        foreach (var value in properties)
                            Assert.AreEqual(value.Value, actual!.GetValue(value.Key));
                    }

                    await Transaction.CommitAsync();
                }
            }

            scenariosRemove.AssertSuccess();

            #endregion
        }

        [Test]
        public async Task TimeDepCollAddAndRemoveWithProperties()
        {
            #region Add Same Streaming Service

            List<TestScenario> scenariosAdd = TestScenario.Get(TestAction.AddSame);

            foreach (TestScenario scenario in scenariosAdd)
            {
                Debug.WriteLine($"Add Streaming Service: {scenario}");

                await using (MockModel.BeginTransactionAsync())
                {
                    Person? person = await Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    StreamingService? netflix = await StreamingService.LoadAsync(DatabaseUids.StreamingServices.Netflix);
                    Assert.IsNotNull(netflix);

                    decimal price = StreamingServiceUids.Rates.Netflix;
                    Dictionary<string, object> properties = new Dictionary<string, object>()
                    {
                        { nameof(SUBSCRIBED_TO_STREAMING_SERVICE.MonthlyFee), price },
                    };

                    var initial = await GetSubscribedToStateAsync(scenario.Initial, netflix!, price);
                    var expected = await GetSubscribedToStateAsync(scenario.Expected, netflix!, price);

                    await CleanupRelationsAsync(SUBSCRIBED_TO_STREAMING_SERVICE.Relationship);

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

                    await person!.AddStreamingServiceSubscriptionAsync(netflix, scenario.Moment, MonthlyFee: price);

                    await Transaction.FlushAsync();

                    foreach (var state in expected.Skip(1))
                    {
                        var actual = await ReadRelationsAsync(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target);
                        var expectedAsciiArt = TestScenario.DrawAsciiArtState(state.relations);
                        var actualAsciiArt = TestScenario.DrawAsciiArtState(actual);
                        Assert.AreEqual(expectedAsciiArt, actualAsciiArt);
                    }

                    var relationsWithProperties = await ReadRelationsWithPropertiesAsync(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, netflix!);
                    scenario.SetActual(relationsWithProperties.Select(item => (item.from, item.till)).ToList());

                    foreach (Dictionary<string, object>? actual in relationsWithProperties.Select(item => item.properties))
                    {
                        Assert.IsNotNull(actual);
                        Assert.AreEqual(properties.Count, actual.Count);
                        foreach (var value in properties)
                            Assert.AreEqual(value.Value, actual!.GetValue(value.Key));
                    }

                    await Transaction.CommitAsync();
                }
            }

            scenariosAdd.AssertSuccess();

            #endregion

            #region Add Same Streaming Service with Different Properties

            scenariosAdd = TestScenario.Get(TestAction.AddDiff);

            foreach (TestScenario scenario in scenariosAdd)
            {
                Debug.WriteLine($"Add Streaming Service: {scenario}");

                await using (MockModel.BeginTransactionAsync())
                {
                    Person? person = await Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    StreamingService? netflix = await StreamingService.LoadAsync(DatabaseUids.StreamingServices.Netflix);
                    Assert.IsNotNull(netflix);

                    decimal price = StreamingServiceUids.Rates.Hulu;
                    Dictionary<string, object> properties = new Dictionary<string, object>()
                    {
                        { nameof(SUBSCRIBED_TO_STREAMING_SERVICE.MonthlyFee), price },
                    };

                    var initial = await GetSubscribedToStateAsync(scenario.Initial, netflix!, price);
                    var expected = await GetSubscribedToStateAsync(scenario.Expected, netflix!, price);

                    await CleanupRelationsAsync(SUBSCRIBED_TO_STREAMING_SERVICE.Relationship);

                    foreach (var state in initial)
                    {
                        foreach (var relation in state.relations)
                        {
                            await WriteRelationAsync(person!, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target, relation.from, relation.till, new Dictionary<string, object>()
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

                    await person!.AddStreamingServiceSubscriptionAsync(netflix, scenario.Moment, MonthlyFee: price2);

                    await Transaction.FlushAsync();

                    foreach (var state in expected.Skip(1))
                    {
                        var actual = await ReadRelationsAsync(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target);
                        var expectedAsciiArt = TestScenario.DrawAsciiArtState(state.relations);
                        var actualAsciiArt = TestScenario.DrawAsciiArtState(actual);
                        Assert.AreEqual(expectedAsciiArt, actualAsciiArt);
                    }

                    var relationsWithProperties = await ReadRelationsWithPropertiesAsync(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, netflix!);
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
                    await Transaction.CommitAsync();
                }
            }

            scenariosAdd.AssertSuccess();

            #endregion

            #region Remove Same Streaming Service

            List<TestScenario> scenariosRemove = TestScenario.Get(TestAction.Remove);

            foreach (TestScenario scenario in scenariosRemove)
            {
                Debug.WriteLine($"Remove Streaming Service: {scenario}");

                await using (MockModel.BeginTransactionAsync())
                {
                    Person? person = await Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds);
                    Assert.IsNotNull(person);

                    StreamingService? netflix = await StreamingService.LoadAsync(DatabaseUids.StreamingServices.Netflix);
                    Assert.IsNotNull(netflix);

                    decimal price = StreamingServiceUids.Rates.Netflix;
                    Dictionary<string, object> properties = new Dictionary<string, object>()
                    {
                        { nameof(SUBSCRIBED_TO_STREAMING_SERVICE.MonthlyFee), price },
                    };

                    var initial = await GetSubscribedToStateAsync(scenario.Initial, netflix!, price);
                    var expected = await GetSubscribedToStateAsync(scenario.Expected, netflix!, price);

                    await CleanupRelationsAsync(SUBSCRIBED_TO_STREAMING_SERVICE.Relationship);

                    foreach (var state in initial)
                    {
                        foreach (var relation in state.relations)
                            await WriteRelationAsync(person!, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target, relation.from, relation.till, properties);
                    }

                    await person!.RemoveStreamingServiceSubscriptionAsync(netflix, scenario.Moment);

                    await Transaction.FlushAsync();

                    foreach (var state in expected.Skip(1))
                    {
                        var actual = await ReadRelationsAsync(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target);
                        var expectedAsciiArt = TestScenario.DrawAsciiArtState(state.relations);
                        var actualAsciiArt = TestScenario.DrawAsciiArtState(actual);
                        Assert.AreEqual(expectedAsciiArt, actualAsciiArt);
                    }

                    var relationsWithProperties = await ReadRelationsWithPropertiesAsync(person, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, netflix!);
                    scenario.SetActual(relationsWithProperties.Select(item => (item.from, item.till)).ToList());

                    foreach (Dictionary<string, object>? actual in relationsWithProperties.Select(item => item.properties))
                    {
                        Assert.IsNotNull(actual);
                        Assert.AreEqual(properties.Count, actual.Count);
                        foreach (var value in properties)
                            Assert.AreEqual(value.Value, actual!.GetValue(value.Key));
                    }

                    await Transaction.CommitAsync();
                }
            }

            scenariosRemove.AssertSuccess();

            #endregion
        }
    }
}
