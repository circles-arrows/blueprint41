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
		public static EmployeeNode Employee { get { return new EmployeeNode(); } }
	}

	public partial class EmployeeNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(EmployeeNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(EmployeeNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Employee";
		}

		protected override Entity GetEntity()
        {
			return m.Employee.Entity;
        }

		internal EmployeeNode() { }
		internal EmployeeNode(EmployeeAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal EmployeeNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal EmployeeNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public EmployeeNode Where(JsNotation<string> CreatedBy = default, JsNotation<System.DateTime?> CreatedOn = default, JsNotation<string> Description = default, JsNotation<string> FirstName = default, JsNotation<string> LastModifiedBy = default, JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> LastName = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<EmployeeAlias> alias = new Lazy<EmployeeAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (CreatedBy.HasValue) conditions.Add(new QueryCondition(alias.Value.CreatedBy, Operator.Equals, ((IValue)CreatedBy).GetValue()));
            if (CreatedOn.HasValue) conditions.Add(new QueryCondition(alias.Value.CreatedOn, Operator.Equals, ((IValue)CreatedOn).GetValue()));
            if (Description.HasValue) conditions.Add(new QueryCondition(alias.Value.Description, Operator.Equals, ((IValue)Description).GetValue()));
            if (FirstName.HasValue) conditions.Add(new QueryCondition(alias.Value.FirstName, Operator.Equals, ((IValue)FirstName).GetValue()));
            if (LastModifiedBy.HasValue) conditions.Add(new QueryCondition(alias.Value.LastModifiedBy, Operator.Equals, ((IValue)LastModifiedBy).GetValue()));
            if (LastModifiedOn.HasValue) conditions.Add(new QueryCondition(alias.Value.LastModifiedOn, Operator.Equals, ((IValue)LastModifiedOn).GetValue()));
            if (LastName.HasValue) conditions.Add(new QueryCondition(alias.Value.LastName, Operator.Equals, ((IValue)LastName).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public EmployeeNode Assign(JsNotation<string> CreatedBy = default, JsNotation<System.DateTime?> CreatedOn = default, JsNotation<string> Description = default, JsNotation<string> FirstName = default, JsNotation<string> LastModifiedBy = default, JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> LastName = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<EmployeeAlias> alias = new Lazy<EmployeeAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (CreatedBy.HasValue) assignments.Add(new Assignment(alias.Value.CreatedBy, CreatedBy));
            if (CreatedOn.HasValue) assignments.Add(new Assignment(alias.Value.CreatedOn, CreatedOn));
            if (Description.HasValue) assignments.Add(new Assignment(alias.Value.Description, Description));
            if (FirstName.HasValue) assignments.Add(new Assignment(alias.Value.FirstName, FirstName));
            if (LastModifiedBy.HasValue) assignments.Add(new Assignment(alias.Value.LastModifiedBy, LastModifiedBy));
            if (LastModifiedOn.HasValue) assignments.Add(new Assignment(alias.Value.LastModifiedOn, LastModifiedOn));
            if (LastName.HasValue) assignments.Add(new Assignment(alias.Value.LastName, LastName));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public EmployeeNode Alias(out EmployeeAlias alias)
        {
            if (NodeAlias is EmployeeAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new EmployeeAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public EmployeeNode Alias(out EmployeeAlias alias, string name)
        {
            if (NodeAlias is EmployeeAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new EmployeeAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public EmployeeNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public EmployeeIn  In  { get { return new EmployeeIn(this); } }
		public class EmployeeIn
		{
			private EmployeeNode Parent;
			internal EmployeeIn(EmployeeNode parent)
			{
				Parent = parent;
			}
			public IFromIn_HAS_STATUS_REL HAS_STATUS { get { return new HAS_STATUS_REL(Parent, DirectionEnum.In); } }
			public IFromIn_WORKS_IN_REL WORKS_IN { get { return new WORKS_IN_REL(Parent, DirectionEnum.In); } }

		}
	}

	public class EmployeeAlias : AliasResult<EmployeeAlias, EmployeeListAlias>
	{
		internal EmployeeAlias(EmployeeNode parent)
		{
			Node = parent;
		}
		internal EmployeeAlias(EmployeeNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  EmployeeAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  EmployeeAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  EmployeeAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> CreatedBy = default, JsNotation<System.DateTime?> CreatedOn = default, JsNotation<string> Description = default, JsNotation<string> FirstName = default, JsNotation<string> LastModifiedBy = default, JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> LastName = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (CreatedBy.HasValue) assignments.Add(new Assignment(this.CreatedBy, CreatedBy));
			if (CreatedOn.HasValue) assignments.Add(new Assignment(this.CreatedOn, CreatedOn));
			if (Description.HasValue) assignments.Add(new Assignment(this.Description, Description));
			if (FirstName.HasValue) assignments.Add(new Assignment(this.FirstName, FirstName));
			if (LastModifiedBy.HasValue) assignments.Add(new Assignment(this.LastModifiedBy, LastModifiedBy));
			if (LastModifiedOn.HasValue) assignments.Add(new Assignment(this.LastModifiedOn, LastModifiedOn));
			if (LastName.HasValue) assignments.Add(new Assignment(this.LastName, LastName));
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
						{ "FirstName", new StringResult(this, "FirstName", MemgraphTestApp.HumanResources.Model.Entities["Employee"], MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["FirstName"]) },
						{ "LastName", new StringResult(this, "LastName", MemgraphTestApp.HumanResources.Model.Entities["Employee"], MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["LastName"]) },
						{ "Uid", new StringResult(this, "Uid", MemgraphTestApp.HumanResources.Model.Entities["Employee"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["Uid"]) },
						{ "CreatedOn", new DateTimeResult(this, "CreatedOn", MemgraphTestApp.HumanResources.Model.Entities["Employee"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["CreatedOn"]) },
						{ "CreatedBy", new StringResult(this, "CreatedBy", MemgraphTestApp.HumanResources.Model.Entities["Employee"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["CreatedBy"]) },
						{ "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", MemgraphTestApp.HumanResources.Model.Entities["Employee"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["LastModifiedOn"]) },
						{ "LastModifiedBy", new StringResult(this, "LastModifiedBy", MemgraphTestApp.HumanResources.Model.Entities["Employee"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["LastModifiedBy"]) },
						{ "Description", new StringResult(this, "Description", MemgraphTestApp.HumanResources.Model.Entities["Employee"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["Description"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public EmployeeNode.EmployeeIn In { get { return new EmployeeNode.EmployeeIn(new EmployeeNode(this, true)); } }

		public StringResult FirstName
		{
			get
			{
				if (m_FirstName is null)
					m_FirstName = (StringResult)AliasFields["FirstName"];

				return m_FirstName;
			}
		}
		private StringResult m_FirstName = null;
		public StringResult LastName
		{
			get
			{
				if (m_LastName is null)
					m_LastName = (StringResult)AliasFields["LastName"];

				return m_LastName;
			}
		}
		private StringResult m_LastName = null;
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
		public DateTimeResult CreatedOn
		{
			get
			{
				if (m_CreatedOn is null)
					m_CreatedOn = (DateTimeResult)AliasFields["CreatedOn"];

				return m_CreatedOn;
			}
		}
		private DateTimeResult m_CreatedOn = null;
		public StringResult CreatedBy
		{
			get
			{
				if (m_CreatedBy is null)
					m_CreatedBy = (StringResult)AliasFields["CreatedBy"];

				return m_CreatedBy;
			}
		}
		private StringResult m_CreatedBy = null;
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
		public StringResult LastModifiedBy
		{
			get
			{
				if (m_LastModifiedBy is null)
					m_LastModifiedBy = (StringResult)AliasFields["LastModifiedBy"];

				return m_LastModifiedBy;
			}
		}
		private StringResult m_LastModifiedBy = null;
		public StringResult Description
		{
			get
			{
				if (m_Description is null)
					m_Description = (StringResult)AliasFields["Description"];

				return m_Description;
			}
		}
		private StringResult m_Description = null;
		public AsResult As(string aliasName, out EmployeeAlias alias)
		{
			alias = new EmployeeAlias((EmployeeNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class EmployeeListAlias : ListResult<EmployeeListAlias, EmployeeAlias>, IAliasListResult
	{
		private EmployeeListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private EmployeeListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private EmployeeListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class EmployeeJaggedListAlias : ListResult<EmployeeJaggedListAlias, EmployeeListAlias>, IAliasJaggedListResult
	{
		private EmployeeJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private EmployeeJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private EmployeeJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
