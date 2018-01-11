using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static SalesPersonNode SalesPerson { get { return new SalesPersonNode(); } }
	}

	public partial class SalesPersonNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "SalesPerson";
            }
        }

		internal SalesPersonNode() { }
		internal SalesPersonNode(SalesPersonAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesPersonNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public SalesPersonNode Alias(out SalesPersonAlias alias)
		{
			alias = new SalesPersonAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public SalesPersonIn  In  { get { return new SalesPersonIn(this); } }
		public class SalesPersonIn
		{
			private SalesPersonNode Parent;
			internal SalesPersonIn(SalesPersonNode parent)
			{
                Parent = parent;
			}
			public IFromIn_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL SALESPERSON_HAS_SALESPERSONQUOTAHISTORY { get { return new SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL(Parent, DirectionEnum.In); } }
			public IFromIn_SALESPERSON_HAS_SALESTERRITORY_REL SALESPERSON_HAS_SALESTERRITORY { get { return new SALESPERSON_HAS_SALESTERRITORY_REL(Parent, DirectionEnum.In); } }
			public IFromIn_SALESPERSON_IS_PERSON_REL SALESPERSON_IS_PERSON { get { return new SALESPERSON_IS_PERSON_REL(Parent, DirectionEnum.In); } }

		}

		public SalesPersonOut Out { get { return new SalesPersonOut(this); } }
		public class SalesPersonOut
		{
			private SalesPersonNode Parent;
			internal SalesPersonOut(SalesPersonNode parent)
			{
                Parent = parent;
			}
			public IFromOut_EMPLOYEE_BECOMES_SALESPERSON_REL EMPLOYEE_BECOMES_SALESPERSON { get { return new EMPLOYEE_BECOMES_SALESPERSON_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_STORE_VALID_FOR_SALESPERSON_REL STORE_VALID_FOR_SALESPERSON { get { return new STORE_VALID_FOR_SALESPERSON_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class SalesPersonAlias : AliasResult
    {
        internal SalesPersonAlias(SalesPersonNode parent)
        {
			Node = parent;
            SalesQuota = new StringResult(this, "SalesQuota", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["SalesQuota"]);
            Bonus = new StringResult(this, "Bonus", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["Bonus"]);
            CommissionPct = new StringResult(this, "CommissionPct", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["CommissionPct"]);
            SalesYTD = new StringResult(this, "SalesYTD", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["SalesYTD"]);
            SalesLastYear = new StringResult(this, "SalesLastYear", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["SalesLastYear"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public SalesPersonNode.SalesPersonIn In { get { return new SalesPersonNode.SalesPersonIn(new SalesPersonNode(this, true)); } }
        public SalesPersonNode.SalesPersonOut Out { get { return new SalesPersonNode.SalesPersonOut(new SalesPersonNode(this, true)); } }

        public StringResult SalesQuota { get; private set; } 
        public StringResult Bonus { get; private set; } 
        public StringResult CommissionPct { get; private set; } 
        public StringResult SalesYTD { get; private set; } 
        public StringResult SalesLastYear { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
