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
		public static BranchNode Branch { get { return new BranchNode(); } }
	}

	public partial class BranchNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(BranchNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(BranchNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Branch";
		}

		protected override Entity GetEntity()
        {
			return m.Branch.Entity;
        }

		internal BranchNode() { }
		internal BranchNode(BranchAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal BranchNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal BranchNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public BranchNode Where(JsNotation<string> CreatedBy = default, JsNotation<System.DateTime?> CreatedOn = default, JsNotation<string> Description = default, JsNotation<string> LastModifiedBy = default, JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<BranchAlias> alias = new Lazy<BranchAlias>(delegate()
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
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public BranchNode Assign(JsNotation<string> CreatedBy = default, JsNotation<System.DateTime?> CreatedOn = default, JsNotation<string> Description = default, JsNotation<string> LastModifiedBy = default, JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<BranchAlias> alias = new Lazy<BranchAlias>(delegate()
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
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public BranchNode Alias(out BranchAlias alias)
        {
            if (NodeAlias is BranchAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new BranchAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public BranchNode Alias(out BranchAlias alias, string name)
        {
            if (NodeAlias is BranchAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new BranchAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public BranchNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public BranchIn  In  { get { return new BranchIn(this); } }
		public class BranchIn
		{
			private BranchNode Parent;
			internal BranchIn(BranchNode parent)
			{
				Parent = parent;
			}
			public IFromIn_BELONGS_TO_REL BELONGS_TO { get { return new BELONGS_TO_REL(Parent, DirectionEnum.In); } }

		}

		public BranchOut Out { get { return new BranchOut(this); } }
		public class BranchOut
		{
			private BranchNode Parent;
			internal BranchOut(BranchNode parent)
			{
				Parent = parent;
			}
			public IFromOut_MANAGES_BRANCH_REL MANAGES_BRANCH { get { return new MANAGES_BRANCH_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class BranchAlias : AliasResult<BranchAlias, BranchListAlias>
	{
		internal BranchAlias(BranchNode parent)
		{
			Node = parent;
		}
		internal BranchAlias(BranchNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  BranchAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  BranchAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  BranchAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> CreatedBy = default, JsNotation<System.DateTime?> CreatedOn = default, JsNotation<string> Description = default, JsNotation<string> LastModifiedBy = default, JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (CreatedBy.HasValue) assignments.Add(new Assignment(this.CreatedBy, CreatedBy));
			if (CreatedOn.HasValue) assignments.Add(new Assignment(this.CreatedOn, CreatedOn));
			if (Description.HasValue) assignments.Add(new Assignment(this.Description, Description));
			if (LastModifiedBy.HasValue) assignments.Add(new Assignment(this.LastModifiedBy, LastModifiedBy));
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
						{ "Name", new StringResult(this, "Name", MemgraphTestApp.HumanResources.Model.Entities["Branch"], MemgraphTestApp.HumanResources.Model.Entities["Branch"].Properties["Name"]) },
						{ "Uid", new StringResult(this, "Uid", MemgraphTestApp.HumanResources.Model.Entities["Branch"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["Uid"]) },
						{ "CreatedOn", new DateTimeResult(this, "CreatedOn", MemgraphTestApp.HumanResources.Model.Entities["Branch"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["CreatedOn"]) },
						{ "CreatedBy", new StringResult(this, "CreatedBy", MemgraphTestApp.HumanResources.Model.Entities["Branch"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["CreatedBy"]) },
						{ "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", MemgraphTestApp.HumanResources.Model.Entities["Branch"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["LastModifiedOn"]) },
						{ "LastModifiedBy", new StringResult(this, "LastModifiedBy", MemgraphTestApp.HumanResources.Model.Entities["Branch"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["LastModifiedBy"]) },
						{ "Description", new StringResult(this, "Description", MemgraphTestApp.HumanResources.Model.Entities["Branch"], MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["Description"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public BranchNode.BranchIn In { get { return new BranchNode.BranchIn(new BranchNode(this, true)); } }
		public BranchNode.BranchOut Out { get { return new BranchNode.BranchOut(new BranchNode(this, true)); } }

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
		public AsResult As(string aliasName, out BranchAlias alias)
		{
			alias = new BranchAlias((BranchNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class BranchListAlias : ListResult<BranchListAlias, BranchAlias>, IAliasListResult
	{
		private BranchListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private BranchListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private BranchListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class BranchJaggedListAlias : ListResult<BranchJaggedListAlias, BranchListAlias>, IAliasJaggedListResult
	{
		private BranchJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private BranchJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private BranchJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
