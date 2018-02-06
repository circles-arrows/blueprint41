using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static PasswordNode Password { get { return new PasswordNode(); } }
	}

	public partial class PasswordNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Password";
            }
        }

		internal PasswordNode() { }
		internal PasswordNode(PasswordAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal PasswordNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public PasswordNode Alias(out PasswordAlias alias)
		{
			alias = new PasswordAlias(this);
            NodeAlias = alias;
			return this;
		}


		public PasswordOut Out { get { return new PasswordOut(this); } }
		public class PasswordOut
		{
			private PasswordNode Parent;
			internal PasswordOut(PasswordNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PERSON_HAS_PASSWORD_REL PERSON_HAS_PASSWORD { get { return new PERSON_HAS_PASSWORD_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class PasswordAlias : AliasResult
    {
        internal PasswordAlias(PasswordNode parent)
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
						{ "PasswordHash", new StringResult(this, "PasswordHash", Datastore.AdventureWorks.Model.Entities["Password"], Datastore.AdventureWorks.Model.Entities["Password"].Properties["PasswordHash"]) },
						{ "PasswordSalt", new StringResult(this, "PasswordSalt", Datastore.AdventureWorks.Model.Entities["Password"], Datastore.AdventureWorks.Model.Entities["Password"].Properties["PasswordSalt"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Password"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public PasswordNode.PasswordOut Out { get { return new PasswordNode.PasswordOut(new PasswordNode(this, true)); } }

        public StringResult PasswordHash
		{
			get
			{
				if ((object)m_PasswordHash == null)
					m_PasswordHash = (StringResult)AliasFields["PasswordHash"];

				return m_PasswordHash;
			}
		} 
        private StringResult m_PasswordHash = null;
        public StringResult PasswordSalt
		{
			get
			{
				if ((object)m_PasswordSalt == null)
					m_PasswordSalt = (StringResult)AliasFields["PasswordSalt"];

				return m_PasswordSalt;
			}
		} 
        private StringResult m_PasswordSalt = null;
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
