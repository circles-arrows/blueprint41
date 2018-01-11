using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static SpecialOfferNode SpecialOffer { get { return new SpecialOfferNode(); } }
	}

	public partial class SpecialOfferNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "SpecialOffer";
            }
        }

		internal SpecialOfferNode() { }
		internal SpecialOfferNode(SpecialOfferAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SpecialOfferNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public SpecialOfferNode Alias(out SpecialOfferAlias alias)
		{
			alias = new SpecialOfferAlias(this);
            NodeAlias = alias;
			return this;
		}


		public SpecialOfferOut Out { get { return new SpecialOfferOut(this); } }
		public class SpecialOfferOut
		{
			private SpecialOfferNode Parent;
			internal SpecialOfferOut(SpecialOfferNode parent)
			{
                Parent = parent;
			}
			public IFromOut_SALESORDERDETAIL_HAS_SPECIALOFFER_REL SALESORDERDETAIL_HAS_SPECIALOFFER { get { return new SALESORDERDETAIL_HAS_SPECIALOFFER_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class SpecialOfferAlias : AliasResult
    {
        internal SpecialOfferAlias(SpecialOfferNode parent)
        {
			Node = parent;
            Description = new StringResult(this, "Description", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["Description"]);
            DiscountPct = new StringResult(this, "DiscountPct", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["DiscountPct"]);
            Type = new StringResult(this, "Type", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["Type"]);
            Category = new StringResult(this, "Category", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["Category"]);
            StartDate = new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["StartDate"]);
            EndDate = new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["EndDate"]);
            MinQty = new NumericResult(this, "MinQty", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["MinQty"]);
            MaxQty = new StringResult(this, "MaxQty", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["MaxQty"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public SpecialOfferNode.SpecialOfferOut Out { get { return new SpecialOfferNode.SpecialOfferOut(new SpecialOfferNode(this, true)); } }

        public StringResult Description { get; private set; } 
        public StringResult DiscountPct { get; private set; } 
        public StringResult Type { get; private set; } 
        public StringResult Category { get; private set; } 
        public DateTimeResult StartDate { get; private set; } 
        public DateTimeResult EndDate { get; private set; } 
        public NumericResult MinQty { get; private set; } 
        public StringResult MaxQty { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
