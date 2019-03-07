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


        /// <summary>
        /// Gets all the relationships for this submodel
        /// </summary>
        /// <param name="submodel"></param>
        /// <returns></returns>
        public IEnumerable<Relationship> GetRelationships(Submodel submodel)
        {
            if (Model == null)
                throw new InvalidOperationException("Cannot get relationship of nodes when model is not set.");

            return submodel.Relationships.Where(item => item.Source.Label == Label || item.Target?.Label == Label);
        }

        /// <summary>
        /// Gets all the relationships regardless of submodel
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="includeInherited"></param>
        /// <returns></returns>
        public IEnumerable<Relationship> GetRelationships(RelationshipDirection direction, bool includeInherited)
        {
            if (Model == null)
                throw new InvalidOperationException("Cannot get relationship of nodes when model is not set.");

            return Model.GetRelationships(this, includeInherited);
        }

        /// <summary>
        /// Gets all the relationships for this submodel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="direction"></param>
        /// <param name="includeInherited"></param>
        /// <returns></returns>
        public IEnumerable<Relationship> GetRelationships(Submodel model, RelationshipDirection direction, bool includeInherited)
        {
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

            Dictionary<string, Relationship> relationships = model.Relationships.Where(func).ToDictionary(x => x.Name);

            if (includeInherited == false)
                return relationships.Select(x => x.Value).ToList();

            Dictionary<RelationshipDirection, List<Relationship>> inheritedPropertyByDirection = this.GetInheritedRelationships(model);

            foreach (Relationship item in inheritedPropertyByDirection[RelationshipDirection.In])
            {
                if (relationships.ContainsKey(item.Name))
                    continue;

                Relationship relationship = new Relationship(model.Model, (relationship)item.Xml.Clone());
                relationships.Add(relationship.Name, relationship);
                model.CreatedInheritedRelationships.Add(relationship);
            }

            foreach (Relationship item in inheritedPropertyByDirection[RelationshipDirection.Out])
            {
                if (relationships.ContainsKey(item.Name))
                    continue;

                Relationship relationship = new Relationship(model.Model, (relationship)item.Xml.Clone());
                relationships.Add(relationship.Name, relationship);
                model.CreatedInheritedRelationships.Add(relationship);
            }

            return relationships.Select(x => x.Value).ToList();

            bool RelationshipIn(Relationship item)
            {
                return item.Source.Label == this.Label;
            }

            bool RelationshipOut(Relationship item)
            {
                return item.Target?.Label == this.Label;
            }

            bool RelationshipBoth(Relationship item)
            {
                return item.Source.Label == this.Label || item.Target?.Label == this.Label;
            }
        }

        /// <summary>
        /// Gets the inherited relationship for this submodel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Dictionary<RelationshipDirection, List<Relationship>> GetInheritedRelationships(Submodel model)
        {
            Dictionary<RelationshipDirection, List<Relationship>> result = new Dictionary<RelationshipDirection, List<Schemas.Relationship>>();
            result.Add(RelationshipDirection.In, new List<Schemas.Relationship>());
            result.Add(RelationshipDirection.Out, new List<Schemas.Relationship>());

            Entity current = ParentEntity;

            while (current != null)
            {
                result[RelationshipDirection.In].AddRange(model.Relationships.Where(item => item.Source.Label == current.Label));
                result[RelationshipDirection.Out].AddRange(model.Relationships.Where(item => item.Target?.Label == current.Label));

                current = current.ParentEntity;
            }

            return result;
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

                if (e.NewValue != e.OldValue)
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

                    foreach (var sub in Model.Entities.Entity)
                    {
                        if (sub.Inherits == this.Guid)
                            sub.Inherits = newGuid;
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
