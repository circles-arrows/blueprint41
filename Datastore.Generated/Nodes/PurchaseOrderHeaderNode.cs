using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static PurchaseOrderHeaderNode PurchaseOrderHeader { get { return new PurchaseOrderHeaderNode(); } }
	}

	public partial class PurchaseOrderHeaderNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "PurchaseOrderHeader";
        }

		internal PurchaseOrderHeaderNode() { }
		internal PurchaseOrderHeaderNode(PurchaseOrderHeaderAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal PurchaseOrderHeaderNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public PurchaseOrderHeaderNode Alias(out PurchaseOrderHeaderAlias alias)
		{
			alias = new PurchaseOrderHeaderAlias(this);
            NodeAlias = alias;
			return this;
		}

		public PurchaseOrderHeaderNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }

	
		public PurchaseOrderHeaderIn  In  { get { return new PurchaseOrderHeaderIn(this); } }
		public class PurchaseOrderHeaderIn
		{
			private PurchaseOrderHeaderNode Parent;
			internal PurchaseOrderHeaderIn(PurchaseOrderHeaderNode parent)
			{
                Parent = parent;
			}
			public IFromIn_PURCHASEORDERHEADER_HAS_SHIPMETHOD_REL PURCHASEORDERHEADER_HAS_SHIPMETHOD { get { return new PURCHASEORDERHEADER_HAS_SHIPMETHOD_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PURCHASEORDERHEADER_HAS_VENDOR_REL PURCHASEORDERHEADER_HAS_VENDOR { get { return new PURCHASEORDERHEADER_HAS_VENDOR_REL(Parent, DirectionEnum.In); } }

		}

		public PurchaseOrderHeaderOut Out { get { return new PurchaseOrderHeaderOut(this); } }
		public class PurchaseOrderHeaderOut
		{
			private PurchaseOrderHeaderNode Parent;
			internal PurchaseOrderHeaderOut(PurchaseOrderHeaderNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER_REL PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER { get { return new PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class PurchaseOrderHeaderAlias : AliasResult
    {
        internal PurchaseOrderHeaderAlias(PurchaseOrderHeaderNode parent)
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
						{ "RevisionNumber", new StringResult(this, "RevisionNumber", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["RevisionNumber"]) },
						{ "Status", new StringResult(this, "Status", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["Status"]) },
						{ "OrderDate", new DateTimeResult(this, "OrderDate", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["OrderDate"]) },
						{ "ShipDate", new DateTimeResult(this, "ShipDate", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["ShipDate"]) },
						{ "SubTotal", new FloatResult(this, "SubTotal", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["SubTotal"]) },
						{ "TaxAmt", new FloatResult(this, "TaxAmt", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["TaxAmt"]) },
						{ "Freight", new StringResult(this, "Freight", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["Freight"]) },
						{ "TotalDue", new FloatResult(this, "TotalDue", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["TotalDue"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public PurchaseOrderHeaderNode.PurchaseOrderHeaderIn In { get { return new PurchaseOrderHeaderNode.PurchaseOrderHeaderIn(new PurchaseOrderHeaderNode(this, true)); } }
        public PurchaseOrderHeaderNode.PurchaseOrderHeaderOut Out { get { return new PurchaseOrderHeaderNode.PurchaseOrderHeaderOut(new PurchaseOrderHeaderNode(this, true)); } }

        public StringResult RevisionNumber
		{
			get
			{
				if ((object)m_RevisionNumber == null)
					m_RevisionNumber = (StringResult)AliasFields["RevisionNumber"];

				return m_RevisionNumber;
			}
		} 
        private StringResult m_RevisionNumber = null;
        public StringResult Status
		{
			get
			{
				if ((object)m_Status == null)
					m_Status = (StringResult)AliasFields["Status"];

				return m_Status;
			}
		} 
        private StringResult m_Status = null;
        public DateTimeResult OrderDate
		{
			get
			{
				if ((object)m_OrderDate == null)
					m_OrderDate = (DateTimeResult)AliasFields["OrderDate"];

				return m_OrderDate;
			}
		} 
        private DateTimeResult m_OrderDate = null;
        public DateTimeResult ShipDate
		{
			get
			{
				if ((object)m_ShipDate == null)
					m_ShipDate = (DateTimeResult)AliasFields["ShipDate"];

				return m_ShipDate;
			}
		} 
        private DateTimeResult m_ShipDate = null;
        public FloatResult SubTotal
		{
			get
			{
				if ((object)m_SubTotal == null)
					m_SubTotal = (FloatResult)AliasFields["SubTotal"];

				return m_SubTotal;
			}
		} 
        private FloatResult m_SubTotal = null;
        public FloatResult TaxAmt
		{
			get
			{
				if ((object)m_TaxAmt == null)
					m_TaxAmt = (FloatResult)AliasFields["TaxAmt"];

				return m_TaxAmt;
			}
		} 
        private FloatResult m_TaxAmt = null;
        public StringResult Freight
		{
			get
			{
				if ((object)m_Freight == null)
					m_Freight = (StringResult)AliasFields["Freight"];

				return m_Freight;
			}
		} 
        private StringResult m_Freight = null;
        public FloatResult TotalDue
		{
			get
			{
				if ((object)m_TotalDue == null)
					m_TotalDue = (FloatResult)AliasFields["TotalDue"];

				return m_TotalDue;
			}
		} 
        private FloatResult m_TotalDue = null;
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
