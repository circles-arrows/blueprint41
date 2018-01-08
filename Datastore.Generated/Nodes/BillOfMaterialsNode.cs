
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static BillOfMaterialsNode BillOfMaterials { get { return new BillOfMaterialsNode(); } }
	}

	public partial class BillOfMaterialsNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "BillOfMaterials";
            }
        }

		internal BillOfMaterialsNode() { }
		internal BillOfMaterialsNode(BillOfMaterialsAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal BillOfMaterialsNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public BillOfMaterialsNode Alias(out BillOfMaterialsAlias alias)
		{
			alias = new BillOfMaterialsAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public BillOfMaterialsIn  In  { get { return new BillOfMaterialsIn(this); } }
		public class BillOfMaterialsIn
		{
			private BillOfMaterialsNode Parent;
			internal BillOfMaterialsIn(BillOfMaterialsNode parent)
			{
                Parent = parent;
			}
			public IFromIn_BILLOFMATERIALS_HAS_PRODUCT_REL BILLOFMATERIALS_HAS_PRODUCT { get { return new BILLOFMATERIALS_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_BILLOFMATERIALS_HAS_UNITMEASURE_REL BILLOFMATERIALS_HAS_UNITMEASURE { get { return new BILLOFMATERIALS_HAS_UNITMEASURE_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class BillOfMaterialsAlias : AliasResult
    {
        internal BillOfMaterialsAlias(BillOfMaterialsNode parent)
        {
			Node = parent;
            StartDate = new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["StartDate"]);
            EndDate = new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["EndDate"]);
            UnitMeasureCode = new StringResult(this, "UnitMeasureCode", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["UnitMeasureCode"]);
            BOMLevel = new StringResult(this, "BOMLevel", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["BOMLevel"]);
            PerAssemblyQty = new NumericResult(this, "PerAssemblyQty", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["PerAssemblyQty"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public BillOfMaterialsNode.BillOfMaterialsIn In { get { return new BillOfMaterialsNode.BillOfMaterialsIn(new BillOfMaterialsNode(this, true)); } }

        public DateTimeResult StartDate { get; private set; } 
        public DateTimeResult EndDate { get; private set; } 
        public StringResult UnitMeasureCode { get; private set; } 
        public StringResult BOMLevel { get; private set; } 
        public NumericResult PerAssemblyQty { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
