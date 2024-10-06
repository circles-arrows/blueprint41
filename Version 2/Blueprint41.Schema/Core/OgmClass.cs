using System;
using System.Collections.Generic;

using Blueprint41.Events;
using Blueprint41.Persistence;

namespace Blueprint41.Core
{
    public abstract class OgmClass : OGM
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

        protected OgmClass()
        {
            Transaction? transaction = Transaction.Current;

            if (!GetEntity().Parent.IsUpgraded)
                throw new InvalidOperationException("You cannot use entity inside the upgrade script.");

            if (transaction?.InTransaction ?? false)
            {
                Transaction = transaction;
                transaction.Register(this);
            }
        }
        protected OgmClass(Transaction? transaction)
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

        public abstract PersistenceProvider PersistenceProvider { get; set; }

        public abstract PersistenceState OriginalPersistenceState { get; set; }
        public abstract PersistenceState PersistenceState { get; set; }
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

        protected internal Transaction? Transaction { get; private set; }
        protected internal abstract Entity GetEntity();
        protected abstract IDictionary<string, object?> GetData();
        protected abstract object? GetKey();
        protected abstract DateTime GetRowVersion();
        protected abstract void SetChanged();
        protected abstract void SetData(IReadOnlyDictionary<string, object?> data);
        protected internal virtual void SetKey(object key) => throw new NotImplementedException();
        protected abstract void SetRowVersion(DateTime? value);
        protected abstract void ValidateDelete();
        protected abstract void ValidateSave();

        public abstract void Save();
        public abstract void Delete(bool force);

        #region OGM

        Transaction? OGM.Transaction { get => Transaction; set => Transaction = value; }
        object? OGM.GetKey() => GetKey();
        void OGM.SetKey(object key) => SetKey(key);
        DateTime OGM.GetRowVersion() => GetRowVersion();
        void OGM.SetRowVersion(DateTime? value) => SetRowVersion(value);
        IDictionary<string, object?> OGM.GetData() => GetData();
        void OGM.SetData(IReadOnlyDictionary<string, object?> data) => SetData(data);
        void OGM.ValidateSave() => ValidateSave();
        void OGM.ValidateDelete() => ValidateDelete();
        Entity OGM.GetEntity() => GetEntity();
        void OGM.SetChanged() => SetChanged();

        #endregion

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
