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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntityEditor));
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkIsAbstract = new System.Windows.Forms.CheckBox();
            this.chkIsVirtual = new System.Windows.Forms.CheckBox();
            this.bindingSourcePrimitiveProperties = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceCollectionProperties = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceEntities = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbInherits = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.btnEditStaticData = new System.Windows.Forms.Button();
            this.cmbFunctionalId = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtExample = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chkIsStaticData = new System.Windows.Forms.CheckBox();
            this.bindingSourceInheritedPrimitiveProperties = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceInheritedRelationships = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnPin = new System.Windows.Forms.Button();
            this.pre = new Blueprint41.Modeller.PrimitiveRelationshipEditor();
            this.gbProperties = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePrimitiveProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCollectionProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEntities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceInheritedPrimitiveProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceInheritedRelationships)).BeginInit();
            this.gbProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(6, 63);
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
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtName.Location = new System.Drawing.Point(90, 57);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(338, 20);
            this.txtName.TabIndex = 2;
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label5.Location = new System.Drawing.Point(111, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Abstract";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label6.Location = new System.Drawing.Point(195, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Virtual";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label7.Location = new System.Drawing.Point(6, 92);
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
            this.cmbInherits.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbInherits.FormattingEnabled = true;
            this.cmbInherits.Location = new System.Drawing.Point(90, 84);
            this.cmbInherits.Name = "cmbInherits";
            this.cmbInherits.Size = new System.Drawing.Size(338, 21);
            this.cmbInherits.TabIndex = 3;
            this.cmbInherits.ValueMember = "Guid";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label8.Location = new System.Drawing.Point(6, 145);
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
            this.txtSummary.Location = new System.Drawing.Point(90, 142);
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
            this.cmbFunctionalId.Location = new System.Drawing.Point(90, 113);
            this.cmbFunctionalId.Name = "cmbFunctionalId";
            this.cmbFunctionalId.Size = new System.Drawing.Size(204, 21);
            this.cmbFunctionalId.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(6, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Functional ID: ";
            // 
            // txtExample
            // 
            this.txtExample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExample.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "example", true));
            this.txtExample.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtExample.Location = new System.Drawing.Point(90, 194);
            this.txtExample.Name = "txtExample";
            this.txtExample.Size = new System.Drawing.Size(338, 20);
            this.txtExample.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label4.Location = new System.Drawing.Point(6, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Example:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label9.Location = new System.Drawing.Point(258, 224);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "StaticData";
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
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(184)))), ((int)(((byte)(235)))));
            this.button1.Location = new System.Drawing.Point(300, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "ADD FUNCTIONAL ID";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnAddFunctionalId_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "thumbtack");
            // 
            // btnPin
            // 
            this.btnPin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPin.BackColor = System.Drawing.Color.Transparent;
            this.btnPin.FlatAppearance.BorderSize = 0;
            this.btnPin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPin.ImageIndex = 0;
            this.btnPin.ImageList = this.imageList;
            this.btnPin.Location = new System.Drawing.Point(422, 5);
            this.btnPin.Name = "btnPin";
            this.btnPin.Size = new System.Drawing.Size(18, 14);
            this.btnPin.TabIndex = 32;
            this.btnPin.UseVisualStyleBackColor = false;
            this.btnPin.Click += new System.EventHandler(this.btnPin_Click);
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
            // gbProperties
            // 
            this.gbProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbProperties.Controls.Add(this.label2);
            this.gbProperties.Controls.Add(this.label1);
            this.gbProperties.Controls.Add(this.txtName);
            this.gbProperties.Controls.Add(this.button1);
            this.gbProperties.Controls.Add(this.txtLabel);
            this.gbProperties.Controls.Add(this.chkIsAbstract);
            this.gbProperties.Controls.Add(this.chkIsStaticData);
            this.gbProperties.Controls.Add(this.chkIsVirtual);
            this.gbProperties.Controls.Add(this.label9);
            this.gbProperties.Controls.Add(this.label5);
            this.gbProperties.Controls.Add(this.label4);
            this.gbProperties.Controls.Add(this.label6);
            this.gbProperties.Controls.Add(this.txtExample);
            this.gbProperties.Controls.Add(this.label7);
            this.gbProperties.Controls.Add(this.cmbFunctionalId);
            this.gbProperties.Controls.Add(this.cmbInherits);
            this.gbProperties.Controls.Add(this.label3);
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

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkIsAbstract;
        private System.Windows.Forms.CheckBox chkIsVirtual;
        private System.Windows.Forms.BindingSource bindingSourcePrimitiveProperties;
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
        private System.Windows.Forms.BindingSource bindingSourceInheritedPrimitiveProperties;
        private System.Windows.Forms.BindingSource bindingSourceInheritedRelationships;
        private System.Windows.Forms.Button button1;
        private PrimitiveRelationshipEditor pre;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button btnPin;
        private System.Windows.Forms.GroupBox gbProperties;
    }
}
