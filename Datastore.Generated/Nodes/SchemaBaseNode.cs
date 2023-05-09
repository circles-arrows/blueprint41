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
		[Obsolete("This entity is virtual, consider making entity SchemaBase concrete or use another entity as your starting point.", true)]
		public static SchemaBaseNode SchemaBase { get { return new SchemaBaseNode(); } }
	}

	public partial class SchemaBaseNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(SchemaBaseNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(SchemaBaseNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return null;
		}

		protected override Entity GetEntity()
        {
			return null;
        }

		internal SchemaBaseNode() { }
		internal SchemaBaseNode(SchemaBaseAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SchemaBaseNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal SchemaBaseNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public SchemaBaseNode Where(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SchemaBaseAlias> alias = new Lazy<SchemaBaseAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public SchemaBaseNode Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SchemaBaseAlias> alias = new Lazy<SchemaBaseAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public SchemaBaseNode Alias(out SchemaBaseAlias alias)
        {
            if (NodeAlias is SchemaBaseAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new SchemaBaseAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public SchemaBaseNode Alias(out SchemaBaseAlias alias, string name)
        {
            if (NodeAlias is SchemaBaseAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new SchemaBaseAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public SchemaBaseNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public AddressNode CastToAddress()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new AddressNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public AddressTypeNode CastToAddressType()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new AddressTypeNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public BillOfMaterialsNode CastToBillOfMaterials()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new BillOfMaterialsNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public ContactTypeNode CastToContactType()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new ContactTypeNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public CountryRegionNode CastToCountryRegion()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new CountryRegionNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public CreditCardNode CastToCreditCard()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new CreditCardNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public CultureNode CastToCulture()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new CultureNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public CurrencyNode CastToCurrency()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new CurrencyNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public CurrencyRateNode CastToCurrencyRate()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new CurrencyRateNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public CustomerNode CastToCustomer()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new CustomerNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public DepartmentNode CastToDepartment()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new DepartmentNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public DocumentNode CastToDocument()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new DocumentNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public EmployeeNode CastToEmployee()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new EmployeeNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public EmployeeDepartmentHistoryNode CastToEmployeeDepartmentHistory()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new EmployeeDepartmentHistoryNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public EmployeePayHistoryNode CastToEmployeePayHistory()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new EmployeePayHistoryNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public IllustrationNode CastToIllustration()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new IllustrationNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public JobCandidateNode CastToJobCandidate()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new JobCandidateNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public LocationNode CastToLocation()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new LocationNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public PersonNode CastToPerson()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new PersonNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public ProductNode CastToProduct()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new ProductNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public ProductCategoryNode CastToProductCategory()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new ProductCategoryNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public ProductCostHistoryNode CastToProductCostHistory()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new ProductCostHistoryNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public ProductDescriptionNode CastToProductDescription()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new ProductDescriptionNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public ProductInventoryNode CastToProductInventory()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new ProductInventoryNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public ProductListPriceHistoryNode CastToProductListPriceHistory()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new ProductListPriceHistoryNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public ProductModelNode CastToProductModel()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new ProductModelNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public ProductPhotoNode CastToProductPhoto()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new ProductPhotoNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public ProductProductPhotoNode CastToProductProductPhoto()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new ProductProductPhotoNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public ProductReviewNode CastToProductReview()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new ProductReviewNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public ProductVendorNode CastToProductVendor()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new ProductVendorNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public PurchaseOrderDetailNode CastToPurchaseOrderDetail()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new PurchaseOrderDetailNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public PurchaseOrderHeaderNode CastToPurchaseOrderHeader()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new PurchaseOrderHeaderNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public SalesOrderDetailNode CastToSalesOrderDetail()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new SalesOrderDetailNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public SalesOrderHeaderNode CastToSalesOrderHeader()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new SalesOrderHeaderNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public SalesPersonNode CastToSalesPerson()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new SalesPersonNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public SalesPersonQuotaHistoryNode CastToSalesPersonQuotaHistory()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new SalesPersonQuotaHistoryNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public SalesReasonNode CastToSalesReason()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new SalesReasonNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public SalesTaxRateNode CastToSalesTaxRate()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new SalesTaxRateNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public SalesTerritoryNode CastToSalesTerritory()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new SalesTerritoryNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public SalesTerritoryHistoryNode CastToSalesTerritoryHistory()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new SalesTerritoryHistoryNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public ScrapReasonNode CastToScrapReason()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new ScrapReasonNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public ShiftNode CastToShift()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new ShiftNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public ShipMethodNode CastToShipMethod()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new ShipMethodNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public ShoppingCartItemNode CastToShoppingCartItem()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new ShoppingCartItemNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public SpecialOfferNode CastToSpecialOffer()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new SpecialOfferNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public StateProvinceNode CastToStateProvince()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new StateProvinceNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public StoreNode CastToStore()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new StoreNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public TransactionHistoryNode CastToTransactionHistory()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new TransactionHistoryNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public TransactionHistoryArchiveNode CastToTransactionHistoryArchive()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new TransactionHistoryArchiveNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public UnitMeasureNode CastToUnitMeasure()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new UnitMeasureNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public VendorNode CastToVendor()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new VendorNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public WorkOrderNode CastToWorkOrder()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new WorkOrderNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

		public WorkOrderRoutingNode CastToWorkOrderRouting()
		{
			if (this.Neo4jLabel is null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

			if (FromRelationship is null)
				throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

			return new WorkOrderRoutingNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
		}

	}

	public class SchemaBaseAlias : AliasResult<SchemaBaseAlias, SchemaBaseListAlias>
	{
		internal SchemaBaseAlias(SchemaBaseNode parent)
		{
			Node = parent;
		}
		internal SchemaBaseAlias(SchemaBaseNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  SchemaBaseAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  SchemaBaseAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  SchemaBaseAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Uid.HasValue) assignments.Add(new Assignment(this.Uid, Uid));
            
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
		public AsResult As(string aliasName, out SchemaBaseAlias alias)
		{
			alias = new SchemaBaseAlias((SchemaBaseNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class SchemaBaseListAlias : ListResult<SchemaBaseListAlias, SchemaBaseAlias>, IAliasListResult
	{
		private SchemaBaseListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SchemaBaseListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SchemaBaseListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class SchemaBaseJaggedListAlias : ListResult<SchemaBaseJaggedListAlias, SchemaBaseListAlias>, IAliasJaggedListResult
	{
		private SchemaBaseJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SchemaBaseJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SchemaBaseJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
