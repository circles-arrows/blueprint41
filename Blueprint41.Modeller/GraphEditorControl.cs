using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Blueprint41.Modeller.Schemas;

namespace Blueprint41.Modeller
{
    public partial class GraphEditorControl : UserControl
    {
        public event EventHandler<NodeEventArgs> NodeInsertedByUser;

        public event EventHandler<NodeEventArgs> SelectedNodeChanged;

        public event EventHandler<EdgeEventArgs> SelectedEdgeChanged;

        public event EventHandler<InsertRelationshipEventArgs> InsertRelationship;

        public event EventHandler<EventArgs> RedoLayout;

        public event EventHandler<EventArgs> AddDisplayedEntities;

        public event EventHandler<NodeEventArgs> RemoveNodeFromDiagramChanged;

        public event EventHandler<EdgeEventArgs> RemoveEdgeFromDiagramChanged;

        public event EventHandler<NodeEventArgs> RemoveNodeFromStorageChanged;

        public event EventHandler<EdgeEventArgs> RemoveEdgeFromStorageChanged;


        public event EventHandler NoSelectionEvent;

        private List<IViewerObject> _selectedEntities = null;

        public List<IViewerObject> SelectedEntities {
            get
            {
                return _selectedEntities;
            }
            set
            {
                _selectedEntities = value;
            }
        }

        /// <summary>
        /// Null if the current selection is edge or user does not select any node
        /// </summary>
        public Node SelectedNode { get; private set; }

        /// <summary>
        /// Null if the current selection is edge or user does not select any node
        /// </summary>
        public Node SelectedTargetNode { get; private set; }

        /// <summary>
        /// Null if the current selection is node or user does not select any edge
        /// </summary>
        public Edge SelectedEdge { get; private set; }

        protected IViewerObject RightClickedObject { get; private set; }

        public bool ShowContextMenuOnRightClick { get; internal set; }

        public ToolBar ToolBar
        {
            get
            {
                foreach (Control c in Viewer.Controls)
                {
                    ToolBar t = c as ToolBar;
                    if (t != null)
                        return t;
                }
                return null;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Graph Graph
        {
            get
            {
                return Viewer.Graph;
            }
            set
            {
                Viewer.Graph = value;
            }
        }

        [Browsable(false)]
        /// <summary>
        /// A list containing all the node type entries (custom node types for insertion).
        /// </summary>
        internal List<NodeTypeEntry> NodeTypes { get; private set; }

        [Browsable(false)]
        /// <summary>
        /// The point where the user called up the context menu.
        /// </summary>
        internal Microsoft.Msagl.Point MouseRightButtonDownPoint { get; private set; }

        public GraphEditorControl()
        {
            InitializeComponent();
            Initialize();
            ClearAll();
        }

        private void Initialize()
        {
            Viewer.MouseWheel += Viewer_MouseWheel;

            ShowContextMenuOnRightClick = false;
            Viewer.LayoutAlgorithmSettingsButtonVisible = false;
            NodeTypes = new List<Modeller.NodeTypeEntry>();

            //ToolBar.ButtonClick += new ToolBarButtonClickEventHandler(ToolBar_ButtonClick);
            (Viewer as Microsoft.Msagl.Drawing.IViewer).MouseMove += Viewer_MouseMove;
            (Viewer as Microsoft.Msagl.Drawing.IViewer).MouseDown += Viewer_MouseDown;
            (Viewer as Microsoft.Msagl.Drawing.IViewer).MouseUp += Viewer_MouseUp;
        }
        
        private void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            foreach (NodeTypeEntry nodeTypeEntry in NodeTypes)
            {
                if (nodeTypeEntry.Button == e.Button)
                {
                    Microsoft.Msagl.Point center = new Microsoft.Msagl.Point();
                    Random random = new Random();

                    Rectangle rect1 = Viewer.ClientRectangle;//gViewer.Graph.GeometryGraph.BoundingBox;
                    Microsoft.Msagl.Splines.Rectangle rect2 = Viewer.Graph.BoundingBox;
                    Microsoft.Msagl.Point p = Viewer.ScreenToSource(rect1.Location);
                    Microsoft.Msagl.Point p2 = Viewer.ScreenToSource(rect1.Location + rect1.Size);
                    if (p.X < rect2.Left)
                        p.X = rect2.Left;
                    if (p2.X > rect2.Right)
                        p2.X = rect2.Right;
                    if (p.Y > rect2.Top)
                        p.Y = rect2.Top;
                    if (p2.Y < rect2.Bottom)
                        p2.Y = rect2.Bottom;
                    Microsoft.Msagl.Splines.Rectangle rect = new Microsoft.Msagl.Splines.Rectangle(p, p2);

                    center.X = rect.Left + random.NextDouble() * rect.Width;
                    center.Y = rect.Bottom + random.NextDouble() * rect.Height;

                    //Microsoft.Msagl.Drawing.Node newNode = InsertNode(center, nodeTypeEntry);
                    if (NodeInsertedByUser != null)
                        NodeInsertedByUser(this, new Modeller.NodeEventArgs(null, center));
                }
            }
        }

        public void RemoveEdges(string nodeId)
        {
            List< Edge> edges = Graph.Edges.Where(edge => edge.Source == nodeId || edge.Target == nodeId).ToList();
            foreach (var edge in edges)
            {
                Graph.Edges.Remove(edge);
            }
        }

        public void AddEdge(string sourceNodeId, string targetNodeId, string edgeLabel)
        {
            //Edge edge = Graph.AddEdge(sourceNodeId, edgeLabel, targetNodeId);
            DEdge edge = (DEdge)Viewer.CreateEdge((Node)Graph.NodeMap[sourceNodeId], (Node)Graph.NodeMap[targetNodeId]);
            //edge.Edge.Attr.Color = Microsoft.Msagl.Drawing.GraphStyles.FORMS_WARNING;
            Viewer.AddEdge(edge, true);
            RelabelEdge(edge.Edge, edgeLabel);
            edge.Label.Font = new Font("Arial", 8);
            Viewer.Invalidate();
        }

        ///// <summary>
        ///// Call this to recalculate the graph layout
        ///// </summary>
        //public virtual void RedoLayout()
        //{
        //    Viewer.NeedToCalculateLayout = true;
        //    Viewer.Graph = Viewer.Graph;
        //    Viewer.NeedToCalculateLayout = false;
        //    Viewer.Graph = Viewer.Graph;
        //}

        public void InvalidateViewer()
        {
            Viewer.Invalidate();
        }

        /// <summary>
        /// Changes a node's label and updates the display
        /// </summary>
        /// <param name="n">The node whose label has to be changed</param>
        /// <param name="newLabel">The new label</param>
        public void RelabelNode(Microsoft.Msagl.Drawing.Node n, string newLabel)
        {
            if (n.LabelText == newLabel)
                return;

            foreach (var edge in n.InEdges.ToList())
            {
                Graph.Edges.Remove(edge);
                Graph.AddEdge(edge.Source, newLabel);
            }

            foreach (var edge in n.OutEdges.ToList()) 
            {
                Graph.Edges.Remove(edge);
                Graph.AddEdge(newLabel, edge.Target);
            }

            Graph.RemoveNode(n);
            n.Label.Text = newLabel;
            n.Id = newLabel;
            Graph.AddNode(n);
            Viewer.ResizeNodeToLabel(n);
        }

        /// <summary>
        /// Changes an edge's label and updates the display
        /// </summary>
        /// <param name="e">The edge whose label has to be changed</param>
        /// <param name="newLabel">The new label</param>
        internal void RelabelEdge(Microsoft.Msagl.Drawing.Edge e, string newLabel)
        {
            if (e.Label == null)
                e.Label = new Microsoft.Msagl.Drawing.Label(newLabel);
            else
                e.Label.Text = newLabel;

            Viewer.SetEdgeLabel(e, e.Label);
            Viewer.Invalidate();
        }

        /// <summary>
        /// Builds the context menu for when the user right-clicks on the graph.
        /// </summary>
        /// <param name="point">The point where the user clicked</param>
        /// <returns>The context menu to be displayed</returns>
        protected virtual ContextMenuStrip BuildContextMenu(Microsoft.Msagl.Point point, int x, int y)
        {
            ContextMenuStrip cm = new ContextMenuStrip();

            ToolStripMenuItem mi;

            foreach (NodeTypeEntry nte in NodeTypes)
            {
                mi = new ToolStripMenuItem();
                mi.Text = "Insert " + nte.Name;
                mi.Click += new EventHandler(insertNode_Click);
                mi.Tag = new Microsoft.Msagl.Point(x, y);
                cm.Items.Add(mi);
            }

            mi = new ToolStripMenuItem("Edit Submodel");
            //mi.OwnerDraw = false;
            mi.Click += new EventHandler(DisplayEntity);
            cm.Items.Add(mi);

            SelectedTargetNode = GetNodeAtPosition(x, y);

            if ((SelectedNode != null && SelectedNode.Attr.GeometryNode.LineWidth == 2) &&
                SelectedTargetNode != null)
            {
                mi = new ToolStripMenuItem("Insert Relationship");
                cm.Items.Add(mi);

                ToolStripLabel source = new ToolStripLabel("-Source Property Type-");
                source.Font = new Font(source.Font, FontStyle.Bold);
                mi.DropDownItems.Add(source);

                foreach (PropertyType item in Enum.GetValues(typeof(PropertyType)))
                {
                    if (item == PropertyType.None)
                        continue;

                    ToolStripMenuItem sourcePropertyTypeMenuItem = new ToolStripMenuItem(item.ToString());
                    sourcePropertyTypeMenuItem.Tag = item;
                    mi.DropDownItems.Add(sourcePropertyTypeMenuItem);

                    ToolStripLabel target = new ToolStripLabel("-Target Property Type-");
                    target.Font = new Font(target.Font, FontStyle.Bold);
                    sourcePropertyTypeMenuItem.DropDownItems.Add(target);

                    foreach (PropertyType item2 in Enum.GetValues(typeof(PropertyType)))
                    {
                        ToolStripMenuItem targetPropertyTypeMenuItem = new ToolStripMenuItem(item2.ToString());
                        targetPropertyTypeMenuItem.Tag = item2;
                        targetPropertyTypeMenuItem.Click += new EventHandler(insertRelationship_Click);
                        sourcePropertyTypeMenuItem.DropDownItems.Add(targetPropertyTypeMenuItem);
                    }
                }
            }

            if (Viewer.Graph.NodeCount == 0)
                return cm;

            ToolStripSeparator separator = new ToolStripSeparator();
            cm.Items.Add(separator);

            RightClickedObject = Viewer.ObjectUnderMouseCursor;
            if (RightClickedObject != null && (SelectedNode != null | SelectedEdge != null | SelectedEntities.Count > 0))
            {
                if (RightClickedObject is IViewerNode && (Node)RightClickedObject.DrawingObject == SelectedNode ||
                   (RightClickedObject is IViewerEdge && (Edge)RightClickedObject.DrawingObject == SelectedEdge))
                {
                    if (!(RightClickedObject is IViewerEdge))
                    {
                        mi = new ToolStripMenuItem();
                        mi.Text = "Remove from Diagram";
                        mi.Click += new EventHandler(removeFromDiagram_Click);
                        cm.Items.Add(mi);
                    }

                    mi = new ToolStripMenuItem();
                    mi.Text = "Remove from Storage";
                    mi.ForeColor = Styles.FORMS_WARNING;
                    mi.Click += new EventHandler(removeFromStorage_Click);
                    cm.Items.Add(mi);
                }
                else if(SelectedEntities.Count > 0)
                {
                    var rightclickObject = SelectedEntities.Where(e => e.DrawingObject == RightClickedObject.DrawingObject).FirstOrDefault();

                    if(rightclickObject != null)
                    {
                        mi = new ToolStripMenuItem();
                        mi.Text = "Remove from Diagram";
                        mi.Click += new EventHandler(removeFromDiagram_Click);
                        cm.Items.Add(mi);

                        mi = new ToolStripMenuItem();
                        mi.Text = "Remove from Storage";
                        mi.ForeColor = Styles.FORMS_WARNING;
                        mi.Click += new EventHandler(removeFromStorage_Click);
                        cm.Items.Add(mi);
                    }
                }
            }

            mi = new ToolStripMenuItem();
            mi.Text = "Redo Layout";
            mi.Click += new EventHandler(RedoLayout);
            cm.Items.Add(mi);

            return cm;
        }

        /// <summary>
        /// Clears the editor and resets it to a default size.
        /// </summary>
        internal void ClearAll()
        {
            Graph g = new Graph();
            g.GeometryGraph = new Microsoft.Msagl.GeometryGraph();
            g.GeometryGraph.BoundingBox = new Microsoft.Msagl.Splines.Rectangle(0, 0, 200, 100);
            Viewer.Graph = g;
            Viewer.Refresh();
        }

        private Node GetNodeAtPosition(int x, int y)
        {
            object obj = null;

            try
            {
                obj = Viewer.GetObjectAt(x, y);
            }
            catch
            {
                return null;
            }

            Node node = null;
            
            DNode dnode = obj as DNode;
            DLabel dl = obj as DLabel;
            if (dnode != null)
                node = dnode.DrawingNode;
            else if (dl != null)
            {
                if (dl.Parent is DNode)
                    node = (dl.Parent as DNode).DrawingNode;
            }

            return node;
        }

        private Edge GetEdgeAtPosition(int x, int y)
        {
            object obj = null;

            try
            {
                obj = Viewer.GetObjectAt(x, y);
            }
            catch
            {
                return null;
            }

            Edge edge = null;
            DNode dnode = obj as DNode;
            DEdge dedge = obj as DEdge;
            DLabel dl = obj as DLabel;
            if (dedge != null)
                edge = dedge.DrawingEdge;
            else if (dl != null)
            {
                if (dl.Parent is DEdge)
                    edge = (dl.Parent as DEdge).DrawingEdge;
            }

            return edge;
        }

        /// <summary>
        /// Overloaded. Adds a new node type to the list. If the parameter contains an image, a button with that image will be added to the toolbar.
        /// </summary>
        /// <param name="nte">The NodeTypeEntry structure containing the initial aspect of the node, type name, and additional parameters required
        /// for node insertion.</param>
        private void AddNodeType(NodeTypeEntry nte)
        {
            NodeTypes.Add(nte);

            if (nte.ButtonImage != null)
            {
                ToolBar tb = this.ToolBar;
                ToolBarButton btn = new ToolBarButton();
                tb.ImageList.Images.Add(nte.ButtonImage);
                btn.ImageIndex = tb.ImageList.Images.Count - 1;
                tb.Buttons.Add(btn);
                nte.Button = btn;
            }
        }

        public void AddNodeType(string name, Shape shape, Microsoft.Msagl.Drawing.Color fillcolor, Microsoft.Msagl.Drawing.Color fontcolor, int fontsize, object userdata, string deflabel)
        {
            AddNodeType(new NodeTypeEntry(name, shape, fillcolor, fontcolor, fontsize, userdata, deflabel));
        }

        private void CreateNodeGeometry(Microsoft.Msagl.Drawing.Node node, Microsoft.Msagl.Point center)
        {
            double width, height;
            Microsoft.Msagl.GraphViewerGdi.StringMeasure.MeasureWithFont(node.Label.Text, new Font(node.Label.FontName, node.Label.FontSize), out width, out height);

            if (node.Label != null)
            {
                width += 2 * node.Attr.LabelMargin;
                height += 2 * node.Attr.LabelMargin;
            }
            if (width < Viewer.Graph.Attr.MinNodeWidth)
                width = Viewer.Graph.Attr.MinNodeWidth;
            if (height < Viewer.Graph.Attr.MinNodeHeight)
                height = Viewer.Graph.Attr.MinNodeHeight;


            Microsoft.Msagl.Node geomNode =
                node.Attr.GeometryNode = Microsoft.Msagl.Drawing.CreateLayoutGraph.CreateGeometryNode(this.Graph.GeometryGraph, node, Connection.Disconnected);
            geomNode.BoundaryCurve = Microsoft.Msagl.Drawing.NodeBoundaryCurves.GetNodeBoundaryCurve(node, width, height).Translate(center);
            geomNode.Center = center;
        }

        private void Viewer_MouseDown(object sender, Microsoft.Msagl.Drawing.MsaglMouseEventArgs e)
        {
            /*if (e.RightButtonIsPressed && this.gViewer.DrawingLayoutEditor.SelectedEdge != null && this.gViewer.ObjectUnderMouseCursor == this.gViewer.DrawingLayoutEditor.SelectedEdge)
                ProcessRightClickOnSelectedEdge(e);
            else*/
            //if (e.RightButtonIsPressed && ShowContextMenuOnRightClick)
            //{
            //    MouseRightButtonDownPoint = Viewer.ScreenToSource(e);

            //    ContextMenuStrip cm = BuildContextMenu(MouseRightButtonDownPoint, e.X, e.Y);

            //    cm.Show(this, new System.Drawing.Point(e.X, e.Y));
            //}
        }

        private void Viewer_MouseUp(object sender, Microsoft.Msagl.Drawing.MsaglMouseEventArgs e)
        {
            if (e.RightButtonIsPressed)
            {
                _selectedEntities = new List<IViewerObject>();
                foreach (var en in Viewer.Entities)
                {
                    if (en.MarkedForDragging && en is IViewerNode)
                    {
                        _selectedEntities.Add(en);
                    }
                }

                if (ShowContextMenuOnRightClick)
                {
                    MouseRightButtonDownPoint = Viewer.ScreenToSource(e);

                    ContextMenuStrip cm = BuildContextMenu(MouseRightButtonDownPoint, e.X, e.Y);

                    cm.Show(this, new System.Drawing.Point(e.X, e.Y));
                }
                return;
            }

            SelectedNode = null;
            SelectedEdge = null;
            SelectedTargetNode = null;

            Node node = GetNodeAtPosition(e.X, e.Y);
            Edge edge = GetEdgeAtPosition(e.X, e.Y);

            if (node == null && edge == null)
            {
                if (NoSelectionEvent != null)
                    NoSelectionEvent(this, new EventArgs());
                return;
            }

            if (node != null && node.Attr.LineWidth == 2)
            {
                SelectedNode = node;
                if (SelectedNodeChanged != null)
                {
                    Microsoft.Msagl.Point clickPosition = new Microsoft.Msagl.Point(e.X, e.Y);
                    SelectedNodeChanged(this, new NodeEventArgs(node, clickPosition));
                }
            }
            else if (edge != null)
            {
                SelectedEdge = edge;
                if (SelectedEdgeChanged != null)
                    SelectedEdgeChanged(this, new EdgeEventArgs(edge));
            }
            else
            {
                if(NoSelectionEvent != null)
                    NoSelectionEvent(this, new EventArgs());
            }

        }
        
        private void Viewer_MouseMove(object sender, MsaglMouseEventArgs e)
        {
            
        }

        private void Viewer_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
                Viewer.ZoomOutPressed();
            else
                Viewer.ZoomInPressed();
        }

        private void gViewer_SelectionChanged(object sender, EventArgs e)
        {
            IViewerObject editableObject = Viewer.ObjectUnderMouseCursor;
            if (editableObject != null)
            {
                if (editableObject.MarkedForDragging)
                    this.Viewer.SetToolTip(this.toolTip, "Drag me");
                else
                    this.Viewer.SetToolTip(this.toolTip, "Click me");
            }
            else
                this.Viewer.SetToolTip(toolTip, "");
        }

        private void gViewer_Click(object sender, EventArgs e)
        {
            Microsoft.Msagl.Drawing.IViewerObject obj = Viewer.ObjectUnderMouseCursor;
            if (obj != null)
                if (!obj.MarkedForDragging)
                {
                    this.Viewer.SetToolTip(toolTip, "Drag me");
                }
                else
                    this.Viewer.SetToolTip(toolTip, "");
        }

        private void DisplayEntity(object sender, EventArgs e)
        {
            if (AddDisplayedEntities != null)
                AddDisplayedEntities(this, e);
        }

        private void insertNode_Click(object sender, EventArgs e)
        {
            NodeTypeEntry selectedNTE = null;
            //if(sender is ToolStripMenuItem)

            Microsoft.Msagl.Point point = (Microsoft.Msagl.Point)(sender as ToolStripMenuItem).Tag;
            foreach (NodeTypeEntry nte in NodeTypes)
            {
                if (nte.MenuItem == sender)
                    selectedNTE = nte;
            }

            //Microsoft.Msagl.Drawing.Node newNode = InsertNode(MouseRightButtonDownPoint, selectedNTE);
            if (NodeInsertedByUser != null)
                NodeInsertedByUser(this, new NodeEventArgs(null, MouseRightButtonDownPoint));
        }

        private void insertRelationship_Click(object sender, EventArgs e)
        {
            if (SelectedNode == null || SelectedTargetNode == null)
                return;

            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            PropertyType targetPropertyType = (PropertyType)menuItem.Tag;
            PropertyType sourcePropertyType = (PropertyType)menuItem.OwnerItem.Tag;

            InsertRelationshipEventArgs eventArgs = new InsertRelationshipEventArgs(SelectedNode, SelectedTargetNode, sourcePropertyType, targetPropertyType);
            if (InsertRelationship != null)
                InsertRelationship(this, eventArgs);
        }

        private void removeFromDiagram_Click(object sender, EventArgs e)
        {
            IViewerEdge edge = RightClickedObject as IViewerEdge;
            if (edge != null && RemoveEdgeFromDiagramChanged != null)
                RemoveEdgeFromDiagramChanged(this, new EdgeEventArgs(edge.Edge));
            else
            {
                IViewerNode node = RightClickedObject as IViewerNode;
                if (node != null && RemoveNodeFromDiagramChanged != null)
                    RemoveNodeFromDiagramChanged(this, new NodeEventArgs(node.Node, new Microsoft.Msagl.Point()));
            }
        }

        private void removeFromStorage_Click(object sender, EventArgs e)
        {
            IViewerEdge edge = RightClickedObject as IViewerEdge;
            if (edge != null && RemoveEdgeFromStorageChanged != null)
                RemoveEdgeFromStorageChanged(this, new EdgeEventArgs(edge.Edge));
            else
            {
                IViewerNode node = RightClickedObject as IViewerNode;
                if (node != null && RemoveNodeFromStorageChanged != null)
                    RemoveNodeFromStorageChanged(this, new NodeEventArgs(node.Node, new Microsoft.Msagl.Point()));
            }
        }


        private void MenuItem_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (NodeTypes.Count - 1 < e.Index)
                return;


            e.DrawBackground();
            if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                e.Graphics.FillRectangle(SystemBrushes.Control, e.Bounds);

            NodeTypeEntry nte = (NodeTypes[e.Index] as NodeTypeEntry);
            int x = 14;
            if (nte.ButtonImage != null)
            {
                e.Graphics.DrawImage(nte.ButtonImage, e.Bounds.X, e.Bounds.Y);
                x = nte.ButtonImage.Width + 1;
            }
            MenuItem mi = sender as MenuItem;
            int h = (int)e.Graphics.MeasureString(mi.Text, Font).Height;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e.Graphics.DrawString(mi.Text, Font, SystemBrushes.HighlightText, x, (e.Bounds.Height - h) / 2 + e.Bounds.Y);
            else
                e.Graphics.DrawString(mi.Text, Font, SystemBrushes.ControlText, x, (e.Bounds.Height - h) / 2 + e.Bounds.Y);
        }
        private void MenuItem_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < NodeTypes.Count)
            {
                NodeTypeEntry nte = (NodeTypes[e.Index] as NodeTypeEntry);

                MenuItem mi = sender as MenuItem;
                e.ItemHeight = SystemInformation.MenuHeight;
                e.ItemWidth = (int)e.Graphics.MeasureString(mi.Text, Font).Width;

                if (nte.ButtonImage != null)
                {
                    if (e.ItemHeight < nte.ButtonImage.Height)
                        e.ItemHeight = nte.ButtonImage.Height;
                    e.ItemWidth += nte.ButtonImage.Width + 1;
                }
            }
        }
    }
}
