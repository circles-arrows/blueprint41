using Blueprint41.Core;

namespace Domain.Data.Manipulation
{
  internal class Register
    {
        private static bool isInitialized = false;

        public static void Types()
        {
            if (isInitialized)
                return;

            lock (typeof(Register))
            {
                if (isInitialized)
                    return;

				isInitialized = true;

                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["Address"]).SetRuntimeTypes(typeof(Address), typeof(Address));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["AddressType"]).SetRuntimeTypes(typeof(AddressType), typeof(AddressType));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["BillOfMaterials"]).SetRuntimeTypes(typeof(BillOfMaterials), typeof(BillOfMaterials));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["ContactType"]).SetRuntimeTypes(typeof(ContactType), typeof(ContactType));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["CountryRegion"]).SetRuntimeTypes(typeof(CountryRegion), typeof(CountryRegion));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["CreditCard"]).SetRuntimeTypes(typeof(CreditCard), typeof(CreditCard));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["Culture"]).SetRuntimeTypes(typeof(Culture), typeof(Culture));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["Currency"]).SetRuntimeTypes(typeof(Currency), typeof(Currency));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["CurrencyRate"]).SetRuntimeTypes(typeof(CurrencyRate), typeof(CurrencyRate));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["Customer"]).SetRuntimeTypes(typeof(Customer), typeof(Customer));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["Department"]).SetRuntimeTypes(typeof(Department), typeof(Department));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["Document"]).SetRuntimeTypes(typeof(Document), typeof(Document));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["EmailAddress"]).SetRuntimeTypes(typeof(EmailAddress), typeof(EmailAddress));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["Employee"]).SetRuntimeTypes(typeof(Employee), typeof(Employee));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"]).SetRuntimeTypes(typeof(EmployeeDepartmentHistory), typeof(EmployeeDepartmentHistory));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"]).SetRuntimeTypes(typeof(EmployeePayHistory), typeof(EmployeePayHistory));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["Illustration"]).SetRuntimeTypes(typeof(Illustration), typeof(Illustration));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["JobCandidate"]).SetRuntimeTypes(typeof(JobCandidate), typeof(JobCandidate));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["Location"]).SetRuntimeTypes(typeof(Location), typeof(Location));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["Neo4jBase"]).SetRuntimeTypes(typeof(INeo4jBase), typeof(Neo4jBase));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["Password"]).SetRuntimeTypes(typeof(Password), typeof(Password));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["Person"]).SetRuntimeTypes(typeof(Person), typeof(Person));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["PhoneNumberType"]).SetRuntimeTypes(typeof(PhoneNumberType), typeof(PhoneNumberType));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["Product"]).SetRuntimeTypes(typeof(Product), typeof(Product));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["ProductCategory"]).SetRuntimeTypes(typeof(ProductCategory), typeof(ProductCategory));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["ProductCostHistory"]).SetRuntimeTypes(typeof(ProductCostHistory), typeof(ProductCostHistory));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["ProductDescription"]).SetRuntimeTypes(typeof(ProductDescription), typeof(ProductDescription));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["ProductInventory"]).SetRuntimeTypes(typeof(ProductInventory), typeof(ProductInventory));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"]).SetRuntimeTypes(typeof(ProductListPriceHistory), typeof(ProductListPriceHistory));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["ProductModel"]).SetRuntimeTypes(typeof(ProductModel), typeof(ProductModel));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["ProductPhoto"]).SetRuntimeTypes(typeof(ProductPhoto), typeof(ProductPhoto));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["ProductProductPhoto"]).SetRuntimeTypes(typeof(ProductProductPhoto), typeof(ProductProductPhoto));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["ProductReview"]).SetRuntimeTypes(typeof(ProductReview), typeof(ProductReview));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["ProductSubcategory"]).SetRuntimeTypes(typeof(ProductSubcategory), typeof(ProductSubcategory));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["ProductVendor"]).SetRuntimeTypes(typeof(ProductVendor), typeof(ProductVendor));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"]).SetRuntimeTypes(typeof(PurchaseOrderDetail), typeof(PurchaseOrderDetail));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"]).SetRuntimeTypes(typeof(PurchaseOrderHeader), typeof(PurchaseOrderHeader));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"]).SetRuntimeTypes(typeof(SalesOrderDetail), typeof(SalesOrderDetail));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"]).SetRuntimeTypes(typeof(SalesOrderHeader), typeof(SalesOrderHeader));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["SalesPerson"]).SetRuntimeTypes(typeof(SalesPerson), typeof(SalesPerson));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"]).SetRuntimeTypes(typeof(SalesPersonQuotaHistory), typeof(SalesPersonQuotaHistory));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["SalesReason"]).SetRuntimeTypes(typeof(SalesReason), typeof(SalesReason));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["SalesTaxRate"]).SetRuntimeTypes(typeof(SalesTaxRate), typeof(SalesTaxRate));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["SalesTerritory"]).SetRuntimeTypes(typeof(SalesTerritory), typeof(SalesTerritory));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"]).SetRuntimeTypes(typeof(SalesTerritoryHistory), typeof(SalesTerritoryHistory));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["SchemaBase"]).SetRuntimeTypes(typeof(ISchemaBase), typeof(SchemaBase));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["ScrapReason"]).SetRuntimeTypes(typeof(ScrapReason), typeof(ScrapReason));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["Shift"]).SetRuntimeTypes(typeof(Shift), typeof(Shift));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["ShipMethod"]).SetRuntimeTypes(typeof(ShipMethod), typeof(ShipMethod));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"]).SetRuntimeTypes(typeof(ShoppingCartItem), typeof(ShoppingCartItem));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["SpecialOffer"]).SetRuntimeTypes(typeof(SpecialOffer), typeof(SpecialOffer));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["StateProvince"]).SetRuntimeTypes(typeof(StateProvince), typeof(StateProvince));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["Store"]).SetRuntimeTypes(typeof(Store), typeof(Store));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["TransactionHistory"]).SetRuntimeTypes(typeof(TransactionHistory), typeof(TransactionHistory));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"]).SetRuntimeTypes(typeof(TransactionHistoryArchive), typeof(TransactionHistoryArchive));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["UnitMeasure"]).SetRuntimeTypes(typeof(UnitMeasure), typeof(UnitMeasure));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["Vendor"]).SetRuntimeTypes(typeof(Vendor), typeof(Vendor));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["WorkOrder"]).SetRuntimeTypes(typeof(WorkOrder), typeof(WorkOrder));
                ((ISetRuntimeType)Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"]).SetRuntimeTypes(typeof(WorkOrderRouting), typeof(WorkOrderRouting));
            }
        }
    }
}
