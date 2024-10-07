#nullable disable
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable CS8981 // Names should not be lower type only

using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Events;
using Blueprint41.Query;
using Blueprint41.Persistence;
using Blueprint41.DatastoreTemplates;
using q = Datastore.Query;
using node = Datastore.Query.Node;

namespace Datastore.Manipulation
{
    /// <summary>
    /// Relationship: (Person)-[PERSON_EATS_AT]->(Restaurant)
    /// </summary>
    public partial class PERSON_EATS_AT
    {
        private PERSON_EATS_AT(string elementId, Person @in, Restaurant @out, Dictionary<string, object> properties)
        {
            _elementId = elementId;
            
            Person = @in;
            Restaurant = @out;
            
            //CreationDate = (System.DateTime?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime?), properties.GetValue("CreationDate"));
            throw new NotImplementedException();
        }
        internal string _elementId { get; private set; }

        /// <summary>
        /// Person (In Node)
        /// </summary>
        public Person Person { get; private set; }

        /// <summary>
        /// Restaurant (Out Node)
        /// </summary>
        public Restaurant Restaurant { get; private set; }

        public System.DateTime? CreationDate { get; private set; }

        public void Assign()
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_EATS_AT.Alias(out var relAlias).Out.Restaurant.Alias(out var outAlias))
                .Where(inAlias.Uid == Person.Uid, outAlias.Uid == Restaurant.Uid, relAlias.ElementId == _elementId)
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.PERSON_EATS_AT_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
               
                return assignments.ToArray();
            }
        }
        public static List<PERSON_EATS_AT> Where(Func<Alias, QueryCondition> expression)
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_EATS_AT.Alias(out var relAlias).Out.Restaurant.Alias(out var outAlias))
                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<PERSON_EATS_AT> Where(Func<Alias, QueryCondition[]> expression)
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_EATS_AT.Alias(out var relAlias).Out.Restaurant.Alias(out var outAlias))

                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<PERSON_EATS_AT> Where(JsNotation<System.DateTime?> CreationDate = default, JsNotation<Person> InNode = default, JsNotation<Restaurant> OutNode = default)
        {
            return Where(delegate(Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (InNode.HasValue) conditions.Add(alias.Person(InNode.Value));
                if (OutNode.HasValue) conditions.Add(alias.Restaurant(OutNode.Value));

                return conditions.ToArray();
            });
        }
        internal static List<PERSON_EATS_AT> Load(ICompiled query) => Load(query, null);
        internal static List<PERSON_EATS_AT> Load(ICompiled query, params (string name, object value)[] arguments)
        {
            var context = query.GetExecutionContext();
            if (arguments is not null && arguments.Length > 0)
            {
                foreach ((string name, object value) in arguments)
                    context.SetParameter(name, value);
            }

            var results = context.Execute(NodeMapping.AsWritableEntity);

            return results.Select(result => new PERSON_EATS_AT(
                result.elementId,
                result.@in,
                result.@out,
                result.properties
            )).ToList();
        }

        public static Relationship Relationship => ThreadSafe.LazyInit(ref _relationship, () => Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_EATS_AT"]);
        private static Relationship _relationship = null;

        /// <summary>
        /// CRUD Specific alias for relationship: (Person)-[PERSON_EATS_AT]->(Restaurant)
        /// </summary>
        public partial class Alias
        {
            internal Alias(q.PERSON_EATS_AT_ALIAS relAlias, q.PersonAlias inAlias, q.RestaurantAlias outAlias)
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
            /// Person in-node: (Person)-[PERSON_EATS_AT]->(Restaurant)
            /// </summary>
            /// <returns>
            /// Condition where in-node is the given person
            /// </returns>
            public QueryCondition Person(Person person)
            {
                return _inAlias.Uid == person.Uid;
            }
            /// <summary>
            /// Person in-node: (Person)-[PERSON_EATS_AT]->(Restaurant)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of persons
            /// </returns>
            public QueryCondition Persons(IEnumerable<Person> persons)
            {
                return _inAlias.Uid.In(persons.Select(item => item.Uid));
            }
            /// <summary>
            /// Person in-node: (Person)-[PERSON_EATS_AT]->(Restaurant)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of persons
            /// </returns>
            public QueryCondition Persons(params Person[] persons)
            {
                return _inAlias.Uid.In(persons.Select(item => item.Uid));
            }

            /// <summary>
            /// Restaurant out-node: (Person)-[PERSON_EATS_AT]->(Restaurant)
            /// </summary>
            /// <returns>
            /// Condition where out-node is the given restaurant
            /// </returns>
            public QueryCondition Restaurant(Restaurant restaurant)
            {
                return _outAlias.Uid == restaurant.Uid;
            }
            /// <summary>
            /// Restaurant out-node: (Person)-[PERSON_EATS_AT]->(Restaurant)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of restaurants
            /// </returns>
            public QueryCondition Restaurants(IEnumerable<Restaurant> restaurants)
            {
                return _outAlias.Uid.In(restaurants.Select(item => item.Uid));
            }
            /// <summary>
            /// Restaurant out-node: (Person)-[PERSON_EATS_AT]->(Restaurant)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of restaurants
            /// </returns>
            public QueryCondition Restaurants(params Restaurant[] restaurants)
            {
                return _outAlias.Uid.In(restaurants.Select(item => item.Uid));
            }

            private readonly q.PERSON_EATS_AT_ALIAS _relAlias;
            private readonly q.PersonAlias _inAlias;
            private readonly q.RestaurantAlias _outAlias;
        }
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<PERSON_EATS_AT> @this)
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_EATS_AT.Alias(out var relAlias).Out.Restaurant.Alias(out var outAlias))
                .Where(relAlias.ElementId.In(@this.Select(item => item._elementId)))
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.PERSON_EATS_AT_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
               
                return assignments.ToArray();
            }
        }
    }
}
