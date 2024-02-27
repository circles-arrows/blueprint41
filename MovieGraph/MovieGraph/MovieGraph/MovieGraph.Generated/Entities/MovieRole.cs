using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Domain.Data.Query;
using node = Domain.Data.Query.Node;

namespace Domain.Data.Manipulation
{
    public interface IMovieRoleOriginalData
    {
        string Uid { get; }
        System.Collections.Generic.List<string> Role { get; }
        Movie Movie { get; }
    }

    public partial class MovieRole : OGM<MovieRole, MovieRole.MovieRoleData, System.String>, IMovieRoleOriginalData
    {
        #region Initialize

        static MovieRole()
        {
            Register.Types();
        }


        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadByKeys
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

            #region LoadByUid

            RegisterQuery(nameof(LoadByUid), (query, alias) => query.
                Where(alias.Uid == Parameter.New<string>(Param0)));

            #endregion

            AdditionalGeneratedStoredQueries();
        }
        public static MovieRole LoadByUid(string uid)
        {
            return FromQuery(nameof(LoadByUid), new Parameter(Param0, uid)).FirstOrDefault();
        }
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, MovieRole> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

        protected static void RegisterQuery(string name, Func<IMatchQuery, q.MovieRoleAlias, IWhereQuery> query)
        {
            q.MovieRoleAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.MovieRole.Alias(out alias, "node"));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

            RegisterQuery(name, compiled);
        }

        public override string ToString()
        {
            return $"MovieRole => Uid : {this.Uid}, Role : {this.Role?.ToString() ?? "null"}";
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
                    OriginalData = new MovieRoleData(InnerData);
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

        public class MovieRoleData : Data<System.String>
        {
            public MovieRoleData()
            {

            }

            public MovieRoleData(MovieRoleData data)
            {
                Uid = data.Uid;
                Role = data.Role;
                Movie = data.Movie;
            }


            #region Initialize Collections

            protected override void InitializeCollections()
            {
                NodeType = "MovieRole";

                Movie = new EntityCollection<Movie>(Wrapper, Members.Movie);
            }
            public string NodeType { get; private set; }
            sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
            sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

            #endregion
            #region Map Data

            sealed public override IDictionary<string, object> MapTo()
            {
                IDictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("Uid",  Uid);
                dictionary.Add("Role",  Conversion<System.Collections.Generic.List<string>, System.Collections.Generic.List<object>>.Convert(Role));
                return dictionary;
            }

            sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
            {
                object value;
                if (properties.TryGetValue("Uid", out value))
                    Uid = (string)value;
                if (properties.TryGetValue("Role", out value))
                    Role = Conversion<System.Collections.Generic.List<object>, System.Collections.Generic.List<string>>.Convert((System.Collections.Generic.List<object>)value);
            }

            #endregion

            #region Members for interface IMovieRole

            public string Uid { get; set; }
            public System.Collections.Generic.List<string> Role { get; set; }
            public EntityCollection<Movie> Movie { get; private set; }

            #endregion
        }

        #endregion

        #region Outer Data

        #region Members for interface IMovieRole

        public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }
        public System.Collections.Generic.List<string> Role { get { LazyGet(); return InnerData.Role; } set { if (LazySet(Members.Role, InnerData.Role, value)) InnerData.Role = value; } }
        public Movie Movie
        {
            get { return ((ILookupHelper<Movie>)InnerData.Movie).GetItem(null); }
            set 
            { 
                if (LazySet(Members.Movie, ((ILookupHelper<Movie>)InnerData.Movie).GetItem(null), value))
                    ((ILookupHelper<Movie>)InnerData.Movie).SetItem(value, null); 
            }
        }

        #endregion

        #region Virtual Node Type
        
        public string NodeType  { get { return InnerData.NodeType; } }
        
        #endregion

        #endregion

        #region Relationship Properties

        #region Movie (Lookup)

        public MOVIEROLE_HAS_MOVIE MovieRelation()
        {
            return MOVIEROLE_HAS_MOVIE.Load(_queryMovieRelation.Value, ("key", Uid)).FirstOrDefault();
        }
        private readonly Lazy<ICompiled> _queryMovieRelation = new Lazy<ICompiled>(delegate()
        {
            return Transaction.CompiledQuery
                .Match(node.MovieRole.Alias(out var inAlias).In.MOVIEROLE_HAS_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public MOVIEROLE_HAS_MOVIE GetMovieIf(Func<MOVIEROLE_HAS_MOVIE.Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.MovieRole.Alias(out var inAlias).In.MOVIEROLE_HAS_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new MOVIEROLE_HAS_MOVIE.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return MOVIEROLE_HAS_MOVIE.Load(query).FirstOrDefault();
        }
        public MOVIEROLE_HAS_MOVIE GetMovieIf(Func<MOVIEROLE_HAS_MOVIE.Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.MovieRole.Alias(out var inAlias).In.MOVIEROLE_HAS_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new MOVIEROLE_HAS_MOVIE.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return MOVIEROLE_HAS_MOVIE.Load(query).FirstOrDefault();
        }
        public MOVIEROLE_HAS_MOVIE GetMovieIf(JsNotation<System.DateTime?> CreationDate = default)
        {
            return GetMovieIf(delegate(MOVIEROLE_HAS_MOVIE.Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);

                return conditions.ToArray();
            });
        }
        public void SetMovie(Movie movie)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();

            ((ILookupHelper<Movie>)InnerData.Movie).SetItem(movie, null, properties);
        }

        #endregion

        private static readonly Parameter key = Parameter.New<string>("key");
        private static readonly Parameter moment = Parameter.New<DateTime>("moment");

        #endregion

        #region Reflection

        private static MovieRoleMembers members = null;
        public static MovieRoleMembers Members
        {
            get
            {
                if (members is null)
                {
                    lock (typeof(MovieRole))
                    {
                        if (members is null)
                            members = new MovieRoleMembers();
                    }
                }
                return members;
            }
        }
        public class MovieRoleMembers
        {
            internal MovieRoleMembers() { }

            #region Members for interface IMovieRole

            public EntityProperty Uid { get; } = MovieGraph.Model.Datastore.Model.Entities["MovieRole"].Properties["Uid"];
            public EntityProperty Role { get; } = MovieGraph.Model.Datastore.Model.Entities["MovieRole"].Properties["Role"];
            public EntityProperty Movie { get; } = MovieGraph.Model.Datastore.Model.Entities["MovieRole"].Properties["Movie"];
            #endregion

        }

        private static MovieRoleFullTextMembers fullTextMembers = null;
        public static MovieRoleFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers is null)
                {
                    lock (typeof(MovieRole))
                    {
                        if (fullTextMembers is null)
                            fullTextMembers = new MovieRoleFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class MovieRoleFullTextMembers
        {
            internal MovieRoleFullTextMembers() { }

        }

        sealed public override Entity GetEntity()
        {
            if (entity is null)
            {
                lock (typeof(MovieRole))
                {
                    if (entity is null)
                        entity = MovieGraph.Model.Datastore.Model.Entities["MovieRole"];
                }
            }
            return entity;
        }

        private static MovieRoleEvents events = null;
        public static MovieRoleEvents Events
        {
            get
            {
                if (events is null)
                {
                    lock (typeof(MovieRole))
                    {
                        if (events is null)
                            events = new MovieRoleEvents();
                    }
                }
                return events;
            }
        }
        public class MovieRoleEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<MovieRole, EntityEventArgs> onNew;
            public event EventHandler<MovieRole, EntityEventArgs> OnNew
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
                EventHandler<MovieRole, EntityEventArgs> handler = onNew;
                if (handler is not null)
                    handler.Invoke((MovieRole)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<MovieRole, EntityEventArgs> onDelete;
            public event EventHandler<MovieRole, EntityEventArgs> OnDelete
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
                EventHandler<MovieRole, EntityEventArgs> handler = onDelete;
                if (handler is not null)
                    handler.Invoke((MovieRole)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<MovieRole, EntityEventArgs> onSave;
            public event EventHandler<MovieRole, EntityEventArgs> OnSave
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
                EventHandler<MovieRole, EntityEventArgs> handler = onSave;
                if (handler is not null)
                    handler.Invoke((MovieRole)sender, args);
            }

            #endregion

            #region OnAfterSave

            private bool onAfterSaveIsRegistered = false;

            private EventHandler<MovieRole, EntityEventArgs> onAfterSave;
            public event EventHandler<MovieRole, EntityEventArgs> OnAfterSave
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
                EventHandler<MovieRole, EntityEventArgs> handler = onAfterSave;
                if (handler is not null)
                    handler.Invoke((MovieRole)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

                #region OnUid

                private static bool onUidIsRegistered = false;

                private static EventHandler<MovieRole, PropertyEventArgs> onUid;
                public static event EventHandler<MovieRole, PropertyEventArgs> OnUid
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
                    EventHandler<MovieRole, PropertyEventArgs> handler = onUid;
                    if (handler is not null)
                        handler.Invoke((MovieRole)sender, args);
                }

                #endregion

                #region OnRole

                private static bool onRoleIsRegistered = false;

                private static EventHandler<MovieRole, PropertyEventArgs> onRole;
                public static event EventHandler<MovieRole, PropertyEventArgs> OnRole
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onRoleIsRegistered)
                            {
                                Members.Role.Events.OnChange -= onRoleProxy;
                                Members.Role.Events.OnChange += onRoleProxy;
                                onRoleIsRegistered = true;
                            }
                            onRole += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onRole -= value;
                            if (onRole is null && onRoleIsRegistered)
                            {
                                Members.Role.Events.OnChange -= onRoleProxy;
                                onRoleIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onRoleProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<MovieRole, PropertyEventArgs> handler = onRole;
                    if (handler is not null)
                        handler.Invoke((MovieRole)sender, args);
                }

                #endregion

                #region OnMovie

                private static bool onMovieIsRegistered = false;

                private static EventHandler<MovieRole, PropertyEventArgs> onMovie;
                public static event EventHandler<MovieRole, PropertyEventArgs> OnMovie
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onMovieIsRegistered)
                            {
                                Members.Movie.Events.OnChange -= onMovieProxy;
                                Members.Movie.Events.OnChange += onMovieProxy;
                                onMovieIsRegistered = true;
                            }
                            onMovie += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onMovie -= value;
                            if (onMovie is null && onMovieIsRegistered)
                            {
                                Members.Movie.Events.OnChange -= onMovieProxy;
                                onMovieIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onMovieProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<MovieRole, PropertyEventArgs> handler = onMovie;
                    if (handler is not null)
                        handler.Invoke((MovieRole)sender, args);
                }

                #endregion

            }

            #endregion
        }

        #endregion

        #region IMovieRoleOriginalData

        public IMovieRoleOriginalData OriginalVersion { get { return this; } }

        #region Members for interface IMovieRole

        string IMovieRoleOriginalData.Uid { get { return OriginalData.Uid; } }
        System.Collections.Generic.List<string> IMovieRoleOriginalData.Role { get { return OriginalData.Role; } }
        Movie IMovieRoleOriginalData.Movie { get { return ((ILookupHelper<Movie>)OriginalData.Movie).GetOriginalItem(null); } }

        #endregion
        #endregion
    }
}