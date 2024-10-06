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
    /// Relationship: (Restaurant)-[RESTAURANT_LOCATED_AT]->(City)
    /// </summary>
    public partial class RESTAURANT_LOCATED_AT
    {
        private RESTAURANT_LOCATED_AT(string elementId, Restaurant @in, City @out, Dictionary<string, object> properties)
        {
            _elementId = elementId;
            
            Restaurant = @in;
            City = @out;
            
            //CreationDate = (System.DateTime?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime?), properties.GetValue("CreationDate"));
            throw new NotImplementedException();
        }
        internal string _elementId { get; private set; }

        /// <summary>
        /// Restaurant (In Node)
        /// </summary>
        public Restaurant Restaurant { get; private set; }

        /// <summary>
        /// City (Out Node)
        /// </summary>
        public City City { get; private set; }

        public System.DateTime? CreationDate { get; private set; }

        public void Assign()
        {
            var query = Cypher
                .Match(node.Restaurant.Alias(out var inAlias).In.RESTAURANT_LOCATED_AT.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(inAlias.Uid == Restaurant.Uid, outAlias.Uid == City.Uid, relAlias.ElementId == _elementId)
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.RESTAURANT_LOCATED_AT_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
               
                return assignments.ToArray();
            }
        }
        public static List<RESTAURANT_LOCATED_AT> Where(Func<Alias, QueryCondition> expression)
        {
            var query = Cypher
                .Match(node.Restaurant.Alias(out var inAlias).In.RESTAURANT_LOCATED_AT.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<RESTAURANT_LOCATED_AT> Where(Func<Alias, QueryCondition[]> expression)
        {
            var query = Cypher
                .Match(node.Restaurant.Alias(out var inAlias).In.RESTAURANT_LOCATED_AT.Alias(out var relAlias).Out.City.Alias(out var outAlias))

                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<RESTAURANT_LOCATED_AT> Where(JsNotation<System.DateTime?> CreationDate = default, JsNotation<Restaurant> InNode = default, JsNotation<City> OutNode = default)
        {
            return Where(delegate(Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (InNode.HasValue) conditions.Add(alias.Restaurant(InNode.Value));
                if (OutNode.HasValue) conditions.Add(alias.City(OutNode.Value));

                return conditions.ToArray();
            });
        }
        internal static List<RESTAURANT_LOCATED_AT> Load(ICompiled query) => Load(query, null);
        internal static List<RESTAURANT_LOCATED_AT> Load(ICompiled query, params (string name, object value)[] arguments)
        {
            var context = query.GetExecutionContext();
            if (arguments is not null && arguments.Length > 0)
            {
                foreach ((string name, object value) in arguments)
                    context.SetParameter(name, value);
            }

            var results = context.Execute(NodeMapping.AsWritableEntity);

            return results.Select(result => new RESTAURANT_LOCATED_AT(
                result.elementId,
                result.@in,
                result.@out,
                result.properties
            )).ToList();
        }

        public static Relationship Relationship => ThreadSafe.LazyInit(ref _relationship, () => Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["RESTAURANT_LOCATED_AT"]);
        private static Relationship _relationship = null;

        /// <summary>
        /// CRUD Specific alias for relationship: (Restaurant)-[RESTAURANT_LOCATED_AT]->(City)
        /// </summary>
        public partial class Alias
        {
            internal Alias(q.RESTAURANT_LOCATED_AT_ALIAS relAlias, q.RestaurantAlias inAlias, q.CityAlias outAlias)
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
            /// Restaurant in-node: (Restaurant)-[RESTAURANT_LOCATED_AT]->(City)
            /// </summary>
            /// <returns>
            /// Condition where in-node is the given restaurant
            /// </returns>
            public QueryCondition Restaurant(Restaurant restaurant)
            {
                return _inAlias.Uid == restaurant.Uid;
            }
            /// <summary>
            /// Restaurant in-node: (Restaurant)-[RESTAURANT_LOCATED_AT]->(City)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of restaurants
            /// </returns>
            public QueryCondition Restaurants(IEnumerable<Restaurant> restaurants)
            {
                return _inAlias.Uid.In(restaurants.Select(item => item.Uid));
            }
            /// <summary>
            /// Restaurant in-node: (Restaurant)-[RESTAURANT_LOCATED_AT]->(City)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of restaurants
            /// </returns>
            public QueryCondition Restaurants(params Restaurant[] restaurants)
            {
                return _inAlias.Uid.In(restaurants.Select(item => item.Uid));
            }

            /// <summary>
            /// City out-node: (Restaurant)-[RESTAURANT_LOCATED_AT]->(City)
            /// </summary>
            /// <returns>
            /// Condition where out-node is the given city
            /// </returns>
            public QueryCondition City(City city)
            {
                return _outAlias.Uid == city.Uid;
            }
            /// <summary>
            /// City out-node: (Restaurant)-[RESTAURANT_LOCATED_AT]->(City)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of cities
            /// </returns>
            public QueryCondition Cities(IEnumerable<City> cities)
            {
                return _outAlias.Uid.In(cities.Select(item => item.Uid));
            }
            /// <summary>
            /// City out-node: (Restaurant)-[RESTAURANT_LOCATED_AT]->(City)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of cities
            /// </returns>
            public QueryCondition Cities(params City[] cities)
            {
                return _outAlias.Uid.In(cities.Select(item => item.Uid));
            }

            private readonly q.RESTAURANT_LOCATED_AT_ALIAS _relAlias;
            private readonly q.RestaurantAlias _inAlias;
            private readonly q.CityAlias _outAlias;
        }
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<RESTAURANT_LOCATED_AT> @this)
        {
            var query = Cypher
                .Match(node.Restaurant.Alias(out var inAlias).In.RESTAURANT_LOCATED_AT.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(relAlias.ElementId.In(@this.Select(item => item._elementId)))
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.RESTAURANT_LOCATED_AT_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
               
                return assignments.ToArray();
            }
        }
    }
}
