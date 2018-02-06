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
	public interface IVendorOriginalData : ISchemaBaseOriginalData
    {
		string AccountNumber { get; }
		string Name { get; }
		string CreditRating { get; }
		string PreferredVendorStatus { get; }
		string ActiveFlag { get; }
		string PurchasingWebServiceURL { get; }
		Employee Employee { get; }
		ProductVendor ProductVendor { get; }
    }

	public partial class Vendor : OGM<Vendor, Vendor.VendorData, System.String>, ISchemaBase, INeo4jBase, IVendorOriginalData
	{
        #region Initialize

        static Vendor()
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

        public static Dictionary<System.String, Vendor> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.VendorAlias, IWhereQuery> query)
        {
            q.VendorAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Vendor.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"Vendor => AccountNumber : {this.AccountNumber}, Name : {this.Name}, CreditRating : {this.CreditRating}, PreferredVendorStatus : {this.PreferredVendorStatus}, ActiveFlag : {this.ActiveFlag}, PurchasingWebServiceURL : {this.PurchasingWebServiceURL?.ToString() ?? "null"}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new VendorData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.AccountNumber == null)
				throw new PersistenceException(string.Format("Cannot save Vendor with key '{0}' because the AccountNumber cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Name == null)
				throw new PersistenceException(string.Format("Cannot save Vendor with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.CreditRating == null)
				throw new PersistenceException(string.Format("Cannot save Vendor with key '{0}' because the CreditRating cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.PreferredVendorStatus == null)
				throw new PersistenceException(string.Format("Cannot save Vendor with key '{0}' because the PreferredVendorStatus cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ActiveFlag == null)
				throw new PersistenceException(string.Format("Cannot save Vendor with key '{0}' because the ActiveFlag cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save Vendor with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class VendorData : Data<System.String>
		{
			public VendorData()
            {

            }

            public VendorData(VendorData data)
            {
				AccountNumber = data.AccountNumber;
				Name = data.Name;
				CreditRating = data.CreditRating;
				PreferredVendorStatus = data.PreferredVendorStatus;
				ActiveFlag = data.ActiveFlag;
				PurchasingWebServiceURL = data.PurchasingWebServiceURL;
				Employee = data.Employee;
				ProductVendor = data.ProductVendor;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Vendor";

				Employee = new EntityCollection<Employee>(Wrapper, Members.Employee, item => { if (Members.Employee.Events.HasRegisteredChangeHandlers) { int loadHack = item.Vendors.Count; } });
				ProductVendor = new EntityCollection<ProductVendor>(Wrapper, Members.ProductVendor);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("AccountNumber",  AccountNumber);
				dictionary.Add("Name",  Name);
				dictionary.Add("CreditRating",  CreditRating);
				dictionary.Add("PreferredVendorStatus",  PreferredVendorStatus);
				dictionary.Add("ActiveFlag",  ActiveFlag);
				dictionary.Add("PurchasingWebServiceURL",  PurchasingWebServiceURL);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("AccountNumber", out value))
					AccountNumber = (string)value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("CreditRating", out value))
					CreditRating = (string)value;
				if (properties.TryGetValue("PreferredVendorStatus", out value))
					PreferredVendorStatus = (string)value;
				if (properties.TryGetValue("ActiveFlag", out value))
					ActiveFlag = (string)value;
				if (properties.TryGetValue("PurchasingWebServiceURL", out value))
					PurchasingWebServiceURL = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IVendor

			public string AccountNumber { get; set; }
			public string Name { get; set; }
			public string CreditRating { get; set; }
			public string PreferredVendorStatus { get; set; }
			public string ActiveFlag { get; set; }
			public string PurchasingWebServiceURL { get; set; }
			public EntityCollection<Employee> Employee { get; private set; }
			public EntityCollection<ProductVendor> ProductVendor { get; private set; }

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

		#region Members for interface IVendor

		public string AccountNumber { get { LazyGet(); return InnerData.AccountNumber; } set { if (LazySet(Members.AccountNumber, InnerData.AccountNumber, value)) InnerData.AccountNumber = value; } }
		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
		public string CreditRating { get { LazyGet(); return InnerData.CreditRating; } set { if (LazySet(Members.CreditRating, InnerData.CreditRating, value)) InnerData.CreditRating = value; } }
		public string PreferredVendorStatus { get { LazyGet(); return InnerData.PreferredVendorStatus; } set { if (LazySet(Members.PreferredVendorStatus, InnerData.PreferredVendorStatus, value)) InnerData.PreferredVendorStatus = value; } }
		public string ActiveFlag { get { LazyGet(); return InnerData.ActiveFlag; } set { if (LazySet(Members.ActiveFlag, InnerData.ActiveFlag, value)) InnerData.ActiveFlag = value; } }
		public string PurchasingWebServiceURL { get { LazyGet(); return InnerData.PurchasingWebServiceURL; } set { if (LazySet(Members.PurchasingWebServiceURL, InnerData.PurchasingWebServiceURL, value)) InnerData.PurchasingWebServiceURL = value; } }
		public Employee Employee
		{
			get { return ((ILookupHelper<Employee>)InnerData.Employee).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Employee, ((ILookupHelper<Employee>)InnerData.Employee).GetItem(null), value))
					((ILookupHelper<Employee>)InnerData.Employee).SetItem(value, null); 
			}
		}
		private void ClearEmployee(DateTime? moment)
		{
			((ILookupHelper<Employee>)InnerData.Employee).ClearLookup(moment);
		}
		public ProductVendor ProductVendor
		{
			get { return ((ILookupHelper<ProductVendor>)InnerData.ProductVendor).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.ProductVendor, ((ILookupHelper<ProductVendor>)InnerData.ProductVendor).GetItem(null), value))
					((ILookupHelper<ProductVendor>)InnerData.ProductVendor).SetItem(value, null); 
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

        private static VendorMembers members = null;
        public static VendorMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(Vendor))
                    {
                        if (members == null)
                            members = new VendorMembers();
                    }
                }
                return members;
            }
        }
        public class VendorMembers
        {
            internal VendorMembers() { }

			#region Members for interface IVendor

            public Property AccountNumber { get; } = Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["AccountNumber"];
            public Property Name { get; } = Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["Name"];
            public Property CreditRating { get; } = Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["CreditRating"];
            public Property PreferredVendorStatus { get; } = Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["PreferredVendorStatus"];
            public Property ActiveFlag { get; } = Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["ActiveFlag"];
            public Property PurchasingWebServiceURL { get; } = Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["PurchasingWebServiceURL"];
            public Property Employee { get; } = Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["Employee"];
            public Property ProductVendor { get; } = Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["ProductVendor"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static VendorFullTextMembers fullTextMembers = null;
        public static VendorFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(Vendor))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new VendorFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class VendorFullTextMembers
        {
            internal VendorFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(Vendor))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["Vendor"];
                }
            }
            return entity;
        }

		private static VendorEvents events = null;
        public static VendorEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(Vendor))
                    {
                        if (events == null)
                            events = new VendorEvents();
                    }
                }
                return events;
            }
        }
        public class VendorEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Vendor, EntityEventArgs> onNew;
            public event EventHandler<Vendor, EntityEventArgs> OnNew
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
                EventHandler<Vendor, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((Vendor)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<Vendor, EntityEventArgs> onDelete;
            public event EventHandler<Vendor, EntityEventArgs> OnDelete
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
                EventHandler<Vendor, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((Vendor)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<Vendor, EntityEventArgs> onSave;
            public event EventHandler<Vendor, EntityEventArgs> OnSave
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
                EventHandler<Vendor, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((Vendor)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnAccountNumber

				private static bool onAccountNumberIsRegistered = false;

				private static EventHandler<Vendor, PropertyEventArgs> onAccountNumber;
				public static event EventHandler<Vendor, PropertyEventArgs> OnAccountNumber
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
					EventHandler<Vendor, PropertyEventArgs> handler = onAccountNumber;
					if ((object)handler != null)
						handler.Invoke((Vendor)sender, args);
				}

				#endregion

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<Vendor, PropertyEventArgs> onName;
				public static event EventHandler<Vendor, PropertyEventArgs> OnName
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
					EventHandler<Vendor, PropertyEventArgs> handler = onName;
					if ((object)handler != null)
						handler.Invoke((Vendor)sender, args);
				}

				#endregion

				#region OnCreditRating

				private static bool onCreditRatingIsRegistered = false;

				private static EventHandler<Vendor, PropertyEventArgs> onCreditRating;
				public static event EventHandler<Vendor, PropertyEventArgs> OnCreditRating
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCreditRatingIsRegistered)
							{
								Members.CreditRating.Events.OnChange -= onCreditRatingProxy;
								Members.CreditRating.Events.OnChange += onCreditRatingProxy;
								onCreditRatingIsRegistered = true;
							}
							onCreditRating += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCreditRating -= value;
							if (onCreditRating == null && onCreditRatingIsRegistered)
							{
								Members.CreditRating.Events.OnChange -= onCreditRatingProxy;
								onCreditRatingIsRegistered = false;
							}
						}
					}
				}
            
				private static void onCreditRatingProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Vendor, PropertyEventArgs> handler = onCreditRating;
					if ((object)handler != null)
						handler.Invoke((Vendor)sender, args);
				}

				#endregion

				#region OnPreferredVendorStatus

				private static bool onPreferredVendorStatusIsRegistered = false;

				private static EventHandler<Vendor, PropertyEventArgs> onPreferredVendorStatus;
				public static event EventHandler<Vendor, PropertyEventArgs> OnPreferredVendorStatus
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPreferredVendorStatusIsRegistered)
							{
								Members.PreferredVendorStatus.Events.OnChange -= onPreferredVendorStatusProxy;
								Members.PreferredVendorStatus.Events.OnChange += onPreferredVendorStatusProxy;
								onPreferredVendorStatusIsRegistered = true;
							}
							onPreferredVendorStatus += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPreferredVendorStatus -= value;
							if (onPreferredVendorStatus == null && onPreferredVendorStatusIsRegistered)
							{
								Members.PreferredVendorStatus.Events.OnChange -= onPreferredVendorStatusProxy;
								onPreferredVendorStatusIsRegistered = false;
							}
						}
					}
				}
            
				private static void onPreferredVendorStatusProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Vendor, PropertyEventArgs> handler = onPreferredVendorStatus;
					if ((object)handler != null)
						handler.Invoke((Vendor)sender, args);
				}

				#endregion

				#region OnActiveFlag

				private static bool onActiveFlagIsRegistered = false;

				private static EventHandler<Vendor, PropertyEventArgs> onActiveFlag;
				public static event EventHandler<Vendor, PropertyEventArgs> OnActiveFlag
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onActiveFlagIsRegistered)
							{
								Members.ActiveFlag.Events.OnChange -= onActiveFlagProxy;
								Members.ActiveFlag.Events.OnChange += onActiveFlagProxy;
								onActiveFlagIsRegistered = true;
							}
							onActiveFlag += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onActiveFlag -= value;
							if (onActiveFlag == null && onActiveFlagIsRegistered)
							{
								Members.ActiveFlag.Events.OnChange -= onActiveFlagProxy;
								onActiveFlagIsRegistered = false;
							}
						}
					}
				}
            
				private static void onActiveFlagProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Vendor, PropertyEventArgs> handler = onActiveFlag;
					if ((object)handler != null)
						handler.Invoke((Vendor)sender, args);
				}

				#endregion

				#region OnPurchasingWebServiceURL

				private static bool onPurchasingWebServiceURLIsRegistered = false;

				private static EventHandler<Vendor, PropertyEventArgs> onPurchasingWebServiceURL;
				public static event EventHandler<Vendor, PropertyEventArgs> OnPurchasingWebServiceURL
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPurchasingWebServiceURLIsRegistered)
							{
								Members.PurchasingWebServiceURL.Events.OnChange -= onPurchasingWebServiceURLProxy;
								Members.PurchasingWebServiceURL.Events.OnChange += onPurchasingWebServiceURLProxy;
								onPurchasingWebServiceURLIsRegistered = true;
							}
							onPurchasingWebServiceURL += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPurchasingWebServiceURL -= value;
							if (onPurchasingWebServiceURL == null && onPurchasingWebServiceURLIsRegistered)
							{
								Members.PurchasingWebServiceURL.Events.OnChange -= onPurchasingWebServiceURLProxy;
								onPurchasingWebServiceURLIsRegistered = false;
							}
						}
					}
				}
            
				private static void onPurchasingWebServiceURLProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Vendor, PropertyEventArgs> handler = onPurchasingWebServiceURL;
					if ((object)handler != null)
						handler.Invoke((Vendor)sender, args);
				}

				#endregion

				#region OnEmployee

				private static bool onEmployeeIsRegistered = false;

				private static EventHandler<Vendor, PropertyEventArgs> onEmployee;
				public static event EventHandler<Vendor, PropertyEventArgs> OnEmployee
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onEmployeeIsRegistered)
							{
								Members.Employee.Events.OnChange -= onEmployeeProxy;
								Members.Employee.Events.OnChange += onEmployeeProxy;
								onEmployeeIsRegistered = true;
							}
							onEmployee += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onEmployee -= value;
							if (onEmployee == null && onEmployeeIsRegistered)
							{
								Members.Employee.Events.OnChange -= onEmployeeProxy;
								onEmployeeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onEmployeeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Vendor, PropertyEventArgs> handler = onEmployee;
					if ((object)handler != null)
						handler.Invoke((Vendor)sender, args);
				}

				#endregion

				#region OnProductVendor

				private static bool onProductVendorIsRegistered = false;

				private static EventHandler<Vendor, PropertyEventArgs> onProductVendor;
				public static event EventHandler<Vendor, PropertyEventArgs> OnProductVendor
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onProductVendorIsRegistered)
							{
								Members.ProductVendor.Events.OnChange -= onProductVendorProxy;
								Members.ProductVendor.Events.OnChange += onProductVendorProxy;
								onProductVendorIsRegistered = true;
							}
							onProductVendor += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onProductVendor -= value;
							if (onProductVendor == null && onProductVendorIsRegistered)
							{
								Members.ProductVendor.Events.OnChange -= onProductVendorProxy;
								onProductVendorIsRegistered = false;
							}
						}
					}
				}
            
				private static void onProductVendorProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Vendor, PropertyEventArgs> handler = onProductVendor;
					if ((object)handler != null)
						handler.Invoke((Vendor)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<Vendor, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<Vendor, PropertyEventArgs> OnModifiedDate
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
					EventHandler<Vendor, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((Vendor)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Vendor, PropertyEventArgs> onUid;
				public static event EventHandler<Vendor, PropertyEventArgs> OnUid
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
					EventHandler<Vendor, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((Vendor)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IVendorOriginalData

		public IVendorOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IVendor

		string IVendorOriginalData.AccountNumber { get { return OriginalData.AccountNumber; } }
		string IVendorOriginalData.Name { get { return OriginalData.Name; } }
		string IVendorOriginalData.CreditRating { get { return OriginalData.CreditRating; } }
		string IVendorOriginalData.PreferredVendorStatus { get { return OriginalData.PreferredVendorStatus; } }
		string IVendorOriginalData.ActiveFlag { get { return OriginalData.ActiveFlag; } }
		string IVendorOriginalData.PurchasingWebServiceURL { get { return OriginalData.PurchasingWebServiceURL; } }
		Employee IVendorOriginalData.Employee { get { return ((ILookupHelper<Employee>)OriginalData.Employee).GetOriginalItem(null); } }
		ProductVendor IVendorOriginalData.ProductVendor { get { return ((ILookupHelper<ProductVendor>)OriginalData.ProductVendor).GetOriginalItem(null); } }

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