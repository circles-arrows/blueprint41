﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
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
    using Blueprint41.Dynamic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultLookupValue.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    internal partial class SetDefaultLookupValue : TemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            
            #line 9 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultLookupValue.tt"


    Debug.WriteLine("	executing {0} -> Set Default Lookup Value for {1}.{2}", this.GetType().Name, Property.Parent.Name, Property.Name);

            
            #line default
            #line hidden
            this.Write("MATCH (in:");
            
            #line 13 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultLookupValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Property.Parent.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nMATCH (target:");
            
            #line 14 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultLookupValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Property.ForeignEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(" { ");
            
            #line 14 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultLookupValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Property.ForeignEntity.Key.Name));
            
            #line default
            #line hidden
            this.Write(" : \'");
            
            #line 14 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultLookupValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Value));
            
            #line default
            #line hidden
            this.Write("\'})\r\nOPTIONAL MATCH (in)-[rel:");
            
            #line 15 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultLookupValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Property.Relationship.Neo4JRelationshipType));
            
            #line default
            #line hidden
            this.Write("]-(out:");
            
            #line 15 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultLookupValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Property.ForeignEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWITH in, count(out) as count, target\r\nWHERE count = 0\r\nWITH in, target LIMIT 1" +
                    "0000\r\nMERGE (in)-[rel:");
            
            #line 19 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultLookupValue.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Property.Relationship.Neo4JRelationshipType));
            
            #line default
            #line hidden
            this.Write("]-(target)\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 20 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\SetDefaultLookupValue.tt"


	// Template Parameters
	public Property Property { get; set; }
	public string	Value	 { get; set; }

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
}
