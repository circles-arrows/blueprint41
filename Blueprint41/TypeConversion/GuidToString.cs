using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.TypeConversion
{
    internal class GuidToString : Conversion<Guid, string>
    {
        protected override string Converter(Guid value)
        {
            return value.ToString("B");
        }
    }
}
