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
        private const string NAMESPACE_ARG = "--namespace=";

        static void Main(string[] args)
        {
            bool hasCommandlineArgumentErrors = false;
            Dictionary<string, string> arguments = args.Select(item => item.Split('=')).ToDictionary(item => item[0].TrimStart('-', '/', ' ').TrimEnd(' ').ToLowerInvariant(), item => (item.Length == 1) ? item[0].Trim(' ', '"').ToLowerInvariant() : string.Join("=", item.Skip(1)).Trim(' ', '"'));

            if (Exists("?") || Exists("help"))
            {
                Console.WriteLine("-help    Displays command line argument information");
                Console.WriteLine(MODEL_PATH_ARG);
                Console.WriteLine(GENERATE_PATH_ARG);
                Console.WriteLine(NAMESPACE_ARG);
            }
            else
            {
                string modelPath = null;
                string generatePath = null;
                string namespaceName = "Datastore";

                foreach (var arg in args)
                {
                    if (arg.StartsWith(MODEL_PATH_ARG))
                    {
                        modelPath = Path.GetFullPath(arg[MODEL_PATH_ARG.Length..]);
                    }
                    else if (arg.StartsWith(GENERATE_PATH_ARG))
                    {
                        generatePath = Path.GetFullPath(arg[GENERATE_PATH_ARG.Length..]);
                    }
                    else if (arg.StartsWith(NAMESPACE_ARG))
                    {
                        namespaceName = arg[NAMESPACE_ARG.Length..];
                    }
                }
                if (modelPath == null || generatePath == null)
                {
                    Console.WriteLine($"Please provide both {MODEL_PATH_ARG} and {GENERATE_PATH_ARG} arguments for the file path.");
                    hasCommandlineArgumentErrors = true;
                }

                if (!hasCommandlineArgumentErrors)
                {
                    Console.WriteLine("Generate Task Starting...");
                    Console.WriteLine($"ModelPath: '{modelPath}'");
                    Console.WriteLine($"GeneratePath: '{generatePath}'");
                    Console.WriteLine($"Namespace: '{namespaceName}'");
                    try
                    {
                        Generate(modelPath, generatePath, namespaceName);
                        Console.WriteLine("Generate Task Complete.");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Generate Task Failed.");
                        Console.Error.WriteLine(ex.Message);
                        throw;
                    }
                }
            }
            Console.WriteLine("Generate Task Exiting.");
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
        public static void Generate(string modelDllPath, string generatePath, string namespaceName)
        {
            Console.WriteLine("Load assembly.");
            AssemblyLoader.Load(modelDllPath, (Assembly assembly) =>
            {
                Console.WriteLine($"Load types of {assembly.FullName}");
                Type[] types = GetTypes(assembly);
                Console.WriteLine(string.Concat($"Found {types.Length} types"));
                Console.WriteLine(string.Join('|', types.Select(item => item.Name)));
                foreach ((Type datastoreType, Assembly bp41assembly) in types.Select(type => (type, bp41: GetDatastoreType(type)!)).Where(item => item.bp41 is not null))
                {
                    Console.WriteLine("Load datastore.");
                    Type[] bp41types = GetTypes(bp41assembly);
                    Type generatorType = bp41types.First(type => type.FullName == "Blueprint41.DatastoreTemplates.Generator");
                    MethodInfo executeMethod = generatorType.GetMethod("Execute", BindingFlags.Public | BindingFlags.Static)!;

                    MethodInfo executeMethodGeneric = executeMethod.MakeGenericMethod(datastoreType);

                    Type generatorSettingsType = bp41types.First(type => type.FullName == "Blueprint41.DatastoreTemplates.GeneratorSettings");
                    object generatorSettingsInstance = Activator.CreateInstance(generatorSettingsType, generatePath, namespaceName)!;

                    Console.WriteLine("Execute Generate.");
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
                Console.Error.WriteLine(ex.StackTrace);
                types = ex.Types.Where(type => type is not null).ToArray()!;
            }
            return types;
        }
    }
}