using Blueprint41.Modeller.Schemas;
using Microsoft.Msagl.Drawing;
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
using Blueprint41.Modeller.Generation;
using System.Xml;
using System.Reflection;
using Microsoft.Win32;

namespace Blueprint41.Modeller
{
    public partial class MainForm : Form
    {
        private bool showLabels = false;
        private bool showInherited = false;

        private const string NEWSUBMODEL = "New...";
        //private const string DATASTORE_MODEL_REGISTRY_KEY = "DatastoreModelDll";
        //private const string DATASTORE_MODEL_REGISTRY_SUBKEY = @"SOFTWARE\Blueprint41.Modeller";
        public Model Model { get; private set; }
        private string m_StoragePath = null;

        public string StoragePath
        {
            set
            {
                this.m_StoragePath = value;
            }
            get
            {
                if (this.m_StoragePath == null)
                    return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Xml\", "modeller.xml"));

                return this.m_StoragePath;
            }
        }

        private Submodel MainSubmodel
        {
            get
            {
                return Model.Submodels.Submodel.Where(sub => sub.Name == "Main Model").SingleOrDefault();
            }
        }

        internal NodeTypeEntry NewEntity { get; private set; }
        internal Submodel.NodeLocalType SelectedNode { get; private set; }

        public MainForm()
        {
            if (!string.IsNullOrEmpty(RegistryHandler.LastOpenedFile))
                StoragePath = RegistryHandler.LastOpenedFile;

            InitializeComponent();

            if (DatastoreModelComparer.Instance == null)
                toolStripDropDownButton3.Visible = false;

            Initialize();
        }

        private void Initialize()
        {
            if (File.Exists(StoragePath))
                Model = new Model(StoragePath);
            else
                Model = new Model();

            Model.ShowRelationshipLabels = false;
            Model.ShowInheritedRelationships = false;
        }

        int idcounter = 0;
        private int _splitterDistance;

        internal string GetNewId(NodeTypeEntry nte)
        {
            string ret = nte.DefaultLabel + idcounter++;
            if (Model.Entities.Entity.Any(item => item.Label == ret))
                return GetNewId(nte);
            return ret;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ReloadForm();
            AddNewEntitiesToSubModel("Main Model");
            _splitterDistance = splitContainer.SplitterDistance;
            SizeChanged += MainForm_SizeChanged;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            _splitterDistance = splitContainer.SplitterDistance;
        }

        private void AddNewEntitiesToSubModel(string submodelName)
        {
            Submodel subModel = Model.Submodels.Submodel.FirstOrDefault(submodel => submodel.Name == submodelName);
            if (subModel == null)
                throw new ArgumentNullException($"Submodel '{submodelName}' does not exist in the current model.");

            IEnumerable<string> mainModelNodeLabels = subModel.Node.Select(node => node.Label).OrderBy(label => label);
            IEnumerable<string> entityLabels = Model.Entities.Entity.Select(entity => entity.Label).OrderBy(label => label);

            List<Entity> entitiesToAddToMainModel = new List<Entity>();

            foreach (var entityLabel in entityLabels.Except(mainModelNodeLabels))
                entitiesToAddToMainModel.Add(Model.Entities.Entity.FirstOrDefault(label => label.Label == entityLabel));

            subModel.AddEntities(entitiesToAddToMainModel, 0, 0);

            Model.CaptureCoordinates();
            Model.Save(StoragePath);

            if (this.entityEditor.Enabled)
                entityEditor.Reload();

            ReloadForm();
        }

        private void ReloadForm()
        {
            if (Model.DisplayedSubmodel == null)
            {
                if (Model.Submodels.Submodel.Count == 0)
                {
                    Submodel main = new Submodel(Model);
                    main.Name = "Main Model";
                    Model.Submodels.Submodel.Add(main);
                }

                Model.DisplayedSubmodel = Model.Submodels.Submodel.Where(m => m.Name == RegistryHandler.LastOpenedSubmodel).FirstOrDefault();

                if (Model.DisplayedSubmodel == null)
                    Model.DisplayedSubmodel = Model.Submodels.Submodel[0];
            }

            FillSubmodelComboBox(Model.DisplayedSubmodel);

            if (NewEntity == null)
            {
                NewEntity = new Modeller.NodeTypeEntry("New Entity", Shape.Circle, Microsoft.Msagl.Drawing.Color.Transparent, Microsoft.Msagl.Drawing.Color.Black, 10, null, "Entity");
                graphEditor.NodeTypes.Add(NewEntity);
            }

            Model.GraphEditor = graphEditor;

            graphEditor.ShowContextMenuOnRightClick = true;

            btnShowLabels.Checked = Model.ShowRelationshipLabels;
            btnShowInheritedRelationships.Checked = Model.ShowInheritedRelationships;

            idcounter = 0;
        }

        private void FillSubmodelComboBox(Submodel selectedSub = null)
        {
            cmbSubmodels.Items.Clear();
            cmbSubmodels.Items.Add(NEWSUBMODEL);
            foreach (Submodel submodel in Model.Submodels.Submodel)
            {
                cmbSubmodels.Items.Add(submodel);
            }

            if (selectedSub == null)
                cmbSubmodels.SelectedIndex = 1;
            else
                cmbSubmodels.SelectedItem = selectedSub;
        }

        private void CloseNodeEditor()
        {
            entityEditor.CloseEditor();
        }

        private void CloseEdgeEditor()
        {
            //txtNodeLabel.Enabled = false;
            //txtNodeLabel.Text = "";
        }

        private void graphEditor_AddDisplayedEntities(object sender, EventArgs e)
        {
            ManageSubmodelForm form = new Modeller.ManageSubmodelForm(Model.DisplayedSubmodel);
            if (form.ShowDialog() == DialogResult.OK)
            {
                Model.RebindControl();
                RefreshNodeCombobox();
                FillSubmodelComboBox(form.Submodel);
            }
        }

        private void graphEditor_SelectedNodeChanged(object sender, NodeEventArgs e)
        {
            if (!(e.Node.UserData is Submodel.NodeLocalType))
                throw new NotSupportedException();

            entityEditor.Show((e.Node.UserData as Submodel.NodeLocalType).Entity, Model);
            DefaultOrExpandPropertiesWidth(true);
        }

        private void graphEditor_SelectedEdgeChanged(object sender, EdgeEventArgs e)
        {
            //txtNodeLabel.Enabled = true;
            //txtNodeLabel.Text = e.Edge.LabelText;
        }

        private void graphEditor_NoSelectionEvent(object sender, EventArgs e)
        {
            CloseNodeEditor();
            CloseEdgeEditor();
            RefreshNodeCombobox();
            DefaultOrExpandPropertiesWidth(false);
        }

        private void entityEditor_ApplyChangesButtonClicked(object sender, EventArgs e)
        {
            CloseNodeEditor();
            graphEditor.InvalidateViewer();
        }

        private void graphEditor_NodeInsertedByUser(object sender, NodeEventArgs e)
        {
            Submodel.NodeLocalType model = e?.Node?.UserData as Submodel.NodeLocalType;
            if (model == null)
            {
                Entity entity = new Schemas.Entity(Model);
                entity.Label = GetNewId(NewEntity);
                entity.Name = entity.Label;
                Model.Entities.Entity.Add(entity);

                model = new Submodel.NodeLocalType(Model);
                model.Label = entity.Label;
                model.Xcoordinate = e.Center.X;
                model.Ycoordinate = e.Center.Y;
                model.EntityGuid = entity.Guid;

                if (Model.DisplayedSubmodel != MainSubmodel)
                    MainSubmodel.Node.Add(model.Clone());

                Model.DisplayedSubmodel.Node.Add(model);

                RefreshNodeCombobox();
            }

            // Auto select newly created entity
            entityEditor.Show(model.Entity, Model);
        }

        private void graphEditor_InsertRelationship(object sender, InsertRelationshipEventArgs e)
        {
            Submodel.NodeLocalType source = e.SourceNode.UserData as Submodel.NodeLocalType;
            Submodel.NodeLocalType target = e.TargetNode.UserData as Submodel.NodeLocalType;

            Relationship relationship = new Schemas.Relationship(Model);
            relationship.InEntity = source.Label;
            relationship.OutEntity = target.Label;

            string neo4jType;
            string relationshipName;

            switch (e.SourcePropertyType)
            {
                case PropertyType.None:
                    throw new NotSupportedException(string.Format("Source Property Type {0} is currently not supported", e.SourcePropertyType));
                case PropertyType.Lookup:
                    switch (e.TargetPropertyType)
                    {
                        case PropertyType.None:
                            relationshipName = string.Concat(source.Label.ToUpper(), "_HAS_", target.Label.ToUpper());
                            neo4jType = string.Concat("HAS_", target.Label.ToUpper());

                            relationship.InProperty = target.Entity.Name;
                            relationship.InPropertyType = PropertyType.Lookup.ToString();
                            break;
                        case PropertyType.Lookup:
                            relationshipName = string.Concat(source.Label.ToUpper(), "_IS_", target.Label.ToUpper());
                            neo4jType = string.Concat("IS_", target.Label.ToUpper());

                            relationship.InProperty = target.Entity.Name;
                            relationship.InPropertyType = PropertyType.Lookup.ToString();

                            relationship.OutProperty = source.Entity.Name;
                            relationship.OutPropertyType = PropertyType.Lookup.ToString();
                            break;
                        case PropertyType.Collection:
                            relationshipName = string.Concat(source.Label.ToUpper(), "_VALID_FOR_", target.Label.ToUpper());
                            neo4jType = string.Concat("VALID_FOR_", target.Label.ToUpper());

                            relationship.InProperty = target.Entity.Name;
                            relationship.InPropertyType = PropertyType.Lookup.ToString();

                            relationship.OutProperty = ToPlural(source.Entity.Name);
                            relationship.OutPropertyType = PropertyType.Collection.ToString();
                            break;
                        default:
                            throw new NotSupportedException(string.Format("Target Property Type value {0} is currently not supported", e.TargetPropertyType));
                    }
                    break;
                case PropertyType.Collection:
                    switch (e.TargetPropertyType)
                    {
                        case PropertyType.None:
                            relationshipName = string.Concat(source.Label.ToUpper(), "_CONTAINS_", target.Label.ToUpper());
                            neo4jType = string.Concat("CONTAINS_", target.Label.ToUpper());

                            relationship.InProperty = ToPlural(target.Entity.Name);
                            relationship.InPropertyType = PropertyType.Collection.ToString();
                            break;
                        case PropertyType.Lookup:
                            relationshipName = string.Concat(source.Label.ToUpper(), "_CONTAINS_", target.Label.ToUpper());
                            neo4jType = string.Concat("CONTAINS_", target.Label.ToUpper());

                            relationship.InProperty = ToPlural(target.Entity.Name);
                            relationship.InPropertyType = PropertyType.Collection.ToString();

                            relationship.OutProperty = source.Entity.Name;
                            relationship.OutPropertyType = PropertyType.Lookup.ToString();
                            break;
                        case PropertyType.Collection:
                            relationshipName = string.Concat(source.Label.ToUpper(), "_CONTAINS_", target.Label.ToUpper());
                            neo4jType = string.Concat("CONTAINS_", target.Label.ToUpper());

                            relationship.InProperty = ToPlural(target.Entity.Name);
                            relationship.InPropertyType = PropertyType.Collection.ToString();

                            relationship.OutProperty = ToPlural(source.Entity.Name);
                            relationship.OutPropertyType = PropertyType.Collection.ToString();
                            break;
                        default:
                            throw new NotSupportedException(string.Format("Target Property Type value {0} is currently not supported", e.TargetPropertyType));
                    }
                    break;
                default:
                    throw new NotSupportedException(string.Format("Source Property Type {0} is currently not supported", e.SourcePropertyType));
            }

            int count = 0;
            while (Model.Relationships.Relationship.Any(item => item.Name == relationshipName))
            {
                relationshipName += count++;
            }

            relationship.Name = relationshipName;
            relationship.Type = neo4jType;
            relationship.Target.ReferenceGuid = target.Entity.Guid;
            relationship.Source.ReferenceGuid = source.Entity.Guid;

            Model.Relationships.Relationship.Add(relationship);
        }

        public string ToPlural(string Singular)
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

        private bool MatchAndReplace(ref string Text, string Match, string Replace, int Amount)
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

        private void graphEditor_RemoveEdgeFromDiagramChanged(object sender, EdgeEventArgs e)
        {
            Relationship model = e?.Edge?.UserData as Relationship;
            if (model != null)
                model.DeleteEdge();
        }

        private void graphEditor_RemoveEdgeFromStorageChanged(object sender, EdgeEventArgs e)
        {
            Relationship model = e?.Edge?.UserData as Relationship;
            DialogResult dialogResult = MessageBox.Show($"Are you sure you want to delete the relationship '{model.Source.Label}->[{model.Name}]->{model.Target.Label}' from storage?", "WARNING!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (model != null)
                {
                    Model.Relationships.Relationship.Remove(model);
                    if (this.entityEditor.Enabled)
                    {
                        this.entityEditor.UpdateRelationshipGridView();
                    }
                }
            }
        }

        private void graphEditor_RemoveNodeFromDiagramChanged(object sender, NodeEventArgs e)
        {
            if (graphEditor.SelectedEntities.Count > 0)
            {
                foreach (IViewerNode selectedNode in graphEditor.SelectedEntities)
                {
                    Submodel.NodeLocalType model = selectedNode.DrawingObject.UserData as Submodel.NodeLocalType;
                    if (model != null)
                    {
                        Model.DisplayedSubmodel.Node.Remove(model);
                    }
                }
                RefreshNodeCombobox();
                entityEditor.CloseEditor();
            }
        }

        private void graphEditor_RemoveNodeFromStorageChanged(object sender, NodeEventArgs e)
        {
            if (graphEditor.SelectedEntities.Count > 0)
            {
                string ending = graphEditor.SelectedEntities.Count > 1 ? "ies" : "y";
                DialogResult dialogResult = MessageBox.Show($"Are you sure you want to delete the selected entit{ending} from storage?", "WARNING!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    foreach (IViewerNode selectedNode in graphEditor.SelectedEntities)
                    {
                        Submodel.NodeLocalType node = selectedNode.DrawingObject.UserData as Submodel.NodeLocalType;
                        DeleteEntityFromStorage(node);
                    }

                    RefreshNodeCombobox();
                    entityEditor.CloseEditor();
                }
            }
        }

        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Width:{0}", splitContainer.Panel2.Width));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Model.CaptureCoordinates();
            Model.Save(StoragePath);
            MessageBox.Show("Diagram saved successfully.", "Confirmation", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (this.entityEditor.Enabled)
                entityEditor.Reload();
        }

        private void btnShowLabels_Click(object sender, EventArgs e)
        {
            showLabels = btnShowLabels.Checked;
            showLabelsToolStripMenuItem.Checked = btnShowLabels.Checked;
            Model.ShowRelationshipLabels = btnShowLabels.Checked;
        }

        private void cmbSubmodels_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedNode = null;

            if (cmbSubmodels.Text == NEWSUBMODEL)
            {
                ManageSubmodelForm form = new Modeller.ManageSubmodelForm(Model);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Model.Submodels.Submodel.Add(form.Submodel);
                    FillSubmodelComboBox();
                    cmbSubmodels.SelectedItem = form.Submodel;
                }
            }
            else
                Model.DisplayedSubmodel = cmbSubmodels.SelectedItem as Submodel;

            RefreshNodeCombobox();
        }

        private void cmbNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNodes.SelectedItem == null)
                return;

            if (SelectedNode != null)
            {
                SelectedNode.RemoveHighlight();
                SelectedNode = null;
            }

            if (cmbNodes.SelectedIndex == 0)
                return;

            SelectedNode = cmbNodes.SelectedItem as Submodel.NodeLocalType;
            SelectedNode.Highlight();
        }

        private void generateCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeGeneration codeGeneration = new CodeGeneration();
            codeGeneration.Size = this.Size;
            codeGeneration.T4Template = new DatastoreModel();
            codeGeneration.T4Template.Name = GenerationEnum.DataStoreModel;
            codeGeneration.Model = Model;
            codeGeneration.T4Template.Modeller = Model;
            codeGeneration.T4Template.FunctionalIds = new List<FunctionalId>();
            codeGeneration.ShowDialog();
        }

        private void staticDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeGeneration codeGeneration = new CodeGeneration();
            codeGeneration.Size = this.Size;
            codeGeneration.EntitiesListBox.SelectionMode = SelectionMode.One;
            codeGeneration.T4Template = new Blueprint41.Modeller.Generation.StaticData();
            codeGeneration.T4Template.Name = GenerationEnum.StaticData;
            codeGeneration.Model = Model;
            codeGeneration.T4Template.Modeller = Model;
            codeGeneration.ShowDialog();
        }

        private void aPIDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeGeneration codeGeneration = new CodeGeneration();
            codeGeneration.Size = this.Size;
            codeGeneration.T4Template = new ApiDefinition();
            codeGeneration.T4Template.Name = GenerationEnum.ApiDefinition;
            codeGeneration.Model = Model;
            codeGeneration.T4Template.Modeller = Model;
            codeGeneration.ShowDialog();
        }

        private void btnShowInheritedRelationships_Click(object sender, EventArgs e)
        {
            showInherited = btnShowInheritedRelationships.Checked;
            showInheritedRelationshipsToolStripMenuItem.Checked = btnShowInheritedRelationships.Checked;
            Model.ShowInheritedRelationships = btnShowInheritedRelationships.Checked;
        }

        private void DeleteEntityFromStorage(Submodel.NodeLocalType node)
        {
            if (node != null)
            {
                Model.Entities.Entity.Remove(node.Entity);

                // delete in sub models
                foreach (Submodel subModel in Model.Submodels.Submodel)
                {
                    List<Submodel.NodeLocalType> tempSubModeNodes = new List<Submodel.NodeLocalType>();
                    tempSubModeNodes.AddRange(subModel.Node);
                    foreach (Submodel.NodeLocalType subModelNode in tempSubModeNodes)
                    {
                        if (subModelNode.Label == node.Label)
                            subModel.Node.Remove(subModelNode);
                    }
                }

                // delete related relationships
                List<Relationship> tempRelationships = new List<Relationship>();
                tempRelationships.AddRange(Model.Relationships.Relationship);
                foreach (Relationship relationship in tempRelationships)
                {
                    if (relationship.Target.Label == node.Label || relationship.Source.Label == node.Label)
                        Model.Relationships.Relationship.Remove(relationship);
                }
            }
        }

        private void menuFielNew_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "XML Document (*.xml)|*.xml";
            saveDialog.DefaultExt = "xml";
            DialogResult result = saveDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                this.StoragePath = saveDialog.FileName;
                FileStream newFile = File.Create(this.StoragePath);
                newFile.Close();

                // Create the XmlDocument.
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><modeller xmlns=\"http://xirqlz.com/2016/Blueprint/Modeller\"></modeller>");

                // Save the document to a file and auto-indent the output.
                XmlTextWriter writer = new XmlTextWriter(this.StoragePath, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                doc.Save(writer);

                this.Model = new Model();
                ReloadForm();
            }

        }

        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            DialogResult result = openDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                StoragePath = openDialog.FileName;
                Initialize();
                Model.ShowRelationshipLabels = showLabels;
                Model.ShowInheritedRelationships = showInherited;
                ReloadForm();
            }
        }

        public void RefreshNodeCombobox()
        {
            cmbNodes.Items.Clear();
            cmbNodes.Items.Add("No Selection");
            foreach (var item in Model.DisplayedSubmodel.Node.OrderBy(item => item.Label))
            {
                item.RemoveHighlight();
                cmbNodes.Items.Add(item);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            RegistryHandler.LastOpenedSubmodel = Model.DisplayedSubmodel.Name;
            RegistryHandler.LastOpenedFile = Path.GetFullPath(this.StoragePath);
        }

        private void btnManageFunctionalIds_Click(object sender, EventArgs e)
        {
            ManageFunctionalId functionalIdForm = new ManageFunctionalId(Model);
            functionalIdForm.ShowDialog(this);
        }

        private void toolStripDropDownButtonGenerateUpgradeScript_Click(object sender, EventArgs e)
        {
            if (DatastoreModelComparer.Instance == null)
                MessageBox.Show("The comparer is not available.", "Info", System.Windows.Forms.MessageBoxButtons.OK);
            else
                DatastoreModelComparer.Instance.GenerateUpgradeScript(Model.Xml, StoragePath);

        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer.Panel2Collapsed = !propertiesToolStripMenuItem.Checked;
            ShowHideToolStripMenu(splitContainer.Panel2Collapsed);
        }

        private void ShowHideToolStripMenu(bool panel2Collapsed)
        {
            toolStripRight.Visible = panel2Collapsed;

            if (panel2Collapsed)
            {
                ToolStripMenuItem propertiesItem = new ToolStripMenuItem("Properties");
                propertiesItem.Font = new Font(FontFamily.GenericSansSerif, 8.25f);
                propertiesItem.Click += PropertiesItem_Click;
                toolStripRight.Items.Add(propertiesItem);
            }
            else
            {
                if (toolStripRight.Items[0] is ToolStripMenuItem menuItem)
                    menuItem.Click -= PropertiesItem_Click;

                toolStripRight.Items.Clear();
            }
        }

        private void PropertiesItem_Click(object sender, EventArgs e)
        {
            propertiesToolStripMenuItem.Checked = !propertiesToolStripMenuItem.Checked;
            propertiesToolStripMenuItem_Click(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void showLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnShowLabels.Checked = !btnShowLabels.Checked;
            btnShowLabels_Click(sender, e);
        }

        private void showInheritedRelationshipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnShowInheritedRelationships.Checked = !btnShowInheritedRelationships.Checked;
            btnShowInheritedRelationships_Click(sender, e);
        }

        private void DefaultOrExpandPropertiesWidth(bool expand)
        {
            if (expand && expandPropertiesWidthToolStripMenuItem.Checked)
            {
                int width = splitContainer.Width - entityEditor.DataGridMaxWidth;
                splitContainer.SplitterDistance = width;
            }
            else
                splitContainer.SplitterDistance = _splitterDistance;
        }
    }
}
