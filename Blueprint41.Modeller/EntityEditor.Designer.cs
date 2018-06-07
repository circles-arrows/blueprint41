namespace Blueprint41.Modeller
{
    partial class EntityEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkIsAbstract = new System.Windows.Forms.CheckBox();
            this.chkIsVirtual = new System.Windows.Forms.CheckBox();
            this.dataGridViewPrimitiveProperties = new System.Windows.Forms.DataGridView();
            this.bindingSourcePrimitiveProperties = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewRelationships = new System.Windows.Forms.DataGridView();
            this.bindingSourceCollectionProperties = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceEntities = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbInherits = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.dataGridViewInheritedPrimitiveProperties = new System.Windows.Forms.DataGridView();
            this.btnEditStaticData = new System.Windows.Forms.Button();
            this.cmbFunctionalId = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtExample = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chkIsStaticData = new System.Windows.Forms.CheckBox();
            this.bindingSourceInheritedPrimitiveProperties = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainerProperties = new System.Windows.Forms.SplitContainer();
            this.splitContainerRelationships = new System.Windows.Forms.SplitContainer();
            this.dataGridViewInheritedRelationships = new System.Windows.Forms.DataGridView();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerPropBox = new System.Windows.Forms.SplitContainer();
            this.label10 = new System.Windows.Forms.Label();
            this.splitContainerRelBox = new System.Windows.Forms.SplitContainer();
            this.checkBoxShowAllRelationships = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.bindingSourceInheritedRelationships = new System.Windows.Forms.BindingSource(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrimitiveProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePrimitiveProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRelationships)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCollectionProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEntities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInheritedPrimitiveProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceInheritedPrimitiveProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerProperties)).BeginInit();
            this.splitContainerProperties.Panel1.SuspendLayout();
            this.splitContainerProperties.Panel2.SuspendLayout();
            this.splitContainerProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRelationships)).BeginInit();
            this.splitContainerRelationships.Panel1.SuspendLayout();
            this.splitContainerRelationships.Panel2.SuspendLayout();
            this.splitContainerRelationships.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInheritedRelationships)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPropBox)).BeginInit();
            this.splitContainerPropBox.Panel1.SuspendLayout();
            this.splitContainerPropBox.Panel2.SuspendLayout();
            this.splitContainerPropBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRelBox)).BeginInit();
            this.splitContainerRelBox.Panel1.SuspendLayout();
            this.splitContainerRelBox.Panel2.SuspendLayout();
            this.splitContainerRelBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceInheritedRelationships)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entity Name:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "name", true));
            this.txtName.Location = new System.Drawing.Point(97, 70);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(354, 20);
            this.txtName.TabIndex = 1;
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(Blueprint41.Modeller.Schemas.entity);
            // 
            // txtLabel
            // 
            this.txtLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "label", true));
            this.txtLabel.Location = new System.Drawing.Point(97, 44);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(354, 20);
            this.txtLabel.TabIndex = 3;
            this.txtLabel.Leave += new System.EventHandler(this.txtLabel_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Neo4j Label:";
            // 
            // chkIsAbstract
            // 
            this.chkIsAbstract.AutoSize = true;
            this.chkIsAbstract.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSource, "abstract", true));
            this.chkIsAbstract.Location = new System.Drawing.Point(97, 235);
            this.chkIsAbstract.Name = "chkIsAbstract";
            this.chkIsAbstract.Size = new System.Drawing.Size(15, 14);
            this.chkIsAbstract.TabIndex = 4;
            this.chkIsAbstract.UseVisualStyleBackColor = true;
            // 
            // chkIsVirtual
            // 
            this.chkIsVirtual.AutoSize = true;
            this.chkIsVirtual.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSource, "virtual", true));
            this.chkIsVirtual.Location = new System.Drawing.Point(181, 236);
            this.chkIsVirtual.Name = "chkIsVirtual";
            this.chkIsVirtual.Size = new System.Drawing.Size(15, 14);
            this.chkIsVirtual.TabIndex = 5;
            this.chkIsVirtual.UseVisualStyleBackColor = true;
            this.chkIsVirtual.CheckedChanged += new System.EventHandler(this.chkIsVirtual_CheckedChanged);
            // 
            // dataGridViewPrimitiveProperties
            // 
            this.dataGridViewPrimitiveProperties.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewPrimitiveProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewPrimitiveProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPrimitiveProperties.GridColor = System.Drawing.Color.LightGray;
            this.dataGridViewPrimitiveProperties.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewPrimitiveProperties.Name = "dataGridViewPrimitiveProperties";
            this.dataGridViewPrimitiveProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPrimitiveProperties.Size = new System.Drawing.Size(438, 121);
            this.dataGridViewPrimitiveProperties.TabIndex = 8;
            this.dataGridViewPrimitiveProperties.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPrimitiveProperties_CellValueChanged);
            this.dataGridViewPrimitiveProperties.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
            this.dataGridViewPrimitiveProperties.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewPrimitiveProperties_DefaultValuesNeeded);
            this.dataGridViewPrimitiveProperties.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewPrimitiveProperties_KeyDown);
            // 
            // dataGridViewRelationships
            // 
            this.dataGridViewRelationships.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewRelationships.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRelationships.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewRelationships.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewRelationships.Name = "dataGridViewRelationships";
            this.dataGridViewRelationships.Size = new System.Drawing.Size(438, 124);
            this.dataGridViewRelationships.TabIndex = 9;
            this.dataGridViewRelationships.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewRelationships_CellValueChanged);
            this.dataGridViewRelationships.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewCollectionProperties_DataError);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(117, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Abstract";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(202, 236);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Virtual";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Base Entity:";
            // 
            // cmbInherits
            // 
            this.cmbInherits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInherits.DisplayMember = "Label";
            this.cmbInherits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInherits.FormattingEnabled = true;
            this.cmbInherits.Location = new System.Drawing.Point(97, 97);
            this.cmbInherits.Name = "cmbInherits";
            this.cmbInherits.Size = new System.Drawing.Size(354, 21);
            this.cmbInherits.TabIndex = 15;
            this.cmbInherits.ValueMember = "Guid";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Summary:";
            // 
            // txtSummary
            // 
            this.txtSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSummary.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "summary", true));
            this.txtSummary.Location = new System.Drawing.Point(97, 155);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(354, 45);
            this.txtSummary.TabIndex = 17;
            // 
            // dataGridViewInheritedPrimitiveProperties
            // 
            this.dataGridViewInheritedPrimitiveProperties.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewInheritedPrimitiveProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewInheritedPrimitiveProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewInheritedPrimitiveProperties.GridColor = System.Drawing.Color.LightGray;
            this.dataGridViewInheritedPrimitiveProperties.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewInheritedPrimitiveProperties.Name = "dataGridViewInheritedPrimitiveProperties";
            this.dataGridViewInheritedPrimitiveProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewInheritedPrimitiveProperties.Size = new System.Drawing.Size(438, 120);
            this.dataGridViewInheritedPrimitiveProperties.TabIndex = 9;
            // 
            // btnEditStaticData
            // 
            this.btnEditStaticData.BackColor = System.Drawing.Color.Transparent;
            this.btnEditStaticData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditStaticData.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditStaticData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnEditStaticData.Location = new System.Drawing.Point(351, 231);
            this.btnEditStaticData.Name = "btnEditStaticData";
            this.btnEditStaticData.Size = new System.Drawing.Size(97, 23);
            this.btnEditStaticData.TabIndex = 20;
            this.btnEditStaticData.Text = "Edit Data";
            this.btnEditStaticData.UseVisualStyleBackColor = false;
            this.btnEditStaticData.Visible = false;
            this.btnEditStaticData.Click += new System.EventHandler(this.btnEditStaticData_Click);
            // 
            // cmbFunctionalId
            // 
            this.cmbFunctionalId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFunctionalId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFunctionalId.FormattingEnabled = true;
            this.cmbFunctionalId.Location = new System.Drawing.Point(97, 126);
            this.cmbFunctionalId.Name = "cmbFunctionalId";
            this.cmbFunctionalId.Size = new System.Drawing.Size(220, 21);
            this.cmbFunctionalId.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Functional ID: ";
            // 
            // txtExample
            // 
            this.txtExample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExample.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "example", true));
            this.txtExample.Location = new System.Drawing.Point(97, 207);
            this.txtExample.Name = "txtExample";
            this.txtExample.Size = new System.Drawing.Size(354, 20);
            this.txtExample.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Example:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(278, 236);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "StaticData";
            // 
            // chkIsStaticData
            // 
            this.chkIsStaticData.AutoSize = true;
            this.chkIsStaticData.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSource, "isStaticData", true));
            this.chkIsStaticData.Location = new System.Drawing.Point(257, 235);
            this.chkIsStaticData.Name = "chkIsStaticData";
            this.chkIsStaticData.Size = new System.Drawing.Size(15, 14);
            this.chkIsStaticData.TabIndex = 28;
            this.chkIsStaticData.UseVisualStyleBackColor = true;
            this.chkIsStaticData.CheckedChanged += new System.EventHandler(this.chkIsStaticData_CheckedChanged);
            // 
            // splitContainerProperties
            // 
            this.splitContainerProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerProperties.Location = new System.Drawing.Point(0, 0);
            this.splitContainerProperties.Name = "splitContainerProperties";
            this.splitContainerProperties.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerProperties.Panel1
            // 
            this.splitContainerProperties.Panel1.Controls.Add(this.dataGridViewPrimitiveProperties);
            // 
            // splitContainerProperties.Panel2
            // 
            this.splitContainerProperties.Panel2.Controls.Add(this.dataGridViewInheritedPrimitiveProperties);
            this.splitContainerProperties.Size = new System.Drawing.Size(438, 242);
            this.splitContainerProperties.SplitterDistance = 121;
            this.splitContainerProperties.SplitterWidth = 1;
            this.splitContainerProperties.TabIndex = 0;
            // 
            // splitContainerRelationships
            // 
            this.splitContainerRelationships.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRelationships.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRelationships.Name = "splitContainerRelationships";
            this.splitContainerRelationships.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRelationships.Panel1
            // 
            this.splitContainerRelationships.Panel1.Controls.Add(this.dataGridViewRelationships);
            // 
            // splitContainerRelationships.Panel2
            // 
            this.splitContainerRelationships.Panel2.Controls.Add(this.dataGridViewInheritedRelationships);
            this.splitContainerRelationships.Size = new System.Drawing.Size(438, 238);
            this.splitContainerRelationships.SplitterDistance = 124;
            this.splitContainerRelationships.SplitterWidth = 1;
            this.splitContainerRelationships.TabIndex = 0;
            // 
            // dataGridViewInheritedRelationships
            // 
            this.dataGridViewInheritedRelationships.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewInheritedRelationships.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInheritedRelationships.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewInheritedRelationships.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewInheritedRelationships.Name = "dataGridViewInheritedRelationships";
            this.dataGridViewInheritedRelationships.Size = new System.Drawing.Size(438, 113);
            this.dataGridViewInheritedRelationships.TabIndex = 10;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMain.Location = new System.Drawing.Point(10, 265);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerPropBox);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerRelBox);
            this.splitContainerMain.Size = new System.Drawing.Size(438, 542);
            this.splitContainerMain.SplitterDistance = 271;
            this.splitContainerMain.TabIndex = 29;
            // 
            // splitContainerPropBox
            // 
            this.splitContainerPropBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPropBox.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerPropBox.IsSplitterFixed = true;
            this.splitContainerPropBox.Location = new System.Drawing.Point(0, 0);
            this.splitContainerPropBox.Name = "splitContainerPropBox";
            this.splitContainerPropBox.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerPropBox.Panel1
            // 
            this.splitContainerPropBox.Panel1.Controls.Add(this.label10);
            this.splitContainerPropBox.Panel1MinSize = 18;
            // 
            // splitContainerPropBox.Panel2
            // 
            this.splitContainerPropBox.Panel2.Controls.Add(this.splitContainerProperties);
            this.splitContainerPropBox.Size = new System.Drawing.Size(438, 271);
            this.splitContainerPropBox.SplitterDistance = 25;
            this.splitContainerPropBox.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(-1, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(199, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Properties (Direct && Inherited):";
            // 
            // splitContainerRelBox
            // 
            this.splitContainerRelBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRelBox.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerRelBox.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRelBox.Name = "splitContainerRelBox";
            this.splitContainerRelBox.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRelBox.Panel1
            // 
            this.splitContainerRelBox.Panel1.Controls.Add(this.checkBoxShowAllRelationships);
            this.splitContainerRelBox.Panel1.Controls.Add(this.label11);
            this.splitContainerRelBox.Panel1MinSize = 18;
            // 
            // splitContainerRelBox.Panel2
            // 
            this.splitContainerRelBox.Panel2.Controls.Add(this.splitContainerRelationships);
            this.splitContainerRelBox.Size = new System.Drawing.Size(438, 267);
            this.splitContainerRelBox.SplitterDistance = 25;
            this.splitContainerRelBox.TabIndex = 0;
            // 
            // checkBoxShowAllRelationships
            // 
            this.checkBoxShowAllRelationships.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxShowAllRelationships.AutoSize = true;
            this.checkBoxShowAllRelationships.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxShowAllRelationships.Location = new System.Drawing.Point(277, 10);
            this.checkBoxShowAllRelationships.Name = "checkBoxShowAllRelationships";
            this.checkBoxShowAllRelationships.Size = new System.Drawing.Size(158, 17);
            this.checkBoxShowAllRelationships.TabIndex = 1;
            this.checkBoxShowAllRelationships.Text = "Show All Relationships";
            this.checkBoxShowAllRelationships.UseVisualStyleBackColor = true;
            this.checkBoxShowAllRelationships.CheckedChanged += new System.EventHandler(this.checkBoxShowAllRelationships_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(-1, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(217, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Relationships (Direct && Inherited):";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(8, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(130, 24);
            this.label12.TabIndex = 30;
            this.label12.Text = "Properties";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.button1.Location = new System.Drawing.Point(323, 125);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 31;
            this.button1.Text = "ADD FUNCTIONAL ID";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnAddFunctionalId_Click);
            // 
            // EntityEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.chkIsStaticData);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtExample);
            this.Controls.Add(this.cmbFunctionalId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnEditStaticData);
            this.Controls.Add(this.txtSummary);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbInherits);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkIsVirtual);
            this.Controls.Add(this.chkIsAbstract);
            this.Controls.Add(this.txtLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Name = "EntityEditor";
            this.Size = new System.Drawing.Size(460, 819);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrimitiveProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePrimitiveProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRelationships)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCollectionProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEntities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInheritedPrimitiveProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceInheritedPrimitiveProperties)).EndInit();
            this.splitContainerProperties.Panel1.ResumeLayout(false);
            this.splitContainerProperties.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerProperties)).EndInit();
            this.splitContainerProperties.ResumeLayout(false);
            this.splitContainerRelationships.Panel1.ResumeLayout(false);
            this.splitContainerRelationships.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRelationships)).EndInit();
            this.splitContainerRelationships.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInheritedRelationships)).EndInit();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerPropBox.Panel1.ResumeLayout(false);
            this.splitContainerPropBox.Panel1.PerformLayout();
            this.splitContainerPropBox.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPropBox)).EndInit();
            this.splitContainerPropBox.ResumeLayout(false);
            this.splitContainerRelBox.Panel1.ResumeLayout(false);
            this.splitContainerRelBox.Panel1.PerformLayout();
            this.splitContainerRelBox.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRelBox)).EndInit();
            this.splitContainerRelBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceInheritedRelationships)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkIsAbstract;
        private System.Windows.Forms.CheckBox chkIsVirtual;
        private System.Windows.Forms.DataGridView dataGridViewPrimitiveProperties;
        private System.Windows.Forms.BindingSource bindingSourcePrimitiveProperties;
        private System.Windows.Forms.DataGridView dataGridViewRelationships;
        private System.Windows.Forms.BindingSource bindingSourceCollectionProperties;
        private System.Windows.Forms.BindingSource bindingSourceEntities;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbInherits;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.Button btnEditStaticData;
        private System.Windows.Forms.ComboBox cmbFunctionalId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtExample;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkIsStaticData;
        private System.Windows.Forms.DataGridView dataGridViewInheritedPrimitiveProperties;
        private System.Windows.Forms.BindingSource bindingSourceInheritedPrimitiveProperties;
        private System.Windows.Forms.SplitContainer splitContainerProperties;
        private System.Windows.Forms.SplitContainer splitContainerRelationships;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.SplitContainer splitContainerPropBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.SplitContainer splitContainerRelBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBoxShowAllRelationships;
        private System.Windows.Forms.DataGridView dataGridViewInheritedRelationships;
        private System.Windows.Forms.BindingSource bindingSourceInheritedRelationships;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button1;
    }
}
