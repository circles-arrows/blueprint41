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
    /// Relationship: (Person)-[SUBSCRIBED_TO_STREAMING_SERVICE]->(StreamingService)
    /// </summary>
    public partial class SUBSCRIBED_TO_STREAMING_SERVICE
    {
        private SUBSCRIBED_TO_STREAMING_SERVICE(string elementId, Person @in, StreamingService @out, Dictionary<string, object> properties)
        {
            _elementId = elementId;
            
            Person = @in;
            StreamingService = @out;
            
            CreationDate = (System.DateTime?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime?), properties.GetValue("CreationDate"));
            MonthlyFee = (decimal)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(decimal), properties.GetValue("MonthlyFee"));
            StartDate = (System.DateTime?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime?), properties.GetValue("StartDate"));
            EndDate = (System.DateTime?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime?), properties.GetValue("EndDate"));
        }

        internal string _elementId { get; private set; }

        /// <summary>
        /// Person (In Node)
        /// </summary>
        public Person Person { get; private set; }

        /// <summary>
        /// StreamingService (Out Node)
        /// </summary>
        public StreamingService StreamingService { get; private set; }

        public System.DateTime? CreationDate { get; private set; }
        public decimal MonthlyFee { get; private set; }
        public System.DateTime? StartDate { get; private set; }
        public System.DateTime? EndDate { get; private set; }

        public void Assign(JsNotation<decimal> MonthlyFee = default)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.SUBSCRIBED_TO_STREAMING_SERVICE.Alias(out var relAlias).Out.StreamingService.Alias(out var outAlias))
                .Where(inAlias.Uid == Person.Uid, outAlias.Uid == StreamingService.Uid, relAlias.ElementId == _elementId)
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
                if (MonthlyFee.HasValue) assignments.Add(new Assignment(alias.MonthlyFee, MonthlyFee));
               
                return assignments.ToArray();
            }
        }
        public static List<SUBSCRIBED_TO_STREAMING_SERVICE> Where(Func<Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.SUBSCRIBED_TO_STREAMING_SERVICE.Alias(out var relAlias).Out.StreamingService.Alias(out var outAlias))
                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<SUBSCRIBED_TO_STREAMING_SERVICE> Where(Func<Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.SUBSCRIBED_TO_STREAMING_SERVICE.Alias(out var relAlias).Out.StreamingService.Alias(out var outAlias))

                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<SUBSCRIBED_TO_STREAMING_SERVICE> Where(JsNotation<System.DateTime?> CreationDate = default, JsNotation<System.DateTime?> EndDate = default, JsNotation<decimal> MonthlyFee = default, JsNotation<System.DateTime?> StartDate = default, JsNotation<Person> InNode = default, JsNotation<StreamingService> OutNode = default)
        {
            return Where(delegate(Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (MonthlyFee.HasValue) conditions.Add(alias.MonthlyFee == MonthlyFee.Value);
                if (StartDate.HasValue) conditions.Add(alias.StartDate == StartDate.Value);
                if (EndDate.HasValue) conditions.Add(alias.EndDate == EndDate.Value);
                if (InNode.HasValue) conditions.Add(alias.Person(InNode.Value));
                if (OutNode.HasValue) conditions.Add(alias.StreamingService(OutNode.Value));

                return conditions.ToArray();
            });
        }
        internal static List<SUBSCRIBED_TO_STREAMING_SERVICE> Load(ICompiled query) => Load(query, null);
        internal static List<SUBSCRIBED_TO_STREAMING_SERVICE> Load(ICompiled query, params (string name, object value)[] arguments)
        {
            var context = query.GetExecutionContext();
            if (arguments is not null && arguments.Length > 0)
            {
                foreach ((string name, object value) in arguments)
                    context.SetParameter(name, value);
            }

            var results = context.Execute(NodeMapping.AsWritableEntity);

            return results.Select(result => new SUBSCRIBED_TO_STREAMING_SERVICE(
                result.elementId,
                result.@in,
                result.@out,
                result.properties
            )).ToList();
        }

        public static Relationship Relationship => ThreadSafe.LazyInit(ref _relationship, () => Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["SUBSCRIBED_TO_STREAMING_SERVICE"]);
        private static Relationship _relationship = null;

        /// <summary>
        /// CRUD Specific alias for relationship: (Person)-[SUBSCRIBED_TO_STREAMING_SERVICE]->(StreamingService)
        /// </summary>
        public partial class Alias
        {
            internal Alias(q.SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS relAlias, q.PersonAlias inAlias, q.StreamingServiceAlias outAlias)
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
            public NumericResult MonthlyFee
            {
                get
                {
                    if (_monthlyFee is null)
                        _monthlyFee = _relAlias.MonthlyFee;

                    return _monthlyFee;
                }
            }
            private NumericResult _monthlyFee = null;
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

            /// <summary>
            /// Person in-node: (Person)-[SUBSCRIBED_TO_STREAMING_SERVICE]->(StreamingService)
            /// </summary>
            /// <returns>
            /// Condition where in-node is the given person
            /// </returns>
            public QueryCondition Person(Person person)
            {
                return _inAlias.Uid == person.Uid;
            }
            /// <summary>
            /// Person in-node: (Person)-[SUBSCRIBED_TO_STREAMING_SERVICE]->(StreamingService)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of persons
            /// </returns>
            public QueryCondition Persons(IEnumerable<Person> persons)
            {
                return _inAlias.Uid.In(persons.Select(item => item.Uid));
            }
            /// <summary>
            /// Person in-node: (Person)-[SUBSCRIBED_TO_STREAMING_SERVICE]->(StreamingService)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of persons
            /// </returns>
            public QueryCondition Persons(params Person[] persons)
            {
                return _inAlias.Uid.In(persons.Select(item => item.Uid));
            }

            /// <summary>
            /// StreamingService out-node: (Person)-[SUBSCRIBED_TO_STREAMING_SERVICE]->(StreamingService)
            /// </summary>
            /// <returns>
            /// Condition where out-node is the given streaming  service
            /// </returns>
            public QueryCondition StreamingService(StreamingService streamingService)
            {
                return _outAlias.Uid == streamingService.Uid;
            }
            /// <summary>
            /// StreamingService out-node: (Person)-[SUBSCRIBED_TO_STREAMING_SERVICE]->(StreamingService)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of streaming  services
            /// </returns>
            public QueryCondition StreamingServices(IEnumerable<StreamingService> streamingServices)
            {
                return _outAlias.Uid.In(streamingServices.Select(item => item.Uid));
            }
            /// <summary>
            /// StreamingService out-node: (Person)-[SUBSCRIBED_TO_STREAMING_SERVICE]->(StreamingService)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of streaming  services
            /// </returns>
            public QueryCondition StreamingServices(params StreamingService[] streamingServices)
            {
                return _outAlias.Uid.In(streamingServices.Select(item => item.Uid));
            }

            public QueryCondition[] Moment(DateTime? moment)      => _relAlias.Moment(moment);
            public QueryCondition[] Moment(DateTimeResult moment) => _relAlias.Moment(moment);
            public QueryCondition[] Moment(Parameter moment)      => _relAlias.Moment(moment);

            private readonly q.SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS _relAlias;
            private readonly q.PersonAlias _inAlias;
            private readonly q.StreamingServiceAlias _outAlias;
        }
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<SUBSCRIBED_TO_STREAMING_SERVICE> @this, JsNotation<decimal> MonthlyFee = default)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.SUBSCRIBED_TO_STREAMING_SERVICE.Alias(out var relAlias).Out.StreamingService.Alias(out var outAlias))
                .Where(relAlias.ElementId.In(@this.Select(item => item._elementId)))
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
                if (MonthlyFee.HasValue) assignments.Add(new Assignment(alias.MonthlyFee, MonthlyFee));
               
                return assignments.ToArray();
            }
        }
    }
}