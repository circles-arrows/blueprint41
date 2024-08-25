using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Laboratory.CloneProject
{
    public record struct NamespaceRename
    {
        public NamespaceRename(string @namespace, string renameTo)
        {
            Namespace = @namespace;
            RenameTo = renameTo;

            _regex = new Regex(@"^[ \t]*((?<start>namespace[ \t]+)(?:BLUEPRINT41)(?<end>[._0-9A-Za-z]*)|(?<start>using([ \t]+static)?([ \t]+[_0-9A-Za-z]+[_0-9A-Za-z]*[ \t]*=)?[ \t]*)(?:BLUEPRINT41)(?<end>[._0-9A-Za-z]*[ \t]*;)|(?<start>#line [0-9]+ ""[:\\. \-_0-9A-Za-z]*)(?:BLUEPRINT41)(?<end>[:\\. \-_0-9A-Za-z]*"")|(?<start><#@[ \t]*import[ \t]+namespace[ \t]*=[ \t]*"")(?:BLUEPRINT41)(?<end>[._0-9A-Za-z]*""[ \t]*#>))[ \t]*$".Replace("BLUEPRINT41", @namespace.Replace(".", @"\.")), RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.NonBacktracking);
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
