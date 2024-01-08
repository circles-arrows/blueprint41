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
                .SetInProperty("City", PropertyType.Lookup);

            Relations.New(Entities["Restaurant"], Entities["City"], "RESTAURANT_LOCATED_AT", "LOCATED_AT")
                .SetInProperty("City", PropertyType.Lookup)
                .SetOutProperty("Restaurants", PropertyType.Collection);

            Relations.New(Entities["Person"], Entities["Restaurant"], "PERSON_EATS_AT", "EATS_AT")
                .SetInProperty("Restaurants", PropertyType.Collection)
                .SetOutProperty("Persons", PropertyType.Collection);

            Relations.New(Entities["Person"], Entities["Movie"], "PERSON_DIRECTED", "DIRECTED_BY")
                .SetInProperty("DirectedMovies", PropertyType.Collection)
                .SetOutProperty("Director", PropertyType.Lookup);

            Relations.New(Entities["Person"], Entities["Movie"], "ACTED_IN", "ACTORS")
                .SetInProperty("ActedInMovies", PropertyType.Collection)
                .SetOutProperty("Actors", PropertyType.Collection);



            // Lookup with properties
            // Collection with properties
            // Time Dependent Lookup with properties
            // Time Dependent Collection with properties
            // Lookup with properties & key-properties (Time Dependent == key-properties: StartDate + EndDate)
            // Collection with properties & key-properties

            // Property types: string, int, datetime, Enum
            // Mandatory & Optional

            // Collection, time dependent:   (Person)-[SUBSCRIBED_TO { StartDate, EndDate, MonthlyFee }]->(StreamingService)
            // Lookup, time dependent:       (Person)-[FAVORITE_STREAMING_SERVICE { StartDate, EndDate, Rating }]->(StreamingService)
            // Collection, time independent: (Person)-[WATCHED { Minutes }]->(Movie)
            // Lookup, time independent:     (Movie)-[CERTIFICATION { SexNudity: Mild, ViolenceGore: Moderate, Profanity, AlcoholDrugsSmoking, FrighteningIntenseScenes }]->(Rating)
        }
    }
}
