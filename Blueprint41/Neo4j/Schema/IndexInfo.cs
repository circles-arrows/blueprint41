﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence.Void;

namespace Blueprint41.Neo4j.Schema
{
    public class IndexInfo
    {
        internal IndexInfo(RawRecord record, Neo4jPersistenceProvider persistenceProvider)
        {
            PersistenceProvider = persistenceProvider;
            Initialize(record);
        }

        protected Neo4jPersistenceProvider PersistenceProvider { get; private set; }
        protected bool SupportsRelationshipIndexes => PersistenceProvider.VersionGreaterOrEqual(5, 7);

        protected virtual void Initialize(RawRecord record)
        {
            EntityType = "NODE";
            Name = record.Values["description"].As<string>();
            State = record.Values["state"].As<string>();
            Type = record.Values["type"].As<string>();
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

        private static readonly Regex index = new Regex(@"^ *index* on *: *(?<entity>[a-z0-9_]+) *\( *(?<field>[a-z0-9_]+) *\) *$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);

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
