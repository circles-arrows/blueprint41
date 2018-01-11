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
	public interface ITransactionHistoryArchiveOriginalData
    {
		#region Outer Data

		#region Members for interface ITransactionHistoryArchive

		int ReferenceOrderID { get; }
		System.DateTime TransactionDate { get; }
		string TransactionType { get; }
		int Quantity { get; }
		decimal ActualCost { get; }
		int ReferenceOrderLineID { get; }
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

	public partial class TransactionHistoryArchive : OGM<TransactionHistoryArchive, TransactionHistoryArchive.TransactionHistoryArchiveData, System.String>, ISchemaBase, INeo4jBase, ITransactionHistoryArchiveOriginalData
	{
        #region Initialize

        static TransactionHistoryArchive()
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

        public static Dictionary<System.String, TransactionHistoryArchive> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.TransactionHistoryArchiveAlias, IWhereQuery> query)
        {
            q.TransactionHistoryArchiveAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.TransactionHistoryArchive.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"TransactionHistoryArchive => ReferenceOrderID : {this.ReferenceOrderID}, TransactionDate : {this.TransactionDate}, TransactionType : {this.TransactionType}, Quantity : {this.Quantity}, ActualCost : {this.ActualCost}, ReferenceOrderLineID : {this.ReferenceOrderLineID}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new TransactionHistoryArchiveData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.ReferenceOrderID == null)
				throw new PersistenceException(string.Format("Cannot save TransactionHistoryArchive with key '{0}' because the ReferenceOrderID cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.TransactionDate == null)
				throw new PersistenceException(string.Format("Cannot save TransactionHistoryArchive with key '{0}' because the TransactionDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.TransactionType == null)
				throw new PersistenceException(string.Format("Cannot save TransactionHistoryArchive with key '{0}' because the TransactionType cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Quantity == null)
				throw new PersistenceException(string.Format("Cannot save TransactionHistoryArchive with key '{0}' because the Quantity cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ActualCost == null)
				throw new PersistenceException(string.Format("Cannot save TransactionHistoryArchive with key '{0}' because the ActualCost cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ReferenceOrderLineID == null)
				throw new PersistenceException(string.Format("Cannot save TransactionHistoryArchive with key '{0}' because the ReferenceOrderLineID cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save TransactionHistoryArchive with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class TransactionHistoryArchiveData : Data<System.String>
		{
			public TransactionHistoryArchiveData()
            {

            }

            public TransactionHistoryArchiveData(TransactionHistoryArchiveData data)
            {
				ReferenceOrderID = data.ReferenceOrderID;
				TransactionDate = data.TransactionDate;
				TransactionType = data.TransactionType;
				Quantity = data.Quantity;
				ActualCost = data.ActualCost;
				ReferenceOrderLineID = data.ReferenceOrderLineID;
				Product = data.Product;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "TransactionHistoryArchive";

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
				dictionary.Add("ReferenceOrderID",  Conversion<int, long>.Convert(ReferenceOrderID));
				dictionary.Add("TransactionDate",  Conversion<System.DateTime, long>.Convert(TransactionDate));
				dictionary.Add("TransactionType",  TransactionType);
				dictionary.Add("Quantity",  Conversion<int, long>.Convert(Quantity));
				dictionary.Add("ActualCost",  Conversion<decimal, long>.Convert(ActualCost));
				dictionary.Add("ReferenceOrderLineID",  Conversion<int, long>.Convert(ReferenceOrderLineID));
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("ReferenceOrderID", out value))
					ReferenceOrderID = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("TransactionDate", out value))
					TransactionDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("TransactionType", out value))
					TransactionType = (string)value;
				if (properties.TryGetValue("Quantity", out value))
					Quantity = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("ActualCost", out value))
					ActualCost = Conversion<long, decimal>.Convert((long)value);
				if (properties.TryGetValue("ReferenceOrderLineID", out value))
					ReferenceOrderLineID = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface ITransactionHistoryArchive

			public int ReferenceOrderID { get; set; }
			public System.DateTime TransactionDate { get; set; }
			public string TransactionType { get; set; }
			public int Quantity { get; set; }
			public decimal ActualCost { get; set; }
			public int ReferenceOrderLineID { get; set; }
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

		#region Members for interface ITransactionHistoryArchive

		public int ReferenceOrderID { get { LazyGet(); return InnerData.ReferenceOrderID; } set { if (LazySet(Members.ReferenceOrderID, InnerData.ReferenceOrderID, value)) InnerData.ReferenceOrderID = value; } }
		public System.DateTime TransactionDate { get { LazyGet(); return InnerData.TransactionDate; } set { if (LazySet(Members.TransactionDate, InnerData.TransactionDate, value)) InnerData.TransactionDate = value; } }
		public string TransactionType { get { LazyGet(); return InnerData.TransactionType; } set { if (LazySet(Members.TransactionType, InnerData.TransactionType, value)) InnerData.TransactionType = value; } }
		public int Quantity { get { LazyGet(); return InnerData.Quantity; } set { if (LazySet(Members.Quantity, InnerData.Quantity, value)) InnerData.Quantity = value; } }
		public decimal ActualCost { get { LazyGet(); return InnerData.ActualCost; } set { if (LazySet(Members.ActualCost, InnerData.ActualCost, value)) InnerData.ActualCost = value; } }
		public int ReferenceOrderLineID { get { LazyGet(); return InnerData.ReferenceOrderLineID; } set { if (LazySet(Members.ReferenceOrderLineID, InnerData.ReferenceOrderLineID, value)) InnerData.ReferenceOrderLineID = value; } }
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

        private static TransactionHistoryArchiveMembers members = null;
        public static TransactionHistoryArchiveMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(TransactionHistoryArchive))
                    {
                        if (members == null)
                            members = new TransactionHistoryArchiveMembers();
                    }
                }
                return members;
            }
        }
        public class TransactionHistoryArchiveMembers
        {
            internal TransactionHistoryArchiveMembers() { }

			#region Members for interface ITransactionHistoryArchive

            public Property ReferenceOrderID { get; } = Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["ReferenceOrderID"];
            public Property TransactionDate { get; } = Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["TransactionDate"];
            public Property TransactionType { get; } = Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["TransactionType"];
            public Property Quantity { get; } = Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["Quantity"];
            public Property ActualCost { get; } = Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["ActualCost"];
            public Property ReferenceOrderLineID { get; } = Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["ReferenceOrderLineID"];
            public Property Product { get; } = Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["Product"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static TransactionHistoryArchiveFullTextMembers fullTextMembers = null;
        public static TransactionHistoryArchiveFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(TransactionHistoryArchive))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new TransactionHistoryArchiveFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class TransactionHistoryArchiveFullTextMembers
        {
            internal TransactionHistoryArchiveFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(TransactionHistoryArchive))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"];
                }
            }
            return entity;
        }

		private static TransactionHistoryArchiveEvents events = null;
        public static TransactionHistoryArchiveEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(TransactionHistoryArchive))
                    {
                        if (events == null)
                            events = new TransactionHistoryArchiveEvents();
                    }
                }
                return events;
            }
        }
        public class TransactionHistoryArchiveEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<TransactionHistoryArchive, EntityEventArgs> onNew;
            public event EventHandler<TransactionHistoryArchive, EntityEventArgs> OnNew
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
                EventHandler<TransactionHistoryArchive, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((TransactionHistoryArchive)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<TransactionHistoryArchive, EntityEventArgs> onDelete;
            public event EventHandler<TransactionHistoryArchive, EntityEventArgs> OnDelete
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
                EventHandler<TransactionHistoryArchive, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((TransactionHistoryArchive)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<TransactionHistoryArchive, EntityEventArgs> onSave;
            public event EventHandler<TransactionHistoryArchive, EntityEventArgs> OnSave
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
                EventHandler<TransactionHistoryArchive, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((TransactionHistoryArchive)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnReferenceOrderID

				private static bool onReferenceOrderIDIsRegistered = false;

				private static EventHandler<TransactionHistoryArchive, PropertyEventArgs> onReferenceOrderID;
				public static event EventHandler<TransactionHistoryArchive, PropertyEventArgs> OnReferenceOrderID
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onReferenceOrderIDIsRegistered)
							{
								Members.ReferenceOrderID.Events.OnChange -= onReferenceOrderIDProxy;
								Members.ReferenceOrderID.Events.OnChange += onReferenceOrderIDProxy;
								onReferenceOrderIDIsRegistered = true;
							}
							onReferenceOrderID += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onReferenceOrderID -= value;
							if (onReferenceOrderID == null && onReferenceOrderIDIsRegistered)
							{
								Members.ReferenceOrderID.Events.OnChange -= onReferenceOrderIDProxy;
								onReferenceOrderIDIsRegistered = false;
							}
						}
					}
				}
            
				private static void onReferenceOrderIDProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<TransactionHistoryArchive, PropertyEventArgs> handler = onReferenceOrderID;
					if ((object)handler != null)
						handler.Invoke((TransactionHistoryArchive)sender, args);
				}

				#endregion

				#region OnTransactionDate

				private static bool onTransactionDateIsRegistered = false;

				private static EventHandler<TransactionHistoryArchive, PropertyEventArgs> onTransactionDate;
				public static event EventHandler<TransactionHistoryArchive, PropertyEventArgs> OnTransactionDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onTransactionDateIsRegistered)
							{
								Members.TransactionDate.Events.OnChange -= onTransactionDateProxy;
								Members.TransactionDate.Events.OnChange += onTransactionDateProxy;
								onTransactionDateIsRegistered = true;
							}
							onTransactionDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onTransactionDate -= value;
							if (onTransactionDate == null && onTransactionDateIsRegistered)
							{
								Members.TransactionDate.Events.OnChange -= onTransactionDateProxy;
								onTransactionDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onTransactionDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<TransactionHistoryArchive, PropertyEventArgs> handler = onTransactionDate;
					if ((object)handler != null)
						handler.Invoke((TransactionHistoryArchive)sender, args);
				}

				#endregion

				#region OnTransactionType

				private static bool onTransactionTypeIsRegistered = false;

				private static EventHandler<TransactionHistoryArchive, PropertyEventArgs> onTransactionType;
				public static event EventHandler<TransactionHistoryArchive, PropertyEventArgs> OnTransactionType
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onTransactionTypeIsRegistered)
							{
								Members.TransactionType.Events.OnChange -= onTransactionTypeProxy;
								Members.TransactionType.Events.OnChange += onTransactionTypeProxy;
								onTransactionTypeIsRegistered = true;
							}
							onTransactionType += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onTransactionType -= value;
							if (onTransactionType == null && onTransactionTypeIsRegistered)
							{
								Members.TransactionType.Events.OnChange -= onTransactionTypeProxy;
								onTransactionTypeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onTransactionTypeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<TransactionHistoryArchive, PropertyEventArgs> handler = onTransactionType;
					if ((object)handler != null)
						handler.Invoke((TransactionHistoryArchive)sender, args);
				}

				#endregion

				#region OnQuantity

				private static bool onQuantityIsRegistered = false;

				private static EventHandler<TransactionHistoryArchive, PropertyEventArgs> onQuantity;
				public static event EventHandler<TransactionHistoryArchive, PropertyEventArgs> OnQuantity
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
					EventHandler<TransactionHistoryArchive, PropertyEventArgs> handler = onQuantity;
					if ((object)handler != null)
						handler.Invoke((TransactionHistoryArchive)sender, args);
				}

				#endregion

				#region OnActualCost

				private static bool onActualCostIsRegistered = false;

				private static EventHandler<TransactionHistoryArchive, PropertyEventArgs> onActualCost;
				public static event EventHandler<TransactionHistoryArchive, PropertyEventArgs> OnActualCost
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onActualCostIsRegistered)
							{
								Members.ActualCost.Events.OnChange -= onActualCostProxy;
								Members.ActualCost.Events.OnChange += onActualCostProxy;
								onActualCostIsRegistered = true;
							}
							onActualCost += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onActualCost -= value;
							if (onActualCost == null && onActualCostIsRegistered)
							{
								Members.ActualCost.Events.OnChange -= onActualCostProxy;
								onActualCostIsRegistered = false;
							}
						}
					}
				}
            
				private static void onActualCostProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<TransactionHistoryArchive, PropertyEventArgs> handler = onActualCost;
					if ((object)handler != null)
						handler.Invoke((TransactionHistoryArchive)sender, args);
				}

				#endregion

				#region OnReferenceOrderLineID

				private static bool onReferenceOrderLineIDIsRegistered = false;

				private static EventHandler<TransactionHistoryArchive, PropertyEventArgs> onReferenceOrderLineID;
				public static event EventHandler<TransactionHistoryArchive, PropertyEventArgs> OnReferenceOrderLineID
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onReferenceOrderLineIDIsRegistered)
							{
								Members.ReferenceOrderLineID.Events.OnChange -= onReferenceOrderLineIDProxy;
								Members.ReferenceOrderLineID.Events.OnChange += onReferenceOrderLineIDProxy;
								onReferenceOrderLineIDIsRegistered = true;
							}
							onReferenceOrderLineID += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onReferenceOrderLineID -= value;
							if (onReferenceOrderLineID == null && onReferenceOrderLineIDIsRegistered)
							{
								Members.ReferenceOrderLineID.Events.OnChange -= onReferenceOrderLineIDProxy;
								onReferenceOrderLineIDIsRegistered = false;
							}
						}
					}
				}
            
				private static void onReferenceOrderLineIDProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<TransactionHistoryArchive, PropertyEventArgs> handler = onReferenceOrderLineID;
					if ((object)handler != null)
						handler.Invoke((TransactionHistoryArchive)sender, args);
				}

				#endregion

				#region OnProduct

				private static bool onProductIsRegistered = false;

				private static EventHandler<TransactionHistoryArchive, PropertyEventArgs> onProduct;
				public static event EventHandler<TransactionHistoryArchive, PropertyEventArgs> OnProduct
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
					EventHandler<TransactionHistoryArchive, PropertyEventArgs> handler = onProduct;
					if ((object)handler != null)
						handler.Invoke((TransactionHistoryArchive)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<TransactionHistoryArchive, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<TransactionHistoryArchive, PropertyEventArgs> OnModifiedDate
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
					EventHandler<TransactionHistoryArchive, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((TransactionHistoryArchive)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<TransactionHistoryArchive, PropertyEventArgs> onUid;
				public static event EventHandler<TransactionHistoryArchive, PropertyEventArgs> OnUid
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
					EventHandler<TransactionHistoryArchive, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((TransactionHistoryArchive)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region ITransactionHistoryArchiveOriginalData

		public ITransactionHistoryArchiveOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ITransactionHistoryArchive

		int ITransactionHistoryArchiveOriginalData.ReferenceOrderID { get { return OriginalData.ReferenceOrderID; } }
		System.DateTime ITransactionHistoryArchiveOriginalData.TransactionDate { get { return OriginalData.TransactionDate; } }
		string ITransactionHistoryArchiveOriginalData.TransactionType { get { return OriginalData.TransactionType; } }
		int ITransactionHistoryArchiveOriginalData.Quantity { get { return OriginalData.Quantity; } }
		decimal ITransactionHistoryArchiveOriginalData.ActualCost { get { return OriginalData.ActualCost; } }
		int ITransactionHistoryArchiveOriginalData.ReferenceOrderLineID { get { return OriginalData.ReferenceOrderLineID; } }
		Product ITransactionHistoryArchiveOriginalData.Product { get { return ((ILookupHelper<Product>)OriginalData.Product).GetOriginalItem(null); } }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ITransactionHistoryArchiveOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		string ITransactionHistoryArchiveOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}