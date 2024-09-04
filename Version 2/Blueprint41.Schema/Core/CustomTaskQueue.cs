using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Blueprint41.Core
{
    internal sealed class CustomTaskQueue : BlockingCollectionSlim<CustomTask>
    {
        internal CustomTaskQueue(CustomTaskScheduler scheduler, CustomTaskQueueType queueType, CustomTaskQueueOptions options)
        {
            Scheduler = scheduler;
            Options = options;
            Options.QueueType = queueType;

            if (options.Enabled)
            {
                taskRunners = new List<CustomTaskRunner>(options.MaxThreads);
                taskRunners.AddRange(Enumerable.Range(0, options.MinThreads).Select(id => new CustomTaskRunner(this, id)));
            }
            else
            {
                taskRunners = new List<CustomTaskRunner>(0);
            }
        }

        private readonly List<CustomTaskRunner> taskRunners;

        internal CustomTaskScheduler Scheduler { get; private set; }
        public int ThreadCount => taskRunners.Count;
        public readonly CustomTaskQueueOptions Options;

        protected override void OnCurrent(CustomTask item)
        {
            Interlocked.Exchange(ref lastItemAccessed, DateTime.UtcNow.Ticks);

            if (item.Delay > Options.TolerableWaitForExecution)
            {
                lock (taskRunners)
                {
                    long last = Interlocked.Read(ref lastThreadCountIncrease);
                    if ((last + Options.DelayBeforeIncreasingAgain.Ticks) > DateTime.UtcNow.Ticks)
                        return;

                    // it's 1 second after the last thread count increase
                    if (taskRunners.Count < Options.MaxThreads) // if there are stills threads we can add
                    {
                        int currentId = taskRunners.Last().Id; // Seed Id
                        for (int index = 0; index < Options.IncreaseThreadCountWith; index++)
                        {
                            if (taskRunners.Count >= Options.MaxThreads)
                                break; // if there are no more threads we can add

                            taskRunners.Add(new CustomTaskRunner(this, ++currentId));
                        }
                    }
                    Interlocked.Exchange(ref lastThreadCountIncrease, DateTime.UtcNow.Ticks);
                }
            }
        }
        protected override void OnIdle(ref bool exit)
        {
            long last = Interlocked.Read(ref lastItemAccessed);
            if ((last + Options.WindDownAfter.Ticks) > DateTime.UtcNow.Ticks)
                return;

            exit = true;
        }

        internal bool Shrink(CustomTaskRunner thread)
        {
            if (thread.Id < Options.MinThreads)
                return false; // Continue this thread

            lock (taskRunners)
                taskRunners.Remove(thread);

            return true; // Exit this thread
        }

        private long lastItemAccessed = DateTime.UtcNow.Ticks;
        private long lastThreadCountIncrease = DateTime.UtcNow.Ticks;

        internal void ThreadJoin()
        {
            foreach (CustomTaskRunner runner in taskRunners)
                runner.Thread.Join();
        }
    }
}
