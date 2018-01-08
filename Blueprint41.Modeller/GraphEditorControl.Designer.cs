namespace Blueprint41.Modeller
{
    partial class GraphEditorControl
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
            this.Viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // Viewer
            // 
            this.Viewer.AsyncLayout = false;
            this.Viewer.AutoScroll = true;
            this.Viewer.BackwardEnabled = false;
            this.Viewer.BuildHitTree = false;
            this.Viewer.CurrentLayoutMethod = Microsoft.Msagl.GraphViewerGdi.LayoutMethod.SugiyamaScheme;
            this.Viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Viewer.ForwardEnabled = false;
            this.Viewer.Graph = null;
            this.Viewer.LayoutAlgorithmSettingsButtonVisible = true;
            this.Viewer.LayoutEditingEnabled = true;
            this.Viewer.Location = new System.Drawing.Point(0, 0);
            this.Viewer.MouseHitDistance = 0.05D;
            this.Viewer.Name = "Viewer";
            this.Viewer.NavigationVisible = true;
            this.Viewer.NeedToCalculateLayout = true;
            this.Viewer.PanButtonPressed = false;
            this.Viewer.SaveAsImageEnabled = true;
            this.Viewer.SaveAsMsaglEnabled = true;
            this.Viewer.SaveButtonVisible = true;
            this.Viewer.SaveGraphButtonVisible = true;
            this.Viewer.SaveInVectorFormatEnabled = true;
            this.Viewer.Size = new System.Drawing.Size(787, 617);
            this.Viewer.TabIndex = 0;
            this.Viewer.ToolBarIsVisible = true;
            this.Viewer.ZoomF = 1D;
            this.Viewer.ZoomFraction = 0.2D;
            this.Viewer.ZoomWindowThreshold = 0.05D;
            this.Viewer.SelectionChanged += new System.EventHandler(this.gViewer_SelectionChanged);
            this.Viewer.Click += new System.EventHandler(this.gViewer_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 5000;
            this.toolTip.ReshowDelay = 500;
            // 
            // GraphEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Viewer);
            this.Name = "GraphEditorControl";
            this.Size = new System.Drawing.Size(787, 617);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip;
        internal Microsoft.Msagl.GraphViewerGdi.GViewer Viewer;
    }
}
