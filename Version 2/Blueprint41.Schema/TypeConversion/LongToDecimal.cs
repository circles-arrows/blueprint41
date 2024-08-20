using System;
using System.Collections.Generic;

using Blueprint41.Core;

namespace Blueprint41.TypeConversion
{
    internal class LongToDecimal : Conversion<long, decimal>
    {
        protected override decimal Converter(long value)
        {
            return ((decimal)value) / PersistenceProvider.DECIMAL_FACTOR;
        }
    }
}
