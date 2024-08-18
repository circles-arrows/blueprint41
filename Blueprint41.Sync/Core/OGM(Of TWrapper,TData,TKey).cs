using Blueprint41.Sync.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = Blueprint41.Sync.Neo4j.Model;
using persistence = Blueprint41.Sync.Neo4j.Persistence;

namespace Blueprint41.Sync.Core
{
    public abstract class OGM<TWrapper, TKey> : OGMImpl
        where TWrapper : OGM<TWrapper, TKey>, new()
    {
        protected OGM() : base() { }

        public static TWrapper? Load(TKey key) => Load(key, false);
        public static TWrapper? Load(TKey key, bool locked)
        {
            if (key is null && locked)
                throw new ArgumentNullException(nameof(key), "The key cannot be null when trying to acquire a lock.");

            TWrapper? item = Lookup(key);
            if (item is null)
                return null;

            if (locked || item.PersistenceState != PersistenceState.DoesntExist)
                item.LazyGet(locked);

            if (item.PersistenceState != PersistenceState.New && item.PersistenceState != PersistenceState.DoesntExist)
                return item;
            else
                return null;
        }
        public static TWrapper? Lookup(TKey key)
        {
            if (key is null)
                return null;

            TWrapper? instance = (TWrapper?)Transaction.RunningTransaction.GetEntityByKey(Entity.Name, key);
            if (instance is not null)
                return instance;

            TWrapper item = Transaction.Execute(() => new TWrapper(), EventOptions.GraphEvents);
            item.SetKey(key);

            return item;
        }

        internal static OGM? Map(RawNode node, string cypher, Dictionary<string, object?>? parameters, NodeMapping mappingMode)
        {
            object? key = null;
            if (!node.Properties.TryGetValue(Entity.Key.Name, out key))
                throw new ArgumentException($"The node does not contain key '{Entity.Key.Name}' for entity '{Entity.Name}'.");

            if (key is null)
                throw new ArgumentException($"The node contains null key '{Entity.Key.Name}' for entity '{Entity.Name}'.");

            Transaction trans = Transaction.RunningTransaction;

            TWrapper? instance = (TWrapper?)trans.GetEntityByKey(Entity.Name, key);
            if (!(instance is null))
                return instance;

            instance = Transaction.Execute(() => new TWrapper(), EventOptions.GraphEvents);
            instance.SetKey((TKey)key);
            OGM ogm = instance as OGM;

            Dictionary<string, object?> properties = (Dictionary<string, object?>)node.Properties;
            if (cypher is not null)
            {
                Dictionary<string, object?>? customState = null;
                NodeEventArgs loadingArgs = Entity.RaiseOnNodeLoading(trans, ogm, cypher, parameters, ref customState);
                NodeEventArgs args = Entity.RaiseOnNodeLoaded(trans, loadingArgs, node.Id, node.Labels, properties);
                properties = args.Properties!;
            }

            if (instance.PersistenceState == PersistenceState.HasUid || instance.PersistenceState == PersistenceState.Loaded)
            {
                ogm.SetData(properties);
                instance.PersistenceState = (mappingMode == NodeMapping.AsWritableEntity) ? PersistenceState.Loaded : PersistenceState.OutOfScope;
            }

            return instance;
        }

        public static List<TWrapper> GetAll()
        {
            return Transaction.RunningTransaction.NodePersistenceProvider.GetAll<TWrapper>(Entity);
        }
        public static List<TWrapper> GetAll(int page, int pageSize, params Property[] orderBy)
        {
            return GetAll(page, pageSize, true, orderBy);
        }
        public static List<TWrapper> GetAll(int page, int pageSize, bool ascending = true, params Property[] orderBy)
        {
            return Transaction.RunningTransaction.NodePersistenceProvider.GetAll<TWrapper>(Entity, page, pageSize, ascending, orderBy);
        }

        [Obsolete("This method will be made internal in the next release.", false)]
        public static List<TWrapper> LoadWhere(string conditions, Parameter[] parameters)
        {
            return Transaction.RunningTransaction.NodePersistenceProvider.LoadWhere<TWrapper>(Entity, conditions, parameters, 0, 0);
        }
        [Obsolete("This method will be made internal in the next release.", false)]
        public static List<TWrapper> LoadWhere(string conditions, Parameter[] parameters, int page, int pageSize, params Property[] orderBy)
        {
            return LoadWhere(conditions, parameters, page, pageSize, true, orderBy);
        }
        [Obsolete("This method will be made internal in the next release.", false)]
        public static List<TWrapper> LoadWhere(string conditions, Parameter[] parameters, int page, int pageSize, bool ascending = true, params Property[] orderBy)
        {
            return Transaction.RunningTransaction.NodePersistenceProvider.LoadWhere<TWrapper>(Entity, conditions, parameters, page, pageSize, ascending, orderBy);
        }
        public static List<TWrapper> LoadWhere(ICompiled query)
        {
            return LoadWhere(query, new Parameter[0]);
        }
        public static List<TWrapper> LoadWhere(ICompiled query, params Parameter[] parameters)
        {
            return Transaction.RunningTransaction.NodePersistenceProvider.LoadWhere<TWrapper>(Entity, query, parameters);
        }
        public static List<TWrapper> LoadWhere(ICompiled query, Parameter[] parameters, int page, int pageSize, params Property[] orderBy)
        {
            return LoadWhere(query, parameters, page, pageSize, true, orderBy);
        }
        public static List<TWrapper> LoadWhere(ICompiled query, Parameter[] parameters, int page, int pageSize, bool ascending = true, params Property[] orderBy)
        {
            return Transaction.RunningTransaction.NodePersistenceProvider.LoadWhere<TWrapper>(Entity, query, parameters, page, pageSize, ascending, orderBy);
        }
        public static List<TWrapper> Search(string text, params Property[] properties)
        {
            return Transaction.RunningTransaction.NodePersistenceProvider.Search<TWrapper>(Entity, text, properties);
        }
        public static List<TWrapper> Search(string text, int page = 0, int pageSize = 0, params Property[] properties)
        {
            return Search(text, page, pageSize, true, properties);
        }
        public static List<TWrapper> Search(string text, int page = 0, int pageSize = 0, bool ascending = true, params Property[] properties)
        {
            return Transaction.RunningTransaction.NodePersistenceProvider.Search<TWrapper>(Entity, text, properties, page, pageSize, ascending, Entity.Key);
        }

        internal abstract void SetKey(TKey key);

        protected static Entity? entity = null;
        public static Entity Entity
        {

            get
            {
                if (entity is null)
                    Instance.GetEntity();

                return entity!;
            }
        }

        internal static readonly TWrapper Instance = (TWrapper)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(TWrapper));

        internal protected override void LazyGet(bool locked = false)
        {
            switch (PersistenceState)
            {
                case PersistenceState.New:
                case PersistenceState.NewAndChanged:
                    if (locked)
                        PersistenceProvider.NodePersistenceProvider.Load(this, locked);
                    break;
                case PersistenceState.HasUid:
                    PersistenceProvider.NodePersistenceProvider.Load(this, locked);
                    break;
                case PersistenceState.Loaded:
                case PersistenceState.LoadedAndChanged:
                case PersistenceState.OutOfScope:
                case PersistenceState.Persisted:
                case PersistenceState.Delete:
                case PersistenceState.ForceDelete:
                    if (locked)
                        PersistenceProvider.NodePersistenceProvider.Load(this, locked);
                    break;
                case PersistenceState.Deleted:
                    throw new InvalidOperationException("The object has been deleted, you cannot make changes to it anymore.");
                case PersistenceState.DoesntExist:
                    throw new InvalidOperationException($"{GetEntity().Name} with key {GetKey()?.ToString() ?? "<NULL>"} couldn't be loaded from the database.");
                case PersistenceState.Error:
                    throw new InvalidOperationException("The object suffered an unexpected failure.");
                default:
                    throw new NotImplementedException(string.Format("The PersistenceState '{0}' is not yet implemented.", PersistenceState.ToString()));
            }
        }
        internal protected override void LazySet()
        {
            if (PersistenceState == PersistenceState.OutOfScope)
                throw new InvalidOperationException("The transaction for this object has already ended.");

            LazyGet();

            if (PersistenceState == PersistenceState.New)
                PersistenceState = PersistenceState.NewAndChanged;
            else if (PersistenceState == PersistenceState.Loaded || PersistenceState == PersistenceState.Persisted)
                PersistenceState = PersistenceState.LoadedAndChanged;
        }
        internal protected bool LazySet<T>(Property property, T previousValue, T assignValue) => LazySet<T>(property, previousValue, assignValue, Transaction.RunningTransaction.TransactionDate);
        internal protected override bool LazySet<T>(Property property, T previousValue, T assignValue, DateTime? moment)
        {
            if (previousValue is null && assignValue is null)
                return false;

            if (previousValue is not null && previousValue.Equals(assignValue))
                return false;

            if (property.PropertyType == PropertyType.Attribute && previousValue is IList)
            {
                if (assignValue is null)
                    throw new InvalidOperationException("You cannot assign null to a list property.");

                IList pv = (IList)previousValue;
                IList av = (IList)assignValue;
                if (pv.Count == 0 && av.Count == 0)
                    return false;

                if(pv.Count == av.Count)
                {
                    bool equal = true;
                    foreach (var item in pv)
                    {
                        if (!av.Contains(item))
                            equal = false;
                    }

                    if (equal)
                        return false;
                }
            }

            if (base.LazySet(property, previousValue, assignValue, moment))
                return false;

            LazySet();
            return true;
        }
        internal protected override bool LazySet<T>(Property property, IEnumerable<CollectionItem<T>> previousValues, T assignValue, DateTime? moment)
        {
            if (!previousValues.Any() && assignValue is null)
                return false;

            if (previousValues.Take(2).Count() == 1 && previousValues.First().Item.Equals(assignValue) && (previousValues.First().StartDate.IsMin() || previousValues.First().StartDate <= moment) && previousValues.First().EndDate.IsMax())
                return false;

            if (property.PropertyType == PropertyType.Attribute)
                throw new NotSupportedException("Don't use this overload for attributes.");

            if (base.LazySet(property, previousValues!, assignValue, moment))
                return false;

            LazySet();
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            TWrapper? other = obj as TWrapper;
            if (other is null)
                return false;

            object? key = GetKey();
            if (key is null)
                return object.ReferenceEquals(this, other);

            object? otherKey = other.GetKey();
            if (otherKey is null)
                return object.ReferenceEquals(this, other);

            return key.Equals(otherKey);
        }

        public static bool operator ==(OGM<TWrapper, TKey> a, OGM<TWrapper, TKey> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            object? ka = a.GetKey();
            object? kb = b.GetKey();

            if (ka is null && kb is null)
                return true;

            if (ka is null || kb is null)
                return false;

            return ka.Equals(kb);

        }
        public static bool operator !=(OGM<TWrapper, TKey> a, OGM<TWrapper, TKey> b)
        {
            return !(a == b);
        }
    }

    public abstract class OGM<TWrapper, TData, TKey> : OGM<TWrapper, TKey>, IInnerData<TData>
        where TWrapper : OGM<TWrapper, TData, TKey>, new()
        where TData : Data<TKey>, new()
    {
        protected OGM() : base()
        {
            InnerData = new TData();
            InnerData.Initialize(this);
            PersistenceState = PersistenceState.New;
            GetEntity().RaiseOnNew(this, Transaction.RunningTransaction);
            OriginalData = InnerData;
        }

        sealed internal override void SetKey(TKey key)
        {
            InnerData.SetKey(key);
        }

        public static void Delete(TKey key)
        {
            TWrapper? wrapper = Lookup(key);
            wrapper?.Delete();
        }

        public static void ForceDelete(TKey key)
        {
            TWrapper? wrapper = Lookup(key);
            wrapper?.ForceDelete();
        }

        private TData? innerData = default(TData);
        internal protected TData InnerData
        {
            get
            {
                if (innerData is null)
                    throw new InvalidOperationException("The inner data is not yet initialized.");

                return innerData;
            }
            set { innerData = value; }
        }
        internal protected TData? OriginalData = default(TData);
        TData IInnerData<TData>.InnerData { get { return InnerData; } }
        TData IInnerData<TData>.OriginalData { get { return OriginalData ?? InnerData; } }
        Data IInnerData.InnerData => InnerData;
        Data IInnerData.OriginalData => OriginalData ?? InnerData;

        public override PersistenceState PersistenceState
        {
            get { return InnerData.PersistenceState; }
            internal set { InnerData.PersistenceState = value; }
        }
        public override PersistenceState OriginalPersistenceState
        {
            get { return InnerData.OriginalPersistenceState; }
            internal set { InnerData.OriginalPersistenceState = value; }
        }

        internal override object? GetKey()
        {
            TData? data = innerData;
            if (data is null)
                return null;

            return data.GetKey();
        }

        sealed internal override Data GetData()
        {
            return InnerData;
        }
        sealed internal protected override void AfterSetData()
        {
            OriginalData = InnerData;
        }
        //internal TData GetData()
        //{
        //    return Current ?? Loaded;
        //}
        //sealed internal override Data NewData(object key = null)
        //{
        //    return NewData((TKey)key);
        //}
        //internal TData NewData(TKey key)
        //{
        //	  TData item = NewData();

        //	  if (key is not null && !key.Equals(default(TKey)))
        //		  item.SetKey(key);

        //    return item;
        //}

        protected void KeySet(Action set)
        {
            if (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged)
                throw new InvalidOperationException("You cannot set the key unless if the object is a newly created one.");

            if (InnerData.HasKey)
                throw new InvalidOperationException("You cannot set the key multiple times.");

            if (set is not null)
                set.Invoke();

            RunningTransaction.Register(Entity.Name, this);
        }

        #region Stored Queries

        private static IDictionary<string, ICompiled>? StoredQueries = null;
        private static bool IsInitialized = false;

        protected virtual void RegisterStoredQueries() { }
        protected abstract void RegisterGeneratedStoredQueries();

        public static void RegisterQuery(string name, ICompiled query)
        {
            InitializeStoredQueries();

            StoredQueries!.Add(name, query);
        }
        public static List<TWrapper> FromQuery(string name, params Parameter[] parameters)
        {
            InitializeStoredQueries();

            ICompiled query = StoredQueries![name];
            return LoadWhere(query, parameters);
        }
        public static List<TWrapper> FromQuery(string name, Parameter[] parameters, int page, int size, bool ascending = true, params Property[] orderBy)
        {
            InitializeStoredQueries();

            ICompiled query = StoredQueries![name];
            return LoadWhere(query, parameters, page, size, ascending, orderBy);
        }

        private static void InitializeStoredQueries()
        {
            if (StoredQueries is not null && IsInitialized)
                return;

            lock (typeof(TWrapper))
            {
                if (StoredQueries is null)
                {
                    StoredQueries = new AtomicDictionary<string, ICompiled>();
                    Instance.RegisterGeneratedStoredQueries();
                    Instance.RegisterStoredQueries();
                    IsInitialized = true;
                }
            }
        }

        #endregion
    }

    public interface IInnerData
    {
        Data InnerData { get; }
        Data OriginalData { get; }
    }
    public interface IInnerData<TData> : IInnerData
    {
        new TData InnerData { get; }
        new TData OriginalData { get; }
    }
}
