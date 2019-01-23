using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Datastore.Query
{
	public partial class Node
	{
		[Obsolete("This entity is virtual, consider making entity Base concrete or use another entity as your starting point.", true)]
		public static BaseNode Base { get { return new BaseNode(); } }
	}

	public partial class BaseNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return null;
        }

		internal BaseNode() { }
		internal BaseNode(BaseAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal BaseNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public BaseNode Alias(out BaseAlias alias)
		{
			alias = new BaseAlias(this);
            NodeAlias = alias;
			return this;
		}

		public BaseNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }

		public FilmNode CastToFilm()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new FilmNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public PersonNode CastToPerson()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new PersonNode(FromRelationship, Direction, this.Neo4jLabel);
        }

		public GenreNode CastToGenre()
        {
			if (this.Neo4jLabel == null)
				throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship == null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new GenreNode(FromRelationship, Direction, this.Neo4jLabel);
        }

	}

    public class BaseAlias : AliasResult
    {
        internal BaseAlias(BaseNode parent)
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
						{ "Uid", new StringResult(this, "Uid", Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Base"], Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Base"].Properties["Uid"]) },
						{ "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Base"], Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Base"].Properties["LastModifiedOn"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;


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
