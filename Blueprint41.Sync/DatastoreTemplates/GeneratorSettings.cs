using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Sync.DatastoreTemplates
{
    public class GeneratorSettings
    {
        public GeneratorSettings(string projectFolder, string projectNamespace = "Datastore")
        {
            ProjectNamespace = projectNamespace;
            CRUDNamespace = "Manipulation";
            QueryNamespace = "Query";

            ProjectFolder = projectFolder;
            EntitiesFolder = "Entities";
            NodesFolder = "Nodes";
            RelationshipsFolder = "Relationships";
        }

        public string ProjectFolder { get; }
        public string ProjectNamespace { get; }

        public string CRUDNamespace { get; set; }
        public string QueryNamespace { get; set; }
        public string EntitiesFolder { get; set; }
        public string NodesFolder { get; set; }
        public string RelationshipsFolder { get; set; }

        public string FullCRUDNamespace
        {
            get
            {
                if (string.IsNullOrEmpty(ProjectNamespace))
                    return CRUDNamespace ?? "";

                if (string.IsNullOrEmpty(CRUDNamespace))
                    return ProjectNamespace ?? "";

                return string.Join(".", ProjectNamespace, CRUDNamespace);
            }
        }
        public string FullQueryNamespace
        {
            get
            {
                if (string.IsNullOrEmpty(ProjectNamespace))
                    return QueryNamespace ?? "";

                if (string.IsNullOrEmpty(QueryNamespace))
                    return ProjectNamespace ?? "";

                return string.Join(".", ProjectNamespace, QueryNamespace);
            }
        }
    }
}
