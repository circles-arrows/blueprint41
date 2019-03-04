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
	public interface IJobCandidateOriginalData : ISchemaBaseOriginalData
    {
		int JobCandidateID { get; }
		string Resume { get; }
		Employee Employee { get; }
    }

	public partial class JobCandidate : OGM<JobCandidate, JobCandidate.JobCandidateData, System.String>, ISchemaBase, INeo4jBase, IJobCandidateOriginalData
	{
        #region Initialize

        static JobCandidate()
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

        public static Dictionary<System.String, JobCandidate> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.JobCandidateAlias, IWhereQuery> query)
        {
            q.JobCandidateAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.JobCandidate.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"JobCandidate => JobCandidateID : {this.JobCandidateID}, Resume : {this.Resume?.ToString() ?? "null"}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new JobCandidateData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.JobCandidateID == null)
				throw new PersistenceException(string.Format("Cannot save JobCandidate with key '{0}' because the JobCandidateID cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save JobCandidate with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class JobCandidateData : Data<System.String>
		{
			public JobCandidateData()
            {

            }

            public JobCandidateData(JobCandidateData data)
            {
				JobCandidateID = data.JobCandidateID;
				Resume = data.Resume;
				Employee = data.Employee;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "JobCandidate";

				Employee = new EntityCollection<Employee>(Wrapper, Members.Employee, item => { if (Members.Employee.Events.HasRegisteredChangeHandlers) { object loadHack = item.JobCandidate; } });
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("JobCandidateID",  Conversion<int, long>.Convert(JobCandidateID));
				dictionary.Add("Resume",  Resume);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("JobCandidateID", out value))
					JobCandidateID = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("Resume", out value))
					Resume = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IJobCandidate

			public int JobCandidateID { get; set; }
			public string Resume { get; set; }
			public EntityCollection<Employee> Employee { get; private set; }

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

		#region Members for interface IJobCandidate

		public int JobCandidateID { get { LazyGet(); return InnerData.JobCandidateID; } set { if (LazySet(Members.JobCandidateID, InnerData.JobCandidateID, value)) InnerData.JobCandidateID = value; } }
		public string Resume { get { LazyGet(); return InnerData.Resume; } set { if (LazySet(Members.Resume, InnerData.Resume, value)) InnerData.Resume = value; } }
		public Employee Employee
		{
			get { return ((ILookupHelper<Employee>)InnerData.Employee).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Employee, ((ILookupHelper<Employee>)InnerData.Employee).GetItem(null), value))
					((ILookupHelper<Employee>)InnerData.Employee).SetItem(value, null); 
			}
		}
		private void ClearEmployee(DateTime? moment)
		{
			((ILookupHelper<Employee>)InnerData.Employee).ClearLookup(moment);
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

        private static JobCandidateMembers members = null;
        public static JobCandidateMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(JobCandidate))
                    {
                        if (members == null)
                            members = new JobCandidateMembers();
                    }
                }
                return members;
            }
        }
        public class JobCandidateMembers
        {
            internal JobCandidateMembers() { }

			#region Members for interface IJobCandidate

            public Property JobCandidateID { get; } = Datastore.AdventureWorks.Model.Entities["JobCandidate"].Properties["JobCandidateID"];
            public Property Resume { get; } = Datastore.AdventureWorks.Model.Entities["JobCandidate"].Properties["Resume"];
            public Property Employee { get; } = Datastore.AdventureWorks.Model.Entities["JobCandidate"].Properties["Employee"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static JobCandidateFullTextMembers fullTextMembers = null;
        public static JobCandidateFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(JobCandidate))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new JobCandidateFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class JobCandidateFullTextMembers
        {
            internal JobCandidateFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(JobCandidate))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["JobCandidate"];
                }
            }
            return entity;
        }

		private static JobCandidateEvents events = null;
        public static JobCandidateEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(JobCandidate))
                    {
                        if (events == null)
                            events = new JobCandidateEvents();
                    }
                }
                return events;
            }
        }
        public class JobCandidateEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<JobCandidate, EntityEventArgs> onNew;
            public event EventHandler<JobCandidate, EntityEventArgs> OnNew
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
                EventHandler<JobCandidate, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((JobCandidate)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<JobCandidate, EntityEventArgs> onDelete;
            public event EventHandler<JobCandidate, EntityEventArgs> OnDelete
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
                EventHandler<JobCandidate, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((JobCandidate)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<JobCandidate, EntityEventArgs> onSave;
            public event EventHandler<JobCandidate, EntityEventArgs> OnSave
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
                EventHandler<JobCandidate, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((JobCandidate)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnJobCandidateID

				private static bool onJobCandidateIDIsRegistered = false;

				private static EventHandler<JobCandidate, PropertyEventArgs> onJobCandidateID;
				public static event EventHandler<JobCandidate, PropertyEventArgs> OnJobCandidateID
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onJobCandidateIDIsRegistered)
							{
								Members.JobCandidateID.Events.OnChange -= onJobCandidateIDProxy;
								Members.JobCandidateID.Events.OnChange += onJobCandidateIDProxy;
								onJobCandidateIDIsRegistered = true;
							}
							onJobCandidateID += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onJobCandidateID -= value;
							if (onJobCandidateID == null && onJobCandidateIDIsRegistered)
							{
								Members.JobCandidateID.Events.OnChange -= onJobCandidateIDProxy;
								onJobCandidateIDIsRegistered = false;
							}
						}
					}
				}
            
				private static void onJobCandidateIDProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<JobCandidate, PropertyEventArgs> handler = onJobCandidateID;
					if ((object)handler != null)
						handler.Invoke((JobCandidate)sender, args);
				}

				#endregion

				#region OnResume

				private static bool onResumeIsRegistered = false;

				private static EventHandler<JobCandidate, PropertyEventArgs> onResume;
				public static event EventHandler<JobCandidate, PropertyEventArgs> OnResume
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onResumeIsRegistered)
							{
								Members.Resume.Events.OnChange -= onResumeProxy;
								Members.Resume.Events.OnChange += onResumeProxy;
								onResumeIsRegistered = true;
							}
							onResume += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onResume -= value;
							if (onResume == null && onResumeIsRegistered)
							{
								Members.Resume.Events.OnChange -= onResumeProxy;
								onResumeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onResumeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<JobCandidate, PropertyEventArgs> handler = onResume;
					if ((object)handler != null)
						handler.Invoke((JobCandidate)sender, args);
				}

				#endregion

				#region OnEmployee

				private static bool onEmployeeIsRegistered = false;

				private static EventHandler<JobCandidate, PropertyEventArgs> onEmployee;
				public static event EventHandler<JobCandidate, PropertyEventArgs> OnEmployee
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onEmployeeIsRegistered)
							{
								Members.Employee.Events.OnChange -= onEmployeeProxy;
								Members.Employee.Events.OnChange += onEmployeeProxy;
								onEmployeeIsRegistered = true;
							}
							onEmployee += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onEmployee -= value;
							if (onEmployee == null && onEmployeeIsRegistered)
							{
								Members.Employee.Events.OnChange -= onEmployeeProxy;
								onEmployeeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onEmployeeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<JobCandidate, PropertyEventArgs> handler = onEmployee;
					if ((object)handler != null)
						handler.Invoke((JobCandidate)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<JobCandidate, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<JobCandidate, PropertyEventArgs> OnModifiedDate
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
					EventHandler<JobCandidate, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((JobCandidate)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<JobCandidate, PropertyEventArgs> onUid;
				public static event EventHandler<JobCandidate, PropertyEventArgs> OnUid
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
					EventHandler<JobCandidate, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((JobCandidate)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IJobCandidateOriginalData

		public IJobCandidateOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IJobCandidate

		int IJobCandidateOriginalData.JobCandidateID { get { return OriginalData.JobCandidateID; } }
		string IJobCandidateOriginalData.Resume { get { return OriginalData.Resume; } }
		Employee IJobCandidateOriginalData.Employee { get { return ((ILookupHelper<Employee>)OriginalData.Employee).GetOriginalItem(null); } }

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