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
	public interface ICustomerOriginalData : ISchemaBaseOriginalData
    {
		string AccountNumber { get; }
		string rowguid { get; }
		Store Store { get; }
		SalesTerritory SalesTerritory { get; }
		Person Person { get; }
    }

	public partial class Customer : OGM<Customer, Customer.CustomerData, System.String>, ISchemaBase, INeo4jBase, ICustomerOriginalData
	{
        #region Initialize

        static Customer()
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

        public static Dictionary<System.String, Customer> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.CustomerAlias, IWhereQuery> query)
        {
            q.CustomerAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Customer.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"Customer => AccountNumber : {this.AccountNumber}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new CustomerData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.AccountNumber == null)
				throw new PersistenceException(string.Format("Cannot save Customer with key '{0}' because the AccountNumber cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid == null)
				throw new PersistenceException(string.Format("Cannot save Customer with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save Customer with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class CustomerData : Data<System.String>
		{
			public CustomerData()
            {

            }

            public CustomerData(CustomerData data)
            {
				AccountNumber = data.AccountNumber;
				rowguid = data.rowguid;
				Store = data.Store;
				SalesTerritory = data.SalesTerritory;
				Person = data.Person;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Customer";

				Store = new EntityCollection<Store>(Wrapper, Members.Store);
				SalesTerritory = new EntityCollection<SalesTerritory>(Wrapper, Members.SalesTerritory);
				Person = new EntityCollection<Person>(Wrapper, Members.Person);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("AccountNumber",  AccountNumber);
				dictionary.Add("rowguid",  rowguid);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("AccountNumber", out value))
					AccountNumber = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface ICustomer

			public string AccountNumber { get; set; }
			public string rowguid { get; set; }
			public EntityCollection<Store> Store { get; private set; }
			public EntityCollection<SalesTerritory> SalesTerritory { get; private set; }
			public EntityCollection<Person> Person { get; private set; }

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

		#region Members for interface ICustomer

		public string AccountNumber { get { LazyGet(); return InnerData.AccountNumber; } set { if (LazySet(Members.AccountNumber, InnerData.AccountNumber, value)) InnerData.AccountNumber = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }
		public Store Store
		{
			get { return ((ILookupHelper<Store>)InnerData.Store).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Store, ((ILookupHelper<Store>)InnerData.Store).GetItem(null), value))
					((ILookupHelper<Store>)InnerData.Store).SetItem(value, null); 
			}
		}
		public SalesTerritory SalesTerritory
		{
			get { return ((ILookupHelper<SalesTerritory>)InnerData.SalesTerritory).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.SalesTerritory, ((ILookupHelper<SalesTerritory>)InnerData.SalesTerritory).GetItem(null), value))
					((ILookupHelper<SalesTerritory>)InnerData.SalesTerritory).SetItem(value, null); 
			}
		}
		public Person Person
		{
			get { return ((ILookupHelper<Person>)InnerData.Person).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Person, ((ILookupHelper<Person>)InnerData.Person).GetItem(null), value))
					((ILookupHelper<Person>)InnerData.Person).SetItem(value, null); 
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

        private static CustomerMembers members = null;
        public static CustomerMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(Customer))
                    {
                        if (members == null)
                            members = new CustomerMembers();
                    }
                }
                return members;
            }
        }
        public class CustomerMembers
        {
            internal CustomerMembers() { }

			#region Members for interface ICustomer

            public Property AccountNumber { get; } = Datastore.AdventureWorks.Model.Entities["Customer"].Properties["AccountNumber"];
            public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["Customer"].Properties["rowguid"];
            public Property Store { get; } = Datastore.AdventureWorks.Model.Entities["Customer"].Properties["Store"];
            public Property SalesTerritory { get; } = Datastore.AdventureWorks.Model.Entities["Customer"].Properties["SalesTerritory"];
            public Property Person { get; } = Datastore.AdventureWorks.Model.Entities["Customer"].Properties["Person"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static CustomerFullTextMembers fullTextMembers = null;
        public static CustomerFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(Customer))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new CustomerFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class CustomerFullTextMembers
        {
            internal CustomerFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(Customer))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["Customer"];
                }
            }
            return entity;
        }

		private static CustomerEvents events = null;
        public static CustomerEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(Customer))
                    {
                        if (events == null)
                            events = new CustomerEvents();
                    }
                }
                return events;
            }
        }
        public class CustomerEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Customer, EntityEventArgs> onNew;
            public event EventHandler<Customer, EntityEventArgs> OnNew
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
                EventHandler<Customer, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((Customer)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<Customer, EntityEventArgs> onDelete;
            public event EventHandler<Customer, EntityEventArgs> OnDelete
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
                EventHandler<Customer, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((Customer)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<Customer, EntityEventArgs> onSave;
            public event EventHandler<Customer, EntityEventArgs> OnSave
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
                EventHandler<Customer, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((Customer)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnAccountNumber

				private static bool onAccountNumberIsRegistered = false;

				private static EventHandler<Customer, PropertyEventArgs> onAccountNumber;
				public static event EventHandler<Customer, PropertyEventArgs> OnAccountNumber
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onAccountNumberIsRegistered)
							{
								Members.AccountNumber.Events.OnChange -= onAccountNumberProxy;
								Members.AccountNumber.Events.OnChange += onAccountNumberProxy;
								onAccountNumberIsRegistered = true;
							}
							onAccountNumber += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onAccountNumber -= value;
							if (onAccountNumber == null && onAccountNumberIsRegistered)
							{
								Members.AccountNumber.Events.OnChange -= onAccountNumberProxy;
								onAccountNumberIsRegistered = false;
							}
						}
					}
				}
            
				private static void onAccountNumberProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Customer, PropertyEventArgs> handler = onAccountNumber;
					if ((object)handler != null)
						handler.Invoke((Customer)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<Customer, PropertyEventArgs> onrowguid;
				public static event EventHandler<Customer, PropertyEventArgs> Onrowguid
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
					EventHandler<Customer, PropertyEventArgs> handler = onrowguid;
					if ((object)handler != null)
						handler.Invoke((Customer)sender, args);
				}

				#endregion

				#region OnStore

				private static bool onStoreIsRegistered = false;

				private static EventHandler<Customer, PropertyEventArgs> onStore;
				public static event EventHandler<Customer, PropertyEventArgs> OnStore
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onStoreIsRegistered)
							{
								Members.Store.Events.OnChange -= onStoreProxy;
								Members.Store.Events.OnChange += onStoreProxy;
								onStoreIsRegistered = true;
							}
							onStore += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onStore -= value;
							if (onStore == null && onStoreIsRegistered)
							{
								Members.Store.Events.OnChange -= onStoreProxy;
								onStoreIsRegistered = false;
							}
						}
					}
				}
            
				private static void onStoreProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Customer, PropertyEventArgs> handler = onStore;
					if ((object)handler != null)
						handler.Invoke((Customer)sender, args);
				}

				#endregion

				#region OnSalesTerritory

				private static bool onSalesTerritoryIsRegistered = false;

				private static EventHandler<Customer, PropertyEventArgs> onSalesTerritory;
				public static event EventHandler<Customer, PropertyEventArgs> OnSalesTerritory
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalesTerritoryIsRegistered)
							{
								Members.SalesTerritory.Events.OnChange -= onSalesTerritoryProxy;
								Members.SalesTerritory.Events.OnChange += onSalesTerritoryProxy;
								onSalesTerritoryIsRegistered = true;
							}
							onSalesTerritory += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalesTerritory -= value;
							if (onSalesTerritory == null && onSalesTerritoryIsRegistered)
							{
								Members.SalesTerritory.Events.OnChange -= onSalesTerritoryProxy;
								onSalesTerritoryIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSalesTerritoryProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Customer, PropertyEventArgs> handler = onSalesTerritory;
					if ((object)handler != null)
						handler.Invoke((Customer)sender, args);
				}

				#endregion

				#region OnPerson

				private static bool onPersonIsRegistered = false;

				private static EventHandler<Customer, PropertyEventArgs> onPerson;
				public static event EventHandler<Customer, PropertyEventArgs> OnPerson
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPersonIsRegistered)
							{
								Members.Person.Events.OnChange -= onPersonProxy;
								Members.Person.Events.OnChange += onPersonProxy;
								onPersonIsRegistered = true;
							}
							onPerson += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPerson -= value;
							if (onPerson == null && onPersonIsRegistered)
							{
								Members.Person.Events.OnChange -= onPersonProxy;
								onPersonIsRegistered = false;
							}
						}
					}
				}
            
				private static void onPersonProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Customer, PropertyEventArgs> handler = onPerson;
					if ((object)handler != null)
						handler.Invoke((Customer)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<Customer, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<Customer, PropertyEventArgs> OnModifiedDate
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
					EventHandler<Customer, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((Customer)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Customer, PropertyEventArgs> onUid;
				public static event EventHandler<Customer, PropertyEventArgs> OnUid
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
					EventHandler<Customer, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((Customer)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region ICustomerOriginalData

		public ICustomerOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ICustomer

		string ICustomerOriginalData.AccountNumber { get { return OriginalData.AccountNumber; } }
		string ICustomerOriginalData.rowguid { get { return OriginalData.rowguid; } }
		Store ICustomerOriginalData.Store { get { return ((ILookupHelper<Store>)OriginalData.Store).GetOriginalItem(null); } }
		SalesTerritory ICustomerOriginalData.SalesTerritory { get { return ((ILookupHelper<SalesTerritory>)OriginalData.SalesTerritory).GetOriginalItem(null); } }
		Person ICustomerOriginalData.Person { get { return ((ILookupHelper<Person>)OriginalData.Person).GetOriginalItem(null); } }

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