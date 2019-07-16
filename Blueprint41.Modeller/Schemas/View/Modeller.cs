using Blueprint41.Modeller.Schemas;
using Blueprint41.Modeller.Controls;
using Microsoft.Msagl;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Routing;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Layout.Layered;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DrawingNode = Microsoft.Msagl.Drawing.Node;
using System.Diagnostics;

namespace Blueprint41.Modeller.Schemas
{
    public partial class Modeller
    {
        public ModellerType ModellerType
        {
            get
            {
                if (Model == null || Model.Type == null)
                    return ModellerType.Blueprint41;

                return (ModellerType)Enum.Parse(typeof(ModellerType), Model.Type);
            }
        }

        public bool HasChanges { get; internal set; }

        public LayoutMethod ViewMode
        {
            get { return GraphEditor.Viewer.CurrentLayoutMethod; }
            set { GraphEditor.Viewer.CurrentLayoutMethod = value; }
        }

        private GraphEditor m_GraphEditor = null;
        public GraphEditor GraphEditor
        {
            get
            {
                return m_GraphEditor;
            }
            set
            {
                UnRegisterEvents();

                m_GraphEditor = value;

                if (m_GraphEditor != null)
                {
                    RebindControl();
                    RegisterEvents();
                }
            }
        }

        public delegate void BeforeReBindControl();
        public delegate void AfterReBindControl();

        public BeforeReBindControl BeforeReBind { get; set; }
        public AfterReBindControl AfterReBind { get; set; }

        /// <summary>
        /// A new graph, it will be null after assigning to the viewer
        /// </summary>
        internal Graph Graph { get; private set; }

        /// <summary>
        /// Graph's Geometry graph, it will be null after assigning to the viewer
        /// </summary>
        internal GeometryGraph GeometryGraph { get; private set; }

        private void MouseUp(object sender, MsaglMouseEventArgs e)
        {
            AutoResize();
        }

        private void NodeMouseUp(object sender, NodeEventArgs e)
        {
            Submodel.NodeLocalType entity = e?.Node?.Node?.UserData as Submodel.NodeLocalType;
            if (entity != null)
                entity.CaptureCoordinates();
        }

        private void RedoLayout(object sender, EventArgs e)
        {
            if (Model.GraphEditor.Viewer.Graph == null)
                return;

            var currentViewMode = ViewMode;

            if (ViewMode == LayoutMethod.UseSettingsOfTheGraph)
                ViewMode = LayoutMethod.MDS;

            GraphEditor.Viewer.CurrentLayoutMethod = ViewMode;
            GraphEditor.Viewer.NeedToCalculateLayout = true;
            GraphEditor.Viewer.Graph = Model.GraphEditor.Viewer.Graph;
            GraphEditor.Viewer.NeedToCalculateLayout = false;
            GraphEditor.Viewer.Graph = Model.GraphEditor.Viewer.Graph;

            ViewMode = currentViewMode;
        }

        internal void UnRegisterEvents()
        {
            if (m_GraphEditor != null)
            {
                GraphEditor.RedoLayout -= RedoLayout;
                ((IViewer)m_GraphEditor.Viewer).MouseUp -= MouseUp;
                m_GraphEditor.NodeSelected -= NodeMouseUp;
            }
        }

        internal void RegisterEvents()
        {
            if (m_GraphEditor != null)
            {
                ((IViewer)m_GraphEditor.Viewer).MouseUp += MouseUp;
                m_GraphEditor.NodeSelected += NodeMouseUp;
                m_GraphEditor.RedoLayout += RedoLayout;
            }
        }

        public bool ShowRelationshipLabels
        {
            get { return m_ShowRelationshipLabels; }
            set
            {
                m_ShowRelationshipLabels = value;
                RebindControl();
            }
        }
        private bool m_ShowRelationshipLabels = true;

        private Submodel m_DisplayedSubmodel = null;
        public Submodel DisplayedSubmodel
        {
            get
            {
                return m_DisplayedSubmodel;
            }
            set
            {
                if (m_DisplayedSubmodel == value)
                    return;

                m_DisplayedSubmodel = value;
                RebindControl();
            }
        }

        private bool m_ShowInheritedRelationships = false;

        public bool ShowInheritedRelationships
        {
            get { return m_ShowInheritedRelationships; }
            set
            {
                m_ShowInheritedRelationships = value;
                RebindControl();
            }
        }

        public Submodel MainSubmodel { get; set; }

        internal void RebindControl()
        {
            if (GraphEditor == null || DisplayedSubmodel == null)
                return;

            BeforeReBind.Invoke();

            GraphEditor.Graph = null;

            Graph = new Graph("graph");
            GeometryGraph = new GeometryGraph() { Margins = 4 };
            GeometryGraph.MinimalWidth = 200;
            GeometryGraph.MinimalHeight = 200;

            foreach (Submodel.NodeLocalType node in DisplayedSubmodel.Node)
            {
                node.CreateNode();
                node.Parent = DisplayedSubmodel;
            }

            List<Relationship> relationships = DisplayedSubmodel.GetRelationships(ShowInheritedRelationships);

            foreach (Relationship relationship in relationships)
                relationship.CreateEdge();

            UpdateEdgesPlacement();
            Graph.GeometryGraph = GeometryGraph;

            GraphEditor.Viewer.NeedToCalculateLayout = GraphEditor.Viewer.CurrentLayoutMethod != LayoutMethod.UseSettingsOfTheGraph;
            GraphEditor.Graph = Graph;
            GraphEditor.Viewer.NeedToCalculateLayout = true;

            GeometryGraph.UpdateBoundingBox();

            GraphReset();

            Graph = null;
            GeometryGraph = null;

            AfterReBind.Invoke();
        }

        internal void UpdateEdgesPlacement()
        {
            if (DisplayedSubmodel.Node.Count <= 0)
                return;

            GeometryGraph geomGraph = GeometryGraph ?? GraphEditor.Viewer.Graph.GeometryGraph;

            geomGraph.BoundingBox = geomGraph.PumpTheBoxToTheGraphWithMargins();
            EdgeLabelPlacement placement = new EdgeLabelPlacement(geomGraph);
            placement.Run();
        }

        internal void GraphReset()
        {
            GraphEditor.Viewer.Transform = null;
            GraphEditor.Viewer.DrawingPanel.Invalidate();
        }

        internal void CaptureCoordinates()
        {
            foreach (var node in DisplayedSubmodel.Node)
            {
                node.CaptureCoordinates();
            }
        }

        internal void AutoResize()
        {
            if (GraphEditor != null && DisplayedSubmodel.Node.Count > 0)
            {
                AutoResizeWithoutUndoRedo();
                Invalidate();
            }
        }

        void AutoResizeWithoutUndoRedo()
        {
            if (GraphEditor.Viewer.Graph == null)
                return;

            GeometryGraph geometryGraph = GraphEditor.Viewer.Graph.GeometryGraph;

            if (GraphEditor.Viewer.Graph.GeometryGraph != null)
            {
                var r = new Microsoft.Msagl.Core.Geometry.Rectangle();

                foreach (var n in geometryGraph.Nodes)
                {
                    r = n.BoundingBox;
                    break;
                }
                foreach (var n in geometryGraph.Nodes)
                {
                    r.Add(n.BoundingBox);
                }
                foreach (var e in geometryGraph.Edges)
                {
                    r.Add(e.BoundingBox);
                    if (e.Label != null)
                        r.Add(e.Label.BoundingBox);
                }

                r.Left -= geometryGraph.Margins;
                r.Top += geometryGraph.Margins;
                r.Bottom -= geometryGraph.Margins;
                r.Right += geometryGraph.Margins;

                r.Add(geometryGraph.BoundingBox);

                if (geometryGraph.BoundingBox.Contains(r) == false)
                    geometryGraph.BoundingBox = r;
            }
        }

        internal void Invalidate()
        {
            GraphEditor.Viewer.Invalidate();
        }

        internal void RedoLayout()
        {
            RedoLayout(this, EventArgs.Empty);
        }

        public void RemoveAllEdges(Entity entity)
        {
            List<Relationship> relationships = entity.GetCurrentRelationshipsInGraph(DisplayedSubmodel);
            foreach (Relationship relationship in relationships)
                relationship.DeleteDrawingEdge(false);

            AutoResize();
        }

        public void CreateAllEdges(Entity entity)
        {
            List<Relationship> relationships = entity.GetRelationships(DisplayedSubmodel, RelationshipDirection.Both, ShowInheritedRelationships).ToList();
            foreach (Relationship relationship in relationships)
            {
                relationship.CreateEdge();
            }
            AutoResize();
        }

        protected override void InitializeView()
        {
            OnTypeChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                Model.HasChanges = true;
            };

            OnEntitiesChanged += delegate (object sender, PropertyChangedEventArgs<EntitiesLocalType> e)
            {
                RebindControl();

                Model.HasChanges = true;
            };
            OnRelationshipsChanged += delegate (object sender, PropertyChangedEventArgs<RelationshipsLocalType> e)
            {
                RebindControl();

                Model.HasChanges = true;
            };
            OnSubmodelsChanged += delegate (object sender, PropertyChangedEventArgs<SubmodelsLocalType> e)
            {
                if (Model.DisplayedSubmodel != null)
                    RebindControl();

                Model.HasChanges = true;
            };

            OnFunctionalIdsChanged += delegate (object sender, PropertyChangedEventArgs<FunctionalIdsLocalType> e)
            {
                Model.HasChanges = true;
            };
        }

        public partial class EntitiesLocalType
        {
            protected override void InitializeView()
            {
                EntityCollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
                {
                    switch (e.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            foreach (Entity entity in e.NewItems)
                            {
                                if (Model.Entities.Entity.Any(item => item.Label == entity.Label && (object)item != entity))
                                {
                                    System.Windows.Forms.MessageBox.Show($@"Entity with label ""{entity.Label}"" already exist.", "Info", System.Windows.Forms.MessageBoxButtons.OK);
                                    return;
                                }
                            }
                            break;
                        case NotifyCollectionChangedAction.Remove:
                        case NotifyCollectionChangedAction.Reset:
                            break;
                    }

                    Model.HasChanges = true;
                };
            }
        }

        public partial class RelationshipsLocalType
        {
            protected override void InitializeView()
            {
                RelationshipCollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
                {
                    switch (e.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            foreach (Relationship item in e.NewItems)
                                item.CreateEdge();
                            break;
                        case NotifyCollectionChangedAction.Remove:
                            foreach (Relationship item in e.OldItems)
                                item.DeleteDrawingEdge(false);

                            break;
                        case NotifyCollectionChangedAction.Reset:
                            foreach (Relationship item in e.OldItems)
                                item.DeleteDrawingEdge(false);
                            break;
                    }

                    Model.HasChanges = true;
                };
            }
        }

        public partial class SubmodelsLocalType
        {
            protected override void InitializeView()
            {
                SubmodelCollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
                {
                    switch (e.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            // item added
                            break;
                        case NotifyCollectionChangedAction.Remove:
                            if (e.OldItems.Contains(Model.DisplayedSubmodel))
                                Model.RebindControl();
                            break;
                        case NotifyCollectionChangedAction.Reset:
                            if (Model.DisplayedSubmodel != null)
                                Model.RebindControl();
                            break;
                    }

                    if (Model.Entities.Entity.Count != 0)
                        Model.HasChanges = true;
                };
            }
        }
    }
}
