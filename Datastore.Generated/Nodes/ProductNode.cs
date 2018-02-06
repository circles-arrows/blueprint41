using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductNode Product { get { return new ProductNode(); } }
	}

	public partial class ProductNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Product";
            }
        }

		internal ProductNode() { }
		internal ProductNode(ProductAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ProductNode Alias(out ProductAlias alias)
		{
			alias = new ProductAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public ProductIn  In  { get { return new ProductIn(this); } }
		public class ProductIn
		{
			private ProductNode Parent;
			internal ProductIn(ProductNode parent)
			{
                Parent = parent;
			}
			public IFromIn_PRODUCT_HAS_DOCUMENT_REL PRODUCT_HAS_DOCUMENT { get { return new PRODUCT_HAS_DOCUMENT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCT_HAS_PRODUCTMODEL_REL PRODUCT_HAS_PRODUCTMODEL { get { return new PRODUCT_HAS_PRODUCTMODEL_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL PRODUCT_HAS_PRODUCTPRODUCTPHOTO { get { return new PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCT_HAS_TRANSACTIONHISTORY_REL PRODUCT_HAS_TRANSACTIONHISTORY { get { return new PRODUCT_HAS_TRANSACTIONHISTORY_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCT_IN_PRODUCTSUBCATEGORY_REL PRODUCT_IN_PRODUCTSUBCATEGORY { get { return new PRODUCT_IN_PRODUCTSUBCATEGORY_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL PRODUCT_VALID_FOR_PRODUCTREVIEW { get { return new PRODUCT_VALID_FOR_PRODUCTREVIEW_REL(Parent, DirectionEnum.In); } }

		}

		public ProductOut Out { get { return new ProductOut(this); } }
		public class ProductOut
		{
			private ProductNode Parent;
			internal ProductOut(ProductNode parent)
			{
                Parent = parent;
			}
			public IFromOut_BILLOFMATERIALS_HAS_PRODUCT_REL BILLOFMATERIALS_HAS_PRODUCT { get { return new BILLOFMATERIALS_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL PRODUCTCOSTHISTORY_HAS_PRODUCT { get { return new PRODUCTCOSTHISTORY_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PRODUCTINVENTORY_HAS_PRODUCT_REL PRODUCTINVENTORY_HAS_PRODUCT { get { return new PRODUCTINVENTORY_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT { get { return new PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PRODUCTVENDOR_HAS_PRODUCT_REL PRODUCTVENDOR_HAS_PRODUCT { get { return new PRODUCTVENDOR_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PURCHASEORDERDETAIL_HAS_PRODUCT_REL PURCHASEORDERDETAIL_HAS_PRODUCT { get { return new PURCHASEORDERDETAIL_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SALESORDERDETAIL_HAS_PRODUCT_REL SALESORDERDETAIL_HAS_PRODUCT { get { return new SALESORDERDETAIL_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SHOPPINGCARTITEM_HAS_PRODUCT_REL SHOPPINGCARTITEM_HAS_PRODUCT { get { return new SHOPPINGCARTITEM_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT { get { return new TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_WORKORDER_HAS_PRODUCT_REL WORKORDER_HAS_PRODUCT { get { return new WORKORDER_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_WORKORDERROUTING_HAS_PRODUCT_REL WORKORDERROUTING_HAS_PRODUCT { get { return new WORKORDERROUTING_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ProductAlias : AliasResult
    {
        internal ProductAlias(ProductNode parent)
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["Name"]) },
						{ "ProductNumber", new StringResult(this, "ProductNumber", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["ProductNumber"]) },
						{ "MakeFlag", new BooleanResult(this, "MakeFlag", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["MakeFlag"]) },
						{ "FinishedGoodsFlag", new BooleanResult(this, "FinishedGoodsFlag", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["FinishedGoodsFlag"]) },
						{ "Color", new StringResult(this, "Color", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["Color"]) },
						{ "SafetyStockLevel", new NumericResult(this, "SafetyStockLevel", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["SafetyStockLevel"]) },
						{ "ReorderPoint", new NumericResult(this, "ReorderPoint", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["ReorderPoint"]) },
						{ "StandardCost", new FloatResult(this, "StandardCost", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["StandardCost"]) },
						{ "ListPrice", new FloatResult(this, "ListPrice", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["ListPrice"]) },
						{ "Size", new StringResult(this, "Size", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["Size"]) },
						{ "SizeUnitMeasureCode", new StringResult(this, "SizeUnitMeasureCode", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["SizeUnitMeasureCode"]) },
						{ "WeightUnitMeasureCode", new StringResult(this, "WeightUnitMeasureCode", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["WeightUnitMeasureCode"]) },
						{ "Weight", new MiscResult(this, "Weight", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["Weight"]) },
						{ "DaysToManufacture", new NumericResult(this, "DaysToManufacture", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["DaysToManufacture"]) },
						{ "ProductLine", new StringResult(this, "ProductLine", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["ProductLine"]) },
						{ "Class", new StringResult(this, "Class", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["Class"]) },
						{ "Style", new StringResult(this, "Style", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["Style"]) },
						{ "SellStartDate", new DateTimeResult(this, "SellStartDate", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["SellStartDate"]) },
						{ "SellEndDate", new DateTimeResult(this, "SellEndDate", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["SellEndDate"]) },
						{ "DiscontinuedDate", new DateTimeResult(this, "DiscontinuedDate", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["DiscontinuedDate"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public ProductNode.ProductIn In { get { return new ProductNode.ProductIn(new ProductNode(this, true)); } }
        public ProductNode.ProductOut Out { get { return new ProductNode.ProductOut(new ProductNode(this, true)); } }

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
        public StringResult ProductNumber
		{
			get
			{
				if ((object)m_ProductNumber == null)
					m_ProductNumber = (StringResult)AliasFields["ProductNumber"];

				return m_ProductNumber;
			}
		} 
        private StringResult m_ProductNumber = null;
        public BooleanResult MakeFlag
		{
			get
			{
				if ((object)m_MakeFlag == null)
					m_MakeFlag = (BooleanResult)AliasFields["MakeFlag"];

				return m_MakeFlag;
			}
		} 
        private BooleanResult m_MakeFlag = null;
        public BooleanResult FinishedGoodsFlag
		{
			get
			{
				if ((object)m_FinishedGoodsFlag == null)
					m_FinishedGoodsFlag = (BooleanResult)AliasFields["FinishedGoodsFlag"];

				return m_FinishedGoodsFlag;
			}
		} 
        private BooleanResult m_FinishedGoodsFlag = null;
        public StringResult Color
		{
			get
			{
				if ((object)m_Color == null)
					m_Color = (StringResult)AliasFields["Color"];

				return m_Color;
			}
		} 
        private StringResult m_Color = null;
        public NumericResult SafetyStockLevel
		{
			get
			{
				if ((object)m_SafetyStockLevel == null)
					m_SafetyStockLevel = (NumericResult)AliasFields["SafetyStockLevel"];

				return m_SafetyStockLevel;
			}
		} 
        private NumericResult m_SafetyStockLevel = null;
        public NumericResult ReorderPoint
		{
			get
			{
				if ((object)m_ReorderPoint == null)
					m_ReorderPoint = (NumericResult)AliasFields["ReorderPoint"];

				return m_ReorderPoint;
			}
		} 
        private NumericResult m_ReorderPoint = null;
        public FloatResult StandardCost
		{
			get
			{
				if ((object)m_StandardCost == null)
					m_StandardCost = (FloatResult)AliasFields["StandardCost"];

				return m_StandardCost;
			}
		} 
        private FloatResult m_StandardCost = null;
        public FloatResult ListPrice
		{
			get
			{
				if ((object)m_ListPrice == null)
					m_ListPrice = (FloatResult)AliasFields["ListPrice"];

				return m_ListPrice;
			}
		} 
        private FloatResult m_ListPrice = null;
        public StringResult Size
		{
			get
			{
				if ((object)m_Size == null)
					m_Size = (StringResult)AliasFields["Size"];

				return m_Size;
			}
		} 
        private StringResult m_Size = null;
        public StringResult SizeUnitMeasureCode
		{
			get
			{
				if ((object)m_SizeUnitMeasureCode == null)
					m_SizeUnitMeasureCode = (StringResult)AliasFields["SizeUnitMeasureCode"];

				return m_SizeUnitMeasureCode;
			}
		} 
        private StringResult m_SizeUnitMeasureCode = null;
        public StringResult WeightUnitMeasureCode
		{
			get
			{
				if ((object)m_WeightUnitMeasureCode == null)
					m_WeightUnitMeasureCode = (StringResult)AliasFields["WeightUnitMeasureCode"];

				return m_WeightUnitMeasureCode;
			}
		} 
        private StringResult m_WeightUnitMeasureCode = null;
        public MiscResult Weight
		{
			get
			{
				if ((object)m_Weight == null)
					m_Weight = (MiscResult)AliasFields["Weight"];

				return m_Weight;
			}
		} 
        private MiscResult m_Weight = null;
        public NumericResult DaysToManufacture
		{
			get
			{
				if ((object)m_DaysToManufacture == null)
					m_DaysToManufacture = (NumericResult)AliasFields["DaysToManufacture"];

				return m_DaysToManufacture;
			}
		} 
        private NumericResult m_DaysToManufacture = null;
        public StringResult ProductLine
		{
			get
			{
				if ((object)m_ProductLine == null)
					m_ProductLine = (StringResult)AliasFields["ProductLine"];

				return m_ProductLine;
			}
		} 
        private StringResult m_ProductLine = null;
        public StringResult Class
		{
			get
			{
				if ((object)m_Class == null)
					m_Class = (StringResult)AliasFields["Class"];

				return m_Class;
			}
		} 
        private StringResult m_Class = null;
        public StringResult Style
		{
			get
			{
				if ((object)m_Style == null)
					m_Style = (StringResult)AliasFields["Style"];

				return m_Style;
			}
		} 
        private StringResult m_Style = null;
        public DateTimeResult SellStartDate
		{
			get
			{
				if ((object)m_SellStartDate == null)
					m_SellStartDate = (DateTimeResult)AliasFields["SellStartDate"];

				return m_SellStartDate;
			}
		} 
        private DateTimeResult m_SellStartDate = null;
        public DateTimeResult SellEndDate
		{
			get
			{
				if ((object)m_SellEndDate == null)
					m_SellEndDate = (DateTimeResult)AliasFields["SellEndDate"];

				return m_SellEndDate;
			}
		} 
        private DateTimeResult m_SellEndDate = null;
        public DateTimeResult DiscontinuedDate
		{
			get
			{
				if ((object)m_DiscontinuedDate == null)
					m_DiscontinuedDate = (DateTimeResult)AliasFields["DiscontinuedDate"];

				return m_DiscontinuedDate;
			}
		} 
        private DateTimeResult m_DiscontinuedDate = null;
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
