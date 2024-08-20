using System;
using System.Collections.Generic;

using Blueprint41.Core;

namespace Blueprint41.TypeConversion
{
    internal class GuidToString : Conversion<Guid, string?>
    {
        protected override string? Converter(Guid value)
        {
            return value.ToString("B");
        }
    }
}
