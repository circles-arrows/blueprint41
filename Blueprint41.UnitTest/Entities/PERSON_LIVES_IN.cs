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
    /// Relationship: (Person)-[PERSON_LIVES_IN]->(City)
    /// </summary>
    public partial class PERSON_LIVES_IN
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
        public System.DateTime From { get; private set; }
        public int? HouseNr { get; private set; }
        public string Street { get; private set; }
        public System.DateTime Till { get; private set; }

        public void Assign(JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> From = default, JsNotation<int?> HouseNr = default, JsNotation<string> Street = default, JsNotation<System.DateTime> Till = default)
        {
            throw new NotImplementedException();
        }
        public static List<PERSON_LIVES_IN> Where(Func<PERSON_LIVES_IN_ALIAS, QueryCondition> alias)
        {
            throw new NotImplementedException();
        }
        public static List<PERSON_LIVES_IN> Where(Func<PERSON_LIVES_IN_ALIAS, QueryCondition[]> alias)
        {
            throw new NotImplementedException();
        }
        public static List<PERSON_LIVES_IN> Where(JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> From = default, JsNotation<int?> HouseNr = default, JsNotation<string> Street = default, JsNotation<System.DateTime> Till = default, JsNotation<Person> InNode = default, JsNotation<Restaurant> OutNode = default)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Alias for relationship: (Person)-[PERSON_LIVES_IN]->(City)
    /// </summary>
    public partial class PERSON_LIVES_IN_ALIAS
    {
        internal PERSON_LIVES_IN_ALIAS(OGM entity, DirectionEnum direction)
        {
        }
        internal PERSON_LIVES_IN_ALIAS(IEnumerable<OGM> entity, DirectionEnum direction)
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
        public DateTimeResult From
        {
            get
            {
                if (_from is null)
                    _from = _alias.From;

                return _from;
            }
        }
        private DateTimeResult _from = null;
        public NumericResult HouseNr
        {
            get
            {
                if (_houseNr is null)
                    _houseNr = _alias.HouseNr;

                return _houseNr;
            }
        }
        private NumericResult _houseNr = null;
        public StringResult Street
        {
            get
            {
                if (_street is null)
                    _street = _alias.Street;

                return _street;
            }
        }
        private StringResult _street = null;
        public DateTimeResult Till
        {
            get
            {
                if (_till is null)
                    _till = _alias.Till;

                return _till;
            }
        }
        private DateTimeResult _till = null;

        /// <summary>
        /// Person in-node: (Person)-[PERSON_LIVES_IN]->(City)
        /// </summary>
        /// <returns>
        /// Condition where in-node is the given person
        /// </returns>
        public QueryCondition Person(Person person)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Person in-node: (Person)-[PERSON_LIVES_IN]->(City)
        /// </summary>
        /// <returns>
        /// Condition where in-node is in the given set of persons
        /// </returns>
        public QueryCondition Persons(IEnumerable<Person> person)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// City out-node: (Person)-[PERSON_LIVES_IN]->(City)
        /// </summary>
        /// <returns>
        /// Condition where out-node is the given city
        /// </returns>
        public QueryCondition City(City city)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// City out-node: (Person)-[PERSON_LIVES_IN]->(City)
        /// </summary>
        /// <returns>
        /// Condition where out-node is in the given set of cities
        /// </returns>
        public QueryCondition Cities(IEnumerable<City> city)
        {
            throw new NotImplementedException();
        }

        private static readonly q.PERSON_LIVES_IN_ALIAS _alias = new q.PERSON_LIVES_IN_ALIAS(new q.PERSON_LIVES_IN_REL(null, DirectionEnum.None));
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<PERSON_LIVES_IN> @this, JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> From = default, JsNotation<int?> HouseNr = default, JsNotation<string> Street = default, JsNotation<System.DateTime> Till = default)
        {
            throw new NotImplementedException();
        }
    }
}