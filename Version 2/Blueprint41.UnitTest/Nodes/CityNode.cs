#nullable disable
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable CS8981 // Names should not be lower type only

using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Events;
using Blueprint41.Persistence;
using Blueprint41.Query;

using m = Datastore.Manipulation;

namespace Datastore.Query
{
    public partial class Node
    {
        public static CityNode City { get { return new CityNode(); } }
    }

    public partial class CityNode : Blueprint41.Query.Node
    {
        public static implicit operator QueryCondition(CityNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(CityNode a)
        {
            return new QueryCondition(a, true);
        } 

        protected override string GetNeo4jLabel()
        {
            return "City";
        }

        protected override Entity GetEntity()
        {
            return m.City.Entity;
        }
        public FunctionalId FunctionalId
        {
            get
            {
                return m.City.Entity.FunctionalId;
            }
        }

        internal CityNode() { }
        internal CityNode(CityAlias alias, bool isReference = false)
        {
            NodeAlias = alias;
            IsReference = isReference;
        }
        internal CityNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
        internal CityNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
        {
            NodeAlias = nodeAlias;
        }

        public CityNode Where(JsNotation<string> Country = default, JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Name = default, JsNotation<string> State = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<CityAlias> alias = new Lazy<CityAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Country.HasValue) conditions.Add(new QueryCondition(alias.Value.Country, Operator.Equals, ((IValue)Country).GetValue()));
            if (LastModifiedOn.HasValue) conditions.Add(new QueryCondition(alias.Value.LastModifiedOn, Operator.Equals, ((IValue)LastModifiedOn).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (State.HasValue) conditions.Add(new QueryCondition(alias.Value.State, Operator.Equals, ((IValue)State).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public CityNode Assign(JsNotation<string> Country = default, JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Name = default, JsNotation<string> State = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<CityAlias> alias = new Lazy<CityAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Country.HasValue) assignments.Add(new Assignment(alias.Value.Country, Country));
            if (LastModifiedOn.HasValue) assignments.Add(new Assignment(alias.Value.LastModifiedOn, LastModifiedOn));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (State.HasValue) assignments.Add(new Assignment(alias.Value.State, State));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

        public CityNode Alias(out CityAlias alias)
        {
            if (NodeAlias is CityAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new CityAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
        public CityNode Alias(out CityAlias alias, string name)
        {
            if (NodeAlias is CityAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new CityAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

        public CityNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
            IsReference = true;
            return this;
        }


        public CityOut Out { get { return new CityOut(this); } }
        public class CityOut
        {
            private CityNode Parent;
            internal CityOut(CityNode parent)
            {
                Parent = parent;
            }
            public IFromOut_PERSON_LIVES_IN_REL PERSON_LIVES_IN { get { return new PERSON_LIVES_IN_REL(Parent, DirectionEnum.Out); } }
            public IFromOut_RESTAURANT_LOCATED_AT_REL RESTAURANT_LOCATED_AT { get { return new RESTAURANT_LOCATED_AT_REL(Parent, DirectionEnum.Out); } }
        }
    }

    public class CityAlias : AliasResult<CityAlias, CityListAlias>
    {
        internal CityAlias(CityNode parent)
        {
            Node = parent;
        }
        internal CityAlias(CityNode parent, string name)
        {
            Node = parent;
            AliasName = name;
        }
        internal void SetAlias(string name) => AliasName = name;

        private  CityAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private  CityAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private  CityAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
        {
            Node = alias.Node;
        }

        public Assignment[] Assign(JsNotation<string> Country = default, JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Name = default, JsNotation<string> State = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (Country.HasValue) assignments.Add(new Assignment(this.Country, Country));
            if (LastModifiedOn.HasValue) assignments.Add(new Assignment(this.LastModifiedOn, LastModifiedOn));
            if (Name.HasValue) assignments.Add(new Assignment(this.Name, Name));
            if (State.HasValue) assignments.Add(new Assignment(this.State, State));
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
                        { "Name", new StringResult(this, "Name", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"].Properties["Name"]) },
                        { "State", new StringResult(this, "State", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"].Properties["State"]) },
                        { "Country", new StringResult(this, "Country", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"].Properties["Country"]) },
                        { "Uid", new StringResult(this, "Uid", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"]) },
                        { "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"]) },
                    };
                }
                return m_AliasFields;
            }
        }
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public CityNode.CityOut Out { get { return new CityNode.CityOut(new CityNode(this, true)); } }

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
        public StringResult State
        {
            get
            {
                if (m_State is null)
                    m_State = (StringResult)AliasFields["State"];

                return m_State;
            }
        }
        private StringResult m_State = null;
        public StringResult Country
        {
            get
            {
                if (m_Country is null)
                    m_Country = (StringResult)AliasFields["Country"];

                return m_Country;
            }
        }
        private StringResult m_Country = null;
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
        public AsResult As(string aliasName, out CityAlias alias)
        {
            alias = new CityAlias((CityNode)Node)
            {
                AliasName = aliasName
            };
            return this.As(aliasName);
        }
    }

    public class CityListAlias : ListResult<CityListAlias, CityAlias>, IAliasListResult
    {
        private CityListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private CityListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private CityListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
    }
    public class CityJaggedListAlias : ListResult<CityJaggedListAlias, CityListAlias>, IAliasJaggedListResult
    {
        private CityJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private CityJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private CityJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
    }
}
