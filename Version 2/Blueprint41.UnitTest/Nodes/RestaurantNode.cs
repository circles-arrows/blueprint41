#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable CS8981 // Names should not be lower type only

using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Events;
using Blueprint41.Neo4j.Model;
using Blueprint41.Query;

using m = Datastore.Manipulation;

namespace Datastore.Query
{
    public partial class Node
    {
        public static RestaurantNode Restaurant { get { return new RestaurantNode(); } }
    }

    public partial class RestaurantNode : Blueprint41.Query.Node
    {
        public static implicit operator QueryCondition(RestaurantNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(RestaurantNode a)
        {
            return new QueryCondition(a, true);
        } 

        protected override string GetNeo4jLabel()
        {
            return "Restaurant";
        }

        protected override Entity GetEntity()
        {
            return m.Restaurant.Entity;
        }
        public FunctionalId FunctionalId
        {
            get
            {
                return m.Restaurant.Entity.FunctionalId;
            }
        }

        internal RestaurantNode() { }
        internal RestaurantNode(RestaurantAlias alias, bool isReference = false)
        {
            NodeAlias = alias;
            IsReference = isReference;
        }
        internal RestaurantNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
        internal RestaurantNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
        {
            NodeAlias = nodeAlias;
        }

        public RestaurantNode Where(JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<RestaurantAlias> alias = new Lazy<RestaurantAlias>(delegate()
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
        public RestaurantNode Assign(JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<RestaurantAlias> alias = new Lazy<RestaurantAlias>(delegate()
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

        public RestaurantNode Alias(out RestaurantAlias alias)
        {
            if (NodeAlias is RestaurantAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new RestaurantAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
        public RestaurantNode Alias(out RestaurantAlias alias, string name)
        {
            if (NodeAlias is RestaurantAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new RestaurantAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

        public RestaurantNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
            IsReference = true;
            return this;
        }

        public RestaurantIn  In  { get { return new RestaurantIn(this); } }
        public class RestaurantIn
        {
            private RestaurantNode Parent;
            internal RestaurantIn(RestaurantNode parent)
            {
                Parent = parent;
            }
            public IFromIn_RESTAURANT_LOCATED_AT_REL RESTAURANT_LOCATED_AT { get { return new RESTAURANT_LOCATED_AT_REL(Parent, DirectionEnum.In); } }

        }

        public RestaurantOut Out { get { return new RestaurantOut(this); } }
        public class RestaurantOut
        {
            private RestaurantNode Parent;
            internal RestaurantOut(RestaurantNode parent)
            {
                Parent = parent;
            }
            public IFromOut_PERSON_EATS_AT_REL PERSON_EATS_AT { get { return new PERSON_EATS_AT_REL(Parent, DirectionEnum.Out); } }
        }
    }

    public class RestaurantAlias : AliasResult<RestaurantAlias, RestaurantListAlias>
    {
        internal RestaurantAlias(RestaurantNode parent)
        {
            Node = parent;
        }
        internal RestaurantAlias(RestaurantNode parent, string name)
        {
            Node = parent;
            AliasName = name;
        }
        internal void SetAlias(string name) => AliasName = name;

        private  RestaurantAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private  RestaurantAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private  RestaurantAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
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
                        { "Name", new StringResult(this, "Name", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"].Properties["Name"]) },
                        { "Uid", new StringResult(this, "Uid", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"]) },
                        { "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"]) },
                    };
                }
                return m_AliasFields;
            }
        }
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public RestaurantNode.RestaurantIn In { get { return new RestaurantNode.RestaurantIn(new RestaurantNode(this, true)); } }
        public RestaurantNode.RestaurantOut Out { get { return new RestaurantNode.RestaurantOut(new RestaurantNode(this, true)); } }

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
        public AsResult As(string aliasName, out RestaurantAlias alias)
        {
            alias = new RestaurantAlias((RestaurantNode)Node)
            {
                AliasName = aliasName
            };
            return this.As(aliasName);
        }
    }

    public class RestaurantListAlias : ListResult<RestaurantListAlias, RestaurantAlias>, IAliasListResult
    {
        private RestaurantListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private RestaurantListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private RestaurantListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
    }
    public class RestaurantJaggedListAlias : ListResult<RestaurantJaggedListAlias, RestaurantListAlias>, IAliasJaggedListResult
    {
        private RestaurantJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private RestaurantJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private RestaurantJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
    }
}
