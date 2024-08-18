using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Blueprint41.Async.Core;

namespace Blueprint41.Async
{
    public class Consistency : DisposableScope<Consistency>
    {
        protected Consistency(Bookmark bookmark) : base()
        {
            Value = bookmark ?? Bookmark.FromToken(string.Empty);
        }

        public static Consistency Start(string bookmark) => new Consistency(Bookmark.FromToken(bookmark)).Attach();
        public static Consistency Start(Bookmark bookmark) => new Consistency(bookmark).Attach();

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
