using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41
{
    //public class Bookmark
    //{
    //    private Bookmark(string[] values)
    //    {
    //        Values = values;
    //    }

    //    public string[] Values { get; private set; }

    //    internal static string ToTokenInternal(Bookmark consistency)
    //    {
    //        if (consistency is not Bookmark bookmark)
    //            return string.Empty;

    //        return string.Join("|", bookmark.Values);
    //    }
    //    internal static Bookmark FromTokenInternal(string consistencyToken)
    //    {
    //        if (string.IsNullOrEmpty(consistencyToken))
    //            return NullBookmark;

    //        string[] values = consistencyToken.Split(new[] { '|' });

    //        return new Bookmark(values);
    //    }

    //    public string[] ToToken() => Values;
    //    public static Bookmark FromToken(string[] consistencyToken) => new Bookmark(consistencyToken);

    //    internal static readonly Bookmark NullBookmark = new Bookmark(new string[0]);
    //}
}
