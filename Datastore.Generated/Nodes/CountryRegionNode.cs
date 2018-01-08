
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static CountryRegionNode CountryRegion { get { return new CountryRegionNode(); } }
	}

	public partial class CountryRegionNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "CountryRegion";
            }
        }

		internal CountryRegionNode() { }
		internal CountryRegionNode(CountryRegionAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CountryRegionNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public CountryRegionNode Alias(out CountryRegionAlias alias)
		{
			alias = new CountryRegionAlias(this);
            NodeAlias = alias;
			return this;
		}


		public CountryRegionOut Out { get { return new CountryRegionOut(this); } }
		public class CountryRegionOut
		{
			private CountryRegionNode Parent;
			internal CountryRegionOut(CountryRegionNode parent)
			{
                Parent = parent;
			}
			public IFromOut_STATEPROVINCE_HAS_COUNTRYREGION_REL STATEPROVINCE_HAS_COUNTRYREGION { get { return new STATEPROVINCE_HAS_COUNTRYREGION_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class CountryRegionAlias : AliasResult
    {
        internal CountryRegionAlias(CountryRegionNode parent)
        {
			Node = parent;
            Code = new NumericResult(this, "Code", Datastore.AdventureWorks.Model.Entities["CountryRegion"], Datastore.AdventureWorks.Model.Entities["CountryRegion"].Properties["Code"]);
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["CountryRegion"], Datastore.AdventureWorks.Model.Entities["CountryRegion"].Properties["Name"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["CountryRegion"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["CountryRegion"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public CountryRegionNode.CountryRegionOut Out { get { return new CountryRegionNode.CountryRegionOut(new CountryRegionNode(this, true)); } }

        public NumericResult Code { get; private set; } 
        public StringResult Name { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
