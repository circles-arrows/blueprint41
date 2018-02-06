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
	public interface IPurchaseOrderDetailOriginalData : ISchemaBaseOriginalData
    {
		System.DateTime DueDate { get; }
		int OrderQty { get; }
		double UnitPrice { get; }
		string LineTotal { get; }
		int ReceivedQty { get; }
		int RejectedQty { get; }
		int StockedQty { get; }
		Product Product { get; }
		PurchaseOrderHeader PurchaseOrderHeader { get; }
    }

	public partial class PurchaseOrderDetail : OGM<PurchaseOrderDetail, PurchaseOrderDetail.PurchaseOrderDetailData, System.String>, ISchemaBase, INeo4jBase, IPurchaseOrderDetailOriginalData
	{
        #region Initialize

        static PurchaseOrderDetail()
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

        public static Dictionary<System.String, PurchaseOrderDetail> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.PurchaseOrderDetailAlias, IWhereQuery> query)
        {
            q.PurchaseOrderDetailAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.PurchaseOrderDetail.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"PurchaseOrderDetail => DueDate : {this.DueDate}, OrderQty : {this.OrderQty}, UnitPrice : {this.UnitPrice}, LineTotal : {this.LineTotal}, ReceivedQty : {this.ReceivedQty}, RejectedQty : {this.RejectedQty}, StockedQty : {this.StockedQty}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new PurchaseOrderDetailData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.DueDate == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderDetail with key '{0}' because the DueDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.OrderQty == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderDetail with key '{0}' because the OrderQty cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.UnitPrice == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderDetail with key '{0}' because the UnitPrice cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.LineTotal == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderDetail with key '{0}' because the LineTotal cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ReceivedQty == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderDetail with key '{0}' because the ReceivedQty cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.RejectedQty == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderDetail with key '{0}' because the RejectedQty cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.StockedQty == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderDetail with key '{0}' because the StockedQty cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderDetail with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class PurchaseOrderDetailData : Data<System.String>
		{
			public PurchaseOrderDetailData()
            {

            }

            public PurchaseOrderDetailData(PurchaseOrderDetailData data)
            {
				DueDate = data.DueDate;
				OrderQty = data.OrderQty;
				UnitPrice = data.UnitPrice;
				LineTotal = data.LineTotal;
				ReceivedQty = data.ReceivedQty;
				RejectedQty = data.RejectedQty;
				StockedQty = data.StockedQty;
				Product = data.Product;
				PurchaseOrderHeader = data.PurchaseOrderHeader;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "PurchaseOrderDetail";

				Product = new EntityCollection<Product>(Wrapper, Members.Product);
				PurchaseOrderHeader = new EntityCollection<PurchaseOrderHeader>(Wrapper, Members.PurchaseOrderHeader);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("DueDate",  Conversion<System.DateTime, long>.Convert(DueDate));
				dictionary.Add("OrderQty",  Conversion<int, long>.Convert(OrderQty));
				dictionary.Add("UnitPrice",  UnitPrice);
				dictionary.Add("LineTotal",  LineTotal);
				dictionary.Add("ReceivedQty",  Conversion<int, long>.Convert(ReceivedQty));
				dictionary.Add("RejectedQty",  Conversion<int, long>.Convert(RejectedQty));
				dictionary.Add("StockedQty",  Conversion<int, long>.Convert(StockedQty));
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("DueDate", out value))
					DueDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("OrderQty", out value))
					OrderQty = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("UnitPrice", out value))
					UnitPrice = (double)value;
				if (properties.TryGetValue("LineTotal", out value))
					LineTotal = (string)value;
				if (properties.TryGetValue("ReceivedQty", out value))
					ReceivedQty = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("RejectedQty", out value))
					RejectedQty = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("StockedQty", out value))
					StockedQty = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IPurchaseOrderDetail

			public System.DateTime DueDate { get; set; }
			public int OrderQty { get; set; }
			public double UnitPrice { get; set; }
			public string LineTotal { get; set; }
			public int ReceivedQty { get; set; }
			public int RejectedQty { get; set; }
			public int StockedQty { get; set; }
			public EntityCollection<Product> Product { get; private set; }
			public EntityCollection<PurchaseOrderHeader> PurchaseOrderHeader { get; private set; }

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

		#region Members for interface IPurchaseOrderDetail

		public System.DateTime DueDate { get { LazyGet(); return InnerData.DueDate; } set { if (LazySet(Members.DueDate, InnerData.DueDate, value)) InnerData.DueDate = value; } }
		public int OrderQty { get { LazyGet(); return InnerData.OrderQty; } set { if (LazySet(Members.OrderQty, InnerData.OrderQty, value)) InnerData.OrderQty = value; } }
		public double UnitPrice { get { LazyGet(); return InnerData.UnitPrice; } set { if (LazySet(Members.UnitPrice, InnerData.UnitPrice, value)) InnerData.UnitPrice = value; } }
		public string LineTotal { get { LazyGet(); return InnerData.LineTotal; } set { if (LazySet(Members.LineTotal, InnerData.LineTotal, value)) InnerData.LineTotal = value; } }
		public int ReceivedQty { get { LazyGet(); return InnerData.ReceivedQty; } set { if (LazySet(Members.ReceivedQty, InnerData.ReceivedQty, value)) InnerData.ReceivedQty = value; } }
		public int RejectedQty { get { LazyGet(); return InnerData.RejectedQty; } set { if (LazySet(Members.RejectedQty, InnerData.RejectedQty, value)) InnerData.RejectedQty = value; } }
		public int StockedQty { get { LazyGet(); return InnerData.StockedQty; } set { if (LazySet(Members.StockedQty, InnerData.StockedQty, value)) InnerData.StockedQty = value; } }
		public Product Product
		{
			get { return ((ILookupHelper<Product>)InnerData.Product).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Product, ((ILookupHelper<Product>)InnerData.Product).GetItem(null), value))
					((ILookupHelper<Product>)InnerData.Product).SetItem(value, null); 
			}
		}
		public PurchaseOrderHeader PurchaseOrderHeader
		{
			get { return ((ILookupHelper<PurchaseOrderHeader>)InnerData.PurchaseOrderHeader).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.PurchaseOrderHeader, ((ILookupHelper<PurchaseOrderHeader>)InnerData.PurchaseOrderHeader).GetItem(null), value))
					((ILookupHelper<PurchaseOrderHeader>)InnerData.PurchaseOrderHeader).SetItem(value, null); 
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

        private static PurchaseOrderDetailMembers members = null;
        public static PurchaseOrderDetailMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(PurchaseOrderDetail))
                    {
                        if (members == null)
                            members = new PurchaseOrderDetailMembers();
                    }
                }
                return members;
            }
        }
        public class PurchaseOrderDetailMembers
        {
            internal PurchaseOrderDetailMembers() { }

			#region Members for interface IPurchaseOrderDetail

            public Property DueDate { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["DueDate"];
            public Property OrderQty { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["OrderQty"];
            public Property UnitPrice { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["UnitPrice"];
            public Property LineTotal { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["LineTotal"];
            public Property ReceivedQty { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["ReceivedQty"];
            public Property RejectedQty { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["RejectedQty"];
            public Property StockedQty { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["StockedQty"];
            public Property Product { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["Product"];
            public Property PurchaseOrderHeader { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["PurchaseOrderHeader"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static PurchaseOrderDetailFullTextMembers fullTextMembers = null;
        public static PurchaseOrderDetailFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(PurchaseOrderDetail))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new PurchaseOrderDetailFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class PurchaseOrderDetailFullTextMembers
        {
            internal PurchaseOrderDetailFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(PurchaseOrderDetail))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"];
                }
            }
            return entity;
        }

		private static PurchaseOrderDetailEvents events = null;
        public static PurchaseOrderDetailEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(PurchaseOrderDetail))
                    {
                        if (events == null)
                            events = new PurchaseOrderDetailEvents();
                    }
                }
                return events;
            }
        }
        public class PurchaseOrderDetailEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<PurchaseOrderDetail, EntityEventArgs> onNew;
            public event EventHandler<PurchaseOrderDetail, EntityEventArgs> OnNew
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
                EventHandler<PurchaseOrderDetail, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((PurchaseOrderDetail)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<PurchaseOrderDetail, EntityEventArgs> onDelete;
            public event EventHandler<PurchaseOrderDetail, EntityEventArgs> OnDelete
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
                EventHandler<PurchaseOrderDetail, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((PurchaseOrderDetail)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<PurchaseOrderDetail, EntityEventArgs> onSave;
            public event EventHandler<PurchaseOrderDetail, EntityEventArgs> OnSave
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
                EventHandler<PurchaseOrderDetail, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((PurchaseOrderDetail)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnDueDate

				private static bool onDueDateIsRegistered = false;

				private static EventHandler<PurchaseOrderDetail, PropertyEventArgs> onDueDate;
				public static event EventHandler<PurchaseOrderDetail, PropertyEventArgs> OnDueDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDueDateIsRegistered)
							{
								Members.DueDate.Events.OnChange -= onDueDateProxy;
								Members.DueDate.Events.OnChange += onDueDateProxy;
								onDueDateIsRegistered = true;
							}
							onDueDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDueDate -= value;
							if (onDueDate == null && onDueDateIsRegistered)
							{
								Members.DueDate.Events.OnChange -= onDueDateProxy;
								onDueDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDueDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<PurchaseOrderDetail, PropertyEventArgs> handler = onDueDate;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderDetail)sender, args);
				}

				#endregion

				#region OnOrderQty

				private static bool onOrderQtyIsRegistered = false;

				private static EventHandler<PurchaseOrderDetail, PropertyEventArgs> onOrderQty;
				public static event EventHandler<PurchaseOrderDetail, PropertyEventArgs> OnOrderQty
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
					EventHandler<PurchaseOrderDetail, PropertyEventArgs> handler = onOrderQty;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderDetail)sender, args);
				}

				#endregion

				#region OnUnitPrice

				private static bool onUnitPriceIsRegistered = false;

				private static EventHandler<PurchaseOrderDetail, PropertyEventArgs> onUnitPrice;
				public static event EventHandler<PurchaseOrderDetail, PropertyEventArgs> OnUnitPrice
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
					EventHandler<PurchaseOrderDetail, PropertyEventArgs> handler = onUnitPrice;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderDetail)sender, args);
				}

				#endregion

				#region OnLineTotal

				private static bool onLineTotalIsRegistered = false;

				private static EventHandler<PurchaseOrderDetail, PropertyEventArgs> onLineTotal;
				public static event EventHandler<PurchaseOrderDetail, PropertyEventArgs> OnLineTotal
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
					EventHandler<PurchaseOrderDetail, PropertyEventArgs> handler = onLineTotal;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderDetail)sender, args);
				}

				#endregion

				#region OnReceivedQty

				private static bool onReceivedQtyIsRegistered = false;

				private static EventHandler<PurchaseOrderDetail, PropertyEventArgs> onReceivedQty;
				public static event EventHandler<PurchaseOrderDetail, PropertyEventArgs> OnReceivedQty
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onReceivedQtyIsRegistered)
							{
								Members.ReceivedQty.Events.OnChange -= onReceivedQtyProxy;
								Members.ReceivedQty.Events.OnChange += onReceivedQtyProxy;
								onReceivedQtyIsRegistered = true;
							}
							onReceivedQty += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onReceivedQty -= value;
							if (onReceivedQty == null && onReceivedQtyIsRegistered)
							{
								Members.ReceivedQty.Events.OnChange -= onReceivedQtyProxy;
								onReceivedQtyIsRegistered = false;
							}
						}
					}
				}
            
				private static void onReceivedQtyProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<PurchaseOrderDetail, PropertyEventArgs> handler = onReceivedQty;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderDetail)sender, args);
				}

				#endregion

				#region OnRejectedQty

				private static bool onRejectedQtyIsRegistered = false;

				private static EventHandler<PurchaseOrderDetail, PropertyEventArgs> onRejectedQty;
				public static event EventHandler<PurchaseOrderDetail, PropertyEventArgs> OnRejectedQty
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onRejectedQtyIsRegistered)
							{
								Members.RejectedQty.Events.OnChange -= onRejectedQtyProxy;
								Members.RejectedQty.Events.OnChange += onRejectedQtyProxy;
								onRejectedQtyIsRegistered = true;
							}
							onRejectedQty += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onRejectedQty -= value;
							if (onRejectedQty == null && onRejectedQtyIsRegistered)
							{
								Members.RejectedQty.Events.OnChange -= onRejectedQtyProxy;
								onRejectedQtyIsRegistered = false;
							}
						}
					}
				}
            
				private static void onRejectedQtyProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<PurchaseOrderDetail, PropertyEventArgs> handler = onRejectedQty;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderDetail)sender, args);
				}

				#endregion

				#region OnStockedQty

				private static bool onStockedQtyIsRegistered = false;

				private static EventHandler<PurchaseOrderDetail, PropertyEventArgs> onStockedQty;
				public static event EventHandler<PurchaseOrderDetail, PropertyEventArgs> OnStockedQty
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onStockedQtyIsRegistered)
							{
								Members.StockedQty.Events.OnChange -= onStockedQtyProxy;
								Members.StockedQty.Events.OnChange += onStockedQtyProxy;
								onStockedQtyIsRegistered = true;
							}
							onStockedQty += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onStockedQty -= value;
							if (onStockedQty == null && onStockedQtyIsRegistered)
							{
								Members.StockedQty.Events.OnChange -= onStockedQtyProxy;
								onStockedQtyIsRegistered = false;
							}
						}
					}
				}
            
				private static void onStockedQtyProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<PurchaseOrderDetail, PropertyEventArgs> handler = onStockedQty;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderDetail)sender, args);
				}

				#endregion

				#region OnProduct

				private static bool onProductIsRegistered = false;

				private static EventHandler<PurchaseOrderDetail, PropertyEventArgs> onProduct;
				public static event EventHandler<PurchaseOrderDetail, PropertyEventArgs> OnProduct
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
					EventHandler<PurchaseOrderDetail, PropertyEventArgs> handler = onProduct;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderDetail)sender, args);
				}

				#endregion

				#region OnPurchaseOrderHeader

				private static bool onPurchaseOrderHeaderIsRegistered = false;

				private static EventHandler<PurchaseOrderDetail, PropertyEventArgs> onPurchaseOrderHeader;
				public static event EventHandler<PurchaseOrderDetail, PropertyEventArgs> OnPurchaseOrderHeader
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPurchaseOrderHeaderIsRegistered)
							{
								Members.PurchaseOrderHeader.Events.OnChange -= onPurchaseOrderHeaderProxy;
								Members.PurchaseOrderHeader.Events.OnChange += onPurchaseOrderHeaderProxy;
								onPurchaseOrderHeaderIsRegistered = true;
							}
							onPurchaseOrderHeader += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPurchaseOrderHeader -= value;
							if (onPurchaseOrderHeader == null && onPurchaseOrderHeaderIsRegistered)
							{
								Members.PurchaseOrderHeader.Events.OnChange -= onPurchaseOrderHeaderProxy;
								onPurchaseOrderHeaderIsRegistered = false;
							}
						}
					}
				}
            
				private static void onPurchaseOrderHeaderProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<PurchaseOrderDetail, PropertyEventArgs> handler = onPurchaseOrderHeader;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderDetail)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<PurchaseOrderDetail, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<PurchaseOrderDetail, PropertyEventArgs> OnModifiedDate
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
					EventHandler<PurchaseOrderDetail, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderDetail)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<PurchaseOrderDetail, PropertyEventArgs> onUid;
				public static event EventHandler<PurchaseOrderDetail, PropertyEventArgs> OnUid
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
					EventHandler<PurchaseOrderDetail, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderDetail)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IPurchaseOrderDetailOriginalData

		public IPurchaseOrderDetailOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IPurchaseOrderDetail

		System.DateTime IPurchaseOrderDetailOriginalData.DueDate { get { return OriginalData.DueDate; } }
		int IPurchaseOrderDetailOriginalData.OrderQty { get { return OriginalData.OrderQty; } }
		double IPurchaseOrderDetailOriginalData.UnitPrice { get { return OriginalData.UnitPrice; } }
		string IPurchaseOrderDetailOriginalData.LineTotal { get { return OriginalData.LineTotal; } }
		int IPurchaseOrderDetailOriginalData.ReceivedQty { get { return OriginalData.ReceivedQty; } }
		int IPurchaseOrderDetailOriginalData.RejectedQty { get { return OriginalData.RejectedQty; } }
		int IPurchaseOrderDetailOriginalData.StockedQty { get { return OriginalData.StockedQty; } }
		Product IPurchaseOrderDetailOriginalData.Product { get { return ((ILookupHelper<Product>)OriginalData.Product).GetOriginalItem(null); } }
		PurchaseOrderHeader IPurchaseOrderDetailOriginalData.PurchaseOrderHeader { get { return ((ILookupHelper<PurchaseOrderHeader>)OriginalData.PurchaseOrderHeader).GetOriginalItem(null); } }

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