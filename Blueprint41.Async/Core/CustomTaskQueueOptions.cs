using System;
using System.Collections.Generic;

namespace Blueprint41.Async.Core
{
    public class CustomTaskQueueOptions
    {
        public CustomTaskQueueOptions() : this(Math.Min(Math.Max(4, Environment.ProcessorCount), 128)) { }
        public CustomTaskQueueOptions(int threads) : this(threads, threads) { }
        public CustomTaskQueueOptions(int minThreads, int maxThreads)
        {
            int min = Math.Max(minThreads, 0);

            Enabled = (min > 0);
            MinThreads = min;
            MaxThreads = Math.Max(maxThreads, min);
            TolerableWaitForExecution = TimeSpan.FromMilliseconds(500);
            DelayBeforeIncreasingAgain = TimeSpan.FromMilliseconds(500);
            IncreaseThreadCountWith = 10;
            WindDownAfter = TimeSpan.FromSeconds(5);
        }

        public CustomTaskQueueType QueueType { get; internal set; }

        /// <summary>
        /// If the queue is enabled or disabled
        /// </summary>
        public bool Enabled { get; private set; }

        /// <summary>
        /// The minimum amount of threads the queue will reserve to execute tasks.
        /// </summary>
        public int MinThreads { get; private set; }

        /// <summary>
        /// The maximum amount of threads the queue will use to execute tasks.
        /// </summary>
        public int MaxThreads { get; private set; }

        /// <summary>
        /// The tolerable wait time before a task is taken from the queue for processing.
        /// </summary>
        public TimeSpan TolerableWaitForExecution { get; set; }

        /// <summary>
        /// The amount time waiting to see if the Task wait time stabilizes, before threads are added again.
        /// </summary>
        public TimeSpan DelayBeforeIncreasingAgain { get; set; }

        /// <summary>
        /// The amount of threads that will be added when the tolerable wait time was exceeded.
        /// </summary>
        public int IncreaseThreadCountWith { get; set; }

        /// <summary>
        /// The queue idle time before the amount of threads will be decreased to the minimum count.
        /// </summary>
        public TimeSpan WindDownAfter { get; set; }

        public static readonly CustomTaskQueueOptions Disabled = new CustomTaskQueueOptions() { Enabled = false };
    }
}
