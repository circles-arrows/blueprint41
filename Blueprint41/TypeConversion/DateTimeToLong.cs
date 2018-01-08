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
            //return value.Ticks;
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)value.Subtract(dt).TotalMilliseconds;
        }
    }
}
