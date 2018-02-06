using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductVendorNode ProductVendor { get { return new ProductVendorNode(); } }
	}

	public partial class ProductVendorNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ProductVendor";
            }
        }

		internal ProductVendorNode() { }
		internal ProductVendorNode(ProductVendorAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductVendorNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ProductVendorNode Alias(out ProductVendorAlias alias)
		{
			alias = new ProductVendorAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public ProductVendorIn  In  { get { return new ProductVendorIn(this); } }
		public class ProductVendorIn
		{
			private ProductVendorNode Parent;
			internal ProductVendorIn(ProductVendorNode parent)
			{
                Parent = parent;
			}
			public IFromIn_PRODUCTVENDOR_HAS_PRODUCT_REL PRODUCTVENDOR_HAS_PRODUCT { get { return new PRODUCTVENDOR_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCTVENDOR_HAS_UNITMEASURE_REL PRODUCTVENDOR_HAS_UNITMEASURE { get { return new PRODUCTVENDOR_HAS_UNITMEASURE_REL(Parent, DirectionEnum.In); } }

		}

		public ProductVendorOut Out { get { return new ProductVendorOut(this); } }
		public class ProductVendorOut
		{
			private ProductVendorNode Parent;
			internal ProductVendorOut(ProductVendorNode parent)
			{
                Parent = parent;
			}
			public IFromOut_VENDOR_BECOMES_PRODUCTVENDOR_REL VENDOR_BECOMES_PRODUCTVENDOR { get { return new VENDOR_BECOMES_PRODUCTVENDOR_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ProductVendorAlias : AliasResult
    {
        internal ProductVendorAlias(ProductVendorNode parent)
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
						{ "AverageLeadTime", new StringResult(this, "AverageLeadTime", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["AverageLeadTime"]) },
						{ "StandardPrice", new StringResult(this, "StandardPrice", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["StandardPrice"]) },
						{ "LastReceiptCost", new StringResult(this, "LastReceiptCost", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["LastReceiptCost"]) },
						{ "LastReceiptDate", new DateTimeResult(this, "LastReceiptDate", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["LastReceiptDate"]) },
						{ "MinOrderQty", new NumericResult(this, "MinOrderQty", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["MinOrderQty"]) },
						{ "MaxOrderQty", new NumericResult(this, "MaxOrderQty", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["MaxOrderQty"]) },
						{ "OnOrderQty", new NumericResult(this, "OnOrderQty", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["OnOrderQty"]) },
						{ "UnitMeasureCode", new StringResult(this, "UnitMeasureCode", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["UnitMeasureCode"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public ProductVendorNode.ProductVendorIn In { get { return new ProductVendorNode.ProductVendorIn(new ProductVendorNode(this, true)); } }
        public ProductVendorNode.ProductVendorOut Out { get { return new ProductVendorNode.ProductVendorOut(new ProductVendorNode(this, true)); } }

        public StringResult AverageLeadTime
		{
			get
			{
				if ((object)m_AverageLeadTime == null)
					m_AverageLeadTime = (StringResult)AliasFields["AverageLeadTime"];

				return m_AverageLeadTime;
			}
		} 
        private StringResult m_AverageLeadTime = null;
        public StringResult StandardPrice
		{
			get
			{
				if ((object)m_StandardPrice == null)
					m_StandardPrice = (StringResult)AliasFields["StandardPrice"];

				return m_StandardPrice;
			}
		} 
        private StringResult m_StandardPrice = null;
        public StringResult LastReceiptCost
		{
			get
			{
				if ((object)m_LastReceiptCost == null)
					m_LastReceiptCost = (StringResult)AliasFields["LastReceiptCost"];

				return m_LastReceiptCost;
			}
		} 
        private StringResult m_LastReceiptCost = null;
        public DateTimeResult LastReceiptDate
		{
			get
			{
				if ((object)m_LastReceiptDate == null)
					m_LastReceiptDate = (DateTimeResult)AliasFields["LastReceiptDate"];

				return m_LastReceiptDate;
			}
		} 
        private DateTimeResult m_LastReceiptDate = null;
        public NumericResult MinOrderQty
		{
			get
			{
				if ((object)m_MinOrderQty == null)
					m_MinOrderQty = (NumericResult)AliasFields["MinOrderQty"];

				return m_MinOrderQty;
			}
		} 
        private NumericResult m_MinOrderQty = null;
        public NumericResult MaxOrderQty
		{
			get
			{
				if ((object)m_MaxOrderQty == null)
					m_MaxOrderQty = (NumericResult)AliasFields["MaxOrderQty"];

				return m_MaxOrderQty;
			}
		} 
        private NumericResult m_MaxOrderQty = null;
        public NumericResult OnOrderQty
		{
			get
			{
				if ((object)m_OnOrderQty == null)
					m_OnOrderQty = (NumericResult)AliasFields["OnOrderQty"];

				return m_OnOrderQty;
			}
		} 
        private NumericResult m_OnOrderQty = null;
        public StringResult UnitMeasureCode
		{
			get
			{
				if ((object)m_UnitMeasureCode == null)
					m_UnitMeasureCode = (StringResult)AliasFields["UnitMeasureCode"];

				return m_UnitMeasureCode;
			}
		} 
        private StringResult m_UnitMeasureCode = null;
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
