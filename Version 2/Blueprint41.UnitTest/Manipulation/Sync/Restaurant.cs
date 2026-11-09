#nullable disable
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable CS8981 // Names should not be lower type only
#pragma warning disable IDE0130 // Namespace does not match folder structure
#pragma warning disable VSSpell001 // Spell Check

using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Events;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Datastore.Query;
using node = Datastore.Query.Node;

namespace Datastore.Manipulation.Sync
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


        [Obsolete]
        static Restaurant()
        {
            Datastore.Manipulation.Sync.Register.Types();
            Datastore.Manipulation.Async.Register.Types();
        }


        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadByKeys
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

            #region LoadByUid

            RegisterQuery(nameof(LoadByUid), (query, alias) => query.
                Where(alias.Uid == Parameter.New<System.String>(Param0)));

            #endregion

            AdditionalGeneratedStoredQueries();
        }
        public static Restaurant LoadByUid(System.String uid)
        {
            return FromQuery(nameof(LoadByUid), new Parameter(Param0, uid)).FirstOrDefault();
        }
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, Restaurant> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

        protected static void RegisterQuery(string name, Func<IMatchQuery, q.RestaurantAlias, IWhereQuery> query)
        {
            q.RestaurantAlias alias;

            IMatchQuery matchQuery = Cypher.Match(q.Node.Restaurant.Alias(out alias, "node"));
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

                City = new EntityCollection<City>(Wrapper, Members.City, EntityFlavor.Blocking, null);
                Persons = new EntityCollection<Person>(Wrapper, Members.Persons, EntityFlavor.Blocking, null);
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
        protected override void SetRowVersion(DateTime? value) { LastModifiedOn = value ?? DateTime.MinValue; }

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
            return Cypher
                .Match(node.Restaurant.Alias(out var inAlias).In.RESTAURANT_LOCATED_AT.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(inAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public RESTAURANT_LOCATED_AT GetCityIf(Func<RESTAURANT_LOCATED_AT.Alias, QueryCondition> expression)
        {
            var query = Cypher
                .Match(node.Restaurant.Alias(out var inAlias).In.RESTAURANT_LOCATED_AT.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new RESTAURANT_LOCATED_AT.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return RESTAURANT_LOCATED_AT.Load(query).FirstOrDefault();
        }
        public RESTAURANT_LOCATED_AT GetCityIf(Func<RESTAURANT_LOCATED_AT.Alias, QueryCondition[]> expression)
        {
            var query = Cypher
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
            return Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_EATS_AT.Alias(out var relAlias).Out.Restaurant.Alias(out var outAlias))
                .Where(outAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public List<PERSON_EATS_AT> PersonsWhere(Func<PERSON_EATS_AT.Alias, QueryCondition> expression)
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_EATS_AT.Alias(out var relAlias).Out.Restaurant.Alias(out var outAlias))
                .Where(outAlias.Uid == Uid)
                .And(expression.Invoke(new PERSON_EATS_AT.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return PERSON_EATS_AT.Load(query);
        }
        public List<PERSON_EATS_AT> PersonsWhere(Func<PERSON_EATS_AT.Alias, QueryCondition[]> expression)
        {
            var query = Cypher
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

            public EntityProperty Name { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"].Properties["Name"];
            public EntityProperty City { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"].Properties["City"];
            public EntityProperty Persons { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"].Properties["Persons"];
            #endregion

            #region Members for interface IBaseEntity

            public EntityProperty Uid { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"];
            public EntityProperty LastModifiedOn { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"];
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

        sealed protected override Entity GetEntity()
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
        public override EntityFlavor Flavor => EntityFlavor.Blocking;

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
