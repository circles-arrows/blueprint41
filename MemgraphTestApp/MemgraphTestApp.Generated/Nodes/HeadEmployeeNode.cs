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
		public static HeadEmployeeNode HeadEmployee { get { return new HeadEmployeeNode(); } }
	}

	public partial class HeadEmployeeNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(HeadEmployeeNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(HeadEmployeeNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "HeadEmployee";
		}

		protected override Entity GetEntity()
        {
			return m.HeadEmployee.Entity;
        }

		internal HeadEmployeeNode() { }
		internal HeadEmployeeNode(HeadEmployeeAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal HeadEmployeeNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal HeadEmployeeNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public HeadEmployeeNode Where(JsNotation<string> CreatedBy = default, JsNotation<System.DateTime?> CreatedOn = default, JsNotation<string> Description = default, JsNotation<string> FirstName = default, JsNotation<string> LastModifiedBy = default, JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> LastName = default, JsNotation<string> Title = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<HeadEmployeeAlias> alias = new Lazy<HeadEmployeeAlias>(delegate()
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
            if (Title.HasValue) conditions.Add(new QueryCondition(alias.Value.Title, Operator.Equals, ((IValue)Title).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public HeadEmployeeNode Assign(JsNotation<string> CreatedBy = default, JsNotation<System.DateTime?> CreatedOn = default, JsNotation<string> Description = default, JsNotation<string> FirstName = default, JsNotation<string> LastModifiedBy = default, JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> LastName = default, JsNotation<string> Title = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<HeadEmployeeAlias> alias = new Lazy<HeadEmployeeAlias>(delegate()
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
            if (Title.HasValue) assignments.Add(new Assignment(alias.Value.Title, Title));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public HeadEmployeeNode Alias(out HeadEmployeeAlias alias)
        {
            if (NodeAlias is HeadEmployeeAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new HeadEmployeeAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public HeadEmployeeNode Alias(out HeadEmployeeAlias alias, string name)
        {
            if (NodeAlias is HeadEmployeeAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new HeadEmployeeAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public HeadEmployeeNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public HeadEmployeeIn  In  { get { return new HeadEmployeeIn(this); } }
		public class HeadEmployeeIn
		{
			private HeadEmployeeNode Parent;
			internal HeadEmployeeIn(HeadEmployeeNode parent)
			{
				Parent = parent;
			}
			public IFromIn_HAS_STATUS_REL HAS_STATUS { get { return new HAS_STATUS_REL(Parent, DirectionEnum.In); } }
			public IFromIn_MANAGES_BRANCH_REL MANAGES_BRANCH { get { return new MANAGES_BRANCH_REL(Parent, DirectionEnum.In); } }
			public IFromIn_MANAGES_DEPARTMENT_REL MANAGES_DEPARTMENT { get { return new MANAGES_DEPARTMENT_REL(Parent, DirectionEnum.In); } }

		}
	}

	public class HeadEmployeeAlias : AliasResult<HeadEmployeeAlias, HeadEmployeeListAlias>
	{
		internal HeadEmployeeAlias(HeadEmployeeNode parent)
		{
			Node = parent;
		}
		internal HeadEmployeeAlias(HeadEmployeeNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  HeadEmployeeAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  HeadEmployeeAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  HeadEmployeeAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> CreatedBy = default, JsNotation<System.DateTime?> CreatedOn = default, JsNotation<string> Description = default, JsNotation<string> FirstName = default, JsNotation<string> LastModifiedBy = default, JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> LastName = default, JsNotation<string> Title = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (CreatedBy.HasValue) assignments.Add(new Assignment(this.CreatedBy, CreatedBy));
			if (CreatedOn.HasValue) assignments.Add(new Assignment(this.CreatedOn, CreatedOn));
			if (Description.HasValue) assignments.Add(new Assignment(this.Description, Description));
			if (FirstName.HasValue) assignments.Add(new Assignment(this.FirstName, FirstName));
			if (LastModifiedBy.HasValue) assignments.Add(new Assignment(this.LastModifiedBy, LastModifiedBy));
			if (LastModifiedOn.HasValue) assignments.Add(new Assignment(this.LastModifiedOn, LastModifiedOn));
			if (LastName.HasValue) assignments.Add(new Assignment(this.LastName, LastName));
			if (Title.HasValue) assignments.Add(new Assignment(this.Title, Title));
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
						{ "Title", new StringResult(this, "Title", MemgraphTestApp.HumanResources.Model.Entities["HeadEmployee"], MemgraphTestApp.HumanResources.Model.Entities["HeadEmployee"].Properties["Title"]) },
						{ "FirstName", new StringResult(this, "FirstName", MemgraphTestApp.HumanResources.Model.Entities["HeadEmployee"], MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["FirstName"]) },
						{ "LastName", new StringResult(this, "LastName", MemgraphTestApp.HumanResources.Model.Entities["HeadEmployee"], MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["LastName"]) },
						{ "Uid", new StringResult(this, "Uid", MemgraphTestApp.HumanResources.Model.Entities["HeadEmployee"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["Uid"]) },
						{ "CreatedOn", new DateTimeResult(this, "CreatedOn", MemgraphTestApp.HumanResources.Model.Entities["HeadEmployee"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["CreatedOn"]) },
						{ "CreatedBy", new StringResult(this, "CreatedBy", MemgraphTestApp.HumanResources.Model.Entities["HeadEmployee"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["CreatedBy"]) },
						{ "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", MemgraphTestApp.HumanResources.Model.Entities["HeadEmployee"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["LastModifiedOn"]) },
						{ "LastModifiedBy", new StringResult(this, "LastModifiedBy", MemgraphTestApp.HumanResources.Model.Entities["HeadEmployee"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["LastModifiedBy"]) },
						{ "Description", new StringResult(this, "Description", MemgraphTestApp.HumanResources.Model.Entities["HeadEmployee"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["Description"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public HeadEmployeeNode.HeadEmployeeIn In { get { return new HeadEmployeeNode.HeadEmployeeIn(new HeadEmployeeNode(this, true)); } }

		public StringResult Title
		{
			get
			{
				if (m_Title is null)
					m_Title = (StringResult)AliasFields["Title"];

				return m_Title;
			}
		}
		private StringResult m_Title = null;
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
		public AsResult As(string aliasName, out HeadEmployeeAlias alias)
		{
			alias = new HeadEmployeeAlias((HeadEmployeeNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class HeadEmployeeListAlias : ListResult<HeadEmployeeListAlias, HeadEmployeeAlias>, IAliasListResult
	{
		private HeadEmployeeListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private HeadEmployeeListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private HeadEmployeeListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class HeadEmployeeJaggedListAlias : ListResult<HeadEmployeeJaggedListAlias, HeadEmployeeListAlias>, IAliasJaggedListResult
	{
		private HeadEmployeeJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private HeadEmployeeJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private HeadEmployeeJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
