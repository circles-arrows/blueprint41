
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ShipMethodNode ShipMethod { get { return new ShipMethodNode(); } }
	}

	public partial class ShipMethodNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ShipMethod";
            }
        }

		internal ShipMethodNode() { }
		internal ShipMethodNode(ShipMethodAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ShipMethodNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ShipMethodNode Alias(out ShipMethodAlias alias)
		{
			alias = new ShipMethodAlias(this);
            NodeAlias = alias;
			return this;
		}


		public ShipMethodOut Out { get { return new ShipMethodOut(this); } }
		public class ShipMethodOut
		{
			private ShipMethodNode Parent;
			internal ShipMethodOut(ShipMethodNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PURCHASEORDERHEADER_HAS_SHIPMETHOD_REL PURCHASEORDERHEADER_HAS_SHIPMETHOD { get { return new PURCHASEORDERHEADER_HAS_SHIPMETHOD_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SALESORDERHEADER_HAS_SHIPMETHOD_REL SALESORDERHEADER_HAS_SHIPMETHOD { get { return new SALESORDERHEADER_HAS_SHIPMETHOD_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ShipMethodAlias : AliasResult
    {
        internal ShipMethodAlias(ShipMethodNode parent)
        {
			Node = parent;
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["ShipMethod"].Properties["Name"]);
            ShipBase = new StringResult(this, "ShipBase", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["ShipMethod"].Properties["ShipBase"]);
            ShipRate = new StringResult(this, "ShipRate", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["ShipMethod"].Properties["ShipRate"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["ShipMethod"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public ShipMethodNode.ShipMethodOut Out { get { return new ShipMethodNode.ShipMethodOut(new ShipMethodNode(this, true)); } }

        public StringResult Name { get; private set; } 
        public StringResult ShipBase { get; private set; } 
        public StringResult ShipRate { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
