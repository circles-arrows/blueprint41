#nullable disable
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable CS8981 // Names should not be lower type only
#pragma warning disable IDE0130 // Namespace does not match folder structure
#pragma warning disable VSSpell001 // Spell Check

// ASYNC

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Events;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Datastore.Query;
using node = Datastore.Query.Node;

namespace Datastore.Manipulation.Async
{
    public interface ICityOriginalData : IBaseEntityOriginalData
    {
        string Name { get; }
        string State { get; }
        string Country { get; }
        Task<IEnumerable<Restaurant>> RestaurantsAsync();
    }

    public partial class City : OGM<City, City.CityData, System.String>, IBaseEntity, ICityOriginalData
    {
        #region Initialize


        [Obsolete]
        static City()
        {
            Datastore.Manipulation.Sync.Register.Types();
            Datastore.Manipulation.Async.Register.Types();
        }


        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadByKeys
            
            RegisterQuery(nameof(LoadByKeysAsync), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

            #region LoadByName

            RegisterQuery(nameof(LoadByNameAsync), (query, alias) => query.
                Where(alias.Name == Parameter.New<System.String>(Param0)));

            #endregion

            #region LoadByUid

            RegisterQuery(nameof(LoadByUidAsync), (query, alias) => query.
                Where(alias.Uid == Parameter.New<System.String>(Param0)));

            #endregion

            AdditionalGeneratedStoredQueries();
        }
        public async static Task<City> LoadByNameAsync(System.String name)
        {
            return (await FromQueryAsync(nameof(LoadByNameAsync), new Parameter(Param0, name)).ConfigureAwait(false)).FirstOrDefault();
        }
        public async static Task<City> LoadByUidAsync(System.String uid)
        {
            return (await FromQueryAsync(nameof(LoadByUidAsync), new Parameter(Param0, uid)).ConfigureAwait(false)).FirstOrDefault();
        }
        partial void AdditionalGeneratedStoredQueries();

        public static async Task<Dictionary<System.String, City>> LoadByKeysAsync(IEnumerable<System.String> uids)
        {
            return (await FromQueryAsync(nameof(LoadByKeysAsync), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ConfigureAwait(false)).ToDictionary(item=> item.Uid, item => item);
        }

        protected static void RegisterQuery(string name, Func<IMatchQuery, q.CityAlias, IWhereQuery> query)
        {
            q.CityAlias alias;

            IMatchQuery matchQuery = Cypher.Match(q.Node.City.Alias(out alias, "node"));
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

                Restaurants = new EntityCollectionAsync<Restaurant>(Wrapper, Members.Restaurants, EntityFlavor.Async);
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
            public EntityCollectionAsync<Restaurant> Restaurants { get; private set; }

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
        public Task<EntityCollectionAsync<Restaurant>> RestaurantsAsync() { return InnerData.Restaurants.LoadAsync(); }
        private void ClearRestaurants(DateTime? moment)
        {
            ((ILookupHelperAsync<Restaurant>)InnerData.Restaurants).ClearLookup(moment);
        }
        private Task ClearRestaurantsAsync(DateTime? moment)
        {
            return ((ILookupHelperAsync<Restaurant>)InnerData.Restaurants).ClearLookupAsync(moment);
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

        #region Restaurants (Collection)

        public Task<List<RESTAURANT_LOCATED_AT>> RestaurantRelationsAsync()
        {
            return RESTAURANT_LOCATED_AT.LoadAsync(_queryRestaurantRelations.Value, ("key", Uid));
        }
        private readonly Lazy<ICompiled> _queryRestaurantRelations = new Lazy<ICompiled>(delegate()
        {
            return Cypher
                .Match(node.Restaurant.Alias(out var inAlias).In.RESTAURANT_LOCATED_AT.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(outAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public Task<List<RESTAURANT_LOCATED_AT>> RestaurantsWhereAsync(Func<RESTAURANT_LOCATED_AT.Alias, QueryCondition> expression)
        {
            var query = Cypher
                .Match(node.Restaurant.Alias(out var inAlias).In.RESTAURANT_LOCATED_AT.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(outAlias.Uid == Uid)
                .And(expression.Invoke(new RESTAURANT_LOCATED_AT.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return RESTAURANT_LOCATED_AT.LoadAsync(query);
        }
        public Task<List<RESTAURANT_LOCATED_AT>> RestaurantsWhereAsync(Func<RESTAURANT_LOCATED_AT.Alias, QueryCondition[]> expression)
        {
            var query = Cypher
                .Match(node.Restaurant.Alias(out var inAlias).In.RESTAURANT_LOCATED_AT.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(outAlias.Uid == Uid)
                .And(expression.Invoke(new RESTAURANT_LOCATED_AT.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return RESTAURANT_LOCATED_AT.LoadAsync(query);
        }
        public Task<List<RESTAURANT_LOCATED_AT>> RestaurantsWhereAsync(JsNotation<System.DateTime?> CreationDate = default)
        {
            return RestaurantsWhereAsync(delegate(RESTAURANT_LOCATED_AT.Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);

                return conditions.ToArray();
            });
        }
        public async Task AddRestaurantAsync(Restaurant restaurant)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            await InnerData.Restaurants.LoadAsync().ConfigureAwait(false);
            ((ILookupHelperAsync<Restaurant>)InnerData.Restaurants).AddItem(restaurant, null, properties);
        }
        public async Task RemoveRestaurantAsync(Restaurant restaurant)
        {
            (await RestaurantsAsync()).Remove(restaurant);
        }

        #endregion

        private static readonly Parameter key = Parameter.New<string>("key");
        private static readonly Parameter moment = Parameter.New<DateTime>("moment");

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

            public EntityProperty Name { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"].Properties["Name"];
            public EntityProperty State { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"].Properties["State"];
            public EntityProperty Country { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"].Properties["Country"];
            public EntityProperty Restaurants { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"].Properties["Restaurants"];
            #endregion

            #region Members for interface IBaseEntity

            public EntityProperty Uid { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"];
            public EntityProperty LastModifiedOn { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"];
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

        sealed protected override Entity GetEntity()
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
        public override EntityFlavor Flavor => EntityFlavor.Async;

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

        }

        #endregion

        #region ICityOriginalData

        public ICityOriginalData OriginalVersion { get { return this; } }

        #region Members for interface ICity

        string ICityOriginalData.Name { get { return OriginalData.Name; } }
        string ICityOriginalData.State { get { return OriginalData.State; } }
        string ICityOriginalData.Country { get { return OriginalData.Country; } }
        Task<IEnumerable<Restaurant>> ICityOriginalData.RestaurantsAsync() { return OriginalData.Restaurants.OriginalDataAsync(); }

        #endregion
        #region Members for interface IBaseEntity

        IBaseEntityOriginalData IBaseEntity.OriginalVersion { get { return this; } }

        string IBaseEntityOriginalData.Uid { get { return OriginalData.Uid; } }
        System.DateTime IBaseEntityOriginalData.LastModifiedOn { get { return OriginalData.LastModifiedOn; } }

        #endregion
        #endregion
    }
}
