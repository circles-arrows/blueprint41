#pragma warning disable S101 // Types should be named in PascalCase

using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Neo4j.Datastore.Query;
using node = Neo4j.Datastore.Query.Node;

namespace Neo4j.Datastore.Manipulation
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
            
            CreationDate = (System.DateTime?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime?), properties.GetValue("CreationDate"));
            StartDate = (System.DateTime?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime?), properties.GetValue("StartDate"));
            EndDate = (System.DateTime?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime?), properties.GetValue("EndDate"));
            AddressLine1 = (string)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(string), properties.GetValue("AddressLine1"));
            AddressLine2 = (string)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(string), properties.GetValue("AddressLine2"));
            AddressLine3 = (string)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(string), properties.GetValue("AddressLine3"));
        }

        internal string _elementId { get; private set; }

        /// <summary>
        /// Person (In Node)
        /// </summary>
        public Person Person { get; private set; }

        /// <summary>
        /// City (Out Node)
        /// </summary>
        public City City { get; private set; }

        public System.DateTime? CreationDate { get; private set; }
        public System.DateTime? StartDate { get; private set; }
        public System.DateTime? EndDate { get; private set; }
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public string AddressLine3 { get; private set; }

        public void Assign(JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> AddressLine3 = default)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.PERSON_LIVES_IN.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(inAlias.Uid == Person.Uid, outAlias.Uid == City.Uid, relAlias.ElementId == _elementId)
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.PERSON_LIVES_IN_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
                if (AddressLine1.HasValue) assignments.Add(new Assignment(alias.AddressLine1, AddressLine1));
                if (AddressLine2.HasValue) assignments.Add(new Assignment(alias.AddressLine2, AddressLine2));
                if (AddressLine3.HasValue) assignments.Add(new Assignment(alias.AddressLine3, AddressLine3));
               
                return assignments.ToArray();
            }
        }
        public static List<PERSON_LIVES_IN> Where(Func<Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.PERSON_LIVES_IN.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<PERSON_LIVES_IN> Where(Func<Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.PERSON_LIVES_IN.Alias(out var relAlias).Out.City.Alias(out var outAlias))

                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<PERSON_LIVES_IN> Where(JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> AddressLine3 = default, JsNotation<System.DateTime?> CreationDate = default, JsNotation<System.DateTime?> EndDate = default, JsNotation<System.DateTime?> StartDate = default, JsNotation<Person> InNode = default, JsNotation<City> OutNode = default)
        {
            return Where(delegate(Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (StartDate.HasValue) conditions.Add(alias.StartDate == StartDate.Value);
                if (EndDate.HasValue) conditions.Add(alias.EndDate == EndDate.Value);
                if (AddressLine1.HasValue) conditions.Add(alias.AddressLine1 == AddressLine1.Value);
                if (AddressLine2.HasValue) conditions.Add(alias.AddressLine2 == AddressLine2.Value);
                if (AddressLine3.HasValue) conditions.Add(alias.AddressLine3 == AddressLine3.Value);
                if (InNode.HasValue) conditions.Add(alias.Person(InNode.Value));
                if (OutNode.HasValue) conditions.Add(alias.City(OutNode.Value));

                return conditions.ToArray();
            });
        }
        internal static List<PERSON_LIVES_IN> Load(ICompiled query) => Load(query, null);
        internal static List<PERSON_LIVES_IN> Load(ICompiled query, params (string name, object value)[] arguments)
        {
            var context = query.GetExecutionContext();
            if (arguments is not null && arguments.Length > 0)
            {
                foreach ((string name, object value) in arguments)
                    context.SetParameter(name, value);
            }

            var results = context.Execute(NodeMapping.AsWritableEntity);

            return results.Select(result => new PERSON_LIVES_IN(
                result.elementId,
                result.@in,
                result.@out,
                result.properties
            )).ToList();
        }

        public static Relationship Relationship => ThreadSafe.LazyInit(ref _relationship, () => Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Relations["PERSON_LIVES_IN"]);
        private static Relationship _relationship = null;

        /// <summary>
        /// CRUD Specific alias for relationship: (Person)-[PERSON_LIVES_IN]->(City)
        /// </summary>
        public partial class Alias
        {
            internal Alias(q.PERSON_LIVES_IN_ALIAS relAlias, q.PersonAlias inAlias, q.CityAlias outAlias)
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
            public DateTimeResult StartDate
            {
                get
                {
                    if (_startDate is null)
                        _startDate = _relAlias.StartDate;

                    return _startDate;
                }
            }
            private DateTimeResult _startDate = null;
            public DateTimeResult EndDate
            {
                get
                {
                    if (_endDate is null)
                        _endDate = _relAlias.EndDate;

                    return _endDate;
                }
            }
            private DateTimeResult _endDate = null;
            public StringResult AddressLine1
            {
                get
                {
                    if (_addressLine1 is null)
                        _addressLine1 = _relAlias.AddressLine1;

                    return _addressLine1;
                }
            }
            private StringResult _addressLine1 = null;
            public StringResult AddressLine2
            {
                get
                {
                    if (_addressLine2 is null)
                        _addressLine2 = _relAlias.AddressLine2;

                    return _addressLine2;
                }
            }
            private StringResult _addressLine2 = null;
            public StringResult AddressLine3
            {
                get
                {
                    if (_addressLine3 is null)
                        _addressLine3 = _relAlias.AddressLine3;

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
                return _inAlias.Uid == person.Uid;
            }
            /// <summary>
            /// Person in-node: (Person)-[PERSON_LIVES_IN]->(City)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of persons
            /// </returns>
            public QueryCondition Persons(IEnumerable<Person> persons)
            {
                return _inAlias.Uid.In(persons.Select(item => item.Uid));
            }
            /// <summary>
            /// Person in-node: (Person)-[PERSON_LIVES_IN]->(City)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of persons
            /// </returns>
            public QueryCondition Persons(params Person[] persons)
            {
                return _inAlias.Uid.In(persons.Select(item => item.Uid));
            }

            /// <summary>
            /// City out-node: (Person)-[PERSON_LIVES_IN]->(City)
            /// </summary>
            /// <returns>
            /// Condition where out-node is the given city
            /// </returns>
            public QueryCondition City(City city)
            {
                return _outAlias.Uid == city.Uid;
            }
            /// <summary>
            /// City out-node: (Person)-[PERSON_LIVES_IN]->(City)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of cities
            /// </returns>
            public QueryCondition Cities(IEnumerable<City> cities)
            {
                return _outAlias.Uid.In(cities.Select(item => item.Uid));
            }
            /// <summary>
            /// City out-node: (Person)-[PERSON_LIVES_IN]->(City)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of cities
            /// </returns>
            public QueryCondition Cities(params City[] cities)
            {
                return _outAlias.Uid.In(cities.Select(item => item.Uid));
            }

            public QueryCondition[] Moment(DateTime? moment)      => _relAlias.Moment(moment);
            public QueryCondition[] Moment(DateTimeResult moment) => _relAlias.Moment(moment);
            public QueryCondition[] Moment(Parameter moment)      => _relAlias.Moment(moment);

            private readonly q.PERSON_LIVES_IN_ALIAS _relAlias;
            private readonly q.PersonAlias _inAlias;
            private readonly q.CityAlias _outAlias;
        }
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<PERSON_LIVES_IN> @this, JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> AddressLine3 = default)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.PERSON_LIVES_IN.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(relAlias.ElementId.In(@this.Select(item => item._elementId)))
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.PERSON_LIVES_IN_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
                if (AddressLine1.HasValue) assignments.Add(new Assignment(alias.AddressLine1, AddressLine1));
                if (AddressLine2.HasValue) assignments.Add(new Assignment(alias.AddressLine2, AddressLine2));
                if (AddressLine3.HasValue) assignments.Add(new Assignment(alias.AddressLine3, AddressLine3));
               
                return assignments.ToArray();
            }
        }
    }
}