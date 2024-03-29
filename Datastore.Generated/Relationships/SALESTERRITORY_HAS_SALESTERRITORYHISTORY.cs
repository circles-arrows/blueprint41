using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL : RELATIONSHIP, IFromIn_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL, IFromOut_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "HAS_SALESTERRITORYHISTORY";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL Alias(out SALESTERRITORY_HAS_SALESTERRITORYHISTORY_ALIAS alias)
		{
			alias = new SALESTERRITORY_HAS_SALESTERRITORYHISTORY_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL IFromIn_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL.Alias(out SALESTERRITORY_HAS_SALESTERRITORYHISTORY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL IFromOut_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL.Alias(out SALESTERRITORY_HAS_SALESTERRITORYHISTORY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL IFromIn_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL IFromIn_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL IFromOut_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL IFromOut_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public SALESTERRITORY_HAS_SALESTERRITORYHISTORY_IN In { get { return new SALESTERRITORY_HAS_SALESTERRITORYHISTORY_IN(this); } }
		public class SALESTERRITORY_HAS_SALESTERRITORYHISTORY_IN
		{
			private SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL Parent;
			internal SALESTERRITORY_HAS_SALESTERRITORYHISTORY_IN(SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL parent)
			{
				Parent = parent;
			}

			public SalesTerritoryNode SalesTerritory { get { return new SalesTerritoryNode(Parent, DirectionEnum.In); } }
		}

		public SALESTERRITORY_HAS_SALESTERRITORYHISTORY_OUT Out { get { return new SALESTERRITORY_HAS_SALESTERRITORYHISTORY_OUT(this); } }
		public class SALESTERRITORY_HAS_SALESTERRITORYHISTORY_OUT
		{
			private SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL Parent;
			internal SALESTERRITORY_HAS_SALESTERRITORYHISTORY_OUT(SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL parent)
			{
				Parent = parent;
			}

			public SalesTerritoryHistoryNode SalesTerritoryHistory { get { return new SalesTerritoryHistoryNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL
	{
		IFromIn_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL Alias(out SALESTERRITORY_HAS_SALESTERRITORYHISTORY_ALIAS alias);
		IFromIn_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL Repeat(int maxHops);
		IFromIn_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL Repeat(int minHops, int maxHops);

		SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL.SALESTERRITORY_HAS_SALESTERRITORYHISTORY_OUT Out { get; }
	}
	public interface IFromOut_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL
	{
		IFromOut_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL Alias(out SALESTERRITORY_HAS_SALESTERRITORYHISTORY_ALIAS alias);
		IFromOut_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL Repeat(int maxHops);
		IFromOut_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL Repeat(int minHops, int maxHops);

		SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL.SALESTERRITORY_HAS_SALESTERRITORYHISTORY_IN In { get; }
	}

	public class SALESTERRITORY_HAS_SALESTERRITORYHISTORY_ALIAS : AliasResult
	{
		private SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL Parent;

		internal SALESTERRITORY_HAS_SALESTERRITORYHISTORY_ALIAS(SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL parent)
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
