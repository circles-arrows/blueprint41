using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class Extensions
    {
        public static T GetTaskResult<T>(this Task<T> self) => AsyncHelper.RunSync(() => self);
    }
}
