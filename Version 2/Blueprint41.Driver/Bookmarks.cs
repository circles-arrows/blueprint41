using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public  class Bookmarks
    {
        internal Bookmarks(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }

        public string[] Values => Driver.BOOKMARKS.Values(Value);
        public static Bookmarks From(string[] bookmarks) => new Bookmarks(Driver.BOOKMARKS.From(bookmarks));
    }
}
