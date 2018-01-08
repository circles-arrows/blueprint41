
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static SalesTaxRateNode SalesTaxRate { get { return new SalesTaxRateNode(); } }
	}

	public partial class SalesTaxRateNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "SalesTaxRate";
            }
        }

		internal SalesTaxRateNode() { }
		internal SalesTaxRateNode(SalesTaxRateAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesTaxRateNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public SalesTaxRateNode Alias(out SalesTaxRateAlias alias)
		{
			alias = new SalesTaxRateAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public SalesTaxRateIn  In  { get { return new SalesTaxRateIn(this); } }
		public class SalesTaxRateIn
		{
			private SalesTaxRateNode Parent;
			internal SalesTaxRateIn(SalesTaxRateNode parent)
			{
                Parent = parent;
			}
			public IFromIn_SALESTAXRATE_HAS_STATEPROVINCE_REL SALESTAXRATE_HAS_STATEPROVINCE { get { return new SALESTAXRATE_HAS_STATEPROVINCE_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class SalesTaxRateAlias : AliasResult
    {
        internal SalesTaxRateAlias(SalesTaxRateNode parent)
        {
			Node = parent;
            TaxType = new StringResult(this, "TaxType", Datastore.AdventureWorks.Model.Entities["SalesTaxRate"], Datastore.AdventureWorks.Model.Entities["SalesTaxRate"].Properties["TaxType"]);
            TaxRate = new StringResult(this, "TaxRate", Datastore.AdventureWorks.Model.Entities["SalesTaxRate"], Datastore.AdventureWorks.Model.Entities["SalesTaxRate"].Properties["TaxRate"]);
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["SalesTaxRate"], Datastore.AdventureWorks.Model.Entities["SalesTaxRate"].Properties["Name"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesTaxRate"], Datastore.AdventureWorks.Model.Entities["SalesTaxRate"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesTaxRate"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesTaxRate"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public SalesTaxRateNode.SalesTaxRateIn In { get { return new SalesTaxRateNode.SalesTaxRateIn(new SalesTaxRateNode(this, true)); } }

        public StringResult TaxType { get; private set; } 
        public StringResult TaxRate { get; private set; } 
        public StringResult Name { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
