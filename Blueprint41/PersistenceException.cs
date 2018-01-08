using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41
{
    [Serializable]

    public class PersistenceException : ApplicationException
    {
        public PersistenceException() : base(){}
        public PersistenceException(string message): base(message) { }
        public PersistenceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
