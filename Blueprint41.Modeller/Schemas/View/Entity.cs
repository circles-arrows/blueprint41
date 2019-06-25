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
    public partial class Entity
    {

        protected override void InitializeView()
        {
            OnNameChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                Model.HasChanges = true;

                if (Model.ModellerType == ModellerType.Neo4j && e.NewValue != Label)
                    Label = e.NewValue;
            };

            OnLabelChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                Model.HasChanges = true;

                if (Model.ModellerType == ModellerType.Neo4j && e.NewValue != Name)
                    Name = e.NewValue;
            };

            OnAbstractChanged += delegate (object sender, PropertyChangedEventArgs<bool> e)
            {
                Model.HasChanges = true;
            };

            OnVirtualChanged += delegate (object sender, PropertyChangedEventArgs<bool> e)
            {
                Model.HasChanges = true;
            };

            OnSummaryChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                Model.HasChanges = true;
            };

            OnExampleChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                Model.HasChanges = true;
            };

            OnInheritsChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                Model.HasChanges = true;
            };

            OnPrefixChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                Model.HasChanges = true;
            };

            OnIsStaticDataChanged += delegate (object sender, PropertyChangedEventArgs<bool> e)
            {
                Model.HasChanges = true;
            };

            OnFunctionalIdChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                Model.HasChanges = true;
            };

            OnGuidChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                Model.HasChanges = true;
            };

            OnMappingGuidChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                Model.HasChanges = true;
            };
        }
    }
}
