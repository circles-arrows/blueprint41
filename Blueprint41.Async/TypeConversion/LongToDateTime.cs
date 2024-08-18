using Blueprint41.Async.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Async.TypeConversion
{
    internal class LongToDateTime : Conversion<long, DateTime>
    {
        protected override DateTime Converter(long value) => FixDateTime(FromTimeInMS(value));
    }
}
