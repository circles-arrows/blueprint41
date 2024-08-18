using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Async.Core
{
    public abstract class RawResult: IEnumerable<RawRecord>
    {
        public abstract IReadOnlyList<string> Keys { get; }
        public abstract RawRecord? Peek();
        public abstract void Consume();
        public abstract RawResultStatistics Statistics();
        public abstract List<RawResultNotification> Notifications();

        public abstract IEnumerator<RawRecord> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected class RawRecordEnumerator<TFrom> : IEnumerator<RawRecord>
        {
            public RawRecordEnumerator(IEnumerator<TFrom> enumerator, Func<TFrom, RawRecord> converter)
            {
                Enumerator = enumerator;
                Converter = converter;
            }
            private IEnumerator<TFrom> Enumerator;
            private Func<TFrom, RawRecord> Converter;

            public RawRecord Current   => Converter.Invoke(Enumerator.Current);
            object IEnumerator.Current => Converter.Invoke(Enumerator.Current);
            public void Dispose()      => Enumerator.Dispose();
            public bool MoveNext()     => Enumerator.MoveNext();
            public void Reset()        => Enumerator.Reset();
        }
    }

    public abstract class RawResultNotification
    {
        public abstract string Code { get; }
        public abstract string Title { get; }
        public abstract string Description { get; }
        public abstract int Offset { get; }
        public abstract int Line { get; }
        public abstract int Column { get; }
        public abstract string Severity { get; }
    }
}
