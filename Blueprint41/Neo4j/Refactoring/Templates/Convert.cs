﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
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
    using System.Linq.Expressions;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\Convert.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    internal partial class Convert : TemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            
            #line 9 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\Convert.tt"


    Debug.WriteLine("	executing {0} -> Convert value of {1}.{2} = '{3}'", this.GetType().Name, Entity.Name, Property.Name, string.Format(AssignScript, string.Concat(Entity.Name, ".", Property.Name)));


            
            #line default
            #line hidden
            this.Write("MATCH (node:");
            
            #line 14 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\Convert.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Entity.Label.Name));
            
            #line default
            #line hidden
            this.Write(") WHERE node.");
            
            #line 14 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\Convert.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Property.Name));
            
            #line default
            #line hidden
            this.Write(" <> ");
            
            #line 14 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\Convert.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Format(WhereScript, string.Concat("node.", Property.Name))));
            
            #line default
            #line hidden
            this.Write("  WITH node LIMIT 10000 SET node.");
            
            #line 14 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\Convert.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Property.Name));
            
            #line default
            #line hidden
            this.Write(" = ");
            
            #line 14 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\Convert.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Format(AssignScript, string.Concat("node.", Property.Name))));
            
            #line default
            #line hidden
            this.Write("\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 15 "C:\_CirclesArrows\blueprint41\Blueprint41\Neo4j\Refactoring\Templates\Convert.tt"


	// Template Parameters
	public Entity	Entity	     { get; set; }
	public Property Property     { get; set; }
	public string   WhereScript  { get; set; }
	public string   AssignScript { get; set; }

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
}
