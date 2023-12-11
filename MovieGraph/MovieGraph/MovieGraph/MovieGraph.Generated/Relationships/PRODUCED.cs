using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class PRODUCED_REL : RELATIONSHIP, IFromIn_PRODUCED_REL, IFromOut_PRODUCED_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "PRODUCED";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal PRODUCED_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PRODUCED_REL Alias(out PRODUCED_ALIAS alias)
		{
			alias = new PRODUCED_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public PRODUCED_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new PRODUCED_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_PRODUCED_REL IFromIn_PRODUCED_REL.Alias(out PRODUCED_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PRODUCED_REL IFromOut_PRODUCED_REL.Alias(out PRODUCED_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PRODUCED_REL IFromIn_PRODUCED_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PRODUCED_REL IFromIn_PRODUCED_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PRODUCED_REL IFromOut_PRODUCED_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PRODUCED_REL IFromOut_PRODUCED_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PRODUCED_IN In { get { return new PRODUCED_IN(this); } }
		public class PRODUCED_IN
		{
			private PRODUCED_REL Parent;
			internal PRODUCED_IN(PRODUCED_REL parent)
			{
				Parent = parent;
			}

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
		}

		public PRODUCED_OUT Out { get { return new PRODUCED_OUT(this); } }
		public class PRODUCED_OUT
		{
			private PRODUCED_REL Parent;
			internal PRODUCED_OUT(PRODUCED_REL parent)
			{
				Parent = parent;
			}

			public MovieNode Movie { get { return new MovieNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_PRODUCED_REL
	{
		IFromIn_PRODUCED_REL Alias(out PRODUCED_ALIAS alias);
		IFromIn_PRODUCED_REL Repeat(int maxHops);
		IFromIn_PRODUCED_REL Repeat(int minHops, int maxHops);

		PRODUCED_REL.PRODUCED_OUT Out { get; }
	}
	public interface IFromOut_PRODUCED_REL
	{
		IFromOut_PRODUCED_REL Alias(out PRODUCED_ALIAS alias);
		IFromOut_PRODUCED_REL Repeat(int maxHops);
		IFromOut_PRODUCED_REL Repeat(int minHops, int maxHops);

		PRODUCED_REL.PRODUCED_IN In { get; }
	}

	public class PRODUCED_ALIAS : AliasResult
	{
		private PRODUCED_REL Parent;

		internal PRODUCED_ALIAS(PRODUCED_REL parent)
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
