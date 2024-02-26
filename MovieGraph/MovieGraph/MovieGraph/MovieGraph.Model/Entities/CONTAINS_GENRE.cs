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
    /// Relationship: (Movie)-[CONTAINS_GENRE]->(Genre)
    /// </summary>
    public partial class CONTAINS_GENRE
    {
        private CONTAINS_GENRE(string elementId, Movie @in, Genre @out, Dictionary<string, object> properties)
        {
            _elementId = elementId;
            
            Movie = @in;
            Genre = @out;
            
            CreationDate = (System.DateTime?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime?), properties.GetValue("CreationDate"));
        }

        internal string _elementId { get; private set; }

        /// <summary>
        /// Movie (In Node)
        /// </summary>
        public Movie Movie { get; private set; }

        /// <summary>
        /// Genre (Out Node)
        /// </summary>
        public Genre Genre { get; private set; }

        public System.DateTime? CreationDate { get; private set; }

        public void Assign()
        {
            var query = Transaction.CompiledQuery
                .Match(node.Movie.Alias(out var inAlias).In.CONTAINS_GENRE.Alias(out var relAlias).Out.Genre.Alias(out var outAlias))
                .Where(inAlias.Uid == Movie.Uid, outAlias.Uid == Genre.Uid, relAlias.ElementId == _elementId)
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.CONTAINS_GENRE_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
               
                return assignments.ToArray();
            }
        }
        public static List<CONTAINS_GENRE> Where(Func<Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Movie.Alias(out var inAlias).In.CONTAINS_GENRE.Alias(out var relAlias).Out.Genre.Alias(out var outAlias))
                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<CONTAINS_GENRE> Where(Func<Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Movie.Alias(out var inAlias).In.CONTAINS_GENRE.Alias(out var relAlias).Out.Genre.Alias(out var outAlias))

                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<CONTAINS_GENRE> Where(JsNotation<System.DateTime?> CreationDate = default, JsNotation<Movie> InNode = default, JsNotation<Genre> OutNode = default)
        {
            return Where(delegate(Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (InNode.HasValue) conditions.Add(alias.Movie(InNode.Value));
                if (OutNode.HasValue) conditions.Add(alias.Genre(OutNode.Value));

                return conditions.ToArray();
            });
        }
        internal static List<CONTAINS_GENRE> Load(ICompiled query) => Load(query, null);
        internal static List<CONTAINS_GENRE> Load(ICompiled query, params (string name, object value)[] arguments)
        {
            var context = query.GetExecutionContext();
            if (arguments is not null && arguments.Length > 0)
            {
                foreach ((string name, object value) in arguments)
                    context.SetParameter(name, value);
            }

            var results = context.Execute(NodeMapping.AsWritableEntity);

            return results.Select(result => new CONTAINS_GENRE(
                result.elementId,
                result.@in,
                result.@out,
                result.properties
            )).ToList();
        }

        public static Relationship Relationship => ThreadSafe.LazyInit(ref _relationship, () => MovieGraph.Model.Datastore.Model.Relations["CONTAINS_GENRE"]);
        private static Relationship _relationship = null;

        /// <summary>
        /// CRUD Specific alias for relationship: (Movie)-[CONTAINS_GENRE]->(Genre)
        /// </summary>
        public partial class Alias
        {
            internal Alias(q.CONTAINS_GENRE_ALIAS relAlias, q.MovieAlias inAlias, q.GenreAlias outAlias)
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
            /// Movie in-node: (Movie)-[CONTAINS_GENRE]->(Genre)
            /// </summary>
            /// <returns>
            /// Condition where in-node is the given movie
            /// </returns>
            public QueryCondition Movie(Movie movie)
            {
                return _inAlias.Uid == movie.Uid;
            }
            /// <summary>
            /// Movie in-node: (Movie)-[CONTAINS_GENRE]->(Genre)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of movies
            /// </returns>
            public QueryCondition Movies(IEnumerable<Movie> movies)
            {
                return _inAlias.Uid.In(movies.Select(item => item.Uid));
            }
            /// <summary>
            /// Movie in-node: (Movie)-[CONTAINS_GENRE]->(Genre)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of movies
            /// </returns>
            public QueryCondition Movies(params Movie[] movies)
            {
                return _inAlias.Uid.In(movies.Select(item => item.Uid));
            }

            /// <summary>
            /// Genre out-node: (Movie)-[CONTAINS_GENRE]->(Genre)
            /// </summary>
            /// <returns>
            /// Condition where out-node is the given genre
            /// </returns>
            public QueryCondition Genre(Genre genre)
            {
                return _outAlias.Uid == genre.Uid;
            }
            /// <summary>
            /// Genre out-node: (Movie)-[CONTAINS_GENRE]->(Genre)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of genres
            /// </returns>
            public QueryCondition Genres(IEnumerable<Genre> genres)
            {
                return _outAlias.Uid.In(genres.Select(item => item.Uid));
            }
            /// <summary>
            /// Genre out-node: (Movie)-[CONTAINS_GENRE]->(Genre)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of genres
            /// </returns>
            public QueryCondition Genres(params Genre[] genres)
            {
                return _outAlias.Uid.In(genres.Select(item => item.Uid));
            }

            private readonly q.CONTAINS_GENRE_ALIAS _relAlias;
            private readonly q.MovieAlias _inAlias;
            private readonly q.GenreAlias _outAlias;
        }
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<CONTAINS_GENRE> @this)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Movie.Alias(out var inAlias).In.CONTAINS_GENRE.Alias(out var relAlias).Out.Genre.Alias(out var outAlias))
                .Where(relAlias.ElementId.In(@this.Select(item => item._elementId)))
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.CONTAINS_GENRE_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
               
                return assignments.ToArray();
            }
        }
    }
}