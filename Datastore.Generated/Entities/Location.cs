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
	public interface ILocationOriginalData : ISchemaBaseOriginalData
    {
		string Name { get; }
		string CostRate { get; }
		string Availability { get; }
    }

	public partial class Location : OGM<Location, Location.LocationData, System.String>, ISchemaBase, INeo4jBase, ILocationOriginalData
	{
        #region Initialize

        static Location()
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

        public static Dictionary<System.String, Location> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.LocationAlias, IWhereQuery> query)
        {
            q.LocationAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Location.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"Location => Name : {this.Name}, CostRate : {this.CostRate}, Availability : {this.Availability}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new LocationData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.Name == null)
				throw new PersistenceException(string.Format("Cannot save Location with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.CostRate == null)
				throw new PersistenceException(string.Format("Cannot save Location with key '{0}' because the CostRate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Availability == null)
				throw new PersistenceException(string.Format("Cannot save Location with key '{0}' because the Availability cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save Location with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class LocationData : Data<System.String>
		{
			public LocationData()
            {

            }

            public LocationData(LocationData data)
            {
				Name = data.Name;
				CostRate = data.CostRate;
				Availability = data.Availability;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Location";

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
				dictionary.Add("CostRate",  CostRate);
				dictionary.Add("Availability",  Availability);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("CostRate", out value))
					CostRate = (string)value;
				if (properties.TryGetValue("Availability", out value))
					Availability = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface ILocation

			public string Name { get; set; }
			public string CostRate { get; set; }
			public string Availability { get; set; }

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

		#region Members for interface ILocation

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
		public string CostRate { get { LazyGet(); return InnerData.CostRate; } set { if (LazySet(Members.CostRate, InnerData.CostRate, value)) InnerData.CostRate = value; } }
		public string Availability { get { LazyGet(); return InnerData.Availability; } set { if (LazySet(Members.Availability, InnerData.Availability, value)) InnerData.Availability = value; } }

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

        private static LocationMembers members = null;
        public static LocationMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(Location))
                    {
                        if (members == null)
                            members = new LocationMembers();
                    }
                }
                return members;
            }
        }
        public class LocationMembers
        {
            internal LocationMembers() { }

			#region Members for interface ILocation

            public Property Name { get; } = Datastore.AdventureWorks.Model.Entities["Location"].Properties["Name"];
            public Property CostRate { get; } = Datastore.AdventureWorks.Model.Entities["Location"].Properties["CostRate"];
            public Property Availability { get; } = Datastore.AdventureWorks.Model.Entities["Location"].Properties["Availability"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static LocationFullTextMembers fullTextMembers = null;
        public static LocationFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(Location))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new LocationFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class LocationFullTextMembers
        {
            internal LocationFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(Location))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["Location"];
                }
            }
            return entity;
        }

		private static LocationEvents events = null;
        public static LocationEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(Location))
                    {
                        if (events == null)
                            events = new LocationEvents();
                    }
                }
                return events;
            }
        }
        public class LocationEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Location, EntityEventArgs> onNew;
            public event EventHandler<Location, EntityEventArgs> OnNew
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
                EventHandler<Location, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((Location)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<Location, EntityEventArgs> onDelete;
            public event EventHandler<Location, EntityEventArgs> OnDelete
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
                EventHandler<Location, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((Location)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<Location, EntityEventArgs> onSave;
            public event EventHandler<Location, EntityEventArgs> OnSave
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
                EventHandler<Location, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((Location)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<Location, PropertyEventArgs> onName;
				public static event EventHandler<Location, PropertyEventArgs> OnName
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
					EventHandler<Location, PropertyEventArgs> handler = onName;
					if ((object)handler != null)
						handler.Invoke((Location)sender, args);
				}

				#endregion

				#region OnCostRate

				private static bool onCostRateIsRegistered = false;

				private static EventHandler<Location, PropertyEventArgs> onCostRate;
				public static event EventHandler<Location, PropertyEventArgs> OnCostRate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCostRateIsRegistered)
							{
								Members.CostRate.Events.OnChange -= onCostRateProxy;
								Members.CostRate.Events.OnChange += onCostRateProxy;
								onCostRateIsRegistered = true;
							}
							onCostRate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCostRate -= value;
							if (onCostRate == null && onCostRateIsRegistered)
							{
								Members.CostRate.Events.OnChange -= onCostRateProxy;
								onCostRateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onCostRateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Location, PropertyEventArgs> handler = onCostRate;
					if ((object)handler != null)
						handler.Invoke((Location)sender, args);
				}

				#endregion

				#region OnAvailability

				private static bool onAvailabilityIsRegistered = false;

				private static EventHandler<Location, PropertyEventArgs> onAvailability;
				public static event EventHandler<Location, PropertyEventArgs> OnAvailability
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onAvailabilityIsRegistered)
							{
								Members.Availability.Events.OnChange -= onAvailabilityProxy;
								Members.Availability.Events.OnChange += onAvailabilityProxy;
								onAvailabilityIsRegistered = true;
							}
							onAvailability += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onAvailability -= value;
							if (onAvailability == null && onAvailabilityIsRegistered)
							{
								Members.Availability.Events.OnChange -= onAvailabilityProxy;
								onAvailabilityIsRegistered = false;
							}
						}
					}
				}
            
				private static void onAvailabilityProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Location, PropertyEventArgs> handler = onAvailability;
					if ((object)handler != null)
						handler.Invoke((Location)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<Location, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<Location, PropertyEventArgs> OnModifiedDate
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
					EventHandler<Location, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((Location)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Location, PropertyEventArgs> onUid;
				public static event EventHandler<Location, PropertyEventArgs> OnUid
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
					EventHandler<Location, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((Location)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region ILocationOriginalData

		public ILocationOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ILocation

		string ILocationOriginalData.Name { get { return OriginalData.Name; } }
		string ILocationOriginalData.CostRate { get { return OriginalData.CostRate; } }
		string ILocationOriginalData.Availability { get { return OriginalData.Availability; } }

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