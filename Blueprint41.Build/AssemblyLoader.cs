using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Blueprint41.Build
{
    public class AssemblyLoader : AssemblyLoadContext
    {
        private readonly string initialFolder;

        private AssemblyLoader(string filename) : base(isCollectible: true)
        {
            initialFolder = Path.GetDirectoryName(filename) ?? throw new DirectoryNotFoundException($"The directory for '{filename}' was not found.");
            Resolving += OnResolving;
        }

        public static void Load(string filename, Action<Assembly> action)
        {
            var loader = new AssemblyLoader(filename);
            try
            {
                var assembly = loader.LoadFromAssemblyPath(filename);
                action?.Invoke(assembly);
            }
            finally
            {
                loader.Resolving -= loader.OnResolving;
                loader.Unload();
            }
        }

        private Assembly OnResolving(AssemblyLoadContext context, AssemblyName assemblyName)
        {
            var assemblyPath = ResolveAssemblyPath(assemblyName);
            return assemblyPath != null ? LoadFromAssemblyPath(assemblyPath) : null;
        }

        private string ResolveAssemblyPath(AssemblyName assemblyName)
        {
            if (assemblyName.Name.Equals("netstandard", StringComparison.InvariantCultureIgnoreCase))
            {
                return FindNetStandardAssemblyPath();
            }

            var path = Path.Combine(initialFolder, $"{assemblyName.Name}.dll");
            return File.Exists(path) ? path : null;
        }

        private string FindNetStandardAssemblyPath()
        {
            var path = Path.Combine(initialFolder, @"DLLs\netstandard\framework\netstandard.dll");
            return File.Exists(path) ? path : FindInParentDirectories(initialFolder);
        }

        private static string FindInParentDirectories(string startDirectory)
        {
            var directory = startDirectory;
            string path;
            while (!string.IsNullOrEmpty(directory))
            {
                path = Path.Combine(directory, @"DLLs\netstandard\framework\netstandard.dll");
                if (File.Exists(path))
                {
                    return path;
                }
                directory = Path.GetDirectoryName(directory);
            }
            return null;
        }
    }
}
