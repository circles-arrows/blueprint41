using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blueprint41.ApocDocumentationParser
{
    public class ApocNamespace
    {
        internal ApocNamespace(ApocDocumentation parent, string name)
        {
            Parent = parent;
            Name = name;
        }

        public ApocDocumentation Parent { get; private set; }
        public string Name {get; private set;}

        public IReadOnlyCollection<ApocMethod> Methods => m_Methods.Values;
        public ApocMethod? GetMethod(string name)
        {
            ApocMethod? apocMethod;
            m_Methods.TryGetValue(name.ToLowerInvariant(), out apocMethod);
            return apocMethod;
        }
        internal void RegisterMethod(ApocMethod method)
        {
            lock (m_Methods)
            {
                m_Methods.Add(method.Name.ToLowerInvariant(), method);
            }
            Interlocked.Increment(ref Parent.methodsFinished);
        }
        private readonly Dictionary<string, ApocMethod> m_Methods = new Dictionary<string, ApocMethod>();
    }
}
