using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Core;

namespace Blueprint41.ApocDocumentationParser
{
    internal class HttpClientCache : IDisposable
    {
        public HttpClientCache(params string[] folder)
        {
            CustomTaskQueueOptions main = new CustomTaskQueueOptions(4);
            CustomTaskQueueOptions sub = new CustomTaskQueueOptions(10);
            taskScheduler = new CustomTaskScheduler(main, sub);
            
            WithCache = folder.Length > 0;

            ProjectFolder = AppDomain.CurrentDomain.BaseDirectory;
            if (WithCache)
            {
                string projectFileName = $"{typeof(HttpClientCache).Namespace}.csproj";
                while (WithCache && ProjectFolder is not null && !File.Exists(Path.Combine(ProjectFolder, projectFileName)))
                    ProjectFolder = Path.GetDirectoryName(ProjectFolder)!;
            }
            ProjectFolder ??= AppDomain.CurrentDomain.BaseDirectory;

            string[] folderParts = new string[folder.Length + 2];
            folderParts[0] = ProjectFolder;
            folderParts[1] = "HttpClientCache";
            Array.Copy(folder, 0, folderParts, 2, folder.Length);

            CacheFolder = Path.Combine(folderParts);
        }

        public bool WithCache { get; private set; }
        public string ProjectFolder { get; private set; }
        public string CacheFolder { get; private set; }

        public void SchedulePage(string key, string url, Action<string> contentCallback, int autoRetries = 3)
        {
            taskScheduler!.RunAsync(delegate()
            {
                Task<string> task = GetPage(key, url, autoRetries);
                Task callback = task.ContinueWith(item => contentCallback.Invoke(item.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
                taskCache.Add((key, url, task, callback));

            }, $"Get Page {key}");
        }
        public List<(string key, string url, Exception[] errors)> WaitForPages(StatusUpdate? infoCallback = null)
        {
            taskScheduler!.Wait(true, false, infoCallback);

            // Retrieve error details
            List<(string key, string url, Exception[] errors)> result = taskCache.Where(item => item.callback.IsFaulted).Select(item => (item.key, item.url, item.task.Exception?.InnerExceptions?.ToArray() ?? new Exception[0])).ToList();
            taskCache.Clear();

            return result;
        }
        private readonly List<(string key, string url, Task task, Task callback)> taskCache = new List<(string key, string url, Task task, Task callback)>();
        private IEnumerable<Task> AllTasks => taskCache.Select(item => (item.task.IsFaulted) ? item.task : item.callback);
        private IEnumerable<Task> RunningTasks => AllTasks.Where(item => !item.IsCompleted);

        public async Task<string> GetPage(string key, string url, int autoRetries = 3)
        {
            if (WithCache)
            {
                if (!Directory.Exists(CacheFolder))
                    Directory.CreateDirectory(CacheFolder);

                string filename = Path.Combine(CacheFolder, key);
                if (File.Exists(filename))
                    return File.ReadAllText(filename);
            }

            if (autoRetries <= 0)
            {
                string content = await client.GetStringAsync(url);
                if (WithCache)
                    WriteCache(key, content);

                return content;
            }
            else
            {
                while (true)
                {
                    try
                    {
                        string content = await client.GetStringAsync(url);
                        if (WithCache)
                            WriteCache(key, content);

                        return content;
                    }
                    catch (Exception ex)
                    {
                        autoRetries--;
                        if (autoRetries <= 0)
                            throw;
                    }
                }
            }

        }
        private void WriteCache(string key, string content)
        {
            if (!Directory.Exists(CacheFolder))
                Directory.CreateDirectory(CacheFolder);

            string filename = Path.Combine(CacheFolder, key);
            if (!File.Exists(filename))
                File.WriteAllText(filename, content);
        }

        private static readonly HttpClient client = new HttpClient();
        private CustomTaskScheduler? taskScheduler;
        void IDisposable.Dispose()
        {
            taskScheduler?.Dispose();
            taskScheduler = null;
        }
    }
}
