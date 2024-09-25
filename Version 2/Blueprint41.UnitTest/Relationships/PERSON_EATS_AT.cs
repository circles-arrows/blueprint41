#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable CS8981 // Names should not be lower type only

using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Datastore.Query
{
public partial class PERSON_EATS_AT_REL : RELATIONSHIP, IFromIn_PERSON_EATS_AT_REL, IFromOut_PERSON_EATS_AT_REL    {
        public override string NEO4J_TYPE
        {
            get
            {
                return "EATS_AT";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
        internal PERSON_EATS_AT_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

        public PERSON_EATS_AT_REL Alias(out PERSON_EATS_AT_ALIAS alias)
        {
            alias = new PERSON_EATS_AT_ALIAS(this);
            RelationshipAlias = alias;
            return this;
        } 
        public PERSON_EATS_AT_REL Repeat(int maxHops)
        {
            return Repeat(1, maxHops);
        }
        public new PERSON_EATS_AT_REL Repeat(int minHops, int maxHops)
        {
            base.Repeat(minHops, maxHops);
            return this;
        }

        IFromIn_PERSON_EATS_AT_REL IFromIn_PERSON_EATS_AT_REL.Alias(out PERSON_EATS_AT_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromOut_PERSON_EATS_AT_REL IFromOut_PERSON_EATS_AT_REL.Alias(out PERSON_EATS_AT_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromIn_PERSON_EATS_AT_REL IFromIn_PERSON_EATS_AT_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromIn_PERSON_EATS_AT_REL IFromIn_PERSON_EATS_AT_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }
        IFromOut_PERSON_EATS_AT_REL IFromOut_PERSON_EATS_AT_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromOut_PERSON_EATS_AT_REL IFromOut_PERSON_EATS_AT_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }


        public PERSON_EATS_AT_IN In { get { return new PERSON_EATS_AT_IN(this); } }
        public class PERSON_EATS_AT_IN
        {
            private PERSON_EATS_AT_REL Parent;
            internal PERSON_EATS_AT_IN(PERSON_EATS_AT_REL parent)
            {
                Parent = parent;
            }

            public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
        }

        public PERSON_EATS_AT_OUT Out { get { return new PERSON_EATS_AT_OUT(this); } }
        public class PERSON_EATS_AT_OUT
        {
            private PERSON_EATS_AT_REL Parent;
            internal PERSON_EATS_AT_OUT(PERSON_EATS_AT_REL parent)
            {
                Parent = parent;
            }

            public RestaurantNode Restaurant { get { return new RestaurantNode(Parent, DirectionEnum.Out); } }
        }
    }

    public interface IFromIn_PERSON_EATS_AT_REL
    {
        IFromIn_PERSON_EATS_AT_REL Alias(out PERSON_EATS_AT_ALIAS alias);
        IFromIn_PERSON_EATS_AT_REL Repeat(int maxHops);
        IFromIn_PERSON_EATS_AT_REL Repeat(int minHops, int maxHops);

        PERSON_EATS_AT_REL.PERSON_EATS_AT_OUT Out { get; }
    }
    public interface IFromOut_PERSON_EATS_AT_REL
    {
        IFromOut_PERSON_EATS_AT_REL Alias(out PERSON_EATS_AT_ALIAS alias);
        IFromOut_PERSON_EATS_AT_REL Repeat(int maxHops);
        IFromOut_PERSON_EATS_AT_REL Repeat(int minHops, int maxHops);

        PERSON_EATS_AT_REL.PERSON_EATS_AT_IN In { get; }
    }

    public class PERSON_EATS_AT_ALIAS : AliasResult
    {
        private PERSON_EATS_AT_REL Parent;

        internal PERSON_EATS_AT_ALIAS(PERSON_EATS_AT_REL parent)
        {
            Parent = parent;

            CreationDate = new DateTimeResult(this, "CreationDate", Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_EATS_AT"], Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_EATS_AT"].Properties["CreationDate"]);
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
