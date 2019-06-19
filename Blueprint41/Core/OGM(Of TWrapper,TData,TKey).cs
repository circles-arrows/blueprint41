using Blueprint41.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = Blueprint41.Neo4j.Model;
using persistence = Blueprint41.Neo4j.Persistence;

namespace Blueprint41.Core
{
    public abstract class OGM<TWrapper, TKey> : OGMImpl
        where TWrapper : OGM<TWrapper, TKey>, new()
    {
        protected OGM() : base() { }

        public static TWrapper Load(TKey key)
        {
            if (key == null)
                return null;

            TWrapper item = Lookup(key);
            item.LazyGet();

            if (item.PersistenceState != PersistenceState.New && item.PersistenceState != PersistenceState.DoesntExist)
                return item;
            else
                return null;
        }
        public static TWrapper Lookup(TKey key)
        {
            if (key == null)
                return null;

            TWrapper instance = (TWrapper)Transaction.RunningTransaction.GetEntityByKey(Entity.Name, key);
            if (instance != null)
                return instance;

            TWrapper item = Transaction.Execute(() => new TWrapper(), EventOptions.GraphEvents);
            item.SetKey(key);

            return item;
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

        protected static Entity entity = null;
        public static Entity Entity
        {

            get
            {
                if (entity == null)
                    Instance.GetEntity();

                return entity;
            }
        }

        internal static readonly TWrapper Instance = (TWrapper)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(TWrapper));

        internal protected override void LazyGet()
        {
            switch (PersistenceState)
            {
                case PersistenceState.New:
                case PersistenceState.NewAndChanged:
                    break;
                case PersistenceState.HasUid:
                    PersistenceProvider.NodePersistenceProvider.Load(this);
                    break;
                case PersistenceState.Loaded:
                case PersistenceState.LoadedAndChanged:
                case PersistenceState.OutOfScope:
                case PersistenceState.Persisted:
                case PersistenceState.Delete:
                case PersistenceState.ForceDelete:
                    break;
                case PersistenceState.Deleted:
                    throw new InvalidOperationException("The object has been deleted, you cannot make changes to it anymore.");
                case PersistenceState.DoesntExist:
                    throw new InvalidOperationException($"{GetEntity().Name} with key {GetKey().ToString()} couldn't be loaded from the database.");
                case PersistenceState.Error:
                    throw new InvalidOperationException("The object suffered an unexpected failure.");
                default:
                    throw new NotImplementedException(string.Format("The PersistenceState '{0}' is not yet implemented.", PersistenceState.ToString()));
            }
        }
        internal protected override void LazySet()
        {
            if (PersistenceState == PersistenceState.Persisted && DbTransaction != Transaction.RunningTransaction)
                    throw new InvalidOperationException("This object was already flushed to the data store.");
            else if (PersistenceState == PersistenceState.OutOfScope)
                throw new InvalidOperationException("The transaction for this object has already ended.");

            LazyGet();

            if (PersistenceState == PersistenceState.New)
                PersistenceState = PersistenceState.NewAndChanged;
            else if (PersistenceState == PersistenceState.Loaded)
                PersistenceState = PersistenceState.LoadedAndChanged;
        }
        internal protected override bool LazySet<T>(Property property, T previousValue, T assignValue, DateTime? moment = null)
        {
            if (previousValue == null && assignValue == null)
                return false;

            if (previousValue != null && previousValue.Equals(assignValue))
                return false;

            if (property.PropertyType == PropertyType.Attribute && previousValue is IList)
            {
                IList pv = previousValue as IList;
                IList av = assignValue as IList;
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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            TWrapper other = obj as TWrapper;
            if ((object)other == null)
                return false;

            object key = GetKey();
            if (key == null)
                return object.ReferenceEquals(this, other);

            object otherKey = other.GetKey();
            if (otherKey == null)
                return object.ReferenceEquals(this, other);

            return key.Equals(otherKey);
        }

        public static bool operator ==(OGM<TWrapper, TKey> a, OGM<TWrapper, TKey> b)
        {
            if ((object)a == null && (object)b == null)
                return true;

            if ((object)a == null || (object)b == null)
                return false;

            object ka = a.GetKey();
            object kb = b.GetKey();

            if ((object)ka == null && (object)kb == null)
                return true;

            if ((object)ka == null || (object)kb == null)
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
            OriginalData = InnerData = new TData();
            InnerData.Initialize(this);
            PersistenceState = PersistenceState.New;
            GetEntity().RaiseOnNew(this, Transaction.RunningTransaction);
        }

        sealed internal override void SetKey(TKey key)
        {
            InnerData.SetKey(key);
        }
        
        public static void Delete(TKey key)
        {
            TWrapper wrapper = Lookup(key);
            wrapper.Delete();
        }

        public static void ForceDelete(TKey key)
        {
            TWrapper wrapper = Lookup(key);
            wrapper.ForceDelete();
        }

        internal protected TData InnerData = default(TData);
        internal protected TData OriginalData = default(TData);
        TData IInnerData<TData>.InnerData { get { return InnerData; } }

        public override PersistenceState PersistenceState
        {
            get
            {
                return InnerData.PersistenceState;
            }
            internal set
            {
                InnerData.PersistenceState = value;
            }
        }

        internal override object GetKey()
        {
            TData data = InnerData;
            if (data == null)
                return null;

            return data.GetKey();
        }

        sealed internal override Data GetData()
        {
            return InnerData;
        }
        //internal TData GetData()
        //{
        //    return Current ?? Loaded;
        //}
        //sealed internal override Data NewData(object key = null)
        //{
        //    return NewData((TKey)key);
        //}
    //    internal TData NewData(TKey key)
    //    {
    //        TData item = NewData();

    //        if (key != null && !key.Equals(default(TKey)))
				//item.SetKey(key);

    //        return item;
    //    }
        
        protected void KeySet(Action set)
        {
            if (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged)
                throw new InvalidOperationException("You cannot set the key unless if the object is a newly created one.");

            if (InnerData.HasKey)
                throw new InvalidOperationException("You cannot set the key multiple times.");

            if (set != null)
                set.Invoke();

            DbTransaction.Register(Entity.Name, this);
        }

        #region Stored Queries

        private static IDictionary<string, ICompiled> StoredQueries = null;
        private static bool IsInitialized = false;

        protected virtual void RegisterStoredQueries() { }
        protected abstract void RegisterGeneratedStoredQueries();

        public static void RegisterQuery(string name, ICompiled query)
        {
            InitializeStoredQueries();

            StoredQueries.Add(name, query);
        }
        public static List<TWrapper> FromQuery(string name, params Parameter[] parameters)
        {
            InitializeStoredQueries();

            ICompiled query = StoredQueries[name];
            return LoadWhere(query, parameters);
        }
        public static List<TWrapper> FromQuery(string name, Parameter[] parameters, int page, int size, bool ascending = true, params Property[] orderBy)
        {
            InitializeStoredQueries();

            ICompiled query = StoredQueries[name];
            return LoadWhere(query, parameters, page, size, ascending, orderBy);
        }

        private static void InitializeStoredQueries()
        {
            if (StoredQueries != null && IsInitialized)
                return;

            lock (typeof(TWrapper))
            {
                if (StoredQueries == null)
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

    public interface IInnerData<TData>
    {
        TData InnerData { get; }
    }
}
