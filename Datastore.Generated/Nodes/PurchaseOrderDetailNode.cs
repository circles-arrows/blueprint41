using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static PurchaseOrderDetailNode PurchaseOrderDetail { get { return new PurchaseOrderDetailNode(); } }
	}

	public partial class PurchaseOrderDetailNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "PurchaseOrderDetail";
        }

		internal PurchaseOrderDetailNode() { }
		internal PurchaseOrderDetailNode(PurchaseOrderDetailAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal PurchaseOrderDetailNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public PurchaseOrderDetailNode Alias(out PurchaseOrderDetailAlias alias)
		{
			alias = new PurchaseOrderDetailAlias(this);
            NodeAlias = alias;
			return this;
		}

		public PurchaseOrderDetailNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }

	
		public PurchaseOrderDetailIn  In  { get { return new PurchaseOrderDetailIn(this); } }
		public class PurchaseOrderDetailIn
		{
			private PurchaseOrderDetailNode Parent;
			internal PurchaseOrderDetailIn(PurchaseOrderDetailNode parent)
			{
                Parent = parent;
			}
			public IFromIn_PURCHASEORDERDETAIL_HAS_PRODUCT_REL PURCHASEORDERDETAIL_HAS_PRODUCT { get { return new PURCHASEORDERDETAIL_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER_REL PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER { get { return new PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class PurchaseOrderDetailAlias : AliasResult
    {
        internal PurchaseOrderDetailAlias(PurchaseOrderDetailNode parent)
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
						{ "DueDate", new DateTimeResult(this, "DueDate", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["DueDate"]) },
						{ "OrderQty", new NumericResult(this, "OrderQty", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["OrderQty"]) },
						{ "UnitPrice", new FloatResult(this, "UnitPrice", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["UnitPrice"]) },
						{ "LineTotal", new StringResult(this, "LineTotal", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["LineTotal"]) },
						{ "ReceivedQty", new NumericResult(this, "ReceivedQty", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["ReceivedQty"]) },
						{ "RejectedQty", new NumericResult(this, "RejectedQty", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["RejectedQty"]) },
						{ "StockedQty", new NumericResult(this, "StockedQty", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["StockedQty"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public PurchaseOrderDetailNode.PurchaseOrderDetailIn In { get { return new PurchaseOrderDetailNode.PurchaseOrderDetailIn(new PurchaseOrderDetailNode(this, true)); } }

        public DateTimeResult DueDate
		{
			get
			{
				if ((object)m_DueDate == null)
					m_DueDate = (DateTimeResult)AliasFields["DueDate"];

				return m_DueDate;
			}
		} 
        private DateTimeResult m_DueDate = null;
        public NumericResult OrderQty
		{
			get
			{
				if ((object)m_OrderQty == null)
					m_OrderQty = (NumericResult)AliasFields["OrderQty"];

				return m_OrderQty;
			}
		} 
        private NumericResult m_OrderQty = null;
        public FloatResult UnitPrice
		{
			get
			{
				if ((object)m_UnitPrice == null)
					m_UnitPrice = (FloatResult)AliasFields["UnitPrice"];

				return m_UnitPrice;
			}
		} 
        private FloatResult m_UnitPrice = null;
        public StringResult LineTotal
		{
			get
			{
				if ((object)m_LineTotal == null)
					m_LineTotal = (StringResult)AliasFields["LineTotal"];

				return m_LineTotal;
			}
		} 
        private StringResult m_LineTotal = null;
        public NumericResult ReceivedQty
		{
			get
			{
				if ((object)m_ReceivedQty == null)
					m_ReceivedQty = (NumericResult)AliasFields["ReceivedQty"];

				return m_ReceivedQty;
			}
		} 
        private NumericResult m_ReceivedQty = null;
        public NumericResult RejectedQty
		{
			get
			{
				if ((object)m_RejectedQty == null)
					m_RejectedQty = (NumericResult)AliasFields["RejectedQty"];

				return m_RejectedQty;
			}
		} 
        private NumericResult m_RejectedQty = null;
        public NumericResult StockedQty
		{
			get
			{
				if ((object)m_StockedQty == null)
					m_StockedQty = (NumericResult)AliasFields["StockedQty"];

				return m_StockedQty;
			}
		} 
        private NumericResult m_StockedQty = null;
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
