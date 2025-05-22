using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Blueprint41.DatastoreTemplates
{
    public class GeneratorSettings : IEnumerable<GeneratorFlavorSettings>
    {
        private const string ASYNC    = "Async";
        private const string BLOCKING = "Sync";

        public GeneratorSettings(string projectFolder, string projectNamespace = "Datastore", EntityFlavor flavor = EntityFlavor.Both)
        {
            ProjectNamespace = projectNamespace;
            ProjectFolder = projectFolder;

            Blocking = flavor.HasFlag(EntityFlavor.Blocking) ? new GeneratorFlavorSettings(this, EntityFlavor.Blocking, BLOCKING) : null;
            Async    = flavor.HasFlag(EntityFlavor.Async)    ? new GeneratorFlavorSettings(this, EntityFlavor.Async,    ASYNC)    : null;

            settings = new Lazy<IReadOnlyList<GeneratorFlavorSettings>>(delegate ()
            {
                return new List<GeneratorFlavorSettings?> { Blocking, Async }.Where(item => item is not null).ToArray()!;

            }, true);
        }

        public GeneratorSettings SetNamespaces(string? crudNamespace = null, string? queryNamespace = null)
        {
            Blocking?.SetNamespaces(Amend(crudNamespace, BLOCKING), Amend(queryNamespace, BLOCKING));
            Async?.   SetNamespaces(Amend(crudNamespace, ASYNC),    Amend(queryNamespace, ASYNC));

            return this;

            static string? Amend(string? original, string amendment)
            {
                if (original is null)
                    return null;

                return string.Concat(original, ".", amendment);
            }
        }
        public GeneratorSettings SetFolders(string? entitiesFolder = null, string? nodesFolder = null, string? relationshipsFolder = null)
        {
            Blocking?.SetFolders(Amend(entitiesFolder, BLOCKING), Amend(nodesFolder, BLOCKING), Amend(relationshipsFolder, BLOCKING));
            Async?.   SetFolders(Amend(entitiesFolder, ASYNC),    Amend(nodesFolder, ASYNC),    Amend(relationshipsFolder, ASYNC));

            return this;

            static string? Amend(string? original, string amendment)
            {
                if (original is null)
                    return null;

                return string.Concat(original, Path.DirectorySeparatorChar, amendment);
            }
        }

        public string ProjectFolder { get; }
        public string ProjectNamespace { get; }

        public GeneratorFlavorSettings? Blocking { get; }
        public GeneratorFlavorSettings? Async { get; }

        public IEnumerator<GeneratorFlavorSettings> GetEnumerator()
        {
            return settings.Value.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private readonly Lazy<IReadOnlyList<GeneratorFlavorSettings>> settings;
    }

    public class GeneratorFlavorSettings
    {
        internal GeneratorFlavorSettings(GeneratorSettings parent, EntityFlavor flavor, string postfix)
        {
            Parent = parent;

            CRUDNamespace       = string.Concat("Manipulation.", postfix);
            QueryNamespace      = string.Concat("Query.",        postfix);

            EntitiesFolder      = string.Concat("Manipulation",  Path.DirectorySeparatorChar, postfix);
            NodesFolder         = string.Concat("Query",         Path.DirectorySeparatorChar, postfix);
            RelationshipsFolder = string.Concat("Query",         Path.DirectorySeparatorChar, postfix);

            Flavor = flavor;
        }

        public GeneratorFlavorSettings SetNamespaces(string? crudNamespace = null, string? queryNamespace = null)
        {
            if (crudNamespace is not null)
                CRUDNamespace = crudNamespace;

            if (queryNamespace is not null)
                QueryNamespace = queryNamespace;

            return this;
        }
        public GeneratorFlavorSettings SetFolders(string? entitiesFolder = null, string? nodesFolder = null, string? relationshipsFolder = null)
        {
            if (entitiesFolder is not null)
                EntitiesFolder = entitiesFolder;

            if (nodesFolder is not null)
                NodesFolder = nodesFolder;

            if (relationshipsFolder is not null)
                RelationshipsFolder = relationshipsFolder;

            return this;
        }

        private GeneratorSettings Parent { get; }
        public EntityFlavor Flavor { get; }

        public string ProjectFolder => Parent.ProjectFolder;
        public string ProjectNamespace => Parent.ProjectNamespace;

        public string CRUDNamespace { get; private set; }
        public string QueryNamespace { get; private set; }

        public string EntitiesFolder { get; set; }
        public string NodesFolder { get; set; }
        public string RelationshipsFolder { get; set; }

        public string FullCRUDNamespace
        {
            get
            {
                if (string.IsNullOrEmpty(Parent.ProjectNamespace))
                    return CRUDNamespace ?? "";

                if (string.IsNullOrEmpty(CRUDNamespace))
                    return Parent.ProjectNamespace ?? "";

                return string.Join(".", Parent.ProjectNamespace, CRUDNamespace);
            }
        }
        public string FullQueryNamespace
        {
            get
            {
                if (string.IsNullOrEmpty(Parent.ProjectNamespace))
                    return QueryNamespace ?? "";

                if (string.IsNullOrEmpty(QueryNamespace))
                    return Parent.ProjectNamespace ?? "";

                return string.Join(".", Parent.ProjectNamespace, QueryNamespace);
            }
        }
    }


    [Flags]
    public enum EntityFlavor
    {
        Blocking = 1,
        Async = 2,
        Both = 3,
    }
}
