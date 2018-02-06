using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductInventoryNode ProductInventory { get { return new ProductInventoryNode(); } }
	}

	public partial class ProductInventoryNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ProductInventory";
            }
        }

		internal ProductInventoryNode() { }
		internal ProductInventoryNode(ProductInventoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductInventoryNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ProductInventoryNode Alias(out ProductInventoryAlias alias)
		{
			alias = new ProductInventoryAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public ProductInventoryIn  In  { get { return new ProductInventoryIn(this); } }
		public class ProductInventoryIn
		{
			private ProductInventoryNode Parent;
			internal ProductInventoryIn(ProductInventoryNode parent)
			{
                Parent = parent;
			}
			public IFromIn_PRODUCTINVENTORY_HAS_LOCATION_REL PRODUCTINVENTORY_HAS_LOCATION { get { return new PRODUCTINVENTORY_HAS_LOCATION_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCTINVENTORY_HAS_PRODUCT_REL PRODUCTINVENTORY_HAS_PRODUCT { get { return new PRODUCTINVENTORY_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class ProductInventoryAlias : AliasResult
    {
        internal ProductInventoryAlias(ProductInventoryNode parent)
        {
			Node = parent;
        }

        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields == null)
                {
                    m_AliasFields = new Dictionary<string, FieldResult>()
                    {
						{ "Shelf", new StringResult(this, "Shelf", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["Shelf"]) },
						{ "Bin", new StringResult(this, "Bin", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["Bin"]) },
						{ "Quantity", new NumericResult(this, "Quantity", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["Quantity"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public ProductInventoryNode.ProductInventoryIn In { get { return new ProductInventoryNode.ProductInventoryIn(new ProductInventoryNode(this, true)); } }

        public StringResult Shelf
		{
			get
			{
				if ((object)m_Shelf == null)
					m_Shelf = (StringResult)AliasFields["Shelf"];

				return m_Shelf;
			}
		} 
        private StringResult m_Shelf = null;
        public StringResult Bin
		{
			get
			{
				if ((object)m_Bin == null)
					m_Bin = (StringResult)AliasFields["Bin"];

				return m_Bin;
			}
		} 
        private StringResult m_Bin = null;
        public NumericResult Quantity
		{
			get
			{
				if ((object)m_Quantity == null)
					m_Quantity = (NumericResult)AliasFields["Quantity"];

				return m_Quantity;
			}
		} 
        private NumericResult m_Quantity = null;
        public StringResult rowguid
		{
			get
			{
				if ((object)m_rowguid == null)
					m_rowguid = (StringResult)AliasFields["rowguid"];

				return m_rowguid;
			}
		} 
        private StringResult m_rowguid = null;
        public DateTimeResult ModifiedDate
		{
			get
			{
				if ((object)m_ModifiedDate == null)
					m_ModifiedDate = (DateTimeResult)AliasFields["ModifiedDate"];

				return m_ModifiedDate;
			}
		} 
        private DateTimeResult m_ModifiedDate = null;
        public StringResult Uid
		{
			get
			{
				if ((object)m_Uid == null)
					m_Uid = (StringResult)AliasFields["Uid"];

				return m_Uid;
			}
		} 
        private StringResult m_Uid = null;
    }
}
