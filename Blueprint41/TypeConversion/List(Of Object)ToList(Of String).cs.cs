using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.TypeConversion
{
    internal class ListOfObjectToListOfString : Conversion<List<object>, List<string>>
    {
        protected override List<string> Converter(List<object> value)
        {
            return value.Cast<string>().ToList();
        }
    }
}
