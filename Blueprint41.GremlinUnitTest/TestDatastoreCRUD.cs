using Blueprint41.Core;
using Blueprint41.GremlinUnitTest.Misc;
using Blueprint41.Query;
using Datastore.Manipulation;
using Datastore.Query;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.GremlinUnitTest
{
    [TestFixture]
    internal class TestDatastoreCRUD
    {
        public Type Datastoremodel => typeof(GremlinStore);

        [OneTimeSetUp]
        public void Init()
        {
            string hostname = "localhost";
            int port = 8182;
            string authKey = null;
            string database = null;
            string collection = null;

            PersistenceProvider.Initialize(Datastoremodel, hostname, port, authKey, database, collection);

            using (Transaction.Begin())
                Transaction.Run("Match (n) detach delete n");
        }

        [Test, Order(1)]
        public void GremlinCreate()
        {
            using (Transaction.Begin())
            {
                Person actor = new Person();
                actor.Uid = "11";
                actor.Name = "Tom Cruise";

                Person directorAndWriter = new Person();
                directorAndWriter.Uid = "12";
                directorAndWriter.Name = "Christopher McQuarrie";

                Person producer = new Person();
                producer.Uid = "13";
                producer.Name = "J.J Abrams";

                Person producer2 = new Person();
                producer2.Uid = "14";
                producer2.Name = "Bryan Burk";

                Person producer3 = new Person();
                producer3.Uid = "15";
                producer3.Name = "David Ellison";

                Film film = new Film();
                film.Uid = "16";
                film.Title = "Mission Impossible: Rogue Nation";
                film.ReleaseDate = new DateTime(2015, 07, 30);

                film.Actors.Add(actor);
                film.Directors.Add(directorAndWriter);
                film.Writers.Add(directorAndWriter);
                film.Producers.Add(actor);
                film.Producers.Add(producer);
                film.Producers.Add(producer2);
                film.Producers.Add(producer3);

                Transaction.Commit();
            }

            using (Transaction.Begin())
            {
                Person tom = Person.Load("11");
                Assert.IsNotNull(tom);
                Assert.AreEqual(tom.Name, "Tom Cruise");

                Person directorAndWriter = Person.Load("12");
                Assert.IsNotNull(directorAndWriter);
                Assert.AreEqual(directorAndWriter.Name, "Christopher McQuarrie");

                Person producer = Person.Load("13");
                Assert.IsNotNull(producer);
                Assert.AreEqual(producer.Name, "J.J Abrams");

                Person producer2 = Person.Load("14");
                Assert.IsNotNull(producer2);
                Assert.AreEqual(producer2.Name, "Bryan Burk");

                Person producer3 = Person.Load("15");
                Assert.IsNotNull(producer3);
                Assert.AreEqual(producer3.Name, "David Ellison");

                Film film = Film.Load("16");
                Assert.IsNotNull(film);
                Assert.AreEqual(film.Title, "Mission Impossible: Rogue Nation");

                Assert.IsTrue(film.Actors.Contains(tom));
                Assert.IsTrue(film.Directors.Contains(directorAndWriter));
                Assert.IsTrue(film.Writers.Contains(directorAndWriter));
                Assert.IsTrue(film.Producers.Contains(tom));
                Assert.IsTrue(film.Producers.Contains(producer));
                Assert.IsTrue(film.Producers.Contains(producer2));
                Assert.IsTrue(film.Producers.Contains(producer3));
            }
        }

        [Test, Order(2)]
        public void GremlinUpdate()
        {
            using (Transaction.Begin())
            {
                Person keanu = Person.Load("11");
                keanu.Name = "Keanu Reeves";

                Person director = Person.Load("12");
                director.Name = "Chad Stahelski";

                Person producer = Person.Load("13");
                producer.Name = "Basil Iwanyk";

                Person producer2 = Person.Load("14");
                producer2.Name = "David Leitch";

                Person producer3 = Person.Load("15");
                producer3.Name = "Eva Longoria";

                Film film = Film.Load("16");
                film.Title = "John Wick";

                film.Writers.Clear();
                film.Writers.Add(new Person()
                {
                    Uid = "17",
                    Name = "Derek Kolstad"
                });

                Transaction.Commit();
            }

            using (Transaction.Begin())
            {
                Person keanu = Person.Load("11");
                Assert.IsNotNull(keanu);
                Assert.AreEqual(keanu.Name, "Keanu Reeves");

                Person director = Person.Load("12");
                Assert.IsNotNull(director);
                Assert.AreEqual(director.Name, "Chad Stahelski");

                Person producer = Person.Load("13");
                Assert.IsNotNull(producer);
                Assert.AreEqual(producer.Name, "Basil Iwanyk");

                Person producer2 = Person.Load("14");
                Assert.IsNotNull(producer2);
                Assert.AreEqual(producer2.Name, "David Leitch");

                Person producer3 = Person.Load("15");
                Assert.IsNotNull(producer3);
                Assert.AreEqual(producer3.Name, "Eva Longoria");

                Film film = Film.Load("16");
                Assert.IsNotNull(film);
                Assert.AreEqual(film.Title, "John Wick");

                Assert.IsTrue(film.Actors.Contains(keanu));
                Assert.IsTrue(film.Directors.Contains(director));
                Assert.IsTrue(film.Writers.Contains(Person.Load("17")));
                Assert.IsTrue(film.Producers.Contains(keanu));
                Assert.IsTrue(film.Producers.Contains(producer));
                Assert.IsTrue(film.Producers.Contains(producer2));
                Assert.IsTrue(film.Producers.Contains(producer3));
            }
        }

        [Test, Order(3)]
        public void GremlinDelete()
        {
            // TODO: Normal Delete is not supported
            Helper.AssertThrows<System.Data.DBConcurrencyException>(delegate ()
            {
                using (Transaction.Begin())
                {
                    Person keanu = Person.Load("11");
                    keanu.Delete();

                    Transaction.Commit();
                }
            });

            using (Transaction.Begin())
            {
                Person keanu = Person.Load("11");
                keanu.ForceDelete();

                Transaction.Commit();
            }

            using (Transaction.Begin())
            {
                Person keanu = Person.Load("11");
                Assert.IsNull(keanu);
            }
        }

        [Test, Order(4)]
        public void GremlinList()
        {
            Assert.Throws<NotSupportedException>(delegate ()
            {
                using (Transaction.Begin())
                {
                    Person.Search("Derek");
                }

            });

            Parameter Uid = Parameter.New<string>("P0");

            ICompiled query = Transaction.CompiledQuery
                .Match(Datastore.Query.Node
                    .Film.Alias(out var filmAlias)
                    .Out.PERSON_WROTE_FILM.In
                    .Person.Alias(out var persons)
                ).Where(
                    filmAlias.Uid == Uid
                ).Return(persons)
                .Compile();

            using (Transaction.Begin())
            {
                List<Person> writers = Person.LoadWhere(query, new Parameter("P0", "16"));
                Assert.IsNotNull(writers);
                Assert.IsTrue(writers.Count == 1);

                Assert.AreEqual(writers[0].Name, "Derek Kolstad");
            }

            using (Transaction.Begin())
            {
                List<Person> allPersons = Person.GetAll();
                Assert.IsNotNull(allPersons);
                Assert.AreEqual(allPersons.Count, 5);
            }

            using (Transaction.Begin())
            {
                List<Person> allPersons = Person.GetAll(0, 10, Person.Members.Name);
                Assert.IsNotNull(allPersons);
                Assert.AreEqual(allPersons.Count, 5);
                Assert.AreEqual(allPersons[4].Name, "Eva Longoria");
            }

            using (Transaction.Begin())
            {
                List<Person> david = Person.GetAll(2, 1, Person.Members.Name);
                Assert.IsNotNull(david);
                Assert.AreEqual(david.Count, 1);
                Assert.AreEqual(david[0].Name, "David Leitch");
            }
        }
    }
}
