using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.TypeConversion
{
    internal class DecimalToLong : Conversion<decimal, long>
    {
        protected override long Converter(decimal value)
        {
           return (long)Math.Round(value * Neo4JPersistenceProvider.DECIMAL_FACTOR, 0);
        }
    }
}
