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
		public FunctionalId FunctionalId
        {
            get
            {
                return m.Employee.Entity.FunctionalId;
            }
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

        public EmployeeNode Where(JsNotation<string> FirstName = default, JsNotation<string> LastName = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<EmployeeAlias> alias = new Lazy<EmployeeAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (FirstName.HasValue) conditions.Add(new QueryCondition(alias.Value.FirstName, Operator.Equals, ((IValue)FirstName).GetValue()));
            if (LastName.HasValue) conditions.Add(new QueryCondition(alias.Value.LastName, Operator.Equals, ((IValue)LastName).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public EmployeeNode Assign(JsNotation<string> FirstName = default, JsNotation<string> LastName = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<EmployeeAlias> alias = new Lazy<EmployeeAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (FirstName.HasValue) assignments.Add(new Assignment(alias.Value.FirstName, FirstName));
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

		public Assignment[] Assign(JsNotation<string> FirstName = default, JsNotation<string> LastName = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (FirstName.HasValue) assignments.Add(new Assignment(this.FirstName, FirstName));
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
						{ "Uid", new StringResult(this, "Uid", MemgraphTestApp.HumanResources.Model.Entities["Employee"], MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["Uid"]) },
						{ "FirstName", new StringResult(this, "FirstName", MemgraphTestApp.HumanResources.Model.Entities["Employee"], MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["FirstName"]) },
						{ "LastName", new StringResult(this, "LastName", MemgraphTestApp.HumanResources.Model.Entities["Employee"], MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["LastName"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public EmployeeNode.EmployeeIn In { get { return new EmployeeNode.EmployeeIn(new EmployeeNode(this, true)); } }

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
