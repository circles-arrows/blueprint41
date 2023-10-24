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
		public static PersonnelNode Personnel { get { return new PersonnelNode(); } }
	}

	public partial class PersonnelNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(PersonnelNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(PersonnelNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Personnel";
		}

		protected override Entity GetEntity()
        {
			return m.Personnel.Entity;
        }

		internal PersonnelNode() { }
		internal PersonnelNode(PersonnelAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal PersonnelNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal PersonnelNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public PersonnelNode Where(JsNotation<string> FirstName = default, JsNotation<string> LastName = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<PersonnelAlias> alias = new Lazy<PersonnelAlias>(delegate()
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
        public PersonnelNode Assign(JsNotation<string> FirstName = default, JsNotation<string> LastName = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<PersonnelAlias> alias = new Lazy<PersonnelAlias>(delegate()
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

		public PersonnelNode Alias(out PersonnelAlias alias)
        {
            if (NodeAlias is PersonnelAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new PersonnelAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public PersonnelNode Alias(out PersonnelAlias alias, string name)
        {
            if (NodeAlias is PersonnelAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new PersonnelAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public PersonnelNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
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

		public PersonnelIn  In  { get { return new PersonnelIn(this); } }
		public class PersonnelIn
		{
			private PersonnelNode Parent;
			internal PersonnelIn(PersonnelNode parent)
			{
				Parent = parent;
			}
			public IFromIn_HAS_STATUS_REL HAS_STATUS { get { return new HAS_STATUS_REL(Parent, DirectionEnum.In); } }

		}
	}

	public class PersonnelAlias : AliasResult<PersonnelAlias, PersonnelListAlias>
	{
		internal PersonnelAlias(PersonnelNode parent)
		{
			Node = parent;
		}
		internal PersonnelAlias(PersonnelNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  PersonnelAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  PersonnelAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  PersonnelAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
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
						{ "Uid", new StringResult(this, "Uid", MemgraphTestApp.HumanResources.Model.Entities["Personnel"], MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["Uid"]) },
						{ "FirstName", new StringResult(this, "FirstName", MemgraphTestApp.HumanResources.Model.Entities["Personnel"], MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["FirstName"]) },
						{ "LastName", new StringResult(this, "LastName", MemgraphTestApp.HumanResources.Model.Entities["Personnel"], MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["LastName"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public PersonnelNode.PersonnelIn In { get { return new PersonnelNode.PersonnelIn(new PersonnelNode(this, true)); } }

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
		public AsResult As(string aliasName, out PersonnelAlias alias)
		{
			alias = new PersonnelAlias((PersonnelNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class PersonnelListAlias : ListResult<PersonnelListAlias, PersonnelAlias>, IAliasListResult
	{
		private PersonnelListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private PersonnelListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private PersonnelListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class PersonnelJaggedListAlias : ListResult<PersonnelJaggedListAlias, PersonnelListAlias>, IAliasJaggedListResult
	{
		private PersonnelJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private PersonnelJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private PersonnelJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
