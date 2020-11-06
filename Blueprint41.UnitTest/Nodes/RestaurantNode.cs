using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Datastore.Query
{
	public partial class Node
	{
		public static RestaurantNode Restaurant { get { return new RestaurantNode(); } }
	}

	public partial class RestaurantNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "Restaurant";
        }

		internal RestaurantNode() { }
		internal RestaurantNode(RestaurantAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal RestaurantNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }

		public RestaurantNode Alias(out RestaurantAlias alias)
		{
			alias = new RestaurantAlias(this);
            NodeAlias = alias;
			return this;
		}

		public RestaurantNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }

        protected override Entity GetEntity()
        {
            throw new NotImplementedException();
        }

        public RestaurantIn  In  { get { return new RestaurantIn(this); } }
		public class RestaurantIn
		{
			private RestaurantNode Parent;
			internal RestaurantIn(RestaurantNode parent)
			{
                Parent = parent;
			}
			public IFromIn_RESTAURANT_LOCATED_AT_REL RESTAURANT_LOCATED_AT { get { return new RESTAURANT_LOCATED_AT_REL(Parent, DirectionEnum.In); } }

		}

		public RestaurantOut Out { get { return new RestaurantOut(this); } }
		public class RestaurantOut
		{
			private RestaurantNode Parent;
			internal RestaurantOut(RestaurantNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PERSON_EATS_AT_REL PERSON_EATS_AT { get { return new PERSON_EATS_AT_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class RestaurantAlias : AliasResult
    {
        internal RestaurantAlias(RestaurantNode parent)
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
						{ "Name", new StringResult(this, "Name", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"].Properties["Name"]) },
						{ "Uid", new StringResult(this, "Uid", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"]) },
						{ "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public RestaurantNode.RestaurantIn In { get { return new RestaurantNode.RestaurantIn(new RestaurantNode(this, true)); } }
        public RestaurantNode.RestaurantOut Out { get { return new RestaurantNode.RestaurantOut(new RestaurantNode(this, true)); } }

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
