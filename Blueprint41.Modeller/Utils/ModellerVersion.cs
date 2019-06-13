using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller.Utils
{
    internal class ModellerVersion : IComparable<ModellerVersion>
    {
        public static ModellerVersion CurrentVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string[] version = fvi.FileVersion.Split('.');

            long.TryParse(version[0], out long major);
            long.TryParse(version[1], out long minor);
            long.TryParse(version[2], out long patch);

            return new ModellerVersion(major, minor, patch, null);
        }

        public static ModellerVersion Empty => new ModellerVersion(0, 0, 0, null);

        public ModellerVersion(long major, long minor, long patch, string downloadUrl)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            DownloadUrl = downloadUrl;
        }

        public long Major { get; private set; }
        public long Minor { get; private set; }
        public long Patch { get; private set; }
        public string DownloadUrl { get; private set; }

        public bool IsUpdatedVersion()
        {
            ModellerVersion current = ModellerVersion.CurrentVersion();

            if (Major < current.Major)
                return false;

            if (Major > current.Major)
                return true;

            if (Minor < current.Minor)
                return false;

            if (Minor > current.Minor)
                return true;

            if (Patch > current.Patch)
                return true;

            return false;
        }

        int IComparable<ModellerVersion>.CompareTo(ModellerVersion other)
        {
            int result = this.Major.CompareTo(other.Major);
            if (result != 0)
                return result;

            result = this.Minor.CompareTo(other.Minor);
            if (result != 0)
                return result;

            return this.Patch.CompareTo(other.Patch);
        }

        public override bool Equals(object obj)
        {
            ModellerVersion other = obj as ModellerVersion;
            if ((object)other == null)
                return false;

            return (Patch == other.Patch && Minor == other.Minor && Major == other.Major);
        }

        public override int GetHashCode()
        {
            return Patch.GetHashCode() ^ ROL(Minor.GetHashCode(), 10) ^ ROL(Major.GetHashCode(), 20);

            int ROL(int value, int bits)
            {
                uint val = (uint)value;
                return (int)((val << bits) | (val >> (32 - bits)));
            }
        }

        public override string ToString()
        {
            return $"v{Major}.{Minor}.{Patch}";
        }
    }
}
