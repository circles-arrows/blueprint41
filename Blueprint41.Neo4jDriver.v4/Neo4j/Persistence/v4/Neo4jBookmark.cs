using System;
using System.Collections.Generic;
using System.Text;

using neo4j = Neo4j.Driver;

namespace Blueprint41.Neo4j.Persistence.Driver.v4
{
    internal class Neo4jBookmark : Bookmark
    {
        internal Neo4jBookmark(string[] values)
        {
            Values = values;
        }

        public string[] Values { get; private set; }

        internal neo4j.Bookmark ToBookmark() => neo4j.Bookmark.From(Values);

        internal static string ToTokenInternal(Bookmark consistency)
        {
            Neo4jBookmark? bookmark = consistency as Neo4jBookmark;
            if (bookmark == null)
                return string.Empty;

            return string.Join("|", bookmark.Values);
        }
        internal static Bookmark FromTokenInternal(string consistencyToken)
        {
            if (string.IsNullOrEmpty(consistencyToken))
                return NullBookmark;

            string[] values = consistencyToken.Split(new[] { '|' });

            return new Neo4jBookmark(values);
        }
    }
}
