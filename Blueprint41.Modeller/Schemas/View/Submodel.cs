using Blueprint41.Modeller.Schemas;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            private IViewerNode viewerNode = null;
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
                };
            }

            internal void Highlight()
            {
                if (Model == null || viewerNode == null)
                    return;

                double zoom = Model.DisplayedSubmodel.Node.Count / 25d;
                if(Model.Viewer.ZoomF < zoom)
                    Model.Viewer.ZoomF = zoom;
                Model.Viewer.CenterToPoint(viewerNode.DrawingObject.BoundingBox.Center);
                viewerNode.Node.Attr.FillColor = Color.Red;
            }

            internal void RemoveHighlight()
            {
                if (Model == null || viewerNode == null)
                    return;

                if (Entity.Virtual)
                    viewerNode.Node.Attr.FillColor = Color.White;
                else if (Entity.Abstract)
                    viewerNode.Node.Attr.FillColor = Color.LightGreen;
                else
                    viewerNode.Node.Attr.FillColor = Color.Silver;

                Model.Viewer.Invalidate();
            }

            internal void CreateNode()
            {
                IsNodeSelected = false;
                if (Model == null)
                    return;

                if (viewerNode != null)
                {
                    IsNodeSelected = viewerNode.Node.Attr.FillColor == Color.Red;
                    DeleteNode();
                }         

                NodeTypeEntry nte = Model.GraphEditor.NodeTypes.Last();
                Microsoft.Msagl.Point center = new Microsoft.Msagl.Point(Xcoordinate ?? 0.0, Ycoordinate ?? 0.0);

                Node node = new Node(Label);
                node.Label.Text = Entity.Label;

                if(Entity.Virtual)
                    node.Attr.FillColor = Color.White;
                else if (Entity.Abstract)
                    node.Attr.FillColor = Color.LightGreen;
                else
                    node.Attr.FillColor = Color.Silver;

                node.Label.FontColor = nte.FontColor;
                node.Label.FontSize = nte.FontSize;
                node.Attr.Shape = nte.Shape;
                node.UserData = this;
                CreateNodeGeometry(node, center);
                viewerNode = Model.Viewer.CreateNode(node);
                Model.Viewer.AddNode(viewerNode, true);
                if(IsNodeSelected) viewerNode.Node.Attr.FillColor = Color.Red;

                Model.AutoResize();
            }

            internal void DeleteNode()
            {
                if (Model == null || viewerNode == null)
                    return;

                Model.GraphEditor.Graph.RemoveNode(viewerNode.Node);
                Model.Viewer.RemoveNode(viewerNode, false);

                Model.AutoResize();
            }

            internal void CaptureCoordinates()
            {
                if (Model == null)
                    return;

                Xcoordinate = viewerNode?.DrawingObject?.BoundingBox.Center.X ?? 0.0;
                Ycoordinate = viewerNode?.DrawingObject?.BoundingBox.Center.Y ?? 0.0;
            }

            private void CreateNodeGeometry(Node node, Microsoft.Msagl.Point center)
            {
                if (Model == null)
                    return;

                double width, height;
                Microsoft.Msagl.GraphViewerGdi.StringMeasure.MeasureWithFont(node.Label.Text, new System.Drawing.Font(node.Label.FontName, node.Label.FontSize), out width, out height);

                if (node.Label != null)
                {
                    width += 2 * node.Attr.LabelMargin;
                    height += 2 * node.Attr.LabelMargin;
                }
                if (width < Model.Viewer.Graph.Attr.MinNodeWidth)
                    width = Model.Viewer.Graph.Attr.MinNodeWidth;
                if (height < Model.Viewer.Graph.Attr.MinNodeHeight)
                    height = Model.Viewer.Graph.Attr.MinNodeHeight;


                Microsoft.Msagl.Node geomNode =
                    node.Attr.GeometryNode = CreateLayoutGraph.CreateGeometryNode(Model.GraphEditor.Graph.GeometryGraph, node, Connection.Disconnected);
                geomNode.BoundaryCurve = NodeBoundaryCurves.GetNodeBoundaryCurve(node, width, height).Translate(center);
                geomNode.Center = center;
            }

            public override string ToString()
            {
                return Label;
            }
        }
    }
}
