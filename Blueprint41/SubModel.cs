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
        }

        public DatastoreModel Parent { get; private set; }
        public string Name { get; private set; }
        public int Chapter { get; private set; }
        public bool IsDraft { get; private set; }
        public bool IsLaboratory { get; private set; }
        public string? Explanation { get; private set; }

        public IReadOnlyList<Entity> Entities { get { return entities; } }
        private List<Entity> entities = new List<Entity>();

        public SubModel AddEntity(string entity)
        {
            return AddEntity(Parent.Entities[entity]);
        }
        public SubModel AddEntity(Entity entity)
        {
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
            HashSet<Entity> test = new HashSet<Entity>(entities);

            foreach (Entity entity in entities.Distinct())
                if (!test.Contains(entity))
                    this.entities.Add(entity);

            return this;
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
            this.entities = entities.ToList();

            return this;
        }

        public SubModel RemoveEntity(string entity)
        {
            return RemoveEntity(Parent.Entities[entity]);
        }
        public SubModel RemoveEntity(Entity entity)
        {
            entities.Remove(entity);

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
            HashSet<Entity> test = new HashSet<Entity>(entities);

            foreach (Entity entity in entities.Distinct())
                if (test.Contains(entity))
                    this.entities.Remove(entity);

            return this;
        }

        public SubModel Rename(string name)
        {
            Name = name;

            return this;
        }
        public SubModel SetChapter(int chapter)
        {
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
