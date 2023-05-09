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
		public static PasswordNode Password { get { return new PasswordNode(); } }
	}

	public partial class PasswordNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(PasswordNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(PasswordNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Password";
		}

		protected override Entity GetEntity()
        {
			return m.Password.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.Password.Entity.FunctionalId;
            }
        }

		internal PasswordNode() { }
		internal PasswordNode(PasswordAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal PasswordNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal PasswordNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public PasswordNode Where(JsNotation<string> PasswordHash = default, JsNotation<string> PasswordSalt = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<PasswordAlias> alias = new Lazy<PasswordAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (PasswordHash.HasValue) conditions.Add(new QueryCondition(alias.Value.PasswordHash, Operator.Equals, ((IValue)PasswordHash).GetValue()));
            if (PasswordSalt.HasValue) conditions.Add(new QueryCondition(alias.Value.PasswordSalt, Operator.Equals, ((IValue)PasswordSalt).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public PasswordNode Assign(JsNotation<string> PasswordHash = default, JsNotation<string> PasswordSalt = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<PasswordAlias> alias = new Lazy<PasswordAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (PasswordHash.HasValue) assignments.Add(new Assignment(alias.Value.PasswordHash, PasswordHash));
            if (PasswordSalt.HasValue) assignments.Add(new Assignment(alias.Value.PasswordSalt, PasswordSalt));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public PasswordNode Alias(out PasswordAlias alias)
        {
            if (NodeAlias is PasswordAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new PasswordAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public PasswordNode Alias(out PasswordAlias alias, string name)
        {
            if (NodeAlias is PasswordAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new PasswordAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public PasswordNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public PasswordOut Out { get { return new PasswordOut(this); } }
		public class PasswordOut
		{
			private PasswordNode Parent;
			internal PasswordOut(PasswordNode parent)
			{
				Parent = parent;
			}
			public IFromOut_PERSON_HAS_PASSWORD_REL PERSON_HAS_PASSWORD { get { return new PERSON_HAS_PASSWORD_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class PasswordAlias : AliasResult<PasswordAlias, PasswordListAlias>
	{
		internal PasswordAlias(PasswordNode parent)
		{
			Node = parent;
		}
		internal PasswordAlias(PasswordNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  PasswordAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  PasswordAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  PasswordAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> PasswordHash = default, JsNotation<string> PasswordSalt = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (PasswordHash.HasValue) assignments.Add(new Assignment(this.PasswordHash, PasswordHash));
			if (PasswordSalt.HasValue) assignments.Add(new Assignment(this.PasswordSalt, PasswordSalt));
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
						{ "PasswordHash", new StringResult(this, "PasswordHash", Datastore.AdventureWorks.Model.Entities["Password"], Datastore.AdventureWorks.Model.Entities["Password"].Properties["PasswordHash"]) },
						{ "PasswordSalt", new StringResult(this, "PasswordSalt", Datastore.AdventureWorks.Model.Entities["Password"], Datastore.AdventureWorks.Model.Entities["Password"].Properties["PasswordSalt"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Password"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public PasswordNode.PasswordOut Out { get { return new PasswordNode.PasswordOut(new PasswordNode(this, true)); } }

		public StringResult PasswordHash
		{
			get
			{
				if (m_PasswordHash is null)
					m_PasswordHash = (StringResult)AliasFields["PasswordHash"];

				return m_PasswordHash;
			}
		}
		private StringResult m_PasswordHash = null;
		public StringResult PasswordSalt
		{
			get
			{
				if (m_PasswordSalt is null)
					m_PasswordSalt = (StringResult)AliasFields["PasswordSalt"];

				return m_PasswordSalt;
			}
		}
		private StringResult m_PasswordSalt = null;
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
		public AsResult As(string aliasName, out PasswordAlias alias)
		{
			alias = new PasswordAlias((PasswordNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class PasswordListAlias : ListResult<PasswordListAlias, PasswordAlias>, IAliasListResult
	{
		private PasswordListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private PasswordListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private PasswordListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class PasswordJaggedListAlias : ListResult<PasswordJaggedListAlias, PasswordListAlias>, IAliasJaggedListResult
	{
		private PasswordJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private PasswordJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private PasswordJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
