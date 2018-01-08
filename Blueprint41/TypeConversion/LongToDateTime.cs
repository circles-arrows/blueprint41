using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.TypeConversion
{
    internal class LongToDateTime : Conversion<long, DateTime>
    {
        private const long MaxDateTimeInMS = 253402300800000;
        private const long MinDateTimeInMS = -62135596800000;

        protected override DateTime Converter(long value)
        {
            long ticks = (value >= MaxDateTimeInMS) ? (MaxDateTimeInMS * 10000L) - 1 : value * 10000L;
            ticks = (value <= MinDateTimeInMS) ? unchecked((MinDateTimeInMS * 10000L)) : ticks;
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Add(new TimeSpan(ticks)).ToUniversalTime();
        }
    }
}
