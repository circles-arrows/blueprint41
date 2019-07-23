using Blueprint41.Modeller.Schemas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Model = Blueprint41.Modeller.Schemas.Modeller;
using Blueprint41.Modeller.Generation;
using System.Xml;
using Blueprint41.Modeller.Editors;
using Microsoft.Msagl.GraphViewerGdi;
using Blueprint41.Licensing.Connector;
using Blueprint41.Modeller.Utils;

namespace Blueprint41.Modeller
{
    public partial class MainForm : Form
    {
        #region Private Properties
        bool showLabels = false;
        bool showInherited = false;
        bool removingEdge = false;
        int idcounter = 0;
        int splitterDistance;

        const string NEWSUBMODEL = "New...";
        const string FORMNAME = @"Blueprint41:\> Graph Modeller";
        const string NEO4J_EDITOR = "Neo4j Model";
        const string B41_EDITOR = "Blueprint41 Model";

        Submodel mainSubmodel => Model.Submodels.Submodel[0];
        NodeTypeEntry newNodeTypeEntity { get; set; }
        Submodel.NodeLocalType selectedNode { get; set; }
        #endregion

        #region Properties
        public Model Model { get; private set; }
        private string m_StoragePath = null;
        public string StoragePath
        {
            set { this.m_StoragePath = value; }
            get
            {
                if (this.m_StoragePath == null)
                    this.m_StoragePath = Util.DefaultFilePath;

                return this.m_StoragePath;
            }
        }
        public string EntityNodeName => Model.ModellerType == ModellerType.Blueprint41 ? "Entity" : "Node";
        #endregion

        public MainForm()
        {
            if (!string.IsNullOrEmpty(RegistryHandler.LastOpenedFile))
                StoragePath = RegistryHandler.LastOpenedFile;

            InitializeComponent();
            FormClosing += MainForm_FormClosing;
        }

        #region Main Form Events
        private void MainForm_Load(object sender, EventArgs e)
        {
            bool recoveryFileExists = File.Exists(Recovery.Instance.RecoveryFile);
            string storagePath = StoragePath;

            if (recoveryFileExists)
            {
                DialogResult result = MessageBox.Show("Do you want to recover unsaved changes?", "Recover File", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    StoragePath = Recovery.Instance.RecoveryFile;
            }

            SetModuleMenuItemVisibility();
            InitializeXmlModeller();

            StoragePath = storagePath;

            AddNewEntitiesToSubModel(Model.Submodels.Submodel[0].Name);
            splitterDistance = splitContainer.SplitterDistance;
            SizeChanged += MainForm_SizeChanged;

            EnableDisableButtons();
        }
       
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            splitterDistance = splitContainer.SplitterDistance;
        }

        private void SplitContainer1_SizeChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Width:{0}", splitContainer.Panel2.Width));
        }

        protected override void OnClosed(EventArgs e)
        {
            RegistryHandler.LastOpenedSubmodel = Model.DisplayedSubmodel.Name;
            RegistryHandler.LastOpenedFile = string.IsNullOrEmpty(StoragePath) ? string.Empty : Path.GetFullPath(this.StoragePath);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = DialogResult.Yes; // if there is no message box, act as if the user pressed "Yes" to save.

            if (Model.HasChanges)
            {
                result = MessageBox.Show("Do you want to save changes?", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                    BtnSave_Click(this, EventArgs.Empty);
            }

            if (result != DialogResult.Cancel)
                Recovery.Instance.Stop();
            else
                e.Cancel = true;
        }
        #endregion

        #region Graph Editor Events
        private void GraphEditor_PanModeClicked(object sender, EventArgs e)
        {
            if (graphEditor.PanButtonPressedOnMenu)
                graphEditor.Viewer.InsertingEdge = false;

            SetCheckedModeMenuControls();
        }

        private void GraphEditor_EdgeModeClicked(object sender, EventArgs e)
        {
            if (graphEditor.Viewer.InsertingEdge)
                graphEditor.PanButtonPressedOnMenu = false;

            SetCheckedModeMenuControls();
        }

        void GraphEditor_EdgeRemoved(object sender, EdgeEventArgs e)
        {
            removingEdge = true;

            if (e?.Edge?.UserData is Relationship rel && rel.ContainsDrawingEdge && rel.DeleteIncludeRelationshipModel)
            {
                DialogResult dialogResult = MessageBox.Show($@"Are you sure you want to delete the relationship ""{rel.Source.Label}->[{rel.Name}]->{rel.Target.Label}"" from storage?", "WARNING!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                    Model.RemoveRelationship(rel);
                else
                    Model.GraphEditor.Viewer.Undo();
            }

            if (entityEditor.IsEditable)
                entityEditor.UpdateRelationshipGridView();

            removingEdge = false;
            Model.Invalidate();
        }

        void GraphEditor_EdgeAdded(object sender, EdgeEventArgs e)
        {
            if (tsbEdgeInsertion.Checked == false || removingEdge == true)
                return;

            using (EdgeEditor editor = new EdgeEditor())
            {
                editor.Model = Model;
                editor.SourceName = e.Edge.Source;
                editor.TargetName = e.Edge.Target;
                
                if (editor.ShowDialog() == DialogResult.OK)
                {
                    Model.InsertRelationship(editor.SourceName, editor.TargetName, editor.Relationship, e.Edge);
                    entityEditor.CloseEditor();
                    graphEditor.ClearSelection();
                }
                else
                    Model.GraphEditor.Viewer.Undo();
            }

            Model.Invalidate();
        }

        void GraphEditor_DeleteEntity(object sender, NodeEventArgs e)
        {
            if (e.Node.UserData is Submodel.NodeLocalType node)
            {
                DialogResult dialogResult = MessageBox.Show($@"Are you sure you want to delete the {EntityNodeName.ToLower()} ""{node.Label}"" from storage?", "WARNING!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    Model.DeleteEntity(node);

                    if (Model?.GraphEditor?.Viewer?.Entities.Count() == 0)                    
                        ReloadGraph();

                    Model.Invalidate();
                    RefreshNodeCombobox();
                    CloseNodeEditor();
                    EnableDisableButtons();
                }
            }
        }

        void GraphEditor_ExcludeEntity(object sender, NodeEventArgs e)
        {
            if (e.Node.UserData is Submodel.NodeLocalType model)
                Model.ExcludeFromCurrentModel(model);

            ReloadGraph();
            Model.Invalidate();
            RefreshNodeCombobox();
            CloseNodeEditor();
            EnableDisableButtons();
        }

        void GraphEditor_NoSelection(object sender, EventArgs e)
        {
            CloseNodeEditor();
            CloseEdgeEditor();
            RefreshNodeCombobox();
            DefaultOrExpandPropertiesWidth(false);
            selectedNode = null;
        }

        void GraphEditor_NodeSelected(object sender, NodeEventArgs e)
        {          
            if (!(e.Node.UserData is Submodel.NodeLocalType))
                throw new NotSupportedException();

            selectedNode = e.Node.UserData as Submodel.NodeLocalType;

            if (tsbEdgeInsertion.Checked)
                return;

            ShowEntityEditor();

            DefaultOrExpandPropertiesWidth(false);
        }

        void GraphEditor_EditSubmodelClick(object sender, EventArgs e)
        {
            ManageSubmodelForm form = new Modeller.ManageSubmodelForm(Model, Model.DisplayedSubmodel);
            if (form.ShowDialog() == DialogResult.OK)
            {
                Model.CaptureCoordinates();
                Model.RebindControl();
                RefreshNodeCombobox();
                FillSubmodelComboBox(form.Submodel);
            }
        }

        void GraphEditor_InsertNode(object sender, NodeEventArgs e)
        {
            Submodel.NodeLocalType model = e.Node?.UserData as Submodel.NodeLocalType;
            if (model == null)
            {
                Entity entity = new Schemas.Entity(Model);
                entity.Label = GetNewId(newNodeTypeEntity);
                entity.Name = entity.Label;
                Model.Entities.Entity.Add(entity);

                model = new Submodel.NodeLocalType(Model);
                model.Label = entity.Label;
                model.Xcoordinate = e.Center.X;
                model.Ycoordinate = e.Center.Y;
                model.EntityGuid = entity.Guid;

                if (Model.DisplayedSubmodel != mainSubmodel)
                    mainSubmodel.Node.Add(model.Clone());

                Model.DisplayedSubmodel.Node.Add(model);
                RefreshNodeCombobox();
                //ResetLayout();
                Model.RebindControl();     
            }

            // Auto select newly created entity
            selectedNode = model;
            selectedNode.Select();
            entityEditor.Show(model.Entity, Model);

            EnableDisableButtons();
        }
        #endregion

        void InitializeXmlModeller(ModellerType type = ModellerType.Blueprint41)
        {
            InitializeModel(type);
            InitializeSubmodel();
            ReloadGraph();
            Recovery.Instance.Start(this, () => Console.WriteLine("Auto saved modeller"));

            SetModellerTypeMenuItemVisibility();
            EnableDisableButtons();
        }

        void InitializeModel(ModellerType modellerType)
        {
            if ((object)Model != null)
                Model.UnRegisterEvents();

            Model = new Model();

            try
            {
                if (File.Exists(StoragePath))
                    Model = new Model(StoragePath);
            }
            catch (XmlException)
            {
                MessageBox.Show($"The path {StoragePath} is an invalid xml file", "Invalid Xml File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RegistryHandler.LastOpenedFile = string.Empty;

                if (Model == null)
                    Model = new Model();
            }

            Model.Type = Model.Type ?? modellerType.ToString();
            Model.HasChanges = false;
            Model.BeforeReBind = ClearEvents;
            Model.AfterReBind = AddEvents;

            Model.ShowRelationshipLabels = showLabels;
            Model.ShowInheritedRelationships = showInherited;

            string editorName = Model.ModellerType == ModellerType.Blueprint41 ? B41_EDITOR : NEO4J_EDITOR;
            this.Text = $"{FORMNAME} - ({editorName})";
            graphEditor.ModellerType = Model.ModellerType;

            //CheckGuidDiscrepancies();
        }

        void InitializeSubmodel()
        {
            if (Model.DisplayedSubmodel == null)
            {
                if (Model.Submodels.Submodel.Count == 0)
                {
                    Submodel main = new Submodel(Model);
                    main.Name = Constants.MainModel;
                    Model.Submodels.Submodel.Add(main);
                }

                Model.DisplayedSubmodel = Model.Submodels.Submodel.Where(m => m.Name == RegistryHandler.LastOpenedSubmodel).FirstOrDefault();
                Model.MainSubmodel = mainSubmodel;

                if (Model.DisplayedSubmodel == null)
                    Model.DisplayedSubmodel = mainSubmodel;
            }

            FillSubmodelComboBox(Model.DisplayedSubmodel);
            InitializeGraphNodeTypes();
            entityEditor.EnableDisableControlsForType(Model.ModellerType);

            btnShowLabels.Checked = Model.ShowRelationshipLabels;
            btnShowInheritedRelationships.Checked = Model.ShowInheritedRelationships;
            idcounter = 0;
        }

        void InitializeGraphNodeTypes()
        {
            graphEditor.NodeTypes.Clear();
            string contextMenuString = Model.ModellerType == ModellerType.Blueprint41 ? "New Entity" : "New Node";
            string label = Model.ModellerType == ModellerType.Blueprint41 ? "Entity" : "Node";

            if (graphEditor.NodeTypes.SingleOrDefault(x => x.Name == contextMenuString) == null)
            {
                newNodeTypeEntity = new NodeTypeEntry(contextMenuString, Microsoft.Msagl.Drawing.Shape.Circle, Microsoft.Msagl.Drawing.Color.Transparent, Microsoft.Msagl.Drawing.Color.Black, 10, null, label);
                graphEditor.AddNodeType(newNodeTypeEntity);
            }
        }

        void ClearEvents()
        {
            graphEditor.InsertNode -= GraphEditor_InsertNode;
            graphEditor.EditSubmodelClicked -= GraphEditor_EditSubmodelClick;
            graphEditor.NodeSelected -= GraphEditor_NodeSelected;
            graphEditor.NoneSelected -= GraphEditor_NoSelection;
            graphEditor.EntityExcluded -= GraphEditor_ExcludeEntity;
            graphEditor.EntityDeleted -= GraphEditor_DeleteEntity;
            graphEditor.EdgeAdded -= GraphEditor_EdgeAdded;
            graphEditor.EdgeRemoved -= GraphEditor_EdgeRemoved;
            graphEditor.EdgeModeClicked -= GraphEditor_EdgeModeClicked;
            graphEditor.PanModeClicked -= GraphEditor_PanModeClicked;
        }

        void AddEvents()
        {
            graphEditor.InsertNode += GraphEditor_InsertNode;
            graphEditor.EditSubmodelClicked += GraphEditor_EditSubmodelClick;
            graphEditor.NodeSelected += GraphEditor_NodeSelected;
            graphEditor.NoneSelected += GraphEditor_NoSelection;
            graphEditor.EntityExcluded += GraphEditor_ExcludeEntity;
            graphEditor.EntityDeleted += GraphEditor_DeleteEntity;
            graphEditor.EdgeAdded += GraphEditor_EdgeAdded;
            graphEditor.EdgeRemoved += GraphEditor_EdgeRemoved;
            graphEditor.EdgeModeClicked += GraphEditor_EdgeModeClicked;
            graphEditor.PanModeClicked += GraphEditor_PanModeClicked;
        }

        private void EnableDisableButtons()
        {
            bool hasEntities = Model?.GraphEditor?.Viewer?.Entities.Count() > 0;
                        
            tsbEdgeInsertion.Enabled = hasEntities;
            showLabelsToolStripMenuItem.Enabled = hasEntities;
            btnShowLabels.Enabled = hasEntities;
            btnShowInheritedRelationships.Enabled = hasEntities;
            showInheritedRelationshipsToolStripMenuItem.Enabled = hasEntities;
            
            if (!hasEntities)
            {
                graphEditor.Viewer.InsertingEdge = false;
                btnShowLabels.Checked = false;
                showLabelsToolStripMenuItem.Checked = false;
            }

            SetCheckedModeMenuControls();
        }

        void SetCheckedModeMenuControls()
        {
            tsbPan.Checked = graphEditor.PanButtonPressedOnMenu;
            panToolStripMenuItem.Checked = graphEditor.PanButtonPressedOnMenu;
            tsbEdgeInsertion.Checked = graphEditor.Viewer.InsertingEdge;
        }

        void SetModuleMenuItemVisibility()
        {
            generateUpdateScriptToolStripMenuItem.Visible = (ModuleLoader.GetModule("Compare") != null);
            generateUpdateScriptToolStripMenuItem1.Visible = (ModuleLoader.GetModule("Compare") != null);

            generateDocumentToolStripMenuItem.Visible = (ModuleLoader.GetModule("Document") != null);
            generateDocumentationToolStripMenuItem.Visible = (ModuleLoader.GetModule("Document") != null);
        }

        void SetModellerTypeMenuItemVisibility()
        {
            generateCodeToolStripMenuItem.Visible = Model.ModellerType == ModellerType.Blueprint41;
            generateCodeToolStripMenuItem1.Visible = Model.ModellerType == ModellerType.Blueprint41;

            functionalIdToolStripMenuItem.Visible = Model.ModellerType == ModellerType.Blueprint41;
            functionalIdToolStripMenuItem1.Visible = Model.ModellerType == ModellerType.Blueprint41;

            settingsToolStripMenuItem.Visible = Model.ModellerType == ModellerType.Blueprint41;
            toolsToolStripMenuItem.Visible = Model.ModellerType == ModellerType.Blueprint41;

            toolStripDropDownButtonTools.Visible = Model.ModellerType == ModellerType.Blueprint41;
            toolStripDropDownButtonSettings.Visible = Model.ModellerType == ModellerType.Blueprint41;

            toolStripSeparator4.Visible = Model.ModellerType == ModellerType.Blueprint41;
        }

        void ReloadGraph()
        {
            Model.GraphEditor = graphEditor;
        }

        void AddNewEntitiesToSubModel(string submodelName)
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

            if (this.entityEditor.IsEditable)
                entityEditor.Reload();
        }

        void FillSubmodelComboBox(Submodel selectedSub = null)
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

        string GetNewId(NodeTypeEntry nte)
        {
            string ret = nte.DefaultLabel + idcounter++;
            if (Model.Entities.Entity.Any(item => item.Label == ret))
                return GetNewId(nte);
            return ret;
        }

        void RefreshNodeCombobox()
        {
            cmbNodes.Items.Clear();
            cmbNodes.Items.Add("No Selection");
            foreach (var item in Model.DisplayedSubmodel.Node.OrderBy(item => item.Label))
            {
                item.RemoveHighlight();
                item.Deselect();
                cmbNodes.Items.Add(item);
            }
        }

        #region Editor Events

        private void CloseNodeEditor()
        {
            entityEditor.CloseEditor();
        }

        private void CloseEdgeEditor()
        {

        }

        private void CmbSubmodels_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            {
                selectedNode = null;
                Model.DisplayedSubmodel = cmbSubmodels.SelectedItem as Submodel;
            }

            this.graphEditor.Viewer.Tag = Model.DisplayedSubmodel;
            this.entityEditor.CloseEditor();
            EnableDisableButtons();
            RefreshNodeCombobox();
        }

        private void CmbNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNodes.SelectedItem == null)
                return;

            RemoveHighlightNodes();

            if (cmbNodes.SelectedIndex == 0)
            {
                entityEditor.CloseEditor();
                Model.GraphReset();                
                return;
            }
            
            selectedNode = cmbNodes.SelectedItem as Submodel.NodeLocalType;

            entityEditor.Show(selectedNode.Entity, Model);
            selectedNode.Highlight();
        }

        private void RemoveHighlightNodes()
        {
            foreach (var item in Model.DisplayedSubmodel.Node.OrderBy(item => item.Label))
            {
                item.RemoveHighlight();
                item.Deselect();
            }
        }

        private void EntityEditor_EntityTypeChanged(object sender, EventArgs e)
        {
            selectedNode.RemoveHighlight();
        }

        #endregion

        #region File Menu
        private void MenuFileOpen_Click(object sender, EventArgs e)
        {
            DialogResult result = CheckForChangesAndSaveIfPossible();

            if (result != DialogResult.Cancel)
            {
                OpenFileDialog openDialog = new OpenFileDialog();

                openDialog.Filter = "XML Document (*.xml) | *.xml";
                DialogResult openDialogResult = openDialog.ShowDialog(this);

                if (openDialogResult == DialogResult.OK)
                {
                    StoragePath = openDialog.FileName;
                    RegistryHandler.LastOpenedFile = StoragePath;
                    InitializeXmlModeller();
                }
            }
        }

        private void neo4jModellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile(ModellerType.Neo4j);
        }

        private void blueprint41ModellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile(ModellerType.Blueprint41);
        }

        void NewFile(ModellerType type = ModellerType.Blueprint41)
        {
            DialogResult result = CheckForChangesAndSaveIfPossible();

            if (result != DialogResult.Cancel)
            {
                StoragePath = null;
                InitializeXmlModeller(type);
            }
        }

        DialogResult CheckForChangesAndSaveIfPossible()
        {
            if (Model == null || Model.HasChanges == false)
                return DialogResult.No;

            string fileName = Path.GetFileName(StoragePath);

            DialogResult result = MessageBox.Show($@"Do you want to save changes to ""{fileName}"" ?", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Model.HasChanges = false;
                Model.CaptureCoordinates();
                Model.Save(StoragePath);
            }

            return result;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (File.Exists(StoragePath) == false && SaveAs() == DialogResult.Cancel)
                return;

            Model.HasChanges = false;
            Model.CaptureCoordinates();
            Model.Save(StoragePath);
            MessageBox.Show("Diagram saved successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (this.entityEditor.IsEditable)
                entityEditor.Reload();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        DialogResult SaveAs()
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "XML Document (*.xml)|*.xml";
                saveDialog.DefaultExt = "xml";
                DialogResult result = saveDialog.ShowDialog(this);

                if (result == DialogResult.OK)
                    StoragePath = saveDialog.FileName;

                return result;
            }
        }
        #endregion

        #region View Menu
        private void PropertiesItem_Click(object sender, EventArgs e)
        {
            propertiesToolStripMenuItem.Checked = !propertiesToolStripMenuItem.Checked;
            PropertiesToolStripMenuItem_Click(sender, e);
        }

        private void PropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHideToolStripMenu(!propertiesToolStripMenuItem.Checked);
        }

        private void MdsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            AddOrClearViewModeCheckEvents(false);
            mdsToolStripMenuItem.Checked = mdsToolStripMenuItem.Checked;

            sugiyamaToolStripMenuItem.Checked = false;
            originalSettingsToolStripMenuItem.Checked = mdsToolStripMenuItem.Checked == false;

            AddOrClearViewModeCheckEvents(true);
            SetGraphViewModel(originalSettingsToolStripMenuItem.Checked ? LayoutMethod.UseSettingsOfTheGraph : LayoutMethod.MDS);
            Model.RebindControl();
        }

        private void SugiyamaToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            AddOrClearViewModeCheckEvents(false);
            sugiyamaToolStripMenuItem.Checked = sugiyamaToolStripMenuItem.Checked;

            mdsToolStripMenuItem.Checked = false;
            originalSettingsToolStripMenuItem.Checked = sugiyamaToolStripMenuItem.Checked == false;

            AddOrClearViewModeCheckEvents(true);
            SetGraphViewModel(originalSettingsToolStripMenuItem.Checked ? LayoutMethod.UseSettingsOfTheGraph : LayoutMethod.SugiyamaScheme);
            Model.RebindControl();
        }

        private void OriginalSettingsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            AddOrClearViewModeCheckEvents(false);
            originalSettingsToolStripMenuItem.Checked = true;

            mdsToolStripMenuItem.Checked = false;
            sugiyamaToolStripMenuItem.Checked = false;

            AddOrClearViewModeCheckEvents(true);

            Model.ViewMode = LayoutMethod.UseSettingsOfTheGraph;
            Model.RebindControl();
        }

        void AddOrClearViewModeCheckEvents(bool add)
        {
            if (add == false)
            {
                sugiyamaToolStripMenuItem.CheckedChanged -= SugiyamaToolStripMenuItem_CheckedChanged;
                mdsToolStripMenuItem.CheckedChanged -= MdsToolStripMenuItem_CheckedChanged;
                originalSettingsToolStripMenuItem.CheckedChanged -= OriginalSettingsToolStripMenuItem_CheckedChanged;
            }
            else
            {
                sugiyamaToolStripMenuItem.CheckedChanged += SugiyamaToolStripMenuItem_CheckedChanged;
                mdsToolStripMenuItem.CheckedChanged += MdsToolStripMenuItem_CheckedChanged;
                originalSettingsToolStripMenuItem.CheckedChanged += OriginalSettingsToolStripMenuItem_CheckedChanged;
            }
        }

        void SetGraphViewModel(LayoutMethod method)
        {
            Model.ViewMode = method;
            Model.RedoLayout();
        }

        private void ShowLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnShowLabels.Checked = !btnShowLabels.Checked;
            BtnShowLabels_Click(sender, e);
        }

        private void ShowInheritedRelationshipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnShowInheritedRelationships.Checked = !btnShowInheritedRelationships.Checked;
            BtnShowInheritedRelationships_Click(sender, e);
        }

        public void DefaultOrExpandPropertiesWidth(bool expand)
        {
            if (expand && expandPropertiesWidthToolStripMenuItem.Checked)
            {
                int width = splitContainer.Width - entityEditor.DataGridMaxWidth;

                if (splitContainer.SplitterDistance > width)
                    splitContainer.SplitterDistance = width;
            }
            else if (expand == false && expandPropertiesWidthToolStripMenuItem.Checked)
                splitContainer.SplitterDistance = splitterDistance;
        }

        public void ShowHideToolStripMenu(bool panel2Collapsed)
        {
            propertiesToolStripMenuItem.Checked = !panel2Collapsed;
            splitContainer.Panel2Collapsed = panel2Collapsed;
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

        private void TsbZoomOut_Click(object sender, EventArgs e)
        {
            graphEditor.Viewer.ZoomOutPressed();
        }

        private void TsbZoomIn_Click(object sender, EventArgs e)
        {
            graphEditor.Viewer.ZoomInPressed();
        }

        private void TsbPan_Click(object sender, EventArgs e)
        {
            graphEditor.PanButtonPressedOnMenu = !graphEditor.PanButtonPressedOnMenu;
            graphEditor.Viewer.InsertingEdge = false;

            SetCheckedModeMenuControls();
        }

        private void ZoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TsbZoomOut_Click(this, EventArgs.Empty);
        }

        private void ZoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TsbZoomIn_Click(this, EventArgs.Empty);
        }

        private void PanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TsbPan_Click(this, EventArgs.Empty);
        }

        private void TsbEdgeInsertion_Click(object sender, EventArgs e)
        {
            graphEditor.Viewer.InsertingEdge = !graphEditor.Viewer.InsertingEdge;
            graphEditor.PanButtonPressedOnMenu = false;

            SetCheckedModeMenuControls();

            if (graphEditor.Viewer.InsertingEdge)
                CloseNodeEditor();
            else
                ShowEntityEditor();
        }

        private void TsbHome_Click(object sender, EventArgs e)
        {
            Model.GraphReset();
        }

        private void BtnShowInheritedRelationships_Click(object sender, EventArgs e)
        {
            showInherited = btnShowInheritedRelationships.Checked;
            showInheritedRelationshipsToolStripMenuItem.Checked = btnShowInheritedRelationships.Checked;            
            Model.ShowInheritedRelationships = btnShowInheritedRelationships.Checked;
        }

        private void BtnShowLabels_Click(object sender, EventArgs e)
        {
            showLabels = btnShowLabels.Checked;
            showLabelsToolStripMenuItem.Checked = btnShowLabels.Checked;
            Model.ShowRelationshipLabels = btnShowLabels.Checked;
        }

        #endregion

        #region Tools Menu

        private async void GenerateDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModuleLoader module = ModuleLoader.GetModule("Document");

            if (module == null)
            {
                MessageBox.Show("The document generator is not available.", "Info", System.Windows.Forms.MessageBoxButtons.OK);
                return;
            }

            Form loader = this.ShowLoader();
            await module.RunModule(Model.Xml, null);
            this.HideLoader(loader);
        }

        private void GenerateCodeToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void GenerateUpdateScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModuleLoader module = ModuleLoader.GetModule("Compare");

            if (module == null)
            {
                MessageBox.Show("The comparer is not available.", "Info", System.Windows.Forms.MessageBoxButtons.OK);
                return;
            }
            else
            {
                // Save it before generating update script
                Model.CaptureCoordinates();
                Model.Save(StoragePath);

                module.RunModule(Model.Xml, StoragePath);

                // Re initalize model after generating update script
                // this will reload the xml
                InitializeXmlModeller();
            }
        }

        private void GenerateCodeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GenerateCodeToolStripMenuItem_Click(sender, EventArgs.Empty);
        }

        private void GenerateUpdateScriptToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GenerateUpdateScriptToolStripMenuItem_Click(sender, EventArgs.Empty);
        }

        private void GenerateDocumentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateDocumentToolStripMenuItem_Click(sender, EventArgs.Empty);
        }

        #endregion

        #region Settings Menu

        private void FunctionalIdToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FunctionalIdToolStripMenuItem_Click(sender, EventArgs.Empty);
        }

        private void FunctionalIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageFunctionalId functionalIdForm = new ManageFunctionalId(Model);
            functionalIdForm.ShowDialog(this);
            ShowEntityEditor();
        }

        private void ShowEntityEditor()
        {
            if (graphEditor.SelectedEntities.Count > 1)
            {
                CloseNodeEditor();
                selectedNode = null;
            }
            else
            {
                Entity entity = selectedNode?.Entity;
                if (entity != null)
                    entityEditor.Show(entity, Model);
            }
        }

        #endregion

        #region Help Menu
        private void registerProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectorLoader loader = ConnectorLoader.GetConnectorClient();

            if (loader == null)
            {
                MessageBox.Show("Unable to communicate to the server at this time. Please check your internet connection and restart the application. If error persists, contact support@circles-arrows.com", "Register", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            loader.RegisterLicense();
            SetModuleMenuItemVisibility();
        }

        private void aboutBlueprint41ModellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutForm form = new AboutForm())
            {
                form.ShowDialog();
            }
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (UpdaterForm form = new UpdaterForm())
            {
                form.ShowDialog();
            }
        }
        #endregion

    }
}
