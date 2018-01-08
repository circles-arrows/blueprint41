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
                    this.multiPurposeButton.Text = "Export";
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
            txtResult.Clear();
            T4Template.Entities = lbEntities.SelectedItems.Cast<Entity>().ToList();
            T4Template.GenerationEnvironment = null;
            txtResult.Text = T4Template.TransformText();

            if (T4Template.Name == GenerationEnum.ApiDefinition)
            {
                string folder = AppDomain.CurrentDomain.BaseDirectory + @"..\..\Generation\ApiDefinition\";
                string file = Path.Combine(folder, string.Format("{0}Dto.xml", T4Template.CurrentEntity));
                using (FileStream fs = File.Create(file))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(txtResult.Text);
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
            //sb.Append("using System;");
            //sb.Append("using System.Text;");
            //sb.Append("using System.Linq;");
            //sb.Append("using System.Collections.Generic;");
            //sb.Append("using Blueprint41;");
            //sb.Append("using Blueprint41.Core;");
            //sb.Append("using Blueprint41.Neo4j.Persistence;");
            //sb.Append("using Blueprint41.Dynamic;");
            //
            //sb.Append("namespace Datastore");
            //sb.Append("    {");
            //sb.Append("        public partial class GbmModelTestV2 : DatastoreModel<GbmModelTestV2>");
            //sb.Append("        {");
            //sb.Append("            protected override void SubscribeEventHandlers()");
            //sb.Append("            {");
            //sb.Append("                throw new NotImplementedException();");
            //sb.Append("            }");
            //sb.Append("");
            //sb.Append("            protected override void InitializeEntities()");
            //sb.Append("            {");
            //sb.Append("                #region FunctionalID");

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.SelectedPath = fbd.SelectedPath;
                foreach (var entity in Entities)
                {
                    T4Template = new DatastoreModel();
                    T4Template.FunctionalIds = new List<FunctionalId>();
                    T4Template.Modeller = Model;
                    T4Template.Entities = new List<Entity>();
                    T4Template.Entities.Add(entity);
                    string output = T4Template.TransformText();

                    sb.Append(output);
                    sb.AppendLine();
                }

                sb.Append("         }");
                sb.Append("    }");
                sb.Append("}");
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.SelectedPath, string.Format("{0}.cs", "DatastoreModel"));
                using (FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(sb.ToString());
                    fs.Write(info, 0, info.Length);
                }
                MessageBox.Show("Entities Generated on " + SelectedPath, "Generated", MessageBoxButtons.OK);
            }
        }
    }
}
