using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    public abstract class EntityEventArgs
    {
        protected EntityEventArgs(OGMImpl sender)
        {
            SenderInternalBridge = sender;
            Entity = sender.GetEntity();
            Transaction = sender.DbTransaction;

            Canceled = false;
            Flushed = false;
        }

        protected virtual OGMImpl SenderInternalBridge { get; set; }
        public OGM Sender { get { return SenderInternalBridge; } }

        public bool IsInsert { get { return Sender?.PersistenceState == PersistenceState.NewAndChanged; } }
        public bool IsUpdate { get { return Sender?.PersistenceState == PersistenceState.LoadedAndChanged; } }
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
        internal NodeEventArgs(EventTypeEnum eventType, Transaction trans, OGM? sender, string cypher, Dictionary<string, object?>? parameters, IDictionary<string, object?>? customState)
            :this()
        {
            EventType = eventType;
            Transaction = trans;
            Sender = sender;
            IsBatch = (sender == null);
            Id = 0;
            Cypher = cypher;
            Parameters = parameters;
            CustomState = customState;
        }
        internal NodeEventArgs(EventTypeEnum eventType, NodeEventArgs previous, long id = 0, IReadOnlyList<string>? labels = null, Dictionary<string, object?>? properties = null)
            : this()
        {
            EventType = eventType;
            Transaction = previous.Transaction;
            Sender = (eventType == EventTypeEnum.OnBatchFinished) ? null : previous.Sender;
            IsBatch = previous.IsBatch;
            Cypher = previous.Cypher;
            Parameters = previous.Parameters;
            CustomState = previous.CustomState;

            Id = 0;
            Labels = labels ?? new string[0];
            Properties = properties;
        }

        public EventTypeEnum EventType { get; private set; }
        public OGM? Sender { get; internal set; }

        public bool IsBatch { get; private set; }
        public string Cypher { get; set; }
        public Dictionary<string, object?>? Parameters { get; private set; }
        public Transaction Transaction { get; internal set; }

        public long Id { get; internal set; }
        public IReadOnlyList<string> Labels { get; internal set; }
        public Dictionary<string, object?>? Properties { get; internal set; }

        public IDictionary<string, object?>? CustomState { get; private set; }
    }
    public struct RelationshipEventArgs
    {
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

    public enum EventTypeEnum
    {
        OnNew,
        OnPropertyChange,
        OnSave,
        OnDelete,
        OnNodeLoading,
        OnNodeLoaded,
        OnBatchFinished,
        OnNodeCreate,
        OnNodeCreated,
        OnNodeUpdate,
        OnNodeUpdated,
        OnNodeDelete,
        OnNodeDeleted,
    }
    public enum OperationEnum
    {
        Set,
        Add,
        Remove,
    }
}
