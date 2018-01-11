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
	public interface ICountryRegionOriginalData
    {
		#region Outer Data

		#region Members for interface ICountryRegion

		int Code { get; }
		string Name { get; }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ModifiedDate { get; }

		#endregion
		#region Members for interface INeo4jBase

		string Uid { get; }

		#endregion
		#endregion
    }

	public partial class CountryRegion : OGM<CountryRegion, CountryRegion.CountryRegionData, System.String>, ISchemaBase, INeo4jBase, ICountryRegionOriginalData
	{
        #region Initialize

        static CountryRegion()
        {
            Register.Types();
        }

        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadFromNaturalKey
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

			#region LoadByCode

			RegisterQuery(nameof(LoadByCode), (query, alias) => query.
                Where(alias.Code == Parameter.New<string>(Param0)));

			#endregion

			AdditionalGeneratedStoredQueries();
        }
		public static CountryRegion LoadByCode(string code)
		{
			return FromQuery(nameof(LoadByCode), new Parameter(Param0, code)).FirstOrDefault();
		}
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, CountryRegion> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.CountryRegionAlias, IWhereQuery> query)
        {
            q.CountryRegionAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.CountryRegion.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"CountryRegion => Code : {this.Code}, Name : {this.Name}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new CountryRegionData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.Code == null)
				throw new PersistenceException(string.Format("Cannot save CountryRegion with key '{0}' because the Code cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Name == null)
				throw new PersistenceException(string.Format("Cannot save CountryRegion with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save CountryRegion with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class CountryRegionData : Data<System.String>
		{
			public CountryRegionData()
            {

            }

            public CountryRegionData(CountryRegionData data)
            {
				Code = data.Code;
				Name = data.Name;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "CountryRegion";

			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("Code",  Conversion<int, long>.Convert(Code));
				dictionary.Add("Name",  Name);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Code", out value))
					Code = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface ICountryRegion

			public int Code { get; set; }
			public string Name { get; set; }

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

		#region Members for interface ICountryRegion

		public int Code { get { LazyGet(); return InnerData.Code; } set { if (LazySet(Members.Code, InnerData.Code, value)) InnerData.Code = value; } }
		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }

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

        private static CountryRegionMembers members = null;
        public static CountryRegionMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(CountryRegion))
                    {
                        if (members == null)
                            members = new CountryRegionMembers();
                    }
                }
                return members;
            }
        }
        public class CountryRegionMembers
        {
            internal CountryRegionMembers() { }

			#region Members for interface ICountryRegion

            public Property Code { get; } = Datastore.AdventureWorks.Model.Entities["CountryRegion"].Properties["Code"];
            public Property Name { get; } = Datastore.AdventureWorks.Model.Entities["CountryRegion"].Properties["Name"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static CountryRegionFullTextMembers fullTextMembers = null;
        public static CountryRegionFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(CountryRegion))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new CountryRegionFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class CountryRegionFullTextMembers
        {
            internal CountryRegionFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(CountryRegion))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["CountryRegion"];
                }
            }
            return entity;
        }

		private static CountryRegionEvents events = null;
        public static CountryRegionEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(CountryRegion))
                    {
                        if (events == null)
                            events = new CountryRegionEvents();
                    }
                }
                return events;
            }
        }
        public class CountryRegionEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<CountryRegion, EntityEventArgs> onNew;
            public event EventHandler<CountryRegion, EntityEventArgs> OnNew
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
                EventHandler<CountryRegion, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((CountryRegion)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<CountryRegion, EntityEventArgs> onDelete;
            public event EventHandler<CountryRegion, EntityEventArgs> OnDelete
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
                EventHandler<CountryRegion, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((CountryRegion)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<CountryRegion, EntityEventArgs> onSave;
            public event EventHandler<CountryRegion, EntityEventArgs> OnSave
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
                EventHandler<CountryRegion, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((CountryRegion)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnCode

				private static bool onCodeIsRegistered = false;

				private static EventHandler<CountryRegion, PropertyEventArgs> onCode;
				public static event EventHandler<CountryRegion, PropertyEventArgs> OnCode
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCodeIsRegistered)
							{
								Members.Code.Events.OnChange -= onCodeProxy;
								Members.Code.Events.OnChange += onCodeProxy;
								onCodeIsRegistered = true;
							}
							onCode += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCode -= value;
							if (onCode == null && onCodeIsRegistered)
							{
								Members.Code.Events.OnChange -= onCodeProxy;
								onCodeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onCodeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<CountryRegion, PropertyEventArgs> handler = onCode;
					if ((object)handler != null)
						handler.Invoke((CountryRegion)sender, args);
				}

				#endregion

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<CountryRegion, PropertyEventArgs> onName;
				public static event EventHandler<CountryRegion, PropertyEventArgs> OnName
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
					EventHandler<CountryRegion, PropertyEventArgs> handler = onName;
					if ((object)handler != null)
						handler.Invoke((CountryRegion)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<CountryRegion, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<CountryRegion, PropertyEventArgs> OnModifiedDate
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
					EventHandler<CountryRegion, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((CountryRegion)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<CountryRegion, PropertyEventArgs> onUid;
				public static event EventHandler<CountryRegion, PropertyEventArgs> OnUid
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
					EventHandler<CountryRegion, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((CountryRegion)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region ICountryRegionOriginalData

		public ICountryRegionOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ICountryRegion

		int ICountryRegionOriginalData.Code { get { return OriginalData.Code; } }
		string ICountryRegionOriginalData.Name { get { return OriginalData.Name; } }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ICountryRegionOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		string ICountryRegionOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}