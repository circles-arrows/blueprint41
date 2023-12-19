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
    /// Relationship: (Person)-[PERSON_EATS_AT]->(Restaurant)
    /// </summary>
    public partial class PERSON_EATS_AT
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
        public PERSON_EATS_AT.ScoreEnum? Score { get; private set; }

        public void Assign(JsNotation<System.DateTime> CreationDate = default, JsNotation<string> Score = default)
        {
            throw new NotImplementedException();
        }
        public static List<PERSON_EATS_AT> Where(Func<PERSON_EATS_AT_ALIAS, QueryCondition> alias)
        {
            throw new NotImplementedException();
        }
        public static List<PERSON_EATS_AT> Where(Func<PERSON_EATS_AT_ALIAS, QueryCondition[]> alias)
        {
            throw new NotImplementedException();
        }
        public static List<PERSON_EATS_AT> Where(JsNotation<System.DateTime> CreationDate = default, JsNotation<string> Score = default, JsNotation<Person> InNode = default, JsNotation<Restaurant> OutNode = default)
        {
            throw new NotImplementedException();
        }

        #region Enumerations

        public enum ScoreEnum
        {
            Good = 0,
            Average = 1,
            Bad = 2,
        }

        #endregion
    }

    /// <summary>
    /// Alias for relationship: (Person)-[PERSON_EATS_AT]->(Restaurant)
    /// </summary>
    public partial class PERSON_EATS_AT_ALIAS
    {
        internal PERSON_EATS_AT_ALIAS(OGM entity, DirectionEnum direction)
        {
        }
        internal PERSON_EATS_AT_ALIAS(IEnumerable<OGM> entity, DirectionEnum direction)
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
        public StringResult Score
        {
            get
            {
                if (_score is null)
                    _score = _alias.Score;

                return _score;
            }
        }
        private StringResult _score = null;

        /// <summary>
        /// Person in-node: (Person)-[PERSON_EATS_AT]->(Restaurant)
        /// </summary>
        /// <returns>
        /// Condition where in-node is the given person
        /// </returns>
        public QueryCondition Person(Person person)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Person in-node: (Person)-[PERSON_EATS_AT]->(Restaurant)
        /// </summary>
        /// <returns>
        /// Condition where in-node is in the given set of persons
        /// </returns>
        public QueryCondition Persons(IEnumerable<Person> person)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Restaurant out-node: (Person)-[PERSON_EATS_AT]->(Restaurant)
        /// </summary>
        /// <returns>
        /// Condition where out-node is the given restaurant
        /// </returns>
        public QueryCondition Restaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Restaurant out-node: (Person)-[PERSON_EATS_AT]->(Restaurant)
        /// </summary>
        /// <returns>
        /// Condition where out-node is in the given set of restaurants
        /// </returns>
        public QueryCondition Restaurants(IEnumerable<Restaurant> restaurant)
        {
            throw new NotImplementedException();
        }

        private static readonly q.PERSON_EATS_AT_ALIAS _alias = new q.PERSON_EATS_AT_ALIAS(new q.PERSON_EATS_AT_REL(null, DirectionEnum.None));
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<PERSON_EATS_AT> @this, JsNotation<System.DateTime> CreationDate = default, JsNotation<string> Score = default)
        {
            throw new NotImplementedException();
        }
    }
}