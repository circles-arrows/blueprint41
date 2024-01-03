using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Datastore.Query
{
public partial class PERSON_LIVES_IN_REL : RELATIONSHIP, IFromIn_PERSON_LIVES_IN_REL, IFromOut_PERSON_LIVES_IN_REL    {
        public override string NEO4J_TYPE
        {
            get
            {
                return "LIVES_IN";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
        internal PERSON_LIVES_IN_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

        public PERSON_LIVES_IN_REL Alias(out PERSON_LIVES_IN_ALIAS alias)
        {
            alias = new PERSON_LIVES_IN_ALIAS(this);
            RelationshipAlias = alias;
            return this;
        } 
        public PERSON_LIVES_IN_REL Repeat(int maxHops)
        {
            return Repeat(1, maxHops);
        }
        public new PERSON_LIVES_IN_REL Repeat(int minHops, int maxHops)
        {
            base.Repeat(minHops, maxHops);
            return this;
        }

        IFromIn_PERSON_LIVES_IN_REL IFromIn_PERSON_LIVES_IN_REL.Alias(out PERSON_LIVES_IN_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromOut_PERSON_LIVES_IN_REL IFromOut_PERSON_LIVES_IN_REL.Alias(out PERSON_LIVES_IN_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromIn_PERSON_LIVES_IN_REL IFromIn_PERSON_LIVES_IN_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromIn_PERSON_LIVES_IN_REL IFromIn_PERSON_LIVES_IN_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }
        IFromOut_PERSON_LIVES_IN_REL IFromOut_PERSON_LIVES_IN_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromOut_PERSON_LIVES_IN_REL IFromOut_PERSON_LIVES_IN_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }


        public PERSON_LIVES_IN_IN In { get { return new PERSON_LIVES_IN_IN(this); } }
        public class PERSON_LIVES_IN_IN
        {
            private PERSON_LIVES_IN_REL Parent;
            internal PERSON_LIVES_IN_IN(PERSON_LIVES_IN_REL parent)
            {
                Parent = parent;
            }

            public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
        }

        public PERSON_LIVES_IN_OUT Out { get { return new PERSON_LIVES_IN_OUT(this); } }
        public class PERSON_LIVES_IN_OUT
        {
            private PERSON_LIVES_IN_REL Parent;
            internal PERSON_LIVES_IN_OUT(PERSON_LIVES_IN_REL parent)
            {
                Parent = parent;
            }

            public CityNode City { get { return new CityNode(Parent, DirectionEnum.Out); } }
        }
    }

    public interface IFromIn_PERSON_LIVES_IN_REL
    {
        IFromIn_PERSON_LIVES_IN_REL Alias(out PERSON_LIVES_IN_ALIAS alias);
        IFromIn_PERSON_LIVES_IN_REL Repeat(int maxHops);
        IFromIn_PERSON_LIVES_IN_REL Repeat(int minHops, int maxHops);

        PERSON_LIVES_IN_REL.PERSON_LIVES_IN_OUT Out { get; }
    }
    public interface IFromOut_PERSON_LIVES_IN_REL
    {
        IFromOut_PERSON_LIVES_IN_REL Alias(out PERSON_LIVES_IN_ALIAS alias);
        IFromOut_PERSON_LIVES_IN_REL Repeat(int maxHops);
        IFromOut_PERSON_LIVES_IN_REL Repeat(int minHops, int maxHops);

        PERSON_LIVES_IN_REL.PERSON_LIVES_IN_IN In { get; }
    }

    public class PERSON_LIVES_IN_ALIAS : AliasResult
    {
        private PERSON_LIVES_IN_REL Parent;

        internal PERSON_LIVES_IN_ALIAS(PERSON_LIVES_IN_REL parent)
        {
            Parent = parent;

            CreationDate = new DateTimeResult(this, "CreationDate", Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_LIVES_IN"], Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_LIVES_IN"].Properties["CreationDate"]);
        }

        public Assignment[] Assign(JsNotation<System.DateTime> CreationDate = default)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (CreationDate.HasValue) assignments.Add(new Assignment(this.CreationDate, CreationDate));

            return assignments.ToArray();
        }

        public DateTimeResult CreationDate { get; private set; } 
    }
}
