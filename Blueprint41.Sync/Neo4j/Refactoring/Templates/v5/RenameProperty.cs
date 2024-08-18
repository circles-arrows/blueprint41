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
    
    #line 1 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RenameProperty.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    internal partial class RenameProperty : RenamePropertyBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            
            #line 7 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RenameProperty.tt"

    Log("	executing {0} -> Rename property from {1} to {2}", this.GetType().Name, From.Name, To);

    if (IsEntity)
    {

            
            #line default
            #line hidden
            this.Write("MATCH (node:");
            
            #line 13 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RenameProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Entity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWHERE node.");
            
            #line 14 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RenameProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(" IS NOT NULL\r\nWITH node LIMIT 10000 \r\nSET node.");
            
            #line 16 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RenameProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To));
            
            #line default
            #line hidden
            this.Write(" = node.");
            
            #line 16 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RenameProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write("\r\nSET node.");
            
            #line 17 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RenameProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(" = NULL\r\n");
            
            #line 18 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RenameProperty.tt"

    }
    else if (IsRelationship)
    {

            
            #line default
            #line hidden
            this.Write("MATCH (:");
            
            #line 23 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RenameProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.InEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")-[rel:");
            
            #line 23 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RenameProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.Neo4JRelationshipType));
            
            #line default
            #line hidden
            this.Write("]->(:");
            
            #line 23 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RenameProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.OutEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWHERE rel.");
            
            #line 24 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RenameProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(" IS NOT NULL\r\nWITH rel LIMIT 10000 \r\nSET rel.");
            
            #line 26 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RenameProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To));
            
            #line default
            #line hidden
            this.Write(" = rel.");
            
            #line 26 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RenameProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write("\r\nSET rel.");
            
            #line 27 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RenameProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(" = NULL\r\n");
            
            #line 28 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\RenameProperty.tt"

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
