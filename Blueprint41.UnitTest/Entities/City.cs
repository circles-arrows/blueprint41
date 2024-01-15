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
    public interface ICityOriginalData : IBaseEntityOriginalData
    {
        string Name { get; }
        string State { get; }
        string Country { get; }
        IEnumerable<Restaurant> Restaurants { get; }
    }

    public partial class City : OGM<City, City.CityData, System.String>, IBaseEntity, ICityOriginalData
    {
        #region Initialize

        static City()
        {
            Register.Types();
        }


        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadByKeys
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

            #region LoadByName

            RegisterQuery(nameof(LoadByName), (query, alias) => query.
                Where(alias.Name == Parameter.New<string>(Param0)));

            #endregion

            AdditionalGeneratedStoredQueries();
        }
        public static City LoadByName(string name)
        {
            return FromQuery(nameof(LoadByName), new Parameter(Param0, name)).FirstOrDefault();
        }
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, City> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

        protected static void RegisterQuery(string name, Func<IMatchQuery, q.CityAlias, IWhereQuery> query)
        {
            q.CityAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.City.Alias(out alias, "node"));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

            RegisterQuery(name, compiled);
        }

        public override string ToString()
        {
            return $"City => Name : {this.Name}, State : {this.State?.ToString() ?? "null"}, Country : {this.Country?.ToString() ?? "null"}, Uid : {this.Uid}, LastModifiedOn : {this.LastModifiedOn}";
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
                    OriginalData = new CityData(InnerData);
            }
        }


        #endregion

        #region Validations

        protected override void ValidateSave()
        {
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

            if (InnerData.Name is null)
                throw new PersistenceException(string.Format("Cannot save City with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
        }

        protected override void ValidateDelete()
        {
        }

        #endregion

        #region Inner Data

        public class CityData : Data<System.String>
        {
            public CityData()
            {

            }

            public CityData(CityData data)
            {
                Name = data.Name;
                State = data.State;
                Country = data.Country;
                Restaurants = data.Restaurants;
                Uid = data.Uid;
                LastModifiedOn = data.LastModifiedOn;
            }


            #region Initialize Collections

            protected override void InitializeCollections()
            {
                NodeType = "City";

                Restaurants = new EntityCollection<Restaurant>(Wrapper, Members.Restaurants, item => { if (Members.Restaurants.Events.HasRegisteredChangeHandlers) { object loadHack = item.City; } });
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
                dictionary.Add("State",  State);
                dictionary.Add("Country",  Country);
                dictionary.Add("Uid",  Uid);
                dictionary.Add("LastModifiedOn",  Conversion<System.DateTime, long>.Convert(LastModifiedOn));
                return dictionary;
            }

            sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
            {
                object value;
                if (properties.TryGetValue("Name", out value))
                    Name = (string)value;
                if (properties.TryGetValue("State", out value))
                    State = (string)value;
                if (properties.TryGetValue("Country", out value))
                    Country = (string)value;
                if (properties.TryGetValue("Uid", out value))
                    Uid = (string)value;
                if (properties.TryGetValue("LastModifiedOn", out value))
                    LastModifiedOn = Conversion<long, System.DateTime>.Convert((long)value);
            }

            #endregion

            #region Members for interface ICity

            public string Name { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public EntityCollection<Restaurant> Restaurants { get; private set; }

            #endregion
            #region Members for interface IBaseEntity

            public string Uid { get; set; }
            public System.DateTime LastModifiedOn { get; set; }

            #endregion
        }

        #endregion

        #region Outer Data

        #region Members for interface ICity

        public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
        public string State { get { LazyGet(); return InnerData.State; } set { if (LazySet(Members.State, InnerData.State, value)) InnerData.State = value; } }
        public string Country { get { LazyGet(); return InnerData.Country; } set { if (LazySet(Members.Country, InnerData.Country, value)) InnerData.Country = value; } }
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

        #region Relationship Properties

        public List<RESTAURANT_LOCATED_AT> RestaurantRelations()
        {
            throw new NotImplementedException();
        }
        public List<RESTAURANT_LOCATED_AT> RestaurantsWhere(Func<RESTAURANT_LOCATED_AT_CRUD_ALIAS, QueryCondition> alias)
        {
            throw new NotImplementedException();
        }
        public List<RESTAURANT_LOCATED_AT> RestaurantsWhere(Func<RESTAURANT_LOCATED_AT_CRUD_ALIAS, QueryCondition[]> alias)
        {
            throw new NotImplementedException();
        }
        public List<RESTAURANT_LOCATED_AT> RestaurantsWhere(JsNotation<System.DateTime> CreationDate = default)
        {
            throw new NotImplementedException();
        }
        public void AddRestaurant(Restaurant restaurant)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            ((ILookupHelper<Restaurant>)InnerData.Restaurants).AddItem(restaurant, null, properties);
        }
        public void RemoveRestaurant(Restaurant restaurant)
        {
            Restaurants.Remove(restaurant);
        }








































        #endregion

        #region Reflection

        private static CityMembers members = null;
        public static CityMembers Members
        {
            get
            {
                if (members is null)
                {
                    lock (typeof(City))
                    {
                        if (members is null)
                            members = new CityMembers();
                    }
                }
                return members;
            }
        }
        public class CityMembers
        {
            internal CityMembers() { }

            #region Members for interface ICity

            public Property Name { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"].Properties["Name"];
            public Property State { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"].Properties["State"];
            public Property Country { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"].Properties["Country"];
            public Property Restaurants { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"].Properties["Restaurants"];
            #endregion

            #region Members for interface IBaseEntity

            public Property Uid { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"];
            public Property LastModifiedOn { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"];
            #endregion

        }

        private static CityFullTextMembers fullTextMembers = null;
        public static CityFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers is null)
                {
                    lock (typeof(City))
                    {
                        if (fullTextMembers is null)
                            fullTextMembers = new CityFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class CityFullTextMembers
        {
            internal CityFullTextMembers() { }

        }

        sealed public override Entity GetEntity()
        {
            if (entity is null)
            {
                lock (typeof(City))
                {
                    if (entity is null)
                        entity = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"];
                }
            }
            return entity;
        }

        private static CityEvents events = null;
        public static CityEvents Events
        {
            get
            {
                if (events is null)
                {
                    lock (typeof(City))
                    {
                        if (events is null)
                            events = new CityEvents();
                    }
                }
                return events;
            }
        }
        public class CityEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<City, EntityEventArgs> onNew;
            public event EventHandler<City, EntityEventArgs> OnNew
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
                EventHandler<City, EntityEventArgs> handler = onNew;
                if (handler is not null)
                    handler.Invoke((City)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<City, EntityEventArgs> onDelete;
            public event EventHandler<City, EntityEventArgs> OnDelete
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
                EventHandler<City, EntityEventArgs> handler = onDelete;
                if (handler is not null)
                    handler.Invoke((City)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<City, EntityEventArgs> onSave;
            public event EventHandler<City, EntityEventArgs> OnSave
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
                EventHandler<City, EntityEventArgs> handler = onSave;
                if (handler is not null)
                    handler.Invoke((City)sender, args);
            }

            #endregion

            #region OnAfterSave

            private bool onAfterSaveIsRegistered = false;

            private EventHandler<City, EntityEventArgs> onAfterSave;
            public event EventHandler<City, EntityEventArgs> OnAfterSave
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
                EventHandler<City, EntityEventArgs> handler = onAfterSave;
                if (handler is not null)
                    handler.Invoke((City)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

                #region OnName

                private static bool onNameIsRegistered = false;

                private static EventHandler<City, PropertyEventArgs> onName;
                public static event EventHandler<City, PropertyEventArgs> OnName
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
                    EventHandler<City, PropertyEventArgs> handler = onName;
                    if (handler is not null)
                        handler.Invoke((City)sender, args);
                }

                #endregion

                #region OnState

                private static bool onStateIsRegistered = false;

                private static EventHandler<City, PropertyEventArgs> onState;
                public static event EventHandler<City, PropertyEventArgs> OnState
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onStateIsRegistered)
                            {
                                Members.State.Events.OnChange -= onStateProxy;
                                Members.State.Events.OnChange += onStateProxy;
                                onStateIsRegistered = true;
                            }
                            onState += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onState -= value;
                            if (onState is null && onStateIsRegistered)
                            {
                                Members.State.Events.OnChange -= onStateProxy;
                                onStateIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onStateProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<City, PropertyEventArgs> handler = onState;
                    if (handler is not null)
                        handler.Invoke((City)sender, args);
                }

                #endregion

                #region OnCountry

                private static bool onCountryIsRegistered = false;

                private static EventHandler<City, PropertyEventArgs> onCountry;
                public static event EventHandler<City, PropertyEventArgs> OnCountry
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onCountryIsRegistered)
                            {
                                Members.Country.Events.OnChange -= onCountryProxy;
                                Members.Country.Events.OnChange += onCountryProxy;
                                onCountryIsRegistered = true;
                            }
                            onCountry += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onCountry -= value;
                            if (onCountry is null && onCountryIsRegistered)
                            {
                                Members.Country.Events.OnChange -= onCountryProxy;
                                onCountryIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onCountryProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<City, PropertyEventArgs> handler = onCountry;
                    if (handler is not null)
                        handler.Invoke((City)sender, args);
                }

                #endregion

                #region OnRestaurants

                private static bool onRestaurantsIsRegistered = false;

                private static EventHandler<City, PropertyEventArgs> onRestaurants;
                public static event EventHandler<City, PropertyEventArgs> OnRestaurants
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
                            if (onRestaurants is null && onRestaurantsIsRegistered)
                            {
                                Members.Restaurants.Events.OnChange -= onRestaurantsProxy;
                                onRestaurantsIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onRestaurantsProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<City, PropertyEventArgs> handler = onRestaurants;
                    if (handler is not null)
                        handler.Invoke((City)sender, args);
                }

                #endregion

                #region OnUid

                private static bool onUidIsRegistered = false;

                private static EventHandler<City, PropertyEventArgs> onUid;
                public static event EventHandler<City, PropertyEventArgs> OnUid
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
                    EventHandler<City, PropertyEventArgs> handler = onUid;
                    if (handler is not null)
                        handler.Invoke((City)sender, args);
                }

                #endregion

                #region OnLastModifiedOn

                private static bool onLastModifiedOnIsRegistered = false;

                private static EventHandler<City, PropertyEventArgs> onLastModifiedOn;
                public static event EventHandler<City, PropertyEventArgs> OnLastModifiedOn
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
                            if (onLastModifiedOn is null && onLastModifiedOnIsRegistered)
                            {
                                Members.LastModifiedOn.Events.OnChange -= onLastModifiedOnProxy;
                                onLastModifiedOnIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onLastModifiedOnProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<City, PropertyEventArgs> handler = onLastModifiedOn;
                    if (handler is not null)
                        handler.Invoke((City)sender, args);
                }

                #endregion

            }

            #endregion
        }

        #endregion

        #region ICityOriginalData

        public ICityOriginalData OriginalVersion { get { return this; } }

        #region Members for interface ICity

        string ICityOriginalData.Name { get { return OriginalData.Name; } }
        string ICityOriginalData.State { get { return OriginalData.State; } }
        string ICityOriginalData.Country { get { return OriginalData.Country; } }
        IEnumerable<Restaurant> ICityOriginalData.Restaurants { get { return OriginalData.Restaurants.OriginalData; } }

        #endregion
        #region Members for interface IBaseEntity

        IBaseEntityOriginalData IBaseEntity.OriginalVersion { get { return this; } }

        string IBaseEntityOriginalData.Uid { get { return OriginalData.Uid; } }
        System.DateTime IBaseEntityOriginalData.LastModifiedOn { get { return OriginalData.LastModifiedOn; } }

        #endregion
        #endregion
    }
}