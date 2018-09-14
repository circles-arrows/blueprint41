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
