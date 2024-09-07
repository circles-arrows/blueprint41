using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bp41 = Blueprint41;

namespace Blueprint41.Core
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RestrictedToAttribute : Attribute
    {
        public RestrictedToAttribute()
        {
            PropertyType = new PropertyType[] { bp41.PropertyType.Attribute, bp41.PropertyType.Collection, bp41.PropertyType.Lookup };
        }
        public RestrictedToAttribute(params PropertyType[] propertyType)
        {
            PropertyType = propertyType;
        }

        public PropertyType[] PropertyType { get; private set; }

        public bool IsRestricted(object parent)
        {
            switch (parent)
            {
                case Property property:
                    return !PropertyType.Contains(property.PropertyType);
                default:
                    return false;
            }
        }
    }
}
