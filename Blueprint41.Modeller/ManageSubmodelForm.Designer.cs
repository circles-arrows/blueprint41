namespace Blueprint41.Modeller
{
    partial class ManageSubmodelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageSubmodelForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkIsDraft = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericChapter = new System.Windows.Forms.NumericUpDown();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lbAvailable = new System.Windows.Forms.ListBox();
            this.lbExisting = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.buttonAddAll = new System.Windows.Forms.Button();
            this.buttonRemoveAll = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.chkIsLaboratory = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hteExplaination = new Blueprint41.Modeller.HtmlEditor();
            ((System.ComponentModel.ISupportInitialize)(this.numericChapter)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(488, 670);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(407, 670);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Chapter:";
            // 
            // chkIsDraft
            // 
            this.chkIsDraft.AutoSize = true;
            this.chkIsDraft.Location = new System.Drawing.Point(85, 64);
            this.chkIsDraft.Name = "chkIsDraft";
            this.chkIsDraft.Size = new System.Drawing.Size(15, 14);
            this.chkIsDraft.TabIndex = 6;
            this.chkIsDraft.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Is Draft:";
            // 
            // numericChapter
            // 
            this.numericChapter.Location = new System.Drawing.Point(85, 37);
            this.numericChapter.Name = "numericChapter";
            this.numericChapter.Size = new System.Drawing.Size(212, 20);
            this.numericChapter.TabIndex = 8;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(85, 10);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(212, 20);
            this.txtName.TabIndex = 9;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lbAvailable
            // 
            this.lbAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAvailable.FormattingEnabled = true;
            this.lbAvailable.Location = new System.Drawing.Point(3, 3);
            this.lbAvailable.Name = "lbAvailable";
            this.lbAvailable.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbAvailable.Size = new System.Drawing.Size(249, 357);
            this.lbAvailable.TabIndex = 10;
            this.lbAvailable.SelectedIndexChanged += new System.EventHandler(this.lbAvailable_SelectedIndexChanged);
            // 
            // lbExisting
            // 
            this.lbExisting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbExisting.FormattingEnabled = true;
            this.lbExisting.Location = new System.Drawing.Point(301, 3);
            this.lbExisting.Name = "lbExisting";
            this.lbExisting.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbExisting.Size = new System.Drawing.Size(249, 357);
            this.lbExisting.TabIndex = 11;
            this.lbExisting.SelectedIndexChanged += new System.EventHandler(this.lbExisting_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAdd.Location = new System.Drawing.Point(5, 146);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(27, 23);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = ">";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRemove.Location = new System.Drawing.Point(5, 173);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(27, 23);
            this.btnRemove.TabIndex = 13;
            this.btnRemove.Text = "<";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // buttonAddAll
            // 
            this.buttonAddAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonAddAll.Location = new System.Drawing.Point(5, 119);
            this.buttonAddAll.Name = "buttonAddAll";
            this.buttonAddAll.Size = new System.Drawing.Size(27, 23);
            this.buttonAddAll.TabIndex = 14;
            this.buttonAddAll.Text = ">>";
            this.buttonAddAll.UseVisualStyleBackColor = true;
            this.buttonAddAll.Click += new System.EventHandler(this.buttonAddAll_Click);
            // 
            // buttonRemoveAll
            // 
            this.buttonRemoveAll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonRemoveAll.Location = new System.Drawing.Point(5, 200);
            this.buttonRemoveAll.Name = "buttonRemoveAll";
            this.buttonRemoveAll.Size = new System.Drawing.Size(27, 23);
            this.buttonRemoveAll.TabIndex = 15;
            this.buttonRemoveAll.Text = "<<";
            this.buttonRemoveAll.UseVisualStyleBackColor = true;
            this.buttonRemoveAll.Click += new System.EventHandler(this.buttonRemoveAll_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(12, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(316, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Explanation:   (New Paragraph = Enter  |  New Line = Shift+Enter)";
            // 
            // chkIsLaboratory
            // 
            this.chkIsLaboratory.AutoSize = true;
            this.chkIsLaboratory.Location = new System.Drawing.Point(85, 84);
            this.chkIsLaboratory.Name = "chkIsLaboratory";
            this.chkIsLaboratory.Size = new System.Drawing.Size(15, 14);
            this.chkIsLaboratory.TabIndex = 19;
            this.chkIsLaboratory.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Is Laboratory:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbExisting, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbAvailable, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 305);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(553, 363);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel1.Controls.Add(this.buttonAddAll);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnRemove);
            this.panel1.Controls.Add(this.buttonRemoveAll);
            this.panel1.Location = new System.Drawing.Point(258, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(37, 357);
            this.panel1.TabIndex = 22;
            // 
            // hteExplaination
            // 
            this.hteExplaination.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hteExplaination.BodyBackgroundColor = System.Drawing.Color.White;
            this.hteExplaination.BodyHtml = null;
            this.hteExplaination.BodyText = null;
            this.hteExplaination.DocumentText = resources.GetString("hteExplaination.DocumentText");
            this.hteExplaination.EditorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.hteExplaination.EditorForeColor = Blueprint41.Modeller.FontColor.Black;
            this.hteExplaination.FontSize = Blueprint41.Modeller.FontSize.Regular;
            this.hteExplaination.Html = null;
            this.hteExplaination.Location = new System.Drawing.Point(18, 124);
            this.hteExplaination.Name = "hteExplaination";
            this.hteExplaination.Size = new System.Drawing.Size(547, 175);
            this.hteExplaination.TabIndex = 18;
            // 
            // ManageSubmodelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(580, 700);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkIsLaboratory);
            this.Controls.Add(this.hteExplaination);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.numericChapter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkIsDraft);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ManageSubmodelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage sub model";
            ((System.ComponentModel.ISupportInitialize)(this.numericChapter)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkIsDraft;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericChapter;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ListBox lbAvailable;
        private System.Windows.Forms.ListBox lbExisting;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button buttonAddAll;
        private System.Windows.Forms.Button buttonRemoveAll;
        private System.Windows.Forms.Label label4;
        private HtmlEditor hteExplaination;
        private System.Windows.Forms.CheckBox chkIsLaboratory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
    }
}