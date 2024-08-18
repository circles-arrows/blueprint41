using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Blueprint41.Sync.Dynamic;

namespace Blueprint41.Sync.Neo4j.Refactoring
{
    public interface IRefactorInterface
    {
        void RemoveEntity(string entity);
        void RemoveEntity(Entity entity);
        void RemoveEntities(params string[] entities);
        void RemoveEntities(params Entity[] entities);
        void Rename(string newName);
        void Deprecate();
    }
}
