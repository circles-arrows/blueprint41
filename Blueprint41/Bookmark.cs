using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41
{
    public class Bookmark
    {
        public string ToToken<TDatastoreModel>()
             where TDatastoreModel : DatastoreModel<TDatastoreModel>, new()
        {
            return DatastoreModel<TDatastoreModel>.CurrentPersistenceProvider.ToToken(this);
        }
        public string ToToken(PersistenceProvider provider) => provider.ToToken(this);
        public static Bookmark FromToken<TDatastoreModel>(string consistencyToken)
             where TDatastoreModel : DatastoreModel<TDatastoreModel>, new()
        {
            return DatastoreModel<TDatastoreModel>.CurrentPersistenceProvider.FromToken(consistencyToken);
        }
        public static Bookmark FromToken(PersistenceProvider provider, string consistencyToken) => provider.FromToken(consistencyToken);
        
        internal static readonly Bookmark NullBookmark = new Bookmark();
    }
}