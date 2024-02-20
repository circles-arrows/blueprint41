using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace Blueprint41.Build
{
    public static class Generator
    {
        private const string ModelPathArg = "modelPath";
        private const string GeneratePathArg = "generatePath";
        private const string NamespaceArg = "namespace";

        public static void Main(string[] args)
        {
            var parameters = ParseParameters(args);
            var modelPath = GetFullPath(parameters, ModelPathArg);
            var generatePath = GetFullPath(parameters, GeneratePathArg);
            var namespaceName = parameters.GetValueOrDefault(NamespaceArg, "Datastore");

            ValidatePaths(modelPath, generatePath);
            ValidateAndWriteHash(modelPath, generatePath);

            Console.WriteLine("Generate Task Starting...");
            Console.WriteLine($"ModelPath: '{modelPath}'");
            Console.WriteLine($"GeneratePath: '{generatePath}'");
            Console.WriteLine($"Namespace: '{namespaceName}'");

            GenerateCode(modelPath, generatePath, namespaceName);

            Console.WriteLine("Generate Task Exiting.");
            Environment.Exit(0);
        }

        private static void ValidateAndWriteHash(string modelPath, string generatePath)
        {
            var hashFilePath = Path.Combine(generatePath, "currentModelHash");
            string existingHash = File.Exists(hashFilePath) ? File.ReadAllText(hashFilePath) : null;
            string currentHash = ComputeSha256Hash(modelPath);

            if (currentHash != existingHash)
            {
                File.WriteAllText(hashFilePath, currentHash);
            }
        }

        private static void GenerateCode(string modelDllPath, string generatePath, string namespaceName)
        {
            try
            {
                Console.WriteLine("Loading assembly");
                AssemblyLoader.Load(modelDllPath, assembly =>
                {
                    var types = GetTypes(assembly);
                    if (types.Length == 0)
                        throw new InvalidOperationException($"No types found in assembly '{assembly.FullName}'.");

                    foreach (var type in types)
                    {
                        var bp41Assembly = GetBlueprint41Assembly(type);
                        if (bp41Assembly != null)
                        {
                            ExecuteGeneration(bp41Assembly, type, generatePath, namespaceName);
                        }
                    }
                });
                Console.WriteLine("Generate Task Complete");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Generate Task Failed.");
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        private static void ExecuteGeneration(Assembly bp41Assembly, Type datastoreType, string generatePath, string namespaceName)
        {
            var bp41Types = GetTypes(bp41Assembly);
            var generatorType = bp41Types.First(type => type.FullName == "Blueprint41.DatastoreTemplates.Generator");
            var executeMethod = generatorType.GetMethod("Execute", BindingFlags.Public | BindingFlags.Static);

            if (executeMethod != null)
            {
                var executeMethodGeneric = executeMethod.MakeGenericMethod(datastoreType);
                var generatorSettingsType = bp41Types.First(type => type.FullName == "Blueprint41.DatastoreTemplates.GeneratorSettings");
                var generatorSettingsInstance = Activator.CreateInstance(generatorSettingsType, generatePath, namespaceName);

                executeMethodGeneric.Invoke(null, new[] { generatorSettingsInstance });
                Console.WriteLine("Generation Executed");
            }
        }
        private static Assembly GetBlueprint41Assembly(Type type)
        {
            // Traverse the inheritance hierarchy to find the type that matches the target.
            while (type != null)
            {
                // Check if the current type is the one we're looking for.
                if (type.FullName == "Blueprint41.DatastoreModel")
                {
                    return type.Assembly;
                }

                // Move up the inheritance chain.
                type = type.BaseType;
            }

            // Return null if the specified base type is not found in the inheritance hierarchy.
            return null;
        }

        private static Type[] GetTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                Console.Error.WriteLine(ex.StackTrace);
                return ex.Types.Where(type => type != null).ToArray();
            }
        }

        private static Dictionary<string, string> ParseParameters(string[] args) => args
            .Select(arg => arg.Split('='))
            .Where(parts => parts.Length == 2)
            .ToDictionary(parts => parts[0].TrimStart('-'), parts => parts[1].Trim('"'), StringComparer.OrdinalIgnoreCase);

        private static string GetFullPath(Dictionary<string, string> parameters, string key) =>
            parameters.TryGetValue(key, out var value) ? Path.GetFullPath(value) : null;

        private static void ValidatePaths(string modelPath, string generatePath)
        {
            if (string.IsNullOrWhiteSpace(modelPath) || string.IsNullOrWhiteSpace(generatePath))
            {
                throw new InvalidOperationException($"Both {ModelPathArg} and {GeneratePathArg} arguments are required.");
            }
        }

        static string ComputeSha256Hash(string filePath)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    // Compute the SHA256 hash of the file
                    byte[] hashValue = sha256.ComputeHash(fileStream);

                    // Convert hash bytes to hex string
                    return BitConverter.ToString(hashValue).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
