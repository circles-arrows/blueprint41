using System;
using System.Collections.Generic;
using System.Text;

using driver = Blueprint41.Driver.Driver;

namespace Blueprint41
{
    public  class Bookmarks
    {
        internal Bookmarks(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public string[] Values => driver.BOOKMARKS.Values(_instance);
        public static Bookmarks From(string[] bookmarks) => new Bookmarks(driver.BOOKMARKS.From(bookmarks));

        public override bool Equals(object? obj) => driver.BOOKMARKS.Equals(_instance, obj);
        public override int GetHashCode() => driver.BOOKMARKS.GetHashCode(_instance);
    }
}
