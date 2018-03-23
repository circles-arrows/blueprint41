﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Blueprint41.DatastoreTemplates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using Blueprint41;
    using Blueprint41.Core;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class Domain_Data_Relationship : GeneratorBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("using System;\r\n\r\nusing Blueprint41;\r\nusing Blueprint41.Query;\r\n\r\nnamespace ");
            
            #line 14 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.FullQueryNamespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\npublic partial class ");
            
            #line 16 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL : RELATIONSHIP, IFromIn_");
            
            #line 16 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL, IFromOut_");
            
            #line 16 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL");
            
            #line 16 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
 if (DALRelation.InEntity == DALRelation.OutEntity) { 
            
            #line default
            #line hidden
            this.Write(", IFromAny_");
            
            #line 16 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL");
            
            #line 16 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
 }
            
            #line default
            #line hidden
            this.Write("\t{\r\n        public override string NEO4J_TYPE\r\n        {\r\n            get\r\n      " +
                    "      {\r\n                return \"");
            
            #line 22 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Neo4JRelationshipType));
            
            #line default
            #line hidden
            this.Write("\";\r\n            }\r\n        }\r\n        public override AliasResult RelationshipAli" +
                    "as { get; protected set; }\r\n        \r\n\t\tinternal ");
            
            #line 27 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direc" +
                    "tion) { }\r\n\r\n\t\tpublic ");
            
            #line 29 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL Alias(out ");
            
            #line 29 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_ALIAS alias)\r\n\t\t{\r\n\t\t\talias = new ");
            
            #line 31 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_ALIAS(this);\r\n            RelationshipAlias = alias;\r\n\t\t\treturn this;\r\n\t\t} \r\n\t\tp" +
                    "ublic ");
            
            #line 35 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL Repeat(int maxHops)\r\n\t\t{\r\n\t\t\treturn Repeat(1, maxHops);\r\n\t\t}\r\n\t\tpublic ");
            
            #line 39 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL Repeat(int minHops, int maxHops)\r\n\t\t{\r\n\t\t\treturn this;\r\n\t\t}\r\n\r\n\t\tIFromIn_");
            
            #line 44 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL IFromIn_");
            
            #line 44 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL.Alias(out ");
            
            #line 44 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_ALIAS alias)\r\n\t\t{\r\n\t\t\treturn Alias(out alias);\r\n\t\t}\r\n\t\tIFromOut_");
            
            #line 48 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL IFromOut_");
            
            #line 48 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL.Alias(out ");
            
            #line 48 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_ALIAS alias)\r\n\t\t{\r\n\t\t\treturn Alias(out alias);\r\n\t\t}\r\n\t\tIFromIn_");
            
            #line 52 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL IFromIn_");
            
            #line 52 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL.Repeat(int maxHops)\r\n\t\t{\r\n\t\t\treturn Repeat(maxHops);\r\n\t\t}\r\n\t\tIFromIn_");
            
            #line 56 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL IFromIn_");
            
            #line 56 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL.Repeat(int minHops, int maxHops)\r\n\t\t{\r\n\t\t\treturn Repeat(minHops, maxHops);\r\n" +
                    "\t\t}\r\n\t\tIFromOut_");
            
            #line 60 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL IFromOut_");
            
            #line 60 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL.Repeat(int maxHops)\r\n\t\t{\r\n\t\t\treturn Repeat(maxHops);\r\n\t\t}\r\n\t\tIFromOut_");
            
            #line 64 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL IFromOut_");
            
            #line 64 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL.Repeat(int minHops, int maxHops)\r\n\t\t{\r\n\t\t\treturn Repeat(minHops, maxHops);\r\n" +
                    "\t\t}\r\n\r\n");
            
            #line 69 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

	if (DALRelation.InEntity == DALRelation.OutEntity)
    {

            
            #line default
            #line hidden
            this.Write("\t\tIFromAny_");
            
            #line 73 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL IFromAny_");
            
            #line 73 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL.Alias(out ");
            
            #line 73 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_ALIAS alias)\r\n\t\t{\r\n\t\t\treturn Alias(out alias);\r\n\t\t}\r\n\t\tIFromAny_");
            
            #line 77 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL IFromAny_");
            
            #line 77 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL.Repeat(int maxHops)\r\n\t\t{\r\n\t\t\treturn Repeat(maxHops);\r\n\t\t}\r\n\t\tIFromAny_");
            
            #line 81 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL IFromAny_");
            
            #line 81 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL.Repeat(int minHops, int maxHops)\r\n\t\t{\r\n\t\t\treturn Repeat(minHops, maxHops);\r\n" +
                    "\t\t}\r\n");
            
            #line 85 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

    }

            
            #line default
            #line hidden
            this.Write("\r\n\t\tpublic ");
            
            #line 89 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_IN In { get { return new ");
            
            #line 89 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_IN(this); } }\r\n        public class ");
            
            #line 90 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_IN\r\n        {\r\n            private ");
            
            #line 92 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL Parent;\r\n            internal ");
            
            #line 93 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_IN(");
            
            #line 93 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL parent)\r\n            {\r\n                Parent = parent;\r\n            }\r\n\r\n");
            
            #line 98 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

	foreach (Entity entity in DALRelation.InEntity.GetSubclassesOrSelf())
    {

            
            #line default
            #line hidden
            this.Write("\t\t\tpublic ");
            
            #line 102 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write("Node ");
            
            #line 102 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write(" { get { return new ");
            
            #line 102 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write("Node(Parent, DirectionEnum.In); } }\r\n");
            
            #line 103 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

    }

            
            #line default
            #line hidden
            this.Write("        }\r\n\r\n        public ");
            
            #line 108 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_OUT Out { get { return new ");
            
            #line 108 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_OUT(this); } }\r\n        public class ");
            
            #line 109 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_OUT\r\n        {\r\n            private ");
            
            #line 111 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL Parent;\r\n            internal ");
            
            #line 112 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_OUT(");
            
            #line 112 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL parent)\r\n            {\r\n                Parent = parent;\r\n            }\r\n\r\n");
            
            #line 117 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

	foreach (Entity entity in DALRelation.OutEntity.GetSubclassesOrSelf())
    {
		if(entity.IsVirtual && DALRelation.OutEntity != entity)
        {

            
            #line default
            #line hidden
            this.Write("\t\t\t[Obsolete(\"This relationship is virtual, consider making entity ");
            
            #line 123 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write(" concrete or exit this relationship via ");
            
            #line 123 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.OutEntity.Name));
            
            #line default
            #line hidden
            this.Write(".\", true)]\r\n");
            
            #line 124 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

		}

            
            #line default
            #line hidden
            this.Write("\t\t\tpublic ");
            
            #line 127 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write("Node ");
            
            #line 127 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write(" { get { return new ");
            
            #line 127 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write("Node(Parent, DirectionEnum.Out); } }\r\n");
            
            #line 128 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

    }

            
            #line default
            #line hidden
            this.Write("        }\r\n");
            
            #line 132 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

	if (DALRelation.InEntity == DALRelation.OutEntity)
    {

            
            #line default
            #line hidden
            this.Write("\r\n        public ");
            
            #line 137 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_ANY Any { get { return new ");
            
            #line 137 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_ANY(this); } }\r\n        public class ");
            
            #line 138 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_ANY\r\n        {\r\n            private ");
            
            #line 140 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL Parent;\r\n            internal ");
            
            #line 141 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_ANY(");
            
            #line 141 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL parent)\r\n            {\r\n                Parent = parent;\r\n            }\r\n\r\n");
            
            #line 146 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

	foreach (Entity entity in DALRelation.InEntity.GetSubclassesOrSelf())
    {

            
            #line default
            #line hidden
            this.Write("\t\t\tpublic ");
            
            #line 150 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write("Node ");
            
            #line 150 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write(" { get { return new ");
            
            #line 150 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write("Node(Parent, DirectionEnum.None); } }\r\n");
            
            #line 151 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

    }

            
            #line default
            #line hidden
            this.Write("        }\r\n");
            
            #line 155 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

    }

            
            #line default
            #line hidden
            this.Write("\t}\r\n\r\n    public interface IFromIn_");
            
            #line 160 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL\r\n    {\r\n\t\tIFromIn_");
            
            #line 162 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL Alias(out ");
            
            #line 162 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_ALIAS alias);\r\n\t\tIFromIn_");
            
            #line 163 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL Repeat(int maxHops);\r\n\t\tIFromIn_");
            
            #line 164 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL Repeat(int minHops, int maxHops);\r\n\r\n        ");
            
            #line 166 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL.");
            
            #line 166 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_OUT Out { get; }\r\n    }\r\n    public interface IFromOut_");
            
            #line 168 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL\r\n    {\r\n\t\tIFromOut_");
            
            #line 170 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL Alias(out ");
            
            #line 170 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_ALIAS alias);\r\n\t\tIFromOut_");
            
            #line 171 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL Repeat(int maxHops);\r\n\t\tIFromOut_");
            
            #line 172 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL Repeat(int minHops, int maxHops);\r\n\r\n        ");
            
            #line 174 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL.");
            
            #line 174 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_IN In { get; }\r\n    }\r\n");
            
            #line 176 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

	if (DALRelation.InEntity == DALRelation.OutEntity)
    {

            
            #line default
            #line hidden
            this.Write("    public interface IFromAny_");
            
            #line 180 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL\r\n    {\r\n\t\tIFromAny_");
            
            #line 182 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL Alias(out ");
            
            #line 182 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_ALIAS alias);\r\n\t\tIFromAny_");
            
            #line 183 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL Repeat(int maxHops);\r\n\t\tIFromAny_");
            
            #line 184 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL Repeat(int minHops, int maxHops);\r\n\r\n        ");
            
            #line 186 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL.");
            
            #line 186 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_ANY Any { get; }\r\n    }\r\n");
            
            #line 188 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

    }

            
            #line default
            #line hidden
            this.Write("\r\n    public class ");
            
            #line 192 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_ALIAS : AliasResult\r\n    {\r\n\t\tprivate ");
            
            #line 194 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL Parent;\r\n\r\n        internal ");
            
            #line 196 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_ALIAS(");
            
            #line 196 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALRelation.Name));
            
            #line default
            #line hidden
            this.Write("_REL parent)\r\n        {\r\n\t\t\tParent = parent;\r\n");
            
            #line 199 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

	if (DALRelation.IsTimeDependent)
    {

            
            #line default
            #line hidden
            this.Write("            StartDate = new RelationFieldResult(this, \"StartDate\");\r\n            " +
                    "EndDate   = new RelationFieldResult(this, \"EndDate\");\r\n");
            
            #line 205 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

	}

            
            #line default
            #line hidden
            this.Write("        }\r\n");
            
            #line 209 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

	if (DALRelation.IsTimeDependent)
    {

            
            #line default
            #line hidden
            this.Write("\r\n        public RelationFieldResult StartDate { get; private set; } \r\n        pu" +
                    "blic RelationFieldResult EndDate   { get; private set; } \r\n");
            
            #line 216 "E:\_Xirqlz\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Relationship.tt"

    }

            
            #line default
            #line hidden
            this.Write("    }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
