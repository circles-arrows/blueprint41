#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable CS8981 // Names should not be lower type only

using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Events;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Datastore.Query;
using node = Datastore.Query.Node;

namespace Datastore.Manipulation
{
    /// <summary>
    /// Relationship: (Person)-[WATCHED_MOVIE]->(Movie)
    /// </summary>
    public partial class WATCHED_MOVIE
    {
        private WATCHED_MOVIE(string elementId, Person @in, Movie @out, Dictionary<string, object> properties)
        {
            _elementId = elementId;
            
            Person = @in;
            Movie = @out;
            
            CreationDate = (System.DateTime?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime?), properties.GetValue("CreationDate"));
            MinutesWatched = (int)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(int), properties.GetValue("MinutesWatched"));
        }

        internal string _elementId { get; private set; }

        /// <summary>
        /// Person (In Node)
        /// </summary>
        public Person Person { get; private set; }

        /// <summary>
        /// Movie (Out Node)
        /// </summary>
        public Movie Movie { get; private set; }

        public System.DateTime? CreationDate { get; private set; }
        public int MinutesWatched { get; private set; }

        public void Assign(JsNotation<int> MinutesWatched = default)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.WATCHED_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == Person.Uid, outAlias.Uid == Movie.Uid, relAlias.ElementId == _elementId)
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.WATCHED_MOVIE_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
                if (MinutesWatched.HasValue) assignments.Add(new Assignment(alias.MinutesWatched, MinutesWatched));
               
                return assignments.ToArray();
            }
        }
        public static List<WATCHED_MOVIE> Where(Func<Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.WATCHED_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<WATCHED_MOVIE> Where(Func<Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.WATCHED_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))

                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<WATCHED_MOVIE> Where(JsNotation<System.DateTime?> CreationDate = default, JsNotation<int> MinutesWatched = default, JsNotation<Person> InNode = default, JsNotation<Movie> OutNode = default)
        {
            return Where(delegate(Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (MinutesWatched.HasValue) conditions.Add(alias.MinutesWatched == MinutesWatched.Value);
                if (InNode.HasValue) conditions.Add(alias.Person(InNode.Value));
                if (OutNode.HasValue) conditions.Add(alias.Movie(OutNode.Value));

                return conditions.ToArray();
            });
        }
        internal static List<WATCHED_MOVIE> Load(ICompiled query) => Load(query, null);
        internal static List<WATCHED_MOVIE> Load(ICompiled query, params (string name, object value)[] arguments)
        {
            var context = query.GetExecutionContext();
            if (arguments is not null && arguments.Length > 0)
            {
                foreach ((string name, object value) in arguments)
                    context.SetParameter(name, value);
            }

            var results = context.Execute(NodeMapping.AsWritableEntity);

            return results.Select(result => new WATCHED_MOVIE(
                result.elementId,
                result.@in,
                result.@out,
                result.properties
            )).ToList();
        }

        public static Relationship Relationship => ThreadSafe.LazyInit(ref _relationship, () => Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["WATCHED_MOVIE"]);
        private static Relationship _relationship = null;

        /// <summary>
        /// CRUD Specific alias for relationship: (Person)-[WATCHED_MOVIE]->(Movie)
        /// </summary>
        public partial class Alias
        {
            internal Alias(q.WATCHED_MOVIE_ALIAS relAlias, q.PersonAlias inAlias, q.MovieAlias outAlias)
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
            public NumericResult MinutesWatched
            {
                get
                {
                    if (_minutesWatched is null)
                        _minutesWatched = _relAlias.MinutesWatched;

                    return _minutesWatched;
                }
            }
            private NumericResult _minutesWatched = null;

            /// <summary>
            /// Person in-node: (Person)-[WATCHED_MOVIE]->(Movie)
            /// </summary>
            /// <returns>
            /// Condition where in-node is the given person
            /// </returns>
            public QueryCondition Person(Person person)
            {
                return _inAlias.Uid == person.Uid;
            }
            /// <summary>
            /// Person in-node: (Person)-[WATCHED_MOVIE]->(Movie)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of persons
            /// </returns>
            public QueryCondition Persons(IEnumerable<Person> persons)
            {
                return _inAlias.Uid.In(persons.Select(item => item.Uid));
            }
            /// <summary>
            /// Person in-node: (Person)-[WATCHED_MOVIE]->(Movie)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of persons
            /// </returns>
            public QueryCondition Persons(params Person[] persons)
            {
                return _inAlias.Uid.In(persons.Select(item => item.Uid));
            }

            /// <summary>
            /// Movie out-node: (Person)-[WATCHED_MOVIE]->(Movie)
            /// </summary>
            /// <returns>
            /// Condition where out-node is the given movie
            /// </returns>
            public QueryCondition Movie(Movie movie)
            {
                return _outAlias.Uid == movie.Uid;
            }
            /// <summary>
            /// Movie out-node: (Person)-[WATCHED_MOVIE]->(Movie)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of movies
            /// </returns>
            public QueryCondition Movies(IEnumerable<Movie> movies)
            {
                return _outAlias.Uid.In(movies.Select(item => item.Uid));
            }
            /// <summary>
            /// Movie out-node: (Person)-[WATCHED_MOVIE]->(Movie)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of movies
            /// </returns>
            public QueryCondition Movies(params Movie[] movies)
            {
                return _outAlias.Uid.In(movies.Select(item => item.Uid));
            }

            private readonly q.WATCHED_MOVIE_ALIAS _relAlias;
            private readonly q.PersonAlias _inAlias;
            private readonly q.MovieAlias _outAlias;
        }
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<WATCHED_MOVIE> @this, JsNotation<int> MinutesWatched = default)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.WATCHED_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(relAlias.ElementId.In(@this.Select(item => item._elementId)))
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.WATCHED_MOVIE_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
                if (MinutesWatched.HasValue) assignments.Add(new Assignment(alias.MinutesWatched, MinutesWatched));
               
                return assignments.ToArray();
            }
        }
    }
}
