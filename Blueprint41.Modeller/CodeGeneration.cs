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
        internal GenerationBase T4Template { get; set; }
        internal Model Model { get; set; }
        public ListBox EntitiesListBox { get { return lbEntities; } }
        private string SelectedPath { get; set; }
        private IEnumerable<Entity> Entities { get; set; }

        public CodeGeneration()
        {
            InitializeComponent();
        }

        private void CodeGeneration_Load(object sender, EventArgs e)
        {
            T4Template.FunctionalIds = Model.FunctionalIds.FunctionalId.ToList();
            Entities = Model.Entities.Entity.OrderBy(x => x.Name).ToList();
            lbEntities.DataSource = Entities;
            lbEntities.DisplayMember = "name";
            InitializableButton(this.T4Template.Name);
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

        private void lbEntities_SelectedValueChanged(object sender, EventArgs e)
        {
            richTextBox.Clear();
            T4Template.Entities = lbEntities.SelectedItems.Cast<Entity>().ToList();
            T4Template.GenerationEnvironment = null;
            richTextBox.Text = T4Template.TransformText();
            DoStyle();

            if (T4Template.Name == GenerationEnum.ApiDefinition)
            {
                string folder = AppDomain.CurrentDomain.BaseDirectory + @"..\..\Generation\ApiDefinition\";
                string file = Path.Combine(folder, string.Format("{0}Dto.xml", T4Template.CurrentEntity));
                using (FileStream fs = File.Create(file))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(richTextBox.Text);
                    fs.Write(info, 0, info.Length);
                }
            }
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
            string types = @"\b(Console|DateTime|NotSupportedException|Dictionary|Entity|FunctionalId|Relationship)\b";
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

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            lbEntities.SelectedValueChanged -= lbEntities_SelectedValueChanged;

            for (int i = 0; i < lbEntities.Items.Count; i++)
                lbEntities.SetSelected(i, cbSelectAll.Checked);

            lbEntities.SelectedValueChanged += lbEntities_SelectedValueChanged;
            lbEntities_SelectedValueChanged(lbEntities, EventArgs.Empty);
        }
    }
}
