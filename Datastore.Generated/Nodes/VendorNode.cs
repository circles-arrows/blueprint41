using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static VendorNode Vendor { get { return new VendorNode(); } }
	}

	public partial class VendorNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "Vendor";
        }

		internal VendorNode() { }
		internal VendorNode(VendorAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal VendorNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public VendorNode Alias(out VendorAlias alias)
		{
			alias = new VendorAlias(this);
            NodeAlias = alias;
			return this;
		}

		public VendorNode UseExistingAlias(AliasResult alias)
        {
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
        }

        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields == null)
                {
                    m_AliasFields = new Dictionary<string, FieldResult>()
                    {
						{ "AccountNumber", new StringResult(this, "AccountNumber", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["AccountNumber"]) },
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["Name"]) },
						{ "CreditRating", new StringResult(this, "CreditRating", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["CreditRating"]) },
						{ "PreferredVendorStatus", new StringResult(this, "PreferredVendorStatus", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["PreferredVendorStatus"]) },
						{ "ActiveFlag", new StringResult(this, "ActiveFlag", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["ActiveFlag"]) },
						{ "PurchasingWebServiceURL", new StringResult(this, "PurchasingWebServiceURL", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["PurchasingWebServiceURL"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public VendorNode.VendorIn In { get { return new VendorNode.VendorIn(new VendorNode(this, true)); } }
        public VendorNode.VendorOut Out { get { return new VendorNode.VendorOut(new VendorNode(this, true)); } }

        public StringResult AccountNumber
		{
			get
			{
				if ((object)m_AccountNumber == null)
					m_AccountNumber = (StringResult)AliasFields["AccountNumber"];

				return m_AccountNumber;
			}
		} 
        private StringResult m_AccountNumber = null;
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
        public StringResult CreditRating
		{
			get
			{
				if ((object)m_CreditRating == null)
					m_CreditRating = (StringResult)AliasFields["CreditRating"];

				return m_CreditRating;
			}
		} 
        private StringResult m_CreditRating = null;
        public StringResult PreferredVendorStatus
		{
			get
			{
				if ((object)m_PreferredVendorStatus == null)
					m_PreferredVendorStatus = (StringResult)AliasFields["PreferredVendorStatus"];

				return m_PreferredVendorStatus;
			}
		} 
        private StringResult m_PreferredVendorStatus = null;
        public StringResult ActiveFlag
		{
			get
			{
				if ((object)m_ActiveFlag == null)
					m_ActiveFlag = (StringResult)AliasFields["ActiveFlag"];

				return m_ActiveFlag;
			}
		} 
        private StringResult m_ActiveFlag = null;
        public StringResult PurchasingWebServiceURL
		{
			get
			{
				if ((object)m_PurchasingWebServiceURL == null)
					m_PurchasingWebServiceURL = (StringResult)AliasFields["PurchasingWebServiceURL"];

				return m_PurchasingWebServiceURL;
			}
		} 
        private StringResult m_PurchasingWebServiceURL = null;
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
