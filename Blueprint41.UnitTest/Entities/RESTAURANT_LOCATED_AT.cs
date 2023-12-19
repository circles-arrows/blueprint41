#pragma warning disable S101 // Types should be named in PascalCase

using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Datastore.Query;

namespace Datastore.Manipulation
{
    /// <summary>
    /// Relationship: (Restaurant)-[RESTAURANT_LOCATED_AT]->(City)
    /// </summary>
    public partial class RESTAURANT_LOCATED_AT
    {
        private string _elementId { get; set; }

        /// <summary>
        /// Person (In Node)
        /// </summary>
        public Person Person { get; private set; }

        /// <summary>
        /// Restaurant (Out Node)
        /// </summary>
        public Restaurant Restaurant { get; private set; }

        public System.DateTime CreationDate { get; private set; }
        public System.DateTime StartDate { get; private set; }
        public System.DateTime EndDate { get; private set; }

        public void Assign(JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<System.DateTime> StartDate = default)
        {
            throw new NotImplementedException();
        }
        public static List<RESTAURANT_LOCATED_AT> Where(Func<RESTAURANT_LOCATED_AT_ALIAS, QueryCondition> alias)
        {
            throw new NotImplementedException();
        }
        public static List<RESTAURANT_LOCATED_AT> Where(Func<RESTAURANT_LOCATED_AT_ALIAS, QueryCondition[]> alias)
        {
            throw new NotImplementedException();
        }
        public static List<RESTAURANT_LOCATED_AT> Where(JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<System.DateTime> StartDate = default, JsNotation<Person> InNode = default, JsNotation<Restaurant> OutNode = default)
        {
            throw new NotImplementedException();
        }
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
        public DateTimeResult StartDate
        {
            get
            {
                if (_startDate is null)
                    _startDate = _alias.StartDate;

                return _startDate;
            }
        }
        private DateTimeResult _startDate = null;
        public DateTimeResult EndDate
        {
            get
            {
                if (_endDate is null)
                    _endDate = _alias.EndDate;

                return _endDate;
            }
        }
        private DateTimeResult _endDate = null;

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
        public static void Assign(this IEnumerable<RESTAURANT_LOCATED_AT> @this, JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<System.DateTime> StartDate = default)
        {
            throw new NotImplementedException();
        }
    }
}