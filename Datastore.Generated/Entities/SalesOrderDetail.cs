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
	public interface ISalesOrderDetailOriginalData
    {
		#region Outer Data

		#region Members for interface ISalesOrderDetail

		string CarrierTrackingNumber { get; }
		int OrderQty { get; }
		double UnitPrice { get; }
		string UnitPriceDiscount { get; }
		string LineTotal { get; }
		string rowguid { get; }
		SalesOrderHeader SalesOrderHeader { get; }
		Product Product { get; }
		SpecialOffer SpecialOffer { get; }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ModifiedDate { get; }

		#endregion
		#region Members for interface INeo4jBase

		string Uid { get; }

		#endregion
		#endregion
    }

	public partial class SalesOrderDetail : OGM<SalesOrderDetail, SalesOrderDetail.SalesOrderDetailData, System.String>, ISchemaBase, INeo4jBase, ISalesOrderDetailOriginalData
	{
        #region Initialize

        static SalesOrderDetail()
        {
            Register.Types();
        }

        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadFromNaturalKey
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

			AdditionalGeneratedStoredQueries();
        }
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, SalesOrderDetail> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.SalesOrderDetailAlias, IWhereQuery> query)
        {
            q.SalesOrderDetailAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.SalesOrderDetail.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"SalesOrderDetail => CarrierTrackingNumber : {this.CarrierTrackingNumber?.ToString() ?? "null"}, OrderQty : {this.OrderQty}, UnitPrice : {this.UnitPrice}, UnitPriceDiscount : {this.UnitPriceDiscount}, LineTotal : {this.LineTotal}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new SalesOrderDetailData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.OrderQty == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderDetail with key '{0}' because the OrderQty cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.UnitPrice == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderDetail with key '{0}' because the UnitPrice cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.UnitPriceDiscount == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderDetail with key '{0}' because the UnitPriceDiscount cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.LineTotal == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderDetail with key '{0}' because the LineTotal cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderDetail with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderDetail with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class SalesOrderDetailData : Data<System.String>
		{
			public SalesOrderDetailData()
            {

            }

            public SalesOrderDetailData(SalesOrderDetailData data)
            {
				CarrierTrackingNumber = data.CarrierTrackingNumber;
				OrderQty = data.OrderQty;
				UnitPrice = data.UnitPrice;
				UnitPriceDiscount = data.UnitPriceDiscount;
				LineTotal = data.LineTotal;
				rowguid = data.rowguid;
				SalesOrderHeader = data.SalesOrderHeader;
				Product = data.Product;
				SpecialOffer = data.SpecialOffer;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "SalesOrderDetail";

				SalesOrderHeader = new EntityCollection<SalesOrderHeader>(Wrapper, Members.SalesOrderHeader);
				Product = new EntityCollection<Product>(Wrapper, Members.Product);
				SpecialOffer = new EntityCollection<SpecialOffer>(Wrapper, Members.SpecialOffer);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("CarrierTrackingNumber",  CarrierTrackingNumber);
				dictionary.Add("OrderQty",  Conversion<int, long>.Convert(OrderQty));
				dictionary.Add("UnitPrice",  UnitPrice);
				dictionary.Add("UnitPriceDiscount",  UnitPriceDiscount);
				dictionary.Add("LineTotal",  LineTotal);
				dictionary.Add("rowguid",  rowguid);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("CarrierTrackingNumber", out value))
					CarrierTrackingNumber = (string)value;
				if (properties.TryGetValue("OrderQty", out value))
					OrderQty = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("UnitPrice", out value))
					UnitPrice = (double)value;
				if (properties.TryGetValue("UnitPriceDiscount", out value))
					UnitPriceDiscount = (string)value;
				if (properties.TryGetValue("LineTotal", out value))
					LineTotal = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface ISalesOrderDetail

			public string CarrierTrackingNumber { get; set; }
			public int OrderQty { get; set; }
			public double UnitPrice { get; set; }
			public string UnitPriceDiscount { get; set; }
			public string LineTotal { get; set; }
			public string rowguid { get; set; }
			public EntityCollection<SalesOrderHeader> SalesOrderHeader { get; private set; }
			public EntityCollection<Product> Product { get; private set; }
			public EntityCollection<SpecialOffer> SpecialOffer { get; private set; }

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

		#region Members for interface ISalesOrderDetail

		public string CarrierTrackingNumber { get { LazyGet(); return InnerData.CarrierTrackingNumber; } set { if (LazySet(Members.CarrierTrackingNumber, InnerData.CarrierTrackingNumber, value)) InnerData.CarrierTrackingNumber = value; } }
		public int OrderQty { get { LazyGet(); return InnerData.OrderQty; } set { if (LazySet(Members.OrderQty, InnerData.OrderQty, value)) InnerData.OrderQty = value; } }
		public double UnitPrice { get { LazyGet(); return InnerData.UnitPrice; } set { if (LazySet(Members.UnitPrice, InnerData.UnitPrice, value)) InnerData.UnitPrice = value; } }
		public string UnitPriceDiscount { get { LazyGet(); return InnerData.UnitPriceDiscount; } set { if (LazySet(Members.UnitPriceDiscount, InnerData.UnitPriceDiscount, value)) InnerData.UnitPriceDiscount = value; } }
		public string LineTotal { get { LazyGet(); return InnerData.LineTotal; } set { if (LazySet(Members.LineTotal, InnerData.LineTotal, value)) InnerData.LineTotal = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }
		public SalesOrderHeader SalesOrderHeader
		{
			get { return ((ILookupHelper<SalesOrderHeader>)InnerData.SalesOrderHeader).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.SalesOrderHeader, ((ILookupHelper<SalesOrderHeader>)InnerData.SalesOrderHeader).GetItem(null), value))
					((ILookupHelper<SalesOrderHeader>)InnerData.SalesOrderHeader).SetItem(value, null); 
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
		public SpecialOffer SpecialOffer
		{
			get { return ((ILookupHelper<SpecialOffer>)InnerData.SpecialOffer).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.SpecialOffer, ((ILookupHelper<SpecialOffer>)InnerData.SpecialOffer).GetItem(null), value))
					((ILookupHelper<SpecialOffer>)InnerData.SpecialOffer).SetItem(value, null); 
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

        private static SalesOrderDetailMembers members = null;
        public static SalesOrderDetailMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(SalesOrderDetail))
                    {
                        if (members == null)
                            members = new SalesOrderDetailMembers();
                    }
                }
                return members;
            }
        }
        public class SalesOrderDetailMembers
        {
            internal SalesOrderDetailMembers() { }

			#region Members for interface ISalesOrderDetail

            public Property CarrierTrackingNumber { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["CarrierTrackingNumber"];
            public Property OrderQty { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["OrderQty"];
            public Property UnitPrice { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["UnitPrice"];
            public Property UnitPriceDiscount { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["UnitPriceDiscount"];
            public Property LineTotal { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["LineTotal"];
            public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["rowguid"];
            public Property SalesOrderHeader { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["SalesOrderHeader"];
            public Property Product { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["Product"];
            public Property SpecialOffer { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["SpecialOffer"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static SalesOrderDetailFullTextMembers fullTextMembers = null;
        public static SalesOrderDetailFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(SalesOrderDetail))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new SalesOrderDetailFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class SalesOrderDetailFullTextMembers
        {
            internal SalesOrderDetailFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(SalesOrderDetail))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"];
                }
            }
            return entity;
        }

		private static SalesOrderDetailEvents events = null;
        public static SalesOrderDetailEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(SalesOrderDetail))
                    {
                        if (events == null)
                            events = new SalesOrderDetailEvents();
                    }
                }
                return events;
            }
        }
        public class SalesOrderDetailEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<SalesOrderDetail, EntityEventArgs> onNew;
            public event EventHandler<SalesOrderDetail, EntityEventArgs> OnNew
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
                EventHandler<SalesOrderDetail, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((SalesOrderDetail)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<SalesOrderDetail, EntityEventArgs> onDelete;
            public event EventHandler<SalesOrderDetail, EntityEventArgs> OnDelete
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
                EventHandler<SalesOrderDetail, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((SalesOrderDetail)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<SalesOrderDetail, EntityEventArgs> onSave;
            public event EventHandler<SalesOrderDetail, EntityEventArgs> OnSave
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
                EventHandler<SalesOrderDetail, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((SalesOrderDetail)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnCarrierTrackingNumber

				private static bool onCarrierTrackingNumberIsRegistered = false;

				private static EventHandler<SalesOrderDetail, PropertyEventArgs> onCarrierTrackingNumber;
				public static event EventHandler<SalesOrderDetail, PropertyEventArgs> OnCarrierTrackingNumber
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCarrierTrackingNumberIsRegistered)
							{
								Members.CarrierTrackingNumber.Events.OnChange -= onCarrierTrackingNumberProxy;
								Members.CarrierTrackingNumber.Events.OnChange += onCarrierTrackingNumberProxy;
								onCarrierTrackingNumberIsRegistered = true;
							}
							onCarrierTrackingNumber += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCarrierTrackingNumber -= value;
							if (onCarrierTrackingNumber == null && onCarrierTrackingNumberIsRegistered)
							{
								Members.CarrierTrackingNumber.Events.OnChange -= onCarrierTrackingNumberProxy;
								onCarrierTrackingNumberIsRegistered = false;
							}
						}
					}
				}
            
				private static void onCarrierTrackingNumberProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderDetail, PropertyEventArgs> handler = onCarrierTrackingNumber;
					if ((object)handler != null)
						handler.Invoke((SalesOrderDetail)sender, args);
				}

				#endregion

				#region OnOrderQty

				private static bool onOrderQtyIsRegistered = false;

				private static EventHandler<SalesOrderDetail, PropertyEventArgs> onOrderQty;
				public static event EventHandler<SalesOrderDetail, PropertyEventArgs> OnOrderQty
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onOrderQtyIsRegistered)
							{
								Members.OrderQty.Events.OnChange -= onOrderQtyProxy;
								Members.OrderQty.Events.OnChange += onOrderQtyProxy;
								onOrderQtyIsRegistered = true;
							}
							onOrderQty += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onOrderQty -= value;
							if (onOrderQty == null && onOrderQtyIsRegistered)
							{
								Members.OrderQty.Events.OnChange -= onOrderQtyProxy;
								onOrderQtyIsRegistered = false;
							}
						}
					}
				}
            
				private static void onOrderQtyProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderDetail, PropertyEventArgs> handler = onOrderQty;
					if ((object)handler != null)
						handler.Invoke((SalesOrderDetail)sender, args);
				}

				#endregion

				#region OnUnitPrice

				private static bool onUnitPriceIsRegistered = false;

				private static EventHandler<SalesOrderDetail, PropertyEventArgs> onUnitPrice;
				public static event EventHandler<SalesOrderDetail, PropertyEventArgs> OnUnitPrice
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onUnitPriceIsRegistered)
							{
								Members.UnitPrice.Events.OnChange -= onUnitPriceProxy;
								Members.UnitPrice.Events.OnChange += onUnitPriceProxy;
								onUnitPriceIsRegistered = true;
							}
							onUnitPrice += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onUnitPrice -= value;
							if (onUnitPrice == null && onUnitPriceIsRegistered)
							{
								Members.UnitPrice.Events.OnChange -= onUnitPriceProxy;
								onUnitPriceIsRegistered = false;
							}
						}
					}
				}
            
				private static void onUnitPriceProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderDetail, PropertyEventArgs> handler = onUnitPrice;
					if ((object)handler != null)
						handler.Invoke((SalesOrderDetail)sender, args);
				}

				#endregion

				#region OnUnitPriceDiscount

				private static bool onUnitPriceDiscountIsRegistered = false;

				private static EventHandler<SalesOrderDetail, PropertyEventArgs> onUnitPriceDiscount;
				public static event EventHandler<SalesOrderDetail, PropertyEventArgs> OnUnitPriceDiscount
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onUnitPriceDiscountIsRegistered)
							{
								Members.UnitPriceDiscount.Events.OnChange -= onUnitPriceDiscountProxy;
								Members.UnitPriceDiscount.Events.OnChange += onUnitPriceDiscountProxy;
								onUnitPriceDiscountIsRegistered = true;
							}
							onUnitPriceDiscount += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onUnitPriceDiscount -= value;
							if (onUnitPriceDiscount == null && onUnitPriceDiscountIsRegistered)
							{
								Members.UnitPriceDiscount.Events.OnChange -= onUnitPriceDiscountProxy;
								onUnitPriceDiscountIsRegistered = false;
							}
						}
					}
				}
            
				private static void onUnitPriceDiscountProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderDetail, PropertyEventArgs> handler = onUnitPriceDiscount;
					if ((object)handler != null)
						handler.Invoke((SalesOrderDetail)sender, args);
				}

				#endregion

				#region OnLineTotal

				private static bool onLineTotalIsRegistered = false;

				private static EventHandler<SalesOrderDetail, PropertyEventArgs> onLineTotal;
				public static event EventHandler<SalesOrderDetail, PropertyEventArgs> OnLineTotal
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onLineTotalIsRegistered)
							{
								Members.LineTotal.Events.OnChange -= onLineTotalProxy;
								Members.LineTotal.Events.OnChange += onLineTotalProxy;
								onLineTotalIsRegistered = true;
							}
							onLineTotal += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onLineTotal -= value;
							if (onLineTotal == null && onLineTotalIsRegistered)
							{
								Members.LineTotal.Events.OnChange -= onLineTotalProxy;
								onLineTotalIsRegistered = false;
							}
						}
					}
				}
            
				private static void onLineTotalProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderDetail, PropertyEventArgs> handler = onLineTotal;
					if ((object)handler != null)
						handler.Invoke((SalesOrderDetail)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<SalesOrderDetail, PropertyEventArgs> onrowguid;
				public static event EventHandler<SalesOrderDetail, PropertyEventArgs> Onrowguid
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
					EventHandler<SalesOrderDetail, PropertyEventArgs> handler = onrowguid;
					if ((object)handler != null)
						handler.Invoke((SalesOrderDetail)sender, args);
				}

				#endregion

				#region OnSalesOrderHeader

				private static bool onSalesOrderHeaderIsRegistered = false;

				private static EventHandler<SalesOrderDetail, PropertyEventArgs> onSalesOrderHeader;
				public static event EventHandler<SalesOrderDetail, PropertyEventArgs> OnSalesOrderHeader
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalesOrderHeaderIsRegistered)
							{
								Members.SalesOrderHeader.Events.OnChange -= onSalesOrderHeaderProxy;
								Members.SalesOrderHeader.Events.OnChange += onSalesOrderHeaderProxy;
								onSalesOrderHeaderIsRegistered = true;
							}
							onSalesOrderHeader += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalesOrderHeader -= value;
							if (onSalesOrderHeader == null && onSalesOrderHeaderIsRegistered)
							{
								Members.SalesOrderHeader.Events.OnChange -= onSalesOrderHeaderProxy;
								onSalesOrderHeaderIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSalesOrderHeaderProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderDetail, PropertyEventArgs> handler = onSalesOrderHeader;
					if ((object)handler != null)
						handler.Invoke((SalesOrderDetail)sender, args);
				}

				#endregion

				#region OnProduct

				private static bool onProductIsRegistered = false;

				private static EventHandler<SalesOrderDetail, PropertyEventArgs> onProduct;
				public static event EventHandler<SalesOrderDetail, PropertyEventArgs> OnProduct
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
					EventHandler<SalesOrderDetail, PropertyEventArgs> handler = onProduct;
					if ((object)handler != null)
						handler.Invoke((SalesOrderDetail)sender, args);
				}

				#endregion

				#region OnSpecialOffer

				private static bool onSpecialOfferIsRegistered = false;

				private static EventHandler<SalesOrderDetail, PropertyEventArgs> onSpecialOffer;
				public static event EventHandler<SalesOrderDetail, PropertyEventArgs> OnSpecialOffer
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSpecialOfferIsRegistered)
							{
								Members.SpecialOffer.Events.OnChange -= onSpecialOfferProxy;
								Members.SpecialOffer.Events.OnChange += onSpecialOfferProxy;
								onSpecialOfferIsRegistered = true;
							}
							onSpecialOffer += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSpecialOffer -= value;
							if (onSpecialOffer == null && onSpecialOfferIsRegistered)
							{
								Members.SpecialOffer.Events.OnChange -= onSpecialOfferProxy;
								onSpecialOfferIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSpecialOfferProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderDetail, PropertyEventArgs> handler = onSpecialOffer;
					if ((object)handler != null)
						handler.Invoke((SalesOrderDetail)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<SalesOrderDetail, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<SalesOrderDetail, PropertyEventArgs> OnModifiedDate
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
					EventHandler<SalesOrderDetail, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((SalesOrderDetail)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<SalesOrderDetail, PropertyEventArgs> onUid;
				public static event EventHandler<SalesOrderDetail, PropertyEventArgs> OnUid
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
					EventHandler<SalesOrderDetail, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((SalesOrderDetail)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region ISalesOrderDetailOriginalData

		public ISalesOrderDetailOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ISalesOrderDetail

		string ISalesOrderDetailOriginalData.CarrierTrackingNumber { get { return OriginalData.CarrierTrackingNumber; } }
		int ISalesOrderDetailOriginalData.OrderQty { get { return OriginalData.OrderQty; } }
		double ISalesOrderDetailOriginalData.UnitPrice { get { return OriginalData.UnitPrice; } }
		string ISalesOrderDetailOriginalData.UnitPriceDiscount { get { return OriginalData.UnitPriceDiscount; } }
		string ISalesOrderDetailOriginalData.LineTotal { get { return OriginalData.LineTotal; } }
		string ISalesOrderDetailOriginalData.rowguid { get { return OriginalData.rowguid; } }
		SalesOrderHeader ISalesOrderDetailOriginalData.SalesOrderHeader { get { return ((ILookupHelper<SalesOrderHeader>)OriginalData.SalesOrderHeader).GetOriginalItem(null); } }
		Product ISalesOrderDetailOriginalData.Product { get { return ((ILookupHelper<Product>)OriginalData.Product).GetOriginalItem(null); } }
		SpecialOffer ISalesOrderDetailOriginalData.SpecialOffer { get { return ((ILookupHelper<SpecialOffer>)OriginalData.SpecialOffer).GetOriginalItem(null); } }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ISalesOrderDetailOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		string ISalesOrderDetailOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}