using Blueprint41.GremlinUnitTest.Misc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.GremlinUnitTest
{
    internal class GremlinStoreCRUD : DatastoreModel<GremlinStoreCRUD>
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
    }    
}
