using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Blueprint41.Build
{
    public class AssemblyLoader : AssemblyLoadContext
    {
        private AssemblyLoader(string filename) : base(true)
        {
            InitialFolder = Path.GetDirectoryName(filename) ?? throw new DirectoryNotFoundException($"Directory '{filename}' not found.");
            Resolving += AssemblyResolver;
        }

        public static void Load(string filename, Action<Assembly> action)
        {
            AssemblyLoader loader = new(filename);
            try
            {
                Assembly assembly = loader.LoadAssembly(filename);
                if (assembly is null)
                    throw new FileNotFoundException($"Failed to load datastore assembly from path '{filename}'.");

                action?.Invoke(assembly);
            }
            finally
            {
                loader.Resolving -= loader.AssemblyResolver;
                loader.Unload();
            }
        }

        private Assembly LoadAssembly(string filename)
        {
            if (!File.Exists(filename))
                return null;

            return LoadFromAssemblyPath(filename);
        }
        private Assembly AssemblyResolver(AssemblyLoadContext context, AssemblyName assemblyName)
        {
            string name = assemblyName.Name;

            string assemblyPath = Path.Combine(InitialFolder, name + ".dll");
            if (name.Equals("netstandard", StringComparison.InvariantCultureIgnoreCase))
            {
                assemblyPath = InitialFolder;
                while (!string.IsNullOrEmpty(assemblyPath))
                {
                    if (File.Exists(Path.Combine(assemblyPath, @"DLLs\netstandard\framework\netstandard.dll")))
                    {
                        assemblyPath = Path.Combine(assemblyPath, @"DLLs\netstandard\framework\netstandard.dll");
                        break;
                    }
                    assemblyPath = Path.GetDirectoryName(assemblyPath);
                }
            }

            Assembly assembly = LoadAssembly(assemblyPath);
            if (assembly != null && (name.Equals("netstandard", StringComparison.InvariantCultureIgnoreCase) || assembly.FullName == assemblyName.FullName))
                return assembly;

            return null;
        }


        private readonly string InitialFolder;
    }
}
