using System;
using System.IO;
using System.Reflection;

namespace Blueprint41.Modeller.Schemas
{
    public abstract class DatastoreModelComparer
    {
        static public DatastoreModelComparer Instance { get { return instance.Value; } }
        static private Lazy<DatastoreModelComparer> instance = new Lazy<DatastoreModelComparer>(delegate()
        {
            string dll = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Blueprint41.Modeller.Compare.dll");
            if (!File.Exists(dll))
                return null;

            Assembly a = Assembly.LoadFile(dll);
            Type t = a.GetType("Blueprint41.Modeller.Schemas.DatastoreModelComparerImpl");

            return (DatastoreModelComparer)Activator.CreateInstance(t);
        }, true);

        public abstract void GenerateUpgradeScript(modeller model, string storagePath);
    }
}
