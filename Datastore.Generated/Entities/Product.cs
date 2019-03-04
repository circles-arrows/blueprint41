using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Domain.Data.Query;

namespace Domain.Data.Manipulation
{
	public interface IProductOriginalData : ISchemaBaseOriginalData
    {
		string Name { get; }
		string ProductNumber { get; }
		bool MakeFlag { get; }
		bool FinishedGoodsFlag { get; }
		string Color { get; }
		int SafetyStockLevel { get; }
		int ReorderPoint { get; }
		double StandardCost { get; }
		double ListPrice { get; }
		string Size { get; }
		string SizeUnitMeasureCode { get; }
		string WeightUnitMeasureCode { get; }
		decimal? Weight { get; }
		int DaysToManufacture { get; }
		string ProductLine { get; }
		string Class { get; }
		string Style { get; }
		System.DateTime SellStartDate { get; }
		System.DateTime? SellEndDate { get; }
		System.DateTime? DiscontinuedDate { get; }
		string rowguid { get; }
		TransactionHistory TransactionHistory { get; }
		ProductReview ProductReview { get; }
		ProductProductPhoto ProductProductPhoto { get; }
		ProductModel ProductModel { get; }
		Document Document { get; }
		IEnumerable<ProductListPriceHistory> ProductListPriceHistories { get; }
    }

	public partial class Product : OGM<Product, Product.ProductData, System.String>, ISchemaBase, INeo4jBase, IProductOriginalData
	{
        #region Initialize

        static Product()
        {
            Register.Types();
        }

        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadByKeys
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

			AdditionalGeneratedStoredQueries();
        }
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, Product> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.ProductAlias, IWhereQuery> query)
        {
            q.ProductAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Product.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"Product => Name : {this.Name}, ProductNumber : {this.ProductNumber}, MakeFlag : {this.MakeFlag}, FinishedGoodsFlag : {this.FinishedGoodsFlag}, Color : {this.Color?.ToString() ?? "null"}, SafetyStockLevel : {this.SafetyStockLevel}, ReorderPoint : {this.ReorderPoint}, StandardCost : {this.StandardCost}, ListPrice : {this.ListPrice}, Size : {this.Size?.ToString() ?? "null"}, SizeUnitMeasureCode : {this.SizeUnitMeasureCode?.ToString() ?? "null"}, WeightUnitMeasureCode : {this.WeightUnitMeasureCode?.ToString() ?? "null"}, Weight : {this.Weight?.ToString() ?? "null"}, DaysToManufacture : {this.DaysToManufacture}, ProductLine : {this.ProductLine?.ToString() ?? "null"}, Class : {this.Class?.ToString() ?? "null"}, Style : {this.Style?.ToString() ?? "null"}, SellStartDate : {this.SellStartDate}, SellEndDate : {this.SellEndDate?.ToString() ?? "null"}, DiscontinuedDate : {this.DiscontinuedDate?.ToString() ?? "null"}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

		protected override void LazySet()
        {
            base.LazySet();
            if (PersistenceState == PersistenceState.NewAndChanged || PersistenceState == PersistenceState.LoadedAndChanged)
            {
                if ((object)InnerData == (object)OriginalData)
                    OriginalData = new ProductData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.Name == null)
				throw new PersistenceException(string.Format("Cannot save Product with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ProductNumber == null)
				throw new PersistenceException(string.Format("Cannot save Product with key '{0}' because the ProductNumber cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.MakeFlag == null)
				throw new PersistenceException(string.Format("Cannot save Product with key '{0}' because the MakeFlag cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.FinishedGoodsFlag == null)
				throw new PersistenceException(string.Format("Cannot save Product with key '{0}' because the FinishedGoodsFlag cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.SafetyStockLevel == null)
				throw new PersistenceException(string.Format("Cannot save Product with key '{0}' because the SafetyStockLevel cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ReorderPoint == null)
				throw new PersistenceException(string.Format("Cannot save Product with key '{0}' because the ReorderPoint cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.StandardCost == null)
				throw new PersistenceException(string.Format("Cannot save Product with key '{0}' because the StandardCost cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ListPrice == null)
				throw new PersistenceException(string.Format("Cannot save Product with key '{0}' because the ListPrice cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.DaysToManufacture == null)
				throw new PersistenceException(string.Format("Cannot save Product with key '{0}' because the DaysToManufacture cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.SellStartDate == null)
				throw new PersistenceException(string.Format("Cannot save Product with key '{0}' because the SellStartDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid == null)
				throw new PersistenceException(string.Format("Cannot save Product with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save Product with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class ProductData : Data<System.String>
		{
			public ProductData()
            {

            }

            public ProductData(ProductData data)
            {
				Name = data.Name;
				ProductNumber = data.ProductNumber;
				MakeFlag = data.MakeFlag;
				FinishedGoodsFlag = data.FinishedGoodsFlag;
				Color = data.Color;
				SafetyStockLevel = data.SafetyStockLevel;
				ReorderPoint = data.ReorderPoint;
				StandardCost = data.StandardCost;
				ListPrice = data.ListPrice;
				Size = data.Size;
				SizeUnitMeasureCode = data.SizeUnitMeasureCode;
				WeightUnitMeasureCode = data.WeightUnitMeasureCode;
				Weight = data.Weight;
				DaysToManufacture = data.DaysToManufacture;
				ProductLine = data.ProductLine;
				Class = data.Class;
				Style = data.Style;
				SellStartDate = data.SellStartDate;
				SellEndDate = data.SellEndDate;
				DiscontinuedDate = data.DiscontinuedDate;
				rowguid = data.rowguid;
				TransactionHistory = data.TransactionHistory;
				ProductReview = data.ProductReview;
				ProductProductPhoto = data.ProductProductPhoto;
				ProductModel = data.ProductModel;
				Document = data.Document;
				ProductListPriceHistories = data.ProductListPriceHistories;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Product";

				TransactionHistory = new EntityCollection<TransactionHistory>(Wrapper, Members.TransactionHistory);
				ProductReview = new EntityCollection<ProductReview>(Wrapper, Members.ProductReview, item => { if (Members.ProductReview.Events.HasRegisteredChangeHandlers) { int loadHack = item.Products.Count; } });
				ProductProductPhoto = new EntityCollection<ProductProductPhoto>(Wrapper, Members.ProductProductPhoto);
				ProductModel = new EntityCollection<ProductModel>(Wrapper, Members.ProductModel);
				Document = new EntityCollection<Document>(Wrapper, Members.Document);
				ProductListPriceHistories = new EntityCollection<ProductListPriceHistory>(Wrapper, Members.ProductListPriceHistories, item => { if (Members.ProductListPriceHistories.Events.HasRegisteredChangeHandlers) { object loadHack = item.Product; } });
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("Name",  Name);
				dictionary.Add("ProductNumber",  ProductNumber);
				dictionary.Add("MakeFlag",  MakeFlag);
				dictionary.Add("FinishedGoodsFlag",  FinishedGoodsFlag);
				dictionary.Add("Color",  Color);
				dictionary.Add("SafetyStockLevel",  Conversion<int, long>.Convert(SafetyStockLevel));
				dictionary.Add("ReorderPoint",  Conversion<int, long>.Convert(ReorderPoint));
				dictionary.Add("StandardCost",  StandardCost);
				dictionary.Add("ListPrice",  ListPrice);
				dictionary.Add("Size",  Size);
				dictionary.Add("SizeUnitMeasureCode",  SizeUnitMeasureCode);
				dictionary.Add("WeightUnitMeasureCode",  WeightUnitMeasureCode);
				dictionary.Add("Weight",  Conversion<decimal?, long?>.Convert(Weight));
				dictionary.Add("DaysToManufacture",  Conversion<int, long>.Convert(DaysToManufacture));
				dictionary.Add("ProductLine",  ProductLine);
				dictionary.Add("Class",  Class);
				dictionary.Add("Style",  Style);
				dictionary.Add("SellStartDate",  Conversion<System.DateTime, long>.Convert(SellStartDate));
				dictionary.Add("SellEndDate",  Conversion<System.DateTime?, long?>.Convert(SellEndDate));
				dictionary.Add("DiscontinuedDate",  Conversion<System.DateTime?, long?>.Convert(DiscontinuedDate));
				dictionary.Add("rowguid",  rowguid);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("ProductNumber", out value))
					ProductNumber = (string)value;
				if (properties.TryGetValue("MakeFlag", out value))
					MakeFlag = (bool)value;
				if (properties.TryGetValue("FinishedGoodsFlag", out value))
					FinishedGoodsFlag = (bool)value;
				if (properties.TryGetValue("Color", out value))
					Color = (string)value;
				if (properties.TryGetValue("SafetyStockLevel", out value))
					SafetyStockLevel = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("ReorderPoint", out value))
					ReorderPoint = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("StandardCost", out value))
					StandardCost = (double)value;
				if (properties.TryGetValue("ListPrice", out value))
					ListPrice = (double)value;
				if (properties.TryGetValue("Size", out value))
					Size = (string)value;
				if (properties.TryGetValue("SizeUnitMeasureCode", out value))
					SizeUnitMeasureCode = (string)value;
				if (properties.TryGetValue("WeightUnitMeasureCode", out value))
					WeightUnitMeasureCode = (string)value;
				if (properties.TryGetValue("Weight", out value))
					Weight = Conversion<long, decimal>.Convert((long)value);
				if (properties.TryGetValue("DaysToManufacture", out value))
					DaysToManufacture = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("ProductLine", out value))
					ProductLine = (string)value;
				if (properties.TryGetValue("Class", out value))
					Class = (string)value;
				if (properties.TryGetValue("Style", out value))
					Style = (string)value;
				if (properties.TryGetValue("SellStartDate", out value))
					SellStartDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("SellEndDate", out value))
					SellEndDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("DiscontinuedDate", out value))
					DiscontinuedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IProduct

			public string Name { get; set; }
			public string ProductNumber { get; set; }
			public bool MakeFlag { get; set; }
			public bool FinishedGoodsFlag { get; set; }
			public string Color { get; set; }
			public int SafetyStockLevel { get; set; }
			public int ReorderPoint { get; set; }
			public double StandardCost { get; set; }
			public double ListPrice { get; set; }
			public string Size { get; set; }
			public string SizeUnitMeasureCode { get; set; }
			public string WeightUnitMeasureCode { get; set; }
			public decimal? Weight { get; set; }
			public int DaysToManufacture { get; set; }
			public string ProductLine { get; set; }
			public string Class { get; set; }
			public string Style { get; set; }
			public System.DateTime SellStartDate { get; set; }
			public System.DateTime? SellEndDate { get; set; }
			public System.DateTime? DiscontinuedDate { get; set; }
			public string rowguid { get; set; }
			public EntityCollection<TransactionHistory> TransactionHistory { get; private set; }
			public EntityCollection<ProductReview> ProductReview { get; private set; }
			public EntityCollection<ProductProductPhoto> ProductProductPhoto { get; private set; }
			public EntityCollection<ProductModel> ProductModel { get; private set; }
			public EntityCollection<Document> Document { get; private set; }
			public EntityCollection<ProductListPriceHistory> ProductListPriceHistories { get; private set; }

			#endregion
			#region Members for interface ISchemaBase

			public System.DateTime ModifiedDate { get; set; }

			#endregion
			#region Members for interface INeo4jBase

			public string Uid { get; set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IProduct

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
		public string ProductNumber { get { LazyGet(); return InnerData.ProductNumber; } set { if (LazySet(Members.ProductNumber, InnerData.ProductNumber, value)) InnerData.ProductNumber = value; } }
		public bool MakeFlag { get { LazyGet(); return InnerData.MakeFlag; } set { if (LazySet(Members.MakeFlag, InnerData.MakeFlag, value)) InnerData.MakeFlag = value; } }
		public bool FinishedGoodsFlag { get { LazyGet(); return InnerData.FinishedGoodsFlag; } set { if (LazySet(Members.FinishedGoodsFlag, InnerData.FinishedGoodsFlag, value)) InnerData.FinishedGoodsFlag = value; } }
		public string Color { get { LazyGet(); return InnerData.Color; } set { if (LazySet(Members.Color, InnerData.Color, value)) InnerData.Color = value; } }
		public int SafetyStockLevel { get { LazyGet(); return InnerData.SafetyStockLevel; } set { if (LazySet(Members.SafetyStockLevel, InnerData.SafetyStockLevel, value)) InnerData.SafetyStockLevel = value; } }
		public int ReorderPoint { get { LazyGet(); return InnerData.ReorderPoint; } set { if (LazySet(Members.ReorderPoint, InnerData.ReorderPoint, value)) InnerData.ReorderPoint = value; } }
		public double StandardCost { get { LazyGet(); return InnerData.StandardCost; } set { if (LazySet(Members.StandardCost, InnerData.StandardCost, value)) InnerData.StandardCost = value; } }
		public double ListPrice { get { LazyGet(); return InnerData.ListPrice; } set { if (LazySet(Members.ListPrice, InnerData.ListPrice, value)) InnerData.ListPrice = value; } }
		public string Size { get { LazyGet(); return InnerData.Size; } set { if (LazySet(Members.Size, InnerData.Size, value)) InnerData.Size = value; } }
		public string SizeUnitMeasureCode { get { LazyGet(); return InnerData.SizeUnitMeasureCode; } set { if (LazySet(Members.SizeUnitMeasureCode, InnerData.SizeUnitMeasureCode, value)) InnerData.SizeUnitMeasureCode = value; } }
		public string WeightUnitMeasureCode { get { LazyGet(); return InnerData.WeightUnitMeasureCode; } set { if (LazySet(Members.WeightUnitMeasureCode, InnerData.WeightUnitMeasureCode, value)) InnerData.WeightUnitMeasureCode = value; } }
		public decimal? Weight { get { LazyGet(); return InnerData.Weight; } set { if (LazySet(Members.Weight, InnerData.Weight, value)) InnerData.Weight = value; } }
		public int DaysToManufacture { get { LazyGet(); return InnerData.DaysToManufacture; } set { if (LazySet(Members.DaysToManufacture, InnerData.DaysToManufacture, value)) InnerData.DaysToManufacture = value; } }
		public string ProductLine { get { LazyGet(); return InnerData.ProductLine; } set { if (LazySet(Members.ProductLine, InnerData.ProductLine, value)) InnerData.ProductLine = value; } }
		public string Class { get { LazyGet(); return InnerData.Class; } set { if (LazySet(Members.Class, InnerData.Class, value)) InnerData.Class = value; } }
		public string Style { get { LazyGet(); return InnerData.Style; } set { if (LazySet(Members.Style, InnerData.Style, value)) InnerData.Style = value; } }
		public System.DateTime SellStartDate { get { LazyGet(); return InnerData.SellStartDate; } set { if (LazySet(Members.SellStartDate, InnerData.SellStartDate, value)) InnerData.SellStartDate = value; } }
		public System.DateTime? SellEndDate { get { LazyGet(); return InnerData.SellEndDate; } set { if (LazySet(Members.SellEndDate, InnerData.SellEndDate, value)) InnerData.SellEndDate = value; } }
		public System.DateTime? DiscontinuedDate { get { LazyGet(); return InnerData.DiscontinuedDate; } set { if (LazySet(Members.DiscontinuedDate, InnerData.DiscontinuedDate, value)) InnerData.DiscontinuedDate = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }
		public TransactionHistory TransactionHistory
		{
			get { return ((ILookupHelper<TransactionHistory>)InnerData.TransactionHistory).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.TransactionHistory, ((ILookupHelper<TransactionHistory>)InnerData.TransactionHistory).GetItem(null), value))
					((ILookupHelper<TransactionHistory>)InnerData.TransactionHistory).SetItem(value, null); 
			}
		}
		public ProductReview ProductReview
		{
			get { return ((ILookupHelper<ProductReview>)InnerData.ProductReview).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.ProductReview, ((ILookupHelper<ProductReview>)InnerData.ProductReview).GetItem(null), value))
					((ILookupHelper<ProductReview>)InnerData.ProductReview).SetItem(value, null); 
			}
		}
		private void ClearProductReview(DateTime? moment)
		{
			((ILookupHelper<ProductReview>)InnerData.ProductReview).ClearLookup(moment);
		}
		public ProductProductPhoto ProductProductPhoto
		{
			get { return ((ILookupHelper<ProductProductPhoto>)InnerData.ProductProductPhoto).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.ProductProductPhoto, ((ILookupHelper<ProductProductPhoto>)InnerData.ProductProductPhoto).GetItem(null), value))
					((ILookupHelper<ProductProductPhoto>)InnerData.ProductProductPhoto).SetItem(value, null); 
			}
		}
		public ProductModel ProductModel
		{
			get { return ((ILookupHelper<ProductModel>)InnerData.ProductModel).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.ProductModel, ((ILookupHelper<ProductModel>)InnerData.ProductModel).GetItem(null), value))
					((ILookupHelper<ProductModel>)InnerData.ProductModel).SetItem(value, null); 
			}
		}
		public Document Document
		{
			get { return ((ILookupHelper<Document>)InnerData.Document).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Document, ((ILookupHelper<Document>)InnerData.Document).GetItem(null), value))
					((ILookupHelper<Document>)InnerData.Document).SetItem(value, null); 
			}
		}
		public EntityCollection<ProductListPriceHistory> ProductListPriceHistories { get { return InnerData.ProductListPriceHistories; } }
		private void ClearProductListPriceHistories(DateTime? moment)
		{
			((ILookupHelper<ProductListPriceHistory>)InnerData.ProductListPriceHistories).ClearLookup(moment);
		}

		#endregion
		#region Members for interface ISchemaBase

		public System.DateTime ModifiedDate { get { LazyGet(); return InnerData.ModifiedDate; } set { if (LazySet(Members.ModifiedDate, InnerData.ModifiedDate, value)) InnerData.ModifiedDate = value; } }

		#endregion
		#region Members for interface INeo4jBase

		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }

		#endregion

		#region Virtual Node Type
		
		public string NodeType  { get { return InnerData.NodeType; } }
		
		#endregion

		#endregion

		#region Reflection

        private static ProductMembers members = null;
        public static ProductMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(Product))
                    {
                        if (members == null)
                            members = new ProductMembers();
                    }
                }
                return members;
            }
        }
        public class ProductMembers
        {
            internal ProductMembers() { }

			#region Members for interface IProduct

            public Property Name { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["Name"];
            public Property ProductNumber { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["ProductNumber"];
            public Property MakeFlag { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["MakeFlag"];
            public Property FinishedGoodsFlag { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["FinishedGoodsFlag"];
            public Property Color { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["Color"];
            public Property SafetyStockLevel { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["SafetyStockLevel"];
            public Property ReorderPoint { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["ReorderPoint"];
            public Property StandardCost { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["StandardCost"];
            public Property ListPrice { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["ListPrice"];
            public Property Size { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["Size"];
            public Property SizeUnitMeasureCode { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["SizeUnitMeasureCode"];
            public Property WeightUnitMeasureCode { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["WeightUnitMeasureCode"];
            public Property Weight { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["Weight"];
            public Property DaysToManufacture { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["DaysToManufacture"];
            public Property ProductLine { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["ProductLine"];
            public Property Class { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["Class"];
            public Property Style { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["Style"];
            public Property SellStartDate { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["SellStartDate"];
            public Property SellEndDate { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["SellEndDate"];
            public Property DiscontinuedDate { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["DiscontinuedDate"];
            public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["rowguid"];
            public Property TransactionHistory { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["TransactionHistory"];
            public Property ProductReview { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["ProductReview"];
            public Property ProductProductPhoto { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["ProductProductPhoto"];
            public Property ProductModel { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["ProductModel"];
            public Property Document { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["Document"];
            public Property ProductListPriceHistories { get; } = Datastore.AdventureWorks.Model.Entities["Product"].Properties["ProductListPriceHistories"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static ProductFullTextMembers fullTextMembers = null;
        public static ProductFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(Product))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new ProductFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class ProductFullTextMembers
        {
            internal ProductFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(Product))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["Product"];
                }
            }
            return entity;
        }

		private static ProductEvents events = null;
        public static ProductEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(Product))
                    {
                        if (events == null)
                            events = new ProductEvents();
                    }
                }
                return events;
            }
        }
        public class ProductEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Product, EntityEventArgs> onNew;
            public event EventHandler<Product, EntityEventArgs> OnNew
            {
                add
                {
                    lock (this)
                    {
                        if (!onNewIsRegistered)
                        {
                            Entity.Events.OnNew -= onNewProxy;
                            Entity.Events.OnNew += onNewProxy;
                            onNewIsRegistered = true;
                        }
                        onNew += value;
                    }
                }
                remove
                {
                    lock (this)
                    {
                        onNew -= value;
                        if (onNew == null && onNewIsRegistered)
                        {
                            Entity.Events.OnNew -= onNewProxy;
                            onNewIsRegistered = false;
                        }
                    }
                }
            }
            
			private void onNewProxy(object sender, EntityEventArgs args)
            {
                EventHandler<Product, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((Product)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<Product, EntityEventArgs> onDelete;
            public event EventHandler<Product, EntityEventArgs> OnDelete
            {
                add
                {
                    lock (this)
                    {
                        if (!onDeleteIsRegistered)
                        {
                            Entity.Events.OnDelete -= onDeleteProxy;
                            Entity.Events.OnDelete += onDeleteProxy;
                            onDeleteIsRegistered = true;
                        }
                        onDelete += value;
                    }
                }
                remove
                {
                    lock (this)
                    {
                        onDelete -= value;
                        if (onDelete == null && onDeleteIsRegistered)
                        {
                            Entity.Events.OnDelete -= onDeleteProxy;
                            onDeleteIsRegistered = false;
                        }
                    }
                }
            }
            
			private void onDeleteProxy(object sender, EntityEventArgs args)
            {
                EventHandler<Product, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((Product)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<Product, EntityEventArgs> onSave;
            public event EventHandler<Product, EntityEventArgs> OnSave
            {
                add
                {
                    lock (this)
                    {
                        if (!onSaveIsRegistered)
                        {
                            Entity.Events.OnSave -= onSaveProxy;
                            Entity.Events.OnSave += onSaveProxy;
                            onSaveIsRegistered = true;
                        }
                        onSave += value;
                    }
                }
                remove
                {
                    lock (this)
                    {
                        onSave -= value;
                        if (onSave == null && onSaveIsRegistered)
                        {
                            Entity.Events.OnSave -= onSaveProxy;
                            onSaveIsRegistered = false;
                        }
                    }
                }
            }
            
			private void onSaveProxy(object sender, EntityEventArgs args)
            {
                EventHandler<Product, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((Product)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onName;
				public static event EventHandler<Product, PropertyEventArgs> OnName
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onNameIsRegistered)
							{
								Members.Name.Events.OnChange -= onNameProxy;
								Members.Name.Events.OnChange += onNameProxy;
								onNameIsRegistered = true;
							}
							onName += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onName -= value;
							if (onName == null && onNameIsRegistered)
							{
								Members.Name.Events.OnChange -= onNameProxy;
								onNameIsRegistered = false;
							}
						}
					}
				}
            
				private static void onNameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onName;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnProductNumber

				private static bool onProductNumberIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onProductNumber;
				public static event EventHandler<Product, PropertyEventArgs> OnProductNumber
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onProductNumberIsRegistered)
							{
								Members.ProductNumber.Events.OnChange -= onProductNumberProxy;
								Members.ProductNumber.Events.OnChange += onProductNumberProxy;
								onProductNumberIsRegistered = true;
							}
							onProductNumber += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onProductNumber -= value;
							if (onProductNumber == null && onProductNumberIsRegistered)
							{
								Members.ProductNumber.Events.OnChange -= onProductNumberProxy;
								onProductNumberIsRegistered = false;
							}
						}
					}
				}
            
				private static void onProductNumberProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onProductNumber;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnMakeFlag

				private static bool onMakeFlagIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onMakeFlag;
				public static event EventHandler<Product, PropertyEventArgs> OnMakeFlag
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onMakeFlagIsRegistered)
							{
								Members.MakeFlag.Events.OnChange -= onMakeFlagProxy;
								Members.MakeFlag.Events.OnChange += onMakeFlagProxy;
								onMakeFlagIsRegistered = true;
							}
							onMakeFlag += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onMakeFlag -= value;
							if (onMakeFlag == null && onMakeFlagIsRegistered)
							{
								Members.MakeFlag.Events.OnChange -= onMakeFlagProxy;
								onMakeFlagIsRegistered = false;
							}
						}
					}
				}
            
				private static void onMakeFlagProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onMakeFlag;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnFinishedGoodsFlag

				private static bool onFinishedGoodsFlagIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onFinishedGoodsFlag;
				public static event EventHandler<Product, PropertyEventArgs> OnFinishedGoodsFlag
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onFinishedGoodsFlagIsRegistered)
							{
								Members.FinishedGoodsFlag.Events.OnChange -= onFinishedGoodsFlagProxy;
								Members.FinishedGoodsFlag.Events.OnChange += onFinishedGoodsFlagProxy;
								onFinishedGoodsFlagIsRegistered = true;
							}
							onFinishedGoodsFlag += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onFinishedGoodsFlag -= value;
							if (onFinishedGoodsFlag == null && onFinishedGoodsFlagIsRegistered)
							{
								Members.FinishedGoodsFlag.Events.OnChange -= onFinishedGoodsFlagProxy;
								onFinishedGoodsFlagIsRegistered = false;
							}
						}
					}
				}
            
				private static void onFinishedGoodsFlagProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onFinishedGoodsFlag;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnColor

				private static bool onColorIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onColor;
				public static event EventHandler<Product, PropertyEventArgs> OnColor
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onColorIsRegistered)
							{
								Members.Color.Events.OnChange -= onColorProxy;
								Members.Color.Events.OnChange += onColorProxy;
								onColorIsRegistered = true;
							}
							onColor += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onColor -= value;
							if (onColor == null && onColorIsRegistered)
							{
								Members.Color.Events.OnChange -= onColorProxy;
								onColorIsRegistered = false;
							}
						}
					}
				}
            
				private static void onColorProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onColor;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnSafetyStockLevel

				private static bool onSafetyStockLevelIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onSafetyStockLevel;
				public static event EventHandler<Product, PropertyEventArgs> OnSafetyStockLevel
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSafetyStockLevelIsRegistered)
							{
								Members.SafetyStockLevel.Events.OnChange -= onSafetyStockLevelProxy;
								Members.SafetyStockLevel.Events.OnChange += onSafetyStockLevelProxy;
								onSafetyStockLevelIsRegistered = true;
							}
							onSafetyStockLevel += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSafetyStockLevel -= value;
							if (onSafetyStockLevel == null && onSafetyStockLevelIsRegistered)
							{
								Members.SafetyStockLevel.Events.OnChange -= onSafetyStockLevelProxy;
								onSafetyStockLevelIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSafetyStockLevelProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onSafetyStockLevel;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnReorderPoint

				private static bool onReorderPointIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onReorderPoint;
				public static event EventHandler<Product, PropertyEventArgs> OnReorderPoint
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onReorderPointIsRegistered)
							{
								Members.ReorderPoint.Events.OnChange -= onReorderPointProxy;
								Members.ReorderPoint.Events.OnChange += onReorderPointProxy;
								onReorderPointIsRegistered = true;
							}
							onReorderPoint += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onReorderPoint -= value;
							if (onReorderPoint == null && onReorderPointIsRegistered)
							{
								Members.ReorderPoint.Events.OnChange -= onReorderPointProxy;
								onReorderPointIsRegistered = false;
							}
						}
					}
				}
            
				private static void onReorderPointProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onReorderPoint;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnStandardCost

				private static bool onStandardCostIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onStandardCost;
				public static event EventHandler<Product, PropertyEventArgs> OnStandardCost
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onStandardCostIsRegistered)
							{
								Members.StandardCost.Events.OnChange -= onStandardCostProxy;
								Members.StandardCost.Events.OnChange += onStandardCostProxy;
								onStandardCostIsRegistered = true;
							}
							onStandardCost += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onStandardCost -= value;
							if (onStandardCost == null && onStandardCostIsRegistered)
							{
								Members.StandardCost.Events.OnChange -= onStandardCostProxy;
								onStandardCostIsRegistered = false;
							}
						}
					}
				}
            
				private static void onStandardCostProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onStandardCost;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnListPrice

				private static bool onListPriceIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onListPrice;
				public static event EventHandler<Product, PropertyEventArgs> OnListPrice
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onListPriceIsRegistered)
							{
								Members.ListPrice.Events.OnChange -= onListPriceProxy;
								Members.ListPrice.Events.OnChange += onListPriceProxy;
								onListPriceIsRegistered = true;
							}
							onListPrice += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onListPrice -= value;
							if (onListPrice == null && onListPriceIsRegistered)
							{
								Members.ListPrice.Events.OnChange -= onListPriceProxy;
								onListPriceIsRegistered = false;
							}
						}
					}
				}
            
				private static void onListPriceProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onListPrice;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnSize

				private static bool onSizeIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onSize;
				public static event EventHandler<Product, PropertyEventArgs> OnSize
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSizeIsRegistered)
							{
								Members.Size.Events.OnChange -= onSizeProxy;
								Members.Size.Events.OnChange += onSizeProxy;
								onSizeIsRegistered = true;
							}
							onSize += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSize -= value;
							if (onSize == null && onSizeIsRegistered)
							{
								Members.Size.Events.OnChange -= onSizeProxy;
								onSizeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSizeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onSize;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnSizeUnitMeasureCode

				private static bool onSizeUnitMeasureCodeIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onSizeUnitMeasureCode;
				public static event EventHandler<Product, PropertyEventArgs> OnSizeUnitMeasureCode
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSizeUnitMeasureCodeIsRegistered)
							{
								Members.SizeUnitMeasureCode.Events.OnChange -= onSizeUnitMeasureCodeProxy;
								Members.SizeUnitMeasureCode.Events.OnChange += onSizeUnitMeasureCodeProxy;
								onSizeUnitMeasureCodeIsRegistered = true;
							}
							onSizeUnitMeasureCode += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSizeUnitMeasureCode -= value;
							if (onSizeUnitMeasureCode == null && onSizeUnitMeasureCodeIsRegistered)
							{
								Members.SizeUnitMeasureCode.Events.OnChange -= onSizeUnitMeasureCodeProxy;
								onSizeUnitMeasureCodeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSizeUnitMeasureCodeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onSizeUnitMeasureCode;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnWeightUnitMeasureCode

				private static bool onWeightUnitMeasureCodeIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onWeightUnitMeasureCode;
				public static event EventHandler<Product, PropertyEventArgs> OnWeightUnitMeasureCode
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onWeightUnitMeasureCodeIsRegistered)
							{
								Members.WeightUnitMeasureCode.Events.OnChange -= onWeightUnitMeasureCodeProxy;
								Members.WeightUnitMeasureCode.Events.OnChange += onWeightUnitMeasureCodeProxy;
								onWeightUnitMeasureCodeIsRegistered = true;
							}
							onWeightUnitMeasureCode += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onWeightUnitMeasureCode -= value;
							if (onWeightUnitMeasureCode == null && onWeightUnitMeasureCodeIsRegistered)
							{
								Members.WeightUnitMeasureCode.Events.OnChange -= onWeightUnitMeasureCodeProxy;
								onWeightUnitMeasureCodeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onWeightUnitMeasureCodeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onWeightUnitMeasureCode;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnWeight

				private static bool onWeightIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onWeight;
				public static event EventHandler<Product, PropertyEventArgs> OnWeight
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onWeightIsRegistered)
							{
								Members.Weight.Events.OnChange -= onWeightProxy;
								Members.Weight.Events.OnChange += onWeightProxy;
								onWeightIsRegistered = true;
							}
							onWeight += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onWeight -= value;
							if (onWeight == null && onWeightIsRegistered)
							{
								Members.Weight.Events.OnChange -= onWeightProxy;
								onWeightIsRegistered = false;
							}
						}
					}
				}
            
				private static void onWeightProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onWeight;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnDaysToManufacture

				private static bool onDaysToManufactureIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onDaysToManufacture;
				public static event EventHandler<Product, PropertyEventArgs> OnDaysToManufacture
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDaysToManufactureIsRegistered)
							{
								Members.DaysToManufacture.Events.OnChange -= onDaysToManufactureProxy;
								Members.DaysToManufacture.Events.OnChange += onDaysToManufactureProxy;
								onDaysToManufactureIsRegistered = true;
							}
							onDaysToManufacture += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDaysToManufacture -= value;
							if (onDaysToManufacture == null && onDaysToManufactureIsRegistered)
							{
								Members.DaysToManufacture.Events.OnChange -= onDaysToManufactureProxy;
								onDaysToManufactureIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDaysToManufactureProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onDaysToManufacture;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnProductLine

				private static bool onProductLineIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onProductLine;
				public static event EventHandler<Product, PropertyEventArgs> OnProductLine
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onProductLineIsRegistered)
							{
								Members.ProductLine.Events.OnChange -= onProductLineProxy;
								Members.ProductLine.Events.OnChange += onProductLineProxy;
								onProductLineIsRegistered = true;
							}
							onProductLine += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onProductLine -= value;
							if (onProductLine == null && onProductLineIsRegistered)
							{
								Members.ProductLine.Events.OnChange -= onProductLineProxy;
								onProductLineIsRegistered = false;
							}
						}
					}
				}
            
				private static void onProductLineProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onProductLine;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnClass

				private static bool onClassIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onClass;
				public static event EventHandler<Product, PropertyEventArgs> OnClass
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onClassIsRegistered)
							{
								Members.Class.Events.OnChange -= onClassProxy;
								Members.Class.Events.OnChange += onClassProxy;
								onClassIsRegistered = true;
							}
							onClass += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onClass -= value;
							if (onClass == null && onClassIsRegistered)
							{
								Members.Class.Events.OnChange -= onClassProxy;
								onClassIsRegistered = false;
							}
						}
					}
				}
            
				private static void onClassProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onClass;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnStyle

				private static bool onStyleIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onStyle;
				public static event EventHandler<Product, PropertyEventArgs> OnStyle
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onStyleIsRegistered)
							{
								Members.Style.Events.OnChange -= onStyleProxy;
								Members.Style.Events.OnChange += onStyleProxy;
								onStyleIsRegistered = true;
							}
							onStyle += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onStyle -= value;
							if (onStyle == null && onStyleIsRegistered)
							{
								Members.Style.Events.OnChange -= onStyleProxy;
								onStyleIsRegistered = false;
							}
						}
					}
				}
            
				private static void onStyleProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onStyle;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnSellStartDate

				private static bool onSellStartDateIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onSellStartDate;
				public static event EventHandler<Product, PropertyEventArgs> OnSellStartDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSellStartDateIsRegistered)
							{
								Members.SellStartDate.Events.OnChange -= onSellStartDateProxy;
								Members.SellStartDate.Events.OnChange += onSellStartDateProxy;
								onSellStartDateIsRegistered = true;
							}
							onSellStartDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSellStartDate -= value;
							if (onSellStartDate == null && onSellStartDateIsRegistered)
							{
								Members.SellStartDate.Events.OnChange -= onSellStartDateProxy;
								onSellStartDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSellStartDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onSellStartDate;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnSellEndDate

				private static bool onSellEndDateIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onSellEndDate;
				public static event EventHandler<Product, PropertyEventArgs> OnSellEndDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSellEndDateIsRegistered)
							{
								Members.SellEndDate.Events.OnChange -= onSellEndDateProxy;
								Members.SellEndDate.Events.OnChange += onSellEndDateProxy;
								onSellEndDateIsRegistered = true;
							}
							onSellEndDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSellEndDate -= value;
							if (onSellEndDate == null && onSellEndDateIsRegistered)
							{
								Members.SellEndDate.Events.OnChange -= onSellEndDateProxy;
								onSellEndDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSellEndDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onSellEndDate;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnDiscontinuedDate

				private static bool onDiscontinuedDateIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onDiscontinuedDate;
				public static event EventHandler<Product, PropertyEventArgs> OnDiscontinuedDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDiscontinuedDateIsRegistered)
							{
								Members.DiscontinuedDate.Events.OnChange -= onDiscontinuedDateProxy;
								Members.DiscontinuedDate.Events.OnChange += onDiscontinuedDateProxy;
								onDiscontinuedDateIsRegistered = true;
							}
							onDiscontinuedDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDiscontinuedDate -= value;
							if (onDiscontinuedDate == null && onDiscontinuedDateIsRegistered)
							{
								Members.DiscontinuedDate.Events.OnChange -= onDiscontinuedDateProxy;
								onDiscontinuedDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDiscontinuedDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onDiscontinuedDate;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onrowguid;
				public static event EventHandler<Product, PropertyEventArgs> Onrowguid
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onrowguidIsRegistered)
							{
								Members.rowguid.Events.OnChange -= onrowguidProxy;
								Members.rowguid.Events.OnChange += onrowguidProxy;
								onrowguidIsRegistered = true;
							}
							onrowguid += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onrowguid -= value;
							if (onrowguid == null && onrowguidIsRegistered)
							{
								Members.rowguid.Events.OnChange -= onrowguidProxy;
								onrowguidIsRegistered = false;
							}
						}
					}
				}
            
				private static void onrowguidProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onrowguid;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnTransactionHistory

				private static bool onTransactionHistoryIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onTransactionHistory;
				public static event EventHandler<Product, PropertyEventArgs> OnTransactionHistory
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onTransactionHistoryIsRegistered)
							{
								Members.TransactionHistory.Events.OnChange -= onTransactionHistoryProxy;
								Members.TransactionHistory.Events.OnChange += onTransactionHistoryProxy;
								onTransactionHistoryIsRegistered = true;
							}
							onTransactionHistory += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onTransactionHistory -= value;
							if (onTransactionHistory == null && onTransactionHistoryIsRegistered)
							{
								Members.TransactionHistory.Events.OnChange -= onTransactionHistoryProxy;
								onTransactionHistoryIsRegistered = false;
							}
						}
					}
				}
            
				private static void onTransactionHistoryProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onTransactionHistory;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnProductReview

				private static bool onProductReviewIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onProductReview;
				public static event EventHandler<Product, PropertyEventArgs> OnProductReview
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onProductReviewIsRegistered)
							{
								Members.ProductReview.Events.OnChange -= onProductReviewProxy;
								Members.ProductReview.Events.OnChange += onProductReviewProxy;
								onProductReviewIsRegistered = true;
							}
							onProductReview += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onProductReview -= value;
							if (onProductReview == null && onProductReviewIsRegistered)
							{
								Members.ProductReview.Events.OnChange -= onProductReviewProxy;
								onProductReviewIsRegistered = false;
							}
						}
					}
				}
            
				private static void onProductReviewProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onProductReview;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnProductProductPhoto

				private static bool onProductProductPhotoIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onProductProductPhoto;
				public static event EventHandler<Product, PropertyEventArgs> OnProductProductPhoto
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onProductProductPhotoIsRegistered)
							{
								Members.ProductProductPhoto.Events.OnChange -= onProductProductPhotoProxy;
								Members.ProductProductPhoto.Events.OnChange += onProductProductPhotoProxy;
								onProductProductPhotoIsRegistered = true;
							}
							onProductProductPhoto += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onProductProductPhoto -= value;
							if (onProductProductPhoto == null && onProductProductPhotoIsRegistered)
							{
								Members.ProductProductPhoto.Events.OnChange -= onProductProductPhotoProxy;
								onProductProductPhotoIsRegistered = false;
							}
						}
					}
				}
            
				private static void onProductProductPhotoProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onProductProductPhoto;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnProductModel

				private static bool onProductModelIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onProductModel;
				public static event EventHandler<Product, PropertyEventArgs> OnProductModel
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onProductModelIsRegistered)
							{
								Members.ProductModel.Events.OnChange -= onProductModelProxy;
								Members.ProductModel.Events.OnChange += onProductModelProxy;
								onProductModelIsRegistered = true;
							}
							onProductModel += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onProductModel -= value;
							if (onProductModel == null && onProductModelIsRegistered)
							{
								Members.ProductModel.Events.OnChange -= onProductModelProxy;
								onProductModelIsRegistered = false;
							}
						}
					}
				}
            
				private static void onProductModelProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onProductModel;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnDocument

				private static bool onDocumentIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onDocument;
				public static event EventHandler<Product, PropertyEventArgs> OnDocument
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDocumentIsRegistered)
							{
								Members.Document.Events.OnChange -= onDocumentProxy;
								Members.Document.Events.OnChange += onDocumentProxy;
								onDocumentIsRegistered = true;
							}
							onDocument += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDocument -= value;
							if (onDocument == null && onDocumentIsRegistered)
							{
								Members.Document.Events.OnChange -= onDocumentProxy;
								onDocumentIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDocumentProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onDocument;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnProductListPriceHistories

				private static bool onProductListPriceHistoriesIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onProductListPriceHistories;
				public static event EventHandler<Product, PropertyEventArgs> OnProductListPriceHistories
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onProductListPriceHistoriesIsRegistered)
							{
								Members.ProductListPriceHistories.Events.OnChange -= onProductListPriceHistoriesProxy;
								Members.ProductListPriceHistories.Events.OnChange += onProductListPriceHistoriesProxy;
								onProductListPriceHistoriesIsRegistered = true;
							}
							onProductListPriceHistories += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onProductListPriceHistories -= value;
							if (onProductListPriceHistories == null && onProductListPriceHistoriesIsRegistered)
							{
								Members.ProductListPriceHistories.Events.OnChange -= onProductListPriceHistoriesProxy;
								onProductListPriceHistoriesIsRegistered = false;
							}
						}
					}
				}
            
				private static void onProductListPriceHistoriesProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onProductListPriceHistories;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<Product, PropertyEventArgs> OnModifiedDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onModifiedDateIsRegistered)
							{
								Members.ModifiedDate.Events.OnChange -= onModifiedDateProxy;
								Members.ModifiedDate.Events.OnChange += onModifiedDateProxy;
								onModifiedDateIsRegistered = true;
							}
							onModifiedDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onModifiedDate -= value;
							if (onModifiedDate == null && onModifiedDateIsRegistered)
							{
								Members.ModifiedDate.Events.OnChange -= onModifiedDateProxy;
								onModifiedDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onModifiedDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Product, PropertyEventArgs> onUid;
				public static event EventHandler<Product, PropertyEventArgs> OnUid
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onUidIsRegistered)
							{
								Members.Uid.Events.OnChange -= onUidProxy;
								Members.Uid.Events.OnChange += onUidProxy;
								onUidIsRegistered = true;
							}
							onUid += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onUid -= value;
							if (onUid == null && onUidIsRegistered)
							{
								Members.Uid.Events.OnChange -= onUidProxy;
								onUidIsRegistered = false;
							}
						}
					}
				}
            
				private static void onUidProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Product, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((Product)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IProductOriginalData

		public IProductOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IProduct

		string IProductOriginalData.Name { get { return OriginalData.Name; } }
		string IProductOriginalData.ProductNumber { get { return OriginalData.ProductNumber; } }
		bool IProductOriginalData.MakeFlag { get { return OriginalData.MakeFlag; } }
		bool IProductOriginalData.FinishedGoodsFlag { get { return OriginalData.FinishedGoodsFlag; } }
		string IProductOriginalData.Color { get { return OriginalData.Color; } }
		int IProductOriginalData.SafetyStockLevel { get { return OriginalData.SafetyStockLevel; } }
		int IProductOriginalData.ReorderPoint { get { return OriginalData.ReorderPoint; } }
		double IProductOriginalData.StandardCost { get { return OriginalData.StandardCost; } }
		double IProductOriginalData.ListPrice { get { return OriginalData.ListPrice; } }
		string IProductOriginalData.Size { get { return OriginalData.Size; } }
		string IProductOriginalData.SizeUnitMeasureCode { get { return OriginalData.SizeUnitMeasureCode; } }
		string IProductOriginalData.WeightUnitMeasureCode { get { return OriginalData.WeightUnitMeasureCode; } }
		decimal? IProductOriginalData.Weight { get { return OriginalData.Weight; } }
		int IProductOriginalData.DaysToManufacture { get { return OriginalData.DaysToManufacture; } }
		string IProductOriginalData.ProductLine { get { return OriginalData.ProductLine; } }
		string IProductOriginalData.Class { get { return OriginalData.Class; } }
		string IProductOriginalData.Style { get { return OriginalData.Style; } }
		System.DateTime IProductOriginalData.SellStartDate { get { return OriginalData.SellStartDate; } }
		System.DateTime? IProductOriginalData.SellEndDate { get { return OriginalData.SellEndDate; } }
		System.DateTime? IProductOriginalData.DiscontinuedDate { get { return OriginalData.DiscontinuedDate; } }
		string IProductOriginalData.rowguid { get { return OriginalData.rowguid; } }
		TransactionHistory IProductOriginalData.TransactionHistory { get { return ((ILookupHelper<TransactionHistory>)OriginalData.TransactionHistory).GetOriginalItem(null); } }
		ProductReview IProductOriginalData.ProductReview { get { return ((ILookupHelper<ProductReview>)OriginalData.ProductReview).GetOriginalItem(null); } }
		ProductProductPhoto IProductOriginalData.ProductProductPhoto { get { return ((ILookupHelper<ProductProductPhoto>)OriginalData.ProductProductPhoto).GetOriginalItem(null); } }
		ProductModel IProductOriginalData.ProductModel { get { return ((ILookupHelper<ProductModel>)OriginalData.ProductModel).GetOriginalItem(null); } }
		Document IProductOriginalData.Document { get { return ((ILookupHelper<Document>)OriginalData.Document).GetOriginalItem(null); } }
		IEnumerable<ProductListPriceHistory> IProductOriginalData.ProductListPriceHistories { get { return OriginalData.ProductListPriceHistories.OriginalData; } }

		#endregion
		#region Members for interface ISchemaBase

		ISchemaBaseOriginalData ISchemaBase.OriginalVersion { get { return this; } }

		System.DateTime ISchemaBaseOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		INeo4jBaseOriginalData INeo4jBase.OriginalVersion { get { return this; } }

		string INeo4jBaseOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}