using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using HtmlAgilityPack;

namespace Blueprint41.ApocDocumentationParser
{
    public class ApocMethod
    {
        internal static void Build(ApocNamespace parent, string name, HtmlNode col1, HtmlNode col2, HtmlNode col3)
        {
            List<string> warnings = new List<string>();

            string key = $"{parent.Name.Replace(".", "-")}-{name}";
            string url = $"{parent.Parent.baseUrl}{col1.SelectSingleNode(".//a[contains(@class, 'page')]").Attributes["href"].Value}";

            string type = col2.SelectSingleNode(".//span[contains(@class, 'label')]").InnerText.Trim();
            string release = col3.SelectSingleNode(".//span[contains(@class, 'label')]").InnerText.Trim();
            string description = GetInnerTextClean(col1, ".//div[contains(@class, 'paragraph')][2]/p") ?? "No documentation available.";

            parent.Parent.client.SchedulePage(key, url, content =>
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(content);

                var errors = doc.DocumentNode.SelectNodes("//div[contains(@class, 'warning')]");
                if (errors is not null)
                {
                    foreach (HtmlNode warning in errors)
                    {
                        string text = warning.SelectSingleNode(".//p")?.InnerText ?? "xxx";
                        warnings.Add(text);
                    }
                }

                ApocMethod apocMethod = new ApocMethod(parent, name, type, release, description, warnings.ToArray());

                GetParameters(apocMethod, "_input_parameters", apocMethod.GetInputParameter, apocMethod.RegisterInputParameter);
                GetParameters(apocMethod, "_output_parameters", apocMethod.GetOutputParameter, apocMethod.RegisterOutputParameter);

                parent.RegisterMethod(apocMethod);

                if (type == "Function" && apocMethod.OutputParameters.Count == 0)
                {
                    string signature = HtmlEntity.DeEntitize(doc.DocumentNode.SelectSingleNode("//article[@class = 'doc']/div[@class = 'sect1' and ./h2/@id = '_signature']//code").InnerText).Trim();
                    int index = signature.LastIndexOf(":");
                    string returnType = signature.Substring(index + 1).Trim(' ', '(', ')');

                    ApocParameter? apocParameter = apocMethod.GetOutputParameter("Return Value");
                    if (apocParameter is null)
                    {
                        apocParameter = new ApocParameter(apocMethod, "Return Value", returnType, null);
                        apocMethod.RegisterOutputParameter(apocParameter);
                    }
                }

                void GetParameters(ApocMethod apocMethod, string xpathId, Func<string, ApocParameter?> getParameter, Action<ApocParameter> registerParameter)
                {
                    var parameters = doc.DocumentNode.SelectNodes($"//article[@class = 'doc']/div[@class = 'sect1' and ./h2/@id = '{xpathId}']//tbody/tr");
                    if (parameters is not null)
                    {
                        foreach (HtmlNode parameter in parameters)
                        {
                            string name = GetInnerTextClean(parameter, $"./td[1]/p") ?? throw new NotSupportedException();
                            string type = GetInnerTextClean(parameter, $"./td[2]/p") ?? throw new NotSupportedException();
                            string? defaultValue = GetInnerTextClean(parameter, $"./td[3]/p");

                            ApocParameter? apocParameter = getParameter.Invoke(name);
                            if (apocParameter is null)
                            {
                                apocParameter = new ApocParameter(apocMethod, name, type, defaultValue);
                                registerParameter.Invoke(apocParameter);
                            }
                        }
                    }
                }
            });

            string? GetInnerTextClean(HtmlNode node, string xpath)
            {
                string? text = node.SelectSingleNode(xpath)?.InnerText;
                if (string.IsNullOrWhiteSpace(text))
                    return null;

                return HtmlEntity.DeEntitize(text).Trim();
            }
        }

        private ApocMethod(ApocNamespace parent, string name, string type, string release, string description, string[] warnings)
        {
            Namespace = parent;
            Name = name;
            Type = type;
            Release = release;
            Description = description;
            Warnings = warnings;
        }

        public ApocNamespace Namespace { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Release { get; private set; }
        public string Description { get; private set; }

        public IReadOnlyCollection<ApocParameter> InputParameters => m_InputParameters.Values;
        public ApocParameter? GetInputParameter(string name)
        {
            ApocParameter? apocParameter;
            m_InputParameters.TryGetValue(name.ToLowerInvariant(), out apocParameter);
            return apocParameter;
        }
        internal void RegisterInputParameter(ApocParameter parameter)
        {
            m_InputParameters.Add(parameter.Name.ToLowerInvariant(), parameter);
        }
        private readonly Dictionary<string, ApocParameter> m_InputParameters = new Dictionary<string, ApocParameter>();
        
        public IReadOnlyCollection<ApocParameter> OutputParameters => m_OutputParameters.Values;
        public ApocParameter? GetOutputParameter(string name)
        {
            ApocParameter? apocParameter;
            m_OutputParameters.TryGetValue(name.ToLowerInvariant(), out apocParameter);
            return apocParameter;
        }
        internal void RegisterOutputParameter(ApocParameter parameter)
        {
            m_OutputParameters.Add(parameter.Name.ToLowerInvariant(), parameter);
        }
        private readonly Dictionary<string, ApocParameter> m_OutputParameters = new Dictionary<string, ApocParameter>();

        public IReadOnlyCollection<string> Warnings { get; private set; }
    }
}
