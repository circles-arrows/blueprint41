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
            StartDate = new DateTimeResult(this, "StartDate", Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_LIVES_IN"], Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_LIVES_IN"].Properties["StartDate"]);
            EndDate = new DateTimeResult(this, "EndDate", Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_LIVES_IN"], Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_LIVES_IN"].Properties["EndDate"]);
            AddressLine1 = new StringResult(this, "AddressLine1", Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_LIVES_IN"], Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_LIVES_IN"].Properties["AddressLine1"]);
            AddressLine2 = new StringResult(this, "AddressLine2", Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_LIVES_IN"], Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_LIVES_IN"].Properties["AddressLine2"]);
            AddressLine3 = new StringResult(this, "AddressLine3", Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_LIVES_IN"], Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["PERSON_LIVES_IN"].Properties["AddressLine3"]);
        }

        public Assignment[] Assign(JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> AddressLine3 = default, JsNotation<System.DateTime> CreationDate = default, JsNotation<System.DateTime> EndDate = default, JsNotation<System.DateTime> StartDate = default)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (CreationDate.HasValue) assignments.Add(new Assignment(this.CreationDate, CreationDate));
            if (StartDate.HasValue) assignments.Add(new Assignment(this.StartDate, StartDate));
            if (EndDate.HasValue) assignments.Add(new Assignment(this.EndDate, EndDate));
            if (AddressLine1.HasValue) assignments.Add(new Assignment(this.AddressLine1, AddressLine1));
            if (AddressLine2.HasValue) assignments.Add(new Assignment(this.AddressLine2, AddressLine2));
            if (AddressLine3.HasValue) assignments.Add(new Assignment(this.AddressLine3, AddressLine3));

            return assignments.ToArray();
        }

        public DateTimeResult CreationDate { get; private set; } 
        public DateTimeResult StartDate { get; private set; } 
        public DateTimeResult EndDate { get; private set; } 
        public StringResult AddressLine1 { get; private set; } 
        public StringResult AddressLine2 { get; private set; } 
        public StringResult AddressLine3 { get; private set; } 
    }
}
