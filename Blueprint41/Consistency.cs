using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Blueprint41.Core;

namespace Blueprint41
{
    public class Consistency : DisposableScope<Consistency>
    {
        protected Consistency(PersistenceProvider provider, Bookmark bookmark) : base()
        {
            Value = bookmark ?? Bookmark.FromToken(provider, string.Empty);
        }

        [Obsolete("Do not use this Start method on a multi-database project")]
        public static Consistency Start(string bookmark) => new Consistency(PersistenceProvider.CurrentPersistenceProvider, Bookmark.FromToken(PersistenceProvider.CurrentPersistenceProvider, bookmark)).Attach();
        public static Consistency Start<TDatastoreModel>(string bookmark)
            where TDatastoreModel : DatastoreModel<TDatastoreModel>, new()
        {
            return new Consistency(DatastoreModel<TDatastoreModel>.CurrentPersistenceProvider, Bookmark.FromToken(DatastoreModel<TDatastoreModel>.CurrentPersistenceProvider, bookmark)).Attach();
        }

        [Obsolete("Do not use this Start method on a multi-database project")]
        public static Consistency Start(Bookmark bookmark) => new Consistency(PersistenceProvider.CurrentPersistenceProvider, bookmark).Attach();
        public static Consistency Start<TDatastoreModel>(Bookmark bookmark)
             where TDatastoreModel : DatastoreModel<TDatastoreModel>, new()
        {
            return new Consistency(DatastoreModel<TDatastoreModel>.CurrentPersistenceProvider, bookmark).Attach();
        }

        private Bookmark Value;

        protected virtual void OnBookmarkChange(Bookmark bookmark) { }

        public static void Register()
        {
            Transaction.OnBegin -= OnBeginHandler;
            Transaction.OnBegin += OnBeginHandler;
        }
        private static void OnBeginHandler(object sender, TransactionEventArgs e)
        {
            if (e.Transaction is not null)
            {
                if (Current is not null)
                    e.Transaction.WithConsistency(Current.Value);

                e.Transaction.OnCommit += OnCommitHandler;
            }
        }
        private static void OnCommitHandler(object sender, TransactionEventArgs e)
        {
            if (e.Transaction is not null)
            {
                if (Current is not null)
                {
                    Bookmark bookmark = e.Transaction.GetConsistency();
                    Current.Value = bookmark;
                    Current.OnBookmarkChange(bookmark);
                }

                e.Transaction.OnCommit -= OnCommitHandler;
            }
        }
    }
}
