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
	public interface IAddressOriginalData : ISchemaBaseOriginalData
	{
		string AddressLine1 { get; }
		string AddressLine2 { get; }
		string City { get; }
		string PostalCode { get; }
		string SpatialLocation { get; }
		string rowguid { get; }
		StateProvince StateProvince { get; }
		AddressType AddressType { get; }
	}

	public partial class Address : OGM<Address, Address.AddressData, System.String>, ISchemaBase, INeo4jBase, IAddressOriginalData
	{
		#region Initialize

		static Address()
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

		public static Dictionary<System.String, Address> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.AddressAlias, IWhereQuery> query)
		{
			q.AddressAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Address.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"Address => AddressLine1 : {this.AddressLine1}, AddressLine2 : {this.AddressLine2?.ToString() ?? "null"}, City : {this.City}, PostalCode : {this.PostalCode}, SpatialLocation : {this.SpatialLocation?.ToString() ?? "null"}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
					OriginalData = new AddressData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.AddressLine1 is null)
				throw new PersistenceException(string.Format("Cannot save Address with key '{0}' because the AddressLine1 cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.City is null)
				throw new PersistenceException(string.Format("Cannot save Address with key '{0}' because the City cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.PostalCode is null)
				throw new PersistenceException(string.Format("Cannot save Address with key '{0}' because the PostalCode cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid is null)
				throw new PersistenceException(string.Format("Cannot save Address with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class AddressData : Data<System.String>
		{
			public AddressData()
			{

			}

			public AddressData(AddressData data)
			{
				AddressLine1 = data.AddressLine1;
				AddressLine2 = data.AddressLine2;
				City = data.City;
				PostalCode = data.PostalCode;
				SpatialLocation = data.SpatialLocation;
				rowguid = data.rowguid;
				StateProvince = data.StateProvince;
				AddressType = data.AddressType;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Address";

				StateProvince = new EntityCollection<StateProvince>(Wrapper, Members.StateProvince);
				AddressType = new EntityCollection<AddressType>(Wrapper, Members.AddressType);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("AddressLine1",  AddressLine1);
				dictionary.Add("AddressLine2",  AddressLine2);
				dictionary.Add("City",  City);
				dictionary.Add("PostalCode",  PostalCode);
				dictionary.Add("SpatialLocation",  SpatialLocation);
				dictionary.Add("rowguid",  rowguid);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("AddressLine1", out value))
					AddressLine1 = (string)value;
				if (properties.TryGetValue("AddressLine2", out value))
					AddressLine2 = (string)value;
				if (properties.TryGetValue("City", out value))
					City = (string)value;
				if (properties.TryGetValue("PostalCode", out value))
					PostalCode = (string)value;
				if (properties.TryGetValue("SpatialLocation", out value))
					SpatialLocation = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IAddress

			public string AddressLine1 { get; set; }
			public string AddressLine2 { get; set; }
			public string City { get; set; }
			public string PostalCode { get; set; }
			public string SpatialLocation { get; set; }
			public string rowguid { get; set; }
			public EntityCollection<StateProvince> StateProvince { get; private set; }
			public EntityCollection<AddressType> AddressType { get; private set; }

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

		#region Members for interface IAddress

		public string AddressLine1 { get { LazyGet(); return InnerData.AddressLine1; } set { if (LazySet(Members.AddressLine1, InnerData.AddressLine1, value)) InnerData.AddressLine1 = value; } }
		public string AddressLine2 { get { LazyGet(); return InnerData.AddressLine2; } set { if (LazySet(Members.AddressLine2, InnerData.AddressLine2, value)) InnerData.AddressLine2 = value; } }
		public string City { get { LazyGet(); return InnerData.City; } set { if (LazySet(Members.City, InnerData.City, value)) InnerData.City = value; } }
		public string PostalCode { get { LazyGet(); return InnerData.PostalCode; } set { if (LazySet(Members.PostalCode, InnerData.PostalCode, value)) InnerData.PostalCode = value; } }
		public string SpatialLocation { get { LazyGet(); return InnerData.SpatialLocation; } set { if (LazySet(Members.SpatialLocation, InnerData.SpatialLocation, value)) InnerData.SpatialLocation = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }
		public StateProvince StateProvince
		{
			get { return ((ILookupHelper<StateProvince>)InnerData.StateProvince).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.StateProvince, ((ILookupHelper<StateProvince>)InnerData.StateProvince).GetItem(null), value))
					((ILookupHelper<StateProvince>)InnerData.StateProvince).SetItem(value, null); 
			}
		}
		public AddressType AddressType
		{
			get { return ((ILookupHelper<AddressType>)InnerData.AddressType).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.AddressType, ((ILookupHelper<AddressType>)InnerData.AddressType).GetItem(null), value))
					((ILookupHelper<AddressType>)InnerData.AddressType).SetItem(value, null); 
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

		private static AddressMembers members = null;
		public static AddressMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(Address))
					{
						if (members is null)
							members = new AddressMembers();
					}
				}
				return members;
			}
		}
		public class AddressMembers
		{
			internal AddressMembers() { }

			#region Members for interface IAddress

			public Property AddressLine1 { get; } = Datastore.AdventureWorks.Model.Entities["Address"].Properties["AddressLine1"];
			public Property AddressLine2 { get; } = Datastore.AdventureWorks.Model.Entities["Address"].Properties["AddressLine2"];
			public Property City { get; } = Datastore.AdventureWorks.Model.Entities["Address"].Properties["City"];
			public Property PostalCode { get; } = Datastore.AdventureWorks.Model.Entities["Address"].Properties["PostalCode"];
			public Property SpatialLocation { get; } = Datastore.AdventureWorks.Model.Entities["Address"].Properties["SpatialLocation"];
			public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["Address"].Properties["rowguid"];
			public Property StateProvince { get; } = Datastore.AdventureWorks.Model.Entities["Address"].Properties["StateProvince"];
			public Property AddressType { get; } = Datastore.AdventureWorks.Model.Entities["Address"].Properties["AddressType"];
			#endregion

			#region Members for interface ISchemaBase

			public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		private static AddressFullTextMembers fullTextMembers = null;
		public static AddressFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(Address))
					{
						if (fullTextMembers is null)
							fullTextMembers = new AddressFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class AddressFullTextMembers
		{
			internal AddressFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(Address))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["Address"];
				}
			}
			return entity;
		}

		private static AddressEvents events = null;
		public static AddressEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(Address))
					{
						if (events is null)
							events = new AddressEvents();
					}
				}
				return events;
			}
		}
		public class AddressEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<Address, EntityEventArgs> onNew;
			public event EventHandler<Address, EntityEventArgs> OnNew
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
				EventHandler<Address, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((Address)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<Address, EntityEventArgs> onDelete;
			public event EventHandler<Address, EntityEventArgs> OnDelete
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
				EventHandler<Address, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((Address)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<Address, EntityEventArgs> onSave;
			public event EventHandler<Address, EntityEventArgs> OnSave
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
				EventHandler<Address, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((Address)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<Address, EntityEventArgs> onAfterSave;
			public event EventHandler<Address, EntityEventArgs> OnAfterSave
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
				EventHandler<Address, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((Address)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnAddressLine1

				private static bool onAddressLine1IsRegistered = false;

				private static EventHandler<Address, PropertyEventArgs> onAddressLine1;
				public static event EventHandler<Address, PropertyEventArgs> OnAddressLine1
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onAddressLine1IsRegistered)
							{
								Members.AddressLine1.Events.OnChange -= onAddressLine1Proxy;
								Members.AddressLine1.Events.OnChange += onAddressLine1Proxy;
								onAddressLine1IsRegistered = true;
							}
							onAddressLine1 += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onAddressLine1 -= value;
							if (onAddressLine1 is null && onAddressLine1IsRegistered)
							{
								Members.AddressLine1.Events.OnChange -= onAddressLine1Proxy;
								onAddressLine1IsRegistered = false;
							}
						}
					}
				}
			
				private static void onAddressLine1Proxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Address, PropertyEventArgs> handler = onAddressLine1;
					if (handler is not null)
						handler.Invoke((Address)sender, args);
				}

				#endregion

				#region OnAddressLine2

				private static bool onAddressLine2IsRegistered = false;

				private static EventHandler<Address, PropertyEventArgs> onAddressLine2;
				public static event EventHandler<Address, PropertyEventArgs> OnAddressLine2
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onAddressLine2IsRegistered)
							{
								Members.AddressLine2.Events.OnChange -= onAddressLine2Proxy;
								Members.AddressLine2.Events.OnChange += onAddressLine2Proxy;
								onAddressLine2IsRegistered = true;
							}
							onAddressLine2 += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onAddressLine2 -= value;
							if (onAddressLine2 is null && onAddressLine2IsRegistered)
							{
								Members.AddressLine2.Events.OnChange -= onAddressLine2Proxy;
								onAddressLine2IsRegistered = false;
							}
						}
					}
				}
			
				private static void onAddressLine2Proxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Address, PropertyEventArgs> handler = onAddressLine2;
					if (handler is not null)
						handler.Invoke((Address)sender, args);
				}

				#endregion

				#region OnCity

				private static bool onCityIsRegistered = false;

				private static EventHandler<Address, PropertyEventArgs> onCity;
				public static event EventHandler<Address, PropertyEventArgs> OnCity
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCityIsRegistered)
							{
								Members.City.Events.OnChange -= onCityProxy;
								Members.City.Events.OnChange += onCityProxy;
								onCityIsRegistered = true;
							}
							onCity += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCity -= value;
							if (onCity is null && onCityIsRegistered)
							{
								Members.City.Events.OnChange -= onCityProxy;
								onCityIsRegistered = false;
							}
						}
					}
				}
			
				private static void onCityProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Address, PropertyEventArgs> handler = onCity;
					if (handler is not null)
						handler.Invoke((Address)sender, args);
				}

				#endregion

				#region OnPostalCode

				private static bool onPostalCodeIsRegistered = false;

				private static EventHandler<Address, PropertyEventArgs> onPostalCode;
				public static event EventHandler<Address, PropertyEventArgs> OnPostalCode
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPostalCodeIsRegistered)
							{
								Members.PostalCode.Events.OnChange -= onPostalCodeProxy;
								Members.PostalCode.Events.OnChange += onPostalCodeProxy;
								onPostalCodeIsRegistered = true;
							}
							onPostalCode += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPostalCode -= value;
							if (onPostalCode is null && onPostalCodeIsRegistered)
							{
								Members.PostalCode.Events.OnChange -= onPostalCodeProxy;
								onPostalCodeIsRegistered = false;
							}
						}
					}
				}
			
				private static void onPostalCodeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Address, PropertyEventArgs> handler = onPostalCode;
					if (handler is not null)
						handler.Invoke((Address)sender, args);
				}

				#endregion

				#region OnSpatialLocation

				private static bool onSpatialLocationIsRegistered = false;

				private static EventHandler<Address, PropertyEventArgs> onSpatialLocation;
				public static event EventHandler<Address, PropertyEventArgs> OnSpatialLocation
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSpatialLocationIsRegistered)
							{
								Members.SpatialLocation.Events.OnChange -= onSpatialLocationProxy;
								Members.SpatialLocation.Events.OnChange += onSpatialLocationProxy;
								onSpatialLocationIsRegistered = true;
							}
							onSpatialLocation += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSpatialLocation -= value;
							if (onSpatialLocation is null && onSpatialLocationIsRegistered)
							{
								Members.SpatialLocation.Events.OnChange -= onSpatialLocationProxy;
								onSpatialLocationIsRegistered = false;
							}
						}
					}
				}
			
				private static void onSpatialLocationProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Address, PropertyEventArgs> handler = onSpatialLocation;
					if (handler is not null)
						handler.Invoke((Address)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<Address, PropertyEventArgs> onrowguid;
				public static event EventHandler<Address, PropertyEventArgs> Onrowguid
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
					EventHandler<Address, PropertyEventArgs> handler = onrowguid;
					if (handler is not null)
						handler.Invoke((Address)sender, args);
				}

				#endregion

				#region OnStateProvince

				private static bool onStateProvinceIsRegistered = false;

				private static EventHandler<Address, PropertyEventArgs> onStateProvince;
				public static event EventHandler<Address, PropertyEventArgs> OnStateProvince
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onStateProvinceIsRegistered)
							{
								Members.StateProvince.Events.OnChange -= onStateProvinceProxy;
								Members.StateProvince.Events.OnChange += onStateProvinceProxy;
								onStateProvinceIsRegistered = true;
							}
							onStateProvince += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onStateProvince -= value;
							if (onStateProvince is null && onStateProvinceIsRegistered)
							{
								Members.StateProvince.Events.OnChange -= onStateProvinceProxy;
								onStateProvinceIsRegistered = false;
							}
						}
					}
				}
			
				private static void onStateProvinceProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Address, PropertyEventArgs> handler = onStateProvince;
					if (handler is not null)
						handler.Invoke((Address)sender, args);
				}

				#endregion

				#region OnAddressType

				private static bool onAddressTypeIsRegistered = false;

				private static EventHandler<Address, PropertyEventArgs> onAddressType;
				public static event EventHandler<Address, PropertyEventArgs> OnAddressType
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onAddressTypeIsRegistered)
							{
								Members.AddressType.Events.OnChange -= onAddressTypeProxy;
								Members.AddressType.Events.OnChange += onAddressTypeProxy;
								onAddressTypeIsRegistered = true;
							}
							onAddressType += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onAddressType -= value;
							if (onAddressType is null && onAddressTypeIsRegistered)
							{
								Members.AddressType.Events.OnChange -= onAddressTypeProxy;
								onAddressTypeIsRegistered = false;
							}
						}
					}
				}
			
				private static void onAddressTypeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Address, PropertyEventArgs> handler = onAddressType;
					if (handler is not null)
						handler.Invoke((Address)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<Address, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<Address, PropertyEventArgs> OnModifiedDate
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
					EventHandler<Address, PropertyEventArgs> handler = onModifiedDate;
					if (handler is not null)
						handler.Invoke((Address)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Address, PropertyEventArgs> onUid;
				public static event EventHandler<Address, PropertyEventArgs> OnUid
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
					EventHandler<Address, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((Address)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region IAddressOriginalData

		public IAddressOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IAddress

		string IAddressOriginalData.AddressLine1 { get { return OriginalData.AddressLine1; } }
		string IAddressOriginalData.AddressLine2 { get { return OriginalData.AddressLine2; } }
		string IAddressOriginalData.City { get { return OriginalData.City; } }
		string IAddressOriginalData.PostalCode { get { return OriginalData.PostalCode; } }
		string IAddressOriginalData.SpatialLocation { get { return OriginalData.SpatialLocation; } }
		string IAddressOriginalData.rowguid { get { return OriginalData.rowguid; } }
		StateProvince IAddressOriginalData.StateProvince { get { return ((ILookupHelper<StateProvince>)OriginalData.StateProvince).GetOriginalItem(null); } }
		AddressType IAddressOriginalData.AddressType { get { return ((ILookupHelper<AddressType>)OriginalData.AddressType).GetOriginalItem(null); } }

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