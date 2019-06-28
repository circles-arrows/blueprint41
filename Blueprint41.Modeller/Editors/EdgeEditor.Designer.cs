namespace Blueprint41.Modeller.Editors
{
    partial class EdgeEditor
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
            this.gbSource = new System.Windows.Forms.GroupBox();
            this.lblCardinality = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbTargetCollection = new System.Windows.Forms.RadioButton();
            this.rbTargetLookupRequired = new System.Windows.Forms.RadioButton();
            this.rbTargetNone = new System.Windows.Forms.RadioButton();
            this.rbTargetLookupOptional = new System.Windows.Forms.RadioButton();
            this.lblTargetName = new System.Windows.Forms.Label();
            this.lblSourceName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbTarget = new System.Windows.Forms.GroupBox();
            this.rbSourceCollection = new System.Windows.Forms.RadioButton();
            this.rbSourceLookupRequired = new System.Windows.Forms.RadioButton();
            this.rbSourceLookupOptional = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtRelationshipName = new System.Windows.Forms.TextBox();
            this.txtNeo4jName = new System.Windows.Forms.TextBox();
            this.lblNeo4jName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbAutoLabel = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbSource.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbTarget.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSource
            // 
            this.gbSource.Controls.Add(this.lblCardinality);
            this.gbSource.Controls.Add(this.label4);
            this.gbSource.Controls.Add(this.groupBox2);
            this.gbSource.Controls.Add(this.lblTargetName);
            this.gbSource.Controls.Add(this.lblSourceName);
            this.gbSource.Controls.Add(this.label2);
            this.gbSource.Controls.Add(this.label1);
            this.gbSource.Controls.Add(this.gbTarget);
            this.gbSource.Location = new System.Drawing.Point(12, 152);
            this.gbSource.Name = "gbSource";
            this.gbSource.Size = new System.Drawing.Size(388, 189);
            this.gbSource.TabIndex = 1;
            this.gbSource.TabStop = false;
            this.gbSource.Text = "Cardinality";
            // 
            // lblCardinality
            // 
            this.lblCardinality.AutoSize = true;
            this.lblCardinality.Location = new System.Drawing.Point(7, 164);
            this.lblCardinality.Name = "lblCardinality";
            this.lblCardinality.Size = new System.Drawing.Size(0, 13);
            this.lblCardinality.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(160, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 39);
            this.label4.TabIndex = 13;
            this.label4.Text = "→";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbTargetCollection);
            this.groupBox2.Controls.Add(this.rbTargetLookupRequired);
            this.groupBox2.Controls.Add(this.rbTargetNone);
            this.groupBox2.Controls.Add(this.rbTargetLookupOptional);
            this.groupBox2.Location = new System.Drawing.Point(239, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(75, 105);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // rbTargetCollection
            // 
            this.rbTargetCollection.AutoSize = true;
            this.rbTargetCollection.Location = new System.Drawing.Point(6, 81);
            this.rbTargetCollection.Name = "rbTargetCollection";
            this.rbTargetCollection.Size = new System.Drawing.Size(33, 17);
            this.rbTargetCollection.TabIndex = 3;
            this.rbTargetCollection.TabStop = true;
            this.rbTargetCollection.Text = "N";
            this.rbTargetCollection.UseVisualStyleBackColor = true;
            // 
            // rbTargetLookupRequired
            // 
            this.rbTargetLookupRequired.AutoSize = true;
            this.rbTargetLookupRequired.Location = new System.Drawing.Point(6, 58);
            this.rbTargetLookupRequired.Name = "rbTargetLookupRequired";
            this.rbTargetLookupRequired.Size = new System.Drawing.Size(31, 17);
            this.rbTargetLookupRequired.TabIndex = 2;
            this.rbTargetLookupRequired.TabStop = true;
            this.rbTargetLookupRequired.Text = "1";
            this.rbTargetLookupRequired.UseVisualStyleBackColor = true;
            // 
            // rbTargetNone
            // 
            this.rbTargetNone.AutoSize = true;
            this.rbTargetNone.Location = new System.Drawing.Point(6, 14);
            this.rbTargetNone.Name = "rbTargetNone";
            this.rbTargetNone.Size = new System.Drawing.Size(31, 17);
            this.rbTargetNone.TabIndex = 0;
            this.rbTargetNone.TabStop = true;
            this.rbTargetNone.Text = "0";
            this.rbTargetNone.UseVisualStyleBackColor = true;
            // 
            // rbTargetLookupOptional
            // 
            this.rbTargetLookupOptional.AutoSize = true;
            this.rbTargetLookupOptional.Location = new System.Drawing.Point(6, 35);
            this.rbTargetLookupOptional.Name = "rbTargetLookupOptional";
            this.rbTargetLookupOptional.Size = new System.Drawing.Size(46, 17);
            this.rbTargetLookupOptional.TabIndex = 1;
            this.rbTargetLookupOptional.TabStop = true;
            this.rbTargetLookupOptional.Text = "0...1";
            this.rbTargetLookupOptional.UseVisualStyleBackColor = true;
            // 
            // lblTargetName
            // 
            this.lblTargetName.AutoSize = true;
            this.lblTargetName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTargetName.Location = new System.Drawing.Point(236, 29);
            this.lblTargetName.Name = "lblTargetName";
            this.lblTargetName.Size = new System.Drawing.Size(78, 13);
            this.lblTargetName.TabIndex = 7;
            this.lblTargetName.Text = "Target name";
            // 
            // lblSourceName
            // 
            this.lblSourceName.AutoSize = true;
            this.lblSourceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSourceName.Location = new System.Drawing.Point(67, 29);
            this.lblSourceName.Name = "lblSourceName";
            this.lblSourceName.Size = new System.Drawing.Size(81, 13);
            this.lblSourceName.TabIndex = 3;
            this.lblSourceName.Text = "Source name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Multiplicity:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // gbTarget
            // 
            this.gbTarget.Controls.Add(this.rbSourceCollection);
            this.gbTarget.Controls.Add(this.rbSourceLookupRequired);
            this.gbTarget.Controls.Add(this.rbSourceLookupOptional);
            this.gbTarget.Location = new System.Drawing.Point(70, 45);
            this.gbTarget.Name = "gbTarget";
            this.gbTarget.Size = new System.Drawing.Size(55, 105);
            this.gbTarget.TabIndex = 2;
            this.gbTarget.TabStop = false;
            // 
            // rbSourceCollection
            // 
            this.rbSourceCollection.AutoSize = true;
            this.rbSourceCollection.Location = new System.Drawing.Point(6, 61);
            this.rbSourceCollection.Name = "rbSourceCollection";
            this.rbSourceCollection.Size = new System.Drawing.Size(33, 17);
            this.rbSourceCollection.TabIndex = 9;
            this.rbSourceCollection.TabStop = true;
            this.rbSourceCollection.Text = "N";
            this.rbSourceCollection.UseVisualStyleBackColor = true;
            // 
            // rbSourceLookupRequired
            // 
            this.rbSourceLookupRequired.AutoSize = true;
            this.rbSourceLookupRequired.Location = new System.Drawing.Point(6, 38);
            this.rbSourceLookupRequired.Name = "rbSourceLookupRequired";
            this.rbSourceLookupRequired.Size = new System.Drawing.Size(31, 17);
            this.rbSourceLookupRequired.TabIndex = 8;
            this.rbSourceLookupRequired.TabStop = true;
            this.rbSourceLookupRequired.Text = "1";
            this.rbSourceLookupRequired.UseVisualStyleBackColor = true;
            // 
            // rbSourceLookupOptional
            // 
            this.rbSourceLookupOptional.AutoSize = true;
            this.rbSourceLookupOptional.Location = new System.Drawing.Point(6, 15);
            this.rbSourceLookupOptional.Name = "rbSourceLookupOptional";
            this.rbSourceLookupOptional.Size = new System.Drawing.Size(46, 17);
            this.rbSourceLookupOptional.TabIndex = 7;
            this.rbSourceLookupOptional.TabStop = true;
            this.rbSourceLookupOptional.Text = "0...1";
            this.rbSourceLookupOptional.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(239, 347);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(320, 347);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtRelationshipName
            // 
            this.txtRelationshipName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRelationshipName.Location = new System.Drawing.Point(9, 45);
            this.txtRelationshipName.Name = "txtRelationshipName";
            this.txtRelationshipName.Size = new System.Drawing.Size(373, 20);
            this.txtRelationshipName.TabIndex = 1;
            this.txtRelationshipName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRelationshipName_KeyDown);
            // 
            // txtNeo4jName
            // 
            this.txtNeo4jName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNeo4jName.Location = new System.Drawing.Point(9, 84);
            this.txtNeo4jName.Name = "txtNeo4jName";
            this.txtNeo4jName.Size = new System.Drawing.Size(373, 20);
            this.txtNeo4jName.TabIndex = 2;
            this.txtNeo4jName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNeo4jName_KeyDown);
            // 
            // lblNeo4jName
            // 
            this.lblNeo4jName.AutoSize = true;
            this.lblNeo4jName.Location = new System.Drawing.Point(6, 68);
            this.lblNeo4jName.Name = "lblNeo4jName";
            this.lblNeo4jName.Size = new System.Drawing.Size(67, 13);
            this.lblNeo4jName.TabIndex = 7;
            this.lblNeo4jName.Text = "Neo4j name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Relationship name:";
            // 
            // cbAutoLabel
            // 
            this.cbAutoLabel.AutoSize = true;
            this.cbAutoLabel.Checked = true;
            this.cbAutoLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoLabel.Location = new System.Drawing.Point(12, 12);
            this.cbAutoLabel.Name = "cbAutoLabel";
            this.cbAutoLabel.Size = new System.Drawing.Size(77, 17);
            this.cbAutoLabel.TabIndex = 9;
            this.cbAutoLabel.Text = "Auto Label";
            this.cbAutoLabel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtRelationshipName);
            this.groupBox1.Controls.Add(this.txtNeo4jName);
            this.groupBox1.Controls.Add(this.lblNeo4jName);
            this.groupBox1.Location = new System.Drawing.Point(12, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 111);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Relationship Properties";
            // 
            // EdgeEditor
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(408, 378);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbAutoLabel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gbSource);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EdgeEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edge Editor";
            this.gbSource.ResumeLayout(false);
            this.gbSource.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbTarget.ResumeLayout(false);
            this.gbTarget.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbTarget;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSourceName;
        private System.Windows.Forms.Label lblTargetName;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtRelationshipName;
        private System.Windows.Forms.TextBox txtNeo4jName;
        private System.Windows.Forms.Label lblNeo4jName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbAutoLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSourceCollection;
        private System.Windows.Forms.RadioButton rbSourceLookupRequired;
        private System.Windows.Forms.RadioButton rbSourceLookupOptional;
        private System.Windows.Forms.RadioButton rbTargetLookupRequired;
        private System.Windows.Forms.RadioButton rbTargetLookupOptional;
        private System.Windows.Forms.RadioButton rbTargetNone;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbTargetCollection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCardinality;
    }
}