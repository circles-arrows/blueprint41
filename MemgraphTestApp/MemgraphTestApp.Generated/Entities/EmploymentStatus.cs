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
	public interface IEmploymentStatusOriginalData : INeo4jBaseOriginalData
	{
		string Name { get; }
	}

	public partial class EmploymentStatus : OGM<EmploymentStatus, EmploymentStatus.EmploymentStatusData, System.String>, INeo4jBase, IEmploymentStatusOriginalData
	{
		#region Initialize

		static EmploymentStatus()
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

		public static Dictionary<System.String, EmploymentStatus> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.EmploymentStatusAlias, IWhereQuery> query)
		{
			q.EmploymentStatusAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.EmploymentStatus.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"EmploymentStatus => Name : {this.Name}, Uid : {this.Uid}, CreatedOn : {this.CreatedOn?.ToString() ?? "null"}, CreatedBy : {this.CreatedBy?.ToString() ?? "null"}, LastModifiedOn : {this.LastModifiedOn}, LastModifiedBy : {this.LastModifiedBy?.ToString() ?? "null"}, Description : {this.Description?.ToString() ?? "null"}";
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
					OriginalData = new EmploymentStatusData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.Name is null)
				throw new PersistenceException(string.Format("Cannot save EmploymentStatus with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Uid is null)
				throw new PersistenceException(string.Format("Cannot save EmploymentStatus with key '{0}' because the Uid cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class EmploymentStatusData : Data<System.String>
		{
			public EmploymentStatusData()
			{

			}

			public EmploymentStatusData(EmploymentStatusData data)
			{
				Name = data.Name;
				Uid = data.Uid;
				CreatedOn = data.CreatedOn;
				CreatedBy = data.CreatedBy;
				LastModifiedOn = data.LastModifiedOn;
				LastModifiedBy = data.LastModifiedBy;
				Description = data.Description;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "EmploymentStatus";

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
				dictionary.Add("Uid",  Uid);
				dictionary.Add("CreatedOn",  Conversion<System.DateTime?, long?>.Convert(CreatedOn));
				dictionary.Add("CreatedBy",  CreatedBy);
				dictionary.Add("LastModifiedOn",  Conversion<System.DateTime, long>.Convert(LastModifiedOn));
				dictionary.Add("LastModifiedBy",  LastModifiedBy);
				dictionary.Add("Description",  Description);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
				if (properties.TryGetValue("CreatedOn", out value))
					CreatedOn = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("CreatedBy", out value))
					CreatedBy = (string)value;
				if (properties.TryGetValue("LastModifiedOn", out value))
					LastModifiedOn = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("LastModifiedBy", out value))
					LastModifiedBy = (string)value;
				if (properties.TryGetValue("Description", out value))
					Description = (string)value;
			}

			#endregion

			#region Members for interface IEmploymentStatus

			public string Name { get; set; }

			#endregion
			#region Members for interface INeo4jBase

			public string Uid { get; set; }
			public System.DateTime? CreatedOn { get; set; }
			public string CreatedBy { get; set; }
			public System.DateTime LastModifiedOn { get; set; }
			public string LastModifiedBy { get; set; }
			public string Description { get; set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IEmploymentStatus

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }

		#endregion
		#region Members for interface INeo4jBase

		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }
		public System.DateTime? CreatedOn { get { LazyGet(); return InnerData.CreatedOn; } set { if (LazySet(Members.CreatedOn, InnerData.CreatedOn, value)) InnerData.CreatedOn = value; } }
		public string CreatedBy { get { LazyGet(); return InnerData.CreatedBy; } set { if (LazySet(Members.CreatedBy, InnerData.CreatedBy, value)) InnerData.CreatedBy = value; } }
		public System.DateTime LastModifiedOn { get { LazyGet(); return InnerData.LastModifiedOn; } set { if (LazySet(Members.LastModifiedOn, InnerData.LastModifiedOn, value)) InnerData.LastModifiedOn = value; } }
		protected override DateTime GetRowVersion() { return LastModifiedOn; }
		public override void SetRowVersion(DateTime? value) { LastModifiedOn = value ?? DateTime.MinValue; }
		public string LastModifiedBy { get { LazyGet(); return InnerData.LastModifiedBy; } set { if (LazySet(Members.LastModifiedBy, InnerData.LastModifiedBy, value)) InnerData.LastModifiedBy = value; } }
		public string Description { get { LazyGet(); return InnerData.Description; } set { if (LazySet(Members.Description, InnerData.Description, value)) InnerData.Description = value; } }

		#endregion

		#region Virtual Node Type
		
		public string NodeType  { get { return InnerData.NodeType; } }
		
		#endregion

		#endregion

		#region Reflection

		private static EmploymentStatusMembers members = null;
		public static EmploymentStatusMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(EmploymentStatus))
					{
						if (members is null)
							members = new EmploymentStatusMembers();
					}
				}
				return members;
			}
		}
		public class EmploymentStatusMembers
		{
			internal EmploymentStatusMembers() { }

			#region Members for interface IEmploymentStatus

			public Property Name { get; } = MemgraphTestApp.HumanResources.Model.Entities["EmploymentStatus"].Properties["Name"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["Uid"];
			public Property CreatedOn { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["CreatedOn"];
			public Property CreatedBy { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["CreatedBy"];
			public Property LastModifiedOn { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["LastModifiedOn"];
			public Property LastModifiedBy { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["LastModifiedBy"];
			public Property Description { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["Description"];
			#endregion

		}

		private static EmploymentStatusFullTextMembers fullTextMembers = null;
		public static EmploymentStatusFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(EmploymentStatus))
					{
						if (fullTextMembers is null)
							fullTextMembers = new EmploymentStatusFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class EmploymentStatusFullTextMembers
		{
			internal EmploymentStatusFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(EmploymentStatus))
				{
					if (entity is null)
						entity = MemgraphTestApp.HumanResources.Model.Entities["EmploymentStatus"];
				}
			}
			return entity;
		}

		private static EmploymentStatusEvents events = null;
		public static EmploymentStatusEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(EmploymentStatus))
					{
						if (events is null)
							events = new EmploymentStatusEvents();
					}
				}
				return events;
			}
		}
		public class EmploymentStatusEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<EmploymentStatus, EntityEventArgs> onNew;
			public event EventHandler<EmploymentStatus, EntityEventArgs> OnNew
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
				EventHandler<EmploymentStatus, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((EmploymentStatus)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<EmploymentStatus, EntityEventArgs> onDelete;
			public event EventHandler<EmploymentStatus, EntityEventArgs> OnDelete
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
				EventHandler<EmploymentStatus, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((EmploymentStatus)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<EmploymentStatus, EntityEventArgs> onSave;
			public event EventHandler<EmploymentStatus, EntityEventArgs> OnSave
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
				EventHandler<EmploymentStatus, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((EmploymentStatus)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<EmploymentStatus, EntityEventArgs> onAfterSave;
			public event EventHandler<EmploymentStatus, EntityEventArgs> OnAfterSave
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
				EventHandler<EmploymentStatus, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((EmploymentStatus)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<EmploymentStatus, PropertyEventArgs> onName;
				public static event EventHandler<EmploymentStatus, PropertyEventArgs> OnName
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
					EventHandler<EmploymentStatus, PropertyEventArgs> handler = onName;
					if (handler is not null)
						handler.Invoke((EmploymentStatus)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<EmploymentStatus, PropertyEventArgs> onUid;
				public static event EventHandler<EmploymentStatus, PropertyEventArgs> OnUid
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
					EventHandler<EmploymentStatus, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((EmploymentStatus)sender, args);
				}

				#endregion

				#region OnCreatedOn

				private static bool onCreatedOnIsRegistered = false;

				private static EventHandler<EmploymentStatus, PropertyEventArgs> onCreatedOn;
				public static event EventHandler<EmploymentStatus, PropertyEventArgs> OnCreatedOn
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCreatedOnIsRegistered)
							{
								Members.CreatedOn.Events.OnChange -= onCreatedOnProxy;
								Members.CreatedOn.Events.OnChange += onCreatedOnProxy;
								onCreatedOnIsRegistered = true;
							}
							onCreatedOn += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCreatedOn -= value;
							if (onCreatedOn is null && onCreatedOnIsRegistered)
							{
								Members.CreatedOn.Events.OnChange -= onCreatedOnProxy;
								onCreatedOnIsRegistered = false;
							}
						}
					}
				}
			
				private static void onCreatedOnProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<EmploymentStatus, PropertyEventArgs> handler = onCreatedOn;
					if (handler is not null)
						handler.Invoke((EmploymentStatus)sender, args);
				}

				#endregion

				#region OnCreatedBy

				private static bool onCreatedByIsRegistered = false;

				private static EventHandler<EmploymentStatus, PropertyEventArgs> onCreatedBy;
				public static event EventHandler<EmploymentStatus, PropertyEventArgs> OnCreatedBy
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCreatedByIsRegistered)
							{
								Members.CreatedBy.Events.OnChange -= onCreatedByProxy;
								Members.CreatedBy.Events.OnChange += onCreatedByProxy;
								onCreatedByIsRegistered = true;
							}
							onCreatedBy += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCreatedBy -= value;
							if (onCreatedBy is null && onCreatedByIsRegistered)
							{
								Members.CreatedBy.Events.OnChange -= onCreatedByProxy;
								onCreatedByIsRegistered = false;
							}
						}
					}
				}
			
				private static void onCreatedByProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<EmploymentStatus, PropertyEventArgs> handler = onCreatedBy;
					if (handler is not null)
						handler.Invoke((EmploymentStatus)sender, args);
				}

				#endregion

				#region OnLastModifiedOn

				private static bool onLastModifiedOnIsRegistered = false;

				private static EventHandler<EmploymentStatus, PropertyEventArgs> onLastModifiedOn;
				public static event EventHandler<EmploymentStatus, PropertyEventArgs> OnLastModifiedOn
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onLastModifiedOnIsRegistered)
							{
								Members.LastModifiedOn.Events.OnChange -= onLastModifiedOnProxy;
								Members.LastModifiedOn.Events.OnChange += onLastModifiedOnProxy;
								onLastModifiedOnIsRegistered = true;
							}
							onLastModifiedOn += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onLastModifiedOn -= value;
							if (onLastModifiedOn is null && onLastModifiedOnIsRegistered)
							{
								Members.LastModifiedOn.Events.OnChange -= onLastModifiedOnProxy;
								onLastModifiedOnIsRegistered = false;
							}
						}
					}
				}
			
				private static void onLastModifiedOnProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<EmploymentStatus, PropertyEventArgs> handler = onLastModifiedOn;
					if (handler is not null)
						handler.Invoke((EmploymentStatus)sender, args);
				}

				#endregion

				#region OnLastModifiedBy

				private static bool onLastModifiedByIsRegistered = false;

				private static EventHandler<EmploymentStatus, PropertyEventArgs> onLastModifiedBy;
				public static event EventHandler<EmploymentStatus, PropertyEventArgs> OnLastModifiedBy
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onLastModifiedByIsRegistered)
							{
								Members.LastModifiedBy.Events.OnChange -= onLastModifiedByProxy;
								Members.LastModifiedBy.Events.OnChange += onLastModifiedByProxy;
								onLastModifiedByIsRegistered = true;
							}
							onLastModifiedBy += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onLastModifiedBy -= value;
							if (onLastModifiedBy is null && onLastModifiedByIsRegistered)
							{
								Members.LastModifiedBy.Events.OnChange -= onLastModifiedByProxy;
								onLastModifiedByIsRegistered = false;
							}
						}
					}
				}
			
				private static void onLastModifiedByProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<EmploymentStatus, PropertyEventArgs> handler = onLastModifiedBy;
					if (handler is not null)
						handler.Invoke((EmploymentStatus)sender, args);
				}

				#endregion

				#region OnDescription

				private static bool onDescriptionIsRegistered = false;

				private static EventHandler<EmploymentStatus, PropertyEventArgs> onDescription;
				public static event EventHandler<EmploymentStatus, PropertyEventArgs> OnDescription
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDescriptionIsRegistered)
							{
								Members.Description.Events.OnChange -= onDescriptionProxy;
								Members.Description.Events.OnChange += onDescriptionProxy;
								onDescriptionIsRegistered = true;
							}
							onDescription += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDescription -= value;
							if (onDescription is null && onDescriptionIsRegistered)
							{
								Members.Description.Events.OnChange -= onDescriptionProxy;
								onDescriptionIsRegistered = false;
							}
						}
					}
				}
			
				private static void onDescriptionProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<EmploymentStatus, PropertyEventArgs> handler = onDescription;
					if (handler is not null)
						handler.Invoke((EmploymentStatus)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region Static Data

		public static class StaticData
		{
			/// <summary>
			/// Get the 'Uid' value for the corresponding Name
			/// </summary>
			public static class Name
			{
				public const string Regular = "1";
				public const string Probationary = "2";
				public const string Contractual = "3";
				public static bool Exist(string name)
				{
					if (name == "Regular")
						return true;
					if (name == "Probationary")
						return true;
					if (name == "Contractual")
						return true;
					return false;
				}
			}
			/// <summary>
			/// Get the 'Uid' value for the corresponding Uid
			/// </summary>
			public static class Uid
			{
				public const string _1 = "1";
				public const string _2 = "2";
				public const string _3 = "3";
				public static bool Exist(string uid)
				{
					if (uid == "1")
						return true;
					if (uid == "2")
						return true;
					if (uid == "3")
						return true;
					return false;
				}
			}
		}

		#endregion

		#region IEmploymentStatusOriginalData

		public IEmploymentStatusOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IEmploymentStatus

		string IEmploymentStatusOriginalData.Name { get { return OriginalData.Name; } }

		#endregion
		#region Members for interface INeo4jBase

		INeo4jBaseOriginalData INeo4jBase.OriginalVersion { get { return this; } }

		string INeo4jBaseOriginalData.Uid { get { return OriginalData.Uid; } }
		System.DateTime? INeo4jBaseOriginalData.CreatedOn { get { return OriginalData.CreatedOn; } }
		string INeo4jBaseOriginalData.CreatedBy { get { return OriginalData.CreatedBy; } }
		System.DateTime INeo4jBaseOriginalData.LastModifiedOn { get { return OriginalData.LastModifiedOn; } }
		string INeo4jBaseOriginalData.LastModifiedBy { get { return OriginalData.LastModifiedBy; } }
		string INeo4jBaseOriginalData.Description { get { return OriginalData.Description; } }

		#endregion
		#endregion
	}
}