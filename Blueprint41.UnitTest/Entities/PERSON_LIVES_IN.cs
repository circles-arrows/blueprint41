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
    /// Relationship: (Person)-[PERSON_LIVES_IN]->(City)
    /// </summary>
    public partial class PERSON_LIVES_IN
    {
        private PERSON_LIVES_IN(string elementId, Person @in, City @out, Dictionary<string, object> properties)
        {
            _elementId = elementId;
            
            Person = @in;
            City = @out;
            
            CreationDate = (System.DateTime)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime), properties.GetValue("CreationDate"));
            StartDate = (System.DateTime)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime), properties.GetValue("StartDate"));
            EndDate = (System.DateTime)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime), properties.GetValue("EndDate"));
            AddressLine1 = (string)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(string), properties.GetValue("AddressLine1"));
            AddressLine2 = (string)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(string), properties.GetValue("AddressLine2"));
            AddressLine3 = (string)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(string), properties.GetValue("AddressLine3"));
        }

        private string _elementId { get; set; }

        /// <summary>
        /// Person (In Node)
        /// </summary>
        public Person Person { get; private set; }

        /// <summary>
        /// Restaurant (Out Node)
        /// </summary>
        public City City { get; private set; }

        public System.DateTime CreationDate { get; private set; }
        public System.DateTime StartDate { get; private set; }
        public System.DateTime EndDate { get; private set; }
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public string AddressLine3 { get; private set; }

        public void Assign(JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> AddressLine3 = default, JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<System.DateTime> StartDate = default)
        {
            throw new NotImplementedException();
        }
        public static List<PERSON_LIVES_IN> Where(Func<(q.PersonAlias In, q.PERSON_LIVES_IN_ALIAS Rel, q.CityAlias Out), QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.PERSON_LIVES_IN.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(expression.Invoke((inAlias, relAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<PERSON_LIVES_IN> Where(Func<(q.PersonAlias In, q.PERSON_LIVES_IN_ALIAS Rel, q.CityAlias Out), QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.PERSON_LIVES_IN.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(expression.Invoke((inAlias, relAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<PERSON_LIVES_IN> Where(JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> AddressLine3 = default, JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<System.DateTime> StartDate = default, JsNotation<Person> InNode = default, JsNotation<Restaurant> OutNode = default)
        {
            throw new NotImplementedException();
        }
        private static List<PERSON_LIVES_IN> Load(ICompiled query)
        {
            var context = query.GetExecutionContext();
            var results = context.Execute(NodeMapping.AsWritableEntity);

            return results.Select(result => new PERSON_LIVES_IN(
                result.elementId,
                result.@in,
                result.@out,
                result.properties
            )).ToList();
        }

        public static Relationship Relationship => Threadsafe.LazyInit(ref _relationship, () => Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_LIVES_IN"]);
        private static Relationship _relationship = null;
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
        public static void Assign(this IEnumerable<PERSON_LIVES_IN> @this, JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> AddressLine3 = default, JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<System.DateTime> StartDate = default)
        {
            throw new NotImplementedException();
        }
    }
}