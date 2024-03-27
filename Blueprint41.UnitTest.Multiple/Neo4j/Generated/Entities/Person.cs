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
    public interface IPersonOriginalData
    {
        string name { get; }
        int? born { get; }
        string Uid { get; }
        IEnumerable<Movie> ActedMovies { get; }
    }

    public partial class Person : OGM<Person, Person.PersonData, System.String>, IPersonOriginalData
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

            #region LoadByname

            RegisterQuery(nameof(LoadByname), (query, alias) => query.
                Where(alias.name == Parameter.New<string>(Param0)));

            #endregion

            #region LoadByUid

            RegisterQuery(nameof(LoadByUid), (query, alias) => query.
                Where(alias.Uid == Parameter.New<string>(Param0)));

            #endregion

            AdditionalGeneratedStoredQueries();
        }
        public static Person LoadByname(string name)
        {
            return FromQuery(nameof(LoadByname), new Parameter(Param0, name)).FirstOrDefault();
        }
        public static Person LoadByUid(string uid)
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

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Person.Alias(out alias, "node"));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

            RegisterQuery(name, compiled);
        }

        public override string ToString()
        {
            return $"Person => name : {this.name?.ToString() ?? "null"}, born : {this.born?.ToString() ?? "null"}, Uid : {this.Uid}";
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
                name = data.name;
                born = data.born;
                Uid = data.Uid;
                ActedMovies = data.ActedMovies;
            }


            #region Initialize Collections

            protected override void InitializeCollections()
            {
                NodeType = "Person";

                ActedMovies = new EntityCollection<Movie>(Wrapper, Members.ActedMovies, item => { if (Members.ActedMovies.Events.HasRegisteredChangeHandlers) { int loadHack = item.Actors.Count; } });
            }
            public string NodeType { get; private set; }
            sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
            sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

            #endregion
            #region Map Data

            sealed public override IDictionary<string, object> MapTo()
            {
                IDictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("name",  name);
                dictionary.Add("born",  Conversion<int?, long?>.Convert(born));
                dictionary.Add("Uid",  Uid);
                return dictionary;
            }

            sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
            {
                object value;
                if (properties.TryGetValue("name", out value))
                    name = (string)value;
                if (properties.TryGetValue("born", out value))
                    born = Conversion<long, int>.Convert((long)value);
                if (properties.TryGetValue("Uid", out value))
                    Uid = (string)value;
            }

            #endregion

            #region Members for interface IPerson

            public string name { get; set; }
            public int? born { get; set; }
            public string Uid { get; set; }
            public EntityCollection<Movie> ActedMovies { get; private set; }

            #endregion
        }

        #endregion

        #region Outer Data

        #region Members for interface IPerson

        public string name { get { LazyGet(); return InnerData.name; } set { if (LazySet(Members.name, InnerData.name, value)) InnerData.name = value; } }
        public int? born { get { LazyGet(); return InnerData.born; } set { if (LazySet(Members.born, InnerData.born, value)) InnerData.born = value; } }
        public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }
        public EntityCollection<Movie> ActedMovies { get { return InnerData.ActedMovies; } }
        private void ClearActedMovies(DateTime? moment)
        {
            ((ILookupHelper<Movie>)InnerData.ActedMovies).ClearLookup(moment);
        }

        #endregion

        #region Virtual Node Type
        
        public string NodeType  { get { return InnerData.NodeType; } }
        
        #endregion

        #endregion

        #region Relationship Properties

        #region ActedMovies (Collection)

        public List<ACTED_IN> ActedMovieRelations()
        {
            return ACTED_IN.Load(_queryActedMovieRelations.Value, ("key", Uid));
        }
        private readonly Lazy<ICompiled> _queryActedMovieRelations = new Lazy<ICompiled>(delegate()
        {
            return Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.ACTED_IN.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == key)
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();
        });
        public List<ACTED_IN> ActedMoviesWhere(Func<ACTED_IN.Alias, QueryCondition> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.ACTED_IN.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new ACTED_IN.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return ACTED_IN.Load(query);
        }
        public List<ACTED_IN> ActedMoviesWhere(Func<ACTED_IN.Alias, QueryCondition[]> expression)
        {
            var query = Transaction.CompiledQuery
                .Match(node.Person.Alias(out var inAlias).In.ACTED_IN.Alias(out var relAlias).Out.Movie.Alias(out var outAlias))
                .Where(inAlias.Uid == Uid)
                .And(expression.Invoke(new ACTED_IN.Alias(relAlias, inAlias, outAlias)))
                .Return(relAlias.ElementId.As("elementId"), relAlias.Properties("properties"), inAlias.As("in"), outAlias.As("out"))
                .Compile();

            return ACTED_IN.Load(query);
        }
        public List<ACTED_IN> ActedMoviesWhere(JsNotation<System.DateTime?> CreationDate = default, JsNotation<string[]> roles = default)
        {
            return ActedMoviesWhere(delegate(ACTED_IN.Alias alias)
            {
                List<QueryCondition> conditions = new List<QueryCondition>();

                if (CreationDate.HasValue) conditions.Add(alias.CreationDate == CreationDate.Value);
                if (roles.HasValue) conditions.Add(alias.roles == roles.Value);

                return conditions.ToArray();
            });
        }
        public void AddActedMovie(Movie movie, JsNotation<string[]> roles = default)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            if (roles.HasValue) properties.Add("roles", roles.Value);
            ((ILookupHelper<Movie>)InnerData.ActedMovies).AddItem(movie, null, properties);
        }
        public void RemoveActedMovie(Movie movie)
        {
            ActedMovies.Remove(movie);
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

            public EntityProperty name { get; } = Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Person"].Properties["name"];
            public EntityProperty born { get; } = Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Person"].Properties["born"];
            public EntityProperty Uid { get; } = Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Person"].Properties["Uid"];
            public EntityProperty ActedMovies { get; } = Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Person"].Properties["ActedMovies"];
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
                        entity = Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Person"];
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

                #region Onname

                private static bool onnameIsRegistered = false;

                private static EventHandler<Person, PropertyEventArgs> onname;
                public static event EventHandler<Person, PropertyEventArgs> Onname
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onnameIsRegistered)
                            {
                                Members.name.Events.OnChange -= onnameProxy;
                                Members.name.Events.OnChange += onnameProxy;
                                onnameIsRegistered = true;
                            }
                            onname += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onname -= value;
                            if (onname is null && onnameIsRegistered)
                            {
                                Members.name.Events.OnChange -= onnameProxy;
                                onnameIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onnameProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Person, PropertyEventArgs> handler = onname;
                    if (handler is not null)
                        handler.Invoke((Person)sender, args);
                }

                #endregion

                #region Onborn

                private static bool onbornIsRegistered = false;

                private static EventHandler<Person, PropertyEventArgs> onborn;
                public static event EventHandler<Person, PropertyEventArgs> Onborn
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onbornIsRegistered)
                            {
                                Members.born.Events.OnChange -= onbornProxy;
                                Members.born.Events.OnChange += onbornProxy;
                                onbornIsRegistered = true;
                            }
                            onborn += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onborn -= value;
                            if (onborn is null && onbornIsRegistered)
                            {
                                Members.born.Events.OnChange -= onbornProxy;
                                onbornIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onbornProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Person, PropertyEventArgs> handler = onborn;
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

                #region OnActedMovies

                private static bool onActedMoviesIsRegistered = false;

                private static EventHandler<Person, PropertyEventArgs> onActedMovies;
                public static event EventHandler<Person, PropertyEventArgs> OnActedMovies
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onActedMoviesIsRegistered)
                            {
                                Members.ActedMovies.Events.OnChange -= onActedMoviesProxy;
                                Members.ActedMovies.Events.OnChange += onActedMoviesProxy;
                                onActedMoviesIsRegistered = true;
                            }
                            onActedMovies += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onActedMovies -= value;
                            if (onActedMovies is null && onActedMoviesIsRegistered)
                            {
                                Members.ActedMovies.Events.OnChange -= onActedMoviesProxy;
                                onActedMoviesIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onActedMoviesProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<Person, PropertyEventArgs> handler = onActedMovies;
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

        string IPersonOriginalData.name { get { return OriginalData.name; } }
        int? IPersonOriginalData.born { get { return OriginalData.born; } }
        string IPersonOriginalData.Uid { get { return OriginalData.Uid; } }
        IEnumerable<Movie> IPersonOriginalData.ActedMovies { get { return OriginalData.ActedMovies.OriginalData; } }

        #endregion
        #endregion
    }
}