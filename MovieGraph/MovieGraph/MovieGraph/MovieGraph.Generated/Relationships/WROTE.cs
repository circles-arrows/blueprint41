using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class WROTE_REL : RELATIONSHIP, IFromIn_WROTE_REL, IFromOut_WROTE_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "WROTE";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal WROTE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public WROTE_REL Alias(out WROTE_ALIAS alias)
		{
			alias = new WROTE_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public WROTE_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new WROTE_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_WROTE_REL IFromIn_WROTE_REL.Alias(out WROTE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_WROTE_REL IFromOut_WROTE_REL.Alias(out WROTE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_WROTE_REL IFromIn_WROTE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_WROTE_REL IFromIn_WROTE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_WROTE_REL IFromOut_WROTE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_WROTE_REL IFromOut_WROTE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public WROTE_IN In { get { return new WROTE_IN(this); } }
		public class WROTE_IN
		{
			private WROTE_REL Parent;
			internal WROTE_IN(WROTE_REL parent)
			{
				Parent = parent;
			}

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
		}

		public WROTE_OUT Out { get { return new WROTE_OUT(this); } }
		public class WROTE_OUT
		{
			private WROTE_REL Parent;
			internal WROTE_OUT(WROTE_REL parent)
			{
				Parent = parent;
			}

			public MovieNode Movie { get { return new MovieNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_WROTE_REL
	{
		IFromIn_WROTE_REL Alias(out WROTE_ALIAS alias);
		IFromIn_WROTE_REL Repeat(int maxHops);
		IFromIn_WROTE_REL Repeat(int minHops, int maxHops);

		WROTE_REL.WROTE_OUT Out { get; }
	}
	public interface IFromOut_WROTE_REL
	{
		IFromOut_WROTE_REL Alias(out WROTE_ALIAS alias);
		IFromOut_WROTE_REL Repeat(int maxHops);
		IFromOut_WROTE_REL Repeat(int minHops, int maxHops);

		WROTE_REL.WROTE_IN In { get; }
	}

	public class WROTE_ALIAS : AliasResult
	{
		private WROTE_REL Parent;

		internal WROTE_ALIAS(WROTE_REL parent)
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
