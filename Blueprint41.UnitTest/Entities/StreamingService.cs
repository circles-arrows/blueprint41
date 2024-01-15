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
    public interface IStreamingServiceOriginalData : IBaseEntityOriginalData
    {
        string Name { get; }
        IEnumerable<Person> Subscribers { get; }
    }

    public partial class StreamingService : OGM<StreamingService, StreamingService.StreamingServiceData, System.String>, IBaseEntity, IStreamingServiceOriginalData
    {
        #region Initialize

        static StreamingService()
        {
            Register.Types();
        }


        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadByKeys
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

            #region LoadByName

            RegisterQuery(nameof(LoadByName), (query, alias) => query.
                Where(alias.Name == Parameter.New<string>(Param0)));

            #endregion

            AdditionalGeneratedStoredQueries();
        }
        public static StreamingService LoadByName(string name)
        {
            return FromQuery(nameof(LoadByName), new Parameter(Param0, name)).FirstOrDefault();
        }
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, StreamingService> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

        protected static void RegisterQuery(string name, Func<IMatchQuery, q.StreamingServiceAlias, IWhereQuery> query)
        {
            q.StreamingServiceAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.StreamingService.Alias(out alias, "node"));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

            RegisterQuery(name, compiled);
        }

        public override string ToString()
        {
            return $"StreamingService => Name : {this.Name}, Uid : {this.Uid}, LastModifiedOn : {this.LastModifiedOn}";
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
                    OriginalData = new StreamingServiceData(InnerData);
            }
        }


        #endregion

        #region Validations

        protected override void ValidateSave()
        {
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

            if (InnerData.Name is null)
                throw new PersistenceException(string.Format("Cannot save StreamingService with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
        }

        protected override void ValidateDelete()
        {
        }

        #endregion

        #region Inner Data

        public class StreamingServiceData : Data<System.String>
        {
            public StreamingServiceData()
            {

            }

            public StreamingServiceData(StreamingServiceData data)
            {
                Name = data.Name;
                Subscribers = data.Subscribers;
                Uid = data.Uid;
                LastModifiedOn = data.LastModifiedOn;
            }


            #region Initialize Collections

            protected override void InitializeCollections()
            {
                NodeType = "StreamingService";

                Subscribers = new EntityTimeCollection<Person>(Wrapper, Members.Subscribers, item => { if (Members.Subscribers.Events.HasRegisteredChangeHandlers) { int loadHack = item.StreamingServiceSubscriptions.CountAll; } });
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

            #region Members for interface IStreamingService

            public string Name { get; set; }
            public EntityTimeCollection<Person> Subscribers { get; private set; }

            #endregion
            #region Members for interface IBaseEntity

            public string Uid { get; set; }
            public System.DateTime LastModifiedOn { get; set; }

            #endregion
        }

        #endregion

        #region Outer Data

        #region Members for interface IStreamingService

        public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
        public EntityTimeCollection<Person> Subscribers { get { return InnerData.Subscribers; } }
        private void ClearSubscribers(DateTime? moment)
        {
            ((ILookupHelper<Person>)InnerData.Subscribers).ClearLookup(moment);
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

        public List<SUBSCRIBED_TO_STREAMING_SERVICE> SubscriberRelations()
        {
            throw new NotImplementedException();
        }
        public List<SUBSCRIBED_TO_STREAMING_SERVICE> SubscribersWhere(Func<SUBSCRIBED_TO_STREAMING_SERVICE_CRUD_ALIAS, QueryCondition> alias)
        {
            throw new NotImplementedException();
        }
        public List<SUBSCRIBED_TO_STREAMING_SERVICE> SubscribersWhere(Func<SUBSCRIBED_TO_STREAMING_SERVICE_CRUD_ALIAS, QueryCondition[]> alias)
        {
            throw new NotImplementedException();
        }
        public List<SUBSCRIBED_TO_STREAMING_SERVICE> SubscribersWhere(JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<decimal> MonthlyFee = default, JsNotation<System.DateTime> StartDate = default)
        {
            throw new NotImplementedException();
        }
        public void AddSubscriber(Person person, DateTime? moment, JsNotation<decimal> MonthlyFee = default)
        {
            if (moment is null)
                moment = DateTime.UtcNow;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            if (MonthlyFee.HasValue) properties.Add("MonthlyFee", MonthlyFee.Value);
            ((ILookupHelper<Person>)InnerData.Subscribers).AddItem(person, moment, properties);
        }
        public void RemoveSubscriber(Person person, DateTime? moment)
        {
            Subscribers.Remove(person, moment);
        }








































        #endregion

        #region Reflection

        private static StreamingServiceMembers members = null;
        public static StreamingServiceMembers Members
        {
            get
            {
                if (members is null)
                {
                    lock (typeof(StreamingService))
                    {
                        if (members is null)
                            members = new StreamingServiceMembers();
                    }
                }
                return members;
            }
        }
        public class StreamingServiceMembers
        {
            internal StreamingServiceMembers() { }

            #region Members for interface IStreamingService

            public Property Name { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["StreamingService"].Properties["Name"];
            public Property Subscribers { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["StreamingService"].Properties["Subscribers"];
            #endregion

            #region Members for interface IBaseEntity

            public Property Uid { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"];
            public Property LastModifiedOn { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"];
            #endregion

        }

        private static StreamingServiceFullTextMembers fullTextMembers = null;
        public static StreamingServiceFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers is null)
                {
                    lock (typeof(StreamingService))
                    {
                        if (fullTextMembers is null)
                            fullTextMembers = new StreamingServiceFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class StreamingServiceFullTextMembers
        {
            internal StreamingServiceFullTextMembers() { }

        }

        sealed public override Entity GetEntity()
        {
            if (entity is null)
            {
                lock (typeof(StreamingService))
                {
                    if (entity is null)
                        entity = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["StreamingService"];
                }
            }
            return entity;
        }

        private static StreamingServiceEvents events = null;
        public static StreamingServiceEvents Events
        {
            get
            {
                if (events is null)
                {
                    lock (typeof(StreamingService))
                    {
                        if (events is null)
                            events = new StreamingServiceEvents();
                    }
                }
                return events;
            }
        }
        public class StreamingServiceEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<StreamingService, EntityEventArgs> onNew;
            public event EventHandler<StreamingService, EntityEventArgs> OnNew
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
                EventHandler<StreamingService, EntityEventArgs> handler = onNew;
                if (handler is not null)
                    handler.Invoke((StreamingService)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<StreamingService, EntityEventArgs> onDelete;
            public event EventHandler<StreamingService, EntityEventArgs> OnDelete
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
                EventHandler<StreamingService, EntityEventArgs> handler = onDelete;
                if (handler is not null)
                    handler.Invoke((StreamingService)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<StreamingService, EntityEventArgs> onSave;
            public event EventHandler<StreamingService, EntityEventArgs> OnSave
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
                EventHandler<StreamingService, EntityEventArgs> handler = onSave;
                if (handler is not null)
                    handler.Invoke((StreamingService)sender, args);
            }

            #endregion

            #region OnAfterSave

            private bool onAfterSaveIsRegistered = false;

            private EventHandler<StreamingService, EntityEventArgs> onAfterSave;
            public event EventHandler<StreamingService, EntityEventArgs> OnAfterSave
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
                EventHandler<StreamingService, EntityEventArgs> handler = onAfterSave;
                if (handler is not null)
                    handler.Invoke((StreamingService)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

                #region OnName

                private static bool onNameIsRegistered = false;

                private static EventHandler<StreamingService, PropertyEventArgs> onName;
                public static event EventHandler<StreamingService, PropertyEventArgs> OnName
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
                    EventHandler<StreamingService, PropertyEventArgs> handler = onName;
                    if (handler is not null)
                        handler.Invoke((StreamingService)sender, args);
                }

                #endregion

                #region OnSubscribers

                private static bool onSubscribersIsRegistered = false;

                private static EventHandler<StreamingService, PropertyEventArgs> onSubscribers;
                public static event EventHandler<StreamingService, PropertyEventArgs> OnSubscribers
                {
                    add
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            if (!onSubscribersIsRegistered)
                            {
                                Members.Subscribers.Events.OnChange -= onSubscribersProxy;
                                Members.Subscribers.Events.OnChange += onSubscribersProxy;
                                onSubscribersIsRegistered = true;
                            }
                            onSubscribers += value;
                        }
                    }
                    remove
                    {
                        lock (typeof(OnPropertyChange))
                        {
                            onSubscribers -= value;
                            if (onSubscribers is null && onSubscribersIsRegistered)
                            {
                                Members.Subscribers.Events.OnChange -= onSubscribersProxy;
                                onSubscribersIsRegistered = false;
                            }
                        }
                    }
                }
            
                private static void onSubscribersProxy(object sender, PropertyEventArgs args)
                {
                    EventHandler<StreamingService, PropertyEventArgs> handler = onSubscribers;
                    if (handler is not null)
                        handler.Invoke((StreamingService)sender, args);
                }

                #endregion

                #region OnUid

                private static bool onUidIsRegistered = false;

                private static EventHandler<StreamingService, PropertyEventArgs> onUid;
                public static event EventHandler<StreamingService, PropertyEventArgs> OnUid
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
                    EventHandler<StreamingService, PropertyEventArgs> handler = onUid;
                    if (handler is not null)
                        handler.Invoke((StreamingService)sender, args);
                }

                #endregion

                #region OnLastModifiedOn

                private static bool onLastModifiedOnIsRegistered = false;

                private static EventHandler<StreamingService, PropertyEventArgs> onLastModifiedOn;
                public static event EventHandler<StreamingService, PropertyEventArgs> OnLastModifiedOn
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
                    EventHandler<StreamingService, PropertyEventArgs> handler = onLastModifiedOn;
                    if (handler is not null)
                        handler.Invoke((StreamingService)sender, args);
                }

                #endregion

            }

            #endregion
        }

        #endregion

        #region IStreamingServiceOriginalData

        public IStreamingServiceOriginalData OriginalVersion { get { return this; } }

        #region Members for interface IStreamingService

        string IStreamingServiceOriginalData.Name { get { return OriginalData.Name; } }
        IEnumerable<Person> IStreamingServiceOriginalData.Subscribers { get { return OriginalData.Subscribers.OriginalData; } }

        #endregion
        #region Members for interface IBaseEntity

        IBaseEntityOriginalData IBaseEntity.OriginalVersion { get { return this; } }

        string IBaseEntityOriginalData.Uid { get { return OriginalData.Uid; } }
        System.DateTime IBaseEntityOriginalData.LastModifiedOn { get { return OriginalData.LastModifiedOn; } }

        #endregion
        #endregion
    }
}