using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Query;

using m = Neo4j.Datastore.Manipulation;

namespace Neo4j.Datastore.Query
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

        public MovieNode Where(JsNotation<int?> released = default, JsNotation<string> tagline = default, JsNotation<string> title = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<MovieAlias> alias = new Lazy<MovieAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (released.HasValue) conditions.Add(new QueryCondition(alias.Value.released, Operator.Equals, ((IValue)released).GetValue()));
            if (tagline.HasValue) conditions.Add(new QueryCondition(alias.Value.tagline, Operator.Equals, ((IValue)tagline).GetValue()));
            if (title.HasValue) conditions.Add(new QueryCondition(alias.Value.title, Operator.Equals, ((IValue)title).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public MovieNode Assign(JsNotation<int?> released = default, JsNotation<string> tagline = default, JsNotation<string> title = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<MovieAlias> alias = new Lazy<MovieAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (released.HasValue) assignments.Add(new Assignment(alias.Value.released, released));
            if (tagline.HasValue) assignments.Add(new Assignment(alias.Value.tagline, tagline));
            if (title.HasValue) assignments.Add(new Assignment(alias.Value.title, title));
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

        public Assignment[] Assign(JsNotation<int?> released = default, JsNotation<string> tagline = default, JsNotation<string> title = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (released.HasValue) assignments.Add(new Assignment(this.released, released));
            if (tagline.HasValue) assignments.Add(new Assignment(this.tagline, tagline));
            if (title.HasValue) assignments.Add(new Assignment(this.title, title));
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
                        { "title", new StringResult(this, "title", Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Movie"], Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Movie"].Properties["title"]) },
                        { "tagline", new StringResult(this, "tagline", Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Movie"], Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Movie"].Properties["tagline"]) },
                        { "released", new NumericResult(this, "released", Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Movie"], Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Movie"].Properties["released"]) },
                        { "Uid", new StringResult(this, "Uid", Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Movie"], Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Movie"].Properties["Uid"]) },
                    };
                }
                return m_AliasFields;
            }
        }
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;


        public StringResult title
        {
            get
            {
                if (m_title is null)
                    m_title = (StringResult)AliasFields["title"];

                return m_title;
            }
        }
        private StringResult m_title = null;
        public StringResult tagline
        {
            get
            {
                if (m_tagline is null)
                    m_tagline = (StringResult)AliasFields["tagline"];

                return m_tagline;
            }
        }
        private StringResult m_tagline = null;
        public NumericResult released
        {
            get
            {
                if (m_released is null)
                    m_released = (NumericResult)AliasFields["released"];

                return m_released;
            }
        }
        private NumericResult m_released = null;
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