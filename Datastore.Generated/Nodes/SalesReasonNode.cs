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
		public static SalesReasonNode SalesReason { get { return new SalesReasonNode(); } }
	}

	public partial class SalesReasonNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(SalesReasonNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(SalesReasonNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "SalesReason";
		}

		protected override Entity GetEntity()
        {
			return m.SalesReason.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.SalesReason.Entity.FunctionalId;
            }
        }

		internal SalesReasonNode() { }
		internal SalesReasonNode(SalesReasonAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesReasonNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal SalesReasonNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public SalesReasonNode Where(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> ReasonType = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SalesReasonAlias> alias = new Lazy<SalesReasonAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (ReasonType.HasValue) conditions.Add(new QueryCondition(alias.Value.ReasonType, Operator.Equals, ((IValue)ReasonType).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public SalesReasonNode Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> ReasonType = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SalesReasonAlias> alias = new Lazy<SalesReasonAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (ReasonType.HasValue) assignments.Add(new Assignment(alias.Value.ReasonType, ReasonType));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public SalesReasonNode Alias(out SalesReasonAlias alias)
        {
            if (NodeAlias is SalesReasonAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new SalesReasonAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public SalesReasonNode Alias(out SalesReasonAlias alias, string name)
        {
            if (NodeAlias is SalesReasonAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new SalesReasonAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public SalesReasonNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public SalesReasonOut Out { get { return new SalesReasonOut(this); } }
		public class SalesReasonOut
		{
			private SalesReasonNode Parent;
			internal SalesReasonOut(SalesReasonNode parent)
			{
				Parent = parent;
			}
			public IFromOut_SALESORDERHEADER_HAS_SALESREASON_REL SALESORDERHEADER_HAS_SALESREASON { get { return new SALESORDERHEADER_HAS_SALESREASON_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class SalesReasonAlias : AliasResult<SalesReasonAlias, SalesReasonListAlias>
	{
		internal SalesReasonAlias(SalesReasonNode parent)
		{
			Node = parent;
		}
		internal SalesReasonAlias(SalesReasonNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  SalesReasonAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  SalesReasonAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  SalesReasonAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> ReasonType = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Name.HasValue) assignments.Add(new Assignment(this.Name, Name));
			if (ReasonType.HasValue) assignments.Add(new Assignment(this.ReasonType, ReasonType));
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["SalesReason"], Datastore.AdventureWorks.Model.Entities["SalesReason"].Properties["Name"]) },
						{ "ReasonType", new StringResult(this, "ReasonType", Datastore.AdventureWorks.Model.Entities["SalesReason"], Datastore.AdventureWorks.Model.Entities["SalesReason"].Properties["ReasonType"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesReason"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesReason"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public SalesReasonNode.SalesReasonOut Out { get { return new SalesReasonNode.SalesReasonOut(new SalesReasonNode(this, true)); } }

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
		public StringResult ReasonType
		{
			get
			{
				if (m_ReasonType is null)
					m_ReasonType = (StringResult)AliasFields["ReasonType"];

				return m_ReasonType;
			}
		}
		private StringResult m_ReasonType = null;
		public DateTimeResult ModifiedDate
		{
			get
			{
				if (m_ModifiedDate is null)
					m_ModifiedDate = (DateTimeResult)AliasFields["ModifiedDate"];

				return m_ModifiedDate;
			}
		}
		private DateTimeResult m_ModifiedDate = null;
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
		public AsResult As(string aliasName, out SalesReasonAlias alias)
		{
			alias = new SalesReasonAlias((SalesReasonNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class SalesReasonListAlias : ListResult<SalesReasonListAlias, SalesReasonAlias>, IAliasListResult
	{
		private SalesReasonListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SalesReasonListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SalesReasonListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class SalesReasonJaggedListAlias : ListResult<SalesReasonJaggedListAlias, SalesReasonListAlias>, IAliasJaggedListResult
	{
		private SalesReasonJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SalesReasonJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SalesReasonJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
