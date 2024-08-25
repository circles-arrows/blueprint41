using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Async
{
    [Serializable]

    public class PersistenceException : ApplicationException
    {
        protected PersistenceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public PersistenceException() : base(){}
        public PersistenceException(string? message): base(message) { }
        public PersistenceException(string? message, Exception? innerException) : base(message, innerException) { }

    }
}
