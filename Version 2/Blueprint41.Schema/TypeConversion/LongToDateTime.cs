using System;
using System.Collections.Generic;

using Blueprint41.Core;

namespace Blueprint41.TypeConversion
{
    internal class LongToDateTime : Conversion<long, DateTime>
    {
        protected override DateTime Converter(long value) => FixDateTime(FromTimeInMS(value));
    }
}
