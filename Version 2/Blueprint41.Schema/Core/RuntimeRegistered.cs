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
        }
        internal RuntimeRegistered(T blocking, T async)
        {
            _blockingValue = blocking;
            _blockingSet = true;

            _asyncValue = async;
            _asyncSet = true;
        }
        internal RuntimeRegistered(Func<EntityFlavor, T> valueFactory)
        {
            _factory = valueFactory;
        }

        #region Blocking

        public T Blocking
        {
            get
            {
                if (!_blockingSet && _factory is not null)
                {
                    lock (this)
                    {
                        if (!_blockingSet)
                        {
                            _blockingValue = _factory.Invoke(EntityFlavor.Blocking);
                            _blockingSet = true;
                        }
                    }
                }
                return _blockingValue;
            }
            set
            {
                if (_factory is not null)
                    throw new InvalidOperationException("You cannot set a value when a factory has been provided.");

                _blockingValue = value;
                _blockingSet = true;
            }
        }
        public bool IsBlockingSet => _blockingSet;

        private bool _blockingSet = false;
        private T _blockingValue = default;

        #endregion

        #region Async

        public T Async
        {
            get
            {
                if (!_asyncSet && _factory is not null)
                {
                    lock (this)
                    {
                        if (!_asyncSet)
                        {
                            _asyncValue = _factory.Invoke(EntityFlavor.Async);
                            _asyncSet = true;
                        }
                    }
                }
                return _asyncValue;
            }
            set
            {
                if (_factory is not null)
                    throw new InvalidOperationException("You cannot set a value when a factory has been provided.");

                _asyncValue = value;
                _asyncSet = true;
            }
        }
        public bool IsAsyncSet => _asyncSet;

        private bool _asyncSet = false;
        private T _asyncValue = default;

        #endregion

        public T Get(EntityFlavor flavor) => flavor switch
        {
            EntityFlavor.Blocking => Blocking,
            EntityFlavor.Async    => Async,
            _                     => throw new NotSupportedException(),
        };
        public bool IsSet(EntityFlavor flavor) => flavor switch
        {
            EntityFlavor.Blocking => IsBlockingSet,
            EntityFlavor.Async => IsAsyncSet,
            _ => throw new NotSupportedException(),
        };
        internal void Set(EntityFlavor flavor, T value)
        {
            switch (flavor)
            {
                case EntityFlavor.Blocking:
                    _blockingValue = value;
                    _blockingSet = true;
                    break;
                case EntityFlavor.Async:
                    _asyncValue = value;
                    _asyncSet = true;
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        internal T GetOrSet(EntityFlavor flavor, Func<EntityFlavor, T> valueFactory)
        {
            _factory = valueFactory;
            return Get(flavor);
        }

        private Func<EntityFlavor, T> _factory = null;

#nullable enable
    }
}
