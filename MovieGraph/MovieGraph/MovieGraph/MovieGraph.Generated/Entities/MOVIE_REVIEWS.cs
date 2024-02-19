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
    /// Relationship: (Person)-[MOVIE_REVIEWS]->(MovieReview)
    /// </summary>
    public partial class MOVIE_REVIEWS
    {
        private MOVIE_REVIEWS(string elementId, Person @in, MovieReview @out, Dictionary<string, object> properties)
        {
            _elementId = elementId;
            
            Person = @in;
            MovieReview = @out;
            
            CreationDate = (System.DateTime?)PersistenceProvider.CurrentPersistenceProvider.ConvertFromStoredType(typeof(System.DateTime?), properties.GetValue("CreationDate"));
        }

        internal string _elementId { get; private set; }

        /// <summary>
        /// Person (In Node)
        /// </summary>
        public Person Person { get; private set; }

        /// <summary>
        /// MovieReview (Out Node)
        /// </summary>
        public MovieReview MovieReview { get; private set; }

        public System.DateTime? CreationDate { get; private set; }

        public void Assign()
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.MOVIE_REVIEWS.Alias(out var relAlias).Out.MovieReview.Alias(out var outAlias))
                .Where(inAlias.Uid == Person.Uid, outAlias.Uid == MovieReview.Uid, relAlias.ElementId == _elementId)
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.MOVIE_REVIEWS_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
               
                return assignments.ToArray();
            }
        }
        public static List<MOVIE_REVIEWS> Where(Func<Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.MOVIE_REVIEWS.Alias(out var relAlias).Out.MovieReview.Alias(out var outAlias))
                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<MOVIE_REVIEWS> Where(Func<Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.MOVIE_REVIEWS.Alias(out var relAlias).Out.MovieReview.Alias(out var outAlias))
                .Where(expression.Invoke(new Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return Load(query);
        }
        public static List<MOVIE_REVIEWS> Where(JsNotation<System.DateTime?> CreationDate = default, JsNotation<Person> InNode = default, JsNotation<MovieReview> OutNode = default)
        {
            return Where(delegate(Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (InNode.HasValue) conditions.Add(alias.Person(InNode.Value));
                if (OutNode.HasValue) conditions.Add(alias.MovieReview(OutNode.Value));

                return conditions.ToArray();
            });
        }
        internal static List<MOVIE_REVIEWS> Load(ICompiled query) => Load(query, null);
        internal static List<MOVIE_REVIEWS> Load(ICompiled query, params (string name, object value)[] arguments)
        {
            var context = query.GetExecutionContext();
            if (arguments is not null && arguments.Length > 0)
            {
                foreach ((string name, object value) in arguments)
                    context.SetParameter(name, value);
            }

            var results = context.Execute(NodeMapping.AsWritableEntity);

            return results.Select(result => new MOVIE_REVIEWS(
                result.elementId,
                result.@in,
                result.@out,
                result.properties
            )).ToList();
        }

        public static Relationship Relationship => ThreadSafe.LazyInit(ref _relationship, () => MovieGraph.Model.Datastore.Model.Relations["MOVIE_REVIEWS"]);
        private static Relationship _relationship = null;

        /// <summary>
        /// CRUD Specific alias for relationship: (Person)-[MOVIE_REVIEWS]->(MovieReview)
        /// </summary>
        public partial class Alias
        {
            internal Alias(q.MOVIE_REVIEWS_ALIAS relAlias, q.PersonAlias inAlias, q.MovieReviewAlias outAlias)
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
            /// Person in-node: (Person)-[MOVIE_REVIEWS]->(MovieReview)
            /// </summary>
            /// <returns>
            /// Condition where in-node is the given person
            /// </returns>
            public QueryCondition Person(Person person)
            {
                return _inAlias.Uid == person.Uid;
            }
            /// <summary>
            /// Person in-node: (Person)-[MOVIE_REVIEWS]->(MovieReview)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of persons
            /// </returns>
            public QueryCondition Persons(IEnumerable<Person> persons)
            {
                return _inAlias.Uid.In(persons.Select(item => item.Uid));
            }
            /// <summary>
            /// Person in-node: (Person)-[MOVIE_REVIEWS]->(MovieReview)
            /// </summary>
            /// <returns>
            /// Condition where in-node is in the given set of persons
            /// </returns>
            public QueryCondition Persons(params Person[] persons)
            {
                return _inAlias.Uid.In(persons.Select(item => item.Uid));
            }

            /// <summary>
            /// MovieReview out-node: (Person)-[MOVIE_REVIEWS]->(MovieReview)
            /// </summary>
            /// <returns>
            /// Condition where out-node is the given movie  review
            /// </returns>
            public QueryCondition MovieReview(MovieReview movieReview)
            {
                return _outAlias.Uid == movieReview.Uid;
            }
            /// <summary>
            /// MovieReview out-node: (Person)-[MOVIE_REVIEWS]->(MovieReview)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of movie  reviews
            /// </returns>
            public QueryCondition MovieReviews(IEnumerable<MovieReview> movieReviews)
            {
                return _outAlias.Uid.In(movieReviews.Select(item => item.Uid));
            }
            /// <summary>
            /// MovieReview out-node: (Person)-[MOVIE_REVIEWS]->(MovieReview)
            /// </summary>
            /// <returns>
            /// Condition where out-node is in the given set of movie  reviews
            /// </returns>
            public QueryCondition MovieReviews(params MovieReview[] movieReviews)
            {
                return _outAlias.Uid.In(movieReviews.Select(item => item.Uid));
            }

            private readonly q.MOVIE_REVIEWS_ALIAS _relAlias;
            private readonly q.PersonAlias _inAlias;
            private readonly q.MovieReviewAlias _outAlias;
        }
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<MOVIE_REVIEWS> @this)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.MOVIE_REVIEWS.Alias(out var relAlias).Out.MovieReview.Alias(out var outAlias))
                .Where(relAlias.ElementId.In(@this.Select(item => item._elementId)))
                .Set(GetAssignments(relAlias))
                .Compile();

            var context = query.GetExecutionContext();
            context.Execute();

            Assignment[] GetAssignments(q.MOVIE_REVIEWS_ALIAS alias)
            {
                List<Assignment> assignments = new List<Assignment>();
               
                return assignments.ToArray();
            }
        }
    }
}