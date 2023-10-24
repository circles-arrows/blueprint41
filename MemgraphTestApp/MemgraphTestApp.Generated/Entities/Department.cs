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
	public interface IDepartmentOriginalData
	{
		string Name { get; }
		string Uid { get; }
		Branch Branch { get; }
		IEnumerable<Employee> Employees { get; }
		HeadEmployee DepartmentHead { get; }
	}

	public partial class Department : OGM<Department, Department.DepartmentData, System.String>, IDepartmentOriginalData
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

			#region LoadByUid

			RegisterQuery(nameof(LoadByUid), (query, alias) => query.
				Where(alias.Uid == Parameter.New<string>(Param0)));

			#endregion

			AdditionalGeneratedStoredQueries();
		}
		public static Department LoadByUid(string uid)
		{
			return FromQuery(nameof(LoadByUid), new Parameter(Param0, uid)).FirstOrDefault();
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
			return $"Department => Name : {this.Name?.ToString() ?? "null"}, Uid : {this.Uid}";
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
				Uid = data.Uid;
				Branch = data.Branch;
				Employees = data.Employees;
				DepartmentHead = data.DepartmentHead;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Department";

				Branch = new EntityCollection<Branch>(Wrapper, Members.Branch, item => { if (Members.Branch.Events.HasRegisteredChangeHandlers) { int loadHack = item.Departments.Count; } });
				Employees = new EntityCollection<Employee>(Wrapper, Members.Employees, item => { if (Members.Employees.Events.HasRegisteredChangeHandlers) { int loadHack = item.Department.Count; } });
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
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IDepartment

			public string Name { get; set; }
			public string Uid { get; set; }
			public EntityCollection<Branch> Branch { get; private set; }
			public EntityCollection<Employee> Employees { get; private set; }
			public EntityCollection<HeadEmployee> DepartmentHead { get; private set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IDepartment

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }
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
			public Property Uid { get; } = MemgraphTestApp.HumanResources.Model.Entities["Department"].Properties["Uid"];
			public Property Branch { get; } = MemgraphTestApp.HumanResources.Model.Entities["Department"].Properties["Branch"];
			public Property Employees { get; } = MemgraphTestApp.HumanResources.Model.Entities["Department"].Properties["Employees"];
			public Property DepartmentHead { get; } = MemgraphTestApp.HumanResources.Model.Entities["Department"].Properties["DepartmentHead"];
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

			}

			#endregion
		}

		#endregion

		#region IDepartmentOriginalData

		public IDepartmentOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IDepartment

		string IDepartmentOriginalData.Name { get { return OriginalData.Name; } }
		string IDepartmentOriginalData.Uid { get { return OriginalData.Uid; } }
		Branch IDepartmentOriginalData.Branch { get { return ((ILookupHelper<Branch>)OriginalData.Branch).GetOriginalItem(null); } }
		IEnumerable<Employee> IDepartmentOriginalData.Employees { get { return OriginalData.Employees.OriginalData; } }
		HeadEmployee IDepartmentOriginalData.DepartmentHead { get { return ((ILookupHelper<HeadEmployee>)OriginalData.DepartmentHead).GetOriginalItem(null); } }

		#endregion
		#endregion
	}
}