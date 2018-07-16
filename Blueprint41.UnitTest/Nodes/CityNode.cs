using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Datastore.Query
{
	public partial class Node
	{
		public static CityNode City { get { return new CityNode(); } }
	}

	public partial class CityNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "City";
        }

		internal CityNode() { }
		internal CityNode(CityAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CityNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public CityNode Alias(out CityAlias alias)
		{
			alias = new CityAlias(this);
            NodeAlias = alias;
			return this;
		}

		public CityNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }


		public CityOut Out { get { return new CityOut(this); } }
		public class CityOut
		{
			private CityNode Parent;
			internal CityOut(CityNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PERSON_LIVES_IN_REL PERSON_LIVES_IN { get { return new PERSON_LIVES_IN_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_RESTAURANT_LOCATED_AT_REL RESTAURANT_LOCATED_AT { get { return new RESTAURANT_LOCATED_AT_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class CityAlias : AliasResult
    {
        internal CityAlias(CityNode parent)
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
						{ "Name", new StringResult(this, "Name", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"].Properties["Name"]) },
						{ "Uid", new StringResult(this, "Uid", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"]) },
						{ "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public CityNode.CityOut Out { get { return new CityNode.CityOut(new CityNode(this, true)); } }

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
        public DateTimeResult LastModifiedOn
		{
			get
			{
				if ((object)m_LastModifiedOn == null)
					m_LastModifiedOn = (DateTimeResult)AliasFields["LastModifiedOn"];

				return m_LastModifiedOn;
			}
		} 
        private DateTimeResult m_LastModifiedOn = null;
    }
}
