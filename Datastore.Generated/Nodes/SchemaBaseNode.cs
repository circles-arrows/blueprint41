using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		[Obsolete("This entity is virtual, consider making entity SchemaBase concrete or use another entity as your starting point.", true)]
		public static SchemaBaseNode SchemaBase { get { return new SchemaBaseNode(); } }
	}

	public partial class SchemaBaseNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return null;
        }

		internal SchemaBaseNode() { }
		internal SchemaBaseNode(SchemaBaseAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SchemaBaseNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public SchemaBaseNode Alias(out SchemaBaseAlias alias)
		{
			alias = new SchemaBaseAlias(this);
            NodeAlias = alias;
			return this;
		}

		public SchemaBaseNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }

		public AddressNode CastToAddress()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new AddressNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public AddressTypeNode CastToAddressType()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new AddressTypeNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public BillOfMaterialsNode CastToBillOfMaterials()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new BillOfMaterialsNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public ContactTypeNode CastToContactType()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new ContactTypeNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public CountryRegionNode CastToCountryRegion()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new CountryRegionNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public CreditCardNode CastToCreditCard()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new CreditCardNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public CultureNode CastToCulture()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new CultureNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public CurrencyNode CastToCurrency()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new CurrencyNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public CurrencyRateNode CastToCurrencyRate()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new CurrencyRateNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public CustomerNode CastToCustomer()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new CustomerNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public DepartmentNode CastToDepartment()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new DepartmentNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public DocumentNode CastToDocument()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new DocumentNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public EmployeeNode CastToEmployee()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new EmployeeNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public EmployeeDepartmentHistoryNode CastToEmployeeDepartmentHistory()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new EmployeeDepartmentHistoryNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public EmployeePayHistoryNode CastToEmployeePayHistory()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new EmployeePayHistoryNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public IllustrationNode CastToIllustration()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new IllustrationNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public JobCandidateNode CastToJobCandidate()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new JobCandidateNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public LocationNode CastToLocation()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new LocationNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public PersonNode CastToPerson()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new PersonNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public ProductNode CastToProduct()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new ProductNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public ProductCategoryNode CastToProductCategory()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new ProductCategoryNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public ProductCostHistoryNode CastToProductCostHistory()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new ProductCostHistoryNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public ProductDescriptionNode CastToProductDescription()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new ProductDescriptionNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public ProductInventoryNode CastToProductInventory()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new ProductInventoryNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public ProductListPriceHistoryNode CastToProductListPriceHistory()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new ProductListPriceHistoryNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public ProductModelNode CastToProductModel()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new ProductModelNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public ProductPhotoNode CastToProductPhoto()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new ProductPhotoNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public ProductProductPhotoNode CastToProductProductPhoto()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new ProductProductPhotoNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public ProductReviewNode CastToProductReview()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new ProductReviewNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public ProductVendorNode CastToProductVendor()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new ProductVendorNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public PurchaseOrderDetailNode CastToPurchaseOrderDetail()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new PurchaseOrderDetailNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public PurchaseOrderHeaderNode CastToPurchaseOrderHeader()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new PurchaseOrderHeaderNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public SalesOrderDetailNode CastToSalesOrderDetail()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new SalesOrderDetailNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public SalesOrderHeaderNode CastToSalesOrderHeader()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new SalesOrderHeaderNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public SalesPersonNode CastToSalesPerson()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new SalesPersonNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public SalesPersonQuotaHistoryNode CastToSalesPersonQuotaHistory()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new SalesPersonQuotaHistoryNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public SalesReasonNode CastToSalesReason()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new SalesReasonNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public SalesTaxRateNode CastToSalesTaxRate()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new SalesTaxRateNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public SalesTerritoryNode CastToSalesTerritory()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new SalesTerritoryNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public SalesTerritoryHistoryNode CastToSalesTerritoryHistory()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new SalesTerritoryHistoryNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public ScrapReasonNode CastToScrapReason()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new ScrapReasonNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public ShiftNode CastToShift()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new ShiftNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public ShipMethodNode CastToShipMethod()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new ShipMethodNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public ShoppingCartItemNode CastToShoppingCartItem()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new ShoppingCartItemNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public SpecialOfferNode CastToSpecialOffer()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new SpecialOfferNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public StateProvinceNode CastToStateProvince()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new StateProvinceNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public StoreNode CastToStore()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new StoreNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public TransactionHistoryNode CastToTransactionHistory()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new TransactionHistoryNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public TransactionHistoryArchiveNode CastToTransactionHistoryArchive()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new TransactionHistoryArchiveNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public UnitMeasureNode CastToUnitMeasure()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new UnitMeasureNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public VendorNode CastToVendor()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new VendorNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public WorkOrderNode CastToWorkOrder()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new WorkOrderNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public WorkOrderRoutingNode CastToWorkOrderRouting()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new WorkOrderRoutingNode(FromRelationship, Direction, this.Neo4jLabel);
        }

	}

    public class SchemaBaseAlias : AliasResult
    {
        internal SchemaBaseAlias(SchemaBaseNode parent)
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
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SchemaBase"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SchemaBase"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;


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
