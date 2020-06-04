using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence.Void;

namespace Blueprint41.TypeConversion
{
    internal class DecimalToLong : Conversion<decimal, long>
    {
        protected override long Converter(decimal value)
        {
           return (long)Math.Round(value * Neo4jPersistenceProvider.DECIMAL_FACTOR, 0);
        }
    }
}
