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
    /// Relationship: (Restaurant)-[RESTAURANT_LOCATED_AT]->(City)
    /// </summary>
    public partial class RESTAURANT_LOCATED_AT
    {
        private RESTAURANT_LOCATED_AT(string elementId, Restaurant @in, City @out, Dictionary<string, object> properties)
        {
            _elementId = elementId;
            
            Restaurant = @in;
            City = @out;
            
            CreationDate = (System.DateTime)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime), properties.GetValue("CreationDate"));
        }

        private string _elementId { get; set; }

        /// <summary>
        /// Person (In Node)
        /// </summary>
        public Restaurant Restaurant { get; private set; }

        /// <summary>
        /// Restaurant (Out Node)
        /// </summary>
        public City City { get; private set; }

        public System.DateTime CreationDate { get; private set; }

        public void Assign(JsNotation<System.DateTime> CreationDate = default)
        {
            throw new NotImplementedException();
        }
        public static List<RESTAURANT_LOCATED_AT> Where(Func<(q.RestaurantAlias In, q.RESTAURANT_LOCATED_AT_ALIAS Rel, q.CityAlias Out), QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Restaurant.Alias(out var inAlias).In.RESTAURANT_LOCATED_AT.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(expression.Invoke((inAlias, relAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<RESTAURANT_LOCATED_AT> Where(Func<(q.RestaurantAlias In, q.RESTAURANT_LOCATED_AT_ALIAS Rel, q.CityAlias Out), QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Restaurant.Alias(out var inAlias).In.RESTAURANT_LOCATED_AT.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(expression.Invoke((inAlias, relAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<RESTAURANT_LOCATED_AT> Where(JsNotation<System.DateTime> CreationDate = default, JsNotation<Person> InNode = default, JsNotation<Restaurant> OutNode = default)
        {
            throw new NotImplementedException();
        }
        private static List<RESTAURANT_LOCATED_AT> Load(ICompiled query)
        {
            var context = query.GetExecutionContext();
            var results = context.Execute(NodeMapping.AsWritableEntity);

            return results.Select(result => new RESTAURANT_LOCATED_AT(
                result.elementId,
                result.@in,
                result.@out,
                result.properties
            )).ToList();
        }

        public static Relationship Relationship => Threadsafe.LazyInit(ref _relationship, () => Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["RESTAURANT_LOCATED_AT"]);
        private static Relationship _relationship = null;
    }

    /// <summary>
    /// Alias for relationship: (Restaurant)-[RESTAURANT_LOCATED_AT]->(City)
    /// </summary>
    public partial class RESTAURANT_LOCATED_AT_ALIAS
    {
        internal RESTAURANT_LOCATED_AT_ALIAS(OGM entity, DirectionEnum direction)
        {
        }
        internal RESTAURANT_LOCATED_AT_ALIAS(IEnumerable<OGM> entity, DirectionEnum direction)
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
        /// Restaurant in-node: (Restaurant)-[RESTAURANT_LOCATED_AT]->(City)
        /// </summary>
        /// <returns>
        /// Condition where in-node is the given restaurant
        /// </returns>
        public QueryCondition Restaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Restaurant in-node: (Restaurant)-[RESTAURANT_LOCATED_AT]->(City)
        /// </summary>
        /// <returns>
        /// Condition where in-node is in the given set of restaurants
        /// </returns>
        public QueryCondition Restaurants(IEnumerable<Restaurant> restaurant)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// City out-node: (Restaurant)-[RESTAURANT_LOCATED_AT]->(City)
        /// </summary>
        /// <returns>
        /// Condition where out-node is the given city
        /// </returns>
        public QueryCondition City(City city)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// City out-node: (Restaurant)-[RESTAURANT_LOCATED_AT]->(City)
        /// </summary>
        /// <returns>
        /// Condition where out-node is in the given set of cities
        /// </returns>
        public QueryCondition Cities(IEnumerable<City> city)
        {
            throw new NotImplementedException();
        }

        private static readonly q.RESTAURANT_LOCATED_AT_ALIAS _alias = new q.RESTAURANT_LOCATED_AT_ALIAS(new q.RESTAURANT_LOCATED_AT_REL(null, DirectionEnum.None));
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<RESTAURANT_LOCATED_AT> @this, JsNotation<System.DateTime> CreationDate = default)
        {
            throw new NotImplementedException();
        }
    }
}