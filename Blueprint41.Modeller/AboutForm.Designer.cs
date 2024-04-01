namespace Blueprint41.Modeller
{
    partial class AboutForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.llSite = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.llLicense = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Blueprint41.Modeller.Properties.Resources.bp41Logo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(139, 136);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(157, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Version:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(156, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Blueprint41 Modeller";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(208, 47);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(37, 13);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "v1.6.0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(157, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(239, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Copyright 2024 (c) CIRCLES ARROWS LIMITED";
            // 
            // llSite
            // 
            this.llSite.AutoSize = true;
            this.llSite.Location = new System.Drawing.Point(157, 121);
            this.llSite.Name = "llSite";
            this.llSite.Size = new System.Drawing.Size(109, 13);
            this.llSite.TabIndex = 5;
            this.llSite.TabStop = true;
            this.llSite.Text = "www.blueprint41.com";
            this.llSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSite_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(157, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Licensed under the";
            // 
            // llLicense
            // 
            this.llLicense.AutoSize = true;
            this.llLicense.Location = new System.Drawing.Point(251, 60);
            this.llLicense.Name = "llLicense";
            this.llLicense.Size = new System.Drawing.Size(66, 13);
            this.llLicense.TabIndex = 7;
            this.llLicense.TabStop = true;
            this.llLicense.Text = "MIT License";
            this.llLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llLicense_LinkClicked);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 158);
            this.Controls.Add(this.llLicense);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.llSite);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(439, 197);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(439, 197);
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Blueprin41 Modeller";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel llSite;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel llLicense;
    }
}