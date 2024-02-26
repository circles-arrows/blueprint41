#pragma warning disable S101 // Types should be named in PascalCase

using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Domain.Data.Query;
using node = Domain.Data.Query.Node;

namespace Domain.Data.Manipulation
{
    /// <summary>
    /// Relationship: (MovieReview)-[MOVIEREVIEW_HAS_MOVIE]->(Movie)
    /// </summary>
    public partial class MOVIEREVIEW_HAS_MOVIE
    {
        private MOVIEREVIEW_HAS_MOVIE(string elementId, MovieReview @in, Movie @out, Dictionary<string, object> properties)
        {
            _elementId = elementId;
            
            MovieReview = @in;
            Movie = @out;
            
            CreationDate = (System.DateTime?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime?), properties.GetValue("CreationDate"));
        }

        internal string _elementId { get; private set; }

        /// <summary>
        /// MovieReview (In Node)
        /// </summary>
        public MovieReview MovieReview { get; private set; }

        /// <summary>
        /// Movie (Out Node)
        /// </summary>
        public Movie Movie { get; private set; }

        public System.DateTime? CreationDate { get; private set; }

        public void Assign()
        {
            var query = Transaction.CompiledQuery
                .Match(node.MovieReview.Alias(out var inAlias).In.MOVIEREVIEW_HAS_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == MovieReview.Uid, outAlias.Uid == Movie.Uid, relAlias.ElementId == _elementId)
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.MOVIEREVIEW_HAS_MOVIE_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
               
                return assignments.ToArray();
            }
        }
        public static List<MOVIEREVIEW_HAS_MOVIE> Where(Func<Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.MovieReview.Alias(out var inAlias).In.MOVIEREVIEW_HAS_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<MOVIEREVIEW_HAS_MOVIE> Where(Func<Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.MovieReview.Alias(out var inAlias).In.MOVIEREVIEW_HAS_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))

                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<MOVIEREVIEW_HAS_MOVIE> Where(JsNotation<System.DateTime?> CreationDate = default, JsNotation<MovieReview> InNode = default, JsNotation<Movie> OutNode = default)
        {
            return Where(delegate(Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (InNode.HasValue) conditions.Add(alias.MovieReview(InNode.Value));
                if (OutNode.HasValue) conditions.Add(alias.Movie(OutNode.Value));

                return conditions.ToArray();
            });
        }
        internal static List<MOVIEREVIEW_HAS_MOVIE> Load(ICompiled query) => Load(query, null);
        internal static List<MOVIEREVIEW_HAS_MOVIE> Load(ICompiled query, params (string name, object value)[] arguments)
        {
            var context = query.GetExecutionContext();
            if (arguments is not null && arguments.Length > 0)
            {
                foreach ((string name, object value) in arguments)
                    context.SetParameter(name, value);
            }

            var results = context.Execute(NodeMapping.AsWritableEntity);

            return results.Select(result => new MOVIEREVIEW_HAS_MOVIE(
                result.elementId,
                result.@in,
                result.@out,
                result.properties
            )).ToList();
        }

        public static Relationship Relationship => ThreadSafe.LazyInit(ref _relationship, () => MovieGraph.Model.Datastore.Model.Relations["MOVIEREVIEW_HAS_MOVIE"]);
        private static Relationship _relationship = null;

        /// <summary>
        /// CRUD Specific alias for relationship: (MovieReview)-[MOVIEREVIEW_HAS_MOVIE]->(Movie)
        /// </summary>
        public partial class Alias
        {
            internal Alias(q.MOVIEREVIEW_HAS_MOVIE_ALIAS relAlias, q.MovieReviewAlias inAlias, q.MovieAlias outAlias)
            {
                _relAlias = relAlias;
                _inAlias = inAlias;
                _outAlias = outAlias;
            }

            public DateTimeResult CreationDate
            {
                get
                {
                    if (_creationDate is null)
                        _creationDate = _relAlias.CreationDate;

                    return _creationDate;
                }
            }
            private DateTimeResult _creationDate = null;

            /// <summary>
            /// MovieReview in-node: (MovieReview)-[MOVIEREVIEW_HAS_MOVIE]->(Movie)
            /// </summary>
            /// <returns>
            /// Condition where in-node is the given movie  review
            /// </returns>
            public QueryCondition MovieReview(MovieReview movieReview)
            {
                return _inAlias.Uid == movieReview.Uid;
            }
            /// <summary>
            /// MovieReview in-node: (MovieReview)-[MOVIEREVIEW_HAS_MOVIE]->(Movie)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of movie  reviews
            /// </returns>
            public QueryCondition MovieReviews(IEnumerable<MovieReview> movieReviews)
            {
                return _inAlias.Uid.In(movieReviews.Select(item => item.Uid));
            }
            /// <summary>
            /// MovieReview in-node: (MovieReview)-[MOVIEREVIEW_HAS_MOVIE]->(Movie)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of movie  reviews
            /// </returns>
            public QueryCondition MovieReviews(params MovieReview[] movieReviews)
            {
                return _inAlias.Uid.In(movieReviews.Select(item => item.Uid));
            }

            /// <summary>
            /// Movie out-node: (MovieReview)-[MOVIEREVIEW_HAS_MOVIE]->(Movie)
            /// </summary>
            /// <returns>
            /// Condition where out-node is the given movie
            /// </returns>
            public QueryCondition Movie(Movie movie)
            {
                return _outAlias.Uid == movie.Uid;
            }
            /// <summary>
            /// Movie out-node: (MovieReview)-[MOVIEREVIEW_HAS_MOVIE]->(Movie)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of movies
            /// </returns>
            public QueryCondition Movies(IEnumerable<Movie> movies)
            {
                return _outAlias.Uid.In(movies.Select(item => item.Uid));
            }
            /// <summary>
            /// Movie out-node: (MovieReview)-[MOVIEREVIEW_HAS_MOVIE]->(Movie)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of movies
            /// </returns>
            public QueryCondition Movies(params Movie[] movies)
            {
                return _outAlias.Uid.In(movies.Select(item => item.Uid));
            }

            private readonly q.MOVIEREVIEW_HAS_MOVIE_ALIAS _relAlias;
            private readonly q.MovieReviewAlias _inAlias;
            private readonly q.MovieAlias _outAlias;
        }
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<MOVIEREVIEW_HAS_MOVIE> @this)
        {
            var query = Transaction.CompiledQuery
                .Match(node.MovieReview.Alias(out var inAlias).In.MOVIEREVIEW_HAS_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(relAlias.ElementId.In(@this.Select(item => item._elementId)))
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.MOVIEREVIEW_HAS_MOVIE_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
               
                return assignments.ToArray();
            }
        }
    }
}