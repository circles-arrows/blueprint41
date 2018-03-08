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
            //PopulateKnownGuids();

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
