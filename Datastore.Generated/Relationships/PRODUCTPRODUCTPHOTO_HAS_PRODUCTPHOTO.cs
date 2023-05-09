using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL : RELATIONSHIP, IFromIn_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL, IFromOut_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "HAS_PRODUCTPHOTO";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL Alias(out PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_ALIAS alias)
		{
			alias = new PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL IFromIn_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL.Alias(out PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL IFromOut_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL.Alias(out PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL IFromIn_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL IFromIn_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL IFromOut_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL IFromOut_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_IN In { get { return new PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_IN(this); } }
		public class PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_IN
		{
			private PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL Parent;
			internal PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_IN(PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL parent)
			{
				Parent = parent;
			}

			public ProductProductPhotoNode ProductProductPhoto { get { return new ProductProductPhotoNode(Parent, DirectionEnum.In); } }
		}

		public PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_OUT Out { get { return new PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_OUT(this); } }
		public class PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_OUT
		{
			private PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL Parent;
			internal PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_OUT(PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL parent)
			{
				Parent = parent;
			}

			public ProductPhotoNode ProductPhoto { get { return new ProductPhotoNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL
	{
		IFromIn_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL Alias(out PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_ALIAS alias);
		IFromIn_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL Repeat(int maxHops);
		IFromIn_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL Repeat(int minHops, int maxHops);

		PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL.PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_OUT Out { get; }
	}
	public interface IFromOut_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL
	{
		IFromOut_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL Alias(out PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_ALIAS alias);
		IFromOut_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL Repeat(int maxHops);
		IFromOut_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL Repeat(int minHops, int maxHops);

		PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL.PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_IN In { get; }
	}

	public class PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_ALIAS : AliasResult
	{
		private PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL Parent;

		internal PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_ALIAS(PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL parent)
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
