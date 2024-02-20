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
	public interface IHeadEmployeeOriginalData : IPersonnelOriginalData
	{
		string Title { get; }
		Branch Branch { get; }
	}

	public partial class HeadEmployee : OGM<HeadEmployee, HeadEmployee.HeadEmployeeData, System.String>, IPersonnel, INeo4jBase, IHeadEmployeeOriginalData
	{
		#region Initialize

		static HeadEmployee()
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

		public static Dictionary<System.String, HeadEmployee> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.HeadEmployeeAlias, IWhereQuery> query)
		{
			q.HeadEmployeeAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.HeadEmployee.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"HeadEmployee => Title : {this.Title?.ToString() ?? "null"}, FirstName : {this.FirstName?.ToString() ?? "null"}, LastName : {this.LastName?.ToString() ?? "null"}, Uid : {this.Uid}, CreatedOn : {this.CreatedOn?.ToString() ?? "null"}, CreatedBy : {this.CreatedBy?.ToString() ?? "null"}, LastModifiedOn : {this.LastModifiedOn}, LastModifiedBy : {this.LastModifiedBy?.ToString() ?? "null"}, Description : {this.Description?.ToString() ?? "null"}";
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
					OriginalData = new HeadEmployeeData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.Uid is null)
				throw new PersistenceException(string.Format("Cannot save HeadEmployee with key '{0}' because the Uid cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class HeadEmployeeData : Data<System.String>
		{
			public HeadEmployeeData()
			{

			}

			public HeadEmployeeData(HeadEmployeeData data)
			{
				Title = data.Title;
				Branch = data.Branch;
				FirstName = data.FirstName;
				LastName = data.LastName;
				Status = data.Status;
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
				NodeType = "HeadEmployee";

				Branch = new EntityCollection<Branch>(Wrapper, Members.Branch, item => { if (Members.Branch.Events.HasRegisteredChangeHandlers) { object loadHack = item.BranchHead; } });
				Status = new EntityCollection<EmploymentStatus>(Wrapper, Members.Status);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("Title",  Title);
				dictionary.Add("FirstName",  FirstName);
				dictionary.Add("LastName",  LastName);
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
				if (properties.TryGetValue("Title", out value))
					Title = (string)value;
				if (properties.TryGetValue("FirstName", out value))
					FirstName = (string)value;
				if (properties.TryGetValue("LastName", out value))
					LastName = (string)value;
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

			#region Members for interface IHeadEmployee

			public string Title { get; set; }
			public EntityCollection<Branch> Branch { get; private set; }

			#endregion
			#region Members for interface IPersonnel

			public string FirstName { get; set; }
			public string LastName { get; set; }
			public EntityCollection<EmploymentStatus> Status { get; private set; }

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

		#region Members for interface IHeadEmployee

		public string Title { get { LazyGet(); return InnerData.Title; } set { if (LazySet(Members.Title, InnerData.Title, value)) InnerData.Title = value; } }
		public Branch Branch
		{
			get { return ((ILookupHelper<Branch>)InnerData.Branch).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Branch, ((ILookupHelper<Branch>)InnerData.Branch).GetItem(null), value))
					((ILookupHelper<Branch>)InnerData.Branch).SetItem(value, null); 
			}
		}
		private void ClearBranch(DateTime? moment)
		{
			((ILookupHelper<Branch>)InnerData.Branch).ClearLookup(moment);
		}

		#endregion
		#region Members for interface IPersonnel

		public string FirstName { get { LazyGet(); return InnerData.FirstName; } set { if (LazySet(Members.FirstName, InnerData.FirstName, value)) InnerData.FirstName = value; } }
		public string LastName { get { LazyGet(); return InnerData.LastName; } set { if (LazySet(Members.LastName, InnerData.LastName, value)) InnerData.LastName = value; } }
		public EmploymentStatus Status
		{
			get { return ((ILookupHelper<EmploymentStatus>)InnerData.Status).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Status, ((ILookupHelper<EmploymentStatus>)InnerData.Status).GetItem(null), value))
					((ILookupHelper<EmploymentStatus>)InnerData.Status).SetItem(value, null); 
			}
		}

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

		private static HeadEmployeeMembers members = null;
		public static HeadEmployeeMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(HeadEmployee))
					{
						if (members is null)
							members = new HeadEmployeeMembers();
					}
				}
				return members;
			}
		}
		public class HeadEmployeeMembers
		{
			internal HeadEmployeeMembers() { }

			#region Members for interface IHeadEmployee

			public Property Title { get; } = MemgraphTestApp.HumanResources.Model.Entities["HeadEmployee"].Properties["Title"];
			public Property Branch { get; } = MemgraphTestApp.HumanResources.Model.Entities["HeadEmployee"].Properties["Branch"];
			#endregion

			#region Members for interface IPersonnel

			public Property FirstName { get; } = MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["FirstName"];
			public Property LastName { get; } = MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["LastName"];
			public Property Status { get; } = MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["Status"];
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

		private static HeadEmployeeFullTextMembers fullTextMembers = null;
		public static HeadEmployeeFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(HeadEmployee))
					{
						if (fullTextMembers is null)
							fullTextMembers = new HeadEmployeeFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class HeadEmployeeFullTextMembers
		{
			internal HeadEmployeeFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(HeadEmployee))
				{
					if (entity is null)
						entity = MemgraphTestApp.HumanResources.Model.Entities["HeadEmployee"];
				}
			}
			return entity;
		}

		private static HeadEmployeeEvents events = null;
		public static HeadEmployeeEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(HeadEmployee))
					{
						if (events is null)
							events = new HeadEmployeeEvents();
					}
				}
				return events;
			}
		}
		public class HeadEmployeeEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<HeadEmployee, EntityEventArgs> onNew;
			public event EventHandler<HeadEmployee, EntityEventArgs> OnNew
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
				EventHandler<HeadEmployee, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((HeadEmployee)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<HeadEmployee, EntityEventArgs> onDelete;
			public event EventHandler<HeadEmployee, EntityEventArgs> OnDelete
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
				EventHandler<HeadEmployee, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((HeadEmployee)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<HeadEmployee, EntityEventArgs> onSave;
			public event EventHandler<HeadEmployee, EntityEventArgs> OnSave
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
				EventHandler<HeadEmployee, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((HeadEmployee)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<HeadEmployee, EntityEventArgs> onAfterSave;
			public event EventHandler<HeadEmployee, EntityEventArgs> OnAfterSave
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
				EventHandler<HeadEmployee, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((HeadEmployee)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnTitle

				private static bool onTitleIsRegistered = false;

				private static EventHandler<HeadEmployee, PropertyEventArgs> onTitle;
				public static event EventHandler<HeadEmployee, PropertyEventArgs> OnTitle
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onTitleIsRegistered)
							{
								Members.Title.Events.OnChange -= onTitleProxy;
								Members.Title.Events.OnChange += onTitleProxy;
								onTitleIsRegistered = true;
							}
							onTitle += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onTitle -= value;
							if (onTitle is null && onTitleIsRegistered)
							{
								Members.Title.Events.OnChange -= onTitleProxy;
								onTitleIsRegistered = false;
							}
						}
					}
				}
			
				private static void onTitleProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<HeadEmployee, PropertyEventArgs> handler = onTitle;
					if (handler is not null)
						handler.Invoke((HeadEmployee)sender, args);
				}

				#endregion

				#region OnBranch

				private static bool onBranchIsRegistered = false;

				private static EventHandler<HeadEmployee, PropertyEventArgs> onBranch;
				public static event EventHandler<HeadEmployee, PropertyEventArgs> OnBranch
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onBranchIsRegistered)
							{
								Members.Branch.Events.OnChange -= onBranchProxy;
								Members.Branch.Events.OnChange += onBranchProxy;
								onBranchIsRegistered = true;
							}
							onBranch += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onBranch -= value;
							if (onBranch is null && onBranchIsRegistered)
							{
								Members.Branch.Events.OnChange -= onBranchProxy;
								onBranchIsRegistered = false;
							}
						}
					}
				}
			
				private static void onBranchProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<HeadEmployee, PropertyEventArgs> handler = onBranch;
					if (handler is not null)
						handler.Invoke((HeadEmployee)sender, args);
				}

				#endregion

				#region OnFirstName

				private static bool onFirstNameIsRegistered = false;

				private static EventHandler<HeadEmployee, PropertyEventArgs> onFirstName;
				public static event EventHandler<HeadEmployee, PropertyEventArgs> OnFirstName
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onFirstNameIsRegistered)
							{
								Members.FirstName.Events.OnChange -= onFirstNameProxy;
								Members.FirstName.Events.OnChange += onFirstNameProxy;
								onFirstNameIsRegistered = true;
							}
							onFirstName += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onFirstName -= value;
							if (onFirstName is null && onFirstNameIsRegistered)
							{
								Members.FirstName.Events.OnChange -= onFirstNameProxy;
								onFirstNameIsRegistered = false;
							}
						}
					}
				}
			
				private static void onFirstNameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<HeadEmployee, PropertyEventArgs> handler = onFirstName;
					if (handler is not null)
						handler.Invoke((HeadEmployee)sender, args);
				}

				#endregion

				#region OnLastName

				private static bool onLastNameIsRegistered = false;

				private static EventHandler<HeadEmployee, PropertyEventArgs> onLastName;
				public static event EventHandler<HeadEmployee, PropertyEventArgs> OnLastName
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onLastNameIsRegistered)
							{
								Members.LastName.Events.OnChange -= onLastNameProxy;
								Members.LastName.Events.OnChange += onLastNameProxy;
								onLastNameIsRegistered = true;
							}
							onLastName += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onLastName -= value;
							if (onLastName is null && onLastNameIsRegistered)
							{
								Members.LastName.Events.OnChange -= onLastNameProxy;
								onLastNameIsRegistered = false;
							}
						}
					}
				}
			
				private static void onLastNameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<HeadEmployee, PropertyEventArgs> handler = onLastName;
					if (handler is not null)
						handler.Invoke((HeadEmployee)sender, args);
				}

				#endregion

				#region OnStatus

				private static bool onStatusIsRegistered = false;

				private static EventHandler<HeadEmployee, PropertyEventArgs> onStatus;
				public static event EventHandler<HeadEmployee, PropertyEventArgs> OnStatus
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onStatusIsRegistered)
							{
								Members.Status.Events.OnChange -= onStatusProxy;
								Members.Status.Events.OnChange += onStatusProxy;
								onStatusIsRegistered = true;
							}
							onStatus += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onStatus -= value;
							if (onStatus is null && onStatusIsRegistered)
							{
								Members.Status.Events.OnChange -= onStatusProxy;
								onStatusIsRegistered = false;
							}
						}
					}
				}
			
				private static void onStatusProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<HeadEmployee, PropertyEventArgs> handler = onStatus;
					if (handler is not null)
						handler.Invoke((HeadEmployee)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<HeadEmployee, PropertyEventArgs> onUid;
				public static event EventHandler<HeadEmployee, PropertyEventArgs> OnUid
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
					EventHandler<HeadEmployee, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((HeadEmployee)sender, args);
				}

				#endregion

				#region OnCreatedOn

				private static bool onCreatedOnIsRegistered = false;

				private static EventHandler<HeadEmployee, PropertyEventArgs> onCreatedOn;
				public static event EventHandler<HeadEmployee, PropertyEventArgs> OnCreatedOn
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
					EventHandler<HeadEmployee, PropertyEventArgs> handler = onCreatedOn;
					if (handler is not null)
						handler.Invoke((HeadEmployee)sender, args);
				}

				#endregion

				#region OnCreatedBy

				private static bool onCreatedByIsRegistered = false;

				private static EventHandler<HeadEmployee, PropertyEventArgs> onCreatedBy;
				public static event EventHandler<HeadEmployee, PropertyEventArgs> OnCreatedBy
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
					EventHandler<HeadEmployee, PropertyEventArgs> handler = onCreatedBy;
					if (handler is not null)
						handler.Invoke((HeadEmployee)sender, args);
				}

				#endregion

				#region OnLastModifiedOn

				private static bool onLastModifiedOnIsRegistered = false;

				private static EventHandler<HeadEmployee, PropertyEventArgs> onLastModifiedOn;
				public static event EventHandler<HeadEmployee, PropertyEventArgs> OnLastModifiedOn
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
					EventHandler<HeadEmployee, PropertyEventArgs> handler = onLastModifiedOn;
					if (handler is not null)
						handler.Invoke((HeadEmployee)sender, args);
				}

				#endregion

				#region OnLastModifiedBy

				private static bool onLastModifiedByIsRegistered = false;

				private static EventHandler<HeadEmployee, PropertyEventArgs> onLastModifiedBy;
				public static event EventHandler<HeadEmployee, PropertyEventArgs> OnLastModifiedBy
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
					EventHandler<HeadEmployee, PropertyEventArgs> handler = onLastModifiedBy;
					if (handler is not null)
						handler.Invoke((HeadEmployee)sender, args);
				}

				#endregion

				#region OnDescription

				private static bool onDescriptionIsRegistered = false;

				private static EventHandler<HeadEmployee, PropertyEventArgs> onDescription;
				public static event EventHandler<HeadEmployee, PropertyEventArgs> OnDescription
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
					EventHandler<HeadEmployee, PropertyEventArgs> handler = onDescription;
					if (handler is not null)
						handler.Invoke((HeadEmployee)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region IHeadEmployeeOriginalData

		public IHeadEmployeeOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IHeadEmployee

		string IHeadEmployeeOriginalData.Title { get { return OriginalData.Title; } }
		Branch IHeadEmployeeOriginalData.Branch { get { return ((ILookupHelper<Branch>)OriginalData.Branch).GetOriginalItem(null); } }

		#endregion
		#region Members for interface IPersonnel

		IPersonnelOriginalData IPersonnel.OriginalVersion { get { return this; } }

		string IPersonnelOriginalData.FirstName { get { return OriginalData.FirstName; } }
		string IPersonnelOriginalData.LastName { get { return OriginalData.LastName; } }
		EmploymentStatus IPersonnelOriginalData.Status { get { return ((ILookupHelper<EmploymentStatus>)OriginalData.Status).GetOriginalItem(null); } }

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