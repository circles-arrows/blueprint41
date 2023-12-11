using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class MOVIE_REVIEWS_REL : RELATIONSHIP, IFromIn_MOVIE_REVIEWS_REL, IFromOut_MOVIE_REVIEWS_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "MOVIE_REVIEWS";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal MOVIE_REVIEWS_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public MOVIE_REVIEWS_REL Alias(out MOVIE_REVIEWS_ALIAS alias)
		{
			alias = new MOVIE_REVIEWS_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public MOVIE_REVIEWS_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new MOVIE_REVIEWS_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_MOVIE_REVIEWS_REL IFromIn_MOVIE_REVIEWS_REL.Alias(out MOVIE_REVIEWS_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_MOVIE_REVIEWS_REL IFromOut_MOVIE_REVIEWS_REL.Alias(out MOVIE_REVIEWS_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_MOVIE_REVIEWS_REL IFromIn_MOVIE_REVIEWS_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_MOVIE_REVIEWS_REL IFromIn_MOVIE_REVIEWS_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_MOVIE_REVIEWS_REL IFromOut_MOVIE_REVIEWS_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_MOVIE_REVIEWS_REL IFromOut_MOVIE_REVIEWS_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public MOVIE_REVIEWS_IN In { get { return new MOVIE_REVIEWS_IN(this); } }
		public class MOVIE_REVIEWS_IN
		{
			private MOVIE_REVIEWS_REL Parent;
			internal MOVIE_REVIEWS_IN(MOVIE_REVIEWS_REL parent)
			{
				Parent = parent;
			}

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
		}

		public MOVIE_REVIEWS_OUT Out { get { return new MOVIE_REVIEWS_OUT(this); } }
		public class MOVIE_REVIEWS_OUT
		{
			private MOVIE_REVIEWS_REL Parent;
			internal MOVIE_REVIEWS_OUT(MOVIE_REVIEWS_REL parent)
			{
				Parent = parent;
			}

			public MovieReviewNode MovieReview { get { return new MovieReviewNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_MOVIE_REVIEWS_REL
	{
		IFromIn_MOVIE_REVIEWS_REL Alias(out MOVIE_REVIEWS_ALIAS alias);
		IFromIn_MOVIE_REVIEWS_REL Repeat(int maxHops);
		IFromIn_MOVIE_REVIEWS_REL Repeat(int minHops, int maxHops);

		MOVIE_REVIEWS_REL.MOVIE_REVIEWS_OUT Out { get; }
	}
	public interface IFromOut_MOVIE_REVIEWS_REL
	{
		IFromOut_MOVIE_REVIEWS_REL Alias(out MOVIE_REVIEWS_ALIAS alias);
		IFromOut_MOVIE_REVIEWS_REL Repeat(int maxHops);
		IFromOut_MOVIE_REVIEWS_REL Repeat(int minHops, int maxHops);

		MOVIE_REVIEWS_REL.MOVIE_REVIEWS_IN In { get; }
	}

	public class MOVIE_REVIEWS_ALIAS : AliasResult
	{
		private MOVIE_REVIEWS_REL Parent;

		internal MOVIE_REVIEWS_ALIAS(MOVIE_REVIEWS_REL parent)
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
