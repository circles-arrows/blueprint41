using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Blueprint41.Modeller.Schemas
{
    public abstract class ModuleLoader
    {
        public static ModuleLoader GetModule(string moduleName)
        {
            string dll = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules", moduleName, $"Blueprint41.Modeller.{moduleName}.dll");
            if (!File.Exists(dll))
                return null;

            string pdb = dll.Replace("dll", "pdb");
            if (!File.Exists(pdb))
                throw new FileNotFoundException($"File '{pdb}' not found.");

            Type type = AssemblyLoader.GetType(moduleName, dll, pdb, "ModuleLoaderImpl");
            if (type == null)
                throw new FileNotFoundException($"File '{dll}' is not a Blueprint41 extension module.");

            return (ModuleLoader)Activator.CreateInstance(type);
        }

        public abstract Task RunModule(modeller model, string storagePath);
        public abstract bool Installed { get; }
    }

    //public abstract class DatastoreModelComparer
    //{
    //    public static DatastoreModelComparer Instance { get { return instance.Value; } }
    //    private static Lazy<DatastoreModelComparer> instance = new Lazy<DatastoreModelComparer>(delegate ()
    //    {
    //        string dll = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Blueprint41.Modeller.Compare.dll");
    //        if (!File.Exists(dll))
    //            return null;

    //        string pdb = dll.Replace("dll", "pdb");
    //        if (!File.Exists(pdb))
    //            throw new FileNotFoundException($"File '{pdb}' not found.");

    //        Type type = AssemblyLoader.GetType(dll, pdb, "DatastoreModelComparerImpl");
    //        return (DatastoreModelComparer)Activator.CreateInstance(type);
    //    }, true);

    //    public abstract void GenerateUpgradeScript(modeller model, string storagePath);
    //}
    //public abstract class DatastoreModelDocumentGenerator
    //{
    //    public static DatastoreModelDocumentGenerator Instance { get { return instance.Value; } }
    //    private static Lazy<DatastoreModelDocumentGenerator> instance = new Lazy<DatastoreModelDocumentGenerator>(delegate ()
    //    {
    //        string dll = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Blueprint41.Modeller.Document.dll");
    //        if (!File.Exists(dll))
    //            return null;

    //        string pdb = dll.Replace("dll", "pdb");
    //        if (!File.Exists(pdb))
    //            throw new FileNotFoundException($"File '{pdb}' not found.");

    //        Type type = AssemblyLoader.GetType(dll, pdb, "DocumentGeneratorImpl");
    //        return (DatastoreModelDocumentGenerator)Activator.CreateInstance(type);
    //    }, true);

    //    public abstract Task ShowAndGenerateDocument(modeller model);
    //    public abstract bool GraphVizInstalled { get; }
    //    public abstract string GraphVizPath { get; set; }
    //}

    public static class AssemblyLoader
    {
        public static Assembly LoadAssemblyAndPdbByBytes(string assemblyFile, string pdbFile)
        {
            byte[] assemblyBytes = File.ReadAllBytes(assemblyFile);
            byte[] pdbBytes = File.ReadAllBytes(pdbFile);
            return Assembly.Load(assemblyBytes, pdbBytes);
        }
        public static Type GetType(string moduleName, string assemblyFile, string pdbFile, string typeName)
        {
            ResolveEventHandler eventHandler = delegate (object sender, ResolveEventArgs args)
            {
                string filename = args.Name;
                if (filename.Contains(","))
                    filename = filename.Split(',')[0] + ".dll";

                string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules", moduleName, filename);
                if (File.Exists(file))
                    return Assembly.LoadFile(file);

                return null;
            };

            AppDomain.CurrentDomain.AssemblyResolve += eventHandler;

            Assembly assembly = LoadAssemblyAndPdbByBytes(assemblyFile, pdbFile);
            Type type = GetType(assembly, typeName);

            AppDomain.CurrentDomain.AssemblyResolve -= eventHandler;

            return type;
        }

        public static Type GetType(Assembly assembly, string typeName)
        {
            return assembly?.GetTypes().FirstOrDefault(x => x.Name == typeName || x.BaseType?.Name == typeName);
        }
    }
}
