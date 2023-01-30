using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Collections;

using Blueprint41.Neo4j.Persistence;

namespace Blueprint41.Log
{
    public class TransactionLogger
    {
        #region Properties

        private readonly AdvancedConfig Config;
        private readonly string LogDirectory;
        private readonly int MaxFileSizeInBytes;
        private readonly int ThresholdInMilliSeconds;

        public string? LogFile { get; private set; }


        private Stopwatch watcher;

        private readonly object FileLock = new object();

        #endregion

        public TransactionLogger(AdvancedConfig config)
        {
            Config = config;
            LogDirectory = config.LogDirectory ?? InitialLogDirectory();
            MaxFileSizeInBytes = GetMaxFileSize(config.MaxFileSize);
            ThresholdInMilliSeconds = config.ThresholdInSeconds * 1000;

            watcher = new Stopwatch();

            static int GetMaxFileSize(string? text)
            {
                if (text is null)
                    return 2 * 1024 * 1024;

                string lower = text.ToLowerInvariant();

                if (lower.Contains("kb"))
                {
                    return int.Parse(lower.Replace("kb", "000"));
                }
                else if (lower.Contains("mb"))
                {
                    return int.Parse(lower.Replace("mb", "000000"));
                }
                else if (lower.Contains("gb"))
                {
                    return int.Parse(lower.Replace("gb", "000000000"));
                }
                else if (lower.Contains("b"))
                {
                    return int.Parse(lower.Replace("b", ""));
                }
                else
                    throw new NotSupportedException("This method requires that you specify the unit like GB, MB, KB, B.");
            }
            static string InitialLogDirectory()
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? @"C:\", "TransactionLogs");
            }
        }

        internal void Start()
        {
            watcher.Reset();
            watcher.Start();
        }

        internal void Stop(string cypher, Dictionary<string, object?>? parameters, string? memberName = null, string? sourceFilePath = null, int sourceLineNumber = 0)
        {
            watcher.Stop();
            if (watcher.ElapsedMilliseconds >= ThresholdInMilliSeconds)
            {
                string? cypherWithArgs = null;
                                
                Config.CustomCypherLogging?.Invoke(cypher, parameters, watcher.ElapsedMilliseconds, memberName, sourceFilePath, sourceLineNumber);

                if (Config.SimpleLogging)
                {
                    if (cypherWithArgs is null)
                        cypherWithArgs = FixArgs(cypher, parameters);

                    if (memberName is null || sourceFilePath is null)
                        LogToFile(string.Format("{0}\t\t\t\t{1}", TimeSpan.FromMilliseconds(watcher.ElapsedMilliseconds), cypherWithArgs));
                    else
                    {
                        LogToFile(string.Format("{0}\t{1}\t{2}\t{3}\t{4}", memberName, sourceFilePath, sourceLineNumber, TimeSpan.FromMilliseconds(watcher.ElapsedMilliseconds), cypherWithArgs));
                    }
                }
            }

            static string FixArgs(string cypher, Dictionary<string, object?>? parameters)
            {
                if (parameters is not null)
                {
                    foreach (var par in parameters)
                    {
                        if (par.Value is IEnumerable val)
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (var value in val)
                            {
                                sb.Append(Serializer.Serialize(value));
                                sb.Append(",");
                            }
                            cypher = cypher.Replace("{" + par.Key + "}", sb.ToString());
                        }
                        else
                            cypher = cypher.Replace("{" + par.Key + "}", Serializer.Serialize(par.Value).Replace("\"", "\'"));
                    }
                }

                return cypher;
            }
        }

        public void Log(string message)
        {
            Config.CustomLogging?.Invoke(message);

            if (Config.SimpleLogging)
                LogToFile(message);
        }
        private void LogToFile(string message)
        {
            if (!Directory.Exists(LogDirectory))
                Directory.CreateDirectory(LogDirectory);
            
            LogFile ??= Path.Combine(LogDirectory, string.Concat("Log_", DateTime.Now.ToString("yyyyMMdd_hhmmss"), ".csv"));

            FileInfo fileInfo = new FileInfo(LogFile);
            if (fileInfo.Exists)
            {
                if (fileInfo.Length >= MaxFileSizeInBytes)
                    LogFile = Path.Combine(LogDirectory, string.Concat("Log_", DateTime.Now.ToString("yyyyMMdd_hhmmss"), ".csv"));
            }
            else
                message = string.Concat("Caller\tFile\tLine\tDuration\tQuery", "\r\n", message);

            lock (FileLock)
            {
                File.AppendAllText(LogFile, string.Concat(message, "\r\n"));
            }
        }
    }
}
