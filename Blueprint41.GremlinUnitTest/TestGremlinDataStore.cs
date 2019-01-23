using Blueprint41.DatastoreTemplates;
using Blueprint41.GremlinUnitTest.Misc;
using Datastore.Manipulation;
using NUnit.Framework;
using System;
using System.IO;

namespace Blueprint41.GremlinUnitTest
{
    [TestFixture]
    internal class TestGremlinDataStore : GremlinBase
    {
        class GremlinStore : DatastoreModel<GremlinStore>
        {
            public override GraphFeatures TargetFeatures => GraphFeatures.Gremlin;

            protected override void SubscribeEventHandlers() { }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                Helper.AssertThrows<NotSupportedException>(() =>
                {
                    FunctionalIds.Default = FunctionalIds.New("Shared", "SH", IdFormat.Numeric);

                }, "The current graph database does not support this feature 'Functional Id'");

                Entities.New("Base")
                    .Abstract()
                    .Virtual()
                    .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                    .SetKey("Uid", true)
                    .AddProperty("LastModifiedOn", typeof(DateTime))
                    .SetRowVersionField("LastModifiedOn");

                Entities.New("Film", Entities["Base"])
                .AddProperty("Title", typeof(string), false)
                .AddProperty("TagLine", typeof(string))
                .AddProperty("ReleaseDate", typeof(DateTime));

                Entities.New("Person", Entities["Base"])
                    .AddProperty("Name", typeof(string))
                    .AddProperty("Address", typeof(string));

                Entities.New("Genre", Entities["Base"])
                    .HasStaticData()
                    .AddProperty("Name", typeof(string), false)
                    .AddProperty("DateAdded", typeof(DateTime));

                Relations.New(Entities["Person"], Entities["Film"], "PERSON_ACTED_IN_FILM", "ACTED_IN")
                    .SetInProperty("ActedFilms", PropertyType.Collection)
                    .SetOutProperty("Actors", PropertyType.Collection);

                Relations.New(Entities["Person"], Entities["Film"], "PERSON_DIRECTED_FILM", "DIRECTED")
                    .SetInProperty("DirectedFilms", PropertyType.Collection)
                    .SetOutProperty("Directors", PropertyType.Collection);

                Relations.New(Entities["Person"], Entities["Film"], "PERSON_PRODUCED_FILM", "PRODUCED")
                    .SetInProperty("ProducedFilms", PropertyType.Collection)
                    .SetOutProperty("Producers", PropertyType.Collection);

                Relations.New(Entities["Person"], Entities["Film"], "PERSON_WROTE_FILM", "WROTE")
                    .SetInProperty("FilmsWrited", PropertyType.Collection)
                    .SetOutProperty("Writers", PropertyType.Collection);

                Relations.New(Entities["Genre"], Entities["Genre"], "GENRE_HAS_SUBGENRE", "SUBGENRE")
                    .SetInProperty("SubGenre", PropertyType.Collection)
                    .SetOutProperty("ParentGenre", PropertyType.Collection);

                dynamic action = Entities["Genre"].Refactor.CreateNode(new { Uid = "1", Name = "Action", DateAdded = DateTime.Now });
                dynamic horror = Entities["Genre"].Refactor.CreateNode(new { Uid = "2", Name = "Horror", DateAdded = DateTime.Now });
                dynamic comedy = Entities["Genre"].Refactor.CreateNode(new { Uid = "3", Name = "Comedy", DateAdded = DateTime.Now });
                dynamic doc = Entities["Genre"].Refactor.CreateNode(new { Uid = "4", Name = "Documentary", DateAdded = DateTime.Now });

                Entities["Genre"].Refactor.MatchNode("1").SubGenre.Add(horror);
                Entities["Genre"].Refactor.MatchNode("1").SubGenre.Add(comedy);
                Entities["Genre"].Refactor.MatchNode("1").SubGenre.Add(doc);
            }

            [Version(0, 0, 1)]
            public void RefactorProperties()
            {
                Entities["Genre"].Properties["Name"].Refactor.Rename("Title");
                Entities["Genre"].Properties["DateAdded"].Refactor.MakeMandatory();                
                Assert.IsFalse(Entities["Genre"].Properties["DateAdded"].Nullable);

                Entities["Genre"].Properties["DateAdded"].Refactor.MakeNullable();
                Assert.IsTrue(Entities["Genre"].Properties["DateAdded"].Nullable);

                // TODO: Refactor Reroute 
                // Not sure how to implement this, will check for samples.
                //Entities["Genre"].Properties["DateAdded"].Refactor.Reroute();
                Entities["Genre"].Properties["DateAdded"].Refactor.Deprecate();
            }

            [Version(0, 0, 2)]
            public void RefactorRelationship()
            {
                Entities.New("SubGenre", Entities["Genre"])
                    .HasStaticData()
                    .AddProperty("Name", typeof(string), false)
                    .AddProperty("DateAdded", typeof(DateTime));

                Assert.Throws<NotSupportedException>(delegate ()
                {
                    Relations["GENRE_HAS_SUBGENRE"].Refactor.Rename("HAS_SUB_GENRE", "HAS_SUB");
                });

                Assert.Throws<NotSupportedException>(delegate ()
                {
                    Relations["HAS_SUB_GENRE"].Refactor.Rename("GENRE_HAS_SUBGENRE", "SUBGENRE");
                });

                Relations["GENRE_HAS_SUBGENRE"].Refactor.SetOutEntity(Entities["SubGenre"], true);
                Relations["GENRE_HAS_SUBGENRE"].Refactor.Deprecate();
            }

            [Version(0, 0, 3)]
            public void RefactorEntities()
            {
                Entities["Person"].Refactor.SetDefaultValue((a) =>
                {
                    a.Address = "DefaultAddress";
                });

                Assert.Throws<NotSupportedException>(() => Entities["Person"].Refactor.Rename("People"));

                dynamic node = Entities["Genre"].Refactor.MatchNode("1");
                Assert.IsNotNull((object)node);

                Entities["Genre"].Refactor.DeleteNode("1");
                Assert.Throws<ArgumentOutOfRangeException>(() => Entities["Genre"].Refactor.MatchNode("1"));

                Entities["Film"].Refactor.CopyValue("Title", "TagLine");
                Assert.Throws<NotSupportedException>(() => Entities["Film"].Refactor.SetFunctionalId(null, ApplyAlgorithm.ReapplyAll));

                Entities["Genre"].Refactor.Deprecate();
            }
        }

        public override Type Datastoremodel => typeof(GremlinStore);

        [Test]
        public void CheckGremlinGraphFeatures()
        {
            using (Transaction.Begin())
            {
                Film film = new Film();
                film.Uid = "112";
                film.Title = "Film Title";

                Person person = new Person();
                person.Uid = "113";
                person.Name = "Name";

                Transaction.Commit();
            }

            GremlinStore store = new GremlinStore();
            store.Execute(true);

            using (Transaction.Begin())
            {
                Film film = Film.Load("112");
                Assert.AreEqual(film.Title, film.TagLine);
            }

            using (Transaction.Begin())
            {
                Person person = Person.Load("113");
                Assert.AreEqual(person.Address, "DefaultAddress");
            }
        }
    }
}
