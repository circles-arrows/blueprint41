using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public interface ILogger
    {
        void Error(Exception cause, string message, params object[] args);
        void Warn(Exception cause, string message, params object[] args);
        void Info(string message, params object[] args);
        void Debug(string message, params object[] args);
        void Trace(string message, params object[] args);
        bool IsTraceEnabled();
        bool IsDebugEnabled();
    }
}
