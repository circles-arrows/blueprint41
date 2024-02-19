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
    public interface IMovieReviewOriginalData
    {
        string Uid { get; }
        string Review { get; }
        decimal? Rating { get; }
        Movie Movie { get; }
    }

    public partial class MovieReview : OGM<MovieReview, MovieReview.MovieReviewData, System.String>, IMovieReviewOriginalData
    {
        #region Initialize

        static MovieReview()
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
        public static MovieReview LoadByUid(string uid)
        {
            return FromQuery(nameof(LoadByUid), new Parameter(Param0, uid)).FirstOrDefault();
        }
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, MovieReview> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

        protected static void RegisterQuery(string name, Func<IMatchQuery, q.MovieReviewAlias, IWhereQuery> query)
        {
            q.MovieReviewAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.MovieReview.Alias(out alias, "node"));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

            RegisterQuery(name, compiled);
        }

        public override string ToString()
        {
            return $"MovieReview => Uid : {this.Uid}, Review : {this.Review?.ToString() ?? "null"}, Rating : {this.Rating?.ToString() ?? "null"}";
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
                    OriginalData = new MovieReviewData(InnerData);
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

        public class MovieReviewData : Data<System.String>
        {
            public MovieReviewData()
            {

            }

            public MovieReviewData(MovieReviewData data)
            {
                Uid = data.Uid;
                Review = data.Review;
                Rating = data.Rating;
                Movie = data.Movie;
            }


            #region Initialize Collections

            protected override void InitializeCollections()
            {
                NodeType = "MovieReview";

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
                dictionary.Add("Review",  Review);
                dictionary.Add("Rating",  Conversion<decimal?, long?>.Convert(Rating));
                return dictionary;
            }

            sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
            {
                object value;
                if (properties.TryGetValue("Uid", out value))
                    Uid = (string)value;
                if (properties.TryGetValue("Review", out value))
                    Review = (string)value;
                if (properties.TryGetValue("Rating", out value))
                    Rating = Conversion<long, decimal>.Convert((long)value);
            }

            #endregion

            #region Members for interface IMovieReview

            public string Uid { get; set; }
            public string Review { get; set; }
            public decimal? Rating { get; set; }
            public EntityCollection<Movie> Movie { get; private set; }

            #endregion
        }

        #endregion

        #region Outer Data

        #region Members for interface IMovieReview

        public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }
        public string Review { get { LazyGet(); return InnerData.Review; } set { if (LazySet(Members.Review, InnerData.Review, value)) InnerData.Review = value; } }
        public decimal? Rating { get { LazyGet(); return InnerData.Rating; } set { if (LazySet(Members.Rating, InnerData.Rating, value)) InnerData.Rating = value; } }
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

        public MOVIEREVIEW_HAS_MOVIE MovieRelation()
        {
            return MOVIEREVIEW_HAS_MOVIE.Load(_queryMovieRelation.Value, ("key", Uid)).FirstOrDefault();
        }
        private readonly Lazy<ICompiled> _queryMovieRelation = new Lazy<ICompiled>(delegate()
        {
            return Transaction.CompiledQuery
                .Match(node.MovieReview.Alias(out var inAlias).In.MOVIEREVIEW_HAS_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public MOVIEREVIEW_HAS_MOVIE GetMovieIf(Func<MOVIEREVIEW_HAS_MOVIE.Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.MovieReview.Alias(out var inAlias).In.MOVIEREVIEW_HAS_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new MOVIEREVIEW_HAS_MOVIE.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return MOVIEREVIEW_HAS_MOVIE.Load(query).FirstOrDefault();
        }
        public MOVIEREVIEW_HAS_MOVIE GetMovieIf(Func<MOVIEREVIEW_HAS_MOVIE.Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.MovieReview.Alias(out var inAlias).In.MOVIEREVIEW_HAS_MOVIE.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new MOVIEREVIEW_HAS_MOVIE.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return MOVIEREVIEW_HAS_MOVIE.Load(query).FirstOrDefault();
        }
        public MOVIEREVIEW_HAS_MOVIE GetMovieIf(JsNotation<System.DateTime?> CreationDate = default)
        {
            return GetMovieIf(delegate(MOVIEREVIEW_HAS_MOVIE.Alias alias)
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

        private static MovieReviewMembers members = null;
        public static MovieReviewMembers Members
        {
            get
            {
                if (members is null)
                {
                    lock (typeof(MovieReview))
                    {
                        if (members is null)
                            members = new MovieReviewMembers();
                    }
                }
                return members;
            }
        }
        public class MovieReviewMembers
        {
            internal MovieReviewMembers() { }

            #region Members for interface IMovieReview

            public Property Uid { get; } = MovieGraph.Model.Datastore.Model.Entities["MovieReview"].Properties["Uid"];
            public Property Review { get; } = MovieGraph.Model.Datastore.Model.Entities["MovieReview"].Properties["Review"];
            public Property Rating { get; } = MovieGraph.Model.Datastore.Model.Entities["MovieReview"].Properties["Rating"];
            public Property Movie { get; } = MovieGraph.Model.Datastore.Model.Entities["MovieReview"].Properties["Movie"];
            #endregion

        }

        private static MovieReviewFullTextMembers fullTextMembers = null;
        public static MovieReviewFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers is null)
                {
                    lock (typeof(MovieReview))
                    {
                        if (fullTextMembers is null)
                            fullTextMembers = new MovieReviewFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class MovieReviewFullTextMembers
        {
            internal MovieReviewFullTextMembers() { }

        }

        sealed public override Entity GetEntity()
        {
            if (entity is null)
            {
                lock (typeof(MovieReview))
                {
                    if (entity is null)
                        entity = MovieGraph.Model.Datastore.Model.Entities["MovieReview"];
                }
            }
            return entity;
        }

        private static MovieReviewEvents events = null;
        public static MovieReviewEvents Events
        {
            get
            {
                if (events is null)
                {
                    lock (typeof(MovieReview))
                    {
                        if (events is null)
                            events = new MovieReviewEvents();
                    }
                }
                return events;
            }
        }
        public class MovieReviewEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<MovieReview, EntityEventArgs> onNew;
            public event EventHandler<MovieReview, EntityEventArgs> OnNew
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
                EventHandler<MovieReview, EntityEventArgs> handler = onNew;
                if (handler is not null)
                    handler.Invoke((MovieReview)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<MovieReview, EntityEventArgs> onDelete;
            public event EventHandler<MovieReview, EntityEventArgs> OnDelete
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
                EventHandler<MovieReview, EntityEventArgs> handler = onDelete;
                if (handler is not null)
                    handler.Invoke((MovieReview)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<MovieReview, EntityEventArgs> onSave;
            public event EventHandler<MovieReview, EntityEventArgs> OnSave
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
                EventHandler<MovieReview, EntityEventArgs> handler = onSave;
                if (handler is not null)
                    handler.Invoke((MovieReview)sender, args);
            }

            #endregion

            #region OnAfterSave

            private bool onAfterSaveIsRegistered = false;

            private EventHandler<MovieReview, EntityEventArgs> onAfterSave;
            public event EventHandler<MovieReview, EntityEventArgs> OnAfterSave
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
                EventHandler<MovieReview, EntityEventArgs> handler = onAfterSave;
                if (handler is not null)
                    handler.Invoke((MovieReview)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

                #region OnUid

                private static bool onUidIsRegistered = false;

                private static EventHandler<MovieReview, PropertyEventArgs> onUid;
                public static event EventHandler<MovieReview, PropertyEventArgs> OnUid
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
                    EventHandler<MovieReview, PropertyEventArgs> handler = onUid;
                    if (handler is not null)
                        handler.Invoke((MovieReview)sender, args);
                }

                #endregion

                #region OnReview

                private static bool onReviewIsRegistered = false;

                private static EventHandler<MovieReview, PropertyEventArgs> onReview;
                public static event EventHandler<MovieReview, PropertyEventArgs> OnReview
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onReviewIsRegistered)
                            {
                                Members.Review.Events.OnChange -= onReviewProxy;
                                Members.Review.Events.OnChange += onReviewProxy;
                                onReviewIsRegistered = true;
                            }
                            onReview += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onReview -= value;
                            if (onReview is null && onReviewIsRegistered)
                            {
                                Members.Review.Events.OnChange -= onReviewProxy;
                                onReviewIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onReviewProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<MovieReview, PropertyEventArgs> handler = onReview;
                    if (handler is not null)
                        handler.Invoke((MovieReview)sender, args);
                }

                #endregion

                #region OnRating

                private static bool onRatingIsRegistered = false;

                private static EventHandler<MovieReview, PropertyEventArgs> onRating;
                public static event EventHandler<MovieReview, PropertyEventArgs> OnRating
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onRatingIsRegistered)
                            {
                                Members.Rating.Events.OnChange -= onRatingProxy;
                                Members.Rating.Events.OnChange += onRatingProxy;
                                onRatingIsRegistered = true;
                            }
                            onRating += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onRating -= value;
                            if (onRating is null && onRatingIsRegistered)
                            {
                                Members.Rating.Events.OnChange -= onRatingProxy;
                                onRatingIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onRatingProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<MovieReview, PropertyEventArgs> handler = onRating;
                    if (handler is not null)
                        handler.Invoke((MovieReview)sender, args);
                }

                #endregion

                #region OnMovie

                private static bool onMovieIsRegistered = false;

                private static EventHandler<MovieReview, PropertyEventArgs> onMovie;
                public static event EventHandler<MovieReview, PropertyEventArgs> OnMovie
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
                    EventHandler<MovieReview, PropertyEventArgs> handler = onMovie;
                    if (handler is not null)
                        handler.Invoke((MovieReview)sender, args);
                }

                #endregion

            }

            #endregion
        }

        #endregion

        #region IMovieReviewOriginalData

        public IMovieReviewOriginalData OriginalVersion { get { return this; } }

        #region Members for interface IMovieReview

        string IMovieReviewOriginalData.Uid { get { return OriginalData.Uid; } }
        string IMovieReviewOriginalData.Review { get { return OriginalData.Review; } }
        decimal? IMovieReviewOriginalData.Rating { get { return OriginalData.Rating; } }
        Movie IMovieReviewOriginalData.Movie { get { return ((ILookupHelper<Movie>)OriginalData.Movie).GetOriginalItem(null); } }

        #endregion
        #endregion
    }
}