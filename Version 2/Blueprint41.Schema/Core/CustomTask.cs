using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    public sealed class CustomTask
    {
        internal CustomTask(Task task, string? description, List<CustomTask>? waitsFor = null)
        {
            MainTask = this;
            Task = task;
            Type = (description is null) ? CustomTaskQueueType.Sub : CustomTaskQueueType.Main;
            Description = description ?? task.GetAsyncTaskDescription() ?? "Unknown task";

            antecedentTasks = waitsFor ?? new List<CustomTask>(0);
            foreach (CustomTask successor in successiveTasks)
                successor.successiveTasks.Add(successor);
        }
        internal CustomTask(CustomTask parent, Task task, List<CustomTask>? waitsFor = null)
        {
            Task = task;
            Type = CustomTaskQueueType.Sub;

            antecedentTasks = waitsFor ?? new List<CustomTask>(0);
            foreach (CustomTask successor in successiveTasks)
                successor.successiveTasks.Add(successor);

            SetLinks(parent, this);
        }

        internal static void SetLinks(CustomTask parentTask, CustomTask subTask)
        {
            if (subTask.ParentTask is not null)
                throw new InvalidOperationException();

            parentTask.subTasks.Add(subTask);
            subTask.ParentTask = parentTask;
            subTask.MainTask = parentTask.MainTask;

            string? description = subTask.Task.GetAsyncTaskDescription();
            if (string.IsNullOrEmpty(description))
                subTask.Description = parentTask.MainTask.Description;
            else
                subTask.Description = $"{parentTask.MainTask.Description} ({description})";
        }

        public Task Task { get; private set; }
        public CustomTaskQueueType Type { get; private set; }
        public string Description { get; private set; } = null!;

        public CustomTask MainTask { get; private set; } = null!;
        public CustomTask? ParentTask { get; private set; }
        public IReadOnlyList<CustomTask> SubTasks => subTasks;
        private readonly List<CustomTask> subTasks = new List<CustomTask>();

        public IReadOnlyList<CustomTask> AntecedentTasks => antecedentTasks;
        private readonly List<CustomTask> antecedentTasks;

        public IReadOnlyList<CustomTask> SuccessiveTasks => successiveTasks;
        private readonly List<CustomTask> successiveTasks = new List<CustomTask>();

        public int? TaskRunnerId { get; internal set; }

        internal bool Scheduled { get => scheduled is not null; set => scheduled = DateTime.UtcNow; }
        private DateTime? scheduled = null;

        public TimeSpan Delay
        {
            get
            {
                if (delay == TimeSpan.Zero)
                    delay = (scheduled.HasValue) ? (DateTime.UtcNow - scheduled.Value) : TimeSpan.Zero;

                return delay;
            }
        }
        private TimeSpan delay = TimeSpan.Zero;

        public bool Completed(bool recursive = true) => Task.Completed(recursive);
        public void Wait(bool recursive = true) => Task.WaitEx(recursive);

        public static CustomTask? Current
        {
            get => current.Value;
            internal set => current.Value = value;
        }
        private static AsyncLocal<CustomTask?> current = new AsyncLocal<CustomTask?>();

        public override string ToString()
        {
            if (TaskRunnerId.HasValue)
                return $"{Type} Task {Task.Id} '{Description}' {Task.Status} on task-runner {TaskRunnerId.Value}";
            
            return $"{Type} Task {Task.Id} '{Description}' {Task.Status}";
        }
    }
    public enum CustomTaskQueueType
    {
        Main,
        Sub,
    }
}
