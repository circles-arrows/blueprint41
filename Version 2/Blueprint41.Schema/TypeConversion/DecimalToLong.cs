using System;
using System.Collections.Generic;
using Blueprint41.Core;
using Blueprint41.Persistence.Provider;

namespace Blueprint41.TypeConversion
{
    internal class DecimalToLong : Conversion<decimal, long>
    {
        protected override long Converter(decimal value)
        {
           return (long)Math.Round(value * PersistenceProvider.DECIMAL_FACTOR, 0);
        }
    }
}
