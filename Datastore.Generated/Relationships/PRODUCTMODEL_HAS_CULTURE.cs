using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class PRODUCTMODEL_HAS_CULTURE_REL : RELATIONSHIP, IFromIn_PRODUCTMODEL_HAS_CULTURE_REL, IFromOut_PRODUCTMODEL_HAS_CULTURE_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "HAS_CULTURE";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal PRODUCTMODEL_HAS_CULTURE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PRODUCTMODEL_HAS_CULTURE_REL Alias(out PRODUCTMODEL_HAS_CULTURE_ALIAS alias)
		{
			alias = new PRODUCTMODEL_HAS_CULTURE_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public PRODUCTMODEL_HAS_CULTURE_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new PRODUCTMODEL_HAS_CULTURE_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_PRODUCTMODEL_HAS_CULTURE_REL IFromIn_PRODUCTMODEL_HAS_CULTURE_REL.Alias(out PRODUCTMODEL_HAS_CULTURE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PRODUCTMODEL_HAS_CULTURE_REL IFromOut_PRODUCTMODEL_HAS_CULTURE_REL.Alias(out PRODUCTMODEL_HAS_CULTURE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PRODUCTMODEL_HAS_CULTURE_REL IFromIn_PRODUCTMODEL_HAS_CULTURE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PRODUCTMODEL_HAS_CULTURE_REL IFromIn_PRODUCTMODEL_HAS_CULTURE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PRODUCTMODEL_HAS_CULTURE_REL IFromOut_PRODUCTMODEL_HAS_CULTURE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PRODUCTMODEL_HAS_CULTURE_REL IFromOut_PRODUCTMODEL_HAS_CULTURE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PRODUCTMODEL_HAS_CULTURE_IN In { get { return new PRODUCTMODEL_HAS_CULTURE_IN(this); } }
		public class PRODUCTMODEL_HAS_CULTURE_IN
		{
			private PRODUCTMODEL_HAS_CULTURE_REL Parent;
			internal PRODUCTMODEL_HAS_CULTURE_IN(PRODUCTMODEL_HAS_CULTURE_REL parent)
			{
				Parent = parent;
			}

			public ProductModelNode ProductModel { get { return new ProductModelNode(Parent, DirectionEnum.In); } }
		}

		public PRODUCTMODEL_HAS_CULTURE_OUT Out { get { return new PRODUCTMODEL_HAS_CULTURE_OUT(this); } }
		public class PRODUCTMODEL_HAS_CULTURE_OUT
		{
			private PRODUCTMODEL_HAS_CULTURE_REL Parent;
			internal PRODUCTMODEL_HAS_CULTURE_OUT(PRODUCTMODEL_HAS_CULTURE_REL parent)
			{
				Parent = parent;
			}

			public CultureNode Culture { get { return new CultureNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_PRODUCTMODEL_HAS_CULTURE_REL
	{
		IFromIn_PRODUCTMODEL_HAS_CULTURE_REL Alias(out PRODUCTMODEL_HAS_CULTURE_ALIAS alias);
		IFromIn_PRODUCTMODEL_HAS_CULTURE_REL Repeat(int maxHops);
		IFromIn_PRODUCTMODEL_HAS_CULTURE_REL Repeat(int minHops, int maxHops);

		PRODUCTMODEL_HAS_CULTURE_REL.PRODUCTMODEL_HAS_CULTURE_OUT Out { get; }
	}
	public interface IFromOut_PRODUCTMODEL_HAS_CULTURE_REL
	{
		IFromOut_PRODUCTMODEL_HAS_CULTURE_REL Alias(out PRODUCTMODEL_HAS_CULTURE_ALIAS alias);
		IFromOut_PRODUCTMODEL_HAS_CULTURE_REL Repeat(int maxHops);
		IFromOut_PRODUCTMODEL_HAS_CULTURE_REL Repeat(int minHops, int maxHops);

		PRODUCTMODEL_HAS_CULTURE_REL.PRODUCTMODEL_HAS_CULTURE_IN In { get; }
	}

	public class PRODUCTMODEL_HAS_CULTURE_ALIAS : AliasResult
	{
		private PRODUCTMODEL_HAS_CULTURE_REL Parent;

		internal PRODUCTMODEL_HAS_CULTURE_ALIAS(PRODUCTMODEL_HAS_CULTURE_REL parent)
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
