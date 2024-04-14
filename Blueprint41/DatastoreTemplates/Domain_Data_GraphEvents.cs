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
    
    #line 1 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class Domain_Data_GraphEvents : GeneratorBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("#pragma warning disable S101 // Types should be named in PascalCase\r\n#pragma warn" +
                    "ing disable CS8981 // Types should not be in lower case\r\n\r\nusing System;\r\n\r\nusin" +
                    "g Blueprint41;\r\nusing Blueprint41.Core;\r\nusing Blueprint41.DatastoreTemplates;\r\n" +
                    "\r\nnamespace ");
            
            #line 15 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.FullCRUDNamespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    public static class GraphEvents\r\n    {\r\n        public static class Node" +
                    "s\r\n        {\r\n");
            
            #line 21 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"

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
            
            #line 36 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("\r\n            {\r\n                public static readonly Entity Entity = ");
            
            #line 38 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Datastore.GetType().FullName.Replace("+", ".")));
            
            #line default
            #line hidden
            this.Write(".Model.Entities[\"");
            
            #line 38 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("\"];\r\n\r\n");
            
            #line 40 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"

    foreach (var evnt in nodeEvents)
    {
        string eventargs = "NodeEventArgs";

            
            #line default
            #line hidden
            this.Write("                #region ");
            
            #line 45 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt));
            
            #line default
            #line hidden
            this.Write("\r\n\r\n                private static bool ");
            
            #line 47 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered = false;\r\n\r\n                private static event EventHandler<Entity" +
                    ", ");
            
            #line 49 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(eventargs));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 49 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(";\r\n                public static event EventHandler<Entity, ");
            
            #line 50 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(eventargs));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 50 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt));
            
            #line default
            #line hidden
            this.Write("\r\n                {\r\n                    add\r\n                    {\r\n            " +
                    "            lock (Entity)\r\n                        {\r\n                          " +
                    "  if (!");
            
            #line 56 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered)\r\n                            {\r\n                                Ent" +
                    "ity.Events.");
            
            #line 58 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt));
            
            #line default
            #line hidden
            this.Write(" += ");
            
            #line 58 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Proxy;\r\n                                ");
            
            #line 59 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered = true;\r\n                            }\r\n                            " +
                    "");
            
            #line 61 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(" += value;\r\n                        }\r\n                    }\r\n                   " +
                    " remove\r\n                    {\r\n                        lock (Entity)\r\n         " +
                    "               {\r\n                            ");
            
            #line 68 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(" -= value;\r\n                            if (");
            
            #line 69 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(" is null && ");
            
            #line 69 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered)\r\n                            {\r\n                                Ent" +
                    "ity.Events.");
            
            #line 71 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt));
            
            #line default
            #line hidden
            this.Write(" -= ");
            
            #line 71 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Proxy;\r\n                                ");
            
            #line 72 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered = false;\r\n                            }\r\n                        }\r\n" +
                    "                    }\r\n                }\r\n\r\n                private static void " +
                    "");
            
            #line 78 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Proxy(object sender, ");
            
            #line 78 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(eventargs));
            
            #line default
            #line hidden
            this.Write(" args)\r\n                {\r\n                    EventHandler<Entity, ");
            
            #line 80 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(eventargs));
            
            #line default
            #line hidden
            this.Write("> handler = ");
            
            #line 80 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(";\r\n                    if (handler is not null)\r\n                        handler." +
                    "Invoke((Entity)sender, args);\r\n                }\r\n\r\n                #endregion\r\n" +
                    "");
            
            #line 86 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"

    }  

            
            #line default
            #line hidden
            this.Write("            }\r\n");
            
            #line 90 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"

}

            
            #line default
            #line hidden
            this.Write("        }\r\n        public static class Relationships\r\n        {\r\n");
            
            #line 96 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"

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
            
            #line 106 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("\r\n            {\r\n                public static readonly Relationship Relationship" +
                    " = ");
            
            #line 108 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Datastore.GetType().FullName.Replace("+", ".")));
            
            #line default
            #line hidden
            this.Write(".Model.Relations[\"");
            
            #line 108 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DALModel.Name));
            
            #line default
            #line hidden
            this.Write("\"];\r\n\r\n");
            
            #line 110 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"

    foreach (var evnt in relationEvents)
    {
        string eventargs = "RelationshipEventArgs";

            
            #line default
            #line hidden
            this.Write("                #region ");
            
            #line 115 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt));
            
            #line default
            #line hidden
            this.Write("\r\n\r\n                private static bool ");
            
            #line 117 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered = false;\r\n\r\n                private static event EventHandler<Relati" +
                    "onship, ");
            
            #line 119 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(eventargs));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 119 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(";\r\n                public static event EventHandler<Relationship, ");
            
            #line 120 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(eventargs));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 120 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt));
            
            #line default
            #line hidden
            this.Write("\r\n                {\r\n                    add\r\n                    {\r\n            " +
                    "            lock (Relationship)\r\n                        {\r\n                    " +
                    "        if (!");
            
            #line 126 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered)\r\n                            {\r\n                                Rel" +
                    "ationship.Events.");
            
            #line 128 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt));
            
            #line default
            #line hidden
            this.Write(" += ");
            
            #line 128 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Proxy;\r\n                                ");
            
            #line 129 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered = true;\r\n                            }\r\n                            " +
                    "");
            
            #line 131 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(" += value;\r\n                        }\r\n                    }\r\n                   " +
                    " remove\r\n                    {\r\n                        lock (Relationship)\r\n   " +
                    "                     {\r\n                            ");
            
            #line 138 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(" -= value;\r\n                            if (");
            
            #line 139 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(" is null && ");
            
            #line 139 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered)\r\n                            {\r\n                                Rel" +
                    "ationship.Events.");
            
            #line 141 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt));
            
            #line default
            #line hidden
            this.Write(" -= ");
            
            #line 141 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Proxy;\r\n                                ");
            
            #line 142 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("IsRegistered = false;\r\n                            }\r\n                        }\r\n" +
                    "                    }\r\n                }\r\n\r\n                private static void " +
                    "");
            
            #line 148 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Proxy(object sender, ");
            
            #line 148 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(eventargs));
            
            #line default
            #line hidden
            this.Write(" args)\r\n                {\r\n                    EventHandler<Relationship, ");
            
            #line 150 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(eventargs));
            
            #line default
            #line hidden
            this.Write("> handler = ");
            
            #line 150 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(evnt.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write(";\r\n                    if (handler is not null)\r\n                        handler." +
                    "Invoke((Relationship)sender, args);\r\n                }\r\n\r\n                #endre" +
                    "gion\r\n");
            
            #line 156 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"

    }  

            
            #line default
            #line hidden
            this.Write("            }\r\n");
            
            #line 160 "C:\_CirclesArrows\blueprint41\Blueprint41\DatastoreTemplates\Domain_Data_GraphEvents.tt"

}

            
            #line default
            #line hidden
            this.Write("        }\r\n    }\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
