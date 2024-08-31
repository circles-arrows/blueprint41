﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
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
    }
}