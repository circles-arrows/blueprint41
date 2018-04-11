namespace Blueprint41.Modeller
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.graphEditor = new Blueprint41.Modeller.GraphEditorControl();
            this.entityEditor = new Blueprint41.Modeller.EntityEditor();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuFielNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnShowLabels = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnShowInheritedRelationships = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbSubmodels = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cmbNodes = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnManageFunctionalIds = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.generateCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staticDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aPIDefinitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 30);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.graphEditor);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.entityEditor);
            this.splitContainer1.Size = new System.Drawing.Size(1215, 671);
            this.splitContainer1.SplitterDistance = 847;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.SizeChanged += new System.EventHandler(this.splitContainer1_SizeChanged);
            // 
            // graphEditor
            // 
            this.graphEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphEditor.Location = new System.Drawing.Point(0, 0);
            this.graphEditor.Name = "graphEditor";
            this.graphEditor.SelectedEntities = null;
            this.graphEditor.Size = new System.Drawing.Size(847, 671);
            this.graphEditor.TabIndex = 0;
            this.graphEditor.NodeInsertedByUser += new System.EventHandler<Blueprint41.Modeller.NodeEventArgs>(this.graphEditor_NodeInsertedByUser);
            this.graphEditor.SelectedNodeChanged += new System.EventHandler<Blueprint41.Modeller.NodeEventArgs>(this.graphEditor_SelectedNodeChanged);
            this.graphEditor.SelectedEdgeChanged += new System.EventHandler<Blueprint41.Modeller.EdgeEventArgs>(this.graphEditor_SelectedEdgeChanged);
            this.graphEditor.InsertRelationship += new System.EventHandler<Blueprint41.Modeller.InsertRelationshipEventArgs>(this.graphEditor_InsertRelationship);
            this.graphEditor.AddDisplayedEntities += new System.EventHandler<System.EventArgs>(this.graphEditor_AddDisplayedEntities);
            this.graphEditor.RemoveNodeFromDiagramChanged += new System.EventHandler<Blueprint41.Modeller.NodeEventArgs>(this.graphEditor_RemoveNodeFromDiagramChanged);
            this.graphEditor.RemoveEdgeFromDiagramChanged += new System.EventHandler<Blueprint41.Modeller.EdgeEventArgs>(this.graphEditor_RemoveEdgeFromDiagramChanged);
            this.graphEditor.RemoveNodeFromStorageChanged += new System.EventHandler<Blueprint41.Modeller.NodeEventArgs>(this.graphEditor_RemoveNodeFromStorageChanged);
            this.graphEditor.RemoveEdgeFromStorageChanged += new System.EventHandler<Blueprint41.Modeller.EdgeEventArgs>(this.graphEditor_RemoveEdgeFromStorageChanged);
            this.graphEditor.NoSelectionEvent += new System.EventHandler(this.graphEditor_NoSelectionEvent);
            // 
            // entityEditor
            // 
            this.entityEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.entityEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entityEditor.Enabled = false;
            this.entityEditor.FunctionalIdDataTable = null;
            this.entityEditor.Location = new System.Drawing.Point(0, 0);
            this.entityEditor.Name = "entityEditor";
            this.entityEditor.Size = new System.Drawing.Size(364, 671);
            this.entityEditor.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton2,
            this.btnSave,
            this.toolStripSeparator1,
            this.btnShowLabels,
            this.toolStripSeparator2,
            this.btnShowInheritedRelationships,
            this.toolStripSeparator5,
            this.toolStripLabel1,
            this.cmbSubmodels,
            this.toolStripLabel2,
            this.cmbNodes,
            this.toolStripSeparator3,
            this.btnManageFunctionalIds,
            this.toolStripSeparator6,
            this.toolStripDropDownButton1,
            this.toolStripSeparator4,
            this.toolStripDropDownButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1215, 30);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFielNew,
            this.menuFileOpen});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(38, 27);
            this.toolStripDropDownButton2.Text = "File";
            // 
            // menuFielNew
            // 
            this.menuFielNew.Name = "menuFielNew";
            this.menuFielNew.Size = new System.Drawing.Size(103, 22);
            this.menuFielNew.Text = "New";
            this.menuFielNew.Click += new System.EventHandler(this.menuFielNew_Click);
            // 
            // menuFileOpen
            // 
            this.menuFileOpen.Name = "menuFileOpen";
            this.menuFileOpen.Size = new System.Drawing.Size(103, 22);
            this.menuFileOpen.Text = "Open";
            this.menuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(72, 27);
            this.btnSave.Text = "Save Model";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // btnShowLabels
            // 
            this.btnShowLabels.CheckOnClick = true;
            this.btnShowLabels.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnShowLabels.Image = ((System.Drawing.Image)(resources.GetObject("btnShowLabels.Image")));
            this.btnShowLabels.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowLabels.Name = "btnShowLabels";
            this.btnShowLabels.Size = new System.Drawing.Size(76, 27);
            this.btnShowLabels.Text = "Show Labels";
            this.btnShowLabels.Click += new System.EventHandler(this.btnShowLabels_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 30);
            // 
            // btnShowInheritedRelationships
            // 
            this.btnShowInheritedRelationships.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnShowInheritedRelationships.CheckOnClick = true;
            this.btnShowInheritedRelationships.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnShowInheritedRelationships.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowInheritedRelationships.Name = "btnShowInheritedRelationships";
            this.btnShowInheritedRelationships.Size = new System.Drawing.Size(163, 27);
            this.btnShowInheritedRelationships.Text = "Show Inherited Relationships";
            this.btnShowInheritedRelationships.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnShowInheritedRelationships.Click += new System.EventHandler(this.btnShowInheritedRelationships_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 30);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(78, 27);
            this.toolStripLabel1.Text = "Select Model:";
            // 
            // cmbSubmodels
            // 
            this.cmbSubmodels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubmodels.Name = "cmbSubmodels";
            this.cmbSubmodels.Size = new System.Drawing.Size(121, 30);
            this.cmbSubmodels.SelectedIndexChanged += new System.EventHandler(this.cmbSubmodels_SelectedIndexChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(65, 27);
            this.toolStripLabel2.Text = "Find Node:";
            // 
            // cmbNodes
            // 
            this.cmbNodes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNodes.DropDownWidth = 200;
            this.cmbNodes.Name = "cmbNodes";
            this.cmbNodes.Size = new System.Drawing.Size(121, 30);
            this.cmbNodes.SelectedIndexChanged += new System.EventHandler(this.cmbNodes_SelectedIndexChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 30);
            // 
            // btnManageFunctionalIds
            // 
            this.btnManageFunctionalIds.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnManageFunctionalIds.Image = ((System.Drawing.Image)(resources.GetObject("btnManageFunctionalIds.Image")));
            this.btnManageFunctionalIds.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnManageFunctionalIds.Name = "btnManageFunctionalIds";
            this.btnManageFunctionalIds.Size = new System.Drawing.Size(122, 27);
            this.btnManageFunctionalIds.Text = "Manage Function Ids";
            this.btnManageFunctionalIds.Click += new System.EventHandler(this.btnManageFunctionalIds_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 30);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateCodeToolStripMenuItem,
            this.staticDataToolStripMenuItem,
            this.aPIDefinitionToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(98, 27);
            this.toolStripDropDownButton1.Text = "Generate Code";
            this.toolStripDropDownButton1.Visible = false;
            // 
            // generateCodeToolStripMenuItem
            // 
            this.generateCodeToolStripMenuItem.Name = "generateCodeToolStripMenuItem";
            this.generateCodeToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.generateCodeToolStripMenuItem.Text = "Datastore Model";
            this.generateCodeToolStripMenuItem.Click += new System.EventHandler(this.generateCodeToolStripMenuItem_Click);
            // 
            // staticDataToolStripMenuItem
            // 
            this.staticDataToolStripMenuItem.Name = "staticDataToolStripMenuItem";
            this.staticDataToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.staticDataToolStripMenuItem.Text = "Static Data";
            this.staticDataToolStripMenuItem.Click += new System.EventHandler(this.staticDataToolStripMenuItem_Click);
            // 
            // aPIDefinitionToolStripMenuItem
            // 
            this.aPIDefinitionToolStripMenuItem.Name = "aPIDefinitionToolStripMenuItem";
            this.aPIDefinitionToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.aPIDefinitionToolStripMenuItem.Text = "API Definition";
            this.aPIDefinitionToolStripMenuItem.Click += new System.EventHandler(this.aPIDefinitionToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 30);
            this.toolStripSeparator4.Visible = false;
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton3.Image")));
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(139, 27);
            this.toolStripDropDownButton3.Text = "Generate Upgrade Script";
            this.toolStripDropDownButton3.Click += new System.EventHandler(this.toolStripDropDownButtonGenerateUpgradeScript_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 702);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "Graph Modeller";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private GraphEditorControl graphEditor;
        private EntityEditor entityEditor;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnShowLabels;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cmbSubmodels;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cmbNodes;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem generateCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem staticDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aPIDefinitionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnShowInheritedRelationships;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem menuFielNew;
        private System.Windows.Forms.ToolStripMenuItem menuFileOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnManageFunctionalIds;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolStripDropDownButton3;
    }
}

