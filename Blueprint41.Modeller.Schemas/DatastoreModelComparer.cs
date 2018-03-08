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

            byte[] assembly = File.ReadAllBytes(dll);
            Assembly asm = Assembly.Load(assembly);
            Type type = asm.GetTypes().First(x => x.Name == "DatastoreModelComparerImpl");
            return (DatastoreModelComparer)Activator.CreateInstance(type);
        }, true);

        public abstract void GenerateUpgradeScript(modeller model, string storagePath);
    }
}
