using Blueprint41.Modeller.Schemas;
using Microsoft.Msagl;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller.Schemas
{
    public partial class Modeller
    {
        private GraphEditorControl m_GraphEditor = null;
        public GraphEditorControl GraphEditor
        {
            get
            {
                return m_GraphEditor;
            }
            set
            {
                if (m_GraphEditor != null)
                {
                    GraphEditor.RedoLayout -= RedoLayout;
                    ((IViewer)m_GraphEditor.Viewer).MouseUp -= MouseUp;
                    m_GraphEditor.SelectedNodeChanged -= NodeMouseUp;
                }

                m_GraphEditor = value;

                if (m_GraphEditor != null)
                {
                    GraphEditor.Viewer.CurrentLayoutMethod = LayoutMethod.UseSettingsOfTheGraph;

                    RebindControl();
                    ((IViewer)m_GraphEditor.Viewer).MouseUp += MouseUp;
                    m_GraphEditor.SelectedNodeChanged += NodeMouseUp;
                    m_GraphEditor.RedoLayout += RedoLayout;
                }
            }
        }
        internal GViewer Viewer
        {
            get
            {
                if (GraphEditor != null)
                    return GraphEditor.Viewer;

                return null;
            }
        }


        private void MouseUp(object sender, MsaglMouseEventArgs e)
        {
            AutoResize();
        }
        private void NodeMouseUp(object sender, NodeEventArgs e)
        {
            Submodel.NodeLocalType entity = e?.Node?.UserData as Submodel.NodeLocalType;
            if (entity != null)
                entity.CaptureCoordinates();
        }
        private void RedoLayout(object sender, EventArgs e)
        {
            //// SugiyamaScheme works without much trouble
            //GraphEditor.Viewer.NeedToCalculateLayout = true;
            //GraphEditor.Viewer.Graph = Viewer.Graph;
            //GraphEditor.Viewer.NeedToCalculateLayout = false;
            //GraphEditor.Viewer.Graph = Viewer.Graph;
            //CaptureCoordinates();

            // MDS does not work well, after redoing the layout it crashes when you drag a node...
            //GraphEditor.Viewer.CurrentLayoutMethod = LayoutMethod.SugiyamaScheme;
            //GraphEditor.Viewer.NeedToCalculateLayout = true;
            //GraphEditor.Viewer.Graph = Viewer.Graph;
            //GraphEditor.Viewer.NeedToCalculateLayout = false;
            //GraphEditor.Viewer.Graph = Viewer.Graph;
            //CaptureCoordinates();

            //  GraphEditor = m_GraphEditor; // <--- MDS has bugs in the layout engine, rebind the control with captured coordinates & Sugiyama...

            GraphEditor.Viewer.CalculateLayout(Viewer.Graph);
            GraphEditor.Viewer.Graph = Viewer.Graph;
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

        internal void RebindControl()
        {
            if (GraphEditor == null || DisplayedSubmodel == null)
                return;

            GraphEditor.ClearAll();

            foreach (var node in DisplayedSubmodel.Node)
            {
                node.CreateNode();
                node.Parent = DisplayedSubmodel;
            }

            foreach (Relationship relationship in DisplayedSubmodel.GetRelationships(ShowInheritedRelationships))
            {
                relationship.CreateEdge();
            }

            AutoResize();
        }

        public void RedoAutoLayout()
        {
            GraphEditor.Viewer.NeedToCalculateLayout = true;
            GraphEditor.Viewer.Graph = Viewer.Graph;
            GraphEditor.Viewer.NeedToCalculateLayout = false;
            GraphEditor.Viewer.Graph = Viewer.Graph;
            CaptureCoordinates();
        }

        public void UpdateGraph()
        {
            GraphEditor.Viewer.NeedToCalculateLayout = false;

            // We must get the zoom factor and the scroll values of the viewer
            // and setting it after re assigning the graph object
            var zoom = Viewer.ZoomF;
            var hScroll = GetPropertyValue(typeof(GViewer), Viewer, "HVal");
            var vScroll = GetPropertyValue(typeof(GViewer), Viewer, "VVal");

            // MSAGL have this issue wherein it does not reflect directly after updating some nodes
            // Re assigning it will redraw the graph
            GraphEditor.Viewer.Graph = Viewer.Graph;
            GraphEditor.Viewer.ZoomF = zoom;
            BindProperty(GraphEditor.Viewer, "HVal", (int)hScroll);
            BindProperty(GraphEditor.Viewer, "VVal", (int)vScroll);
            CaptureCoordinates();
        }

        internal object GetPropertyValue(Type type, object instance, string fieldName)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            PropertyInfo field = type.GetProperty(fieldName, bindFlags);
            return field.GetValue(instance);
        }

        internal void BindProperty(object obj, string propertyName, object propertyValue)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName, bindFlags);

            if (propertyInfo != null && propertyInfo.CanWrite)
                propertyInfo.SetValue(obj, propertyValue, null);
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
            if (GraphEditor != null)
            {
                GraphEditor.Viewer.FitGraphBoundingBox();
            }
        }

        public void RemoveAllEdges(Entity entity)
        {
            List<Relationship> relationships = entity.GetCurrentRelationshipsInGraph(DisplayedSubmodel);
            foreach (Relationship relationship in relationships)
            {
                relationship.DeleteEdge();
            }
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
            OnEntitiesChanged += delegate (object sender, PropertyChangedEventArgs<EntitiesLocalType> e)
            {
                RebindControl();
            };
            OnRelationshipsChanged += delegate (object sender, PropertyChangedEventArgs<RelationshipsLocalType> e)
            {
                RebindControl();
            };
            OnSubmodelsChanged += delegate (object sender, PropertyChangedEventArgs<SubmodelsLocalType> e)
            {
                if (Model.DisplayedSubmodel != null)
                    RebindControl();
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
                                    System.Windows.Forms.MessageBox.Show($"Entity with label {entity.Label} already exist.", "Info", System.Windows.Forms.MessageBoxButtons.OK);
                                    return;
                                }
                            }
                            break;
                        case NotifyCollectionChangedAction.Remove:
                        case NotifyCollectionChangedAction.Reset:
                            break;
                    }
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
                                item.DeleteEdge();
                            break;
                        case NotifyCollectionChangedAction.Reset:
                            foreach (Relationship item in e.OldItems)
                                item.DeleteEdge();
                            break;
                    }
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
                };
            }
        }
    }
}
