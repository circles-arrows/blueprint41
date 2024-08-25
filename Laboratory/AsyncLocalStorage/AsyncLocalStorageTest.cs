using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Laboratory.AsyncLocalStorage
{
    public static class AsyncLocalStorageTest
    {
        public static void Test()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;

            bool parallel = true;

            List<Thread> threads = new List<Thread>();

            for (int index = 0; index < 4; index++)
            {
                Thread thread = new Thread(TestTaskLocalStorage);
                threads.Add(thread);

                thread.Start(parallel);

                parallel = !parallel;
            }

            threads.ForEach(t => t.Join());

            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey(true);
        }
        public static void TestTaskLocalStorage(object? arg)
        {
            string context = Guid.NewGuid().ToString();
            bool parallel = arg is bool && (bool)arg;
            Stack stack = new Stack();

            Console.WriteLine($"Starting Test: {context}");

            _TaskLocalStorageContext.Value = context;
            _TaskLocalStoragePath.Value = stack;

            TestTaskLocalStorageRecursive(context, 0, 0, parallel, stack.ToString()).Wait();

            Console.WriteLine($"Finished Test: {context}");
        }

        public static async Task TestTaskLocalStorageRecursive(string context, int depth, int index, bool parallel, string stck)
        {
            Console.WriteLine($"\tEntering: {context}, Depth: {depth}, Index: {index}, Parallel: {parallel}");

            Stack newStack = (_TaskLocalStoragePath.Value ?? new Stack()).Push(context, depth, index);

            _TaskLocalStoragePath.Value = newStack;

            if (context != _TaskLocalStorageContext.Value)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\tExpected: {context}, Actual: {_TaskLocalStorageContext.Value}, Depth: {depth}, Index: {index}, Parallel: {parallel}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            if (stck != newStack.ToString(1))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\tExpected:{Environment.NewLine}{stck}{Environment.NewLine}Actual:{newStack.ToString(1)}{Environment.NewLine}Depth: {depth}, Index: {index}, Parallel: {parallel}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Task[]? result = null;

            if (depth < 3)
            {
                if (parallel)
                {
                    result = Enumerable.Range(0, 3).Select(item => TestTaskLocalStorageRecursive(context, depth + 1, item, parallel, newStack.ToString())).ToArray();
                }
                else
                {
                    foreach (int item in Enumerable.Range(0, 3))
                        await TestTaskLocalStorageRecursive(context, depth + 1, item, parallel, newStack.ToString());
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\tWaiting: {context}, Depth: {depth}, Index: {index}, Parallel: {parallel}");
                Console.ForegroundColor = ConsoleColor.Gray;

                if (parallel)
                    result = [Task.Delay(1000)];
                else
                    await Task.Delay(1000);
            }

            Console.WriteLine($"\tLeaving: {context}, Depth: {depth}, Index: {index}, Parallel: {parallel}");

            if (result is not null)
                Task.WaitAll(result);
        }

        private static readonly AsyncLocal<string> _TaskLocalStorageContext = new AsyncLocal<string>();
        private static readonly AsyncLocal<Stack> _TaskLocalStoragePath = new AsyncLocal<Stack>();
    }
}
