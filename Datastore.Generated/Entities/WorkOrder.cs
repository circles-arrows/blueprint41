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
	public interface IWorkOrderOriginalData : ISchemaBaseOriginalData
    {
		int OrderQty { get; }
		int StockedQty { get; }
		int ScrappedQty { get; }
		System.DateTime StartDate { get; }
		System.DateTime? EndDate { get; }
		System.DateTime DueDate { get; }
		ScrapReason ScrapReason { get; }
		Product Product { get; }
    }

	public partial class WorkOrder : OGM<WorkOrder, WorkOrder.WorkOrderData, System.String>, ISchemaBase, INeo4jBase, IWorkOrderOriginalData
	{
        #region Initialize

        static WorkOrder()
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

        public static Dictionary<System.String, WorkOrder> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.WorkOrderAlias, IWhereQuery> query)
        {
            q.WorkOrderAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.WorkOrder.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"WorkOrder => OrderQty : {this.OrderQty}, StockedQty : {this.StockedQty}, ScrappedQty : {this.ScrappedQty}, StartDate : {this.StartDate}, EndDate : {this.EndDate?.ToString() ?? "null"}, DueDate : {this.DueDate}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new WorkOrderData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.OrderQty == null)
				throw new PersistenceException(string.Format("Cannot save WorkOrder with key '{0}' because the OrderQty cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.StockedQty == null)
				throw new PersistenceException(string.Format("Cannot save WorkOrder with key '{0}' because the StockedQty cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ScrappedQty == null)
				throw new PersistenceException(string.Format("Cannot save WorkOrder with key '{0}' because the ScrappedQty cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.StartDate == null)
				throw new PersistenceException(string.Format("Cannot save WorkOrder with key '{0}' because the StartDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.DueDate == null)
				throw new PersistenceException(string.Format("Cannot save WorkOrder with key '{0}' because the DueDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save WorkOrder with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class WorkOrderData : Data<System.String>
		{
			public WorkOrderData()
            {

            }

            public WorkOrderData(WorkOrderData data)
            {
				OrderQty = data.OrderQty;
				StockedQty = data.StockedQty;
				ScrappedQty = data.ScrappedQty;
				StartDate = data.StartDate;
				EndDate = data.EndDate;
				DueDate = data.DueDate;
				ScrapReason = data.ScrapReason;
				Product = data.Product;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "WorkOrder";

				ScrapReason = new EntityCollection<ScrapReason>(Wrapper, Members.ScrapReason);
				Product = new EntityCollection<Product>(Wrapper, Members.Product);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("OrderQty",  Conversion<int, long>.Convert(OrderQty));
				dictionary.Add("StockedQty",  Conversion<int, long>.Convert(StockedQty));
				dictionary.Add("ScrappedQty",  Conversion<int, long>.Convert(ScrappedQty));
				dictionary.Add("StartDate",  Conversion<System.DateTime, long>.Convert(StartDate));
				dictionary.Add("EndDate",  Conversion<System.DateTime?, long?>.Convert(EndDate));
				dictionary.Add("DueDate",  Conversion<System.DateTime, long>.Convert(DueDate));
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("OrderQty", out value))
					OrderQty = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("StockedQty", out value))
					StockedQty = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("ScrappedQty", out value))
					ScrappedQty = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("StartDate", out value))
					StartDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("EndDate", out value))
					EndDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("DueDate", out value))
					DueDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IWorkOrder

			public int OrderQty { get; set; }
			public int StockedQty { get; set; }
			public int ScrappedQty { get; set; }
			public System.DateTime StartDate { get; set; }
			public System.DateTime? EndDate { get; set; }
			public System.DateTime DueDate { get; set; }
			public EntityCollection<ScrapReason> ScrapReason { get; private set; }
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

		#region Members for interface IWorkOrder

		public int OrderQty { get { LazyGet(); return InnerData.OrderQty; } set { if (LazySet(Members.OrderQty, InnerData.OrderQty, value)) InnerData.OrderQty = value; } }
		public int StockedQty { get { LazyGet(); return InnerData.StockedQty; } set { if (LazySet(Members.StockedQty, InnerData.StockedQty, value)) InnerData.StockedQty = value; } }
		public int ScrappedQty { get { LazyGet(); return InnerData.ScrappedQty; } set { if (LazySet(Members.ScrappedQty, InnerData.ScrappedQty, value)) InnerData.ScrappedQty = value; } }
		public System.DateTime StartDate { get { LazyGet(); return InnerData.StartDate; } set { if (LazySet(Members.StartDate, InnerData.StartDate, value)) InnerData.StartDate = value; } }
		public System.DateTime? EndDate { get { LazyGet(); return InnerData.EndDate; } set { if (LazySet(Members.EndDate, InnerData.EndDate, value)) InnerData.EndDate = value; } }
		public System.DateTime DueDate { get { LazyGet(); return InnerData.DueDate; } set { if (LazySet(Members.DueDate, InnerData.DueDate, value)) InnerData.DueDate = value; } }
		public ScrapReason ScrapReason
		{
			get { return ((ILookupHelper<ScrapReason>)InnerData.ScrapReason).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.ScrapReason, ((ILookupHelper<ScrapReason>)InnerData.ScrapReason).GetItem(null), value))
					((ILookupHelper<ScrapReason>)InnerData.ScrapReason).SetItem(value, null); 
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

        private static WorkOrderMembers members = null;
        public static WorkOrderMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(WorkOrder))
                    {
                        if (members == null)
                            members = new WorkOrderMembers();
                    }
                }
                return members;
            }
        }
        public class WorkOrderMembers
        {
            internal WorkOrderMembers() { }

			#region Members for interface IWorkOrder

            public Property OrderQty { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["OrderQty"];
            public Property StockedQty { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["StockedQty"];
            public Property ScrappedQty { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["ScrappedQty"];
            public Property StartDate { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["StartDate"];
            public Property EndDate { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["EndDate"];
            public Property DueDate { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["DueDate"];
            public Property ScrapReason { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["ScrapReason"];
            public Property Product { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["Product"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static WorkOrderFullTextMembers fullTextMembers = null;
        public static WorkOrderFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(WorkOrder))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new WorkOrderFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class WorkOrderFullTextMembers
        {
            internal WorkOrderFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(WorkOrder))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["WorkOrder"];
                }
            }
            return entity;
        }

		private static WorkOrderEvents events = null;
        public static WorkOrderEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(WorkOrder))
                    {
                        if (events == null)
                            events = new WorkOrderEvents();
                    }
                }
                return events;
            }
        }
        public class WorkOrderEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<WorkOrder, EntityEventArgs> onNew;
            public event EventHandler<WorkOrder, EntityEventArgs> OnNew
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
                EventHandler<WorkOrder, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((WorkOrder)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<WorkOrder, EntityEventArgs> onDelete;
            public event EventHandler<WorkOrder, EntityEventArgs> OnDelete
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
                EventHandler<WorkOrder, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((WorkOrder)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<WorkOrder, EntityEventArgs> onSave;
            public event EventHandler<WorkOrder, EntityEventArgs> OnSave
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
                EventHandler<WorkOrder, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((WorkOrder)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnOrderQty

				private static bool onOrderQtyIsRegistered = false;

				private static EventHandler<WorkOrder, PropertyEventArgs> onOrderQty;
				public static event EventHandler<WorkOrder, PropertyEventArgs> OnOrderQty
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
					EventHandler<WorkOrder, PropertyEventArgs> handler = onOrderQty;
					if ((object)handler != null)
						handler.Invoke((WorkOrder)sender, args);
				}

				#endregion

				#region OnStockedQty

				private static bool onStockedQtyIsRegistered = false;

				private static EventHandler<WorkOrder, PropertyEventArgs> onStockedQty;
				public static event EventHandler<WorkOrder, PropertyEventArgs> OnStockedQty
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
					EventHandler<WorkOrder, PropertyEventArgs> handler = onStockedQty;
					if ((object)handler != null)
						handler.Invoke((WorkOrder)sender, args);
				}

				#endregion

				#region OnScrappedQty

				private static bool onScrappedQtyIsRegistered = false;

				private static EventHandler<WorkOrder, PropertyEventArgs> onScrappedQty;
				public static event EventHandler<WorkOrder, PropertyEventArgs> OnScrappedQty
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onScrappedQtyIsRegistered)
							{
								Members.ScrappedQty.Events.OnChange -= onScrappedQtyProxy;
								Members.ScrappedQty.Events.OnChange += onScrappedQtyProxy;
								onScrappedQtyIsRegistered = true;
							}
							onScrappedQty += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onScrappedQty -= value;
							if (onScrappedQty == null && onScrappedQtyIsRegistered)
							{
								Members.ScrappedQty.Events.OnChange -= onScrappedQtyProxy;
								onScrappedQtyIsRegistered = false;
							}
						}
					}
				}
            
				private static void onScrappedQtyProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<WorkOrder, PropertyEventArgs> handler = onScrappedQty;
					if ((object)handler != null)
						handler.Invoke((WorkOrder)sender, args);
				}

				#endregion

				#region OnStartDate

				private static bool onStartDateIsRegistered = false;

				private static EventHandler<WorkOrder, PropertyEventArgs> onStartDate;
				public static event EventHandler<WorkOrder, PropertyEventArgs> OnStartDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onStartDateIsRegistered)
							{
								Members.StartDate.Events.OnChange -= onStartDateProxy;
								Members.StartDate.Events.OnChange += onStartDateProxy;
								onStartDateIsRegistered = true;
							}
							onStartDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onStartDate -= value;
							if (onStartDate == null && onStartDateIsRegistered)
							{
								Members.StartDate.Events.OnChange -= onStartDateProxy;
								onStartDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onStartDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<WorkOrder, PropertyEventArgs> handler = onStartDate;
					if ((object)handler != null)
						handler.Invoke((WorkOrder)sender, args);
				}

				#endregion

				#region OnEndDate

				private static bool onEndDateIsRegistered = false;

				private static EventHandler<WorkOrder, PropertyEventArgs> onEndDate;
				public static event EventHandler<WorkOrder, PropertyEventArgs> OnEndDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onEndDateIsRegistered)
							{
								Members.EndDate.Events.OnChange -= onEndDateProxy;
								Members.EndDate.Events.OnChange += onEndDateProxy;
								onEndDateIsRegistered = true;
							}
							onEndDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onEndDate -= value;
							if (onEndDate == null && onEndDateIsRegistered)
							{
								Members.EndDate.Events.OnChange -= onEndDateProxy;
								onEndDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onEndDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<WorkOrder, PropertyEventArgs> handler = onEndDate;
					if ((object)handler != null)
						handler.Invoke((WorkOrder)sender, args);
				}

				#endregion

				#region OnDueDate

				private static bool onDueDateIsRegistered = false;

				private static EventHandler<WorkOrder, PropertyEventArgs> onDueDate;
				public static event EventHandler<WorkOrder, PropertyEventArgs> OnDueDate
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
					EventHandler<WorkOrder, PropertyEventArgs> handler = onDueDate;
					if ((object)handler != null)
						handler.Invoke((WorkOrder)sender, args);
				}

				#endregion

				#region OnScrapReason

				private static bool onScrapReasonIsRegistered = false;

				private static EventHandler<WorkOrder, PropertyEventArgs> onScrapReason;
				public static event EventHandler<WorkOrder, PropertyEventArgs> OnScrapReason
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onScrapReasonIsRegistered)
							{
								Members.ScrapReason.Events.OnChange -= onScrapReasonProxy;
								Members.ScrapReason.Events.OnChange += onScrapReasonProxy;
								onScrapReasonIsRegistered = true;
							}
							onScrapReason += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onScrapReason -= value;
							if (onScrapReason == null && onScrapReasonIsRegistered)
							{
								Members.ScrapReason.Events.OnChange -= onScrapReasonProxy;
								onScrapReasonIsRegistered = false;
							}
						}
					}
				}
            
				private static void onScrapReasonProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<WorkOrder, PropertyEventArgs> handler = onScrapReason;
					if ((object)handler != null)
						handler.Invoke((WorkOrder)sender, args);
				}

				#endregion

				#region OnProduct

				private static bool onProductIsRegistered = false;

				private static EventHandler<WorkOrder, PropertyEventArgs> onProduct;
				public static event EventHandler<WorkOrder, PropertyEventArgs> OnProduct
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
					EventHandler<WorkOrder, PropertyEventArgs> handler = onProduct;
					if ((object)handler != null)
						handler.Invoke((WorkOrder)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<WorkOrder, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<WorkOrder, PropertyEventArgs> OnModifiedDate
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
					EventHandler<WorkOrder, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((WorkOrder)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<WorkOrder, PropertyEventArgs> onUid;
				public static event EventHandler<WorkOrder, PropertyEventArgs> OnUid
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
					EventHandler<WorkOrder, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((WorkOrder)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IWorkOrderOriginalData

		public IWorkOrderOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IWorkOrder

		int IWorkOrderOriginalData.OrderQty { get { return OriginalData.OrderQty; } }
		int IWorkOrderOriginalData.StockedQty { get { return OriginalData.StockedQty; } }
		int IWorkOrderOriginalData.ScrappedQty { get { return OriginalData.ScrappedQty; } }
		System.DateTime IWorkOrderOriginalData.StartDate { get { return OriginalData.StartDate; } }
		System.DateTime? IWorkOrderOriginalData.EndDate { get { return OriginalData.EndDate; } }
		System.DateTime IWorkOrderOriginalData.DueDate { get { return OriginalData.DueDate; } }
		ScrapReason IWorkOrderOriginalData.ScrapReason { get { return ((ILookupHelper<ScrapReason>)OriginalData.ScrapReason).GetOriginalItem(null); } }
		Product IWorkOrderOriginalData.Product { get { return ((ILookupHelper<Product>)OriginalData.Product).GetOriginalItem(null); } }

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