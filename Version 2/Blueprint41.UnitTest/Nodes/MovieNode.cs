#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable CS8981 // Names should not be lower type only

using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Query;

using m = Datastore.Manipulation;

namespace Datastore.Query
{
    public partial class Node
    {
        public static MovieNode Movie { get { return new MovieNode(); } }
    }

    public partial class MovieNode : Blueprint41.Query.Node
    {
        public static implicit operator QueryCondition(MovieNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(MovieNode a)
        {
            return new QueryCondition(a, true);
        } 

        protected override string GetNeo4jLabel()
        {
            return "Movie";
        }

        protected override Entity GetEntity()
        {
            return m.Movie.Entity;
        }
        public FunctionalId FunctionalId
        {
            get
            {
                return m.Movie.Entity.FunctionalId;
            }
        }

        internal MovieNode() { }
        internal MovieNode(MovieAlias alias, bool isReference = false)
        {
            NodeAlias = alias;
            IsReference = isReference;
        }
        internal MovieNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
        internal MovieNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
        {
            NodeAlias = nodeAlias;
        }

        public MovieNode Where(JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Title = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<MovieAlias> alias = new Lazy<MovieAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (LastModifiedOn.HasValue) conditions.Add(new QueryCondition(alias.Value.LastModifiedOn, Operator.Equals, ((IValue)LastModifiedOn).GetValue()));
            if (Title.HasValue) conditions.Add(new QueryCondition(alias.Value.Title, Operator.Equals, ((IValue)Title).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public MovieNode Assign(JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Title = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<MovieAlias> alias = new Lazy<MovieAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (LastModifiedOn.HasValue) assignments.Add(new Assignment(alias.Value.LastModifiedOn, LastModifiedOn));
            if (Title.HasValue) assignments.Add(new Assignment(alias.Value.Title, Title));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

        public MovieNode Alias(out MovieAlias alias)
        {
            if (NodeAlias is MovieAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new MovieAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
        public MovieNode Alias(out MovieAlias alias, string name)
        {
            if (NodeAlias is MovieAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new MovieAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

        public MovieNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
            IsReference = true;
            return this;
        }

        public MovieIn  In  { get { return new MovieIn(this); } }
        public class MovieIn
        {
            private MovieNode Parent;
            internal MovieIn(MovieNode parent)
            {
                Parent = parent;
            }
            public IFromIn_MOVIE_CERTIFICATION_REL MOVIE_CERTIFICATION { get { return new MOVIE_CERTIFICATION_REL(Parent, DirectionEnum.In); } }

        }

        public MovieOut Out { get { return new MovieOut(this); } }
        public class MovieOut
        {
            private MovieNode Parent;
            internal MovieOut(MovieNode parent)
            {
                Parent = parent;
            }
            public IFromOut_ACTED_IN_REL ACTED_IN { get { return new ACTED_IN_REL(Parent, DirectionEnum.Out); } }
            public IFromOut_PERSON_DIRECTED_REL PERSON_DIRECTED { get { return new PERSON_DIRECTED_REL(Parent, DirectionEnum.Out); } }
            public IFromOut_WATCHED_MOVIE_REL WATCHED_MOVIE { get { return new WATCHED_MOVIE_REL(Parent, DirectionEnum.Out); } }
        }
    }

    public class MovieAlias : AliasResult<MovieAlias, MovieListAlias>
    {
        internal MovieAlias(MovieNode parent)
        {
            Node = parent;
        }
        internal MovieAlias(MovieNode parent, string name)
        {
            Node = parent;
            AliasName = name;
        }
        internal void SetAlias(string name) => AliasName = name;

        private  MovieAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private  MovieAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private  MovieAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
        {
            Node = alias.Node;
        }

        public Assignment[] Assign(JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Title = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (LastModifiedOn.HasValue) assignments.Add(new Assignment(this.LastModifiedOn, LastModifiedOn));
            if (Title.HasValue) assignments.Add(new Assignment(this.Title, Title));
            if (Uid.HasValue) assignments.Add(new Assignment(this.Uid, Uid));
            
            return assignments.ToArray();
        }


        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields is null)
                {
                    m_AliasFields = new Dictionary<string, FieldResult>()
                    {
                        { "Title", new StringResult(this, "Title", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"].Properties["Title"]) },
                        { "Uid", new StringResult(this, "Uid", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"]) },
                        { "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"]) },
                    };
                }
                return m_AliasFields;
            }
        }
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public MovieNode.MovieIn In { get { return new MovieNode.MovieIn(new MovieNode(this, true)); } }
        public MovieNode.MovieOut Out { get { return new MovieNode.MovieOut(new MovieNode(this, true)); } }

        public StringResult Title
        {
            get
            {
                if (m_Title is null)
                    m_Title = (StringResult)AliasFields["Title"];

                return m_Title;
            }
        }
        private StringResult m_Title = null;
        public StringResult Uid
        {
            get
            {
                if (m_Uid is null)
                    m_Uid = (StringResult)AliasFields["Uid"];

                return m_Uid;
            }
        }
        private StringResult m_Uid = null;
        public DateTimeResult LastModifiedOn
        {
            get
            {
                if (m_LastModifiedOn is null)
                    m_LastModifiedOn = (DateTimeResult)AliasFields["LastModifiedOn"];

                return m_LastModifiedOn;
            }
        }
        private DateTimeResult m_LastModifiedOn = null;
        public AsResult As(string aliasName, out MovieAlias alias)
        {
            alias = new MovieAlias((MovieNode)Node)
            {
                AliasName = aliasName
            };
            return this.As(aliasName);
        }
    }

    public class MovieListAlias : ListResult<MovieListAlias, MovieAlias>, IAliasListResult
    {
        private MovieListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private MovieListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private MovieListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
    }
    public class MovieJaggedListAlias : ListResult<MovieJaggedListAlias, MovieListAlias>, IAliasJaggedListResult
    {
        private MovieJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private MovieJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private MovieJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
    }
}