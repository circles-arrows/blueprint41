using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class MOVIEROLE_HAS_MOVIE_REL : RELATIONSHIP, IFromIn_MOVIEROLE_HAS_MOVIE_REL, IFromOut_MOVIEROLE_HAS_MOVIE_REL    {
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_MOVIE";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
        internal MOVIEROLE_HAS_MOVIE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

        public MOVIEROLE_HAS_MOVIE_REL Alias(out MOVIEROLE_HAS_MOVIE_ALIAS alias)
        {
            alias = new MOVIEROLE_HAS_MOVIE_ALIAS(this);
            RelationshipAlias = alias;
            return this;
        } 
        public MOVIEROLE_HAS_MOVIE_REL Repeat(int maxHops)
        {
            return Repeat(1, maxHops);
        }
        public new MOVIEROLE_HAS_MOVIE_REL Repeat(int minHops, int maxHops)
        {
            base.Repeat(minHops, maxHops);
            return this;
        }

        IFromIn_MOVIEROLE_HAS_MOVIE_REL IFromIn_MOVIEROLE_HAS_MOVIE_REL.Alias(out MOVIEROLE_HAS_MOVIE_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromOut_MOVIEROLE_HAS_MOVIE_REL IFromOut_MOVIEROLE_HAS_MOVIE_REL.Alias(out MOVIEROLE_HAS_MOVIE_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromIn_MOVIEROLE_HAS_MOVIE_REL IFromIn_MOVIEROLE_HAS_MOVIE_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromIn_MOVIEROLE_HAS_MOVIE_REL IFromIn_MOVIEROLE_HAS_MOVIE_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }
        IFromOut_MOVIEROLE_HAS_MOVIE_REL IFromOut_MOVIEROLE_HAS_MOVIE_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromOut_MOVIEROLE_HAS_MOVIE_REL IFromOut_MOVIEROLE_HAS_MOVIE_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }


        public MOVIEROLE_HAS_MOVIE_IN In { get { return new MOVIEROLE_HAS_MOVIE_IN(this); } }
        public class MOVIEROLE_HAS_MOVIE_IN
        {
            private MOVIEROLE_HAS_MOVIE_REL Parent;
            internal MOVIEROLE_HAS_MOVIE_IN(MOVIEROLE_HAS_MOVIE_REL parent)
            {
                Parent = parent;
            }

            public MovieRoleNode MovieRole { get { return new MovieRoleNode(Parent, DirectionEnum.In); } }
        }

        public MOVIEROLE_HAS_MOVIE_OUT Out { get { return new MOVIEROLE_HAS_MOVIE_OUT(this); } }
        public class MOVIEROLE_HAS_MOVIE_OUT
        {
            private MOVIEROLE_HAS_MOVIE_REL Parent;
            internal MOVIEROLE_HAS_MOVIE_OUT(MOVIEROLE_HAS_MOVIE_REL parent)
            {
                Parent = parent;
            }

            public MovieNode Movie { get { return new MovieNode(Parent, DirectionEnum.Out); } }
        }
    }

    public interface IFromIn_MOVIEROLE_HAS_MOVIE_REL
    {
        IFromIn_MOVIEROLE_HAS_MOVIE_REL Alias(out MOVIEROLE_HAS_MOVIE_ALIAS alias);
        IFromIn_MOVIEROLE_HAS_MOVIE_REL Repeat(int maxHops);
        IFromIn_MOVIEROLE_HAS_MOVIE_REL Repeat(int minHops, int maxHops);

        MOVIEROLE_HAS_MOVIE_REL.MOVIEROLE_HAS_MOVIE_OUT Out { get; }
    }
    public interface IFromOut_MOVIEROLE_HAS_MOVIE_REL
    {
        IFromOut_MOVIEROLE_HAS_MOVIE_REL Alias(out MOVIEROLE_HAS_MOVIE_ALIAS alias);
        IFromOut_MOVIEROLE_HAS_MOVIE_REL Repeat(int maxHops);
        IFromOut_MOVIEROLE_HAS_MOVIE_REL Repeat(int minHops, int maxHops);

        MOVIEROLE_HAS_MOVIE_REL.MOVIEROLE_HAS_MOVIE_IN In { get; }
    }

    public class MOVIEROLE_HAS_MOVIE_ALIAS : AliasResult
    {
        private MOVIEROLE_HAS_MOVIE_REL Parent;

        internal MOVIEROLE_HAS_MOVIE_ALIAS(MOVIEROLE_HAS_MOVIE_REL parent)
        {
            Parent = parent;

            CreationDate = new DateTimeResult(this, "CreationDate", MovieGraph.Model.Datastore.Model.Relations["MOVIEROLE_HAS_MOVIE"], MovieGraph.Model.Datastore.Model.Relations["MOVIEROLE_HAS_MOVIE"].Properties["CreationDate"]);
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
