namespace Blueprint41.Modeller
{
    partial class PrimitiveRelationshipEditor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPagePrimivite = new System.Windows.Forms.TabPage();
            this.dataGridViewPrimitive = new System.Windows.Forms.DataGridView();
            this.tabPageRelationship = new System.Windows.Forms.TabPage();
            this.dataGridViewRelationship = new System.Windows.Forms.DataGridView();
            this.cbShowAll = new System.Windows.Forms.CheckBox();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourcePrimitiveProperties = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl.SuspendLayout();
            this.tabPagePrimivite.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrimitive)).BeginInit();
            this.tabPageRelationship.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRelationship)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePrimitiveProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPagePrimivite);
            this.tabControl.Controls.Add(this.tabPageRelationship);
            this.tabControl.Location = new System.Drawing.Point(4, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(515, 627);
            this.tabControl.TabIndex = 0;
            // 
            // tabPagePrimivite
            // 
            this.tabPagePrimivite.Controls.Add(this.dataGridViewPrimitive);
            this.tabPagePrimivite.Location = new System.Drawing.Point(4, 22);
            this.tabPagePrimivite.Name = "tabPagePrimivite";
            this.tabPagePrimivite.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePrimivite.Size = new System.Drawing.Size(507, 601);
            this.tabPagePrimivite.TabIndex = 0;
            this.tabPagePrimivite.Text = "Primitive";
            this.tabPagePrimivite.UseVisualStyleBackColor = true;
            // 
            // dataGridViewPrimitive
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPrimitive.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewPrimitive.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPrimitive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPrimitive.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewPrimitive.Name = "dataGridViewPrimitive";
            this.dataGridViewPrimitive.Size = new System.Drawing.Size(501, 595);
            this.dataGridViewPrimitive.TabIndex = 0;
            // 
            // tabPageRelationship
            // 
            this.tabPageRelationship.Controls.Add(this.dataGridViewRelationship);
            this.tabPageRelationship.Controls.Add(this.cbShowAll);
            this.tabPageRelationship.Location = new System.Drawing.Point(4, 22);
            this.tabPageRelationship.Name = "tabPageRelationship";
            this.tabPageRelationship.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRelationship.Size = new System.Drawing.Size(507, 601);
            this.tabPageRelationship.TabIndex = 1;
            this.tabPageRelationship.Text = "Relationship";
            this.tabPageRelationship.UseVisualStyleBackColor = true;
            // 
            // dataGridViewRelationship
            // 
            this.dataGridViewRelationship.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewRelationship.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewRelationship.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRelationship.Location = new System.Drawing.Point(6, 29);
            this.dataGridViewRelationship.Name = "dataGridViewRelationship";
            this.dataGridViewRelationship.Size = new System.Drawing.Size(498, 566);
            this.dataGridViewRelationship.TabIndex = 0;
            // 
            // cbShowAll
            // 
            this.cbShowAll.AutoSize = true;
            this.cbShowAll.Location = new System.Drawing.Point(6, 6);
            this.cbShowAll.Name = "cbShowAll";
            this.cbShowAll.Size = new System.Drawing.Size(133, 17);
            this.cbShowAll.TabIndex = 2;
            this.cbShowAll.Text = "Show All Relationships";
            this.cbShowAll.UseVisualStyleBackColor = true;
            // 
            // PrimitiveRelationshipEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "PrimitiveRelationshipEditor";
            this.Size = new System.Drawing.Size(522, 634);
            this.tabControl.ResumeLayout(false);
            this.tabPagePrimivite.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrimitive)).EndInit();
            this.tabPageRelationship.ResumeLayout(false);
            this.tabPageRelationship.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRelationship)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePrimitiveProperties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPagePrimivite;
        private System.Windows.Forms.TabPage tabPageRelationship;
        private System.Windows.Forms.DataGridView dataGridViewPrimitive;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.BindingSource bindingSourcePrimitiveProperties;
        private System.Windows.Forms.CheckBox cbShowAll;
        private System.Windows.Forms.DataGridView dataGridViewRelationship;
    }
}
