using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Blueprint41.Core;
using Blueprint41.Persistence;

namespace Blueprint41.Refactoring.Schema
{
    public class ConstraintInfo
    {
        internal ConstraintInfo(IReadOnlyDictionary<string, object> record, PersistenceProvider persistenceProvider)
        {
            PersistenceProvider = persistenceProvider;
            Initialize(record);
        }

        protected PersistenceProvider PersistenceProvider { get; private set; }

        protected virtual void Initialize(IReadOnlyDictionary<string, object> record)
        {
            EntityType = "NODE";
            Name = record["description"].As<string>();
            IsUnique = false;
            IsMandatory = false;

            Match uniqueMatch = UniqueMatch(Name);
            Match notnullMatch = NotNullMatch(Name);
            if (!uniqueMatch.Success && !notnullMatch.Success)
                throw new NotSupportedException("One of the two regular expressions should produce a match...");

            if (uniqueMatch.Success)
            {
                Entity = new string[] { uniqueMatch.Groups["entity"].Value };
                Field = new string[] { uniqueMatch.Groups["field"].Value };
                IsUnique = true;
            }
            if (notnullMatch.Success)
            {
                Entity = new string[] { notnullMatch.Groups["entity"].Value };
                Field = new string[] { notnullMatch.Groups["field"].Value };
                IsMandatory = true;
            }
        }

        public string Name { get; protected set; } = null!;
        public string EntityType { get; protected set; } = null!;
        public IReadOnlyList<string> Entity { get; protected set; } = null!;
        public IReadOnlyList<string> Field { get; protected set; } = null!;

        public bool IsUnique { get; protected set; }
        public bool IsMandatory { get; protected set; }
        public virtual bool IsKey => IsUnique && IsMandatory;

        #region Regex

        internal virtual Match UniqueMatch(string text)
        {
            Match result = unique.Match(text);

            if (!result.Success)
                result = unique_old.Match(text);

            return result;
        }
        internal virtual Match NotNullMatch(string text)
        {
            Match result = notnull.Match(text);

            if (!result.Success)
                result = notnull_old.Match(text);

            return result;
        }

#pragma warning disable S6444

        private static readonly Regex unique_old = new Regex(@"^ *constraint *on *\( *([a-z0-9_]+): *(?<entity>[a-z0-9_]+) *\) *assert *\1\.(?<field>[a-z0-9_]+) *is *unique *$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);
        private static readonly Regex unique = new Regex(@"^ *constraint *on *\( *([a-z0-9_]+): *(?<entity>[a-z0-9_]+) *\) *assert *\( *\1\.(?<field>[a-z0-9_]+) *\) *is *unique *$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);
        private static readonly Regex notnull_old = new Regex(@"^ *constraint *on *\( *([a-z0-9_]+): *(?<entity>[a-z0-9_]+) *\) *assert *exists *\( *\1\.(?<field>[a-z0-9_]+) *\) *$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);
        private static readonly Regex notnull = new Regex(@"^ *constraint *on *\( *([a-z0-9_]+): *(?<entity>[a-z0-9_]+) *\) *assert *\( *\1\.(?<field>[a-z0-9_]+) *\) *is *not *null *$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);

#pragma warning restore S6444

        // description
        // -----------

        // old: CONSTRAINT ON ( account:Account ) ASSERT account.Uid IS UNIQUE
        // new: CONSTRAINT ON ( account:Account ) ASSERT (account.Uid) IS UNIQUE
        // old: CONSTRAINT ON (account:Account ) ASSERT exists(account.Uid)
        // new: CONSTRAINT ON ( account:Account ) ASSERT (account.Uid) IS NOT NULL

        /*
            CREATE CONSTRAINT ON (n:Person) ASSERT n.name IS UNIQUE;
            CREATE CONSTRAINT ON (book:Book) ASSERT exists(book.isbn)
            DROP CONSTRAINT ON (book:Book) ASSERT exists(book.isbn)
            CREATE CONSTRAINT ON ()-[like:LIKED]-() ASSERT exists(like.day)
            DROP CONSTRAINT ON ()-[like:LIKED]-() ASSERT exists(like.day)
        */

        #endregion

        public override string ToString()
        {
            string desc = "";
            if (IsUnique)
                desc = "unique";
            if (IsMandatory)
                desc = "mandatory";

            return $"{string.Join("/", Entity)}.{string.Join("/", Field)} -> {desc}";
        }
    }
}