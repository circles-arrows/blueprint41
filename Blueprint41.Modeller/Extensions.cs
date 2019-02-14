using Blueprint41.Modeller.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model = Blueprint41.Modeller.Schemas.Modeller;
namespace Blueprint41.Modeller
{
    public interface IComboBoxItem
    {
        string Display { get; set; }
        object Value { get; set; }
    }

    public class EntityComboBoxItem : IComboBoxItem
    {
        public string Display { get; set; }
        public object Value { get; set; }

        public EntityComboBoxItem() { }

        public EntityComboBoxItem(string display, object value)
        {
            Display = display;
            Value = value;
        }
    }

    public static class Extensions
    {
        /// <summary>
        /// Gets all the relationships from this submodel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="includeInherited"></param>
        /// <returns></returns>
        public static List<Relationship> GetRelationships(this Submodel model, bool includeInherited = false)
        {
            // Gets all the relationships from the matching entities of submodel
            Dictionary<string, Relationship> relationships = model.Model.Relationships.Relationship.Where(item => model.Node.Any(entity => entity.Label == item.Source.Label) && model.Node.Any(entity => entity.Label == item.Target.Label)).ToDictionary(x => x.Name);

            if (includeInherited == false)
                return relationships.Select(x => x.Value).ToList();

            foreach (var node in model.Node)
            {
                Dictionary<RelationshipDirection, List<Relationship>> inheritedPropertyByDirection = node.Entity.GetInheritedRelationships(model);

                foreach (var item in inheritedPropertyByDirection[RelationshipDirection.In])
                {
                    if (relationships.ContainsKey(item.Name))
                        continue;

                    Relationship relationship = new Relationship(model.Model, (relationship)item.Xml.Clone());
                    relationship.Source.Label = node.Label;
                    relationships.Add(relationship.Name, relationship);
                    model.CreatedInheritedRelationships.Add(relationship);
                }
                foreach (var item in inheritedPropertyByDirection[RelationshipDirection.Out])
                {
                    if (relationships.ContainsKey(item.Name))
                        continue;
                    Relationship relationship = new Relationship(model.Model, (relationship)item.Xml.Clone());
                    relationship.Target.Label = node.Label;
                    relationships.Add(relationship.Name, relationship);
                    model.CreatedInheritedRelationships.Add(relationship);
                }
            }

            return relationships.Select(x => x.Value).ToList();
        }

        public static List<Entity> GetChildStaticEntities(this Entity entity)
        {
            return entity.Model.Entities.Entity.Where(item => item.Inherits == entity.Guid && item.IsStaticData).ToList();
        }

        public static List<Relationship> GetCurrentRelationshipsInGraph(this Entity entity, Submodel model)
        {
            List<Relationship> relationships = model.Relationships.Where(item => item.Source?.Label == entity.Label || item.Target?.Label == entity.Label).ToList();

            foreach (Relationship relationship in model.CreatedInheritedRelationships.Where(rel => rel.InEntity == entity.Label || rel.OutEntity == entity.Label))
            {
                relationships.Add(relationship);
            }

            return relationships;
        }

        public static void SetDataSource<T>(this ComboBox cbo, ref List<T> dataList, bool hasNone)
            where T : IComboBoxItem, new()
        {
            if (hasNone)
            {
                List<T> newList = new List<T>();
                newList.Add(new T() { Display = "--------", Value = null });

                newList.AddRange(dataList);
                dataList = newList;
            }

            cbo.DisplayMember = "Display";
            cbo.ValueMember = "Value";
            cbo.DataSource = dataList;
        }

        public static Form ShowLoader(this Form parent, bool disableControls = true)
        {
            Loader loader = new Loader();
            parent.Enabled = !disableControls;

            loader.Owner = parent;            

            //Control control = parent.GraphEditorControl;

            loader.Width = parent.Width;
            loader.Height = parent.Height;

            //int offSet = 30;
            loader.Left = parent.Bounds.Left;
            loader.Top = parent.Bounds.Top;
            loader.Padding = parent.Padding;

            loader.StartPosition = FormStartPosition.Manual;

            loader.Show();
            return loader;
        }

        public static void HideLoader(this Form parent, Form loader)
        {
            loader.Hide();
            parent.Enabled = true;
        }
    }
}