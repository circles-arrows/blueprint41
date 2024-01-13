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

        static Person()
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

        public static Dictionary<System.String, Person> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

        protected static void RegisterQuery(string name, Func<IMatchQuery, q.PersonAlias, IWhereQuery> query)
        {
            q.PersonAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Person.Alias(out alias, "node"));
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

                Restaurants = new EntityCollection<Restaurant>(Wrapper, Members.Restaurants, item => { if (Members.Restaurants.Events.HasRegisteredChangeHandlers) { int loadHack = item.Persons.Count; } });
                DirectedMovies = new EntityCollection<Movie>(Wrapper, Members.DirectedMovies, item => { if (Members.DirectedMovies.Events.HasRegisteredChangeHandlers) { object loadHack = item.Director; } });
                ActedInMovies = new EntityCollection<Movie>(Wrapper, Members.ActedInMovies, item => { if (Members.ActedInMovies.Events.HasRegisteredChangeHandlers) { int loadHack = item.Actors.Count; } });
                StreamingServiceSubscriptions = new EntityTimeCollection<StreamingService>(Wrapper, Members.StreamingServiceSubscriptions, item => { if (Members.StreamingServiceSubscriptions.Events.HasRegisteredChangeHandlers) { int loadHack = item.Subscribers.CountAll; } });
                WatchedMovies = new EntityCollection<Movie>(Wrapper, Members.WatchedMovies);
                City = new EntityTimeCollection<City>(Wrapper, Members.City);
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
        public void SetCity(City value, DateTime? moment)
        {
            if (LazySet(Members.City, ((ILookupHelper<City>)InnerData.City).GetItems(moment, null), value, moment))
                ((ILookupHelper<City>)InnerData.City).SetItem(value, moment);
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

        public List<PERSON_EATS_AT> RestaurantRelations()
        {
            throw new NotImplementedException();
        }
        public List<PERSON_EATS_AT> RestaurantsWhere(Func<PERSON_EATS_AT_ALIAS, QueryCondition> alias)
        {
            throw new NotImplementedException();
        }
        public List<PERSON_EATS_AT> RestaurantsWhere(Func<PERSON_EATS_AT_ALIAS, QueryCondition[]> alias)
        {
            throw new NotImplementedException();
        }
        public List<PERSON_EATS_AT> RestaurantsWhere(JsNotation<System.DateTime> CreationDate = default)
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
        public List<PERSON_DIRECTED> DirectedMovieRelations()
        {
            throw new NotImplementedException();
        }
        public List<PERSON_DIRECTED> DirectedMoviesWhere(Func<PERSON_DIRECTED_ALIAS, QueryCondition> alias)
        {
            throw new NotImplementedException();
        }
        public List<PERSON_DIRECTED> DirectedMoviesWhere(Func<PERSON_DIRECTED_ALIAS, QueryCondition[]> alias)
        {
            throw new NotImplementedException();
        }
        public List<PERSON_DIRECTED> DirectedMoviesWhere(JsNotation<System.DateTime> CreationDate = default)
        {
            throw new NotImplementedException();
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
        public List<ACTED_IN> ActedInMovieRelations()
        {
            throw new NotImplementedException();
        }
        public List<ACTED_IN> ActedInMoviesWhere(Func<ACTED_IN_ALIAS, QueryCondition> alias)
        {
            throw new NotImplementedException();
        }
        public List<ACTED_IN> ActedInMoviesWhere(Func<ACTED_IN_ALIAS, QueryCondition[]> alias)
        {
            throw new NotImplementedException();
        }
        public List<ACTED_IN> ActedInMoviesWhere(JsNotation<System.DateTime> CreationDate = default)
        {
            throw new NotImplementedException();
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
        public List<SUBSCRIBED_TO_STREAMING_SERVICE> StreamingServiceSubscriptionRelations()
        {
            throw new NotImplementedException();
        }
        public List<SUBSCRIBED_TO_STREAMING_SERVICE> StreamingServiceSubscriptionsWhere(Func<SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS, QueryCondition> alias)
        {
            throw new NotImplementedException();
        }
        public List<SUBSCRIBED_TO_STREAMING_SERVICE> StreamingServiceSubscriptionsWhere(Func<SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS, QueryCondition[]> alias)
        {
            throw new NotImplementedException();
        }
        public List<SUBSCRIBED_TO_STREAMING_SERVICE> StreamingServiceSubscriptionsWhere(JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<decimal> MonthlyFee = default, JsNotation<System.DateTime> StartDate = default)
        {
            throw new NotImplementedException();
        }
        public void AddStreamingServiceSubscription(StreamingService streamingService, DateTime? moment, JsNotation<decimal> MonthlyFee = default)
        {
            if (moment is null)
                moment = DateTime.UtcNow;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            if (MonthlyFee.HasValue) properties.Add("MonthlyFee", MonthlyFee.Value);
            ((ILookupHelper<StreamingService>)InnerData.StreamingServiceSubscriptions).AddItem(streamingService, moment, properties);
        }
        public void RemoveStreamingServiceSubscription(StreamingService streamingService, DateTime? moment)
        {
            StreamingServiceSubscriptions.Remove(streamingService, moment);
        }
        public List<WATCHED_MOVIE> WatchedMovieRelations()
        {
            throw new NotImplementedException();
        }
        public List<WATCHED_MOVIE> WatchedMoviesWhere(Func<WATCHED_MOVIE_ALIAS, QueryCondition> alias)
        {
            throw new NotImplementedException();
        }
        public List<WATCHED_MOVIE> WatchedMoviesWhere(Func<WATCHED_MOVIE_ALIAS, QueryCondition[]> alias)
        {
            throw new NotImplementedException();
        }
        public List<WATCHED_MOVIE> WatchedMoviesWhere(JsNotation<System.DateTime> CreationDate = default, JsNotation<int> MinutesWatched = default)
        {
            throw new NotImplementedException();
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
        public PERSON_LIVES_IN CityRelation(DateTime? moment = null)
        {
            if (moment is null)
                moment = DateTime.UtcNow;

            throw new NotImplementedException();
        }
        public List<PERSON_LIVES_IN> CityRelations()
        {
            throw new NotImplementedException();
        }
        public PERSON_LIVES_IN GetCityIf(DateTime? moment, Func<PERSON_LIVES_IN_ALIAS, QueryCondition> alias)
        {
            if (moment is null)
                moment = DateTime.UtcNow;

            throw new NotImplementedException();
        }
        public PERSON_LIVES_IN GetCityIf(DateTime? moment, Func<PERSON_LIVES_IN_ALIAS, QueryCondition[]> alias)
        {
            if (moment is null)
                moment = DateTime.UtcNow;

            throw new NotImplementedException();
        }
        public PERSON_LIVES_IN GetCityIf(DateTime? moment, JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> AddressLine3 = default, JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<System.DateTime> StartDate = default)
        {
            if (moment is null)
                moment = DateTime.UtcNow;

            throw new NotImplementedException();
        }
        public List<PERSON_LIVES_IN> CityWhere(Func<PERSON_LIVES_IN_ALIAS, QueryCondition> alias)
        {
            throw new NotImplementedException();
        }
        public List<PERSON_LIVES_IN> CityWhere(Func<PERSON_LIVES_IN_ALIAS, QueryCondition[]> alias)
        {
            throw new NotImplementedException();
        }
        public List<PERSON_LIVES_IN> CityWhere(JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> AddressLine3 = default, JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<System.DateTime> StartDate = default)
        {
            throw new NotImplementedException();
        }
        public void SetCity(City city, DateTime? moment, JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> AddressLine3 = default)
        {
            if (moment is null)
                moment = DateTime.UtcNow;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            if (AddressLine1.HasValue) properties.Add("AddressLine1", AddressLine1.Value);
            if (AddressLine2.HasValue) properties.Add("AddressLine2", AddressLine2.Value);
            if (AddressLine3.HasValue) properties.Add("AddressLine3", AddressLine3.Value);

            ((ILookupHelper<City>)InnerData.City).SetItem(city, moment, properties);
        }








































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

            public Property Name { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["Name"];
            public Property Restaurants { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["Restaurants"];
            public Property DirectedMovies { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["DirectedMovies"];
            public Property ActedInMovies { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["ActedInMovies"];
            public Property StreamingServiceSubscriptions { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["StreamingServiceSubscriptions"];
            public Property WatchedMovies { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["WatchedMovies"];
            public Property City { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"].Properties["City"];
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

        sealed public override Entity GetEntity()
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
                EventHandler<Person, EntityEventArgs> handler = onDelete;
                if (handler is not null)
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
                EventHandler<Person, EntityEventArgs> handler = onSave;
                if (handler is not null)
                    handler.Invoke((Person)sender, args);
            }

            #endregion

            #region OnAfterSave

            private bool onAfterSaveIsRegistered = false;

            private EventHandler<Person, EntityEventArgs> onAfterSave;
            public event EventHandler<Person, EntityEventArgs> OnAfterSave
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
                EventHandler<Person, EntityEventArgs> handler = onAfterSave;
                if (handler is not null)
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
                    EventHandler<Person, PropertyEventArgs> handler = onName;
                    if (handler is not null)
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
                    EventHandler<Person, PropertyEventArgs> handler = onRestaurants;
                    if (handler is not null)
                        handler.Invoke((Person)sender, args);
                }

                #endregion

                #region OnDirectedMovies

                private static bool onDirectedMoviesIsRegistered = false;

                private static EventHandler<Person, PropertyEventArgs> onDirectedMovies;
                public static event EventHandler<Person, PropertyEventArgs> OnDirectedMovies
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onDirectedMoviesIsRegistered)
                            {
                                Members.DirectedMovies.Events.OnChange -= onDirectedMoviesProxy;
                                Members.DirectedMovies.Events.OnChange += onDirectedMoviesProxy;
                                onDirectedMoviesIsRegistered = true;
                            }
                            onDirectedMovies += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onDirectedMovies -= value;
                            if (onDirectedMovies is null && onDirectedMoviesIsRegistered)
                            {
                                Members.DirectedMovies.Events.OnChange -= onDirectedMoviesProxy;
                                onDirectedMoviesIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onDirectedMoviesProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Person, PropertyEventArgs> handler = onDirectedMovies;
                    if (handler is not null)
                        handler.Invoke((Person)sender, args);
                }

                #endregion

                #region OnActedInMovies

                private static bool onActedInMoviesIsRegistered = false;

                private static EventHandler<Person, PropertyEventArgs> onActedInMovies;
                public static event EventHandler<Person, PropertyEventArgs> OnActedInMovies
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onActedInMoviesIsRegistered)
                            {
                                Members.ActedInMovies.Events.OnChange -= onActedInMoviesProxy;
                                Members.ActedInMovies.Events.OnChange += onActedInMoviesProxy;
                                onActedInMoviesIsRegistered = true;
                            }
                            onActedInMovies += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onActedInMovies -= value;
                            if (onActedInMovies is null && onActedInMoviesIsRegistered)
                            {
                                Members.ActedInMovies.Events.OnChange -= onActedInMoviesProxy;
                                onActedInMoviesIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onActedInMoviesProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Person, PropertyEventArgs> handler = onActedInMovies;
                    if (handler is not null)
                        handler.Invoke((Person)sender, args);
                }

                #endregion

                #region OnStreamingServiceSubscriptions

                private static bool onStreamingServiceSubscriptionsIsRegistered = false;

                private static EventHandler<Person, PropertyEventArgs> onStreamingServiceSubscriptions;
                public static event EventHandler<Person, PropertyEventArgs> OnStreamingServiceSubscriptions
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onStreamingServiceSubscriptionsIsRegistered)
                            {
                                Members.StreamingServiceSubscriptions.Events.OnChange -= onStreamingServiceSubscriptionsProxy;
                                Members.StreamingServiceSubscriptions.Events.OnChange += onStreamingServiceSubscriptionsProxy;
                                onStreamingServiceSubscriptionsIsRegistered = true;
                            }
                            onStreamingServiceSubscriptions += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onStreamingServiceSubscriptions -= value;
                            if (onStreamingServiceSubscriptions is null && onStreamingServiceSubscriptionsIsRegistered)
                            {
                                Members.StreamingServiceSubscriptions.Events.OnChange -= onStreamingServiceSubscriptionsProxy;
                                onStreamingServiceSubscriptionsIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onStreamingServiceSubscriptionsProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Person, PropertyEventArgs> handler = onStreamingServiceSubscriptions;
                    if (handler is not null)
                        handler.Invoke((Person)sender, args);
                }

                #endregion

                #region OnWatchedMovies

                private static bool onWatchedMoviesIsRegistered = false;

                private static EventHandler<Person, PropertyEventArgs> onWatchedMovies;
                public static event EventHandler<Person, PropertyEventArgs> OnWatchedMovies
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onWatchedMoviesIsRegistered)
                            {
                                Members.WatchedMovies.Events.OnChange -= onWatchedMoviesProxy;
                                Members.WatchedMovies.Events.OnChange += onWatchedMoviesProxy;
                                onWatchedMoviesIsRegistered = true;
                            }
                            onWatchedMovies += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onWatchedMovies -= value;
                            if (onWatchedMovies is null && onWatchedMoviesIsRegistered)
                            {
                                Members.WatchedMovies.Events.OnChange -= onWatchedMoviesProxy;
                                onWatchedMoviesIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onWatchedMoviesProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Person, PropertyEventArgs> handler = onWatchedMovies;
                    if (handler is not null)
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
                    EventHandler<Person, PropertyEventArgs> handler = onCity;
                    if (handler is not null)
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
                    EventHandler<Person, PropertyEventArgs> handler = onUid;
                    if (handler is not null)
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
                    EventHandler<Person, PropertyEventArgs> handler = onLastModifiedOn;
                    if (handler is not null)
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