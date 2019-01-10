using Blueprint41.Modeller.Generation;
using Blueprint41.Modeller.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model = Blueprint41.Modeller.Schemas.Modeller;

namespace Blueprint41.Modeller
{
    public partial class CodeGeneration : Form
    {
        private bool isDoubleClicked;
        private TreeNode oldSelectNode;

        internal GenerationBase T4Template { get; set; }
        internal Model Model { get; set; }
        private string SelectedPath { get; set; }

        public Dictionary<Guid, Entity> EntitiesLookUp { get; private set; }

        public CodeGeneration()
        {
            InitializeComponent();
        }

        private void CodeGeneration_Load(object sender, EventArgs e)
        {
            T4Template.FunctionalIds = Model.FunctionalIds.FunctionalId.ToList();
            EntitiesLookUp = Model.Entities.Entity.ToDictionary(x => Guid.Parse(x.Guid));
            InitializableButton(this.T4Template.Name);
            InitializeEntityTree();
        }

        private void InitializeEntityTree()
        {
            foreach (Entity entity in EntitiesLookUp.Select(x => x.Value).OrderBy(x => x.Name).ToList())
                tvEntities.Nodes.Add(new EntityTreeNode(Model, entity));

            tvEntities.AfterCheck += TvEntities_AfterCheck;
            tvEntities.MouseUp += TvEntities_MouseUp;
            tvEntities.KeyUp += TvEntities_KeyUp;

            richTextBox.KeyUp += RichTextBox_KeyUp;
            richTextBox.MouseUp += RichTextBox_MouseUp;
        }

        private void RichTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            // Point where the mouse is clicked.
            Point p = new Point(e.X, e.Y);
            cmsCopy.Show(richTextBox, p);
        }

        private void RichTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                richTextBox.SelectAll();
        }

        private void TvEntities_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                btnSelectAll_Click(sender, e);
        }

        private void TvEntities_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            // Point where the mouse is clicked.
            Point p = new Point(e.X, e.Y);

            // Get the node that the user has clicked.
            TreeNode node = tvEntities.GetNodeAt(p);
            if (node != null)
            {
                // Select the node the user has clicked.
                // The node appears selected until the menu is displayed on the screen.
                oldSelectNode = tvEntities.SelectedNode;
                tvEntities.SelectedNode = node;

                cmsSelect.Show(tvEntities, p);
            }
        }

        private void TvEntities_AfterCheck(object sender, TreeViewEventArgs e)
        {
            BeforeSelection();
            SelectDeselectNode(e.Node, e.Node.Checked, true);
            AfterSelection();
        }

        private void LoadChildNodes(EntityTreeNode node, bool expand = true)
        {
            if (node.Loaded == false)
            {
                node.LoadInheritance();
                node.LoadRelationship();

                if (expand)
                    node.Expand();
            }
        }

        private List<Entity> SortDependencyEntities(List<Entity> entities, List<Entity> parentEntities)
        {
            parentEntities.RemoveAll(entity => entities.Any(x => x.Guid == entity.Guid));

            List<Entity> result = new List<Entity>();
            result.AddRange(entities.Where(x => x.Inherits == null));
            entities.RemoveAll(entity => entity.Inherits == null);

            if (result.Count > 0)
            {
                result.AddRange(parentEntities.Where(x => result.Any(added => added.Guid == x.Inherits)));
                parentEntities.RemoveAll(entity => result.Any(x => x.Guid == entity.Guid));
            }
            else
            {
                result.AddRange(parentEntities.Where(x => x.Inherits == null));
                parentEntities.RemoveAll(entity => entity.Inherits == null);
            }

            int count;
            // remaining inherited parent
            while (parentEntities.Count > 0)
            {
                count = parentEntities.Count;
                result.AddRange(parentEntities.Where(x => result.Any(added => added.Guid == x.Inherits)));
                parentEntities.RemoveAll(entity => result.Any(added => added.Guid == entity.Guid));

                if (count == parentEntities.Count)
                {
                    //  the user may have omitted to other parent, so add it here
                    result.AddRange(parentEntities.Where(x => result.Any(added => added.Guid == x.Guid) == false));
                    break;
                }
            }

            // add the entity whose parent are already in the list
            result.AddRange(entities.Where(entity => result.Any(added => added.Guid == entity.Inherits)));
            entities.RemoveAll(entity => result.Any(added => added.Guid == entity.Guid));

            // add the remaining entities
            while (entities.Count > 0)
            {
                count = entities.Count;
                result.AddRange(entities.Where(x => result.Any(added => added.Guid == x.Inherits)));
                entities.RemoveAll(entity => result.Any(added => added.Guid == entity.Guid));

                if (count == entities.Count)
                {
                    //  the user may have omitted to other parent, so add it here
                    result.AddRange(entities.Where(x => result.Any(added => added.Guid == x.Guid) == false));
                    break;
                }
            }

            return result;
        }

        private void IterateSelectedEntities()
        {
            List<Relationship> relationships = new List<Relationship>();
            List<EntityTreeNode> checkedEntities = tvEntities.Nodes.Cast<EntityTreeNode>().ToList().Where(x => x.Checked == true).Select(x => x).ToList();

            List<Entity> checkedParentEntities = new List<Entity>();
            checkedEntities.ForEach((node) =>
            {
                checkedParentEntities.AddRange(node.InheritNode.Nodes.Cast<InheritedEntityTreeNode>().ToList().Where(entity => entity.Checked == true && checkedParentEntities.Any(added => added.Guid == entity.Entity.Guid) == false).Select(x => x.Entity));
                relationships.AddRange(node.RelationshipNode.Nodes.Cast<RelationshipTreeNode>().ToList().Where(rel => rel.Checked == true && relationships.Any(added => added.Name == rel.Relationship.Name) == false).Select(x => x.Relationship));
            });

            var result = SortDependencyEntities(checkedEntities.Select(x => x.Entity).ToList(), checkedParentEntities);
            GenerateEntitiesCode(result, relationships);
            EnableDisableButtons();
        }

        private void EnableDisableButtons()
        {
            bool hasGeneratedCodes = !string.IsNullOrEmpty(richTextBox.Text.Trim());
            btnCopyClipboard.Enabled = hasGeneratedCodes;
            multiPurposeButton.Enabled = hasGeneratedCodes;
        }

        private void AddToCheckEntities(List<Entity> checkedEntities, Dictionary<Guid, Entity> selectedEntitiesLookup, Entity entity)
        {
            if (entity == null)
                return;

            if (selectedEntitiesLookup.ContainsKey(Guid.Parse(entity.Guid)) == false)
            {
                selectedEntitiesLookup.Add(Guid.Parse(entity.Guid), entity);
                checkedEntities.Add(entity);
            }
            else
            {
                // move the entity to the last entry
                checkedEntities.Remove(entity);
                checkedEntities.Add(entity);
            }
        }

        private void InitializableButton(GenerationEnum generation)
        {
            switch (generation)
            {
                case GenerationEnum.ApiDefinition:
                    this.multiPurposeButton.Text = "Open Folder";
                    this.multiPurposeButton.Click += new System.EventHandler(this.btnOpenFolder_Click);
                    break;
                case GenerationEnum.DataStoreModel:
                    this.multiPurposeButton.Text = "Export All";
                    this.multiPurposeButton.Click += new System.EventHandler(this.btnExport_Click);
                    break;
                case GenerationEnum.StaticData:
                    this.multiPurposeButton.Visible = false;
                    this.multiPurposeButton.Text = "";
                    break;
                default:
                    break;
            }
        }

        private void GenerateEntitiesCode(List<Entity> entities, List<Relationship> relationships)
        {
            richTextBox.Clear();

            Dictionary<Guid, Entity> selectedEntities = entities.ToDictionary(x => Guid.Parse(x.Guid));
            Dictionary<Guid, Entity> functionalIdByentities = selectedEntities.Where(x => string.IsNullOrEmpty(x.Value.FunctionalId) == false).GroupBy(x => x.Value.FunctionalId).Select(x => x.FirstOrDefault()).ToDictionary(x => Guid.Parse(x.Value.FunctionalId), y => y.Value);

            T4Template.FunctionalIds = Model.FunctionalIds.FunctionalId.Where(x => functionalIdByentities.ContainsKey(Guid.Parse(x.Guid)) || x.IsDefault == true).ToList();
            T4Template.Entities = entities;
            T4Template.Relationships = relationships;

            T4Template.GenerationEnvironment = null;
            richTextBox.Text = T4Template.TransformText();
            DoStyle();
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory + @"..\..\Generation\ApiDefinition\");
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            List<Relationship> relationships = new List<Relationship>();
            List<EntityTreeNode> entities = tvEntities.Nodes.Cast<EntityTreeNode>().ToList().Select(x => x).ToList();

            List<Entity> parentEntities = new List<Entity>();
            entities.ForEach((node) =>
            {
                if (node.Loaded == false)
                {
                    node.LoadInheritance();
                    node.LoadRelationship();
                }

                parentEntities.AddRange(node.InheritNode.Nodes.Cast<InheritedEntityTreeNode>().ToList().Where(entity => parentEntities.Any(added => added.Guid == entity.Entity.Guid) == false).Select(x => x.Entity));
                relationships.AddRange(node.RelationshipNode.Nodes.Cast<RelationshipTreeNode>().ToList().Where(rel => relationships.Any(added => added.Name == rel.Relationship.Name) == false).Select(x => x.Relationship));
            });

            Dictionary<Guid, Entity> functionalIdByentities = EntitiesLookUp.Where(x => string.IsNullOrEmpty(x.Value.FunctionalId) == false).GroupBy(x => x.Value.FunctionalId).Select(x => x.FirstOrDefault()).ToDictionary(x => Guid.Parse(x.Value.FunctionalId), y => y.Value);
            List<Entity> sortedResult = SortDependencyEntities(entities.Select(x => x.Entity).ToList(), parentEntities);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using Blueprint41;");
            sb.AppendLine("using Blueprint41.Core;");
            sb.AppendLine("using Blueprint41.Neo4j.Persistence;");
            sb.AppendLine("using Blueprint41.Dynamic;");
            sb.AppendLine("");
            sb.AppendLine("namespace Datastore");
            sb.AppendLine("{");
            sb.AppendLine("    public partial class GbmModelTestV2 : DatastoreModel<GbmModelTestV2>");
            sb.AppendLine("    {");
            sb.AppendLine("        protected override void SubscribeEventHandlers()");
            sb.AppendLine("        {");
            sb.AppendLine("             throw new NotImplementedException();");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        protected override void InitializeEntities()");
            sb.AppendLine("        {");

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.SelectedPath = fbd.SelectedPath;

                T4Template = new DatastoreModel();
                T4Template.FunctionalIds = Model.FunctionalIds.FunctionalId.Where(x => functionalIdByentities.ContainsKey(Guid.Parse(x.Guid)) || x.IsDefault == true).ToList();
                T4Template.Modeller = Model;
                T4Template.Entities = sortedResult;
                T4Template.Relationships = relationships;
                string output = T4Template.TransformText();

                sb.Append(output);
                sb.AppendLine();

                sb.AppendLine("        }");
                sb.AppendLine("    }");
                sb.AppendLine("}");

                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.SelectedPath, string.Format("{0}.cs", "DatastoreModel"));
                using (FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(sb.ToString());
                    fs.Write(info, 0, info.Length);
                }
                MessageBox.Show("Entities Generated on " + SelectedPath, "Generated", MessageBoxButtons.OK);
            }
        }

        private void DoStyle()
        {
            int originalIndex = 0;
            int originalLength = richTextBox.Text.Length;

            richTextBox.SelectionStart = originalIndex;
            richTextBox.SelectionLength = originalLength;
            richTextBox.SelectionColor = Color.Black;

            // getting keywords/functions
            string keywords = @"\b(public|private|partial|static|namespace|class|using|void|foreach|in|var|new|typeof|string|int|false|true|double|float|null|bool|decimal)\b";
            MatchCollection keywordMatches = Regex.Matches(richTextBox.Text, keywords);

            foreach (Match m in keywordMatches)
            {
                richTextBox.SelectionStart = m.Index;
                richTextBox.SelectionLength = m.Length;
                richTextBox.SelectionColor = Color.Blue;
            }

            // getting types/classes from the text 
            string types = @"\b(Console|DateTime|NotSupportedException|Dictionary|Entity|FunctionalId|Relationship|Guid)\b";
            MatchCollection typeMatches = Regex.Matches(richTextBox.Text, types);

            foreach (Match m in typeMatches)
            {
                richTextBox.SelectionStart = m.Index;
                richTextBox.SelectionLength = m.Length;
                richTextBox.SelectionColor = Color.DarkCyan;
            }

            // getting comments (inline or multiline)
            string comments = @"(\/\/.+?$|\/\*.+?\*\/)";
            MatchCollection commentMatches = Regex.Matches(richTextBox.Text, comments, RegexOptions.Multiline);

            foreach (Match m in commentMatches)
            {
                richTextBox.SelectionStart = m.Index;
                richTextBox.SelectionLength = m.Length;
                richTextBox.SelectionColor = Color.Green;
            }

            string groupedTypes = @"(\u0023region+.*|\u0023endregion)\b";
            MatchCollection groupedMatches = Regex.Matches(richTextBox.Text, groupedTypes);

            foreach (Match m in groupedMatches)
            {
                richTextBox.SelectionStart = m.Index;
                richTextBox.SelectionLength = m.Length;
                richTextBox.SelectionColor = Color.DarkGray;
            }

            // getting strings
            string strings = "\"(?:[^\"\\\\]|\\\\.)*\"";
            MatchCollection stringMatches = Regex.Matches(richTextBox.Text, strings);

            foreach (Match m in stringMatches)
            {
                richTextBox.SelectionStart = m.Index;
                richTextBox.SelectionLength = m.Length;
                richTextBox.SelectionColor = Color.Brown;
            }
        }

        private void btnCopyClipboard_Click(object sender, EventArgs e)
        {
                Clipboard.SetText(richTextBox.Text);
                MessageBox.Show("Contents copied to clipboard", "Code Generation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            LoadUnloadEntities(true, false);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            LoadUnloadEntities(false, false);
        }

        private void RecursiveCheckNode(TreeNode node, bool check)
        {
            foreach (TreeNode childNode in node.Nodes)
            {
                if (childNode.Nodes.Count > 0)
                    RecursiveCheckNode(childNode, check);

                childNode.Checked = check;
            }

            node.Checked = check;
        }

        private void LoadUnloadEntities(bool check, bool expand)
        {
            BeforeSelection();

            foreach (EntityTreeNode node in tvEntities.Nodes)
                SelectDeselectNode(node, check, expand);

            AfterSelection();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeforeSelection();
            SelectDeselectNode(tvEntities.SelectedNode, true, true);
            AfterSelection();
        }

        private void deselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeforeSelection();
            SelectDeselectNode(tvEntities.SelectedNode, false, false);
            AfterSelection();
        }

        private void SelectDeselectNode(TreeNode node, bool check, bool expand)
        {
            RecursiveCheckNode(node, check);

            if (node is EntityTreeNode entityTree)
                LoadChildNodes(entityTree, expand);
        }

        private void BeforeSelection()
        {
            tvEntities.AfterCheck -= TvEntities_AfterCheck;
        }

        private void AfterSelection()
        {
            IterateSelectedEntities();
            tvEntities.AfterCheck += TvEntities_AfterCheck;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox.SelectedText);
        }
    }
}
