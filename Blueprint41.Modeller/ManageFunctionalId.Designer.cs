using Blueprint41.Modeller.Schemas;

namespace Blueprint41.Modeller
{
    partial class ManageFunctionalId
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
            this.components = new System.ComponentModel.Container();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dataGridViewFunctionalIds = new System.Windows.Forms.DataGridView();
            this.dgvTextBoxColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtbcModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtbcGuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtbcXml = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTextBoxColumnPrefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTextBoxColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcbcDefault = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.bindingSourceFunctionalIds = new System.Windows.Forms.BindingSource(this.components);
            this.btnNew = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkDefault = new System.Windows.Forms.CheckBox();
            this.lblDefault = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbPropertiesPanel = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFunctionalIds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFunctionalIds)).BeginInit();
            this.gbPropertiesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(97, 80);
            this.txtPrefix.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(156, 20);
            this.txtPrefix.TabIndex = 0;
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(9, 83);
            this.lblPrefix.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(33, 13);
            this.lblPrefix.TabIndex = 1;
            this.lblPrefix.Text = "Prefix";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(9, 22);
            this.lblType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(31, 13);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Type";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(97, 19);
            this.cmbType.Margin = new System.Windows.Forms.Padding(2);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(156, 21);
            this.cmbType.TabIndex = 3;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(97, 137);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 29);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(9, 52);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 7;
            this.lblName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(97, 49);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(156, 20);
            this.txtName.TabIndex = 8;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(207, 382);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 29);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Enabled = false;
            this.btnDelete.ForeColor = System.Drawing.Color.DarkRed;
            this.btnDelete.Location = new System.Drawing.Point(288, 382);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 29);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dataGridViewFunctionalIds
            // 
            this.dataGridViewFunctionalIds.AllowUserToAddRows = false;
            this.dataGridViewFunctionalIds.AllowUserToDeleteRows = false;
            this.dataGridViewFunctionalIds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewFunctionalIds.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFunctionalIds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFunctionalIds.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvTextBoxColumnName,
            this.dgvtbcModel,
            this.dgvtbcGuid,
            this.dgvtbcXml,
            this.dgvTextBoxColumnPrefix,
            this.dgvTextBoxColumnType,
            this.dgvcbcDefault});
            this.dataGridViewFunctionalIds.Enabled = false;
            this.dataGridViewFunctionalIds.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewFunctionalIds.MultiSelect = false;
            this.dataGridViewFunctionalIds.Name = "dataGridViewFunctionalIds";
            this.dataGridViewFunctionalIds.ReadOnly = true;
            this.dataGridViewFunctionalIds.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFunctionalIds.Size = new System.Drawing.Size(351, 364);
            this.dataGridViewFunctionalIds.TabIndex = 12;
            this.dataGridViewFunctionalIds.SelectionChanged += new System.EventHandler(this.dataGridViewFunctionalIds_SelectionChanged);
            // 
            // dgvTextBoxColumnName
            // 
            this.dgvTextBoxColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvTextBoxColumnName.DataPropertyName = "Name";
            this.dgvTextBoxColumnName.HeaderText = "Name";
            this.dgvTextBoxColumnName.Name = "dgvTextBoxColumnName";
            this.dgvTextBoxColumnName.ReadOnly = true;
            this.dgvTextBoxColumnName.Width = 60;
            // 
            // dgvtbcModel
            // 
            this.dgvtbcModel.DataPropertyName = "Model";
            this.dgvtbcModel.HeaderText = "Model";
            this.dgvtbcModel.Name = "dgvtbcModel";
            this.dgvtbcModel.ReadOnly = true;
            this.dgvtbcModel.Visible = false;
            // 
            // dgvtbcGuid
            // 
            this.dgvtbcGuid.DataPropertyName = "Guid";
            this.dgvtbcGuid.HeaderText = "Guid";
            this.dgvtbcGuid.Name = "dgvtbcGuid";
            this.dgvtbcGuid.ReadOnly = true;
            this.dgvtbcGuid.Visible = false;
            // 
            // dgvtbcXml
            // 
            this.dgvtbcXml.DataPropertyName = "Xml";
            this.dgvtbcXml.HeaderText = "Xml";
            this.dgvtbcXml.Name = "dgvtbcXml";
            this.dgvtbcXml.ReadOnly = true;
            this.dgvtbcXml.Visible = false;
            // 
            // dgvTextBoxColumnPrefix
            // 
            this.dgvTextBoxColumnPrefix.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvTextBoxColumnPrefix.DataPropertyName = "Value";
            this.dgvTextBoxColumnPrefix.HeaderText = "Prefix";
            this.dgvTextBoxColumnPrefix.Name = "dgvTextBoxColumnPrefix";
            this.dgvTextBoxColumnPrefix.ReadOnly = true;
            this.dgvTextBoxColumnPrefix.Width = 58;
            // 
            // dgvTextBoxColumnType
            // 
            this.dgvTextBoxColumnType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvTextBoxColumnType.DataPropertyName = "Type";
            this.dgvTextBoxColumnType.HeaderText = "Type";
            this.dgvTextBoxColumnType.Name = "dgvTextBoxColumnType";
            this.dgvTextBoxColumnType.ReadOnly = true;
            this.dgvTextBoxColumnType.Width = 56;
            // 
            // dgvcbcDefault
            // 
            this.dgvcbcDefault.DataPropertyName = "IsDefault";
            this.dgvcbcDefault.HeaderText = "Default";
            this.dgvcbcDefault.Name = "dgvcbcDefault";
            this.dgvcbcDefault.ReadOnly = true;
            this.dgvcbcDefault.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvcbcDefault.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Enabled = false;
            this.btnNew.Location = new System.Drawing.Point(126, 382);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 29);
            this.btnNew.TabIndex = 13;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(178, 137);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 29);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkDefault
            // 
            this.chkDefault.AutoSize = true;
            this.chkDefault.Location = new System.Drawing.Point(97, 111);
            this.chkDefault.Name = "chkDefault";
            this.chkDefault.Size = new System.Drawing.Size(15, 14);
            this.chkDefault.TabIndex = 16;
            this.chkDefault.UseVisualStyleBackColor = true;
            // 
            // lblDefault
            // 
            this.lblDefault.AutoSize = true;
            this.lblDefault.Location = new System.Drawing.Point(9, 110);
            this.lblDefault.Name = "lblDefault";
            this.lblDefault.Size = new System.Drawing.Size(74, 13);
            this.lblDefault.TabIndex = 15;
            this.lblDefault.Text = "Set as Default";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Prefix";
            this.dataGridViewTextBoxColumn2.HeaderText = "Prefix";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Type";
            this.dataGridViewTextBoxColumn3.HeaderText = "Type";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Value";
            this.dataGridViewTextBoxColumn4.HeaderText = "Prefix";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Type";
            this.dataGridViewTextBoxColumn5.HeaderText = "Type";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Type";
            this.dataGridViewTextBoxColumn6.HeaderText = "Type";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Type";
            this.dataGridViewTextBoxColumn7.HeaderText = "Type";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // gbPropertiesPanel
            // 
            this.gbPropertiesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPropertiesPanel.Controls.Add(this.chkDefault);
            this.gbPropertiesPanel.Controls.Add(this.cmbType);
            this.gbPropertiesPanel.Controls.Add(this.lblDefault);
            this.gbPropertiesPanel.Controls.Add(this.lblName);
            this.gbPropertiesPanel.Controls.Add(this.btnCancel);
            this.gbPropertiesPanel.Controls.Add(this.txtName);
            this.gbPropertiesPanel.Controls.Add(this.txtPrefix);
            this.gbPropertiesPanel.Controls.Add(this.btnSave);
            this.gbPropertiesPanel.Controls.Add(this.lblPrefix);
            this.gbPropertiesPanel.Controls.Add(this.lblType);
            this.gbPropertiesPanel.Location = new System.Drawing.Point(369, 12);
            this.gbPropertiesPanel.Name = "gbPropertiesPanel";
            this.gbPropertiesPanel.Size = new System.Drawing.Size(262, 174);
            this.gbPropertiesPanel.TabIndex = 16;
            this.gbPropertiesPanel.TabStop = false;
            this.gbPropertiesPanel.Text = "Properties";
            // 
            // ManageFunctionalId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 423);
            this.Controls.Add(this.gbPropertiesPanel);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.dataGridViewFunctionalIds);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageFunctionalId";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Functional Id\'s";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFunctionalIds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFunctionalIds)).EndInit();
            this.gbPropertiesPanel.ResumeLayout(false);
            this.gbPropertiesPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dataGridViewFunctionalIds;
        private System.Windows.Forms.BindingSource bindingSourceFunctionalIds;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.CheckBox chkDefault;
        private System.Windows.Forms.Label lblDefault;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtbcModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtbcGuid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtbcXml;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxColumnPrefix;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxColumnType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvcbcDefault;
        private System.Windows.Forms.GroupBox gbPropertiesPanel;
    }
}