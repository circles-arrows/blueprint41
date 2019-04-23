using Blueprint41.Modeller.Schemas;
using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingNode = Microsoft.Msagl.Drawing.Node;
using DrawingEdge = Microsoft.Msagl.Drawing.Edge;
using DrawingColor = Microsoft.Msagl.Drawing.Color;
using DrawingLabel = Microsoft.Msagl.Drawing.Label;
using LayoutEdge = Microsoft.Msagl.Core.Layout.Edge;
using System.Drawing;

namespace Blueprint41.Modeller.Schemas
{
    public partial class Relationship
    {
        private DrawingEdge drawingEdge = null;
        public bool IsAddedOnViewGraph { get; internal set; }

        public bool DeleteIncludeRelationshipModel { get; private set; } = true;

        protected override void InitializeView()
        {
            OnNameChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                RenameEdge();
                Model.HasChanges = true;
            };

            OnTargetChanged += delegate (object sender, PropertyChangedEventArgs<NodeReference> e)
            {
                Model.HasChanges = true;
            };

            OnSourceChanged += delegate (object sender, PropertyChangedEventArgs<NodeReference> e)
            {
                Model.HasChanges = true;
            };
        }

        internal void CreateEdge()
        {
            if (Model == null)
                return;

            if (this.Source.Label == null || this.Target.Label == null)
                return;

            Dictionary<string, Entity> entitiesLookup = Model.DisplayedSubmodel.Entities.ToDictionary(x => x.Label, y => y);           

            if ((entitiesLookup.ContainsKey(Source.Label) || entitiesLookup.ContainsKey(Target.Label)) == false)
                return;

            if (drawingEdge != null)
                DeleteDrawingEdge(false);

            if (Model.Graph != null)
            {
                drawingEdge = Model.Graph.AddEdge(Source.Label, Target.Label);
                drawingEdge.Attr = CreateEdgeAttr();
                drawingEdge.Label = CreateLabel(drawingEdge, Model.ShowRelationshipLabels ? Type : "");

                LayoutEdge coreEdge = GeometryGraphCreator.CreateGeometryEdgeFromDrawingEdge(drawingEdge);
                CreateEdgeCurve(drawingEdge, coreEdge);

                Model.GeometryGraph.Edges.Add(coreEdge);
                drawingEdge.GeometryEdge = coreEdge;
                drawingEdge.UserData = this;
            }
            else
            {
                if (IsAddedOnViewGraph)
                {
                    IsAddedOnViewGraph = false;
                    return;
                }

                DrawingNode source = Model.GraphEditor.Graph.FindNode(Source.Label);
                DrawingNode target = Model.GraphEditor.Graph.FindNode(Target.Label);
                drawingEdge = Model.GraphEditor.Viewer.AddEdge(source, target, false);

                drawingEdge.Attr = CreateEdgeAttr();
                drawingEdge.UserData = this;
                RenameEdge();
            }
        }

        EdgeAttr CreateEdgeAttr()
        {
            return new EdgeAttr
            {
                Color = Styles.RELATION_LINE_COLOR.ToMsAgl(),
                LineWidth = 1,
                Separation = 1,
                Weight = 1,
                ArrowheadAtSource = ArrowStyle.NonSpecified,
                ArrowheadAtTarget = ArrowStyle.NonSpecified,
                ArrowheadLength = 10
            };
        }

        DrawingLabel CreateLabel(DrawingEdge edge, string rel)
        {
            DrawingLabel label = new DrawingLabel
            {
                Text = rel,
                FontName = "Times-Roman",
                FontColor = Styles.RELATION_LABEL_COLOR.ToMsAgl(),
                FontStyle = Microsoft.Msagl.Drawing.FontStyle.Regular,
                FontSize = 4,
                Owner = edge,
            };

            double width, height;
            StringMeasure.MeasureWithFont(rel, new Font(label.FontName, (float)label.FontSize, (System.Drawing.FontStyle)(int)label.FontStyle), out width,
                                          out height);
            label.Width = width;
            label.Height = height;

            return label;
        }

        void CreateEdgeCurve(DrawingEdge de, LayoutEdge le)
        {
            var a = de.SourceNode.GeometryNode.Center;
            var b = de.TargetNode.GeometryNode.Center;

            Site start, end, mids;

            if (Source.Label == Target.Label)
            {
                start = new Site(a);
                end = new Site(b);
                var mid1 = de.SourceNode.GeometryNode.Center;
                mid1.X += (de.SourceNode.GeometryNode.BoundingBox.Width / 3 * 2);
                var mid2 = mid1;
                mid1.Y -= de.SourceNode.GeometryNode.BoundingBox.Height / 2;
                mid2.Y += de.SourceNode.GeometryNode.BoundingBox.Height / 2;
                Site mid1s = new Site(mid1);
                Site mid2s = new Site(mid2);
                start.Next = mid1s;
                mid1s.Previous = start;
                mid1s.Next = mid2s;
                mid2s.Previous = mid1s;
                mid2s.Next = end;
                end.Previous = mid2s;
            }
            else
            {
                start = new Site(a);
                end = new Site(b);
                mids = new Site(a * 0.5 + b * 0.5);
                start.Next = mids;
                mids.Previous = start;
                mids.Next = end;
                end.Previous = mids;
            }

            SmoothedPolyline polyline = new SmoothedPolyline(start);
            le.UnderlyingPolyline = polyline;
            le.Curve = polyline.CreateCurve();

            if (Arrowheads.TrimSplineAndCalculateArrowheads(le, le.Curve, true, true) == false)
                Arrowheads.CreateBigEnoughSpline(le);
        }

        internal void DeleteDrawingEdge(bool includeRelationshipModel = true)
        {
            if ((object)Model == null || (object)drawingEdge == null || (object)Model.GraphEditor.Viewer.Graph == null)
                return;

            DeleteIncludeRelationshipModel = includeRelationshipModel;

            if (Model.GraphEditor.Viewer.Entities.SingleOrDefault(x => x.DrawingObject == drawingEdge) is IViewerEdge edge)
                Model.GraphEditor.Viewer.RemoveEdge(edge, true);

            drawingEdge = null;
            Model.AutoResize();
        }

        internal bool ContainsDrawingEdge
        {
            get { return drawingEdge != null; }
        }

        internal void SetDrawingEdge(DrawingEdge edge)
        {
            drawingEdge = edge;
            drawingEdge.Attr = CreateEdgeAttr();
            drawingEdge.UserData = this;
            RenameEdge();
        }

        internal void RenameEdge()
        {
            if (drawingEdge == null)
                return;

            if (drawingEdge.Source != Source.Label || drawingEdge.Target != Target.Label)
            {
                CreateEdge();
                return;
            }

            drawingEdge.Label = CreateLabel(drawingEdge, Model.ShowRelationshipLabels ? Type : "");
            Model.GraphEditor.Viewer.SetEdgeLabel(drawingEdge, drawingEdge.Label);
            Model.GraphEditor.Viewer.Graph.GeometryGraph.UpdateBoundingBox();
            Model.GraphEditor.Viewer.Invalidate();
        }

        public string InEntity
        {
            get
            {
                return Source.Label;
            }
            set
            {
                Source.Label = value;
            }
        }

        public string InEntityReferenceGuid
        {
            get
            {
                return Source.ReferenceGuid;
            }
            set
            {
                Source.ReferenceGuid = value;
            }
        }

        public string InProperty
        {
            get
            {
                return Source.Name;
            }
            set
            {
                Source.Name = value;
            }
        }

        public string InPropertyType
        {
            get
            {
                return Source.Type;
            }
            set
            {
                Source.Type = value;
            }
        }

        public bool InNullable
        {
            get
            {
                return Source.Nullable;
            }
            set
            {
                Source.Nullable = value;
            }
        }

        public string OutEntity
        {
            get
            {
                return Target.Label;
            }
            set
            {
                Target.Label = value;
            }
        }

        public string OutEntityReferenceGuid
        {
            get
            {
                return Target.ReferenceGuid;
            }
            set
            {
                Target.ReferenceGuid = value;
            }
        }

        public string OutProperty
        {
            get
            {
                return Target.Name;
            }
            set
            {
                Target.Name = value;
            }
        }

        public string OutPropertyType
        {
            get
            {
                return Target.Type;
            }
            set
            {
                Target.Type = value;
            }
        }

        public bool OutNullable
        {
            get
            {
                return Target.Nullable;
            }
            set
            {
                Target.Nullable = value;
            }
        }
    }
}
