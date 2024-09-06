using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    public abstract class DisposableScope<T> : IDisposable, IAsyncDisposable
        where T : DisposableScope<T>
    {
        protected T Attach()
        {
            isDisposed = true;
            isInitialized = false;

            Initialize();
            isInitialized = true;

            if (current is null)
                current = new AsyncLocal<Stack<T>?>();

            if (current.Value is null)
                current.Value = new Stack<T>();

            current.Value.Push((T)this);

            isDisposed = false;

            return (T)this;
        }


        public static T? Current
        {
            get
            {
                if (current?.Value is null || current.Value.Count == 0)
                    return null;

                return current.Value.Peek();
            }
        }

        private static AsyncLocal<Stack<T>?> current = new AsyncLocal<Stack<T>?>();

        private bool isInitialized;
        private bool isDisposed;

        public void Dispose()
        {
            if (!isDisposed)
            {
                try
                {
                    if (isInitialized)
                        Cleanup();
                }
                finally
                {
                    if (current.Value is not null)
                    {
                        if (current.Value.Count > 0)
                            current.Value.Pop();

                        if (current.Value.Count == 0)
                            current.Value = null;
                    }

                    if (!IsDebug.Value)
                        GC.SuppressFinalize(this);

                    isDisposed = true;
                }
            }
        }
        public ValueTask DisposeAsync()
        {
            Dispose();
            return default;
        }

        protected virtual void Initialize() { }
        protected virtual void Cleanup() { }

        ~DisposableScope()
        {
            if (!isDisposed)
            {
                if (IsDebug.Value)
                    throw new InvalidOperationException($"The {GetType().Name} should be used with the using command: using ({GetType().Name}.Begin()) {{ ... }}");
                else
                    Dispose();
            }
        }
        private static readonly Lazy<bool> IsDebug = new Lazy<bool>(() => System.Diagnostics.Debugger.IsAttached, true);
    }
}
