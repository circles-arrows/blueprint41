using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Blueprint41.Build
{
    public static class Generator
    {
        private const string MODEL_PATH_ARG = "modelPath";
        private const string GENERATE_PATH_ARG = "generatePath";
        private const string NAMESPACE_ARG = "namespace";

        public static void Main(string[] args)
        {
            Dictionary<string, string> parameters = ParseParameters(args);

            string modelPath = null;
            string generatePath = null;
            string namespaceName = parameters.GetValueOrDefault(NAMESPACE_ARG, "Datastore");

            if (parameters.TryGetValue(MODEL_PATH_ARG, out string modelPathValue))
                modelPath = Path.GetFullPath(modelPathValue);

            if (parameters.TryGetValue(GENERATE_PATH_ARG, out string generatePathValue))
                generatePath = Path.GetFullPath(generatePathValue);

            if (modelPath == null || generatePath == null)
            {
                Console.WriteLine($"Please provide both {MODEL_PATH_ARG} and {GENERATE_PATH_ARG} arguments for the file path.");
                throw new InvalidOperationException($"Please provide both {MODEL_PATH_ARG} and {GENERATE_PATH_ARG} arguments for the file path.");
            }


            Console.WriteLine("Generate Task Starting...");
            Console.WriteLine($"ModelPath: '{modelPath}'");
            Console.WriteLine($"GeneratePath: '{generatePath}'");
            Console.WriteLine($"Namespace: '{namespaceName}'");
            try
            {
                Generate(modelPath, generatePath, namespaceName);
                Console.WriteLine("Generate Task Complete");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Generate Task Failed.");
                Console.Error.WriteLine(ex.Message);
                throw;
            }

            Console.WriteLine("Generate Task Exiting.");
            Environment.Exit(0);
        }
        public static void Generate(string modelDllPath, string generatePath, string namespaceName)
        {
            Console.WriteLine("Loading assembly");
            AssemblyLoader.Load(modelDllPath, assembly =>
            {
                Type[] types = GetTypes(assembly);
                if (types.Length == 0)
                    throw new InvalidOperationException($"No types found in assembly '{assembly.FullName}'.");

                foreach ((Type datastoreType, Assembly bp41assembly) in types.Select(type => (type, bp41: GetBlueprint41Assembly(type)!)).Where(item => item.bp41 is not null))
                {
                    Type[] bp41types = GetTypes(bp41assembly);
                    Type generatorType = bp41types.First(type => type.FullName == "Blueprint41.DatastoreTemplates.Generator");
                    MethodInfo executeMethod = generatorType.GetMethod("Execute", BindingFlags.Public | BindingFlags.Static)!;

                    MethodInfo executeMethodGeneric = executeMethod.MakeGenericMethod(datastoreType);

                    Type generatorSettingsType = bp41types.First(type => type.FullName == "Blueprint41.DatastoreTemplates.GeneratorSettings");
                    object generatorSettingsInstance = Activator.CreateInstance(generatorSettingsType, generatePath, namespaceName)!;

                    executeMethodGeneric.Invoke(null, new object[] { generatorSettingsInstance });
                    Console.WriteLine("Generate Executed");
                }
            });
        }

        private static Assembly GetBlueprint41Assembly(Type type)
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
        static Dictionary<string, string> ParseParameters(string[] args)
        {
            var parameters = new Dictionary<string, string>();

            foreach (string arg in args)
            {
                string[] parts = arg.Split('=');

                if (parts.Length == 2)
                {
                    string key = parts[0].TrimStart('-').TrimStart('-');
                    string value = parts[1].Trim('"');

                    parameters[key] = value;
                }
            }

            return parameters;
        }
    }
}