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
	public interface IDepartmentOriginalData : INeo4jBaseOriginalData
	{
		string Name { get; }
		Branch Branch { get; }
		IEnumerable<Employee> Employees { get; }
		HeadEmployee DepartmentHead { get; }
	}

	public partial class Department : OGM<Department, Department.DepartmentData, System.String>, INeo4jBase, IDepartmentOriginalData
	{
		#region Initialize

		static Department()
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

		public static Dictionary<System.String, Department> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.DepartmentAlias, IWhereQuery> query)
		{
			q.DepartmentAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Department.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"Department => Name : {this.Name?.ToString() ?? "null"}, Uid : {this.Uid}, CreatedOn : {this.CreatedOn?.ToString() ?? "null"}, CreatedBy : {this.CreatedBy?.ToString() ?? "null"}, LastModifiedOn : {this.LastModifiedOn}, LastModifiedBy : {this.LastModifiedBy?.ToString() ?? "null"}, Description : {this.Description?.ToString() ?? "null"}";
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
					OriginalData = new DepartmentData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.Uid is null)
				throw new PersistenceException(string.Format("Cannot save Department with key '{0}' because the Uid cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class DepartmentData : Data<System.String>
		{
			public DepartmentData()
			{

			}

			public DepartmentData(DepartmentData data)
			{
				Name = data.Name;
				Branch = data.Branch;
				Employees = data.Employees;
				DepartmentHead = data.DepartmentHead;
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
				NodeType = "Department";

				Branch = new EntityCollection<Branch>(Wrapper, Members.Branch, item => { if (Members.Branch.Events.HasRegisteredChangeHandlers) { int loadHack = item.Departments.Count; } });
				Employees = new EntityCollection<Employee>(Wrapper, Members.Employees, item => { if (Members.Employees.Events.HasRegisteredChangeHandlers) { object loadHack = item.Department; } });
				DepartmentHead = new EntityCollection<HeadEmployee>(Wrapper, Members.DepartmentHead);
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

			#region Members for interface IDepartment

			public string Name { get; set; }
			public EntityCollection<Branch> Branch { get; private set; }
			public EntityCollection<Employee> Employees { get; private set; }
			public EntityCollection<HeadEmployee> DepartmentHead { get; private set; }

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

		#region Members for interface IDepartment

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
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
		public EntityCollection<Employee> Employees { get { return InnerData.Employees; } }
		private void ClearEmployees(DateTime? moment)
		{
			((ILookupHelper<Employee>)InnerData.Employees).ClearLookup(moment);
		}
		public HeadEmployee DepartmentHead
		{
			get { return ((ILookupHelper<HeadEmployee>)InnerData.DepartmentHead).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.DepartmentHead, ((ILookupHelper<HeadEmployee>)InnerData.DepartmentHead).GetItem(null), value))
					((ILookupHelper<HeadEmployee>)InnerData.DepartmentHead).SetItem(value, null); 
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

		private static DepartmentMembers members = null;
		public static DepartmentMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(Department))
					{
						if (members is null)
							members = new DepartmentMembers();
					}
				}
				return members;
			}
		}
		public class DepartmentMembers
		{
			internal DepartmentMembers() { }

			#region Members for interface IDepartment

			public Property Name { get; } = MemgraphTestApp.HumanResources.Model.Entities["Department"].Properties["Name"];
			public Property Branch { get; } = MemgraphTestApp.HumanResources.Model.Entities["Department"].Properties["Branch"];
			public Property Employees { get; } = MemgraphTestApp.HumanResources.Model.Entities["Department"].Properties["Employees"];
			public Property DepartmentHead { get; } = MemgraphTestApp.HumanResources.Model.Entities["Department"].Properties["DepartmentHead"];
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

		private static DepartmentFullTextMembers fullTextMembers = null;
		public static DepartmentFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(Department))
					{
						if (fullTextMembers is null)
							fullTextMembers = new DepartmentFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class DepartmentFullTextMembers
		{
			internal DepartmentFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(Department))
				{
					if (entity is null)
						entity = MemgraphTestApp.HumanResources.Model.Entities["Department"];
				}
			}
			return entity;
		}

		private static DepartmentEvents events = null;
		public static DepartmentEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(Department))
					{
						if (events is null)
							events = new DepartmentEvents();
					}
				}
				return events;
			}
		}
		public class DepartmentEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<Department, EntityEventArgs> onNew;
			public event EventHandler<Department, EntityEventArgs> OnNew
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
				EventHandler<Department, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((Department)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<Department, EntityEventArgs> onDelete;
			public event EventHandler<Department, EntityEventArgs> OnDelete
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
				EventHandler<Department, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((Department)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<Department, EntityEventArgs> onSave;
			public event EventHandler<Department, EntityEventArgs> OnSave
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
				EventHandler<Department, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((Department)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<Department, EntityEventArgs> onAfterSave;
			public event EventHandler<Department, EntityEventArgs> OnAfterSave
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
				EventHandler<Department, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((Department)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<Department, PropertyEventArgs> onName;
				public static event EventHandler<Department, PropertyEventArgs> OnName
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
					EventHandler<Department, PropertyEventArgs> handler = onName;
					if (handler is not null)
						handler.Invoke((Department)sender, args);
				}

				#endregion

				#region OnBranch

				private static bool onBranchIsRegistered = false;

				private static EventHandler<Department, PropertyEventArgs> onBranch;
				public static event EventHandler<Department, PropertyEventArgs> OnBranch
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
					EventHandler<Department, PropertyEventArgs> handler = onBranch;
					if (handler is not null)
						handler.Invoke((Department)sender, args);
				}

				#endregion

				#region OnEmployees

				private static bool onEmployeesIsRegistered = false;

				private static EventHandler<Department, PropertyEventArgs> onEmployees;
				public static event EventHandler<Department, PropertyEventArgs> OnEmployees
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onEmployeesIsRegistered)
							{
								Members.Employees.Events.OnChange -= onEmployeesProxy;
								Members.Employees.Events.OnChange += onEmployeesProxy;
								onEmployeesIsRegistered = true;
							}
							onEmployees += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onEmployees -= value;
							if (onEmployees is null && onEmployeesIsRegistered)
							{
								Members.Employees.Events.OnChange -= onEmployeesProxy;
								onEmployeesIsRegistered = false;
							}
						}
					}
				}
			
				private static void onEmployeesProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Department, PropertyEventArgs> handler = onEmployees;
					if (handler is not null)
						handler.Invoke((Department)sender, args);
				}

				#endregion

				#region OnDepartmentHead

				private static bool onDepartmentHeadIsRegistered = false;

				private static EventHandler<Department, PropertyEventArgs> onDepartmentHead;
				public static event EventHandler<Department, PropertyEventArgs> OnDepartmentHead
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDepartmentHeadIsRegistered)
							{
								Members.DepartmentHead.Events.OnChange -= onDepartmentHeadProxy;
								Members.DepartmentHead.Events.OnChange += onDepartmentHeadProxy;
								onDepartmentHeadIsRegistered = true;
							}
							onDepartmentHead += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDepartmentHead -= value;
							if (onDepartmentHead is null && onDepartmentHeadIsRegistered)
							{
								Members.DepartmentHead.Events.OnChange -= onDepartmentHeadProxy;
								onDepartmentHeadIsRegistered = false;
							}
						}
					}
				}
			
				private static void onDepartmentHeadProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Department, PropertyEventArgs> handler = onDepartmentHead;
					if (handler is not null)
						handler.Invoke((Department)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Department, PropertyEventArgs> onUid;
				public static event EventHandler<Department, PropertyEventArgs> OnUid
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
					EventHandler<Department, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((Department)sender, args);
				}

				#endregion

				#region OnCreatedOn

				private static bool onCreatedOnIsRegistered = false;

				private static EventHandler<Department, PropertyEventArgs> onCreatedOn;
				public static event EventHandler<Department, PropertyEventArgs> OnCreatedOn
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
					EventHandler<Department, PropertyEventArgs> handler = onCreatedOn;
					if (handler is not null)
						handler.Invoke((Department)sender, args);
				}

				#endregion

				#region OnCreatedBy

				private static bool onCreatedByIsRegistered = false;

				private static EventHandler<Department, PropertyEventArgs> onCreatedBy;
				public static event EventHandler<Department, PropertyEventArgs> OnCreatedBy
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
					EventHandler<Department, PropertyEventArgs> handler = onCreatedBy;
					if (handler is not null)
						handler.Invoke((Department)sender, args);
				}

				#endregion

				#region OnLastModifiedOn

				private static bool onLastModifiedOnIsRegistered = false;

				private static EventHandler<Department, PropertyEventArgs> onLastModifiedOn;
				public static event EventHandler<Department, PropertyEventArgs> OnLastModifiedOn
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
					EventHandler<Department, PropertyEventArgs> handler = onLastModifiedOn;
					if (handler is not null)
						handler.Invoke((Department)sender, args);
				}

				#endregion

				#region OnLastModifiedBy

				private static bool onLastModifiedByIsRegistered = false;

				private static EventHandler<Department, PropertyEventArgs> onLastModifiedBy;
				public static event EventHandler<Department, PropertyEventArgs> OnLastModifiedBy
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
					EventHandler<Department, PropertyEventArgs> handler = onLastModifiedBy;
					if (handler is not null)
						handler.Invoke((Department)sender, args);
				}

				#endregion

				#region OnDescription

				private static bool onDescriptionIsRegistered = false;

				private static EventHandler<Department, PropertyEventArgs> onDescription;
				public static event EventHandler<Department, PropertyEventArgs> OnDescription
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
					EventHandler<Department, PropertyEventArgs> handler = onDescription;
					if (handler is not null)
						handler.Invoke((Department)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region IDepartmentOriginalData

		public IDepartmentOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IDepartment

		string IDepartmentOriginalData.Name { get { return OriginalData.Name; } }
		Branch IDepartmentOriginalData.Branch { get { return ((ILookupHelper<Branch>)OriginalData.Branch).GetOriginalItem(null); } }
		IEnumerable<Employee> IDepartmentOriginalData.Employees { get { return OriginalData.Employees.OriginalData; } }
		HeadEmployee IDepartmentOriginalData.DepartmentHead { get { return ((ILookupHelper<HeadEmployee>)OriginalData.DepartmentHead).GetOriginalItem(null); } }

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