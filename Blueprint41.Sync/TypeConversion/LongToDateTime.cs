using Blueprint41.Sync.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Sync.TypeConversion
{
    internal class LongToDateTime : Conversion<long, DateTime>
    {
        protected override DateTime Converter(long value) => FixDateTime(FromTimeInMS(value));
    }
}
