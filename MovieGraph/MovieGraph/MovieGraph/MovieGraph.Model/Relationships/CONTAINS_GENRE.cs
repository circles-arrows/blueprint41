using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class CONTAINS_GENRE_REL : RELATIONSHIP, IFromIn_CONTAINS_GENRE_REL, IFromOut_CONTAINS_GENRE_REL    {
        public override string NEO4J_TYPE
        {
            get
            {
                return "CONTAINS_GENRE";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
        internal CONTAINS_GENRE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

        public CONTAINS_GENRE_REL Alias(out CONTAINS_GENRE_ALIAS alias)
        {
            alias = new CONTAINS_GENRE_ALIAS(this);
            RelationshipAlias = alias;
            return this;
        } 
        public CONTAINS_GENRE_REL Repeat(int maxHops)
        {
            return Repeat(1, maxHops);
        }
        public new CONTAINS_GENRE_REL Repeat(int minHops, int maxHops)
        {
            base.Repeat(minHops, maxHops);
            return this;
        }

        IFromIn_CONTAINS_GENRE_REL IFromIn_CONTAINS_GENRE_REL.Alias(out CONTAINS_GENRE_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromOut_CONTAINS_GENRE_REL IFromOut_CONTAINS_GENRE_REL.Alias(out CONTAINS_GENRE_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromIn_CONTAINS_GENRE_REL IFromIn_CONTAINS_GENRE_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromIn_CONTAINS_GENRE_REL IFromIn_CONTAINS_GENRE_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }
        IFromOut_CONTAINS_GENRE_REL IFromOut_CONTAINS_GENRE_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromOut_CONTAINS_GENRE_REL IFromOut_CONTAINS_GENRE_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }


        public CONTAINS_GENRE_IN In { get { return new CONTAINS_GENRE_IN(this); } }
        public class CONTAINS_GENRE_IN
        {
            private CONTAINS_GENRE_REL Parent;
            internal CONTAINS_GENRE_IN(CONTAINS_GENRE_REL parent)
            {
                Parent = parent;
            }

            public MovieNode Movie { get { return new MovieNode(Parent, DirectionEnum.In); } }
        }

        public CONTAINS_GENRE_OUT Out { get { return new CONTAINS_GENRE_OUT(this); } }
        public class CONTAINS_GENRE_OUT
        {
            private CONTAINS_GENRE_REL Parent;
            internal CONTAINS_GENRE_OUT(CONTAINS_GENRE_REL parent)
            {
                Parent = parent;
            }

            public GenreNode Genre { get { return new GenreNode(Parent, DirectionEnum.Out); } }
        }
    }

    public interface IFromIn_CONTAINS_GENRE_REL
    {
        IFromIn_CONTAINS_GENRE_REL Alias(out CONTAINS_GENRE_ALIAS alias);
        IFromIn_CONTAINS_GENRE_REL Repeat(int maxHops);
        IFromIn_CONTAINS_GENRE_REL Repeat(int minHops, int maxHops);

        CONTAINS_GENRE_REL.CONTAINS_GENRE_OUT Out { get; }
    }
    public interface IFromOut_CONTAINS_GENRE_REL
    {
        IFromOut_CONTAINS_GENRE_REL Alias(out CONTAINS_GENRE_ALIAS alias);
        IFromOut_CONTAINS_GENRE_REL Repeat(int maxHops);
        IFromOut_CONTAINS_GENRE_REL Repeat(int minHops, int maxHops);

        CONTAINS_GENRE_REL.CONTAINS_GENRE_IN In { get; }
    }

    public class CONTAINS_GENRE_ALIAS : AliasResult
    {
        private CONTAINS_GENRE_REL Parent;

        internal CONTAINS_GENRE_ALIAS(CONTAINS_GENRE_REL parent)
        {
            Parent = parent;

            CreationDate = new DateTimeResult(this, "CreationDate", MovieGraph.Model.Datastore.Model.Relations["CONTAINS_GENRE"], MovieGraph.Model.Datastore.Model.Relations["CONTAINS_GENRE"].Properties["CreationDate"]);
        }

        public Assignment[] Assign(JsNotation<System.DateTime?> CreationDate = default)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (CreationDate.HasValue) assignments.Add(new Assignment(this.CreationDate, CreationDate));

            return assignments.ToArray();
        }

        public DateTimeResult CreationDate { get; private set; } 
    }
}
