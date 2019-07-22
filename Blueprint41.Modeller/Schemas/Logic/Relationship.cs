using Blueprint41.Modeller.Schemas;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller.Schemas
{
    public partial class Relationship
    {
        public override Modeller Model
        {
            get { return base.Model; }
            internal set
            {
                base.Model = value;

                if (Source != null)
                    Source.Model = value;

                if (Target != null)
                    Target.Model = value;
            }
        }

        protected override void InitializeLogic()
        {
            // Set Guid Id
            if (string.IsNullOrEmpty(this.Guid))
                this.Guid = System.Guid.NewGuid().ToString();

            if (Source != null)
                Source.Parent = this;

            if (Target != null)
                Target.Parent = this;

            OnSourceChanged += delegate (object sender, PropertyChangedEventArgs<NodeReference> e)
            {
                if (e.OldValue != null)
                    e.OldValue.Parent = null;

                if (e.NewValue != null)
                    e.NewValue.Parent = this;
            };

            OnTargetChanged += delegate (object sender, PropertyChangedEventArgs<NodeReference> e)
            {
                if (e.OldValue != null)
                    e.OldValue.Parent = null;

                if (e.NewValue != null)
                    e.NewValue.Parent = this;
            };
            
            OnNameChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                if (e.NewValue != e.OldValue)
                    this.Guid = Model.GenerateGuid(e.NewValue).ToString();

                if (Model.Relationships.Relationship.Any(item => item.Name == e.NewValue && ((object)item) != this))
                {
                    e.Cancel = true;
                    System.Windows.Forms.MessageBox.Show("Relationship name already exist.", "Info", System.Windows.Forms.MessageBoxButtons.OK);
                }
            };
        }

        public override bool Equals(object obj)
        {
            if (obj is Relationship)
            {
                Relationship other = obj as Relationship;
                return this.Name == other.Name && this.Type == other.Type;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
