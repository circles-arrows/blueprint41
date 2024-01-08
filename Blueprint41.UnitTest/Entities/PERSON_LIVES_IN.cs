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
        public System.DateTime StartDate { get; private set; }
        public System.DateTime EndDate { get; private set; }
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public string AddressLine3 { get; private set; }
        public string ZipCode { get; private set; }

        public void Assign(JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> AddressLine3 = default, JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> ZipCode = default)
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
        public static List<PERSON_LIVES_IN> Where(JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> AddressLine3 = default, JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> ZipCode = default, JsNotation<Person> InNode = default, JsNotation<Restaurant> OutNode = default)
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
        public StringResult AddressLine1
        {
            get
            {
                if (_addressLine1 is null)
                    _addressLine1 = _alias.AddressLine1;

                return _addressLine1;
            }
        }
        private StringResult _addressLine1 = null;
        public StringResult AddressLine2
        {
            get
            {
                if (_addressLine2 is null)
                    _addressLine2 = _alias.AddressLine2;

                return _addressLine2;
            }
        }
        private StringResult _addressLine2 = null;
        public StringResult AddressLine3
        {
            get
            {
                if (_addressLine3 is null)
                    _addressLine3 = _alias.AddressLine3;

                return _addressLine3;
            }
        }
        private StringResult _addressLine3 = null;
        public StringResult ZipCode
        {
            get
            {
                if (_zipCode is null)
                    _zipCode = _alias.ZipCode;

                return _zipCode;
            }
        }
        private StringResult _zipCode = null;

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
        public static void Assign(this IEnumerable<PERSON_LIVES_IN> @this, JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> AddressLine3 = default, JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> ZipCode = default)
        {
            throw new NotImplementedException();
        }
    }
}