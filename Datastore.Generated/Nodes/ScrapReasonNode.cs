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
		public static ScrapReasonNode ScrapReason { get { return new ScrapReasonNode(); } }
	}

	public partial class ScrapReasonNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(ScrapReasonNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(ScrapReasonNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "ScrapReason";
		}

		protected override Entity GetEntity()
        {
			return m.ScrapReason.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.ScrapReason.Entity.FunctionalId;
            }
        }

		internal ScrapReasonNode() { }
		internal ScrapReasonNode(ScrapReasonAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ScrapReasonNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal ScrapReasonNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public ScrapReasonNode Where(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ScrapReasonAlias> alias = new Lazy<ScrapReasonAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public ScrapReasonNode Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ScrapReasonAlias> alias = new Lazy<ScrapReasonAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public ScrapReasonNode Alias(out ScrapReasonAlias alias)
        {
            if (NodeAlias is ScrapReasonAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new ScrapReasonAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public ScrapReasonNode Alias(out ScrapReasonAlias alias, string name)
        {
            if (NodeAlias is ScrapReasonAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new ScrapReasonAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public ScrapReasonNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public ScrapReasonOut Out { get { return new ScrapReasonOut(this); } }
		public class ScrapReasonOut
		{
			private ScrapReasonNode Parent;
			internal ScrapReasonOut(ScrapReasonNode parent)
			{
				Parent = parent;
			}
			public IFromOut_WORKORDER_HAS_SCRAPREASON_REL WORKORDER_HAS_SCRAPREASON { get { return new WORKORDER_HAS_SCRAPREASON_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class ScrapReasonAlias : AliasResult<ScrapReasonAlias, ScrapReasonListAlias>
	{
		internal ScrapReasonAlias(ScrapReasonNode parent)
		{
			Node = parent;
		}
		internal ScrapReasonAlias(ScrapReasonNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  ScrapReasonAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  ScrapReasonAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  ScrapReasonAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["ScrapReason"], Datastore.AdventureWorks.Model.Entities["ScrapReason"].Properties["Name"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ScrapReason"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ScrapReason"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public ScrapReasonNode.ScrapReasonOut Out { get { return new ScrapReasonNode.ScrapReasonOut(new ScrapReasonNode(this, true)); } }

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
		public AsResult As(string aliasName, out ScrapReasonAlias alias)
		{
			alias = new ScrapReasonAlias((ScrapReasonNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class ScrapReasonListAlias : ListResult<ScrapReasonListAlias, ScrapReasonAlias>, IAliasListResult
	{
		private ScrapReasonListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ScrapReasonListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ScrapReasonListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class ScrapReasonJaggedListAlias : ListResult<ScrapReasonJaggedListAlias, ScrapReasonListAlias>, IAliasJaggedListResult
	{
		private ScrapReasonJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ScrapReasonJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ScrapReasonJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
