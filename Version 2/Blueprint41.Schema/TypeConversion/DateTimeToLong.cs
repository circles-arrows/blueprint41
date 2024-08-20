using System;
using System.Collections.Generic;

using Blueprint41.Core;

namespace Blueprint41.TypeConversion
{
    internal class DateTimeToLong : Conversion<DateTime, long>
    {
        protected override long Converter(DateTime value) => ToTimeInMS(FixDateTime(value));
    }
}
