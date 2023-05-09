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
	public interface IStoreOriginalData : ISchemaBaseOriginalData
	{
		string Name { get; }
		string Demographics { get; }
		string rowguid { get; }
		SalesPerson SalesPerson { get; }
		Address Address { get; }
	}

	public partial class Store : OGM<Store, Store.StoreData, System.String>, ISchemaBase, INeo4jBase, IStoreOriginalData
	{
		#region Initialize

		static Store()
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

		public static Dictionary<System.String, Store> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.StoreAlias, IWhereQuery> query)
		{
			q.StoreAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Store.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"Store => Name : {this.Name}, Demographics : {this.Demographics?.ToString() ?? "null"}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
				if (ReferenceEquals(InnerData, OriginalData))
					OriginalData = new StoreData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.Name is null)
				throw new PersistenceException(string.Format("Cannot save Store with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid is null)
				throw new PersistenceException(string.Format("Cannot save Store with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class StoreData : Data<System.String>
		{
			public StoreData()
			{

			}

			public StoreData(StoreData data)
			{
				Name = data.Name;
				Demographics = data.Demographics;
				rowguid = data.rowguid;
				SalesPerson = data.SalesPerson;
				Address = data.Address;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Store";

				SalesPerson = new EntityCollection<SalesPerson>(Wrapper, Members.SalesPerson, item => { if (Members.SalesPerson.Events.HasRegisteredChangeHandlers) { int loadHack = item.Stores.Count; } });
				Address = new EntityCollection<Address>(Wrapper, Members.Address);
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
				dictionary.Add("Demographics",  Demographics);
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
				if (properties.TryGetValue("Demographics", out value))
					Demographics = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IStore

			public string Name { get; set; }
			public string Demographics { get; set; }
			public string rowguid { get; set; }
			public EntityCollection<SalesPerson> SalesPerson { get; private set; }
			public EntityCollection<Address> Address { get; private set; }

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

		#region Members for interface IStore

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
		public string Demographics { get { LazyGet(); return InnerData.Demographics; } set { if (LazySet(Members.Demographics, InnerData.Demographics, value)) InnerData.Demographics = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }
		public SalesPerson SalesPerson
		{
			get { return ((ILookupHelper<SalesPerson>)InnerData.SalesPerson).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.SalesPerson, ((ILookupHelper<SalesPerson>)InnerData.SalesPerson).GetItem(null), value))
					((ILookupHelper<SalesPerson>)InnerData.SalesPerson).SetItem(value, null); 
			}
		}
		private void ClearSalesPerson(DateTime? moment)
		{
			((ILookupHelper<SalesPerson>)InnerData.SalesPerson).ClearLookup(moment);
		}
		public Address Address
		{
			get { return ((ILookupHelper<Address>)InnerData.Address).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Address, ((ILookupHelper<Address>)InnerData.Address).GetItem(null), value))
					((ILookupHelper<Address>)InnerData.Address).SetItem(value, null); 
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

		private static StoreMembers members = null;
		public static StoreMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(Store))
					{
						if (members is null)
							members = new StoreMembers();
					}
				}
				return members;
			}
		}
		public class StoreMembers
		{
			internal StoreMembers() { }

			#region Members for interface IStore

			public Property Name { get; } = Datastore.AdventureWorks.Model.Entities["Store"].Properties["Name"];
			public Property Demographics { get; } = Datastore.AdventureWorks.Model.Entities["Store"].Properties["Demographics"];
			public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["Store"].Properties["rowguid"];
			public Property SalesPerson { get; } = Datastore.AdventureWorks.Model.Entities["Store"].Properties["SalesPerson"];
			public Property Address { get; } = Datastore.AdventureWorks.Model.Entities["Store"].Properties["Address"];
			#endregion

			#region Members for interface ISchemaBase

			public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		private static StoreFullTextMembers fullTextMembers = null;
		public static StoreFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(Store))
					{
						if (fullTextMembers is null)
							fullTextMembers = new StoreFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class StoreFullTextMembers
		{
			internal StoreFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(Store))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["Store"];
				}
			}
			return entity;
		}

		private static StoreEvents events = null;
		public static StoreEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(Store))
					{
						if (events is null)
							events = new StoreEvents();
					}
				}
				return events;
			}
		}
		public class StoreEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<Store, EntityEventArgs> onNew;
			public event EventHandler<Store, EntityEventArgs> OnNew
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
						if (onNew is null && onNewIsRegistered)
						{
							Entity.Events.OnNew -= onNewProxy;
							onNewIsRegistered = false;
						}
					}
				}
			}
			
			private void onNewProxy(object sender, EntityEventArgs args)
			{
				EventHandler<Store, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((Store)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<Store, EntityEventArgs> onDelete;
			public event EventHandler<Store, EntityEventArgs> OnDelete
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
						if (onDelete is null && onDeleteIsRegistered)
						{
							Entity.Events.OnDelete -= onDeleteProxy;
							onDeleteIsRegistered = false;
						}
					}
				}
			}
			
			private void onDeleteProxy(object sender, EntityEventArgs args)
			{
				EventHandler<Store, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((Store)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<Store, EntityEventArgs> onSave;
			public event EventHandler<Store, EntityEventArgs> OnSave
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
						if (onSave is null && onSaveIsRegistered)
						{
							Entity.Events.OnSave -= onSaveProxy;
							onSaveIsRegistered = false;
						}
					}
				}
			}
			
			private void onSaveProxy(object sender, EntityEventArgs args)
			{
				EventHandler<Store, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((Store)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<Store, EntityEventArgs> onAfterSave;
			public event EventHandler<Store, EntityEventArgs> OnAfterSave
			{
				add
				{
					lock (this)
					{
						if (!onAfterSaveIsRegistered)
						{
							Entity.Events.OnAfterSave -= onAfterSaveProxy;
							Entity.Events.OnAfterSave += onAfterSaveProxy;
							onAfterSaveIsRegistered = true;
						}
						onAfterSave += value;
					}
				}
				remove
				{
					lock (this)
					{
						onAfterSave -= value;
						if (onAfterSave is null && onAfterSaveIsRegistered)
						{
							Entity.Events.OnAfterSave -= onAfterSaveProxy;
							onAfterSaveIsRegistered = false;
						}
					}
				}
			}
			
			private void onAfterSaveProxy(object sender, EntityEventArgs args)
			{
				EventHandler<Store, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((Store)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<Store, PropertyEventArgs> onName;
				public static event EventHandler<Store, PropertyEventArgs> OnName
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
							if (onName is null && onNameIsRegistered)
							{
								Members.Name.Events.OnChange -= onNameProxy;
								onNameIsRegistered = false;
							}
						}
					}
				}
			
				private static void onNameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Store, PropertyEventArgs> handler = onName;
					if (handler is not null)
						handler.Invoke((Store)sender, args);
				}

				#endregion

				#region OnDemographics

				private static bool onDemographicsIsRegistered = false;

				private static EventHandler<Store, PropertyEventArgs> onDemographics;
				public static event EventHandler<Store, PropertyEventArgs> OnDemographics
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDemographicsIsRegistered)
							{
								Members.Demographics.Events.OnChange -= onDemographicsProxy;
								Members.Demographics.Events.OnChange += onDemographicsProxy;
								onDemographicsIsRegistered = true;
							}
							onDemographics += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDemographics -= value;
							if (onDemographics is null && onDemographicsIsRegistered)
							{
								Members.Demographics.Events.OnChange -= onDemographicsProxy;
								onDemographicsIsRegistered = false;
							}
						}
					}
				}
			
				private static void onDemographicsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Store, PropertyEventArgs> handler = onDemographics;
					if (handler is not null)
						handler.Invoke((Store)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<Store, PropertyEventArgs> onrowguid;
				public static event EventHandler<Store, PropertyEventArgs> Onrowguid
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
							if (onrowguid is null && onrowguidIsRegistered)
							{
								Members.rowguid.Events.OnChange -= onrowguidProxy;
								onrowguidIsRegistered = false;
							}
						}
					}
				}
			
				private static void onrowguidProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Store, PropertyEventArgs> handler = onrowguid;
					if (handler is not null)
						handler.Invoke((Store)sender, args);
				}

				#endregion

				#region OnSalesPerson

				private static bool onSalesPersonIsRegistered = false;

				private static EventHandler<Store, PropertyEventArgs> onSalesPerson;
				public static event EventHandler<Store, PropertyEventArgs> OnSalesPerson
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalesPersonIsRegistered)
							{
								Members.SalesPerson.Events.OnChange -= onSalesPersonProxy;
								Members.SalesPerson.Events.OnChange += onSalesPersonProxy;
								onSalesPersonIsRegistered = true;
							}
							onSalesPerson += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalesPerson -= value;
							if (onSalesPerson is null && onSalesPersonIsRegistered)
							{
								Members.SalesPerson.Events.OnChange -= onSalesPersonProxy;
								onSalesPersonIsRegistered = false;
							}
						}
					}
				}
			
				private static void onSalesPersonProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Store, PropertyEventArgs> handler = onSalesPerson;
					if (handler is not null)
						handler.Invoke((Store)sender, args);
				}

				#endregion

				#region OnAddress

				private static bool onAddressIsRegistered = false;

				private static EventHandler<Store, PropertyEventArgs> onAddress;
				public static event EventHandler<Store, PropertyEventArgs> OnAddress
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onAddressIsRegistered)
							{
								Members.Address.Events.OnChange -= onAddressProxy;
								Members.Address.Events.OnChange += onAddressProxy;
								onAddressIsRegistered = true;
							}
							onAddress += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onAddress -= value;
							if (onAddress is null && onAddressIsRegistered)
							{
								Members.Address.Events.OnChange -= onAddressProxy;
								onAddressIsRegistered = false;
							}
						}
					}
				}
			
				private static void onAddressProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Store, PropertyEventArgs> handler = onAddress;
					if (handler is not null)
						handler.Invoke((Store)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<Store, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<Store, PropertyEventArgs> OnModifiedDate
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
							if (onModifiedDate is null && onModifiedDateIsRegistered)
							{
								Members.ModifiedDate.Events.OnChange -= onModifiedDateProxy;
								onModifiedDateIsRegistered = false;
							}
						}
					}
				}
			
				private static void onModifiedDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Store, PropertyEventArgs> handler = onModifiedDate;
					if (handler is not null)
						handler.Invoke((Store)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Store, PropertyEventArgs> onUid;
				public static event EventHandler<Store, PropertyEventArgs> OnUid
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
							if (onUid is null && onUidIsRegistered)
							{
								Members.Uid.Events.OnChange -= onUidProxy;
								onUidIsRegistered = false;
							}
						}
					}
				}
			
				private static void onUidProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Store, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((Store)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region IStoreOriginalData

		public IStoreOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IStore

		string IStoreOriginalData.Name { get { return OriginalData.Name; } }
		string IStoreOriginalData.Demographics { get { return OriginalData.Demographics; } }
		string IStoreOriginalData.rowguid { get { return OriginalData.rowguid; } }
		SalesPerson IStoreOriginalData.SalesPerson { get { return ((ILookupHelper<SalesPerson>)OriginalData.SalesPerson).GetOriginalItem(null); } }
		Address IStoreOriginalData.Address { get { return ((ILookupHelper<Address>)OriginalData.Address).GetOriginalItem(null); } }

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