// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Blueprint41.Sync.DatastoreTemplates
{
    using System.Linq;
    using System.Collections.Generic;
using Blueprint41.Sync.Core;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Register.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class Domain_Data_Register : GeneratorBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("#pragma warning disable S101 // Types should be named in PascalCase\r\n#pragma warn" +
                    "ing disable CS8981 // Names should not be lower type only\r\n\r\nusing System;\r\nusin" +
                    "g Blueprint41.Core;\r\n\r\nnamespace ");
            
            #line 11 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Register.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.FullCRUDNamespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    internal class Register\r\n    {\r\n        [Obsolete]\r\n        public stati" +
                    "c void Types()\r\n        {\r\n            if (");
            
            #line 18 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Register.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Datastore.GetType().FullName.Replace("+", ".")));
            
            #line default
            #line hidden
            this.Write(".Model.TypesRegistered)\r\n                return;\r\n\r\n            lock (typeof(Regi" +
                    "ster))\r\n            {\r\n                if (");
            
            #line 23 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Register.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Datastore.GetType().FullName.Replace("+", ".")));
            
            #line default
            #line hidden
            this.Write(".Model.TypesRegistered)\r\n                    return;\r\n\r\n");
            
            #line 26 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Register.tt"

foreach (var DALModel in Datastore.Entities.OrderBy(item => item.Name))
{

            
            #line default
            #line hidden
            this.Write("                ((ISetRuntimeType)");
            
            #line 30 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Register.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Datastore.GetType().FullName.Replace("+", ".")));
            
            #line default
            #line hidden
            this.Write(".Model.Entities[\"");
            
            #line 30 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Register.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("\"]).SetRuntimeTypes(typeof(");
            
            #line 30 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Register.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write("), typeof(");
            
            #line 30 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Register.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("));\r\n");
            
            #line 31 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Register.tt"

}

            
            #line default
            #line hidden
            this.Write("            }\r\n        }\r\n    }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
