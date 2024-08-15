using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Laboratory
{
    public static class Program
    {
        public static void Main(string[] args)
        {
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
            bool parallel = (arg is bool) && (bool)arg;

            _TaskLocalStorageContext.Value = context;
            //_TaskLocalStoragePath.Value = new

            TestTaskLocalStorageRecursive(context, 0, 0, parallel).Wait();

            Console.WriteLine($"Finished Test: {context}");
        }

        public static async Task TestTaskLocalStorageRecursive(string context, int depth, int index, bool parallel)
        {
            Console.WriteLine($"\tEntering: {context}, Depth: {depth}, Index: {index}, Parallel: {parallel}");

            if (context != _TaskLocalStorageContext.Value)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\tExpected: {context}, Actual {_TaskLocalStorageContext.Value}, Depth: {depth}, Index: {index}, Parallel: {parallel}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Task[]? result = null;

            if (depth < 3)
            {
                if (parallel)
                {
                    result = Enumerable.Range(0, 3).Select(item => TestTaskLocalStorageRecursive(context, depth + 1, item, parallel)).ToArray();
                }
                else
                {
                    foreach (int item in Enumerable.Range(0, 3))
                        await TestTaskLocalStorageRecursive(context, depth + 1, item, parallel);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\tWaiting: {context}, Depth: {depth}, Index: {index}, Parallel: {parallel}");
                Console.ForegroundColor = ConsoleColor.Gray;

                if (parallel)
                    result = new[] { Task.Delay(1000) };
                else
                    await Task.Delay(1000);
            }

            Console.WriteLine($"\tLeaving: {context}, Depth: {depth}, Index: {index}, Parallel: {parallel}");

            if (result is not null)
                Task.WaitAll(result);
        }

        private static readonly AsyncLocal<string> _TaskLocalStorageContext = new AsyncLocal<string>();
        private static readonly AsyncLocal<List<(string context, int depth, )>> _TaskLocalStoragePath = new AsyncLocal<string>();
    }
}
