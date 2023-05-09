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
	public interface IEmployeePayHistoryOriginalData : ISchemaBaseOriginalData
	{
		System.DateTime RateChangeDate { get; }
		string Rate { get; }
		string PayFrequency { get; }
	}

	public partial class EmployeePayHistory : OGM<EmployeePayHistory, EmployeePayHistory.EmployeePayHistoryData, System.String>, ISchemaBase, INeo4jBase, IEmployeePayHistoryOriginalData
	{
		#region Initialize

		static EmployeePayHistory()
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

		public static Dictionary<System.String, EmployeePayHistory> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.EmployeePayHistoryAlias, IWhereQuery> query)
		{
			q.EmployeePayHistoryAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.EmployeePayHistory.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"EmployeePayHistory => RateChangeDate : {this.RateChangeDate}, Rate : {this.Rate}, PayFrequency : {this.PayFrequency}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
					OriginalData = new EmployeePayHistoryData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.Rate is null)
				throw new PersistenceException(string.Format("Cannot save EmployeePayHistory with key '{0}' because the Rate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.PayFrequency is null)
				throw new PersistenceException(string.Format("Cannot save EmployeePayHistory with key '{0}' because the PayFrequency cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class EmployeePayHistoryData : Data<System.String>
		{
			public EmployeePayHistoryData()
			{

			}

			public EmployeePayHistoryData(EmployeePayHistoryData data)
			{
				RateChangeDate = data.RateChangeDate;
				Rate = data.Rate;
				PayFrequency = data.PayFrequency;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "EmployeePayHistory";

			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("RateChangeDate",  Conversion<System.DateTime, long>.Convert(RateChangeDate));
				dictionary.Add("Rate",  Rate);
				dictionary.Add("PayFrequency",  PayFrequency);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("RateChangeDate", out value))
					RateChangeDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Rate", out value))
					Rate = (string)value;
				if (properties.TryGetValue("PayFrequency", out value))
					PayFrequency = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IEmployeePayHistory

			public System.DateTime RateChangeDate { get; set; }
			public string Rate { get; set; }
			public string PayFrequency { get; set; }

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

		#region Members for interface IEmployeePayHistory

		public System.DateTime RateChangeDate { get { LazyGet(); return InnerData.RateChangeDate; } set { if (LazySet(Members.RateChangeDate, InnerData.RateChangeDate, value)) InnerData.RateChangeDate = value; } }
		public string Rate { get { LazyGet(); return InnerData.Rate; } set { if (LazySet(Members.Rate, InnerData.Rate, value)) InnerData.Rate = value; } }
		public string PayFrequency { get { LazyGet(); return InnerData.PayFrequency; } set { if (LazySet(Members.PayFrequency, InnerData.PayFrequency, value)) InnerData.PayFrequency = value; } }

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

		private static EmployeePayHistoryMembers members = null;
		public static EmployeePayHistoryMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(EmployeePayHistory))
					{
						if (members is null)
							members = new EmployeePayHistoryMembers();
					}
				}
				return members;
			}
		}
		public class EmployeePayHistoryMembers
		{
			internal EmployeePayHistoryMembers() { }

			#region Members for interface IEmployeePayHistory

			public Property RateChangeDate { get; } = Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"].Properties["RateChangeDate"];
			public Property Rate { get; } = Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"].Properties["Rate"];
			public Property PayFrequency { get; } = Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"].Properties["PayFrequency"];
			#endregion

			#region Members for interface ISchemaBase

			public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		private static EmployeePayHistoryFullTextMembers fullTextMembers = null;
		public static EmployeePayHistoryFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(EmployeePayHistory))
					{
						if (fullTextMembers is null)
							fullTextMembers = new EmployeePayHistoryFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class EmployeePayHistoryFullTextMembers
		{
			internal EmployeePayHistoryFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(EmployeePayHistory))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"];
				}
			}
			return entity;
		}

		private static EmployeePayHistoryEvents events = null;
		public static EmployeePayHistoryEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(EmployeePayHistory))
					{
						if (events is null)
							events = new EmployeePayHistoryEvents();
					}
				}
				return events;
			}
		}
		public class EmployeePayHistoryEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<EmployeePayHistory, EntityEventArgs> onNew;
			public event EventHandler<EmployeePayHistory, EntityEventArgs> OnNew
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
				EventHandler<EmployeePayHistory, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((EmployeePayHistory)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<EmployeePayHistory, EntityEventArgs> onDelete;
			public event EventHandler<EmployeePayHistory, EntityEventArgs> OnDelete
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
				EventHandler<EmployeePayHistory, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((EmployeePayHistory)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<EmployeePayHistory, EntityEventArgs> onSave;
			public event EventHandler<EmployeePayHistory, EntityEventArgs> OnSave
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
				EventHandler<EmployeePayHistory, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((EmployeePayHistory)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<EmployeePayHistory, EntityEventArgs> onAfterSave;
			public event EventHandler<EmployeePayHistory, EntityEventArgs> OnAfterSave
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
				EventHandler<EmployeePayHistory, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((EmployeePayHistory)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnRateChangeDate

				private static bool onRateChangeDateIsRegistered = false;

				private static EventHandler<EmployeePayHistory, PropertyEventArgs> onRateChangeDate;
				public static event EventHandler<EmployeePayHistory, PropertyEventArgs> OnRateChangeDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onRateChangeDateIsRegistered)
							{
								Members.RateChangeDate.Events.OnChange -= onRateChangeDateProxy;
								Members.RateChangeDate.Events.OnChange += onRateChangeDateProxy;
								onRateChangeDateIsRegistered = true;
							}
							onRateChangeDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onRateChangeDate -= value;
							if (onRateChangeDate is null && onRateChangeDateIsRegistered)
							{
								Members.RateChangeDate.Events.OnChange -= onRateChangeDateProxy;
								onRateChangeDateIsRegistered = false;
							}
						}
					}
				}
			
				private static void onRateChangeDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<EmployeePayHistory, PropertyEventArgs> handler = onRateChangeDate;
					if (handler is not null)
						handler.Invoke((EmployeePayHistory)sender, args);
				}

				#endregion

				#region OnRate

				private static bool onRateIsRegistered = false;

				private static EventHandler<EmployeePayHistory, PropertyEventArgs> onRate;
				public static event EventHandler<EmployeePayHistory, PropertyEventArgs> OnRate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onRateIsRegistered)
							{
								Members.Rate.Events.OnChange -= onRateProxy;
								Members.Rate.Events.OnChange += onRateProxy;
								onRateIsRegistered = true;
							}
							onRate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onRate -= value;
							if (onRate is null && onRateIsRegistered)
							{
								Members.Rate.Events.OnChange -= onRateProxy;
								onRateIsRegistered = false;
							}
						}
					}
				}
			
				private static void onRateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<EmployeePayHistory, PropertyEventArgs> handler = onRate;
					if (handler is not null)
						handler.Invoke((EmployeePayHistory)sender, args);
				}

				#endregion

				#region OnPayFrequency

				private static bool onPayFrequencyIsRegistered = false;

				private static EventHandler<EmployeePayHistory, PropertyEventArgs> onPayFrequency;
				public static event EventHandler<EmployeePayHistory, PropertyEventArgs> OnPayFrequency
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPayFrequencyIsRegistered)
							{
								Members.PayFrequency.Events.OnChange -= onPayFrequencyProxy;
								Members.PayFrequency.Events.OnChange += onPayFrequencyProxy;
								onPayFrequencyIsRegistered = true;
							}
							onPayFrequency += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPayFrequency -= value;
							if (onPayFrequency is null && onPayFrequencyIsRegistered)
							{
								Members.PayFrequency.Events.OnChange -= onPayFrequencyProxy;
								onPayFrequencyIsRegistered = false;
							}
						}
					}
				}
			
				private static void onPayFrequencyProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<EmployeePayHistory, PropertyEventArgs> handler = onPayFrequency;
					if (handler is not null)
						handler.Invoke((EmployeePayHistory)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<EmployeePayHistory, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<EmployeePayHistory, PropertyEventArgs> OnModifiedDate
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
					EventHandler<EmployeePayHistory, PropertyEventArgs> handler = onModifiedDate;
					if (handler is not null)
						handler.Invoke((EmployeePayHistory)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<EmployeePayHistory, PropertyEventArgs> onUid;
				public static event EventHandler<EmployeePayHistory, PropertyEventArgs> OnUid
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
					EventHandler<EmployeePayHistory, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((EmployeePayHistory)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region IEmployeePayHistoryOriginalData

		public IEmployeePayHistoryOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IEmployeePayHistory

		System.DateTime IEmployeePayHistoryOriginalData.RateChangeDate { get { return OriginalData.RateChangeDate; } }
		string IEmployeePayHistoryOriginalData.Rate { get { return OriginalData.Rate; } }
		string IEmployeePayHistoryOriginalData.PayFrequency { get { return OriginalData.PayFrequency; } }

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