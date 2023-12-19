using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace Blueprint41.UnitTest.DataStore
{
    public class MockModel : DatastoreModel<MockModel>
    {
        protected override void SubscribeEventHandlers()
        {

        }

        [Version(0, 0, 0)]
        public void Script_0_0_0()
        {
            FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

            Entities.New("BaseEntity")
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .Abstract(true)
                   .Virtual(true)
                   .SetKey("Uid", true)
                   .AddProperty("LastModifiedOn", typeof(DateTime))
                   .SetRowVersionField("LastModifiedOn");

            Entities.New("Person", Entities["BaseEntity"])
                   .AddProperty("Name", typeof(string));

            Entities.New("City", Entities["BaseEntity"])
                   .AddProperty("Name", typeof(string), IndexType.Unique);

            Entities.New("Restaurant", Entities["BaseEntity"])
                   .AddProperty("Name", typeof(string));

            Entities.New("Movie", Entities["BaseEntity"])
                   .AddProperty("Title", typeof(string), IndexType.Unique);

            Relations.New(Entities["Person"], Entities["City"], "PERSON_LIVES_IN", "LIVES_IN")
                .SetInProperty("City", PropertyType.Lookup)
                .AddProperty("HouseNr", typeof(int), false)
                .AddProperty("Street", typeof(string), false)
                .AddProperty("StartDate", typeof(DateTime), false)
                .AddProperty("EndDate", typeof(DateTime), false);

            Relations.New(Entities["Restaurant"], Entities["City"], "RESTAURANT_LOCATED_AT", "LOCATED_AT")
                .SetInProperty("City", PropertyType.Lookup)
                .SetOutProperty("Restaurants", PropertyType.Collection)
                .AddTimeDependance();

            Relations.New(Entities["Person"], Entities["Restaurant"], "PERSON_EATS_AT", "EATS_AT")
                .SetInProperty("Restaurants", PropertyType.Collection)
                .SetOutProperty("Persons", PropertyType.Collection)
                .AddProperty("Score", new[] { "Good", "Average", "Bad"}, true, IndexType.Indexed);

            Relations.New(Entities["Person"], Entities["Movie"], "PERSON_DIRECTED", "DIRECTED_BY")
                .SetInProperty("DirectedMovies", PropertyType.Collection)
                .SetOutProperty("Director", PropertyType.Lookup);

            Relations.New(Entities["Person"], Entities["Movie"], "ACTED_IN", "ACTORS")
                .SetInProperty("ActedInMovies", PropertyType.Collection)
                .SetOutProperty("Actors", PropertyType.Collection);
        }
    }
}
