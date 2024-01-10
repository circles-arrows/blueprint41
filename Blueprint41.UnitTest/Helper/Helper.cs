using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.UnitTest
{
    internal class Threadsafe
    {
        public static T Assign<T>(ref T variable, Func<T> factoryMethod)
        {
            if (variable is null)
            {
                lock (typeof(SyncLock<T>))
                {
                    if (variable is null)
                        variable = factoryMethod.Invoke();
                }
            }
            return variable;
        }

        private class SyncLock<T>
        {
        }
    }
}
