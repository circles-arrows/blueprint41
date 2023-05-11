using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Datastore.Query
{
public partial class ACTED_IN_REL : RELATIONSHIP, IFromIn_ACTED_IN_REL, IFromOut_ACTED_IN_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "ACTORS";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal ACTED_IN_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public ACTED_IN_REL Alias(out ACTED_IN_ALIAS alias)
		{
			alias = new ACTED_IN_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public ACTED_IN_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new ACTED_IN_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_ACTED_IN_REL IFromIn_ACTED_IN_REL.Alias(out ACTED_IN_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_ACTED_IN_REL IFromOut_ACTED_IN_REL.Alias(out ACTED_IN_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_ACTED_IN_REL IFromIn_ACTED_IN_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_ACTED_IN_REL IFromIn_ACTED_IN_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_ACTED_IN_REL IFromOut_ACTED_IN_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_ACTED_IN_REL IFromOut_ACTED_IN_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public ACTED_IN_IN In { get { return new ACTED_IN_IN(this); } }
		public class ACTED_IN_IN
		{
			private ACTED_IN_REL Parent;
			internal ACTED_IN_IN(ACTED_IN_REL parent)
			{
				Parent = parent;
			}

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
		}

		public ACTED_IN_OUT Out { get { return new ACTED_IN_OUT(this); } }
		public class ACTED_IN_OUT
		{
			private ACTED_IN_REL Parent;
			internal ACTED_IN_OUT(ACTED_IN_REL parent)
			{
				Parent = parent;
			}

			public MovieNode Movie { get { return new MovieNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_ACTED_IN_REL
	{
		IFromIn_ACTED_IN_REL Alias(out ACTED_IN_ALIAS alias);
		IFromIn_ACTED_IN_REL Repeat(int maxHops);
		IFromIn_ACTED_IN_REL Repeat(int minHops, int maxHops);

		ACTED_IN_REL.ACTED_IN_OUT Out { get; }
	}
	public interface IFromOut_ACTED_IN_REL
	{
		IFromOut_ACTED_IN_REL Alias(out ACTED_IN_ALIAS alias);
		IFromOut_ACTED_IN_REL Repeat(int maxHops);
		IFromOut_ACTED_IN_REL Repeat(int minHops, int maxHops);

		ACTED_IN_REL.ACTED_IN_IN In { get; }
	}

	public class ACTED_IN_ALIAS : AliasResult
	{
		private ACTED_IN_REL Parent;

		internal ACTED_IN_ALIAS(ACTED_IN_REL parent)
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
