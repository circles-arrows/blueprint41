
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static UnitMeasureNode UnitMeasure { get { return new UnitMeasureNode(); } }
	}

	public partial class UnitMeasureNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "UnitMeasure";
            }
        }

		internal UnitMeasureNode() { }
		internal UnitMeasureNode(UnitMeasureAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal UnitMeasureNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public UnitMeasureNode Alias(out UnitMeasureAlias alias)
		{
			alias = new UnitMeasureAlias(this);
            NodeAlias = alias;
			return this;
		}


		public UnitMeasureOut Out { get { return new UnitMeasureOut(this); } }
		public class UnitMeasureOut
		{
			private UnitMeasureNode Parent;
			internal UnitMeasureOut(UnitMeasureNode parent)
			{
                Parent = parent;
			}
			public IFromOut_BILLOFMATERIALS_HAS_UNITMEASURE_REL BILLOFMATERIALS_HAS_UNITMEASURE { get { return new BILLOFMATERIALS_HAS_UNITMEASURE_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PRODUCTVENDOR_HAS_UNITMEASURE_REL PRODUCTVENDOR_HAS_UNITMEASURE { get { return new PRODUCTVENDOR_HAS_UNITMEASURE_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class UnitMeasureAlias : AliasResult
    {
        internal UnitMeasureAlias(UnitMeasureNode parent)
        {
			Node = parent;
            UnitMeasureCorde = new StringResult(this, "UnitMeasureCorde", Datastore.AdventureWorks.Model.Entities["UnitMeasure"], Datastore.AdventureWorks.Model.Entities["UnitMeasure"].Properties["UnitMeasureCorde"]);
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["UnitMeasure"], Datastore.AdventureWorks.Model.Entities["UnitMeasure"].Properties["Name"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["UnitMeasure"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["UnitMeasure"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public UnitMeasureNode.UnitMeasureOut Out { get { return new UnitMeasureNode.UnitMeasureOut(new UnitMeasureNode(this, true)); } }

        public StringResult UnitMeasureCorde { get; private set; } 
        public StringResult Name { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
