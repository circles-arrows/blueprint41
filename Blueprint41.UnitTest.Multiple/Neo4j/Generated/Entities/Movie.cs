using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Neo4j.Datastore.Query;
using node = Neo4j.Datastore.Query.Node;

namespace Neo4j.Datastore.Manipulation
{
    public interface IMovieOriginalData
    {
        string title { get; }
        string tagline { get; }
        int? released { get; }
        string Uid { get; }
    }

    public partial class Movie : OGM<Movie, Movie.MovieData, System.String>, IMovieOriginalData
    {
        #region Initialize

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

            #region LoadBytitle

            RegisterQuery(nameof(LoadBytitle), (query, alias) => query.
                Where(alias.title == Parameter.New<string>(Param0)));

            #endregion

            #region LoadByUid

            RegisterQuery(nameof(LoadByUid), (query, alias) => query.
                Where(alias.Uid == Parameter.New<string>(Param0)));

            #endregion

            AdditionalGeneratedStoredQueries();
        }
        public static Movie LoadBytitle(string title)
        {
            return FromQuery(nameof(LoadBytitle), new Parameter(Param0, title)).FirstOrDefault();
        }
        public static Movie LoadByUid(string uid)
        {
            return FromQuery(nameof(LoadByUid), new Parameter(Param0, uid)).FirstOrDefault();
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
            return $"Movie => title : {this.title?.ToString() ?? "null"}, tagline : {this.tagline?.ToString() ?? "null"}, released : {this.released?.ToString() ?? "null"}, Uid : {this.Uid}";
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
                title = data.title;
                tagline = data.tagline;
                released = data.released;
                Uid = data.Uid;
            }


            #region Initialize Collections

            protected override void InitializeCollections()
            {
                NodeType = "Movie";

            }
            public string NodeType { get; private set; }
            sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
            sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

            #endregion
            #region Map Data

            sealed public override IDictionary<string, object> MapTo()
            {
                IDictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("title",  title);
                dictionary.Add("tagline",  tagline);
                dictionary.Add("released",  Conversion<int?, long?>.Convert(released));
                dictionary.Add("Uid",  Uid);
                return dictionary;
            }

            sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
            {
                object value;
                if (properties.TryGetValue("title", out value))
                    title = (string)value;
                if (properties.TryGetValue("tagline", out value))
                    tagline = (string)value;
                if (properties.TryGetValue("released", out value))
                    released = Conversion<long, int>.Convert((long)value);
                if (properties.TryGetValue("Uid", out value))
                    Uid = (string)value;
            }

            #endregion

            #region Members for interface IMovie

            public string title { get; set; }
            public string tagline { get; set; }
            public int? released { get; set; }
            public string Uid { get; set; }

            #endregion
        }

        #endregion

        #region Outer Data

        #region Members for interface IMovie

        public string title { get { LazyGet(); return InnerData.title; } set { if (LazySet(Members.title, InnerData.title, value)) InnerData.title = value; } }
        public string tagline { get { LazyGet(); return InnerData.tagline; } set { if (LazySet(Members.tagline, InnerData.tagline, value)) InnerData.tagline = value; } }
        public int? released { get { LazyGet(); return InnerData.released; } set { if (LazySet(Members.released, InnerData.released, value)) InnerData.released = value; } }
        public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }

        #endregion

        #region Virtual Node Type
        
        public string NodeType  { get { return InnerData.NodeType; } }
        
        #endregion

        #endregion

        #region Relationship Properties

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

            public EntityProperty title { get; } = Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Movie"].Properties["title"];
            public EntityProperty tagline { get; } = Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Movie"].Properties["tagline"];
            public EntityProperty released { get; } = Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Movie"].Properties["released"];
            public EntityProperty Uid { get; } = Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Movie"].Properties["Uid"];
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
                        entity = Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Movie"];
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

                #region Ontitle

                private static bool ontitleIsRegistered = false;

                private static EventHandler<Movie, PropertyEventArgs> ontitle;
                public static event EventHandler<Movie, PropertyEventArgs> Ontitle
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!ontitleIsRegistered)
                            {
                                Members.title.Events.OnChange -= ontitleProxy;
                                Members.title.Events.OnChange += ontitleProxy;
                                ontitleIsRegistered = true;
                            }
                            ontitle += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            ontitle -= value;
                            if (ontitle is null && ontitleIsRegistered)
                            {
                                Members.title.Events.OnChange -= ontitleProxy;
                                ontitleIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void ontitleProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Movie, PropertyEventArgs> handler = ontitle;
                    if (handler is not null)
                        handler.Invoke((Movie)sender, args);
                }

                #endregion

                #region Ontagline

                private static bool ontaglineIsRegistered = false;

                private static EventHandler<Movie, PropertyEventArgs> ontagline;
                public static event EventHandler<Movie, PropertyEventArgs> Ontagline
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!ontaglineIsRegistered)
                            {
                                Members.tagline.Events.OnChange -= ontaglineProxy;
                                Members.tagline.Events.OnChange += ontaglineProxy;
                                ontaglineIsRegistered = true;
                            }
                            ontagline += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            ontagline -= value;
                            if (ontagline is null && ontaglineIsRegistered)
                            {
                                Members.tagline.Events.OnChange -= ontaglineProxy;
                                ontaglineIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void ontaglineProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Movie, PropertyEventArgs> handler = ontagline;
                    if (handler is not null)
                        handler.Invoke((Movie)sender, args);
                }

                #endregion

                #region Onreleased

                private static bool onreleasedIsRegistered = false;

                private static EventHandler<Movie, PropertyEventArgs> onreleased;
                public static event EventHandler<Movie, PropertyEventArgs> Onreleased
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onreleasedIsRegistered)
                            {
                                Members.released.Events.OnChange -= onreleasedProxy;
                                Members.released.Events.OnChange += onreleasedProxy;
                                onreleasedIsRegistered = true;
                            }
                            onreleased += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onreleased -= value;
                            if (onreleased is null && onreleasedIsRegistered)
                            {
                                Members.released.Events.OnChange -= onreleasedProxy;
                                onreleasedIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onreleasedProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Movie, PropertyEventArgs> handler = onreleased;
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

            }

            #endregion
        }

        #endregion

        #region IMovieOriginalData

        public IMovieOriginalData OriginalVersion { get { return this; } }

        #region Members for interface IMovie

        string IMovieOriginalData.title { get { return OriginalData.title; } }
        string IMovieOriginalData.tagline { get { return OriginalData.tagline; } }
        int? IMovieOriginalData.released { get { return OriginalData.released; } }
        string IMovieOriginalData.Uid { get { return OriginalData.Uid; } }

        #endregion
        #endregion
    }
}