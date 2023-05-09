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
	public interface IStateProvinceOriginalData : ISchemaBaseOriginalData
	{
		string StateProvinceCode { get; }
		string CountryRegionCode { get; }
		bool IsOnlyStateProvinceFlag { get; }
		string Name { get; }
		string rowguid { get; }
		IEnumerable<CountryRegion> CountryRegion { get; }
		SalesTerritory SalesTerritory { get; }
	}

	public partial class StateProvince : OGM<StateProvince, StateProvince.StateProvinceData, System.String>, ISchemaBase, INeo4jBase, IStateProvinceOriginalData
	{
		#region Initialize

		static StateProvince()
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

		public static Dictionary<System.String, StateProvince> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.StateProvinceAlias, IWhereQuery> query)
		{
			q.StateProvinceAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.StateProvince.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"StateProvince => StateProvinceCode : {this.StateProvinceCode}, CountryRegionCode : {this.CountryRegionCode}, IsOnlyStateProvinceFlag : {this.IsOnlyStateProvinceFlag}, Name : {this.Name?.ToString() ?? "null"}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
					OriginalData = new StateProvinceData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.StateProvinceCode is null)
				throw new PersistenceException(string.Format("Cannot save StateProvince with key '{0}' because the StateProvinceCode cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.CountryRegionCode is null)
				throw new PersistenceException(string.Format("Cannot save StateProvince with key '{0}' because the CountryRegionCode cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid is null)
				throw new PersistenceException(string.Format("Cannot save StateProvince with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class StateProvinceData : Data<System.String>
		{
			public StateProvinceData()
			{

			}

			public StateProvinceData(StateProvinceData data)
			{
				StateProvinceCode = data.StateProvinceCode;
				CountryRegionCode = data.CountryRegionCode;
				IsOnlyStateProvinceFlag = data.IsOnlyStateProvinceFlag;
				Name = data.Name;
				rowguid = data.rowguid;
				CountryRegion = data.CountryRegion;
				SalesTerritory = data.SalesTerritory;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "StateProvince";

				CountryRegion = new EntityCollection<CountryRegion>(Wrapper, Members.CountryRegion);
				SalesTerritory = new EntityCollection<SalesTerritory>(Wrapper, Members.SalesTerritory);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("StateProvinceCode",  StateProvinceCode);
				dictionary.Add("CountryRegionCode",  CountryRegionCode);
				dictionary.Add("IsOnlyStateProvinceFlag",  IsOnlyStateProvinceFlag);
				dictionary.Add("Name",  Name);
				dictionary.Add("rowguid",  rowguid);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("StateProvinceCode", out value))
					StateProvinceCode = (string)value;
				if (properties.TryGetValue("CountryRegionCode", out value))
					CountryRegionCode = (string)value;
				if (properties.TryGetValue("IsOnlyStateProvinceFlag", out value))
					IsOnlyStateProvinceFlag = (bool)value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IStateProvince

			public string StateProvinceCode { get; set; }
			public string CountryRegionCode { get; set; }
			public bool IsOnlyStateProvinceFlag { get; set; }
			public string Name { get; set; }
			public string rowguid { get; set; }
			public EntityCollection<CountryRegion> CountryRegion { get; private set; }
			public EntityCollection<SalesTerritory> SalesTerritory { get; private set; }

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

		#region Members for interface IStateProvince

		public string StateProvinceCode { get { LazyGet(); return InnerData.StateProvinceCode; } set { if (LazySet(Members.StateProvinceCode, InnerData.StateProvinceCode, value)) InnerData.StateProvinceCode = value; } }
		public string CountryRegionCode { get { LazyGet(); return InnerData.CountryRegionCode; } set { if (LazySet(Members.CountryRegionCode, InnerData.CountryRegionCode, value)) InnerData.CountryRegionCode = value; } }
		public bool IsOnlyStateProvinceFlag { get { LazyGet(); return InnerData.IsOnlyStateProvinceFlag; } set { if (LazySet(Members.IsOnlyStateProvinceFlag, InnerData.IsOnlyStateProvinceFlag, value)) InnerData.IsOnlyStateProvinceFlag = value; } }
		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }
		public EntityCollection<CountryRegion> CountryRegion { get { return InnerData.CountryRegion; } }
		public SalesTerritory SalesTerritory
		{
			get { return ((ILookupHelper<SalesTerritory>)InnerData.SalesTerritory).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.SalesTerritory, ((ILookupHelper<SalesTerritory>)InnerData.SalesTerritory).GetItem(null), value))
					((ILookupHelper<SalesTerritory>)InnerData.SalesTerritory).SetItem(value, null); 
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

		private static StateProvinceMembers members = null;
		public static StateProvinceMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(StateProvince))
					{
						if (members is null)
							members = new StateProvinceMembers();
					}
				}
				return members;
			}
		}
		public class StateProvinceMembers
		{
			internal StateProvinceMembers() { }

			#region Members for interface IStateProvince

			public Property StateProvinceCode { get; } = Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["StateProvinceCode"];
			public Property CountryRegionCode { get; } = Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["CountryRegionCode"];
			public Property IsOnlyStateProvinceFlag { get; } = Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["IsOnlyStateProvinceFlag"];
			public Property Name { get; } = Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["Name"];
			public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["rowguid"];
			public Property CountryRegion { get; } = Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["CountryRegion"];
			public Property SalesTerritory { get; } = Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["SalesTerritory"];
			#endregion

			#region Members for interface ISchemaBase

			public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		private static StateProvinceFullTextMembers fullTextMembers = null;
		public static StateProvinceFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(StateProvince))
					{
						if (fullTextMembers is null)
							fullTextMembers = new StateProvinceFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class StateProvinceFullTextMembers
		{
			internal StateProvinceFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(StateProvince))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["StateProvince"];
				}
			}
			return entity;
		}

		private static StateProvinceEvents events = null;
		public static StateProvinceEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(StateProvince))
					{
						if (events is null)
							events = new StateProvinceEvents();
					}
				}
				return events;
			}
		}
		public class StateProvinceEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<StateProvince, EntityEventArgs> onNew;
			public event EventHandler<StateProvince, EntityEventArgs> OnNew
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
				EventHandler<StateProvince, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((StateProvince)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<StateProvince, EntityEventArgs> onDelete;
			public event EventHandler<StateProvince, EntityEventArgs> OnDelete
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
				EventHandler<StateProvince, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((StateProvince)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<StateProvince, EntityEventArgs> onSave;
			public event EventHandler<StateProvince, EntityEventArgs> OnSave
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
				EventHandler<StateProvince, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((StateProvince)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<StateProvince, EntityEventArgs> onAfterSave;
			public event EventHandler<StateProvince, EntityEventArgs> OnAfterSave
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
				EventHandler<StateProvince, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((StateProvince)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnStateProvinceCode

				private static bool onStateProvinceCodeIsRegistered = false;

				private static EventHandler<StateProvince, PropertyEventArgs> onStateProvinceCode;
				public static event EventHandler<StateProvince, PropertyEventArgs> OnStateProvinceCode
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onStateProvinceCodeIsRegistered)
							{
								Members.StateProvinceCode.Events.OnChange -= onStateProvinceCodeProxy;
								Members.StateProvinceCode.Events.OnChange += onStateProvinceCodeProxy;
								onStateProvinceCodeIsRegistered = true;
							}
							onStateProvinceCode += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onStateProvinceCode -= value;
							if (onStateProvinceCode is null && onStateProvinceCodeIsRegistered)
							{
								Members.StateProvinceCode.Events.OnChange -= onStateProvinceCodeProxy;
								onStateProvinceCodeIsRegistered = false;
							}
						}
					}
				}
			
				private static void onStateProvinceCodeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<StateProvince, PropertyEventArgs> handler = onStateProvinceCode;
					if (handler is not null)
						handler.Invoke((StateProvince)sender, args);
				}

				#endregion

				#region OnCountryRegionCode

				private static bool onCountryRegionCodeIsRegistered = false;

				private static EventHandler<StateProvince, PropertyEventArgs> onCountryRegionCode;
				public static event EventHandler<StateProvince, PropertyEventArgs> OnCountryRegionCode
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCountryRegionCodeIsRegistered)
							{
								Members.CountryRegionCode.Events.OnChange -= onCountryRegionCodeProxy;
								Members.CountryRegionCode.Events.OnChange += onCountryRegionCodeProxy;
								onCountryRegionCodeIsRegistered = true;
							}
							onCountryRegionCode += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCountryRegionCode -= value;
							if (onCountryRegionCode is null && onCountryRegionCodeIsRegistered)
							{
								Members.CountryRegionCode.Events.OnChange -= onCountryRegionCodeProxy;
								onCountryRegionCodeIsRegistered = false;
							}
						}
					}
				}
			
				private static void onCountryRegionCodeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<StateProvince, PropertyEventArgs> handler = onCountryRegionCode;
					if (handler is not null)
						handler.Invoke((StateProvince)sender, args);
				}

				#endregion

				#region OnIsOnlyStateProvinceFlag

				private static bool onIsOnlyStateProvinceFlagIsRegistered = false;

				private static EventHandler<StateProvince, PropertyEventArgs> onIsOnlyStateProvinceFlag;
				public static event EventHandler<StateProvince, PropertyEventArgs> OnIsOnlyStateProvinceFlag
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onIsOnlyStateProvinceFlagIsRegistered)
							{
								Members.IsOnlyStateProvinceFlag.Events.OnChange -= onIsOnlyStateProvinceFlagProxy;
								Members.IsOnlyStateProvinceFlag.Events.OnChange += onIsOnlyStateProvinceFlagProxy;
								onIsOnlyStateProvinceFlagIsRegistered = true;
							}
							onIsOnlyStateProvinceFlag += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onIsOnlyStateProvinceFlag -= value;
							if (onIsOnlyStateProvinceFlag is null && onIsOnlyStateProvinceFlagIsRegistered)
							{
								Members.IsOnlyStateProvinceFlag.Events.OnChange -= onIsOnlyStateProvinceFlagProxy;
								onIsOnlyStateProvinceFlagIsRegistered = false;
							}
						}
					}
				}
			
				private static void onIsOnlyStateProvinceFlagProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<StateProvince, PropertyEventArgs> handler = onIsOnlyStateProvinceFlag;
					if (handler is not null)
						handler.Invoke((StateProvince)sender, args);
				}

				#endregion

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<StateProvince, PropertyEventArgs> onName;
				public static event EventHandler<StateProvince, PropertyEventArgs> OnName
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
					EventHandler<StateProvince, PropertyEventArgs> handler = onName;
					if (handler is not null)
						handler.Invoke((StateProvince)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<StateProvince, PropertyEventArgs> onrowguid;
				public static event EventHandler<StateProvince, PropertyEventArgs> Onrowguid
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
					EventHandler<StateProvince, PropertyEventArgs> handler = onrowguid;
					if (handler is not null)
						handler.Invoke((StateProvince)sender, args);
				}

				#endregion

				#region OnCountryRegion

				private static bool onCountryRegionIsRegistered = false;

				private static EventHandler<StateProvince, PropertyEventArgs> onCountryRegion;
				public static event EventHandler<StateProvince, PropertyEventArgs> OnCountryRegion
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCountryRegionIsRegistered)
							{
								Members.CountryRegion.Events.OnChange -= onCountryRegionProxy;
								Members.CountryRegion.Events.OnChange += onCountryRegionProxy;
								onCountryRegionIsRegistered = true;
							}
							onCountryRegion += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCountryRegion -= value;
							if (onCountryRegion is null && onCountryRegionIsRegistered)
							{
								Members.CountryRegion.Events.OnChange -= onCountryRegionProxy;
								onCountryRegionIsRegistered = false;
							}
						}
					}
				}
			
				private static void onCountryRegionProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<StateProvince, PropertyEventArgs> handler = onCountryRegion;
					if (handler is not null)
						handler.Invoke((StateProvince)sender, args);
				}

				#endregion

				#region OnSalesTerritory

				private static bool onSalesTerritoryIsRegistered = false;

				private static EventHandler<StateProvince, PropertyEventArgs> onSalesTerritory;
				public static event EventHandler<StateProvince, PropertyEventArgs> OnSalesTerritory
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
							if (onSalesTerritory is null && onSalesTerritoryIsRegistered)
							{
								Members.SalesTerritory.Events.OnChange -= onSalesTerritoryProxy;
								onSalesTerritoryIsRegistered = false;
							}
						}
					}
				}
			
				private static void onSalesTerritoryProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<StateProvince, PropertyEventArgs> handler = onSalesTerritory;
					if (handler is not null)
						handler.Invoke((StateProvince)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<StateProvince, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<StateProvince, PropertyEventArgs> OnModifiedDate
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
					EventHandler<StateProvince, PropertyEventArgs> handler = onModifiedDate;
					if (handler is not null)
						handler.Invoke((StateProvince)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<StateProvince, PropertyEventArgs> onUid;
				public static event EventHandler<StateProvince, PropertyEventArgs> OnUid
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
					EventHandler<StateProvince, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((StateProvince)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region IStateProvinceOriginalData

		public IStateProvinceOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IStateProvince

		string IStateProvinceOriginalData.StateProvinceCode { get { return OriginalData.StateProvinceCode; } }
		string IStateProvinceOriginalData.CountryRegionCode { get { return OriginalData.CountryRegionCode; } }
		bool IStateProvinceOriginalData.IsOnlyStateProvinceFlag { get { return OriginalData.IsOnlyStateProvinceFlag; } }
		string IStateProvinceOriginalData.Name { get { return OriginalData.Name; } }
		string IStateProvinceOriginalData.rowguid { get { return OriginalData.rowguid; } }
		IEnumerable<CountryRegion> IStateProvinceOriginalData.CountryRegion { get { return OriginalData.CountryRegion.OriginalData; } }
		SalesTerritory IStateProvinceOriginalData.SalesTerritory { get { return ((ILookupHelper<SalesTerritory>)OriginalData.SalesTerritory).GetOriginalItem(null); } }

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