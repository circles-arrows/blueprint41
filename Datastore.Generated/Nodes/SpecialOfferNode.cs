using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static SpecialOfferNode SpecialOffer { get { return new SpecialOfferNode(); } }
	}

	public partial class SpecialOfferNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "SpecialOffer";
            }
        }

		internal SpecialOfferNode() { }
		internal SpecialOfferNode(SpecialOfferAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SpecialOfferNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public SpecialOfferNode Alias(out SpecialOfferAlias alias)
		{
			alias = new SpecialOfferAlias(this);
            NodeAlias = alias;
			return this;
		}


		public SpecialOfferOut Out { get { return new SpecialOfferOut(this); } }
		public class SpecialOfferOut
		{
			private SpecialOfferNode Parent;
			internal SpecialOfferOut(SpecialOfferNode parent)
			{
                Parent = parent;
			}
			public IFromOut_SALESORDERDETAIL_HAS_SPECIALOFFER_REL SALESORDERDETAIL_HAS_SPECIALOFFER { get { return new SALESORDERDETAIL_HAS_SPECIALOFFER_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class SpecialOfferAlias : AliasResult
    {
        internal SpecialOfferAlias(SpecialOfferNode parent)
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
						{ "Description", new StringResult(this, "Description", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["Description"]) },
						{ "DiscountPct", new StringResult(this, "DiscountPct", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["DiscountPct"]) },
						{ "Type", new StringResult(this, "Type", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["Type"]) },
						{ "Category", new StringResult(this, "Category", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["Category"]) },
						{ "StartDate", new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["StartDate"]) },
						{ "EndDate", new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["EndDate"]) },
						{ "MinQty", new NumericResult(this, "MinQty", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["MinQty"]) },
						{ "MaxQty", new StringResult(this, "MaxQty", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["MaxQty"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public SpecialOfferNode.SpecialOfferOut Out { get { return new SpecialOfferNode.SpecialOfferOut(new SpecialOfferNode(this, true)); } }

        public StringResult Description
		{
			get
			{
				if ((object)m_Description == null)
					m_Description = (StringResult)AliasFields["Description"];

				return m_Description;
			}
		} 
        private StringResult m_Description = null;
        public StringResult DiscountPct
		{
			get
			{
				if ((object)m_DiscountPct == null)
					m_DiscountPct = (StringResult)AliasFields["DiscountPct"];

				return m_DiscountPct;
			}
		} 
        private StringResult m_DiscountPct = null;
        public StringResult Type
		{
			get
			{
				if ((object)m_Type == null)
					m_Type = (StringResult)AliasFields["Type"];

				return m_Type;
			}
		} 
        private StringResult m_Type = null;
        public StringResult Category
		{
			get
			{
				if ((object)m_Category == null)
					m_Category = (StringResult)AliasFields["Category"];

				return m_Category;
			}
		} 
        private StringResult m_Category = null;
        public DateTimeResult StartDate
		{
			get
			{
				if ((object)m_StartDate == null)
					m_StartDate = (DateTimeResult)AliasFields["StartDate"];

				return m_StartDate;
			}
		} 
        private DateTimeResult m_StartDate = null;
        public DateTimeResult EndDate
		{
			get
			{
				if ((object)m_EndDate == null)
					m_EndDate = (DateTimeResult)AliasFields["EndDate"];

				return m_EndDate;
			}
		} 
        private DateTimeResult m_EndDate = null;
        public NumericResult MinQty
		{
			get
			{
				if ((object)m_MinQty == null)
					m_MinQty = (NumericResult)AliasFields["MinQty"];

				return m_MinQty;
			}
		} 
        private NumericResult m_MinQty = null;
        public StringResult MaxQty
		{
			get
			{
				if ((object)m_MaxQty == null)
					m_MaxQty = (StringResult)AliasFields["MaxQty"];

				return m_MaxQty;
			}
		} 
        private StringResult m_MaxQty = null;
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
