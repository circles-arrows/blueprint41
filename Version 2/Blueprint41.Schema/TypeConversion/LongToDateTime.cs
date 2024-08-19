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
        protected override DateTime Converter(long value) => FixDateTime(FromTimeInMS(value));
    }
}
