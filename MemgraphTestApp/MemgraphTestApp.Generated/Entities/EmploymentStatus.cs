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
	public interface IEmploymentStatusOriginalData
	{
		string Uid { get; }
		string Name { get; }
	}

	public partial class EmploymentStatus : OGM<EmploymentStatus, EmploymentStatus.EmploymentStatusData, System.String>, IEmploymentStatusOriginalData
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

			#region LoadByUid

			RegisterQuery(nameof(LoadByUid), (query, alias) => query.
				Where(alias.Uid == Parameter.New<string>(Param0)));

			#endregion

			AdditionalGeneratedStoredQueries();
		}
		public static EmploymentStatus LoadByUid(string uid)
		{
			return FromQuery(nameof(LoadByUid), new Parameter(Param0, uid)).FirstOrDefault();
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
			return $"EmploymentStatus => Uid : {this.Uid}, Name : {this.Name}";
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
				Uid = data.Uid;
				Name = data.Name;
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
				dictionary.Add("Uid",  Uid);
				dictionary.Add("Name",  Name);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
			}

			#endregion

			#region Members for interface IEmploymentStatus

			public string Uid { get; set; }
			public string Name { get; set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IEmploymentStatus

		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }
		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }

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

			public Property Uid { get; } = MemgraphTestApp.HumanResources.Model.Entities["EmploymentStatus"].Properties["Uid"];
			public Property Name { get; } = MemgraphTestApp.HumanResources.Model.Entities["EmploymentStatus"].Properties["Name"];
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

		string IEmploymentStatusOriginalData.Uid { get { return OriginalData.Uid; } }
		string IEmploymentStatusOriginalData.Name { get { return OriginalData.Name; } }

		#endregion
		#endregion
	}
}