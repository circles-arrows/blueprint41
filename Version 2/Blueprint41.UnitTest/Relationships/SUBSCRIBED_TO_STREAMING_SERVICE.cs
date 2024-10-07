#nullable disable
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable CS8981 // Names should not be lower type only

using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;
using Blueprint41.Events;

namespace Datastore.Query
{
public partial class SUBSCRIBED_TO_STREAMING_SERVICE_REL : RELATIONSHIP, IFromIn_SUBSCRIBED_TO_STREAMING_SERVICE_REL, IFromOut_SUBSCRIBED_TO_STREAMING_SERVICE_REL    {
        public override string NEO4J_TYPE
        {
            get
            {
                return "SUBSCRIBED_TO";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
        internal SUBSCRIBED_TO_STREAMING_SERVICE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

        public SUBSCRIBED_TO_STREAMING_SERVICE_REL Alias(out SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS alias)
        {
            alias = new SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS(this);
            RelationshipAlias = alias;
            return this;
        } 
        public SUBSCRIBED_TO_STREAMING_SERVICE_REL Repeat(int maxHops)
        {
            return Repeat(1, maxHops);
        }
        public new SUBSCRIBED_TO_STREAMING_SERVICE_REL Repeat(int minHops, int maxHops)
        {
            base.Repeat(minHops, maxHops);
            return this;
        }

        IFromIn_SUBSCRIBED_TO_STREAMING_SERVICE_REL IFromIn_SUBSCRIBED_TO_STREAMING_SERVICE_REL.Alias(out SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromOut_SUBSCRIBED_TO_STREAMING_SERVICE_REL IFromOut_SUBSCRIBED_TO_STREAMING_SERVICE_REL.Alias(out SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromIn_SUBSCRIBED_TO_STREAMING_SERVICE_REL IFromIn_SUBSCRIBED_TO_STREAMING_SERVICE_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromIn_SUBSCRIBED_TO_STREAMING_SERVICE_REL IFromIn_SUBSCRIBED_TO_STREAMING_SERVICE_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }
        IFromOut_SUBSCRIBED_TO_STREAMING_SERVICE_REL IFromOut_SUBSCRIBED_TO_STREAMING_SERVICE_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromOut_SUBSCRIBED_TO_STREAMING_SERVICE_REL IFromOut_SUBSCRIBED_TO_STREAMING_SERVICE_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }


        public SUBSCRIBED_TO_STREAMING_SERVICE_IN In { get { return new SUBSCRIBED_TO_STREAMING_SERVICE_IN(this); } }
        public class SUBSCRIBED_TO_STREAMING_SERVICE_IN
        {
            private SUBSCRIBED_TO_STREAMING_SERVICE_REL Parent;
            internal SUBSCRIBED_TO_STREAMING_SERVICE_IN(SUBSCRIBED_TO_STREAMING_SERVICE_REL parent)
            {
                Parent = parent;
            }

            public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
        }

        public SUBSCRIBED_TO_STREAMING_SERVICE_OUT Out { get { return new SUBSCRIBED_TO_STREAMING_SERVICE_OUT(this); } }
        public class SUBSCRIBED_TO_STREAMING_SERVICE_OUT
        {
            private SUBSCRIBED_TO_STREAMING_SERVICE_REL Parent;
            internal SUBSCRIBED_TO_STREAMING_SERVICE_OUT(SUBSCRIBED_TO_STREAMING_SERVICE_REL parent)
            {
                Parent = parent;
            }

            public StreamingServiceNode StreamingService { get { return new StreamingServiceNode(Parent, DirectionEnum.Out); } }
        }
    }

    public interface IFromIn_SUBSCRIBED_TO_STREAMING_SERVICE_REL
    {
        IFromIn_SUBSCRIBED_TO_STREAMING_SERVICE_REL Alias(out SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS alias);
        IFromIn_SUBSCRIBED_TO_STREAMING_SERVICE_REL Repeat(int maxHops);
        IFromIn_SUBSCRIBED_TO_STREAMING_SERVICE_REL Repeat(int minHops, int maxHops);

        SUBSCRIBED_TO_STREAMING_SERVICE_REL.SUBSCRIBED_TO_STREAMING_SERVICE_OUT Out { get; }
    }
    public interface IFromOut_SUBSCRIBED_TO_STREAMING_SERVICE_REL
    {
        IFromOut_SUBSCRIBED_TO_STREAMING_SERVICE_REL Alias(out SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS alias);
        IFromOut_SUBSCRIBED_TO_STREAMING_SERVICE_REL Repeat(int maxHops);
        IFromOut_SUBSCRIBED_TO_STREAMING_SERVICE_REL Repeat(int minHops, int maxHops);

        SUBSCRIBED_TO_STREAMING_SERVICE_REL.SUBSCRIBED_TO_STREAMING_SERVICE_IN In { get; }
    }

    public class SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS : AliasResult
    {
        private SUBSCRIBED_TO_STREAMING_SERVICE_REL Parent;

        internal SUBSCRIBED_TO_STREAMING_SERVICE_ALIAS(SUBSCRIBED_TO_STREAMING_SERVICE_REL parent)
        {
            Parent = parent;

            CreationDate = new DateTimeResult(this, "CreationDate", Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["SUBSCRIBED_TO_STREAMING_SERVICE"], Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["SUBSCRIBED_TO_STREAMING_SERVICE"].Properties["CreationDate"]);
            MonthlyFee = new NumericResult(this, "MonthlyFee", Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["SUBSCRIBED_TO_STREAMING_SERVICE"], Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["SUBSCRIBED_TO_STREAMING_SERVICE"].Properties["MonthlyFee"]);
            StartDate = new DateTimeResult(this, "StartDate", Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["SUBSCRIBED_TO_STREAMING_SERVICE"], Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["SUBSCRIBED_TO_STREAMING_SERVICE"].Properties["StartDate"]);
            EndDate = new DateTimeResult(this, "EndDate", Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["SUBSCRIBED_TO_STREAMING_SERVICE"], Blueprint41.UnitTest.DataStore.MockModel.Model.Relations["SUBSCRIBED_TO_STREAMING_SERVICE"].Properties["EndDate"]);
        }

        public Assignment[] Assign(JsNotation<System.DateTime?> CreationDate = default, JsNotation<System.DateTime?> EndDate = default, JsNotation<decimal> MonthlyFee = default, JsNotation<System.DateTime?> StartDate = default)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (CreationDate.HasValue) assignments.Add(new Assignment(this.CreationDate, CreationDate));
            if (MonthlyFee.HasValue) assignments.Add(new Assignment(this.MonthlyFee, MonthlyFee));
            if (StartDate.HasValue) assignments.Add(new Assignment(this.StartDate, StartDate));
            if (EndDate.HasValue) assignments.Add(new Assignment(this.EndDate, EndDate));

            return assignments.ToArray();
        }

        public DateTimeResult CreationDate { get; private set; } 
        public NumericResult MonthlyFee { get; private set; } 
        public DateTimeResult StartDate { get; private set; } 
        public DateTimeResult EndDate { get; private set; } 

        public QueryCondition[] Moment(DateTime? moment)
        {
            return new QueryCondition[] { StartDate <= moment, EndDate > moment };
        }
        public QueryCondition[] Moment(DateTimeResult moment)
        {
            return new QueryCondition[] { StartDate <= moment, EndDate > moment };
        }
        public QueryCondition[] Moment(Parameter moment)
        {
            return new QueryCondition[] { StartDate <= moment, EndDate > moment };
        }
    }
}
