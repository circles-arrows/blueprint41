using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence.Void;

namespace Blueprint41.TypeConversion
{
    internal class LongToDecimal : Conversion<long, decimal>
    {
        protected override decimal Converter(long value)
        {
            return ((decimal)value) / Neo4jPersistenceProvider.DECIMAL_FACTOR;
        }
    }
}
