using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41
{
    public class Instance
    {
        internal Instance(Entity parent, object values)
        {
            NodeType = parent;
            Values = ToDynamic(values);
        }

        public Entity NodeType { get; private set; }
        public ExpandoObject Values;


        private static ExpandoObject ToDynamic(object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));

            return expando as ExpandoObject;
        }
    }
}
