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
            this.lblEntityName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkIsAbstract = new System.Windows.Forms.CheckBox();
            this.chkIsVirtual = new System.Windows.Forms.CheckBox();
            this.bindingSourcePrimitiveProperties = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceCollectionProperties = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceEntities = new System.Windows.Forms.BindingSource(this.components);
            this.lblAbstract = new System.Windows.Forms.Label();
            this.lblVirtual = new System.Windows.Forms.Label();
            this.lblBaseEntity = new System.Windows.Forms.Label();
            this.cmbInherits = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.btnEditStaticData = new System.Windows.Forms.Button();
            this.cmbFunctionalId = new System.Windows.Forms.ComboBox();
            this.lblFunctionaId = new System.Windows.Forms.Label();
            this.txtExample = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStaticData = new System.Windows.Forms.Label();
            this.chkIsStaticData = new System.Windows.Forms.CheckBox();
            this.bindingSourceInheritedPrimitiveProperties = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceInheritedRelationships = new System.Windows.Forms.BindingSource(this.components);
            this.btnAddFunctionalId = new System.Windows.Forms.Button();
            this.btnPin = new System.Windows.Forms.Button();
            this.gbProperties = new System.Windows.Forms.GroupBox();
            this.pre = new Blueprint41.Modeller.PrimitiveRelationshipEditor();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePrimitiveProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCollectionProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEntities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceInheritedPrimitiveProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceInheritedRelationships)).BeginInit();
            this.gbProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblEntityName
            // 
            this.lblEntityName.AutoSize = true;
            this.lblEntityName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblEntityName.Location = new System.Drawing.Point(6, 63);
            this.lblEntityName.Name = "lblEntityName";
            this.lblEntityName.Size = new System.Drawing.Size(67, 13);
            this.lblEntityName.TabIndex = 0;
            this.lblEntityName.Text = "Entity Name:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "name", true));
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtName.Location = new System.Drawing.Point(90, 57);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(338, 20);
            this.txtName.TabIndex = 2;
            this.txtName.TextChanged += new System.EventHandler(this.TxtName_TextChanged);
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
            this.txtLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtLabel.Location = new System.Drawing.Point(90, 31);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(338, 20);
            this.txtLabel.TabIndex = 1;
            this.txtLabel.TextChanged += new System.EventHandler(this.TxtLabel_TextChanged);
            this.txtLabel.Leave += new System.EventHandler(this.txtLabel_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Neo4j Label:";
            // 
            // chkIsAbstract
            // 
            this.chkIsAbstract.AutoSize = true;
            this.chkIsAbstract.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSource, "abstract", true));
            this.chkIsAbstract.Location = new System.Drawing.Point(90, 222);
            this.chkIsAbstract.Name = "chkIsAbstract";
            this.chkIsAbstract.Size = new System.Drawing.Size(15, 14);
            this.chkIsAbstract.TabIndex = 8;
            this.chkIsAbstract.UseVisualStyleBackColor = true;
            this.chkIsAbstract.CheckedChanged += new System.EventHandler(this.chkIsAbstract_CheckedChanged);
            // 
            // chkIsVirtual
            // 
            this.chkIsVirtual.AutoSize = true;
            this.chkIsVirtual.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSource, "virtual", true));
            this.chkIsVirtual.Location = new System.Drawing.Point(174, 223);
            this.chkIsVirtual.Name = "chkIsVirtual";
            this.chkIsVirtual.Size = new System.Drawing.Size(15, 14);
            this.chkIsVirtual.TabIndex = 8;
            this.chkIsVirtual.UseVisualStyleBackColor = true;
            this.chkIsVirtual.CheckedChanged += new System.EventHandler(this.chkIsVirtual_CheckedChanged);
            // 
            // lblAbstract
            // 
            this.lblAbstract.AutoSize = true;
            this.lblAbstract.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblAbstract.Location = new System.Drawing.Point(111, 223);
            this.lblAbstract.Name = "lblAbstract";
            this.lblAbstract.Size = new System.Drawing.Size(46, 13);
            this.lblAbstract.TabIndex = 12;
            this.lblAbstract.Text = "Abstract";
            // 
            // lblVirtual
            // 
            this.lblVirtual.AutoSize = true;
            this.lblVirtual.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblVirtual.Location = new System.Drawing.Point(195, 223);
            this.lblVirtual.Name = "lblVirtual";
            this.lblVirtual.Size = new System.Drawing.Size(36, 13);
            this.lblVirtual.TabIndex = 13;
            this.lblVirtual.Text = "Virtual";
            // 
            // lblBaseEntity
            // 
            this.lblBaseEntity.AutoSize = true;
            this.lblBaseEntity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblBaseEntity.Location = new System.Drawing.Point(6, 160);
            this.lblBaseEntity.Name = "lblBaseEntity";
            this.lblBaseEntity.Size = new System.Drawing.Size(63, 13);
            this.lblBaseEntity.TabIndex = 14;
            this.lblBaseEntity.Text = "Base Entity:";
            // 
            // cmbInherits
            // 
            this.cmbInherits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInherits.DisplayMember = "Label";
            this.cmbInherits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInherits.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbInherits.FormattingEnabled = true;
            this.cmbInherits.Location = new System.Drawing.Point(89, 160);
            this.cmbInherits.Name = "cmbInherits";
            this.cmbInherits.Size = new System.Drawing.Size(338, 21);
            this.cmbInherits.TabIndex = 3;
            this.cmbInherits.ValueMember = "Guid";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label8.Location = new System.Drawing.Point(6, 86);
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
            this.txtSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtSummary.Location = new System.Drawing.Point(89, 83);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(338, 45);
            this.txtSummary.TabIndex = 6;
            // 
            // btnEditStaticData
            // 
            this.btnEditStaticData.BackColor = System.Drawing.Color.Transparent;
            this.btnEditStaticData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditStaticData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnEditStaticData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(184)))), ((int)(((byte)(235)))));
            this.btnEditStaticData.Location = new System.Drawing.Point(323, 219);
            this.btnEditStaticData.Name = "btnEditStaticData";
            this.btnEditStaticData.Size = new System.Drawing.Size(104, 23);
            this.btnEditStaticData.TabIndex = 10;
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
            this.cmbFunctionalId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbFunctionalId.FormattingEnabled = true;
            this.cmbFunctionalId.Location = new System.Drawing.Point(90, 187);
            this.cmbFunctionalId.Name = "cmbFunctionalId";
            this.cmbFunctionalId.Size = new System.Drawing.Size(204, 21);
            this.cmbFunctionalId.TabIndex = 4;
            // 
            // lblFunctionaId
            // 
            this.lblFunctionaId.AutoSize = true;
            this.lblFunctionaId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblFunctionaId.Location = new System.Drawing.Point(6, 191);
            this.lblFunctionaId.Name = "lblFunctionaId";
            this.lblFunctionaId.Size = new System.Drawing.Size(76, 13);
            this.lblFunctionaId.TabIndex = 22;
            this.lblFunctionaId.Text = "Functional ID: ";
            // 
            // txtExample
            // 
            this.txtExample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExample.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "example", true));
            this.txtExample.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtExample.Location = new System.Drawing.Point(90, 134);
            this.txtExample.Name = "txtExample";
            this.txtExample.Size = new System.Drawing.Size(338, 20);
            this.txtExample.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label4.Location = new System.Drawing.Point(6, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Example:";
            // 
            // lblStaticData
            // 
            this.lblStaticData.AutoSize = true;
            this.lblStaticData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblStaticData.Location = new System.Drawing.Point(258, 224);
            this.lblStaticData.Name = "lblStaticData";
            this.lblStaticData.Size = new System.Drawing.Size(57, 13);
            this.lblStaticData.TabIndex = 27;
            this.lblStaticData.Text = "StaticData";
            // 
            // chkIsStaticData
            // 
            this.chkIsStaticData.AutoSize = true;
            this.chkIsStaticData.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSource, "isStaticData", true));
            this.chkIsStaticData.Location = new System.Drawing.Point(237, 223);
            this.chkIsStaticData.Name = "chkIsStaticData";
            this.chkIsStaticData.Size = new System.Drawing.Size(15, 14);
            this.chkIsStaticData.TabIndex = 9;
            this.chkIsStaticData.UseVisualStyleBackColor = true;
            this.chkIsStaticData.CheckedChanged += new System.EventHandler(this.chkIsStaticData_CheckedChanged);
            // 
            // btnAddFunctionalId
            // 
            this.btnAddFunctionalId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFunctionalId.BackColor = System.Drawing.Color.Transparent;
            this.btnAddFunctionalId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFunctionalId.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFunctionalId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(184)))), ((int)(((byte)(235)))));
            this.btnAddFunctionalId.Location = new System.Drawing.Point(300, 187);
            this.btnAddFunctionalId.Name = "btnAddFunctionalId";
            this.btnAddFunctionalId.Size = new System.Drawing.Size(128, 23);
            this.btnAddFunctionalId.TabIndex = 5;
            this.btnAddFunctionalId.Text = "ADD FUNCTIONAL ID";
            this.btnAddFunctionalId.UseVisualStyleBackColor = false;
            this.btnAddFunctionalId.Click += new System.EventHandler(this.btnAddFunctionalId_Click);
            // 
            // btnPin
            // 
            this.btnPin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPin.BackColor = System.Drawing.Color.Transparent;
            this.btnPin.FlatAppearance.BorderSize = 0;
            this.btnPin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPin.Image = global::Blueprint41.Modeller.Properties.Resources.thumbtack_solid;
            this.btnPin.Location = new System.Drawing.Point(422, 5);
            this.btnPin.Name = "btnPin";
            this.btnPin.Size = new System.Drawing.Size(18, 14);
            this.btnPin.TabIndex = 32;
            this.btnPin.UseVisualStyleBackColor = false;
            this.btnPin.Click += new System.EventHandler(this.btnPin_Click);
            // 
            // gbProperties
            // 
            this.gbProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbProperties.Controls.Add(this.label2);
            this.gbProperties.Controls.Add(this.lblEntityName);
            this.gbProperties.Controls.Add(this.txtName);
            this.gbProperties.Controls.Add(this.btnAddFunctionalId);
            this.gbProperties.Controls.Add(this.txtLabel);
            this.gbProperties.Controls.Add(this.chkIsAbstract);
            this.gbProperties.Controls.Add(this.chkIsStaticData);
            this.gbProperties.Controls.Add(this.chkIsVirtual);
            this.gbProperties.Controls.Add(this.lblStaticData);
            this.gbProperties.Controls.Add(this.lblAbstract);
            this.gbProperties.Controls.Add(this.label4);
            this.gbProperties.Controls.Add(this.lblVirtual);
            this.gbProperties.Controls.Add(this.txtExample);
            this.gbProperties.Controls.Add(this.lblBaseEntity);
            this.gbProperties.Controls.Add(this.cmbFunctionalId);
            this.gbProperties.Controls.Add(this.cmbInherits);
            this.gbProperties.Controls.Add(this.lblFunctionaId);
            this.gbProperties.Controls.Add(this.label8);
            this.gbProperties.Controls.Add(this.btnEditStaticData);
            this.gbProperties.Controls.Add(this.txtSummary);
            this.gbProperties.Font = new System.Drawing.Font("Consolas", 15.25F);
            this.gbProperties.Location = new System.Drawing.Point(7, 24);
            this.gbProperties.Name = "gbProperties";
            this.gbProperties.Size = new System.Drawing.Size(434, 258);
            this.gbProperties.TabIndex = 33;
            this.gbProperties.TabStop = false;
            this.gbProperties.Text = "Properties";
            // 
            // pre
            // 
            this.pre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pre.Location = new System.Drawing.Point(3, 290);
            this.pre.Name = "pre";
            this.pre.Size = new System.Drawing.Size(443, 529);
            this.pre.TabIndex = 31;
            // 
            // EntityEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.gbProperties);
            this.Controls.Add(this.btnPin);
            this.Controls.Add(this.pre);
            this.MinimumSize = new System.Drawing.Size(449, 0);
            this.Name = "EntityEditor";
            this.Size = new System.Drawing.Size(449, 819);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePrimitiveProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCollectionProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEntities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceInheritedPrimitiveProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceInheritedRelationships)).EndInit();
            this.gbProperties.ResumeLayout(false);
            this.gbProperties.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblEntityName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkIsAbstract;
        private System.Windows.Forms.CheckBox chkIsVirtual;
        private System.Windows.Forms.BindingSource bindingSourcePrimitiveProperties;
        private System.Windows.Forms.BindingSource bindingSourceCollectionProperties;
        private System.Windows.Forms.BindingSource bindingSourceEntities;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.Label lblAbstract;
        private System.Windows.Forms.Label lblVirtual;
        private System.Windows.Forms.Label lblBaseEntity;
        private System.Windows.Forms.ComboBox cmbInherits;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.Button btnEditStaticData;
        private System.Windows.Forms.ComboBox cmbFunctionalId;
        private System.Windows.Forms.Label lblFunctionaId;
        private System.Windows.Forms.TextBox txtExample;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblStaticData;
        private System.Windows.Forms.CheckBox chkIsStaticData;
        private System.Windows.Forms.BindingSource bindingSourceInheritedPrimitiveProperties;
        private System.Windows.Forms.BindingSource bindingSourceInheritedRelationships;
        private System.Windows.Forms.Button btnAddFunctionalId;
        private PrimitiveRelationshipEditor pre;
        private System.Windows.Forms.Button btnPin;
        private System.Windows.Forms.GroupBox gbProperties;
    }
}
