using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Query;

using m = Domain.Data.Manipulation;

namespace Domain.Data.Query
{
    public partial class Node
    {
        public static GenreNode Genre { get { return new GenreNode(); } }
    }

    public partial class GenreNode : Blueprint41.Query.Node
    {
        public static implicit operator QueryCondition(GenreNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(GenreNode a)
        {
            return new QueryCondition(a, true);
        } 

        protected override string GetNeo4jLabel()
        {
            return "Genre";
        }

        protected override Entity GetEntity()
        {
            return m.Genre.Entity;
        }
        public FunctionalId FunctionalId
        {
            get
            {
                return m.Genre.Entity.FunctionalId;
            }
        }

        internal GenreNode() { }
        internal GenreNode(GenreAlias alias, bool isReference = false)
        {
            NodeAlias = alias;
            IsReference = isReference;
        }
        internal GenreNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
        internal GenreNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
        {
            NodeAlias = nodeAlias;
        }

        public GenreNode Where(JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<GenreAlias> alias = new Lazy<GenreAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public GenreNode Assign(JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<GenreAlias> alias = new Lazy<GenreAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

        public GenreNode Alias(out GenreAlias alias)
        {
            if (NodeAlias is GenreAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new GenreAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
        public GenreNode Alias(out GenreAlias alias, string name)
        {
            if (NodeAlias is GenreAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new GenreAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

        public GenreNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
            IsReference = true;
            return this;
        }


        public GenreOut Out { get { return new GenreOut(this); } }
        public class GenreOut
        {
            private GenreNode Parent;
            internal GenreOut(GenreNode parent)
            {
                Parent = parent;
            }
            public IFromOut_CONTAINS_GENRE_REL CONTAINS_GENRE { get { return new CONTAINS_GENRE_REL(Parent, DirectionEnum.Out); } }
        }
    }

    public class GenreAlias : AliasResult<GenreAlias, GenreListAlias>
    {
        internal GenreAlias(GenreNode parent)
        {
            Node = parent;
        }
        internal GenreAlias(GenreNode parent, string name)
        {
            Node = parent;
            AliasName = name;
        }
        internal void SetAlias(string name) => AliasName = name;

        private  GenreAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private  GenreAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private  GenreAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
        {
            Node = alias.Node;
        }

        public Assignment[] Assign(JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (Name.HasValue) assignments.Add(new Assignment(this.Name, Name));
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
                        { "Uid", new StringResult(this, "Uid", MovieGraph.Model.Datastore.Model.Entities["Genre"], MovieGraph.Model.Datastore.Model.Entities["Genre"].Properties["Uid"]) },
                        { "Name", new StringResult(this, "Name", MovieGraph.Model.Datastore.Model.Entities["Genre"], MovieGraph.Model.Datastore.Model.Entities["Genre"].Properties["Name"]) },
                    };
                }
                return m_AliasFields;
            }
        }
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public GenreNode.GenreOut Out { get { return new GenreNode.GenreOut(new GenreNode(this, true)); } }

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
        public StringResult Name
        {
            get
            {
                if (m_Name is null)
                    m_Name = (StringResult)AliasFields["Name"];

                return m_Name;
            }
        }
        private StringResult m_Name = null;
        public AsResult As(string aliasName, out GenreAlias alias)
        {
            alias = new GenreAlias((GenreNode)Node)
            {
                AliasName = aliasName
            };
            return this.As(aliasName);
        }
    }

    public class GenreListAlias : ListResult<GenreListAlias, GenreAlias>, IAliasListResult
    {
        private GenreListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private GenreListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private GenreListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
    }
    public class GenreJaggedListAlias : ListResult<GenreJaggedListAlias, GenreListAlias>, IAliasJaggedListResult
    {
        private GenreJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private GenreJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private GenreJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
    }
}