using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static CreditCardNode CreditCard { get { return new CreditCardNode(); } }
	}

	public partial class CreditCardNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "CreditCard";
        }

		internal CreditCardNode() { }
		internal CreditCardNode(CreditCardAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CreditCardNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public CreditCardNode Alias(out CreditCardAlias alias)
		{
			alias = new CreditCardAlias(this);
            NodeAlias = alias;
			return this;
		}

		public CreditCardNode UseExistingAlias(AliasResult alias)
        {
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
        }

        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields == null)
                {
                    m_AliasFields = new Dictionary<string, FieldResult>()
                    {
						{ "CardType", new StringResult(this, "CardType", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["CardType"]) },
						{ "CardNumber", new StringResult(this, "CardNumber", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["CardNumber"]) },
						{ "ExpMonth", new StringResult(this, "ExpMonth", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["ExpMonth"]) },
						{ "ExpYear", new StringResult(this, "ExpYear", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["ExpYear"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public CreditCardNode.CreditCardOut Out { get { return new CreditCardNode.CreditCardOut(new CreditCardNode(this, true)); } }

        public StringResult CardType
		{
			get
			{
				if ((object)m_CardType == null)
					m_CardType = (StringResult)AliasFields["CardType"];

				return m_CardType;
			}
		} 
        private StringResult m_CardType = null;
        public StringResult CardNumber
		{
			get
			{
				if ((object)m_CardNumber == null)
					m_CardNumber = (StringResult)AliasFields["CardNumber"];

				return m_CardNumber;
			}
		} 
        private StringResult m_CardNumber = null;
        public StringResult ExpMonth
		{
			get
			{
				if ((object)m_ExpMonth == null)
					m_ExpMonth = (StringResult)AliasFields["ExpMonth"];

				return m_ExpMonth;
			}
		} 
        private StringResult m_ExpMonth = null;
        public StringResult ExpYear
		{
			get
			{
				if ((object)m_ExpYear == null)
					m_ExpYear = (StringResult)AliasFields["ExpYear"];

				return m_ExpYear;
			}
		} 
        private StringResult m_ExpYear = null;
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
