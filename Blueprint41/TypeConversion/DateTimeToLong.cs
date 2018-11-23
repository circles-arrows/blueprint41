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
        protected override long Converter(DateTime value)
        {
            //if date is not utc convert to utc.
            DateTime utcTime = value.Kind != DateTimeKind.Utc ? value.ToUniversalTime() : value;

            //return value.Ticks;
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)utcTime.Subtract(dt).TotalMilliseconds;
        }
    }
}
