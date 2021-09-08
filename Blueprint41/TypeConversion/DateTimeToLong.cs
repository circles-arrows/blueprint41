using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.TypeConversion
{
    internal class DateTimeToLong : Conversion<DateTime, long>
    {
        protected override long Converter(DateTime value) => ToTimeInMS(FixDateTime(value));
    }
}
