using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Datastore.Query;

namespace Datastore.Manipulation
{
	public interface IPersonOriginalData : IBaseEntityOriginalData
    {
		string Name { get; }
		City City { get; }
		IEnumerable<Restaurant> Restaurants { get; }
    }

	public partial class Person : OGM<Person, Person.PersonData, System.String>, IBaseEntity, IPersonOriginalData
	{
        #region Initialize

        static Person()
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

        public static Dictionary<System.String, Person> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.PersonAlias, IWhereQuery> query)
        {
            q.PersonAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Person.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"Person => Name : {this.Name?.ToString() ?? "null"}, Uid : {this.Uid}, LastModifiedOn : {this.LastModifiedOn}";
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
                    OriginalData = new PersonData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class PersonData : Data<System.String>
		{
			public PersonData()
            {

            }

            public PersonData(PersonData data)
            {
				Name = data.Name;
				City = data.City;
				Restaurants = data.Restaurants;
				Uid = data.Uid;
				LastModifiedOn = data.LastModifiedOn;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Person";

				City = new EntityCollection<City>(Wrapper, Members.City);
				Restaurants = new EntityCollection<Restaurant>(Wrapper, Members.Restaurants, item => { if (Members.Restaurants.Events.HasRegisteredChangeHandlers) { int loadHack = item.Persons.Count; } });
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
				dictionary.Add("Uid",  Uid);
				dictionary.Add("LastModifiedOn",  Conversion<System.DateTime, long>.Convert(LastModifiedOn));
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
				if (properties.TryGetValue("LastModifiedOn", out value))
					LastModifiedOn = Conversion<long, System.DateTime>.Convert((long)value);
			}

			#endregion

			#region Members for interface IPerson

			public string Name { get; set; }
			public EntityCollection<City> City { get; private set; }
			public EntityCollection<Restaurant> Restaurants { get; private set; }

			#endregion
			#region Members for interface IBaseEntity

			public string Uid { get; set; }
			public System.DateTime LastModifiedOn { get; set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IPerson

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
		public City City
		{
			get { return ((ILookupHelper<City>)InnerData.City).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.City, ((ILookupHelper<City>)InnerData.City).GetItem(null), value))
					((ILookupHelper<City>)InnerData.City).SetItem(value, null); 
			}
		}
		public EntityCollection<Restaurant> Restaurants { get { return InnerData.Restaurants; } }
		private void ClearRestaurants(DateTime? moment)
		{
			((ILookupHelper<Restaurant>)InnerData.Restaurants).ClearLookup(moment);
		}

		#endregion
		#region Members for interface IBaseEntity

		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }
		public System.DateTime LastModifiedOn { get { LazyGet(); return InnerData.LastModifiedOn; } set { if (LazySet(Members.LastModifiedOn, InnerData.LastModifiedOn, value)) InnerData.LastModifiedOn = value; } }
		protected override DateTime GetRowVersion() { return LastModifiedOn; }
		public override void SetRowVersion(DateTime? value) { LastModifiedOn = value ?? DateTime.MinValue; }

		#endregion

		#region Virtual Node Type
		
		public string NodeType  { get { return InnerData.NodeType; } }
		
		#endregion

		#endregion

		#region Reflection

        private static PersonMembers members = null;
        public static PersonMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(Person))
                    {
                        if (members == null)
                            members = new PersonMembers();
                    }
                }
                return members;
            }
        }
        public class PersonMembers
        {
            internal PersonMembers() { }

			#region Members for interface IPerson

            public Property Name { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["Name"];
            public Property City { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["City"];
            public Property Restaurants { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["Restaurants"];
			#endregion

			#region Members for interface IBaseEntity

            public Property Uid { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"];
            public Property LastModifiedOn { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"];
			#endregion

        }

        private static PersonFullTextMembers fullTextMembers = null;
        public static PersonFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(Person))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new PersonFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class PersonFullTextMembers
        {
            internal PersonFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(Person))
                {
                    if (entity == null)
                        entity = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"];
                }
            }
            return entity;
        }

		private static PersonEvents events = null;
        public static PersonEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(Person))
                    {
                        if (events == null)
                            events = new PersonEvents();
                    }
                }
                return events;
            }
        }
        public class PersonEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Person, EntityEventArgs> onNew;
            public event EventHandler<Person, EntityEventArgs> OnNew
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
                EventHandler<Person, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((Person)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<Person, EntityEventArgs> onDelete;
            public event EventHandler<Person, EntityEventArgs> OnDelete
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
                EventHandler<Person, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((Person)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<Person, EntityEventArgs> onSave;
            public event EventHandler<Person, EntityEventArgs> OnSave
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
                EventHandler<Person, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((Person)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onName;
				public static event EventHandler<Person, PropertyEventArgs> OnName
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
					EventHandler<Person, PropertyEventArgs> handler = onName;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnCity

				private static bool onCityIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onCity;
				public static event EventHandler<Person, PropertyEventArgs> OnCity
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCityIsRegistered)
							{
								Members.City.Events.OnChange -= onCityProxy;
								Members.City.Events.OnChange += onCityProxy;
								onCityIsRegistered = true;
							}
							onCity += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCity -= value;
							if (onCity == null && onCityIsRegistered)
							{
								Members.City.Events.OnChange -= onCityProxy;
								onCityIsRegistered = false;
							}
						}
					}
				}
            
				private static void onCityProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onCity;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnRestaurants

				private static bool onRestaurantsIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onRestaurants;
				public static event EventHandler<Person, PropertyEventArgs> OnRestaurants
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onRestaurantsIsRegistered)
							{
								Members.Restaurants.Events.OnChange -= onRestaurantsProxy;
								Members.Restaurants.Events.OnChange += onRestaurantsProxy;
								onRestaurantsIsRegistered = true;
							}
							onRestaurants += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onRestaurants -= value;
							if (onRestaurants == null && onRestaurantsIsRegistered)
							{
								Members.Restaurants.Events.OnChange -= onRestaurantsProxy;
								onRestaurantsIsRegistered = false;
							}
						}
					}
				}
            
				private static void onRestaurantsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onRestaurants;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onUid;
				public static event EventHandler<Person, PropertyEventArgs> OnUid
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
					EventHandler<Person, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnLastModifiedOn

				private static bool onLastModifiedOnIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onLastModifiedOn;
				public static event EventHandler<Person, PropertyEventArgs> OnLastModifiedOn
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
							if (onLastModifiedOn == null && onLastModifiedOnIsRegistered)
							{
								Members.LastModifiedOn.Events.OnChange -= onLastModifiedOnProxy;
								onLastModifiedOnIsRegistered = false;
							}
						}
					}
				}
            
				private static void onLastModifiedOnProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onLastModifiedOn;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IPersonOriginalData

		public IPersonOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IPerson

		string IPersonOriginalData.Name { get { return OriginalData.Name; } }
		City IPersonOriginalData.City { get { return ((ILookupHelper<City>)OriginalData.City).GetOriginalItem(null); } }
		IEnumerable<Restaurant> IPersonOriginalData.Restaurants { get { return OriginalData.Restaurants.OriginalData; } }

		#endregion
		#region Members for interface IBaseEntity

		IBaseEntityOriginalData IBaseEntity.OriginalVersion { get { return this; } }

		string IBaseEntityOriginalData.Uid { get { return OriginalData.Uid; } }
		System.DateTime IBaseEntityOriginalData.LastModifiedOn { get { return OriginalData.LastModifiedOn; } }

		#endregion
		#endregion
	}
}