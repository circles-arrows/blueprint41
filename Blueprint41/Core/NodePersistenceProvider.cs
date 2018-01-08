using Blueprint41.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    internal abstract class NodePersistenceProvider
    {
        protected NodePersistenceProvider(PersistenceProvider factory)
        {
            this.PersistenceProviderFactory = factory;
        }

        public PersistenceProvider PersistenceProviderFactory { get; private set; }

        public abstract List<T> GetAll<T>(Entity entity) where T : OGM;
        public abstract List<T> GetAll<T>(Entity entityint, int page = 0, int pageSize = 0, params Property[] orderBy) where T : OGM;

        public abstract List<T> LoadWhere<T>(Entity entity, string conditions, Parameter[] parameters, int page = 0, int pageSize = 0, params Property[] orderBy) where T : OGM;
        public abstract List<T> LoadWhere<T>(Entity entity, ICompiled query, params Parameter[] parameters) where T : OGM;

        internal abstract List<T> Search<T>(Entity entity, string text, Property[] properties, int page = 0, int pageSize = 0, params Property[] orderBy) where T : OGM;

        public abstract void Load(OGM item);
        public abstract void Insert(OGM item);
        public abstract void Update(OGM item);
        public abstract void Delete(OGM item);

        public abstract void ForceDelete(OGM item);

        public abstract bool RelationshipExists(Property foreignProperty, OGM instance);
    }
}
