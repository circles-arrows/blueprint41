using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Blueprint41.Build
{
    public static class Generator
    {
        static void Main(string[] args)
        {
            //string file = Path.Combine(@"C:\Users\Glenn\source\repos\circles-arrows\blueprint41\MovieGraph\MovieGraph\MovieGraph\MovieGraph.Model\bin\Debug\net8.0\MovieGraph.Model.dll");
            //Generator.Generate(file, Directory.GetCurrentDirectory());
            bool hasCommandlineArgumentErrors = false;
            Dictionary<string, string> arguments = args.Select(item => item.Split('=')).ToDictionary(item => item[0].TrimStart('-', '/', ' ').TrimEnd(' ').ToLowerInvariant(), item => (item.Length == 1) ? item[0].Trim(' ', '"').ToLowerInvariant() : string.Join("=", item.Skip(1)).Trim(' ', '"'));

            if (Exists("?") || Exists("help"))
            {
                Console.WriteLine("-help    Displays command line argument information");
                Console.WriteLine("-modelPath ");
                Console.WriteLine("-generatePath");
            }
            else
            {
                string modelPath = GetMandatory("modelPath");
                string generatePath = GetMandatory("generatePath");

                if (!hasCommandlineArgumentErrors)
                    Generate(modelPath, generatePath);
            }

            //string file = Path.Combine(@"C:\Users\Glenn\source\repos\circles-arrows\blueprint41\MovieGraph\MovieGraph\MovieGraph\MovieGraph.Model\bin\Debug\netstandard2.0", "MovieGraph.Model.dll");
            //Generate(file);
            Environment.Exit(0);

            string GetMandatory(string argumentName)
            {
                string value = GetOptional(argumentName, null);
                if (value is null)
                {
                    Console.Error.WriteLine($"Argument '{argumentName}' is mandatory.");
                    hasCommandlineArgumentErrors = true;
                }

                return value;
            }
            string GetOptional(string argumentName, string defaultValue)
            {
                if (!arguments.TryGetValue(argumentName.ToLowerInvariant(), out string value))
                    return defaultValue;

                return value;
            }
            bool Exists(string argumentName)
            {
                string value = GetOptional(argumentName, null);
                if (value is null)
                    return false;

                return true;
            }
        }
        public static void DoNothing()
        {

        }
        public static void Generate(string modelDllPath, string generatePath)
        {
            AssemblyLoader.Load(modelDllPath, (Assembly assembly) =>
            {
                Type[] types = GetTypes(assembly);

                foreach ((Type datastoreType, Assembly bp41assembly) in types.Select(type => (type, bp41: GetDatastoreType(type)!)).Where(item => item.bp41 is not null))
                {
                    Type[] bp41types = GetTypes(bp41assembly);
                    Type generatorType = bp41types.First(type => type.FullName == "Blueprint41.DatastoreTemplates.Generator");
                    MethodInfo executeMethod = generatorType.GetMethod("Execute", BindingFlags.Public | BindingFlags.Static)!;

                    MethodInfo executeMethodGeneric = executeMethod.MakeGenericMethod(datastoreType);

                    Type generatorSettingsType = bp41types.First(type => type.FullName == "Blueprint41.DatastoreTemplates.GeneratorSettings");
                    object generatorSettingsInstance = Activator.CreateInstance(generatorSettingsType, generatePath, "Datastore")!;

                    executeMethodGeneric.Invoke(null, new object[] { generatorSettingsInstance });
                }

            });
        }

        private static Assembly? GetDatastoreType(Type? type)
        {
            while (type is not null)
            {
                if (type.FullName == "Blueprint41.DatastoreModel")
                    return type.Assembly;

                type = type.BaseType;
            }
            return null;
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