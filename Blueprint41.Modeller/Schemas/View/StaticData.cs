using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller.Schemas
{
    public partial class StaticData
    {
        protected override void InitializeView()
        {
            OnRecordsChanged += delegate (object sender, PropertyChangedEventArgs<Records> e)
            {
                Model.HasChanges = true;
            };
        }
    }
}
