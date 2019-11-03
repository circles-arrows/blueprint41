using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    public class TypeMapping
    {
        public TypeMapping(Type returnType, Type persistedType, string comparisonGroup)
        {
            ReturnType = returnType;
            PersistedType = persistedType;
            ComparisonGroup = comparisonGroup;
        }

        public Type ReturnType {get; private set;}
        public Type PersistedType { get; private set; }
        internal string ComparisonGroup { get; private set; }

        public string ShortPersistedType
        {
            get
            {
                if (PersistedType == null) return "";
                return PersistedType.ToCSharp();
            }
        }

        #region Conversion

        public bool NeedsConversion
        {
            get
            {
                return (ReturnType != PersistedType);
            }
        }

        private MethodInfo? getConverter = null;
        public MethodInfo GetGetConverter()
        {
            if (getConverter == null)
                getConverter = typeof(Conversion<,>).MakeGenericType(new Type[] { ReturnType, PersistedType }).GetTypeInfo().DeclaredMethods.FirstOrDefault(item => item.Name == "Convert");

            return getConverter;
        }

        private MethodInfo? setConverter = null;
        public MethodInfo GetSetConverter()
        {
            if (setConverter == null)
                setConverter = typeof(Conversion<,>).MakeGenericType(new Type[] { PersistedType, ReturnType }).GetTypeInfo().DeclaredMethods.FirstOrDefault(item => item.Name == "Convert");

            return setConverter;
        }

        #endregion
    }
}
