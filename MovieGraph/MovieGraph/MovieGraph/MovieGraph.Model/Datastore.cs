using Blueprint41;
using System;
using System.Collections.Generic;

namespace MovieGraph.Model
{
    public class Datastore : DatastoreModel<Datastore>
    {
        [Version(1, 0, 0)]
        protected void Initial()
        {
            FunctionalIds.Default = FunctionalIds.New("Shared", "SH_", IdFormat.Numeric, 0);


            Entities.New("Movie")
                .AddProperty("title", typeof(string))
                .AddProperty("tagline", typeof(string))
                .AddProperty("released", typeof(int))
                .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                .SetKey("Uid", true);

            Entities.New("MovieReview")
                .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                .SetKey("Uid", true)
                .AddProperty("Review", typeof(string))
                .AddProperty("Rating", typeof(decimal));

            Entities.New("MovieRole")
                .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                .SetKey("Uid", true)
                .AddProperty("Role", typeof(List<string>));

            Entities.New("Person")
                .AddProperty("name", typeof(string))
                .AddProperty("born", typeof(int))
                .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                .SetKey("Uid", true);


            Relations.New(Entities["Person"], Entities["Movie"], "ACTED_IN", "ACTED_IN")
                .SetInProperty("ActedMovies", PropertyType.Collection)
                .SetOutProperty("Actors", PropertyType.Collection);

            Relations.New(Entities["Person"], Entities["Movie"], "DIRECTED", "DIRECTED")
                .SetInProperty("DirectedMovies", PropertyType.Collection)
                .SetOutProperty("Directors", PropertyType.Collection);

            Relations.New(Entities["Person"], Entities["Movie"], "PRODUCED", "PRODUCED")
                .SetInProperty("ProducedMovies", PropertyType.Collection)
                .SetOutProperty("Producers", PropertyType.Collection);

            Relations.New(Entities["Person"], Entities["Movie"], "WROTE", "WROTE")
                .SetInProperty("WritedMovies", PropertyType.Collection)
                .SetOutProperty("Writers", PropertyType.Collection);

            Relations.New(Entities["MovieRole"], Entities["Movie"], "MOVIEROLE_HAS_MOVIE", "HAS_MOVIE")
                .SetInProperty("Movie", PropertyType.Lookup);

            Relations.New(Entities["MovieReview"], Entities["Movie"], "MOVIEREVIEW_HAS_MOVIE", "HAS_REVIEWED_MOVIE")
                .SetInProperty("Movie", PropertyType.Lookup);

            Relations.New(Entities["Person"], Entities["MovieReview"], "MOVIE_REVIEWS", "MOVIE_REVIEWS")
                .SetInProperty("MovieReviews", PropertyType.Collection);

            Relations.New(Entities["Person"], Entities["MovieRole"], "MOVIE_ROLES", "MOVIE_ROLES")
                .SetInProperty("MovieRoles", PropertyType.Collection);

            Relations.New(Entities["Person"], Entities["Person"], "FOLLOWS", "FOLLOWS")
                .SetInProperty("FollowedPersons", PropertyType.Collection)
                .SetOutProperty("Followers", PropertyType.Collection);



        }

        protected override void SubscribeEventHandlers()
        {

        }
    }
}
