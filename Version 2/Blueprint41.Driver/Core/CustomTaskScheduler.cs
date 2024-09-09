using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Blueprint41.Core
{
    internal class CustomTaskScheduler : TaskScheduler, IDisposable
    {
        private readonly TaskFactory taskFactory;

        private readonly CustomTaskQueue scheduledMain;
        private readonly CustomTaskQueue scheduledSub;

        private readonly AtomicDictionary<Task, CustomTask> history = new AtomicDictionary<Task, CustomTask>(16, true);
        private int head = 0;

        public CustomTaskScheduler(CustomTaskQueueOptions? mainQueue = null, CustomTaskQueueOptions? subQueue = null)
        {
            taskFactory = new TaskFactory(CancellationToken.None,
                                          TaskCreationOptions.LongRunning | TaskCreationOptions.RunContinuationsAsynchronously,
                                          TaskContinuationOptions.None,
                                          this);

            scheduledMain = new CustomTaskQueue(this, CustomTaskQueueType.Main, mainQueue ?? new CustomTaskQueueOptions());
            scheduledSub = new CustomTaskQueue(this, CustomTaskQueueType.Sub, subQueue ?? CustomTaskQueueOptions.Disabled);
        }

        protected override IEnumerable<Task> GetScheduledTasks() => GetScheduledMainTasks().Union(GetScheduledSubTasks());
        private IEnumerable<Task> GetScheduledMainTasks() => scheduledMain.ToArray().Select(item => item.Task);
        private IEnumerable<Task> GetScheduledSubTasks() => scheduledSub.ToArray().Select(item => item.Task);

        protected override void QueueTask(Task task)
        {
            if (task != null)
            {
                CustomTask customTask = CreateTask(task);
                if (!customTask.Scheduled)
                {
                    lock (customTask)
                    {
                        if (!customTask.Scheduled)
                        {
                            if (customTask.Type == CustomTaskQueueType.Main || scheduledSub.ThreadCount == 0)
                            {
                                scheduledMain.Add(customTask);
                            }
                            else
                            {
                                scheduledSub.Add(customTask);
                            }

                            customTask.Scheduled = true;
                        }
                    }
                }

                taskDescription.Value = null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ExecuteTask(Task task) => TryExecuteTask(task);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal virtual void RegisterTask(Task task)
        {
            if (task != null)
                CreateTask(task);
        }

        protected CustomTask CreateTask(Task task)
        {
            return history.TryGetOrAdd(task, delegate (Task key)
            {
                List<CustomTask>? antecedents = key.GetAntecedentTasks()?.Select(item => CreateTask(item)).ToList();
                if (antecedents is null)
                {
                    Task? antecedent = task.GetAntecedentTask();
                    if (antecedent is not null)
                        antecedents = new List<CustomTask>() { CreateTask(antecedent) };
                }

                if (CustomTask.Current is null)
                {
                    return new CustomTask(key, taskDescription.Value, antecedents);
                }
                else
                {
                    return new CustomTask(CustomTask.Current, task, antecedents);
                }
            });
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return false;
        }

        public virtual void RunBlocking(Func<Task> work, string description)
        {
            Task task = RunAsync(work, description).Unwrap();

            CustomTask customTask = CreateTask(task);
            customTask.Wait(true);
        }
        public virtual TResult RunBlocking<TResult>(Func<Task<TResult>> work, string description)
        {
            Task<TResult> task = RunAsync(work, description).Unwrap();

            CustomTask customTask = CreateTask(task);
            customTask.Wait(true);

            return task.Result;
        }

        public Task RunAsync(Action work, string description)
        {
#pragma warning disable S2696 // Instance members should not write to "static" fields
            taskDescription.Value = description;
            Task task = taskFactory.StartNew(work);
            QueueTask(task);
            if (taskDescription.Value is not null)
                taskDescription.Value = null;
#pragma warning restore S2696 // But here we can because its not static it's ThreadStatic

            return task;
        }
        public Task<TResult> RunAsync<TResult>(Func<TResult> work, string description)
        {
#pragma warning disable S2696 // Instance members should not write to "static" fields
            taskDescription.Value = description;
            Task<TResult> task = taskFactory.StartNew(work);
            QueueTask(task);
            if (taskDescription.Value is not null)
                taskDescription.Value = null;
#pragma warning restore S2696 // But here we can because its not static it's ThreadStatic

            return task;
        }

        static private AsyncLocal<string?> taskDescription = new AsyncLocal<string?>();

        public virtual void Wait(bool includeSubTasks = true, bool clearHistory = true, StatusUpdate? status = null)
        {
            if (!includeSubTasks)
                clearHistory = false;

            Action? action = status is null ? null : () => status.Invoke(history.Count(item => !item.Value.Completed(true)));

            int skip = head;
            while (skip < history.Count)
            {
                KeyValuePair<Task, CustomTask>[] items = history.ToArray();

                items.Skip(skip).Select(item => item.Value.Task).WaitEx(includeSubTasks, action);

                skip = items.Length;
            }

            if (clearHistory)
            {
                head = 0;
                history.Clear();
            }
            else
            {
                head = history.Count;
            }
        }
        public virtual void ClearHistory(bool onlyIfSubtasksAreAlsoCompleted = false)
        {
            foreach (KeyValuePair<Task, CustomTask> task in history.ToArray())
            {
                if (task.Key.Completed(onlyIfSubtasksAreAlsoCompleted))
                    history.Remove(task.Key);
            }
        }

        public int ThreadCount => scheduledMain.ThreadCount + scheduledSub.ThreadCount;
        public ICollection<CustomTask> Tasks => history.Values;

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            if (disposing)
            {
                Wait();
                scheduledMain.CompleteAdding();
                scheduledSub.CompleteAdding();

                scheduledMain.ThreadJoin();
                scheduledSub.ThreadJoin();

                scheduledMain.Dispose();
                scheduledSub.Dispose();
            }

            isDisposed = true;
        }
        private bool isDisposed;

        #endregion
    }
    internal class CustomThreadSafeTaskScheduler : CustomTaskScheduler
    {
        public CustomThreadSafeTaskScheduler(CustomTaskQueueOptions? mainQueue = null, CustomTaskQueueOptions? subQueue = null) : base(mainQueue, subQueue) { }

        public override void Wait(bool includeSubTasks = true, bool clearHistory = true, StatusUpdate? status = null)
        {
            base.Wait(includeSubTasks, false, status);

            if (clearHistory)
                ClearHistory();
        }
    }
    internal delegate void StatusUpdate(int pending);
}
