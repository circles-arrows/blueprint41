#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable CS8981 // Names should not be lower type only

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
    public interface IMovieOriginalData : IBaseEntityOriginalData
    {
        string Title { get; }
        Person Director { get; }
        IEnumerable<Person> Actors { get; }
        Rating Certification { get; }
    }

    public partial class Movie : OGM<Movie, Movie.MovieData, System.String>, IBaseEntity, IMovieOriginalData
    {
        #region Initialize


        [Obsolete]
        static Movie()
        {
            Register.Types();
        }


        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadByKeys
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

            #region LoadByTitle

            RegisterQuery(nameof(LoadByTitle), (query, alias) => query.
                Where(alias.Title == Parameter.New<string>(Param0)));

            #endregion

            AdditionalGeneratedStoredQueries();
        }
        public static Movie LoadByTitle(string title)
        {
            return FromQuery(nameof(LoadByTitle), new Parameter(Param0, title)).FirstOrDefault();
        }
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, Movie> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

        protected static void RegisterQuery(string name, Func<IMatchQuery, q.MovieAlias, IWhereQuery> query)
        {
            q.MovieAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Movie.Alias(out alias, "node"));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

            RegisterQuery(name, compiled);
        }

        public override string ToString()
        {
            return $"Movie => Title : {this.Title}, Uid : {this.Uid}, LastModifiedOn : {this.LastModifiedOn}";
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
                    OriginalData = new MovieData(InnerData);
            }
        }


        #endregion

        #region Validations

        protected override void ValidateSave()
        {
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

            if (InnerData.Title is null)
                throw new PersistenceException(string.Format("Cannot save Movie with key '{0}' because the Title cannot be null.", this.Uid?.ToString() ?? "<null>"));
        }

        protected override void ValidateDelete()
        {
        }

        #endregion

        #region Inner Data

        public class MovieData : Data<System.String>
        {
            public MovieData()
            {

            }

            public MovieData(MovieData data)
            {
                Title = data.Title;
                Director = data.Director;
                Actors = data.Actors;
                Certification = data.Certification;
                Uid = data.Uid;
                LastModifiedOn = data.LastModifiedOn;
            }


            #region Initialize Collections

            protected override void InitializeCollections()
            {
                NodeType = "Movie";

                Director = new EntityCollection<Person>(Wrapper, Members.Director, item => { if (Members.Director.Events.HasRegisteredChangeHandlers) { int loadHack = item.DirectedMovies.Count; } });
                Actors = new EntityCollection<Person>(Wrapper, Members.Actors, item => { if (Members.Actors.Events.HasRegisteredChangeHandlers) { int loadHack = item.ActedInMovies.Count; } });
                Certification = new EntityCollection<Rating>(Wrapper, Members.Certification);
            }
            public string NodeType { get; private set; }
            sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
            sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

            #endregion
            #region Map Data

            sealed public override IDictionary<string, object> MapTo()
            {
                IDictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("Title",  Title);
                dictionary.Add("Uid",  Uid);
                dictionary.Add("LastModifiedOn",  Conversion<System.DateTime, long>.Convert(LastModifiedOn));
                return dictionary;
            }

            sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
            {
                object value;
                if (properties.TryGetValue("Title", out value))
                    Title = (string)value;
                if (properties.TryGetValue("Uid", out value))
                    Uid = (string)value;
                if (properties.TryGetValue("LastModifiedOn", out value))
                    LastModifiedOn = Conversion<long, System.DateTime>.Convert((long)value);
            }

            #endregion

            #region Members for interface IMovie

            public string Title { get; set; }
            public EntityCollection<Person> Director { get; private set; }
            public EntityCollection<Person> Actors { get; private set; }
            public EntityCollection<Rating> Certification { get; private set; }

            #endregion
            #region Members for interface IBaseEntity

            public string Uid { get; set; }
            public System.DateTime LastModifiedOn { get; set; }

            #endregion
        }

        #endregion

        #region Outer Data

        #region Members for interface IMovie

        public string Title { get { LazyGet(); return InnerData.Title; } set { if (LazySet(Members.Title, InnerData.Title, value)) InnerData.Title = value; } }
        public Person Director
        {
            get { return ((ILookupHelper<Person>)InnerData.Director).GetItem(null); }
            set 
            { 
                if (LazySet(Members.Director, ((ILookupHelper<Person>)InnerData.Director).GetItem(null), value))
                    ((ILookupHelper<Person>)InnerData.Director).SetItem(value, null); 
            }
        }
        private void ClearDirector(DateTime? moment)
        {
            ((ILookupHelper<Person>)InnerData.Director).ClearLookup(moment);
        }
        public EntityCollection<Person> Actors { get { return InnerData.Actors; } }
        private void ClearActors(DateTime? moment)
        {
            ((ILookupHelper<Person>)InnerData.Actors).ClearLookup(moment);
        }
        public Rating Certification
        {
            get { return ((ILookupHelper<Rating>)InnerData.Certification).GetItem(null); }
            set 
            { 
                if (LazySet(Members.Certification, ((ILookupHelper<Rating>)InnerData.Certification).GetItem(null), value))
                    ((ILookupHelper<Rating>)InnerData.Certification).SetItem(value, null); 
            }
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

        #region Director (Lookup)

        public PERSON_DIRECTED DirectorRelation()
        {
            return PERSON_DIRECTED.Load(_queryDirectorRelation.Value, ("key", Uid)).FirstOrDefault();
        }
        private readonly Lazy<ICompiled> _queryDirectorRelation = new Lazy<ICompiled>(delegate()
        {
            return Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.PERSON_DIRECTED.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(outAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public PERSON_DIRECTED GetDirectorIf(Func<PERSON_DIRECTED.Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.PERSON_DIRECTED.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(outAlias.Uid == Uid)
                .And(expression.Invoke(new PERSON_DIRECTED.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return PERSON_DIRECTED.Load(query).FirstOrDefault();
        }
        public PERSON_DIRECTED GetDirectorIf(Func<PERSON_DIRECTED.Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.PERSON_DIRECTED.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(outAlias.Uid == Uid)
                .And(expression.Invoke(new PERSON_DIRECTED.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return PERSON_DIRECTED.Load(query).FirstOrDefault();
        }
        public PERSON_DIRECTED GetDirectorIf(JsNotation<System.DateTime?> CreationDate = default)
        {
            return GetDirectorIf(delegate(PERSON_DIRECTED.Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);

                return conditions.ToArray();
            });
        }
        public void SetDirector(Person person)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();

            ((ILookupHelper<Person>)InnerData.Director).SetItem(person, null, properties);
        }

        #endregion

        #region Actors (Collection)

        public List<ACTED_IN> ActorRelations()
        {
            return ACTED_IN.Load(_queryActorRelations.Value, ("key", Uid));
        }
        private readonly Lazy<ICompiled> _queryActorRelations = new Lazy<ICompiled>(delegate()
        {
            return Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.ACTED_IN.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(outAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public List<ACTED_IN> ActorsWhere(Func<ACTED_IN.Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.ACTED_IN.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(outAlias.Uid == Uid)
                .And(expression.Invoke(new ACTED_IN.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return ACTED_IN.Load(query);
        }
        public List<ACTED_IN> ActorsWhere(Func<ACTED_IN.Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.ACTED_IN.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(outAlias.Uid == Uid)
                .And(expression.Invoke(new ACTED_IN.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return ACTED_IN.Load(query);
        }
        public List<ACTED_IN> ActorsWhere(JsNotation<System.DateTime?> CreationDate = default)
        {
            return ActorsWhere(delegate(ACTED_IN.Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);

                return conditions.ToArray();
            });
        }
        public void AddActor(Person person)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            ((ILookupHelper<Person>)InnerData.Actors).AddItem(person, null, properties);
        }
        public void RemoveActor(Person person)
        {
            Actors.Remove(person);
        }

        #endregion

        #region Certification (Lookup)

        public MOVIE_CERTIFICATION CertificationRelation()
        {
            return MOVIE_CERTIFICATION.Load(_queryCertificationRelation.Value, ("key", Uid)).FirstOrDefault();
        }
        private readonly Lazy<ICompiled> _queryCertificationRelation = new Lazy<ICompiled>(delegate()
        {
            return Transaction.CompiledQuery
                .Match(node.Movie.Alias(out var inAlias).In.MOVIE_CERTIFICATION.Alias(out var relAlias).Out.Rating.Alias(out var outAlias))
                .Where(inAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public MOVIE_CERTIFICATION GetCertificationIf(Func<MOVIE_CERTIFICATION.Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Movie.Alias(out var inAlias).In.MOVIE_CERTIFICATION.Alias(out var relAlias).Out.Rating.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new MOVIE_CERTIFICATION.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return MOVIE_CERTIFICATION.Load(query).FirstOrDefault();
        }
        public MOVIE_CERTIFICATION GetCertificationIf(Func<MOVIE_CERTIFICATION.Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Movie.Alias(out var inAlias).In.MOVIE_CERTIFICATION.Alias(out var relAlias).Out.Rating.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new MOVIE_CERTIFICATION.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return MOVIE_CERTIFICATION.Load(query).FirstOrDefault();
        }
        public MOVIE_CERTIFICATION GetCertificationIf(JsNotation<System.DateTime?> CreationDate = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> FrighteningIntense = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> Profanity = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> SexAndNudity = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> Substances = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> ViolenceGore = default)
        {
            return GetCertificationIf(delegate(MOVIE_CERTIFICATION.Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (FrighteningIntense.HasValue) conditions.Add(alias.FrighteningIntense == FrighteningIntense.Value?.ToString());
                if (ViolenceGore.HasValue) conditions.Add(alias.ViolenceGore == ViolenceGore.Value?.ToString());
                if (Profanity.HasValue) conditions.Add(alias.Profanity == Profanity.Value?.ToString());
                if (Substances.HasValue) conditions.Add(alias.Substances == Substances.Value?.ToString());
                if (SexAndNudity.HasValue) conditions.Add(alias.SexAndNudity == SexAndNudity.Value?.ToString());

                return conditions.ToArray();
            });
        }
        public void SetCertification(Rating rating, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> FrighteningIntense = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> Profanity = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> SexAndNudity = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> Substances = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> ViolenceGore = default)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            if (FrighteningIntense.HasValue) properties.Add("FrighteningIntense", FrighteningIntense.Value?.ToString());
            if (ViolenceGore.HasValue) properties.Add("ViolenceGore", ViolenceGore.Value?.ToString());
            if (Profanity.HasValue) properties.Add("Profanity", Profanity.Value?.ToString());
            if (Substances.HasValue) properties.Add("Substances", Substances.Value?.ToString());
            if (SexAndNudity.HasValue) properties.Add("SexAndNudity", SexAndNudity.Value?.ToString());

            ((ILookupHelper<Rating>)InnerData.Certification).SetItem(rating, null, properties);
        }

        #endregion

        private static readonly Parameter key = Parameter.New<string>("key");
        private static readonly Parameter moment = Parameter.New<DateTime>("moment");

        #endregion

        #region Reflection

        private static MovieMembers members = null;
        public static MovieMembers Members
        {
            get
            {
                if (members is null)
                {
                    lock (typeof(Movie))
                    {
                        if (members is null)
                            members = new MovieMembers();
                    }
                }
                return members;
            }
        }
        public class MovieMembers
        {
            internal MovieMembers() { }

            #region Members for interface IMovie

            public EntityProperty Title { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"].Properties["Title"];
            public EntityProperty Director { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"].Properties["Director"];
            public EntityProperty Actors { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"].Properties["Actors"];
            public EntityProperty Certification { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"].Properties["Certification"];
            #endregion

            #region Members for interface IBaseEntity

            public EntityProperty Uid { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"];
            public EntityProperty LastModifiedOn { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"];
            #endregion

        }

        private static MovieFullTextMembers fullTextMembers = null;
        public static MovieFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers is null)
                {
                    lock (typeof(Movie))
                    {
                        if (fullTextMembers is null)
                            fullTextMembers = new MovieFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class MovieFullTextMembers
        {
            internal MovieFullTextMembers() { }

        }

        sealed public override Entity GetEntity()
        {
            if (entity is null)
            {
                lock (typeof(Movie))
                {
                    if (entity is null)
                        entity = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"];
                }
            }
            return entity;
        }

        private static MovieEvents events = null;
        public static MovieEvents Events
        {
            get
            {
                if (events is null)
                {
                    lock (typeof(Movie))
                    {
                        if (events is null)
                            events = new MovieEvents();
                    }
                }
                return events;
            }
        }
        public class MovieEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Movie, EntityEventArgs> onNew;
            public event EventHandler<Movie, EntityEventArgs> OnNew
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
                EventHandler<Movie, EntityEventArgs> handler = onNew;
                if (handler is not null)
                    handler.Invoke((Movie)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<Movie, EntityEventArgs> onDelete;
            public event EventHandler<Movie, EntityEventArgs> OnDelete
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
                EventHandler<Movie, EntityEventArgs> handler = onDelete;
                if (handler is not null)
                    handler.Invoke((Movie)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<Movie, EntityEventArgs> onSave;
            public event EventHandler<Movie, EntityEventArgs> OnSave
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
                EventHandler<Movie, EntityEventArgs> handler = onSave;
                if (handler is not null)
                    handler.Invoke((Movie)sender, args);
            }

            #endregion

            #region OnAfterSave

            private bool onAfterSaveIsRegistered = false;

            private EventHandler<Movie, EntityEventArgs> onAfterSave;
            public event EventHandler<Movie, EntityEventArgs> OnAfterSave
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
                EventHandler<Movie, EntityEventArgs> handler = onAfterSave;
                if (handler is not null)
                    handler.Invoke((Movie)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

                #region OnTitle

                private static bool onTitleIsRegistered = false;

                private static EventHandler<Movie, PropertyEventArgs> onTitle;
                public static event EventHandler<Movie, PropertyEventArgs> OnTitle
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onTitleIsRegistered)
                            {
                                Members.Title.Events.OnChange -= onTitleProxy;
                                Members.Title.Events.OnChange += onTitleProxy;
                                onTitleIsRegistered = true;
                            }
                            onTitle += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onTitle -= value;
                            if (onTitle is null && onTitleIsRegistered)
                            {
                                Members.Title.Events.OnChange -= onTitleProxy;
                                onTitleIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onTitleProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Movie, PropertyEventArgs> handler = onTitle;
                    if (handler is not null)
                        handler.Invoke((Movie)sender, args);
                }

                #endregion

                #region OnDirector

                private static bool onDirectorIsRegistered = false;

                private static EventHandler<Movie, PropertyEventArgs> onDirector;
                public static event EventHandler<Movie, PropertyEventArgs> OnDirector
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onDirectorIsRegistered)
                            {
                                Members.Director.Events.OnChange -= onDirectorProxy;
                                Members.Director.Events.OnChange += onDirectorProxy;
                                onDirectorIsRegistered = true;
                            }
                            onDirector += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onDirector -= value;
                            if (onDirector is null && onDirectorIsRegistered)
                            {
                                Members.Director.Events.OnChange -= onDirectorProxy;
                                onDirectorIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onDirectorProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Movie, PropertyEventArgs> handler = onDirector;
                    if (handler is not null)
                        handler.Invoke((Movie)sender, args);
                }

                #endregion

                #region OnActors

                private static bool onActorsIsRegistered = false;

                private static EventHandler<Movie, PropertyEventArgs> onActors;
                public static event EventHandler<Movie, PropertyEventArgs> OnActors
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onActorsIsRegistered)
                            {
                                Members.Actors.Events.OnChange -= onActorsProxy;
                                Members.Actors.Events.OnChange += onActorsProxy;
                                onActorsIsRegistered = true;
                            }
                            onActors += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onActors -= value;
                            if (onActors is null && onActorsIsRegistered)
                            {
                                Members.Actors.Events.OnChange -= onActorsProxy;
                                onActorsIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onActorsProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Movie, PropertyEventArgs> handler = onActors;
                    if (handler is not null)
                        handler.Invoke((Movie)sender, args);
                }

                #endregion

                #region OnCertification

                private static bool onCertificationIsRegistered = false;

                private static EventHandler<Movie, PropertyEventArgs> onCertification;
                public static event EventHandler<Movie, PropertyEventArgs> OnCertification
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onCertificationIsRegistered)
                            {
                                Members.Certification.Events.OnChange -= onCertificationProxy;
                                Members.Certification.Events.OnChange += onCertificationProxy;
                                onCertificationIsRegistered = true;
                            }
                            onCertification += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onCertification -= value;
                            if (onCertification is null && onCertificationIsRegistered)
                            {
                                Members.Certification.Events.OnChange -= onCertificationProxy;
                                onCertificationIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onCertificationProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Movie, PropertyEventArgs> handler = onCertification;
                    if (handler is not null)
                        handler.Invoke((Movie)sender, args);
                }

                #endregion

                #region OnUid

                private static bool onUidIsRegistered = false;

                private static EventHandler<Movie, PropertyEventArgs> onUid;
                public static event EventHandler<Movie, PropertyEventArgs> OnUid
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
                    EventHandler<Movie, PropertyEventArgs> handler = onUid;
                    if (handler is not null)
                        handler.Invoke((Movie)sender, args);
                }

                #endregion

                #region OnLastModifiedOn

                private static bool onLastModifiedOnIsRegistered = false;

                private static EventHandler<Movie, PropertyEventArgs> onLastModifiedOn;
                public static event EventHandler<Movie, PropertyEventArgs> OnLastModifiedOn
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
                    EventHandler<Movie, PropertyEventArgs> handler = onLastModifiedOn;
                    if (handler is not null)
                        handler.Invoke((Movie)sender, args);
                }

                #endregion

            }

            #endregion
        }

        #endregion

        #region IMovieOriginalData

        public IMovieOriginalData OriginalVersion { get { return this; } }

        #region Members for interface IMovie

        string IMovieOriginalData.Title { get { return OriginalData.Title; } }
        Person IMovieOriginalData.Director { get { return ((ILookupHelper<Person>)OriginalData.Director).GetOriginalItem(null); } }
        IEnumerable<Person> IMovieOriginalData.Actors { get { return OriginalData.Actors.OriginalData; } }
        Rating IMovieOriginalData.Certification { get { return ((ILookupHelper<Rating>)OriginalData.Certification).GetOriginalItem(null); } }

        #endregion
        #region Members for interface IBaseEntity

        IBaseEntityOriginalData IBaseEntity.OriginalVersion { get { return this; } }

        string IBaseEntityOriginalData.Uid { get { return OriginalData.Uid; } }
        System.DateTime IBaseEntityOriginalData.LastModifiedOn { get { return OriginalData.LastModifiedOn; } }

        #endregion
        #endregion
    }
}