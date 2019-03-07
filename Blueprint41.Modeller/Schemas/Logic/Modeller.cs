using Blueprint41.Modeller.Schemas;
using Microsoft.Msagl;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DrawingNode = Microsoft.Msagl.Drawing.Node;
using DrawingEdge = Microsoft.Msagl.Drawing.Edge;

namespace Blueprint41.Modeller.Schemas
{
    public partial class Modeller
    {
        List<Guid> knownGuids = new List<Guid>();

        internal void Save(string storagePath)
        {
            foreach (Entity item in Entities.Entity.ToList())
            {
                if (string.IsNullOrEmpty(item.Name))
                {
                    Entities.Entity.Remove(item);
                    continue;
                }

                foreach (Primitive primitive in item.Primitive.ToList())
                {
                    if (string.IsNullOrEmpty(primitive.Name))
                        item.Primitive.Remove(primitive);
                }

                if (!item.IsStaticData)
                {
                    item.Xml.isStaticData = false;
                    item.ClearStaticData();
                }
            }

            foreach (Relationship item in Relationships.Relationship.ToList())
            {
                if (item.Name == null)
                    Relationships.Relationship.Remove(item);
            }

            Xml.Save(storagePath);
        }

        protected override void InitializeLogic()
        {
            PopulateKnownGuids();

            OnEntitiesChanged += delegate (object sender, PropertyChangedEventArgs<EntitiesLocalType> e)
            {
                RebindControl();
            };
            OnRelationshipsChanged += delegate (object sender, PropertyChangedEventArgs<RelationshipsLocalType> e)
            {
                RebindControl();
            };
            OnSubmodelsChanged += delegate (object sender, PropertyChangedEventArgs<SubmodelsLocalType> e)
            {
                if (Model.DisplayedSubmodel != null)
                    RebindControl();
            };
        }

        protected void PopulateKnownGuids()
        {
            foreach (Entity entity in Entities.Entity)
            {
                if (entity.Guid != null) AddToKnownGuids(new Guid(entity.Guid));

                foreach (Primitive prim in entity.Primitive)
                {
                    if (prim.Guid != null) AddToKnownGuids(new Guid(prim.Guid));
                }
            }

            foreach (Relationship rel in Relationships.Relationship)
            {
                if (rel.Guid != null) AddToKnownGuids(new Guid(rel.Guid));
            }

            foreach (FunctionalId funcId in FunctionalIds.FunctionalId)
            {
                if (funcId.Guid != null) AddToKnownGuids(new Guid(funcId.Guid));
            }
        }

        public Guid GenerateGuid(string name)
        {
            Guid hash = GetHash(name);

            while (knownGuids.Contains(hash))
            {
                name = string.Concat(name + "Conflict...");
                hash = GetHash(name);
            }

            AddToKnownGuids(hash);
            return hash;
        }

        private static Guid GetHash(string name)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(name));
                return new Guid(hash);
            }
        }

        public void AddToKnownGuids(Guid guid)
        {
            if (!knownGuids.Contains(guid)) knownGuids.Add(guid);
        }

        /// <summary>
        /// Gets all the relationships of the Entity regardless of submodel
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="includeInherited"></param>
        /// <returns></returns>
        public IEnumerable<Relationship> GetRelationships(Entity entity, RelationshipDirection direction = RelationshipDirection.Both, bool includeInherited = false)
        {
            Dictionary<string, Relationship> relationships = new Dictionary<string, Relationship>();
            Entity current = entity;

            Func<Relationship, bool> func;
            switch (direction)
            {
                case RelationshipDirection.Out:
                    func = RelationshipOut;
                    break;
                case RelationshipDirection.Both:
                    func = RelationshipBoth;
                    break;
                case RelationshipDirection.In:
                default:
                    func = RelationshipIn;
                    break;
            }

            do
            {
                foreach (Relationship rel in Relationships.Relationship.Where(func).ToList())
                    if (relationships.ContainsKey(rel.Name) == false)
                        relationships.Add(rel.Name, rel);

                current = current.ParentEntity;

            } while (current != null && includeInherited);

            return relationships.Select(x => x.Value).ToList();

            bool RelationshipIn(Relationship item)
            {
                return item.Source.Label == current.Label;
            }

            bool RelationshipOut(Relationship item)
            {
                return item.Target?.Label == current.Label;
            }

            bool RelationshipBoth(Relationship item)
            {
                return item.Source.Label == current.Label || item.Target?.Label == current.Label;
            }
        }

        /// <summary>
        /// Gets all the relationships of the entity regardless of submodel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        /// <param name="includeInherited"></param>
        /// <returns></returns>
        public IEnumerable<Relationship> GetRelationships(Entity entity, bool includeInherited = false)
        {
            return GetRelationships(entity, RelationshipDirection.Both, includeInherited);
        }

        public void RemoveRelationship(Relationship relationship)
        {
            Model.Relationships.Relationship.Remove(relationship);
            relationship.DeleteDrawingEdge(false);
        }

        public void InsertRelationship(string sourceLabel, string targetLabel, Relationship relationship, DrawingEdge edge)
        {
            relationship.IsAddedOnViewGraph = true;
            relationship.Source.Label = sourceLabel;
            relationship.Target.Label = targetLabel;

            DrawingNode sourceNode = GraphEditor.Graph.FindNode(relationship.InEntity);
            DrawingNode targetNode = GraphEditor.Graph.FindNode(relationship.OutEntity);

            Submodel.NodeLocalType source = sourceNode.UserData as Submodel.NodeLocalType;
            Submodel.NodeLocalType target = targetNode.UserData as Submodel.NodeLocalType;

            int count = 0;
            string relName = relationship.Name;
            string typeName = relationship.Type;
            while (Relationships.Relationship.Any(item => item.Name == relName))
            {
                relName += count++;
                typeName += count;
            }

            relationship.Name = relName;
            relationship.Type = typeName;
            relationship.Target.ReferenceGuid = target.Entity.Guid;
            relationship.Source.ReferenceGuid = source.Entity.Guid;

            Model.Relationships.Relationship.Add(relationship);
            relationship.SetDrawingEdge(edge);
        }

        public void ExcludeFromCurrentModel(Submodel.NodeLocalType model)
        {
            if (Model.DisplayedSubmodel.Name == "Main Model")
                throw new NotSupportedException("Could not exclude entity from current model");

            Model.DisplayedSubmodel.Node.Remove(model);
        }

        public void DeleteEntity(Submodel.NodeLocalType node)
        {
            if (node != null)
            {
                Model.Entities.Entity.Remove(node.Entity);

                // delete in sub models
                foreach (Submodel subModel in Model.Submodels.Submodel)
                {
                    List<Submodel.NodeLocalType> tempSubModeNodes = new List<Submodel.NodeLocalType>();
                    tempSubModeNodes.AddRange(subModel.Node);
                    foreach (Submodel.NodeLocalType subModelNode in tempSubModeNodes)
                    {
                        if (subModelNode.Label == node.Label)
                            subModel.Node.Remove(subModelNode);
                    }
                }

                // delete related relationships
                List<Relationship> tempRelationships = new List<Relationship>();
                tempRelationships.AddRange(Model.Relationships.Relationship);
                foreach (Relationship relationship in tempRelationships)
                {
                    if (relationship.Target?.Label == node.Label || relationship.Source.Label == node.Label)
                        Model.Relationships.Relationship.Remove(relationship);
                }
            }
        }

        public partial class EntitiesLocalType
        {
            protected override void InitializeLogic()
            {
                EntityCollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
                {
                    switch (e.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                        case NotifyCollectionChangedAction.Remove:
                        case NotifyCollectionChangedAction.Reset:
                            break;
                    }
                };
            }
        }

        public partial class RelationshipsLocalType
        {
            protected override void InitializeLogic()
            {
                RelationshipCollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
                {
                    switch (e.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                        case NotifyCollectionChangedAction.Remove:
                        case NotifyCollectionChangedAction.Reset:
                            break;
                    }
                };
            }
        }

        public partial class SubmodelsLocalType
        {
            protected override void InitializeLogic()
            {
                SubmodelCollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
                {
                    switch (e.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                        case NotifyCollectionChangedAction.Remove:
                        case NotifyCollectionChangedAction.Reset:
                            break;
                    }
                };
            }
        }
    }
}
