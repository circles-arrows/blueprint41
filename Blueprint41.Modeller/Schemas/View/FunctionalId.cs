using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller.Schemas
{
    public partial class FunctionalId
    {
        protected override void InitializeView()
        {
            OnNameChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                if (Model != null)
                    Model.HasChanges = true;
            };

            OnValueChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                if (Model != null)
                    Model.HasChanges = true;
            };

            OnTypeChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                if (Model != null)
                    Model.HasChanges = true;
            };

            OnGuidChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                if (Model != null)
                    Model.HasChanges = true;
            };

            OnIsDefaultChanged += delegate (object sender, PropertyChangedEventArgs<bool> e)
            {
                if (Model != null)
                    Model.HasChanges = true;
            };
        }
    }
}
