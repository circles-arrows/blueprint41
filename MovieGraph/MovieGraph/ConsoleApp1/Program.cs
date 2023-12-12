// See https://aka.ms/new-console-template for more information
using System.Reflection;

namespace ConsoleApp1
{
    static class Program
    {
        static void Main(string[] args)
        {
            string file = Path.Combine(@"C:\Users\Glenn\source\repos\circles-arrows\blueprint41\MovieGraph\MovieGraph\MovieGraph\MovieGraph.Model\bin\Debug\netstandard2.0", "MovieGraph.Model.dll");

            AssemblyLoader.Load(file, delegate(Assembly assembly)
            {
                Type[] types = GetTypes(assembly);

                foreach ((Type datastoreType, Assembly bp41assembly) in types.Select(type => (type, bp41:GetDatastoreType(type)!)).Where(item => item.bp41 is not null))
                {
                    Type[] bp41types = GetTypes(bp41assembly);
                    Type generatorType = bp41types.First(type => type.FullName == "Blueprint41.DatastoreTemplates.Generator");
                    MethodInfo executeMethod = generatorType.GetMethod("Execute", BindingFlags.Public | BindingFlags.Static);

                    MethodInfo executeMethodGeneric = executeMethod.MakeGenericMethod(datastoreType);

                    Type generatorSettingsType = bp41types.First(type => type.FullName == "Blueprint41.DatastoreTemplates.GeneratorSettings");
                    object generatorSettingsInstance = Activator.CreateInstance(generatorSettingsType, Directory.GetCurrentDirectory(), "Domain.Data");

                    executeMethodGeneric.Invoke(null, new object[] { generatorSettingsInstance });



                }

            });

            static Assembly? GetDatastoreType(Type? type)
            {
                while (type is not null)
                {
                    if (type.FullName == "Blueprint41.DatastoreModel")
                        return type.Assembly;

                    type = type.BaseType;
                }
                return null;
            }
        }

        static Type[] GetTypes(Assembly assembly)
        {
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                types = ex.Types.Where(type => type is not null).ToArray()!;
            }
            return types;
        }
    }
}



//Generator.Execute<Datastore>(
//        new GeneratorSettings(
//            Directory.GetCurrentDirectory(),
//            "Domain.Data"
//        )
//    );