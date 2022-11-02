using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml;

using Blueprint41.Core;
using HtmlAgilityPack;

namespace Blueprint41.ApocDocumentationParser
{
    public class ApocDocumentation
    {
        public static ApocDocumentation Parse(string version)
        {
            ApocDocumentation documentation = new ApocDocumentation(version);

            using (documentation.client = new HttpClientCache("ApocDocumentation", $"v{documentation.Version}"))
            {
                documentation.ProjectFolder = documentation.client!.ProjectFolder;
                documentation.CacheFolder = documentation.client!.CacheFolder;

                documentation.client.SchedulePage("index", documentation.baseUrl, content =>
                {
                    lock (documentation)
                    {
                        Console.Clear();
                        Console.Write("Parsing Apoc Procedure Documentation.");
                    }

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(content);

                    HashSet<string> duplicates = new HashSet<string>();
                    HtmlNodeCollection rows = doc.DocumentNode.SelectNodes("//table[contains(@class, 'procedures')]/tbody/tr");
                    documentation.totalMethods = rows.Count;

                    foreach (HtmlNode row in rows)
                    {
                        HtmlNode col1 = row.SelectSingleNode("./td[1]");
                        HtmlNode col2 = row.SelectSingleNode("./td[2]");
                        HtmlNode col3 = row.SelectSingleNode("./td[3]");

                        string fullname = col1.SelectSingleNode(".//a[@class = 'page']").InnerText.Trim();
                        string[] parts = fullname.Split('.');
                        string @namespace = string.Join(".", parts.Take(parts.Length - 1));
                        string name = parts.Last();

                        ApocNamespace? apocNamespace = documentation.GetNamespace(@namespace);
                        if (apocNamespace is null)
                        {
                            apocNamespace = new ApocNamespace(documentation, @namespace);
                            documentation.RegisterNamespace(apocNamespace);
                        }

                        if (duplicates.Contains(fullname))
                        {
                            Interlocked.Increment(ref documentation.methodsFinished);
                            continue;
                        }

                        ApocMethod.Build(apocNamespace, name, col1, col2, col3);
                        duplicates.Add(fullname);
                    }
                });
                List<(string key, string url, Exception[] errors)> errors = documentation.client.WaitForPages(pending =>
                {
                    lock (documentation)
                    {
                        Console.Write($"\rParsing Apoc Procedure Documentation {documentation.methodsFinished} of {documentation.totalMethods}.");
                    }
                });

                Console.WriteLine();

                foreach ((string key, string url, Exception ex) error in errors.SelectMany(item => item.errors.Select(error => (item.key, item.url, error))))
                    Console.WriteLine($"{error.ex.GetType().Name}: {error.ex.Message} while loading '{error.key}' ({error.url})");

                documentation.client = null;
                return documentation;
            }
        }

        private ApocDocumentation(string version)
        {
            Version = version;
            baseUrl = $"https://neo4j.com/labs/apoc/{version}/overview/";
            totalMethods = 0;
            methodsFinished = 0;
        
            ProjectFolder = null!;
            CacheFolder = null!;
            client = null;
        }

        public string Version { get; private set; }
        public IReadOnlyCollection<ApocNamespace> Namespaces => m_Namespaces.Values;
        public ApocNamespace? GetNamespace(string name)
        {
            ApocNamespace? apocNamespace;
            m_Namespaces.TryGetValue(name.ToLowerInvariant(), out apocNamespace);
            return apocNamespace;
        }
        internal void RegisterNamespace(ApocNamespace @namespace)
        {
            m_Namespaces.Add(@namespace.Name.ToLowerInvariant(), @namespace);
        }
        private readonly Dictionary<string, ApocNamespace> m_Namespaces = new Dictionary<string, ApocNamespace>();

        public string ProjectFolder { get; private set;}
        public string CacheFolder { get; private set; }

        internal HttpClientCache? client;
        internal readonly string baseUrl;
        internal int totalMethods;
        internal int methodsFinished;

        public void SyncTo(string filename)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(filename);

            XmlNode? root = xml.SelectSingleNode("/apoc");
            if (root is null)
            {
                root = CreateNode(xml, "apoc");
            }
            SetAttr(root, "version", Version);

            var cmpNs = Collection.Compare(Namespaces, xml.SelectNodes("/apoc/namespace")!.Cast<XmlNode>(), item => item.Name, item => item.Attributes?["name"]?.Value ?? "MISSING");
            foreach ((ApocNamespace nsObj, XmlNode? nsNode) in GetMatchedOnNew(cmpNs))
            {
                XmlNode? ns = nsNode;
                if (ns is null)
                {
                    ns = CreateNode(root, "namespace");
                    SetAttr(ns, "name", nsObj.Name);
                }

                var cmpMethods = Collection.Compare(nsObj.Methods, ns.SelectNodes("./method")!.Cast<XmlNode>(), item => item.Name, item => item.Attributes?["name"]?.Value ?? "MISSING");
                foreach ((ApocMethod methodObj, XmlNode? methodNode) in GetMatchedOnNew(cmpMethods))
                {
                    XmlNode? method = methodNode;
                    if (method is null)
                    {
                        method = CreateNode(ns, "method");
                        SetAttr(method, "name", methodObj.Name);
                    }
                    SetAttr(method, "description", methodObj.Description);
                    SetAttr(method, "type", methodObj.Type);
                    SetAttr(method, "release", methodObj.Release);

                    var cmpInParams = Collection.Compare(methodObj.InputParameters, method.SelectNodes("./input-parameter")!.Cast<XmlNode>(), item => item.Name, item => item.Attributes?["name"]?.Value ?? "MISSING");
                    foreach ((ApocParameter paramObj, XmlNode? paramNode) in GetMatchedOnNew(cmpInParams))
                    {
                        XmlNode? param = paramNode;
                        if (param is null)
                        {
                            param = CreateNode(method, "input-parameter");
                            SetAttr(param, "name", paramObj.Name);
                        }
                        SetAttr(param, "type", paramObj.ParameterType.Name);
                        SetAttr(param, "neo4j-type", paramObj.ParameterType.Neo4jType.ToString());
                        SetAttr(param, "list-type", paramObj.ParameterType.ListType.ToString());
                        SetAttr(param, "default-value", paramObj.DefaultValue);
                    }
                    Remove(cmpInParams.Added);

                    var cmpOutParams = Collection.Compare(methodObj.OutputParameters, method.SelectNodes("./output-parameter")!.Cast<XmlNode>(), item => item.Name, item => item.Attributes?["name"]?.Value ?? "MISSING");
                    foreach ((ApocParameter paramObj, XmlNode? paramNode) in GetMatchedOnNew(cmpOutParams))
                    {
                        XmlNode? param = paramNode;
                        if (param is null)
                        {
                            param = CreateNode(method, "output-parameter");
                            SetAttr(param, "name", paramObj.Name);
                        }
                        SetAttr(param, "type", paramObj.ParameterType.Name);
                        SetAttr(param, "neo4j-type", paramObj.ParameterType.Neo4jType.ToString());
                        SetAttr(param, "list-type", paramObj.ParameterType.ListType.ToString());
                    }
                    Remove(cmpOutParams.Added);

                    var cmpWarnings = Collection.Compare(methodObj.Warnings, method.SelectNodes("./warning")!.Cast<XmlNode>(), item => item, item => item.InnerText) ;
                    foreach ((string waringStr, XmlNode? warningNode) in GetMatchedOnNew(cmpWarnings))
                    {
                        XmlNode? warning = warningNode;
                        if (warning is null)
                        {
                            warning = CreateNode(method, "warning");
                            warning.InnerText = waringStr;
                        }
                    }
                    Remove(cmpWarnings.Added);
                }
                Remove(cmpMethods.Added);
            }
            Remove(cmpNs.Added);

            xml.Save(filename);

            List<(TLeft, TRight?)> GetMatchedOnNew<TLeft, TRight>((List<TLeft> Removed, List<(TLeft, TRight)> Matched, List<TRight> Added) compare)
            {
                List<(TLeft, TRight?)> result = new List<(TLeft, TRight?)>(cmpNs.Matched.Count + cmpNs.Removed.Count);
                result.AddRange(compare.Removed.Select(item => (item, default(TRight?))));
                result.AddRange(compare.Matched.Select(item => (item.Item1, (TRight?)item.Item2)));
                return result;
            }
            void Remove(List<XmlNode> remove)
            {
                foreach (XmlNode node in remove)
                    node.ParentNode!.RemoveChild(node);
            }

            XmlNode CreateNode(XmlNode parent, string name)
            {
                XmlNode node = xml.CreateElement(name);
                parent.AppendChild(node);
                return node;
            }
            void SetAttr(XmlNode parent, string name, string? value)
            {
                XmlAttribute? attr = parent.Attributes[name];
                if (value is not null)
                {
                    if (attr is null)
                    {
                        attr = xml.CreateAttribute(name);
                        parent.Attributes.Append(attr);
                    }
                    attr.Value = value;
                }
                else
                {
                    if (attr is not null)
                    {
                        parent.Attributes.Remove(attr);
                    }
                }
            }
        }
    }
}
