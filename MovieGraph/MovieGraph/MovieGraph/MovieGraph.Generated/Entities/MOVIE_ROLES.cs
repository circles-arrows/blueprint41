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
    /// Relationship: (Person)-[MOVIE_ROLES]->(MovieRole)
    /// </summary>
    public partial class MOVIE_ROLES
    {
        private MOVIE_ROLES(string elementId, Person @in, MovieRole @out, Dictionary<string, object> properties)
        {
            _elementId = elementId;
            
            Person = @in;
            MovieRole = @out;
            
            CreationDate = (System.DateTime?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime?), properties.GetValue("CreationDate"));
        }

        internal string _elementId { get; private set; }

        /// <summary>
        /// Person (In Node)
        /// </summary>
        public Person Person { get; private set; }

        /// <summary>
        /// MovieRole (Out Node)
        /// </summary>
        public MovieRole MovieRole { get; private set; }

        public System.DateTime? CreationDate { get; private set; }

        public void Assign()
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.MOVIE_ROLES.Alias(out var relAlias).Out.MovieRole.Alias(out var outAlias))
                .Where(inAlias.Uid == Person.Uid, outAlias.Uid == MovieRole.Uid, relAlias.ElementId == _elementId)
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.MOVIE_ROLES_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
               
                return assignments.ToArray();
            }
        }
        public static List<MOVIE_ROLES> Where(Func<Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.MOVIE_ROLES.Alias(out var relAlias).Out.MovieRole.Alias(out var outAlias))
                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<MOVIE_ROLES> Where(Func<Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.MOVIE_ROLES.Alias(out var relAlias).Out.MovieRole.Alias(out var outAlias))

                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<MOVIE_ROLES> Where(JsNotation<System.DateTime?> CreationDate = default, JsNotation<Person> InNode = default, JsNotation<MovieRole> OutNode = default)
        {
            return Where(delegate(Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (InNode.HasValue) conditions.Add(alias.Person(InNode.Value));
                if (OutNode.HasValue) conditions.Add(alias.MovieRole(OutNode.Value));

                return conditions.ToArray();
            });
        }
        internal static List<MOVIE_ROLES> Load(ICompiled query) => Load(query, null);
        internal static List<MOVIE_ROLES> Load(ICompiled query, params (string name, object value)[] arguments)
        {
            var context = query.GetExecutionContext();
            if (arguments is not null && arguments.Length > 0)
            {
                foreach ((string name, object value) in arguments)
                    context.SetParameter(name, value);
            }

            var results = context.Execute(NodeMapping.AsWritableEntity);

            return results.Select(result => new MOVIE_ROLES(
                result.elementId,
                result.@in,
                result.@out,
                result.properties
            )).ToList();
        }

        public static Relationship Relationship => ThreadSafe.LazyInit(ref _relationship, () => MovieGraph.Model.Datastore.Model.Relations["MOVIE_ROLES"]);
        private static Relationship _relationship = null;

        /// <summary>
        /// CRUD Specific alias for relationship: (Person)-[MOVIE_ROLES]->(MovieRole)
        /// </summary>
        public partial class Alias
        {
            internal Alias(q.MOVIE_ROLES_ALIAS relAlias, q.PersonAlias inAlias, q.MovieRoleAlias outAlias)
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
            /// Person in-node: (Person)-[MOVIE_ROLES]->(MovieRole)
            /// </summary>
            /// <returns>
            /// Condition where in-node is the given person
            /// </returns>
            public QueryCondition Person(Person person)
            {
                return _inAlias.Uid == person.Uid;
            }
            /// <summary>
            /// Person in-node: (Person)-[MOVIE_ROLES]->(MovieRole)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of persons
            /// </returns>
            public QueryCondition Persons(IEnumerable<Person> persons)
            {
                return _inAlias.Uid.In(persons.Select(item => item.Uid));
            }
            /// <summary>
            /// Person in-node: (Person)-[MOVIE_ROLES]->(MovieRole)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of persons
            /// </returns>
            public QueryCondition Persons(params Person[] persons)
            {
                return _inAlias.Uid.In(persons.Select(item => item.Uid));
            }

            /// <summary>
            /// MovieRole out-node: (Person)-[MOVIE_ROLES]->(MovieRole)
            /// </summary>
            /// <returns>
            /// Condition where out-node is the given movie  role
            /// </returns>
            public QueryCondition MovieRole(MovieRole movieRole)
            {
                return _outAlias.Uid == movieRole.Uid;
            }
            /// <summary>
            /// MovieRole out-node: (Person)-[MOVIE_ROLES]->(MovieRole)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of movie  roles
            /// </returns>
            public QueryCondition MovieRoles(IEnumerable<MovieRole> movieRoles)
            {
                return _outAlias.Uid.In(movieRoles.Select(item => item.Uid));
            }
            /// <summary>
            /// MovieRole out-node: (Person)-[MOVIE_ROLES]->(MovieRole)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of movie  roles
            /// </returns>
            public QueryCondition MovieRoles(params MovieRole[] movieRoles)
            {
                return _outAlias.Uid.In(movieRoles.Select(item => item.Uid));
            }

            private readonly q.MOVIE_ROLES_ALIAS _relAlias;
            private readonly q.PersonAlias _inAlias;
            private readonly q.MovieRoleAlias _outAlias;
        }
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<MOVIE_ROLES> @this)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.MOVIE_ROLES.Alias(out var relAlias).Out.MovieRole.Alias(out var outAlias))
                .Where(relAlias.ElementId.In(@this.Select(item => item._elementId)))
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.MOVIE_ROLES_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
               
                return assignments.ToArray();
            }
        }
    }
}