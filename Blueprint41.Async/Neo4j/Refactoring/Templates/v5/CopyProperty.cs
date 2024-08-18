// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Blueprint41.Async.Neo4j.Refactoring.Templates.v5
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System.Diagnostics;
using Blueprint41.Async;
    using System.Linq.Expressions;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    internal partial class CopyProperty : CopyPropertyBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            
            #line 8 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"

    Log("	executing {0} -> Copy properties from {1} to {2} for entity {3}", this.GetType().Name, From, To, Caller.Name);

    if (IsEntity)
    {

            
            #line default
            #line hidden
            this.Write("MATCH (node:");
            
            #line 14 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Entity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWHERE node.");
            
            #line 15 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To));
            
            #line default
            #line hidden
            this.Write(" IS NULL OR node.");
            
            #line 15 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From));
            
            #line default
            #line hidden
            this.Write(" <> node.");
            
            #line 15 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To));
            
            #line default
            #line hidden
            this.Write("\r\nWITH node limit 10000 \r\nSET node.");
            
            #line 17 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To));
            
            #line default
            #line hidden
            this.Write(" = node.");
            
            #line 17 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 18 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"

    }
    else if (IsRelationship)
    {

            
            #line default
            #line hidden
            this.Write("MATCH (:");
            
            #line 23 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.InEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")-[rel:");
            
            #line 23 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.Neo4JRelationshipType));
            
            #line default
            #line hidden
            this.Write("]->(:");
            
            #line 23 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.OutEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWHERE rel.");
            
            #line 24 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To));
            
            #line default
            #line hidden
            this.Write(" IS NULL OR rel.");
            
            #line 24 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From));
            
            #line default
            #line hidden
            this.Write(" <> rel.");
            
            #line 24 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To));
            
            #line default
            #line hidden
            this.Write("\r\nWITH rel limit 10000 \r\nSET rel.");
            
            #line 26 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To));
            
            #line default
            #line hidden
            this.Write(" = rel.");
            
            #line 26 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 27 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\v5\CopyProperty.tt"

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
