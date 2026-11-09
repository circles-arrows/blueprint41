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
    public interface IPersonOriginalData : IBaseEntityOriginalData
    {
        string Name { get; }
        IEnumerable<Restaurant> Restaurants { get; }
        IEnumerable<Movie> DirectedMovies { get; }
        IEnumerable<Movie> ActedInMovies { get; }
        IEnumerable<StreamingService> StreamingServiceSubscriptions { get; }
        IEnumerable<Movie> WatchedMovies { get; }
        City City { get; }
    }

    public partial class Person : OGM<Person, Person.PersonData, System.String>, IBaseEntity, IPersonOriginalData
    {
        #region Initialize


        [Obsolete]
        static Person()
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
        public static Person LoadByUid(System.String uid)
        {
            return FromQuery(nameof(LoadByUid), new Parameter(Param0, uid)).FirstOrDefault();
        }
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, Person> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

        protected static void RegisterQuery(string name, Func<IMatchQuery, q.PersonAlias, IWhereQuery> query)
        {
            q.PersonAlias alias;

            IMatchQuery matchQuery = Cypher.Match(q.Node.Person.Alias(out alias, "node"));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

            RegisterQuery(name, compiled);
        }

        public override string ToString()
        {
            return $"Person => Name : {this.Name}, Uid : {this.Uid}, LastModifiedOn : {this.LastModifiedOn}";
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
                    OriginalData = new PersonData(InnerData);
            }
        }


        #endregion

        #region Validations

        protected override void ValidateSave()
        {
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

            if (InnerData.Name is null)
                throw new PersistenceException(string.Format("Cannot save Person with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
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
                Restaurants = data.Restaurants;
                DirectedMovies = data.DirectedMovies;
                ActedInMovies = data.ActedInMovies;
                StreamingServiceSubscriptions = data.StreamingServiceSubscriptions;
                WatchedMovies = data.WatchedMovies;
                City = data.City;
                Uid = data.Uid;
                LastModifiedOn = data.LastModifiedOn;
            }


            #region Initialize Collections

            protected override void InitializeCollections()
            {
                NodeType = "Person";

                Restaurants = new EntityCollection<Restaurant>(Wrapper, Members.Restaurants, EntityFlavor.Blocking, null);
                DirectedMovies = new EntityCollection<Movie>(Wrapper, Members.DirectedMovies, EntityFlavor.Blocking, null);
                ActedInMovies = new EntityCollection<Movie>(Wrapper, Members.ActedInMovies, EntityFlavor.Blocking, null);
                StreamingServiceSubscriptions = new EntityTimeCollection<StreamingService>(Wrapper, Members.StreamingServiceSubscriptions, EntityFlavor.Blocking, null);
                WatchedMovies = new EntityCollection<Movie>(Wrapper, Members.WatchedMovies, EntityFlavor.Blocking);
                City = new EntityTimeCollection<City>(Wrapper, Members.City, EntityFlavor.Blocking);
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

            #region Members for interface IPerson

            public string Name { get; set; }
            public EntityCollection<Restaurant> Restaurants { get; private set; }
            public EntityCollection<Movie> DirectedMovies { get; private set; }
            public EntityCollection<Movie> ActedInMovies { get; private set; }
            public EntityTimeCollection<StreamingService> StreamingServiceSubscriptions { get; private set; }
            public EntityCollection<Movie> WatchedMovies { get; private set; }
            public EntityTimeCollection<City> City { get; private set; }

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
        public EntityCollection<Restaurant> Restaurants { get { return InnerData.Restaurants; } }
        private void ClearRestaurants(DateTime? moment)
        {
            ((ILookupHelper<Restaurant>)InnerData.Restaurants).ClearLookup(moment);
        }
        public EntityCollection<Movie> DirectedMovies { get { return InnerData.DirectedMovies; } }
        private void ClearDirectedMovies(DateTime? moment)
        {
            ((ILookupHelper<Movie>)InnerData.DirectedMovies).ClearLookup(moment);
        }
        public EntityCollection<Movie> ActedInMovies { get { return InnerData.ActedInMovies; } }
        private void ClearActedInMovies(DateTime? moment)
        {
            ((ILookupHelper<Movie>)InnerData.ActedInMovies).ClearLookup(moment);
        }
        public EntityTimeCollection<StreamingService> StreamingServiceSubscriptions { get { return InnerData.StreamingServiceSubscriptions; } }
        private void ClearStreamingServiceSubscriptions(DateTime? moment)
        {
            ((ILookupHelper<StreamingService>)InnerData.StreamingServiceSubscriptions).ClearLookup(moment);
        }
        public EntityCollection<Movie> WatchedMovies { get { return InnerData.WatchedMovies; } }
        public City City { get { return GetCity(Transaction.Current?.TransactionDate ?? DateTime.UtcNow); } set { SetCity(value, Transaction.Current?.TransactionDate ?? DateTime.UtcNow); } }
        public City GetCity(DateTime moment)
        {
            return ((ILookupHelper<City>)InnerData.City).GetItem(moment);
        }
        public IEnumerable<CollectionItem<City>> GetCities(DateTime? from, DateTime? till)
        {
            return ((ILookupHelper<City>)InnerData.City).GetItems(from, till);
        }
        //public void SetCity(City value, DateTime? moment)
        //{
        //    if (LazySet(Members.City, ((ILookupHelper<City>)InnerData.City).GetItems(moment, null), value, moment))
        //        ((ILookupHelper<City>)InnerData.City).SetItem(value, moment);
        //}

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

        public List<PERSON_EATS_AT> RestaurantRelations()
        {
            return PERSON_EATS_AT.Load(_queryRestaurantRelations.Value, ("key", Uid));
        }
        private readonly Lazy<ICompiled> _queryRestaurantRelations = new Lazy<ICompiled>(delegate()
        {
            return Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_EATS_AT.Alias(out var relAlias).Out.Restaurant.Alias(out var outAlias))
                .Where(inAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public List<PERSON_EATS_AT> RestaurantsWhere(Func<PERSON_EATS_AT.Alias, QueryCondition> expression)
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_EATS_AT.Alias(out var relAlias).Out.Restaurant.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new PERSON_EATS_AT.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return PERSON_EATS_AT.Load(query);
        }
        public List<PERSON_EATS_AT> RestaurantsWhere(Func<PERSON_EATS_AT.Alias, QueryCondition[]> expression)
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_EATS_AT.Alias(out var relAlias).Out.Restaurant.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new PERSON_EATS_AT.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return PERSON_EATS_AT.Load(query);
        }
        public List<PERSON_EATS_AT> RestaurantsWhere(JsNotation<System.DateTime?> CreationDate = default)
        {
            return RestaurantsWhere(delegate(PERSON_EATS_AT.Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);

                return conditions.ToArray();
            });
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

        #region DirectedMovies (Collection)

        public List<PERSON_DIRECTED> DirectedMovieRelations()
        {
            return PERSON_DIRECTED.Load(_queryDirectedMovieRelations.Value, ("key", Uid));
        }
        private readonly Lazy<ICompiled> _queryDirectedMovieRelations = new Lazy<ICompiled>(delegate()
        {
            return Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_DIRECTED.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public List<PERSON_DIRECTED> DirectedMoviesWhere(Func<PERSON_DIRECTED.Alias, QueryCondition> expression)
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_DIRECTED.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new PERSON_DIRECTED.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return PERSON_DIRECTED.Load(query);
        }
        public List<PERSON_DIRECTED> DirectedMoviesWhere(Func<PERSON_DIRECTED.Alias, QueryCondition[]> expression)
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_DIRECTED.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new PERSON_DIRECTED.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return PERSON_DIRECTED.Load(query);
        }
        public List<PERSON_DIRECTED> DirectedMoviesWhere(JsNotation<System.DateTime?> CreationDate = default)
        {
            return DirectedMoviesWhere(delegate(PERSON_DIRECTED.Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);

                return conditions.ToArray();
            });
        }
        public void AddDirectedMovie(Movie movie)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            ((ILookupHelper<Movie>)InnerData.DirectedMovies).AddItem(movie, null, properties);
        }
        public void RemoveDirectedMovie(Movie movie)
        {
            DirectedMovies.Remove(movie);
        }

        #endregion

        #region ActedInMovies (Collection)

        public List<ACTED_IN> ActedInMovieRelations()
        {
            return ACTED_IN.Load(_queryActedInMovieRelations.Value, ("key", Uid));
        }
        private readonly Lazy<ICompiled> _queryActedInMovieRelations = new Lazy<ICompiled>(delegate()
        {
            return Cypher
                .Match(node.Person.Alias(out var inAlias).In.ACTED_IN.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public List<ACTED_IN> ActedInMoviesWhere(Func<ACTED_IN.Alias, QueryCondition> expression)
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.ACTED_IN.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new ACTED_IN.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return ACTED_IN.Load(query);
        }
        public List<ACTED_IN> ActedInMoviesWhere(Func<ACTED_IN.Alias, QueryCondition[]> expression)
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.ACTED_IN.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new ACTED_IN.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return ACTED_IN.Load(query);
        }
        public List<ACTED_IN> ActedInMoviesWhere(JsNotation<System.DateTime?> CreationDate = default)
        {
            return ActedInMoviesWhere(delegate(ACTED_IN.Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);

                return conditions.ToArray();
            });
        }
        public void AddActedInMovie(Movie movie)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            ((ILookupHelper<Movie>)InnerData.ActedInMovies).AddItem(movie, null, properties);
        }
        public void RemoveActedInMovie(Movie movie)
        {
            ActedInMovies.Remove(movie);
        }

        #endregion

        #region StreamingServiceSubscriptions (Time Dependent Collection)

        public List<SUBSCRIBED_TO_STREAMING_SERVICE> StreamingServiceSubscriptionRelations()
        {
            return SUBSCRIBED_TO_STREAMING_SERVICE.Load(_queryStreamingServiceSubscriptionRelations.Value, ("key", Uid));
        }
        private readonly Lazy<ICompiled> _queryStreamingServiceSubscriptionRelations = new Lazy<ICompiled>(delegate()
        {
            return Cypher
                .Match(node.Person.Alias(out var inAlias).In.SUBSCRIBED_TO_STREAMING_SERVICE.Alias(out var relAlias).Out.StreamingService.Alias(out var outAlias))
                .Where(inAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public List<SUBSCRIBED_TO_STREAMING_SERVICE> StreamingServiceSubscriptionsWhere(Func<SUBSCRIBED_TO_STREAMING_SERVICE.Alias, QueryCondition> expression)
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.SUBSCRIBED_TO_STREAMING_SERVICE.Alias(out var relAlias).Out.StreamingService.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new SUBSCRIBED_TO_STREAMING_SERVICE.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return SUBSCRIBED_TO_STREAMING_SERVICE.Load(query);
        }
        public List<SUBSCRIBED_TO_STREAMING_SERVICE> StreamingServiceSubscriptionsWhere(Func<SUBSCRIBED_TO_STREAMING_SERVICE.Alias, QueryCondition[]> expression)
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.SUBSCRIBED_TO_STREAMING_SERVICE.Alias(out var relAlias).Out.StreamingService.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new SUBSCRIBED_TO_STREAMING_SERVICE.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return SUBSCRIBED_TO_STREAMING_SERVICE.Load(query);
        }
        public List<SUBSCRIBED_TO_STREAMING_SERVICE> StreamingServiceSubscriptionsWhere(JsNotation<DateTime?> Moment = default, JsNotation<System.DateTime?> CreationDate = default, JsNotation<decimal> MonthlyFee = default)
        {
            return StreamingServiceSubscriptionsWhere(delegate(SUBSCRIBED_TO_STREAMING_SERVICE.Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (MonthlyFee.HasValue) conditions.Add(alias.MonthlyFee == MonthlyFee.Value);
                if (Moment.HasValue) conditions.AddRange(alias.Moment(Moment.Value));

                return conditions.ToArray();
            });
        }
        public void AddStreamingServiceSubscription(StreamingService streamingService, DateTime? moment, JsNotation<decimal> MonthlyFee = default)
        {
            if (moment is null)
                moment = Transaction.Current?.TransactionDate ??  DateTime.UtcNow;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            if (MonthlyFee.HasValue) properties.Add("MonthlyFee", MonthlyFee.Value);
            ((ILookupHelper<StreamingService>)InnerData.StreamingServiceSubscriptions).AddItem(streamingService, moment, properties);
        }
        public void RemoveStreamingServiceSubscription(StreamingService streamingService, DateTime? moment)
        {
            StreamingServiceSubscriptions.Remove(streamingService, moment);
        }

        #endregion

        #region WatchedMovies (Collection)

        public List<WATCHED_MOVIE> WatchedMovieRelations()
        {
            return WATCHED_MOVIE.Load(_queryWatchedMovieRelations.Value, ("key", Uid));
        }
        private readonly Lazy<ICompiled> _queryWatchedMovieRelations = new Lazy<ICompiled>(delegate()
        {
            return Cypher
                .Match(node.Person.Alias(out var inAlias).In.WATCHED_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public List<WATCHED_MOVIE> WatchedMoviesWhere(Func<WATCHED_MOVIE.Alias, QueryCondition> expression)
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.WATCHED_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new WATCHED_MOVIE.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return WATCHED_MOVIE.Load(query);
        }
        public List<WATCHED_MOVIE> WatchedMoviesWhere(Func<WATCHED_MOVIE.Alias, QueryCondition[]> expression)
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.WATCHED_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new WATCHED_MOVIE.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return WATCHED_MOVIE.Load(query);
        }
        public List<WATCHED_MOVIE> WatchedMoviesWhere(JsNotation<System.DateTime?> CreationDate = default, JsNotation<int> MinutesWatched = default)
        {
            return WatchedMoviesWhere(delegate(WATCHED_MOVIE.Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (MinutesWatched.HasValue) conditions.Add(alias.MinutesWatched == MinutesWatched.Value);

                return conditions.ToArray();
            });
        }
        public void AddWatchedMovie(Movie movie, JsNotation<int> MinutesWatched = default)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            if (MinutesWatched.HasValue) properties.Add("MinutesWatched", MinutesWatched.Value);
            ((ILookupHelper<Movie>)InnerData.WatchedMovies).AddItem(movie, null, properties);
        }
        public void RemoveWatchedMovie(Movie movie)
        {
            WatchedMovies.Remove(movie);
        }

        #endregion

        #region City (Time Dependent Lookup)

        public PERSON_LIVES_IN CityRelation(DateTime? moment = null)
        {
            if (moment is null)
                moment = Transaction.Current?.TransactionDate ??  DateTime.UtcNow;

            return PERSON_LIVES_IN.Load(_queryCityRelation.Value, ("key", Uid), ("moment", moment)).FirstOrDefault();
        }
        private readonly Lazy<ICompiled> _queryCityRelation = new Lazy<ICompiled>(delegate()
        {
            return Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_LIVES_IN.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(inAlias.Uid == key)
                .And(relAlias.Moment(moment))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public List<PERSON_LIVES_IN> CityRelations()
        {
            return PERSON_LIVES_IN.Load(_queryCityRelations.Value, ("key", Uid));
        }
        private readonly Lazy<ICompiled> _queryCityRelations = new Lazy<ICompiled>(delegate()
        {
            return Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_LIVES_IN.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(inAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public PERSON_LIVES_IN GetCityIf(DateTime? moment, Func<PERSON_LIVES_IN.Alias, QueryCondition> expression)
        {
            if (moment is null)
                moment = Transaction.Current?.TransactionDate ??  DateTime.UtcNow;

            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_LIVES_IN.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new PERSON_LIVES_IN.Alias(relAlias, inAlias, outAlias)))
                .And(relAlias.Moment(moment))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return PERSON_LIVES_IN.Load(query).FirstOrDefault();
        }
        public PERSON_LIVES_IN GetCityIf(DateTime? moment, Func<PERSON_LIVES_IN.Alias, QueryCondition[]> expression)
        {
            if (moment is null)
                moment = Transaction.Current?.TransactionDate ??  DateTime.UtcNow;

            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_LIVES_IN.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new PERSON_LIVES_IN.Alias(relAlias, inAlias, outAlias)))
                .And(relAlias.Moment(moment))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return PERSON_LIVES_IN.Load(query).FirstOrDefault();
        }
        public PERSON_LIVES_IN GetCityIf(DateTime? moment, JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> AddressLine3 = default, JsNotation<System.DateTime?> CreationDate = default)
        {
            return GetCityIf(moment, delegate(PERSON_LIVES_IN.Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (AddressLine1.HasValue) conditions.Add(alias.AddressLine1 == AddressLine1.Value);
                if (AddressLine2.HasValue) conditions.Add(alias.AddressLine2 == AddressLine2.Value);
                if (AddressLine3.HasValue) conditions.Add(alias.AddressLine3 == AddressLine3.Value);

                return conditions.ToArray();
            });
        }
        public List<PERSON_LIVES_IN> CityWhere(Func<PERSON_LIVES_IN.Alias, QueryCondition> expression)
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_LIVES_IN.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new PERSON_LIVES_IN.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return PERSON_LIVES_IN.Load(query);
        }
        public List<PERSON_LIVES_IN> CityWhere(Func<PERSON_LIVES_IN.Alias, QueryCondition[]> expression)
        {
            var query = Cypher
                .Match(node.Person.Alias(out var inAlias).In.PERSON_LIVES_IN.Alias(out var relAlias).Out.City.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new PERSON_LIVES_IN.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return PERSON_LIVES_IN.Load(query);
        }
        public List<PERSON_LIVES_IN> CityWhere(JsNotation<DateTime?> Moment = default, JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> AddressLine3 = default, JsNotation<System.DateTime?> CreationDate = default)
        {
            return CityWhere(delegate(PERSON_LIVES_IN.Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (AddressLine1.HasValue) conditions.Add(alias.AddressLine1 == AddressLine1.Value);
                if (AddressLine2.HasValue) conditions.Add(alias.AddressLine2 == AddressLine2.Value);
                if (AddressLine3.HasValue) conditions.Add(alias.AddressLine3 == AddressLine3.Value);
                if (Moment.HasValue) conditions.AddRange(alias.Moment(Moment.Value));

                return conditions.ToArray();
            });
        }
        public void SetCity(City city, DateTime? moment, JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> AddressLine3 = default)
        {
            if (moment is null)
                moment = Transaction.Current?.TransactionDate ??  DateTime.UtcNow;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            if (AddressLine1.HasValue) properties.Add("AddressLine1", AddressLine1.Value);
            if (AddressLine2.HasValue) properties.Add("AddressLine2", AddressLine2.Value);
            if (AddressLine3.HasValue) properties.Add("AddressLine3", AddressLine3.Value);
        
            ((ILookupHelper<City>)InnerData.City).SetItem(city, moment, properties);
        }

        #endregion

        private static readonly Parameter key = Parameter.New<string>("key");
        private static readonly Parameter moment = Parameter.New<DateTime>("moment");

        #endregion

        #region Reflection

        private static PersonMembers members = null;
        public static PersonMembers Members
        {
            get
            {
                if (members is null)
                {
                    lock (typeof(Person))
                    {
                        if (members is null)
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

            public EntityProperty Name { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["Name"];
            public EntityProperty Restaurants { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["Restaurants"];
            public EntityProperty DirectedMovies { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["DirectedMovies"];
            public EntityProperty ActedInMovies { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["ActedInMovies"];
            public EntityProperty StreamingServiceSubscriptions { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["StreamingServiceSubscriptions"];
            public EntityProperty WatchedMovies { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["WatchedMovies"];
            public EntityProperty City { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["City"];
            #endregion

            #region Members for interface IBaseEntity

            public EntityProperty Uid { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"];
            public EntityProperty LastModifiedOn { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"];
            #endregion

        }

        private static PersonFullTextMembers fullTextMembers = null;
        public static PersonFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers is null)
                {
                    lock (typeof(Person))
                    {
                        if (fullTextMembers is null)
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

        sealed protected override Entity GetEntity()
        {
            if (entity is null)
            {
                lock (typeof(Person))
                {
                    if (entity is null)
                        entity = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"];
                }
            }
            return entity;
        }
        public override EntityFlavor Flavor => EntityFlavor.Blocking;

        private static PersonEvents events = null;
        public static PersonEvents Events
        {
            get
            {
                if (events is null)
                {
                    lock (typeof(Person))
                    {
                        if (events is null)
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
                EventHandler<Person, EntityEventArgs> handler = onNew;
                if (handler is not null)
                    handler.Invoke((Person)sender, args);
            }

            #endregion

        }

        #endregion

        #region IPersonOriginalData

        public IPersonOriginalData OriginalVersion { get { return this; } }

        #region Members for interface IPerson

        string IPersonOriginalData.Name { get { return OriginalData.Name; } }
        IEnumerable<Restaurant> IPersonOriginalData.Restaurants { get { return OriginalData.Restaurants.OriginalData; } }
        IEnumerable<Movie> IPersonOriginalData.DirectedMovies { get { return OriginalData.DirectedMovies.OriginalData; } }
        IEnumerable<Movie> IPersonOriginalData.ActedInMovies { get { return OriginalData.ActedInMovies.OriginalData; } }
        IEnumerable<StreamingService> IPersonOriginalData.StreamingServiceSubscriptions { get { return OriginalData.StreamingServiceSubscriptions.OriginalData; } }
        IEnumerable<Movie> IPersonOriginalData.WatchedMovies { get { return OriginalData.WatchedMovies.OriginalData; } }
        City IPersonOriginalData.City { get { return ((ILookupHelper<City>)OriginalData.City).GetOriginalItem(DateTime.UtcNow); } }

        #endregion
        #region Members for interface IBaseEntity

        IBaseEntityOriginalData IBaseEntity.OriginalVersion { get { return this; } }

        string IBaseEntityOriginalData.Uid { get { return OriginalData.Uid; } }
        System.DateTime IBaseEntityOriginalData.LastModifiedOn { get { return OriginalData.LastModifiedOn; } }

        #endregion
        #endregion
    }
}
