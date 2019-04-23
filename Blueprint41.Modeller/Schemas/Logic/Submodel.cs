using Blueprint41.Modeller.Schemas;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller.Schemas
{
    public partial class Submodel
    {
        public override Modeller Model
        {
            get { return base.Model; }
            internal set
            {
                base.Model = value;

                if (Node != null)
                {
                    foreach (NodeLocalType node in Node)
                    {
                        node.Model = value;
                    }
                }
            }
        }

        protected override void InitializeLogic()
        {
            NodeCollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (NodeLocalType node in e.NewItems)
                            node.Parent = this;
                        break;
                    case NotifyCollectionChangedAction.Remove:
                    case NotifyCollectionChangedAction.Reset:
                        foreach (NodeLocalType node in e.OldItems)
                            node.Parent = null;
                        break;
                }
            };
        }

        public IReadOnlyList<Entity> Entities
        {
            get
            {
                if (Model == null)
                    return null;

                return Node.Select(node => node.Entity).Distinct().ToList();
            }
        }

        public IReadOnlyList<Relationship> Relationships
        {
            get
            {
                if (this.Model == null)
                    return null;

                return this.GetRelationships(false);
            }
        }

        public override string ToString()
        {
            if (IsDraft)
                return Name + " ------ (Draft)";

            return Name;
        }

        public partial class NodeLocalType
        {

            internal Submodel Parent
            {
                get { return m_Parent; }
                set
                {
                    m_Parent = value;
                    this.Model = m_Parent?.Model;
                }
            }
            private Submodel m_Parent = null;

            protected override void InitializeLogic()
            {
                OnLabelChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
                {
                    entitiesLookUp?.Clear();
                };
            }


            private Dictionary<string, Entity> entitiesLookUp = new Dictionary<string, Entity>();
            public Entity Entity
            {
                get
                {
                    if (Model == null)
                        return null;

                    Entity entity;
                    try
                    {
                        if (entitiesLookUp.Count == 0)
                            entitiesLookUp = Model.Entities.Entity.ToDictionary(x => x.Label, y => y);

                        entity = entitiesLookUp[Label];
                    }
                    catch (Exception)
                    {
                        throw new Exception(string.Format("'{0}' exists in <submodels> but does not exist in <entities>. Remove all '{1}' references from all <submodels>", Label, Label));
                    }

                    return entity;
                }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException("Null value for entity is not allowed.");

                    Label = value.Label;
                }
            }

            public NodeLocalType Clone()
            {
                NodeLocalType clone = new NodeLocalType(Model, (submodel.nodeLocalType)this.Xml.Clone());
                return clone;
            }
        }
    }
}
