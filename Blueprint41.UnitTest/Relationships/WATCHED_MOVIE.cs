using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Datastore.Query
{
public partial class WATCHED_MOVIE_REL : RELATIONSHIP, IFromIn_WATCHED_MOVIE_REL, IFromOut_WATCHED_MOVIE_REL    {
        public override string NEO4J_TYPE
        {
            get
            {
                return "WATCHED";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
        internal WATCHED_MOVIE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

        public WATCHED_MOVIE_REL Alias(out WATCHED_MOVIE_ALIAS alias)
        {
            alias = new WATCHED_MOVIE_ALIAS(this);
            RelationshipAlias = alias;
            return this;
        } 
        public WATCHED_MOVIE_REL Repeat(int maxHops)
        {
            return Repeat(1, maxHops);
        }
        public new WATCHED_MOVIE_REL Repeat(int minHops, int maxHops)
        {
            base.Repeat(minHops, maxHops);
            return this;
        }

        IFromIn_WATCHED_MOVIE_REL IFromIn_WATCHED_MOVIE_REL.Alias(out WATCHED_MOVIE_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromOut_WATCHED_MOVIE_REL IFromOut_WATCHED_MOVIE_REL.Alias(out WATCHED_MOVIE_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromIn_WATCHED_MOVIE_REL IFromIn_WATCHED_MOVIE_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromIn_WATCHED_MOVIE_REL IFromIn_WATCHED_MOVIE_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }
        IFromOut_WATCHED_MOVIE_REL IFromOut_WATCHED_MOVIE_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromOut_WATCHED_MOVIE_REL IFromOut_WATCHED_MOVIE_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }


        public WATCHED_MOVIE_IN In { get { return new WATCHED_MOVIE_IN(this); } }
        public class WATCHED_MOVIE_IN
        {
            private WATCHED_MOVIE_REL Parent;
            internal WATCHED_MOVIE_IN(WATCHED_MOVIE_REL parent)
            {
                Parent = parent;
            }

            public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
        }

        public WATCHED_MOVIE_OUT Out { get { return new WATCHED_MOVIE_OUT(this); } }
        public class WATCHED_MOVIE_OUT
        {
            private WATCHED_MOVIE_REL Parent;
            internal WATCHED_MOVIE_OUT(WATCHED_MOVIE_REL parent)
            {
                Parent = parent;
            }

            public MovieNode Movie { get { return new MovieNode(Parent, DirectionEnum.Out); } }
        }
    }

    public interface IFromIn_WATCHED_MOVIE_REL
    {
        IFromIn_WATCHED_MOVIE_REL Alias(out WATCHED_MOVIE_ALIAS alias);
        IFromIn_WATCHED_MOVIE_REL Repeat(int maxHops);
        IFromIn_WATCHED_MOVIE_REL Repeat(int minHops, int maxHops);

        WATCHED_MOVIE_REL.WATCHED_MOVIE_OUT Out { get; }
    }
    public interface IFromOut_WATCHED_MOVIE_REL
    {
        IFromOut_WATCHED_MOVIE_REL Alias(out WATCHED_MOVIE_ALIAS alias);
        IFromOut_WATCHED_MOVIE_REL Repeat(int maxHops);
        IFromOut_WATCHED_MOVIE_REL Repeat(int minHops, int maxHops);

        WATCHED_MOVIE_REL.WATCHED_MOVIE_IN In { get; }
    }

    public class WATCHED_MOVIE_ALIAS : AliasResult
    {
        private WATCHED_MOVIE_REL Parent;

        internal WATCHED_MOVIE_ALIAS(WATCHED_MOVIE_REL parent)
        {
            Parent = parent;

            CreationDate = new DateTimeResult(this, "CreationDate", Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["WATCHED_MOVIE"], Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["WATCHED_MOVIE"].Properties["CreationDate"]);
            MinutesWatched = new NumericResult(this, "MinutesWatched", Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["WATCHED_MOVIE"], Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["WATCHED_MOVIE"].Properties["MinutesWatched"]);
        }

        public Assignment[] Assign(JsNotation<System.DateTime?> CreationDate = default, JsNotation<int> MinutesWatched = default)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (CreationDate.HasValue) assignments.Add(new Assignment(this.CreationDate, CreationDate));
            if (MinutesWatched.HasValue) assignments.Add(new Assignment(this.MinutesWatched, MinutesWatched));

            return assignments.ToArray();
        }

        public DateTimeResult CreationDate { get; private set; } 
        public NumericResult MinutesWatched { get; private set; } 
    }
}
