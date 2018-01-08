 
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
	public interface IEmployeeDepartmentHistoryOriginalData
    {
		#region Outer Data

		#region Members for interface IEmployeeDepartmentHistory

		System.DateTime StartDate { get; }
		string EndDate { get; }
		Department Department { get; }
		Shift Shift { get; }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ModifiedDate { get; }

		#endregion
		#region Members for interface INeo4jBase

		string Uid { get; }

		#endregion
		#endregion
   
	}

	public partial class EmployeeDepartmentHistory : OGM<EmployeeDepartmentHistory, EmployeeDepartmentHistory.EmployeeDepartmentHistoryData, System.String>, ISchemaBase, INeo4jBase, IEmployeeDepartmentHistoryOriginalData
	{
        #region Initialize

        static EmployeeDepartmentHistory()
        {
            Register.Types();
        }

        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadFromNaturalKey
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

        }

        public static Dictionary<System.String, EmployeeDepartmentHistory> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.EmployeeDepartmentHistoryAlias, IWhereQuery> query)
        {
            q.EmployeeDepartmentHistoryAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.EmployeeDepartmentHistory.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"EmployeeDepartmentHistory => StartDate : {this.StartDate}, EndDate : {this.EndDate?.ToString() ?? "null"}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new EmployeeDepartmentHistoryData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.StartDate == null)
				throw new PersistenceException(string.Format("Cannot save EmployeeDepartmentHistory with key '{0}' because the StartDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save EmployeeDepartmentHistory with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class EmployeeDepartmentHistoryData : Data<System.String>
		{
			public EmployeeDepartmentHistoryData()
            {

            }

            public EmployeeDepartmentHistoryData(EmployeeDepartmentHistoryData data)
            {
				StartDate = data.StartDate;
				EndDate = data.EndDate;
				Department = data.Department;
				Shift = data.Shift;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "EmployeeDepartmentHistory";

				Department = new EntityCollection<Department>(Wrapper, Members.Department, item => { if (Members.Department.Events.HasRegisteredChangeHandlers) { int loadHack = item.EmployeeDepartmentHistories.Count; } });
				Shift = new EntityCollection<Shift>(Wrapper, Members.Shift);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("StartDate",  Conversion<System.DateTime, long>.Convert(StartDate));
				dictionary.Add("EndDate",  EndDate);
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
					EndDate = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IEmployeeDepartmentHistory

			public System.DateTime StartDate { get; set; }
			public string EndDate { get; set; }
			public EntityCollection<Department> Department { get; private set; }
			public EntityCollection<Shift> Shift { get; private set; }

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

		#region Members for interface IEmployeeDepartmentHistory

		public System.DateTime StartDate { get { LazyGet(); return InnerData.StartDate; } set { if (LazySet(Members.StartDate, InnerData.StartDate, value)) InnerData.StartDate = value; } }
		public string EndDate { get { LazyGet(); return InnerData.EndDate; } set { if (LazySet(Members.EndDate, InnerData.EndDate, value)) InnerData.EndDate = value; } }
		public Department Department
		{
			get { return ((ILookupHelper<Department>)InnerData.Department).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Department, ((ILookupHelper<Department>)InnerData.Department).GetItem(null), value))
					((ILookupHelper<Department>)InnerData.Department).SetItem(value, null); 
			}
		}
		private void ClearDepartment(DateTime? moment)
		{
			((ILookupHelper<Department>)InnerData.Department).ClearLookup(moment);
		}
		public Shift Shift
		{
			get { return ((ILookupHelper<Shift>)InnerData.Shift).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Shift, ((ILookupHelper<Shift>)InnerData.Shift).GetItem(null), value))
					((ILookupHelper<Shift>)InnerData.Shift).SetItem(value, null); 
			}
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

        private static EmployeeDepartmentHistoryMembers members = null;
        public static EmployeeDepartmentHistoryMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(EmployeeDepartmentHistory))
                    {
                        if (members == null)
                            members = new EmployeeDepartmentHistoryMembers();
                    }
                }
                return members;
            }
        }
        public class EmployeeDepartmentHistoryMembers
        {
            internal EmployeeDepartmentHistoryMembers() { }

			#region Members for interface IEmployeeDepartmentHistory

            public Property StartDate { get; } = Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"].Properties["StartDate"];
            public Property EndDate { get; } = Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"].Properties["EndDate"];
            public Property Department { get; } = Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"].Properties["Department"];
            public Property Shift { get; } = Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"].Properties["Shift"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static EmployeeDepartmentHistoryFullTextMembers fullTextMembers = null;
        public static EmployeeDepartmentHistoryFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(EmployeeDepartmentHistory))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new EmployeeDepartmentHistoryFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class EmployeeDepartmentHistoryFullTextMembers
        {
            internal EmployeeDepartmentHistoryFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(EmployeeDepartmentHistory))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"];
                }
            }
            return entity;
        }

		private static EmployeeDepartmentHistoryEvents events = null;
        public static EmployeeDepartmentHistoryEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(EmployeeDepartmentHistory))
                    {
                        if (events == null)
                            events = new EmployeeDepartmentHistoryEvents();
                    }
                }
                return events;
            }
        }
        public class EmployeeDepartmentHistoryEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<EmployeeDepartmentHistory, EntityEventArgs> onNew;
            public event EventHandler<EmployeeDepartmentHistory, EntityEventArgs> OnNew
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
                EventHandler<EmployeeDepartmentHistory, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((EmployeeDepartmentHistory)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<EmployeeDepartmentHistory, EntityEventArgs> onDelete;
            public event EventHandler<EmployeeDepartmentHistory, EntityEventArgs> OnDelete
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
                EventHandler<EmployeeDepartmentHistory, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((EmployeeDepartmentHistory)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<EmployeeDepartmentHistory, EntityEventArgs> onSave;
            public event EventHandler<EmployeeDepartmentHistory, EntityEventArgs> OnSave
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
                EventHandler<EmployeeDepartmentHistory, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((EmployeeDepartmentHistory)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnStartDate

				private static bool onStartDateIsRegistered = false;

				private static EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> onStartDate;
				public static event EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> OnStartDate
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
							if (onStartDate == null && onStartDateIsRegistered)
							{
								Members.StartDate.Events.OnChange -= onStartDateProxy;
								onStartDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onStartDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> handler = onStartDate;
					if ((object)handler != null)
						handler.Invoke((EmployeeDepartmentHistory)sender, args);
				}

				#endregion

				#region OnEndDate

				private static bool onEndDateIsRegistered = false;

				private static EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> onEndDate;
				public static event EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> OnEndDate
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
							if (onEndDate == null && onEndDateIsRegistered)
							{
								Members.EndDate.Events.OnChange -= onEndDateProxy;
								onEndDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onEndDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> handler = onEndDate;
					if ((object)handler != null)
						handler.Invoke((EmployeeDepartmentHistory)sender, args);
				}

				#endregion

				#region OnDepartment

				private static bool onDepartmentIsRegistered = false;

				private static EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> onDepartment;
				public static event EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> OnDepartment
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
							if (onDepartment == null && onDepartmentIsRegistered)
							{
								Members.Department.Events.OnChange -= onDepartmentProxy;
								onDepartmentIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDepartmentProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> handler = onDepartment;
					if ((object)handler != null)
						handler.Invoke((EmployeeDepartmentHistory)sender, args);
				}

				#endregion

				#region OnShift

				private static bool onShiftIsRegistered = false;

				private static EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> onShift;
				public static event EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> OnShift
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onShiftIsRegistered)
							{
								Members.Shift.Events.OnChange -= onShiftProxy;
								Members.Shift.Events.OnChange += onShiftProxy;
								onShiftIsRegistered = true;
							}
							onShift += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onShift -= value;
							if (onShift == null && onShiftIsRegistered)
							{
								Members.Shift.Events.OnChange -= onShiftProxy;
								onShiftIsRegistered = false;
							}
						}
					}
				}
            
				private static void onShiftProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> handler = onShift;
					if ((object)handler != null)
						handler.Invoke((EmployeeDepartmentHistory)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> OnModifiedDate
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
					EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((EmployeeDepartmentHistory)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> onUid;
				public static event EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> OnUid
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
					EventHandler<EmployeeDepartmentHistory, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((EmployeeDepartmentHistory)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IEmployeeDepartmentHistoryOriginalData

		public IEmployeeDepartmentHistoryOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IEmployeeDepartmentHistory

		System.DateTime IEmployeeDepartmentHistoryOriginalData.StartDate { get { return OriginalData.StartDate; } }
		string IEmployeeDepartmentHistoryOriginalData.EndDate { get { return OriginalData.EndDate; } }
		Department IEmployeeDepartmentHistoryOriginalData.Department { get { return ((ILookupHelper<Department>)OriginalData.Department).GetOriginalItem(null); } }
		Shift IEmployeeDepartmentHistoryOriginalData.Shift { get { return ((ILookupHelper<Shift>)OriginalData.Shift).GetOriginalItem(null); } }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime IEmployeeDepartmentHistoryOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		string IEmployeeDepartmentHistoryOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}