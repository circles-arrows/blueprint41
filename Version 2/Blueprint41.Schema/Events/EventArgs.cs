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
        protected EntityEventArgs(OGMImpl sender)
        {
            Entity = sender?.GetEntity()!;
            Transaction = sender?.Transaction;

            Canceled = false;
            Flushed = false;
        }

        protected abstract OGMImpl SenderInternalBridge { get; set; }
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

        internal static EntityEventArgs CreateInstance(EventTypeEnum eventType, OGMImpl sender, Transaction trans, bool locked = false)
        {
            Type type = sender.GetEntity().EntityEventArgsType;

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
        public EntityEventArgs(OGMImpl sender) : base(sender)
        {
            Sender = (TSender)(object)sender;
        }

        protected sealed override OGMImpl SenderInternalBridge
        {
            get { return (OGMImpl)(object)Sender; }
            set { Sender = (TSender)(value as OGM)!; }
        }
        new public TSender Sender { get; private set; }
        public sealed override Type SenderType { get { return typeof(TSender); } }
    }

    public abstract class PropertyEventArgs : EntityEventArgs
    {
        protected PropertyEventArgs(OGMImpl sender, Property property, OperationEnum operation, object? previousValue, object? assignedValue) : base(sender)
        {
            Property = property;
            Operation = operation;
            PreviousValueInternalBridge = previousValue;
            AssignedValueInternalBridge = assignedValue;
        }

        public Property Property { get; protected set; }
        public OperationEnum Operation { get; protected set; }

        protected virtual object? PreviousValueInternalBridge { get; set; }
        public object? PreviousValue { get { return PreviousValueInternalBridge; } }

        protected virtual object? AssignedValueInternalBridge { get; set; }
        public object? AssignedValue { get { return AssignedValueInternalBridge; } }

        public DateTime Moment { get; private set; }

        internal static PropertyEventArgs CreateInstance(EventTypeEnum eventType, OGMImpl sender, Property property, object? previousValue, object? assignedValue, DateTime moment, OperationEnum operation, Transaction trans)
        {
            Type senderType = sender.GetType();
            Type argsType = property.GetPropertyEventArgsType(senderType);

            PropertyEventArgs args = (PropertyEventArgs)Activator.CreateInstance(argsType, true)!;
            args.EventType = eventType;
            args.SenderInternalBridge = sender;
            args.Entity = sender.GetEntity();
            args.Property = property;
            args.Operation = operation;
            args.PreviousValueInternalBridge = previousValue;
            args.AssignedValueInternalBridge = assignedValue;
            args.Moment = moment;
            args.Transaction = trans;

            sender.AppendEventHistory(args);

            return args;
        }

        public virtual PropertyEventArgs<TSender, TReturnType> As<TSender, TReturnType>()
             where TSender : OGMImpl
        {
            if (Sender is null || !Sender.GetType().IsSubclassOfOrSelf(typeof(TSender)))
                throw new InvalidCastException(string.Format("The event sender (type={0}) cannot be cast to generic parmameter TSender (type={1})", Sender?.GetType().Name ?? "Unknown", typeof(TSender).Name));

            if (typeof(TReturnType) != (Property.SystemReturnType ?? Property.EntityReturnType?.RuntimeReturnType))
                throw new InvalidCastException(string.Format("The property return value (type={0}) cannot be cast to generic parmameter TReturnType (type={1})", (Property.SystemReturnType ?? Property.EntityReturnType?.RuntimeReturnType)?.Name ?? "Unknown", typeof(TSender).Name));

            return (PropertyEventArgs<TSender, TReturnType>)this;
        }
        public abstract Type ReturnType { get; }
    }
    public abstract class PropertyEventArgs<TSender> : PropertyEventArgs
        where TSender : OGM
    {
        protected PropertyEventArgs(OGMImpl sender, Property property, OperationEnum operation, object? previousValue, object? assignedValue) : base(sender, property, operation, previousValue, assignedValue)
        {
            Sender = (TSender)(object)sender;
        }

        protected sealed override OGMImpl SenderInternalBridge
        {
            get { return (OGMImpl)(object)Sender; }
            set { Sender = (TSender)(value as OGM); }
        }
        new public TSender Sender { get; private set; }

#pragma warning disable CS0809
        [Obsolete("You should use the method 'As<TReturnType>()' instead.", true)]
        public override PropertyEventArgs<T, TReturnType> As<T, TReturnType>() { return base.As<T, TReturnType>(); }
#pragma warning restore CS0809

        public virtual PropertyEventArgs<TSender, TReturnType> As<TReturnType>()
        {
            if (typeof(TReturnType) != (Property.SystemReturnType ?? Property.EntityReturnType?.RuntimeReturnType))
                throw new InvalidCastException(string.Format("The property return value (type={0}) cannot be cast to generic parmameter TReturnType (type={1})", (Property.SystemReturnType ?? Property.EntityReturnType?.RuntimeReturnType)?.Name ?? "Unknown", typeof(TSender).Name));

            return (PropertyEventArgs<TSender, TReturnType>)this;
        }
        public sealed override Type SenderType { get { return typeof(TSender); } }

    }
    public sealed class PropertyEventArgs<TSender, TReturnType> : PropertyEventArgs<TSender>
        where TSender : OGM
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        private PropertyEventArgs() : base(null!, null!, OperationEnum.Set, default(TReturnType)!, default(TReturnType)!) { }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public PropertyEventArgs(OGMImpl sender, Property property, OperationEnum operation, object? previousValue, object? assignedValue) : base(sender, property, operation, previousValue, assignedValue)
        {
            PreviousValue = (TReturnType)previousValue!;
            AssignedValue = (TReturnType)assignedValue!;
        }

        protected sealed override object? PreviousValueInternalBridge
        {
            get { return PreviousValue; }
            set { PreviousValue = (TReturnType)value!; }
        }
        new public TReturnType PreviousValue { get; private set; }

        protected sealed override object? AssignedValueInternalBridge
        {
            get { return AssignedValue; }
            set { AssignedValue = (TReturnType)value!; }
        }
        new public TReturnType AssignedValue { get; private set; }

#pragma warning disable CS0809
        [Obsolete("You don't need to use the method 'As<TSender, TReturnType>()' to get the subclass, this already is the subclass.", true)]
        public sealed override PropertyEventArgs<T, TReturn> As<T, TReturn>() { return base.As<T, TReturn>(); }
        [Obsolete("You don't need to use the method 'As<TReturnType>()' to get the subclass, this already is the subclass.", true)]
        public sealed override PropertyEventArgs<TSender, T> As<T>() { return base.As<T>(); }
#pragma warning restore CS0809

        public sealed override Type ReturnType { get { return typeof(TReturnType); } }
    }

    public struct NodeEventArgs
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal NodeEventArgs(EventTypeEnum eventType, Transaction trans, OGM? sender, string cypher, Dictionary<string, object?>? parameters, IDictionary<string, object?>? customState)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            EventType = eventType;
            Transaction = trans;
            Sender = sender;
            IsBatch = (sender is null);
            ElementId = "";
            Cypher = cypher;
            Parameters = parameters;
            CustomState = customState;
            Labels = new string[0];
        }
        internal NodeEventArgs(EventTypeEnum eventType, NodeEventArgs previous, string elementId = "", IReadOnlyList<string>? labels = null, Dictionary<string, object?>? properties = null)
        {
            EventType = eventType;
            Transaction = previous.Transaction;
            Sender = (eventType == EventTypeEnum.OnBatchFinished) ? null : previous.Sender;
            IsBatch = previous.IsBatch;
            Cypher = previous.Cypher;
            Parameters = previous.Parameters;
            CustomState = previous.CustomState;

            ElementId = elementId;
            Labels = labels ?? new string[0];
            Properties = properties;
        }

        public EventTypeEnum EventType { get; private set; }
        public OGM? Sender { get; internal set; }

        public bool IsBatch { get; private set; }
        public string Cypher { get; set; }
        public Dictionary<string, object?>? Parameters { get; private set; }
        public Transaction Transaction { get; internal set; }

        public string ElementId { get; internal set; }
        public IReadOnlyList<string> Labels { get; internal set; }
        public Dictionary<string, object?>? Properties { get; internal set; }

        public IDictionary<string, object?>? CustomState { get; private set; }
    }
    public struct RelationshipEventArgs
    {
        internal RelationshipEventArgs(EventTypeEnum eventType, Transaction trans)
            : this()
        {
            EventType = eventType;
            Transaction = trans;
        }

        public Transaction? Transaction { get; internal set; }
        public EventTypeEnum EventType { get; private set; }

        #region CustomState

        private Dictionary<string, object?>? customState;
        public IDictionary<string, object?> CustomState
        {
            get
            {
                if (customState is null)
                {
                    lock (typeof(RelationshipEventArgs))
                    {
                        if (customState is null)
                            customState = new Dictionary<string, object?>(32);
                    }
                }
                return customState;
            }
        }

        #endregion
    }
}
