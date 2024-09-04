using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Blueprint41.Core;
using Blueprint41.Persistence;

namespace Blueprint41.Refactoring.Schema
{
    public class IndexInfo
    {
        internal IndexInfo(IDictionary<string, object> record, PersistenceProvider persistenceProvider)
        {
            PersistenceProvider = persistenceProvider;
            Initialize(record);
        }

        protected PersistenceProvider PersistenceProvider { get; private set; }
        protected bool SupportsRelationshipIndexes => PersistenceProvider.VersionGreaterOrEqual(5, 7);

        protected virtual void Initialize(IDictionary<string, object> record)
        {
            EntityType = "NODE";
            Name = record["description"].As<string>();
            State = record["state"].As<string>();
            Type = record["type"].As<string>();
            IsIndexed = true;

            switch (Type.ToLowerInvariant().Trim())
            {
                case "node_unique_property":
                    isUnique = true;
                    break;
                case "node_label_property":
                    isUnique = false;
                    break;
                default:
                    throw new NotSupportedException("There should be no unknown types of indexes");
            }

            Match results = index.Match(Name);
            if (!results.Success)
                throw new NotSupportedException("The regular expression should always produce a match...");

            Entity = new string[] { results.Groups["entity"].Value };
            Field = new string[] { results.Groups["field"].Value };
        }

        public string Name { get; protected set; } = null!;
        public string State { get; protected set; } = null!;
        public string Type { get; protected set; } = null!;
        public string EntityType { get; protected set; } = null!;
        public string OwningConstraint { get; protected set; } = null!;

        public IReadOnlyList<string> Entity { get; protected set; } = null!;
        public IReadOnlyList<string> Field { get; protected set; } = null!;

        [Obsolete("Your code should prefer the use of Constraint.IsUnique to check if a field is unique.", false)]
        public bool IsUnique { get { return isUnique; } }
        protected bool isUnique = false;
        public bool IsIndexed { get; protected set; }

        #region Regex

#pragma warning disable S6444

        private static readonly Regex index = new Regex(@"^ *index* on *: *(?<entity>[a-z0-9_]+) *\( *(?<field>[a-z0-9_]+) *\) *$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);

#pragma warning restore S6444

        // description                  state       type
        // -----------                  -----       ----
        // INDEX ON :Title(Uid)         online      node_unique_property
        // INDEX ON :Vessel(CallSign)   online      node_label_property

        /*
            CREATE INDEX ON :Person(name)
        */

        #endregion

        public override string ToString()
        {
            string desc = "";
            if (IsIndexed)
                desc = "indexed";
            if (isUnique)
                desc = "unique";

            return $"{string.Join("/", Entity)}.{string.Join("/", Field)} -> {desc}";
        }
    }
}
