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


            Entities.New("Genre")
                .HasStaticData(true)
                .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                .SetKey("Uid", true)
                .AddProperty("Name", typeof(string), false);

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


            Relations.New(Entities["Movie"], Entities["Genre"], "CONTAINS_GENRE", "CONTAINS_GENRE")
                .SetInProperty("Genres", PropertyType.Collection)
                .SetOutProperty("Movies", PropertyType.Collection);

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


            Entities["Genre"].Refactor.CreateNode(new { Uid = "1", Name = "Action" });
            Entities["Genre"].Refactor.CreateNode(new { Uid = "2", Name = "Animation" });
            Entities["Genre"].Refactor.CreateNode(new { Uid = "3", Name = "Biography" });
            Entities["Genre"].Refactor.CreateNode(new { Uid = "4", Name = "Comedy" });
            Entities["Genre"].Refactor.CreateNode(new { Uid = "5", Name = "Documentary" });
            Entities["Genre"].Refactor.CreateNode(new { Uid = "6", Name = "Drama" });
            Entities["Genre"].Refactor.CreateNode(new { Uid = "7", Name = "Fantasy" });
            Entities["Genre"].Refactor.CreateNode(new { Uid = "8", Name = "Horror" });
            Entities["Genre"].Refactor.CreateNode(new { Uid = "9", Name = "Musical" });
            Entities["Genre"].Refactor.CreateNode(new { Uid = "10", Name = "Reality-Tv" });
            Entities["Genre"].Refactor.CreateNode(new { Uid = "11", Name = "Romance" });
            Entities["Genre"].Refactor.CreateNode(new { Uid = "12", Name = "Sci-Fi" });
            Entities["Genre"].Refactor.CreateNode(new { Uid = "13", Name = "Short" });
            Entities["Genre"].Refactor.CreateNode(new { Uid = "14", Name = "Talk-Show" });
            Entities["Genre"].Refactor.CreateNode(new { Uid = "15", Name = "Thriller" });
        }

        protected override void SubscribeEventHandlers()
        {

        }
    }
}
