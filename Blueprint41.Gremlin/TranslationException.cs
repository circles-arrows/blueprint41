using System;

namespace Blueprint41
{
    [Serializable]
    public class TranslationException : ApplicationException
    {
        public TranslationException() : base() { }
        public TranslationException(string message) : base(message) { }
        public TranslationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
