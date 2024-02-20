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
		[Obsolete("This entity is virtual, consider making entity Neo4jBase concrete or use another entity as your starting point.", true)]
		public static Neo4jBaseNode Neo4jBase { get { return new Neo4jBaseNode(); } }
	}

	public partial class Neo4jBaseNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(Neo4jBaseNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(Neo4jBaseNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return null;
		}

		protected override Entity GetEntity()
        {
			return null;
        }

		internal Neo4jBaseNode() { }
		internal Neo4jBaseNode(Neo4jBaseAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal Neo4jBaseNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal Neo4jBaseNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public Neo4jBaseNode Where(JsNotation<string> CreatedBy = default, JsNotation<System.DateTime?> CreatedOn = default, JsNotation<string> Description = default, JsNotation<string> LastModifiedBy = default, JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<Neo4jBaseAlias> alias = new Lazy<Neo4jBaseAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (CreatedBy.HasValue) conditions.Add(new QueryCondition(alias.Value.CreatedBy, Operator.Equals, ((IValue)CreatedBy).GetValue()));
            if (CreatedOn.HasValue) conditions.Add(new QueryCondition(alias.Value.CreatedOn, Operator.Equals, ((IValue)CreatedOn).GetValue()));
            if (Description.HasValue) conditions.Add(new QueryCondition(alias.Value.Description, Operator.Equals, ((IValue)Description).GetValue()));
            if (LastModifiedBy.HasValue) conditions.Add(new QueryCondition(alias.Value.LastModifiedBy, Operator.Equals, ((IValue)LastModifiedBy).GetValue()));
            if (LastModifiedOn.HasValue) conditions.Add(new QueryCondition(alias.Value.LastModifiedOn, Operator.Equals, ((IValue)LastModifiedOn).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public Neo4jBaseNode Assign(JsNotation<string> CreatedBy = default, JsNotation<System.DateTime?> CreatedOn = default, JsNotation<string> Description = default, JsNotation<string> LastModifiedBy = default, JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<Neo4jBaseAlias> alias = new Lazy<Neo4jBaseAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (CreatedBy.HasValue) assignments.Add(new Assignment(alias.Value.CreatedBy, CreatedBy));
            if (CreatedOn.HasValue) assignments.Add(new Assignment(alias.Value.CreatedOn, CreatedOn));
            if (Description.HasValue) assignments.Add(new Assignment(alias.Value.Description, Description));
            if (LastModifiedBy.HasValue) assignments.Add(new Assignment(alias.Value.LastModifiedBy, LastModifiedBy));
            if (LastModifiedOn.HasValue) assignments.Add(new Assignment(alias.Value.LastModifiedOn, LastModifiedOn));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public Neo4jBaseNode Alias(out Neo4jBaseAlias alias)
        {
            if (NodeAlias is Neo4jBaseAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new Neo4jBaseAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public Neo4jBaseNode Alias(out Neo4jBaseAlias alias, string name)
        {
            if (NodeAlias is Neo4jBaseAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new Neo4jBaseAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public Neo4jBaseNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public BranchNode CastToBranch()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new BranchNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public DepartmentNode CastToDepartment()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new DepartmentNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public PersonnelNode CastToPersonnel()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new PersonnelNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public EmployeeNode CastToEmployee()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new EmployeeNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public HeadEmployeeNode CastToHeadEmployee()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new HeadEmployeeNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public EmploymentStatusNode CastToEmploymentStatus()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new EmploymentStatusNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

	}

	public class Neo4jBaseAlias : AliasResult<Neo4jBaseAlias, Neo4jBaseListAlias>
	{
		internal Neo4jBaseAlias(Neo4jBaseNode parent)
		{
			Node = parent;
		}
		internal Neo4jBaseAlias(Neo4jBaseNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  Neo4jBaseAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  Neo4jBaseAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  Neo4jBaseAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> CreatedBy = default, JsNotation<System.DateTime?> CreatedOn = default, JsNotation<string> Description = default, JsNotation<string> LastModifiedBy = default, JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (CreatedBy.HasValue) assignments.Add(new Assignment(this.CreatedBy, CreatedBy));
			if (CreatedOn.HasValue) assignments.Add(new Assignment(this.CreatedOn, CreatedOn));
			if (Description.HasValue) assignments.Add(new Assignment(this.Description, Description));
			if (LastModifiedBy.HasValue) assignments.Add(new Assignment(this.LastModifiedBy, LastModifiedBy));
			if (LastModifiedOn.HasValue) assignments.Add(new Assignment(this.LastModifiedOn, LastModifiedOn));
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
						{ "Uid", new StringResult(this, "Uid", MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["Uid"]) },
						{ "CreatedOn", new DateTimeResult(this, "CreatedOn", MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["CreatedOn"]) },
						{ "CreatedBy", new StringResult(this, "CreatedBy", MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["CreatedBy"]) },
						{ "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["LastModifiedOn"]) },
						{ "LastModifiedBy", new StringResult(this, "LastModifiedBy", MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["LastModifiedBy"]) },
						{ "Description", new StringResult(this, "Description", MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["Description"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;


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
		public AsResult As(string aliasName, out Neo4jBaseAlias alias)
		{
			alias = new Neo4jBaseAlias((Neo4jBaseNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class Neo4jBaseListAlias : ListResult<Neo4jBaseListAlias, Neo4jBaseAlias>, IAliasListResult
	{
		private Neo4jBaseListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private Neo4jBaseListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private Neo4jBaseListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class Neo4jBaseJaggedListAlias : ListResult<Neo4jBaseJaggedListAlias, Neo4jBaseListAlias>, IAliasJaggedListResult
	{
		private Neo4jBaseJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private Neo4jBaseJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private Neo4jBaseJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
