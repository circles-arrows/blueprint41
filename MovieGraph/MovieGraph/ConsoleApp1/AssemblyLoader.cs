using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class AssemblyLoader : AssemblyLoadContext
    {
        private AssemblyLoader(string filename) : base(true)
        {
            InitialFolder = Path.GetDirectoryName(filename) ?? throw new FileLoadException("Logical error message here...");
            Resolving += AssemblyResolver;
        }

        public static void Load(string filename, Action<Assembly> action)
        {
            AssemblyLoader loader = new AssemblyLoader(filename);
            try
            {
                Assembly? assembly = loader.LoadAssembly(filename);
                if (assembly is null)
                    throw new FileNotFoundException("Better message here...");

                action?.Invoke(assembly);
            }
            finally
            {
                loader.Resolving -= loader.AssemblyResolver;
                loader.Unload();
            }
        }

        private Assembly? LoadAssembly(string filename)
        {
            if (!File.Exists(filename))
                return null;

            return LoadFromAssemblyPath(filename);
        }
        private Assembly? AssemblyResolver(AssemblyLoadContext context, AssemblyName assemblyName)
        {
            string name= assemblyName.Name;

            string assemblyPath = Path.Combine(InitialFolder, name + ".dll");
            if (name.ToLowerInvariant() == "netstandard")
            {
                assemblyPath = InitialFolder;
                while (assemblyPath != null && assemblyPath != "")
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
            if (assembly != null && (name.ToLowerInvariant() == "netstandard" || assembly.FullName == assemblyName.FullName))
                return assembly;

            //Log.LogMessage(MessageImportance.High, string.Format("### Loading FAILED '{0}' ###", assemblyPath));
            //if (assembly != null)
            //{
            //    Log.LogMessage(MessageImportance.High, string.Format("### Expected '{0}' actual '{1}' ###", eventArgs.Name, assembly.FullName));
            //}
            return null;
        }
        private readonly string InitialFolder;
    }
}
