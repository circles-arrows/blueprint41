// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Blueprint41.Sync.Neo4j.Refactoring.Templates
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
    
    #line 1 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultConstantValue.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    internal partial class SetDefaultConstantValue : SetDefaultConstantValueBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            
            #line 7 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultConstantValue.tt"

    Log("	executing {0} -> {1}.{2} = '{3}'", this.GetType().Name, Caller.Name, Property.Name, (Value is null) ? "<NULL>" : Value.ToString());

    // Setup Cypher Parameters
    OutputParameters.Add(Property.Name, Value);

    if (IsEntity)
    {

            
            #line default
            #line hidden
            this.Write("MATCH (node:");
            
            #line 16 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultConstantValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Entity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWHERE NOT EXISTS(node.");
            
            #line 17 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultConstantValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Property.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWITH node LIMIT 10000\r\nSET node.");
            
            #line 19 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultConstantValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Property.Name));
            
            #line default
            #line hidden
            this.Write(" = {");
            
            #line 19 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultConstantValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Property.Name));
            
            #line default
            #line hidden
            this.Write("}\r\n");
            
            #line 20 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultConstantValue.tt"

    }
    else if (IsRelationship)
    {

            
            #line default
            #line hidden
            this.Write("MATCH (:");
            
            #line 25 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultConstantValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.InEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")-[rel:");
            
            #line 25 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultConstantValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.Neo4JRelationshipType));
            
            #line default
            #line hidden
            this.Write("]->(:");
            
            #line 25 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultConstantValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.OutEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWHERE NOT EXISTS(rel.");
            
            #line 26 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultConstantValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Property.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWITH rel LIMIT 10000\r\nSET rel.");
            
            #line 28 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultConstantValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Property.Name));
            
            #line default
            #line hidden
            this.Write(" = {");
            
            #line 28 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultConstantValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Property.Name));
            
            #line default
            #line hidden
            this.Write("}\r\n");
            
            #line 29 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultConstantValue.tt"

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
