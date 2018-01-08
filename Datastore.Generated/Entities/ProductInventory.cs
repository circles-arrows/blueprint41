 
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
	public interface IProductInventoryOriginalData
    {
		#region Outer Data

		#region Members for interface IProductInventory

		string Shelf { get; }
		string Bin { get; }
		int Quantity { get; }
		string rowguid { get; }
		Location Location { get; }
		Product Product { get; }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ModifiedDate { get; }

		#endregion
		#region Members for interface INeo4jBase

		string Uid { get; }

		#endregion
		#endregion
   
	}

	public partial class ProductInventory : OGM<ProductInventory, ProductInventory.ProductInventoryData, System.String>, ISchemaBase, INeo4jBase, IProductInventoryOriginalData
	{
        #region Initialize

        static ProductInventory()
        {
            Register.Types();
        }

        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadFromNaturalKey
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

        }

        public static Dictionary<System.String, ProductInventory> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.ProductInventoryAlias, IWhereQuery> query)
        {
            q.ProductInventoryAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.ProductInventory.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"ProductInventory => Shelf : {this.Shelf}, Bin : {this.Bin}, Quantity : {this.Quantity}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new ProductInventoryData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.Shelf == null)
				throw new PersistenceException(string.Format("Cannot save ProductInventory with key '{0}' because the Shelf cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Bin == null)
				throw new PersistenceException(string.Format("Cannot save ProductInventory with key '{0}' because the Bin cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Quantity == null)
				throw new PersistenceException(string.Format("Cannot save ProductInventory with key '{0}' because the Quantity cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid == null)
				throw new PersistenceException(string.Format("Cannot save ProductInventory with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save ProductInventory with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class ProductInventoryData : Data<System.String>
		{
			public ProductInventoryData()
            {

            }

            public ProductInventoryData(ProductInventoryData data)
            {
				Shelf = data.Shelf;
				Bin = data.Bin;
				Quantity = data.Quantity;
				rowguid = data.rowguid;
				Location = data.Location;
				Product = data.Product;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "ProductInventory";

				Location = new EntityCollection<Location>(Wrapper, Members.Location);
				Product = new EntityCollection<Product>(Wrapper, Members.Product);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("Shelf",  Shelf);
				dictionary.Add("Bin",  Bin);
				dictionary.Add("Quantity",  Conversion<int, long>.Convert(Quantity));
				dictionary.Add("rowguid",  rowguid);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Shelf", out value))
					Shelf = (string)value;
				if (properties.TryGetValue("Bin", out value))
					Bin = (string)value;
				if (properties.TryGetValue("Quantity", out value))
					Quantity = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IProductInventory

			public string Shelf { get; set; }
			public string Bin { get; set; }
			public int Quantity { get; set; }
			public string rowguid { get; set; }
			public EntityCollection<Location> Location { get; private set; }
			public EntityCollection<Product> Product { get; private set; }

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

		#region Members for interface IProductInventory

		public string Shelf { get { LazyGet(); return InnerData.Shelf; } set { if (LazySet(Members.Shelf, InnerData.Shelf, value)) InnerData.Shelf = value; } }
		public string Bin { get { LazyGet(); return InnerData.Bin; } set { if (LazySet(Members.Bin, InnerData.Bin, value)) InnerData.Bin = value; } }
		public int Quantity { get { LazyGet(); return InnerData.Quantity; } set { if (LazySet(Members.Quantity, InnerData.Quantity, value)) InnerData.Quantity = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }
		public Location Location
		{
			get { return ((ILookupHelper<Location>)InnerData.Location).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Location, ((ILookupHelper<Location>)InnerData.Location).GetItem(null), value))
					((ILookupHelper<Location>)InnerData.Location).SetItem(value, null); 
			}
		}
		public Product Product
		{
			get { return ((ILookupHelper<Product>)InnerData.Product).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Product, ((ILookupHelper<Product>)InnerData.Product).GetItem(null), value))
					((ILookupHelper<Product>)InnerData.Product).SetItem(value, null); 
			}
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

        private static ProductInventoryMembers members = null;
        public static ProductInventoryMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(ProductInventory))
                    {
                        if (members == null)
                            members = new ProductInventoryMembers();
                    }
                }
                return members;
            }
        }
        public class ProductInventoryMembers
        {
            internal ProductInventoryMembers() { }

			#region Members for interface IProductInventory

            public Property Shelf { get; } = Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["Shelf"];
            public Property Bin { get; } = Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["Bin"];
            public Property Quantity { get; } = Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["Quantity"];
            public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["rowguid"];
            public Property Location { get; } = Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["Location"];
            public Property Product { get; } = Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["Product"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static ProductInventoryFullTextMembers fullTextMembers = null;
        public static ProductInventoryFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(ProductInventory))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new ProductInventoryFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class ProductInventoryFullTextMembers
        {
            internal ProductInventoryFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(ProductInventory))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["ProductInventory"];
                }
            }
            return entity;
        }

		private static ProductInventoryEvents events = null;
        public static ProductInventoryEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(ProductInventory))
                    {
                        if (events == null)
                            events = new ProductInventoryEvents();
                    }
                }
                return events;
            }
        }
        public class ProductInventoryEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<ProductInventory, EntityEventArgs> onNew;
            public event EventHandler<ProductInventory, EntityEventArgs> OnNew
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
                EventHandler<ProductInventory, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((ProductInventory)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<ProductInventory, EntityEventArgs> onDelete;
            public event EventHandler<ProductInventory, EntityEventArgs> OnDelete
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
                EventHandler<ProductInventory, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((ProductInventory)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<ProductInventory, EntityEventArgs> onSave;
            public event EventHandler<ProductInventory, EntityEventArgs> OnSave
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
                EventHandler<ProductInventory, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((ProductInventory)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnShelf

				private static bool onShelfIsRegistered = false;

				private static EventHandler<ProductInventory, PropertyEventArgs> onShelf;
				public static event EventHandler<ProductInventory, PropertyEventArgs> OnShelf
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onShelfIsRegistered)
							{
								Members.Shelf.Events.OnChange -= onShelfProxy;
								Members.Shelf.Events.OnChange += onShelfProxy;
								onShelfIsRegistered = true;
							}
							onShelf += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onShelf -= value;
							if (onShelf == null && onShelfIsRegistered)
							{
								Members.Shelf.Events.OnChange -= onShelfProxy;
								onShelfIsRegistered = false;
							}
						}
					}
				}
            
				private static void onShelfProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductInventory, PropertyEventArgs> handler = onShelf;
					if ((object)handler != null)
						handler.Invoke((ProductInventory)sender, args);
				}

				#endregion

				#region OnBin

				private static bool onBinIsRegistered = false;

				private static EventHandler<ProductInventory, PropertyEventArgs> onBin;
				public static event EventHandler<ProductInventory, PropertyEventArgs> OnBin
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onBinIsRegistered)
							{
								Members.Bin.Events.OnChange -= onBinProxy;
								Members.Bin.Events.OnChange += onBinProxy;
								onBinIsRegistered = true;
							}
							onBin += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onBin -= value;
							if (onBin == null && onBinIsRegistered)
							{
								Members.Bin.Events.OnChange -= onBinProxy;
								onBinIsRegistered = false;
							}
						}
					}
				}
            
				private static void onBinProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductInventory, PropertyEventArgs> handler = onBin;
					if ((object)handler != null)
						handler.Invoke((ProductInventory)sender, args);
				}

				#endregion

				#region OnQuantity

				private static bool onQuantityIsRegistered = false;

				private static EventHandler<ProductInventory, PropertyEventArgs> onQuantity;
				public static event EventHandler<ProductInventory, PropertyEventArgs> OnQuantity
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onQuantityIsRegistered)
							{
								Members.Quantity.Events.OnChange -= onQuantityProxy;
								Members.Quantity.Events.OnChange += onQuantityProxy;
								onQuantityIsRegistered = true;
							}
							onQuantity += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onQuantity -= value;
							if (onQuantity == null && onQuantityIsRegistered)
							{
								Members.Quantity.Events.OnChange -= onQuantityProxy;
								onQuantityIsRegistered = false;
							}
						}
					}
				}
            
				private static void onQuantityProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductInventory, PropertyEventArgs> handler = onQuantity;
					if ((object)handler != null)
						handler.Invoke((ProductInventory)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<ProductInventory, PropertyEventArgs> onrowguid;
				public static event EventHandler<ProductInventory, PropertyEventArgs> Onrowguid
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
					EventHandler<ProductInventory, PropertyEventArgs> handler = onrowguid;
					if ((object)handler != null)
						handler.Invoke((ProductInventory)sender, args);
				}

				#endregion

				#region OnLocation

				private static bool onLocationIsRegistered = false;

				private static EventHandler<ProductInventory, PropertyEventArgs> onLocation;
				public static event EventHandler<ProductInventory, PropertyEventArgs> OnLocation
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onLocationIsRegistered)
							{
								Members.Location.Events.OnChange -= onLocationProxy;
								Members.Location.Events.OnChange += onLocationProxy;
								onLocationIsRegistered = true;
							}
							onLocation += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onLocation -= value;
							if (onLocation == null && onLocationIsRegistered)
							{
								Members.Location.Events.OnChange -= onLocationProxy;
								onLocationIsRegistered = false;
							}
						}
					}
				}
            
				private static void onLocationProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductInventory, PropertyEventArgs> handler = onLocation;
					if ((object)handler != null)
						handler.Invoke((ProductInventory)sender, args);
				}

				#endregion

				#region OnProduct

				private static bool onProductIsRegistered = false;

				private static EventHandler<ProductInventory, PropertyEventArgs> onProduct;
				public static event EventHandler<ProductInventory, PropertyEventArgs> OnProduct
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onProductIsRegistered)
							{
								Members.Product.Events.OnChange -= onProductProxy;
								Members.Product.Events.OnChange += onProductProxy;
								onProductIsRegistered = true;
							}
							onProduct += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onProduct -= value;
							if (onProduct == null && onProductIsRegistered)
							{
								Members.Product.Events.OnChange -= onProductProxy;
								onProductIsRegistered = false;
							}
						}
					}
				}
            
				private static void onProductProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductInventory, PropertyEventArgs> handler = onProduct;
					if ((object)handler != null)
						handler.Invoke((ProductInventory)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<ProductInventory, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<ProductInventory, PropertyEventArgs> OnModifiedDate
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
					EventHandler<ProductInventory, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((ProductInventory)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<ProductInventory, PropertyEventArgs> onUid;
				public static event EventHandler<ProductInventory, PropertyEventArgs> OnUid
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
					EventHandler<ProductInventory, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((ProductInventory)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IProductInventoryOriginalData

		public IProductInventoryOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IProductInventory

		string IProductInventoryOriginalData.Shelf { get { return OriginalData.Shelf; } }
		string IProductInventoryOriginalData.Bin { get { return OriginalData.Bin; } }
		int IProductInventoryOriginalData.Quantity { get { return OriginalData.Quantity; } }
		string IProductInventoryOriginalData.rowguid { get { return OriginalData.rowguid; } }
		Location IProductInventoryOriginalData.Location { get { return ((ILookupHelper<Location>)OriginalData.Location).GetOriginalItem(null); } }
		Product IProductInventoryOriginalData.Product { get { return ((ILookupHelper<Product>)OriginalData.Product).GetOriginalItem(null); } }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime IProductInventoryOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		string IProductInventoryOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}