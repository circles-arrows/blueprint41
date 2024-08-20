using System;
using System.Collections.Generic;

namespace Blueprint41.Core
{
    [Serializable]

    public class PersistenceException : ApplicationException
    {
        public PersistenceException() : base(){}
        public PersistenceException(string? message): base(message) { }
        public PersistenceException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
