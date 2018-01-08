
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static VendorNode Vendor { get { return new VendorNode(); } }
	}

	public partial class VendorNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Vendor";
            }
        }

		internal VendorNode() { }
		internal VendorNode(VendorAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal VendorNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public VendorNode Alias(out VendorAlias alias)
		{
			alias = new VendorAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public VendorIn  In  { get { return new VendorIn(this); } }
		public class VendorIn
		{
			private VendorNode Parent;
			internal VendorIn(VendorNode parent)
			{
                Parent = parent;
			}
			public IFromIn_VENDOR_BECOMES_PRODUCTVENDOR_REL VENDOR_BECOMES_PRODUCTVENDOR { get { return new VENDOR_BECOMES_PRODUCTVENDOR_REL(Parent, DirectionEnum.In); } }
			public IFromIn_VENDOR_VALID_FOR_EMPLOYEE_REL VENDOR_VALID_FOR_EMPLOYEE { get { return new VENDOR_VALID_FOR_EMPLOYEE_REL(Parent, DirectionEnum.In); } }

		}

		public VendorOut Out { get { return new VendorOut(this); } }
		public class VendorOut
		{
			private VendorNode Parent;
			internal VendorOut(VendorNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PURCHASEORDERHEADER_HAS_VENDOR_REL PURCHASEORDERHEADER_HAS_VENDOR { get { return new PURCHASEORDERHEADER_HAS_VENDOR_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class VendorAlias : AliasResult
    {
        internal VendorAlias(VendorNode parent)
        {
			Node = parent;
            AccountNumber = new StringResult(this, "AccountNumber", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["AccountNumber"]);
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["Name"]);
            CreditRating = new StringResult(this, "CreditRating", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["CreditRating"]);
            PreferredVendorStatus = new StringResult(this, "PreferredVendorStatus", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["PreferredVendorStatus"]);
            ActiveFlag = new StringResult(this, "ActiveFlag", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["ActiveFlag"]);
            PurchasingWebServiceURL = new StringResult(this, "PurchasingWebServiceURL", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["PurchasingWebServiceURL"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public VendorNode.VendorIn In { get { return new VendorNode.VendorIn(new VendorNode(this, true)); } }
        public VendorNode.VendorOut Out { get { return new VendorNode.VendorOut(new VendorNode(this, true)); } }

        public StringResult AccountNumber { get; private set; } 
        public StringResult Name { get; private set; } 
        public StringResult CreditRating { get; private set; } 
        public StringResult PreferredVendorStatus { get; private set; } 
        public StringResult ActiveFlag { get; private set; } 
        public StringResult PurchasingWebServiceURL { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
