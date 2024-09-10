using System;
using System.Collections.Generic;
using System.Text;

using Blueprint41.Persistence;

namespace Blueprint41
{
    public  class Bookmarks
    {
        internal Bookmarks(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public string[] Values => Driver.BOOKMARKS.Values(_instance);
        public static Bookmarks From(string[] bookmarks) => new Bookmarks(Driver.BOOKMARKS.From(bookmarks));

        public override bool Equals(object? obj) => Driver.BOOKMARKS.Equals(_instance, obj);
        public override int GetHashCode() => Driver.BOOKMARKS.GetHashCode(_instance);
    }
}
