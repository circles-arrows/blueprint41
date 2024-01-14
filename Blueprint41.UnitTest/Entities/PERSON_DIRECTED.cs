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
    /// Relationship: (Person)-[PERSON_DIRECTED]->(Movie)
    /// </summary>
    public partial class PERSON_DIRECTED
    {
        private PERSON_DIRECTED(string elementId, Person @in, Movie @out, Dictionary<string, object> properties)
        {
            _elementId = elementId;
            
            Person = @in;
            Movie = @out;
            
            CreationDate = (System.DateTime)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime), properties.GetValue("CreationDate"));
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

        public void Assign(JsNotation<System.DateTime> CreationDate = default)
        {
            throw new NotImplementedException();
        }
        public static List<PERSON_DIRECTED> Where(Func<(q.PersonAlias In, q.PERSON_DIRECTED_ALIAS Rel, q.MovieAlias Out), QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.PERSON_DIRECTED.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(expression.Invoke((inAlias, relAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<PERSON_DIRECTED> Where(Func<(q.PersonAlias In, q.PERSON_DIRECTED_ALIAS Rel, q.MovieAlias Out), QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.PERSON_DIRECTED.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(expression.Invoke((inAlias, relAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<PERSON_DIRECTED> Where(JsNotation<System.DateTime> CreationDate = default, JsNotation<Person> InNode = default, JsNotation<Restaurant> OutNode = default)
        {
            throw new NotImplementedException();
        }
        private static List<PERSON_DIRECTED> Load(ICompiled query)
        {
            var context = query.GetExecutionContext();
            var results = context.Execute(NodeMapping.AsWritableEntity);

            return results.Select(result => new PERSON_DIRECTED(
                result.elementId,
                result.@in,
                result.@out,
                result.properties
            )).ToList();
        }

        public static Relationship Relationship => Threadsafe.LazyInit(ref _relationship, () => Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_DIRECTED"]);
        private static Relationship _relationship = null;
    }

    /// <summary>
    /// Alias for relationship: (Person)-[PERSON_DIRECTED]->(Movie)
    /// </summary>
    public partial class PERSON_DIRECTED_ALIAS
    {
        internal PERSON_DIRECTED_ALIAS(OGM entity, DirectionEnum direction)
        {
        }
        internal PERSON_DIRECTED_ALIAS(IEnumerable<OGM> entity, DirectionEnum direction)
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

        /// <summary>
        /// Person in-node: (Person)-[PERSON_DIRECTED]->(Movie)
        /// </summary>
        /// <returns>
        /// Condition where in-node is the given person
        /// </returns>
        public QueryCondition Person(Person person)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Person in-node: (Person)-[PERSON_DIRECTED]->(Movie)
        /// </summary>
        /// <returns>
        /// Condition where in-node is in the given set of persons
        /// </returns>
        public QueryCondition Persons(IEnumerable<Person> person)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Movie out-node: (Person)-[PERSON_DIRECTED]->(Movie)
        /// </summary>
        /// <returns>
        /// Condition where out-node is the given movie
        /// </returns>
        public QueryCondition Movie(Movie movie)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Movie out-node: (Person)-[PERSON_DIRECTED]->(Movie)
        /// </summary>
        /// <returns>
        /// Condition where out-node is in the given set of movies
        /// </returns>
        public QueryCondition Movies(IEnumerable<Movie> movie)
        {
            throw new NotImplementedException();
        }

        private static readonly q.PERSON_DIRECTED_ALIAS _alias = new q.PERSON_DIRECTED_ALIAS(new q.PERSON_DIRECTED_REL(null, DirectionEnum.None));
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<PERSON_DIRECTED> @this, JsNotation<System.DateTime> CreationDate = default)
        {
            throw new NotImplementedException();
        }
    }
}