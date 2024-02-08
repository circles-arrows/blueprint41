using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Datastore.Query
{
public partial class RESTAURANT_LOCATED_AT_REL : RELATIONSHIP, IFromIn_RESTAURANT_LOCATED_AT_REL, IFromOut_RESTAURANT_LOCATED_AT_REL    {
        public override string NEO4J_TYPE
        {
            get
            {
                return "LOCATED_AT";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
        internal RESTAURANT_LOCATED_AT_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

        public RESTAURANT_LOCATED_AT_REL Alias(out RESTAURANT_LOCATED_AT_ALIAS alias)
        {
            alias = new RESTAURANT_LOCATED_AT_ALIAS(this);
            RelationshipAlias = alias;
            return this;
        } 
        public RESTAURANT_LOCATED_AT_REL Repeat(int maxHops)
        {
            return Repeat(1, maxHops);
        }
        public new RESTAURANT_LOCATED_AT_REL Repeat(int minHops, int maxHops)
        {
            base.Repeat(minHops, maxHops);
            return this;
        }

        IFromIn_RESTAURANT_LOCATED_AT_REL IFromIn_RESTAURANT_LOCATED_AT_REL.Alias(out RESTAURANT_LOCATED_AT_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromOut_RESTAURANT_LOCATED_AT_REL IFromOut_RESTAURANT_LOCATED_AT_REL.Alias(out RESTAURANT_LOCATED_AT_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromIn_RESTAURANT_LOCATED_AT_REL IFromIn_RESTAURANT_LOCATED_AT_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromIn_RESTAURANT_LOCATED_AT_REL IFromIn_RESTAURANT_LOCATED_AT_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }
        IFromOut_RESTAURANT_LOCATED_AT_REL IFromOut_RESTAURANT_LOCATED_AT_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromOut_RESTAURANT_LOCATED_AT_REL IFromOut_RESTAURANT_LOCATED_AT_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }


        public RESTAURANT_LOCATED_AT_IN In { get { return new RESTAURANT_LOCATED_AT_IN(this); } }
        public class RESTAURANT_LOCATED_AT_IN
        {
            private RESTAURANT_LOCATED_AT_REL Parent;
            internal RESTAURANT_LOCATED_AT_IN(RESTAURANT_LOCATED_AT_REL parent)
            {
                Parent = parent;
            }

            public RestaurantNode Restaurant { get { return new RestaurantNode(Parent, DirectionEnum.In); } }
        }

        public RESTAURANT_LOCATED_AT_OUT Out { get { return new RESTAURANT_LOCATED_AT_OUT(this); } }
        public class RESTAURANT_LOCATED_AT_OUT
        {
            private RESTAURANT_LOCATED_AT_REL Parent;
            internal RESTAURANT_LOCATED_AT_OUT(RESTAURANT_LOCATED_AT_REL parent)
            {
                Parent = parent;
            }

            public CityNode City { get { return new CityNode(Parent, DirectionEnum.Out); } }
        }
    }

    public interface IFromIn_RESTAURANT_LOCATED_AT_REL
    {
        IFromIn_RESTAURANT_LOCATED_AT_REL Alias(out RESTAURANT_LOCATED_AT_ALIAS alias);
        IFromIn_RESTAURANT_LOCATED_AT_REL Repeat(int maxHops);
        IFromIn_RESTAURANT_LOCATED_AT_REL Repeat(int minHops, int maxHops);

        RESTAURANT_LOCATED_AT_REL.RESTAURANT_LOCATED_AT_OUT Out { get; }
    }
    public interface IFromOut_RESTAURANT_LOCATED_AT_REL
    {
        IFromOut_RESTAURANT_LOCATED_AT_REL Alias(out RESTAURANT_LOCATED_AT_ALIAS alias);
        IFromOut_RESTAURANT_LOCATED_AT_REL Repeat(int maxHops);
        IFromOut_RESTAURANT_LOCATED_AT_REL Repeat(int minHops, int maxHops);

        RESTAURANT_LOCATED_AT_REL.RESTAURANT_LOCATED_AT_IN In { get; }
    }

    public class RESTAURANT_LOCATED_AT_ALIAS : AliasResult
    {
        private RESTAURANT_LOCATED_AT_REL Parent;

        internal RESTAURANT_LOCATED_AT_ALIAS(RESTAURANT_LOCATED_AT_REL parent)
        {
            Parent = parent;

            CreationDate = new DateTimeResult(this, "CreationDate", Blueprint41.UnitTest.Memgraph.DataStore.MockModel.Model.Relations["RESTAURANT_LOCATED_AT"], Blueprint41.UnitTest.Memgraph.DataStore.MockModel.Model.Relations["RESTAURANT_LOCATED_AT"].Properties["CreationDate"]);
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
