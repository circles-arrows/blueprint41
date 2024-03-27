using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Neo4j.Datastore.Query
{
public partial class MOVIE_CERTIFICATION_REL : RELATIONSHIP, IFromIn_MOVIE_CERTIFICATION_REL, IFromOut_MOVIE_CERTIFICATION_REL    {
        public override string NEO4J_TYPE
        {
            get
            {
                return "CERTIFICATION";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
        internal MOVIE_CERTIFICATION_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

        public MOVIE_CERTIFICATION_REL Alias(out MOVIE_CERTIFICATION_ALIAS alias)
        {
            alias = new MOVIE_CERTIFICATION_ALIAS(this);
            RelationshipAlias = alias;
            return this;
        } 
        public MOVIE_CERTIFICATION_REL Repeat(int maxHops)
        {
            return Repeat(1, maxHops);
        }
        public new MOVIE_CERTIFICATION_REL Repeat(int minHops, int maxHops)
        {
            base.Repeat(minHops, maxHops);
            return this;
        }

        IFromIn_MOVIE_CERTIFICATION_REL IFromIn_MOVIE_CERTIFICATION_REL.Alias(out MOVIE_CERTIFICATION_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromOut_MOVIE_CERTIFICATION_REL IFromOut_MOVIE_CERTIFICATION_REL.Alias(out MOVIE_CERTIFICATION_ALIAS alias)
        {
            return Alias(out alias);
        }
        IFromIn_MOVIE_CERTIFICATION_REL IFromIn_MOVIE_CERTIFICATION_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromIn_MOVIE_CERTIFICATION_REL IFromIn_MOVIE_CERTIFICATION_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }
        IFromOut_MOVIE_CERTIFICATION_REL IFromOut_MOVIE_CERTIFICATION_REL.Repeat(int maxHops)
        {
            return Repeat(maxHops);
        }
        IFromOut_MOVIE_CERTIFICATION_REL IFromOut_MOVIE_CERTIFICATION_REL.Repeat(int minHops, int maxHops)
        {
            return Repeat(minHops, maxHops);
        }


        public MOVIE_CERTIFICATION_IN In { get { return new MOVIE_CERTIFICATION_IN(this); } }
        public class MOVIE_CERTIFICATION_IN
        {
            private MOVIE_CERTIFICATION_REL Parent;
            internal MOVIE_CERTIFICATION_IN(MOVIE_CERTIFICATION_REL parent)
            {
                Parent = parent;
            }

            public MovieNode Movie { get { return new MovieNode(Parent, DirectionEnum.In); } }
        }

        public MOVIE_CERTIFICATION_OUT Out { get { return new MOVIE_CERTIFICATION_OUT(this); } }
        public class MOVIE_CERTIFICATION_OUT
        {
            private MOVIE_CERTIFICATION_REL Parent;
            internal MOVIE_CERTIFICATION_OUT(MOVIE_CERTIFICATION_REL parent)
            {
                Parent = parent;
            }

            public RatingNode Rating { get { return new RatingNode(Parent, DirectionEnum.Out); } }
        }
    }

    public interface IFromIn_MOVIE_CERTIFICATION_REL
    {
        IFromIn_MOVIE_CERTIFICATION_REL Alias(out MOVIE_CERTIFICATION_ALIAS alias);
        IFromIn_MOVIE_CERTIFICATION_REL Repeat(int maxHops);
        IFromIn_MOVIE_CERTIFICATION_REL Repeat(int minHops, int maxHops);

        MOVIE_CERTIFICATION_REL.MOVIE_CERTIFICATION_OUT Out { get; }
    }
    public interface IFromOut_MOVIE_CERTIFICATION_REL
    {
        IFromOut_MOVIE_CERTIFICATION_REL Alias(out MOVIE_CERTIFICATION_ALIAS alias);
        IFromOut_MOVIE_CERTIFICATION_REL Repeat(int maxHops);
        IFromOut_MOVIE_CERTIFICATION_REL Repeat(int minHops, int maxHops);

        MOVIE_CERTIFICATION_REL.MOVIE_CERTIFICATION_IN In { get; }
    }

    public class MOVIE_CERTIFICATION_ALIAS : AliasResult
    {
        private MOVIE_CERTIFICATION_REL Parent;

        internal MOVIE_CERTIFICATION_ALIAS(MOVIE_CERTIFICATION_REL parent)
        {
            Parent = parent;

            CreationDate = new DateTimeResult(this, "CreationDate", Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Relations["MOVIE_CERTIFICATION"], Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Relations["MOVIE_CERTIFICATION"].Properties["CreationDate"]);
            FrighteningIntense = new StringResult(this, "FrighteningIntense", Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Relations["MOVIE_CERTIFICATION"], Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Relations["MOVIE_CERTIFICATION"].Properties["FrighteningIntense"]);
            ViolenceGore = new StringResult(this, "ViolenceGore", Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Relations["MOVIE_CERTIFICATION"], Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Relations["MOVIE_CERTIFICATION"].Properties["ViolenceGore"]);
            Profanity = new StringResult(this, "Profanity", Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Relations["MOVIE_CERTIFICATION"], Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Relations["MOVIE_CERTIFICATION"].Properties["Profanity"]);
            Substances = new StringResult(this, "Substances", Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Relations["MOVIE_CERTIFICATION"], Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Relations["MOVIE_CERTIFICATION"].Properties["Substances"]);
            SexAndNudity = new StringResult(this, "SexAndNudity", Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Relations["MOVIE_CERTIFICATION"], Blueprint41.UnitTest.Multiple.DataStore.MockModel.Model.Relations["MOVIE_CERTIFICATION"].Properties["SexAndNudity"]);
        }

        public Assignment[] Assign(JsNotation<System.DateTime?> CreationDate = default, JsNotation<Blueprint41.UnitTest.Multiple.DataStore.RatingComponent?> FrighteningIntense = default, JsNotation<Blueprint41.UnitTest.Multiple.DataStore.RatingComponent?> Profanity = default, JsNotation<Blueprint41.UnitTest.Multiple.DataStore.RatingComponent?> SexAndNudity = default, JsNotation<Blueprint41.UnitTest.Multiple.DataStore.RatingComponent?> Substances = default, JsNotation<Blueprint41.UnitTest.Multiple.DataStore.RatingComponent?> ViolenceGore = default)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (CreationDate.HasValue) assignments.Add(new Assignment(this.CreationDate, CreationDate));
            if (FrighteningIntense.HasValue) assignments.Add(new Assignment(this.FrighteningIntense, FrighteningIntense));
            if (ViolenceGore.HasValue) assignments.Add(new Assignment(this.ViolenceGore, ViolenceGore));
            if (Profanity.HasValue) assignments.Add(new Assignment(this.Profanity, Profanity));
            if (Substances.HasValue) assignments.Add(new Assignment(this.Substances, Substances));
            if (SexAndNudity.HasValue) assignments.Add(new Assignment(this.SexAndNudity, SexAndNudity));

            return assignments.ToArray();
        }

        public DateTimeResult CreationDate { get; private set; } 
        public StringResult FrighteningIntense { get; private set; } 
        public StringResult ViolenceGore { get; private set; } 
        public StringResult Profanity { get; private set; } 
        public StringResult Substances { get; private set; } 
        public StringResult SexAndNudity { get; private set; } 
    }
}
