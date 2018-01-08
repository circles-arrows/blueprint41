
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static CreditCardNode CreditCard { get { return new CreditCardNode(); } }
	}

	public partial class CreditCardNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "CreditCard";
            }
        }

		internal CreditCardNode() { }
		internal CreditCardNode(CreditCardAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CreditCardNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public CreditCardNode Alias(out CreditCardAlias alias)
		{
			alias = new CreditCardAlias(this);
            NodeAlias = alias;
			return this;
		}


		public CreditCardOut Out { get { return new CreditCardOut(this); } }
		public class CreditCardOut
		{
			private CreditCardNode Parent;
			internal CreditCardOut(CreditCardNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PERSON_VALID_FOR_CREDITCARD_REL PERSON_VALID_FOR_CREDITCARD { get { return new PERSON_VALID_FOR_CREDITCARD_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SALESORDERHEADER_HAS_CREDITCARD_REL SALESORDERHEADER_HAS_CREDITCARD { get { return new SALESORDERHEADER_HAS_CREDITCARD_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class CreditCardAlias : AliasResult
    {
        internal CreditCardAlias(CreditCardNode parent)
        {
			Node = parent;
            CardType = new StringResult(this, "CardType", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["CardType"]);
            CardNumber = new StringResult(this, "CardNumber", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["CardNumber"]);
            ExpMonth = new StringResult(this, "ExpMonth", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["ExpMonth"]);
            ExpYear = new StringResult(this, "ExpYear", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["ExpYear"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public CreditCardNode.CreditCardOut Out { get { return new CreditCardNode.CreditCardOut(new CreditCardNode(this, true)); } }

        public StringResult CardType { get; private set; } 
        public StringResult CardNumber { get; private set; } 
        public StringResult ExpMonth { get; private set; } 
        public StringResult ExpYear { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
