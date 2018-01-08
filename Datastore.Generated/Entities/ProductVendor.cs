 
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
	public interface IProductVendorOriginalData
    {
		#region Outer Data

		#region Members for interface IProductVendor

		string AverageLeadTime { get; }
		string StandardPrice { get; }
		string LastReceiptCost { get; }
		System.DateTime? LastReceiptDate { get; }
		int MinOrderQty { get; }
		int MaxOrderQty { get; }
		int OnOrderQty { get; }
		string UnitMeasureCode { get; }
		UnitMeasure UnitMeasure { get; }
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

	public partial class ProductVendor : OGM<ProductVendor, ProductVendor.ProductVendorData, System.String>, ISchemaBase, INeo4jBase, IProductVendorOriginalData
	{
        #region Initialize

        static ProductVendor()
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

        public static Dictionary<System.String, ProductVendor> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.ProductVendorAlias, IWhereQuery> query)
        {
            q.ProductVendorAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.ProductVendor.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"ProductVendor => AverageLeadTime : {this.AverageLeadTime}, StandardPrice : {this.StandardPrice}, LastReceiptCost : {this.LastReceiptCost?.ToString() ?? "null"}, LastReceiptDate : {this.LastReceiptDate?.ToString() ?? "null"}, MinOrderQty : {this.MinOrderQty}, MaxOrderQty : {this.MaxOrderQty}, OnOrderQty : {this.OnOrderQty}, UnitMeasureCode : {this.UnitMeasureCode?.ToString() ?? "null"}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new ProductVendorData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.AverageLeadTime == null)
				throw new PersistenceException(string.Format("Cannot save ProductVendor with key '{0}' because the AverageLeadTime cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.StandardPrice == null)
				throw new PersistenceException(string.Format("Cannot save ProductVendor with key '{0}' because the StandardPrice cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.MinOrderQty == null)
				throw new PersistenceException(string.Format("Cannot save ProductVendor with key '{0}' because the MinOrderQty cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.MaxOrderQty == null)
				throw new PersistenceException(string.Format("Cannot save ProductVendor with key '{0}' because the MaxOrderQty cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.OnOrderQty == null)
				throw new PersistenceException(string.Format("Cannot save ProductVendor with key '{0}' because the OnOrderQty cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save ProductVendor with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class ProductVendorData : Data<System.String>
		{
			public ProductVendorData()
            {

            }

            public ProductVendorData(ProductVendorData data)
            {
				AverageLeadTime = data.AverageLeadTime;
				StandardPrice = data.StandardPrice;
				LastReceiptCost = data.LastReceiptCost;
				LastReceiptDate = data.LastReceiptDate;
				MinOrderQty = data.MinOrderQty;
				MaxOrderQty = data.MaxOrderQty;
				OnOrderQty = data.OnOrderQty;
				UnitMeasureCode = data.UnitMeasureCode;
				UnitMeasure = data.UnitMeasure;
				Product = data.Product;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "ProductVendor";

				UnitMeasure = new EntityCollection<UnitMeasure>(Wrapper, Members.UnitMeasure);
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
				dictionary.Add("AverageLeadTime",  AverageLeadTime);
				dictionary.Add("StandardPrice",  StandardPrice);
				dictionary.Add("LastReceiptCost",  LastReceiptCost);
				dictionary.Add("LastReceiptDate",  Conversion<System.DateTime?, long?>.Convert(LastReceiptDate));
				dictionary.Add("MinOrderQty",  Conversion<int, long>.Convert(MinOrderQty));
				dictionary.Add("MaxOrderQty",  Conversion<int, long>.Convert(MaxOrderQty));
				dictionary.Add("OnOrderQty",  Conversion<int, long>.Convert(OnOrderQty));
				dictionary.Add("UnitMeasureCode",  UnitMeasureCode);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("AverageLeadTime", out value))
					AverageLeadTime = (string)value;
				if (properties.TryGetValue("StandardPrice", out value))
					StandardPrice = (string)value;
				if (properties.TryGetValue("LastReceiptCost", out value))
					LastReceiptCost = (string)value;
				if (properties.TryGetValue("LastReceiptDate", out value))
					LastReceiptDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("MinOrderQty", out value))
					MinOrderQty = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("MaxOrderQty", out value))
					MaxOrderQty = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("OnOrderQty", out value))
					OnOrderQty = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("UnitMeasureCode", out value))
					UnitMeasureCode = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IProductVendor

			public string AverageLeadTime { get; set; }
			public string StandardPrice { get; set; }
			public string LastReceiptCost { get; set; }
			public System.DateTime? LastReceiptDate { get; set; }
			public int MinOrderQty { get; set; }
			public int MaxOrderQty { get; set; }
			public int OnOrderQty { get; set; }
			public string UnitMeasureCode { get; set; }
			public EntityCollection<UnitMeasure> UnitMeasure { get; private set; }
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

		#region Members for interface IProductVendor

		public string AverageLeadTime { get { LazyGet(); return InnerData.AverageLeadTime; } set { if (LazySet(Members.AverageLeadTime, InnerData.AverageLeadTime, value)) InnerData.AverageLeadTime = value; } }
		public string StandardPrice { get { LazyGet(); return InnerData.StandardPrice; } set { if (LazySet(Members.StandardPrice, InnerData.StandardPrice, value)) InnerData.StandardPrice = value; } }
		public string LastReceiptCost { get { LazyGet(); return InnerData.LastReceiptCost; } set { if (LazySet(Members.LastReceiptCost, InnerData.LastReceiptCost, value)) InnerData.LastReceiptCost = value; } }
		public System.DateTime? LastReceiptDate { get { LazyGet(); return InnerData.LastReceiptDate; } set { if (LazySet(Members.LastReceiptDate, InnerData.LastReceiptDate, value)) InnerData.LastReceiptDate = value; } }
		public int MinOrderQty { get { LazyGet(); return InnerData.MinOrderQty; } set { if (LazySet(Members.MinOrderQty, InnerData.MinOrderQty, value)) InnerData.MinOrderQty = value; } }
		public int MaxOrderQty { get { LazyGet(); return InnerData.MaxOrderQty; } set { if (LazySet(Members.MaxOrderQty, InnerData.MaxOrderQty, value)) InnerData.MaxOrderQty = value; } }
		public int OnOrderQty { get { LazyGet(); return InnerData.OnOrderQty; } set { if (LazySet(Members.OnOrderQty, InnerData.OnOrderQty, value)) InnerData.OnOrderQty = value; } }
		public string UnitMeasureCode { get { LazyGet(); return InnerData.UnitMeasureCode; } set { if (LazySet(Members.UnitMeasureCode, InnerData.UnitMeasureCode, value)) InnerData.UnitMeasureCode = value; } }
		public UnitMeasure UnitMeasure
		{
			get { return ((ILookupHelper<UnitMeasure>)InnerData.UnitMeasure).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.UnitMeasure, ((ILookupHelper<UnitMeasure>)InnerData.UnitMeasure).GetItem(null), value))
					((ILookupHelper<UnitMeasure>)InnerData.UnitMeasure).SetItem(value, null); 
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

        private static ProductVendorMembers members = null;
        public static ProductVendorMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(ProductVendor))
                    {
                        if (members == null)
                            members = new ProductVendorMembers();
                    }
                }
                return members;
            }
        }
        public class ProductVendorMembers
        {
            internal ProductVendorMembers() { }

			#region Members for interface IProductVendor

            public Property AverageLeadTime { get; } = Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["AverageLeadTime"];
            public Property StandardPrice { get; } = Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["StandardPrice"];
            public Property LastReceiptCost { get; } = Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["LastReceiptCost"];
            public Property LastReceiptDate { get; } = Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["LastReceiptDate"];
            public Property MinOrderQty { get; } = Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["MinOrderQty"];
            public Property MaxOrderQty { get; } = Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["MaxOrderQty"];
            public Property OnOrderQty { get; } = Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["OnOrderQty"];
            public Property UnitMeasureCode { get; } = Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["UnitMeasureCode"];
            public Property UnitMeasure { get; } = Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["UnitMeasure"];
            public Property Product { get; } = Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["Product"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static ProductVendorFullTextMembers fullTextMembers = null;
        public static ProductVendorFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(ProductVendor))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new ProductVendorFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class ProductVendorFullTextMembers
        {
            internal ProductVendorFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(ProductVendor))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["ProductVendor"];
                }
            }
            return entity;
        }

		private static ProductVendorEvents events = null;
        public static ProductVendorEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(ProductVendor))
                    {
                        if (events == null)
                            events = new ProductVendorEvents();
                    }
                }
                return events;
            }
        }
        public class ProductVendorEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<ProductVendor, EntityEventArgs> onNew;
            public event EventHandler<ProductVendor, EntityEventArgs> OnNew
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
                EventHandler<ProductVendor, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((ProductVendor)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<ProductVendor, EntityEventArgs> onDelete;
            public event EventHandler<ProductVendor, EntityEventArgs> OnDelete
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
                EventHandler<ProductVendor, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((ProductVendor)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<ProductVendor, EntityEventArgs> onSave;
            public event EventHandler<ProductVendor, EntityEventArgs> OnSave
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
                EventHandler<ProductVendor, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((ProductVendor)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnAverageLeadTime

				private static bool onAverageLeadTimeIsRegistered = false;

				private static EventHandler<ProductVendor, PropertyEventArgs> onAverageLeadTime;
				public static event EventHandler<ProductVendor, PropertyEventArgs> OnAverageLeadTime
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onAverageLeadTimeIsRegistered)
							{
								Members.AverageLeadTime.Events.OnChange -= onAverageLeadTimeProxy;
								Members.AverageLeadTime.Events.OnChange += onAverageLeadTimeProxy;
								onAverageLeadTimeIsRegistered = true;
							}
							onAverageLeadTime += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onAverageLeadTime -= value;
							if (onAverageLeadTime == null && onAverageLeadTimeIsRegistered)
							{
								Members.AverageLeadTime.Events.OnChange -= onAverageLeadTimeProxy;
								onAverageLeadTimeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onAverageLeadTimeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductVendor, PropertyEventArgs> handler = onAverageLeadTime;
					if ((object)handler != null)
						handler.Invoke((ProductVendor)sender, args);
				}

				#endregion

				#region OnStandardPrice

				private static bool onStandardPriceIsRegistered = false;

				private static EventHandler<ProductVendor, PropertyEventArgs> onStandardPrice;
				public static event EventHandler<ProductVendor, PropertyEventArgs> OnStandardPrice
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onStandardPriceIsRegistered)
							{
								Members.StandardPrice.Events.OnChange -= onStandardPriceProxy;
								Members.StandardPrice.Events.OnChange += onStandardPriceProxy;
								onStandardPriceIsRegistered = true;
							}
							onStandardPrice += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onStandardPrice -= value;
							if (onStandardPrice == null && onStandardPriceIsRegistered)
							{
								Members.StandardPrice.Events.OnChange -= onStandardPriceProxy;
								onStandardPriceIsRegistered = false;
							}
						}
					}
				}
            
				private static void onStandardPriceProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductVendor, PropertyEventArgs> handler = onStandardPrice;
					if ((object)handler != null)
						handler.Invoke((ProductVendor)sender, args);
				}

				#endregion

				#region OnLastReceiptCost

				private static bool onLastReceiptCostIsRegistered = false;

				private static EventHandler<ProductVendor, PropertyEventArgs> onLastReceiptCost;
				public static event EventHandler<ProductVendor, PropertyEventArgs> OnLastReceiptCost
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onLastReceiptCostIsRegistered)
							{
								Members.LastReceiptCost.Events.OnChange -= onLastReceiptCostProxy;
								Members.LastReceiptCost.Events.OnChange += onLastReceiptCostProxy;
								onLastReceiptCostIsRegistered = true;
							}
							onLastReceiptCost += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onLastReceiptCost -= value;
							if (onLastReceiptCost == null && onLastReceiptCostIsRegistered)
							{
								Members.LastReceiptCost.Events.OnChange -= onLastReceiptCostProxy;
								onLastReceiptCostIsRegistered = false;
							}
						}
					}
				}
            
				private static void onLastReceiptCostProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductVendor, PropertyEventArgs> handler = onLastReceiptCost;
					if ((object)handler != null)
						handler.Invoke((ProductVendor)sender, args);
				}

				#endregion

				#region OnLastReceiptDate

				private static bool onLastReceiptDateIsRegistered = false;

				private static EventHandler<ProductVendor, PropertyEventArgs> onLastReceiptDate;
				public static event EventHandler<ProductVendor, PropertyEventArgs> OnLastReceiptDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onLastReceiptDateIsRegistered)
							{
								Members.LastReceiptDate.Events.OnChange -= onLastReceiptDateProxy;
								Members.LastReceiptDate.Events.OnChange += onLastReceiptDateProxy;
								onLastReceiptDateIsRegistered = true;
							}
							onLastReceiptDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onLastReceiptDate -= value;
							if (onLastReceiptDate == null && onLastReceiptDateIsRegistered)
							{
								Members.LastReceiptDate.Events.OnChange -= onLastReceiptDateProxy;
								onLastReceiptDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onLastReceiptDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductVendor, PropertyEventArgs> handler = onLastReceiptDate;
					if ((object)handler != null)
						handler.Invoke((ProductVendor)sender, args);
				}

				#endregion

				#region OnMinOrderQty

				private static bool onMinOrderQtyIsRegistered = false;

				private static EventHandler<ProductVendor, PropertyEventArgs> onMinOrderQty;
				public static event EventHandler<ProductVendor, PropertyEventArgs> OnMinOrderQty
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onMinOrderQtyIsRegistered)
							{
								Members.MinOrderQty.Events.OnChange -= onMinOrderQtyProxy;
								Members.MinOrderQty.Events.OnChange += onMinOrderQtyProxy;
								onMinOrderQtyIsRegistered = true;
							}
							onMinOrderQty += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onMinOrderQty -= value;
							if (onMinOrderQty == null && onMinOrderQtyIsRegistered)
							{
								Members.MinOrderQty.Events.OnChange -= onMinOrderQtyProxy;
								onMinOrderQtyIsRegistered = false;
							}
						}
					}
				}
            
				private static void onMinOrderQtyProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductVendor, PropertyEventArgs> handler = onMinOrderQty;
					if ((object)handler != null)
						handler.Invoke((ProductVendor)sender, args);
				}

				#endregion

				#region OnMaxOrderQty

				private static bool onMaxOrderQtyIsRegistered = false;

				private static EventHandler<ProductVendor, PropertyEventArgs> onMaxOrderQty;
				public static event EventHandler<ProductVendor, PropertyEventArgs> OnMaxOrderQty
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onMaxOrderQtyIsRegistered)
							{
								Members.MaxOrderQty.Events.OnChange -= onMaxOrderQtyProxy;
								Members.MaxOrderQty.Events.OnChange += onMaxOrderQtyProxy;
								onMaxOrderQtyIsRegistered = true;
							}
							onMaxOrderQty += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onMaxOrderQty -= value;
							if (onMaxOrderQty == null && onMaxOrderQtyIsRegistered)
							{
								Members.MaxOrderQty.Events.OnChange -= onMaxOrderQtyProxy;
								onMaxOrderQtyIsRegistered = false;
							}
						}
					}
				}
            
				private static void onMaxOrderQtyProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductVendor, PropertyEventArgs> handler = onMaxOrderQty;
					if ((object)handler != null)
						handler.Invoke((ProductVendor)sender, args);
				}

				#endregion

				#region OnOnOrderQty

				private static bool onOnOrderQtyIsRegistered = false;

				private static EventHandler<ProductVendor, PropertyEventArgs> onOnOrderQty;
				public static event EventHandler<ProductVendor, PropertyEventArgs> OnOnOrderQty
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onOnOrderQtyIsRegistered)
							{
								Members.OnOrderQty.Events.OnChange -= onOnOrderQtyProxy;
								Members.OnOrderQty.Events.OnChange += onOnOrderQtyProxy;
								onOnOrderQtyIsRegistered = true;
							}
							onOnOrderQty += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onOnOrderQty -= value;
							if (onOnOrderQty == null && onOnOrderQtyIsRegistered)
							{
								Members.OnOrderQty.Events.OnChange -= onOnOrderQtyProxy;
								onOnOrderQtyIsRegistered = false;
							}
						}
					}
				}
            
				private static void onOnOrderQtyProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductVendor, PropertyEventArgs> handler = onOnOrderQty;
					if ((object)handler != null)
						handler.Invoke((ProductVendor)sender, args);
				}

				#endregion

				#region OnUnitMeasureCode

				private static bool onUnitMeasureCodeIsRegistered = false;

				private static EventHandler<ProductVendor, PropertyEventArgs> onUnitMeasureCode;
				public static event EventHandler<ProductVendor, PropertyEventArgs> OnUnitMeasureCode
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onUnitMeasureCodeIsRegistered)
							{
								Members.UnitMeasureCode.Events.OnChange -= onUnitMeasureCodeProxy;
								Members.UnitMeasureCode.Events.OnChange += onUnitMeasureCodeProxy;
								onUnitMeasureCodeIsRegistered = true;
							}
							onUnitMeasureCode += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onUnitMeasureCode -= value;
							if (onUnitMeasureCode == null && onUnitMeasureCodeIsRegistered)
							{
								Members.UnitMeasureCode.Events.OnChange -= onUnitMeasureCodeProxy;
								onUnitMeasureCodeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onUnitMeasureCodeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductVendor, PropertyEventArgs> handler = onUnitMeasureCode;
					if ((object)handler != null)
						handler.Invoke((ProductVendor)sender, args);
				}

				#endregion

				#region OnUnitMeasure

				private static bool onUnitMeasureIsRegistered = false;

				private static EventHandler<ProductVendor, PropertyEventArgs> onUnitMeasure;
				public static event EventHandler<ProductVendor, PropertyEventArgs> OnUnitMeasure
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onUnitMeasureIsRegistered)
							{
								Members.UnitMeasure.Events.OnChange -= onUnitMeasureProxy;
								Members.UnitMeasure.Events.OnChange += onUnitMeasureProxy;
								onUnitMeasureIsRegistered = true;
							}
							onUnitMeasure += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onUnitMeasure -= value;
							if (onUnitMeasure == null && onUnitMeasureIsRegistered)
							{
								Members.UnitMeasure.Events.OnChange -= onUnitMeasureProxy;
								onUnitMeasureIsRegistered = false;
							}
						}
					}
				}
            
				private static void onUnitMeasureProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductVendor, PropertyEventArgs> handler = onUnitMeasure;
					if ((object)handler != null)
						handler.Invoke((ProductVendor)sender, args);
				}

				#endregion

				#region OnProduct

				private static bool onProductIsRegistered = false;

				private static EventHandler<ProductVendor, PropertyEventArgs> onProduct;
				public static event EventHandler<ProductVendor, PropertyEventArgs> OnProduct
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
					EventHandler<ProductVendor, PropertyEventArgs> handler = onProduct;
					if ((object)handler != null)
						handler.Invoke((ProductVendor)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<ProductVendor, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<ProductVendor, PropertyEventArgs> OnModifiedDate
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
					EventHandler<ProductVendor, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((ProductVendor)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<ProductVendor, PropertyEventArgs> onUid;
				public static event EventHandler<ProductVendor, PropertyEventArgs> OnUid
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
					EventHandler<ProductVendor, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((ProductVendor)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IProductVendorOriginalData

		public IProductVendorOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IProductVendor

		string IProductVendorOriginalData.AverageLeadTime { get { return OriginalData.AverageLeadTime; } }
		string IProductVendorOriginalData.StandardPrice { get { return OriginalData.StandardPrice; } }
		string IProductVendorOriginalData.LastReceiptCost { get { return OriginalData.LastReceiptCost; } }
		System.DateTime? IProductVendorOriginalData.LastReceiptDate { get { return OriginalData.LastReceiptDate; } }
		int IProductVendorOriginalData.MinOrderQty { get { return OriginalData.MinOrderQty; } }
		int IProductVendorOriginalData.MaxOrderQty { get { return OriginalData.MaxOrderQty; } }
		int IProductVendorOriginalData.OnOrderQty { get { return OriginalData.OnOrderQty; } }
		string IProductVendorOriginalData.UnitMeasureCode { get { return OriginalData.UnitMeasureCode; } }
		UnitMeasure IProductVendorOriginalData.UnitMeasure { get { return ((ILookupHelper<UnitMeasure>)OriginalData.UnitMeasure).GetOriginalItem(null); } }
		Product IProductVendorOriginalData.Product { get { return ((ILookupHelper<Product>)OriginalData.Product).GetOriginalItem(null); } }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime IProductVendorOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		string IProductVendorOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}