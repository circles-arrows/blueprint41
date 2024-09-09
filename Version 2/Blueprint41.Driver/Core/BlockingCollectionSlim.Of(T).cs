using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Blueprint41.Core
{
    internal class BlockingCollectionSlim<T> : IDisposable, IEnumerable<T>
    {
        private readonly ConcurrentQueue<T> queue = new ConcurrentQueue<T>();
        private readonly AutoResetEvent itemAdded = new AutoResetEvent(false);
        private readonly ManualResetEventSlim finished = new ManualResetEventSlim(false);

        public void Add(T item)
        {
            if (isCompleted)
                throw new InvalidOperationException("CompleteAdding was already called.");

            queue.Enqueue(item);
            itemAdded.Set();
        }
        public void CompleteAdding()
        {
            if (isCompleted || IsReadOnly)
                throw new InvalidOperationException("CompleteAdding was already called.");

            finished.Set();
            isCompleted = true;
        }
        public bool IsReadOnly => finished.IsSet;

        public int Count => queue.Count;


        private bool isCompleted = false;
        private bool disposedValue;

        public bool TryPeek(out T? result)
        {
            return queue.TryPeek(out result);
        }
        public T Take()
        {
            T? item;

            while (!queue.TryDequeue(out item))
            {
                itemAdded.WaitOne(maxWait);
                if (finished.IsSet)
                {
                    if (queue.TryDequeue(out item)) // if finished is set, nothing will be added anymore. No matter how long we wait...
                        return item;

                    throw new OperationCanceledException("Nothing available to Take and CompleteAdding was called.");
                }
            }

            return item;
        }
        public bool TryTake(out T? item) => TryTake(out item, TimeSpan.Zero);
        public bool TryTake(out T? item, TimeSpan wait)
        {
            if (queue.TryDequeue(out item))
                return true;

            if (wait != TimeSpan.Zero)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                while (stopwatch.Elapsed < wait)
                {
                    if (queue.TryDequeue(out item)) // This might fail when another thread took the item first
                        return true;

                    if (finished.IsSet)
                        return queue.TryDequeue(out item); // if finished is set, nothing will be added anymore. No matter how long we wait...

                    var waitLeft = wait - stopwatch.Elapsed;
                    if (waitLeft <= TimeSpan.Zero)
                    {
                        break; // Stop if no wait time is left
                    }
                    else if (waitLeft < minWait)
                    {
                        waitLeft = minWait; // If the wait is less than 1ms, wait at least 1ms instead. To keep CPU usage from spiking...
                    }
                    else if (waitLeft > maxWait)
                    {
                        waitLeft = maxWait; // Make sure the finish state is checked at least every 100ms
                    }

                    itemAdded.WaitOne(waitLeft); // Wait till another thread adds an item or the wait time has past
                }
            }
            return false;
        }

        public IEnumerable<T> GetConsumingEnumerable() => this;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            bool isFinished = false;
            while (!isFinished)
            {
                if (queue.TryDequeue(out T? item))
                {
                    OnCurrent(item);
                    yield return item;
                }
                else
                {
                    if (finished.IsSet)
                    {
                        // empty queue before exiting
                        while (queue.TryDequeue(out item))
                        {
                            OnCurrent(item);
                            yield return item;
                        }

                        isFinished = true;
                    }
                    else
                    {
                        itemAdded.WaitOne(maxWait);
                        OnIdle(ref isFinished);
                    }
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();

        protected virtual void OnCurrent(T item) { }
        protected virtual void OnIdle(ref bool exit) { }

        private static readonly TimeSpan minWait = TimeSpan.FromMilliseconds(1);
        private static readonly TimeSpan maxWait = TimeSpan.FromMilliseconds(100);

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    itemAdded.Dispose();
                    finished.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
