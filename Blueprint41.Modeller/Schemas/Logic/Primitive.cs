using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller.Schemas
{
    public partial class Primitive
    {
        protected override void InitializeLogic()
        {
            // Set Guid Id
            if (string.IsNullOrEmpty(this.Guid))
                this.Guid = System.Guid.NewGuid().ToString();

            OnNameChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                if (e.OldValue != e.NewValue)
                {
                    Guid = Model.GenerateGuid(e.NewValue).ToString();
                    Name = e.NewValue;
                }
            };
        }
    }
}
