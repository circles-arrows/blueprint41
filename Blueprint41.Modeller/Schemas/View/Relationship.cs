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
    public partial class Relationship
    {
        private IViewerEdge viewerEdge = null;

        protected override void InitializeView()
        {
            OnNameChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                CreateEdge();
            };
        }
        
        internal void CreateEdge()
        {
            if (Model == null)
                return;

            if (this.Source.Label == null || this.Target.Label == null)
                return;

            if (!Model.DisplayedSubmodel.Entities.Any(item => item.Label == this.Source.Label) || !Model.DisplayedSubmodel.Entities.Any(item => item.Label == this.Target.Label))
                return;

            if (viewerEdge != null)
                DeleteEdge();

            viewerEdge = Model.Viewer.CreateEdge((Node)Model.GraphEditor.Graph.NodeMap[this.Source.Label], (Node)Model.GraphEditor.Graph.NodeMap[this.Target.Label]);
            viewerEdge.Edge.UserData = this;

            viewerEdge.Edge.Attr.Color = Styles.RELATION_LINE_COLOR.ToMsAgl();

            if (Model.ShowRelationshipLabels)
            {
                Label label = new Label(Type);
                label.FontSize = 5;
                label.FontColor = Styles.RELATION_LABEL_COLOR.ToMsAgl();
                viewerEdge.Edge.Attr.Color = Styles.RELATION_LINE_COLOR.ToMsAgl();
                Model.Viewer.AddEdge(viewerEdge, false);
                Model.Viewer.SetEdgeLabel(viewerEdge.Edge, label);
            }
            else
            {
                Model.Viewer.AddEdge(viewerEdge, false);
            }

            Model.AutoResize();
        }

        internal void DeleteEdge()
        {
            if (Model == null || viewerEdge == null)
                return;

            Model.Viewer.RemoveEdge(viewerEdge, false);
            Model.GraphEditor.Graph.RemoveEdge(viewerEdge.Edge);
            viewerEdge = null;

            Model.AutoResize();
        }

        internal void RecreateEdge()
        {
            DeleteEdge();
            CreateEdge();
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
