using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static SalesTerritoryNode SalesTerritory { get { return new SalesTerritoryNode(); } }
	}

	public partial class SalesTerritoryNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "SalesTerritory";
            }
        }

		internal SalesTerritoryNode() { }
		internal SalesTerritoryNode(SalesTerritoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesTerritoryNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public SalesTerritoryNode Alias(out SalesTerritoryAlias alias)
		{
			alias = new SalesTerritoryAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public SalesTerritoryIn  In  { get { return new SalesTerritoryIn(this); } }
		public class SalesTerritoryIn
		{
			private SalesTerritoryNode Parent;
			internal SalesTerritoryIn(SalesTerritoryNode parent)
			{
                Parent = parent;
			}
			public IFromIn_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL SALESTERRITORY_HAS_SALESTERRITORYHISTORY { get { return new SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL(Parent, DirectionEnum.In); } }

		}

		public SalesTerritoryOut Out { get { return new SalesTerritoryOut(this); } }
		public class SalesTerritoryOut
		{
			private SalesTerritoryNode Parent;
			internal SalesTerritoryOut(SalesTerritoryNode parent)
			{
                Parent = parent;
			}
			public IFromOut_CUSTOMER_HAS_SALESTERRITORY_REL CUSTOMER_HAS_SALESTERRITORY { get { return new CUSTOMER_HAS_SALESTERRITORY_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL SALESORDERHEADER_CONTAINS_SALESTERRITORY { get { return new SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SALESPERSON_HAS_SALESTERRITORY_REL SALESPERSON_HAS_SALESTERRITORY { get { return new SALESPERSON_HAS_SALESTERRITORY_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_STATEPROVINCE_HAS_SALESTERRITORY_REL STATEPROVINCE_HAS_SALESTERRITORY { get { return new STATEPROVINCE_HAS_SALESTERRITORY_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class SalesTerritoryAlias : AliasResult
    {
        internal SalesTerritoryAlias(SalesTerritoryNode parent)
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["Name"]) },
						{ "CountryRegionCode", new StringResult(this, "CountryRegionCode", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["CountryRegionCode"]) },
						{ "Group", new StringResult(this, "Group", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["Group"]) },
						{ "SalesYTD", new StringResult(this, "SalesYTD", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["SalesYTD"]) },
						{ "SalesLastYear", new StringResult(this, "SalesLastYear", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["SalesLastYear"]) },
						{ "CostYTD", new StringResult(this, "CostYTD", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["CostYTD"]) },
						{ "CostLastYear", new StringResult(this, "CostLastYear", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["CostLastYear"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public SalesTerritoryNode.SalesTerritoryIn In { get { return new SalesTerritoryNode.SalesTerritoryIn(new SalesTerritoryNode(this, true)); } }
        public SalesTerritoryNode.SalesTerritoryOut Out { get { return new SalesTerritoryNode.SalesTerritoryOut(new SalesTerritoryNode(this, true)); } }

        public StringResult Name
		{
			get
			{
				if ((object)m_Name == null)
					m_Name = (StringResult)AliasFields["Name"];

				return m_Name;
			}
		} 
        private StringResult m_Name = null;
        public StringResult CountryRegionCode
		{
			get
			{
				if ((object)m_CountryRegionCode == null)
					m_CountryRegionCode = (StringResult)AliasFields["CountryRegionCode"];

				return m_CountryRegionCode;
			}
		} 
        private StringResult m_CountryRegionCode = null;
        public StringResult Group
		{
			get
			{
				if ((object)m_Group == null)
					m_Group = (StringResult)AliasFields["Group"];

				return m_Group;
			}
		} 
        private StringResult m_Group = null;
        public StringResult SalesYTD
		{
			get
			{
				if ((object)m_SalesYTD == null)
					m_SalesYTD = (StringResult)AliasFields["SalesYTD"];

				return m_SalesYTD;
			}
		} 
        private StringResult m_SalesYTD = null;
        public StringResult SalesLastYear
		{
			get
			{
				if ((object)m_SalesLastYear == null)
					m_SalesLastYear = (StringResult)AliasFields["SalesLastYear"];

				return m_SalesLastYear;
			}
		} 
        private StringResult m_SalesLastYear = null;
        public StringResult CostYTD
		{
			get
			{
				if ((object)m_CostYTD == null)
					m_CostYTD = (StringResult)AliasFields["CostYTD"];

				return m_CostYTD;
			}
		} 
        private StringResult m_CostYTD = null;
        public StringResult CostLastYear
		{
			get
			{
				if ((object)m_CostLastYear == null)
					m_CostLastYear = (StringResult)AliasFields["CostLastYear"];

				return m_CostLastYear;
			}
		} 
        private StringResult m_CostLastYear = null;
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
