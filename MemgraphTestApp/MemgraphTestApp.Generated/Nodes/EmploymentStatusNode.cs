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
		public static EmploymentStatusNode EmploymentStatus { get { return new EmploymentStatusNode(); } }
	}

	public partial class EmploymentStatusNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(EmploymentStatusNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(EmploymentStatusNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "EmploymentStatus";
		}

		protected override Entity GetEntity()
        {
			return m.EmploymentStatus.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.EmploymentStatus.Entity.FunctionalId;
            }
        }

		internal EmploymentStatusNode() { }
		internal EmploymentStatusNode(EmploymentStatusAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal EmploymentStatusNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal EmploymentStatusNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public EmploymentStatusNode Where(JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<EmploymentStatusAlias> alias = new Lazy<EmploymentStatusAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public EmploymentStatusNode Assign(JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<EmploymentStatusAlias> alias = new Lazy<EmploymentStatusAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public EmploymentStatusNode Alias(out EmploymentStatusAlias alias)
        {
            if (NodeAlias is EmploymentStatusAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new EmploymentStatusAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public EmploymentStatusNode Alias(out EmploymentStatusAlias alias, string name)
        {
            if (NodeAlias is EmploymentStatusAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new EmploymentStatusAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public EmploymentStatusNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public EmploymentStatusOut Out { get { return new EmploymentStatusOut(this); } }
		public class EmploymentStatusOut
		{
			private EmploymentStatusNode Parent;
			internal EmploymentStatusOut(EmploymentStatusNode parent)
			{
				Parent = parent;
			}
			public IFromOut_HAS_STATUS_REL HAS_STATUS { get { return new HAS_STATUS_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class EmploymentStatusAlias : AliasResult<EmploymentStatusAlias, EmploymentStatusListAlias>
	{
		internal EmploymentStatusAlias(EmploymentStatusNode parent)
		{
			Node = parent;
		}
		internal EmploymentStatusAlias(EmploymentStatusNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  EmploymentStatusAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  EmploymentStatusAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  EmploymentStatusAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
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
						{ "Uid", new StringResult(this, "Uid", MemgraphTestApp.HumanResources.Model.Entities["EmploymentStatus"], MemgraphTestApp.HumanResources.Model.Entities["EmploymentStatus"].Properties["Uid"]) },
						{ "Name", new StringResult(this, "Name", MemgraphTestApp.HumanResources.Model.Entities["EmploymentStatus"], MemgraphTestApp.HumanResources.Model.Entities["EmploymentStatus"].Properties["Name"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public EmploymentStatusNode.EmploymentStatusOut Out { get { return new EmploymentStatusNode.EmploymentStatusOut(new EmploymentStatusNode(this, true)); } }

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
		public AsResult As(string aliasName, out EmploymentStatusAlias alias)
		{
			alias = new EmploymentStatusAlias((EmploymentStatusNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class EmploymentStatusListAlias : ListResult<EmploymentStatusListAlias, EmploymentStatusAlias>, IAliasListResult
	{
		private EmploymentStatusListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private EmploymentStatusListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private EmploymentStatusListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class EmploymentStatusJaggedListAlias : ListResult<EmploymentStatusJaggedListAlias, EmploymentStatusListAlias>, IAliasJaggedListResult
	{
		private EmploymentStatusJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private EmploymentStatusJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private EmploymentStatusJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
