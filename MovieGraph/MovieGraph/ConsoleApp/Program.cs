using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence.Driver.v5;
using Blueprint41.Query;
using Domain.Data.Manipulation;
using MovieGraph.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PersistenceProvider.CurrentPersistenceProvider = new Neo4jPersistenceProvider($"bolt://localhost:7687", $"neo4j", $"neo");
            

            // Execute only once
            CreateMovieGraph();

            FindActorTomHanks();
            FindMovieCloudAtlas();
            Find10People();
            FindMoviesIn1990s();

            ListAllTomHanksMovies();
            DirectedCloutAtlas();
            TomHanksCoActors();
            SomeoneToIntroduceToTomHanks();

            Console.WriteLine("Done. Press any key to exit.");
            Console.ReadLine();
        }

        private static void SomeoneToIntroduceToTomHanks()
        {
            //Find someone to introduce Tom Hanks to Tom Cruise

            //MATCH (tom:Person {name:"Tom Hanks"})-[:ACTED_IN]->(m)<-[:ACTED_IN]-(coActors),
            //      (coActors)-[:ACTED_IN]->(m2)<-[:ACTED_IN]-(cruise: Person { name: "Tom Cruise"})
            //RETURN tom, m, coActors, m2, cruise

            List<string> coActorsWithCruise = new List<string>() { "Bonnie Hunt", "Meg Ryan", "Kevin Bacon" };

            using (Transaction.Begin())
            {
                ICompiled query = Transaction.CompiledQuery
                    .Match
                    (
                        Domain.Data.Query.Node.Person.Alias(out var tomHAlias)
                        .In
                        .ACTED_IN
                        .Out
                        .Movie.Alias(out var m)
                        .Out
                        .ACTED_IN
                        .In
                        .Person.Alias(out var coActorsAlias),
                        Domain.Data.Query.Node.Person.UseExistingAlias(coActorsAlias)
                        .In
                        .ACTED_IN
                        .Out
                        .Movie.Alias(out var m2)
                        .Out
                        .ACTED_IN
                        .In
                        .Person.Alias(out var tomCAlias)
                    )
                    .Where(tomHAlias.name == "Tom Hanks", tomCAlias.name == "Tom Cruise")
                    .Return(tomHAlias, m, coActorsAlias, m2, tomCAlias)
                    .Compile();

                RawResult records = Transaction.RunningTransaction.Run(query.ToString());

                foreach (RawRecord record in records)
                {   
                    var tomH = record[0].As<RawNode>();
                    var movieWithTom = record[1].As<RawNode>();
                    var coActor = record[2].As<RawNode>();
                    var movieWithCoActorAndTom = record[3].As<RawNode>();
                    var cruise = record[4].As<RawNode>();

                    Debug.Assert(coActorsWithCruise.Contains(coActor.Properties["name"].ToString()));
                    Console.WriteLine($"{tomH.Properties["name"].ToString()} -> {movieWithTom.Properties["title"].ToString()} -> {coActor.Properties["name"].ToString()} <- {movieWithCoActorAndTom.Properties["title"].ToString()} <- {cruise.Properties["name"].ToString()}");
                }
            }
        }

        private static void TomHanksCoActors()
        {
            // Tom Hanks' co-actors...
            //MATCH (tom:Person {name:"Tom Hanks"})-[:ACTED_IN]->(m)<-[:ACTED_IN]-(coActors) RETURN DISTINCT coActors.name

            #region CoActors
            List<string> coActors = new List<string>()
            {
                "Philip Seymour Hoffman",
                "Julia Roberts",
                "Madonna",
                "Bill Paxton",
                "Geena Davis",
                "Lori Petty",
                "Rosie O'Donnell",
                "Kevin Bacon",
                "Ed Harris",
                "Gary Sinise",
                "Helen Hunt",
                "Sam Rockwell",
                "James Cromwell",
                "Bonnie Hunt",
                "David Morse",
                "Michael Clarke Duncan",
                "Patricia Clarkson",
                "Ian McKellen",
                "Audrey Tautou",
                "Paul Bettany",
                "Jim Broadbent",
                "Hugo Weaving",
                "Halle Berry",
                "Nathan Lane",
                "Victor Garber",
                "Bill Pullman",
                "Rita Wilson",
                "Liv Tyler",
                "Charlize Theron",
                "Parker Posey",
                "Greg Kinnear",
                "Meg Ryan",
                "Steve Zahn",
                "Dave Chappelle",
            };
            #endregion

            using (Transaction.Begin())
            {
                ICompiled query = Transaction.CompiledQuery
                    .Match
                    (
                        Domain.Data.Query.Node.Person.Alias(out var actors)
                        .In
                        .ACTED_IN
                        .Out
                        .Movie
                        .Out
                        .ACTED_IN
                        .In
                        .Person.Alias(out var coActorsAlias)
                    )
                    .Where(actors.name == "Tom Hanks")
                    .Return(coActorsAlias)
                    .Compile();

                List<Person> coActorsNeo = Person.LoadWhere(query);

                Debug.Assert(coActors.Count == coActorsNeo.Count);
                foreach (string coActor in coActors)
                    Debug.Assert(coActorsNeo.SingleOrDefault(x => x.name == coActor) != null);
            }
        }

        private static void DirectedCloutAtlas()
        {
            //MATCH (cloudAtlas {title: "Cloud Atlas"})<-[:DIRECTED]-(directors) RETURN directors.name
            List<string> cloudAtlasDirectors = new List<string>() { "Tom Tykwer", "Lilly Wachowski", "Lana Wachowski" };

            // Solution 1
            using (Transaction.Begin())
            {
                ICompiled query = Transaction.CompiledQuery
                    .Match
                    (
                        Domain.Data.Query.Node.Movie.Alias(out var movieAlias)
                        .Out
                        .DIRECTED
                        .In
                        .Person.Alias(out var personAlias)
                    )
                    .Where(movieAlias.title == "Cloud Atlas")
                    .Return(personAlias)
                    .Compile();

                List<Person> directors = Person.LoadWhere(query);

                Debug.Assert(directors.Count == cloudAtlasDirectors.Count);
                foreach (string director in cloudAtlasDirectors)
                    Debug.Assert(directors.SingleOrDefault(x => x.name == director) != null);
            }

            // Solution 2
            using (Transaction.Begin())
            {
                ICompiled query = Transaction
                    .CompiledQuery
                    .Match(Domain.Data.Query.Node.Movie.Alias(out var movieAlias))
                    .Where(movieAlias.title == "Cloud Atlas")
                    .Return(movieAlias)
                    .Compile();

                Movie cloudAtlas = Movie.LoadWhere(query).SingleOrDefault();
                Debug.Assert(cloudAtlas.title == "Cloud Atlas");

                Debug.Assert(cloudAtlasDirectors.Count == cloudAtlas.Directors.Count);
                foreach (string director in cloudAtlasDirectors)
                    Debug.Assert(cloudAtlas.Directors.SingleOrDefault(x => x.name == director) != null);
            }
        }

        private static void ListAllTomHanksMovies()
        {
            // List all Tom Hanks movies...
            // MATCH(tom: Person { name: "Tom Hanks"})-[:ACTED_IN]->(tomHanksMovies) RETURN tom, tomHanksMovies

            List<string> movies = new List<string>()
                {
                    "Charlie Wilson's War",
                    "A League of Their Own",
                    "The Polar Express",
                    "Apollo 13",
                    "Cast Away",
                    "The Green Mile",
                    "The Da Vinci Code",
                    "Cloud Atlas",
                    "Joe Versus the Volcano",
                    "Sleepless In Seattle",
                    "That Thing You Do",
                    "You've Got Mail"
                };

            // Solution 1
            using (Transaction.Begin())
            {
                ICompiled query = Transaction
                    .CompiledQuery
                    .Match
                    (
                         Domain.Data.Query.Node.Person.Alias(out var personAlias)
                        .In
                        .ACTED_IN
                        .Out
                        .Movie.Alias(out var movieAlias)
                    )
                    .Where(personAlias.name == "Tom Hanks")
                    .Return(movieAlias)
                    .Compile();

                List<Movie> tomHanksMovies = Movie.LoadWhere(query);
                Debug.Assert(tomHanksMovies.Count == movies.Count);

                foreach (string m in movies)
                    Debug.Assert(tomHanksMovies.SingleOrDefault(y => y.title == m) != null);
            }

            // Solution 2
            using (Transaction.Begin())
            {
                ICompiled query = Transaction
                    .CompiledQuery
                    .Match(Domain.Data.Query.Node.Person.Alias(out var personAlias))
                    .Where(personAlias.name == "Tom Hanks")
                    .Return(personAlias)
                    .Compile();

                Person tom = Person.LoadWhere(query).SingleOrDefault();
                Debug.Assert(tom.name == "Tom Hanks");

                foreach (string m in movies)
                    Debug.Assert(tom.ActedMovies.SingleOrDefault(y => y.title == m) != null);
            }
        }

        private static void FindMoviesIn1990s()
        {
            //Find movies released in the 1990s...
            //MATCH (nineties:Movie) WHERE nineties.released >= 1990 AND nineties.released < 2000 RETURN nineties.title
            using (Transaction.Begin())
            {
                ICompiled query = Transaction
                    .CompiledQuery
                    .Match(Domain.Data.Query.Node.Movie.Alias(out var movieAlias))
                    .Where(movieAlias.released >= 1990 & movieAlias.released < 2000)
                    .Return(movieAlias)
                    .Compile();

                List<Movie> moviesIn90s = Movie.LoadWhere(query);

                foreach (Movie movie in moviesIn90s)
                    Debug.Assert(movie.released >= 1990 && movie.released < 2000);
            }
        }

        private static void Find10People()
        {
            // Find 10 people...
            //MATCH (people: Person) RETURN people.name LIMIT 10
            using (Transaction.Begin())
            {
                ICompiled query = Transaction
                    .CompiledQuery
                    .Match(Domain.Data.Query.Node.Person.Alias(out var personAlias))
                    .Return(personAlias)
                    .Limit(10)
                    .Compile();

                List<Person> tenPersons = Person.LoadWhere(query);
                Debug.Assert(tenPersons.Count == 10);
            }

        }

        private static void FindMovieCloudAtlas()
        {
            //Find the movie with title "Cloud Atlas"...
            //MATCH (cloudAtlas { title: "Cloud Atlas"}) RETURN cloudAtlas

            using (Transaction.Begin())
            {
                ICompiled query = Transaction
                    .CompiledQuery
                    .Match(Domain.Data.Query.Node.Movie.Alias(out var movieAlias))
                    .Where(movieAlias.title == "Cloud Atlas")
                    .Return(movieAlias)
                    .Compile();

                Movie cloudAtlas = Movie.LoadWhere(query).SingleOrDefault();
                Debug.Assert(cloudAtlas.title == "Cloud Atlas");
            }
        }

        private static void FindActorTomHanks()
        {
            //Find the actor named "Tom Hanks"...
            //MATCH (tom { name: "Tom Hanks"}) RETURN tom

            using (Transaction.Begin())
            {
                ICompiled query = Transaction
                    .CompiledQuery
                    .Match(Domain.Data.Query.Node.Person.Alias(out var personAlias))
                    .Where(personAlias.name == "Tom Hanks")
                    .Return(personAlias)
                    .Compile();

                Person tom = Person.LoadWhere(query).SingleOrDefault();
                Debug.Assert(tom.name == "Tom Hanks");
            }
        }

        static void CreateMovieGraph()
        {
            Datastore datastore = new Datastore();
            datastore.Execute(true);

            using (Transaction.Begin(true))
            {
                #region The Matrix
                Movie theMatrix = new Movie();
                theMatrix.title = "The Matrix";
                theMatrix.released = 1999;
                theMatrix.tagline = "Welcome to the Real Word";

                Person keanu = new Person();
                keanu.name = "Keanu Reeves";
                keanu.born = 1964;

                Person carrie = new Person();
                carrie.name = "Carrie-Ann Moss";
                carrie.born = 1967;

                Person laurence = new Person();
                laurence.name = "Laurence Fishburne";
                laurence.born = 1961;

                Person hugo = new Person();
                hugo.name = "Hugo Weaving";
                hugo.born = 1960;

                Person lillyW = new Person();
                lillyW.name = "Lilly Wachowski";
                lillyW.born = 1967;

                Person lanaw = new Person();
                lanaw.name = "Lana Wachowski";
                lanaw.born = 1965;

                Person joels = new Person();
                joels.name = "Joel Silver";
                joels.born = 1952;

                theMatrix.Actors.Add(keanu);
                theMatrix.Actors.Add(carrie);
                theMatrix.Actors.Add(laurence);
                theMatrix.Actors.Add(hugo);

                theMatrix.Directors.Add(lillyW);
                theMatrix.Directors.Add(lanaw);
                theMatrix.Producers.Add(joels);

                Person emil = new Person();
                emil.name = "Emil Eifrem";
                emil.born = 1978;

                theMatrix.Actors.Add(emil);

                keanu.MovieRoles.Add(new MovieRole() { Movie = theMatrix, Role = new List<string> { "Neo" } });
                carrie.MovieRoles.Add(new MovieRole() { Movie = theMatrix, Role = new List<string> { "Trinity" } });
                laurence.MovieRoles.Add(new MovieRole() { Movie = theMatrix, Role = new List<string> { "Morpheus" } });
                hugo.MovieRoles.Add(new MovieRole() { Movie = theMatrix, Role = new List<string> { "Agent Smith" } });
                emil.MovieRoles.Add(new MovieRole() { Movie = theMatrix, Role = new List<string> { "Emil" } });

                #endregion

                #region The Matrix Reloaded
                Movie theMatrixReloaded = new Movie();
                theMatrixReloaded.title = "The Matrix Reloaded";
                theMatrixReloaded.released = 2003;
                theMatrixReloaded.tagline = "Free your mind";

                theMatrixReloaded.Actors.Add(keanu);
                theMatrixReloaded.Actors.Add(carrie);
                theMatrixReloaded.Actors.Add(laurence);
                theMatrixReloaded.Actors.Add(hugo);

                theMatrixReloaded.Directors.Add(lillyW);
                theMatrixReloaded.Directors.Add(lanaw);
                theMatrixReloaded.Producers.Add(joels);

                keanu.MovieRoles.Add(new MovieRole() { Movie = theMatrixReloaded, Role = new List<string> { "Neo" } });
                carrie.MovieRoles.Add(new MovieRole() { Movie = theMatrixReloaded, Role = new List<string> { "Trinity" } });
                laurence.MovieRoles.Add(new MovieRole() { Movie = theMatrixReloaded, Role = new List<string> { "Morpheus" } });
                hugo.MovieRoles.Add(new MovieRole() { Movie = theMatrixReloaded, Role = new List<string> { "Agent Smith" } });

                #endregion

                #region The Matrix Revolutions
                Movie theMatrixRevo = new Movie();
                theMatrixRevo.title = "The Matrix Revolutions";
                theMatrixRevo.released = 2003;
                theMatrixRevo.tagline = "Everything that has a beginning has an end.";

                theMatrixRevo.Actors.Add(keanu);
                theMatrixRevo.Actors.Add(carrie);
                theMatrixRevo.Actors.Add(laurence);
                theMatrixRevo.Actors.Add(hugo);
                theMatrixRevo.Directors.Add(lillyW);
                theMatrixRevo.Directors.Add(lanaw);
                theMatrixRevo.Producers.Add(joels);

                keanu.MovieRoles.Add(new MovieRole() { Movie = theMatrixRevo, Role = new List<string> { "Neo" } });
                carrie.MovieRoles.Add(new MovieRole() { Movie = theMatrixRevo, Role = new List<string> { "Trinity" } });
                laurence.MovieRoles.Add(new MovieRole() { Movie = theMatrixRevo, Role = new List<string> { "Morpheus" } });
                hugo.MovieRoles.Add(new MovieRole() { Movie = theMatrixRevo, Role = new List<string> { "Agent Smith" } });
                #endregion

                #region Devil's Advocate

                Movie devilsAdvocate = new Movie();
                devilsAdvocate.title = "The Devil's Advocate";
                devilsAdvocate.released = 1997;
                devilsAdvocate.tagline = "Evil has its winning ways";

                Person charlize = new Person();
                charlize.name = "Charlize Theron";
                charlize.born = 1975;

                Person al = new Person();
                al.name = "Al Pacino";
                al.born = 1940;

                Person taylor = new Person();
                taylor.name = "Taylor Hackfor";
                taylor.born = 1944;

                devilsAdvocate.Actors.Add(keanu);
                devilsAdvocate.Actors.Add(charlize);
                devilsAdvocate.Actors.Add(al);
                devilsAdvocate.Directors.Add(taylor);

                keanu.MovieRoles.Add(new MovieRole() { Movie = devilsAdvocate, Role = new List<string> { "Kevin Lomax" } });
                charlize.MovieRoles.Add(new MovieRole() { Movie = devilsAdvocate, Role = new List<string> { "Mary Ann Lomax" } });
                al.MovieRoles.Add(new MovieRole() { Movie = devilsAdvocate, Role = new List<string> { "John Milton" } });

                #endregion

                #region A Few Good Men

                Movie fewGoodMen = new Movie();
                fewGoodMen.title = "A Few Good Men";
                fewGoodMen.released = 1992;
                fewGoodMen.tagline = "In the heart of the nation's capital, in a courthouse of the U.S. government, one man will stop at nothing to keep his honor, and one will stop at nothing to find the truth.";

                Person tomC = new Person();
                tomC.name = "Tom Cruise";
                tomC.born = 1962;

                Person jackN = new Person();
                jackN.name = "Jack Nicholson";
                jackN.born = 1937;

                Person demiM = new Person();
                demiM.name = "Demi Moore";
                demiM.born = 1962;

                Person kevinB = new Person();
                kevinB.name = "Kevin Bacon";
                kevinB.born = 1958;

                Person kieferS = new Person();
                kieferS.name = "Kiefer Sutherland";
                kieferS.born = 1966;

                Person noahW = new Person();
                noahW.name = "Noah Wyle";
                noahW.born = 1971;

                Person cubaG = new Person();
                cubaG.name = "Cuba Gooding Jr.";
                cubaG.born = 1968;

                Person kevinP = new Person();
                kevinP.name = "Kevin Pollak";
                kevinP.born = 1957;

                Person jtw = new Person();
                jtw.name = "J.T. Walsh";
                jtw.born = 1946;

                Person jamesM = new Person();
                jamesM.name = "James Marshall";
                jamesM.born = 1967;

                Person chrisG = new Person();
                chrisG.name = "Christopher Guest";
                chrisG.born = 1948;

                Person robR = new Person();
                robR.name = "Rob Reiner";
                robR.born = 1947;

                Person aaronS = new Person();
                aaronS.name = "Aaron Sorkin";
                aaronS.born = 1961;

                fewGoodMen.Actors.Add(tomC);
                fewGoodMen.Actors.Add(jackN);
                fewGoodMen.Actors.Add(demiM);
                fewGoodMen.Actors.Add(kevinB);
                fewGoodMen.Actors.Add(kieferS);
                fewGoodMen.Actors.Add(noahW);
                fewGoodMen.Actors.Add(cubaG);
                fewGoodMen.Actors.Add(kevinP);
                fewGoodMen.Actors.Add(jtw);
                fewGoodMen.Actors.Add(jamesM);
                fewGoodMen.Actors.Add(chrisG);
                fewGoodMen.Actors.Add(aaronS);
                fewGoodMen.Directors.Add(robR);
                fewGoodMen.Writers.Add(aaronS);

                tomC.MovieRoles.Add(new MovieRole() { Movie = fewGoodMen, Role = new List<string> { "Lt. Daniel Kaffee" } });
                jackN.MovieRoles.Add(new MovieRole() { Movie = fewGoodMen, Role = new List<string> { "Col. Nathan R. Jessup" } });
                demiM.MovieRoles.Add(new MovieRole() { Movie = fewGoodMen, Role = new List<string> { "Lt. Cdr. JoAnne Galloway" } });
                kevinB.MovieRoles.Add(new MovieRole() { Movie = fewGoodMen, Role = new List<string> { "Capt. Jack Ross" } });
                kieferS.MovieRoles.Add(new MovieRole() { Movie = fewGoodMen, Role = new List<string> { "Lt. Jonathan Kendrick" } });
                noahW.MovieRoles.Add(new MovieRole() { Movie = fewGoodMen, Role = new List<string> { "Cpl. Jeffrey Barnes" } });
                cubaG.MovieRoles.Add(new MovieRole() { Movie = fewGoodMen, Role = new List<string> { "Cpl. Carl Hammaker" } });
                kevinP.MovieRoles.Add(new MovieRole() { Movie = fewGoodMen, Role = new List<string> { "Lt. Sam Weinberg" } });
                jtw.MovieRoles.Add(new MovieRole() { Movie = fewGoodMen, Role = new List<string> { "Lt. Col. Matthew Andrew Markinson" } });
                jamesM.MovieRoles.Add(new MovieRole() { Movie = fewGoodMen, Role = new List<string> { "Pfc. Louden Downey" } });
                chrisG.MovieRoles.Add(new MovieRole() { Movie = fewGoodMen, Role = new List<string> { "Dr. Stone" } });
                aaronS.MovieRoles.Add(new MovieRole() { Movie = fewGoodMen, Role = new List<string> { "Man in Bar" } });

                #endregion

                #region Top Gun

                Movie topGun = new Movie();
                topGun.title = "Top Gun";
                topGun.released = 1986;
                topGun.tagline = "I feel the need, the need for speed";

                Person kellyM = new Person();
                kellyM.name = "Kelly McGillis";
                kellyM.born = 1957;

                Person valK = new Person();
                valK.name = "Val Kilmer";
                valK.born = 1959;

                Person anthonyE = new Person();
                anthonyE.name = "Anthony Edwards";
                anthonyE.born = 1962;

                Person tomS = new Person();
                tomS.name = "Tom Skerritt";
                tomS.born = 1933;

                Person megR = new Person();
                megR.name = "Meg Ryan";
                megR.born = 1961;

                Person tonyS = new Person();
                tonyS.name = "Tony Scott";
                tonyS.born = 1944;

                Person jimC = new Person();
                jimC.name = "Jim Cash";
                jimC.born = 1941;

                topGun.Actors.Add(tomC);
                topGun.Actors.Add(kellyM);
                topGun.Actors.Add(valK);
                topGun.Actors.Add(anthonyE);
                topGun.Actors.Add(tomS);
                topGun.Actors.Add(megR);
                topGun.Directors.Add(tonyS);
                topGun.Writers.Add(jimC);

                tomC.MovieRoles.Add(new MovieRole() { Movie = topGun, Role = new List<string> { "Maverick" } });
                kellyM.MovieRoles.Add(new MovieRole() { Movie = topGun, Role = new List<string> { "Charlie" } });
                valK.MovieRoles.Add(new MovieRole() { Movie = topGun, Role = new List<string> { "Iceman" } });
                anthonyE.MovieRoles.Add(new MovieRole() { Movie = topGun, Role = new List<string> { "Goose" } });
                tomS.MovieRoles.Add(new MovieRole() { Movie = topGun, Role = new List<string> { "Viper" } });
                megR.MovieRoles.Add(new MovieRole() { Movie = topGun, Role = new List<string> { "Carole" } });

                #endregion

                #region Jerry Maguire

                Movie jerrymaguire = new Movie();
                jerrymaguire.title = "Jerry Maguire";
                jerrymaguire.released = 200;
                jerrymaguire.tagline = "The rest of his life begins now.";

                Person reneeZ = new Person();
                reneeZ.name = "Renee Zellweger";
                reneeZ.born = 1969;

                Person kellyP = new Person();
                kellyP.name = "Kelly Preston";
                kellyP.born = 1962;

                Person jerryO = new Person();
                jerryO.name = "Jerry O'Connell";
                jerryO.born = 1974;

                Person jayM = new Person();
                jayM.name = "Jay Mohr";
                jayM.born = 1970;

                Person bonnieH = new Person();
                bonnieH.name = "Bonnie Hunt";
                bonnieH.born = 1961;

                Person reginaK = new Person();
                reginaK.name = "Regina King";
                reginaK.born = 1971;

                Person jonathanL = new Person();
                jonathanL.name = "Jonathan Lipnicki";
                jonathanL.born = 1996;

                Person cameronC = new Person();
                cameronC.name = "Cameron Crowe";
                cameronC.born = 1957;

                jerrymaguire.Actors.Add(tomC);
                jerrymaguire.Actors.Add(cubaG);
                jerrymaguire.Actors.Add(reneeZ);
                jerrymaguire.Actors.Add(kellyP);
                jerrymaguire.Actors.Add(jerryO);
                jerrymaguire.Actors.Add(jayM);
                jerrymaguire.Actors.Add(bonnieH);
                jerrymaguire.Actors.Add(reginaK);
                jerrymaguire.Actors.Add(jonathanL);
                jerrymaguire.Directors.Add(cameronC);
                jerrymaguire.Producers.Add(cameronC);
                jerrymaguire.Writers.Add(cameronC);


                tomC.MovieRoles.Add(new MovieRole() { Movie = jerrymaguire, Role = new List<string> { "Jerry Maguire" } });
                cubaG.MovieRoles.Add(new MovieRole() { Movie = jerrymaguire, Role = new List<string> { "Rod Tidwell" } });
                reneeZ.MovieRoles.Add(new MovieRole() { Movie = jerrymaguire, Role = new List<string> { "Dorothy Boyd" } });
                kellyP.MovieRoles.Add(new MovieRole() { Movie = jerrymaguire, Role = new List<string> { "Avery Bishop" } });
                jerryO.MovieRoles.Add(new MovieRole() { Movie = jerrymaguire, Role = new List<string> { "Frank Cushman" } });
                jayM.MovieRoles.Add(new MovieRole() { Movie = jerrymaguire, Role = new List<string> { "Bob Sugar" } });
                bonnieH.MovieRoles.Add(new MovieRole() { Movie = jerrymaguire, Role = new List<string> { "Laurel Boyd" } });
                reginaK.MovieRoles.Add(new MovieRole() { Movie = jerrymaguire, Role = new List<string> { "Marcee Tidwell" } });
                jonathanL.MovieRoles.Add(new MovieRole() { Movie = jerrymaguire, Role = new List<string> { "Ray Boyd" } });

                #endregion

                #region Stand by me
                Movie standByMe = new Movie();
                standByMe.title = "Stand By Me";
                standByMe.released = 1986;
                standByMe.tagline = "For some, it's the last real taste of innocence, and the first real taste of life. But for everyone, it's the time that memories are made of.";

                Person riverP = new Person();
                riverP.name = "River Phoenix";
                riverP.born = 1970;

                Person coreyF = new Person();
                coreyF.name = "Corey Feldman";
                coreyF.born = 1971;

                Person wilW = new Person();
                wilW.name = "Wil Wheaton";
                wilW.born = 1972;

                Person johnC = new Person();
                johnC.name = "John Cusack";
                johnC.born = 1966;

                Person marshallB = new Person();
                marshallB.name = "Marshall Bell";
                marshallB.born = 1942;

                standByMe.Actors.Add(wilW);
                standByMe.Actors.Add(riverP);
                standByMe.Actors.Add(jerryO);
                standByMe.Actors.Add(coreyF);
                standByMe.Actors.Add(johnC);
                standByMe.Actors.Add(kieferS);
                standByMe.Actors.Add(marshallB);
                standByMe.Actors.Add(robR);

                wilW.MovieRoles.Add(new MovieRole() { Movie = standByMe, Role = new List<string> { "Gordie Lachance" } });
                riverP.MovieRoles.Add(new MovieRole() { Movie = standByMe, Role = new List<string> { "Chris Chambers" } });
                jerryO.MovieRoles.Add(new MovieRole() { Movie = standByMe, Role = new List<string> { "Vern Tessio" } });
                coreyF.MovieRoles.Add(new MovieRole() { Movie = standByMe, Role = new List<string> { "Teddy Duchamp" } });
                johnC.MovieRoles.Add(new MovieRole() { Movie = standByMe, Role = new List<string> { "Denny Lachance" } });
                kieferS.MovieRoles.Add(new MovieRole() { Movie = standByMe, Role = new List<string> { "Ace Merrill" } });
                marshallB.MovieRoles.Add(new MovieRole() { Movie = standByMe, Role = new List<string> { "Mr. Lachance" } });

                #endregion

                #region As Good as it Gets
                Movie asGoodAsItGets = new Movie();
                asGoodAsItGets.title = "As Good as it Gets";
                asGoodAsItGets.released = 1997;
                asGoodAsItGets.tagline = "A comedy from the heart that goes for the throat.";

                Person helenH = new Person();
                helenH.name = "Helen Hunt";
                helenH.born = 1963;

                Person gregK = new Person();
                gregK.name = "Greg Kinnear";
                gregK.born = 1963;

                Person jamesB = new Person();
                jamesB.name = "James L. Brooks";
                jamesB.born = 1940;

                asGoodAsItGets.Actors.Add(jackN);
                asGoodAsItGets.Actors.Add(helenH);
                asGoodAsItGets.Actors.Add(gregK);
                asGoodAsItGets.Actors.Add(cubaG);
                asGoodAsItGets.Directors.Add(jamesB);


                jackN.MovieRoles.Add(new MovieRole() { Movie = asGoodAsItGets, Role = new List<string> { "Melvin Udall" } });
                helenH.MovieRoles.Add(new MovieRole() { Movie = asGoodAsItGets, Role = new List<string> { "Carol Connelly" } });
                gregK.MovieRoles.Add(new MovieRole() { Movie = asGoodAsItGets, Role = new List<string> { "Simon Bishop" } });
                cubaG.MovieRoles.Add(new MovieRole() { Movie = asGoodAsItGets, Role = new List<string> { "Frank Sachs" } });

                #endregion

                #region What Dreams May Come
                Movie whatDreamsMayCome = new Movie();
                whatDreamsMayCome.title = "What Dreams May Come";
                whatDreamsMayCome.released = 1998;
                whatDreamsMayCome.tagline = "After life there is more. The end is just the beginning.";

                Person annabellaS = new Person();
                annabellaS.name = "Annabella Sciorra";
                annabellaS.born = 1960;

                Person maxS = new Person();
                maxS.name = "Max von Sydow";
                maxS.born = 1929;

                Person wernerH = new Person();
                wernerH.name = "Werner Herzog";
                wernerH.born = 1942;

                Person robin = new Person();
                robin.name = "Robin Williams";
                robin.born = 1951;

                Person vincentW = new Person();
                vincentW.name = "Vincent Ward";
                vincentW.born = 1956;

                whatDreamsMayCome.Actors.Add(robin);
                whatDreamsMayCome.Actors.Add(cubaG);
                whatDreamsMayCome.Actors.Add(annabellaS);
                whatDreamsMayCome.Actors.Add(maxS);
                whatDreamsMayCome.Actors.Add(wernerH);
                whatDreamsMayCome.Directors.Add(vincentW);


                robin.MovieRoles.Add(new MovieRole() { Movie = whatDreamsMayCome, Role = new List<string> { "Chris Nielsen" } });
                cubaG.MovieRoles.Add(new MovieRole() { Movie = whatDreamsMayCome, Role = new List<string> { "Albert Lewis" } });
                annabellaS.MovieRoles.Add(new MovieRole() { Movie = whatDreamsMayCome, Role = new List<string> { "Annie Collins-Nielsen" } });
                maxS.MovieRoles.Add(new MovieRole() { Movie = whatDreamsMayCome, Role = new List<string> { "The Tracker" } });
                wernerH.MovieRoles.Add(new MovieRole() { Movie = whatDreamsMayCome, Role = new List<string> { "The Face" } });

                #endregion

                #region Snow Falling on Cedars
                Movie snowFallingOnCedars = new Movie();
                snowFallingOnCedars.title = "Snow Falling on Cedars";
                snowFallingOnCedars.released = 1999;
                snowFallingOnCedars.tagline = "First loves last. Forever.";

                Person ethanH = new Person();
                ethanH.name = "Ethan Hawke";
                ethanH.born = 1970;

                Person rickY = new Person();
                rickY.name = "Rick Yune";
                rickY.born = 1971;

                Person jamesC = new Person();
                jamesC.name = "James Cromwell";
                jamesC.born = 1940;

                Person scottH = new Person();
                scottH.name = "Scott Hicks";
                scottH.born = 1953;

                snowFallingOnCedars.Actors.Add(ethanH);
                snowFallingOnCedars.Actors.Add(rickY);
                snowFallingOnCedars.Actors.Add(maxS);
                snowFallingOnCedars.Actors.Add(jamesC);
                snowFallingOnCedars.Actors.Add(scottH);


                ethanH.MovieRoles.Add(new MovieRole() { Movie = snowFallingOnCedars, Role = new List<string> { "Ishmael Chambers" } });
                rickY.MovieRoles.Add(new MovieRole() { Movie = snowFallingOnCedars, Role = new List<string> { "Kazuo Miyamoto" } });
                maxS.MovieRoles.Add(new MovieRole() { Movie = snowFallingOnCedars, Role = new List<string> { "Nels Gudmundsson" } });
                jamesC.MovieRoles.Add(new MovieRole() { Movie = snowFallingOnCedars, Role = new List<string> { "Judge Fielding" } });

                #endregion

                #region You've Got Mail
                Movie youGotMail = new Movie();
                youGotMail.title = "You've Got Mail";
                youGotMail.released = 1998;
                youGotMail.tagline = "At odds in life... in love on-line.";

                Person parkerP = new Person();
                parkerP.name = "Parker Posey";
                parkerP.born = 1968;

                Person daveC = new Person();
                daveC.name = "Dave Chappelle";
                daveC.born = 1973;

                Person steveZ = new Person();
                steveZ.name = "Steve Zahn";
                steveZ.born = 1967;

                Person tomH = new Person();
                tomH.name = "Tom Hanks";
                tomH.born = 1956;

                Person noraE = new Person();
                noraE.name = "Nora Ephron";
                noraE.born = 1941;

                youGotMail.Actors.Add(tomH);
                youGotMail.Actors.Add(megR);
                youGotMail.Actors.Add(gregK);
                youGotMail.Actors.Add(parkerP);
                youGotMail.Actors.Add(daveC);
                youGotMail.Actors.Add(steveZ);
                youGotMail.Directors.Add(noraE);

                tomH.MovieRoles.Add(new MovieRole() { Movie = youGotMail, Role = new List<string> { "Joe Fox" } });
                megR.MovieRoles.Add(new MovieRole() { Movie = youGotMail, Role = new List<string> { "Kathleen Kelly" } });
                gregK.MovieRoles.Add(new MovieRole() { Movie = youGotMail, Role = new List<string> { "Frank Navasky" } });
                parkerP.MovieRoles.Add(new MovieRole() { Movie = youGotMail, Role = new List<string> { "Patricia Eden" } });
                daveC.MovieRoles.Add(new MovieRole() { Movie = youGotMail, Role = new List<string> { "Kevin Jackson'" } });
                steveZ.MovieRoles.Add(new MovieRole() { Movie = youGotMail, Role = new List<string> { "George Pappas" } });

                #endregion

                #region Sleepless In Seattle
                Movie sleeplessInSeattle = new Movie();
                sleeplessInSeattle.title = "Sleepless In Seattle";
                sleeplessInSeattle.released = 1993;
                sleeplessInSeattle.tagline = "What if someone you never met, someone you never saw, someone you never knew was the only someone for you?";

                Person ritaW = new Person();
                ritaW.name = "Rita Wilson";
                ritaW.born = 1956;

                Person billPull = new Person();
                billPull.name = "Bill Pullman";
                billPull.born = 1953;

                Person victorG = new Person();
                victorG.name = "Victor Garber";
                victorG.born = 1949;

                Person rosieO = new Person();
                rosieO.name = "Rosie O'Donnell";
                rosieO.born = 1962;

                sleeplessInSeattle.Actors.Add(tomH);
                sleeplessInSeattle.Actors.Add(megR);
                sleeplessInSeattle.Actors.Add(ritaW);
                sleeplessInSeattle.Actors.Add(billPull);
                sleeplessInSeattle.Actors.Add(victorG);
                sleeplessInSeattle.Actors.Add(rosieO);
                sleeplessInSeattle.Directors.Add(noraE);

                tomH.MovieRoles.Add(new MovieRole() { Movie = sleeplessInSeattle, Role = new List<string> { "Sam Baldwin" } });
                megR.MovieRoles.Add(new MovieRole() { Movie = sleeplessInSeattle, Role = new List<string> { "Annie Reed" } });
                ritaW.MovieRoles.Add(new MovieRole() { Movie = sleeplessInSeattle, Role = new List<string> { "Suzy" } });
                billPull.MovieRoles.Add(new MovieRole() { Movie = sleeplessInSeattle, Role = new List<string> { "Walter" } });
                victorG.MovieRoles.Add(new MovieRole() { Movie = sleeplessInSeattle, Role = new List<string> { "Greg" } });
                rosieO.MovieRoles.Add(new MovieRole() { Movie = sleeplessInSeattle, Role = new List<string> { "Becky" } });

                #endregion

                #region Joe Versus the Volcano
                Movie joeVersustheVolcano = new Movie();
                joeVersustheVolcano.title = "Joe Versus the Volcano";
                joeVersustheVolcano.released = 1990;
                joeVersustheVolcano.tagline = "A story of love, lava and burning desire.";

                Person johnS = new Person();
                johnS.name = "John Patrick Stanley";
                johnS.born = 1950;

                Person nathan = new Person();
                nathan.name = "Nathan Lane";
                nathan.born = 1956;

                joeVersustheVolcano.Actors.Add(tomH);
                joeVersustheVolcano.Actors.Add(megR);
                joeVersustheVolcano.Actors.Add(nathan);
                joeVersustheVolcano.Directors.Add(johnS);

                tomH.MovieRoles.Add(new MovieRole() { Movie = joeVersustheVolcano, Role = new List<string> { "Joe Banks" } });
                megR.MovieRoles.Add(new MovieRole() { Movie = joeVersustheVolcano, Role = new List<string> { "DeDe" } });
                nathan.MovieRoles.Add(new MovieRole() { Movie = joeVersustheVolcano, Role = new List<string> { "Baw" } });

                #endregion

                #region When Harry Met Sally
                Movie whenHarryMetSally = new Movie();
                whenHarryMetSally.title = "When Harry Met Sally";
                whenHarryMetSally.released = 1998;
                whenHarryMetSally.tagline = "Can two friends sleep together and still love each other in the morning? ";

                Person billyC = new Person();
                billyC.name = "Billy Crystal";
                billyC.born = 1956;

                Person carrieF = new Person();
                carrieF.name = "Carrie Fisher";
                carrieF.born = 1956;

                Person brunoK = new Person();
                brunoK.name = "Bruno Kirby";
                brunoK.born = 1949;

                whenHarryMetSally.Actors.Add(billyC);
                whenHarryMetSally.Actors.Add(megR);
                whenHarryMetSally.Actors.Add(carrieF);
                whenHarryMetSally.Actors.Add(brunoK);
                whenHarryMetSally.Directors.Add(robR);
                whenHarryMetSally.Producers.Add(robR);
                whenHarryMetSally.Producers.Add(noraE);
                whenHarryMetSally.Writers.Add(noraE);

                billyC.MovieRoles.Add(new MovieRole() { Movie = whenHarryMetSally, Role = new List<string> { "Harry Burns" } });
                megR.MovieRoles.Add(new MovieRole() { Movie = whenHarryMetSally, Role = new List<string> { "Sally Albright" } });
                carrieF.MovieRoles.Add(new MovieRole() { Movie = whenHarryMetSally, Role = new List<string> { "Marie" } });
                brunoK.MovieRoles.Add(new MovieRole() { Movie = whenHarryMetSally, Role = new List<string> { "Jess" } });

                #endregion

                #region That Thing You Do
                Movie thatThingYouDo = new Movie();
                thatThingYouDo.title = "That Thing You Do";
                thatThingYouDo.released = 1996;
                thatThingYouDo.tagline = "In every life there comes a time when that thing you dream becomes that thing you do";

                Person livT = new Person();
                livT.name = "Liv Tyler";
                livT.born = 1977;

                thatThingYouDo.Actors.Add(tomH);
                thatThingYouDo.Actors.Add(livT);
                thatThingYouDo.Actors.Add(charlize);
                thatThingYouDo.Directors.Add(tomH);

                tomH.MovieRoles.Add(new MovieRole() { Movie = thatThingYouDo, Role = new List<string> { "Mr. White" } });
                livT.MovieRoles.Add(new MovieRole() { Movie = thatThingYouDo, Role = new List<string> { "Faye Dolan" } });
                charlize.MovieRoles.Add(new MovieRole() { Movie = thatThingYouDo, Role = new List<string> { "Tina" } });

                #endregion

                #region The Replacements
                Movie theReplacements = new Movie();
                theReplacements.title = "The Replacements";
                theReplacements.released = 2000;
                theReplacements.tagline = "Pain heals, Chicks dig scars... Glory lasts forever";

                Person brooke = new Person();
                brooke.name = "Brooke Langton";
                brooke.born = 1970;

                Person gene = new Person();
                gene.name = "Gene Hackman";
                gene.born = 1930;

                Person orlando = new Person();
                orlando.name = "Orlando Jones";
                orlando.born = 1968;

                Person howard = new Person();
                howard.name = "Howard Deutch";
                howard.born = 1950;

                theReplacements.Actors.Add(keanu);
                theReplacements.Actors.Add(brooke);
                theReplacements.Actors.Add(gene);
                theReplacements.Actors.Add(orlando);
                theReplacements.Directors.Add(howard);

                keanu.MovieRoles.Add(new MovieRole() { Movie = theReplacements, Role = new List<string> { "Shane Falco" } });
                brooke.MovieRoles.Add(new MovieRole() { Movie = theReplacements, Role = new List<string> { "Annabelle Farrell" } });
                gene.MovieRoles.Add(new MovieRole() { Movie = theReplacements, Role = new List<string> { "Jimmy McGinty" } });
                orlando.MovieRoles.Add(new MovieRole() { Movie = theReplacements, Role = new List<string> { "Clifford Franklin" } });

                #endregion

                #region RescueDawn
                Movie rescueDawn = new Movie();
                rescueDawn.title = "RescueDawn";
                rescueDawn.released = 2006;
                rescueDawn.tagline = "Based on the extraordinary true story of one man's fight for freedom";

                Person christianB = new Person();
                christianB.name = "Christian Bale";
                christianB.born = 1974;

                Person zachG = new Person();
                zachG.name = "Zach Grenier";
                zachG.born = 1954;

                rescueDawn.Actors.Add(marshallB);
                rescueDawn.Actors.Add(christianB);
                rescueDawn.Actors.Add(zachG);
                rescueDawn.Actors.Add(steveZ);
                rescueDawn.Directors.Add(wernerH);

                marshallB.MovieRoles.Add(new MovieRole() { Movie = rescueDawn, Role = new List<string> { "Admiral" } });
                christianB.MovieRoles.Add(new MovieRole() { Movie = rescueDawn, Role = new List<string> { "Dieter Dengler" } });
                zachG.MovieRoles.Add(new MovieRole() { Movie = rescueDawn, Role = new List<string> { "Squad Leader" } });
                steveZ.MovieRoles.Add(new MovieRole() { Movie = rescueDawn, Role = new List<string> { "Duane" } });

                #endregion

                #region The Birdcage
                Movie theBirdcage = new Movie();
                theBirdcage.title = "The Birdcage";
                theBirdcage.released = 1996;
                theBirdcage.tagline = "Come as you are";

                Person mikeN = new Person();
                mikeN.name = "Mike Nichols";
                mikeN.born = 1931;

                theBirdcage.Actors.Add(robin);
                theBirdcage.Actors.Add(nathan);
                theBirdcage.Actors.Add(gene);
                theBirdcage.Directors.Add(mikeN);

                robin.MovieRoles.Add(new MovieRole() { Movie = theBirdcage, Role = new List<string> { "Armand Goldman" } });
                nathan.MovieRoles.Add(new MovieRole() { Movie = theBirdcage, Role = new List<string> { "Albert Goldman" } });
                gene.MovieRoles.Add(new MovieRole() { Movie = theBirdcage, Role = new List<string> { "Sen. Kevin Keeley" } });

                #endregion

                #region Unforgiven
                Movie unforgiven = new Movie();
                unforgiven.title = "Unforgiven";
                unforgiven.released = 1992;
                unforgiven.tagline = "It's a hell of a thing, killing a man";

                Person richardH = new Person();
                richardH.name = "Richard Harris";
                richardH.born = 1930;

                Person clintE = new Person();
                clintE.name = "Clint Eastwood";
                clintE.born = 1930;

                unforgiven.Actors.Add(richardH);
                unforgiven.Actors.Add(clintE);
                unforgiven.Actors.Add(gene);
                unforgiven.Directors.Add(clintE);

                richardH.MovieRoles.Add(new MovieRole() { Movie = unforgiven, Role = new List<string> { "English Bob" } });
                clintE.MovieRoles.Add(new MovieRole() { Movie = unforgiven, Role = new List<string> { "Bill Munny" } });
                gene.MovieRoles.Add(new MovieRole() { Movie = unforgiven, Role = new List<string> { "Little Bill Daggett" } });

                #endregion

                #region Johnny Mnemonic
                Movie johnnyMnemonic = new Movie();
                johnnyMnemonic.title = "Johnny Mnemonic";
                johnnyMnemonic.released = 1995;
                johnnyMnemonic.tagline = "The hottest data on earth. In the coolest head in town";

                Person takeshi = new Person();
                takeshi.name = "Takeshi Kitano";
                takeshi.born = 1947;

                Person dina = new Person();
                dina.name = "Dina Meyer";
                dina.born = 1968;

                Person iceT = new Person();
                iceT.name = "Ice-T";
                iceT.born = 1958;

                Person robertL = new Person();
                robertL.name = "Robert Longo";
                robertL.born = 1953;

                johnnyMnemonic.Actors.Add(keanu);
                johnnyMnemonic.Actors.Add(takeshi);
                johnnyMnemonic.Actors.Add(dina);
                johnnyMnemonic.Actors.Add(iceT);
                johnnyMnemonic.Directors.Add(robertL);

                keanu.MovieRoles.Add(new MovieRole() { Movie = johnnyMnemonic, Role = new List<string> { "Johnny Mnemonic" } });
                takeshi.MovieRoles.Add(new MovieRole() { Movie = johnnyMnemonic, Role = new List<string> { "Takahashi" } });
                dina.MovieRoles.Add(new MovieRole() { Movie = johnnyMnemonic, Role = new List<string> { "Jane" } });
                iceT.MovieRoles.Add(new MovieRole() { Movie = johnnyMnemonic, Role = new List<string> { "J-Bone" } });

                #endregion

                #region Clout Atlas
                Movie cloudAtlas = new Movie();
                cloudAtlas.title = "Cloud Atlas";
                cloudAtlas.released = 2012;
                cloudAtlas.tagline = "Everything is connected";

                Person halleB = new Person();
                halleB.name = "Halle Berry";
                halleB.born = 1966;

                Person jimB = new Person();
                jimB.name = "Jim Broadbent";
                jimB.born = 1949;

                Person tomT = new Person();
                tomT.name = "Tom Tykwer";
                tomT.born = 1965;

                Person davidMitchell = new Person();
                davidMitchell.name = "David Mitchell";
                davidMitchell.born = 1969;

                Person stefanArndt = new Person();
                stefanArndt.name = "Stefan Arndt";
                stefanArndt.born = 1961;

                cloudAtlas.Actors.Add(tomH);
                cloudAtlas.Actors.Add(hugo);
                cloudAtlas.Actors.Add(halleB);
                cloudAtlas.Actors.Add(jimB);
                cloudAtlas.Directors.Add(tomT);
                cloudAtlas.Directors.Add(lillyW);
                cloudAtlas.Directors.Add(lanaw);
                cloudAtlas.Writers.Add(davidMitchell);
                cloudAtlas.Producers.Add(stefanArndt);

                tomH.MovieRoles.Add(new MovieRole() { Movie = cloudAtlas, Role = new List<string> { "Zachry", "Dr. Henry Goose", "Isaac Sachs", "Dermot Hoggins" } });
                hugo.MovieRoles.Add(new MovieRole() { Movie = cloudAtlas, Role = new List<string> { "Bill Smoke", "Haskell Moore", "Tadeusz Kesselring", "Nurse Noakes", "Boardman Mephi", "Old Georgie" } });
                halleB.MovieRoles.Add(new MovieRole() { Movie = cloudAtlas, Role = new List<string> { "Luisa Rey", "Jocasta Ayrs", "Ovid", "Meronym" } });
                jimB.MovieRoles.Add(new MovieRole() { Movie = cloudAtlas, Role = new List<string> { "Vyvyan Ayrs", "Captain Molyneux", "Timothy Cavendish" } });

                #endregion

                #region The Da Vinci Code
                Movie theDavinciCode = new Movie();
                theDavinciCode.title = "The Da Vinci Code";
                theDavinciCode.released = 2006;
                theDavinciCode.tagline = "Break The Codes";

                Person ianM = new Person();
                ianM.name = "Ian McKellen";
                ianM.born = 1939;

                Person audreyT = new Person();
                audreyT.name = "Audrey Tautou";
                audreyT.born = 1976;

                Person paulB = new Person();
                paulB.name = "Paul Bettany";
                paulB.born = 1971;

                Person ronH = new Person();
                ronH.name = "Ron Howard";
                ronH.born = 1954;

                theDavinciCode.Actors.Add(tomH);
                theDavinciCode.Actors.Add(ianM);
                theDavinciCode.Actors.Add(audreyT);
                theDavinciCode.Actors.Add(paulB);
                theDavinciCode.Directors.Add(ronH);

                tomH.MovieRoles.Add(new MovieRole() { Movie = theDavinciCode, Role = new List<string> { "Dr. Robert Langdon" } });
                ianM.MovieRoles.Add(new MovieRole() { Movie = theDavinciCode, Role = new List<string> { "Sir Leight Teabing" } });
                audreyT.MovieRoles.Add(new MovieRole() { Movie = theDavinciCode, Role = new List<string> { "Sophie Neveu" } });
                paulB.MovieRoles.Add(new MovieRole() { Movie = theDavinciCode, Role = new List<string> { "Silas" } });

                #endregion

                #region V for Vendetta
                Movie vForVendetta = new Movie();
                vForVendetta.title = "V for Vendetta";
                vForVendetta.released = 2006;
                vForVendetta.tagline = "Freedom! Forever!";

                Person natalieP = new Person();
                natalieP.name = "Natalie Portman";
                natalieP.born = 1981;

                Person stephenR = new Person();
                stephenR.name = "Stephen Rea";
                stephenR.born = 1946;

                Person johnH = new Person();
                johnH.name = "John Hurt";
                johnH.born = 1940;

                Person benM = new Person();
                benM.name = "Ben Miles";
                benM.born = 1967;

                vForVendetta.Actors.Add(hugo);
                vForVendetta.Actors.Add(natalieP);
                vForVendetta.Actors.Add(stephenR);
                vForVendetta.Actors.Add(johnH);
                vForVendetta.Actors.Add(benM);
                vForVendetta.Directors.Add(jamesM);
                vForVendetta.Producers.Add(lillyW);
                vForVendetta.Producers.Add(lanaw);
                vForVendetta.Producers.Add(joels);
                vForVendetta.Writers.Add(lillyW);
                vForVendetta.Writers.Add(lanaw);

                hugo.MovieRoles.Add(new MovieRole() { Movie = vForVendetta, Role = new List<string> { "V" } });
                natalieP.MovieRoles.Add(new MovieRole() { Movie = vForVendetta, Role = new List<string> { "Evey Hammond" } });
                stephenR.MovieRoles.Add(new MovieRole() { Movie = vForVendetta, Role = new List<string> { "Eric Finch" } });
                johnH.MovieRoles.Add(new MovieRole() { Movie = vForVendetta, Role = new List<string> { "High Chancellor Adam Sutler" } });
                benM.MovieRoles.Add(new MovieRole() { Movie = vForVendetta, Role = new List<string> { "Dascomb" } });

                #endregion

                #region Speed Racer
                Movie speedRacer = new Movie();
                speedRacer.title = "Speed Racer";
                speedRacer.released = 2008;
                speedRacer.tagline = "Speed has no limits";

                Person emileH = new Person();
                emileH.name = "Emile Hirsch";
                emileH.born = 1985;

                Person johnG = new Person();
                johnG.name = "John Goodman";
                johnG.born = 1960;

                Person susanS = new Person();
                susanS.name = "Susan Sarandon";
                susanS.born = 1946;

                Person matthewF = new Person();
                matthewF.name = "Matthew Fox";
                matthewF.born = 1966;

                Person christinaR = new Person();
                christinaR.name = "Christina Ricci";
                christinaR.born = 1980;

                Person rain = new Person();
                rain.name = "Rain";
                rain.born = 1982;

                speedRacer.Actors.Add(emileH);
                speedRacer.Actors.Add(johnG);
                speedRacer.Actors.Add(susanS);
                speedRacer.Actors.Add(matthewF);
                speedRacer.Actors.Add(christinaR);
                speedRacer.Actors.Add(rain);
                speedRacer.Actors.Add(benM);
                speedRacer.Directors.Add(lillyW);
                speedRacer.Directors.Add(lanaw);
                speedRacer.Writers.Add(lillyW);
                speedRacer.Writers.Add(lanaw);
                speedRacer.Producers.Add(joels);

                emileH.MovieRoles.Add(new MovieRole() { Movie = speedRacer, Role = new List<string> { "Speed Racer" } });
                johnG.MovieRoles.Add(new MovieRole() { Movie = speedRacer, Role = new List<string> { "Pops" } });
                susanS.MovieRoles.Add(new MovieRole() { Movie = speedRacer, Role = new List<string> { "Mom" } });
                matthewF.MovieRoles.Add(new MovieRole() { Movie = speedRacer, Role = new List<string> { "Racer X" } });
                christinaR.MovieRoles.Add(new MovieRole() { Movie = speedRacer, Role = new List<string> { "Trixie" } });
                rain.MovieRoles.Add(new MovieRole() { Movie = speedRacer, Role = new List<string> { "Taejo Togokahn" } });
                benM.MovieRoles.Add(new MovieRole() { Movie = speedRacer, Role = new List<string> { "Cass Jones" } });

                #endregion

                #region Ninja Assassin
                Movie ninjaAssassin = new Movie();
                ninjaAssassin.title = "Ninja Assassin";
                ninjaAssassin.released = 2009;
                ninjaAssassin.tagline = "Prepare to enter a secret world of assassins";

                Person naomieH = new Person();
                naomieH.name = "Naomie Harris";

                ninjaAssassin.Actors.Add(rain);
                ninjaAssassin.Actors.Add(naomieH);
                ninjaAssassin.Actors.Add(rickY);
                ninjaAssassin.Actors.Add(benM);
                ninjaAssassin.Directors.Add(jamesM);
                ninjaAssassin.Producers.Add(lillyW);
                ninjaAssassin.Producers.Add(lanaw);
                ninjaAssassin.Producers.Add(joels);

                rain.MovieRoles.Add(new MovieRole() { Movie = ninjaAssassin, Role = new List<string> { "Raizo" } });
                naomieH.MovieRoles.Add(new MovieRole() { Movie = ninjaAssassin, Role = new List<string> { "Mika Coretti" } });
                rickY.MovieRoles.Add(new MovieRole() { Movie = ninjaAssassin, Role = new List<string> { "Takeshi" } });
                benM.MovieRoles.Add(new MovieRole() { Movie = ninjaAssassin, Role = new List<string> { "Ryan Maslow" } });

                #endregion

                #region The Green Mile
                Movie theGreenMile = new Movie();
                theGreenMile.title = "The Green Mile";
                theGreenMile.released = 1999;
                theGreenMile.tagline = "Walk a mile you'll never forget.";

                Person michaelD = new Person();
                michaelD.name = "Michael Clarke Duncan";
                michaelD.born = 1957;

                Person davidM = new Person();
                davidM.name = "David Morse";
                davidM.born = 1953;

                Person samR = new Person();
                samR.name = "Sam Rockwell";
                samR.born = 1968;

                Person garyS = new Person();
                garyS.name = "Gary Sinise";
                garyS.born = 1955;

                Person patriciaC = new Person();
                patriciaC.name = "Patricia Clarkson";
                patriciaC.born = 1959;

                Person frankD = new Person();
                frankD.name = "Frank Darabont";
                frankD.born = 1959;

                theGreenMile.Actors.Add(tomH);
                theGreenMile.Actors.Add(michaelD);
                theGreenMile.Actors.Add(davidM);
                theGreenMile.Actors.Add(bonnieH);
                theGreenMile.Actors.Add(jamesC);
                theGreenMile.Actors.Add(samR);
                theGreenMile.Actors.Add(garyS);
                theGreenMile.Actors.Add(patriciaC);
                theGreenMile.Directors.Add(frankD);

                tomH.MovieRoles.Add(new MovieRole() { Movie = theGreenMile, Role = new List<string> { "Paul Edgecomb" } });
                michaelD.MovieRoles.Add(new MovieRole() { Movie = theGreenMile, Role = new List<string> { "John Coffey" } });
                davidM.MovieRoles.Add(new MovieRole() { Movie = theGreenMile, Role = new List<string> { "Brutus \"Brutal\" Howell" } });
                bonnieH.MovieRoles.Add(new MovieRole() { Movie = theGreenMile, Role = new List<string> { "Jan Edgecomb" } });
                jamesC.MovieRoles.Add(new MovieRole() { Movie = theGreenMile, Role = new List<string> { "Warden Hal Moores" } });
                samR.MovieRoles.Add(new MovieRole() { Movie = theGreenMile, Role = new List<string> { "\"Wild Bill\" Wharton" } });
                garyS.MovieRoles.Add(new MovieRole() { Movie = theGreenMile, Role = new List<string> { "Burt Hammersmith" } });
                patriciaC.MovieRoles.Add(new MovieRole() { Movie = theGreenMile, Role = new List<string> { "Melinda Moores" } });

                #endregion

                #region Frost/Nixon
                Movie frostNixon = new Movie();
                frostNixon.title = "Frost/Nixon";
                frostNixon.released = 2008;
                frostNixon.tagline = "400 million people were waiting for the truth.";

                Person frankL = new Person();
                frankL.name = "Frank Langella";
                frankL.born = 1938;

                Person michaelS = new Person();
                michaelS.name = "Michael Sheen";
                michaelS.born = 1969;

                Person oliverP = new Person();
                oliverP.name = "Oliver Platt";
                oliverP.born = 1960;

                frostNixon.Actors.Add(frankL);
                frostNixon.Actors.Add(michaelS);
                frostNixon.Actors.Add(kevinB);
                frostNixon.Actors.Add(oliverP);
                frostNixon.Actors.Add(samR);
                frostNixon.Directors.Add(ronH);

                frankL.MovieRoles.Add(new MovieRole() { Movie = frostNixon, Role = new List<string> { "Richard Nixon" } });
                michaelS.MovieRoles.Add(new MovieRole() { Movie = frostNixon, Role = new List<string> { "David Frost" } });
                kevinB.MovieRoles.Add(new MovieRole() { Movie = frostNixon, Role = new List<string> { "Jack Brennan" } });
                oliverP.MovieRoles.Add(new MovieRole() { Movie = frostNixon, Role = new List<string> { "Bob Zelnick" } });
                samR.MovieRoles.Add(new MovieRole() { Movie = frostNixon, Role = new List<string> { "James Reston, Jr." } });

                #endregion

                #region Hoffa
                Movie hoffa = new Movie();
                hoffa.title = "Hoffa";
                hoffa.released = 1992;
                hoffa.tagline = "He didn't want law. He wanted justice.";

                Person dannyD = new Person();
                dannyD.name = "Danny DeVito";
                dannyD.born = 1944;

                Person johnR = new Person();
                johnR.name = "John C. Reilly";
                johnR.born = 1965;

                hoffa.Actors.Add(jackN);
                hoffa.Actors.Add(dannyD);
                hoffa.Actors.Add(jtw);
                hoffa.Actors.Add(johnR);
                hoffa.Directors.Add(dannyD);

                jackN.MovieRoles.Add(new MovieRole() { Movie = hoffa, Role = new List<string> { "Hoffa" } });
                dannyD.MovieRoles.Add(new MovieRole() { Movie = hoffa, Role = new List<string> { "Robert \"Bobby\" Ciaro" } });
                jtw.MovieRoles.Add(new MovieRole() { Movie = hoffa, Role = new List<string> { "Frank Fitzsimmons" } });
                johnR.MovieRoles.Add(new MovieRole() { Movie = hoffa, Role = new List<string> { "Peter \"Pete\" Connelly" } });

                #endregion

                #region Apollo 13
                Movie apollo13 = new Movie();
                apollo13.title = "Apollo 13";
                apollo13.released = 1995;
                apollo13.tagline = "Houston, we have a problem.";

                Person edH = new Person();
                edH.name = "Ed Harris";
                edH.born = 1950;

                Person billPax = new Person();
                billPax.name = "Bill Paxton";
                billPax.born = 1955;

                apollo13.Actors.Add(tomH);
                apollo13.Actors.Add(kevinB);
                apollo13.Actors.Add(edH);
                apollo13.Actors.Add(billPax);
                apollo13.Actors.Add(garyS);
                apollo13.Directors.Add(ronH);

                tomH.MovieRoles.Add(new MovieRole() { Movie = apollo13, Role = new List<string> { "Jim Lovell" } });
                kevinB.MovieRoles.Add(new MovieRole() { Movie = apollo13, Role = new List<string> { "Jack Swigert" } });
                edH.MovieRoles.Add(new MovieRole() { Movie = apollo13, Role = new List<string> { "Gene Kranz" } });
                billPax.MovieRoles.Add(new MovieRole() { Movie = apollo13, Role = new List<string> { "Fred Haise" } });
                garyS.MovieRoles.Add(new MovieRole() { Movie = apollo13, Role = new List<string> { "Ken Mattingly" } });

                #endregion

                #region Twister
                Movie twister = new Movie();
                twister.title = "Twister";
                twister.released = 1996;
                twister.tagline = "Don't Breathe. Don't Look Back.";

                Person philipH = new Person();
                philipH.name = "Philip Seymour Hoffman";
                philipH.born = 1967;

                Person janB = new Person();
                janB.name = "Jan de Bont";
                janB.born = 1943;

                twister.Actors.Add(billPax);
                twister.Actors.Add(helenH);
                twister.Actors.Add(zachG);
                twister.Actors.Add(philipH);
                twister.Directors.Add(janB);

                billPax.MovieRoles.Add(new MovieRole() { Movie = twister, Role = new List<string> { "Bill Harding" } });
                helenH.MovieRoles.Add(new MovieRole() { Movie = twister, Role = new List<string> { "Dr. Jo Harding" } });
                zachG.MovieRoles.Add(new MovieRole() { Movie = twister, Role = new List<string> { "Eddie" } });
                philipH.MovieRoles.Add(new MovieRole() { Movie = twister, Role = new List<string> { "Dustin \"Dusty\" Davis" } });

                #endregion

                #region Cast Away
                Movie castAway = new Movie();
                castAway.title = "Cast Away";
                castAway.released = 2000;
                castAway.tagline = "At the edge of the world, his journey begins.";

                Person robertZ = new Person();
                robertZ.name = "Robert Zemeckis";
                robertZ.born = 1951;

                castAway.Actors.Add(tomH);
                castAway.Actors.Add(helenH);
                castAway.Directors.Add(robertZ);

                tomH.MovieRoles.Add(new MovieRole() { Movie = castAway, Role = new List<string> { "Chuck Noland" } });
                helenH.MovieRoles.Add(new MovieRole() { Movie = castAway, Role = new List<string> { "Kelly Frears" } });

                #endregion

                #region One Flew Over the Cuckoo's Nest
                Movie oneFlewOvertheCuckoosNest = new Movie();
                oneFlewOvertheCuckoosNest.title = "One Flew Over the Cuckoo's Nest";
                oneFlewOvertheCuckoosNest.released = 1975;
                oneFlewOvertheCuckoosNest.tagline = "If he's crazy, what does that make you?";

                Person milosF = new Person();
                milosF.name = "Milos Forman";
                milosF.born = 1932;

                oneFlewOvertheCuckoosNest.Actors.Add(jackN);
                oneFlewOvertheCuckoosNest.Actors.Add(dannyD);
                oneFlewOvertheCuckoosNest.Directors.Add(milosF);

                jackN.MovieRoles.Add(new MovieRole() { Movie = oneFlewOvertheCuckoosNest, Role = new List<string> { "Randle McMurphy" } });
                dannyD.MovieRoles.Add(new MovieRole() { Movie = oneFlewOvertheCuckoosNest, Role = new List<string> { "Martini" } });

                #endregion

                #region Something's Gotta Give
                Movie somethingsGottaGive = new Movie();
                somethingsGottaGive.title = "Something's Gotta Give";
                somethingsGottaGive.released = 2003;

                Person dianeK = new Person();
                dianeK.name = "Diane Keaton";
                dianeK.born = 1946;

                Person nancyM = new Person();
                nancyM.name = "Nancy Meyers";
                nancyM.born = 1949;

                somethingsGottaGive.Actors.Add(jackN);
                somethingsGottaGive.Actors.Add(dianeK);
                somethingsGottaGive.Actors.Add(keanu);
                somethingsGottaGive.Directors.Add(nancyM);
                somethingsGottaGive.Producers.Add(nancyM);
                somethingsGottaGive.Writers.Add(nancyM);

                jackN.MovieRoles.Add(new MovieRole() { Movie = somethingsGottaGive, Role = new List<string> { "Harry Sanborn" } });
                dianeK.MovieRoles.Add(new MovieRole() { Movie = somethingsGottaGive, Role = new List<string> { "Erica Barry" } });
                keanu.MovieRoles.Add(new MovieRole() { Movie = somethingsGottaGive, Role = new List<string> { "Julian Mercer" } });

                #endregion

                #region Bicentennial Man
                Movie bicentennialMan = new Movie();
                bicentennialMan.title = "Bicentennial Man";
                bicentennialMan.released = 1999;
                bicentennialMan.tagline = "One robot's 200 year journey to become an ordinary man.";

                Person chrisC = new Person();
                chrisC.name = "Chris Columbus";
                chrisC.born = 1958;

                bicentennialMan.Actors.Add(robin);
                bicentennialMan.Actors.Add(oliverP);
                bicentennialMan.Directors.Add(chrisC);

                robin.MovieRoles.Add(new MovieRole() { Movie = bicentennialMan, Role = new List<string> { "Andrew Marin" } });
                oliverP.MovieRoles.Add(new MovieRole() { Movie = bicentennialMan, Role = new List<string> { "Rupert Burns" } });

                #endregion

                #region Charlie Wilson's War
                Movie charlieWilsonsWar = new Movie();
                charlieWilsonsWar.title = "Charlie Wilson's War";
                charlieWilsonsWar.released = 2007;
                charlieWilsonsWar.tagline = "A stiff drink. A little mascara. A lot of nerve. Who said they couldn't bring down the Soviet empire.";

                Person juliaR = new Person();
                juliaR.name = "Julia Roberts";
                juliaR.born = 1967;

                charlieWilsonsWar.Actors.Add(tomH);
                charlieWilsonsWar.Actors.Add(juliaR);
                charlieWilsonsWar.Actors.Add(philipH);
                charlieWilsonsWar.Directors.Add(mikeN);

                tomH.MovieRoles.Add(new MovieRole() { Movie = charlieWilsonsWar, Role = new List<string> { "Rep. Charlie Wilson" } });
                juliaR.MovieRoles.Add(new MovieRole() { Movie = charlieWilsonsWar, Role = new List<string> { "Joanne Herring" } });
                philipH.MovieRoles.Add(new MovieRole() { Movie = charlieWilsonsWar, Role = new List<string> { "Gust Avrakotos" } });

                #endregion

                #region The Polar Express
                Movie thePolarExpress = new Movie();
                thePolarExpress.title = "The Polar Express";
                thePolarExpress.released = 2004;
                thePolarExpress.tagline = "This Holiday Season… Believe";

                thePolarExpress.Actors.Add(tomH);
                thePolarExpress.Directors.Add(robertZ);

                tomH.MovieRoles.Add(new MovieRole() { Movie = thePolarExpress, Role = new List<string> { "Hero Boy", "Father", "Conductor", "Hobo", "Scrooge", "Santa Claus" } });

                #endregion

                #region A League of Their Own
                Movie aLeagueofTheirOwn = new Movie();
                aLeagueofTheirOwn.title = "A League of Their Own";
                aLeagueofTheirOwn.released = 1992;
                aLeagueofTheirOwn.tagline = "Once in a lifetime you get a chance to do something different.";

                Person madonna = new Person();
                madonna.name = "Madonna";
                madonna.born = 1954;

                Person geenaD = new Person();
                geenaD.name = "Geena Davis";
                geenaD.born = 1956;

                Person loriP = new Person();
                loriP.name = "Lori Petty";
                loriP.born = 1963;

                Person pennyM = new Person();
                pennyM.name = "Penny Marshall";
                pennyM.born = 1943;

                aLeagueofTheirOwn.Actors.Add(tomH);
                aLeagueofTheirOwn.Actors.Add(geenaD);
                aLeagueofTheirOwn.Actors.Add(loriP);
                aLeagueofTheirOwn.Actors.Add(rosieO);
                aLeagueofTheirOwn.Actors.Add(madonna);
                aLeagueofTheirOwn.Actors.Add(billPax);
                aLeagueofTheirOwn.Directors.Add(pennyM);

                tomH.MovieRoles.Add(new MovieRole() { Movie = thePolarExpress, Role = new List<string> { "Jimmy Dugan" } });
                geenaD.MovieRoles.Add(new MovieRole() { Movie = thePolarExpress, Role = new List<string> { "Dottie Hinson" } });
                loriP.MovieRoles.Add(new MovieRole() { Movie = thePolarExpress, Role = new List<string> { "Kit Keller" } });
                rosieO.MovieRoles.Add(new MovieRole() { Movie = thePolarExpress, Role = new List<string> { "Doris Murphy" } });
                madonna.MovieRoles.Add(new MovieRole() { Movie = thePolarExpress, Role = new List<string> { "\"All the Way\" Mae Mordabito" } });
                billPax.MovieRoles.Add(new MovieRole() { Movie = thePolarExpress, Role = new List<string> { "Bob Hinson" } });

                #endregion

                #region Followers
                Person paulBlythe = new Person();
                paulBlythe.name = "Paul Blythe";

                Person angelaScope = new Person();
                angelaScope.name = "Angela Scope";

                Person jessicaThompson = new Person();
                jessicaThompson.name = "Jessica Thompson";

                Person jamesThompson = new Person();
                jamesThompson.name = "James Thompson";

                paulBlythe.FollowedPersons.Add(jessicaThompson);
                angelaScope.FollowedPersons.Add(jessicaThompson);
                paulBlythe.FollowedPersons.Add(angelaScope);
                #endregion

                #region Movie Reviews
                jessicaThompson.MovieReviews.Add(new MovieReview()
                {
                    Review = "An amazing journey",
                    Rating = 95,
                    Movie = cloudAtlas
                });

                jessicaThompson.MovieReviews.Add(new MovieReview()
                {
                    Review = "Silly, but fun",
                    Rating = 65,
                    Movie = theReplacements
                });

                jamesThompson.MovieReviews.Add(new MovieReview()
                {
                    Review = "The coolest football movie ever",
                    Rating = 100,
                    Movie = theReplacements
                });

                angelaScope.MovieReviews.Add(new MovieReview()
                {
                    Review = "Pretty funny at times",
                    Rating = 62,
                    Movie = theReplacements
                });

                jessicaThompson.MovieReviews.Add(new MovieReview()
                {
                    Review = "Dark, but compelling",
                    Rating = 85,
                    Movie = unforgiven
                });

                jessicaThompson.MovieReviews.Add(new MovieReview()
                {
                    Review = "Slapstick redeemed only by the Robin Williams and Gene Hackman's stellar performances",
                    Rating = 45,
                    Movie = theBirdcage
                });

                jessicaThompson.MovieReviews.Add(new MovieReview()
                {
                    Review = "A solid romp",
                    Rating = 68,
                    Movie = theDavinciCode
                });

                jamesThompson.MovieReviews.Add(new MovieReview()
                {
                    Review = "Fun, but a little far fetched",
                    Rating = 65,
                    Movie = theDavinciCode
                });

                jessicaThompson.MovieReviews.Add(new MovieReview()
                {
                    Review = "You had me at Jerry",
                    Rating = 92,
                    Movie = jerrymaguire
                });

                #endregion

                Transaction.Commit();
            }
        }
    }
}
