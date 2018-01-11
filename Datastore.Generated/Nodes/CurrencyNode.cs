using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static CurrencyNode Currency { get { return new CurrencyNode(); } }
	}

	public partial class CurrencyNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Currency";
            }
        }

		internal CurrencyNode() { }
		internal CurrencyNode(CurrencyAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CurrencyNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public CurrencyNode Alias(out CurrencyAlias alias)
		{
			alias = new CurrencyAlias(this);
            NodeAlias = alias;
			return this;
		}


		public CurrencyOut Out { get { return new CurrencyOut(this); } }
		public class CurrencyOut
		{
			private CurrencyNode Parent;
			internal CurrencyOut(CurrencyNode parent)
			{
                Parent = parent;
			}
			public IFromOut_CURRENCYRATE_HAS_CURRENCY_REL CURRENCYRATE_HAS_CURRENCY { get { return new CURRENCYRATE_HAS_CURRENCY_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class CurrencyAlias : AliasResult
    {
        internal CurrencyAlias(CurrencyNode parent)
        {
			Node = parent;
            CurrencyCode = new StringResult(this, "CurrencyCode", Datastore.AdventureWorks.Model.Entities["Currency"], Datastore.AdventureWorks.Model.Entities["Currency"].Properties["CurrencyCode"]);
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Currency"], Datastore.AdventureWorks.Model.Entities["Currency"].Properties["Name"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Currency"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Currency"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public CurrencyNode.CurrencyOut Out { get { return new CurrencyNode.CurrencyOut(new CurrencyNode(this, true)); } }

        public StringResult CurrencyCode { get; private set; } 
        public StringResult Name { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
