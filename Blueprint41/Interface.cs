using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Neo4j.Refactoring;

namespace Blueprint41
{
    public class Interface : IRefactorInterface
    {
        internal Interface(DatastoreModel parent, string name)
        {
            Parent = parent;
            Name = name;
            Guid = parent.GenerateGuid(name);
        }

        public DatastoreModel Parent { get; private set; }
        public string Name { get; private set; }
        public Guid Guid { get; private set; }

        public IReadOnlyList<Entity> Entities { get { return entities; } }
        private List<Entity> entities = new List<Entity>();

        public Interface AddEntity(string entity)
        {
            return AddEntity(Parent.Entities[entity]);
        }
        public Interface AddEntity(Entity entity)
        {
            if (Name == "Main")
                throw new InvalidOperationException("You cannot add entities to the 'Main' SubModel.");

            if (entities.Contains(entity))
                return this;

            entities.Add(entity);

            return this;
        }
        public Interface AddEntities(params string[] entities)
        {
            Entity[] target = new Entity[entities.Length];
            for (int index = 0; index < entities.Length; index++)
                target[index] = Parent.Entities[entities[index]];

            return AddEntities(target);
        }
        public Interface AddEntities(params Entity[] entities)
        {
            if (Name == "Main")
                throw new InvalidOperationException("You cannot add entities to the 'Main' SubModel.");

            HashSet<Entity> test = new HashSet<Entity>(this.entities);

            foreach (Entity entity in entities.Distinct())
                if (!test.Contains(entity))
                    this.entities.Add(entity);

            return this;
        }

        public IRefactorInterface Refactor { get { return this; } }

        void IRefactorInterface.RemoveEntity(string entity)
        {
            Parent.Entities.Remove(entity);
        }
        void IRefactorInterface.RemoveEntity(Entity entity)
        {
            Parent.Entities.Remove(entity.Name);
        }
        void IRefactorInterface.RemoveEntities(params string[] entities)
        {
            foreach (string entity in entities)
                Parent.Entities.Remove(entity);
        }
        void IRefactorInterface.RemoveEntities(params Entity[] entities)
        {
            foreach (Entity entity in entities)
                Parent.Entities.Remove(entity.Name);
        }

        void IRefactorInterface.Rename(string name)
        {
            Parent.Interfaces.Remove(Name);
            Name = name;
            Parent.Interfaces.Add(Name, this);
        }
        void IRefactorInterface.Deprecate()
        {
            Parent.Interfaces.Remove(Name);
        }
    }
}
