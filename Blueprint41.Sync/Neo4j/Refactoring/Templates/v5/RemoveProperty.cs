// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Blueprint41.Sync.Neo4j.Refactoring.Templates.v5
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System.Diagnostics;
using Blueprint41.Sync;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RemoveProperty.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    internal partial class RemoveProperty : RemovePropertyBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            
            #line 7 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RemoveProperty.tt"

    Log("	executing {0} -> Remove property from {1}.{2}", this.GetType().Name, Caller.Name, Name);

    if (IsEntity)
    {

            
            #line default
            #line hidden
            this.Write("MATCH (node:");
            
            #line 13 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RemoveProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Entity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWHERE node.");
            
            #line 14 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RemoveProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write(" IS NOT NULL\r\nWITH node LIMIT 10000\r\nREMOVE node.");
            
#line 16 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\RemoveProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 17 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RemoveProperty.tt"

    }
    else if (IsRelationship)
    {

            
            #line default
            #line hidden
            this.Write("MATCH (:");
            
            #line 22 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RemoveProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.InEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")-[rel:");
            
            #line 22 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RemoveProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.Neo4JRelationshipType));
            
            #line default
            #line hidden
            this.Write("]->(:");
            
            #line 22 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RemoveProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.OutEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWHERE rel.");
            
            #line 23 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RemoveProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write(" IS NOT NULL\r\nWITH rel LIMIT 10000\r\nREMOVE rel.");
            
            #line 25 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RemoveProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 26 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RemoveProperty.tt"

    }
    else
    {
        throw new NotSupportedException();
    }

            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
