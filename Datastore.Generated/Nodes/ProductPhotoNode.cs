using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductPhotoNode ProductPhoto { get { return new ProductPhotoNode(); } }
	}

	public partial class ProductPhotoNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "ProductPhoto";
        }

		internal ProductPhotoNode() { }
		internal ProductPhotoNode(ProductPhotoAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductPhotoNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public ProductPhotoNode Alias(out ProductPhotoAlias alias)
		{
			alias = new ProductPhotoAlias(this);
            NodeAlias = alias;
			return this;
		}

		public ProductPhotoNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }


		public ProductPhotoOut Out { get { return new ProductPhotoOut(this); } }
		public class ProductPhotoOut
		{
			private ProductPhotoNode Parent;
			internal ProductPhotoOut(ProductPhotoNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO { get { return new PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ProductPhotoAlias : AliasResult
    {
        internal ProductPhotoAlias(ProductPhotoNode parent)
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
						{ "ThumbNailPhoto", new StringResult(this, "ThumbNailPhoto", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["ProductPhoto"].Properties["ThumbNailPhoto"]) },
						{ "ThumbNailPhotoFileName", new StringResult(this, "ThumbNailPhotoFileName", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["ProductPhoto"].Properties["ThumbNailPhotoFileName"]) },
						{ "LargePhoto", new StringResult(this, "LargePhoto", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["ProductPhoto"].Properties["LargePhoto"]) },
						{ "LargePhotoFileName", new StringResult(this, "LargePhotoFileName", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["ProductPhoto"].Properties["LargePhotoFileName"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public ProductPhotoNode.ProductPhotoOut Out { get { return new ProductPhotoNode.ProductPhotoOut(new ProductPhotoNode(this, true)); } }

        public StringResult ThumbNailPhoto
		{
			get
			{
				if ((object)m_ThumbNailPhoto == null)
					m_ThumbNailPhoto = (StringResult)AliasFields["ThumbNailPhoto"];

				return m_ThumbNailPhoto;
			}
		} 
        private StringResult m_ThumbNailPhoto = null;
        public StringResult ThumbNailPhotoFileName
		{
			get
			{
				if ((object)m_ThumbNailPhotoFileName == null)
					m_ThumbNailPhotoFileName = (StringResult)AliasFields["ThumbNailPhotoFileName"];

				return m_ThumbNailPhotoFileName;
			}
		} 
        private StringResult m_ThumbNailPhotoFileName = null;
        public StringResult LargePhoto
		{
			get
			{
				if ((object)m_LargePhoto == null)
					m_LargePhoto = (StringResult)AliasFields["LargePhoto"];

				return m_LargePhoto;
			}
		} 
        private StringResult m_LargePhoto = null;
        public StringResult LargePhotoFileName
		{
			get
			{
				if ((object)m_LargePhotoFileName == null)
					m_LargePhotoFileName = (StringResult)AliasFields["LargePhotoFileName"];

				return m_LargePhotoFileName;
			}
		} 
        private StringResult m_LargePhotoFileName = null;
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
