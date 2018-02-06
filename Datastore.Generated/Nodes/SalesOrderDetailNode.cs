using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static SalesOrderDetailNode SalesOrderDetail { get { return new SalesOrderDetailNode(); } }
	}

	public partial class SalesOrderDetailNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "SalesOrderDetail";
            }
        }

		internal SalesOrderDetailNode() { }
		internal SalesOrderDetailNode(SalesOrderDetailAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesOrderDetailNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public SalesOrderDetailNode Alias(out SalesOrderDetailAlias alias)
		{
			alias = new SalesOrderDetailAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public SalesOrderDetailIn  In  { get { return new SalesOrderDetailIn(this); } }
		public class SalesOrderDetailIn
		{
			private SalesOrderDetailNode Parent;
			internal SalesOrderDetailIn(SalesOrderDetailNode parent)
			{
                Parent = parent;
			}
			public IFromIn_SALESORDERDETAIL_HAS_PRODUCT_REL SALESORDERDETAIL_HAS_PRODUCT { get { return new SALESORDERDETAIL_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_SALESORDERDETAIL_HAS_SALESORDERHEADER_REL SALESORDERDETAIL_HAS_SALESORDERHEADER { get { return new SALESORDERDETAIL_HAS_SALESORDERHEADER_REL(Parent, DirectionEnum.In); } }
			public IFromIn_SALESORDERDETAIL_HAS_SPECIALOFFER_REL SALESORDERDETAIL_HAS_SPECIALOFFER { get { return new SALESORDERDETAIL_HAS_SPECIALOFFER_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class SalesOrderDetailAlias : AliasResult
    {
        internal SalesOrderDetailAlias(SalesOrderDetailNode parent)
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
						{ "CarrierTrackingNumber", new StringResult(this, "CarrierTrackingNumber", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["CarrierTrackingNumber"]) },
						{ "OrderQty", new NumericResult(this, "OrderQty", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["OrderQty"]) },
						{ "UnitPrice", new FloatResult(this, "UnitPrice", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["UnitPrice"]) },
						{ "UnitPriceDiscount", new StringResult(this, "UnitPriceDiscount", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["UnitPriceDiscount"]) },
						{ "LineTotal", new StringResult(this, "LineTotal", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["LineTotal"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public SalesOrderDetailNode.SalesOrderDetailIn In { get { return new SalesOrderDetailNode.SalesOrderDetailIn(new SalesOrderDetailNode(this, true)); } }

        public StringResult CarrierTrackingNumber
		{
			get
			{
				if ((object)m_CarrierTrackingNumber == null)
					m_CarrierTrackingNumber = (StringResult)AliasFields["CarrierTrackingNumber"];

				return m_CarrierTrackingNumber;
			}
		} 
        private StringResult m_CarrierTrackingNumber = null;
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
        public StringResult UnitPriceDiscount
		{
			get
			{
				if ((object)m_UnitPriceDiscount == null)
					m_UnitPriceDiscount = (StringResult)AliasFields["UnitPriceDiscount"];

				return m_UnitPriceDiscount;
			}
		} 
        private StringResult m_UnitPriceDiscount = null;
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
