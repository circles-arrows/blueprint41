using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Memgraph.Datastore.Query
{
public partial class PERSON_DIRECTED_REL : RELATIONSHIP, IFromIn_PERSON_DIRECTED_REL, IFromOut_PERSON_DIRECTED_REL    {
        public override string NEO4J_TYPE
        {
            get
            {
                return "DIRECTED_BY";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
        internal PERSON_DIRECTED_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

        public PERSON_DIRECTED_REL Alias(out PERSON_DIRECTED_ALIAS alias)
        {
            alias = new PERSON_DIRECTED_ALIAS(this);
            RelationshipAlias = alias;
            return this;
        } 
        public PERSON_DIRECTED_REL Repeat(int maxHops)
        {
            return Repeat(1, maxHops);
        }
        public new PERSON_DIRECTED_REL Repeat(int minHops, int maxHops)
        {
            base.Repeat(minHops, maxHops);
            return this;
        }

        IFromIn_PERSON_DIRECTED_REL IFromIn_PERSON_DIRECTED_REL.Alias(out PERSON_DIRECTED_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromOut_PERSON_DIRECTED_REL IFromOut_PERSON_DIRECTED_REL.Alias(out PERSON_DIRECTED_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromIn_PERSON_DIRECTED_REL IFromIn_PERSON_DIRECTED_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromIn_PERSON_DIRECTED_REL IFromIn_PERSON_DIRECTED_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }
        IFromOut_PERSON_DIRECTED_REL IFromOut_PERSON_DIRECTED_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromOut_PERSON_DIRECTED_REL IFromOut_PERSON_DIRECTED_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }


        public PERSON_DIRECTED_IN In { get { return new PERSON_DIRECTED_IN(this); } }
        public class PERSON_DIRECTED_IN
        {
            private PERSON_DIRECTED_REL Parent;
            internal PERSON_DIRECTED_IN(PERSON_DIRECTED_REL parent)
            {
                Parent = parent;
            }

            public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
        }

        public PERSON_DIRECTED_OUT Out { get { return new PERSON_DIRECTED_OUT(this); } }
        public class PERSON_DIRECTED_OUT
        {
            private PERSON_DIRECTED_REL Parent;
            internal PERSON_DIRECTED_OUT(PERSON_DIRECTED_REL parent)
            {
                Parent = parent;
            }

            public MovieNode Movie { get { return new MovieNode(Parent, DirectionEnum.Out); } }
        }
    }

    public interface IFromIn_PERSON_DIRECTED_REL
    {
        IFromIn_PERSON_DIRECTED_REL Alias(out PERSON_DIRECTED_ALIAS alias);
        IFromIn_PERSON_DIRECTED_REL Repeat(int maxHops);
        IFromIn_PERSON_DIRECTED_REL Repeat(int minHops, int maxHops);

        PERSON_DIRECTED_REL.PERSON_DIRECTED_OUT Out { get; }
    }
    public interface IFromOut_PERSON_DIRECTED_REL
    {
        IFromOut_PERSON_DIRECTED_REL Alias(out PERSON_DIRECTED_ALIAS alias);
        IFromOut_PERSON_DIRECTED_REL Repeat(int maxHops);
        IFromOut_PERSON_DIRECTED_REL Repeat(int minHops, int maxHops);

        PERSON_DIRECTED_REL.PERSON_DIRECTED_IN In { get; }
    }

    public class PERSON_DIRECTED_ALIAS : AliasResult
    {
        private PERSON_DIRECTED_REL Parent;

        internal PERSON_DIRECTED_ALIAS(PERSON_DIRECTED_REL parent)
        {
            Parent = parent;

            CreationDate = new DateTimeResult(this, "CreationDate", Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Relations["PERSON_DIRECTED"], Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Relations["PERSON_DIRECTED"].Properties["CreationDate"]);
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
