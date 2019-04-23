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
            Dictionary<string, Submodel.NodeLocalType> nodeLookup = model.Node.ToDictionary(x => x.Label);

            var relationships = model.Model.Relationships.Relationship
                 .Where(item => nodeLookup.ContainsKey(item.Source.Label) && nodeLookup.ContainsKey(item.Target.Label)).ToList();

            if (includeInherited == false)
                return relationships;

            List<Relationship> inheritedRelationships = new List<Relationship>();

            foreach (var node in model.Node)
            {
                Dictionary<RelationshipDirection, List<Relationship>> inheritedPropertyByDirection = node.Entity.GetInheritedRelationships(model);

                foreach (var item in inheritedPropertyByDirection[RelationshipDirection.In])
                {
                    Relationship relationship = new Relationship(model.Model, (relationship)item.Xml.Clone());
                    relationship.Source.Label = node.Label;
                    inheritedRelationships.Add(relationship);
                    model.CreatedInheritedRelationships.Add(relationship);
                }
                foreach (var item in inheritedPropertyByDirection[RelationshipDirection.Out])
                {
                    Relationship relationship = new Relationship(model.Model, (relationship)item.Xml.Clone());
                    relationship.Target.Label = node.Label;
                    inheritedRelationships.Add(relationship);
                    model.CreatedInheritedRelationships.Add(relationship);
                }
            }

            relationships.AddRange(inheritedRelationships);
            return relationships;
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

        public static string ToPlural(this string Singular)
        {
            if (MatchAndReplace(ref Singular, "people", "", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "craft", "", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "tooth", "eeth", 4) == true) return Singular;
            if (MatchAndReplace(ref Singular, "goose", "eese", 4) == true) return Singular;
            if (MatchAndReplace(ref Singular, "trix", "ces", 1) == true) return Singular;
            if (MatchAndReplace(ref Singular, "mouse", "ice", 4) == true) return Singular;
            if (MatchAndReplace(ref Singular, "louse", "ice", 4) == true) return Singular;
            if (MatchAndReplace(ref Singular, "foot", "eet", 3) == true) return Singular;
            if (MatchAndReplace(ref Singular, "zoon", "a", 2) == true) return Singular;
            if (MatchAndReplace(ref Singular, "info", "s", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "eau", "x", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "ieu", "x", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "man", "en", 2) == true) return Singular;
            if (MatchAndReplace(ref Singular, "cis", "es", 2) == true) return Singular;
            if (MatchAndReplace(ref Singular, "sis", "es", 2) == true) return Singular;
            if (MatchAndReplace(ref Singular, "xis", "es", 2) == true) return Singular;
            if (MatchAndReplace(ref Singular, "ies", "", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "ch", "es", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "fe", "ves", 2) == true) return Singular;
            if (MatchAndReplace(ref Singular, "sh", "es", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "o", "es", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "f", "ves", 1) == true) return Singular;
            if (MatchAndReplace(ref Singular, "s", "es", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "x", "es", 0) == true) return Singular;
            if (MatchAndReplace(ref Singular, "y", "ies", 1) == true) return Singular;
            MatchAndReplace(ref Singular, "", "s", 0);
            return Singular;
        }

        private static bool MatchAndReplace(ref string Text, string Match, string Replace, int Amount)
        {
            if (Text.EndsWith(Match, System.StringComparison.CurrentCultureIgnoreCase) == true)
            {
                if (Text.Length > 0 && Text.Substring(Text.Length - 1) == Text.Substring(Text.Length - 1).ToUpper())
                    Replace = Replace.ToUpper();

                Text = Text.Substring(0, Text.Length - Amount) + Replace;
                return true;
            }
            return false;
        }

        public static Form ShowLoader(this MainForm parent, bool disableControls = true)
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