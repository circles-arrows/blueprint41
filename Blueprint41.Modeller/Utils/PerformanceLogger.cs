using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller.Utils
{
    internal class PerformanceLogger
    {
        public static Lazy<PerformanceLogger> _instance = new Lazy<PerformanceLogger>(() => new PerformanceLogger());
        public static PerformanceLogger Instance => _instance.Value;

        public long ThresholdInMilliseconds { get; set; } = 0;
        Stopwatch watcher;

        private PerformanceLogger()
        {
#if DEBUG
            watcher = new Stopwatch();
#endif
        }

        public void Start()
        {
#if DEBUG

            watcher.Reset();
            watcher.Start();
#endif
        }

        public void Stop(
        [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
#if DEBUG
            watcher.Stop();
            if (watcher.ElapsedMilliseconds >= ThresholdInMilliseconds)
            {
                System.Diagnostics.Trace.WriteLine("member name: " + memberName);
                System.Diagnostics.Trace.WriteLine("source file path: " + sourceFilePath);
                System.Diagnostics.Trace.WriteLine("source line number: " + sourceLineNumber);
                System.Diagnostics.Trace.WriteLine("Time Spent: " + watcher.ElapsedMilliseconds + " ms");
            }
#endif
        }
    }
}
