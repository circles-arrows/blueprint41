// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Blueprint41.Async.Neo4j.Refactoring.Templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System.Diagnostics;
using Blueprint41.Async;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\RemoveEntity.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    internal partial class RemoveEntity : RemoveEntityBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            
            #line 7 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\RemoveEntity.tt"


    Log("	executing {0} -> Deprecate entity from {1}", this.GetType().Name, Name);

            
            #line default
            #line hidden
            this.Write("MATCH (n:");
            
            #line 11 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\RemoveEntity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write(") WITH n LIMIT 10000 DETACH DELETE n\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
