using System;
using System.Windows.Forms;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Drawing;
using DrawingNode = Microsoft.Msagl.Drawing.Node;
using DrawingColor = Microsoft.Msagl.Drawing.Color;
using GeometryPoint = Microsoft.Msagl.Core.Geometry.Point;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using Blueprint41.Modeller.Schemas;
using System.Linq;
using Blueprint41.Modeller.Utils;
using System.Threading;
using System.Diagnostics;

namespace Blueprint41.Modeller.Controls
{
    public partial class GraphEditor : UserControl
    {
        #region Events
        public event EventHandler<NodeEventArgs> InsertNode;
        public event EventHandler<NodeEventArgs> NodeSelected;
        public event EventHandler<NodeEventArgs> EntityDeleted;
        public event EventHandler<NodeEventArgs> EntityExcluded;
        public event EventHandler<EdgeEventArgs> EdgeSelected;
        public event EventHandler<EdgeEventArgs> EdgeRemoved;
        public event EventHandler<EdgeEventArgs> EdgeAdded;
        public event EventHandler NoneSelected;
        public event EventHandler EditSubmodelClicked;
        public event EventHandler RedoLayout;
        public event EventHandler EdgeModeClicked;
        public event EventHandler PanModeClicked;
        #endregion

        #region Properties
        /// <summary>
        /// Returns the GViewer contained in the control.
        /// </summary>
        public GViewer Viewer
        {
            get { return gViewer; }
        }

        /// <summary>
        /// Returns the viewer graph
        /// </summary>
        public Graph Graph
        {
            get { return gViewer.Graph; }
            set { gViewer.Graph = value; }
        }

        public ModellerType ModellerType { get; set; }
        public string EntityNodeName => ModellerType == ModellerType.Blueprint41 ? "Entity" : "Node";

        bool panButtonPressedOnMenu;
        public bool PanButtonPressedOnMenu
        {
            get { return panButtonPressedOnMenu; }
            set { panButtonPressedOnMenu = value; }
        }

        /// <summary>
        /// Current draggable entities
        /// </summary>
        public ArrayList SelectedEntities
        {
            get
            {
                ArrayList al = new ArrayList();
                foreach (IViewerObject ob in gViewer.Entities)
                    if (ob.MarkedForDragging)
                        al.Add(ob);

                return al;
            }
        } 
        #endregion

        /// <summary>
        /// An List containing all the node type entries (custom node types for insetion).
        /// </summary>
        internal List<NodeTypeEntry> NodeTypes { get; } = new List<NodeTypeEntry>();

        /// <summary>
        /// The point where the user called up the context menu.
        /// </summary>
        protected GeometryPoint m_MouseRightButtonDownPoint;

        object selectedObject;
        AttributeBase selectedObjectAttr;
        AttributeBase selectedNodeAttr;
        readonly ToolTip toolTip = new ToolTip();
        ContextMenuStrip contextMenuStrip;

        public GraphEditor()
        {
            InitializeComponent();
            Load += GraphEditor_Load;
        }

        void GraphEditor_Load(object sender, EventArgs e)
        {
            (gViewer as IViewer).MouseDown += GraphEditor_MouseDown;
            (gViewer as IViewer).MouseMove += GraphEditor_MouseMove;
            (gViewer as IViewer).MouseUp += GraphEditor_MouseUp;

            gViewer.ToolBarIsVisible = false;
            gViewer.EdgeRemoved += GViewer_EdgeRemoved;
            gViewer.EdgeAdded += GViewer_EdgeAdded;

            gViewer.LayoutEditor.DecorateObjectForDragging = SetDragDecorator;
            gViewer.LayoutEditor.RemoveObjDraggingDecorations = RemoveDragDecorator;
            gViewer.LayoutEditor.DecorateEdgeForDragging = SetEdgeDragDecorator;
            gViewer.MouseWheel += GViewerMouseWheel;
            gViewer.ObjectUnderMouseCursorChanged += GViewer_ObjectUnderMouseCursorChanged;
            gViewer.KeyUp += GViewer_KeyUp;
            // disable the multiple selection highlight
            gViewer.LayoutEditor.ToggleEntityPredicate = (mk, mb, d) => { return false; };
        }

        #region IViewer Event Handler
        private void GraphEditor_MouseUp(object sender, MsaglMouseEventArgs e)
        {
            if (e.RightButtonIsPressed)
                return;

            var point = new GeometryPoint(e.X, e.Y);
            object obj = gViewer.GetObjectAt((int)point.X, (int)point.Y);

            if (obj is DNode && SelectedEntities.Count > 0)
            {
                Submodel displayedModel = null;

                if (this.Viewer.Tag != null && this.Viewer.Tag is Submodel)
                    displayedModel = this.Viewer.Tag as Submodel;

                if (displayedModel != null)
                    displayedModel.Model.CaptureCoordinates();
            }
        }

        private void GraphEditor_MouseMove(object sender, MsaglMouseEventArgs e)
        {
            if (PanButtonPressedOnMenu)
            {
                gViewer.PanButtonPressed = true;
                return;
            }

            bool altPressed = (ModifierKeys & Keys.Alt) == Keys.Alt;

            if (altPressed && gViewer.PanButtonPressed)
                return;

            if (PanButtonPressedOnMenu == false)
                gViewer.PanButtonPressed = false;
        }

        private void GraphEditor_MouseDown(object sender, MsaglMouseEventArgs e)
        {
            if (e.RightButtonIsPressed && !e.Handled)
            {
                m_MouseRightButtonDownPoint = (gViewer).ScreenToSource(e);
                contextMenuStrip = BuildContextMenu(new GeometryPoint(e.X, e.Y));
                contextMenuStrip.Show(this, new Point(e.X, e.Y));
            }

            bool leftButtonPressed = e.LeftButtonIsPressed;
            bool altPressed = (ModifierKeys & Keys.Alt) == Keys.Alt;

            if (altPressed && leftButtonPressed && !PanButtonPressedOnMenu)
                gViewer.PanButtonPressed = true;
        }
        #endregion

        #region GViewer Event Handler
        private void GViewer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                AnalyzeObjectsToRemove();
                e.Handled = true;
            }
        }

        private void GViewer_ObjectUnderMouseCursorChanged(object sender, ObjectUnderMouseCursorChangedEventArgs e)
        {
            selectedObject = e.OldObject?.DrawingObject;

            if (selectedObject != null)
            {
                RestoreSelectedObjAttr();

                gViewer.Invalidate(e.OldObject);
                selectedObject = null;
            }

            IViewerObject objectUnderMouseCursor = gViewer.ObjectUnderMouseCursor;
            
            if (objectUnderMouseCursor == null)
            {
                gViewer.SetToolTip(toolTip, "");
            }
            else
            {
                selectedObject = objectUnderMouseCursor.DrawingObject;
                if (selectedObject is Edge edge)
                {
                    selectedObjectAttr = edge.Attr.Clone();
                    edge.Attr.Color = DrawingColor.Blue;

                    if (e.NewObject != null)
                        gViewer.Invalidate(e.NewObject);

                    if (edge.UserData is Relationship relationship)
                        gViewer.SetToolTip(toolTip, relationship.Type);
                }
                else if (selectedObject is DrawingNode dNode)
                {
                    selectedNodeAttr = dNode.Attr.Clone();
                    dNode.Attr.Color = DrawingColor.LimeGreen;                    
                    gViewer.SetToolTip(toolTip, "Drag me");

                    if (e.NewObject != null)
                        gViewer.Invalidate(e.NewObject);
                }
            }
        }

        void GViewerMouseWheel(object sender, MouseEventArgs e)
        {
            int delta = e.Delta;
            if (delta != 0)
                gViewer.ZoomF *= delta < 0 ? 0.9 : 1.1;
        }

        private void GViewer_EdgeAdded(object sender, EventArgs e)
        {
            EdgeAdded?.Invoke(this, new EdgeEventArgs(sender as Edge));
        }

        void GViewer_EdgeRemoved(object sender, EventArgs e)
        {
            EdgeRemoved?.Invoke(this, new EdgeEventArgs(sender as Edge));
        }
        #endregion

        void SetDragDecorator(IViewerObject obj)
        {
            if (obj is DNode dNode)
            {
                dNode.DrawingNode.Attr.Color = Styles.SELECTED_NODE_LINE_COLOR.ToMsAgl();
                gViewer.Invalidate(obj);
            }

            AnaylyzeLeftButtonClick(obj);
        }

        void RemoveDragDecorator(IViewerObject obj)
        {
            if (obj is DNode dNode)
            {
                dNode.DrawingNode.Attr.Color = DrawingColor.LightGray;
                gViewer.Invalidate(obj);
            }

            NoneSelected?.Invoke(this, EventArgs.Empty);
        }

        void SetEdgeDragDecorator(IViewerEdge viewerEdge)
        {
            if (viewerEdge is DEdge dEdge)
                EdgeSelected?.Invoke(this, new EdgeEventArgs(dEdge.DrawingEdge));
        }

        void RestoreSelectedObjAttr()
        {
            if (selectedObject is Edge edge)
                edge.Attr = (EdgeAttr)selectedObjectAttr;

            if (selectedObject is DrawingNode dNode)
            {
                if (dNode.Attr.Color != Styles.SELECTED_NODE_LINE_COLOR.ToMsAgl())
                    dNode.Attr = (NodeAttr)selectedNodeAttr;
            }
        }

        void RemoveDraggableEntities()
        {
            foreach (IViewerObject obj in SelectedEntities)
            {
                if (obj.MarkedForDragging)
                    obj.MarkedForDragging = false;

                RemoveDragDecorator(obj);
            }
        }

        void AnalyzeObjectsToRemove()
        {
            if (gViewer.LayoutEditor.SelectedEdge != null)
            {
                gViewer.RemoveEdge(gViewer.LayoutEditor.SelectedEdge, true);
                return;
            }

            foreach (IViewerObject ob in SelectedEntities)
            {
                if (ob is IViewerNode node)
                    EntityDeleted?.Invoke(this, new NodeEventArgs(node.Node, m_MouseRightButtonDownPoint));
            }
        }

        void AnaylyzeLeftButtonClick(IViewerObject obj)
        {
            if (obj is DNode dNode)
            {
                NodeSelected?.Invoke(this, new NodeEventArgs(dNode.DrawingNode, new GeometryPoint(0, 0)));
            }
        }

        #region Context Menu

        void BuildSelectionModeContextMenu(ContextMenuStrip cm)
        {
            ToolStripSeparator separator = new ToolStripSeparator();
            cm.Items.Add(separator);

            ToolStripMenuItem edgeMode = new ToolStripMenuItem("Edge mode");
            edgeMode.Name = "edgeModeMenuItem";
            edgeMode.Checked = gViewer.InsertingEdge;
            edgeMode.CheckOnClick = true;
            edgeMode.Click += EdgeMode_Click;
            cm.Items.Add(edgeMode);

            ToolStripMenuItem panMode = new ToolStripMenuItem("Pan mode");
            panMode.Name = "panModeMenuItem";
            panMode.Checked = gViewer.PanButtonPressed;
            panMode.CheckOnClick = true;
            panMode.Click += PanMode_Click;
            cm.Items.Add(panMode);

            separator = new ToolStripSeparator();
            cm.Items.Add(separator);
        }

        protected virtual ContextMenuStrip BuildContextMenu(GeometryPoint point)
        {
            ContextMenuStrip cm = new ContextMenuStrip();

            ToolStripMenuItem mi;
            foreach (NodeTypeEntry nte in NodeTypes)
            {
                mi = new ToolStripMenuItem() { Text = "Insert " + nte.Name };
                mi.Click += InsertNode_Click;
                cm.Items.Add(mi);
            }

            BuildSelectionModeContextMenu(cm);

            mi = new ToolStripMenuItem("Edit Submodel");
            mi.Click += EditSubmodel;
            cm.Items.Add(mi);

            ToolStripSeparator separator = new ToolStripSeparator();
            cm.Items.Add(separator);

            object obj = gViewer.GetObjectAt((int)point.X, (int)point.Y);

            if (obj is DNode && SelectedEntities.Count > 0)
            {
                Submodel displayedModel = null;

                if (this.Viewer.Tag != null && this.Viewer.Tag is Submodel)
                    displayedModel = this.Viewer.Tag as Submodel;

                if (displayedModel != null && displayedModel.Name != Constants.MainModel)
                {
                    mi = new ToolStripMenuItem();
                    mi.Text = "Exclude from Model";
                    mi.Click += ExcludeEntityClick;
                    cm.Items.Add(mi);
                }

                mi = new ToolStripMenuItem();
                mi.Text = $"Delete {EntityNodeName}";
                mi.ForeColor = Styles.FORMS_WARNING;
                mi.Click += RemoveEntityClick;
                cm.Items.Add(mi);
            }

            mi = new ToolStripMenuItem();
            mi.Text = "Redo layout";
            mi.Click += RedoLayout_Click;
            cm.Items.Add(mi);

            return cm;
        }
        #endregion

        #region ContextMenu Event Hanlder
        void ExcludeEntityClick(object sender, EventArgs e)
        {
            foreach (IViewerObject ob in SelectedEntities)
            {
                if (ob is IViewerNode node)
                    EntityExcluded?.Invoke(this, new NodeEventArgs(node.Node, m_MouseRightButtonDownPoint));
            }
        }

        void RemoveEntityClick(object sender, EventArgs e)
        {
            foreach (IViewerObject ob in SelectedEntities)
            {
                if (ob is IViewerNode node)
                    EntityDeleted?.Invoke(this, new NodeEventArgs(node.Node, m_MouseRightButtonDownPoint));
            }
        }

        void EditSubmodel(object sender, EventArgs e)
        {
            EditSubmodelClicked?.Invoke(this, EventArgs.Empty);
        }

        void InsertNode_Click(object sender, EventArgs e)
        {
            RemoveDraggableEntities();

            NodeTypeEntry selectedNTE = null;
            foreach (NodeTypeEntry nte in NodeTypes)
                if (nte.MenuItem == sender)
                    selectedNTE = nte;

            InsertNode?.Invoke(this, new NodeEventArgs(null, m_MouseRightButtonDownPoint));
        }

        void RedoLayout_Click(object sender, EventArgs e)
        {
            RemoveDraggableEntities();
            RedoLayout?.Invoke(this, EventArgs.Empty);
        }

        // Selection modes event args

        void EdgeMode_Click(object sender, EventArgs e)
        {
            gViewer.InsertingEdge = !gViewer.InsertingEdge;

            if (gViewer.InsertingEdge && gViewer.PanButtonPressed)
                gViewer.PanButtonPressed = false;

            EdgeModeClicked?.Invoke(sender, EventArgs.Empty);
        }

        void PanMode_Click(object sender, EventArgs e)
        {
            PanButtonPressedOnMenu = !PanButtonPressedOnMenu;
            gViewer.InsertingEdge = false;

            PanModeClicked?.Invoke(sender, EventArgs.Empty);
        }
        #endregion

        internal void AddNodeType(NodeTypeEntry nte)
        {
            NodeTypes.Add(nte);
        }

        /// <summary>
        /// Unselect all draggable / selected entities
        /// </summary>
        public void ClearSelection()
        {
            gViewer.LayoutEditor.Clear();
        }
    }
}
