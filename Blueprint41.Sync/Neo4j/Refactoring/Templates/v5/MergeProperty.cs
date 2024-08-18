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
    
#line 1 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    internal partial class MergeProperty : MergePropertyBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            
#line 7 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"

    Log("	executing {0} -> Merge property from {0} to {1}", this.GetType().Name, From.Name, To.Name);

    if (IsEntity)
    {
        switch (MergeAlgorithm)
        {
            case MergeAlgorithm.NotApplicable:

            
            #line default
            #line hidden
            this.Write("MATCH (node:");
            
#line 16 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Entity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWITH node LIMIT 10000 \r\nSET node.");
            
#line 18 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To.Name));
            
            #line default
            #line hidden
            this.Write(" = node.");
            
#line 18 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write("\r\nSET node.");
            
#line 19 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(" = NULL\r\n");
            
#line 20 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"

                break;
            case MergeAlgorithm.ThrowOnConflict:

            
            #line default
            #line hidden
            this.Write("MATCH (node:");
            
#line 24 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Entity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWHERE node.");
            
#line 25 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To.Name));
            
            #line default
            #line hidden
            this.Write(" IS NOT NULL\r\nRETURN count(node) AS Count\r\n");
            
#line 27 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"

                break;
            case MergeAlgorithm.PreferSource:

            
            #line default
            #line hidden
            this.Write("MATCH (node:");
            
#line 31 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Entity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWHERE node.");
            
#line 32 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(" IS NOT NULL\r\nWITH node LIMIT 10000 \r\nSET node.");
            
#line 34 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To.Name));
            
            #line default
            #line hidden
            this.Write(" = COALESCE(node.");
            
#line 34 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(", node.");
            
#line 34 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nSET node.");
            
#line 35 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(" = NULL\r\n");
            
#line 36 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"

                break;
            case MergeAlgorithm.PreferTarget:

            
            #line default
            #line hidden
            this.Write("MATCH (node:");
            
#line 40 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Entity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWHERE node.");
            
#line 41 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(" IS NOT NULL\r\nWITH node LIMIT 10000 \r\nSET node.");
            
#line 43 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To.Name));
            
            #line default
            #line hidden
            this.Write(" = COALESCE(node.");
            
#line 43 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To.Name));
            
            #line default
            #line hidden
            this.Write(", node.");
            
#line 43 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nSET node.");
            
#line 44 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(" = NULL\r\n");
            
#line 45 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"

                break;
        }
    }
    else if (IsRelationship)
    {
        switch (MergeAlgorithm)
        {
            case MergeAlgorithm.NotApplicable:

            
            #line default
            #line hidden
            this.Write("MATCH (:");
            
#line 55 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.InEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")-[rel:");
            
#line 55 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.Neo4JRelationshipType));
            
            #line default
            #line hidden
            this.Write("]->(:");
            
#line 55 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.OutEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWITH rel LIMIT 10000 \r\nSET rel.");
            
#line 57 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To.Name));
            
            #line default
            #line hidden
            this.Write(" = rel.");
            
#line 57 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write("\r\nSET rel.");
            
#line 58 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(" = NULL\r\n");
            
#line 59 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"

                break;
            case MergeAlgorithm.ThrowOnConflict:

            
            #line default
            #line hidden
            this.Write("MATCH (:");
            
#line 63 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.InEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")-[rel:");
            
#line 63 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.Neo4JRelationshipType));
            
            #line default
            #line hidden
            this.Write("]->(:");
            
#line 63 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.OutEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWHERE rel.");
            
#line 64 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To.Name));
            
            #line default
            #line hidden
            this.Write(" IS NOT NULL\r\nRETURN count(rel) AS Count\r\n");
            
#line 66 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"

                break;
            case MergeAlgorithm.PreferSource:

            
            #line default
            #line hidden
            this.Write("MATCH (:");
            
#line 70 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.InEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")-[rel:");
            
#line 70 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.Neo4JRelationshipType));
            
            #line default
            #line hidden
            this.Write("]->(:");
            
#line 70 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.OutEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWHERE rel.");
            
#line 71 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(" IS NOT NULL\r\nWITH rel LIMIT 10000 \r\nSET rel.");
            
#line 73 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To.Name));
            
            #line default
            #line hidden
            this.Write(" = COALESCE(rel.");
            
#line 73 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(", rel.");
            
#line 73 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nSET rel.");
            
#line 74 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(" = NULL\r\n");
            
#line 75 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"

                break;
            case MergeAlgorithm.PreferTarget:

            
            #line default
            #line hidden
            this.Write("MATCH (:");
            
#line 79 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.InEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")-[rel:");
            
#line 79 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.Neo4JRelationshipType));
            
            #line default
            #line hidden
            this.Write("]->(:");
            
#line 79 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Relationship.OutEntity.Label.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nWHERE rel.");
            
#line 80 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(" IS NOT NULL\r\nWITH rel LIMIT 10000 \r\nSET rel.");
            
#line 82 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To.Name));
            
            #line default
            #line hidden
            this.Write(" = COALESCE(rel.");
            
#line 82 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(To.Name));
            
            #line default
            #line hidden
            this.Write(", rel.");
            
#line 82 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(")\r\nSET rel.");
            
#line 83 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(From.Name));
            
            #line default
            #line hidden
            this.Write(" = NULL\r\n");
            
#line 84 "C:\_CirclesArrows\blueprint41\Blueprint41.Sync\Neo4j\Refactoring\Templates\v5\MergeProperty.tt"

                break;
        }
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
