using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL : RELATIONSHIP, IFromIn_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL, IFromOut_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "CONTAINS_SALESTERRITORY";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL Alias(out SALESORDERHEADER_CONTAINS_SALESTERRITORY_ALIAS alias)
		{
			alias = new SALESORDERHEADER_CONTAINS_SALESTERRITORY_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL IFromIn_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL.Alias(out SALESORDERHEADER_CONTAINS_SALESTERRITORY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL IFromOut_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL.Alias(out SALESORDERHEADER_CONTAINS_SALESTERRITORY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL IFromIn_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL IFromIn_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL IFromOut_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL IFromOut_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public SALESORDERHEADER_CONTAINS_SALESTERRITORY_IN In { get { return new SALESORDERHEADER_CONTAINS_SALESTERRITORY_IN(this); } }
		public class SALESORDERHEADER_CONTAINS_SALESTERRITORY_IN
		{
			private SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL Parent;
			internal SALESORDERHEADER_CONTAINS_SALESTERRITORY_IN(SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL parent)
			{
				Parent = parent;
			}

			public SalesOrderHeaderNode SalesOrderHeader { get { return new SalesOrderHeaderNode(Parent, DirectionEnum.In); } }
		}

		public SALESORDERHEADER_CONTAINS_SALESTERRITORY_OUT Out { get { return new SALESORDERHEADER_CONTAINS_SALESTERRITORY_OUT(this); } }
		public class SALESORDERHEADER_CONTAINS_SALESTERRITORY_OUT
		{
			private SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL Parent;
			internal SALESORDERHEADER_CONTAINS_SALESTERRITORY_OUT(SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL parent)
			{
				Parent = parent;
			}

			public SalesTerritoryNode SalesTerritory { get { return new SalesTerritoryNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL
	{
		IFromIn_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL Alias(out SALESORDERHEADER_CONTAINS_SALESTERRITORY_ALIAS alias);
		IFromIn_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL Repeat(int maxHops);
		IFromIn_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL Repeat(int minHops, int maxHops);

		SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL.SALESORDERHEADER_CONTAINS_SALESTERRITORY_OUT Out { get; }
	}
	public interface IFromOut_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL
	{
		IFromOut_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL Alias(out SALESORDERHEADER_CONTAINS_SALESTERRITORY_ALIAS alias);
		IFromOut_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL Repeat(int maxHops);
		IFromOut_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL Repeat(int minHops, int maxHops);

		SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL.SALESORDERHEADER_CONTAINS_SALESTERRITORY_IN In { get; }
	}

	public class SALESORDERHEADER_CONTAINS_SALESTERRITORY_ALIAS : AliasResult
	{
		private SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL Parent;

		internal SALESORDERHEADER_CONTAINS_SALESTERRITORY_ALIAS(SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL parent)
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
