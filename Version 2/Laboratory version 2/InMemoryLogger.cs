using System;
using System.Collections.Generic;
using System.Linq;

using Blueprint41.Driver;

namespace Laboratory
{
    public class InMemoryLogger : ILogger
    {
        public InMemoryLogger(bool debug, bool trace, int maxEntries = 1000)
        {
            _debug = debug;
            _trace = trace;
            _maxEntries = maxEntries;
        }

        public void Debug(string message, params object[] args)
        {
            if (_debug)
                Add(new LogEntry(LogEntryType.Debug, message, args));
        }
        public void Error(Exception cause, string message, params object[] args)
        {
            Add(new LogEntry(LogEntryType.Error, message, args, cause));
        }
        public void Info(string message, params object[] args)
        {
            Add(new LogEntry(LogEntryType.Info, message, args));
        }
        public void Trace(string message, params object[] args)
        {
            if (_trace)
                Add(new LogEntry(LogEntryType.Trace, message, args));
        }
        public void Warn(Exception cause, string message, params object[] args)
        {
            Add(new LogEntry(LogEntryType.Warn, message, args, cause));
        }

        public bool IsDebugEnabled() => _debug;
        private readonly bool _debug;

        public bool IsTraceEnabled() => _trace;
        private readonly bool _trace;

        public IReadOnlyList<LogEntry> GetLog()
        {
            lock (_log)
            {
                return _log.ToList();
            }
        }
        private void Add(LogEntry entry)
        {
            lock (_log)
            {
                _log.AddLast(entry);
                while (_log.Count > _maxEntries)
                    _log.RemoveFirst();
            }
        }
        private readonly LinkedList<LogEntry> _log = new LinkedList<LogEntry>();
        private readonly int _maxEntries;
    }
    public class LogEntry
    {
        internal LogEntry(LogEntryType type, string message, object[] arguments, Exception? cause = null)
        {
            Type = type;
            Message = message;
            Arguments = arguments;
            Cause = cause;
        }

        public LogEntryType Type { get; private set; }
        public string Message { get; private set; }
        public object[] Arguments { get; private set; }
        public Exception? Cause { get; private set; }
    }
    public enum LogEntryType
    {
        Debug,
        Error,
        Info,
        Trace,
        Warn,
    }
}
