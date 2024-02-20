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
    public interface IGenreOriginalData
    {
        string Uid { get; }
        string Name { get; }
        IEnumerable<Movie> Movies { get; }
    }

    public partial class Genre : OGM<Genre, Genre.GenreData, System.String>, IGenreOriginalData
    {
        #region Initialize

        static Genre()
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
        public static Genre LoadByUid(string uid)
        {
            return FromQuery(nameof(LoadByUid), new Parameter(Param0, uid)).FirstOrDefault();
        }
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, Genre> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

        protected static void RegisterQuery(string name, Func<IMatchQuery, q.GenreAlias, IWhereQuery> query)
        {
            q.GenreAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Genre.Alias(out alias, "node"));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

            RegisterQuery(name, compiled);
        }

        public override string ToString()
        {
            return $"Genre => Uid : {this.Uid}, Name : {this.Name}";
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
                    OriginalData = new GenreData(InnerData);
            }
        }


        #endregion

        #region Validations

        protected override void ValidateSave()
        {
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

            if (InnerData.Name is null)
                throw new PersistenceException(string.Format("Cannot save Genre with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
        }

        protected override void ValidateDelete()
        {
        }

        #endregion

        #region Inner Data

        public class GenreData : Data<System.String>
        {
            public GenreData()
            {

            }

            public GenreData(GenreData data)
            {
                Uid = data.Uid;
                Name = data.Name;
                Movies = data.Movies;
            }


            #region Initialize Collections

            protected override void InitializeCollections()
            {
                NodeType = "Genre";

                Movies = new EntityCollection<Movie>(Wrapper, Members.Movies, item => { if (Members.Movies.Events.HasRegisteredChangeHandlers) { int loadHack = item.Genres.Count; } });
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
                dictionary.Add("Name",  Name);
                return dictionary;
            }

            sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
            {
                object value;
                if (properties.TryGetValue("Uid", out value))
                    Uid = (string)value;
                if (properties.TryGetValue("Name", out value))
                    Name = (string)value;
            }

            #endregion

            #region Members for interface IGenre

            public string Uid { get; set; }
            public string Name { get; set; }
            public EntityCollection<Movie> Movies { get; private set; }

            #endregion
        }

        #endregion

        #region Outer Data

        #region Members for interface IGenre

        public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }
        public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
        public EntityCollection<Movie> Movies { get { return InnerData.Movies; } }
        private void ClearMovies(DateTime? moment)
        {
            ((ILookupHelper<Movie>)InnerData.Movies).ClearLookup(moment);
        }

        #endregion

        #region Virtual Node Type
        
        public string NodeType  { get { return InnerData.NodeType; } }
        
        #endregion

        #endregion

        #region Relationship Properties

        #region Movies (Collection)

        public List<CONTAINS_GENRE> MovieRelations()
        {
            return CONTAINS_GENRE.Load(_queryMovieRelations.Value, ("key", Uid));
        }
        private readonly Lazy<ICompiled> _queryMovieRelations = new Lazy<ICompiled>(delegate()
        {
            return Transaction.CompiledQuery
                .Match(node.Movie.Alias(out var inAlias).In.CONTAINS_GENRE.Alias(out var relAlias).Out.Genre.Alias(out var outAlias))
                .Where(outAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public List<CONTAINS_GENRE> MoviesWhere(Func<CONTAINS_GENRE.Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Movie.Alias(out var inAlias).In.CONTAINS_GENRE.Alias(out var relAlias).Out.Genre.Alias(out var outAlias))
                .Where(outAlias.Uid == Uid)
                .And(expression.Invoke(new CONTAINS_GENRE.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return CONTAINS_GENRE.Load(query);
        }
        public List<CONTAINS_GENRE> MoviesWhere(Func<CONTAINS_GENRE.Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Movie.Alias(out var inAlias).In.CONTAINS_GENRE.Alias(out var relAlias).Out.Genre.Alias(out var outAlias))
                .Where(outAlias.Uid == Uid)
                .And(expression.Invoke(new CONTAINS_GENRE.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return CONTAINS_GENRE.Load(query);
        }
        public List<CONTAINS_GENRE> MoviesWhere(JsNotation<System.DateTime?> CreationDate = default)
        {
            return MoviesWhere(delegate(CONTAINS_GENRE.Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);

                return conditions.ToArray();
            });
        }
        public void AddMovie(Movie movie)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            ((ILookupHelper<Movie>)InnerData.Movies).AddItem(movie, null, properties);
        }
        public void RemoveMovie(Movie movie)
        {
            Movies.Remove(movie);
        }

        #endregion

        private static readonly Parameter key = Parameter.New<string>("key");
        private static readonly Parameter moment = Parameter.New<DateTime>("moment");

        #endregion

        #region Reflection

        private static GenreMembers members = null;
        public static GenreMembers Members
        {
            get
            {
                if (members is null)
                {
                    lock (typeof(Genre))
                    {
                        if (members is null)
                            members = new GenreMembers();
                    }
                }
                return members;
            }
        }
        public class GenreMembers
        {
            internal GenreMembers() { }

            #region Members for interface IGenre

            public Property Uid { get; } = MovieGraph.Model.Datastore.Model.Entities["Genre"].Properties["Uid"];
            public Property Name { get; } = MovieGraph.Model.Datastore.Model.Entities["Genre"].Properties["Name"];
            public Property Movies { get; } = MovieGraph.Model.Datastore.Model.Entities["Genre"].Properties["Movies"];
            #endregion

        }

        private static GenreFullTextMembers fullTextMembers = null;
        public static GenreFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers is null)
                {
                    lock (typeof(Genre))
                    {
                        if (fullTextMembers is null)
                            fullTextMembers = new GenreFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class GenreFullTextMembers
        {
            internal GenreFullTextMembers() { }

        }

        sealed public override Entity GetEntity()
        {
            if (entity is null)
            {
                lock (typeof(Genre))
                {
                    if (entity is null)
                        entity = MovieGraph.Model.Datastore.Model.Entities["Genre"];
                }
            }
            return entity;
        }

        private static GenreEvents events = null;
        public static GenreEvents Events
        {
            get
            {
                if (events is null)
                {
                    lock (typeof(Genre))
                    {
                        if (events is null)
                            events = new GenreEvents();
                    }
                }
                return events;
            }
        }
        public class GenreEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Genre, EntityEventArgs> onNew;
            public event EventHandler<Genre, EntityEventArgs> OnNew
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
                EventHandler<Genre, EntityEventArgs> handler = onNew;
                if (handler is not null)
                    handler.Invoke((Genre)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<Genre, EntityEventArgs> onDelete;
            public event EventHandler<Genre, EntityEventArgs> OnDelete
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
                EventHandler<Genre, EntityEventArgs> handler = onDelete;
                if (handler is not null)
                    handler.Invoke((Genre)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<Genre, EntityEventArgs> onSave;
            public event EventHandler<Genre, EntityEventArgs> OnSave
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
                EventHandler<Genre, EntityEventArgs> handler = onSave;
                if (handler is not null)
                    handler.Invoke((Genre)sender, args);
            }

            #endregion

            #region OnAfterSave

            private bool onAfterSaveIsRegistered = false;

            private EventHandler<Genre, EntityEventArgs> onAfterSave;
            public event EventHandler<Genre, EntityEventArgs> OnAfterSave
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
                EventHandler<Genre, EntityEventArgs> handler = onAfterSave;
                if (handler is not null)
                    handler.Invoke((Genre)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

                #region OnUid

                private static bool onUidIsRegistered = false;

                private static EventHandler<Genre, PropertyEventArgs> onUid;
                public static event EventHandler<Genre, PropertyEventArgs> OnUid
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
                    EventHandler<Genre, PropertyEventArgs> handler = onUid;
                    if (handler is not null)
                        handler.Invoke((Genre)sender, args);
                }

                #endregion

                #region OnName

                private static bool onNameIsRegistered = false;

                private static EventHandler<Genre, PropertyEventArgs> onName;
                public static event EventHandler<Genre, PropertyEventArgs> OnName
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
                    EventHandler<Genre, PropertyEventArgs> handler = onName;
                    if (handler is not null)
                        handler.Invoke((Genre)sender, args);
                }

                #endregion

                #region OnMovies

                private static bool onMoviesIsRegistered = false;

                private static EventHandler<Genre, PropertyEventArgs> onMovies;
                public static event EventHandler<Genre, PropertyEventArgs> OnMovies
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onMoviesIsRegistered)
                            {
                                Members.Movies.Events.OnChange -= onMoviesProxy;
                                Members.Movies.Events.OnChange += onMoviesProxy;
                                onMoviesIsRegistered = true;
                            }
                            onMovies += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onMovies -= value;
                            if (onMovies is null && onMoviesIsRegistered)
                            {
                                Members.Movies.Events.OnChange -= onMoviesProxy;
                                onMoviesIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onMoviesProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Genre, PropertyEventArgs> handler = onMovies;
                    if (handler is not null)
                        handler.Invoke((Genre)sender, args);
                }

                #endregion

            }

            #endregion
        }

        #endregion

        #region Static Data

        public static class StaticData
        {
            /// <summary>
            /// Get the 'Key' value for the corresponding Name
            /// </summary>
            public static class Name
            {
                public const string Action = "1";
                public const string Animation = "2";
                public const string Biography = "3";
                public const string Comedy = "4";
                public const string Documentary = "5";
                public const string Drama = "6";
                public const string Fantasy = "7";
                public const string Horror = "8";
                public const string Musical = "9";
                public const string Reality_Tv = "10";
                public const string Romance = "11";
                public const string Sci_Fi = "12";
                public const string Short = "13";
                public const string Talk_Show = "14";
                public const string Thriller = "15";
                public static bool Exist(string name)
                {
                    if (name == "Action")
                        return true;
                    if (name == "Animation")
                        return true;
                    if (name == "Biography")
                        return true;
                    if (name == "Comedy")
                        return true;
                    if (name == "Documentary")
                        return true;
                    if (name == "Drama")
                        return true;
                    if (name == "Fantasy")
                        return true;
                    if (name == "Horror")
                        return true;
                    if (name == "Musical")
                        return true;
                    if (name == "Reality-Tv")
                        return true;
                    if (name == "Romance")
                        return true;
                    if (name == "Sci-Fi")
                        return true;
                    if (name == "Short")
                        return true;
                    if (name == "Talk-Show")
                        return true;
                    if (name == "Thriller")
                        return true;
                    return false;
                }
            }
            /// <summary>
            /// Get the 'Key' value for the corresponding Uid
            /// </summary>
            public static class Uid
            {
                public const string _1 = "1";
                public const string _2 = "2";
                public const string _3 = "3";
                public const string _4 = "4";
                public const string _5 = "5";
                public const string _6 = "6";
                public const string _7 = "7";
                public const string _8 = "8";
                public const string _9 = "9";
                public const string _10 = "10";
                public const string _11 = "11";
                public const string _12 = "12";
                public const string _13 = "13";
                public const string _14 = "14";
                public const string _15 = "15";
                public static bool Exist(string uid)
                {
                    if (uid == "1")
                        return true;
                    if (uid == "2")
                        return true;
                    if (uid == "3")
                        return true;
                    if (uid == "4")
                        return true;
                    if (uid == "5")
                        return true;
                    if (uid == "6")
                        return true;
                    if (uid == "7")
                        return true;
                    if (uid == "8")
                        return true;
                    if (uid == "9")
                        return true;
                    if (uid == "10")
                        return true;
                    if (uid == "11")
                        return true;
                    if (uid == "12")
                        return true;
                    if (uid == "13")
                        return true;
                    if (uid == "14")
                        return true;
                    if (uid == "15")
                        return true;
                    return false;
                }
            }
        }

        #endregion

        #region IGenreOriginalData

        public IGenreOriginalData OriginalVersion { get { return this; } }

        #region Members for interface IGenre

        string IGenreOriginalData.Uid { get { return OriginalData.Uid; } }
        string IGenreOriginalData.Name { get { return OriginalData.Name; } }
        IEnumerable<Movie> IGenreOriginalData.Movies { get { return OriginalData.Movies.OriginalData; } }

        #endregion
        #endregion
    }
}