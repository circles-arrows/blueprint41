using System;
using System.Collections.Generic;

using Blueprint41.Core;

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
