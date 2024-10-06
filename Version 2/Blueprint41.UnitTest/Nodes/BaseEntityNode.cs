#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable CS8981 // Names should not be lower type only

using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Events;
using Blueprint41.Persistence;
using Blueprint41.Query;

using m = Datastore.Manipulation;

namespace Datastore.Query
{
    public partial class Node
    {
        [Obsolete("This entity is virtual, consider making entity BaseEntity concrete or use another entity as your starting point.", true)]
        public static BaseEntityNode BaseEntity { get { return new BaseEntityNode(); } }
    }

    public partial class BaseEntityNode : Blueprint41.Query.Node
    {
        public static implicit operator QueryCondition(BaseEntityNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(BaseEntityNode a)
        {
            return new QueryCondition(a, true);
        } 

        protected override string GetNeo4jLabel()
        {
            return null;
        }

        protected override Entity GetEntity()
        {
            return null;
        }

        internal BaseEntityNode() { }
        internal BaseEntityNode(BaseEntityAlias alias, bool isReference = false)
        {
            NodeAlias = alias;
            IsReference = isReference;
        }
        internal BaseEntityNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
        internal BaseEntityNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
        {
            NodeAlias = nodeAlias;
        }

        public BaseEntityNode Where(JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<BaseEntityAlias> alias = new Lazy<BaseEntityAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (LastModifiedOn.HasValue) conditions.Add(new QueryCondition(alias.Value.LastModifiedOn, Operator.Equals, ((IValue)LastModifiedOn).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public BaseEntityNode Assign(JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<BaseEntityAlias> alias = new Lazy<BaseEntityAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (LastModifiedOn.HasValue) assignments.Add(new Assignment(alias.Value.LastModifiedOn, LastModifiedOn));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

        public BaseEntityNode Alias(out BaseEntityAlias alias)
        {
            if (NodeAlias is BaseEntityAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new BaseEntityAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
        public BaseEntityNode Alias(out BaseEntityAlias alias, string name)
        {
            if (NodeAlias is BaseEntityAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new BaseEntityAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

        public BaseEntityNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
            IsReference = true;
            return this;
        }

        public PersonNode CastToPerson()
        {
            if (this.Neo4jLabel is null)
                throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship is null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new PersonNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
        }

        public CityNode CastToCity()
        {
            if (this.Neo4jLabel is null)
                throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship is null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new CityNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
        }

        public RestaurantNode CastToRestaurant()
        {
            if (this.Neo4jLabel is null)
                throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship is null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new RestaurantNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
        }

        public MovieNode CastToMovie()
        {
            if (this.Neo4jLabel is null)
                throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship is null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new MovieNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
        }

        public StreamingServiceNode CastToStreamingService()
        {
            if (this.Neo4jLabel is null)
                throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship is null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new StreamingServiceNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
        }

        public RatingNode CastToRating()
        {
            if (this.Neo4jLabel is null)
                throw new InvalidOperationException("Casting is not supported for virtual entities.");

            if (FromRelationship is null)
                throw new InvalidOperationException("Please use the right type immediately, casting is only support after you have match through a relationship.");

            return new RatingNode(FromRelationship, Direction, NodeAlias, this.Neo4jLabel, this.Entity);
        }

    }

    public class BaseEntityAlias : AliasResult<BaseEntityAlias, BaseEntityListAlias>
    {
        internal BaseEntityAlias(BaseEntityNode parent)
        {
            Node = parent;
        }
        internal BaseEntityAlias(BaseEntityNode parent, string name)
        {
            Node = parent;
            AliasName = name;
        }
        internal void SetAlias(string name) => AliasName = name;

        private  BaseEntityAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private  BaseEntityAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private  BaseEntityAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
        {
            Node = alias.Node;
        }

        public Assignment[] Assign(JsNotation<System.DateTime> LastModifiedOn = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (LastModifiedOn.HasValue) assignments.Add(new Assignment(this.LastModifiedOn, LastModifiedOn));
            if (Uid.HasValue) assignments.Add(new Assignment(this.Uid, Uid));
            
            return assignments.ToArray();
        }


        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields is null)
                {
                    m_AliasFields = new Dictionary<string, FieldResult>()
                    {
                        { "Uid", new StringResult(this, "Uid", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"]) },
                        { "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"]) },
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
                if (m_Uid is null)
                    m_Uid = (StringResult)AliasFields["Uid"];

                return m_Uid;
            }
        }
        private StringResult m_Uid = null;
        public DateTimeResult LastModifiedOn
        {
            get
            {
                if (m_LastModifiedOn is null)
                    m_LastModifiedOn = (DateTimeResult)AliasFields["LastModifiedOn"];

                return m_LastModifiedOn;
            }
        }
        private DateTimeResult m_LastModifiedOn = null;
        public AsResult As(string aliasName, out BaseEntityAlias alias)
        {
            alias = new BaseEntityAlias((BaseEntityNode)Node)
            {
                AliasName = aliasName
            };
            return this.As(aliasName);
        }
    }

    public class BaseEntityListAlias : ListResult<BaseEntityListAlias, BaseEntityAlias>, IAliasListResult
    {
        private BaseEntityListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private BaseEntityListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private BaseEntityListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
    }
    public class BaseEntityJaggedListAlias : ListResult<BaseEntityJaggedListAlias, BaseEntityListAlias>, IAliasJaggedListResult
    {
        private BaseEntityJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
        private BaseEntityJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        private BaseEntityJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
    }
}
