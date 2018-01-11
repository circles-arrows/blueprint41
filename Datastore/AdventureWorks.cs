using Blueprint41;
using System;

namespace Datastore
{
    public partial class AdventureWorks : DatastoreModel<AdventureWorks>
    {
        [Version(1,0,0)]
        protected void Initial()
        {
            AddNewEntities();
            AddNewRelationships();
        }

        protected override void SubscribeEventHandlers()
        {
        }

        #region Entities
        public Entity Neo4jBase { get; private set; }
        public Entity SchemaBase { get; private set; }
        public Entity Address { get; private set; }
        public Entity AddressType { get; private set; }
        public Entity BillOfMaterials { get; private set; }
        public Entity ContactType { get; private set; }
        public Entity CountryRegion { get; private set; }
        public Entity CreditCard { get; private set; }
        public Entity Culture { get; private set; }
        public Entity Currency { get; private set; }
        public Entity CurrencyRate { get; private set; }
        public Entity Customer { get; private set; }
        public Entity Department { get; private set; }
        public Entity Document { get; private set; }
        public Entity EmailAddress { get; private set; }
        public Entity Employee { get; private set; }
        public Entity EmployeeDepartmentHistory { get; private set; }
        public Entity EmployeePayHistory { get; private set; }
        public Entity Illustration { get; private set; }
        public Entity JobCandidate { get; private set; }
        public Entity Location { get; private set; }
        public Entity Password { get; private set; }
        public Entity Person { get; private set; }
        public Entity PhoneNumberType { get; private set; }
        public Entity Product { get; private set; }
        public Entity ProductCategory { get; private set; }
        public Entity ProductCostHistory { get; private set; }
        public Entity ProductDescription { get; private set; }
        public Entity ProductInventory { get; private set; }
        public Entity ProductListPriceHistory { get; private set; }
        public Entity ProductModel { get; private set; }
        public Entity ProductPhoto { get; private set; }
        public Entity ProductProductPhoto { get; private set; }
        public Entity ProductReview { get; private set; }
        public Entity ProductVendor { get; private set; }
        public Entity PurchaseOrderDetail { get; private set; }
        public Entity PurchaseOrderHeader { get; private set; }
        public Entity SalesOrderDetail { get; private set; }
        public Entity SalesOrderHeader { get; private set; }
        public Entity SalesPerson { get; private set; }
        public Entity SalesPersonQuotaHistory { get; private set; }
        public Entity SalesReason { get; private set; }
        public Entity SalesTaxRate { get; private set; }
        public Entity SalesTerritory { get; private set; }
        public Entity SalesTerritoryHistory { get; private set; }
        public Entity ScrapReason { get; private set; }
        public Entity Shift { get; private set; }
        public Entity ShipMethod { get; private set; }
        public Entity ShoppingCartItem { get; private set; }
        public Entity SpecialOffer { get; private set; }
        public Entity StateProvince { get; private set; }
        public Entity Store { get; private set; }
        public Entity TransactionHistory { get; private set; }
        public Entity TransactionHistoryArchive { get; private set; }
        public Entity UnitMeasure { get; private set; }
        public Entity Vendor { get; private set; }
        public Entity WorkOrder { get; private set; }
        public Entity WorkOrderRouting { get; private set; }

        private void AddNewEntities()
        {

            #region FunctionalIds

            FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

            #endregion

            Neo4jBase =
                Entities.New("Neo4jBase")
                .Abstract(true)
                .Virtual(true)
                .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                .SetKey("Uid", true);

            SchemaBase =
                Entities.New("SchemaBase", Neo4jBase)
                .Abstract(true)
                .Virtual(true)
                .AddProperty("ModifiedDate", typeof(DateTime), false);

            Address =
                Entities.New("Address", SchemaBase)
                .AddProperty("AddressLine1", typeof(string), false)
                .AddProperty("AddressLine2", typeof(string))
                .AddProperty("City", typeof(string), false)
                .AddProperty("PostalCode", typeof(string), false)
                .AddProperty("SpatialLocation", typeof(string))
                .AddProperty("rowguid", typeof(string), false);


            AddressType =
                Entities.New("AddressType", SchemaBase)
                .AddProperty("Name", typeof(string), false)
                .AddProperty("rowguid", typeof(string), false);

            BillOfMaterials =
                Entities.New("BillOfMaterials", SchemaBase)
                .AddProperty("StartDate", typeof(DateTime), false)
                .AddProperty("EndDate", typeof(DateTime))
                .AddProperty("UnitMeasureCode", typeof(string), false)
                .AddProperty("BOMLevel", typeof(string), false)
                .AddProperty("PerAssemblyQty", typeof(int), false);

            ContactType =
                Entities.New("ContactType", SchemaBase)
                .AddProperty("Name", typeof(string), false);

            CountryRegion =
                Entities.New("CountryRegion", SchemaBase)
                .AddProperty("Code", typeof(int), false, IndexType.Unique)
                .AddProperty("Name", typeof(string), false);

            CreditCard =
                Entities.New("CreditCard", SchemaBase)
                .AddProperty("CardType", typeof(string), false)
                .AddProperty("CardNumber", typeof(string), false)
                .AddProperty("ExpMonth", typeof(string), false)
                .AddProperty("ExpYear", typeof(string), false);

            Culture =
                Entities.New("Culture", SchemaBase)
                .AddProperty("Name", typeof(string), false);

            Currency =
                Entities.New("Currency", SchemaBase)
                .AddProperty("CurrencyCode", typeof(string), false)
                .AddProperty("Name", typeof(string), false);

            CurrencyRate =
                Entities.New("CurrencyRate", SchemaBase)
                .AddProperty("CurrencyRateDate", typeof(DateTime), false)
                .AddProperty("FromCurrencyCode", typeof(string), false)
                .AddProperty("ToCurrencyCode", typeof(string), false)
                .AddProperty("AverageRate", typeof(string), false)
                .AddProperty("EndOfDayRate", typeof(string), false);

            Customer =
                Entities.New("Customer", SchemaBase)
                .AddProperty("AccountNumber", typeof(string), false)
                .AddProperty("rowguid", typeof(string), false);

            Department =
                Entities.New("Department", SchemaBase)
                .AddProperty("Name", typeof(string), false)
                .AddProperty("GroupName", typeof(string), false);

            Document =
                Entities.New("Document", SchemaBase)
                .AddProperty("DocumentNode", typeof(string), false)
                .AddProperty("DocumentLevel", typeof(string), false)
                .AddProperty("Title", typeof(string), false)
                .AddProperty("Owner", typeof(string), false)
                .AddProperty("FolderFlag", typeof(string), false)
                .AddProperty("FileName", typeof(string), false)
                .AddProperty("FileExtension", typeof(string), false)
                .AddProperty("Revision", typeof(string), false)
                .AddProperty("ChangeNumber", typeof(string), false)
                .AddProperty("Status", typeof(string), false)
                .AddProperty("DocumentSummary", typeof(string))
                .AddProperty("Doc", typeof(string))
                .AddProperty("rowguid", typeof(string), false);

            EmailAddress =
                Entities.New("EmailAddress", Neo4jBase)
                .AddProperty("EmailAddr", typeof(string));

            Employee =
                Entities.New("Employee", SchemaBase)
                .AddProperty("NationalIDNumber", typeof(string), false)
                .AddProperty("LoginID", typeof(int), false)
                .AddProperty("JobTitle", typeof(string), false)
                .AddProperty("BirthDate", typeof(DateTime), false)
                .AddProperty("MaritalStatus", typeof(string), false)
                .AddProperty("Gender", typeof(string), false)
                .AddProperty("HireDate", typeof(string), false)
                .AddProperty("SalariedFlag", typeof(string), false)
                .AddProperty("VacationHours", typeof(string), false)
                .AddProperty("SickLeaveHours", typeof(string), false)
                .AddProperty("Currentflag", typeof(string), false)
                .AddProperty("rowguid", typeof(string), false);


            EmployeeDepartmentHistory =
                Entities.New("EmployeeDepartmentHistory", SchemaBase)
                .AddProperty("StartDate", typeof(DateTime), false)
                .AddProperty("EndDate", typeof(string));

            EmployeePayHistory =
                Entities.New("EmployeePayHistory", SchemaBase)
                .AddProperty("RateChangeDate", typeof(DateTime), false)
                .AddProperty("Rate", typeof(string), false)
                .AddProperty("PayFrequency", typeof(string), false);

            Illustration =
                Entities.New("Illustration", SchemaBase)
                .AddProperty("Diagram", typeof(string), false);

            JobCandidate =
                Entities.New("JobCandidate", SchemaBase)
                .AddProperty("JobCandidateID", typeof(int), false)
                .AddProperty("Resume", typeof(string));

            Location =
                Entities.New("Location", SchemaBase)
                .AddProperty("Name", typeof(string), false)
                .AddProperty("CostRate", typeof(string), false)
                .AddProperty("Availability", typeof(string), false);

            Password =
                Entities.New("Password", Neo4jBase)
                .AddProperty("PasswordHash", typeof(string), false)
                .AddProperty("PasswordSalt", typeof(string), false);


            Person =
                Entities.New("Person", SchemaBase)
                .AddProperty("PersonType", typeof(int))
                .AddProperty("NameStyle", typeof(string), false)
                .AddProperty("Title", typeof(string))
                .AddProperty("FirstName", typeof(string), false)
                .AddProperty("MiddleName", typeof(string))
                .AddProperty("LastName", typeof(string), false)
                .AddProperty("Suffix", typeof(string))
                .AddProperty("EmailPromotion", typeof(string), false)
                .AddProperty("AdditionalContactInfo", typeof(string))
                .AddProperty("Demographics", typeof(string))
                .AddProperty("rowguid", typeof(string), false);

            PhoneNumberType =
                Entities.New("PhoneNumberType", Neo4jBase)
                .AddProperty("Name", typeof(string));

            Product =
                Entities.New("Product", SchemaBase)
                .AddProperty("Name", typeof(string), false)
                .AddProperty("ProductNumber", typeof(string), false)
                .AddProperty("MakeFlag", typeof(bool), false)
                .AddProperty("FinishedGoodsFlag", typeof(bool), false)
                .AddProperty("Color", typeof(string))
                .AddProperty("SafetyStockLevel", typeof(int), false)
                .AddProperty("ReorderPoint", typeof(int), false)
                .AddProperty("StandardCost", typeof(double), false)
                .AddProperty("ListPrice", typeof(double), false)
                .AddProperty("Size", typeof(string))
                .AddProperty("SizeUnitMeasureCode", typeof(string))
                .AddProperty("WeightUnitMeasureCode", typeof(string))
                .AddProperty("Weight", typeof(decimal))
                .AddProperty("DaysToManufacture", typeof(int), false)
                .AddProperty("ProductLine", typeof(string))
                .AddProperty("Class", typeof(string))
                .AddProperty("Style", typeof(string))
                .AddProperty("SellStartDate", typeof(DateTime), false)
                .AddProperty("SellEndDate", typeof(DateTime))
                .AddProperty("DiscontinuedDate", typeof(DateTime))
                .AddProperty("rowguid", typeof(string), false);

            ProductCategory =
                Entities.New("ProductCategory", SchemaBase)
                .AddProperty("Name", typeof(string), false)
                .AddProperty("rowguid", typeof(string), false);

            ProductCostHistory =
                Entities.New("ProductCostHistory", SchemaBase)
                .AddProperty("StartDate", typeof(DateTime), false)
                .AddProperty("EndDate", typeof(DateTime), false)
                .AddProperty("StandardCost", typeof(string), false);

            ProductDescription =
                Entities.New("ProductDescription", SchemaBase)
                .AddProperty("Description", typeof(string), false)
                .AddProperty("rowguid", typeof(string), false);

            ProductInventory =
                Entities.New("ProductInventory", SchemaBase)
                .AddProperty("Shelf", typeof(string), false)
                .AddProperty("Bin", typeof(string), false)
                .AddProperty("Quantity", typeof(int), false)
                .AddProperty("rowguid", typeof(string), false);

            ProductListPriceHistory =
                Entities.New("ProductListPriceHistory", SchemaBase)
                .AddProperty("StartDate", typeof(DateTime), false)
                .AddProperty("EndDate", typeof(DateTime), false)
                .AddProperty("ListPrice", typeof(string), false);

            ProductModel =
                Entities.New("ProductModel", SchemaBase)
                .AddProperty("Name", typeof(string), false)
                .AddProperty("CatalogDescription", typeof(string))
                .AddProperty("Instructions", typeof(string))
                .AddProperty("rowguid", typeof(string), false);
          
            ProductPhoto =
                Entities.New("ProductPhoto", SchemaBase)
                .AddProperty("ThumbNailPhoto", typeof(string))
                .AddProperty("ThumbNailPhotoFileName", typeof(string))
                .AddProperty("LargePhoto", typeof(string))
                .AddProperty("LargePhotoFileName", typeof(string));

            ProductProductPhoto =
                Entities.New("ProductProductPhoto", SchemaBase)
                .AddProperty("Primary", typeof(string), false);

            ProductReview =
                Entities.New("ProductReview", SchemaBase)
                .AddProperty("ReviewerName", typeof(string), false)
                .AddProperty("ReviewDate", typeof(DateTime), false)
                .AddProperty("EmailAddress", typeof(string), false)
                .AddProperty("Rating", typeof(string), false)
                .AddProperty("Comments", typeof(string), false);

            ProductVendor =
                Entities.New("ProductVendor", SchemaBase)
                .AddProperty("AverageLeadTime", typeof(string), false)
                .AddProperty("StandardPrice", typeof(string), false)
                .AddProperty("LastReceiptCost", typeof(string))
                .AddProperty("LastReceiptDate", typeof(DateTime))
                .AddProperty("MinOrderQty", typeof(int), false)
                .AddProperty("MaxOrderQty", typeof(int), false)
                .AddProperty("OnOrderQty", typeof(int), false)
                .AddProperty("UnitMeasureCode", typeof(string));

            PurchaseOrderDetail =
                Entities.New("PurchaseOrderDetail", SchemaBase)
                .AddProperty("DueDate", typeof(DateTime), false)
                .AddProperty("OrderQty", typeof(int), false)
                .AddProperty("UnitPrice", typeof(double), false)
                .AddProperty("LineTotal", typeof(string), false)
                .AddProperty("ReceivedQty", typeof(int), false)
                .AddProperty("RejectedQty", typeof(int), false)
                .AddProperty("StockedQty", typeof(int), false);

            PurchaseOrderHeader =
                Entities.New("PurchaseOrderHeader", SchemaBase)
                .AddProperty("RevisionNumber", typeof(string), false)
                .AddProperty("Status", typeof(string), false)
                .AddProperty("OrderDate", typeof(DateTime), false)
                .AddProperty("ShipDate", typeof(DateTime), false)
                .AddProperty("SubTotal", typeof(double), false)
                .AddProperty("TaxAmt", typeof(double), false)
                .AddProperty("Freight", typeof(string), false)
                .AddProperty("TotalDue", typeof(double), false);

            SalesOrderDetail =
                Entities.New("SalesOrderDetail", SchemaBase)
                .AddProperty("CarrierTrackingNumber", typeof(string))
                .AddProperty("OrderQty", typeof(int), false)
                .AddProperty("UnitPrice", typeof(double), false)
                .AddProperty("UnitPriceDiscount", typeof(string), false)
                .AddProperty("LineTotal", typeof(string), false)
                .AddProperty("rowguid", typeof(string), false);

            SalesOrderHeader =
                Entities.New("SalesOrderHeader", SchemaBase)
                .AddProperty("RevisionNumber", typeof(string), false)
                .AddProperty("OrderDate", typeof(DateTime), false)
                .AddProperty("DueDate", typeof(DateTime), false)
                .AddProperty("ShipDate", typeof(DateTime))
                .AddProperty("Status", typeof(string), false)
                .AddProperty("OnlineOrderFlag", typeof(string), false)
                .AddProperty("SalesOrderNumber", typeof(string), false)
                .AddProperty("PurchaseOrderNumber", typeof(string))
                .AddProperty("AccountNumber", typeof(string))
                .AddProperty("CreditCardID", typeof(int))
                .AddProperty("CreditCardApprovalCode", typeof(string))
                .AddProperty("CurrencyRateID", typeof(int))
                .AddProperty("SubTotal", typeof(string), false)
                .AddProperty("TaxAmt", typeof(string), false)
                .AddProperty("Freight", typeof(string), false)
                .AddProperty("TotalDue", typeof(string), false)
                .AddProperty("Comment", typeof(string))
                .AddProperty("rowguid", typeof(string), false);

            SalesPerson =
                Entities.New("SalesPerson", SchemaBase)
                .AddProperty("SalesQuota", typeof(string))
                .AddProperty("Bonus", typeof(string), false)
                .AddProperty("CommissionPct", typeof(string), false)
                .AddProperty("SalesYTD", typeof(string), false)
                .AddProperty("SalesLastYear", typeof(string), false)
                .AddProperty("rowguid", typeof(string), false);

            SalesPersonQuotaHistory =
                Entities.New("SalesPersonQuotaHistory", SchemaBase)
                .AddProperty("QuotaDate", typeof(DateTime), false)
                .AddProperty("SalesQuota", typeof(string), false)
                .AddProperty("rowguid", typeof(string), false);

            SalesReason =
                Entities.New("SalesReason", SchemaBase)
                .AddProperty("Name", typeof(string), false)
                .AddProperty("ReasonType", typeof(string), false);

            SalesTaxRate =
                Entities.New("SalesTaxRate", SchemaBase)
                .AddProperty("TaxType", typeof(string), false)
                .AddProperty("TaxRate", typeof(string), false)
                .AddProperty("Name", typeof(string), false)
                .AddProperty("rowguid", typeof(string), false);

            SalesTerritory =
                Entities.New("SalesTerritory", SchemaBase)
                .AddProperty("Name", typeof(string), false)
                .AddProperty("CountryRegionCode", typeof(string), false)
                .AddProperty("Group", typeof(string), false)
                .AddProperty("SalesYTD", typeof(string), false)
                .AddProperty("SalesLastYear", typeof(string), false)
                .AddProperty("CostYTD", typeof(string), false)
                .AddProperty("CostLastYear", typeof(string), false)
                .AddProperty("rowguid", typeof(string), false);

            SalesTerritoryHistory =
                Entities.New("SalesTerritoryHistory", SchemaBase)
                .AddProperty("StartDate", typeof(DateTime), false)
                .AddProperty("EndDate", typeof(DateTime))
                .AddProperty("rowguid", typeof(string), false);

            ScrapReason =
                Entities.New("ScrapReason", SchemaBase)
                .AddProperty("Name", typeof(string), false);

            Shift =
                Entities.New("Shift", SchemaBase)
                .AddProperty("Name", typeof(string), false)
                .AddProperty("StartTime", typeof(DateTime), false)
                .AddProperty("EndTime", typeof(DateTime), false);

            ShipMethod =
                Entities.New("ShipMethod", SchemaBase)
                .AddProperty("Name", typeof(string), false)
                .AddProperty("ShipBase", typeof(string), false)
                .AddProperty("ShipRate", typeof(string), false)
                .AddProperty("rowguid", typeof(string), false);

            ShoppingCartItem =
                Entities.New("ShoppingCartItem", SchemaBase)
                .AddProperty("Quantity", typeof(int), false)
                .AddProperty("DateCreated", typeof(DateTime), false);

            SpecialOffer =
                Entities.New("SpecialOffer", SchemaBase)
                .AddProperty("Description", typeof(string), false)
                .AddProperty("DiscountPct", typeof(string), false)
                .AddProperty("Type", typeof(string), false)
                .AddProperty("Category", typeof(string), false)
                .AddProperty("StartDate", typeof(DateTime), false)
                .AddProperty("EndDate", typeof(DateTime), false)
                .AddProperty("MinQty", typeof(int), false)
                .AddProperty("MaxQty", typeof(string), false)
                .AddProperty("rowguid", typeof(string), false);

            StateProvince =
                Entities.New("StateProvince", SchemaBase)
                .AddProperty("StateProvinceCode", typeof(string), false)
                .AddProperty("CountryRegionCode", typeof(string), false)
                .AddProperty("IsOnlyStateProvinceFlag", typeof(bool), false)
                .AddProperty("Name", typeof(string))
                .AddProperty("rowguid", typeof(string), false);

            Store =
                Entities.New("Store", SchemaBase)
                .AddProperty("Name", typeof(string), false)
                .AddProperty("Demographics", typeof(string))
                .AddProperty("rowguid", typeof(string), false);

            TransactionHistory =
                Entities.New("TransactionHistory", SchemaBase)
                .AddProperty("ReferenceOrderID", typeof(int), false)
                .AddProperty("TransactionDate", typeof(DateTime), false)
                .AddProperty("TransactionType", typeof(string), false)
                .AddProperty("Quantity", typeof(int), false)
                .AddProperty("ActualCost", typeof(string), false)
                .AddProperty("ReferenceOrderLineID", typeof(int), false);

            TransactionHistoryArchive =
                Entities.New("TransactionHistoryArchive", SchemaBase)
                .AddProperty("ReferenceOrderID", typeof(int), false)
                .AddProperty("TransactionDate", typeof(DateTime), false)
                .AddProperty("TransactionType", typeof(string), false)
                .AddProperty("Quantity", typeof(int), false)
                .AddProperty("ActualCost", typeof(decimal), false)
                .AddProperty("ReferenceOrderLineID", typeof(int), false);

            UnitMeasure =
                Entities.New("UnitMeasure", SchemaBase)
                .AddProperty("UnitMeasureCorde", typeof(string), false)
                .AddProperty("Name", typeof(string), false);

            Vendor =
                Entities.New("Vendor", SchemaBase)
                .AddProperty("AccountNumber", typeof(string), false)
                .AddProperty("Name", typeof(string), false)
                .AddProperty("CreditRating", typeof(string), false)
                .AddProperty("PreferredVendorStatus", typeof(string), false)
                .AddProperty("ActiveFlag", typeof(string), false)
                .AddProperty("PurchasingWebServiceURL", typeof(string));

            WorkOrder =
                Entities.New("WorkOrder", SchemaBase)
                .AddProperty("OrderQty", typeof(int), false)
                .AddProperty("StockedQty", typeof(int), false)
                .AddProperty("ScrappedQty", typeof(int), false)
                .AddProperty("StartDate", typeof(DateTime), false)
                .AddProperty("EndDate", typeof(DateTime))
                .AddProperty("DueDate", typeof(DateTime), false);

            WorkOrderRouting =
                Entities.New("WorkOrderRouting", SchemaBase)
                .AddProperty("OperationSequence", typeof(string), false)
                .AddProperty("ScheduledStartDate", typeof(DateTime), false)
                .AddProperty("ScheduledEndDate", typeof(DateTime), false)
                .AddProperty("ActualStartDate", typeof(DateTime), false)
                .AddProperty("ActualEndDate", typeof(DateTime), false)
                .AddProperty("ActualResourceHrs", typeof(string), false)
                .AddProperty("PlannedCost", typeof(string), false)
                .AddProperty("ActualCost", typeof(string), false);
        }

        #endregion

        #region Relationships

        public Relationship ADDRESS_HAS_STATEPROVINCE { get; private set; }
        public Relationship ADDRESS_HAS_ADDRESSTYPE { get; private set; }
        public Relationship BILLOFMATERIALS_HAS_UNITMEASURE { get; private set; }
        public Relationship BILLOFMATERIALS_HAS_PRODUCT { get; private set; }
        public Relationship CURRENCYRATE_HAS_CURRENCY { get; private set; }
        public Relationship CUSTOMER_HAS_STORE { get; private set; }
        public Relationship CUSTOMER_HAS_SALESTERRITORY { get; private set; }
        public Relationship CUSTOMER_HAS_PERSON { get; private set; }
        public Relationship DEPARTMENT_CONTAINS_EMPLOYEE { get; private set; }
        public Relationship EMPLOYEE_HAS_EMPLOYEEPAYHISTORY { get; private set; }
        public Relationship EMPLOYEE_BECOMES_SALESPERSON { get; private set; }
        public Relationship EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY { get; private set; }
        public Relationship EMPLOYEE_HAS_SHIFT { get; private set; }
        public Relationship EMPLOYEE_IS_JOBCANDIDATE { get; private set; }
        public Relationship EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT { get; private set; }
        public Relationship EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT { get; private set; }
        public Relationship PERSON_HAS_PASSWORD { get; private set; }
        public Relationship PERSON_HAS_EMAILADDRESS { get; private set; }
        public Relationship PERSON_VALID_FOR_DOCUMENT { get; private set; }
        public Relationship PERSON_BECOMES_EMPLOYEE { get; private set; }
        public Relationship PERSON_VALID_FOR_CREDITCARD { get; private set; }
        public Relationship PERSON_HAS_CONTACTTYPE { get; private set; }
        public Relationship PERSON_HAS_PHONENUMBERTYPE { get; private set; }
        public Relationship PERSON_HAS_ADDRESS { get; private set; }
        public Relationship PRODUCT_HAS_TRANSACTIONHISTORY { get; private set; }
        public Relationship PRODUCT_VALID_FOR_PRODUCTREVIEW { get; private set; }
        public Relationship PRODUCT_HAS_PRODUCTPRODUCTPHOTO { get; private set; }
        public Relationship PRODUCT_HAS_PRODUCTMODEL { get; private set; }
        public Relationship PRODUCT_HAS_DOCUMENT { get; private set; }
        public Relationship PRODUCTCOSTHISTORY_HAS_PRODUCT { get; private set; }
        public Relationship PRODUCTINVENTORY_HAS_LOCATION { get; private set; }
        public Relationship PRODUCTINVENTORY_HAS_PRODUCT { get; private set; }
        public Relationship PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT { get; private set; }
        public Relationship PRODUCTMODEL_HAS_ILLUSTRATION { get; private set; }
        public Relationship PRODUCTMODEL_HAS_PRODUCTDESCRIPTION { get; private set; }
        public Relationship PRODUCTMODEL_HAS_CULTURE { get; private set; }
        public Relationship PRODUCTMODELPRODUCTDESCRIPTIONCULTURE_HAS_PRODUCTMODEL { get; private set; }
        public Relationship PRODUCTMODELPRODUCTDESCRIPTIONCULTURE_HAS_PRODUCTDESCRIPTION { get; private set; }
        public Relationship PRODUCTMODELPRODUCTDESCRIPTIONCULTURE_HAS_CULTURE { get; private set; }
        public Relationship PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO { get; private set; }
        public Relationship PRODUCTVENDOR_HAS_UNITMEASURE { get; private set; }
        public Relationship PRODUCTVENDOR_HAS_PRODUCT { get; private set; }
        public Relationship PURCHASEORDERDETAIL_HAS_PRODUCT { get; private set; }
        public Relationship PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER { get; private set; }
        public Relationship PURCHASEORDERHEADER_HAS_VENDOR { get; private set; }
        public Relationship PURCHASEORDERHEADER_HAS_SHIPMETHOD { get; private set; }
        public Relationship SALESORDERDETAIL_HAS_SALESORDERHEADER { get; private set; }
        public Relationship SALESORDERDETAIL_HAS_PRODUCT { get; private set; }
        public Relationship SALESORDERDETAIL_HAS_SPECIALOFFER { get; private set; }
        public Relationship SALESORDERHEADER_HAS_CURRENCYRATE { get; private set; }
        public Relationship SALESORDERHEADER_HAS_CREDITCARD { get; private set; }
        public Relationship SALESORDERHEADER_HAS_ADDRESS { get; private set; }
        public Relationship SALESORDERHEADER_HAS_SHIPMETHOD { get; private set; }
        public Relationship SALESORDERHEADER_HAS_SALESREASON { get; private set; }
        public Relationship SALESORDERHEADER_CONTAINS_SALESTERRITORY { get; private set; }
        public Relationship SALESPERSON_HAS_SALESPERSONQUOTAHISTORY { get; private set; }
        public Relationship SALESPERSON_HAS_SALESTERRITORY { get; private set; }
        public Relationship SALESPERSON_IS_PERSON { get; private set; }
        public Relationship SALESTAXRATE_HAS_STATEPROVINCE { get; private set; }
        public Relationship SALESTERRITORY_HAS_SALESTERRITORYHISTORY { get; private set; }
        public Relationship SHOPPINGCARTITEM_HAS_PRODUCT { get; private set; }
        public Relationship STATEPROVINCE_HAS_COUNTRYREGION { get; private set; }
        public Relationship STATEPROVINCE_HAS_SALESTERRITORY { get; private set; }
        public Relationship STORE_VALID_FOR_SALESPERSON { get; private set; }
        public Relationship STORE_HAS_ADDRESS { get; private set; }
        public Relationship TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT { get; private set; }
        public Relationship VENDOR_VALID_FOR_EMPLOYEE { get; private set; }
        public Relationship VENDOR_BECOMES_PRODUCTVENDOR { get; private set; }
        public Relationship WORKORDER_HAS_SCRAPREASON { get; private set; }
        public Relationship WORKORDER_HAS_PRODUCT { get; private set; }
        public Relationship WORKORDERROUTING_HAS_WORKORDER { get; private set; }
        public Relationship WORKORDERROUTING_HAS_PRODUCT { get; private set; }
        public Relationship WORKORDERROUTING_HAS_LOCATION { get; private set; }


        private void AddNewRelationships()
        {

            ADDRESS_HAS_STATEPROVINCE =
                Relations.New(Address, StateProvince, "ADDRESS_HAS_STATEPROVINCE", "HAS_STATEPROVINCE")
                    .SetInProperty("StateProvince", PropertyType.Lookup);
            ADDRESS_HAS_ADDRESSTYPE =
                Relations.New(Address, AddressType, "ADDRESS_HAS_ADDRESSTYPE", "HAS_ADDRESSTYPE")
                    .SetInProperty("AddressType", PropertyType.Lookup);

            BILLOFMATERIALS_HAS_UNITMEASURE =
                Relations.New(BillOfMaterials, UnitMeasure, "BILLOFMATERIALS_HAS_UNITMEASURE", "HAS_UNITMEASURE")
                    .SetInProperty("UnitMeasure", PropertyType.Lookup);
            BILLOFMATERIALS_HAS_PRODUCT =
                Relations.New(BillOfMaterials, Product, "BILLOFMATERIALS_HAS_PRODUCT", "HAS_PRODUCT")
                    .SetInProperty("Product", PropertyType.Lookup);

            CURRENCYRATE_HAS_CURRENCY =
                Relations.New(CurrencyRate, Currency, "CURRENCYRATE_HAS_CURRENCY", "HAS_CURRENCY")
                    .SetInProperty("Currency", PropertyType.Lookup);

            CUSTOMER_HAS_STORE =
                Relations.New(Customer, Store, "CUSTOMER_HAS_STORE", "HAS_STORE")
                    .SetInProperty("Store", PropertyType.Lookup);
            CUSTOMER_HAS_SALESTERRITORY =
                Relations.New(Customer, SalesTerritory, "CUSTOMER_HAS_SALESTERRITORY", "HAS_SALESTERRITORY")
                    .SetInProperty("SalesTerritory", PropertyType.Lookup);
            CUSTOMER_HAS_PERSON =
                Relations.New(Customer, Person, "CUSTOMER_HAS_PERSON", "HAS_PERSON")
                    .SetInProperty("Person", PropertyType.Lookup);

            DEPARTMENT_CONTAINS_EMPLOYEE =
                Relations.New(Department, Employee, "DEPARTMENT_CONTAINS_EMPLOYEE", "CONTAINS_EMPLOYEE")
                    .SetInProperty("Employees", PropertyType.Collection);

            EMPLOYEE_HAS_EMPLOYEEPAYHISTORY =
                Relations.New(Employee, EmployeePayHistory, "EMPLOYEE_HAS_EMPLOYEEPAYHISTORY", "HAS_EMPLOYEEPAYHISTORY")
                    .SetInProperty("EmployeePayHistory", PropertyType.Lookup);
            EMPLOYEE_BECOMES_SALESPERSON =
                Relations.New(Employee, SalesPerson, "EMPLOYEE_BECOMES_SALESPERSON", "BECOMES_SALESPERSON")
                    .SetInProperty("SalesPerson", PropertyType.Lookup);
            EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY =
                Relations.New(Employee, EmployeeDepartmentHistory, "EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY", "HAS_EMPLOYEEDEPARTMENTHISTORY")
                    .SetInProperty("EmployeeDepartmentHistory", PropertyType.Lookup);
            EMPLOYEE_HAS_SHIFT =
                Relations.New(Employee, Shift, "EMPLOYEE_HAS_SHIFT", "HAS_SHIFT")
                    .SetInProperty("Shift", PropertyType.Lookup);
            EMPLOYEE_IS_JOBCANDIDATE =
                Relations.New(Employee, JobCandidate, "EMPLOYEE_IS_JOBCANDIDATE", "IS_JOBCANDIDATE")
                    .SetInProperty("JobCandidate", PropertyType.Lookup)
                    .SetOutProperty("Employee", PropertyType.Lookup);

            EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT =
                Relations.New(EmployeeDepartmentHistory, Department, "EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT", "VALID_FOR_DEPARTMENT")
                    .SetInProperty("Department", PropertyType.Lookup)
                    .SetOutProperty("EmployeeDepartmentHistories", PropertyType.Collection);
            EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT =
                Relations.New(EmployeeDepartmentHistory, Shift, "EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT", "HAS_SHIFT")
                    .SetInProperty("Shift", PropertyType.Lookup);

            PERSON_HAS_PASSWORD =
                Relations.New(Person, Password, "PERSON_HAS_PASSWORD", "HAS_PASSWORD")
                    .SetInProperty("Password", PropertyType.Lookup);
            PERSON_HAS_EMAILADDRESS =
                Relations.New(Person, EmailAddress, "PERSON_HAS_EMAILADDRESS", "HAS_EMAILADDRESS")
                    .SetInProperty("EmailAddress", PropertyType.Lookup)
                    .SetOutProperty("EmailAddresses", PropertyType.Collection);
            PERSON_VALID_FOR_DOCUMENT =
                Relations.New(Person, Document, "PERSON_VALID_FOR_DOCUMENT", "VALID_FOR_DOCUMENT")
                    .SetInProperty("Document", PropertyType.Lookup)
                    .SetOutProperty("Persons", PropertyType.Collection);
            PERSON_BECOMES_EMPLOYEE =
                Relations.New(Person, Employee, "PERSON_BECOMES_EMPLOYEE", "BECOMES_EMPLOYEE")
                    .SetInProperty("Employee", PropertyType.Lookup);
            PERSON_VALID_FOR_CREDITCARD =
                Relations.New(Person, CreditCard, "PERSON_VALID_FOR_CREDITCARD", "VALID_FOR_CREDITCARD")
                    .SetInProperty("CreditCard", PropertyType.Lookup)
                    .SetOutProperty("Persons", PropertyType.Collection);
            PERSON_HAS_CONTACTTYPE =
                Relations.New(Person, ContactType, "PERSON_HAS_CONTACTTYPE", "HAS_CONTACTTYPE")
                    .SetInProperty("ContactType", PropertyType.Lookup);
            PERSON_HAS_PHONENUMBERTYPE =
                Relations.New(Person, PhoneNumberType, "PERSON_HAS_PHONENUMBERTYPE", "HAS_PHONENUMBERTYPE")
                    .SetInProperty("PhoneNumberType", PropertyType.Lookup);
            PERSON_HAS_ADDRESS =
                Relations.New(Person, Address, "PERSON_HAS_ADDRESS", "HAS_ADDRESS")
                    .SetInProperty("Address", PropertyType.Lookup); ;

            PRODUCT_HAS_TRANSACTIONHISTORY =
                Relations.New(Product, TransactionHistory, "PRODUCT_HAS_TRANSACTIONHISTORY", "HAS_TRANSACTIONHISTORY")
                    .SetInProperty("TransactionHistory", PropertyType.Lookup);
            PRODUCT_VALID_FOR_PRODUCTREVIEW =
                Relations.New(Product, ProductReview, "PRODUCT_VALID_FOR_PRODUCTREVIEW", "VALID_FOR_PRODUCTREVIEW")
                    .SetInProperty("ProductReview", PropertyType.Lookup)
                    .SetOutProperty("Products", PropertyType.Collection);
            PRODUCT_HAS_PRODUCTPRODUCTPHOTO =
                Relations.New(Product, ProductProductPhoto, "PRODUCT_HAS_PRODUCTPRODUCTPHOTO", "HAS_PRODUCTPRODUCTPHOTO")
                    .SetInProperty("ProductProductPhoto", PropertyType.Lookup);
            PRODUCT_HAS_PRODUCTMODEL =
                Relations.New(Product, ProductModel, "PRODUCT_HAS_PRODUCTMODEL", "HAS_PRODUCTMODEL")
                    .SetInProperty("ProductModel", PropertyType.Lookup);
            PRODUCT_HAS_DOCUMENT =
                Relations.New(Product, Document, "PRODUCT_HAS_DOCUMENT", "HAS_DOCUMENT")
                    .SetInProperty("Document", PropertyType.Lookup);

            PRODUCTCOSTHISTORY_HAS_PRODUCT =
                Relations.New(ProductCostHistory, Product, "PRODUCTCOSTHISTORY_HAS_PRODUCT", "HAS_PRODUCT")
                    .SetInProperty("Product", PropertyType.Lookup);

            PRODUCTINVENTORY_HAS_LOCATION =
                    Relations.New(ProductInventory, Location, "PRODUCTINVENTORY_HAS_LOCATION", "HAS_LOCATION")
                        .SetInProperty("Location", PropertyType.Lookup);
            PRODUCTINVENTORY_HAS_PRODUCT =
                Relations.New(ProductInventory, Product, "PRODUCTINVENTORY_HAS_PRODUCT", "HAS_PRODUCT")
                    .SetInProperty("Product", PropertyType.Lookup);

            PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT =
                Relations.New(ProductListPriceHistory, Product, "PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT", "VALID_FOR_PRODUCT")
                    .SetInProperty("Product", PropertyType.Lookup)
                    .SetOutProperty("ProductListPriceHistories", PropertyType.Collection);

            PRODUCTMODEL_HAS_ILLUSTRATION =
                Relations.New(ProductModel, Illustration, "PRODUCTMODEL_HAS_ILLUSTRATION", "HAS_ILLUSTRATION")
                    .SetInProperty("Illustration", PropertyType.Lookup);
            PRODUCTMODEL_HAS_PRODUCTDESCRIPTION =
                Relations.New(ProductModel, ProductDescription, "PRODUCTMODEL_HAS_PRODUCTDESCRIPTION", "HAS_PRODUCTDESCRIPTION")
                    .SetInProperty("ProductDescription", PropertyType.Lookup);
            PRODUCTMODEL_HAS_CULTURE =
                Relations.New(ProductModel, Culture, "PRODUCTMODEL_HAS_CULTURE", "HAS_CULTURE")
                    .SetInProperty("Culture", PropertyType.Lookup);

            PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO =
                Relations.New(ProductProductPhoto, ProductPhoto, "PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO", "HAS_PRODUCTPHOTO")
                    .SetInProperty("ProductPhoto", PropertyType.Lookup);

            PRODUCTVENDOR_HAS_UNITMEASURE =
                Relations.New(ProductVendor, UnitMeasure, "PRODUCTVENDOR_HAS_UNITMEASURE", "HAS_UNITMEASURE")
                    .SetInProperty("UnitMeasure", PropertyType.Lookup);
            PRODUCTVENDOR_HAS_PRODUCT =
                Relations.New(ProductVendor, Product, "PRODUCTVENDOR_HAS_PRODUCT", "HAS_PRODUCT")
                    .SetInProperty("Product", PropertyType.Lookup);

            PURCHASEORDERDETAIL_HAS_PRODUCT =
                Relations.New(PurchaseOrderDetail, Product, "PURCHASEORDERDETAIL_HAS_PRODUCT", "HAS_PRODUCT")
                    .SetInProperty("Product", PropertyType.Lookup);
            PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER =
                Relations.New(PurchaseOrderDetail, PurchaseOrderHeader, "PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER", "HAS_PURCHASEORDERHEADER")
                    .SetInProperty("PurchaseOrderHeader", PropertyType.Lookup);

            PURCHASEORDERHEADER_HAS_VENDOR =
                Relations.New(PurchaseOrderHeader, Vendor, "PURCHASEORDERHEADER_HAS_VENDOR", "HAS_VENDOR")
                    .SetInProperty("Vendor", PropertyType.Lookup);
            PURCHASEORDERHEADER_HAS_SHIPMETHOD =
                Relations.New(PurchaseOrderHeader, ShipMethod, "PURCHASEORDERHEADER_HAS_SHIPMETHOD", "HAS_SHIPMETHOD")
                    .SetInProperty("ShipMethod", PropertyType.Lookup);

            SALESORDERDETAIL_HAS_SALESORDERHEADER =
                Relations.New(SalesOrderDetail, SalesOrderHeader, "SALESORDERDETAIL_HAS_SALESORDERHEADER", "HAS_SALESORDERHEADER")
                    .SetInProperty("SalesOrderHeader", PropertyType.Lookup);
            SALESORDERDETAIL_HAS_PRODUCT =
                Relations.New(SalesOrderDetail, Product, "SALESORDERDETAIL_HAS_PRODUCT", "HAS_PRODUCT")
                    .SetInProperty("Product", PropertyType.Lookup);
            SALESORDERDETAIL_HAS_SPECIALOFFER =
                Relations.New(SalesOrderDetail, SpecialOffer, "SALESORDERDETAIL_HAS_SPECIALOFFER", "HAS_SPECIALOFFER")
                    .SetInProperty("SpecialOffer", PropertyType.Lookup);

            SALESORDERHEADER_HAS_CURRENCYRATE =
                Relations.New(SalesOrderHeader, CurrencyRate, "SALESORDERHEADER_HAS_CURRENCYRATE", "HAS_CURRENCYRATE")
                    .SetInProperty("CurrencyRate", PropertyType.Lookup);
            SALESORDERHEADER_HAS_CREDITCARD =
                Relations.New(SalesOrderHeader, CreditCard, "SALESORDERHEADER_HAS_CREDITCARD", "HAS_CREDITCARD")
                    .SetInProperty("CreditCard", PropertyType.Lookup);
            SALESORDERHEADER_HAS_ADDRESS =
                Relations.New(SalesOrderHeader, Address, "SALESORDERHEADER_HAS_ADDRESS", "HAS_ADDRESS")
                    .SetInProperty("Address", PropertyType.Lookup);
            SALESORDERHEADER_HAS_SHIPMETHOD =
                Relations.New(SalesOrderHeader, ShipMethod, "SALESORDERHEADER_HAS_SHIPMETHOD", "HAS_SHIPMETHOD")
                    .SetInProperty("ShipMethod", PropertyType.Lookup);
            SALESORDERHEADER_CONTAINS_SALESTERRITORY =
                Relations.New(SalesOrderHeader, SalesTerritory, "SALESORDERHEADER_CONTAINS_SALESTERRITORY", "CONTAINS_SALESTERRITORY")
                    .SetInProperty("SalesTerritories", PropertyType.Collection);
            SALESORDERHEADER_HAS_SALESREASON =
                Relations.New(SalesOrderHeader, SalesReason, "SALESORDERHEADER_HAS_SALESREASON", "HAS_SALESREASON")
                    .SetInProperty("SalesReason", PropertyType.Lookup);

            SALESPERSON_HAS_SALESPERSONQUOTAHISTORY =
                Relations.New(SalesPerson, SalesPersonQuotaHistory, "SALESPERSON_HAS_SALESPERSONQUOTAHISTORY", "HAS_SALESPERSONQUOTAHISTORY")
                    .SetInProperty("SalesPersonQuotaHistory", PropertyType.Lookup);
            SALESPERSON_HAS_SALESTERRITORY =
                Relations.New(SalesPerson, SalesTerritory, "SALESPERSON_HAS_SALESTERRITORY", "HAS_SALESTERRITORY")
                    .SetInProperty("SalesTerritory", PropertyType.Lookup);
            SALESPERSON_IS_PERSON =
                Relations.New(SalesPerson, Person, "SALESPERSON_IS_PERSON", "IS_PERSON")
                    .SetInProperty("Person", PropertyType.Lookup)
                    .SetOutProperty("SalesPerson", PropertyType.Lookup);
            
            SALESTAXRATE_HAS_STATEPROVINCE =
                Relations.New(SalesTaxRate, StateProvince, "SALESTAXRATE_HAS_STATEPROVINCE", "HAS_STATEPROVINCE")
                    .SetInProperty("StateProvince", PropertyType.Lookup);

            SALESTERRITORY_HAS_SALESTERRITORYHISTORY =
                Relations.New(SalesTerritory, SalesTerritoryHistory, "SALESTERRITORY_HAS_SALESTERRITORYHISTORY", "HAS_SALESTERRITORYHISTORY")
                    .SetInProperty("SalesTerritoryHistory", PropertyType.Lookup);

            SHOPPINGCARTITEM_HAS_PRODUCT =
                Relations.New(ShoppingCartItem, Product, "SHOPPINGCARTITEM_HAS_PRODUCT", "HAS_PRODUCT")
                    .SetInProperty("Product", PropertyType.Lookup);

            STATEPROVINCE_HAS_COUNTRYREGION =
                Relations.New(StateProvince, CountryRegion, "STATEPROVINCE_HAS_COUNTRYREGION", "HAS_COUNTRYREGION")
                    .SetInProperty("CountryRegion", PropertyType.Collection);
            STATEPROVINCE_HAS_SALESTERRITORY =
                Relations.New(StateProvince, SalesTerritory, "STATEPROVINCE_HAS_SALESTERRITORY", "HAS_SALESTERRITORY")
                    .SetInProperty("SalesTerritory", PropertyType.Lookup);

            STORE_VALID_FOR_SALESPERSON =
                Relations.New(Store, SalesPerson, "STORE_VALID_FOR_SALESPERSON", "VALID_FOR_SALESPERSON")
                    .SetInProperty("SalesPerson", PropertyType.Lookup)
                    .SetOutProperty("Stores", PropertyType.Collection);
            STORE_HAS_ADDRESS =
                Relations.New(Store, Address, "STORE_HAS_ADDRESS", "HAS_ADDRESS")
                    .SetInProperty("Address", PropertyType.Lookup);

            TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT =
                Relations.New(TransactionHistoryArchive, Product, "TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT", "HAS_PRODUCT")
                    .SetInProperty("Product", PropertyType.Lookup);
            
            VENDOR_VALID_FOR_EMPLOYEE =
                Relations.New(Vendor, Employee, "VENDOR_VALID_FOR_EMPLOYEE", "VALID_FOR_EMPLOYEE")
                    .SetInProperty("Employee", PropertyType.Lookup)
                    .SetOutProperty("Vendors", PropertyType.Collection);
            VENDOR_BECOMES_PRODUCTVENDOR =
                Relations.New(Vendor, ProductVendor, "VENDOR_BECOMES_PRODUCTVENDOR", "BECOMES_PRODUCTVENDOR")
                    .SetInProperty("ProductVendor", PropertyType.Lookup);

            WORKORDER_HAS_SCRAPREASON =
                Relations.New(WorkOrder, ScrapReason, "WORKORDER_HAS_SCRAPREASON", "HAS_SCRAPREASON")
                    .SetInProperty("ScrapReason", PropertyType.Lookup);
            WORKORDER_HAS_PRODUCT =
                Relations.New(WorkOrder, Product, "WORKORDER_HAS_PRODUCT", "HAS_PRODUCT")
                    .SetInProperty("Product", PropertyType.Lookup);

            WORKORDERROUTING_HAS_WORKORDER =
                Relations.New(WorkOrderRouting, WorkOrder, "WORKORDERROUTING_HAS_WORKORDER", "HAS_WORKORDER")
                    .SetInProperty("WorkOrder", PropertyType.Lookup);
            WORKORDERROUTING_HAS_PRODUCT =
                Relations.New(WorkOrderRouting, Product, "WORKORDERROUTING_HAS_PRODUCT", "HAS_PRODUCT")
                    .SetInProperty("Product", PropertyType.Lookup);
            WORKORDERROUTING_HAS_LOCATION =
                Relations.New(WorkOrderRouting, Location, "WORKORDERROUTING_HAS_LOCATION", "HAS_LOCATION")
                    .SetInProperty("Location", PropertyType.Lookup);
        }

        #endregion

        #region Testing
        public Entity ProductSubcategory { get; private set; }
        public Relationship PRODUCTSUBCATEGORY_IN_PRODUCTCATEGORY { get; private set; }
        public Relationship PRODUCT_IN_PRODUCTSUBCATEGORY { get; private set; }
        private void TestWithProductSubcategory()
        {

            ProductSubcategory =
                Entities.New("ProductSubcategory", SchemaBase)
                .AddProperty("Name", typeof(string), false)
                .AddProperty("rowguid", typeof(string), false);

            PRODUCTSUBCATEGORY_IN_PRODUCTCATEGORY =
                Relations.New(ProductSubcategory, ProductCategory, "PRODUCTSUBCATEGORY_IN_PRODUCTCATEGORY", "IN_PRODUCTCATEGORY")
                    .SetInProperty("ProductCategory", PropertyType.Lookup)
                    .SetOutProperty("ProductSubcategories", PropertyType.Collection);

            PRODUCT_IN_PRODUCTSUBCATEGORY =
                 Relations.New(Product, ProductSubcategory, "PRODUCT_IN_PRODUCTSUBCATEGORY", "IN_PRODUCTSUBCATEGORY")
                    .SetInProperty("ProductSubcategory", PropertyType.Lookup)
                    .SetOutProperty("Product", PropertyType.Collection);
        }
        #endregion
    }
}
