using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Sync
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class VersionAttribute : Attribute
    {
        public VersionAttribute(int major, int minor, int patch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public int Major { get; private set; }
        public int Minor { get; private set; }
        public int Patch { get; private set; }
    }
}
