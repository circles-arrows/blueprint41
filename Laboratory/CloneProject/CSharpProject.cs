using System;
using System.Collections.Generic;
using System.IO;

namespace Laboratory.CloneProject
{
    public static class CSharpProject
    {
        public static void Test()
        {
            Clone(
                source: FindProjectFolder("Blueprint41"),
                target: FindProjectFolder("Blueprint41.Async"),
                renames: new NamespaceRename(@"Blueprint41", @"Blueprint41.Async")
            );

            Clone(
                    source: FindProjectFolder("Blueprint41"),
                    target: FindProjectFolder("Blueprint41.Sync"),
                    renames: new NamespaceRename(@"Blueprint41", @"Blueprint41.Sync")
                );
        }

        public static string FindProjectFolder(string projectName)
        {
            string? folder = Path.GetDirectoryName(typeof(CSharpProject).Assembly.Location);
            while (folder is not null)
            {
                string projectFolder = Path.Combine(folder, projectName);
                if (Directory.Exists(projectFolder) && File.Exists(Path.Combine(projectFolder, $"{projectName}.csproj")))
                    return projectFolder;

                folder = Path.GetDirectoryName(folder);
            }

            throw new FileNotFoundException($"{projectName}.csproj");
        }

        public static void Clone(string source, string target, params NamespaceRename[] renames)
        {
            ReadOnlySpan<char> trimElements = new ReadOnlySpan<char>([' ', '\t']);

            foreach (string sourceFile in Directory.GetFiles(source, "*.*", SearchOption.TopDirectoryOnly))
            {
                string? org = null, line = null;
                string filename = Path.GetFileName(sourceFile)!;
                string extension = Path.GetExtension(sourceFile)?.ToLowerInvariant();

                // exclude files without a name or files starting with a dot (like .gitignore)
                if (string.IsNullOrEmpty(filename) || filename.StartsWith('.'))
                    continue;

                string targetFile = Path.Combine(target, filename);

                Console.WriteLine($"\t{filename}");

                if (extension == ".cs" || extension == ".tt")
                {
                    string[] content = File.ReadAllLines(sourceFile);

                    for (int i = 0; i < content.Length; i++)
                    {
                        line = content[i];

                        bool isChanged = false;
                        foreach (NamespaceRename rename in renames)
                        {
                            isChanged = rename.Apply(ref line);
                            if (isChanged)
                            {
                                content[i] = line;
                                break;
                            }
                        }
                    }

                    File.WriteAllLines(targetFile, content);
                }
                else if (extension == ".txt" || extension == ".md")
                {
                    string content = File.ReadAllText(sourceFile);
                    File.WriteAllText(targetFile, content);
                }

            }

            foreach (string sourcePath in Directory.GetDirectories(source, "*", SearchOption.TopDirectoryOnly))
            {
                string folder = Path.GetFileName(sourcePath)!;

                // exclude empty folders or folders starting with a dot (like .vs)
                if (string.IsNullOrEmpty(folder) || folder.StartsWith('.'))
                    continue;

                // exclude bin and obj folders
                if (folder.Equals("bin", StringComparison.OrdinalIgnoreCase) || folder.Equals("obj", StringComparison.OrdinalIgnoreCase))
                    continue;

                string targetPath = Path.Combine(target, folder);

                Console.WriteLine($"Folder: {sourcePath}");

                Clone(sourcePath, targetPath, renames);
            }
        }
    }
}
