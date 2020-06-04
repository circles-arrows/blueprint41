using System;
using System.Collections.Generic;
using System.Text;

using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence.Void;

namespace Blueprint41.DatastoreTemplates
{
    public abstract partial class GeneratorBase
    {
        public abstract string TransformText();
        public Entity? DALModel { get; set; }
        public Relationship? DALRelation { get; set; }
        public DatastoreModel? Datastore { get; set; }
        public List<TypeMapping> SupportedTypeMappings => Datastore?.PersistenceProvider?.SupportedTypeMappings ?? Neo4jPersistenceProvider.supportedTypeMappings;

        public GeneratorSettings? Settings { get; set; }

        public void Log(string text, params object[] arguments)
        {
          
        }

        public GeneratorBase()
        {
        }
    }
}
