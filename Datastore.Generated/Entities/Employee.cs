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
	public interface IEmployeeOriginalData : ISchemaBaseOriginalData
    {
		string NationalIDNumber { get; }
		int LoginID { get; }
		string JobTitle { get; }
		System.DateTime BirthDate { get; }
		string MaritalStatus { get; }
		string Gender { get; }
		string HireDate { get; }
		string SalariedFlag { get; }
		string VacationHours { get; }
		string SickLeaveHours { get; }
		string Currentflag { get; }
		string rowguid { get; }
		EmployeePayHistory EmployeePayHistory { get; }
		SalesPerson SalesPerson { get; }
		EmployeeDepartmentHistory EmployeeDepartmentHistory { get; }
		Shift Shift { get; }
		JobCandidate JobCandidate { get; }
		IEnumerable<Vendor> Vendors { get; }
    }

	public partial class Employee : OGM<Employee, Employee.EmployeeData, System.String>, ISchemaBase, INeo4jBase, IEmployeeOriginalData
	{
        #region Initialize

        static Employee()
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

        public static Dictionary<System.String, Employee> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.EmployeeAlias, IWhereQuery> query)
        {
            q.EmployeeAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Employee.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"Employee => NationalIDNumber : {this.NationalIDNumber}, LoginID : {this.LoginID}, JobTitle : {this.JobTitle}, BirthDate : {this.BirthDate}, MaritalStatus : {this.MaritalStatus}, Gender : {this.Gender}, HireDate : {this.HireDate}, SalariedFlag : {this.SalariedFlag}, VacationHours : {this.VacationHours}, SickLeaveHours : {this.SickLeaveHours}, Currentflag : {this.Currentflag}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new EmployeeData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.NationalIDNumber == null)
				throw new PersistenceException(string.Format("Cannot save Employee with key '{0}' because the NationalIDNumber cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.LoginID == null)
				throw new PersistenceException(string.Format("Cannot save Employee with key '{0}' because the LoginID cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.JobTitle == null)
				throw new PersistenceException(string.Format("Cannot save Employee with key '{0}' because the JobTitle cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.BirthDate == null)
				throw new PersistenceException(string.Format("Cannot save Employee with key '{0}' because the BirthDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.MaritalStatus == null)
				throw new PersistenceException(string.Format("Cannot save Employee with key '{0}' because the MaritalStatus cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Gender == null)
				throw new PersistenceException(string.Format("Cannot save Employee with key '{0}' because the Gender cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.HireDate == null)
				throw new PersistenceException(string.Format("Cannot save Employee with key '{0}' because the HireDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.SalariedFlag == null)
				throw new PersistenceException(string.Format("Cannot save Employee with key '{0}' because the SalariedFlag cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.VacationHours == null)
				throw new PersistenceException(string.Format("Cannot save Employee with key '{0}' because the VacationHours cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.SickLeaveHours == null)
				throw new PersistenceException(string.Format("Cannot save Employee with key '{0}' because the SickLeaveHours cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Currentflag == null)
				throw new PersistenceException(string.Format("Cannot save Employee with key '{0}' because the Currentflag cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid == null)
				throw new PersistenceException(string.Format("Cannot save Employee with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save Employee with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
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
				NationalIDNumber = data.NationalIDNumber;
				LoginID = data.LoginID;
				JobTitle = data.JobTitle;
				BirthDate = data.BirthDate;
				MaritalStatus = data.MaritalStatus;
				Gender = data.Gender;
				HireDate = data.HireDate;
				SalariedFlag = data.SalariedFlag;
				VacationHours = data.VacationHours;
				SickLeaveHours = data.SickLeaveHours;
				Currentflag = data.Currentflag;
				rowguid = data.rowguid;
				EmployeePayHistory = data.EmployeePayHistory;
				SalesPerson = data.SalesPerson;
				EmployeeDepartmentHistory = data.EmployeeDepartmentHistory;
				Shift = data.Shift;
				JobCandidate = data.JobCandidate;
				Vendors = data.Vendors;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Employee";

				EmployeePayHistory = new EntityCollection<EmployeePayHistory>(Wrapper, Members.EmployeePayHistory);
				SalesPerson = new EntityCollection<SalesPerson>(Wrapper, Members.SalesPerson);
				EmployeeDepartmentHistory = new EntityCollection<EmployeeDepartmentHistory>(Wrapper, Members.EmployeeDepartmentHistory);
				Shift = new EntityCollection<Shift>(Wrapper, Members.Shift);
				JobCandidate = new EntityCollection<JobCandidate>(Wrapper, Members.JobCandidate, item => { if (Members.JobCandidate.Events.HasRegisteredChangeHandlers) { object loadHack = item.Employee; } });
				Vendors = new EntityCollection<Vendor>(Wrapper, Members.Vendors, item => { if (Members.Vendors.Events.HasRegisteredChangeHandlers) { object loadHack = item.Employee; } });
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("NationalIDNumber",  NationalIDNumber);
				dictionary.Add("LoginID",  Conversion<int, long>.Convert(LoginID));
				dictionary.Add("JobTitle",  JobTitle);
				dictionary.Add("BirthDate",  Conversion<System.DateTime, long>.Convert(BirthDate));
				dictionary.Add("MaritalStatus",  MaritalStatus);
				dictionary.Add("Gender",  Gender);
				dictionary.Add("HireDate",  HireDate);
				dictionary.Add("SalariedFlag",  SalariedFlag);
				dictionary.Add("VacationHours",  VacationHours);
				dictionary.Add("SickLeaveHours",  SickLeaveHours);
				dictionary.Add("Currentflag",  Currentflag);
				dictionary.Add("rowguid",  rowguid);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("NationalIDNumber", out value))
					NationalIDNumber = (string)value;
				if (properties.TryGetValue("LoginID", out value))
					LoginID = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("JobTitle", out value))
					JobTitle = (string)value;
				if (properties.TryGetValue("BirthDate", out value))
					BirthDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("MaritalStatus", out value))
					MaritalStatus = (string)value;
				if (properties.TryGetValue("Gender", out value))
					Gender = (string)value;
				if (properties.TryGetValue("HireDate", out value))
					HireDate = (string)value;
				if (properties.TryGetValue("SalariedFlag", out value))
					SalariedFlag = (string)value;
				if (properties.TryGetValue("VacationHours", out value))
					VacationHours = (string)value;
				if (properties.TryGetValue("SickLeaveHours", out value))
					SickLeaveHours = (string)value;
				if (properties.TryGetValue("Currentflag", out value))
					Currentflag = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IEmployee

			public string NationalIDNumber { get; set; }
			public int LoginID { get; set; }
			public string JobTitle { get; set; }
			public System.DateTime BirthDate { get; set; }
			public string MaritalStatus { get; set; }
			public string Gender { get; set; }
			public string HireDate { get; set; }
			public string SalariedFlag { get; set; }
			public string VacationHours { get; set; }
			public string SickLeaveHours { get; set; }
			public string Currentflag { get; set; }
			public string rowguid { get; set; }
			public EntityCollection<EmployeePayHistory> EmployeePayHistory { get; private set; }
			public EntityCollection<SalesPerson> SalesPerson { get; private set; }
			public EntityCollection<EmployeeDepartmentHistory> EmployeeDepartmentHistory { get; private set; }
			public EntityCollection<Shift> Shift { get; private set; }
			public EntityCollection<JobCandidate> JobCandidate { get; private set; }
			public EntityCollection<Vendor> Vendors { get; private set; }

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

		#region Members for interface IEmployee

		public string NationalIDNumber { get { LazyGet(); return InnerData.NationalIDNumber; } set { if (LazySet(Members.NationalIDNumber, InnerData.NationalIDNumber, value)) InnerData.NationalIDNumber = value; } }
		public int LoginID { get { LazyGet(); return InnerData.LoginID; } set { if (LazySet(Members.LoginID, InnerData.LoginID, value)) InnerData.LoginID = value; } }
		public string JobTitle { get { LazyGet(); return InnerData.JobTitle; } set { if (LazySet(Members.JobTitle, InnerData.JobTitle, value)) InnerData.JobTitle = value; } }
		public System.DateTime BirthDate { get { LazyGet(); return InnerData.BirthDate; } set { if (LazySet(Members.BirthDate, InnerData.BirthDate, value)) InnerData.BirthDate = value; } }
		public string MaritalStatus { get { LazyGet(); return InnerData.MaritalStatus; } set { if (LazySet(Members.MaritalStatus, InnerData.MaritalStatus, value)) InnerData.MaritalStatus = value; } }
		public string Gender { get { LazyGet(); return InnerData.Gender; } set { if (LazySet(Members.Gender, InnerData.Gender, value)) InnerData.Gender = value; } }
		public string HireDate { get { LazyGet(); return InnerData.HireDate; } set { if (LazySet(Members.HireDate, InnerData.HireDate, value)) InnerData.HireDate = value; } }
		public string SalariedFlag { get { LazyGet(); return InnerData.SalariedFlag; } set { if (LazySet(Members.SalariedFlag, InnerData.SalariedFlag, value)) InnerData.SalariedFlag = value; } }
		public string VacationHours { get { LazyGet(); return InnerData.VacationHours; } set { if (LazySet(Members.VacationHours, InnerData.VacationHours, value)) InnerData.VacationHours = value; } }
		public string SickLeaveHours { get { LazyGet(); return InnerData.SickLeaveHours; } set { if (LazySet(Members.SickLeaveHours, InnerData.SickLeaveHours, value)) InnerData.SickLeaveHours = value; } }
		public string Currentflag { get { LazyGet(); return InnerData.Currentflag; } set { if (LazySet(Members.Currentflag, InnerData.Currentflag, value)) InnerData.Currentflag = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }
		public EmployeePayHistory EmployeePayHistory
		{
			get { return ((ILookupHelper<EmployeePayHistory>)InnerData.EmployeePayHistory).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.EmployeePayHistory, ((ILookupHelper<EmployeePayHistory>)InnerData.EmployeePayHistory).GetItem(null), value))
					((ILookupHelper<EmployeePayHistory>)InnerData.EmployeePayHistory).SetItem(value, null); 
			}
		}
		public SalesPerson SalesPerson
		{
			get { return ((ILookupHelper<SalesPerson>)InnerData.SalesPerson).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.SalesPerson, ((ILookupHelper<SalesPerson>)InnerData.SalesPerson).GetItem(null), value))
					((ILookupHelper<SalesPerson>)InnerData.SalesPerson).SetItem(value, null); 
			}
		}
		public EmployeeDepartmentHistory EmployeeDepartmentHistory
		{
			get { return ((ILookupHelper<EmployeeDepartmentHistory>)InnerData.EmployeeDepartmentHistory).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.EmployeeDepartmentHistory, ((ILookupHelper<EmployeeDepartmentHistory>)InnerData.EmployeeDepartmentHistory).GetItem(null), value))
					((ILookupHelper<EmployeeDepartmentHistory>)InnerData.EmployeeDepartmentHistory).SetItem(value, null); 
			}
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
		public JobCandidate JobCandidate
		{
			get { return ((ILookupHelper<JobCandidate>)InnerData.JobCandidate).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.JobCandidate, ((ILookupHelper<JobCandidate>)InnerData.JobCandidate).GetItem(null), value))
					((ILookupHelper<JobCandidate>)InnerData.JobCandidate).SetItem(value, null); 
			}
		}
		private void ClearJobCandidate(DateTime? moment)
		{
			((ILookupHelper<JobCandidate>)InnerData.JobCandidate).ClearLookup(moment);
		}
		public EntityCollection<Vendor> Vendors { get { return InnerData.Vendors; } }
		private void ClearVendors(DateTime? moment)
		{
			((ILookupHelper<EntityCollection<Vendor>>)InnerData.Vendors).ClearLookup(moment);
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

        private static EmployeeMembers members = null;
        public static EmployeeMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(Employee))
                    {
                        if (members == null)
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

            public Property NationalIDNumber { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["NationalIDNumber"];
            public Property LoginID { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["LoginID"];
            public Property JobTitle { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["JobTitle"];
            public Property BirthDate { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["BirthDate"];
            public Property MaritalStatus { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["MaritalStatus"];
            public Property Gender { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["Gender"];
            public Property HireDate { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["HireDate"];
            public Property SalariedFlag { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["SalariedFlag"];
            public Property VacationHours { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["VacationHours"];
            public Property SickLeaveHours { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["SickLeaveHours"];
            public Property Currentflag { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["Currentflag"];
            public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["rowguid"];
            public Property EmployeePayHistory { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["EmployeePayHistory"];
            public Property SalesPerson { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["SalesPerson"];
            public Property EmployeeDepartmentHistory { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["EmployeeDepartmentHistory"];
            public Property Shift { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["Shift"];
            public Property JobCandidate { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["JobCandidate"];
            public Property Vendors { get; } = Datastore.AdventureWorks.Model.Entities["Employee"].Properties["Vendors"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static EmployeeFullTextMembers fullTextMembers = null;
        public static EmployeeFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(Employee))
                    {
                        if (fullTextMembers == null)
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
            if (entity == null)
            {
                lock (typeof(Employee))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["Employee"];
                }
            }
            return entity;
        }

		private static EmployeeEvents events = null;
        public static EmployeeEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(Employee))
                    {
                        if (events == null)
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
                EventHandler<Employee, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
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
                EventHandler<Employee, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
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
                EventHandler<Employee, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((Employee)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnNationalIDNumber

				private static bool onNationalIDNumberIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onNationalIDNumber;
				public static event EventHandler<Employee, PropertyEventArgs> OnNationalIDNumber
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onNationalIDNumberIsRegistered)
							{
								Members.NationalIDNumber.Events.OnChange -= onNationalIDNumberProxy;
								Members.NationalIDNumber.Events.OnChange += onNationalIDNumberProxy;
								onNationalIDNumberIsRegistered = true;
							}
							onNationalIDNumber += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onNationalIDNumber -= value;
							if (onNationalIDNumber == null && onNationalIDNumberIsRegistered)
							{
								Members.NationalIDNumber.Events.OnChange -= onNationalIDNumberProxy;
								onNationalIDNumberIsRegistered = false;
							}
						}
					}
				}
            
				private static void onNationalIDNumberProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onNationalIDNumber;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnLoginID

				private static bool onLoginIDIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onLoginID;
				public static event EventHandler<Employee, PropertyEventArgs> OnLoginID
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onLoginIDIsRegistered)
							{
								Members.LoginID.Events.OnChange -= onLoginIDProxy;
								Members.LoginID.Events.OnChange += onLoginIDProxy;
								onLoginIDIsRegistered = true;
							}
							onLoginID += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onLoginID -= value;
							if (onLoginID == null && onLoginIDIsRegistered)
							{
								Members.LoginID.Events.OnChange -= onLoginIDProxy;
								onLoginIDIsRegistered = false;
							}
						}
					}
				}
            
				private static void onLoginIDProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onLoginID;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnJobTitle

				private static bool onJobTitleIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onJobTitle;
				public static event EventHandler<Employee, PropertyEventArgs> OnJobTitle
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onJobTitleIsRegistered)
							{
								Members.JobTitle.Events.OnChange -= onJobTitleProxy;
								Members.JobTitle.Events.OnChange += onJobTitleProxy;
								onJobTitleIsRegistered = true;
							}
							onJobTitle += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onJobTitle -= value;
							if (onJobTitle == null && onJobTitleIsRegistered)
							{
								Members.JobTitle.Events.OnChange -= onJobTitleProxy;
								onJobTitleIsRegistered = false;
							}
						}
					}
				}
            
				private static void onJobTitleProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onJobTitle;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnBirthDate

				private static bool onBirthDateIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onBirthDate;
				public static event EventHandler<Employee, PropertyEventArgs> OnBirthDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onBirthDateIsRegistered)
							{
								Members.BirthDate.Events.OnChange -= onBirthDateProxy;
								Members.BirthDate.Events.OnChange += onBirthDateProxy;
								onBirthDateIsRegistered = true;
							}
							onBirthDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onBirthDate -= value;
							if (onBirthDate == null && onBirthDateIsRegistered)
							{
								Members.BirthDate.Events.OnChange -= onBirthDateProxy;
								onBirthDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onBirthDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onBirthDate;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnMaritalStatus

				private static bool onMaritalStatusIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onMaritalStatus;
				public static event EventHandler<Employee, PropertyEventArgs> OnMaritalStatus
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onMaritalStatusIsRegistered)
							{
								Members.MaritalStatus.Events.OnChange -= onMaritalStatusProxy;
								Members.MaritalStatus.Events.OnChange += onMaritalStatusProxy;
								onMaritalStatusIsRegistered = true;
							}
							onMaritalStatus += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onMaritalStatus -= value;
							if (onMaritalStatus == null && onMaritalStatusIsRegistered)
							{
								Members.MaritalStatus.Events.OnChange -= onMaritalStatusProxy;
								onMaritalStatusIsRegistered = false;
							}
						}
					}
				}
            
				private static void onMaritalStatusProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onMaritalStatus;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnGender

				private static bool onGenderIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onGender;
				public static event EventHandler<Employee, PropertyEventArgs> OnGender
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onGenderIsRegistered)
							{
								Members.Gender.Events.OnChange -= onGenderProxy;
								Members.Gender.Events.OnChange += onGenderProxy;
								onGenderIsRegistered = true;
							}
							onGender += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onGender -= value;
							if (onGender == null && onGenderIsRegistered)
							{
								Members.Gender.Events.OnChange -= onGenderProxy;
								onGenderIsRegistered = false;
							}
						}
					}
				}
            
				private static void onGenderProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onGender;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnHireDate

				private static bool onHireDateIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onHireDate;
				public static event EventHandler<Employee, PropertyEventArgs> OnHireDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onHireDateIsRegistered)
							{
								Members.HireDate.Events.OnChange -= onHireDateProxy;
								Members.HireDate.Events.OnChange += onHireDateProxy;
								onHireDateIsRegistered = true;
							}
							onHireDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onHireDate -= value;
							if (onHireDate == null && onHireDateIsRegistered)
							{
								Members.HireDate.Events.OnChange -= onHireDateProxy;
								onHireDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onHireDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onHireDate;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnSalariedFlag

				private static bool onSalariedFlagIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onSalariedFlag;
				public static event EventHandler<Employee, PropertyEventArgs> OnSalariedFlag
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalariedFlagIsRegistered)
							{
								Members.SalariedFlag.Events.OnChange -= onSalariedFlagProxy;
								Members.SalariedFlag.Events.OnChange += onSalariedFlagProxy;
								onSalariedFlagIsRegistered = true;
							}
							onSalariedFlag += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalariedFlag -= value;
							if (onSalariedFlag == null && onSalariedFlagIsRegistered)
							{
								Members.SalariedFlag.Events.OnChange -= onSalariedFlagProxy;
								onSalariedFlagIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSalariedFlagProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onSalariedFlag;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnVacationHours

				private static bool onVacationHoursIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onVacationHours;
				public static event EventHandler<Employee, PropertyEventArgs> OnVacationHours
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onVacationHoursIsRegistered)
							{
								Members.VacationHours.Events.OnChange -= onVacationHoursProxy;
								Members.VacationHours.Events.OnChange += onVacationHoursProxy;
								onVacationHoursIsRegistered = true;
							}
							onVacationHours += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onVacationHours -= value;
							if (onVacationHours == null && onVacationHoursIsRegistered)
							{
								Members.VacationHours.Events.OnChange -= onVacationHoursProxy;
								onVacationHoursIsRegistered = false;
							}
						}
					}
				}
            
				private static void onVacationHoursProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onVacationHours;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnSickLeaveHours

				private static bool onSickLeaveHoursIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onSickLeaveHours;
				public static event EventHandler<Employee, PropertyEventArgs> OnSickLeaveHours
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSickLeaveHoursIsRegistered)
							{
								Members.SickLeaveHours.Events.OnChange -= onSickLeaveHoursProxy;
								Members.SickLeaveHours.Events.OnChange += onSickLeaveHoursProxy;
								onSickLeaveHoursIsRegistered = true;
							}
							onSickLeaveHours += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSickLeaveHours -= value;
							if (onSickLeaveHours == null && onSickLeaveHoursIsRegistered)
							{
								Members.SickLeaveHours.Events.OnChange -= onSickLeaveHoursProxy;
								onSickLeaveHoursIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSickLeaveHoursProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onSickLeaveHours;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnCurrentflag

				private static bool onCurrentflagIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onCurrentflag;
				public static event EventHandler<Employee, PropertyEventArgs> OnCurrentflag
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCurrentflagIsRegistered)
							{
								Members.Currentflag.Events.OnChange -= onCurrentflagProxy;
								Members.Currentflag.Events.OnChange += onCurrentflagProxy;
								onCurrentflagIsRegistered = true;
							}
							onCurrentflag += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCurrentflag -= value;
							if (onCurrentflag == null && onCurrentflagIsRegistered)
							{
								Members.Currentflag.Events.OnChange -= onCurrentflagProxy;
								onCurrentflagIsRegistered = false;
							}
						}
					}
				}
            
				private static void onCurrentflagProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onCurrentflag;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onrowguid;
				public static event EventHandler<Employee, PropertyEventArgs> Onrowguid
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onrowguidIsRegistered)
							{
								Members.rowguid.Events.OnChange -= onrowguidProxy;
								Members.rowguid.Events.OnChange += onrowguidProxy;
								onrowguidIsRegistered = true;
							}
							onrowguid += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onrowguid -= value;
							if (onrowguid == null && onrowguidIsRegistered)
							{
								Members.rowguid.Events.OnChange -= onrowguidProxy;
								onrowguidIsRegistered = false;
							}
						}
					}
				}
            
				private static void onrowguidProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onrowguid;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnEmployeePayHistory

				private static bool onEmployeePayHistoryIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onEmployeePayHistory;
				public static event EventHandler<Employee, PropertyEventArgs> OnEmployeePayHistory
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onEmployeePayHistoryIsRegistered)
							{
								Members.EmployeePayHistory.Events.OnChange -= onEmployeePayHistoryProxy;
								Members.EmployeePayHistory.Events.OnChange += onEmployeePayHistoryProxy;
								onEmployeePayHistoryIsRegistered = true;
							}
							onEmployeePayHistory += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onEmployeePayHistory -= value;
							if (onEmployeePayHistory == null && onEmployeePayHistoryIsRegistered)
							{
								Members.EmployeePayHistory.Events.OnChange -= onEmployeePayHistoryProxy;
								onEmployeePayHistoryIsRegistered = false;
							}
						}
					}
				}
            
				private static void onEmployeePayHistoryProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onEmployeePayHistory;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnSalesPerson

				private static bool onSalesPersonIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onSalesPerson;
				public static event EventHandler<Employee, PropertyEventArgs> OnSalesPerson
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalesPersonIsRegistered)
							{
								Members.SalesPerson.Events.OnChange -= onSalesPersonProxy;
								Members.SalesPerson.Events.OnChange += onSalesPersonProxy;
								onSalesPersonIsRegistered = true;
							}
							onSalesPerson += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalesPerson -= value;
							if (onSalesPerson == null && onSalesPersonIsRegistered)
							{
								Members.SalesPerson.Events.OnChange -= onSalesPersonProxy;
								onSalesPersonIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSalesPersonProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onSalesPerson;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnEmployeeDepartmentHistory

				private static bool onEmployeeDepartmentHistoryIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onEmployeeDepartmentHistory;
				public static event EventHandler<Employee, PropertyEventArgs> OnEmployeeDepartmentHistory
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onEmployeeDepartmentHistoryIsRegistered)
							{
								Members.EmployeeDepartmentHistory.Events.OnChange -= onEmployeeDepartmentHistoryProxy;
								Members.EmployeeDepartmentHistory.Events.OnChange += onEmployeeDepartmentHistoryProxy;
								onEmployeeDepartmentHistoryIsRegistered = true;
							}
							onEmployeeDepartmentHistory += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onEmployeeDepartmentHistory -= value;
							if (onEmployeeDepartmentHistory == null && onEmployeeDepartmentHistoryIsRegistered)
							{
								Members.EmployeeDepartmentHistory.Events.OnChange -= onEmployeeDepartmentHistoryProxy;
								onEmployeeDepartmentHistoryIsRegistered = false;
							}
						}
					}
				}
            
				private static void onEmployeeDepartmentHistoryProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onEmployeeDepartmentHistory;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnShift

				private static bool onShiftIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onShift;
				public static event EventHandler<Employee, PropertyEventArgs> OnShift
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
					EventHandler<Employee, PropertyEventArgs> handler = onShift;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnJobCandidate

				private static bool onJobCandidateIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onJobCandidate;
				public static event EventHandler<Employee, PropertyEventArgs> OnJobCandidate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onJobCandidateIsRegistered)
							{
								Members.JobCandidate.Events.OnChange -= onJobCandidateProxy;
								Members.JobCandidate.Events.OnChange += onJobCandidateProxy;
								onJobCandidateIsRegistered = true;
							}
							onJobCandidate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onJobCandidate -= value;
							if (onJobCandidate == null && onJobCandidateIsRegistered)
							{
								Members.JobCandidate.Events.OnChange -= onJobCandidateProxy;
								onJobCandidateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onJobCandidateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onJobCandidate;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnVendors

				private static bool onVendorsIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onVendors;
				public static event EventHandler<Employee, PropertyEventArgs> OnVendors
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onVendorsIsRegistered)
							{
								Members.Vendors.Events.OnChange -= onVendorsProxy;
								Members.Vendors.Events.OnChange += onVendorsProxy;
								onVendorsIsRegistered = true;
							}
							onVendors += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onVendors -= value;
							if (onVendors == null && onVendorsIsRegistered)
							{
								Members.Vendors.Events.OnChange -= onVendorsProxy;
								onVendorsIsRegistered = false;
							}
						}
					}
				}
            
				private static void onVendorsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Employee, PropertyEventArgs> handler = onVendors;
					if ((object)handler != null)
						handler.Invoke((Employee)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<Employee, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<Employee, PropertyEventArgs> OnModifiedDate
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
					EventHandler<Employee, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
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
					EventHandler<Employee, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
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

		string IEmployeeOriginalData.NationalIDNumber { get { return OriginalData.NationalIDNumber; } }
		int IEmployeeOriginalData.LoginID { get { return OriginalData.LoginID; } }
		string IEmployeeOriginalData.JobTitle { get { return OriginalData.JobTitle; } }
		System.DateTime IEmployeeOriginalData.BirthDate { get { return OriginalData.BirthDate; } }
		string IEmployeeOriginalData.MaritalStatus { get { return OriginalData.MaritalStatus; } }
		string IEmployeeOriginalData.Gender { get { return OriginalData.Gender; } }
		string IEmployeeOriginalData.HireDate { get { return OriginalData.HireDate; } }
		string IEmployeeOriginalData.SalariedFlag { get { return OriginalData.SalariedFlag; } }
		string IEmployeeOriginalData.VacationHours { get { return OriginalData.VacationHours; } }
		string IEmployeeOriginalData.SickLeaveHours { get { return OriginalData.SickLeaveHours; } }
		string IEmployeeOriginalData.Currentflag { get { return OriginalData.Currentflag; } }
		string IEmployeeOriginalData.rowguid { get { return OriginalData.rowguid; } }
		EmployeePayHistory IEmployeeOriginalData.EmployeePayHistory { get { return ((ILookupHelper<EmployeePayHistory>)OriginalData.EmployeePayHistory).GetOriginalItem(null); } }
		SalesPerson IEmployeeOriginalData.SalesPerson { get { return ((ILookupHelper<SalesPerson>)OriginalData.SalesPerson).GetOriginalItem(null); } }
		EmployeeDepartmentHistory IEmployeeOriginalData.EmployeeDepartmentHistory { get { return ((ILookupHelper<EmployeeDepartmentHistory>)OriginalData.EmployeeDepartmentHistory).GetOriginalItem(null); } }
		Shift IEmployeeOriginalData.Shift { get { return ((ILookupHelper<Shift>)OriginalData.Shift).GetOriginalItem(null); } }
		JobCandidate IEmployeeOriginalData.JobCandidate { get { return ((ILookupHelper<JobCandidate>)OriginalData.JobCandidate).GetOriginalItem(null); } }
		IEnumerable<Vendor> IEmployeeOriginalData.Vendors { get { return OriginalData.Vendors.OriginalData; } }

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