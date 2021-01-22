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
        internal Interface(Entity entity)
        {
            Parent = null!;
            Name = "Ad-hoc";
            Guid = Guid.Empty;
            
            entities.Add(entity);
        }

        public DatastoreModel Parent { get; private set; }
        public string Name { get; private set; }
        public Guid Guid { get; private set; }

        public IReadOnlyList<Entity> Entities { get { return entities; } }
        private List<Entity> entities = new List<Entity>();

        public Interface AddEntity(string entity)
        {
            if (Parent is null)
                throw new InvalidOperationException("You cannot change an 'ad-hoc' interface.");

            return AddEntity(Parent.Entities[entity]);
        }
        public Interface AddEntity(Entity entity)
        {
            if (Parent is null)
                throw new InvalidOperationException("You cannot change an 'ad-hoc' interface.");

            if (entities.Contains(entity))
                return this;

            entities.Add(entity);

            return this;
        }
        public Interface AddEntities(params string[] entities)
        {
            if (Parent is null)
                throw new InvalidOperationException("You cannot change an 'ad-hoc' interface.");

            Entity[] target = new Entity[entities.Length];
            for (int index = 0; index < entities.Length; index++)
                target[index] = Parent.Entities[entities[index]];

            return AddEntities(target);
        }
        public Interface AddEntities(params Entity[] entities)
        {
            if (Parent is null)
                throw new InvalidOperationException("You cannot change an 'ad-hoc' interface.");

            HashSet<Entity> test = new HashSet<Entity>(this.entities);

            foreach (Entity entity in entities.Distinct())
                if (!test.Contains(entity))
                    this.entities.Add(entity);

            return this;
        }

        public IRefactorInterface Refactor { get { return this; } }

        void IRefactorInterface.RemoveEntity(string entity)
        {
            if (Parent is null)
                throw new InvalidOperationException("You cannot change an 'ad-hoc' interface.");

            int index = entities.FindIndex((e) => e.Name == entity);

            if (index == -1)
                return;

            entities.RemoveAt(index);
        }
        void IRefactorInterface.RemoveEntity(Entity entity)
        {
            if (Parent is null)
                throw new InvalidOperationException("You cannot change an 'ad-hoc' interface.");

            ((IRefactorInterface)this).RemoveEntity(entity.Name);
        }
        void IRefactorInterface.RemoveEntities(params string[] entities)
        {
            if (Parent is null)
                throw new InvalidOperationException("You cannot change an 'ad-hoc' interface.");

            foreach (string entity in entities)
                ((IRefactorInterface)this).RemoveEntity(entity);
        }
        void IRefactorInterface.RemoveEntities(params Entity[] entities)
        {
            if (Parent is null)
                throw new InvalidOperationException("You cannot change an 'ad-hoc' interface.");

            foreach (Entity entity in entities)
                ((IRefactorInterface)this).RemoveEntity(entity.Name);
        }

        void IRefactorInterface.Rename(string name)
        {
            if (Parent is null)
                throw new InvalidOperationException("You cannot change an 'ad-hoc' interface.");

            Parent.Interfaces.Remove(Name);
            Name = name;
            Parent.Interfaces.Add(Name, this);
        }
        void IRefactorInterface.Deprecate()
        {
            if (Parent is null)
                throw new InvalidOperationException("You cannot change an 'ad-hoc' interface.");

            Parent.Interfaces.Remove(Name);
        }

        internal Entity BaseEntity
        {
            get
            {
                if (Parent is null)
                    return entities.First();

                if (entities.Count == 0)
                    throw new InvalidOperationException($"The interface '{Name}' is not implemented by any entity.");

                Entity? shared = Entity.FindCommonBaseClass(entities);
                if (shared is null)
                    throw new InvalidOperationException($"The entities that implement interface '{Name}' do not share a common base entity. Hint, let all entities inherit from a common base entity similar like how all 'types' inherit from 'object'.");

                return shared;
            }
        }
    }
}
