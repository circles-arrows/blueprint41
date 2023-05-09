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
		public static EmployeeDepartmentHistoryNode EmployeeDepartmentHistory { get { return new EmployeeDepartmentHistoryNode(); } }
	}

	public partial class EmployeeDepartmentHistoryNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(EmployeeDepartmentHistoryNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(EmployeeDepartmentHistoryNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "EmployeeDepartmentHistory";
		}

		protected override Entity GetEntity()
        {
			return m.EmployeeDepartmentHistory.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.EmployeeDepartmentHistory.Entity.FunctionalId;
            }
        }

		internal EmployeeDepartmentHistoryNode() { }
		internal EmployeeDepartmentHistoryNode(EmployeeDepartmentHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal EmployeeDepartmentHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal EmployeeDepartmentHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public EmployeeDepartmentHistoryNode Where(JsNotation<string> EndDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<EmployeeDepartmentHistoryAlias> alias = new Lazy<EmployeeDepartmentHistoryAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (EndDate.HasValue) conditions.Add(new QueryCondition(alias.Value.EndDate, Operator.Equals, ((IValue)EndDate).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (StartDate.HasValue) conditions.Add(new QueryCondition(alias.Value.StartDate, Operator.Equals, ((IValue)StartDate).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public EmployeeDepartmentHistoryNode Assign(JsNotation<string> EndDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<EmployeeDepartmentHistoryAlias> alias = new Lazy<EmployeeDepartmentHistoryAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (EndDate.HasValue) assignments.Add(new Assignment(alias.Value.EndDate, EndDate));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (StartDate.HasValue) assignments.Add(new Assignment(alias.Value.StartDate, StartDate));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public EmployeeDepartmentHistoryNode Alias(out EmployeeDepartmentHistoryAlias alias)
        {
            if (NodeAlias is EmployeeDepartmentHistoryAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new EmployeeDepartmentHistoryAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public EmployeeDepartmentHistoryNode Alias(out EmployeeDepartmentHistoryAlias alias, string name)
        {
            if (NodeAlias is EmployeeDepartmentHistoryAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new EmployeeDepartmentHistoryAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public EmployeeDepartmentHistoryNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public EmployeeDepartmentHistoryIn  In  { get { return new EmployeeDepartmentHistoryIn(this); } }
		public class EmployeeDepartmentHistoryIn
		{
			private EmployeeDepartmentHistoryNode Parent;
			internal EmployeeDepartmentHistoryIn(EmployeeDepartmentHistoryNode parent)
			{
				Parent = parent;
			}
			public IFromIn_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT { get { return new EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT_REL EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT { get { return new EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT_REL(Parent, DirectionEnum.In); } }

		}

		public EmployeeDepartmentHistoryOut Out { get { return new EmployeeDepartmentHistoryOut(this); } }
		public class EmployeeDepartmentHistoryOut
		{
			private EmployeeDepartmentHistoryNode Parent;
			internal EmployeeDepartmentHistoryOut(EmployeeDepartmentHistoryNode parent)
			{
				Parent = parent;
			}
			public IFromOut_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY { get { return new EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class EmployeeDepartmentHistoryAlias : AliasResult<EmployeeDepartmentHistoryAlias, EmployeeDepartmentHistoryListAlias>
	{
		internal EmployeeDepartmentHistoryAlias(EmployeeDepartmentHistoryNode parent)
		{
			Node = parent;
		}
		internal EmployeeDepartmentHistoryAlias(EmployeeDepartmentHistoryNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  EmployeeDepartmentHistoryAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  EmployeeDepartmentHistoryAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  EmployeeDepartmentHistoryAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> EndDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (EndDate.HasValue) assignments.Add(new Assignment(this.EndDate, EndDate));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (StartDate.HasValue) assignments.Add(new Assignment(this.StartDate, StartDate));
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
						{ "StartDate", new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"], Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"].Properties["StartDate"]) },
						{ "EndDate", new StringResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"], Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"].Properties["EndDate"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public EmployeeDepartmentHistoryNode.EmployeeDepartmentHistoryIn In { get { return new EmployeeDepartmentHistoryNode.EmployeeDepartmentHistoryIn(new EmployeeDepartmentHistoryNode(this, true)); } }
		public EmployeeDepartmentHistoryNode.EmployeeDepartmentHistoryOut Out { get { return new EmployeeDepartmentHistoryNode.EmployeeDepartmentHistoryOut(new EmployeeDepartmentHistoryNode(this, true)); } }

		public DateTimeResult StartDate
		{
			get
			{
				if (m_StartDate is null)
					m_StartDate = (DateTimeResult)AliasFields["StartDate"];

				return m_StartDate;
			}
		}
		private DateTimeResult m_StartDate = null;
		public StringResult EndDate
		{
			get
			{
				if (m_EndDate is null)
					m_EndDate = (StringResult)AliasFields["EndDate"];

				return m_EndDate;
			}
		}
		private StringResult m_EndDate = null;
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
		public AsResult As(string aliasName, out EmployeeDepartmentHistoryAlias alias)
		{
			alias = new EmployeeDepartmentHistoryAlias((EmployeeDepartmentHistoryNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class EmployeeDepartmentHistoryListAlias : ListResult<EmployeeDepartmentHistoryListAlias, EmployeeDepartmentHistoryAlias>, IAliasListResult
	{
		private EmployeeDepartmentHistoryListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private EmployeeDepartmentHistoryListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private EmployeeDepartmentHistoryListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class EmployeeDepartmentHistoryJaggedListAlias : ListResult<EmployeeDepartmentHistoryJaggedListAlias, EmployeeDepartmentHistoryListAlias>, IAliasJaggedListResult
	{
		private EmployeeDepartmentHistoryJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private EmployeeDepartmentHistoryJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private EmployeeDepartmentHistoryJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
