using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class MOVIEREVIEW_HAS_MOVIE_REL : RELATIONSHIP, IFromIn_MOVIEREVIEW_HAS_MOVIE_REL, IFromOut_MOVIEREVIEW_HAS_MOVIE_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "HAS_REVIEWED_MOVIE";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal MOVIEREVIEW_HAS_MOVIE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public MOVIEREVIEW_HAS_MOVIE_REL Alias(out MOVIEREVIEW_HAS_MOVIE_ALIAS alias)
		{
			alias = new MOVIEREVIEW_HAS_MOVIE_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public MOVIEREVIEW_HAS_MOVIE_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new MOVIEREVIEW_HAS_MOVIE_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_MOVIEREVIEW_HAS_MOVIE_REL IFromIn_MOVIEREVIEW_HAS_MOVIE_REL.Alias(out MOVIEREVIEW_HAS_MOVIE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_MOVIEREVIEW_HAS_MOVIE_REL IFromOut_MOVIEREVIEW_HAS_MOVIE_REL.Alias(out MOVIEREVIEW_HAS_MOVIE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_MOVIEREVIEW_HAS_MOVIE_REL IFromIn_MOVIEREVIEW_HAS_MOVIE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_MOVIEREVIEW_HAS_MOVIE_REL IFromIn_MOVIEREVIEW_HAS_MOVIE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_MOVIEREVIEW_HAS_MOVIE_REL IFromOut_MOVIEREVIEW_HAS_MOVIE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_MOVIEREVIEW_HAS_MOVIE_REL IFromOut_MOVIEREVIEW_HAS_MOVIE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public MOVIEREVIEW_HAS_MOVIE_IN In { get { return new MOVIEREVIEW_HAS_MOVIE_IN(this); } }
		public class MOVIEREVIEW_HAS_MOVIE_IN
		{
			private MOVIEREVIEW_HAS_MOVIE_REL Parent;
			internal MOVIEREVIEW_HAS_MOVIE_IN(MOVIEREVIEW_HAS_MOVIE_REL parent)
			{
				Parent = parent;
			}

			public MovieReviewNode MovieReview { get { return new MovieReviewNode(Parent, DirectionEnum.In); } }
		}

		public MOVIEREVIEW_HAS_MOVIE_OUT Out { get { return new MOVIEREVIEW_HAS_MOVIE_OUT(this); } }
		public class MOVIEREVIEW_HAS_MOVIE_OUT
		{
			private MOVIEREVIEW_HAS_MOVIE_REL Parent;
			internal MOVIEREVIEW_HAS_MOVIE_OUT(MOVIEREVIEW_HAS_MOVIE_REL parent)
			{
				Parent = parent;
			}

			public MovieNode Movie { get { return new MovieNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_MOVIEREVIEW_HAS_MOVIE_REL
	{
		IFromIn_MOVIEREVIEW_HAS_MOVIE_REL Alias(out MOVIEREVIEW_HAS_MOVIE_ALIAS alias);
		IFromIn_MOVIEREVIEW_HAS_MOVIE_REL Repeat(int maxHops);
		IFromIn_MOVIEREVIEW_HAS_MOVIE_REL Repeat(int minHops, int maxHops);

		MOVIEREVIEW_HAS_MOVIE_REL.MOVIEREVIEW_HAS_MOVIE_OUT Out { get; }
	}
	public interface IFromOut_MOVIEREVIEW_HAS_MOVIE_REL
	{
		IFromOut_MOVIEREVIEW_HAS_MOVIE_REL Alias(out MOVIEREVIEW_HAS_MOVIE_ALIAS alias);
		IFromOut_MOVIEREVIEW_HAS_MOVIE_REL Repeat(int maxHops);
		IFromOut_MOVIEREVIEW_HAS_MOVIE_REL Repeat(int minHops, int maxHops);

		MOVIEREVIEW_HAS_MOVIE_REL.MOVIEREVIEW_HAS_MOVIE_IN In { get; }
	}

	public class MOVIEREVIEW_HAS_MOVIE_ALIAS : AliasResult
	{
		private MOVIEREVIEW_HAS_MOVIE_REL Parent;

		internal MOVIEREVIEW_HAS_MOVIE_ALIAS(MOVIEREVIEW_HAS_MOVIE_REL parent)
		{
			Parent = parent;

			CreationDate = new RelationFieldResult(this, "CreationDate");
		}

        public Assignment[] Assign(JsNotation<DateTime> CreationDate = default)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (CreationDate.HasValue) assignments.Add(new Assignment(this.CreationDate, CreationDate));

            return assignments.ToArray();
        }

		public RelationFieldResult CreationDate { get; private set; } 
	}
}
