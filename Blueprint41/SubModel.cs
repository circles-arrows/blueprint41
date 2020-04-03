using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blueprint41
{
    public class SubModel
    {
        internal SubModel(DatastoreModel parent, string name, int chapter, bool isDraft, bool isLaboratory)
        {
            Parent = parent;
            Name = name;
            Chapter = chapter;
            IsDraft = isDraft;
            IsLaboratory = isLaboratory;
            Explanation = null;
            Guid = parent.GenerateGuid(name);
        }

        public DatastoreModel Parent { get; private set; }
        public string Name { get; private set; }
        public int Chapter { get; private set; }
        public bool IsDraft { get; private set; }
        public bool IsLaboratory { get; private set; }
        public string? Explanation { get; private set; }
        public Guid Guid { get; private set; }

        public IReadOnlyList<Entity> Entities { get { return entities; } }
        private List<Entity> entities = new List<Entity>();

        public SubModel AddEntity(string entity)
        {
            return AddEntity(Parent.Entities[entity]);
        }
        public SubModel AddEntity(Entity entity)
        {
            if (Name == "Main")
                throw new InvalidOperationException("You cannot add entities to the 'Main' SubModel.");

            if (entities.Contains(entity))
                return this;

            entities.Add(entity);

            return this;
        }
        public SubModel AddEntities(params string[] entities)
        {
            Entity[] target = new Entity[entities.Length];
            for (int index = 0; index < entities.Length; index++)
                target[index] = Parent.Entities[entities[index]];

            return AddEntities(target);
        }
        public SubModel AddEntities(params Entity[] entities)
        {
            if (Name == "Main")
                throw new InvalidOperationException("You cannot add entities to the 'Main' SubModel.");

            HashSet<Entity> test = new HashSet<Entity>(this.entities);

            foreach (Entity entity in entities.Distinct())
                if (!test.Contains(entity))
                    this.entities.Add(entity);

            return this;
        }
        internal void AddEntityInternal(Entity entity)
        {
            entities.Add(entity);
        }

        public SubModel SetEntities(params string[] entities)
        {
            Entity[] target = new Entity[entities.Length];
            for (int index = 0; index < entities.Length; index++)
                target[index] = Parent.Entities[entities[index]];

            return SetEntities(target);
        }
        public SubModel SetEntities(params Entity[] entities)
        {
            if (Name == "Main")
                throw new InvalidOperationException("You cannot set entities for the 'Main' SubModel.");

            this.entities = entities.ToList();

            return this;
        }

        public SubModel RemoveEntity(string entity)
        {
            return RemoveEntity(Parent.Entities[entity]);
        }
        public SubModel RemoveEntity(Entity entity)
        {
            if (Name == "Main")
                throw new InvalidOperationException("You cannot remove entities from the 'Main' SubModel.");

            RemoveEntityInternal(entity);

            return this;
        }
        public SubModel RemoveEntities(params string[] entities)
        {
            Entity[] target = new Entity[entities.Length];
            for (int index = 0; index < entities.Length; index++)
                target[index] = Parent.Entities[entities[index]];

            return RemoveEntities(target);
        }
        public SubModel RemoveEntities(params Entity[] entities)
        {
            if (Name == "Main")
                throw new InvalidOperationException("You cannot remove entities from the 'Main' SubModel.");

            HashSet<Entity> test = new HashSet<Entity>(entities);

            foreach (Entity entity in entities.Distinct())
                if (test.Contains(entity))
                    this.entities.Remove(entity);

            return this;
        }
        internal void RemoveEntityInternal(Entity entity)
        {
            entities.Remove(entity);
        }

        public SubModel Rename(string name)
        {
            if (Name == "Main")
                throw new InvalidOperationException("You cannot rename the 'Main' SubModel.");

            if (name == "Main")
                throw new InvalidOperationException("You cannot rename a SubModel to 'Main'.");

            Parent.SubModels.Remove(Name);
            Name = name;
            Parent.SubModels.Add(Name, this);

            return this;
        }
        public SubModel SetChapter(int chapter)
        {
            if (Name == "Main")
                throw new InvalidOperationException("You cannot set the chapter for the 'Main' SubModel.");

            Chapter = chapter;

            return this;
        }
        public SubModel SetStatus(bool isDraft, bool isLaboratory)
        {
            IsDraft = isDraft;
            IsLaboratory = isLaboratory;

            return this;
        }
        public SubModel SetExplanation(string? explanation)
        {
            Explanation = explanation;

            return this;
        }
    }
}
