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
    /// Relationship: (Person)-[WATCHED_MOVIE]->(Movie)
    /// </summary>
    public partial class WATCHED_MOVIE
    {
        private WATCHED_MOVIE(string elementId, Person @in, Movie @out, Dictionary<string, object> properties)
        {
            _elementId = elementId;
            
            Person = @in;
            Movie = @out;
            
            CreationDate = (System.DateTime)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime), properties.GetValue("CreationDate"));
            MinutesWatched = (int)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(int), properties.GetValue("MinutesWatched"));
        }

        private string _elementId { get; set; }

        /// <summary>
        /// Person (In Node)
        /// </summary>
        public Person Person { get; private set; }

        /// <summary>
        /// Restaurant (Out Node)
        /// </summary>
        public Movie Movie { get; private set; }

        public System.DateTime CreationDate { get; private set; }
        public int MinutesWatched { get; private set; }

        public void Assign(JsNotation<System.DateTime> CreationDate = default, JsNotation<int> MinutesWatched = default)
        {
            throw new NotImplementedException();
        }
        public static List<WATCHED_MOVIE> Where(Func<(q.PersonAlias In, q.WATCHED_MOVIE_ALIAS Rel, q.MovieAlias Out), QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.WATCHED_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(expression.Invoke((inAlias, relAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<WATCHED_MOVIE> Where(Func<(q.PersonAlias In, q.WATCHED_MOVIE_ALIAS Rel, q.MovieAlias Out), QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.WATCHED_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(expression.Invoke((inAlias, relAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<WATCHED_MOVIE> Where(JsNotation<System.DateTime> CreationDate = default, JsNotation<int> MinutesWatched = default, JsNotation<Person> InNode = default, JsNotation<Restaurant> OutNode = default)
        {
            throw new NotImplementedException();
        }
        private static List<WATCHED_MOVIE> Load(ICompiled query)
        {
            var context = query.GetExecutionContext();
            var results = context.Execute(NodeMapping.AsWritableEntity);

            return results.Select(result => new WATCHED_MOVIE(
                result.elementId,
                result.@in,
                result.@out,
                result.properties
            )).ToList();
        }

        public static Relationship Relationship => Threadsafe.LazyInit(ref _relationship, () => Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["WATCHED_MOVIE"]);
        private static Relationship _relationship = null;
    }

    /// <summary>
    /// Alias for relationship: (Person)-[WATCHED_MOVIE]->(Movie)
    /// </summary>
    public partial class WATCHED_MOVIE_ALIAS
    {
        internal WATCHED_MOVIE_ALIAS(OGM entity, DirectionEnum direction)
        {
        }
        internal WATCHED_MOVIE_ALIAS(IEnumerable<OGM> entity, DirectionEnum direction)
        {
        }

        public DateTimeResult CreationDate
        {
            get
            {
                if (_creationDate is null)
                    _creationDate = _alias.CreationDate;

                return _creationDate;
            }
        }
        private DateTimeResult _creationDate = null;
        public NumericResult MinutesWatched
        {
            get
            {
                if (_minutesWatched is null)
                    _minutesWatched = _alias.MinutesWatched;

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
            throw new NotImplementedException();
        }
        /// <summary>
        /// Person in-node: (Person)-[WATCHED_MOVIE]->(Movie)
        /// </summary>
        /// <returns>
        /// Condition where in-node is in the given set of persons
        /// </returns>
        public QueryCondition Persons(IEnumerable<Person> person)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Movie out-node: (Person)-[WATCHED_MOVIE]->(Movie)
        /// </summary>
        /// <returns>
        /// Condition where out-node is the given movie
        /// </returns>
        public QueryCondition Movie(Movie movie)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Movie out-node: (Person)-[WATCHED_MOVIE]->(Movie)
        /// </summary>
        /// <returns>
        /// Condition where out-node is in the given set of movies
        /// </returns>
        public QueryCondition Movies(IEnumerable<Movie> movie)
        {
            throw new NotImplementedException();
        }

        private static readonly q.WATCHED_MOVIE_ALIAS _alias = new q.WATCHED_MOVIE_ALIAS(new q.WATCHED_MOVIE_REL(null, DirectionEnum.None));
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<WATCHED_MOVIE> @this, JsNotation<System.DateTime> CreationDate = default, JsNotation<int> MinutesWatched = default)
        {
            throw new NotImplementedException();
        }
    }
}