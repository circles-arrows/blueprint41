using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static SalesTerritoryHistoryNode SalesTerritoryHistory { get { return new SalesTerritoryHistoryNode(); } }
	}

	public partial class SalesTerritoryHistoryNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "SalesTerritoryHistory";
        }

		internal SalesTerritoryHistoryNode() { }
		internal SalesTerritoryHistoryNode(SalesTerritoryHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesTerritoryHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public SalesTerritoryHistoryNode Alias(out SalesTerritoryHistoryAlias alias)
		{
			alias = new SalesTerritoryHistoryAlias(this);
            NodeAlias = alias;
			return this;
		}

		public SalesTerritoryHistoryNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
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

    public class SalesTerritoryHistoryAlias : AliasResult
    {
        internal SalesTerritoryHistoryAlias(SalesTerritoryHistoryNode parent)
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
				if ((object)m_StartDate == null)
					m_StartDate = (DateTimeResult)AliasFields["StartDate"];

				return m_StartDate;
			}
		} 
        private DateTimeResult m_StartDate = null;
        public DateTimeResult EndDate
		{
			get
			{
				if ((object)m_EndDate == null)
					m_EndDate = (DateTimeResult)AliasFields["EndDate"];

				return m_EndDate;
			}
		} 
        private DateTimeResult m_EndDate = null;
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
