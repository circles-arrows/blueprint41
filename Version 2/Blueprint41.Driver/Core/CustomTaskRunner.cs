//#define OUTPUT_DEBUG_TO_CONSOLE
//#define SUPRESS_SUB_TASKS

using System;
using System.Collections.Generic;
using System.Threading;

namespace Blueprint41.Core
{
    internal sealed class CustomTaskRunner
    {
        internal CustomTaskRunner(CustomTaskQueue queue, int id)
        {
            Queue = queue;
            Id = id;
            RunningTask = null;

            Thread = new Thread(ExecuteTasks);
            Thread.Name = $"{queue.Options.QueueType} Runner {Id}";
            Thread.IsBackground = true;
            Thread.Start();
        }

        public readonly int Id;
        public readonly Thread Thread;
        internal readonly CustomTaskQueue Queue;

        private void ExecuteTasks()
        {
            do
            {
                foreach (CustomTask task in Queue.GetConsumingEnumerable())
                {
                    try
                    {
                        task.TaskRunnerId = Id;
                        RunningTask = task;
                        CustomTask.Current = task;

#if OUTPUT_DEBUG_TO_CONSOLE
                        if (task.Type == CustomTaskQueueType.Main)
                        {
                            Console.WriteLine($"Running Task '{task.Description}' on Thread {Id}");
                        }
#if !SUPRESS_SUB_TASKS
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine($"Running Task '{task.Description}' on Thread {Id}");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
#endif
#endif

                        Queue.Scheduler.ExecuteTask(task.Task);

#if OUTPUT_DEBUG_TO_CONSOLE
                        if (task.Type == CustomTaskQueueType.Main)
                        {
                            Console.WriteLine($"Finished Task '{task.Description}' on Thread {Id} with status '{task.Task.Status}'");
                        }
#if !SUPRESS_SUB_TASKS
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine($"Finished Task '{task.Description}' on Thread {Id} with status '{task.Task.Status}'");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
#endif
                        if (task.Task.Exception != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{task.Task.Exception.GetType().Name}: {task.Task.Exception.Message}");
                            Console.WriteLine(task.Task.Exception.StackTrace);
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        }
#endif
                    }
                    finally
                    {
                        task.Task.OnSubTask(subTask => Queue.Scheduler.RegisterTask(subTask));
                        RunningTask = null;
                        CustomTask.Current = null;
                        //Idle = DateTime.UtcNow;
                    }
                }
            }
            while (!Queue.IsReadOnly && !Queue.Shrink(this));
        }

        public CustomTask? RunningTask { get; private set; }
        //internal DateTime? Idle { get; private set; }
        //internal TimeSpan Delay { get; private set; }

        public override string ToString()
        {
            return $"Runner {Id}, Thread {Thread.ManagedThreadId} ({Thread.ThreadState}), Task {RunningTask?.Task.Id.ToString() ?? "None"} ({RunningTask?.Task.Status.ToString() ?? "NA"})";
        }
    }
}
