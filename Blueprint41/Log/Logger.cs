using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace Blueprint41.Log
{
    public class TransactionLogger
    {
        #region Properties

        private string m_LogDirectory = null!;
        public string LogDirectory
        {
            get
            {
                return m_LogDirectory;
            }
            set
            {
                m_LogDirectory = value ?? InitialLogDirectory();
                LogFile = Path.Combine(LogDirectory, string.Concat("Log_", DateTime.Now.ToString("yyyyMMdd_hhmmss"), ".csv"));
            }
        }

        public int MaxFileSize { get; private set; }

        public int ThresholdInSeconds { get; set; }

        public string LogFile { get; private set; }

        private Stopwatch watcher;

        private readonly object FileLock = new object();

        #endregion

        public TransactionLogger()
        {
            LogFile = string.Empty;

            ThresholdInSeconds = 100 /*1s*/;
            MaxFileSize = 2 * 1024 * 1024;
            LogDirectory = InitialLogDirectory();

            watcher = new Stopwatch();
        }
        private static string InitialLogDirectory()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? @"C:\", "TransactionLogs");
        }

        internal void Start()
        {
            watcher.Reset();
            watcher.Start();
        }

        internal void Stop(string message, Dictionary<string, object?>? parameters = null, List<string>? callerInfo = null)
        {
            watcher.Stop();
            if (watcher.ElapsedMilliseconds >= ThresholdInSeconds)
            {
                if (parameters != null)
                    foreach (var par in parameters)
                        message = message.Replace("{" + par.Key + "}", Serializer.Serialize(par.Value).Replace("\"", "\'"));

                if (callerInfo is null)
                    Log(string.Format("{0}\t{1}", TimeSpan.FromMilliseconds(watcher.ElapsedMilliseconds), message));
                else
                    Log(string.Format("{0}\t{1}\t{2}\t{3}\t{4}", callerInfo[0], callerInfo[1], callerInfo[2], TimeSpan.FromMilliseconds(watcher.ElapsedMilliseconds), message));
            }
        }

        internal void Log(string message)
        {
            if (!Directory.Exists(LogDirectory))
                Directory.CreateDirectory(LogDirectory);

            FileInfo fileInfo = new FileInfo(LogFile);
            if (fileInfo.Exists)
            {
                if (fileInfo.Length >= MaxFileSize)
                    LogFile = Path.Combine(LogDirectory, string.Concat("Log_", DateTime.Now.ToString("yyyyMMdd_hhmmss"), ".csv"));
            }
            else
                message = string.Concat("Caller\tFile\tLine\tDuration\tQuery", "\r\n", message);

            lock (FileLock)
            {
                File.AppendAllText(LogFile, string.Concat(message, "\r\n"));
            }
        }

        #region Utilities

        /// <summary>
        /// kb, mb, gb, b
        /// </summary>
        /// <param name="text"></param>
        public void SetMaxFileSize(string text)
        {
            if (text.ToLower().Contains("kb"))
            {
                MaxFileSize = int.Parse(text.ToLower().Replace("kb", "000"));
            }
            else if (text.ToLower().Contains("mb"))
            {
                MaxFileSize = int.Parse(text.ToLower().Replace("mb", "000000"));
            }
            else if (text.ToLower().Contains("gb"))
            {
                MaxFileSize = int.Parse(text.ToLower().Replace("gb", "000000000"));
            }
            else if (text.ToLower().Contains("b"))
            {
                MaxFileSize = int.Parse(text.ToLower().Replace("b", ""));
            }
            else
                throw new NotSupportedException("This method requires that you specify the unit like GB, MB, KB, B.");
        }

        #endregion
    }
}
