#pragma warning disable S101 // Types should be named in PascalCase

using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Datastore.Query;
using node = Datastore.Query.Node;

namespace Datastore.Manipulation
{
    /// <summary>
    /// Relationship: (Movie)-[MOVIE_CERTIFICATION]->(Rating)
    /// </summary>
    public partial class MOVIE_CERTIFICATION
    {
        private MOVIE_CERTIFICATION(string elementId, Movie @in, Rating @out, Dictionary<string, object> properties)
        {
            _elementId = elementId;
            
            Movie = @in;
            Rating = @out;
            
            CreationDate = (System.DateTime?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime?), properties.GetValue("CreationDate"));
            FrighteningIntense = (Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?), properties.GetValue("FrighteningIntense"));
            ViolenceGore = (Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?), properties.GetValue("ViolenceGore"));
            Profanity = (Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?), properties.GetValue("Profanity"));
            Substances = (Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?), properties.GetValue("Substances"));
            SexAndNudity = (Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?), properties.GetValue("SexAndNudity"));
        }

        internal string _elementId { get; private set; }

        /// <summary>
        /// Person (In Node)
        /// </summary>
        public Movie Movie { get; private set; }

        /// <summary>
        /// Restaurant (Out Node)
        /// </summary>
        public Rating Rating { get; private set; }

        public System.DateTime? CreationDate { get; private set; }
        public Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent? FrighteningIntense { get; private set; }
        public Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent? ViolenceGore { get; private set; }
        public Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent? Profanity { get; private set; }
        public Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent? Substances { get; private set; }
        public Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent? SexAndNudity { get; private set; }

        public void Assign(JsNotation<Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?> FrighteningIntense = default, JsNotation<Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?> Profanity = default, JsNotation<Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?> SexAndNudity = default, JsNotation<Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?> Substances = default, JsNotation<Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?> ViolenceGore = default)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Movie.Alias(out var inAlias).In.MOVIE_CERTIFICATION.Alias(out var relAlias).Out.Rating.Alias(out var outAlias))
                .Where(inAlias.Uid == Movie.Uid, outAlias.Uid == Rating.Uid, relAlias.ElementId == _elementId)
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.MOVIE_CERTIFICATION_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
                if (FrighteningIntense.HasValue) assignments.Add(new Assignment(alias.FrighteningIntense, FrighteningIntense));
                if (ViolenceGore.HasValue) assignments.Add(new Assignment(alias.ViolenceGore, ViolenceGore));
                if (Profanity.HasValue) assignments.Add(new Assignment(alias.Profanity, Profanity));
                if (Substances.HasValue) assignments.Add(new Assignment(alias.Substances, Substances));
                if (SexAndNudity.HasValue) assignments.Add(new Assignment(alias.SexAndNudity, SexAndNudity));
               
                return assignments.ToArray();
            }
        }
        public static List<MOVIE_CERTIFICATION> Where(Func<Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Movie.Alias(out var inAlias).In.MOVIE_CERTIFICATION.Alias(out var relAlias).Out.Rating.Alias(out var outAlias))
                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<MOVIE_CERTIFICATION> Where(Func<Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Movie.Alias(out var inAlias).In.MOVIE_CERTIFICATION.Alias(out var relAlias).Out.Rating.Alias(out var outAlias))
                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<MOVIE_CERTIFICATION> Where(JsNotation<System.DateTime?> CreationDate = default, JsNotation<Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?> FrighteningIntense = default, JsNotation<Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?> Profanity = default, JsNotation<Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?> SexAndNudity = default, JsNotation<Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?> Substances = default, JsNotation<Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?> ViolenceGore = default, JsNotation<Movie> InNode = default, JsNotation<Rating> OutNode = default)
        {
            return Where(delegate(Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (FrighteningIntense.HasValue) conditions.Add(alias.FrighteningIntense == FrighteningIntense.Value?.ToString());
                if (ViolenceGore.HasValue) conditions.Add(alias.ViolenceGore == ViolenceGore.Value?.ToString());
                if (Profanity.HasValue) conditions.Add(alias.Profanity == Profanity.Value?.ToString());
                if (Substances.HasValue) conditions.Add(alias.Substances == Substances.Value?.ToString());
                if (SexAndNudity.HasValue) conditions.Add(alias.SexAndNudity == SexAndNudity.Value?.ToString());
                if (InNode.HasValue) conditions.Add(alias.Movie(InNode.Value));
                if (OutNode.HasValue) conditions.Add(alias.Rating(OutNode.Value));

                return conditions.ToArray();
            });
        }
        internal static List<MOVIE_CERTIFICATION> Load(ICompiled query) => Load(query, null);
        internal static List<MOVIE_CERTIFICATION> Load(ICompiled query, params (string name, object value)[] arguments)
        {
            var context = query.GetExecutionContext();
            if (arguments is not null && arguments.Length > 0)
            {
                foreach ((string name, object value) in arguments)
                    context.SetParameter(name, value);
            }

            var results = context.Execute(NodeMapping.AsWritableEntity);

            return results.Select(result => new MOVIE_CERTIFICATION(
                result.elementId,
                result.@in,
                result.@out,
                result.properties
            )).ToList();
        }

        public static Relationship Relationship => ThreadSafe.LazyInit(ref _relationship, () => Blueprint41.UnitTest.Memgraph.DataStore.MockModel.Model.Relations["MOVIE_CERTIFICATION"]);
        private static Relationship _relationship = null;

        /// <summary>
        /// CRUD Specific alias for relationship: (Movie)-[MOVIE_CERTIFICATION]->(Rating)
        /// </summary>
        public partial class Alias
        {
            internal Alias(q.MOVIE_CERTIFICATION_ALIAS relAlias, q.MovieAlias inAlias, q.RatingAlias outAlias)
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
            public StringResult FrighteningIntense
            {
                get
                {
                    if (_frighteningIntense is null)
                        _frighteningIntense = _relAlias.FrighteningIntense;

                    return _frighteningIntense;
                }
            }
            private StringResult _frighteningIntense = null;
            public StringResult ViolenceGore
            {
                get
                {
                    if (_violenceGore is null)
                        _violenceGore = _relAlias.ViolenceGore;

                    return _violenceGore;
                }
            }
            private StringResult _violenceGore = null;
            public StringResult Profanity
            {
                get
                {
                    if (_profanity is null)
                        _profanity = _relAlias.Profanity;

                    return _profanity;
                }
            }
            private StringResult _profanity = null;
            public StringResult Substances
            {
                get
                {
                    if (_substances is null)
                        _substances = _relAlias.Substances;

                    return _substances;
                }
            }
            private StringResult _substances = null;
            public StringResult SexAndNudity
            {
                get
                {
                    if (_sexAndNudity is null)
                        _sexAndNudity = _relAlias.SexAndNudity;

                    return _sexAndNudity;
                }
            }
            private StringResult _sexAndNudity = null;

            /// <summary>
            /// Movie in-node: (Movie)-[MOVIE_CERTIFICATION]->(Rating)
            /// </summary>
            /// <returns>
            /// Condition where in-node is the given movie
            /// </returns>
            public QueryCondition Movie(Movie movie)
            {
                return _inAlias.Uid == movie.Uid;
            }
            /// <summary>
            /// Movie in-node: (Movie)-[MOVIE_CERTIFICATION]->(Rating)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of movies
            /// </returns>
            public QueryCondition Movies(IEnumerable<Movie> movies)
            {
                return _inAlias.Uid.In(movies.Select(item => item.Uid));
            }
            /// <summary>
            /// Movie in-node: (Movie)-[MOVIE_CERTIFICATION]->(Rating)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of movies
            /// </returns>
            public QueryCondition Movies(params Movie[] movies)
            {
                return _inAlias.Uid.In(movies.Select(item => item.Uid));
            }

            /// <summary>
            /// Rating out-node: (Movie)-[MOVIE_CERTIFICATION]->(Rating)
            /// </summary>
            /// <returns>
            /// Condition where out-node is the given rating
            /// </returns>
            public QueryCondition Rating(Rating rating)
            {
                return _outAlias.Uid == rating.Uid;
            }
            /// <summary>
            /// Rating out-node: (Movie)-[MOVIE_CERTIFICATION]->(Rating)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of ratings
            /// </returns>
            public QueryCondition Ratings(IEnumerable<Rating> ratings)
            {
                return _outAlias.Uid.In(ratings.Select(item => item.Uid));
            }
            /// <summary>
            /// Rating out-node: (Movie)-[MOVIE_CERTIFICATION]->(Rating)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of ratings
            /// </returns>
            public QueryCondition Ratings(params Rating[] ratings)
            {
                return _outAlias.Uid.In(ratings.Select(item => item.Uid));
            }

            private readonly q.MOVIE_CERTIFICATION_ALIAS _relAlias;
            private readonly q.MovieAlias _inAlias;
            private readonly q.RatingAlias _outAlias;
        }
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<MOVIE_CERTIFICATION> @this, JsNotation<Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?> FrighteningIntense = default, JsNotation<Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?> Profanity = default, JsNotation<Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?> SexAndNudity = default, JsNotation<Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?> Substances = default, JsNotation<Blueprint41.UnitTest.Memgraph.DataStore.RatingComponent?> ViolenceGore = default)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Movie.Alias(out var inAlias).In.MOVIE_CERTIFICATION.Alias(out var relAlias).Out.Rating.Alias(out var outAlias))
                .Where(relAlias.ElementId.In(@this.Select(item => item._elementId)))
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.MOVIE_CERTIFICATION_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
                if (FrighteningIntense.HasValue) assignments.Add(new Assignment(alias.FrighteningIntense, FrighteningIntense));
                if (ViolenceGore.HasValue) assignments.Add(new Assignment(alias.ViolenceGore, ViolenceGore));
                if (Profanity.HasValue) assignments.Add(new Assignment(alias.Profanity, Profanity));
                if (Substances.HasValue) assignments.Add(new Assignment(alias.Substances, Substances));
                if (SexAndNudity.HasValue) assignments.Add(new Assignment(alias.SexAndNudity, SexAndNudity));
               
                return assignments.ToArray();
            }
        }
    }
}