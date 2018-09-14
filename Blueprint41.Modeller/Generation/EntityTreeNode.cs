using Blueprint41.Modeller.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model = Blueprint41.Modeller.Schemas.Modeller;

namespace Blueprint41.Modeller.Generation
{
    internal class EntityTreeNode : TreeNode
    {
        public EntityTreeNode(Model model, Entity entity) : base(entity.Label)
        {
            Model = model;
            Entity = entity;
            Guid = Guid.Parse(entity.Guid);
            relationship = new Lazy<List<Relationship>>(() => entity.GetRelationships(RelationshipDirection.In, true).ToList());
        }

        public Model Model { get; }

        public Entity Entity { get; }
        public Guid Guid { get; }

        private readonly Lazy<List<Relationship>> relationship;
        private bool loaded;
        private bool loadedInheritance;

        public List<Relationship> Relationship
        {
            get { return relationship.Value; }
        }

        public TreeNode InheritNode { get; } = new TreeNode("Inherit");
        public TreeNode RelationshipNode { get; } = new TreeNode("Relationship");
        public bool Loaded
        {
            get { return loaded && loadedInheritance; }
        }

        internal void LoadRelationship()
        {
            if (loaded)
                return;

            foreach (Relationship rel in Relationship)
                RelationshipNode.Nodes.Add(new RelationshipTreeNode(Model, rel));

            if (RelationshipNode.Nodes.Count > 0)
                Nodes.Add(RelationshipNode);

            RelationshipNode.Expand();
            RelationshipNode.Checked = true;

            loaded = true;
        }

        internal void LoadInheritance()
        {
            if (loadedInheritance)
                return;

            if (Entity.Inherits == null)
                return;

            var entityLookup = Model.Entities.Entity.ToDictionary(x => Guid.Parse(x.Guid));

            if (Entity.Inherits != null)
            {
                entityLookup.TryGetValue(Guid.Parse(Entity.Inherits), out Entity parent);

                while (parent != null)
                {
                    InheritNode.Nodes.Add(new InheritedEntityTreeNode(Model, parent));

                    if (parent.Inherits == null)
                        break;

                    entityLookup.TryGetValue(Guid.Parse(parent.Inherits), out parent);
                }
            }

            if (InheritNode.Nodes.Count > 0)
                Nodes.Add(InheritNode);

            InheritNode.Expand();
            InheritNode.Checked = true;
            loadedInheritance = true;
        }
    }

    internal class InheritedEntityTreeNode : TreeNode
    {
        public InheritedEntityTreeNode(Model model, Entity entity) : base(entity.Label)
        {
            Model = model;
            Entity = entity;
            Checked = true;
        }

        public Model Model { get; }
        public Entity Entity { get; }
    }

    internal class RelationshipTreeNode : TreeNode
    {
        private readonly Lazy<Entity> inEntity;
        private readonly Lazy<Entity> outEntity;

        public Entity InEntity
        {
            get { return inEntity.Value; }
        }
        public Entity OutEntity
        {
            get { return outEntity.Value; }
        }

        public Relationship Relationship { get; }
        public Guid Guid { get; }

        public RelationshipTreeNode(Model model, Relationship rel) : base(rel.Name)
        {
            inEntity = new Lazy<Entity>(() => model.Entities.Entity.Where(x => x.Label == rel.Source.Label).FirstOrDefault());
            outEntity = new Lazy<Entity>(() => model.Entities.Entity.Where(x => x.Label == rel.Target?.Label).FirstOrDefault());

            Checked = true;
            Relationship = rel;
            Guid = Guid.Parse(rel.Guid);
        }
    }
}
