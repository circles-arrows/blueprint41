using System;
using Blueprint41;
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
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["Name"]);
            CountryRegionCode = new StringResult(this, "CountryRegionCode", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["CountryRegionCode"]);
            Group = new StringResult(this, "Group", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["Group"]);
            SalesYTD = new StringResult(this, "SalesYTD", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["SalesYTD"]);
            SalesLastYear = new StringResult(this, "SalesLastYear", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["SalesLastYear"]);
            CostYTD = new StringResult(this, "CostYTD", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["CostYTD"]);
            CostLastYear = new StringResult(this, "CostLastYear", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["CostLastYear"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public SalesTerritoryNode.SalesTerritoryIn In { get { return new SalesTerritoryNode.SalesTerritoryIn(new SalesTerritoryNode(this, true)); } }
        public SalesTerritoryNode.SalesTerritoryOut Out { get { return new SalesTerritoryNode.SalesTerritoryOut(new SalesTerritoryNode(this, true)); } }

        public StringResult Name { get; private set; } 
        public StringResult CountryRegionCode { get; private set; } 
        public StringResult Group { get; private set; } 
        public StringResult SalesYTD { get; private set; } 
        public StringResult SalesLastYear { get; private set; } 
        public StringResult CostYTD { get; private set; } 
        public StringResult CostLastYear { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
