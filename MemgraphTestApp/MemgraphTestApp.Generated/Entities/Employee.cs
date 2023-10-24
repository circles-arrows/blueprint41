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
	public interface IEmployeeOriginalData : IPersonnelOriginalData
	{
		IEnumerable<Department> Department { get; }
	}

	public partial class Employee : OGM<Employee, Employee.EmployeeData, System.String>, IPersonnel, IEmployeeOriginalData
	{
		#region Initialize

		static Employee()
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

		public static Dictionary<System.String, Employee> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.EmployeeAlias, IWhereQuery> query)
		{
			q.EmployeeAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Employee.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"Employee => Uid : {this.Uid}, FirstName : {this.FirstName?.ToString() ?? "null"}, LastName : {this.LastName?.ToString() ?? "null"}";
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
					OriginalData = new EmployeeData(InnerData);
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

		public class EmployeeData : Data<System.String>
		{
			public EmployeeData()
			{

			}

			public EmployeeData(EmployeeData data)
			{
				Department = data.Department;
				Uid = data.Uid;
				FirstName = data.FirstName;
				LastName = data.LastName;
				Status = data.Status;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Employee";

				Department = new EntityCollection<Department>(Wrapper, Members.Department, item => { if (Members.Department.Events.HasRegisteredChangeHandlers) { int loadHack = item.Employees.Count; } });
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
				dictionary.Add("Uid",  Uid);
				dictionary.Add("FirstName",  FirstName);
				dictionary.Add("LastName",  LastName);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
				if (properties.TryGetValue("FirstName", out value))
					FirstName = (string)value;
				if (properties.TryGetValue("LastName", out value))
					LastName = (string)value;
			}

			#endregion

			#region Members for interface IEmployee

			public EntityCollection<Department> Department { get; private set; }

			#endregion
			#region Members for interface IPersonnel

			public string Uid { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public EntityCollection<EmploymentStatus> Status { get; private set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IEmployee

		public EntityCollection<Department> Department { get { return InnerData.Department; } }
		private void ClearDepartment(DateTime? moment)
		{
			((ILookupHelper<Department>)InnerData.Department).ClearLookup(moment);
		}

		#endregion
		#region Members for interface IPersonnel

		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }
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

		#region Virtual Node Type
		
		public string NodeType  { get { return InnerData.NodeType; } }
		
		#endregion

		#endregion

		#region Reflection

		private static EmployeeMembers members = null;
		public static EmployeeMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(Employee))
					{
						if (members is null)
							members = new EmployeeMembers();
					}
				}
				return members;
			}
		}
		public class EmployeeMembers
		{
			internal EmployeeMembers() { }

			#region Members for interface IEmployee

			public Property Department { get; } = MemgraphTestApp.HumanResources.Model.Entities["Employee"].Properties["Department"];
			#endregion

			#region Members for interface IPersonnel

			public Property Uid { get; } = MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["Uid"];
			public Property FirstName { get; } = MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["FirstName"];
			public Property LastName { get; } = MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["LastName"];
			public Property Status { get; } = MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["Status"];
			#endregion

		}

		private static EmployeeFullTextMembers fullTextMembers = null;
		public static EmployeeFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(Employee))
					{
						if (fullTextMembers is null)
							fullTextMembers = new EmployeeFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class EmployeeFullTextMembers
		{
			internal EmployeeFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(Employee))
				{
					if (entity is null)
						entity = MemgraphTestApp.HumanResources.Model.Entities["Employee"];
				}
			}
			return entity;
		}

		private static EmployeeEvents events = null;
		public static EmployeeEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(Employee))
					{
						if (events is null)
							events = new EmployeeEvents();
					}
				}
				return events;
			}
		}
		public class EmployeeEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<Employee, EntityEventArgs> onNew;
			public event EventHandler<Employee, EntityEventArgs> OnNew
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
				EventHandler<Employee, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((Employee)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<Employee, EntityEventArgs> onDelete;
			public event EventHandler<Employee, EntityEventArgs> OnDelete
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
				EventHandler<Employee, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((Employee)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<Employee, EntityEventArgs> onSave;
			public event EventHandler<Employee, EntityEventArgs> OnSave
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
				EventHandler<Employee, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((Employee)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<Employee, EntityEventArgs> onAfterSave;
			public event EventHandler<Employee, EntityEventArgs> OnAfterSave
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
				EventHandler<Employee, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((Employee)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnDepartment

				private static bool onDepartmentIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onDepartment;
				public static event EventHandler<Employee, PropertyEventArgs> OnDepartment
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDepartmentIsRegistered)
							{
								Members.Department.Events.OnChange -= onDepartmentProxy;
								Members.Department.Events.OnChange += onDepartmentProxy;
								onDepartmentIsRegistered = true;
							}
							onDepartment += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDepartment -= value;
							if (onDepartment is null && onDepartmentIsRegistered)
							{
								Members.Department.Events.OnChange -= onDepartmentProxy;
								onDepartmentIsRegistered = false;
							}
						}
					}
				}
			
				private static void onDepartmentProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onDepartment;
					if (handler is not null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onUid;
				public static event EventHandler<Employee, PropertyEventArgs> OnUid
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
					EventHandler<Employee, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnFirstName

				private static bool onFirstNameIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onFirstName;
				public static event EventHandler<Employee, PropertyEventArgs> OnFirstName
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
					EventHandler<Employee, PropertyEventArgs> handler = onFirstName;
					if (handler is not null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnLastName

				private static bool onLastNameIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onLastName;
				public static event EventHandler<Employee, PropertyEventArgs> OnLastName
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
					EventHandler<Employee, PropertyEventArgs> handler = onLastName;
					if (handler is not null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnStatus

				private static bool onStatusIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onStatus;
				public static event EventHandler<Employee, PropertyEventArgs> OnStatus
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
					EventHandler<Employee, PropertyEventArgs> handler = onStatus;
					if (handler is not null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region IEmployeeOriginalData

		public IEmployeeOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IEmployee

		IEnumerable<Department> IEmployeeOriginalData.Department { get { return OriginalData.Department.OriginalData; } }

		#endregion
		#region Members for interface IPersonnel

		IPersonnelOriginalData IPersonnel.OriginalVersion { get { return this; } }

		string IPersonnelOriginalData.Uid { get { return OriginalData.Uid; } }
		string IPersonnelOriginalData.FirstName { get { return OriginalData.FirstName; } }
		string IPersonnelOriginalData.LastName { get { return OriginalData.LastName; } }
		EmploymentStatus IPersonnelOriginalData.Status { get { return ((ILookupHelper<EmploymentStatus>)OriginalData.Status).GetOriginalItem(null); } }

		#endregion
		#endregion
	}
}