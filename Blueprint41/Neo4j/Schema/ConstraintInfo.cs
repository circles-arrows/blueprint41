using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blueprint41.Core;

namespace Blueprint41.Neo4j.Schema
{
    public class ConstraintInfo
    {
        internal ConstraintInfo(RawRecord record)
        {
            Initialize(record);
        }
        protected virtual void Initialize(RawRecord record)
        {
            Name = record.Values["description"].As<string>();
            IsUnique = false;
            IsMandatory = false;

            Match uniqueMatch = UniqueMatch(Name);
            Match notnullMatch = NotNullMatch(Name);
            if (!uniqueMatch.Success && !notnullMatch.Success)
                throw new NotSupportedException("One of the two regular expressions should produce a match...");

            if (uniqueMatch.Success)
            {
                Entity = uniqueMatch.Groups["entity"].Value;
                Field = uniqueMatch.Groups["field"].Value;
                IsUnique = true;
            }
            if (notnullMatch.Success)
            {
                Entity = notnullMatch.Groups["entity"].Value;
                Field = notnullMatch.Groups["field"].Value;
                IsMandatory = true;
            }
        }

        public string Name { get; protected set; } = null!;
        public string Entity { get; protected set; } = null!;
        public string Field { get; protected set; } = null!;

        public bool IsUnique { get; protected set; }
        public bool IsMandatory { get; protected set; }

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

        private static readonly Regex unique_old = new Regex(@"^ *constraint *on *\( *([a-z0-9_]+): *(?<entity>[a-z0-9_]+) *\) *assert *\1\.(?<field>[a-z0-9_]+) *is *unique *$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);
        private static readonly Regex notnull_old = new Regex(@"^ *constraint *on *\( *([a-z0-9_]+): *(?<entity>[a-z0-9_]+) *\) *assert *exists *\( *\1\.(?<field>[a-z0-9_]+) *\) *$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);
     
        private static readonly Regex unique = new Regex(@"^ *constraint *on *\( *([a-z0-9_]+): *(?<entity>[a-z0-9_]+) *\) *assert *\( *\1\.(?<field>[a-z0-9_]+) *\) *is *unique *$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);
        private static readonly Regex notnull = new Regex(@"^ *constraint *on *\( *([a-z0-9_]+): *(?<entity>[a-z0-9_]+) *\) *assert *\( *\1\.(?<field>[a-z0-9_]+) *\) *is *not *null *$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);


        // description
        // -----------
        //  CONSTRAINT ON ( genre:Genre ) ASSERT (genre.Uid) IS NOT NULL

        // CONSTRAINT ON ( account:Account ) ASSERT account.Uid IS UNIQUE
        // CONSTRAINT ON (account:Account ) ASSERT exists(account.Uid)

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

            return $"{Entity}.{Field} -> {desc}";
        }
    }
}