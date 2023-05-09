using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Query;

using m = Domain.Data.Manipulation;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static VendorNode Vendor { get { return new VendorNode(); } }
	}

	public partial class VendorNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(VendorNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(VendorNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Vendor";
		}

		protected override Entity GetEntity()
        {
			return m.Vendor.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.Vendor.Entity.FunctionalId;
            }
        }

		internal VendorNode() { }
		internal VendorNode(VendorAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal VendorNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal VendorNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public VendorNode Where(JsNotation<string> AccountNumber = default, JsNotation<string> ActiveFlag = default, JsNotation<string> CreditRating = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> PreferredVendorStatus = default, JsNotation<string> PurchasingWebServiceURL = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<VendorAlias> alias = new Lazy<VendorAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (AccountNumber.HasValue) conditions.Add(new QueryCondition(alias.Value.AccountNumber, Operator.Equals, ((IValue)AccountNumber).GetValue()));
            if (ActiveFlag.HasValue) conditions.Add(new QueryCondition(alias.Value.ActiveFlag, Operator.Equals, ((IValue)ActiveFlag).GetValue()));
            if (CreditRating.HasValue) conditions.Add(new QueryCondition(alias.Value.CreditRating, Operator.Equals, ((IValue)CreditRating).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (PreferredVendorStatus.HasValue) conditions.Add(new QueryCondition(alias.Value.PreferredVendorStatus, Operator.Equals, ((IValue)PreferredVendorStatus).GetValue()));
            if (PurchasingWebServiceURL.HasValue) conditions.Add(new QueryCondition(alias.Value.PurchasingWebServiceURL, Operator.Equals, ((IValue)PurchasingWebServiceURL).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public VendorNode Assign(JsNotation<string> AccountNumber = default, JsNotation<string> ActiveFlag = default, JsNotation<string> CreditRating = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> PreferredVendorStatus = default, JsNotation<string> PurchasingWebServiceURL = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<VendorAlias> alias = new Lazy<VendorAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (AccountNumber.HasValue) assignments.Add(new Assignment(alias.Value.AccountNumber, AccountNumber));
            if (ActiveFlag.HasValue) assignments.Add(new Assignment(alias.Value.ActiveFlag, ActiveFlag));
            if (CreditRating.HasValue) assignments.Add(new Assignment(alias.Value.CreditRating, CreditRating));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (PreferredVendorStatus.HasValue) assignments.Add(new Assignment(alias.Value.PreferredVendorStatus, PreferredVendorStatus));
            if (PurchasingWebServiceURL.HasValue) assignments.Add(new Assignment(alias.Value.PurchasingWebServiceURL, PurchasingWebServiceURL));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public VendorNode Alias(out VendorAlias alias)
        {
            if (NodeAlias is VendorAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new VendorAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public VendorNode Alias(out VendorAlias alias, string name)
        {
            if (NodeAlias is VendorAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new VendorAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public VendorNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public VendorIn  In  { get { return new VendorIn(this); } }
		public class VendorIn
		{
			private VendorNode Parent;
			internal VendorIn(VendorNode parent)
			{
				Parent = parent;
			}
			public IFromIn_VENDOR_BECOMES_PRODUCTVENDOR_REL VENDOR_BECOMES_PRODUCTVENDOR { get { return new VENDOR_BECOMES_PRODUCTVENDOR_REL(Parent, DirectionEnum.In); } }
			public IFromIn_VENDOR_VALID_FOR_EMPLOYEE_REL VENDOR_VALID_FOR_EMPLOYEE { get { return new VENDOR_VALID_FOR_EMPLOYEE_REL(Parent, DirectionEnum.In); } }

		}

		public VendorOut Out { get { return new VendorOut(this); } }
		public class VendorOut
		{
			private VendorNode Parent;
			internal VendorOut(VendorNode parent)
			{
				Parent = parent;
			}
			public IFromOut_PURCHASEORDERHEADER_HAS_VENDOR_REL PURCHASEORDERHEADER_HAS_VENDOR { get { return new PURCHASEORDERHEADER_HAS_VENDOR_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class VendorAlias : AliasResult<VendorAlias, VendorListAlias>
	{
		internal VendorAlias(VendorNode parent)
		{
			Node = parent;
		}
		internal VendorAlias(VendorNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  VendorAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  VendorAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  VendorAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> AccountNumber = default, JsNotation<string> ActiveFlag = default, JsNotation<string> CreditRating = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> PreferredVendorStatus = default, JsNotation<string> PurchasingWebServiceURL = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (AccountNumber.HasValue) assignments.Add(new Assignment(this.AccountNumber, AccountNumber));
			if (ActiveFlag.HasValue) assignments.Add(new Assignment(this.ActiveFlag, ActiveFlag));
			if (CreditRating.HasValue) assignments.Add(new Assignment(this.CreditRating, CreditRating));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Name.HasValue) assignments.Add(new Assignment(this.Name, Name));
			if (PreferredVendorStatus.HasValue) assignments.Add(new Assignment(this.PreferredVendorStatus, PreferredVendorStatus));
			if (PurchasingWebServiceURL.HasValue) assignments.Add(new Assignment(this.PurchasingWebServiceURL, PurchasingWebServiceURL));
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
						{ "AccountNumber", new StringResult(this, "AccountNumber", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["AccountNumber"]) },
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["Name"]) },
						{ "CreditRating", new StringResult(this, "CreditRating", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["CreditRating"]) },
						{ "PreferredVendorStatus", new StringResult(this, "PreferredVendorStatus", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["PreferredVendorStatus"]) },
						{ "ActiveFlag", new StringResult(this, "ActiveFlag", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["ActiveFlag"]) },
						{ "PurchasingWebServiceURL", new StringResult(this, "PurchasingWebServiceURL", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Vendor"].Properties["PurchasingWebServiceURL"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Vendor"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public VendorNode.VendorIn In { get { return new VendorNode.VendorIn(new VendorNode(this, true)); } }
		public VendorNode.VendorOut Out { get { return new VendorNode.VendorOut(new VendorNode(this, true)); } }

		public StringResult AccountNumber
		{
			get
			{
				if (m_AccountNumber is null)
					m_AccountNumber = (StringResult)AliasFields["AccountNumber"];

				return m_AccountNumber;
			}
		}
		private StringResult m_AccountNumber = null;
		public StringResult Name
		{
			get
			{
				if (m_Name is null)
					m_Name = (StringResult)AliasFields["Name"];

				return m_Name;
			}
		}
		private StringResult m_Name = null;
		public StringResult CreditRating
		{
			get
			{
				if (m_CreditRating is null)
					m_CreditRating = (StringResult)AliasFields["CreditRating"];

				return m_CreditRating;
			}
		}
		private StringResult m_CreditRating = null;
		public StringResult PreferredVendorStatus
		{
			get
			{
				if (m_PreferredVendorStatus is null)
					m_PreferredVendorStatus = (StringResult)AliasFields["PreferredVendorStatus"];

				return m_PreferredVendorStatus;
			}
		}
		private StringResult m_PreferredVendorStatus = null;
		public StringResult ActiveFlag
		{
			get
			{
				if (m_ActiveFlag is null)
					m_ActiveFlag = (StringResult)AliasFields["ActiveFlag"];

				return m_ActiveFlag;
			}
		}
		private StringResult m_ActiveFlag = null;
		public StringResult PurchasingWebServiceURL
		{
			get
			{
				if (m_PurchasingWebServiceURL is null)
					m_PurchasingWebServiceURL = (StringResult)AliasFields["PurchasingWebServiceURL"];

				return m_PurchasingWebServiceURL;
			}
		}
		private StringResult m_PurchasingWebServiceURL = null;
		public DateTimeResult ModifiedDate
		{
			get
			{
				if (m_ModifiedDate is null)
					m_ModifiedDate = (DateTimeResult)AliasFields["ModifiedDate"];

				return m_ModifiedDate;
			}
		}
		private DateTimeResult m_ModifiedDate = null;
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
		public AsResult As(string aliasName, out VendorAlias alias)
		{
			alias = new VendorAlias((VendorNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class VendorListAlias : ListResult<VendorListAlias, VendorAlias>, IAliasListResult
	{
		private VendorListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private VendorListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private VendorListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class VendorJaggedListAlias : ListResult<VendorJaggedListAlias, VendorListAlias>, IAliasJaggedListResult
	{
		private VendorJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private VendorJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private VendorJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
