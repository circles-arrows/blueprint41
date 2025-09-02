using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Blueprint41.Core;
using Blueprint41.Persistence;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.UnitTest.Helper;
using Blueprint41.UnitTest.Mocks;

using neo4j = Neo4j.Driver;
using a = Datastore.Manipulation.Async;
using s = Datastore.Manipulation.Sync;

using NUnit.Framework;
using NUnit.Framework.Internal;

using ClientException = Neo4j.Driver.ClientException;

namespace Blueprint41.UnitTest.Tests.Async
{
    [TestFixture]
    public partial class TestRelationships : TestBase
    {
        #region Initialize Test Class

        public override void Setup()
        {
            base.Setup();

            DatabaseUids = Uids.SetupDb();
        }

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

                    s.Movie aliens = new s.Movie()
                    {
                        Title = "Aliens",
                    };
                    s.Movie dieHard = new s.Movie()
                    {
                        Title = "Die Hard",
                    };
                    s.Movie matrix = new s.Movie()
                    {
                        Title = "The Matrix",
                    };
                    s.Movie serenity = new s.Movie()
                    {
                        Title = "Serenity",
                    };
                    s.Movie terminator2 = new s.Movie()
                    {
                        Title = "Terminator 2: Judgment Day",
                    };
                    s.Movie theFifthElement = new s.Movie()
                    {
                        Title = "The Fifth Element",
                    };
                    s.Movie topGunMaverick = new s.Movie()
                    {
                        Title = "Top Gun: Maverick",
                    };

                    #endregion

                    #region Ratings

                    s.Rating g = new s.Rating()
                    {
                        Code = "G",
                        Name = "Rated G",
                        Description = "General audiences – All ages admitted.",
                    };
                    s.Rating pg = new s.Rating()
                    {
                        Code = "PG",
                        Name = "Rated PG",
                        Description = "Parental guidance suggested – Some material may not be suitable for children.",
                    };
                    s.Rating pg13 = new s.Rating()
                    {
                        Code = "PG-13",
                        Name = "Rated PG-13",
                        Description = "Parents strongly cautioned – Some material may be inappropriate for children under 13.",
                    };
                    s.Rating r = new s.Rating()
                    {
                        Code = "R",
                        Name = "Rated R",
                        Description = "Restricted – Under 17 requires accompanying parent or adult guardian.",
                    };
                    s.Rating nc17 = new s.Rating()
                    {
                        Code = "NC-17",
                        Name = "Rated NC-17",
                        Description = "Adults Only – No one 17 and under admitted.",
                    };

                    #endregion

                    #region Persons

                    s.Person alanTuring = new s.Person()
                    {
                        Name = "Alan Turing",
                    };
                    s.Person dennisRitchie = new s.Person()
                    {
                        Name = "Dennis Ritchie",
                    };
                    s.Person martinFowler = new s.Person()
                    {
                        Name = "Martin Fowler",
                    };
                    s.Person uncleBob = new s.Person()
                    {
                        Name = "Robert C. Martin",
                    };
                    s.Person adaLovelace = new s.Person()
                    {
                        Name = "Ada Lovelace",
                    };
                    s.Person linusTorvalds = new s.Person()
                    {
                        Name = "Linus Torvalds",
                    };
                    s.Person alanKay = new s.Person()
                    {
                        Name = "Alan Kay",
                    };
                    s.Person steveWozniak = new s.Person()
                    {
                        Name = "Steve Wozniak",
                    };
                    s.Person billGates = new s.Person()
                    {
                        Name = "Bill Gates",
                    };

                    #endregion

                    #region Cities

                    s.City london = new s.City()
                    {
                        Name = "London",
                        Country = "UK",
                    };
                    s.City littleWhinging = new s.City()
                    {
                        Name = "Little Whinging",
                        State = "Surrey",
                        Country = "UK",
                    };
                    s.City springfield = new s.City()
                    {
                        Name = "Springfield",
                        Country = "US",
                    };
                    s.City hillValley = new s.City()
                    {
                        Name = "Hill Valley",
                        State = "CA",
                        Country = "US",
                    };
                    s.City sunnydale = new s.City()
                    {
                        Name = "Sunnydale",
                        State = "CA",
                        Country = "US",
                    };
                    s.City quahog = new s.City()
                    {
                        Name = "Quahog",
                        State = "Rhode Island",
                        Country = "US",
                    };
                    s.City muncie = new s.City()
                    {
                        Name = "Muncie",
                        State = "Indiana",
                        Country = "US",
                    };
                    s.City metropolis = new s.City()
                    {
                        Name = "Metropolis",
                        Country = "US",
                    };

                    #endregion

                    #region Streaming Services

                    s.StreamingService netflix = new s.StreamingService()
                    {
                        Name = "Netflix",
                    };
                    s.StreamingService hulu = new s.StreamingService()
                    {
                        Name = "Hulu",
                    };
                    s.StreamingService peacock = new s.StreamingService()
                    {
                        Name = "Peacock",
                    };
                    s.StreamingService amazonPrimeVideo = new s.StreamingService()
                    {
                        Name = "Amazon Prime Video",
                    };
                    s.StreamingService hboMax = new s.StreamingService()
                    {
                        Name = "Max",
                    };
                    s.StreamingService disneyPlus = new s.StreamingService()
                    {
                        Name = "Disney+",
                    };
                    s.StreamingService historyVault = new s.StreamingService()
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

            public (s.Movie movie, s.Rating rating, RatingComponent frighteningIntense, RatingComponent violenceGore, RatingComponent profanity, RatingComponent substances, RatingComponent sexAndNudity)[] Movies => new[]
            {
                (s.Movie.Load(Aliens),          s.Rating.Load(Ratings.Aliens.Rating),          Ratings.Aliens.FrighteningIntense,          Ratings.Aliens.ViolenceGore,          Ratings.Aliens.Profanity,         Ratings.Aliens.Substances,          Ratings.Aliens.SexAndNudity),
                (s.Movie.Load(DieHard),         s.Rating.Load(Ratings.DieHard.Rating),         Ratings.DieHard.FrighteningIntense,         Ratings.DieHard.ViolenceGore,         Ratings.DieHard.Profanity,        Ratings.DieHard.Substances,         Ratings.DieHard.SexAndNudity),
                (s.Movie.Load(Matrix),          s.Rating.Load(Ratings.Matrix.Rating),          Ratings.Matrix.FrighteningIntense,          Ratings.Matrix.ViolenceGore,          Ratings.Matrix.Profanity,         Ratings.Matrix.Substances,          Ratings.Matrix.SexAndNudity),
                (s.Movie.Load(Serenity),        s.Rating.Load(Ratings.Serenity.Rating),        Ratings.Serenity.FrighteningIntense,        Ratings.Serenity.ViolenceGore,        Ratings.Serenity.Profanity,       Ratings.Serenity.Substances,        Ratings.Serenity.SexAndNudity),
                (s.Movie.Load(Terminator2),     s.Rating.Load(Ratings.Terminator2.Rating),     Ratings.Terminator2.FrighteningIntense,     Ratings.Terminator2.ViolenceGore,     Ratings.Terminator2.Profanity,    Ratings.Terminator2.Substances,     Ratings.Terminator2.SexAndNudity),
                (s.Movie.Load(TheFifthElement), s.Rating.Load(Ratings.TheFifthElement.Rating), Ratings.TheFifthElement.FrighteningIntense, Ratings.TheFifthElement.ViolenceGore, Ratings.TheFifthElement.Profanity,Ratings.TheFifthElement.Substances, Ratings.TheFifthElement.SexAndNudity),
                (s.Movie.Load(TopGunMaverick),  s.Rating.Load(Ratings.TopGunMaverick.Rating),  Ratings.TopGunMaverick.FrighteningIntense,  Ratings.TopGunMaverick.ViolenceGore,  Ratings.TopGunMaverick.Profanity, Ratings.TopGunMaverick.Substances,  Ratings.TopGunMaverick.SexAndNudity),
            };
            public async Task<(a.Movie movie, a.Rating rating, RatingComponent frighteningIntense, RatingComponent violenceGore, RatingComponent profanity, RatingComponent substances, RatingComponent sexAndNudity)[]> MoviesAsync() => new[]
            {
                (await a.Movie.LoadAsync(Aliens),          await a.Rating.LoadAsync(Ratings.Aliens.Rating),          Ratings.Aliens.FrighteningIntense,          Ratings.Aliens.ViolenceGore,          Ratings.Aliens.Profanity,         Ratings.Aliens.Substances,          Ratings.Aliens.SexAndNudity),
                (await a.Movie.LoadAsync(DieHard),         await a.Rating.LoadAsync(Ratings.DieHard.Rating),         Ratings.DieHard.FrighteningIntense,         Ratings.DieHard.ViolenceGore,         Ratings.DieHard.Profanity,        Ratings.DieHard.Substances,         Ratings.DieHard.SexAndNudity),
                (await a.Movie.LoadAsync(Matrix),          await a.Rating.LoadAsync(Ratings.Matrix.Rating),          Ratings.Matrix.FrighteningIntense,          Ratings.Matrix.ViolenceGore,          Ratings.Matrix.Profanity,         Ratings.Matrix.Substances,          Ratings.Matrix.SexAndNudity),
                (await a.Movie.LoadAsync(Serenity),        await a.Rating.LoadAsync(Ratings.Serenity.Rating),        Ratings.Serenity.FrighteningIntense,        Ratings.Serenity.ViolenceGore,        Ratings.Serenity.Profanity,       Ratings.Serenity.Substances,        Ratings.Serenity.SexAndNudity),
                (await a.Movie.LoadAsync(Terminator2),     await a.Rating.LoadAsync(Ratings.Terminator2.Rating),     Ratings.Terminator2.FrighteningIntense,     Ratings.Terminator2.ViolenceGore,     Ratings.Terminator2.Profanity,    Ratings.Terminator2.Substances,     Ratings.Terminator2.SexAndNudity),
                (await a.Movie.LoadAsync(TheFifthElement), await a.Rating.LoadAsync(Ratings.TheFifthElement.Rating), Ratings.TheFifthElement.FrighteningIntense, Ratings.TheFifthElement.ViolenceGore, Ratings.TheFifthElement.Profanity,Ratings.TheFifthElement.Substances, Ratings.TheFifthElement.SexAndNudity),
                (await a.Movie.LoadAsync(TopGunMaverick),  await a.Rating.LoadAsync(Ratings.TopGunMaverick.Rating),  Ratings.TopGunMaverick.FrighteningIntense,  Ratings.TopGunMaverick.ViolenceGore,  Ratings.TopGunMaverick.Profanity, Ratings.TopGunMaverick.Substances,  Ratings.TopGunMaverick.SexAndNudity),
            };
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

            public s.Person[] Persons => new[]
            {
                s.Person.Load(AlanTuring),
                s.Person.Load(DennisRitchie),
                s.Person.Load(MartinFowler),
                s.Person.Load(UncleBob),
                s.Person.Load(AdaLovelace),
                s.Person.Load(LinusTorvalds),
                s.Person.Load(AlanKay),
                s.Person.Load(SteveWozniak),
                s.Person.Load(BillGates),
            };
            public async Task<a.Person[]> PersonsAsync() => new[]
            {
                await a.Person.LoadAsync(AlanTuring),
                await a.Person.LoadAsync(DennisRitchie),
                await a.Person.LoadAsync(MartinFowler),
                await a.Person.LoadAsync(UncleBob),
                await a.Person.LoadAsync(AdaLovelace),
                await a.Person.LoadAsync(LinusTorvalds),
                await a.Person.LoadAsync(AlanKay),
                await a.Person.LoadAsync(SteveWozniak),
                await a.Person.LoadAsync(BillGates),
            };
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

            public (s.City city, string[] addressLines, string[] moveTo)[] Addresses => new[]
            {
                (s.City.Load(London),         AddressLines.London.SherlockHolmes,        null),
                (s.City.Load(London),         AddressLines.London.HerculePoirot,         null),
                (s.City.Load(LittleWhinging), AddressLines.LittleWhinging.HarryPotter,   null),
                (s.City.Load(Springfield),    AddressLines.Springfield.TheSimpsons,      null),
                (s.City.Load(HillValley),     AddressLines.HillValley.EmmettBrown,       null),
                (s.City.Load(Sunnydale),      AddressLines.Sunnydale.BuffySummers,       null),
                (s.City.Load(Quahog),         AddressLines.Quahog.PeterGriffin,          null),
                (s.City.Load(Muncie),         AddressLines.Muncie.Garfield,              null),
                (s.City.Load(Metropolis),     AddressLines.Metropolis.ClarkKent_Earlier, AddressLines.Metropolis.ClarkKent_Later)
            }!;

            public async Task<(a.City city, string[] addressLines, string[] moveTo)[]> AddressesAsync() => new[]
            {
                (await a.City.LoadAsync(London),         AddressLines.London.SherlockHolmes,        null),
                (await a.City.LoadAsync(London),         AddressLines.London.HerculePoirot,         null),
                (await a.City.LoadAsync(LittleWhinging), AddressLines.LittleWhinging.HarryPotter,   null),
                (await a.City.LoadAsync(Springfield),    AddressLines.Springfield.TheSimpsons,      null),
                (await a.City.LoadAsync(HillValley),     AddressLines.HillValley.EmmettBrown,       null),
                (await a.City.LoadAsync(Sunnydale),      AddressLines.Sunnydale.BuffySummers,       null),
                (await a.City.LoadAsync(Quahog),         AddressLines.Quahog.PeterGriffin,          null),
                (await a.City.LoadAsync(Muncie),         AddressLines.Muncie.Garfield,              null),
                (await a.City.LoadAsync(Metropolis),     AddressLines.Metropolis.ClarkKent_Earlier, AddressLines.Metropolis.ClarkKent_Later)
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

            public (s.StreamingService streamingService, decimal monthlyFee, decimal? monthlyFeeChanged)[] StreamingServices => new[]
            {
                (s.StreamingService.Load(Netflix),          Rates.Netflix,          default(decimal?)),
                (s.StreamingService.Load(Hulu),             Rates.Hulu,             Rates.HuluAdFree),
                (s.StreamingService.Load(Peacock),          Rates.Peacock,          default(decimal?)),
                (s.StreamingService.Load(AmazonPrimeVideo), Rates.AmazonPrimeVideo, default(decimal?)),
                (s.StreamingService.Load(HboMax),           Rates.HboMax,           default(decimal?)),
                (s.StreamingService.Load(DisneyPlus),       Rates.DisneyPlus,       default(decimal?)),
                (s.StreamingService.Load(HistoryVault),     Rates.HistoryVault,     default(decimal?)),
            }!;

            public async Task<(a.StreamingService streamingService, decimal monthlyFee, decimal? monthlyFeeChanged)[]> StreamingServicesAsync() => new[]
            {
                (await a.StreamingService.LoadAsync(Netflix),          Rates.Netflix,          default(decimal?)),
                (await a.StreamingService.LoadAsync(Hulu),             Rates.Hulu,             Rates.HuluAdFree),
                (await a.StreamingService.LoadAsync(Peacock),          Rates.Peacock,          default(decimal?)),
                (await a.StreamingService.LoadAsync(AmazonPrimeVideo), Rates.AmazonPrimeVideo, default(decimal?)),
                (await a.StreamingService.LoadAsync(HboMax),           Rates.HboMax,           default(decimal?)),
                (await a.StreamingService.LoadAsync(DisneyPlus),       Rates.DisneyPlus,       default(decimal?)),
                (await a.StreamingService.LoadAsync(HistoryVault),     Rates.HistoryVault,     default(decimal?)),
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
        private Task CleanupRelationsAsync(Relationship relationship)
        {
            string cypher = $"""
                MATCH (:{relationship.InEntity.Label.Name})-[r:{relationship.Neo4JRelationshipType}]->(:{relationship.OutEntity.Label.Name})
                DELETE r
                """;

            return Transaction.RunAsync(cypher);
        }

        private async Task SetupTestDataSetAsync()
        {
            await using (MockModel.BeginTransactionAsync())
            {
                // Person lives in
                foreach ((s.Person person, List<(DateTime from, DateTime till)> relations, s.City city, Dictionary<string, object> properties) data in SampleDataLivesIn())
                {
                    foreach ((DateTime from, DateTime till) in data.relations)
                        await WriteRelationAsync(data.person, s.PERSON_LIVES_IN.Relationship, data.city, from, till, data.properties);
                }

                // Movie certifications
                foreach (var certification in await DatabaseUids.Movies.MoviesAsync())
                {
                    await certification.movie.SetCertificationAsync(
                        certification.rating,
                        FrighteningIntense: certification.frighteningIntense,
                        Profanity: certification.profanity,
                        SexAndNudity: certification.sexAndNudity,
                        Substances: certification.substances,
                        ViolenceGore: certification.violenceGore
                    );
                }

                // Subscribed streaming service
                var person = s.Person.Load(DatabaseUids.Persons.LinusTorvalds);
                Assert.IsNotNull(person);

                var netflix = s.StreamingService.Load(DatabaseUids.StreamingServices.Netflix);
                Assert.IsNotNull(netflix);

                var price = StreamingServiceUids.Rates.Netflix;

                foreach (var state in GetSubscribedToState(TestScenario.RelationsFromMask(0b1111), netflix!, price))
                {
                    foreach (var relation in state.relations)
                    {
                        WriteRelation(person!, s.SUBSCRIBED_TO_STREAMING_SERVICE.Relationship, state.target, relation.from, relation.till, new Dictionary<string, object>()
                        {
                            { nameof(s.SUBSCRIBED_TO_STREAMING_SERVICE.MonthlyFee), state.price },
                        });
                    }
                }

                // Watched minutes
                foreach (var watched in SampleDataWatchedMovies())
                {
                    watched.person.AddWatchedMovie(watched.movie, MinutesWatched: watched.minutes);
                }

                await Transaction.CommitAsync();
            }
        }

        private void WriteRelation(OGM @in, Relationship relationship, OGM @out, DateTime? from, DateTime? till) => WriteRelation(@in, relationship, @out, from, till, new Dictionary<string, object>());
        private void WriteRelation(OGM @in, Relationship relationship, OGM @out, DateTime? from, DateTime? till, Dictionary<string, object> properties)
        {
            Entity inEntity = @in.GetEntity();
            Entity outEntity = @out.GetEntity();

            if (inEntity.Key is null || outEntity.Key is null)
                throw new InvalidOperationException("No key has been defined for this entity.");

            Dictionary<string, object> map = new Dictionary<string, object>(properties);
            map!.AddOrSet(relationship.StartDate, MockModel.Model.PersistenceProvider.ConvertToStoredType(from));
            map!.AddOrSet(relationship.EndDate, MockModel.Model.PersistenceProvider.ConvertToStoredType(till));
            map!.AddOrSet(relationship.CreationDate, MockModel.Model.PersistenceProvider.ConvertToStoredType(Transaction.RunningTransaction.TransactionDate));

            string cypher = $"""
                MATCH (in:{relationship.InEntity.Label.Name}), (out:{relationship.OutEntity.Label.Name})
                WHERE in.{inEntity.Key.Name} = $in AND out.{outEntity.Key.Name} = $out
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
        private Task WriteRelationAsync(OGM @in, Relationship relationship, OGM @out, DateTime? from, DateTime? till) => WriteRelationAsync(@in, relationship, @out, from, till, new Dictionary<string, object>());
        private Task WriteRelationAsync(OGM @in, Relationship relationship, OGM @out, DateTime? from, DateTime? till, Dictionary<string, object> properties)
        {
            Entity inEntity = @in.GetEntity();
            Entity outEntity = @out.GetEntity();

            if (inEntity.Key is null || outEntity.Key is null)
                throw new InvalidOperationException("No key has been defined for this entity.");

            Dictionary<string, object> map = new Dictionary<string, object>(properties);
            map!.AddOrSet(relationship.StartDate, MockModel.Model.PersistenceProvider.ConvertToStoredType(from));
            map!.AddOrSet(relationship.EndDate, MockModel.Model.PersistenceProvider.ConvertToStoredType(till));
            map!.AddOrSet(relationship.CreationDate, MockModel.Model.PersistenceProvider.ConvertToStoredType(Transaction.RunningTransaction.TransactionDate));

            string cypher = $"""
                MATCH (in:{relationship.InEntity.Label.Name}), (out:{relationship.OutEntity.Label.Name})
                WHERE in.{inEntity.Key.Name} = $in AND out.{outEntity.Key.Name} = $out
                CREATE (in)-[r:{relationship.Neo4JRelationshipType}]->(out)
                SET r = $map
                """;

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "in", @in.GetKey()! },
                { "out", @out.GetKey()! },
                { "map", map },
            };

            return Transaction.RunAsync(cypher, parameters!);
        }

        private List<(DateTime from, DateTime till)> ReadRelations(OGM @in, Relationship relationship, OGM @out)
        {
            Entity inEntity = @in.GetEntity();
            Entity outEntity = @out.GetEntity();

            if (inEntity.Key is null || outEntity.Key is null)
                throw new InvalidOperationException("No key has been defined for this entity.");

            string cypher = $"""
                MATCH (in:{relationship.InEntity.Label.Name})-[r:{relationship.Neo4JRelationshipType}]->(out:{relationship.OutEntity.Label.Name})
                WHERE in.{inEntity.Key.Name} = $in AND out.{outEntity.Key.Name} = $out
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
            //TODO: REMOVE BLOCKING METHOD

            Entity inEntity = @in.GetEntity();
            Entity outEntity = @out.GetEntity();

            if (inEntity.Key is null || outEntity.Key is null)
                throw new InvalidOperationException("No key has been defined for this entity.");

            string cypher = $"""
                             MATCH (in:{relationship.InEntity.Label.Name})-[r:{relationship.Neo4JRelationshipType}]->(out:{relationship.OutEntity.Label.Name})
                             WHERE in.{inEntity.Key.Name} = $in AND out.{outEntity.Key.Name} = $out
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
            if (relationship.InEntity.Key is null || relationship.OutEntity.Key is null)
                throw new InvalidOperationException("No key has been defined for this entity.");

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

         private List<(s.Person person, List<(DateTime from, DateTime till)> relations, s.City city, Dictionary<string, object> properties)> SampleDataLivesIn()
        {
            return new List<(s.Person, List<(DateTime, DateTime)>, s.City, Dictionary<string, object>)>()
            {
                (s.Person.Load(DatabaseUids.Persons.AdaLovelace),   TestScenario.RelationsFromMask(0b1111), s.City.Load(DatabaseUids.Cities.London),         GetAddrLines(CityUids.AddressLines.London.HerculePoirot))!,
                (s.Person.Load(DatabaseUids.Persons.AlanKay),       TestScenario.RelationsFromMask(0b0111), s.City.Load(DatabaseUids.Cities.HillValley),     GetAddrLines(CityUids.AddressLines.HillValley.EmmettBrown))!,
                (s.Person.Load(DatabaseUids.Persons.AlanTuring),    TestScenario.RelationsFromMask(0b0011), s.City.Load(DatabaseUids.Cities.London),         GetAddrLines(CityUids.AddressLines.London.SherlockHolmes))!,
                (s.Person.Load(DatabaseUids.Persons.BillGates),     TestScenario.RelationsFromMask(0b0110), s.City.Load(DatabaseUids.Cities.LittleWhinging), GetAddrLines(CityUids.AddressLines.LittleWhinging.HarryPotter))!,
                (s.Person.Load(DatabaseUids.Persons.DennisRitchie), TestScenario.RelationsFromMask(0b1010), s.City.Load(DatabaseUids.Cities.Muncie),         GetAddrLines(CityUids.AddressLines.Muncie.Garfield))!,
                (s.Person.Load(DatabaseUids.Persons.LinusTorvalds), TestScenario.RelationsFromMask(0b1100), s.City.Load(DatabaseUids.Cities.Metropolis),     GetAddrLines(CityUids.AddressLines.Metropolis.ClarkKent_Earlier))!,
                (s.Person.Load(DatabaseUids.Persons.LinusTorvalds), TestScenario.RelationsFromMask(0b0011), s.City.Load(DatabaseUids.Cities.Metropolis),     GetAddrLines(CityUids.AddressLines.Metropolis.ClarkKent_Later))!,
                (s.Person.Load(DatabaseUids.Persons.MartinFowler),  TestScenario.RelationsFromMask(0b0101), s.City.Load(DatabaseUids.Cities.Quahog),         GetAddrLines(CityUids.AddressLines.Quahog.PeterGriffin))!,
                (s.Person.Load(DatabaseUids.Persons.SteveWozniak),  TestScenario.RelationsFromMask(0b0111), s.City.Load(DatabaseUids.Cities.Springfield),    GetAddrLines(CityUids.AddressLines.Springfield.TheSimpsons))!,
                (s.Person.Load(DatabaseUids.Persons.UncleBob),      TestScenario.RelationsFromMask(0b1111), s.City.Load(DatabaseUids.Cities.Sunnydale),      GetAddrLines(CityUids.AddressLines.Sunnydale.BuffySummers))!,
            };

            Dictionary<string, object> GetAddrLines(string[] addressLines)
            {
                Dictionary<string, object> properties = new Dictionary<string, object>();
                if (addressLines.Length > 0) properties.Add(nameof(s.PERSON_LIVES_IN.AddressLine1), addressLines[0]);
                if (addressLines.Length > 1) properties.Add(nameof(s.PERSON_LIVES_IN.AddressLine2), addressLines[1]);
                if (addressLines.Length > 2) properties.Add(nameof(s.PERSON_LIVES_IN.AddressLine3), addressLines[2]);

                return properties;
            }
        }
        private List<(s.Person person, s.Movie movie, int minutes, int total)> SampleDataWatchedMovies()
        {
            return new List<(s.Person person, s.Movie movie, int minutes, int total)>()
            {
                (s.Person.Load(DatabaseUids.Persons.AlanKay),       s.Movie.Load(DatabaseUids.Movies.Aliens),          137, 137)!,
                (s.Person.Load(DatabaseUids.Persons.DennisRitchie), s.Movie.Load(DatabaseUids.Movies.DieHard),         132, 132)!,
                (s.Person.Load(DatabaseUids.Persons.LinusTorvalds), s.Movie.Load(DatabaseUids.Movies.Aliens),          137, 137)!,
                (s.Person.Load(DatabaseUids.Persons.LinusTorvalds), s.Movie.Load(DatabaseUids.Movies.Serenity),        34,  119)!,
                (s.Person.Load(DatabaseUids.Persons.MartinFowler),  s.Movie.Load(DatabaseUids.Movies.Matrix),          136, 136)!,
                (s.Person.Load(DatabaseUids.Persons.MartinFowler),  s.Movie.Load(DatabaseUids.Movies.Terminator2),     137, 137)!,
                (s.Person.Load(DatabaseUids.Persons.SteveWozniak),  s.Movie.Load(DatabaseUids.Movies.Matrix),          136, 136)!,
                (s.Person.Load(DatabaseUids.Persons.SteveWozniak),  s.Movie.Load(DatabaseUids.Movies.Terminator2),     137, 137)!,
                (s.Person.Load(DatabaseUids.Persons.UncleBob),      s.Movie.Load(DatabaseUids.Movies.TheFifthElement), 126, 126)!,
                (s.Person.Load(DatabaseUids.Persons.UncleBob),      s.Movie.Load(DatabaseUids.Movies.Serenity),        119, 119)!,
                (s.Person.Load(DatabaseUids.Persons.UncleBob),      s.Movie.Load(DatabaseUids.Movies.TopGunMaverick),  130, 130)!,
            };
        }
        private List<(s.Person person, s.Movie movie, int minutes)> SampleDataWatchedMoviesMutations()
        {
            return new List<(s.Person person, s.Movie movie, int minutes)>()
            {
                (s.Person.Load(DatabaseUids.Persons.LinusTorvalds), s.Movie.Load(DatabaseUids.Movies.Serenity), 52)!,
                (s.Person.Load(DatabaseUids.Persons.LinusTorvalds), s.Movie.Load(DatabaseUids.Movies.Serenity), 33)!,
            };
        }

        private List<(List<(DateTime from, DateTime till)> relations, s.StreamingService target, decimal price)> GetSubscribedToState(List<(DateTime from, DateTime till)> scenario, s.StreamingService item, decimal price = 0m)
        {
            var amazon = s.StreamingService.Load(DatabaseUids.StreamingServices.AmazonPrimeVideo);
            var hboMax = s.StreamingService.Load(DatabaseUids.StreamingServices.HboMax);
            var peacock = s.StreamingService.Load(DatabaseUids.StreamingServices.Peacock);
            var hulu = s.StreamingService.Load(DatabaseUids.StreamingServices.Hulu);
            var history = s.StreamingService.Load(DatabaseUids.StreamingServices.HistoryVault);

            return new List<(List<(DateTime, DateTime)> initial, s.StreamingService, decimal)>()
                {
                    (scenario, item, price),
                    (TestScenario.RelationsFromMask(0b0010), amazon,  StreamingServiceUids.Rates.AmazonPrimeVideo)!,
                    (TestScenario.RelationsFromMask(0b0101), hboMax,  StreamingServiceUids.Rates.HboMax)!,
                    (TestScenario.RelationsFromMask(0b1010), peacock, StreamingServiceUids.Rates.Peacock)!,
                    (TestScenario.RelationsFromMask(0b1001), hulu,    StreamingServiceUids.Rates.Hulu)!,
                    (TestScenario.RelationsFromMask(0b1111), history, StreamingServiceUids.Rates.HistoryVault)!,
                };
        }

        private async Task<List<(DateTime from, DateTime till)>> ReadRelationsAsync(OGM @in, Relationship relationship, OGM @out)
        {
            Entity inEntity = @in.GetEntity();
            Entity outEntity = @out.GetEntity();

            if (inEntity.Key is null || outEntity.Key is null)
                throw new InvalidOperationException("No key has been defined for this entity.");

            string cypher = $"""
                MATCH (in:{relationship.InEntity.Label.Name})-[r:{relationship.Neo4JRelationshipType}]->(out:{relationship.OutEntity.Label.Name})
                WHERE in.{inEntity.Key.Name} = $in AND out.{outEntity.Key.Name} = $out
                RETURN r.StartDate AS `From`, r.EndDate AS `Till`
                """;

            var parameters = new Dictionary<string, object>()
            {
                { "in", @in.GetKey()! },
                { "out", @out.GetKey()! },
            };

            ResultCursor result = await Transaction.RunAsync(cypher, parameters!);

            return (await result.ToListAsync()).Select(delegate (Record record)
            {
                DateTime from = Conversion<long?, DateTime?>.Convert(record["From"]?.As<long?>()) ?? Conversion.MinDateTime;
                DateTime till = Conversion<long?, DateTime?>.Convert(record["Till"]?.As<long?>()) ?? Conversion.MaxDateTime;

                return (from, till);
            }).ToList();
        }
        private async Task<List<(DateTime from, DateTime till, Dictionary<string, object> properties)>> ReadRelationsWithPropertiesAsync(OGM @in, Relationship relationship, OGM @out)
        {
            Entity inEntity = @in.GetEntity();
            Entity outEntity = @out.GetEntity();

            if (inEntity.Key is null || outEntity.Key is null)
                throw new InvalidOperationException("No key has been defined for this entity.");

            string cypher = $"""
                MATCH (in:{relationship.InEntity.Label.Name})-[r:{relationship.Neo4JRelationshipType}]->(out:{relationship.OutEntity.Label.Name})
                WHERE in.{inEntity.Key.Name} = $in AND out.{outEntity.Key.Name} = $out
                RETURN r.StartDate AS `From`, r.EndDate AS `Till`, properties(r) AS Properties
                """;

            var parameters = new Dictionary<string, object>()
            {
                { "in", @in.GetKey()! },
                { "out", @out.GetKey()! },
            };

            ResultCursor result = await Transaction.RunAsync(cypher, parameters!);
            List<Record> list = await result.ToListAsync();

            return list.Select(delegate (Record record)
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
        private async Task<List<(object inNodeKey, string[] inNodeLabels, object outNodeKey, string[] outNodeLabels, Dictionary<string, object> properties)>> ReadAllRelationsAsync(Relationship relationship)
        {
            if (relationship.InEntity.Key is null || relationship.OutEntity.Key is null)
                throw new InvalidOperationException("No key has been defined for this entity.");

            string cypher = $"""
                MATCH (in:{relationship.InEntity.Label.Name})-[r:{relationship.Neo4JRelationshipType}]->(out:{relationship.OutEntity.Label.Name})
                RETURN  in.{relationship.InEntity.Key.Name} AS InNodeKey,
                        labels(in) AS InNodeLabels,
                        out.{relationship.OutEntity.Key.Name} AS OutNodeKey,
                        labels(out) AS OutNodeLabels,
                        properties(r) AS Properties
                """;

            ResultCursor result = await Transaction.RunAsync(cypher);

            return (await result.ToListAsync()).Select(delegate (Record record)
            {
                object inNodeKey = record["InNodeKey"];
                string[] inNodeLabels = record["InNodeLabels"].As<List<string>>().ToArray();
                object outNodeKey = record["OutNodeKey"];
                string[] outNodeLabels = record["OutNodeLabels"].As<List<string>>().ToArray();
                Dictionary<string, object> properties = record["Properties"].As<Dictionary<string, object>>();

                return (inNodeKey, inNodeLabels, outNodeKey, outNodeLabels, properties);
            }).ToList();
        }

        private async Task<List<(a.Person person, List<(DateTime from, DateTime till)> relations, a.City city, Dictionary<string, object> properties)>> SampleDataLivesInAsync()
        {
            return new List<(a.Person, List<(DateTime, DateTime)>, a.City, Dictionary<string, object>)>()
            {
                (await a.Person.LoadAsync(DatabaseUids.Persons.AdaLovelace),   TestScenario.RelationsFromMask(0b1111), await a.City.LoadAsync(DatabaseUids.Cities.London),         GetAddrLines(CityUids.AddressLines.London.HerculePoirot))!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.AlanKay),       TestScenario.RelationsFromMask(0b0111), await a.City.LoadAsync(DatabaseUids.Cities.HillValley),     GetAddrLines(CityUids.AddressLines.HillValley.EmmettBrown))!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.AlanTuring),    TestScenario.RelationsFromMask(0b0011), await a.City.LoadAsync(DatabaseUids.Cities.London),         GetAddrLines(CityUids.AddressLines.London.SherlockHolmes))!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.BillGates),     TestScenario.RelationsFromMask(0b0110), await a.City.LoadAsync(DatabaseUids.Cities.LittleWhinging), GetAddrLines(CityUids.AddressLines.LittleWhinging.HarryPotter))!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.DennisRitchie), TestScenario.RelationsFromMask(0b1010), await a.City.LoadAsync(DatabaseUids.Cities.Muncie),         GetAddrLines(CityUids.AddressLines.Muncie.Garfield))!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds), TestScenario.RelationsFromMask(0b1100), await a.City.LoadAsync(DatabaseUids.Cities.Metropolis),     GetAddrLines(CityUids.AddressLines.Metropolis.ClarkKent_Earlier))!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds), TestScenario.RelationsFromMask(0b0011), await a.City.LoadAsync(DatabaseUids.Cities.Metropolis),     GetAddrLines(CityUids.AddressLines.Metropolis.ClarkKent_Later))!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.MartinFowler),  TestScenario.RelationsFromMask(0b0101), await a.City.LoadAsync(DatabaseUids.Cities.Quahog),         GetAddrLines(CityUids.AddressLines.Quahog.PeterGriffin))!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.SteveWozniak),  TestScenario.RelationsFromMask(0b0111), await a.City.LoadAsync(DatabaseUids.Cities.Springfield),    GetAddrLines(CityUids.AddressLines.Springfield.TheSimpsons))!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.UncleBob),      TestScenario.RelationsFromMask(0b1111), await a.City.LoadAsync(DatabaseUids.Cities.Sunnydale),      GetAddrLines(CityUids.AddressLines.Sunnydale.BuffySummers))!,
            };

            Dictionary<string, object> GetAddrLines(string[] addressLines)
            {
                Dictionary<string, object> properties = new Dictionary<string, object>();
                if (addressLines.Length > 0) properties.Add(nameof(s.PERSON_LIVES_IN.AddressLine1), addressLines[0]);
                if (addressLines.Length > 1) properties.Add(nameof(s.PERSON_LIVES_IN.AddressLine2), addressLines[1]);
                if (addressLines.Length > 2) properties.Add(nameof(s.PERSON_LIVES_IN.AddressLine3), addressLines[2]);

                return properties;
            }
        }
        private async Task<List<(a.Person person, a.Movie movie, int minutes, int total)>> SampleDataWatchedMoviesAsync()
        {
            return new List<(a.Person person, a.Movie movie, int minutes, int total)>()
            {
                (await a.Person.LoadAsync(DatabaseUids.Persons.AlanKay),       await a.Movie.LoadAsync(DatabaseUids.Movies.Aliens),          137, 137)!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.DennisRitchie), await a.Movie.LoadAsync(DatabaseUids.Movies.DieHard),         132, 132)!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds), await a.Movie.LoadAsync(DatabaseUids.Movies.Aliens),          137, 137)!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds), await a.Movie.LoadAsync(DatabaseUids.Movies.Serenity),        34,  119)!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.MartinFowler),  await a.Movie.LoadAsync(DatabaseUids.Movies.Matrix),          136, 136)!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.MartinFowler),  await a.Movie.LoadAsync(DatabaseUids.Movies.Terminator2),     137, 137)!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.SteveWozniak),  await a.Movie.LoadAsync(DatabaseUids.Movies.Matrix),          136, 136)!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.SteveWozniak),  await a.Movie.LoadAsync(DatabaseUids.Movies.Terminator2),     137, 137)!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.UncleBob),      await a.Movie.LoadAsync(DatabaseUids.Movies.TheFifthElement), 126, 126)!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.UncleBob),      await a.Movie.LoadAsync(DatabaseUids.Movies.Serenity),        119, 119)!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.UncleBob),      await a.Movie.LoadAsync(DatabaseUids.Movies.TopGunMaverick),  130, 130)!,
            };
        }
        private async Task<List<(a.Person person, a.Movie movie, int minutes)>> SampleDataWatchedMoviesMutationsAsync()
        {
            return new List<(a.Person person, a.Movie movie, int minutes)>()
            {
                (await a.Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds), await a.Movie.LoadAsync(DatabaseUids.Movies.Serenity), 52)!,
                (await a.Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds), await a.Movie.LoadAsync(DatabaseUids.Movies.Serenity), 33)!,
            };
        }

        private async Task<List<(List<(DateTime from, DateTime till)> relations, a.StreamingService target, decimal price)>> GetSubscribedToStateAsync(List<(DateTime from, DateTime till)> scenario, a.StreamingService item, decimal price = 0m)
        {
            var amazon = await a.StreamingService.LoadAsync(DatabaseUids.StreamingServices.AmazonPrimeVideo);
            var hboMax = await a.StreamingService.LoadAsync(DatabaseUids.StreamingServices.HboMax);
            var peacock = await a.StreamingService.LoadAsync(DatabaseUids.StreamingServices.Peacock);
            var hulu = await a.StreamingService.LoadAsync(DatabaseUids.StreamingServices.Hulu);
            var history = await a.StreamingService.LoadAsync(DatabaseUids.StreamingServices.HistoryVault);

            return new List<(List<(DateTime, DateTime)> initial, a.StreamingService, decimal)>()
                {
                    (scenario, item, price),
                    (TestScenario.RelationsFromMask(0b0010), amazon,  StreamingServiceUids.Rates.AmazonPrimeVideo)!,
                    (TestScenario.RelationsFromMask(0b0101), hboMax,  StreamingServiceUids.Rates.HboMax)!,
                    (TestScenario.RelationsFromMask(0b1010), peacock, StreamingServiceUids.Rates.Peacock)!,
                    (TestScenario.RelationsFromMask(0b1001), hulu,    StreamingServiceUids.Rates.Hulu)!,
                    (TestScenario.RelationsFromMask(0b1111), history, StreamingServiceUids.Rates.HistoryVault)!,
                };
        }





        public void Execute(Action<DatastoreModel> script)
        {
            string name = script.Method.Name;

            var model = Connect<MockModel>(true, false);

            ((IDatastoreUnitTesting)model).Execute(true, typeof(Blocking.TestRelationships).GetMethod(name));
        }
        public Task ExecuteAsync(Action<DatastoreModel> script)
        {
            return Task.Run(() =>
            {
                string name = script.Method.Name;

                var model = Connect<MockModel>(true, false);

                ((IDatastoreUnitTesting)model).Execute(true, typeof(Blocking.TestRelationships).GetMethod(name));
            });
        }

        #endregion
    }
}
