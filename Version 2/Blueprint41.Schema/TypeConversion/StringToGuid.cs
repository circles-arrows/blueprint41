using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.TypeConversion
{
    internal class StringToGuid : Conversion<string?, Guid>
    {
        protected override Guid Converter(string? value)
        {
            if (value is null)
                return Guid.Empty;

            return Guid.Parse(value);
        }
    }
}
