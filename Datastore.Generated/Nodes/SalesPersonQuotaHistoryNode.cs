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
		public static SalesPersonQuotaHistoryNode SalesPersonQuotaHistory { get { return new SalesPersonQuotaHistoryNode(); } }
	}

	public partial class SalesPersonQuotaHistoryNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(SalesPersonQuotaHistoryNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(SalesPersonQuotaHistoryNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "SalesPersonQuotaHistory";
		}

		protected override Entity GetEntity()
        {
			return m.SalesPersonQuotaHistory.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.SalesPersonQuotaHistory.Entity.FunctionalId;
            }
        }

		internal SalesPersonQuotaHistoryNode() { }
		internal SalesPersonQuotaHistoryNode(SalesPersonQuotaHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesPersonQuotaHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal SalesPersonQuotaHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public SalesPersonQuotaHistoryNode Where(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<System.DateTime> QuotaDate = default, JsNotation<string> rowguid = default, JsNotation<string> SalesQuota = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SalesPersonQuotaHistoryAlias> alias = new Lazy<SalesPersonQuotaHistoryAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (QuotaDate.HasValue) conditions.Add(new QueryCondition(alias.Value.QuotaDate, Operator.Equals, ((IValue)QuotaDate).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (SalesQuota.HasValue) conditions.Add(new QueryCondition(alias.Value.SalesQuota, Operator.Equals, ((IValue)SalesQuota).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public SalesPersonQuotaHistoryNode Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<System.DateTime> QuotaDate = default, JsNotation<string> rowguid = default, JsNotation<string> SalesQuota = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SalesPersonQuotaHistoryAlias> alias = new Lazy<SalesPersonQuotaHistoryAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (QuotaDate.HasValue) assignments.Add(new Assignment(alias.Value.QuotaDate, QuotaDate));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (SalesQuota.HasValue) assignments.Add(new Assignment(alias.Value.SalesQuota, SalesQuota));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public SalesPersonQuotaHistoryNode Alias(out SalesPersonQuotaHistoryAlias alias)
        {
            if (NodeAlias is SalesPersonQuotaHistoryAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new SalesPersonQuotaHistoryAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public SalesPersonQuotaHistoryNode Alias(out SalesPersonQuotaHistoryAlias alias, string name)
        {
            if (NodeAlias is SalesPersonQuotaHistoryAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new SalesPersonQuotaHistoryAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public SalesPersonQuotaHistoryNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public SalesPersonQuotaHistoryOut Out { get { return new SalesPersonQuotaHistoryOut(this); } }
		public class SalesPersonQuotaHistoryOut
		{
			private SalesPersonQuotaHistoryNode Parent;
			internal SalesPersonQuotaHistoryOut(SalesPersonQuotaHistoryNode parent)
			{
				Parent = parent;
			}
			public IFromOut_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL SALESPERSON_HAS_SALESPERSONQUOTAHISTORY { get { return new SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class SalesPersonQuotaHistoryAlias : AliasResult<SalesPersonQuotaHistoryAlias, SalesPersonQuotaHistoryListAlias>
	{
		internal SalesPersonQuotaHistoryAlias(SalesPersonQuotaHistoryNode parent)
		{
			Node = parent;
		}
		internal SalesPersonQuotaHistoryAlias(SalesPersonQuotaHistoryNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  SalesPersonQuotaHistoryAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  SalesPersonQuotaHistoryAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  SalesPersonQuotaHistoryAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<System.DateTime> QuotaDate = default, JsNotation<string> rowguid = default, JsNotation<string> SalesQuota = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (QuotaDate.HasValue) assignments.Add(new Assignment(this.QuotaDate, QuotaDate));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
			if (SalesQuota.HasValue) assignments.Add(new Assignment(this.SalesQuota, SalesQuota));
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
						{ "QuotaDate", new DateTimeResult(this, "QuotaDate", Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"], Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"].Properties["QuotaDate"]) },
						{ "SalesQuota", new StringResult(this, "SalesQuota", Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"], Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"].Properties["SalesQuota"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"], Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public SalesPersonQuotaHistoryNode.SalesPersonQuotaHistoryOut Out { get { return new SalesPersonQuotaHistoryNode.SalesPersonQuotaHistoryOut(new SalesPersonQuotaHistoryNode(this, true)); } }

		public DateTimeResult QuotaDate
		{
			get
			{
				if (m_QuotaDate is null)
					m_QuotaDate = (DateTimeResult)AliasFields["QuotaDate"];

				return m_QuotaDate;
			}
		}
		private DateTimeResult m_QuotaDate = null;
		public StringResult SalesQuota
		{
			get
			{
				if (m_SalesQuota is null)
					m_SalesQuota = (StringResult)AliasFields["SalesQuota"];

				return m_SalesQuota;
			}
		}
		private StringResult m_SalesQuota = null;
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
		public AsResult As(string aliasName, out SalesPersonQuotaHistoryAlias alias)
		{
			alias = new SalesPersonQuotaHistoryAlias((SalesPersonQuotaHistoryNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class SalesPersonQuotaHistoryListAlias : ListResult<SalesPersonQuotaHistoryListAlias, SalesPersonQuotaHistoryAlias>, IAliasListResult
	{
		private SalesPersonQuotaHistoryListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SalesPersonQuotaHistoryListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SalesPersonQuotaHistoryListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class SalesPersonQuotaHistoryJaggedListAlias : ListResult<SalesPersonQuotaHistoryJaggedListAlias, SalesPersonQuotaHistoryListAlias>, IAliasJaggedListResult
	{
		private SalesPersonQuotaHistoryJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SalesPersonQuotaHistoryJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SalesPersonQuotaHistoryJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
