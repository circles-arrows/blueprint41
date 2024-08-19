using System;
using System.Collections.Generic;

using Blueprint41.Events;

namespace Blueprint41.Core
{
    public abstract class OGMImpl : OGM
    {
        protected const string Param0 = "P0";
        protected const string Param1 = "P1";
        protected const string Param2 = "P2";
        protected const string Param3 = "P3";
        protected const string Param4 = "P4";
        protected const string Param5 = "P5";
        protected const string Param6 = "P6";
        protected const string Param7 = "P7";
        protected const string Param8 = "P8";
        protected const string Param9 = "P9";

        protected OGMImpl(Transaction? transaction)
        {
            if (!GetEntity().Parent.IsUpgraded)
                throw new InvalidOperationException("You cannot use entity inside the upgrade script.");

            Transaction = null;
            if (transaction?.InTransaction ?? false)
            {
                Transaction = transaction;
                transaction.Register(this);
            }
        }

        public abstract PersistenceState OriginalPersistenceState { get; set; }
        public abstract PersistenceState PersistenceState { get; set; }
        public Transaction? Transaction { get; private set; }
        Transaction? OGM.Transaction { get => Transaction; set => Transaction = value; }
        public Transaction RunningTransaction
        {
            get
            {
                Transaction? trans = Transaction;

                if (trans is null)
                    throw new InvalidOperationException("There is no transaction, you should create one first -> using (Transaction.Begin()) { ... Transaction.Commit(); }");

                if (!trans.InTransaction)
                    throw new InvalidOperationException("The transaction was already committed or rolled back.");

                return trans;
            }
        }

        public abstract void Delete(bool force);
        public abstract IDictionary<string, object?> GetData();
        public abstract Entity GetEntity();
        public abstract object? GetKey();
        public abstract DateTime GetRowVersion();
        public abstract void Save();
        public abstract void SetChanged();
        public abstract void SetData(IReadOnlyDictionary<string, object?> data);
        public abstract void SetKey(object key);
        public abstract void SetRowVersion(DateTime? value);
        public abstract void ValidateDelete();
        public abstract void ValidateSave();


        #region Events

        private Dictionary<string, object?>? customState = null;
        internal IDictionary<string, object?> CustomState
        {
            get
            {
                if (customState is null)
                {
                    lock (this)
                    {
                        if (customState is null)
                            customState = new Dictionary<string, object?>();
                    }
                }
                return customState;
            }
        }

        public IReadOnlyList<EntityEventArgs> EventHistory
        {
            get
            {
                return eventHistory;
            }
        }
        internal void AppendEventHistory(EntityEventArgs args)
        {
            eventHistory.Add(args);
        }
        private readonly List<EntityEventArgs> eventHistory = new List<EntityEventArgs>(16);

        #endregion
    }
}
