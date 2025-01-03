using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Blueprint41.Core;
using Blueprint41.Persistence;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.UnitTest.Helper;
using Blueprint41.UnitTest.Mocks;

using Datastore.Manipulation;

using NUnit.Framework;
using NUnit.Framework.Internal;

using ClientException = Neo4j.Driver.ClientException;

namespace Blueprint41.UnitTest.Tests
{
    [TestFixture]
    public partial class TestRelationships : TestBase
    {
        #region Initialize Test Class

//        [OneTimeSetUp]
//        public void OneTimeSetUp()
//        {
//            MockModel model = MockModel.Connect(new Uri(DatabaseConnectionSettings.URI), AuthToken.Basic(DatabaseConnectionSettings.USER_NAME, DatabaseConnectionSettings.PASSWORD), DatabaseConnectionSettings.DATA_BASE);
//            model.Execute(true);
//        }

//        [SetUp]
//        public void Setup()
//        {
//            TearDown();

//            // Run mock model every time because the FunctionalId is wiped out by cleanup and needs to be recreated!
//            MockModel model = new MockModel()
//            {
//                LogToConsole = true
//            };
//            model.Execute(true);

//            DatabaseUids = Uids.SetupDb();
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            using (MockModel.BeginTransaction())
//            {
//                string reset = "Match (n) detach delete n";
//                Transaction.Run(reset);

//                Transaction.Commit();
//            }
//#if NEO4J
//            using (MockModel.BeginTransaction())
//            {
//                string clearSchema = "CALL apoc.schema.assert({},{},true) YIELD label, key RETURN *";
//                Transaction.Run(clearSchema);
//                Transaction.Commit();
//            }
//#elif MEMGRAPH
//            using (MockModel.BeginSession())
//            {
//                string clearSchema = "CALL schema.assert({},{}, {}, true) YIELD label, key RETURN *";
//                Session.Run(clearSchema);
//            }
//#endif
//        }

        public Uids DatabaseUids = null!;

#nullable disable

        public record class Uids
        {
            public Uids(MovieUids movies, RatingUids ratings, PersonUids persons, CityUids cities, StreamingServiceUids streamingServices)
            {
                Movies = movies;
                Movies.Parent = this;

                Ratings = ratings;
                Ratings.Parent = this;

                Persons = persons;
                Persons.Parent = this;

                Cities = cities;
                Cities.Parent = this;

                StreamingServices = streamingServices;
                StreamingServices.Parent = this;
            }

            public MovieUids Movies;
            public RatingUids Ratings;
            public PersonUids Persons;
            public CityUids Cities;
            public StreamingServiceUids StreamingServices;

            public static Uids SetupDb()
            {
                using (MockModel.BeginTransaction())
                {
                    #region Movies

                    Movie aliens = new Movie()
                    {
                        Title = "Aliens",
                    };
                    Movie dieHard = new Movie()
                    {
                        Title = "Die Hard",
                    };
                    Movie matrix = new Movie()
                    {
                        Title = "The Matrix",
                    };
                    Movie serenity = new Movie()
                    {
                        Title = "Serenity",
                    };
                    Movie terminator2 = new Movie()
                    {
                        Title = "Terminator 2: Judgment Day",
                    };
                    Movie theFifthElement = new Movie()
                    {
                        Title = "The Fifth Element",
                    };
                    Movie topGunMaverick = new Movie()
                    {
                        Title = "Top Gun: Maverick",
                    };

                    #endregion

                    #region Ratings

                    Rating g = new Rating()
                    {
                        Code = "G",
                        Name = "Rated G",
                        Description = "General audiences – All ages admitted.",
                    };
                    Rating pg = new Rating()
                    {
                        Code = "PG",
                        Name = "Rated PG",
                        Description = "Parental guidance suggested – Some material may not be suitable for children.",
                    };
                    Rating pg13 = new Rating()
                    {
                        Code = "PG-13",
                        Name = "Rated PG-13",
                        Description = "Parents strongly cautioned – Some material may be inappropriate for children under 13.",
                    };
                    Rating r = new Rating()
                    {
                        Code = "R",
                        Name = "Rated R",
                        Description = "Restricted – Under 17 requires accompanying parent or adult guardian.",
                    };
                    Rating nc17 = new Rating()
                    {
                        Code = "NC-17",
                        Name = "Rated NC-17",
                        Description = "Adults Only – No one 17 and under admitted.",
                    };

                    #endregion

                    #region Persons

                    Person alanTuring = new Person()
                    {
                        Name = "Alan Turing",
                    };
                    Person dennisRitchie = new Person()
                    {
                        Name = "Dennis Ritchie",
                    };
                    Person martinFowler = new Person()
                    {
                        Name = "Martin Fowler",
                    };
                    Person uncleBob = new Person()
                    {
                        Name = "Robert C. Martin",
                    };
                    Person adaLovelace = new Person()
                    {
                        Name = "Ada Lovelace",
                    };
                    Person linusTorvalds = new Person()
                    {
                        Name = "Linus Torvalds",
                    };
                    Person alanKay = new Person()
                    {
                        Name = "Alan Kay",
                    };
                    Person steveWozniak = new Person()
                    {
                        Name = "Steve Wozniak",
                    };
                    Person billGates = new Person()
                    {
                        Name = "Bill Gates",
                    };

                    #endregion

                    #region Cities

                    City london = new City()
                    {
                        Name = "London",
                        Country = "UK",
                    };
                    City littleWhinging = new City()
                    {
                        Name = "Little Whinging",
                        State = "Surrey",
                        Country = "UK",
                    };
                    City springfield = new City()
                    {
                        Name = "Springfield",
                        Country = "US",
                    };
                    City hillValley = new City()
                    {
                        Name = "Hill Valley",
                        State = "CA",
                        Country = "US",
                    };
                    City sunnydale = new City()
                    {
                        Name = "Sunnydale",
                        State = "CA",
                        Country = "US",
                    };
                    City quahog = new City()
                    {
                        Name = "Quahog",
                        State = "Rhode Island",
                        Country = "US",
                    };
                    City muncie = new City()
                    {
                        Name = "Muncie",
                        State = "Indiana",
                        Country = "US",
                    };
                    City metropolis = new City()
                    {
                        Name = "Metropolis",
                        Country = "US",
                    };

                    #endregion

                    #region Streaming Services

                    StreamingService netflix = new StreamingService()
                    {
                        Name = "Netflix",
                    };
                    StreamingService hulu = new StreamingService()
                    {
                        Name = "Hulu",
                    };
                    StreamingService peacock = new StreamingService()
                    {
                        Name = "Peacock",
                    };
                    StreamingService amazonPrimeVideo = new StreamingService()
                    {
                        Name = "Amazon Prime Video",
                    };
                    StreamingService hboMax = new StreamingService()
                    {
                        Name = "Max",
                    };
                    StreamingService disneyPlus = new StreamingService()
                    {
                        Name = "Disney+",
                    };
                    StreamingService historyVault = new StreamingService()
                    {
                        Name = "History Vault",
                    };

                    #endregion

                    Transaction.Commit();

                    return new Uids(
                        new MovieUids()
                        {
                            Aliens = aliens.Uid,
                            DieHard = dieHard.Uid,
                            Matrix = matrix.Uid,
                            Serenity = serenity.Uid,
                            Terminator2 = terminator2.Uid,
                            TheFifthElement = theFifthElement.Uid,
                            TopGunMaverick = topGunMaverick.Uid,
                        },
                        new RatingUids()
                        {
                            G = g.Uid,
                            PG = pg.Uid,
                            PG13 = pg13.Uid,
                            R = r.Uid,
                            NC17 = nc17.Uid,
                        },
                        new PersonUids()
                        {
                            AlanTuring = alanTuring.Uid,
                            DennisRitchie = dennisRitchie.Uid,
                            MartinFowler = martinFowler.Uid,
                            UncleBob = uncleBob.Uid,
                            AdaLovelace = adaLovelace.Uid,
                            LinusTorvalds = linusTorvalds.Uid,
                            AlanKay = alanKay.Uid,
                            SteveWozniak = steveWozniak.Uid,
                            BillGates = billGates.Uid,
                        },
                        new CityUids()
                        {
                            London = london.Uid,
                            LittleWhinging = littleWhinging.Uid,
                            Springfield = springfield.Uid,
                            HillValley = hillValley.Uid,
                            Sunnydale = sunnydale.Uid,
                            Quahog = quahog.Uid,
                            Muncie = muncie.Uid,
                            Metropolis = metropolis.Uid,
                        },
                        new StreamingServiceUids()
                        {
                            Netflix = netflix.Uid,
                            Hulu = hulu.Uid,
                            Peacock = peacock.Uid,
                            AmazonPrimeVideo = amazonPrimeVideo.Uid,
                            HboMax = hboMax.Uid,
                            DisneyPlus = disneyPlus.Uid,
                            HistoryVault = historyVault.Uid,
                        }
                    );
                }
            }
        }
        public record class MovieUids
        {
            public Uids Parent;

            public string Aliens;
            public string DieHard;
            public string Matrix;
            public string Serenity;
            public string Terminator2;
            public string TheFifthElement;
            public string TopGunMaverick;

            public Ratings Ratings => ThreadSafe.LazyInit(ref _ratings, () => new Ratings(Parent))!;
            private Ratings _ratings = null;

            public (Movie movie, Rating rating, RatingComponent frighteningIntense, RatingComponent violenceGore, RatingComponent profanity, RatingComponent substances, RatingComponent sexAndNudity)[] Movies => new[]
            {
                (Movie.Load(Aliens),          Rating.Load(Ratings.Aliens.Rating)         , Ratings.Aliens.FrighteningIntense,          Ratings.Aliens.ViolenceGore,          Ratings.Aliens.Profanity,         Ratings.Aliens.Substances,          Ratings.Aliens.SexAndNudity),
                (Movie.Load(DieHard),         Rating.Load(Ratings.DieHard.Rating)        , Ratings.DieHard.FrighteningIntense,         Ratings.DieHard.ViolenceGore,         Ratings.DieHard.Profanity,        Ratings.DieHard.Substances,         Ratings.DieHard.SexAndNudity),
                (Movie.Load(Matrix),          Rating.Load(Ratings.Matrix.Rating)         , Ratings.Matrix.FrighteningIntense,          Ratings.Matrix.ViolenceGore,          Ratings.Matrix.Profanity,         Ratings.Matrix.Substances,          Ratings.Matrix.SexAndNudity),
                (Movie.Load(Serenity),        Rating.Load(Ratings.Serenity.Rating)       , Ratings.Serenity.FrighteningIntense,        Ratings.Serenity.ViolenceGore,        Ratings.Serenity.Profanity,       Ratings.Serenity.Substances,        Ratings.Serenity.SexAndNudity),
                (Movie.Load(Terminator2),     Rating.Load(Ratings.Terminator2.Rating)    , Ratings.Terminator2.FrighteningIntense,     Ratings.Terminator2.ViolenceGore,     Ratings.Terminator2.Profanity,    Ratings.Terminator2.Substances,     Ratings.Terminator2.SexAndNudity),
                (Movie.Load(TheFifthElement), Rating.Load(Ratings.TheFifthElement.Rating), Ratings.TheFifthElement.FrighteningIntense, Ratings.TheFifthElement.ViolenceGore, Ratings.TheFifthElement.Profanity,Ratings.TheFifthElement.Substances, Ratings.TheFifthElement.SexAndNudity),
                (Movie.Load(TopGunMaverick),  Rating.Load(Ratings.TopGunMaverick.Rating) , Ratings.TopGunMaverick.FrighteningIntense,  Ratings.TopGunMaverick.ViolenceGore,  Ratings.TopGunMaverick.Profanity, Ratings.TopGunMaverick.Substances,  Ratings.TopGunMaverick.SexAndNudity),
            }!;
        }
        public record class RatingUids
        {
            public Uids Parent;

            public string G;
            public string PG;
            public string PG13;
            public string R;
            public string NC17;
        }
        public record class PersonUids
        {
            public Uids Parent;

            public string AlanTuring;           // Inventor of the modern computer
            public string DennisRitchie;        // Unix developer & Teacher
            public string MartinFowler;         // Co creator of the Agile Manifesto
            public string UncleBob;             // Uncle Bob
            public string AdaLovelace;          // Inventor of the Ada language
            public string LinusTorvalds;        // Developer of the Linux kernel
            public string AlanKay;              // Smalltalk (first OO programming language)
            public string SteveWozniak;         // Inventor of the Apple computer
            public string BillGates;            // Programmed the most famous BASIC interpreter

            public Person[] Persons => new[]
            {
                Person.Load(AlanTuring),
                Person.Load(DennisRitchie),
                Person.Load(MartinFowler),
                Person.Load(UncleBob),
                Person.Load(AdaLovelace),
                Person.Load(LinusTorvalds),
                Person.Load(AlanKay),
                Person.Load(SteveWozniak),
                Person.Load(BillGates),
            }!;
        }
        public record class CityUids
        {
            public Uids Parent;

            public string London;
            public string LittleWhinging;
            public string Springfield;
            public string HillValley;
            public string Sunnydale;
            public string Quahog;
            public string Muncie;
            public string Metropolis;

            public static class AddressLines
            {
                public static class London
                {
                    // Sherlock Holmes - 221B Baker Street, London, UK
                    public static readonly string[] SherlockHolmes = { "221B Baker Street" };

                    // Hercule Poirot - Apt. 56B, Whitehaven Mansions, Sandhurst Square, London, UK
                    public static readonly string[] HerculePoirot = { "Apt. 56B Whitehaven Mansions", "Sandhurst Square" };
                }
                public static class LittleWhinging
                {
                    // Harry Potter - The cupboard under the Stairs, 4 Privet Drive, Little Whinging, Surrey
                    public static readonly string[] HarryPotter = { "The cupboard under the Stairs", "4 Privet Drive", "Little Whinging" };
                }
                public static class Springfield
                {
                    // the Simpsons - 742 Evergreen Terrace, Springfield
                    public static readonly string[] TheSimpsons = { "742 Evergreen Terrace" };
                }
                public static class HillValley
                {
                    // Emmett Brown (Back to the Future) - 1640 Riverside Drive, Hill Valley, California
                    public static readonly string[] EmmettBrown = { "1640 Riverside Drive" };
                }
                public static class Sunnydale
                {
                    // Buffy Summers (Buffy the Vampire Slayer) - 1630 Revello Drive, Sunnydale, CA
                    public static readonly string[] BuffySummers = { "1630 Revello Drive" };
                }
                public static class Quahog
                {
                    // Peter Griffin (Family Guy) - 31 Spooner Street, Quahog, Rhode Island
                    public static readonly string[] PeterGriffin = { "31 Spooner Street" };
                }
                public static class Muncie
                {
                    // Garfield - 711 Maple Street, Muncie, Indiana, USA
                    public static readonly string[] Garfield = { "711 Maple Street" };
                }
                public static class Metropolis
                {
                    // Clark Kent (Superman) - 344 Clinton St., Apt. 3B, Metropolis, USA (later 1938 Sullivan Lane, Metropolis)
                    public static readonly string[] ClarkKent_Earlier = { "Apt. 3B ", "344 Clinton St." };
                    public static readonly string[] ClarkKent_Later = { "1938 Sullivan Lane" };
                }
            }

            public (City city, string[] addressLines, string[] moveTo)[] Addresses => new[]
            {
                (City.Load(London),         AddressLines.London.SherlockHolmes,        null),
                (City.Load(London),         AddressLines.London.HerculePoirot,         null),
                (City.Load(LittleWhinging), AddressLines.LittleWhinging.HarryPotter,   null),
                (City.Load(Springfield),    AddressLines.Springfield.TheSimpsons,      null),
                (City.Load(HillValley),     AddressLines.HillValley.EmmettBrown,       null),
                (City.Load(Sunnydale),      AddressLines.Sunnydale.BuffySummers,       null),
                (City.Load(Quahog),         AddressLines.Quahog.PeterGriffin,          null),
                (City.Load(Muncie),         AddressLines.Muncie.Garfield,              null),
                (City.Load(Metropolis),     AddressLines.Metropolis.ClarkKent_Earlier, AddressLines.Metropolis.ClarkKent_Later)
            }!;
        }
        public record class StreamingServiceUids
        {
            public Uids Parent;

            public string Netflix;
            public string Hulu;
            public string Peacock;
            public string AmazonPrimeVideo;
            public string HboMax;
            public string DisneyPlus;
            public string HistoryVault;

            public static class Rates
            {
                public static readonly decimal Netflix = 6.99m;
                public static readonly decimal Hulu = 7.99m;
                public static readonly decimal HuluAdFree = 17.99m;
                public static readonly decimal Peacock = 5.99m;
                public static readonly decimal AmazonPrimeVideo = 8.99m;
                public static readonly decimal HboMax = 9.99m;
                public static readonly decimal DisneyPlus = 7.99m;
                public static readonly decimal HistoryVault = 4.99m;
            }

            public (StreamingService streamingService, decimal monthlyFee, decimal? monthlyFeeChanged)[] StreamingServices => new[]
            {
                (StreamingService.Load(Netflix),          Rates.Netflix,          default(decimal?)),
                (StreamingService.Load(Hulu),             Rates.Hulu,             Rates.HuluAdFree),
                (StreamingService.Load(Peacock),          Rates.Peacock,          default(decimal?)),
                (StreamingService.Load(AmazonPrimeVideo), Rates.AmazonPrimeVideo, default(decimal?)),
                (StreamingService.Load(HboMax),           Rates.HboMax,           default(decimal?)),
                (StreamingService.Load(DisneyPlus),       Rates.DisneyPlus,       default(decimal?)),
                (StreamingService.Load(HistoryVault),     Rates.HistoryVault,     default(decimal?)),
            }!;
        }

        public record class Ratings
        {
            public Ratings(Uids parent)
            {
                Aliens = new AliensRatings(parent);
                DieHard = new DieHardRatings(parent);
                Matrix = new MatrixRatings(parent);
                Serenity = new SerenityRatings(parent);
                Terminator2 = new Terminator2Ratings(parent);
                TheFifthElement = new TheFifthElementRatings(parent);
                TopGunMaverick = new TopGunMaverickRatings(parent);
            }

            public AliensRatings Aliens;
            public DieHardRatings DieHard;
            public MatrixRatings Matrix;
            public SerenityRatings Serenity;
            public Terminator2Ratings Terminator2;
            public TheFifthElementRatings TheFifthElement;
            public TopGunMaverickRatings TopGunMaverick;
        }
        public record class AliensRatings(Uids Parent)
        {
            public string Rating => Parent.Ratings.R;
            public RatingComponent FrighteningIntense = RatingComponent.Severe;
            public RatingComponent ViolenceGore = RatingComponent.Moderate;
            public RatingComponent Profanity = RatingComponent.Moderate;
            public RatingComponent Substances = RatingComponent.Mild;
            public RatingComponent SexAndNudity = RatingComponent.None;
        }
        public record class DieHardRatings(Uids Parent)
        {
            public string Rating => Parent.Ratings.R;
            public RatingComponent FrighteningIntense = RatingComponent.Moderate;
            public RatingComponent ViolenceGore = RatingComponent.Severe;
            public RatingComponent Profanity = RatingComponent.Severe;
            public RatingComponent Substances = RatingComponent.Moderate;
            public RatingComponent SexAndNudity = RatingComponent.Mild;
        }
        public record class MatrixRatings(Uids Parent)
        {
            public string Rating => Parent.Ratings.R;
            public RatingComponent FrighteningIntense = RatingComponent.None;
            public RatingComponent ViolenceGore = RatingComponent.Moderate;
            public RatingComponent Profanity = RatingComponent.Moderate;
            public RatingComponent Substances = RatingComponent.Mild;
            public RatingComponent SexAndNudity = RatingComponent.Mild;
        }
        public record class SerenityRatings(Uids Parent)
        {
            public string Rating => Parent.Ratings.PG13;
            public RatingComponent FrighteningIntense = RatingComponent.Moderate;
            public RatingComponent ViolenceGore = RatingComponent.Moderate;
            public RatingComponent Profanity = RatingComponent.Mild;
            public RatingComponent Substances = RatingComponent.Mild;
            public RatingComponent SexAndNudity = RatingComponent.Mild;
        }
        public record class Terminator2Ratings(Uids Parent)
        {
            public string Rating => Parent.Ratings.R;
            public RatingComponent FrighteningIntense = RatingComponent.Moderate;
            public RatingComponent ViolenceGore = RatingComponent.Severe;
            public RatingComponent Profanity = RatingComponent.Moderate;
            public RatingComponent Substances = RatingComponent.Mild;
            public RatingComponent SexAndNudity = RatingComponent.Mild;
        }
        public record class TheFifthElementRatings(Uids Parent)
        {
            public string Rating => Parent.Ratings.PG13;
            public RatingComponent FrighteningIntense = RatingComponent.Mild;
            public RatingComponent ViolenceGore = RatingComponent.Mild;
            public RatingComponent Profanity = RatingComponent.Mild;
            public RatingComponent Substances = RatingComponent.Mild;
            public RatingComponent SexAndNudity = RatingComponent.Moderate;
        }
        public record class TopGunMaverickRatings(Uids Parent)
        {
            public string Rating => Parent.Ratings.PG13;
            public RatingComponent FrighteningIntense = RatingComponent.Moderate;
            public RatingComponent ViolenceGore = RatingComponent.Mild;
            public RatingComponent Profanity = RatingComponent.Moderate;
            public RatingComponent Substances = RatingComponent.Mild;
            public RatingComponent SexAndNudity = RatingComponent.None;
        }

#nullable enable

        #endregion

        #region Helper Methods

        private void CleanupRelations(Relationship relationship)
        {
            string cypher = $"""
                MATCH (:{relationship.InEntity.Label.Name})-[r:{relationship.Neo4JRelationshipType}]->(:{relationship.OutEntity.Label.Name})
                DELETE r
                """;

            Transaction.Run(cypher);
        }
        private void SetupTestDataSet()
        {
            using (MockModel.BeginTransaction())
            {
                // Person lives in
                foreach ((Person person, List<(DateTime from, DateTime till)> relations, City city, Dictionary<string, object> properties) data in SampleDataLivesIn())
                {
                    foreach ((DateTime from, DateTime till) in data.relations)
                        WriteRelation(data.person, PERSON_LIVES_IN.Relationship, data.city, from, till, data.properties);
                }

                // Movie certifications
                foreach (var certification in DatabaseUids.Movies.Movies)
                {
                    certification.movie.SetCertification(
                        certification.rating,
                        FrighteningIntense: certification.frighteningIntense,
                        Profanity: certification.profanity,
                        SexAndNudity: certification.sexAndNudity,
                        Substances: certification.substances,
                        ViolenceGore: certification.violenceGore
                    );
                }

                // Subscribed streaming service
                var person = Person.Load(DatabaseUids.Persons.LinusTorvalds);
                Assert.IsNotNull(person);

                var netflix = StreamingService.Load(DatabaseUids.StreamingServices.Netflix);
                Assert.IsNotNull(netflix);

                var price = StreamingServiceUids.Rates.Netflix;

                foreach (var state in GetSubscribedToState(TestScenario.RelationsFromMask(0b1111), netflix!, price))
                {
                    foreach (var relation in state.relations)
                    {
                        WriteRelation(person!, SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target, relation.from, relation.till, new Dictionary<string, object>()
                        {
                            { nameof(SUBSCRIBED_TO_STREAMING_SERVICE.MonthlyFee), state.price },
                        });
                    }
                }

                // Watched minutes
                foreach (var watched in SampleDataWatchedMovies())
                {
                    watched.person.AddWatchedMovie(watched.movie, MinutesWatched: watched.minutes);
                }

                Transaction.Commit();
            }
        }

        private void WriteRelation(OGM @in, Relationship relationship, OGM @out, DateTime? from, DateTime? till) => WriteRelation(@in, relationship, @out, from, till, new Dictionary<string, object>());
        private void WriteRelation(OGM @in, Relationship relationship, OGM @out, DateTime? from, DateTime? till, Dictionary<string, object> properties)
        {
            Dictionary<string, object> map = new Dictionary<string, object>(properties);
            map!.AddOrSet(relationship.StartDate, MockModel.Model.PersistenceProvider.ConvertToStoredType(from));
            map!.AddOrSet(relationship.EndDate, MockModel.Model.PersistenceProvider.ConvertToStoredType(till));
            map!.AddOrSet(relationship.CreationDate, MockModel.Model.PersistenceProvider.ConvertToStoredType(Transaction.RunningTransaction.TransactionDate));

            string cypher = $"""
                MATCH (in:{relationship.InEntity.Label.Name}), (out:{relationship.OutEntity.Label.Name})
                WHERE in.{@in.GetEntity().Key.Name} = $in AND out.{@out.GetEntity().Key.Name} = $out
                CREATE (in)-[r:{relationship.Neo4JRelationshipType}]->(out)
                SET r = $map
                """;

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "in", @in.GetKey()! },
                { "out", @out.GetKey()! },
                { "map", map },
            };

            Transaction.Run(cypher, parameters!);
        }

        private List<(DateTime from, DateTime till)> ReadRelations(OGM @in, Relationship relationship, OGM @out)
        {
            string cypher = $"""
                MATCH (in:{relationship.InEntity.Label.Name})-[r:{relationship.Neo4JRelationshipType}]->(out:{relationship.OutEntity.Label.Name})
                WHERE in.{@in.GetEntity().Key.Name} = $in AND out.{@out.GetEntity().Key.Name} = $out
                RETURN r.StartDate AS `From`, r.EndDate AS `Till`
                """;

            var parameters = new Dictionary<string, object>()
            {
                { "in", @in.GetKey()! },
                { "out", @out.GetKey()! },
            };

            ResultCursor result = Transaction.Run(cypher, parameters!);

            return result.ToList().Select(delegate (Record record)
            {
                DateTime from = Conversion<long?, DateTime?>.Convert(record["From"]?.As<long?>()) ?? Conversion.MinDateTime;
                DateTime till = Conversion<long?, DateTime?>.Convert(record["Till"]?.As<long?>()) ?? Conversion.MaxDateTime;

                return (from, till);
            }).ToList();
        }
        private List<(DateTime from, DateTime till, Dictionary<string, object> properties)> ReadRelationsWithProperties(OGM @in, Relationship relationship, OGM @out)
        {
            string cypher = $"""
                MATCH (in:{relationship.InEntity.Label.Name})-[r:{relationship.Neo4JRelationshipType}]->(out:{relationship.OutEntity.Label.Name})
                WHERE in.{@in.GetEntity().Key.Name} = $in AND out.{@out.GetEntity().Key.Name} = $out
                RETURN r.StartDate AS `From`, r.EndDate AS `Till`, properties(r) AS Properties
                """;

            var parameters = new Dictionary<string, object>()
            {
                { "in", @in.GetKey()! },
                { "out", @out.GetKey()! },
            };

            ResultCursor result = Transaction.Run(cypher, parameters!);

            return result.ToList().Select(delegate (Record record)
            {
                DateTime from = Conversion<long?, DateTime?>.Convert(record["From"]?.As<long?>()) ?? Conversion.MinDateTime;
                DateTime till = Conversion<long?, DateTime?>.Convert(record["Till"]?.As<long?>()) ?? Conversion.MaxDateTime;
                Dictionary<string, object> properties = record["Properties"]
                    .As<Dictionary<string, object>>()
                    .Where(item => item.Key != relationship.StartDate && item.Key != relationship.EndDate && item.Key != relationship.CreationDate)
                    .ToDictionary(item => item.Key, item => item.Value);

                return (from, till, properties);
            }).ToList();
        }
        private List<(object inNodeKey, string[] inNodeLabels, object outNodeKey, string[] outNodeLabels, Dictionary<string, object> properties)> ReadAllRelations(Relationship relationship)
        {
            string cypher = $"""
                MATCH (in:{relationship.InEntity.Label.Name})-[r:{relationship.Neo4JRelationshipType}]->(out:{relationship.OutEntity.Label.Name})
                RETURN  in.{relationship.InEntity.Key.Name} AS InNodeKey,
                        labels(in) AS InNodeLabels,
                        out.{relationship.OutEntity.Key.Name} AS OutNodeKey,
                        labels(out) AS OutNodeLabels,
                        properties(r) AS Properties
                """;

            ResultCursor result = Transaction.Run(cypher);

            return result.ToList().Select(delegate (Record record)
            {
                object inNodeKey = record["InNodeKey"];
                string[] inNodeLabels = record["InNodeLabels"].As<List<string>>().ToArray();
                object outNodeKey = record["OutNodeKey"];
                string[] outNodeLabels = record["OutNodeLabels"].As<List<string>>().ToArray();
                Dictionary<string, object> properties = record["Properties"].As<Dictionary<string, object>>();

                return (inNodeKey, inNodeLabels, outNodeKey, outNodeLabels, properties);
            }).ToList();
        }

        private List<(Person person, List<(DateTime from, DateTime till)> relations, City city, Dictionary<string, object> properties)> SampleDataLivesIn()
        {
            return new List<(Person, List<(DateTime, DateTime)>, City, Dictionary<string, object>)>()
            {
                (Person.Load(DatabaseUids.Persons.AdaLovelace),   TestScenario.RelationsFromMask(0b1111), City.Load(DatabaseUids.Cities.London),         GetAddrLines(CityUids.AddressLines.London.HerculePoirot))!,
                (Person.Load(DatabaseUids.Persons.AlanKay),       TestScenario.RelationsFromMask(0b0111), City.Load(DatabaseUids.Cities.HillValley),     GetAddrLines(CityUids.AddressLines.HillValley.EmmettBrown))!,
                (Person.Load(DatabaseUids.Persons.AlanTuring),    TestScenario.RelationsFromMask(0b0011), City.Load(DatabaseUids.Cities.London),         GetAddrLines(CityUids.AddressLines.London.SherlockHolmes))!,
                (Person.Load(DatabaseUids.Persons.BillGates),     TestScenario.RelationsFromMask(0b0110), City.Load(DatabaseUids.Cities.LittleWhinging), GetAddrLines(CityUids.AddressLines.LittleWhinging.HarryPotter))!,
                (Person.Load(DatabaseUids.Persons.DennisRitchie), TestScenario.RelationsFromMask(0b1010), City.Load(DatabaseUids.Cities.Muncie),         GetAddrLines(CityUids.AddressLines.Muncie.Garfield))!,
                (Person.Load(DatabaseUids.Persons.LinusTorvalds), TestScenario.RelationsFromMask(0b1100), City.Load(DatabaseUids.Cities.Metropolis),     GetAddrLines(CityUids.AddressLines.Metropolis.ClarkKent_Earlier))!,
                (Person.Load(DatabaseUids.Persons.LinusTorvalds), TestScenario.RelationsFromMask(0b0011), City.Load(DatabaseUids.Cities.Metropolis),     GetAddrLines(CityUids.AddressLines.Metropolis.ClarkKent_Later))!,
                (Person.Load(DatabaseUids.Persons.MartinFowler),  TestScenario.RelationsFromMask(0b0101), City.Load(DatabaseUids.Cities.Quahog),         GetAddrLines(CityUids.AddressLines.Quahog.PeterGriffin))!,
                (Person.Load(DatabaseUids.Persons.SteveWozniak),  TestScenario.RelationsFromMask(0b0111), City.Load(DatabaseUids.Cities.Springfield),    GetAddrLines(CityUids.AddressLines.Springfield.TheSimpsons))!,
                (Person.Load(DatabaseUids.Persons.UncleBob),      TestScenario.RelationsFromMask(0b1111), City.Load(DatabaseUids.Cities.Sunnydale),      GetAddrLines(CityUids.AddressLines.Sunnydale.BuffySummers))!,
            };

            Dictionary<string, object> GetAddrLines(string[] addressLines)
            {
                Dictionary<string, object> properties = new Dictionary<string, object>();
                if (addressLines.Length > 0) properties.Add(nameof(PERSON_LIVES_IN.AddressLine1), addressLines[0]);
                if (addressLines.Length > 1) properties.Add(nameof(PERSON_LIVES_IN.AddressLine2), addressLines[1]);
                if (addressLines.Length > 2) properties.Add(nameof(PERSON_LIVES_IN.AddressLine3), addressLines[2]);

                return properties;
            }
        }
        private List<(Person person, Movie movie, int minutes, int total)> SampleDataWatchedMovies()
        {
            return new List<(Person person, Movie movie, int minutes, int total)>()
            {
                (Person.Load(DatabaseUids.Persons.AlanKay),       Movie.Load(DatabaseUids.Movies.Aliens),          137, 137)!,
                (Person.Load(DatabaseUids.Persons.DennisRitchie), Movie.Load(DatabaseUids.Movies.DieHard),         132, 132)!,
                (Person.Load(DatabaseUids.Persons.LinusTorvalds), Movie.Load(DatabaseUids.Movies.Aliens),          137, 137)!,
                (Person.Load(DatabaseUids.Persons.LinusTorvalds), Movie.Load(DatabaseUids.Movies.Serenity),        34,  119)!,
                (Person.Load(DatabaseUids.Persons.MartinFowler),  Movie.Load(DatabaseUids.Movies.Matrix),          136, 136)!,
                (Person.Load(DatabaseUids.Persons.MartinFowler),  Movie.Load(DatabaseUids.Movies.Terminator2),     137, 137)!,
                (Person.Load(DatabaseUids.Persons.SteveWozniak),  Movie.Load(DatabaseUids.Movies.Matrix),          136, 136)!,
                (Person.Load(DatabaseUids.Persons.SteveWozniak),  Movie.Load(DatabaseUids.Movies.Terminator2),     137, 137)!,
                (Person.Load(DatabaseUids.Persons.UncleBob),      Movie.Load(DatabaseUids.Movies.TheFifthElement), 126, 126)!,
                (Person.Load(DatabaseUids.Persons.UncleBob),      Movie.Load(DatabaseUids.Movies.Serenity),        119, 119)!,
                (Person.Load(DatabaseUids.Persons.UncleBob),      Movie.Load(DatabaseUids.Movies.TopGunMaverick),  130, 130)!,
            };
        }
        private List<(Person person, Movie movie, int minutes)> SampleDataWatchedMoviesMutations()
        {
            return new List<(Person person, Movie movie, int minutes)>()
            {
                (Person.Load(DatabaseUids.Persons.LinusTorvalds), Movie.Load(DatabaseUids.Movies.Serenity), 52)!,
                (Person.Load(DatabaseUids.Persons.LinusTorvalds), Movie.Load(DatabaseUids.Movies.Serenity), 33)!,
            };
        }

        private List<(List<(DateTime from, DateTime till)> relations, StreamingService target, decimal price)> GetSubscribedToState(List<(DateTime from, DateTime till)> scenario, StreamingService item, decimal price = 0m)
        {
            var amazon = StreamingService.Load(DatabaseUids.StreamingServices.AmazonPrimeVideo);
            var hboMax = StreamingService.Load(DatabaseUids.StreamingServices.HboMax);
            var peacock = StreamingService.Load(DatabaseUids.StreamingServices.Peacock);
            var hulu = StreamingService.Load(DatabaseUids.StreamingServices.Hulu);
            var history = StreamingService.Load(DatabaseUids.StreamingServices.HistoryVault);

            return new List<(List<(DateTime, DateTime)> initial, StreamingService, decimal)>()
                {
                    (scenario, item, price),
                    (TestScenario.RelationsFromMask(0b0010), amazon,  StreamingServiceUids.Rates.AmazonPrimeVideo)!,
                    (TestScenario.RelationsFromMask(0b0101), hboMax,  StreamingServiceUids.Rates.HboMax)!,
                    (TestScenario.RelationsFromMask(0b1010), peacock, StreamingServiceUids.Rates.Peacock)!,
                    (TestScenario.RelationsFromMask(0b1001), hulu,    StreamingServiceUids.Rates.Hulu)!,
                    (TestScenario.RelationsFromMask(0b1111), history, StreamingServiceUids.Rates.HistoryVault)!,
                };
        }

        #endregion
    }
}
