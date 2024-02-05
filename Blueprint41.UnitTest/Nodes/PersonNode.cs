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

        public PersonNode Where(JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<PersonAlias> alias = new Lazy<PersonAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (LastModifiedOn.HasValue) conditions.Add(new QueryCondition(alias.Value.LastModifiedOn, Operator.Equals, ((IValue)LastModifiedOn).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public PersonNode Assign(JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<PersonAlias> alias = new Lazy<PersonAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (LastModifiedOn.HasValue) assignments.Add(new Assignment(alias.Value.LastModifiedOn, LastModifiedOn));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
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
            public IFromIn_PERSON_DIRECTED_REL PERSON_DIRECTED { get { return new PERSON_DIRECTED_REL(Parent, DirectionEnum.In); } }
            public IFromIn_PERSON_EATS_AT_REL PERSON_EATS_AT { get { return new PERSON_EATS_AT_REL(Parent, DirectionEnum.In); } }
            public IFromIn_PERSON_LIVES_IN_REL PERSON_LIVES_IN { get { return new PERSON_LIVES_IN_REL(Parent, DirectionEnum.In); } }
            public IFromIn_SUBSCRIBED_TO_STREAMING_SERVICE_REL SUBSCRIBED_TO_STREAMING_SERVICE { get { return new SUBSCRIBED_TO_STREAMING_SERVICE_REL(Parent, DirectionEnum.In); } }
            public IFromIn_WATCHED_MOVIE_REL WATCHED_MOVIE { get { return new WATCHED_MOVIE_REL(Parent, DirectionEnum.In); } }

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

        public Assignment[] Assign(JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (LastModifiedOn.HasValue) assignments.Add(new Assignment(this.LastModifiedOn, LastModifiedOn));
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
                        { "Name", new StringResult(this, "Name", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["Name"]) },
                        { "Uid", new StringResult(this, "Uid", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"]) },
                        { "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"]) },
                    };
                }
                return m_AliasFields;
            }
        }
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public PersonNode.PersonIn In { get { return new PersonNode.PersonIn(new PersonNode(this, true)); } }

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