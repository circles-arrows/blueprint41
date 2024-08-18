using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Laboratory
{
    public static partial class Program
    {
        public static void Main(string[] args)
        {
            //AsyncLocalStorageTest.Test();

            CopySourceCode(
                    source: @"C:\_CirclesArrows\blueprint41\Blueprint41",
                    target: @"C:\_CirclesArrows\blueprint41\Blueprint41.Async",
                    renames: new NamespaceRename(@"Blueprint41", @"Blueprint41.Async")
                );

            CopySourceCode(
                source: @"C:\_CirclesArrows\blueprint41\Blueprint41",
                target: @"C:\_CirclesArrows\blueprint41\Blueprint41.Sync",
                renames: new NamespaceRename(@"Blueprint41", @"Blueprint41.Sync")
            );



            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey(true);
        }

        public static void CopySourceCode(string source, string target, params NamespaceRename[] renames)
        {
            ReadOnlySpan<char> trimElements = new ReadOnlySpan<char>([ ' ', '\t']);

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

                CopySourceCode(sourcePath, targetPath, renames);
            }
        }
    }

    public record struct NamespaceRename
    {
        public NamespaceRename(string @namespace, string renameTo) 
        { 
            Namespace = @namespace;
            RenameTo = renameTo;

            _regex = new Regex(@"^[ \t]*((?<start>namespace[ \t]+)(?:BLUEPRINT41)(?<end>[._0-9A-Za-z]*)|(?<start>using([ \t]+static)?([ \t]+[_0-9A-Za-z]+[_0-9A-Za-z]*[ \t]*=)?[ \t]*)(?:BLUEPRINT41)(?<end>[._0-9A-Za-z]*[ \t]*;)|(?<start>#line 16 ""[:\\. \-_0-9A-Za-z]*)(?:BLUEPRINT41)(?<end>[:\\. \-_0-9A-Za-z]*"")|(?<start><#@[ \t]*import[ \t]+namespace[ \t]*=[ \t]*"")(?:BLUEPRINT41)(?<end>[._0-9A-Za-z]*""[ \t]*#>))[ \t]*$".Replace("BLUEPRINT41", @namespace.Replace(".", @"\.")), RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.NonBacktracking);
            _replace = @$"${{start}}{renameTo}${{end}}";
        }

        public string Namespace { get; private set; }
        public string RenameTo { get; private set; }

        public bool Apply(ref string line)
        {
            string before = line;
            line = _regex.Replace(line, _replace);

            return !ReferenceEquals(before, line);
        }

        private readonly Regex _regex;
        private readonly string _replace;
    }
}
