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
    public partial class Entity
    {
        public Entity ParentEntity
        {
            get
            {
                if (string.IsNullOrEmpty(Inherits))
                    return null;

                return Model.Entities.Entity.FirstOrDefault(entity => entity.Guid == Inherits);
            }
        }

        public IEnumerable<Relationship> GetRelationships(Submodel submodel)
        {
            if (Model == null)
                throw new InvalidOperationException("Cannot get relationship of nodes when model is not set.");

            return submodel.Relationships.Where(item => item.Source.Label == Label || item.Target.Label == Label);
        }

        public IEnumerable<Relationship> GetRelationships(RelationshipDirection direction, bool includeInherited)
        {
            if (Model == null)
                throw new InvalidOperationException("Cannot get relationship of nodes when model is not set.");

            List<Relationship> relationships = new List<Schemas.Relationship>();


            Entity current = this;
            do
            {
                if(direction != RelationshipDirection.Out)
                    relationships.AddRange(Model.Relationships.Relationship.Where(item => item.Source.ReferenceGuid == current.Guid));

                if (direction != RelationshipDirection.In)
                    relationships.AddRange(Model.Relationships.Relationship.Where(item => item.Target.ReferenceGuid == current.Guid));

                current = current.ParentEntity;
            } while (current != null && includeInherited);

            return relationships;
        }

        public IEnumerable<Relationship> GetRelationships(Submodel model, bool includeInherited)
        {
            List<Relationship> relationships = model.Model.Relationships.Relationship.Where(item => item.Source.ReferenceGuid == this.Guid || item.Target.ReferenceGuid == this.Guid).ToList();

            if (includeInherited == false)
                return relationships;

            Dictionary<RelationshipDirection, List<Relationship>> inheritedPropertyByDirection = this.GetInheritedRelationships(model);
            foreach (var item in inheritedPropertyByDirection[RelationshipDirection.In])
            {
                Relationship relationship = new Relationship(model.Model, (relationship)item.Xml.Clone());
                relationship.Source.Label = this.Label;
                relationships.Add(relationship);
                model.CreatedInheritedRelationships.Add(relationship);
            }
            foreach (var item in inheritedPropertyByDirection[RelationshipDirection.Out])
            {
                Relationship relationship = new Relationship(model.Model, (relationship)item.Xml.Clone());
                relationship.Target.Label = this.Label;
                relationships.Add(relationship);
                model.CreatedInheritedRelationships.Add(relationship);
            }

            return relationships;
        }

        public void CleanPrimitive()
        {
            Schemas.Primitive[] copy = new Schemas.Primitive[Primitive.Count];
            Primitive.CopyTo(copy, 0);

            foreach (Primitive item in copy.ToList())
            {
                if (string.IsNullOrEmpty(item.Name))
                    Primitive.Remove(item);
            }
        }

        protected override void InitializeLogic()
        {
            // Set Guid Id
            if (string.IsNullOrEmpty(this.Guid))
                this.Guid = System.Guid.NewGuid().ToString();

            OnLabelChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                if (Model == null)
                    return;

                if (e.NewValue != e.OldValue && e.OldValue == Name)
                {
                    string newGuid = Model.GenerateGuid(e.NewValue).ToString();

                    foreach (var sub in Model.Submodels.Submodel)
                    {
                        foreach (var node in sub.Node)
                        {
                            if (node.EntityGuid == this.Guid)
                            {
                                node.Label = e.NewValue;
                                node.EntityGuid = newGuid;
                            }
                        }
                    }

                    foreach (var relationship in Model.Relationships.Relationship)
                    {
                        if (relationship.Source.ReferenceGuid == this.Guid)
                        { 
                            relationship.Source.Label = e.NewValue;
                            relationship.Source.ReferenceGuid = newGuid;
                        }

                        if (relationship.Target.ReferenceGuid == this.Guid)
                        {
                            relationship.Target.Label = e.NewValue;
                            relationship.Target.ReferenceGuid = newGuid;
                        }
                    }

                    Name = e.NewValue;
                    Guid = newGuid;
                }  
            };

            OnLabelChanging += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                if (string.IsNullOrEmpty(e.NewValue))
                {
                    e.Cancel = true;
                    System.Windows.Forms.MessageBox.Show($"Entity label cannot be empty.", "Info", System.Windows.Forms.MessageBoxButtons.OK);
                    return;
                }

                if (Model.Entities.Entity.Any(item => item.Label == e.NewValue))
                {
                    e.Cancel = true;
                    System.Windows.Forms.MessageBox.Show($"Entity with label {e.NewValue} already exist.", "Info", System.Windows.Forms.MessageBoxButtons.OK);
                    return;
                }
            };

            OnNameChanging += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                if (string.IsNullOrEmpty(e.NewValue))
                {
                    e.Cancel = true;
                    System.Windows.Forms.MessageBox.Show($"Entity name cannot be empty.", "Info", System.Windows.Forms.MessageBoxButtons.OK);
                    return;
                }

                if (Model.Entities.Entity.Any(item => item.Name == e.NewValue))
                {
                    e.Cancel = true;
                    System.Windows.Forms.MessageBox.Show($"Entity with name {e.NewValue} already exist.", "Info", System.Windows.Forms.MessageBoxButtons.OK);
                    return;
                }
            };

            OnIsStaticDataChanged += delegate (object sender, PropertyChangedEventArgs<bool> e)
            {
                if (e.NewValue)
                    CreateStaticData();
                else
                    ClearStaticData();
            };
        }

        private void CreateStaticData()
        {
            if (Xml.@staticData == null)
                Xml.@staticData = new staticData();
            m_StaticData = new StaticData(Model, Xml.@staticData);
        }

        public void ClearStaticData()
        {
            Xml.@staticData = null;
            StaticData = null;
        }

        public override bool Equals(object obj)
        {
            if (obj is Entity)
            {
                Entity other = obj as Entity;
                return this.Name == other.Name && this.Label == other.Label;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
