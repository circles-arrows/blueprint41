using System;
using System.Collections.Generic;

namespace Blueprint41.Refactoring
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
