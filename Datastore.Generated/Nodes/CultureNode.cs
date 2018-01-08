
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static CultureNode Culture { get { return new CultureNode(); } }
	}

	public partial class CultureNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Culture";
            }
        }

		internal CultureNode() { }
		internal CultureNode(CultureAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CultureNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public CultureNode Alias(out CultureAlias alias)
		{
			alias = new CultureAlias(this);
            NodeAlias = alias;
			return this;
		}


		public CultureOut Out { get { return new CultureOut(this); } }
		public class CultureOut
		{
			private CultureNode Parent;
			internal CultureOut(CultureNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PRODUCTMODEL_HAS_CULTURE_REL PRODUCTMODEL_HAS_CULTURE { get { return new PRODUCTMODEL_HAS_CULTURE_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class CultureAlias : AliasResult
    {
        internal CultureAlias(CultureNode parent)
        {
			Node = parent;
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Culture"], Datastore.AdventureWorks.Model.Entities["Culture"].Properties["Name"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Culture"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Culture"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public CultureNode.CultureOut Out { get { return new CultureNode.CultureOut(new CultureNode(this, true)); } }

        public StringResult Name { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
