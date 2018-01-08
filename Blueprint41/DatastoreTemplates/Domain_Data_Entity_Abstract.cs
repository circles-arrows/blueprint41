﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Blueprint41.DatastoreTemplates
{
    using System;
    using System.IO;
    using System.Diagnostics;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class Domain_Data_Entity_Abstract : GeneratorBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write(" \r\n");
            this.Write("using System;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\nusing Bluep" +
                    "rint41;\r\nusing Blueprint41.Core;\r\nusing Blueprint41.Query;\r\nusing Blueprint41.Da" +
                    "tastoreTemplates;\r\nusing q = ");
            
            #line 19 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.FullQueryNamespace));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\nnamespace ");
            
            #line 21 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.FullCRUDNamespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n");
            
            #line 23 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

if (DALModel.IsAbstract)
{

            
            #line default
            #line hidden
            this.Write("\tpublic partial interface ");
            
            #line 27 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write(" : OGM");
            
            #line 27 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join("", DALModel.GetBaseTypes().Where(item => item.IsAbstract).Select(item => string.Concat(", ", item.ClassName)).ToArray())));
            
            #line default
            #line hidden
            this.Write("\r\n\t{\r\n");
            
            #line 29 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

	if((object)DALModel.NodeType == null)
    {

            
            #line default
            #line hidden
            this.Write("\t\tstring ");
            
            #line 33 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.NodeTypeName));
            
            #line default
            #line hidden
            this.Write(" { get; }\r\n");
            
            #line 34 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

	}

	foreach (Property attr in DALModel.Properties)
	{
		if (attr.PropertyType == PropertyType.Collection || attr.IsNodeType)
		{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 42 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.OuterReturnType));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 42 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write(" { get; }\r\n");
            
            #line 43 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

		}
		else
		{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 48 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.OuterReturnType));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 48 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write(" { get; set; }\r\n");
            
            #line 49 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

			if (attr.PropertyType == PropertyType.Lookup && attr.Relationship.IsTimeDependent)
			{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 53 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.OuterReturnType));
            
            #line default
            #line hidden
            this.Write(" Get");
            
            #line 53 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write("(DateTime? moment = null);\r\n\t\tvoid Set");
            
            #line 54 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 54 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.OuterReturnType));
            
            #line default
            #line hidden
            this.Write(" value, DateTime? moment = null);\r\n");
            
            #line 55 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

			}
		}
	}

            
            #line default
            #line hidden
            this.Write("\t}\r\n\r\n\tpublic partial class ");
            
            #line 62 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write(" : OGMAbstractImpl<");
            
            #line 62 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 62 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 62 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Key.SystemReturnType));
            
            #line default
            #line hidden
            this.Write(">\r\n\t{\r\n        #region Initialize\r\n\r\n\t\tstatic ");
            
            #line 66 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write(@"()
        {
            Register.Types();
        }

        protected override void RegisterGeneratedStoredQueries()
        {
			AdditionalGeneratedStoredQueries();
        }
		partial void AdditionalGeneratedStoredQueries();

		#endregion

        private static ");
            
            #line 79 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write("Members members = null;\r\n        public static ");
            
            #line 80 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write("Members Members\r\n        {\r\n            get\r\n            {\r\n                if (m" +
                    "embers == null)\r\n                {\r\n                    lock (typeof(");
            
            #line 86 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write("))\r\n                    {\r\n                        if (members == null)\r\n        " +
                    "                    members = new ");
            
            #line 89 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write("Members();\r\n                    }\r\n                }\r\n                return memb" +
                    "ers;\r\n            }\r\n        }\r\n        public class ");
            
            #line 95 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write("Members\r\n        {\r\n            internal ");
            
            #line 97 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write("Members() { }\r\n\r\n");
            
            #line 99 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

	foreach (Entity inherited in DALModel.GetBaseTypesAndSelf())
	{

            
            #line default
            #line hidden
            this.Write("\t\t\t#region Members for interface I");
            
            #line 103 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(inherited.Name));
            
            #line default
            #line hidden
            this.Write("\r\n\r\n");
            
            #line 105 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

		foreach (Property attr in inherited.Properties)
		{

            
            #line default
            #line hidden
            this.Write("            public Property ");
            
            #line 109 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write(" { get; } = ");
            
            #line 109 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Datastore.GetType().FullName));
            
            #line default
            #line hidden
            this.Write(".Model.Entities[\"");
            
            #line 109 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(inherited.Name));
            
            #line default
            #line hidden
            this.Write("\"].Properties[\"");
            
            #line 109 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write("\"];\r\n");
            
            #line 110 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

		}

            
            #line default
            #line hidden
            this.Write("\t\t\t#endregion\r\n\r\n");
            
            #line 115 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

	}

            
            #line default
            #line hidden
            this.Write("        }\r\n\r\n\t\tsealed public override Entity GetEntity()\r\n        {\r\n            " +
                    "if (entity == null)\r\n            {\r\n                lock (typeof(");
            
            #line 124 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write("))\r\n                {\r\n                    if (entity == null)\r\n                 " +
                    "       entity = ");
            
            #line 127 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Datastore.GetType().FullName));
            
            #line default
            #line hidden
            this.Write(".Model.Entities[\"");
            
            #line 127 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("\"];\r\n                }\r\n            }\r\n            return entity;\r\n        }\r\n\t}\r" +
                    "\n");
            
            #line 133 "C:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

}

            
            #line default
            #line hidden
            this.Write("}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
