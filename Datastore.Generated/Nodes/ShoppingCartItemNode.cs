using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ShoppingCartItemNode ShoppingCartItem { get { return new ShoppingCartItemNode(); } }
	}

	public partial class ShoppingCartItemNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "ShoppingCartItem";
        }

		internal ShoppingCartItemNode() { }
		internal ShoppingCartItemNode(ShoppingCartItemAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ShoppingCartItemNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public ShoppingCartItemNode Alias(out ShoppingCartItemAlias alias)
		{
			alias = new ShoppingCartItemAlias(this);
            NodeAlias = alias;
			return this;
		}

		public ShoppingCartItemNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }

	
		public ShoppingCartItemIn  In  { get { return new ShoppingCartItemIn(this); } }
		public class ShoppingCartItemIn
		{
			private ShoppingCartItemNode Parent;
			internal ShoppingCartItemIn(ShoppingCartItemNode parent)
			{
                Parent = parent;
			}
			public IFromIn_SHOPPINGCARTITEM_HAS_PRODUCT_REL SHOPPINGCARTITEM_HAS_PRODUCT { get { return new SHOPPINGCARTITEM_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class ShoppingCartItemAlias : AliasResult
    {
        internal ShoppingCartItemAlias(ShoppingCartItemNode parent)
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
						{ "Quantity", new NumericResult(this, "Quantity", Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"], Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"].Properties["Quantity"]) },
						{ "DateCreated", new DateTimeResult(this, "DateCreated", Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"], Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"].Properties["DateCreated"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public ShoppingCartItemNode.ShoppingCartItemIn In { get { return new ShoppingCartItemNode.ShoppingCartItemIn(new ShoppingCartItemNode(this, true)); } }

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
        public DateTimeResult DateCreated
		{
			get
			{
				if ((object)m_DateCreated == null)
					m_DateCreated = (DateTimeResult)AliasFields["DateCreated"];

				return m_DateCreated;
			}
		} 
        private DateTimeResult m_DateCreated = null;
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
