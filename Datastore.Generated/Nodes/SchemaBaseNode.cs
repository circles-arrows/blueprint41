using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		[Obsolete("This entity is virtual, consider making entity SchemaBase concrete or use another entity as your starting point.", true)]
		public static SchemaBaseNode SchemaBase { get { return new SchemaBaseNode(); } }
	}

	public partial class SchemaBaseNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
                return null;
            }
        }

		internal SchemaBaseNode() { }
		internal SchemaBaseNode(SchemaBaseAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SchemaBaseNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public SchemaBaseNode Alias(out SchemaBaseAlias alias)
		{
			alias = new SchemaBaseAlias(this);
            NodeAlias = alias;
			return this;
		}

	}

    public class SchemaBaseAlias : AliasResult
    {
        internal SchemaBaseAlias(SchemaBaseNode parent)
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
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SchemaBase"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SchemaBase"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;


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
