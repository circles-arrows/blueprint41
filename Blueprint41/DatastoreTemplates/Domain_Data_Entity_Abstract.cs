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
    using System;
    using System.IO;
    using System.Diagnostics;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class Domain_Data_Entity_Abstract : GeneratorBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write(" \r\nusing System;\r\nusing System.Linq;\r\nusing System.Collections.Generic;\r\n\r\n\r\nusin" +
                    "g Blueprint41;\r\nusing Blueprint41.Core;\r\nusing Blueprint41.Query;\r\nusing Bluepri" +
                    "nt41.DatastoreTemplates;\r\nusing q = ");
            
            #line 20 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.FullQueryNamespace));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\nnamespace ");
            
            #line 22 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.FullCRUDNamespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n");
            
            #line 24 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

if (DALModel.IsAbstract)
{

            
            #line default
            #line hidden
            this.Write("\tpublic interface I");
            
            #line 28 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("OriginalData");
            
            #line 28 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Inherits == null ? "" : string.Concat(" : I", DALModel.Inherits.Name, "OriginalData")));
            
            #line default
            #line hidden
            this.Write("\r\n    {\r\n");
            
            #line 30 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

	foreach (Property attr in DALModel.Properties)
	{
		if (attr.IsKey)
		{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 36 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.OuterReturnTypeReadOnly));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 36 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write(" { get; }\r\n");
            
            #line 37 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

		}
		else if (attr.PropertyType == PropertyType.Collection || attr.IsNodeType)
		{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 42 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.OuterReturnTypeReadOnly));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 42 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write(" { get; }\r\n");
            
            #line 43 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

			if (attr.EntityReturnType != null && attr.EntityReturnType.IsAbstract)
			{
				foreach (Entity concrete in attr.EntityReturnType.GetSubclasses())
				{
					string concreteOuterType = string.Concat("IEnumerable<", concrete.ClassName, ">");

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 50 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(concreteOuterType));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 50 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 50 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(concrete.Name));
            
            #line default
            #line hidden
            this.Write(" { get; }\r\n");
            
            #line 51 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

				}
			}
		}
		else if (attr.PropertyType == PropertyType.Lookup)
		{
			if (attr.Relationship.IsTimeDependent)
			{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 60 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.OuterReturnTypeReadOnly));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 60 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write(" { get; }\r\n");
            
            #line 61 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

			}
			else
			{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 66 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.OuterReturnTypeReadOnly));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 66 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write(" { get; }\r\n");
            
            #line 67 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

			}
		}
		else
		{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 73 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.OuterReturnTypeReadOnly));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 73 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write(" { get; }\r\n");
            
            #line 74 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

		}
	}

	if (!string.IsNullOrEmpty(DALModel.UnidentifiedProperties))
	{

            
            #line default
            #line hidden
            this.Write("        \r\n\t\t#region Member for UnidentifiedProperties\r\n\r\n        IDictionary<stri" +
                    "ng, object> ");
            
            #line 84 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.UnidentifiedProperties));
            
            #line default
            #line hidden
            this.Write(" { get; }\r\n\r\n        #endregion\r\n");
            
            #line 87 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

	}

            
            #line default
            #line hidden
            this.Write("    }\r\n\r\n\tpublic partial interface ");
            
            #line 92 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write(" : OGM");
            
            #line 92 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join("", DALModel.GetBaseTypes().Where(item => item.IsAbstract).Select(item => string.Concat(", ", item.ClassName)).ToArray())));
            
            #line default
            #line hidden
            this.Write("\r\n\t{\r\n");
            
            #line 94 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

	if((object)DALModel.NodeType == null && DALModel.GetBaseTypes().Count == 0)
    {

            
            #line default
            #line hidden
            this.Write("\t\tstring ");
            
            #line 98 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.NodeTypeName));
            
            #line default
            #line hidden
            this.Write(" { get; }\r\n");
            
            #line 99 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

	}

	foreach (Property attr in DALModel.Properties)
	{
		if (attr.PropertyType == PropertyType.Collection || attr.IsNodeType)
		{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 107 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.OuterReturnType));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 107 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write(" { get; }\r\n");
            
            #line 108 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

		}
		else
		{
			string accessors = (attr.HideSetter) ? "get;" : "get; set;";

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 114 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.OuterReturnType));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 114 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write(" { ");
            
            #line 114 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(accessors));
            
            #line default
            #line hidden
            this.Write(" }\r\n");
            
            #line 115 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

			if (attr.PropertyType == PropertyType.Lookup && attr.Relationship.IsTimeDependent)
			{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 119 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.OuterReturnType));
            
            #line default
            #line hidden
            this.Write(" Get");
            
            #line 119 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write("(DateTime? moment = null);\r\n\t\tvoid Set");
            
            #line 120 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 120 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.OuterReturnType));
            
            #line default
            #line hidden
            this.Write(" value, DateTime? moment = null);\r\n");
            
            #line 121 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

			}
		}
	}

	if (!string.IsNullOrEmpty(DALModel.UnidentifiedProperties))
	{

            
            #line default
            #line hidden
            this.Write("        \r\n\t\t#region Member for UnidentifiedProperties\r\n\r\n        IDictionary<stri" +
                    "ng, object> ");
            
            #line 132 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.UnidentifiedProperties));
            
            #line default
            #line hidden
            this.Write(" { get; }\r\n\r\n        #endregion\r\n");
            
            #line 135 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

	}

            
            #line default
            #line hidden
            this.Write("\r\n\t\t");
            
            #line 139 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Inherits == null ? "" : "new "));
            
            #line default
            #line hidden
            this.Write("I");
            
            #line 139 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("OriginalData OriginalVersion { get; }\r\n\t}\r\n\r\n\tpublic partial class ");
            
            #line 142 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write(" : OGMAbstractImpl<");
            
            #line 142 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 142 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 142 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Key.SystemReturnType));
            
            #line default
            #line hidden
            this.Write(">\r\n\t{\r\n        #region Initialize\r\n\r\n\t\tstatic ");
            
            #line 146 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("()\r\n        {\r\n            Register.Types();\r\n        }\r\n\r\n        protected over" +
                    "ride void RegisterGeneratedStoredQueries()\r\n        {\r\n");
            
            #line 153 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

    if (!DALModel.IsVirtual)
    {

            
            #line default
            #line hidden
            this.Write("            #region LoadByKeys\r\n            \r\n            RegisterQuery(nameof(Lo" +
                    "adByKeys), (query, alias) => query.\r\n                Where(alias.");
            
            #line 160 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Key.Name));
            
            #line default
            #line hidden
            this.Write(".In(Parameter.New<");
            
            #line 160 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Key.SystemReturnType));
            
            #line default
            #line hidden
            this.Write(">(Param0))));\r\n\r\n            #endregion\r\n\r\n");
            
            #line 164 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

    }

            
            #line default
            #line hidden
            this.Write("\t\t\tAdditionalGeneratedStoredQueries();\r\n        }\r\n\t\tpartial void AdditionalGener" +
                    "atedStoredQueries();\r\n");
            
            #line 170 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

    if (!DALModel.IsVirtual)
    {

            
            #line default
            #line hidden
            this.Write("        \r\n        public static Dictionary<");
            
            #line 175 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Key.SystemReturnType));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 175 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write("> LoadByKeys(IEnumerable<");
            
            #line 175 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Key.SystemReturnType));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 175 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Key.Name.ToPlural().ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(")\r\n        {\r\n            return FromQuery(nameof(LoadByKeys), new Parameter(Para" +
                    "m0, ");
            
            #line 177 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Key.Name.ToPlural().ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(".ToArray(), typeof(");
            
            #line 177 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Key.SystemReturnType));
            
            #line default
            #line hidden
            this.Write("))).ToDictionary(item=> item.");
            
            #line 177 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Key.Name));
            
            #line default
            #line hidden
            this.Write(", item => item);\r\n        }\r\n\r\n\t\tprotected static void RegisterQuery(string name," +
                    " Func<IMatchQuery, q.");
            
            #line 180 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("Alias, IWhereQuery> query)\r\n        {\r\n            q.");
            
            #line 182 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("Alias alias;\r\n\r\n            IMatchQuery matchQuery = Blueprint41.Transaction.Comp" +
                    "iledQuery.Match(q.Node.");
            
            #line 184 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write(".Alias(out alias));\r\n            IWhereQuery partial = query.Invoke(matchQuery, a" +
                    "lias);\r\n            ICompiled compiled = partial.Return(alias).Compile();\r\n\r\n\t\t\t" +
                    "RegisterQuery(name, compiled);\r\n        }\r\n");
            
            #line 190 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

    }

            
            #line default
            #line hidden
            this.Write("\r\n\t\t#endregion\r\n\r\n        private static ");
            
            #line 196 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write("Members members = null;\r\n        public static ");
            
            #line 197 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write("Members Members\r\n        {\r\n            get\r\n            {\r\n                if (m" +
                    "embers == null)\r\n                {\r\n                    lock (typeof(");
            
            #line 203 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write("))\r\n                    {\r\n                        if (members == null)\r\n        " +
                    "                    members = new ");
            
            #line 206 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write("Members();\r\n                    }\r\n                }\r\n                return memb" +
                    "ers;\r\n            }\r\n        }\r\n        public class ");
            
            #line 212 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write("Members\r\n        {\r\n            internal ");
            
            #line 214 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write("Members() { }\r\n\r\n");
            
            #line 216 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

	foreach (Entity inherited in DALModel.GetBaseTypesAndSelf())
	{

            
            #line default
            #line hidden
            this.Write("\t\t\t#region Members for interface I");
            
            #line 220 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(inherited.Name));
            
            #line default
            #line hidden
            this.Write("\r\n\r\n");
            
            #line 222 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

		foreach (Property attr in inherited.Properties)
		{

            
            #line default
            #line hidden
            this.Write("            public Property ");
            
            #line 226 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write(" { get; } = ");
            
            #line 226 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Datastore.GetType().FullName));
            
            #line default
            #line hidden
            this.Write(".Model.Entities[\"");
            
            #line 226 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(inherited.Name));
            
            #line default
            #line hidden
            this.Write("\"].Properties[\"");
            
            #line 226 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write("\"];\r\n");
            
            #line 227 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

		}

            
            #line default
            #line hidden
            this.Write("\t\t\t#endregion\r\n\r\n");
            
            #line 232 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

	}

            
            #line default
            #line hidden
            this.Write("        }\r\n");
            
            #line 236 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

    var enumProperties = DALModel.Properties.Where(item => item.EnumValues != null).ToList();

    if (enumProperties.Count != 0)
    {

            
            #line default
            #line hidden
            this.Write("\r\n        #region Enumerations\r\n\r\n");
            
            #line 245 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

		foreach (Property attr in enumProperties)
		{
			if (attr.EnumValues == null)
                continue;

            
            #line default
            #line hidden
            this.Write("        public enum ");
            
            #line 251 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.Name));
            
            #line default
            #line hidden
            this.Write("Enum\r\n        {\r\n");
            
            #line 253 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

            foreach (string value in attr.EnumValues)
            {

            
            #line default
            #line hidden
            this.Write("            ");
            
            #line 257 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(value));
            
            #line default
            #line hidden
            this.Write(",\r\n");
            
            #line 258 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

            }

            
            #line default
            #line hidden
            this.Write("        }\r\n\r\n");
            
            #line 263 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

        }

            
            #line default
            #line hidden
            this.Write("        #endregion\r\n");
            
            #line 267 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

    }

            
            #line default
            #line hidden
            this.Write("\r\n\t\tsealed public override Entity GetEntity()\r\n        {\r\n            if (entity " +
                    "== null)\r\n            {\r\n                lock (typeof(");
            
            #line 275 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.ClassName));
            
            #line default
            #line hidden
            this.Write("))\r\n                {\r\n                    if (entity == null)\r\n                 " +
                    "       entity = ");
            
            #line 278 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Datastore.GetType().FullName));
            
            #line default
            #line hidden
            this.Write(".Model.Entities[\"");
            
            #line 278 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("\"];\r\n                }\r\n            }\r\n            return entity;\r\n        }\r\n\t}\r" +
                    "\n");
            
            #line 284 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_Entity_Abstract.tt"

}

            
            #line default
            #line hidden
            this.Write("}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
