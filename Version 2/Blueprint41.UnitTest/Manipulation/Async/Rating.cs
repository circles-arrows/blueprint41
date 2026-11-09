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
    public interface IRatingOriginalData : IBaseEntityOriginalData
    {
        string Code { get; }
        string Name { get; }
        string Description { get; }
    }

    public partial class Rating : OGM<Rating, Rating.RatingData, System.String>, IBaseEntity, IRatingOriginalData
    {
        #region Initialize


        [Obsolete]
        static Rating()
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

            #region LoadByCode

            RegisterQuery(nameof(LoadByCodeAsync), (query, alias) => query.
                Where(alias.Code == Parameter.New<System.String>(Param0)));

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
        public async static Task<Rating> LoadByCodeAsync(System.String code)
        {
            return (await FromQueryAsync(nameof(LoadByCodeAsync), new Parameter(Param0, code)).ConfigureAwait(false)).FirstOrDefault();
        }
        public async static Task<Rating> LoadByNameAsync(System.String name)
        {
            return (await FromQueryAsync(nameof(LoadByNameAsync), new Parameter(Param0, name)).ConfigureAwait(false)).FirstOrDefault();
        }
        public async static Task<Rating> LoadByUidAsync(System.String uid)
        {
            return (await FromQueryAsync(nameof(LoadByUidAsync), new Parameter(Param0, uid)).ConfigureAwait(false)).FirstOrDefault();
        }
        partial void AdditionalGeneratedStoredQueries();

        public static async Task<Dictionary<System.String, Rating>> LoadByKeysAsync(IEnumerable<System.String> uids)
        {
            return (await FromQueryAsync(nameof(LoadByKeysAsync), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ConfigureAwait(false)).ToDictionary(item=> item.Uid, item => item);
        }

        protected static void RegisterQuery(string name, Func<IMatchQuery, q.RatingAlias, IWhereQuery> query)
        {
            q.RatingAlias alias;

            IMatchQuery matchQuery = Cypher.Match(q.Node.Rating.Alias(out alias, "node"));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

            RegisterQuery(name, compiled);
        }

        public override string ToString()
        {
            return $"Rating => Code : {this.Code}, Name : {this.Name}, Description : {this.Description}, Uid : {this.Uid}, LastModifiedOn : {this.LastModifiedOn}";
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
                    OriginalData = new RatingData(InnerData);
            }
        }


        #endregion

        #region Validations

        protected override void ValidateSave()
        {
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

            if (InnerData.Code is null)
                throw new PersistenceException(string.Format("Cannot save Rating with key '{0}' because the Code cannot be null.", this.Uid?.ToString() ?? "<null>"));
            if (InnerData.Name is null)
                throw new PersistenceException(string.Format("Cannot save Rating with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
            if (InnerData.Description is null)
                throw new PersistenceException(string.Format("Cannot save Rating with key '{0}' because the Description cannot be null.", this.Uid?.ToString() ?? "<null>"));
        }

        protected override void ValidateDelete()
        {
        }

        #endregion

        #region Inner Data

        public class RatingData : Data<System.String>
        {
            public RatingData()
            {

            }

            public RatingData(RatingData data)
            {
                Code = data.Code;
                Name = data.Name;
                Description = data.Description;
                Uid = data.Uid;
                LastModifiedOn = data.LastModifiedOn;
            }


            #region Initialize Collections

            protected override void InitializeCollections()
            {
                NodeType = "Rating";

            }
            public string NodeType { get; private set; }
            sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
            sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

            #endregion
            #region Map Data

            sealed public override IDictionary<string, object> MapTo()
            {
                IDictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("Code",  Code);
                dictionary.Add("Name",  Name);
                dictionary.Add("Description",  Description);
                dictionary.Add("Uid",  Uid);
                dictionary.Add("LastModifiedOn",  Conversion<System.DateTime, long>.Convert(LastModifiedOn));
                return dictionary;
            }

            sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
            {
                object value;
                if (properties.TryGetValue("Code", out value))
                    Code = (string)value;
                if (properties.TryGetValue("Name", out value))
                    Name = (string)value;
                if (properties.TryGetValue("Description", out value))
                    Description = (string)value;
                if (properties.TryGetValue("Uid", out value))
                    Uid = (string)value;
                if (properties.TryGetValue("LastModifiedOn", out value))
                    LastModifiedOn = Conversion<long, System.DateTime>.Convert((long)value);
            }

            #endregion

            #region Members for interface IRating

            public string Code { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

            #endregion
            #region Members for interface IBaseEntity

            public string Uid { get; set; }
            public System.DateTime LastModifiedOn { get; set; }

            #endregion
        }

        #endregion

        #region Outer Data

        #region Members for interface IRating

        public string Code { get { LazyGet(); return InnerData.Code; } set { if (LazySet(Members.Code, InnerData.Code, value)) InnerData.Code = value; } }
        public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
        public string Description { get { LazyGet(); return InnerData.Description; } set { if (LazySet(Members.Description, InnerData.Description, value)) InnerData.Description = value; } }

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

        private static readonly Parameter key = Parameter.New<string>("key");
        private static readonly Parameter moment = Parameter.New<DateTime>("moment");

        #endregion

        #region Reflection

        private static RatingMembers members = null;
        public static RatingMembers Members
        {
            get
            {
                if (members is null)
                {
                    lock (typeof(Rating))
                    {
                        if (members is null)
                            members = new RatingMembers();
                    }
                }
                return members;
            }
        }
        public class RatingMembers
        {
            internal RatingMembers() { }

            #region Members for interface IRating

            public EntityProperty Code { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Rating"].Properties["Code"];
            public EntityProperty Name { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Rating"].Properties["Name"];
            public EntityProperty Description { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Rating"].Properties["Description"];
            #endregion

            #region Members for interface IBaseEntity

            public EntityProperty Uid { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"];
            public EntityProperty LastModifiedOn { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"];
            #endregion

        }

        private static RatingFullTextMembers fullTextMembers = null;
        public static RatingFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers is null)
                {
                    lock (typeof(Rating))
                    {
                        if (fullTextMembers is null)
                            fullTextMembers = new RatingFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class RatingFullTextMembers
        {
            internal RatingFullTextMembers() { }

        }

        sealed protected override Entity GetEntity()
        {
            if (entity is null)
            {
                lock (typeof(Rating))
                {
                    if (entity is null)
                        entity = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Rating"];
                }
            }
            return entity;
        }
        public override EntityFlavor Flavor => EntityFlavor.Async;

        private static RatingEvents events = null;
        public static RatingEvents Events
        {
            get
            {
                if (events is null)
                {
                    lock (typeof(Rating))
                    {
                        if (events is null)
                            events = new RatingEvents();
                    }
                }
                return events;
            }
        }
        public class RatingEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Rating, EntityEventArgs> onNew;
            public event EventHandler<Rating, EntityEventArgs> OnNew
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
                EventHandler<Rating, EntityEventArgs> handler = onNew;
                if (handler is not null)
                    handler.Invoke((Rating)sender, args);
            }

            #endregion

        }

        #endregion

        #region IRatingOriginalData

        public IRatingOriginalData OriginalVersion { get { return this; } }

        #region Members for interface IRating

        string IRatingOriginalData.Code { get { return OriginalData.Code; } }
        string IRatingOriginalData.Name { get { return OriginalData.Name; } }
        string IRatingOriginalData.Description { get { return OriginalData.Description; } }

        #endregion
        #region Members for interface IBaseEntity

        IBaseEntityOriginalData IBaseEntity.OriginalVersion { get { return this; } }

        string IBaseEntityOriginalData.Uid { get { return OriginalData.Uid; } }
        System.DateTime IBaseEntityOriginalData.LastModifiedOn { get { return OriginalData.LastModifiedOn; } }

        #endregion
        #endregion
    }
}
