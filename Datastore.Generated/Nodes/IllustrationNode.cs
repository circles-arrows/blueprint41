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
		public static IllustrationNode Illustration { get { return new IllustrationNode(); } }
	}

	public partial class IllustrationNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(IllustrationNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(IllustrationNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Illustration";
		}

		protected override Entity GetEntity()
        {
			return m.Illustration.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.Illustration.Entity.FunctionalId;
            }
        }

		internal IllustrationNode() { }
		internal IllustrationNode(IllustrationAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal IllustrationNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal IllustrationNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public IllustrationNode Where(JsNotation<string> Diagram = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<IllustrationAlias> alias = new Lazy<IllustrationAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Diagram.HasValue) conditions.Add(new QueryCondition(alias.Value.Diagram, Operator.Equals, ((IValue)Diagram).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public IllustrationNode Assign(JsNotation<string> Diagram = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<IllustrationAlias> alias = new Lazy<IllustrationAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Diagram.HasValue) assignments.Add(new Assignment(alias.Value.Diagram, Diagram));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public IllustrationNode Alias(out IllustrationAlias alias)
        {
            if (NodeAlias is IllustrationAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new IllustrationAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public IllustrationNode Alias(out IllustrationAlias alias, string name)
        {
            if (NodeAlias is IllustrationAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new IllustrationAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public IllustrationNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public IllustrationOut Out { get { return new IllustrationOut(this); } }
		public class IllustrationOut
		{
			private IllustrationNode Parent;
			internal IllustrationOut(IllustrationNode parent)
			{
				Parent = parent;
			}
			public IFromOut_PRODUCTMODEL_HAS_ILLUSTRATION_REL PRODUCTMODEL_HAS_ILLUSTRATION { get { return new PRODUCTMODEL_HAS_ILLUSTRATION_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class IllustrationAlias : AliasResult<IllustrationAlias, IllustrationListAlias>
	{
		internal IllustrationAlias(IllustrationNode parent)
		{
			Node = parent;
		}
		internal IllustrationAlias(IllustrationNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  IllustrationAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  IllustrationAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  IllustrationAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> Diagram = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (Diagram.HasValue) assignments.Add(new Assignment(this.Diagram, Diagram));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
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
						{ "Diagram", new StringResult(this, "Diagram", Datastore.AdventureWorks.Model.Entities["Illustration"], Datastore.AdventureWorks.Model.Entities["Illustration"].Properties["Diagram"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Illustration"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Illustration"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public IllustrationNode.IllustrationOut Out { get { return new IllustrationNode.IllustrationOut(new IllustrationNode(this, true)); } }

		public StringResult Diagram
		{
			get
			{
				if (m_Diagram is null)
					m_Diagram = (StringResult)AliasFields["Diagram"];

				return m_Diagram;
			}
		}
		private StringResult m_Diagram = null;
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
		public AsResult As(string aliasName, out IllustrationAlias alias)
		{
			alias = new IllustrationAlias((IllustrationNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class IllustrationListAlias : ListResult<IllustrationListAlias, IllustrationAlias>, IAliasListResult
	{
		private IllustrationListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private IllustrationListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private IllustrationListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class IllustrationJaggedListAlias : ListResult<IllustrationJaggedListAlias, IllustrationListAlias>, IAliasJaggedListResult
	{
		private IllustrationJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private IllustrationJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private IllustrationJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
