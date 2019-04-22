using Blueprint41.Modeller.Schemas;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Node = Microsoft.Msagl.Core.Layout.Node;
using DrawingEdge = Microsoft.Msagl.Drawing.Edge;
using DrawingNode = Microsoft.Msagl.Drawing.Node;
using Edge = Microsoft.Msagl.Core.Layout.Edge;
using GeometryPoint = Microsoft.Msagl.Core.Geometry.Point;
using System.Drawing;
using Microsoft.Msagl.Core.Layout;

namespace Blueprint41.Modeller.Schemas
{
    public partial class Submodel
    {
        public List<Relationship> CreatedInheritedRelationships = new List<Relationship>();

        protected override void InitializeView()
        {
            NodeCollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
            {
                if (this != Model.DisplayedSubmodel)
                    return;

                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (NodeLocalType item in e.NewItems)
                            item.CreateNode();
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (NodeLocalType item in e.OldItems)
                            item.DeleteNode();
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        foreach (NodeLocalType item in e.OldItems)
                            item.DeleteNode();
                        break;
                }

                Model.HasChanges = true;
            };

            OnExplainationChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                Model.HasChanges = true;
            };

            OnNameChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                Model.HasChanges = true;
            };

            OnChapterChanged += delegate (object sender, PropertyChangedEventArgs<Int32?> e)
            {
                Model.HasChanges = true;
            };

            OnIsDraftChanged += delegate (object sender, PropertyChangedEventArgs<bool> e)
            {
                Model.HasChanges = true;
            };

            OnIsLaboratoryChanged += delegate (object sender, PropertyChangedEventArgs<bool> e)
            {
                Model.HasChanges = true;
            };
        }

        internal void AddEntities(List<Entity> selectedEntities, double x, double y)
        {
            foreach (Entity entity in selectedEntities)
            {
                NodeLocalType nodeLocalType = new Schemas.Submodel.NodeLocalType(Model);
                nodeLocalType.Label = entity.Label;
                nodeLocalType.Xcoordinate = x;
                nodeLocalType.Ycoordinate = y;
                nodeLocalType.EntityGuid = entity.Guid;
                Node.Add(nodeLocalType);
            }
        }

        public partial class NodeLocalType
        {

            private DrawingNode drawingNode = null;
            private bool IsNodeSelected = false;

            protected override void InitializeView()
            {
                OnLabelChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
                {
                    if (Model == null)
                        return;

                    if (Parent != Model.DisplayedSubmodel)
                        return;

                    CreateNode();

                    Model.HasChanges = true;
                };

                OnEntityGuidChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
                {
                    Model.HasChanges = true;
                };

                OnXcoordinateChanged += delegate (object sender, PropertyChangedEventArgs<double?> e)
                {
                    Model.HasChanges = true;
                };

                OnYcoordinateChanged += delegate (object sender, PropertyChangedEventArgs<double?> e)
                {
                    Model.HasChanges = true;
                };
            }

            internal void Highlight()
            {
                if (Model == null || drawingNode == null)
                    return;

                double zoom = Model.DisplayedSubmodel.Node.Count / 25d;
                if (Model.GraphEditor.Viewer.ZoomF < zoom)
                    Model.GraphEditor.Viewer.ZoomF = zoom;

                Model.GraphEditor.Viewer.CenterToPoint(drawingNode.BoundingBox.Center);

                if (Model.GraphEditor.Viewer.Entities.SingleOrDefault(x => x.DrawingObject == drawingNode) is DNode dNode)
                {
                    dNode.DrawingNode.Attr.FillColor = Styles.NODE_BGCOLOR_SELECTED.ToMsAgl();
                    Model.GraphEditor.Viewer.Invalidate(dNode);
                }
            }

            internal void RemoveHighlight()
            {
                if (Model == null || drawingNode == null)
                    return;

                DNode node = Model.GraphEditor.Viewer.Entities.SingleOrDefault(x => x.DrawingObject == drawingNode) as DNode;
                DrawingNode dNode = node.DrawingNode;

                if (Entity.Virtual)
                    dNode.Attr.FillColor = Styles.NODE_BGCOLOR_VIRTUAL.ToMsAgl();
                else if (Entity.Abstract)
                    dNode.Attr.FillColor = Styles.NODE_BGCOLOR_ABSTRACT.ToMsAgl();
                else
                    dNode.Attr.FillColor = Styles.NODE_BGCOLOR_NORMAL.ToMsAgl();

                Model.GraphEditor.Viewer.Invalidate(node);
            }

            internal void CreateNode()
            {
                IsNodeSelected = false;
                if (Model == null)
                    return;

                if (drawingNode != null)
                {
                    IsNodeSelected = drawingNode.Attr.FillColor == Styles.NODE_BGCOLOR_SELECTED.ToMsAgl();
                    DeleteNode();
                }

                GeometryPoint center = new GeometryPoint(Xcoordinate ?? 0.0, Ycoordinate ?? 0.0);
                NodeAttr attr = CreateNodeAttr(Label, IsNodeSelected);

                if (Model.Graph != null)
                {
                    drawingNode = Model.Graph.AddNode(Label);
                    drawingNode.Attr = attr;
                    drawingNode.Label.Text = Entity.Label;
                    drawingNode.UserData = this;
                    CreateNodeGeometry(drawingNode, Model.Graph, Model.GeometryGraph, center, ConnectionToGraph.Connected);                    
                }
                else
                {
                    drawingNode = new DrawingNode(Label);
                    drawingNode.Attr = attr;
                    drawingNode.Label.Text = Entity.Label;
                    drawingNode.UserData = this;
                                       
                    IViewerNode viewerNode = Model.GraphEditor.Viewer.CreateIViewerNode(drawingNode, center, null);
                    Model.GraphEditor.Viewer.AddNode(viewerNode, false);
                    Model.GraphEditor.Viewer.Invalidate(viewerNode);
                }

                Model.AutoResize();
            }

            NodeAttr CreateNodeAttr(string id, bool isSelected)
            {
                NodeTypeEntry nte = Model.GraphEditor.NodeTypes.Last();

                NodeAttr nodeAttr = new NodeAttr();
                nodeAttr.Id = id;
                nodeAttr.Color = Styles.NODE_LINE_COLOR.ToMsAgl();


                if (Entity.Virtual)
                    nodeAttr.FillColor = Styles.NODE_BGCOLOR_VIRTUAL.ToMsAgl();
                else if (Entity.Abstract)
                    nodeAttr.FillColor = Styles.NODE_BGCOLOR_ABSTRACT.ToMsAgl();
                else
                    nodeAttr.FillColor = Styles.NODE_BGCOLOR_NORMAL.ToMsAgl();

                if (isSelected)
                    nodeAttr.FillColor = Styles.NODE_BGCOLOR_SELECTED.ToMsAgl();

                nodeAttr.Shape = nte.Shape;
                nodeAttr.LabelMargin = 8;
                nodeAttr.LineWidth = 4;

                return nodeAttr;
            }

            void CreateNodeGeometry(DrawingNode node, Graph graph, GeometryGraph geometryGraph, GeometryPoint center, ConnectionToGraph connectionTo = ConnectionToGraph.Connected)
            {
                double width, height;
                StringMeasure.MeasureWithFont(node.Label.Text, new Font(node.Label.FontName, (float)node.Label.FontSize, (System.Drawing.FontStyle)(int)node.Label.FontStyle), out width,
                                              out height);

                node.Label.Width = width;
                node.Label.Height = height;

                if (node.Label != null)
                {
                    width += 2 * node.Attr.LabelMargin;
                    height += 2 * node.Attr.LabelMargin;
                }
                if (width < graph.Attr.MinNodeWidth)
                    width = graph.Attr.MinNodeWidth;
                if (height < graph.Attr.MinNodeHeight)
                    height = graph.Attr.MinNodeHeight;

                Node geomNode =
                    node.GeometryNode =
                    GeometryGraphCreator.CreateGeometryNode(graph, geometryGraph, node, connectionTo);

                geomNode.BoundaryCurve = NodeBoundaryCurves.GetNodeBoundaryCurve(node, width, height);
                geomNode.BoundaryCurve.Translate(center);
                geomNode.Center = center;
            }

            internal void DeleteNode()
            {
                if (Model == null || drawingNode == null || Model.GraphEditor.Viewer.Graph == null)
                    return;

                if (Model.GraphEditor.Viewer.Entities.SingleOrDefault(x => x.DrawingObject == drawingNode) is IViewerNode node)
                    Model.GraphEditor.Viewer.RemoveNode(node, true);

                drawingNode = null;
                Model.AutoResize();
            }

            internal void CaptureCoordinates()
            {
                if (Model == null)
                    return;

                Xcoordinate = drawingNode?.BoundingBox.Center.X ?? 0.0;
                Ycoordinate = drawingNode?.BoundingBox.Center.Y ?? 0.0;
            }

            public override string ToString()
            {
                return Label;
            }
        }
    }
}
