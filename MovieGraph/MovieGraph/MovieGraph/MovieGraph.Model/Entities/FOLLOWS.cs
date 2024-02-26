#pragma warning disable S101 // Types should be named in PascalCase

using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Domain.Data.Query;
using node = Domain.Data.Query.Node;

namespace Domain.Data.Manipulation
{
    /// <summary>
    /// Relationship: (Person)-[FOLLOWS]->(Person)
    /// </summary>
    public partial class FOLLOWS
    {
        private FOLLOWS(string elementId, Person @in, Person @out, Dictionary<string, object> properties)
        {
            _elementId = elementId;
            
            InPerson = @in;
            OutPerson = @out;
            
            CreationDate = (System.DateTime?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime?), properties.GetValue("CreationDate"));
        }

        internal string _elementId { get; private set; }

        /// <summary>
        /// InPerson (In Node)
        /// </summary>
        public Person InPerson { get; private set; }

        /// <summary>
        /// OutPerson (Out Node)
        /// </summary>
        public Person OutPerson { get; private set; }

        public System.DateTime? CreationDate { get; private set; }

        public void Assign()
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.FOLLOWS.Alias(out var relAlias).Out.Person.Alias(out var outAlias))
                .Where(inAlias.Uid == InPerson.Uid, outAlias.Uid == OutPerson.Uid, relAlias.ElementId == _elementId)
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.FOLLOWS_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
               
                return assignments.ToArray();
            }
        }
        public static List<FOLLOWS> Where(Func<Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.FOLLOWS.Alias(out var relAlias).Out.Person.Alias(out var outAlias))
                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<FOLLOWS> Where(Func<Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.FOLLOWS.Alias(out var relAlias).Out.Person.Alias(out var outAlias))

                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<FOLLOWS> Where(JsNotation<System.DateTime?> CreationDate = default, JsNotation<Person> InNode = default, JsNotation<Person> OutNode = default)
        {
            return Where(delegate(Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (InNode.HasValue) conditions.Add(alias.InPerson(InNode.Value));
                if (OutNode.HasValue) conditions.Add(alias.OutPerson(OutNode.Value));

                return conditions.ToArray();
            });
        }
        internal static List<FOLLOWS> Load(ICompiled query) => Load(query, null);
        internal static List<FOLLOWS> Load(ICompiled query, params (string name, object value)[] arguments)
        {
            var context = query.GetExecutionContext();
            if (arguments is not null && arguments.Length > 0)
            {
                foreach ((string name, object value) in arguments)
                    context.SetParameter(name, value);
            }

            var results = context.Execute(NodeMapping.AsWritableEntity);

            return results.Select(result => new FOLLOWS(
                result.elementId,
                result.@in,
                result.@out,
                result.properties
            )).ToList();
        }

        public static Relationship Relationship => ThreadSafe.LazyInit(ref _relationship, () => MovieGraph.Model.Datastore.Model.Relations["FOLLOWS"]);
        private static Relationship _relationship = null;

        /// <summary>
        /// CRUD Specific alias for relationship: (Person)-[FOLLOWS]->(Person)
        /// </summary>
        public partial class Alias
        {
            internal Alias(q.FOLLOWS_ALIAS relAlias, q.PersonAlias inAlias, q.PersonAlias outAlias)
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
            /// InPerson in-node: (Person)-[FOLLOWS]->(Person)
            /// </summary>
            /// <returns>
            /// Condition where in-node is the given person
            /// </returns>
            public QueryCondition InPerson(Person person)
            {
                return _inAlias.Uid == person.Uid;
            }
            /// <summary>
            /// InPerson in-node: (Person)-[FOLLOWS]->(Person)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of persons
            /// </returns>
            public QueryCondition InPersons(IEnumerable<Person> persons)
            {
                return _inAlias.Uid.In(persons.Select(item => item.Uid));
            }
            /// <summary>
            /// InPerson in-node: (Person)-[FOLLOWS]->(Person)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of persons
            /// </returns>
            public QueryCondition InPersons(params Person[] persons)
            {
                return _inAlias.Uid.In(persons.Select(item => item.Uid));
            }

            /// <summary>
            /// OutPerson out-node: (Person)-[FOLLOWS]->(Person)
            /// </summary>
            /// <returns>
            /// Condition where out-node is the given person
            /// </returns>
            public QueryCondition OutPerson(Person person)
            {
                return _outAlias.Uid == person.Uid;
            }
            /// <summary>
            /// OutPerson out-node: (Person)-[FOLLOWS]->(Person)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of persons
            /// </returns>
            public QueryCondition OutPersons(IEnumerable<Person> persons)
            {
                return _outAlias.Uid.In(persons.Select(item => item.Uid));
            }
            /// <summary>
            /// OutPerson out-node: (Person)-[FOLLOWS]->(Person)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of persons
            /// </returns>
            public QueryCondition OutPersons(params Person[] persons)
            {
                return _outAlias.Uid.In(persons.Select(item => item.Uid));
            }

            private readonly q.FOLLOWS_ALIAS _relAlias;
            private readonly q.PersonAlias _inAlias;
            private readonly q.PersonAlias _outAlias;
        }
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<FOLLOWS> @this)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.FOLLOWS.Alias(out var relAlias).Out.Person.Alias(out var outAlias))
                .Where(relAlias.ElementId.In(@this.Select(item => item._elementId)))
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.FOLLOWS_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
               
                return assignments.ToArray();
            }
        }
    }
}