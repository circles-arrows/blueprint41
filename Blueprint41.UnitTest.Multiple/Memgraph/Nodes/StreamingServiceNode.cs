using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Query;

using m = Memgraph.Datastore.Manipulation;

namespace Memgraph.Datastore.Query
{
    public partial class Node
    {
        public static StreamingServiceNode StreamingService { get { return new StreamingServiceNode(); } }
    }

    public partial class StreamingServiceNode : Blueprint41.Query.Node
    {
        public static implicit operator QueryCondition(StreamingServiceNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(StreamingServiceNode a)
        {
            return new QueryCondition(a, true);
        } 

        protected override string GetNeo4jLabel()
        {
            return "StreamingService";
        }

        protected override Entity GetEntity()
        {
            return m.StreamingService.Entity;
        }

        internal StreamingServiceNode() { }
        internal StreamingServiceNode(StreamingServiceAlias alias, bool isReference = false)
        {
            NodeAlias = alias;
            IsReference = isReference;
        }
        internal StreamingServiceNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
        internal StreamingServiceNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
        {
            NodeAlias = nodeAlias;
        }

        public StreamingServiceNode Where(JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<StreamingServiceAlias> alias = new Lazy<StreamingServiceAlias>(delegate()
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
        public StreamingServiceNode Assign(JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<StreamingServiceAlias> alias = new Lazy<StreamingServiceAlias>(delegate()
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

        public StreamingServiceNode Alias(out StreamingServiceAlias alias)
        {
            if (NodeAlias is StreamingServiceAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new StreamingServiceAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
        public StreamingServiceNode Alias(out StreamingServiceAlias alias, string name)
        {
            if (NodeAlias is StreamingServiceAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new StreamingServiceAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

        public StreamingServiceNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
            IsReference = true;
            return this;
        }


        public StreamingServiceOut Out { get { return new StreamingServiceOut(this); } }
        public class StreamingServiceOut
        {
            private StreamingServiceNode Parent;
            internal StreamingServiceOut(StreamingServiceNode parent)
            {
                Parent = parent;
            }
            public IFromOut_SUBSCRIBED_TO_STREAMING_SERVICE_REL SUBSCRIBED_TO_STREAMING_SERVICE { get { return new SUBSCRIBED_TO_STREAMING_SERVICE_REL(Parent, DirectionEnum.Out); } }
        }
    }

    public class StreamingServiceAlias : AliasResult<StreamingServiceAlias, StreamingServiceListAlias>
    {
        internal StreamingServiceAlias(StreamingServiceNode parent)
        {
            Node = parent;
        }
        internal StreamingServiceAlias(StreamingServiceNode parent, string name)
        {
            Node = parent;
            AliasName = name;
        }
        internal void SetAlias(string name) => AliasName = name;

        private  StreamingServiceAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private  StreamingServiceAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private  StreamingServiceAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
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
                        { "Name", new StringResult(this, "Name", Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Entities["StreamingService"], Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Entities["StreamingService"].Properties["Name"]) },
                        { "Uid", new StringResult(this, "Uid", Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Entities["StreamingService"], Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"]) },
                        { "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Entities["StreamingService"], Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"]) },
                    };
                }
                return m_AliasFields;
            }
        }
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public StreamingServiceNode.StreamingServiceOut Out { get { return new StreamingServiceNode.StreamingServiceOut(new StreamingServiceNode(this, true)); } }

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
        public AsResult As(string aliasName, out StreamingServiceAlias alias)
        {
            alias = new StreamingServiceAlias((StreamingServiceNode)Node)
            {
                AliasName = aliasName
            };
            return this.As(aliasName);
        }
    }

    public class StreamingServiceListAlias : ListResult<StreamingServiceListAlias, StreamingServiceAlias>, IAliasListResult
    {
        private StreamingServiceListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private StreamingServiceListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private StreamingServiceListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
    }
    public class StreamingServiceJaggedListAlias : ListResult<StreamingServiceJaggedListAlias, StreamingServiceListAlias>, IAliasJaggedListResult
    {
        private StreamingServiceJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private StreamingServiceJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private StreamingServiceJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
    }
}