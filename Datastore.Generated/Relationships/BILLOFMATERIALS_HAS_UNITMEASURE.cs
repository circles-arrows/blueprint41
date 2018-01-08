
using Blueprint41;
using Blueprint41.Query;


namespace Domain.Data.Query
{
public partial class BILLOFMATERIALS_HAS_UNITMEASURE_REL : RELATIONSHIP, IFromIn_BILLOFMATERIALS_HAS_UNITMEASURE_REL, IFromOut_BILLOFMATERIALS_HAS_UNITMEASURE_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_UNITMEASURE";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal BILLOFMATERIALS_HAS_UNITMEASURE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public BILLOFMATERIALS_HAS_UNITMEASURE_REL Alias(out BILLOFMATERIALS_HAS_UNITMEASURE_ALIAS alias)
		{
			alias = new BILLOFMATERIALS_HAS_UNITMEASURE_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public BILLOFMATERIALS_HAS_UNITMEASURE_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public BILLOFMATERIALS_HAS_UNITMEASURE_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_BILLOFMATERIALS_HAS_UNITMEASURE_REL IFromIn_BILLOFMATERIALS_HAS_UNITMEASURE_REL.Alias(out BILLOFMATERIALS_HAS_UNITMEASURE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_BILLOFMATERIALS_HAS_UNITMEASURE_REL IFromOut_BILLOFMATERIALS_HAS_UNITMEASURE_REL.Alias(out BILLOFMATERIALS_HAS_UNITMEASURE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_BILLOFMATERIALS_HAS_UNITMEASURE_REL IFromIn_BILLOFMATERIALS_HAS_UNITMEASURE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_BILLOFMATERIALS_HAS_UNITMEASURE_REL IFromIn_BILLOFMATERIALS_HAS_UNITMEASURE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_BILLOFMATERIALS_HAS_UNITMEASURE_REL IFromOut_BILLOFMATERIALS_HAS_UNITMEASURE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_BILLOFMATERIALS_HAS_UNITMEASURE_REL IFromOut_BILLOFMATERIALS_HAS_UNITMEASURE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public BILLOFMATERIALS_HAS_UNITMEASURE_IN In { get { return new BILLOFMATERIALS_HAS_UNITMEASURE_IN(this); } }
        public class BILLOFMATERIALS_HAS_UNITMEASURE_IN
        {
            private BILLOFMATERIALS_HAS_UNITMEASURE_REL Parent;
            internal BILLOFMATERIALS_HAS_UNITMEASURE_IN(BILLOFMATERIALS_HAS_UNITMEASURE_REL parent)
            {
                Parent = parent;
            }

			public BillOfMaterialsNode BillOfMaterials { get { return new BillOfMaterialsNode(Parent, DirectionEnum.In); } }
        }

        public BILLOFMATERIALS_HAS_UNITMEASURE_OUT Out { get { return new BILLOFMATERIALS_HAS_UNITMEASURE_OUT(this); } }
        public class BILLOFMATERIALS_HAS_UNITMEASURE_OUT
        {
            private BILLOFMATERIALS_HAS_UNITMEASURE_REL Parent;
            internal BILLOFMATERIALS_HAS_UNITMEASURE_OUT(BILLOFMATERIALS_HAS_UNITMEASURE_REL parent)
            {
                Parent = parent;
            }

			public UnitMeasureNode UnitMeasure { get { return new UnitMeasureNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_BILLOFMATERIALS_HAS_UNITMEASURE_REL
    {
		IFromIn_BILLOFMATERIALS_HAS_UNITMEASURE_REL Alias(out BILLOFMATERIALS_HAS_UNITMEASURE_ALIAS alias);
		IFromIn_BILLOFMATERIALS_HAS_UNITMEASURE_REL Repeat(int maxHops);
		IFromIn_BILLOFMATERIALS_HAS_UNITMEASURE_REL Repeat(int minHops, int maxHops);

        BILLOFMATERIALS_HAS_UNITMEASURE_REL.BILLOFMATERIALS_HAS_UNITMEASURE_OUT Out { get; }
    }
    public interface IFromOut_BILLOFMATERIALS_HAS_UNITMEASURE_REL
    {
		IFromOut_BILLOFMATERIALS_HAS_UNITMEASURE_REL Alias(out BILLOFMATERIALS_HAS_UNITMEASURE_ALIAS alias);
		IFromOut_BILLOFMATERIALS_HAS_UNITMEASURE_REL Repeat(int maxHops);
		IFromOut_BILLOFMATERIALS_HAS_UNITMEASURE_REL Repeat(int minHops, int maxHops);

        BILLOFMATERIALS_HAS_UNITMEASURE_REL.BILLOFMATERIALS_HAS_UNITMEASURE_IN In { get; }
    }

    public class BILLOFMATERIALS_HAS_UNITMEASURE_ALIAS : AliasResult
    {
		private BILLOFMATERIALS_HAS_UNITMEASURE_REL Parent;

        internal BILLOFMATERIALS_HAS_UNITMEASURE_ALIAS(BILLOFMATERIALS_HAS_UNITMEASURE_REL parent)
        {
			Parent = parent;
        }
    }
}
