using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ShipMethodNode ShipMethod { get { return new ShipMethodNode(); } }
	}

	public partial class ShipMethodNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "ShipMethod";
        }

		internal ShipMethodNode() { }
		internal ShipMethodNode(ShipMethodAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ShipMethodNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public ShipMethodNode Alias(out ShipMethodAlias alias)
		{
			alias = new ShipMethodAlias(this);
            NodeAlias = alias;
			return this;
		}

		public ShipMethodNode UseExistingAlias(AliasResult alias)
        {
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
        }

        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields == null)
                {
                    m_AliasFields = new Dictionary<string, FieldResult>()
                    {
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["ShipMethod"].Properties["Name"]) },
						{ "ShipBase", new StringResult(this, "ShipBase", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["ShipMethod"].Properties["ShipBase"]) },
						{ "ShipRate", new StringResult(this, "ShipRate", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["ShipMethod"].Properties["ShipRate"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["ShipMethod"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public ShipMethodNode.ShipMethodOut Out { get { return new ShipMethodNode.ShipMethodOut(new ShipMethodNode(this, true)); } }

        public StringResult Name
		{
			get
			{
				if ((object)m_Name == null)
					m_Name = (StringResult)AliasFields["Name"];

				return m_Name;
			}
		} 
        private StringResult m_Name = null;
        public StringResult ShipBase
		{
			get
			{
				if ((object)m_ShipBase == null)
					m_ShipBase = (StringResult)AliasFields["ShipBase"];

				return m_ShipBase;
			}
		} 
        private StringResult m_ShipBase = null;
        public StringResult ShipRate
		{
			get
			{
				if ((object)m_ShipRate == null)
					m_ShipRate = (StringResult)AliasFields["ShipRate"];

				return m_ShipRate;
			}
		} 
        private StringResult m_ShipRate = null;
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
