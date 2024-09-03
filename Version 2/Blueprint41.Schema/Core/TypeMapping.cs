using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blueprint41.Core
{
    public class TypeMapping
    {
        public TypeMapping(Type returnType, Type persistedType, string comparisonGroup)
        {
            ReturnType = returnType;
            PersistedType = persistedType;

            ShortReturnType = ReturnType.ToCSharp();
            ShortPersistedType = PersistedType.ToCSharp();

            ComparisonGroup = comparisonGroup;
        }

        public Type ReturnType { get; private set; }
        public Type PersistedType { get; private set; }

        public string ShortReturnType { get; private set; }
        public string ShortPersistedType { get; private set; }

        internal string ComparisonGroup { get; private set; }

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
            if (getConverter is null)
                getConverter = typeof(Conversion<,>).MakeGenericType(new Type[] { ReturnType, PersistedType }).GetTypeInfo().DeclaredMethods.FirstOrDefault(item => item.Name == "Convert") ?? throw new MissingMethodException();

            return getConverter;
        }

        private MethodInfo? setConverter = null;
        public MethodInfo GetSetConverter()
        {
            if (setConverter is null)
                setConverter = typeof(Conversion<,>).MakeGenericType(new Type[] { PersistedType, ReturnType }).GetTypeInfo().DeclaredMethods.FirstOrDefault(item => item.Name == "Convert") ?? throw new MissingMethodException();

            return setConverter;
        }

        #endregion
    }
}
