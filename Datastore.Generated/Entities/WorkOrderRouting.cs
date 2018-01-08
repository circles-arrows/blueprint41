 
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
	public interface IWorkOrderRoutingOriginalData
    {
		#region Outer Data

		#region Members for interface IWorkOrderRouting

		string OperationSequence { get; }
		System.DateTime ScheduledStartDate { get; }
		System.DateTime ScheduledEndDate { get; }
		System.DateTime ActualStartDate { get; }
		System.DateTime ActualEndDate { get; }
		string ActualResourceHrs { get; }
		string PlannedCost { get; }
		string ActualCost { get; }
		WorkOrder WorkOrder { get; }
		Product Product { get; }
		Location Location { get; }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ModifiedDate { get; }

		#endregion
		#region Members for interface INeo4jBase

		string Uid { get; }

		#endregion
		#endregion
   
	}

	public partial class WorkOrderRouting : OGM<WorkOrderRouting, WorkOrderRouting.WorkOrderRoutingData, System.String>, ISchemaBase, INeo4jBase, IWorkOrderRoutingOriginalData
	{
        #region Initialize

        static WorkOrderRouting()
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

        public static Dictionary<System.String, WorkOrderRouting> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.WorkOrderRoutingAlias, IWhereQuery> query)
        {
            q.WorkOrderRoutingAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.WorkOrderRouting.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"WorkOrderRouting => OperationSequence : {this.OperationSequence}, ScheduledStartDate : {this.ScheduledStartDate}, ScheduledEndDate : {this.ScheduledEndDate}, ActualStartDate : {this.ActualStartDate}, ActualEndDate : {this.ActualEndDate}, ActualResourceHrs : {this.ActualResourceHrs}, PlannedCost : {this.PlannedCost}, ActualCost : {this.ActualCost}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new WorkOrderRoutingData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.OperationSequence == null)
				throw new PersistenceException(string.Format("Cannot save WorkOrderRouting with key '{0}' because the OperationSequence cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ScheduledStartDate == null)
				throw new PersistenceException(string.Format("Cannot save WorkOrderRouting with key '{0}' because the ScheduledStartDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ScheduledEndDate == null)
				throw new PersistenceException(string.Format("Cannot save WorkOrderRouting with key '{0}' because the ScheduledEndDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ActualStartDate == null)
				throw new PersistenceException(string.Format("Cannot save WorkOrderRouting with key '{0}' because the ActualStartDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ActualEndDate == null)
				throw new PersistenceException(string.Format("Cannot save WorkOrderRouting with key '{0}' because the ActualEndDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ActualResourceHrs == null)
				throw new PersistenceException(string.Format("Cannot save WorkOrderRouting with key '{0}' because the ActualResourceHrs cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.PlannedCost == null)
				throw new PersistenceException(string.Format("Cannot save WorkOrderRouting with key '{0}' because the PlannedCost cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ActualCost == null)
				throw new PersistenceException(string.Format("Cannot save WorkOrderRouting with key '{0}' because the ActualCost cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save WorkOrderRouting with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class WorkOrderRoutingData : Data<System.String>
		{
			public WorkOrderRoutingData()
            {

            }

            public WorkOrderRoutingData(WorkOrderRoutingData data)
            {
				OperationSequence = data.OperationSequence;
				ScheduledStartDate = data.ScheduledStartDate;
				ScheduledEndDate = data.ScheduledEndDate;
				ActualStartDate = data.ActualStartDate;
				ActualEndDate = data.ActualEndDate;
				ActualResourceHrs = data.ActualResourceHrs;
				PlannedCost = data.PlannedCost;
				ActualCost = data.ActualCost;
				WorkOrder = data.WorkOrder;
				Product = data.Product;
				Location = data.Location;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "WorkOrderRouting";

				WorkOrder = new EntityCollection<WorkOrder>(Wrapper, Members.WorkOrder);
				Product = new EntityCollection<Product>(Wrapper, Members.Product);
				Location = new EntityCollection<Location>(Wrapper, Members.Location);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("OperationSequence",  OperationSequence);
				dictionary.Add("ScheduledStartDate",  Conversion<System.DateTime, long>.Convert(ScheduledStartDate));
				dictionary.Add("ScheduledEndDate",  Conversion<System.DateTime, long>.Convert(ScheduledEndDate));
				dictionary.Add("ActualStartDate",  Conversion<System.DateTime, long>.Convert(ActualStartDate));
				dictionary.Add("ActualEndDate",  Conversion<System.DateTime, long>.Convert(ActualEndDate));
				dictionary.Add("ActualResourceHrs",  ActualResourceHrs);
				dictionary.Add("PlannedCost",  PlannedCost);
				dictionary.Add("ActualCost",  ActualCost);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("OperationSequence", out value))
					OperationSequence = (string)value;
				if (properties.TryGetValue("ScheduledStartDate", out value))
					ScheduledStartDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("ScheduledEndDate", out value))
					ScheduledEndDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("ActualStartDate", out value))
					ActualStartDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("ActualEndDate", out value))
					ActualEndDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("ActualResourceHrs", out value))
					ActualResourceHrs = (string)value;
				if (properties.TryGetValue("PlannedCost", out value))
					PlannedCost = (string)value;
				if (properties.TryGetValue("ActualCost", out value))
					ActualCost = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IWorkOrderRouting

			public string OperationSequence { get; set; }
			public System.DateTime ScheduledStartDate { get; set; }
			public System.DateTime ScheduledEndDate { get; set; }
			public System.DateTime ActualStartDate { get; set; }
			public System.DateTime ActualEndDate { get; set; }
			public string ActualResourceHrs { get; set; }
			public string PlannedCost { get; set; }
			public string ActualCost { get; set; }
			public EntityCollection<WorkOrder> WorkOrder { get; private set; }
			public EntityCollection<Product> Product { get; private set; }
			public EntityCollection<Location> Location { get; private set; }

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

		#region Members for interface IWorkOrderRouting

		public string OperationSequence { get { LazyGet(); return InnerData.OperationSequence; } set { if (LazySet(Members.OperationSequence, InnerData.OperationSequence, value)) InnerData.OperationSequence = value; } }
		public System.DateTime ScheduledStartDate { get { LazyGet(); return InnerData.ScheduledStartDate; } set { if (LazySet(Members.ScheduledStartDate, InnerData.ScheduledStartDate, value)) InnerData.ScheduledStartDate = value; } }
		public System.DateTime ScheduledEndDate { get { LazyGet(); return InnerData.ScheduledEndDate; } set { if (LazySet(Members.ScheduledEndDate, InnerData.ScheduledEndDate, value)) InnerData.ScheduledEndDate = value; } }
		public System.DateTime ActualStartDate { get { LazyGet(); return InnerData.ActualStartDate; } set { if (LazySet(Members.ActualStartDate, InnerData.ActualStartDate, value)) InnerData.ActualStartDate = value; } }
		public System.DateTime ActualEndDate { get { LazyGet(); return InnerData.ActualEndDate; } set { if (LazySet(Members.ActualEndDate, InnerData.ActualEndDate, value)) InnerData.ActualEndDate = value; } }
		public string ActualResourceHrs { get { LazyGet(); return InnerData.ActualResourceHrs; } set { if (LazySet(Members.ActualResourceHrs, InnerData.ActualResourceHrs, value)) InnerData.ActualResourceHrs = value; } }
		public string PlannedCost { get { LazyGet(); return InnerData.PlannedCost; } set { if (LazySet(Members.PlannedCost, InnerData.PlannedCost, value)) InnerData.PlannedCost = value; } }
		public string ActualCost { get { LazyGet(); return InnerData.ActualCost; } set { if (LazySet(Members.ActualCost, InnerData.ActualCost, value)) InnerData.ActualCost = value; } }
		public WorkOrder WorkOrder
		{
			get { return ((ILookupHelper<WorkOrder>)InnerData.WorkOrder).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.WorkOrder, ((ILookupHelper<WorkOrder>)InnerData.WorkOrder).GetItem(null), value))
					((ILookupHelper<WorkOrder>)InnerData.WorkOrder).SetItem(value, null); 
			}
		}
		public Product Product
		{
			get { return ((ILookupHelper<Product>)InnerData.Product).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Product, ((ILookupHelper<Product>)InnerData.Product).GetItem(null), value))
					((ILookupHelper<Product>)InnerData.Product).SetItem(value, null); 
			}
		}
		public Location Location
		{
			get { return ((ILookupHelper<Location>)InnerData.Location).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Location, ((ILookupHelper<Location>)InnerData.Location).GetItem(null), value))
					((ILookupHelper<Location>)InnerData.Location).SetItem(value, null); 
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

        private static WorkOrderRoutingMembers members = null;
        public static WorkOrderRoutingMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(WorkOrderRouting))
                    {
                        if (members == null)
                            members = new WorkOrderRoutingMembers();
                    }
                }
                return members;
            }
        }
        public class WorkOrderRoutingMembers
        {
            internal WorkOrderRoutingMembers() { }

			#region Members for interface IWorkOrderRouting

            public Property OperationSequence { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["OperationSequence"];
            public Property ScheduledStartDate { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ScheduledStartDate"];
            public Property ScheduledEndDate { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ScheduledEndDate"];
            public Property ActualStartDate { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ActualStartDate"];
            public Property ActualEndDate { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ActualEndDate"];
            public Property ActualResourceHrs { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ActualResourceHrs"];
            public Property PlannedCost { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["PlannedCost"];
            public Property ActualCost { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ActualCost"];
            public Property WorkOrder { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["WorkOrder"];
            public Property Product { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["Product"];
            public Property Location { get; } = Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["Location"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static WorkOrderRoutingFullTextMembers fullTextMembers = null;
        public static WorkOrderRoutingFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(WorkOrderRouting))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new WorkOrderRoutingFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class WorkOrderRoutingFullTextMembers
        {
            internal WorkOrderRoutingFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(WorkOrderRouting))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"];
                }
            }
            return entity;
        }

		private static WorkOrderRoutingEvents events = null;
        public static WorkOrderRoutingEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(WorkOrderRouting))
                    {
                        if (events == null)
                            events = new WorkOrderRoutingEvents();
                    }
                }
                return events;
            }
        }
        public class WorkOrderRoutingEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<WorkOrderRouting, EntityEventArgs> onNew;
            public event EventHandler<WorkOrderRouting, EntityEventArgs> OnNew
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
                EventHandler<WorkOrderRouting, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((WorkOrderRouting)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<WorkOrderRouting, EntityEventArgs> onDelete;
            public event EventHandler<WorkOrderRouting, EntityEventArgs> OnDelete
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
                EventHandler<WorkOrderRouting, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((WorkOrderRouting)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<WorkOrderRouting, EntityEventArgs> onSave;
            public event EventHandler<WorkOrderRouting, EntityEventArgs> OnSave
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
                EventHandler<WorkOrderRouting, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((WorkOrderRouting)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnOperationSequence

				private static bool onOperationSequenceIsRegistered = false;

				private static EventHandler<WorkOrderRouting, PropertyEventArgs> onOperationSequence;
				public static event EventHandler<WorkOrderRouting, PropertyEventArgs> OnOperationSequence
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onOperationSequenceIsRegistered)
							{
								Members.OperationSequence.Events.OnChange -= onOperationSequenceProxy;
								Members.OperationSequence.Events.OnChange += onOperationSequenceProxy;
								onOperationSequenceIsRegistered = true;
							}
							onOperationSequence += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onOperationSequence -= value;
							if (onOperationSequence == null && onOperationSequenceIsRegistered)
							{
								Members.OperationSequence.Events.OnChange -= onOperationSequenceProxy;
								onOperationSequenceIsRegistered = false;
							}
						}
					}
				}
            
				private static void onOperationSequenceProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<WorkOrderRouting, PropertyEventArgs> handler = onOperationSequence;
					if ((object)handler != null)
						handler.Invoke((WorkOrderRouting)sender, args);
				}

				#endregion

				#region OnScheduledStartDate

				private static bool onScheduledStartDateIsRegistered = false;

				private static EventHandler<WorkOrderRouting, PropertyEventArgs> onScheduledStartDate;
				public static event EventHandler<WorkOrderRouting, PropertyEventArgs> OnScheduledStartDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onScheduledStartDateIsRegistered)
							{
								Members.ScheduledStartDate.Events.OnChange -= onScheduledStartDateProxy;
								Members.ScheduledStartDate.Events.OnChange += onScheduledStartDateProxy;
								onScheduledStartDateIsRegistered = true;
							}
							onScheduledStartDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onScheduledStartDate -= value;
							if (onScheduledStartDate == null && onScheduledStartDateIsRegistered)
							{
								Members.ScheduledStartDate.Events.OnChange -= onScheduledStartDateProxy;
								onScheduledStartDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onScheduledStartDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<WorkOrderRouting, PropertyEventArgs> handler = onScheduledStartDate;
					if ((object)handler != null)
						handler.Invoke((WorkOrderRouting)sender, args);
				}

				#endregion

				#region OnScheduledEndDate

				private static bool onScheduledEndDateIsRegistered = false;

				private static EventHandler<WorkOrderRouting, PropertyEventArgs> onScheduledEndDate;
				public static event EventHandler<WorkOrderRouting, PropertyEventArgs> OnScheduledEndDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onScheduledEndDateIsRegistered)
							{
								Members.ScheduledEndDate.Events.OnChange -= onScheduledEndDateProxy;
								Members.ScheduledEndDate.Events.OnChange += onScheduledEndDateProxy;
								onScheduledEndDateIsRegistered = true;
							}
							onScheduledEndDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onScheduledEndDate -= value;
							if (onScheduledEndDate == null && onScheduledEndDateIsRegistered)
							{
								Members.ScheduledEndDate.Events.OnChange -= onScheduledEndDateProxy;
								onScheduledEndDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onScheduledEndDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<WorkOrderRouting, PropertyEventArgs> handler = onScheduledEndDate;
					if ((object)handler != null)
						handler.Invoke((WorkOrderRouting)sender, args);
				}

				#endregion

				#region OnActualStartDate

				private static bool onActualStartDateIsRegistered = false;

				private static EventHandler<WorkOrderRouting, PropertyEventArgs> onActualStartDate;
				public static event EventHandler<WorkOrderRouting, PropertyEventArgs> OnActualStartDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onActualStartDateIsRegistered)
							{
								Members.ActualStartDate.Events.OnChange -= onActualStartDateProxy;
								Members.ActualStartDate.Events.OnChange += onActualStartDateProxy;
								onActualStartDateIsRegistered = true;
							}
							onActualStartDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onActualStartDate -= value;
							if (onActualStartDate == null && onActualStartDateIsRegistered)
							{
								Members.ActualStartDate.Events.OnChange -= onActualStartDateProxy;
								onActualStartDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onActualStartDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<WorkOrderRouting, PropertyEventArgs> handler = onActualStartDate;
					if ((object)handler != null)
						handler.Invoke((WorkOrderRouting)sender, args);
				}

				#endregion

				#region OnActualEndDate

				private static bool onActualEndDateIsRegistered = false;

				private static EventHandler<WorkOrderRouting, PropertyEventArgs> onActualEndDate;
				public static event EventHandler<WorkOrderRouting, PropertyEventArgs> OnActualEndDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onActualEndDateIsRegistered)
							{
								Members.ActualEndDate.Events.OnChange -= onActualEndDateProxy;
								Members.ActualEndDate.Events.OnChange += onActualEndDateProxy;
								onActualEndDateIsRegistered = true;
							}
							onActualEndDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onActualEndDate -= value;
							if (onActualEndDate == null && onActualEndDateIsRegistered)
							{
								Members.ActualEndDate.Events.OnChange -= onActualEndDateProxy;
								onActualEndDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onActualEndDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<WorkOrderRouting, PropertyEventArgs> handler = onActualEndDate;
					if ((object)handler != null)
						handler.Invoke((WorkOrderRouting)sender, args);
				}

				#endregion

				#region OnActualResourceHrs

				private static bool onActualResourceHrsIsRegistered = false;

				private static EventHandler<WorkOrderRouting, PropertyEventArgs> onActualResourceHrs;
				public static event EventHandler<WorkOrderRouting, PropertyEventArgs> OnActualResourceHrs
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onActualResourceHrsIsRegistered)
							{
								Members.ActualResourceHrs.Events.OnChange -= onActualResourceHrsProxy;
								Members.ActualResourceHrs.Events.OnChange += onActualResourceHrsProxy;
								onActualResourceHrsIsRegistered = true;
							}
							onActualResourceHrs += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onActualResourceHrs -= value;
							if (onActualResourceHrs == null && onActualResourceHrsIsRegistered)
							{
								Members.ActualResourceHrs.Events.OnChange -= onActualResourceHrsProxy;
								onActualResourceHrsIsRegistered = false;
							}
						}
					}
				}
            
				private static void onActualResourceHrsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<WorkOrderRouting, PropertyEventArgs> handler = onActualResourceHrs;
					if ((object)handler != null)
						handler.Invoke((WorkOrderRouting)sender, args);
				}

				#endregion

				#region OnPlannedCost

				private static bool onPlannedCostIsRegistered = false;

				private static EventHandler<WorkOrderRouting, PropertyEventArgs> onPlannedCost;
				public static event EventHandler<WorkOrderRouting, PropertyEventArgs> OnPlannedCost
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPlannedCostIsRegistered)
							{
								Members.PlannedCost.Events.OnChange -= onPlannedCostProxy;
								Members.PlannedCost.Events.OnChange += onPlannedCostProxy;
								onPlannedCostIsRegistered = true;
							}
							onPlannedCost += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPlannedCost -= value;
							if (onPlannedCost == null && onPlannedCostIsRegistered)
							{
								Members.PlannedCost.Events.OnChange -= onPlannedCostProxy;
								onPlannedCostIsRegistered = false;
							}
						}
					}
				}
            
				private static void onPlannedCostProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<WorkOrderRouting, PropertyEventArgs> handler = onPlannedCost;
					if ((object)handler != null)
						handler.Invoke((WorkOrderRouting)sender, args);
				}

				#endregion

				#region OnActualCost

				private static bool onActualCostIsRegistered = false;

				private static EventHandler<WorkOrderRouting, PropertyEventArgs> onActualCost;
				public static event EventHandler<WorkOrderRouting, PropertyEventArgs> OnActualCost
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onActualCostIsRegistered)
							{
								Members.ActualCost.Events.OnChange -= onActualCostProxy;
								Members.ActualCost.Events.OnChange += onActualCostProxy;
								onActualCostIsRegistered = true;
							}
							onActualCost += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onActualCost -= value;
							if (onActualCost == null && onActualCostIsRegistered)
							{
								Members.ActualCost.Events.OnChange -= onActualCostProxy;
								onActualCostIsRegistered = false;
							}
						}
					}
				}
            
				private static void onActualCostProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<WorkOrderRouting, PropertyEventArgs> handler = onActualCost;
					if ((object)handler != null)
						handler.Invoke((WorkOrderRouting)sender, args);
				}

				#endregion

				#region OnWorkOrder

				private static bool onWorkOrderIsRegistered = false;

				private static EventHandler<WorkOrderRouting, PropertyEventArgs> onWorkOrder;
				public static event EventHandler<WorkOrderRouting, PropertyEventArgs> OnWorkOrder
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onWorkOrderIsRegistered)
							{
								Members.WorkOrder.Events.OnChange -= onWorkOrderProxy;
								Members.WorkOrder.Events.OnChange += onWorkOrderProxy;
								onWorkOrderIsRegistered = true;
							}
							onWorkOrder += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onWorkOrder -= value;
							if (onWorkOrder == null && onWorkOrderIsRegistered)
							{
								Members.WorkOrder.Events.OnChange -= onWorkOrderProxy;
								onWorkOrderIsRegistered = false;
							}
						}
					}
				}
            
				private static void onWorkOrderProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<WorkOrderRouting, PropertyEventArgs> handler = onWorkOrder;
					if ((object)handler != null)
						handler.Invoke((WorkOrderRouting)sender, args);
				}

				#endregion

				#region OnProduct

				private static bool onProductIsRegistered = false;

				private static EventHandler<WorkOrderRouting, PropertyEventArgs> onProduct;
				public static event EventHandler<WorkOrderRouting, PropertyEventArgs> OnProduct
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onProductIsRegistered)
							{
								Members.Product.Events.OnChange -= onProductProxy;
								Members.Product.Events.OnChange += onProductProxy;
								onProductIsRegistered = true;
							}
							onProduct += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onProduct -= value;
							if (onProduct == null && onProductIsRegistered)
							{
								Members.Product.Events.OnChange -= onProductProxy;
								onProductIsRegistered = false;
							}
						}
					}
				}
            
				private static void onProductProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<WorkOrderRouting, PropertyEventArgs> handler = onProduct;
					if ((object)handler != null)
						handler.Invoke((WorkOrderRouting)sender, args);
				}

				#endregion

				#region OnLocation

				private static bool onLocationIsRegistered = false;

				private static EventHandler<WorkOrderRouting, PropertyEventArgs> onLocation;
				public static event EventHandler<WorkOrderRouting, PropertyEventArgs> OnLocation
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onLocationIsRegistered)
							{
								Members.Location.Events.OnChange -= onLocationProxy;
								Members.Location.Events.OnChange += onLocationProxy;
								onLocationIsRegistered = true;
							}
							onLocation += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onLocation -= value;
							if (onLocation == null && onLocationIsRegistered)
							{
								Members.Location.Events.OnChange -= onLocationProxy;
								onLocationIsRegistered = false;
							}
						}
					}
				}
            
				private static void onLocationProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<WorkOrderRouting, PropertyEventArgs> handler = onLocation;
					if ((object)handler != null)
						handler.Invoke((WorkOrderRouting)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<WorkOrderRouting, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<WorkOrderRouting, PropertyEventArgs> OnModifiedDate
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
					EventHandler<WorkOrderRouting, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((WorkOrderRouting)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<WorkOrderRouting, PropertyEventArgs> onUid;
				public static event EventHandler<WorkOrderRouting, PropertyEventArgs> OnUid
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
					EventHandler<WorkOrderRouting, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((WorkOrderRouting)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IWorkOrderRoutingOriginalData

		public IWorkOrderRoutingOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IWorkOrderRouting

		string IWorkOrderRoutingOriginalData.OperationSequence { get { return OriginalData.OperationSequence; } }
		System.DateTime IWorkOrderRoutingOriginalData.ScheduledStartDate { get { return OriginalData.ScheduledStartDate; } }
		System.DateTime IWorkOrderRoutingOriginalData.ScheduledEndDate { get { return OriginalData.ScheduledEndDate; } }
		System.DateTime IWorkOrderRoutingOriginalData.ActualStartDate { get { return OriginalData.ActualStartDate; } }
		System.DateTime IWorkOrderRoutingOriginalData.ActualEndDate { get { return OriginalData.ActualEndDate; } }
		string IWorkOrderRoutingOriginalData.ActualResourceHrs { get { return OriginalData.ActualResourceHrs; } }
		string IWorkOrderRoutingOriginalData.PlannedCost { get { return OriginalData.PlannedCost; } }
		string IWorkOrderRoutingOriginalData.ActualCost { get { return OriginalData.ActualCost; } }
		WorkOrder IWorkOrderRoutingOriginalData.WorkOrder { get { return ((ILookupHelper<WorkOrder>)OriginalData.WorkOrder).GetOriginalItem(null); } }
		Product IWorkOrderRoutingOriginalData.Product { get { return ((ILookupHelper<Product>)OriginalData.Product).GetOriginalItem(null); } }
		Location IWorkOrderRoutingOriginalData.Location { get { return ((ILookupHelper<Location>)OriginalData.Location).GetOriginalItem(null); } }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime IWorkOrderRoutingOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		string IWorkOrderRoutingOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}