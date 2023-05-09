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
	public interface IShiftOriginalData : ISchemaBaseOriginalData
	{
		string Name { get; }
		System.DateTime StartTime { get; }
		System.DateTime EndTime { get; }
	}

	public partial class Shift : OGM<Shift, Shift.ShiftData, System.String>, ISchemaBase, INeo4jBase, IShiftOriginalData
	{
		#region Initialize

		static Shift()
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

		public static Dictionary<System.String, Shift> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.ShiftAlias, IWhereQuery> query)
		{
			q.ShiftAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Shift.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"Shift => Name : {this.Name}, StartTime : {this.StartTime}, EndTime : {this.EndTime}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
					OriginalData = new ShiftData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.Name is null)
				throw new PersistenceException(string.Format("Cannot save Shift with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class ShiftData : Data<System.String>
		{
			public ShiftData()
			{

			}

			public ShiftData(ShiftData data)
			{
				Name = data.Name;
				StartTime = data.StartTime;
				EndTime = data.EndTime;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Shift";

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
				dictionary.Add("StartTime",  Conversion<System.DateTime, long>.Convert(StartTime));
				dictionary.Add("EndTime",  Conversion<System.DateTime, long>.Convert(EndTime));
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("StartTime", out value))
					StartTime = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("EndTime", out value))
					EndTime = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IShift

			public string Name { get; set; }
			public System.DateTime StartTime { get; set; }
			public System.DateTime EndTime { get; set; }

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

		#region Members for interface IShift

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
		public System.DateTime StartTime { get { LazyGet(); return InnerData.StartTime; } set { if (LazySet(Members.StartTime, InnerData.StartTime, value)) InnerData.StartTime = value; } }
		public System.DateTime EndTime { get { LazyGet(); return InnerData.EndTime; } set { if (LazySet(Members.EndTime, InnerData.EndTime, value)) InnerData.EndTime = value; } }

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

		private static ShiftMembers members = null;
		public static ShiftMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(Shift))
					{
						if (members is null)
							members = new ShiftMembers();
					}
				}
				return members;
			}
		}
		public class ShiftMembers
		{
			internal ShiftMembers() { }

			#region Members for interface IShift

			public Property Name { get; } = Datastore.AdventureWorks.Model.Entities["Shift"].Properties["Name"];
			public Property StartTime { get; } = Datastore.AdventureWorks.Model.Entities["Shift"].Properties["StartTime"];
			public Property EndTime { get; } = Datastore.AdventureWorks.Model.Entities["Shift"].Properties["EndTime"];
			#endregion

			#region Members for interface ISchemaBase

			public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		private static ShiftFullTextMembers fullTextMembers = null;
		public static ShiftFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(Shift))
					{
						if (fullTextMembers is null)
							fullTextMembers = new ShiftFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class ShiftFullTextMembers
		{
			internal ShiftFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(Shift))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["Shift"];
				}
			}
			return entity;
		}

		private static ShiftEvents events = null;
		public static ShiftEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(Shift))
					{
						if (events is null)
							events = new ShiftEvents();
					}
				}
				return events;
			}
		}
		public class ShiftEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<Shift, EntityEventArgs> onNew;
			public event EventHandler<Shift, EntityEventArgs> OnNew
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
				EventHandler<Shift, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((Shift)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<Shift, EntityEventArgs> onDelete;
			public event EventHandler<Shift, EntityEventArgs> OnDelete
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
				EventHandler<Shift, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((Shift)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<Shift, EntityEventArgs> onSave;
			public event EventHandler<Shift, EntityEventArgs> OnSave
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
				EventHandler<Shift, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((Shift)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<Shift, EntityEventArgs> onAfterSave;
			public event EventHandler<Shift, EntityEventArgs> OnAfterSave
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
				EventHandler<Shift, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((Shift)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<Shift, PropertyEventArgs> onName;
				public static event EventHandler<Shift, PropertyEventArgs> OnName
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
					EventHandler<Shift, PropertyEventArgs> handler = onName;
					if (handler is not null)
						handler.Invoke((Shift)sender, args);
				}

				#endregion

				#region OnStartTime

				private static bool onStartTimeIsRegistered = false;

				private static EventHandler<Shift, PropertyEventArgs> onStartTime;
				public static event EventHandler<Shift, PropertyEventArgs> OnStartTime
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onStartTimeIsRegistered)
							{
								Members.StartTime.Events.OnChange -= onStartTimeProxy;
								Members.StartTime.Events.OnChange += onStartTimeProxy;
								onStartTimeIsRegistered = true;
							}
							onStartTime += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onStartTime -= value;
							if (onStartTime is null && onStartTimeIsRegistered)
							{
								Members.StartTime.Events.OnChange -= onStartTimeProxy;
								onStartTimeIsRegistered = false;
							}
						}
					}
				}
			
				private static void onStartTimeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Shift, PropertyEventArgs> handler = onStartTime;
					if (handler is not null)
						handler.Invoke((Shift)sender, args);
				}

				#endregion

				#region OnEndTime

				private static bool onEndTimeIsRegistered = false;

				private static EventHandler<Shift, PropertyEventArgs> onEndTime;
				public static event EventHandler<Shift, PropertyEventArgs> OnEndTime
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onEndTimeIsRegistered)
							{
								Members.EndTime.Events.OnChange -= onEndTimeProxy;
								Members.EndTime.Events.OnChange += onEndTimeProxy;
								onEndTimeIsRegistered = true;
							}
							onEndTime += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onEndTime -= value;
							if (onEndTime is null && onEndTimeIsRegistered)
							{
								Members.EndTime.Events.OnChange -= onEndTimeProxy;
								onEndTimeIsRegistered = false;
							}
						}
					}
				}
			
				private static void onEndTimeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Shift, PropertyEventArgs> handler = onEndTime;
					if (handler is not null)
						handler.Invoke((Shift)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<Shift, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<Shift, PropertyEventArgs> OnModifiedDate
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
					EventHandler<Shift, PropertyEventArgs> handler = onModifiedDate;
					if (handler is not null)
						handler.Invoke((Shift)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Shift, PropertyEventArgs> onUid;
				public static event EventHandler<Shift, PropertyEventArgs> OnUid
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
					EventHandler<Shift, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((Shift)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region IShiftOriginalData

		public IShiftOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IShift

		string IShiftOriginalData.Name { get { return OriginalData.Name; } }
		System.DateTime IShiftOriginalData.StartTime { get { return OriginalData.StartTime; } }
		System.DateTime IShiftOriginalData.EndTime { get { return OriginalData.EndTime; } }

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