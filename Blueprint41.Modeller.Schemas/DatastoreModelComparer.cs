using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Blueprint41.Modeller.Schemas
{
    public abstract class DatastoreModelComparer
    {
        public static DatastoreModelComparer Instance { get { return instance.Value; } }
        private static Lazy<DatastoreModelComparer> instance = new Lazy<DatastoreModelComparer>(delegate ()
        {
            string dll = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Blueprint41.Modeller.Compare.dll");
            if (!File.Exists(dll))
                return null;

            Type type = AssemblyLoader.GetType(dll, "DatastoreModelComparerImpl");
            return (DatastoreModelComparer)Activator.CreateInstance(type);
        }, true);

        public abstract void GenerateUpgradeScript(modeller model, string storagePath);
    }

    public static class AssemblyLoader
    {
        public static Assembly Load(string assemblyFile)
        {
            byte[] assemblyBytes = File.ReadAllBytes(assemblyFile);
            return Assembly.Load(assemblyBytes);
        }

        public static Type GetType(string assemblyFile, string typeName)
        {
            Assembly assembly = Load(assemblyFile);
            return assembly.GetTypes().First(x => x.Name == typeName || x.BaseType.Name == typeName);
        }

        public static Assembly Load(string assemblyFile, string pdbFile)
        {
            byte[] assemblyBytes = File.ReadAllBytes(assemblyFile);
            byte[] pdbBytes = File.ReadAllBytes(pdbFile);
            return Assembly.Load(assemblyBytes, pdbBytes);
        }
        public static Type GetType(string assemblyFile, string pdbFile, string typeName)
        {
            Assembly assembly = Load(assemblyFile, pdbFile);
            return assembly.GetTypes().First(x => x.Name == typeName);
        }

    }
}
