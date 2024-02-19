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
        public static PersonNode Person { get { return new PersonNode(); } }
    }

    public partial class PersonNode : Blueprint41.Query.Node
    {
        public static implicit operator QueryCondition(PersonNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(PersonNode a)
        {
            return new QueryCondition(a, true);
        } 

        protected override string GetNeo4jLabel()
        {
            return "Person";
        }

        protected override Entity GetEntity()
        {
            return m.Person.Entity;
        }
        public FunctionalId FunctionalId
        {
            get
            {
                return m.Person.Entity.FunctionalId;
            }
        }

        internal PersonNode() { }
        internal PersonNode(PersonAlias alias, bool isReference = false)
        {
            NodeAlias = alias;
            IsReference = isReference;
        }
        internal PersonNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
        internal PersonNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
        {
            NodeAlias = nodeAlias;
        }

        public PersonNode Where(JsNotation<int?> born = default, JsNotation<string> name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<PersonAlias> alias = new Lazy<PersonAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (born.HasValue) conditions.Add(new QueryCondition(alias.Value.born, Operator.Equals, ((IValue)born).GetValue()));
            if (name.HasValue) conditions.Add(new QueryCondition(alias.Value.name, Operator.Equals, ((IValue)name).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public PersonNode Assign(JsNotation<int?> born = default, JsNotation<string> name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<PersonAlias> alias = new Lazy<PersonAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (born.HasValue) assignments.Add(new Assignment(alias.Value.born, born));
            if (name.HasValue) assignments.Add(new Assignment(alias.Value.name, name));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

        public PersonNode Alias(out PersonAlias alias)
        {
            if (NodeAlias is PersonAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new PersonAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
        public PersonNode Alias(out PersonAlias alias, string name)
        {
            if (NodeAlias is PersonAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new PersonAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

        public PersonNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
            IsReference = true;
            return this;
        }

        public PersonIn  In  { get { return new PersonIn(this); } }
        public class PersonIn
        {
            private PersonNode Parent;
            internal PersonIn(PersonNode parent)
            {
                Parent = parent;
            }
            public IFromIn_ACTED_IN_REL ACTED_IN { get { return new ACTED_IN_REL(Parent, DirectionEnum.In); } }
            public IFromIn_DIRECTED_REL DIRECTED { get { return new DIRECTED_REL(Parent, DirectionEnum.In); } }
            public IFromIn_FOLLOWS_REL FOLLOWS { get { return new FOLLOWS_REL(Parent, DirectionEnum.In); } }
            public IFromIn_MOVIE_REVIEWS_REL MOVIE_REVIEWS { get { return new MOVIE_REVIEWS_REL(Parent, DirectionEnum.In); } }
            public IFromIn_MOVIE_ROLES_REL MOVIE_ROLES { get { return new MOVIE_ROLES_REL(Parent, DirectionEnum.In); } }
            public IFromIn_PRODUCED_REL PRODUCED { get { return new PRODUCED_REL(Parent, DirectionEnum.In); } }
            public IFromIn_WROTE_REL WROTE { get { return new WROTE_REL(Parent, DirectionEnum.In); } }

        }

        public PersonOut Out { get { return new PersonOut(this); } }
        public class PersonOut
        {
            private PersonNode Parent;
            internal PersonOut(PersonNode parent)
            {
                Parent = parent;
            }
            public IFromOut_FOLLOWS_REL FOLLOWS { get { return new FOLLOWS_REL(Parent, DirectionEnum.Out); } }
        }

        public PersonAny Any { get { return new PersonAny(this); } }
        public class PersonAny
        {
            private PersonNode Parent;
            internal PersonAny(PersonNode parent)
            {
                Parent = parent;
            }
            public IFromAny_FOLLOWS_REL FOLLOWS { get { return new FOLLOWS_REL(Parent, DirectionEnum.None); } }
        }
    }

    public class PersonAlias : AliasResult<PersonAlias, PersonListAlias>
    {
        internal PersonAlias(PersonNode parent)
        {
            Node = parent;
        }
        internal PersonAlias(PersonNode parent, string name)
        {
            Node = parent;
            AliasName = name;
        }
        internal void SetAlias(string name) => AliasName = name;

        private  PersonAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private  PersonAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private  PersonAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
        {
            Node = alias.Node;
        }

        public Assignment[] Assign(JsNotation<int?> born = default, JsNotation<string> name = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (born.HasValue) assignments.Add(new Assignment(this.born, born));
            if (name.HasValue) assignments.Add(new Assignment(this.name, name));
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
                        { "name", new StringResult(this, "name", MovieGraph.Model.Datastore.Model.Entities["Person"], MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["name"]) },
                        { "born", new NumericResult(this, "born", MovieGraph.Model.Datastore.Model.Entities["Person"], MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["born"]) },
                        { "Uid", new StringResult(this, "Uid", MovieGraph.Model.Datastore.Model.Entities["Person"], MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["Uid"]) },
                    };
                }
                return m_AliasFields;
            }
        }
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public PersonNode.PersonIn In { get { return new PersonNode.PersonIn(new PersonNode(this, true)); } }
        public PersonNode.PersonOut Out { get { return new PersonNode.PersonOut(new PersonNode(this, true)); } }
        public PersonNode.PersonAny Any { get { return new PersonNode.PersonAny(new PersonNode(this, true)); } }

        public StringResult name
        {
            get
            {
                if (m_name is null)
                    m_name = (StringResult)AliasFields["name"];

                return m_name;
            }
        }
        private StringResult m_name = null;
        public NumericResult born
        {
            get
            {
                if (m_born is null)
                    m_born = (NumericResult)AliasFields["born"];

                return m_born;
            }
        }
        private NumericResult m_born = null;
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
        public AsResult As(string aliasName, out PersonAlias alias)
        {
            alias = new PersonAlias((PersonNode)Node)
            {
                AliasName = aliasName
            };
            return this.As(aliasName);
        }
    }

    public class PersonListAlias : ListResult<PersonListAlias, PersonAlias>, IAliasListResult
    {
        private PersonListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private PersonListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private PersonListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
    }
    public class PersonJaggedListAlias : ListResult<PersonJaggedListAlias, PersonListAlias>, IAliasJaggedListResult
    {
        private PersonJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private PersonJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private PersonJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
    }
}