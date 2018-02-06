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
	public interface IDepartmentOriginalData : ISchemaBaseOriginalData
    {
		string Name { get; }
		string GroupName { get; }
		IEnumerable<Employee> Employees { get; }
		IEnumerable<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; }
    }

	public partial class Department : OGM<Department, Department.DepartmentData, System.String>, ISchemaBase, INeo4jBase, IDepartmentOriginalData
	{
        #region Initialize

        static Department()
        {
            Register.Types();
        }

        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadFromNaturalKey
            
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

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Department.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"Department => Name : {this.Name}, GroupName : {this.GroupName}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                if ((object)InnerData == (object)OriginalData)
                    OriginalData = new DepartmentData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.Name == null)
				throw new PersistenceException(string.Format("Cannot save Department with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.GroupName == null)
				throw new PersistenceException(string.Format("Cannot save Department with key '{0}' because the GroupName cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save Department with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
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
				GroupName = data.GroupName;
				Employees = data.Employees;
				EmployeeDepartmentHistories = data.EmployeeDepartmentHistories;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Department";

				Employees = new EntityCollection<Employee>(Wrapper, Members.Employees);
				EmployeeDepartmentHistories = new EntityCollection<EmployeeDepartmentHistory>(Wrapper, Members.EmployeeDepartmentHistories, item => { if (Members.EmployeeDepartmentHistories.Events.HasRegisteredChangeHandlers) { object loadHack = item.Department; } });
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("Name",  Name);
				dictionary.Add("GroupName",  GroupName);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("GroupName", out value))
					GroupName = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IDepartment

			public string Name { get; set; }
			public string GroupName { get; set; }
			public EntityCollection<Employee> Employees { get; private set; }
			public EntityCollection<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; private set; }

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

		#region Members for interface IDepartment

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
		public string GroupName { get { LazyGet(); return InnerData.GroupName; } set { if (LazySet(Members.GroupName, InnerData.GroupName, value)) InnerData.GroupName = value; } }
		public EntityCollection<Employee> Employees { get { return InnerData.Employees; } }
		public EntityCollection<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get { return InnerData.EmployeeDepartmentHistories; } }
		private void ClearEmployeeDepartmentHistories(DateTime? moment)
		{
			((ILookupHelper<EntityCollection<EmployeeDepartmentHistory>>)InnerData.EmployeeDepartmentHistories).ClearLookup(moment);
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

        private static DepartmentMembers members = null;
        public static DepartmentMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(Department))
                    {
                        if (members == null)
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

            public Property Name { get; } = Datastore.AdventureWorks.Model.Entities["Department"].Properties["Name"];
            public Property GroupName { get; } = Datastore.AdventureWorks.Model.Entities["Department"].Properties["GroupName"];
            public Property Employees { get; } = Datastore.AdventureWorks.Model.Entities["Department"].Properties["Employees"];
            public Property EmployeeDepartmentHistories { get; } = Datastore.AdventureWorks.Model.Entities["Department"].Properties["EmployeeDepartmentHistories"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static DepartmentFullTextMembers fullTextMembers = null;
        public static DepartmentFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(Department))
                    {
                        if (fullTextMembers == null)
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
            if (entity == null)
            {
                lock (typeof(Department))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["Department"];
                }
            }
            return entity;
        }

		private static DepartmentEvents events = null;
        public static DepartmentEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(Department))
                    {
                        if (events == null)
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
                        if (onNew == null && onNewIsRegistered)
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
                if ((object)handler != null)
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
                        if (onDelete == null && onDeleteIsRegistered)
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
                if ((object)handler != null)
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
                        if (onSave == null && onSaveIsRegistered)
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
                if ((object)handler != null)
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
							if (onName == null && onNameIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Department)sender, args);
				}

				#endregion

				#region OnGroupName

				private static bool onGroupNameIsRegistered = false;

				private static EventHandler<Department, PropertyEventArgs> onGroupName;
				public static event EventHandler<Department, PropertyEventArgs> OnGroupName
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onGroupNameIsRegistered)
							{
								Members.GroupName.Events.OnChange -= onGroupNameProxy;
								Members.GroupName.Events.OnChange += onGroupNameProxy;
								onGroupNameIsRegistered = true;
							}
							onGroupName += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onGroupName -= value;
							if (onGroupName == null && onGroupNameIsRegistered)
							{
								Members.GroupName.Events.OnChange -= onGroupNameProxy;
								onGroupNameIsRegistered = false;
							}
						}
					}
				}
            
				private static void onGroupNameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Department, PropertyEventArgs> handler = onGroupName;
					if ((object)handler != null)
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
							if (onEmployees == null && onEmployeesIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Department)sender, args);
				}

				#endregion

				#region OnEmployeeDepartmentHistories

				private static bool onEmployeeDepartmentHistoriesIsRegistered = false;

				private static EventHandler<Department, PropertyEventArgs> onEmployeeDepartmentHistories;
				public static event EventHandler<Department, PropertyEventArgs> OnEmployeeDepartmentHistories
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onEmployeeDepartmentHistoriesIsRegistered)
							{
								Members.EmployeeDepartmentHistories.Events.OnChange -= onEmployeeDepartmentHistoriesProxy;
								Members.EmployeeDepartmentHistories.Events.OnChange += onEmployeeDepartmentHistoriesProxy;
								onEmployeeDepartmentHistoriesIsRegistered = true;
							}
							onEmployeeDepartmentHistories += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onEmployeeDepartmentHistories -= value;
							if (onEmployeeDepartmentHistories == null && onEmployeeDepartmentHistoriesIsRegistered)
							{
								Members.EmployeeDepartmentHistories.Events.OnChange -= onEmployeeDepartmentHistoriesProxy;
								onEmployeeDepartmentHistoriesIsRegistered = false;
							}
						}
					}
				}
            
				private static void onEmployeeDepartmentHistoriesProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Department, PropertyEventArgs> handler = onEmployeeDepartmentHistories;
					if ((object)handler != null)
						handler.Invoke((Department)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<Department, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<Department, PropertyEventArgs> OnModifiedDate
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
							if (onModifiedDate == null && onModifiedDateIsRegistered)
							{
								Members.ModifiedDate.Events.OnChange -= onModifiedDateProxy;
								onModifiedDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onModifiedDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Department, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
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
							if (onUid == null && onUidIsRegistered)
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
					if ((object)handler != null)
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
		string IDepartmentOriginalData.GroupName { get { return OriginalData.GroupName; } }
		IEnumerable<Employee> IDepartmentOriginalData.Employees { get { return OriginalData.Employees.OriginalData; } }
		IEnumerable<EmployeeDepartmentHistory> IDepartmentOriginalData.EmployeeDepartmentHistories { get { return OriginalData.EmployeeDepartmentHistories.OriginalData; } }

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