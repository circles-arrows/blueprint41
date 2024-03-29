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
		public static DepartmentNode Department { get { return new DepartmentNode(); } }
	}

	public partial class DepartmentNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(DepartmentNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(DepartmentNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Department";
		}

		protected override Entity GetEntity()
        {
			return m.Department.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.Department.Entity.FunctionalId;
            }
        }

		internal DepartmentNode() { }
		internal DepartmentNode(DepartmentAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal DepartmentNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal DepartmentNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public DepartmentNode Where(JsNotation<string> GroupName = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<DepartmentAlias> alias = new Lazy<DepartmentAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (GroupName.HasValue) conditions.Add(new QueryCondition(alias.Value.GroupName, Operator.Equals, ((IValue)GroupName).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public DepartmentNode Assign(JsNotation<string> GroupName = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<DepartmentAlias> alias = new Lazy<DepartmentAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (GroupName.HasValue) assignments.Add(new Assignment(alias.Value.GroupName, GroupName));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public DepartmentNode Alias(out DepartmentAlias alias)
        {
            if (NodeAlias is DepartmentAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new DepartmentAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public DepartmentNode Alias(out DepartmentAlias alias, string name)
        {
            if (NodeAlias is DepartmentAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new DepartmentAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public DepartmentNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public DepartmentIn  In  { get { return new DepartmentIn(this); } }
		public class DepartmentIn
		{
			private DepartmentNode Parent;
			internal DepartmentIn(DepartmentNode parent)
			{
				Parent = parent;
			}
			public IFromIn_DEPARTMENT_CONTAINS_EMPLOYEE_REL DEPARTMENT_CONTAINS_EMPLOYEE { get { return new DEPARTMENT_CONTAINS_EMPLOYEE_REL(Parent, DirectionEnum.In); } }

		}

		public DepartmentOut Out { get { return new DepartmentOut(this); } }
		public class DepartmentOut
		{
			private DepartmentNode Parent;
			internal DepartmentOut(DepartmentNode parent)
			{
				Parent = parent;
			}
			public IFromOut_EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT_REL EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT { get { return new EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class DepartmentAlias : AliasResult<DepartmentAlias, DepartmentListAlias>
	{
		internal DepartmentAlias(DepartmentNode parent)
		{
			Node = parent;
		}
		internal DepartmentAlias(DepartmentNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  DepartmentAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  DepartmentAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  DepartmentAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> GroupName = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (GroupName.HasValue) assignments.Add(new Assignment(this.GroupName, GroupName));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Department"], Datastore.AdventureWorks.Model.Entities["Department"].Properties["Name"]) },
						{ "GroupName", new StringResult(this, "GroupName", Datastore.AdventureWorks.Model.Entities["Department"], Datastore.AdventureWorks.Model.Entities["Department"].Properties["GroupName"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Department"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Department"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public DepartmentNode.DepartmentIn In { get { return new DepartmentNode.DepartmentIn(new DepartmentNode(this, true)); } }
		public DepartmentNode.DepartmentOut Out { get { return new DepartmentNode.DepartmentOut(new DepartmentNode(this, true)); } }

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
		public StringResult GroupName
		{
			get
			{
				if (m_GroupName is null)
					m_GroupName = (StringResult)AliasFields["GroupName"];

				return m_GroupName;
			}
		}
		private StringResult m_GroupName = null;
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
		public AsResult As(string aliasName, out DepartmentAlias alias)
		{
			alias = new DepartmentAlias((DepartmentNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class DepartmentListAlias : ListResult<DepartmentListAlias, DepartmentAlias>, IAliasListResult
	{
		private DepartmentListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private DepartmentListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private DepartmentListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class DepartmentJaggedListAlias : ListResult<DepartmentJaggedListAlias, DepartmentListAlias>, IAliasJaggedListResult
	{
		private DepartmentJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private DepartmentJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private DepartmentJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
