using Blueprint41.Async.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Async
{
    public class Bookmark
    {
        public string ToToken() => PersistenceProvider.CurrentPersistenceProvider.ToToken(this);
        public static Bookmark FromToken(string consistencyToken) => PersistenceProvider.CurrentPersistenceProvider.FromToken(consistencyToken);
        
        internal static readonly Bookmark NullBookmark = new Bookmark();
    }
}
