﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Blueprint41.Neo4j.Refactoring.Templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Blueprint41;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\RemoveRelationship.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    internal partial class RemoveRelationship : RemoveRelationshipBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("\n");
            this.Write("\n");
            this.Write("\n");
            this.Write("\n");
            this.Write("\n");
            this.Write("\n");
            
            #line 1 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\RemoveRelationship.tt"


    Log("	executing {0} -> Rename relationship from {0} to {1}", this.GetType().Name, Relation);

            
            #line default
            #line hidden
            this.Write("\nMATCH (in:");
            
            #line 1 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\RemoveRelationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(InEntity));
            
            #line default
            #line hidden
            this.Write(")-[rel:");
            
            #line 1 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\RemoveRelationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relation));
            
            #line default
            #line hidden
            this.Write("]->(out:");
            
            #line 1 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\RemoveRelationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(OutEntity));
            
            #line default
            #line hidden
            this.Write(")\nWITH rel LIMIT 10000\nDELETE rel\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
