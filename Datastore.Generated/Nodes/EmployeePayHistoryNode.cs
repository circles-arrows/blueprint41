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
		public static EmployeePayHistoryNode EmployeePayHistory { get { return new EmployeePayHistoryNode(); } }
	}

	public partial class EmployeePayHistoryNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(EmployeePayHistoryNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(EmployeePayHistoryNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "EmployeePayHistory";
		}

		protected override Entity GetEntity()
        {
			return m.EmployeePayHistory.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.EmployeePayHistory.Entity.FunctionalId;
            }
        }

		internal EmployeePayHistoryNode() { }
		internal EmployeePayHistoryNode(EmployeePayHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal EmployeePayHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal EmployeePayHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public EmployeePayHistoryNode Where(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> PayFrequency = default, JsNotation<string> Rate = default, JsNotation<System.DateTime> RateChangeDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<EmployeePayHistoryAlias> alias = new Lazy<EmployeePayHistoryAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (PayFrequency.HasValue) conditions.Add(new QueryCondition(alias.Value.PayFrequency, Operator.Equals, ((IValue)PayFrequency).GetValue()));
            if (Rate.HasValue) conditions.Add(new QueryCondition(alias.Value.Rate, Operator.Equals, ((IValue)Rate).GetValue()));
            if (RateChangeDate.HasValue) conditions.Add(new QueryCondition(alias.Value.RateChangeDate, Operator.Equals, ((IValue)RateChangeDate).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public EmployeePayHistoryNode Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> PayFrequency = default, JsNotation<string> Rate = default, JsNotation<System.DateTime> RateChangeDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<EmployeePayHistoryAlias> alias = new Lazy<EmployeePayHistoryAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (PayFrequency.HasValue) assignments.Add(new Assignment(alias.Value.PayFrequency, PayFrequency));
            if (Rate.HasValue) assignments.Add(new Assignment(alias.Value.Rate, Rate));
            if (RateChangeDate.HasValue) assignments.Add(new Assignment(alias.Value.RateChangeDate, RateChangeDate));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public EmployeePayHistoryNode Alias(out EmployeePayHistoryAlias alias)
        {
            if (NodeAlias is EmployeePayHistoryAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new EmployeePayHistoryAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public EmployeePayHistoryNode Alias(out EmployeePayHistoryAlias alias, string name)
        {
            if (NodeAlias is EmployeePayHistoryAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new EmployeePayHistoryAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public EmployeePayHistoryNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public EmployeePayHistoryOut Out { get { return new EmployeePayHistoryOut(this); } }
		public class EmployeePayHistoryOut
		{
			private EmployeePayHistoryNode Parent;
			internal EmployeePayHistoryOut(EmployeePayHistoryNode parent)
			{
				Parent = parent;
			}
			public IFromOut_EMPLOYEE_HAS_EMPLOYEEPAYHISTORY_REL EMPLOYEE_HAS_EMPLOYEEPAYHISTORY { get { return new EMPLOYEE_HAS_EMPLOYEEPAYHISTORY_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class EmployeePayHistoryAlias : AliasResult<EmployeePayHistoryAlias, EmployeePayHistoryListAlias>
	{
		internal EmployeePayHistoryAlias(EmployeePayHistoryNode parent)
		{
			Node = parent;
		}
		internal EmployeePayHistoryAlias(EmployeePayHistoryNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  EmployeePayHistoryAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  EmployeePayHistoryAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  EmployeePayHistoryAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> PayFrequency = default, JsNotation<string> Rate = default, JsNotation<System.DateTime> RateChangeDate = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (PayFrequency.HasValue) assignments.Add(new Assignment(this.PayFrequency, PayFrequency));
			if (Rate.HasValue) assignments.Add(new Assignment(this.Rate, Rate));
			if (RateChangeDate.HasValue) assignments.Add(new Assignment(this.RateChangeDate, RateChangeDate));
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
						{ "RateChangeDate", new DateTimeResult(this, "RateChangeDate", Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"], Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"].Properties["RateChangeDate"]) },
						{ "Rate", new StringResult(this, "Rate", Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"], Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"].Properties["Rate"]) },
						{ "PayFrequency", new StringResult(this, "PayFrequency", Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"], Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"].Properties["PayFrequency"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public EmployeePayHistoryNode.EmployeePayHistoryOut Out { get { return new EmployeePayHistoryNode.EmployeePayHistoryOut(new EmployeePayHistoryNode(this, true)); } }

		public DateTimeResult RateChangeDate
		{
			get
			{
				if (m_RateChangeDate is null)
					m_RateChangeDate = (DateTimeResult)AliasFields["RateChangeDate"];

				return m_RateChangeDate;
			}
		}
		private DateTimeResult m_RateChangeDate = null;
		public StringResult Rate
		{
			get
			{
				if (m_Rate is null)
					m_Rate = (StringResult)AliasFields["Rate"];

				return m_Rate;
			}
		}
		private StringResult m_Rate = null;
		public StringResult PayFrequency
		{
			get
			{
				if (m_PayFrequency is null)
					m_PayFrequency = (StringResult)AliasFields["PayFrequency"];

				return m_PayFrequency;
			}
		}
		private StringResult m_PayFrequency = null;
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
		public AsResult As(string aliasName, out EmployeePayHistoryAlias alias)
		{
			alias = new EmployeePayHistoryAlias((EmployeePayHistoryNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class EmployeePayHistoryListAlias : ListResult<EmployeePayHistoryListAlias, EmployeePayHistoryAlias>, IAliasListResult
	{
		private EmployeePayHistoryListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private EmployeePayHistoryListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private EmployeePayHistoryListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class EmployeePayHistoryJaggedListAlias : ListResult<EmployeePayHistoryJaggedListAlias, EmployeePayHistoryListAlias>, IAliasJaggedListResult
	{
		private EmployeePayHistoryJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private EmployeePayHistoryJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private EmployeePayHistoryJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
