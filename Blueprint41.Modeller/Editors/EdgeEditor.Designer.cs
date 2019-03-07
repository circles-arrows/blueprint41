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
            this.lblSourceName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSourcePropertyType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbTarget = new System.Windows.Forms.GroupBox();
            this.lblTargetName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTargetPropertyType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtRelationshipName = new System.Windows.Forms.TextBox();
            this.txtNeo4jName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbAutoLabel = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbSource.SuspendLayout();
            this.gbTarget.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSource
            // 
            this.gbSource.Controls.Add(this.lblSourceName);
            this.gbSource.Controls.Add(this.label2);
            this.gbSource.Controls.Add(this.cmbSourcePropertyType);
            this.gbSource.Controls.Add(this.label1);
            this.gbSource.Location = new System.Drawing.Point(12, 152);
            this.gbSource.Name = "gbSource";
            this.gbSource.Size = new System.Drawing.Size(388, 82);
            this.gbSource.TabIndex = 1;
            this.gbSource.TabStop = false;
            this.gbSource.Text = "Source";
            // 
            // lblSourceName
            // 
            this.lblSourceName.AutoSize = true;
            this.lblSourceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSourceName.Location = new System.Drawing.Point(51, 29);
            this.lblSourceName.Name = "lblSourceName";
            this.lblSourceName.Size = new System.Drawing.Size(81, 13);
            this.lblSourceName.TabIndex = 3;
            this.lblSourceName.Text = "Source name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Type:";
            // 
            // cmbSourcePropertyType
            // 
            this.cmbSourcePropertyType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSourcePropertyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourcePropertyType.FormattingEnabled = true;
            this.cmbSourcePropertyType.ItemHeight = 13;
            this.cmbSourcePropertyType.Location = new System.Drawing.Point(54, 45);
            this.cmbSourcePropertyType.Name = "cmbSourcePropertyType";
            this.cmbSourcePropertyType.Size = new System.Drawing.Size(328, 21);
            this.cmbSourcePropertyType.TabIndex = 3;
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
            this.gbTarget.Controls.Add(this.lblTargetName);
            this.gbTarget.Controls.Add(this.label4);
            this.gbTarget.Controls.Add(this.cmbTargetPropertyType);
            this.gbTarget.Controls.Add(this.label5);
            this.gbTarget.Location = new System.Drawing.Point(12, 240);
            this.gbTarget.Name = "gbTarget";
            this.gbTarget.Size = new System.Drawing.Size(388, 82);
            this.gbTarget.TabIndex = 2;
            this.gbTarget.TabStop = false;
            this.gbTarget.Text = "Target";
            // 
            // lblTargetName
            // 
            this.lblTargetName.AutoSize = true;
            this.lblTargetName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTargetName.Location = new System.Drawing.Point(51, 27);
            this.lblTargetName.Name = "lblTargetName";
            this.lblTargetName.Size = new System.Drawing.Size(78, 13);
            this.lblTargetName.TabIndex = 7;
            this.lblTargetName.Text = "Target name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Type:";
            // 
            // cmbTargetPropertyType
            // 
            this.cmbTargetPropertyType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTargetPropertyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTargetPropertyType.FormattingEnabled = true;
            this.cmbTargetPropertyType.Location = new System.Drawing.Point(54, 43);
            this.cmbTargetPropertyType.Name = "cmbTargetPropertyType";
            this.cmbTargetPropertyType.Size = new System.Drawing.Size(328, 21);
            this.cmbTargetPropertyType.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Name:";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(244, 328);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(325, 328);
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
            // 
            // txtNeo4jName
            // 
            this.txtNeo4jName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNeo4jName.Location = new System.Drawing.Point(9, 84);
            this.txtNeo4jName.Name = "txtNeo4jName";
            this.txtNeo4jName.Size = new System.Drawing.Size(373, 20);
            this.txtNeo4jName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Neo4j name:";
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
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 111);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Relationship Properties";
            // 
            // EdgeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 359);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbAutoLabel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gbTarget);
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
        private System.Windows.Forms.ComboBox cmbSourcePropertyType;
        private System.Windows.Forms.GroupBox gbTarget;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSourceName;
        private System.Windows.Forms.Label lblTargetName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbTargetPropertyType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtRelationshipName;
        private System.Windows.Forms.TextBox txtNeo4jName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbAutoLabel;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}