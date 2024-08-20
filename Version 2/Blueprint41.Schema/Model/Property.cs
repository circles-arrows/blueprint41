using System;
using System.Collections.Generic;

namespace Blueprint41.Model
{
    internal abstract class PropertyBase
    {
        protected PropertyBase(string name, Type type, bool unique)
        {
            Name = name;
            SystemType = type;
            Unique = unique;
        }

        #region Properties

        public string Name { get; private set; }
        public Type SystemType { get; private set; }
        public bool Unique { get; private set; }

        #endregion
    }
    internal class Property<TParent> : PropertyBase
    {
        internal Property(TParent parent, string name, Type type, bool unique)
            : base(name, type, unique)
        {
            Parent = parent;
        }

        #region Properties

        public TParent Parent { get; private set; }

        #endregion
    }
}
