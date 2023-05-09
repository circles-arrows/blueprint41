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
	public interface ISalesTerritoryOriginalData : ISchemaBaseOriginalData
	{
		string Name { get; }
		string CountryRegionCode { get; }
		string Group { get; }
		string SalesYTD { get; }
		string SalesLastYear { get; }
		string CostYTD { get; }
		string CostLastYear { get; }
		string rowguid { get; }
		SalesTerritoryHistory SalesTerritoryHistory { get; }
	}

	public partial class SalesTerritory : OGM<SalesTerritory, SalesTerritory.SalesTerritoryData, System.String>, ISchemaBase, INeo4jBase, ISalesTerritoryOriginalData
	{
		#region Initialize

		static SalesTerritory()
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

		public static Dictionary<System.String, SalesTerritory> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.SalesTerritoryAlias, IWhereQuery> query)
		{
			q.SalesTerritoryAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.SalesTerritory.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"SalesTerritory => Name : {this.Name}, CountryRegionCode : {this.CountryRegionCode}, Group : {this.Group}, SalesYTD : {this.SalesYTD}, SalesLastYear : {this.SalesLastYear}, CostYTD : {this.CostYTD}, CostLastYear : {this.CostLastYear}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
					OriginalData = new SalesTerritoryData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.Name is null)
				throw new PersistenceException(string.Format("Cannot save SalesTerritory with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.CountryRegionCode is null)
				throw new PersistenceException(string.Format("Cannot save SalesTerritory with key '{0}' because the CountryRegionCode cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Group is null)
				throw new PersistenceException(string.Format("Cannot save SalesTerritory with key '{0}' because the Group cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.SalesYTD is null)
				throw new PersistenceException(string.Format("Cannot save SalesTerritory with key '{0}' because the SalesYTD cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.SalesLastYear is null)
				throw new PersistenceException(string.Format("Cannot save SalesTerritory with key '{0}' because the SalesLastYear cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.CostYTD is null)
				throw new PersistenceException(string.Format("Cannot save SalesTerritory with key '{0}' because the CostYTD cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.CostLastYear is null)
				throw new PersistenceException(string.Format("Cannot save SalesTerritory with key '{0}' because the CostLastYear cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid is null)
				throw new PersistenceException(string.Format("Cannot save SalesTerritory with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class SalesTerritoryData : Data<System.String>
		{
			public SalesTerritoryData()
			{

			}

			public SalesTerritoryData(SalesTerritoryData data)
			{
				Name = data.Name;
				CountryRegionCode = data.CountryRegionCode;
				Group = data.Group;
				SalesYTD = data.SalesYTD;
				SalesLastYear = data.SalesLastYear;
				CostYTD = data.CostYTD;
				CostLastYear = data.CostLastYear;
				rowguid = data.rowguid;
				SalesTerritoryHistory = data.SalesTerritoryHistory;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "SalesTerritory";

				SalesTerritoryHistory = new EntityCollection<SalesTerritoryHistory>(Wrapper, Members.SalesTerritoryHistory);
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
				dictionary.Add("CountryRegionCode",  CountryRegionCode);
				dictionary.Add("Group",  Group);
				dictionary.Add("SalesYTD",  SalesYTD);
				dictionary.Add("SalesLastYear",  SalesLastYear);
				dictionary.Add("CostYTD",  CostYTD);
				dictionary.Add("CostLastYear",  CostLastYear);
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
				if (properties.TryGetValue("CountryRegionCode", out value))
					CountryRegionCode = (string)value;
				if (properties.TryGetValue("Group", out value))
					Group = (string)value;
				if (properties.TryGetValue("SalesYTD", out value))
					SalesYTD = (string)value;
				if (properties.TryGetValue("SalesLastYear", out value))
					SalesLastYear = (string)value;
				if (properties.TryGetValue("CostYTD", out value))
					CostYTD = (string)value;
				if (properties.TryGetValue("CostLastYear", out value))
					CostLastYear = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface ISalesTerritory

			public string Name { get; set; }
			public string CountryRegionCode { get; set; }
			public string Group { get; set; }
			public string SalesYTD { get; set; }
			public string SalesLastYear { get; set; }
			public string CostYTD { get; set; }
			public string CostLastYear { get; set; }
			public string rowguid { get; set; }
			public EntityCollection<SalesTerritoryHistory> SalesTerritoryHistory { get; private set; }

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

		#region Members for interface ISalesTerritory

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
		public string CountryRegionCode { get { LazyGet(); return InnerData.CountryRegionCode; } set { if (LazySet(Members.CountryRegionCode, InnerData.CountryRegionCode, value)) InnerData.CountryRegionCode = value; } }
		public string Group { get { LazyGet(); return InnerData.Group; } set { if (LazySet(Members.Group, InnerData.Group, value)) InnerData.Group = value; } }
		public string SalesYTD { get { LazyGet(); return InnerData.SalesYTD; } set { if (LazySet(Members.SalesYTD, InnerData.SalesYTD, value)) InnerData.SalesYTD = value; } }
		public string SalesLastYear { get { LazyGet(); return InnerData.SalesLastYear; } set { if (LazySet(Members.SalesLastYear, InnerData.SalesLastYear, value)) InnerData.SalesLastYear = value; } }
		public string CostYTD { get { LazyGet(); return InnerData.CostYTD; } set { if (LazySet(Members.CostYTD, InnerData.CostYTD, value)) InnerData.CostYTD = value; } }
		public string CostLastYear { get { LazyGet(); return InnerData.CostLastYear; } set { if (LazySet(Members.CostLastYear, InnerData.CostLastYear, value)) InnerData.CostLastYear = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }
		public SalesTerritoryHistory SalesTerritoryHistory
		{
			get { return ((ILookupHelper<SalesTerritoryHistory>)InnerData.SalesTerritoryHistory).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.SalesTerritoryHistory, ((ILookupHelper<SalesTerritoryHistory>)InnerData.SalesTerritoryHistory).GetItem(null), value))
					((ILookupHelper<SalesTerritoryHistory>)InnerData.SalesTerritoryHistory).SetItem(value, null); 
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

		private static SalesTerritoryMembers members = null;
		public static SalesTerritoryMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(SalesTerritory))
					{
						if (members is null)
							members = new SalesTerritoryMembers();
					}
				}
				return members;
			}
		}
		public class SalesTerritoryMembers
		{
			internal SalesTerritoryMembers() { }

			#region Members for interface ISalesTerritory

			public Property Name { get; } = Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["Name"];
			public Property CountryRegionCode { get; } = Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["CountryRegionCode"];
			public Property Group { get; } = Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["Group"];
			public Property SalesYTD { get; } = Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["SalesYTD"];
			public Property SalesLastYear { get; } = Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["SalesLastYear"];
			public Property CostYTD { get; } = Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["CostYTD"];
			public Property CostLastYear { get; } = Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["CostLastYear"];
			public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["rowguid"];
			public Property SalesTerritoryHistory { get; } = Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["SalesTerritoryHistory"];
			#endregion

			#region Members for interface ISchemaBase

			public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		private static SalesTerritoryFullTextMembers fullTextMembers = null;
		public static SalesTerritoryFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(SalesTerritory))
					{
						if (fullTextMembers is null)
							fullTextMembers = new SalesTerritoryFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class SalesTerritoryFullTextMembers
		{
			internal SalesTerritoryFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(SalesTerritory))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["SalesTerritory"];
				}
			}
			return entity;
		}

		private static SalesTerritoryEvents events = null;
		public static SalesTerritoryEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(SalesTerritory))
					{
						if (events is null)
							events = new SalesTerritoryEvents();
					}
				}
				return events;
			}
		}
		public class SalesTerritoryEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<SalesTerritory, EntityEventArgs> onNew;
			public event EventHandler<SalesTerritory, EntityEventArgs> OnNew
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
				EventHandler<SalesTerritory, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((SalesTerritory)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<SalesTerritory, EntityEventArgs> onDelete;
			public event EventHandler<SalesTerritory, EntityEventArgs> OnDelete
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
				EventHandler<SalesTerritory, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((SalesTerritory)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<SalesTerritory, EntityEventArgs> onSave;
			public event EventHandler<SalesTerritory, EntityEventArgs> OnSave
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
				EventHandler<SalesTerritory, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((SalesTerritory)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<SalesTerritory, EntityEventArgs> onAfterSave;
			public event EventHandler<SalesTerritory, EntityEventArgs> OnAfterSave
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
				EventHandler<SalesTerritory, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((SalesTerritory)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<SalesTerritory, PropertyEventArgs> onName;
				public static event EventHandler<SalesTerritory, PropertyEventArgs> OnName
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
					EventHandler<SalesTerritory, PropertyEventArgs> handler = onName;
					if (handler is not null)
						handler.Invoke((SalesTerritory)sender, args);
				}

				#endregion

				#region OnCountryRegionCode

				private static bool onCountryRegionCodeIsRegistered = false;

				private static EventHandler<SalesTerritory, PropertyEventArgs> onCountryRegionCode;
				public static event EventHandler<SalesTerritory, PropertyEventArgs> OnCountryRegionCode
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
					EventHandler<SalesTerritory, PropertyEventArgs> handler = onCountryRegionCode;
					if (handler is not null)
						handler.Invoke((SalesTerritory)sender, args);
				}

				#endregion

				#region OnGroup

				private static bool onGroupIsRegistered = false;

				private static EventHandler<SalesTerritory, PropertyEventArgs> onGroup;
				public static event EventHandler<SalesTerritory, PropertyEventArgs> OnGroup
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onGroupIsRegistered)
							{
								Members.Group.Events.OnChange -= onGroupProxy;
								Members.Group.Events.OnChange += onGroupProxy;
								onGroupIsRegistered = true;
							}
							onGroup += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onGroup -= value;
							if (onGroup is null && onGroupIsRegistered)
							{
								Members.Group.Events.OnChange -= onGroupProxy;
								onGroupIsRegistered = false;
							}
						}
					}
				}
			
				private static void onGroupProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesTerritory, PropertyEventArgs> handler = onGroup;
					if (handler is not null)
						handler.Invoke((SalesTerritory)sender, args);
				}

				#endregion

				#region OnSalesYTD

				private static bool onSalesYTDIsRegistered = false;

				private static EventHandler<SalesTerritory, PropertyEventArgs> onSalesYTD;
				public static event EventHandler<SalesTerritory, PropertyEventArgs> OnSalesYTD
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalesYTDIsRegistered)
							{
								Members.SalesYTD.Events.OnChange -= onSalesYTDProxy;
								Members.SalesYTD.Events.OnChange += onSalesYTDProxy;
								onSalesYTDIsRegistered = true;
							}
							onSalesYTD += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalesYTD -= value;
							if (onSalesYTD is null && onSalesYTDIsRegistered)
							{
								Members.SalesYTD.Events.OnChange -= onSalesYTDProxy;
								onSalesYTDIsRegistered = false;
							}
						}
					}
				}
			
				private static void onSalesYTDProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesTerritory, PropertyEventArgs> handler = onSalesYTD;
					if (handler is not null)
						handler.Invoke((SalesTerritory)sender, args);
				}

				#endregion

				#region OnSalesLastYear

				private static bool onSalesLastYearIsRegistered = false;

				private static EventHandler<SalesTerritory, PropertyEventArgs> onSalesLastYear;
				public static event EventHandler<SalesTerritory, PropertyEventArgs> OnSalesLastYear
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalesLastYearIsRegistered)
							{
								Members.SalesLastYear.Events.OnChange -= onSalesLastYearProxy;
								Members.SalesLastYear.Events.OnChange += onSalesLastYearProxy;
								onSalesLastYearIsRegistered = true;
							}
							onSalesLastYear += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalesLastYear -= value;
							if (onSalesLastYear is null && onSalesLastYearIsRegistered)
							{
								Members.SalesLastYear.Events.OnChange -= onSalesLastYearProxy;
								onSalesLastYearIsRegistered = false;
							}
						}
					}
				}
			
				private static void onSalesLastYearProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesTerritory, PropertyEventArgs> handler = onSalesLastYear;
					if (handler is not null)
						handler.Invoke((SalesTerritory)sender, args);
				}

				#endregion

				#region OnCostYTD

				private static bool onCostYTDIsRegistered = false;

				private static EventHandler<SalesTerritory, PropertyEventArgs> onCostYTD;
				public static event EventHandler<SalesTerritory, PropertyEventArgs> OnCostYTD
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCostYTDIsRegistered)
							{
								Members.CostYTD.Events.OnChange -= onCostYTDProxy;
								Members.CostYTD.Events.OnChange += onCostYTDProxy;
								onCostYTDIsRegistered = true;
							}
							onCostYTD += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCostYTD -= value;
							if (onCostYTD is null && onCostYTDIsRegistered)
							{
								Members.CostYTD.Events.OnChange -= onCostYTDProxy;
								onCostYTDIsRegistered = false;
							}
						}
					}
				}
			
				private static void onCostYTDProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesTerritory, PropertyEventArgs> handler = onCostYTD;
					if (handler is not null)
						handler.Invoke((SalesTerritory)sender, args);
				}

				#endregion

				#region OnCostLastYear

				private static bool onCostLastYearIsRegistered = false;

				private static EventHandler<SalesTerritory, PropertyEventArgs> onCostLastYear;
				public static event EventHandler<SalesTerritory, PropertyEventArgs> OnCostLastYear
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCostLastYearIsRegistered)
							{
								Members.CostLastYear.Events.OnChange -= onCostLastYearProxy;
								Members.CostLastYear.Events.OnChange += onCostLastYearProxy;
								onCostLastYearIsRegistered = true;
							}
							onCostLastYear += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCostLastYear -= value;
							if (onCostLastYear is null && onCostLastYearIsRegistered)
							{
								Members.CostLastYear.Events.OnChange -= onCostLastYearProxy;
								onCostLastYearIsRegistered = false;
							}
						}
					}
				}
			
				private static void onCostLastYearProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesTerritory, PropertyEventArgs> handler = onCostLastYear;
					if (handler is not null)
						handler.Invoke((SalesTerritory)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<SalesTerritory, PropertyEventArgs> onrowguid;
				public static event EventHandler<SalesTerritory, PropertyEventArgs> Onrowguid
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
					EventHandler<SalesTerritory, PropertyEventArgs> handler = onrowguid;
					if (handler is not null)
						handler.Invoke((SalesTerritory)sender, args);
				}

				#endregion

				#region OnSalesTerritoryHistory

				private static bool onSalesTerritoryHistoryIsRegistered = false;

				private static EventHandler<SalesTerritory, PropertyEventArgs> onSalesTerritoryHistory;
				public static event EventHandler<SalesTerritory, PropertyEventArgs> OnSalesTerritoryHistory
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalesTerritoryHistoryIsRegistered)
							{
								Members.SalesTerritoryHistory.Events.OnChange -= onSalesTerritoryHistoryProxy;
								Members.SalesTerritoryHistory.Events.OnChange += onSalesTerritoryHistoryProxy;
								onSalesTerritoryHistoryIsRegistered = true;
							}
							onSalesTerritoryHistory += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalesTerritoryHistory -= value;
							if (onSalesTerritoryHistory is null && onSalesTerritoryHistoryIsRegistered)
							{
								Members.SalesTerritoryHistory.Events.OnChange -= onSalesTerritoryHistoryProxy;
								onSalesTerritoryHistoryIsRegistered = false;
							}
						}
					}
				}
			
				private static void onSalesTerritoryHistoryProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesTerritory, PropertyEventArgs> handler = onSalesTerritoryHistory;
					if (handler is not null)
						handler.Invoke((SalesTerritory)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<SalesTerritory, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<SalesTerritory, PropertyEventArgs> OnModifiedDate
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
					EventHandler<SalesTerritory, PropertyEventArgs> handler = onModifiedDate;
					if (handler is not null)
						handler.Invoke((SalesTerritory)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<SalesTerritory, PropertyEventArgs> onUid;
				public static event EventHandler<SalesTerritory, PropertyEventArgs> OnUid
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
					EventHandler<SalesTerritory, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((SalesTerritory)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region ISalesTerritoryOriginalData

		public ISalesTerritoryOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ISalesTerritory

		string ISalesTerritoryOriginalData.Name { get { return OriginalData.Name; } }
		string ISalesTerritoryOriginalData.CountryRegionCode { get { return OriginalData.CountryRegionCode; } }
		string ISalesTerritoryOriginalData.Group { get { return OriginalData.Group; } }
		string ISalesTerritoryOriginalData.SalesYTD { get { return OriginalData.SalesYTD; } }
		string ISalesTerritoryOriginalData.SalesLastYear { get { return OriginalData.SalesLastYear; } }
		string ISalesTerritoryOriginalData.CostYTD { get { return OriginalData.CostYTD; } }
		string ISalesTerritoryOriginalData.CostLastYear { get { return OriginalData.CostLastYear; } }
		string ISalesTerritoryOriginalData.rowguid { get { return OriginalData.rowguid; } }
		SalesTerritoryHistory ISalesTerritoryOriginalData.SalesTerritoryHistory { get { return ((ILookupHelper<SalesTerritoryHistory>)OriginalData.SalesTerritoryHistory).GetOriginalItem(null); } }

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