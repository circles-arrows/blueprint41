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
        private IEnumerable<Entity> Entities { get; set; }

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

        private void IterateSelectedEntities()
        {
            #region TODO Sort

            //List<Relationship> relationships = new List<Relationship>();
            //List<EntityTreeNode> checkedEntities = tvEntities.Nodes.Cast<EntityTreeNode>().ToList().Where(x => x.Checked == true).Select(x => x).ToList();

            //List<Entity> result = new List<Entity>();
            //result.AddRange(checkedEntities.Where(x => x.Entity.Inherits == null).Select(x => x.Entity));
            //checkedEntities.RemoveAll(entity => entity.Entity.Inherits == null);

            //// add the entity whose parent are already in the list
            //result.AddRange(checkedEntities.Where(entity => result.Any(parent => parent.Guid == entity.Entity.Inherits)).Select(x => x.Entity));
            //checkedEntities.RemoveAll(entity => result.Any(parent => parent.Guid == entity.Entity.Inherits));

            //// these entities are having dependencies
            //int count;
            //while (checkedEntities.Count > 0)
            //{
            //    count = checkedEntities.Count - 1;

            //    List<Entity> inheritedNodes = checkedEntities[count].InheritNode.Nodes.Cast<InheritedEntityTreeNode>().ToList().Where(x => x.Checked == true).Select(x => x.Entity).ToList();

            //    int inheritedCount = inheritedNodes.Count;

            //    result.AddRange(inheritedNodes.Where(x => x.Inherits == null));
            //    inheritedNodes.RemoveAll(entity => entity.Inherits == null);

            //    while (inheritedCount > 0)
            //    {
            //        var toadd = inheritedNodes.Where(entity => result.Any(parent => Guid.Parse(parent.Guid) == Guid.Parse(entity.Inherits))).ToList();

            //        result.AddRange(toadd);

            //        foreach (var a in toadd)
            //            inheritedNodes.Remove(a);

            //        inheritedCount = inheritedNodes.Count;
            //    }

            //    result.Add(checkedEntities[count].Entity);
            //    checkedEntities.RemoveAt(count);
            //} 
            #endregion

            List<Entity> checkedEntities = new List<Entity>();
            List<Relationship> relationships = new List<Relationship>();

            Dictionary<Guid, Entity> selectedEntitiesLookup = new Dictionary<Guid, Entity>();
            Dictionary<string, Relationship> relationshipLookup = new Dictionary<string, Relationship>();

            foreach (EntityTreeNode node in tvEntities.Nodes)
            {
                if (node.Checked == false)
                    continue;

                if (node.RelationshipNode.Checked)
                {
                    foreach (RelationshipTreeNode relNode in node.RelationshipNode.Nodes)
                    {
                        if (relNode.Checked)
                        {
                            if (relationshipLookup.ContainsKey(relNode.Relationship.Name) == false)
                            {
                                relationshipLookup.Add(relNode.Relationship.Name, relNode.Relationship);
                                relationships.Add(relNode.Relationship);
                            }
                        }
                    }
                }

                AddToCheckEntities(checkedEntities, selectedEntitiesLookup, node.Entity);

                if (node.InheritNode.Checked)
                {
                    foreach (InheritedEntityTreeNode inheritedNode in node.InheritNode.Nodes)
                    {
                        if (inheritedNode.Checked)
                            AddToCheckEntities(checkedEntities, selectedEntitiesLookup, inheritedNode.Entity);
                    }
                }
            }

            // This will rearrange the inherited entities to its proper place, this works for now
            foreach (Entity e in checkedEntities.ToList())
            {
                Entity currentE = e;

                do
                {
                    if (string.IsNullOrEmpty(currentE.Inherits))
                        break;

                    currentE = EntitiesLookUp[Guid.Parse(currentE.Inherits)];

                    // we only iterate to the selected nodes
                    if (selectedEntitiesLookup.ContainsKey(Guid.Parse(currentE.Guid)))
                        AddToCheckEntities(checkedEntities, selectedEntitiesLookup, currentE);

                } while (currentE != null);
            }

            GenerateEntitiesCode(checkedEntities, relationships);
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
            entities.Reverse();

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
                foreach (var entity in Entities)
                {
                    T4Template = new DatastoreModel();
                    T4Template.FunctionalIds = Model.FunctionalIds.FunctionalId.Where(fid => fid.Guid == entity.FunctionalId).ToList();
                    T4Template.Modeller = Model;
                    T4Template.Entities = new List<Entity>();
                    T4Template.Entities.Add(entity);
                    string output = T4Template.TransformText();

                    sb.Append(output);
                    sb.AppendLine();
                }

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
            string strings = "\".*?\"";
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
    }
}
