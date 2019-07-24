using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller.Schemas
{
    public partial class Record
    {
        protected override void InitializeView()
        {
            OnGuidChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                if (Model != null)
                    Model.HasChanges = true;
            };

            OnMappingGuidChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                if (Model != null)
                    Model.HasChanges = true;
            };
        }
    }
}
