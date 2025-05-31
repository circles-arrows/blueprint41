using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blueprint41.Events;

namespace Blueprint41.Core
{
#nullable disable
    public class RuntimeRegistered<T>
    {
        internal RuntimeRegistered()
        {
            Blocking = default(T);
            Async = default(T);
        }
        internal RuntimeRegistered(T blocking, T async)
        {
            Blocking = blocking;
            Async = async;
        }

        public virtual T Blocking { get; internal set; }
        public virtual T Async { get; internal set; }

        public T Get(EntityFlavor flavor) => flavor switch
        {
            EntityFlavor.Blocking => Blocking,
            EntityFlavor.Async => Async,
            _ => throw new NotSupportedException(),
        };
        internal void Set(EntityFlavor flavor, T type)
        {
            switch (flavor)
            {
                case EntityFlavor.Blocking:
                    Blocking = type;
                    break;
                case EntityFlavor.Async:
                    Async = type;
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
#nullable enable
    }
    public class RuntimeReturnTypes : RuntimeRegistered<Type?>
    {
        internal RuntimeReturnTypes()
        {
            _returnTypes = null;
        }
        internal RuntimeReturnTypes(RuntimeReturnTypes returnTypes)
        {
            _returnTypes = returnTypes;
        }

#pragma warning disable CS8764
        public override Type? Blocking
        {
            get
            {
                if (_returnTypes?.Blocking is not null && base.Blocking is null)
                {
                    lock (this)
                    {
                        if (base.Blocking is null)
                            base.Blocking = typeof(EntityEventArgs<>).MakeGenericType(_returnTypes.Blocking);
                    }
                }
                return base.Blocking;
            }
        }
        public override Type? Async
        {
            get
            {
                if (_returnTypes?.Async is not null && base.Async is null)
                {
                    lock (this)
                    {
                        if (base.Async is null)
                            base.Async = typeof(EntityEventArgs<>).MakeGenericType(_returnTypes.Async);
                    }
                }
                return base.Async;
            }
        }

        private readonly RuntimeReturnTypes? _returnTypes;
    }
}
