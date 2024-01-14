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
            
            CreationDate = (System.DateTime)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime), properties.GetValue("CreationDate"));
            MonthlyFee = (decimal)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(decimal), properties.GetValue("MonthlyFee"));
            StartDate = (System.DateTime)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime), properties.GetValue("StartDate"));
            EndDate = (System.DateTime)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime), properties.GetValue("EndDate"));
        }

        private string _elementId { get; set; }

        /// <summary>
        /// Person (In Node)
        /// </summary>
        public Person Person { get; private set; }

        /// <summary>
        /// Restaurant (Out Node)
        /// </summary>
        public StreamingService StreamingService { get; private set; }

        public System.DateTime CreationDate { get; private set; }
        public decimal MonthlyFee { get; private set; }
        public System.DateTime StartDate { get; private set; }
        public System.DateTime EndDate { get; private set; }

        public void Assign(JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<decimal> MonthlyFee = default, JsNotation<System.DateTime> StartDate = default)
        {
            throw new NotImplementedException();
        }
        public static List<SUBSCRIBED_TO_STREAMING_SERVICE> Where(Func<(q.PersonAlias In, q.SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS Rel, q.StreamingServiceAlias Out), QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.SUBSCRIBED_TO_STREAMING_SERVICE.Alias(out var relAlias).Out.StreamingService.Alias(out var outAlias))
                .Where(expression.Invoke((inAlias, relAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<SUBSCRIBED_TO_STREAMING_SERVICE> Where(Func<(q.PersonAlias In, q.SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS Rel, q.StreamingServiceAlias Out), QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.SUBSCRIBED_TO_STREAMING_SERVICE.Alias(out var relAlias).Out.StreamingService.Alias(out var outAlias))
                .Where(expression.Invoke((inAlias, relAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<SUBSCRIBED_TO_STREAMING_SERVICE> Where(JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<decimal> MonthlyFee = default, JsNotation<System.DateTime> StartDate = default, JsNotation<Person> InNode = default, JsNotation<Restaurant> OutNode = default)
        {
            throw new NotImplementedException();
        }
        private static List<SUBSCRIBED_TO_STREAMING_SERVICE> Load(ICompiled query)
        {
            var context = query.GetExecutionContext();
            var results = context.Execute(NodeMapping.AsWritableEntity);

            return results.Select(result => new SUBSCRIBED_TO_STREAMING_SERVICE(
                result.elementId,
                result.@in,
                result.@out,
                result.properties
            )).ToList();
        }

        public static Relationship Relationship => Threadsafe.LazyInit(ref _relationship, () => Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["SUBSCRIBED_TO_STREAMING_SERVICE"]);
        private static Relationship _relationship = null;
    }

    /// <summary>
    /// Alias for relationship: (Person)-[SUBSCRIBED_TO_STREAMING_SERVICE]->(StreamingService)
    /// </summary>
    public partial class SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS
    {
        internal SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS(OGM entity, DirectionEnum direction)
        {
        }
        internal SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS(IEnumerable<OGM> entity, DirectionEnum direction)
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
        public NumericResult MonthlyFee
        {
            get
            {
                if (_monthlyFee is null)
                    _monthlyFee = _alias.MonthlyFee;

                return _monthlyFee;
            }
        }
        private NumericResult _monthlyFee = null;
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
        /// Person in-node: (Person)-[SUBSCRIBED_TO_STREAMING_SERVICE]->(StreamingService)
        /// </summary>
        /// <returns>
        /// Condition where in-node is the given person
        /// </returns>
        public QueryCondition Person(Person person)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Person in-node: (Person)-[SUBSCRIBED_TO_STREAMING_SERVICE]->(StreamingService)
        /// </summary>
        /// <returns>
        /// Condition where in-node is in the given set of persons
        /// </returns>
        public QueryCondition Persons(IEnumerable<Person> person)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// StreamingService out-node: (Person)-[SUBSCRIBED_TO_STREAMING_SERVICE]->(StreamingService)
        /// </summary>
        /// <returns>
        /// Condition where out-node is the given streaming  service
        /// </returns>
        public QueryCondition StreamingService(StreamingService streamingService)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// StreamingService out-node: (Person)-[SUBSCRIBED_TO_STREAMING_SERVICE]->(StreamingService)
        /// </summary>
        /// <returns>
        /// Condition where out-node is in the given set of streaming  services
        /// </returns>
        public QueryCondition StreamingServices(IEnumerable<StreamingService> streamingService)
        {
            throw new NotImplementedException();
        }

        private static readonly q.SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS _alias = new q.SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS(new q.SUBSCRIBED_TO_STREAMING_SERVICE_REL(null, DirectionEnum.None));
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<SUBSCRIBED_TO_STREAMING_SERVICE> @this, JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<decimal> MonthlyFee = default, JsNotation<System.DateTime> StartDate = default)
        {
            throw new NotImplementedException();
        }
    }
}