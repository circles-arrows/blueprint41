using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using System.Reflection;
using System.Linq.Expressions;

namespace Blueprint41.Core
{
    public static class AsyncHelper
    {
        private static readonly TaskFactory _myTaskFactory = new
             TaskFactory(CancellationToken.None,
                         TaskCreationOptions.None,
                         TaskContinuationOptions.None,
                         TaskScheduler.Default);

        [Obsolete("Please stop using this and consider using the CustomTaskScheduler instead", true)]
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return _myTaskFactory
              .StartNew(func)
              .Unwrap()
              .GetAwaiter()
              .GetResult();
        }

        [Obsolete("Please stop using this and consider using the CustomTaskScheduler instead", true)]
        public static void RunSync(Func<Task> func)
        {
            _myTaskFactory
              .StartNew(func)
              .Unwrap()
              .GetAwaiter()
              .GetResult();
        }

        public static Task<TResult> NewTask<TResult>(Func<TResult> func)
        {
            return _myTaskFactory.StartNew(func);
        }

        public static Task NewTask(Action func)
        {
            return _myTaskFactory.StartNew(func);
        }

        public static bool Completed(this Task task, bool recursive = true)
        {
            switch (task.Status)
            {
                case TaskStatus.Created:
                case TaskStatus.Running:
                case TaskStatus.WaitingForActivation:
                case TaskStatus.WaitingForChildrenToComplete:
                case TaskStatus.WaitingToRun:
                    return false;
                case TaskStatus.Canceled:
                case TaskStatus.Faulted:
                case TaskStatus.RanToCompletion:
                    if (!recursive)
                        return true;

                    bool isCompleted = true;
                    OnSubTask(task, subTask =>
                    {
                        if (isCompleted && !subTask.Completed(recursive))
                            isCompleted = false;
                    });

                    return isCompleted;
                default:
                    throw new NotSupportedException($"Task status '{task.Status}' is not supported.");
            }
        }
        public static T WaitEx<T>(this T task, bool recursive = true)
            where T : Task
        {
            WaitEx(task, recursive, null);
            return task;
        }
        internal static void WaitEx<T>(T task, bool recursive, Action? status)
            where T :Task
        {
            if (status is null)
            {
                task.Wait();
            }
            else
            {
                while (!task.IsCompleted)
                {
                    task.Wait(100);
                    status.Invoke();
                }
            }

            if (recursive)
                OnSubTask(task, subTask => WaitEx(subTask, recursive, status));
        }

        public static void WaitEx(this IEnumerable<Task> tasks, bool recursive = true)
        {
            WaitEx(tasks, recursive, null);
        }
        internal static void WaitEx(this IEnumerable<Task> tasks, bool recursive, Action? status)
        {
            foreach (Task task in tasks)
            {
                if (task is not null)
                    WaitEx(task, recursive, status);
            }
        }
        public static T ResultEx<T>(this Task<T> task)
        {
            task.WaitEx();
            return task.Result;
        }

        public static void OnSubTask(this Task task, Action<Task> action)
        {
            Task? subTask = GetInnerTask(task);
            if (subTask is not null)
            {
                action.Invoke(subTask);
            }
            else
            {
                IEnumerable<Task>? subTasks = GetInnerTasks(task);
                if (subTasks is not null)
                {
                    foreach (Task? item in subTasks)
                    {
                        if (item is not null)
                            action.Invoke(item);
                    }
                }
            }
        }

        public static Task? GetAntecedentTask(this Task task)
        {
            if (task is null)
                return null;

            return getAntecedentTaskMethods.TryGetOrAdd(task.GetType(), delegate (Type type)
            {
                FieldInfo? fieldInfo = type.GetField("m_antecedent", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo is null)
                    return null;

                ParameterExpression parameter = Expression.Parameter(typeof(Task), "task");
                Expression methodCall = Expression.Field(Expression.Convert(parameter, type), fieldInfo);

                return Expression.Lambda<Func<Task, Task?>>(methodCall, parameter).Compile();

            })?.Invoke(task);

            // private Task<TAntecedentResult> m_antecedent;
        }
        public static IEnumerable<Task>? GetAntecedentTasks(this Task task)
        {
            if (task is null)
                return null;

            return getAntecedentTasksMethods.TryGetOrAdd(task.GetType(), delegate (Type type)
            {
                FieldInfo? fieldInfo = type.GetField("_tasks", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo is null)
                    return null;

                ParameterExpression parameter = Expression.Parameter(typeof(Task), "task");
                Expression methodCall = Expression.Convert(Expression.Field(Expression.Convert(parameter, type), fieldInfo), typeof(IEnumerable<Task>));

                return Expression.Lambda<Func<Task, IEnumerable<Task>?>>(methodCall, parameter).Compile();

            })?.Invoke(task);

            // private readonly Task[] _tasks;
            // private readonly Task<T>[] _tasks;
            // private IList<Task> _tasks;
        }

        private static readonly AtomicDictionary<Type, Func<Task, Task?>?> getAntecedentTaskMethods = new AtomicDictionary<Type, Func<Task, Task?>?>();
        private static readonly AtomicDictionary<Type, Func<Task, IEnumerable<Task>?>?> getAntecedentTasksMethods = new AtomicDictionary<Type, Func<Task, IEnumerable<Task>?>?>();

        private static Task? GetInnerTask(Task task)
        {
            if (!Completed(task, false))
                return null;

            return getTaskResultMethods.TryGetOrAdd(task.GetType(), delegate (Type type)
            {
                if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Task<>))
                    return null;

                Type returnType = type.GetGenericArguments()[0];
                if (!typeof(IAsyncResult).IsAssignableFrom(returnType))
                    return null;

                MethodInfo? methodInfo = type.GetProperty("Result", BindingFlags.Instance | BindingFlags.Public)?.GetGetMethod();
                if (methodInfo is null)
                    return null;

                ParameterExpression parameter = Expression.Parameter(typeof(Task), "task");
                Expression methodCall = Expression.Convert(Expression.Call(Expression.Convert(parameter, type), methodInfo), typeof(Task));

                return Expression.Lambda<Func<Task, Task?>>(methodCall, parameter).Compile();

            })?.Invoke(task);
        }
        private static IEnumerable<Task>? GetInnerTasks(Task task)
        {
            if (task is null)
                return null;

            if (!Completed(task, false))
                return null;

            return getTaskResultsMethods.TryGetOrAdd(task.GetType(), delegate (Type type)
            {
                if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IEnumerable<>) || type.GenericTypeArguments[0].GetGenericTypeDefinition() != typeof(Task<>))
                    return null;

                Type returnType = type.GetGenericArguments()[0];
                if (!typeof(IEnumerable<IAsyncResult>).IsAssignableFrom(returnType))
                    return null;

                MethodInfo? methodInfo = type.GetProperty("Result", BindingFlags.Instance | BindingFlags.Public)?.GetGetMethod();
                if (methodInfo is null)
                    return null;

                ParameterExpression parameter = Expression.Parameter(typeof(Task), "task");
                Expression methodCall = Expression.Convert(Expression.Call(Expression.Convert(parameter, type), methodInfo), typeof(IEnumerable<Task>));

                return Expression.Lambda<Func<Task, IEnumerable<Task>?>>(methodCall, parameter).Compile();

            })?.Invoke(task);
        }
        private static readonly AtomicDictionary<Type, Func<Task, Task?>?> getTaskResultMethods = new AtomicDictionary<Type, Func<Task, Task?>?>();
        private static readonly AtomicDictionary<Type, Func<Task, IEnumerable<Task>?>?> getTaskResultsMethods = new AtomicDictionary<Type, Func<Task, IEnumerable<Task>?>?>();
    }
    public static class Threadsafe
    {
        public static T LazyInit<T>(ref T variable, Func<T> factoryMethod)
        {
            if (variable is null)
            {
                lock (typeof(SyncLock<T>))
                {
                    if (variable is null)
                        variable = factoryMethod.Invoke();
                }
            }
            return variable;
        }

        private class SyncLock<T>
        {
        }
    }

}
