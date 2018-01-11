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
	public interface IShoppingCartItemOriginalData
    {
		#region Outer Data

		#region Members for interface IShoppingCartItem

		int Quantity { get; }
		System.DateTime DateCreated { get; }
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

	public partial class ShoppingCartItem : OGM<ShoppingCartItem, ShoppingCartItem.ShoppingCartItemData, System.String>, ISchemaBase, INeo4jBase, IShoppingCartItemOriginalData
	{
        #region Initialize

        static ShoppingCartItem()
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

        public static Dictionary<System.String, ShoppingCartItem> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.ShoppingCartItemAlias, IWhereQuery> query)
        {
            q.ShoppingCartItemAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.ShoppingCartItem.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"ShoppingCartItem => Quantity : {this.Quantity}, DateCreated : {this.DateCreated}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new ShoppingCartItemData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.Quantity == null)
				throw new PersistenceException(string.Format("Cannot save ShoppingCartItem with key '{0}' because the Quantity cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.DateCreated == null)
				throw new PersistenceException(string.Format("Cannot save ShoppingCartItem with key '{0}' because the DateCreated cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save ShoppingCartItem with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class ShoppingCartItemData : Data<System.String>
		{
			public ShoppingCartItemData()
            {

            }

            public ShoppingCartItemData(ShoppingCartItemData data)
            {
				Quantity = data.Quantity;
				DateCreated = data.DateCreated;
				Product = data.Product;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "ShoppingCartItem";

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
				dictionary.Add("Quantity",  Conversion<int, long>.Convert(Quantity));
				dictionary.Add("DateCreated",  Conversion<System.DateTime, long>.Convert(DateCreated));
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Quantity", out value))
					Quantity = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("DateCreated", out value))
					DateCreated = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IShoppingCartItem

			public int Quantity { get; set; }
			public System.DateTime DateCreated { get; set; }
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

		#region Members for interface IShoppingCartItem

		public int Quantity { get { LazyGet(); return InnerData.Quantity; } set { if (LazySet(Members.Quantity, InnerData.Quantity, value)) InnerData.Quantity = value; } }
		public System.DateTime DateCreated { get { LazyGet(); return InnerData.DateCreated; } set { if (LazySet(Members.DateCreated, InnerData.DateCreated, value)) InnerData.DateCreated = value; } }
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

        private static ShoppingCartItemMembers members = null;
        public static ShoppingCartItemMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(ShoppingCartItem))
                    {
                        if (members == null)
                            members = new ShoppingCartItemMembers();
                    }
                }
                return members;
            }
        }
        public class ShoppingCartItemMembers
        {
            internal ShoppingCartItemMembers() { }

			#region Members for interface IShoppingCartItem

            public Property Quantity { get; } = Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"].Properties["Quantity"];
            public Property DateCreated { get; } = Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"].Properties["DateCreated"];
            public Property Product { get; } = Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"].Properties["Product"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static ShoppingCartItemFullTextMembers fullTextMembers = null;
        public static ShoppingCartItemFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(ShoppingCartItem))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new ShoppingCartItemFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class ShoppingCartItemFullTextMembers
        {
            internal ShoppingCartItemFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(ShoppingCartItem))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"];
                }
            }
            return entity;
        }

		private static ShoppingCartItemEvents events = null;
        public static ShoppingCartItemEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(ShoppingCartItem))
                    {
                        if (events == null)
                            events = new ShoppingCartItemEvents();
                    }
                }
                return events;
            }
        }
        public class ShoppingCartItemEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<ShoppingCartItem, EntityEventArgs> onNew;
            public event EventHandler<ShoppingCartItem, EntityEventArgs> OnNew
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
                EventHandler<ShoppingCartItem, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((ShoppingCartItem)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<ShoppingCartItem, EntityEventArgs> onDelete;
            public event EventHandler<ShoppingCartItem, EntityEventArgs> OnDelete
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
                EventHandler<ShoppingCartItem, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((ShoppingCartItem)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<ShoppingCartItem, EntityEventArgs> onSave;
            public event EventHandler<ShoppingCartItem, EntityEventArgs> OnSave
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
                EventHandler<ShoppingCartItem, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((ShoppingCartItem)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnQuantity

				private static bool onQuantityIsRegistered = false;

				private static EventHandler<ShoppingCartItem, PropertyEventArgs> onQuantity;
				public static event EventHandler<ShoppingCartItem, PropertyEventArgs> OnQuantity
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
					EventHandler<ShoppingCartItem, PropertyEventArgs> handler = onQuantity;
					if ((object)handler != null)
						handler.Invoke((ShoppingCartItem)sender, args);
				}

				#endregion

				#region OnDateCreated

				private static bool onDateCreatedIsRegistered = false;

				private static EventHandler<ShoppingCartItem, PropertyEventArgs> onDateCreated;
				public static event EventHandler<ShoppingCartItem, PropertyEventArgs> OnDateCreated
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDateCreatedIsRegistered)
							{
								Members.DateCreated.Events.OnChange -= onDateCreatedProxy;
								Members.DateCreated.Events.OnChange += onDateCreatedProxy;
								onDateCreatedIsRegistered = true;
							}
							onDateCreated += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDateCreated -= value;
							if (onDateCreated == null && onDateCreatedIsRegistered)
							{
								Members.DateCreated.Events.OnChange -= onDateCreatedProxy;
								onDateCreatedIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDateCreatedProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ShoppingCartItem, PropertyEventArgs> handler = onDateCreated;
					if ((object)handler != null)
						handler.Invoke((ShoppingCartItem)sender, args);
				}

				#endregion

				#region OnProduct

				private static bool onProductIsRegistered = false;

				private static EventHandler<ShoppingCartItem, PropertyEventArgs> onProduct;
				public static event EventHandler<ShoppingCartItem, PropertyEventArgs> OnProduct
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
					EventHandler<ShoppingCartItem, PropertyEventArgs> handler = onProduct;
					if ((object)handler != null)
						handler.Invoke((ShoppingCartItem)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<ShoppingCartItem, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<ShoppingCartItem, PropertyEventArgs> OnModifiedDate
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
					EventHandler<ShoppingCartItem, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((ShoppingCartItem)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<ShoppingCartItem, PropertyEventArgs> onUid;
				public static event EventHandler<ShoppingCartItem, PropertyEventArgs> OnUid
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
					EventHandler<ShoppingCartItem, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((ShoppingCartItem)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IShoppingCartItemOriginalData

		public IShoppingCartItemOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IShoppingCartItem

		int IShoppingCartItemOriginalData.Quantity { get { return OriginalData.Quantity; } }
		System.DateTime IShoppingCartItemOriginalData.DateCreated { get { return OriginalData.DateCreated; } }
		Product IShoppingCartItemOriginalData.Product { get { return ((ILookupHelper<Product>)OriginalData.Product).GetOriginalItem(null); } }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime IShoppingCartItemOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		string IShoppingCartItemOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}