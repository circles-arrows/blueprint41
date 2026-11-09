using System;
using System.Collections.Generic;
using System.Linq;

using Blueprint41.Core;

/********************************************************************************************************
 ********************************************************************************************************
 ********************************************************************************************************
 ********                                                                                        ********
 ********   There is a lot of mess going on in this file:                                        ********
 ********   #pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider...   ********
 ********                                                                                        ********
 ********   We should change the "CreateInstance" methods to call the real constructor via a     ********
 ********   delegate pulled out of an dictionary/cache.                                          ********
 ********                                                                                        ********
 ********   https://stackoverflow.com/questions/2051359/using-a-delegate-to-call-a-constructor   ********
 ********                                                                                        ********
 ********************************************************************************************************
 ********************************************************************************************************
 ********************************************************************************************************/

namespace Blueprint41.Events
{
    public sealed class TransactionEventArgs
    {
        private TransactionEventArgs(EventTypeEnum eventType, Transaction sender)
        {
            Transaction = sender;
            EventType = eventType;
        }

        public Transaction? Transaction { get; private set; }
        public EventTypeEnum EventType { get; private set; }

        internal static TransactionEventArgs CreateInstance(EventTypeEnum eventType, Transaction trans) => new TransactionEventArgs(eventType, trans);

        public Type SenderType => typeof(Transaction);
    }
    public abstract class EntityEventArgs
    {
        protected EntityEventArgs(OgmClass sender)
        {
            Entity = sender?.GetEntity()!;
            Transaction = sender?.Transaction;

            Canceled = false;
            Flushed = false;
        }

        protected abstract OgmClass SenderInternalBridge { get; set; }
        public OGM Sender { get { return SenderInternalBridge; } }

        public bool IsInsert 
        { 
            get 
            {
                if (Sender?.PersistenceState == PersistenceState.NewAndChanged)
                    return true;
                else if (Sender?.PersistenceState == PersistenceState.Persisted && Sender?.OriginalPersistenceState == PersistenceState.New)
                    return true;
                return false;
            } 
        }
        public bool IsUpdate
        {
            get
            {
                if (Sender?.PersistenceState == PersistenceState.LoadedAndChanged)
                    return true;
                else if (Sender?.PersistenceState == PersistenceState.Persisted && Sender?.OriginalPersistenceState == PersistenceState.Loaded)
                    return true;
                return false;
            }
        }
        public bool IsDelete { get { return Sender?.PersistenceState == PersistenceState.Delete || Sender?.PersistenceState == PersistenceState.ForceDelete; } }

        public Entity Entity { get; protected set; }

        public Transaction? Transaction { get; protected set; }
        public EventTypeEnum EventType { get; protected set; }

        public IDictionary<string, object?> CustomState { get { return SenderInternalBridge.CustomState; } }

        internal static EntityEventArgs CreateInstance(EventTypeEnum eventType, OgmClass sender, Transaction trans, bool locked = false)
        {
            Type? type = sender.GetEntity().EntityEventArgsType.Get(sender.Flavor);
            if (type is null)
                throw new NotSupportedException($"{sender.Flavor} code not generated.");

            EntityEventArgs args = (EntityEventArgs)Activator.CreateInstance(type, true)!;
            args.EventType = eventType;
            args.SenderInternalBridge = sender;
            args.Entity = sender.GetEntity();
            args.Transaction = trans;
            args.Locked = locked;

            sender.AppendEventHistory(args);

            return args;
        }

        internal void Lock()
        {
            Locked = true;
        }
        private bool Locked = false;
        public bool Canceled { get; private set; }
        public void CancelEvent()
        {
            if (Locked)
                throw new InvalidOperationException("You can only set the AssignedValue during the event.");

            Canceled = true;
        }
        public bool Flushed { get; private set; }
        internal void Flush()
        {
            Flushed = true;
        }

        public abstract Type SenderType { get; }
    }
    public sealed class EntityEventArgs<TSender> : EntityEventArgs
        where TSender : OGM
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        private EntityEventArgs() : base(null!) { }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public EntityEventArgs(OgmClass sender) : base(sender)
        {
            Sender = (TSender)(object)sender;
        }

        protected sealed override OgmClass SenderInternalBridge
        {
            get { return (OgmClass)(object)Sender; }
            set { Sender = (TSender)(value as OGM)!; }
        }
        new public TSender Sender { get; private set; }
        public sealed override Type SenderType { get { return typeof(TSender); } }
    }
}
