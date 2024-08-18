using Blueprint41.Sync.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Sync.TypeConversion
{
    internal class GuidToString : Conversion<Guid, string?>
    {
        protected override string? Converter(Guid value)
        {
            return value.ToString("B");
        }
    }
}
