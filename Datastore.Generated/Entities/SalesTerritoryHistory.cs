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
	public interface ISalesTerritoryHistoryOriginalData : ISchemaBaseOriginalData
	{
		System.DateTime StartDate { get; }
		System.DateTime? EndDate { get; }
		string rowguid { get; }
	}

	public partial class SalesTerritoryHistory : OGM<SalesTerritoryHistory, SalesTerritoryHistory.SalesTerritoryHistoryData, System.String>, ISchemaBase, INeo4jBase, ISalesTerritoryHistoryOriginalData
	{
		#region Initialize

		static SalesTerritoryHistory()
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

		public static Dictionary<System.String, SalesTerritoryHistory> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.SalesTerritoryHistoryAlias, IWhereQuery> query)
		{
			q.SalesTerritoryHistoryAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.SalesTerritoryHistory.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"SalesTerritoryHistory => StartDate : {this.StartDate}, EndDate : {this.EndDate?.ToString() ?? "null"}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
					OriginalData = new SalesTerritoryHistoryData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.rowguid is null)
				throw new PersistenceException(string.Format("Cannot save SalesTerritoryHistory with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class SalesTerritoryHistoryData : Data<System.String>
		{
			public SalesTerritoryHistoryData()
			{

			}

			public SalesTerritoryHistoryData(SalesTerritoryHistoryData data)
			{
				StartDate = data.StartDate;
				EndDate = data.EndDate;
				rowguid = data.rowguid;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "SalesTerritoryHistory";

			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("StartDate",  Conversion<System.DateTime, long>.Convert(StartDate));
				dictionary.Add("EndDate",  Conversion<System.DateTime?, long?>.Convert(EndDate));
				dictionary.Add("rowguid",  rowguid);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("StartDate", out value))
					StartDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("EndDate", out value))
					EndDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface ISalesTerritoryHistory

			public System.DateTime StartDate { get; set; }
			public System.DateTime? EndDate { get; set; }
			public string rowguid { get; set; }

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

		#region Members for interface ISalesTerritoryHistory

		public System.DateTime StartDate { get { LazyGet(); return InnerData.StartDate; } set { if (LazySet(Members.StartDate, InnerData.StartDate, value)) InnerData.StartDate = value; } }
		public System.DateTime? EndDate { get { LazyGet(); return InnerData.EndDate; } set { if (LazySet(Members.EndDate, InnerData.EndDate, value)) InnerData.EndDate = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }

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

		private static SalesTerritoryHistoryMembers members = null;
		public static SalesTerritoryHistoryMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(SalesTerritoryHistory))
					{
						if (members is null)
							members = new SalesTerritoryHistoryMembers();
					}
				}
				return members;
			}
		}
		public class SalesTerritoryHistoryMembers
		{
			internal SalesTerritoryHistoryMembers() { }

			#region Members for interface ISalesTerritoryHistory

			public Property StartDate { get; } = Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"].Properties["StartDate"];
			public Property EndDate { get; } = Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"].Properties["EndDate"];
			public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"].Properties["rowguid"];
			#endregion

			#region Members for interface ISchemaBase

			public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		private static SalesTerritoryHistoryFullTextMembers fullTextMembers = null;
		public static SalesTerritoryHistoryFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(SalesTerritoryHistory))
					{
						if (fullTextMembers is null)
							fullTextMembers = new SalesTerritoryHistoryFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class SalesTerritoryHistoryFullTextMembers
		{
			internal SalesTerritoryHistoryFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(SalesTerritoryHistory))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"];
				}
			}
			return entity;
		}

		private static SalesTerritoryHistoryEvents events = null;
		public static SalesTerritoryHistoryEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(SalesTerritoryHistory))
					{
						if (events is null)
							events = new SalesTerritoryHistoryEvents();
					}
				}
				return events;
			}
		}
		public class SalesTerritoryHistoryEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<SalesTerritoryHistory, EntityEventArgs> onNew;
			public event EventHandler<SalesTerritoryHistory, EntityEventArgs> OnNew
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
				EventHandler<SalesTerritoryHistory, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((SalesTerritoryHistory)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<SalesTerritoryHistory, EntityEventArgs> onDelete;
			public event EventHandler<SalesTerritoryHistory, EntityEventArgs> OnDelete
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
				EventHandler<SalesTerritoryHistory, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((SalesTerritoryHistory)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<SalesTerritoryHistory, EntityEventArgs> onSave;
			public event EventHandler<SalesTerritoryHistory, EntityEventArgs> OnSave
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
				EventHandler<SalesTerritoryHistory, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((SalesTerritoryHistory)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<SalesTerritoryHistory, EntityEventArgs> onAfterSave;
			public event EventHandler<SalesTerritoryHistory, EntityEventArgs> OnAfterSave
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
				EventHandler<SalesTerritoryHistory, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((SalesTerritoryHistory)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnStartDate

				private static bool onStartDateIsRegistered = false;

				private static EventHandler<SalesTerritoryHistory, PropertyEventArgs> onStartDate;
				public static event EventHandler<SalesTerritoryHistory, PropertyEventArgs> OnStartDate
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
							if (onStartDate is null && onStartDateIsRegistered)
							{
								Members.StartDate.Events.OnChange -= onStartDateProxy;
								onStartDateIsRegistered = false;
							}
						}
					}
				}
			
				private static void onStartDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesTerritoryHistory, PropertyEventArgs> handler = onStartDate;
					if (handler is not null)
						handler.Invoke((SalesTerritoryHistory)sender, args);
				}

				#endregion

				#region OnEndDate

				private static bool onEndDateIsRegistered = false;

				private static EventHandler<SalesTerritoryHistory, PropertyEventArgs> onEndDate;
				public static event EventHandler<SalesTerritoryHistory, PropertyEventArgs> OnEndDate
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
							if (onEndDate is null && onEndDateIsRegistered)
							{
								Members.EndDate.Events.OnChange -= onEndDateProxy;
								onEndDateIsRegistered = false;
							}
						}
					}
				}
			
				private static void onEndDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesTerritoryHistory, PropertyEventArgs> handler = onEndDate;
					if (handler is not null)
						handler.Invoke((SalesTerritoryHistory)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<SalesTerritoryHistory, PropertyEventArgs> onrowguid;
				public static event EventHandler<SalesTerritoryHistory, PropertyEventArgs> Onrowguid
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
					EventHandler<SalesTerritoryHistory, PropertyEventArgs> handler = onrowguid;
					if (handler is not null)
						handler.Invoke((SalesTerritoryHistory)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<SalesTerritoryHistory, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<SalesTerritoryHistory, PropertyEventArgs> OnModifiedDate
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
					EventHandler<SalesTerritoryHistory, PropertyEventArgs> handler = onModifiedDate;
					if (handler is not null)
						handler.Invoke((SalesTerritoryHistory)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<SalesTerritoryHistory, PropertyEventArgs> onUid;
				public static event EventHandler<SalesTerritoryHistory, PropertyEventArgs> OnUid
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
					EventHandler<SalesTerritoryHistory, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((SalesTerritoryHistory)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region ISalesTerritoryHistoryOriginalData

		public ISalesTerritoryHistoryOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ISalesTerritoryHistory

		System.DateTime ISalesTerritoryHistoryOriginalData.StartDate { get { return OriginalData.StartDate; } }
		System.DateTime? ISalesTerritoryHistoryOriginalData.EndDate { get { return OriginalData.EndDate; } }
		string ISalesTerritoryHistoryOriginalData.rowguid { get { return OriginalData.rowguid; } }

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