using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Blueprint41.Build
{
    public static class Generator
    {
        private const string MODEL_PATH_ARG = "--modelPath=";
        private const string GENERATE_PATH_ARG = "--generatePath=";

        static void Main(string[] args)
        {
            //string file = Path.Combine(@"C:\Users\Glenn\source\repos\circles-arrows\blueprint41\MovieGraph\MovieGraph\MovieGraph\MovieGraph.Model\bin\Debug\net8.0\MovieGraph.Model.dll");
            //Generator.Generate(file, Directory.GetCurrentDirectory());
            bool hasCommandlineArgumentErrors = false;
            Dictionary<string, string> arguments = args.Select(item => item.Split('=')).ToDictionary(item => item[0].TrimStart('-', '/', ' ').TrimEnd(' ').ToLowerInvariant(), item => (item.Length == 1) ? item[0].Trim(' ', '"').ToLowerInvariant() : string.Join("=", item.Skip(1)).Trim(' ', '"'));

            if (Exists("?") || Exists("help"))
            {
                Console.WriteLine("-help    Displays command line argument information");
                Console.WriteLine(MODEL_PATH_ARG);
                Console.WriteLine(GENERATE_PATH_ARG);
            }
            else
            {
                string modelPath = null;
                string generatePath = null;

                foreach (var arg in args)
                {
                    if (arg.StartsWith(MODEL_PATH_ARG))
                    {
                        modelPath = arg[MODEL_PATH_ARG.Length..];
                    }
                    else if (arg.StartsWith(GENERATE_PATH_ARG))
                    {
                        generatePath = arg[GENERATE_PATH_ARG.Length..];
                    }
                }
                if (modelPath == null || generatePath == null)
                {
                    Console.WriteLine($"Please provide both {MODEL_PATH_ARG} and {GENERATE_PATH_ARG} arguments for the file path.");
                    return;
                }

                if (!hasCommandlineArgumentErrors)
                    Generate(modelPath, generatePath);
            }

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
                return value is not null;
            }
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

        private static Assembly GetDatastoreType(Type? type)
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