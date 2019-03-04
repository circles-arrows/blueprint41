using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static SalesPersonQuotaHistoryNode SalesPersonQuotaHistory { get { return new SalesPersonQuotaHistoryNode(); } }
	}

	public partial class SalesPersonQuotaHistoryNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "SalesPersonQuotaHistory";
        }

		internal SalesPersonQuotaHistoryNode() { }
		internal SalesPersonQuotaHistoryNode(SalesPersonQuotaHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesPersonQuotaHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public SalesPersonQuotaHistoryNode Alias(out SalesPersonQuotaHistoryAlias alias)
		{
			alias = new SalesPersonQuotaHistoryAlias(this);
            NodeAlias = alias;
			return this;
		}

		public SalesPersonQuotaHistoryNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
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

    public class SalesPersonQuotaHistoryAlias : AliasResult
    {
        internal SalesPersonQuotaHistoryAlias(SalesPersonQuotaHistoryNode parent)
        {
			Node = parent;
        }

        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields == null)
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
				if ((object)m_QuotaDate == null)
					m_QuotaDate = (DateTimeResult)AliasFields["QuotaDate"];

				return m_QuotaDate;
			}
		} 
        private DateTimeResult m_QuotaDate = null;
        public StringResult SalesQuota
		{
			get
			{
				if ((object)m_SalesQuota == null)
					m_SalesQuota = (StringResult)AliasFields["SalesQuota"];

				return m_SalesQuota;
			}
		} 
        private StringResult m_SalesQuota = null;
        public StringResult rowguid
		{
			get
			{
				if ((object)m_rowguid == null)
					m_rowguid = (StringResult)AliasFields["rowguid"];

				return m_rowguid;
			}
		} 
        private StringResult m_rowguid = null;
        public DateTimeResult ModifiedDate
		{
			get
			{
				if ((object)m_ModifiedDate == null)
					m_ModifiedDate = (DateTimeResult)AliasFields["ModifiedDate"];

				return m_ModifiedDate;
			}
		} 
        private DateTimeResult m_ModifiedDate = null;
        public StringResult Uid
		{
			get
			{
				if ((object)m_Uid == null)
					m_Uid = (StringResult)AliasFields["Uid"];

				return m_Uid;
			}
		} 
        private StringResult m_Uid = null;
    }
}
