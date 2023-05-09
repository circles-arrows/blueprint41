using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Query;

using m = Domain.Data.Manipulation;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductNode Product { get { return new ProductNode(); } }
	}

	public partial class ProductNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(ProductNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(ProductNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Product";
		}

		protected override Entity GetEntity()
        {
			return m.Product.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.Product.Entity.FunctionalId;
            }
        }

		internal ProductNode() { }
		internal ProductNode(ProductAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal ProductNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public ProductNode Where(JsNotation<string> Class = default, JsNotation<string> Color = default, JsNotation<int> DaysToManufacture = default, JsNotation<System.DateTime?> DiscontinuedDate = default, JsNotation<bool> FinishedGoodsFlag = default, JsNotation<double> ListPrice = default, JsNotation<bool> MakeFlag = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> ProductLine = default, JsNotation<string> ProductNumber = default, JsNotation<int> ReorderPoint = default, JsNotation<string> rowguid = default, JsNotation<int> SafetyStockLevel = default, JsNotation<System.DateTime?> SellEndDate = default, JsNotation<System.DateTime> SellStartDate = default, JsNotation<string> Size = default, JsNotation<string> SizeUnitMeasureCode = default, JsNotation<double> StandardCost = default, JsNotation<string> Style = default, JsNotation<string> Uid = default, JsNotation<decimal?> Weight = default, JsNotation<string> WeightUnitMeasureCode = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductAlias> alias = new Lazy<ProductAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Class.HasValue) conditions.Add(new QueryCondition(alias.Value.Class, Operator.Equals, ((IValue)Class).GetValue()));
            if (Color.HasValue) conditions.Add(new QueryCondition(alias.Value.Color, Operator.Equals, ((IValue)Color).GetValue()));
            if (DaysToManufacture.HasValue) conditions.Add(new QueryCondition(alias.Value.DaysToManufacture, Operator.Equals, ((IValue)DaysToManufacture).GetValue()));
            if (DiscontinuedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.DiscontinuedDate, Operator.Equals, ((IValue)DiscontinuedDate).GetValue()));
            if (FinishedGoodsFlag.HasValue) conditions.Add(new QueryCondition(alias.Value.FinishedGoodsFlag, Operator.Equals, ((IValue)FinishedGoodsFlag).GetValue()));
            if (ListPrice.HasValue) conditions.Add(new QueryCondition(alias.Value.ListPrice, Operator.Equals, ((IValue)ListPrice).GetValue()));
            if (MakeFlag.HasValue) conditions.Add(new QueryCondition(alias.Value.MakeFlag, Operator.Equals, ((IValue)MakeFlag).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (ProductLine.HasValue) conditions.Add(new QueryCondition(alias.Value.ProductLine, Operator.Equals, ((IValue)ProductLine).GetValue()));
            if (ProductNumber.HasValue) conditions.Add(new QueryCondition(alias.Value.ProductNumber, Operator.Equals, ((IValue)ProductNumber).GetValue()));
            if (ReorderPoint.HasValue) conditions.Add(new QueryCondition(alias.Value.ReorderPoint, Operator.Equals, ((IValue)ReorderPoint).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (SafetyStockLevel.HasValue) conditions.Add(new QueryCondition(alias.Value.SafetyStockLevel, Operator.Equals, ((IValue)SafetyStockLevel).GetValue()));
            if (SellEndDate.HasValue) conditions.Add(new QueryCondition(alias.Value.SellEndDate, Operator.Equals, ((IValue)SellEndDate).GetValue()));
            if (SellStartDate.HasValue) conditions.Add(new QueryCondition(alias.Value.SellStartDate, Operator.Equals, ((IValue)SellStartDate).GetValue()));
            if (Size.HasValue) conditions.Add(new QueryCondition(alias.Value.Size, Operator.Equals, ((IValue)Size).GetValue()));
            if (SizeUnitMeasureCode.HasValue) conditions.Add(new QueryCondition(alias.Value.SizeUnitMeasureCode, Operator.Equals, ((IValue)SizeUnitMeasureCode).GetValue()));
            if (StandardCost.HasValue) conditions.Add(new QueryCondition(alias.Value.StandardCost, Operator.Equals, ((IValue)StandardCost).GetValue()));
            if (Style.HasValue) conditions.Add(new QueryCondition(alias.Value.Style, Operator.Equals, ((IValue)Style).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));
            if (Weight.HasValue) conditions.Add(new QueryCondition(alias.Value.Weight, Operator.Equals, ((IValue)Weight).GetValue()));
            if (WeightUnitMeasureCode.HasValue) conditions.Add(new QueryCondition(alias.Value.WeightUnitMeasureCode, Operator.Equals, ((IValue)WeightUnitMeasureCode).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public ProductNode Assign(JsNotation<string> Class = default, JsNotation<string> Color = default, JsNotation<int> DaysToManufacture = default, JsNotation<System.DateTime?> DiscontinuedDate = default, JsNotation<bool> FinishedGoodsFlag = default, JsNotation<double> ListPrice = default, JsNotation<bool> MakeFlag = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> ProductLine = default, JsNotation<string> ProductNumber = default, JsNotation<int> ReorderPoint = default, JsNotation<string> rowguid = default, JsNotation<int> SafetyStockLevel = default, JsNotation<System.DateTime?> SellEndDate = default, JsNotation<System.DateTime> SellStartDate = default, JsNotation<string> Size = default, JsNotation<string> SizeUnitMeasureCode = default, JsNotation<double> StandardCost = default, JsNotation<string> Style = default, JsNotation<string> Uid = default, JsNotation<decimal?> Weight = default, JsNotation<string> WeightUnitMeasureCode = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductAlias> alias = new Lazy<ProductAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Class.HasValue) assignments.Add(new Assignment(alias.Value.Class, Class));
            if (Color.HasValue) assignments.Add(new Assignment(alias.Value.Color, Color));
            if (DaysToManufacture.HasValue) assignments.Add(new Assignment(alias.Value.DaysToManufacture, DaysToManufacture));
            if (DiscontinuedDate.HasValue) assignments.Add(new Assignment(alias.Value.DiscontinuedDate, DiscontinuedDate));
            if (FinishedGoodsFlag.HasValue) assignments.Add(new Assignment(alias.Value.FinishedGoodsFlag, FinishedGoodsFlag));
            if (ListPrice.HasValue) assignments.Add(new Assignment(alias.Value.ListPrice, ListPrice));
            if (MakeFlag.HasValue) assignments.Add(new Assignment(alias.Value.MakeFlag, MakeFlag));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (ProductLine.HasValue) assignments.Add(new Assignment(alias.Value.ProductLine, ProductLine));
            if (ProductNumber.HasValue) assignments.Add(new Assignment(alias.Value.ProductNumber, ProductNumber));
            if (ReorderPoint.HasValue) assignments.Add(new Assignment(alias.Value.ReorderPoint, ReorderPoint));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (SafetyStockLevel.HasValue) assignments.Add(new Assignment(alias.Value.SafetyStockLevel, SafetyStockLevel));
            if (SellEndDate.HasValue) assignments.Add(new Assignment(alias.Value.SellEndDate, SellEndDate));
            if (SellStartDate.HasValue) assignments.Add(new Assignment(alias.Value.SellStartDate, SellStartDate));
            if (Size.HasValue) assignments.Add(new Assignment(alias.Value.Size, Size));
            if (SizeUnitMeasureCode.HasValue) assignments.Add(new Assignment(alias.Value.SizeUnitMeasureCode, SizeUnitMeasureCode));
            if (StandardCost.HasValue) assignments.Add(new Assignment(alias.Value.StandardCost, StandardCost));
            if (Style.HasValue) assignments.Add(new Assignment(alias.Value.Style, Style));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));
            if (Weight.HasValue) assignments.Add(new Assignment(alias.Value.Weight, Weight));
            if (WeightUnitMeasureCode.HasValue) assignments.Add(new Assignment(alias.Value.WeightUnitMeasureCode, WeightUnitMeasureCode));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public ProductNode Alias(out ProductAlias alias)
        {
            if (NodeAlias is ProductAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new ProductAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public ProductNode Alias(out ProductAlias alias, string name)
        {
            if (NodeAlias is ProductAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new ProductAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public ProductNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
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

	public class ProductAlias : AliasResult<ProductAlias, ProductListAlias>
	{
		internal ProductAlias(ProductNode parent)
		{
			Node = parent;
		}
		internal ProductAlias(ProductNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  ProductAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  ProductAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  ProductAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> Class = default, JsNotation<string> Color = default, JsNotation<int> DaysToManufacture = default, JsNotation<System.DateTime?> DiscontinuedDate = default, JsNotation<bool> FinishedGoodsFlag = default, JsNotation<double> ListPrice = default, JsNotation<bool> MakeFlag = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> ProductLine = default, JsNotation<string> ProductNumber = default, JsNotation<int> ReorderPoint = default, JsNotation<string> rowguid = default, JsNotation<int> SafetyStockLevel = default, JsNotation<System.DateTime?> SellEndDate = default, JsNotation<System.DateTime> SellStartDate = default, JsNotation<string> Size = default, JsNotation<string> SizeUnitMeasureCode = default, JsNotation<double> StandardCost = default, JsNotation<string> Style = default, JsNotation<string> Uid = default, JsNotation<decimal?> Weight = default, JsNotation<string> WeightUnitMeasureCode = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (Class.HasValue) assignments.Add(new Assignment(this.Class, Class));
			if (Color.HasValue) assignments.Add(new Assignment(this.Color, Color));
			if (DaysToManufacture.HasValue) assignments.Add(new Assignment(this.DaysToManufacture, DaysToManufacture));
			if (DiscontinuedDate.HasValue) assignments.Add(new Assignment(this.DiscontinuedDate, DiscontinuedDate));
			if (FinishedGoodsFlag.HasValue) assignments.Add(new Assignment(this.FinishedGoodsFlag, FinishedGoodsFlag));
			if (ListPrice.HasValue) assignments.Add(new Assignment(this.ListPrice, ListPrice));
			if (MakeFlag.HasValue) assignments.Add(new Assignment(this.MakeFlag, MakeFlag));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Name.HasValue) assignments.Add(new Assignment(this.Name, Name));
			if (ProductLine.HasValue) assignments.Add(new Assignment(this.ProductLine, ProductLine));
			if (ProductNumber.HasValue) assignments.Add(new Assignment(this.ProductNumber, ProductNumber));
			if (ReorderPoint.HasValue) assignments.Add(new Assignment(this.ReorderPoint, ReorderPoint));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
			if (SafetyStockLevel.HasValue) assignments.Add(new Assignment(this.SafetyStockLevel, SafetyStockLevel));
			if (SellEndDate.HasValue) assignments.Add(new Assignment(this.SellEndDate, SellEndDate));
			if (SellStartDate.HasValue) assignments.Add(new Assignment(this.SellStartDate, SellStartDate));
			if (Size.HasValue) assignments.Add(new Assignment(this.Size, Size));
			if (SizeUnitMeasureCode.HasValue) assignments.Add(new Assignment(this.SizeUnitMeasureCode, SizeUnitMeasureCode));
			if (StandardCost.HasValue) assignments.Add(new Assignment(this.StandardCost, StandardCost));
			if (Style.HasValue) assignments.Add(new Assignment(this.Style, Style));
			if (Uid.HasValue) assignments.Add(new Assignment(this.Uid, Uid));
			if (Weight.HasValue) assignments.Add(new Assignment(this.Weight, Weight));
			if (WeightUnitMeasureCode.HasValue) assignments.Add(new Assignment(this.WeightUnitMeasureCode, WeightUnitMeasureCode));
            
            return assignments.ToArray();
        }


		public override IReadOnlyDictionary<string, FieldResult> AliasFields
		{
			get
			{
				if (m_AliasFields is null)
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
						{ "Weight", new NumericResult(this, "Weight", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["Weight"]) },
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
				if (m_Name is null)
					m_Name = (StringResult)AliasFields["Name"];

				return m_Name;
			}
		}
		private StringResult m_Name = null;
		public StringResult ProductNumber
		{
			get
			{
				if (m_ProductNumber is null)
					m_ProductNumber = (StringResult)AliasFields["ProductNumber"];

				return m_ProductNumber;
			}
		}
		private StringResult m_ProductNumber = null;
		public BooleanResult MakeFlag
		{
			get
			{
				if (m_MakeFlag is null)
					m_MakeFlag = (BooleanResult)AliasFields["MakeFlag"];

				return m_MakeFlag;
			}
		}
		private BooleanResult m_MakeFlag = null;
		public BooleanResult FinishedGoodsFlag
		{
			get
			{
				if (m_FinishedGoodsFlag is null)
					m_FinishedGoodsFlag = (BooleanResult)AliasFields["FinishedGoodsFlag"];

				return m_FinishedGoodsFlag;
			}
		}
		private BooleanResult m_FinishedGoodsFlag = null;
		public StringResult Color
		{
			get
			{
				if (m_Color is null)
					m_Color = (StringResult)AliasFields["Color"];

				return m_Color;
			}
		}
		private StringResult m_Color = null;
		public NumericResult SafetyStockLevel
		{
			get
			{
				if (m_SafetyStockLevel is null)
					m_SafetyStockLevel = (NumericResult)AliasFields["SafetyStockLevel"];

				return m_SafetyStockLevel;
			}
		}
		private NumericResult m_SafetyStockLevel = null;
		public NumericResult ReorderPoint
		{
			get
			{
				if (m_ReorderPoint is null)
					m_ReorderPoint = (NumericResult)AliasFields["ReorderPoint"];

				return m_ReorderPoint;
			}
		}
		private NumericResult m_ReorderPoint = null;
		public FloatResult StandardCost
		{
			get
			{
				if (m_StandardCost is null)
					m_StandardCost = (FloatResult)AliasFields["StandardCost"];

				return m_StandardCost;
			}
		}
		private FloatResult m_StandardCost = null;
		public FloatResult ListPrice
		{
			get
			{
				if (m_ListPrice is null)
					m_ListPrice = (FloatResult)AliasFields["ListPrice"];

				return m_ListPrice;
			}
		}
		private FloatResult m_ListPrice = null;
		public StringResult Size
		{
			get
			{
				if (m_Size is null)
					m_Size = (StringResult)AliasFields["Size"];

				return m_Size;
			}
		}
		private StringResult m_Size = null;
		public StringResult SizeUnitMeasureCode
		{
			get
			{
				if (m_SizeUnitMeasureCode is null)
					m_SizeUnitMeasureCode = (StringResult)AliasFields["SizeUnitMeasureCode"];

				return m_SizeUnitMeasureCode;
			}
		}
		private StringResult m_SizeUnitMeasureCode = null;
		public StringResult WeightUnitMeasureCode
		{
			get
			{
				if (m_WeightUnitMeasureCode is null)
					m_WeightUnitMeasureCode = (StringResult)AliasFields["WeightUnitMeasureCode"];

				return m_WeightUnitMeasureCode;
			}
		}
		private StringResult m_WeightUnitMeasureCode = null;
		public NumericResult Weight
		{
			get
			{
				if (m_Weight is null)
					m_Weight = (NumericResult)AliasFields["Weight"];

				return m_Weight;
			}
		}
		private NumericResult m_Weight = null;
		public NumericResult DaysToManufacture
		{
			get
			{
				if (m_DaysToManufacture is null)
					m_DaysToManufacture = (NumericResult)AliasFields["DaysToManufacture"];

				return m_DaysToManufacture;
			}
		}
		private NumericResult m_DaysToManufacture = null;
		public StringResult ProductLine
		{
			get
			{
				if (m_ProductLine is null)
					m_ProductLine = (StringResult)AliasFields["ProductLine"];

				return m_ProductLine;
			}
		}
		private StringResult m_ProductLine = null;
		public StringResult Class
		{
			get
			{
				if (m_Class is null)
					m_Class = (StringResult)AliasFields["Class"];

				return m_Class;
			}
		}
		private StringResult m_Class = null;
		public StringResult Style
		{
			get
			{
				if (m_Style is null)
					m_Style = (StringResult)AliasFields["Style"];

				return m_Style;
			}
		}
		private StringResult m_Style = null;
		public DateTimeResult SellStartDate
		{
			get
			{
				if (m_SellStartDate is null)
					m_SellStartDate = (DateTimeResult)AliasFields["SellStartDate"];

				return m_SellStartDate;
			}
		}
		private DateTimeResult m_SellStartDate = null;
		public DateTimeResult SellEndDate
		{
			get
			{
				if (m_SellEndDate is null)
					m_SellEndDate = (DateTimeResult)AliasFields["SellEndDate"];

				return m_SellEndDate;
			}
		}
		private DateTimeResult m_SellEndDate = null;
		public DateTimeResult DiscontinuedDate
		{
			get
			{
				if (m_DiscontinuedDate is null)
					m_DiscontinuedDate = (DateTimeResult)AliasFields["DiscontinuedDate"];

				return m_DiscontinuedDate;
			}
		}
		private DateTimeResult m_DiscontinuedDate = null;
		public StringResult rowguid
		{
			get
			{
				if (m_rowguid is null)
					m_rowguid = (StringResult)AliasFields["rowguid"];

				return m_rowguid;
			}
		}
		private StringResult m_rowguid = null;
		public DateTimeResult ModifiedDate
		{
			get
			{
				if (m_ModifiedDate is null)
					m_ModifiedDate = (DateTimeResult)AliasFields["ModifiedDate"];

				return m_ModifiedDate;
			}
		}
		private DateTimeResult m_ModifiedDate = null;
		public StringResult Uid
		{
			get
			{
				if (m_Uid is null)
					m_Uid = (StringResult)AliasFields["Uid"];

				return m_Uid;
			}
		}
		private StringResult m_Uid = null;
		public AsResult As(string aliasName, out ProductAlias alias)
		{
			alias = new ProductAlias((ProductNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class ProductListAlias : ListResult<ProductListAlias, ProductAlias>, IAliasListResult
	{
		private ProductListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class ProductJaggedListAlias : ListResult<ProductJaggedListAlias, ProductListAlias>, IAliasJaggedListResult
	{
		private ProductJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
