using System;
using System.Collections.Generic;

namespace Blueprint41.Core
{
    [Serializable]

    public class PersistenceException : ApplicationException
    {
        public PersistenceException() : base() { }
        public PersistenceException(string? message) : base(message) { }
        public PersistenceException(string? message, Exception? innerException) : base(message, innerException) { }
    }

    [Serializable]
    public class DBConcurrencyException : ApplicationException
    {
        public DBConcurrencyException() : base() { }
        public DBConcurrencyException(string? message) : base(message) { }
        public DBConcurrencyException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
