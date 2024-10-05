using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.UnitTest.DataStore
{
    public class MockModel : DatastoreModel<MockModel>
    {
#if NEO4J
        public override GDMS DatastoreTechnology => GDMS.Neo4j;
#elif MEMGRAPH
        public override GDMS DatastoreTechnology => GDMS.Memgraph;
#endif

        protected override void SubscribeEventHandlers()
        {

        }

        [Version(0, 0, 0)]
        public void Script_0_0_0()
        {
#if NEO4J
            FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);
#elif MEMGRAPH
            FunctionalIds.Default = FunctionalIds.UUID;
#endif

            Entities.New("BaseEntity")
                .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                .Abstract(true)
                .Virtual(true)
                .SetKey("Uid")
                .AddProperty("LastModifiedOn", typeof(DateTime))
                .SetRowVersionField("LastModifiedOn");

            Entities.New("Person", Entities["BaseEntity"])
                .AddProperty("Name", typeof(string), false);

            Entities.New("City", Entities["BaseEntity"])
                .AddProperty("Name", typeof(string), false, IndexType.Unique)
                .AddProperty("State", typeof(string))
                .AddProperty("Country", typeof(string));

            Entities.New("Restaurant", Entities["BaseEntity"])
                .AddProperty("Name", typeof(string));

            Entities.New("Movie", Entities["BaseEntity"])
                   .AddProperty("Title", typeof(string), false, IndexType.Unique);

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


            // Property types: string, int, datetime, Enum
            // Mandatory & Optional

            Entities.New("StreamingService", Entities["BaseEntity"])
                .AddProperty("Name", typeof(string), false, IndexType.Unique);

            Entities.New("Rating", Entities["BaseEntity"])
                .AddProperty("Code", typeof(string), false, IndexType.Unique)
                .AddProperty("Name", typeof(string), false, IndexType.Unique)
                .AddProperty("Description", typeof(string), false);


            // Collection, time dependent:   (Person)-[SUBSCRIBED_TO { StartDate, EndDate, MonthlyFee }]->(StreamingService)
            Relations.New(Entities["Person"], Entities["StreamingService"], "SUBSCRIBED_TO_STREAMING_SERVICE", "SUBSCRIBED_TO")
                .SetInProperty("StreamingServiceSubscriptions", PropertyType.Collection)
                .SetOutProperty("Subscribers", PropertyType.Collection)
                .AddProperty("MonthlyFee", typeof(decimal), false)
                .AddTimeDependence();

            // Collection, time independent: (Person)-[WATCHED { Minutes }]->(Movie)
            Relations.New(Entities["Person"], Entities["Movie"], "WATCHED_MOVIE", "WATCHED")
                .SetInProperty("WatchedMovies", PropertyType.Collection)
                .AddProperty("MinutesWatched", typeof(int), false);  // Not a key-property, because if minutes watched changes, it should overwrite

            // Lookup, time dependent:       (Person)-[PERSON_LIVES_IN { AddressLine1..3, ZipCode }]->(City)
            Relations.New(Entities["Person"], Entities["City"], "PERSON_LIVES_IN", "LIVES_IN")
                .SetInProperty("City", PropertyType.Lookup)
                .AddTimeDependence()
                .AddProperty("AddressLine1", typeof(string))
                .AddProperty("AddressLine2", typeof(string))
                .AddProperty("AddressLine3", typeof(string));

            // Lookup, time independent:     (Movie)-[CERTIFICATION { SexNudity: Mild, ViolenceGore: Moderate, Profanity, AlcoholDrugsSmoking, FrighteningIntenseScenes }]->(Rating)
            Relations.New(Entities["Movie"], Entities["Rating"], "MOVIE_CERTIFICATION", "CERTIFICATION")
                .SetInProperty("Certification", PropertyType.Lookup)
                .AddProperty("FrighteningIntense", typeof(RatingComponent))
                .AddProperty("ViolenceGore", typeof(RatingComponent))
                .AddProperty("Profanity", typeof(RatingComponent))
                .AddProperty("Substances", typeof(RatingComponent)) // IMDB: AlcoholDrugsSmoking
                .AddProperty("SexAndNudity", typeof(RatingComponent));

            /*
              
                CURRENTLY NOT SUPPORTED:
            
                Relationship with key-properties and optionally other properties.
                Time dependent relationships essentially NOT relationships with key-properties == StartDate + EndDate!!!!

             */
        }
    }

    public enum RatingComponent
    {
        None,
        Mild,
        Moderate,
        Severe,
    }


    // (person-[]-(city)

    // (P1)-[]-(C1)  prop: X = 1
    // <------------->

    // Code: P1.SetCity(C1, X: 2);
    // (P1)-[]-(C1)  prop: X = 1
    // <------|

    // (P1)-[]-(C1)  prop: X = 2
    //        |------>


    // Code: P1.CityRelation().Assign(X: 2);
    // (P1)-[]-(C1)  prop: X = 2
    // <------------->
}
