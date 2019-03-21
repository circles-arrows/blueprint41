using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller.Schemas
{
    public partial class Property
    {
        protected override void InitializeView()
        {
            OnPropertyGuidChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                Model.HasChanges = true;
            };

            OnMappingGuidChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                Model.HasChanges = true;
            };

            OnValueChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                Model.HasChanges = true;
            };
        }
    }
}
