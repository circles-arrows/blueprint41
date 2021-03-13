using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blueprint41.Core;

namespace Blueprint41.Neo4j.Schema.v4
{
    public class ConstraintInfo_v4 : ConstraintInfo
    {
        internal ConstraintInfo_v4(RawRecord record) : base(record) { }

        internal override Match UniqueMatch(string text)
        {
            return unique.Match(text);
        }
        internal override Match NotNullMatch(string text)
        {
            return notnull.Match(text);
        }

        private static readonly Regex unique = new Regex(@"^ *constraint *on *\( *([a-z0-9_]+): *(?<entity>[a-z0-9_]+) *\) *assert *\( *\1\.(?<field>[a-z0-9_]+) *\) *is *unique *$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);
        private static readonly Regex notnull = new Regex(@"^ *constraint *on *\( *([a-z0-9_]+): *(?<entity>[a-z0-9_]+) *\) *assert *exists *\( *\1\.(?<field>[a-z0-9_]+) *\) *$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);
    }
}