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
		public static SalesTerritoryHistoryNode SalesTerritoryHistory { get { return new SalesTerritoryHistoryNode(); } }
	}

	public partial class SalesTerritoryHistoryNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(SalesTerritoryHistoryNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(SalesTerritoryHistoryNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "SalesTerritoryHistory";
		}

		protected override Entity GetEntity()
        {
			return m.SalesTerritoryHistory.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.SalesTerritoryHistory.Entity.FunctionalId;
            }
        }

		internal SalesTerritoryHistoryNode() { }
		internal SalesTerritoryHistoryNode(SalesTerritoryHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesTerritoryHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal SalesTerritoryHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public SalesTerritoryHistoryNode Where(JsNotation<System.DateTime?> EndDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> rowguid = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SalesTerritoryHistoryAlias> alias = new Lazy<SalesTerritoryHistoryAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (EndDate.HasValue) conditions.Add(new QueryCondition(alias.Value.EndDate, Operator.Equals, ((IValue)EndDate).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (StartDate.HasValue) conditions.Add(new QueryCondition(alias.Value.StartDate, Operator.Equals, ((IValue)StartDate).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public SalesTerritoryHistoryNode Assign(JsNotation<System.DateTime?> EndDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> rowguid = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SalesTerritoryHistoryAlias> alias = new Lazy<SalesTerritoryHistoryAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (EndDate.HasValue) assignments.Add(new Assignment(alias.Value.EndDate, EndDate));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (StartDate.HasValue) assignments.Add(new Assignment(alias.Value.StartDate, StartDate));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public SalesTerritoryHistoryNode Alias(out SalesTerritoryHistoryAlias alias)
        {
            if (NodeAlias is SalesTerritoryHistoryAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new SalesTerritoryHistoryAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public SalesTerritoryHistoryNode Alias(out SalesTerritoryHistoryAlias alias, string name)
        {
            if (NodeAlias is SalesTerritoryHistoryAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new SalesTerritoryHistoryAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public SalesTerritoryHistoryNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public SalesTerritoryHistoryOut Out { get { return new SalesTerritoryHistoryOut(this); } }
		public class SalesTerritoryHistoryOut
		{
			private SalesTerritoryHistoryNode Parent;
			internal SalesTerritoryHistoryOut(SalesTerritoryHistoryNode parent)
			{
				Parent = parent;
			}
			public IFromOut_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL SALESTERRITORY_HAS_SALESTERRITORYHISTORY { get { return new SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class SalesTerritoryHistoryAlias : AliasResult<SalesTerritoryHistoryAlias, SalesTerritoryHistoryListAlias>
	{
		internal SalesTerritoryHistoryAlias(SalesTerritoryHistoryNode parent)
		{
			Node = parent;
		}
		internal SalesTerritoryHistoryAlias(SalesTerritoryHistoryNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  SalesTerritoryHistoryAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  SalesTerritoryHistoryAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  SalesTerritoryHistoryAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime?> EndDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> rowguid = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (EndDate.HasValue) assignments.Add(new Assignment(this.EndDate, EndDate));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
			if (StartDate.HasValue) assignments.Add(new Assignment(this.StartDate, StartDate));
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
						{ "StartDate", new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"], Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"].Properties["StartDate"]) },
						{ "EndDate", new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"], Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"].Properties["EndDate"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"], Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public SalesTerritoryHistoryNode.SalesTerritoryHistoryOut Out { get { return new SalesTerritoryHistoryNode.SalesTerritoryHistoryOut(new SalesTerritoryHistoryNode(this, true)); } }

		public DateTimeResult StartDate
		{
			get
			{
				if (m_StartDate is null)
					m_StartDate = (DateTimeResult)AliasFields["StartDate"];

				return m_StartDate;
			}
		}
		private DateTimeResult m_StartDate = null;
		public DateTimeResult EndDate
		{
			get
			{
				if (m_EndDate is null)
					m_EndDate = (DateTimeResult)AliasFields["EndDate"];

				return m_EndDate;
			}
		}
		private DateTimeResult m_EndDate = null;
		public StringResult rowguid
		{
			get
			{
				if (m_rowguid is null)
					m_rowguid = (StringResult)AliasFields["rowguid"];

				return m_rowguid;
			}
		}
		private StringResult m_rowguid = null;
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
		public AsResult As(string aliasName, out SalesTerritoryHistoryAlias alias)
		{
			alias = new SalesTerritoryHistoryAlias((SalesTerritoryHistoryNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class SalesTerritoryHistoryListAlias : ListResult<SalesTerritoryHistoryListAlias, SalesTerritoryHistoryAlias>, IAliasListResult
	{
		private SalesTerritoryHistoryListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SalesTerritoryHistoryListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SalesTerritoryHistoryListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class SalesTerritoryHistoryJaggedListAlias : ListResult<SalesTerritoryHistoryJaggedListAlias, SalesTerritoryHistoryListAlias>, IAliasJaggedListResult
	{
		private SalesTerritoryHistoryJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SalesTerritoryHistoryJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SalesTerritoryHistoryJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
