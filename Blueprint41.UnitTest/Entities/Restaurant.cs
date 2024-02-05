using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Datastore.Query;
using node = Datastore.Query.Node;

namespace Datastore.Manipulation
{
    public interface IRestaurantOriginalData : IBaseEntityOriginalData
    {
        string Name { get; }
        City City { get; }
        IEnumerable<Person> Persons { get; }
    }

    public partial class Restaurant : OGM<Restaurant, Restaurant.RestaurantData, System.String>, IBaseEntity, IRestaurantOriginalData
    {
        #region Initialize

        static Restaurant()
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

        public static Dictionary<System.String, Restaurant> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

        protected static void RegisterQuery(string name, Func<IMatchQuery, q.RestaurantAlias, IWhereQuery> query)
        {
            q.RestaurantAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Restaurant.Alias(out alias, "node"));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

            RegisterQuery(name, compiled);
        }

        public override string ToString()
        {
            return $"Restaurant => Name : {this.Name?.ToString() ?? "null"}, Uid : {this.Uid}, LastModifiedOn : {this.LastModifiedOn}";
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
                    OriginalData = new RestaurantData(InnerData);
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

        public class RestaurantData : Data<System.String>
        {
            public RestaurantData()
            {

            }

            public RestaurantData(RestaurantData data)
            {
                Name = data.Name;
                City = data.City;
                Persons = data.Persons;
                Uid = data.Uid;
                LastModifiedOn = data.LastModifiedOn;
            }


            #region Initialize Collections

            protected override void InitializeCollections()
            {
                NodeType = "Restaurant";

                City = new EntityCollection<City>(Wrapper, Members.City, item => { if (Members.City.Events.HasRegisteredChangeHandlers) { int loadHack = item.Restaurants.Count; } });
                Persons = new EntityCollection<Person>(Wrapper, Members.Persons, item => { if (Members.Persons.Events.HasRegisteredChangeHandlers) { int loadHack = item.Restaurants.Count; } });
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

            #region Members for interface IRestaurant

            public string Name { get; set; }
            public EntityCollection<City> City { get; private set; }
            public EntityCollection<Person> Persons { get; private set; }

            #endregion
            #region Members for interface IBaseEntity

            public string Uid { get; set; }
            public System.DateTime LastModifiedOn { get; set; }

            #endregion
        }

        #endregion

        #region Outer Data

        #region Members for interface IRestaurant

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
        private void ClearCity(DateTime? moment)
        {
            ((ILookupHelper<City>)InnerData.City).ClearLookup(moment);
        }
        public EntityCollection<Person> Persons { get { return InnerData.Persons; } }
        private void ClearPersons(DateTime? moment)
        {
            ((ILookupHelper<Person>)InnerData.Persons).ClearLookup(moment);
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

        #region City (Lookup)

        public RESTAURANT_LOCATED_AT CityRelation()
        {
            return RESTAURANT_LOCATED_AT.Load(_queryCityRelation.Value, ("key", Uid)).FirstOrDefault();
        }
        private readonly Lazy<ICompiled> _queryCityRelation = new Lazy<ICompiled>(delegate()
        {
            return Transaction.CompiledQuery
                .Match(node.Restaurant.Alias(out var inAlias).In.RESTAURANT_LOCATED_AT.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(inAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public RESTAURANT_LOCATED_AT GetCityIf(Func<RESTAURANT_LOCATED_AT.Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Restaurant.Alias(out var inAlias).In.RESTAURANT_LOCATED_AT.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new RESTAURANT_LOCATED_AT.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return RESTAURANT_LOCATED_AT.Load(query).FirstOrDefault();
        }
        public RESTAURANT_LOCATED_AT GetCityIf(Func<RESTAURANT_LOCATED_AT.Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Restaurant.Alias(out var inAlias).In.RESTAURANT_LOCATED_AT.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new RESTAURANT_LOCATED_AT.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return RESTAURANT_LOCATED_AT.Load(query).FirstOrDefault();
        }
        public RESTAURANT_LOCATED_AT GetCityIf(JsNotation<System.DateTime?> CreationDate = default)
        {
            return GetCityIf(delegate(RESTAURANT_LOCATED_AT.Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);

                return conditions.ToArray();
            });
        }
        public void SetCity(City city)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();

            ((ILookupHelper<City>)InnerData.City).SetItem(city, null, properties);

        }

        #endregion

        #region Persons (Collection)

        public List<PERSON_EATS_AT> PersonRelations()
        {
            return PERSON_EATS_AT.Load(_queryPersonRelations.Value, ("key", Uid));
        }
        private readonly Lazy<ICompiled> _queryPersonRelations = new Lazy<ICompiled>(delegate()
        {
            return Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.PERSON_EATS_AT.Alias(out var relAlias).Out.Restaurant.Alias(out var outAlias))
                .Where(outAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public List<PERSON_EATS_AT> PersonsWhere(Func<PERSON_EATS_AT.Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.PERSON_EATS_AT.Alias(out var relAlias).Out.Restaurant.Alias(out var outAlias))
                .Where(outAlias.Uid == Uid)
                .And(expression.Invoke(new PERSON_EATS_AT.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return PERSON_EATS_AT.Load(query);
        }
        public List<PERSON_EATS_AT> PersonsWhere(Func<PERSON_EATS_AT.Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.PERSON_EATS_AT.Alias(out var relAlias).Out.Restaurant.Alias(out var outAlias))
                .Where(outAlias.Uid == Uid)
                .And(expression.Invoke(new PERSON_EATS_AT.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return PERSON_EATS_AT.Load(query);
        }
        public List<PERSON_EATS_AT> PersonsWhere(JsNotation<System.DateTime?> CreationDate = default)
        {
            return PersonsWhere(delegate(PERSON_EATS_AT.Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);

                return conditions.ToArray();
            });
        }
        public void AddPerson(Person person)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            ((ILookupHelper<Person>)InnerData.Persons).AddItem(person, null, properties);
        }
        public void RemovePerson(Person person)
        {
            Persons.Remove(person);
        }

        #endregion

        private static readonly Parameter key = Parameter.New<string>("key");
        private static readonly Parameter moment = Parameter.New<DateTime>("moment");

        #endregion

        #region Reflection

        private static RestaurantMembers members = null;
        public static RestaurantMembers Members
        {
            get
            {
                if (members is null)
                {
                    lock (typeof(Restaurant))
                    {
                        if (members is null)
                            members = new RestaurantMembers();
                    }
                }
                return members;
            }
        }
        public class RestaurantMembers
        {
            internal RestaurantMembers() { }

            #region Members for interface IRestaurant

            public Property Name { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"].Properties["Name"];
            public Property City { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"].Properties["City"];
            public Property Persons { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"].Properties["Persons"];
            #endregion

            #region Members for interface IBaseEntity

            public Property Uid { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"];
            public Property LastModifiedOn { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"];
            #endregion

        }

        private static RestaurantFullTextMembers fullTextMembers = null;
        public static RestaurantFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers is null)
                {
                    lock (typeof(Restaurant))
                    {
                        if (fullTextMembers is null)
                            fullTextMembers = new RestaurantFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class RestaurantFullTextMembers
        {
            internal RestaurantFullTextMembers() { }

        }

        sealed public override Entity GetEntity()
        {
            if (entity is null)
            {
                lock (typeof(Restaurant))
                {
                    if (entity is null)
                        entity = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"];
                }
            }
            return entity;
        }

        private static RestaurantEvents events = null;
        public static RestaurantEvents Events
        {
            get
            {
                if (events is null)
                {
                    lock (typeof(Restaurant))
                    {
                        if (events is null)
                            events = new RestaurantEvents();
                    }
                }
                return events;
            }
        }
        public class RestaurantEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Restaurant, EntityEventArgs> onNew;
            public event EventHandler<Restaurant, EntityEventArgs> OnNew
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
                EventHandler<Restaurant, EntityEventArgs> handler = onNew;
                if (handler is not null)
                    handler.Invoke((Restaurant)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<Restaurant, EntityEventArgs> onDelete;
            public event EventHandler<Restaurant, EntityEventArgs> OnDelete
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
                EventHandler<Restaurant, EntityEventArgs> handler = onDelete;
                if (handler is not null)
                    handler.Invoke((Restaurant)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<Restaurant, EntityEventArgs> onSave;
            public event EventHandler<Restaurant, EntityEventArgs> OnSave
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
                EventHandler<Restaurant, EntityEventArgs> handler = onSave;
                if (handler is not null)
                    handler.Invoke((Restaurant)sender, args);
            }

            #endregion

            #region OnAfterSave

            private bool onAfterSaveIsRegistered = false;

            private EventHandler<Restaurant, EntityEventArgs> onAfterSave;
            public event EventHandler<Restaurant, EntityEventArgs> OnAfterSave
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
                EventHandler<Restaurant, EntityEventArgs> handler = onAfterSave;
                if (handler is not null)
                    handler.Invoke((Restaurant)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

                #region OnName

                private static bool onNameIsRegistered = false;

                private static EventHandler<Restaurant, PropertyEventArgs> onName;
                public static event EventHandler<Restaurant, PropertyEventArgs> OnName
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
                    EventHandler<Restaurant, PropertyEventArgs> handler = onName;
                    if (handler is not null)
                        handler.Invoke((Restaurant)sender, args);
                }

                #endregion

                #region OnCity

                private static bool onCityIsRegistered = false;

                private static EventHandler<Restaurant, PropertyEventArgs> onCity;
                public static event EventHandler<Restaurant, PropertyEventArgs> OnCity
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
                            if (onCity is null && onCityIsRegistered)
                            {
                                Members.City.Events.OnChange -= onCityProxy;
                                onCityIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onCityProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Restaurant, PropertyEventArgs> handler = onCity;
                    if (handler is not null)
                        handler.Invoke((Restaurant)sender, args);
                }

                #endregion

                #region OnPersons

                private static bool onPersonsIsRegistered = false;

                private static EventHandler<Restaurant, PropertyEventArgs> onPersons;
                public static event EventHandler<Restaurant, PropertyEventArgs> OnPersons
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onPersonsIsRegistered)
                            {
                                Members.Persons.Events.OnChange -= onPersonsProxy;
                                Members.Persons.Events.OnChange += onPersonsProxy;
                                onPersonsIsRegistered = true;
                            }
                            onPersons += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onPersons -= value;
                            if (onPersons is null && onPersonsIsRegistered)
                            {
                                Members.Persons.Events.OnChange -= onPersonsProxy;
                                onPersonsIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onPersonsProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Restaurant, PropertyEventArgs> handler = onPersons;
                    if (handler is not null)
                        handler.Invoke((Restaurant)sender, args);
                }

                #endregion

                #region OnUid

                private static bool onUidIsRegistered = false;

                private static EventHandler<Restaurant, PropertyEventArgs> onUid;
                public static event EventHandler<Restaurant, PropertyEventArgs> OnUid
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
                    EventHandler<Restaurant, PropertyEventArgs> handler = onUid;
                    if (handler is not null)
                        handler.Invoke((Restaurant)sender, args);
                }

                #endregion

                #region OnLastModifiedOn

                private static bool onLastModifiedOnIsRegistered = false;

                private static EventHandler<Restaurant, PropertyEventArgs> onLastModifiedOn;
                public static event EventHandler<Restaurant, PropertyEventArgs> OnLastModifiedOn
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
                    EventHandler<Restaurant, PropertyEventArgs> handler = onLastModifiedOn;
                    if (handler is not null)
                        handler.Invoke((Restaurant)sender, args);
                }

                #endregion

            }

            #endregion
        }

        #endregion

        #region IRestaurantOriginalData

        public IRestaurantOriginalData OriginalVersion { get { return this; } }

        #region Members for interface IRestaurant

        string IRestaurantOriginalData.Name { get { return OriginalData.Name; } }
        City IRestaurantOriginalData.City { get { return ((ILookupHelper<City>)OriginalData.City).GetOriginalItem(null); } }
        IEnumerable<Person> IRestaurantOriginalData.Persons { get { return OriginalData.Persons.OriginalData; } }

        #endregion
        #region Members for interface IBaseEntity

        IBaseEntityOriginalData IBaseEntity.OriginalVersion { get { return this; } }

        string IBaseEntityOriginalData.Uid { get { return OriginalData.Uid; } }
        System.DateTime IBaseEntityOriginalData.LastModifiedOn { get { return OriginalData.LastModifiedOn; } }

        #endregion
        #endregion
    }
}