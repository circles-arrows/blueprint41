﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Blueprint41.DatastoreTemplates
{
    using System.Linq;
    using System.Collections.Generic;
    using Blueprint41;
    using Blueprint41.Core;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class Domain_Data_GraphEvents : GeneratorBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write(@"#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable CS8981 // Names should not be lower type only

using System;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Events;
using Blueprint41.DatastoreTemplates;

namespace ");
            
            #line 16 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.FullCRUDNamespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    public static class GraphEvents\r\n    {\r\n        public static class Node" +
                    "s\r\n        {\r\n");
            
            #line 22 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"

List<string> nodeEvents = new List<string>();
nodeEvents.Add("OnNodeLoading");
nodeEvents.Add("OnNodeLoaded");
nodeEvents.Add("OnBatchFinished");
nodeEvents.Add("OnNodeCreate");
nodeEvents.Add("OnNodeCreated");
nodeEvents.Add("OnNodeUpdate");
nodeEvents.Add("OnNodeUpdated");
nodeEvents.Add("OnNodeDelete");
nodeEvents.Add("OnNodeDeleted");

foreach (var DALModel in Datastore.Entities.Where(item => !item.IsAbstract).OrderBy(item => item.Name))
{	

            
            #line default
            #line hidden
            this.Write("            public static class ");
            
            #line 37 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("\r\n            {\r\n                public static readonly Entity Entity = ");
            
            #line 39 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Datastore.GetType().FullName.Replace("+", ".")));
            
            #line default
            #line hidden
            this.Write(".Model.Entities[\"");
            
            #line 39 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("\"];\r\n\r\n");
            
            #line 41 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"

    foreach (var evnt in nodeEvents)
    {
        string eventargs = "NodeEventArgs";

            
            #line default
            #line hidden
            this.Write("                #region ");
            
            #line 46 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt));
            
            #line default
            #line hidden
            this.Write("\r\n\r\n                private static bool ");
            
            #line 48 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered = false;\r\n\r\n                private static event EventHandler<Entity" +
                    ", ");
            
            #line 50 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(eventargs));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 50 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(";\r\n                public static event EventHandler<Entity, ");
            
            #line 51 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(eventargs));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 51 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt));
            
            #line default
            #line hidden
            this.Write("\r\n                {\r\n                    add\r\n                    {\r\n            " +
                    "            lock (Entity)\r\n                        {\r\n                          " +
                    "  if (!");
            
            #line 57 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered)\r\n                            {\r\n                                Ent" +
                    "ity.Events.");
            
            #line 59 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt));
            
            #line default
            #line hidden
            this.Write(" += ");
            
            #line 59 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Proxy;\r\n                                ");
            
            #line 60 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered = true;\r\n                            }\r\n                            " +
                    "");
            
            #line 62 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(" += value;\r\n                        }\r\n                    }\r\n                   " +
                    " remove\r\n                    {\r\n                        lock (Entity)\r\n         " +
                    "               {\r\n                            ");
            
            #line 69 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(" -= value;\r\n                            if (");
            
            #line 70 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(" is null && ");
            
            #line 70 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered)\r\n                            {\r\n                                Ent" +
                    "ity.Events.");
            
            #line 72 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt));
            
            #line default
            #line hidden
            this.Write(" -= ");
            
            #line 72 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Proxy;\r\n                                ");
            
            #line 73 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered = false;\r\n                            }\r\n                        }\r\n" +
                    "                    }\r\n                }\r\n\r\n                private static void " +
                    "");
            
            #line 79 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Proxy(object sender, ");
            
            #line 79 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(eventargs));
            
            #line default
            #line hidden
            this.Write(" args)\r\n                {\r\n                    EventHandler<Entity, ");
            
            #line 81 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(eventargs));
            
            #line default
            #line hidden
            this.Write("> handler = ");
            
            #line 81 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(";\r\n                    if (handler is not null)\r\n                        handler." +
                    "Invoke((Entity)sender, args);\r\n                }\r\n\r\n                #endregion\r\n" +
                    "");
            
            #line 87 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"

    }  

            
            #line default
            #line hidden
            this.Write("            }\r\n");
            
            #line 91 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"

}

            
            #line default
            #line hidden
            this.Write("        }\r\n        public static class Relationships\r\n        {\r\n");
            
            #line 97 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"

List<string> relationEvents = new List<string>();
relationEvents.Add("OnRelationCreate");
relationEvents.Add("OnRelationCreated");
relationEvents.Add("OnRelationDelete");
relationEvents.Add("OnRelationDeleted");

foreach (var DALModel in Datastore.Relations.OrderBy(item => item.Name))
{	

            
            #line default
            #line hidden
            this.Write("            public static class ");
            
            #line 107 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("\r\n            {\r\n                public static readonly Relationship Relationship" +
                    " = ");
            
            #line 109 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Datastore.GetType().FullName.Replace("+", ".")));
            
            #line default
            #line hidden
            this.Write(".Model.Relations[\"");
            
            #line 109 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("\"];\r\n\r\n");
            
            #line 111 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"

    foreach (var evnt in relationEvents)
    {
        string eventargs = "RelationshipEventArgs";

            
            #line default
            #line hidden
            this.Write("                #region ");
            
            #line 116 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt));
            
            #line default
            #line hidden
            this.Write("\r\n\r\n                private static bool ");
            
            #line 118 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered = false;\r\n\r\n                private static event EventHandler<Relati" +
                    "onship, ");
            
            #line 120 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(eventargs));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 120 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(";\r\n                public static event EventHandler<Relationship, ");
            
            #line 121 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(eventargs));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 121 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt));
            
            #line default
            #line hidden
            this.Write("\r\n                {\r\n                    add\r\n                    {\r\n            " +
                    "            lock (Relationship)\r\n                        {\r\n                    " +
                    "        if (!");
            
            #line 127 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered)\r\n                            {\r\n                                Rel" +
                    "ationship.Events.");
            
            #line 129 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt));
            
            #line default
            #line hidden
            this.Write(" += ");
            
            #line 129 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Proxy;\r\n                                ");
            
            #line 130 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered = true;\r\n                            }\r\n                            " +
                    "");
            
            #line 132 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(" += value;\r\n                        }\r\n                    }\r\n                   " +
                    " remove\r\n                    {\r\n                        lock (Relationship)\r\n   " +
                    "                     {\r\n                            ");
            
            #line 139 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(" -= value;\r\n                            if (");
            
            #line 140 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(" is null && ");
            
            #line 140 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered)\r\n                            {\r\n                                Rel" +
                    "ationship.Events.");
            
            #line 142 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt));
            
            #line default
            #line hidden
            this.Write(" -= ");
            
            #line 142 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Proxy;\r\n                                ");
            
            #line 143 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered = false;\r\n                            }\r\n                        }\r\n" +
                    "                    }\r\n                }\r\n\r\n                private static void " +
                    "");
            
            #line 149 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Proxy(object sender, ");
            
            #line 149 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(eventargs));
            
            #line default
            #line hidden
            this.Write(" args)\r\n                {\r\n                    EventHandler<Relationship, ");
            
            #line 151 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(eventargs));
            
            #line default
            #line hidden
            this.Write("> handler = ");
            
            #line 151 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(";\r\n                    if (handler is not null)\r\n                        handler." +
                    "Invoke((Relationship)sender, args);\r\n                }\r\n\r\n                #endre" +
                    "gion\r\n");
            
            #line 157 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"

    }  

            
            #line default
            #line hidden
            this.Write("            }\r\n");
            
            #line 161 "C:\_CirclesArrows\blueprint41\Version 2\Blueprint41.OGM\DatastoreTemplates\Domain_Data_GraphEvents.tt"

}

            
            #line default
            #line hidden
            this.Write("        }\r\n    }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}