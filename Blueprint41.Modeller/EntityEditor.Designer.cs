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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnEditStaticData = new System.Windows.Forms.Button();
            this.cmbFunctionalId = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddFunctionalId = new System.Windows.Forms.Label();
            this.txtExample = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chkIsStaticData = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrimitiveProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePrimitiveProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRelationships)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCollectionProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEntities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entity Name:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "name", true));
            this.txtName.Location = new System.Drawing.Point(97, 32);
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
            this.txtLabel.Location = new System.Drawing.Point(97, 6);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(354, 20);
            this.txtLabel.TabIndex = 3;
            this.txtLabel.Leave += new System.EventHandler(this.txtLabel_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Neo4j Label:";
            // 
            // chkIsAbstract
            // 
            this.chkIsAbstract.AutoSize = true;
            this.chkIsAbstract.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSource, "abstract", true));
            this.chkIsAbstract.Location = new System.Drawing.Point(97, 197);
            this.chkIsAbstract.Name = "chkIsAbstract";
            this.chkIsAbstract.Size = new System.Drawing.Size(15, 14);
            this.chkIsAbstract.TabIndex = 4;
            this.chkIsAbstract.UseVisualStyleBackColor = true;
            // 
            // chkIsVirtual
            // 
            this.chkIsVirtual.AutoSize = true;
            this.chkIsVirtual.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSource, "virtual", true));
            this.chkIsVirtual.Location = new System.Drawing.Point(97, 222);
            this.chkIsVirtual.Name = "chkIsVirtual";
            this.chkIsVirtual.Size = new System.Drawing.Size(15, 14);
            this.chkIsVirtual.TabIndex = 5;
            this.chkIsVirtual.UseVisualStyleBackColor = true;
            this.chkIsVirtual.CheckedChanged += new System.EventHandler(this.chkIsVirtual_CheckedChanged);
            // 
            // dataGridViewPrimitiveProperties
            // 
            this.dataGridViewPrimitiveProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewPrimitiveProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPrimitiveProperties.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewPrimitiveProperties.Name = "dataGridViewPrimitiveProperties";
            this.dataGridViewPrimitiveProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPrimitiveProperties.Size = new System.Drawing.Size(431, 128);
            this.dataGridViewPrimitiveProperties.TabIndex = 8;
            this.dataGridViewPrimitiveProperties.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPrimitiveProperties_CellValueChanged);
            this.dataGridViewPrimitiveProperties.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
            this.dataGridViewPrimitiveProperties.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewPrimitiveProperties_DefaultValuesNeeded);
            this.dataGridViewPrimitiveProperties.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewPrimitiveProperties_KeyDown);
            // 
            // dataGridViewRelationships
            // 
            this.dataGridViewRelationships.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewRelationships.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRelationships.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewRelationships.Name = "dataGridViewRelationships";
            this.dataGridViewRelationships.Size = new System.Drawing.Size(431, 43);
            this.dataGridViewRelationships.TabIndex = 9;
            this.dataGridViewRelationships.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewRelationships_CellValueChanged);
            this.dataGridViewRelationships.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewCollectionProperties_DataError);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Is Abstract?";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Is Virtual?";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
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
            this.cmbInherits.Location = new System.Drawing.Point(97, 59);
            this.cmbInherits.Name = "cmbInherits";
            this.cmbInherits.Size = new System.Drawing.Size(354, 21);
            this.cmbInherits.TabIndex = 15;
            this.cmbInherits.ValueMember = "Guid";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Summary:";
            // 
            // txtSummary
            // 
            this.txtSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSummary.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "summary", true));
            this.txtSummary.Location = new System.Drawing.Point(97, 117);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(354, 45);
            this.txtSummary.TabIndex = 17;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewPrimitiveProperties);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewRelationships);
            this.splitContainer1.Size = new System.Drawing.Size(431, 217);
            this.splitContainer1.SplitterDistance = 128;
            this.splitContainer1.TabIndex = 18;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(14, 274);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            this.splitContainer2.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Panel1_Paint);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer2.Size = new System.Drawing.Size(437, 295);
            this.splitContainer2.SplitterDistance = 184;
            this.splitContainer2.TabIndex = 19;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(431, 98);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnEditStaticData
            // 
            this.btnEditStaticData.Location = new System.Drawing.Point(127, 241);
            this.btnEditStaticData.Name = "btnEditStaticData";
            this.btnEditStaticData.Size = new System.Drawing.Size(104, 23);
            this.btnEditStaticData.TabIndex = 20;
            this.btnEditStaticData.Text = "Edit Data";
            this.btnEditStaticData.UseVisualStyleBackColor = true;
            this.btnEditStaticData.Visible = false;
            this.btnEditStaticData.Click += new System.EventHandler(this.btnEditStaticData_Click);
            // 
            // cmbFunctionalId
            // 
            this.cmbFunctionalId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFunctionalId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFunctionalId.FormattingEnabled = true;
            this.cmbFunctionalId.Location = new System.Drawing.Point(97, 88);
            this.cmbFunctionalId.Name = "cmbFunctionalId";
            this.cmbFunctionalId.Size = new System.Drawing.Size(255, 21);
            this.cmbFunctionalId.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Functional ID: ";
            // 
            // btnAddFunctionalId
            // 
            this.btnAddFunctionalId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFunctionalId.AutoSize = true;
            this.btnAddFunctionalId.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddFunctionalId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFunctionalId.ForeColor = System.Drawing.Color.Blue;
            this.btnAddFunctionalId.Location = new System.Drawing.Point(358, 92);
            this.btnAddFunctionalId.Name = "btnAddFunctionalId";
            this.btnAddFunctionalId.Size = new System.Drawing.Size(90, 13);
            this.btnAddFunctionalId.TabIndex = 24;
            this.btnAddFunctionalId.Text = "Add Functional Id";
            this.btnAddFunctionalId.Click += new System.EventHandler(this.btnAddFunctionalId_Click);
            // 
            // txtExample
            // 
            this.txtExample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExample.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "example", true));
            this.txtExample.Location = new System.Drawing.Point(97, 169);
            this.txtExample.Name = "txtExample";
            this.txtExample.Size = new System.Drawing.Size(354, 20);
            this.txtExample.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Example:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 245);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Is StaticData?";
            // 
            // chkIsStaticData
            // 
            this.chkIsStaticData.AutoSize = true;
            this.chkIsStaticData.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSource, "isStaticData", true));
            this.chkIsStaticData.Location = new System.Drawing.Point(97, 245);
            this.chkIsStaticData.Name = "chkIsStaticData";
            this.chkIsStaticData.Size = new System.Drawing.Size(15, 14);
            this.chkIsStaticData.TabIndex = 28;
            this.chkIsStaticData.UseVisualStyleBackColor = true;
            this.chkIsStaticData.CheckedChanged += new System.EventHandler(this.chkIsStaticData_CheckedChanged);
            // 
            // EntityEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkIsStaticData);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtExample);
            this.Controls.Add(this.btnAddFunctionalId);
            this.Controls.Add(this.cmbFunctionalId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnEditStaticData);
            this.Controls.Add(this.splitContainer2);
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
            this.Size = new System.Drawing.Size(460, 581);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrimitiveProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePrimitiveProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRelationships)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCollectionProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEntities)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnEditStaticData;
        private System.Windows.Forms.ComboBox cmbFunctionalId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label btnAddFunctionalId;
        private System.Windows.Forms.TextBox txtExample;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkIsStaticData;
    }
}
