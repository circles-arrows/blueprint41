using Blueprint41.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    public abstract class OGMAbstractImpl<TSelf, TInterface, TKey>
        where TSelf : OGMAbstractImpl<TSelf, TInterface, TKey>
        where TInterface : OGM
    {
        protected const string Param0 = "P0";
        protected const string Param1 = "P1";
        protected const string Param2 = "P2";
        protected const string Param3 = "P3";
        protected const string Param4 = "P4";
        protected const string Param5 = "P5";
        protected const string Param6 = "P6";
        protected const string Param7 = "P7";
        protected const string Param8 = "P8";
        protected const string Param9 = "P9";

        public static TInterface Load(TKey key)
        {
            if (Entity.Key.IndexType != IndexType.Unique)
                throw new NotSupportedException("The key is not marked unique for the abstract base type, please load the entity via its concrete sub type instead.");

            if (key == null)
                return default(TInterface);

            return Transaction.RunningTransaction.NodePersistenceProvider.LoadWhere<TInterface>(Entity, string.Format("{{0}}.{0} = {{{{key}}}}", Entity.Key.Name), new Parameter[] { new Parameter("key", key) }).FirstOrDefault();
        }

        public static List<TInterface> GetAll()
        {
            return Transaction.RunningTransaction.NodePersistenceProvider.GetAll<TInterface>(Entity);
        }
        public static List<TInterface> GetAll(int page, int pageSize, params Property[] orderBy)
        {
            return Transaction.RunningTransaction.NodePersistenceProvider.GetAll<TInterface>(Entity, page, pageSize, orderBy);
        }

        [Obsolete("This method will be made internal in the next release.", false)]
        public static List<TInterface> LoadWhere(string conditions, Parameter[] parameters)
        {
            return Transaction.RunningTransaction.NodePersistenceProvider.LoadWhere<TInterface>(Entity, conditions, parameters, 0, 0);
        }
        [Obsolete("This method will be made internal in the next release.", false)]
        public static List<TInterface> LoadWhere(string conditions, Parameter[] parameters, int page, int pageSize, params Property[] orderBy)
        {
            return Transaction.RunningTransaction.NodePersistenceProvider.LoadWhere<TInterface>(Entity, conditions, parameters, page, pageSize, orderBy);
        }
        public static List<TInterface> LoadWhere(ICompiled query)
        {
            return LoadWhere(query, new Parameter[0]);
        }
        public static List<TInterface> LoadWhere(ICompiled query, params Parameter[] parameters)
        {
            return Transaction.RunningTransaction.NodePersistenceProvider.LoadWhere<TInterface>(Entity, query, parameters);
        }

        public static List<TInterface> Search(string text, params Property[] properties)
        {
            return Transaction.RunningTransaction.NodePersistenceProvider.Search<TInterface>(Entity, text, properties);
        }
        public static List<TInterface> Search(string text, int page = 0, int pageSize = 0, params Property[] properties)
        {
            return Transaction.RunningTransaction.NodePersistenceProvider.Search<TInterface>(Entity, text, properties, page, pageSize, Entity.Key);
        }

        public static void ForceDelete(TKey key)
        {
            TInterface instance = Load(key);
            instance.Delete(true);
        }

        protected static Entity entity = null;
        public static Entity Entity
        {

            get
            {
                if (entity == null)
                    entity = Instance.GetEntity();

                return entity;
            }
        }

        internal static readonly TSelf Instance = (TSelf)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(TSelf));
        public abstract Entity GetEntity();

        #region Stored Queries

        private static Dictionary<string, ICompiled> StoredQueries = null;

        protected virtual void RegisterStoredQueries() { }
        protected abstract void RegisterGeneratedStoredQueries();

        public static void RegisterQuery(string name, ICompiled query)
        {
            InitializeStoredQueries();

            StoredQueries.Add(name, query);
        }
        public static List<TInterface> FromQuery(string name, params Parameter[] parameters)
        {
            InitializeStoredQueries();

            ICompiled query = StoredQueries[name];
            return LoadWhere(query, parameters);
        }

        private static void InitializeStoredQueries()
        {
            if (StoredQueries != null)
                return;

            lock (typeof(TInterface))
            {
                if (StoredQueries == null)
                {
                    StoredQueries = new Dictionary<string, ICompiled>();
                    Instance.RegisterGeneratedStoredQueries();
                    Instance.RegisterStoredQueries();
                }
            }
        }

        #endregion
    }
}
