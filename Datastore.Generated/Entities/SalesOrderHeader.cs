using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Domain.Data.Query;

namespace Domain.Data.Manipulation
{
	public interface ISalesOrderHeaderOriginalData : ISchemaBaseOriginalData
    {
		string RevisionNumber { get; }
		System.DateTime OrderDate { get; }
		System.DateTime DueDate { get; }
		System.DateTime? ShipDate { get; }
		string Status { get; }
		string OnlineOrderFlag { get; }
		string SalesOrderNumber { get; }
		string PurchaseOrderNumber { get; }
		string AccountNumber { get; }
		int? CreditCardID { get; }
		string CreditCardApprovalCode { get; }
		int? CurrencyRateID { get; }
		string SubTotal { get; }
		string TaxAmt { get; }
		string Freight { get; }
		string TotalDue { get; }
		string Comment { get; }
		string rowguid { get; }
		CurrencyRate CurrencyRate { get; }
		CreditCard CreditCard { get; }
		Address Address { get; }
		ShipMethod ShipMethod { get; }
		IEnumerable<SalesTerritory> SalesTerritories { get; }
		SalesReason SalesReason { get; }
    }

	public partial class SalesOrderHeader : OGM<SalesOrderHeader, SalesOrderHeader.SalesOrderHeaderData, System.String>, ISchemaBase, INeo4jBase, ISalesOrderHeaderOriginalData
	{
        #region Initialize

        static SalesOrderHeader()
        {
            Register.Types();
        }

        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadByKeys
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

			AdditionalGeneratedStoredQueries();
        }
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, SalesOrderHeader> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.SalesOrderHeaderAlias, IWhereQuery> query)
        {
            q.SalesOrderHeaderAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.SalesOrderHeader.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"SalesOrderHeader => RevisionNumber : {this.RevisionNumber}, OrderDate : {this.OrderDate}, DueDate : {this.DueDate}, ShipDate : {this.ShipDate?.ToString() ?? "null"}, Status : {this.Status}, OnlineOrderFlag : {this.OnlineOrderFlag}, SalesOrderNumber : {this.SalesOrderNumber}, PurchaseOrderNumber : {this.PurchaseOrderNumber?.ToString() ?? "null"}, AccountNumber : {this.AccountNumber?.ToString() ?? "null"}, CreditCardID : {this.CreditCardID?.ToString() ?? "null"}, CreditCardApprovalCode : {this.CreditCardApprovalCode?.ToString() ?? "null"}, CurrencyRateID : {this.CurrencyRateID?.ToString() ?? "null"}, SubTotal : {this.SubTotal}, TaxAmt : {this.TaxAmt}, Freight : {this.Freight}, TotalDue : {this.TotalDue}, Comment : {this.Comment?.ToString() ?? "null"}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

		protected override void LazySet()
        {
            base.LazySet();
            if (PersistenceState == PersistenceState.NewAndChanged || PersistenceState == PersistenceState.LoadedAndChanged)
            {
                if ((object)InnerData == (object)OriginalData)
                    OriginalData = new SalesOrderHeaderData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.RevisionNumber == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderHeader with key '{0}' because the RevisionNumber cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.OrderDate == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderHeader with key '{0}' because the OrderDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.DueDate == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderHeader with key '{0}' because the DueDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Status == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderHeader with key '{0}' because the Status cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.OnlineOrderFlag == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderHeader with key '{0}' because the OnlineOrderFlag cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.SalesOrderNumber == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderHeader with key '{0}' because the SalesOrderNumber cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.SubTotal == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderHeader with key '{0}' because the SubTotal cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.TaxAmt == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderHeader with key '{0}' because the TaxAmt cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Freight == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderHeader with key '{0}' because the Freight cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.TotalDue == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderHeader with key '{0}' because the TotalDue cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderHeader with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save SalesOrderHeader with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class SalesOrderHeaderData : Data<System.String>
		{
			public SalesOrderHeaderData()
            {

            }

            public SalesOrderHeaderData(SalesOrderHeaderData data)
            {
				RevisionNumber = data.RevisionNumber;
				OrderDate = data.OrderDate;
				DueDate = data.DueDate;
				ShipDate = data.ShipDate;
				Status = data.Status;
				OnlineOrderFlag = data.OnlineOrderFlag;
				SalesOrderNumber = data.SalesOrderNumber;
				PurchaseOrderNumber = data.PurchaseOrderNumber;
				AccountNumber = data.AccountNumber;
				CreditCardID = data.CreditCardID;
				CreditCardApprovalCode = data.CreditCardApprovalCode;
				CurrencyRateID = data.CurrencyRateID;
				SubTotal = data.SubTotal;
				TaxAmt = data.TaxAmt;
				Freight = data.Freight;
				TotalDue = data.TotalDue;
				Comment = data.Comment;
				rowguid = data.rowguid;
				CurrencyRate = data.CurrencyRate;
				CreditCard = data.CreditCard;
				Address = data.Address;
				ShipMethod = data.ShipMethod;
				SalesTerritories = data.SalesTerritories;
				SalesReason = data.SalesReason;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "SalesOrderHeader";

				CurrencyRate = new EntityCollection<CurrencyRate>(Wrapper, Members.CurrencyRate);
				CreditCard = new EntityCollection<CreditCard>(Wrapper, Members.CreditCard);
				Address = new EntityCollection<Address>(Wrapper, Members.Address);
				ShipMethod = new EntityCollection<ShipMethod>(Wrapper, Members.ShipMethod);
				SalesTerritories = new EntityCollection<SalesTerritory>(Wrapper, Members.SalesTerritories);
				SalesReason = new EntityCollection<SalesReason>(Wrapper, Members.SalesReason);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("RevisionNumber",  RevisionNumber);
				dictionary.Add("OrderDate",  Conversion<System.DateTime, long>.Convert(OrderDate));
				dictionary.Add("DueDate",  Conversion<System.DateTime, long>.Convert(DueDate));
				dictionary.Add("ShipDate",  Conversion<System.DateTime?, long?>.Convert(ShipDate));
				dictionary.Add("Status",  Status);
				dictionary.Add("OnlineOrderFlag",  OnlineOrderFlag);
				dictionary.Add("SalesOrderNumber",  SalesOrderNumber);
				dictionary.Add("PurchaseOrderNumber",  PurchaseOrderNumber);
				dictionary.Add("AccountNumber",  AccountNumber);
				dictionary.Add("CreditCardID",  Conversion<int?, long?>.Convert(CreditCardID));
				dictionary.Add("CreditCardApprovalCode",  CreditCardApprovalCode);
				dictionary.Add("CurrencyRateID",  Conversion<int?, long?>.Convert(CurrencyRateID));
				dictionary.Add("SubTotal",  SubTotal);
				dictionary.Add("TaxAmt",  TaxAmt);
				dictionary.Add("Freight",  Freight);
				dictionary.Add("TotalDue",  TotalDue);
				dictionary.Add("Comment",  Comment);
				dictionary.Add("rowguid",  rowguid);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("RevisionNumber", out value))
					RevisionNumber = (string)value;
				if (properties.TryGetValue("OrderDate", out value))
					OrderDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("DueDate", out value))
					DueDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("ShipDate", out value))
					ShipDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Status", out value))
					Status = (string)value;
				if (properties.TryGetValue("OnlineOrderFlag", out value))
					OnlineOrderFlag = (string)value;
				if (properties.TryGetValue("SalesOrderNumber", out value))
					SalesOrderNumber = (string)value;
				if (properties.TryGetValue("PurchaseOrderNumber", out value))
					PurchaseOrderNumber = (string)value;
				if (properties.TryGetValue("AccountNumber", out value))
					AccountNumber = (string)value;
				if (properties.TryGetValue("CreditCardID", out value))
					CreditCardID = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("CreditCardApprovalCode", out value))
					CreditCardApprovalCode = (string)value;
				if (properties.TryGetValue("CurrencyRateID", out value))
					CurrencyRateID = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("SubTotal", out value))
					SubTotal = (string)value;
				if (properties.TryGetValue("TaxAmt", out value))
					TaxAmt = (string)value;
				if (properties.TryGetValue("Freight", out value))
					Freight = (string)value;
				if (properties.TryGetValue("TotalDue", out value))
					TotalDue = (string)value;
				if (properties.TryGetValue("Comment", out value))
					Comment = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface ISalesOrderHeader

			public string RevisionNumber { get; set; }
			public System.DateTime OrderDate { get; set; }
			public System.DateTime DueDate { get; set; }
			public System.DateTime? ShipDate { get; set; }
			public string Status { get; set; }
			public string OnlineOrderFlag { get; set; }
			public string SalesOrderNumber { get; set; }
			public string PurchaseOrderNumber { get; set; }
			public string AccountNumber { get; set; }
			public int? CreditCardID { get; set; }
			public string CreditCardApprovalCode { get; set; }
			public int? CurrencyRateID { get; set; }
			public string SubTotal { get; set; }
			public string TaxAmt { get; set; }
			public string Freight { get; set; }
			public string TotalDue { get; set; }
			public string Comment { get; set; }
			public string rowguid { get; set; }
			public EntityCollection<CurrencyRate> CurrencyRate { get; private set; }
			public EntityCollection<CreditCard> CreditCard { get; private set; }
			public EntityCollection<Address> Address { get; private set; }
			public EntityCollection<ShipMethod> ShipMethod { get; private set; }
			public EntityCollection<SalesTerritory> SalesTerritories { get; private set; }
			public EntityCollection<SalesReason> SalesReason { get; private set; }

			#endregion
			#region Members for interface ISchemaBase

			public System.DateTime ModifiedDate { get; set; }

			#endregion
			#region Members for interface INeo4jBase

			public string Uid { get; set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface ISalesOrderHeader

		public string RevisionNumber { get { LazyGet(); return InnerData.RevisionNumber; } set { if (LazySet(Members.RevisionNumber, InnerData.RevisionNumber, value)) InnerData.RevisionNumber = value; } }
		public System.DateTime OrderDate { get { LazyGet(); return InnerData.OrderDate; } set { if (LazySet(Members.OrderDate, InnerData.OrderDate, value)) InnerData.OrderDate = value; } }
		public System.DateTime DueDate { get { LazyGet(); return InnerData.DueDate; } set { if (LazySet(Members.DueDate, InnerData.DueDate, value)) InnerData.DueDate = value; } }
		public System.DateTime? ShipDate { get { LazyGet(); return InnerData.ShipDate; } set { if (LazySet(Members.ShipDate, InnerData.ShipDate, value)) InnerData.ShipDate = value; } }
		public string Status { get { LazyGet(); return InnerData.Status; } set { if (LazySet(Members.Status, InnerData.Status, value)) InnerData.Status = value; } }
		public string OnlineOrderFlag { get { LazyGet(); return InnerData.OnlineOrderFlag; } set { if (LazySet(Members.OnlineOrderFlag, InnerData.OnlineOrderFlag, value)) InnerData.OnlineOrderFlag = value; } }
		public string SalesOrderNumber { get { LazyGet(); return InnerData.SalesOrderNumber; } set { if (LazySet(Members.SalesOrderNumber, InnerData.SalesOrderNumber, value)) InnerData.SalesOrderNumber = value; } }
		public string PurchaseOrderNumber { get { LazyGet(); return InnerData.PurchaseOrderNumber; } set { if (LazySet(Members.PurchaseOrderNumber, InnerData.PurchaseOrderNumber, value)) InnerData.PurchaseOrderNumber = value; } }
		public string AccountNumber { get { LazyGet(); return InnerData.AccountNumber; } set { if (LazySet(Members.AccountNumber, InnerData.AccountNumber, value)) InnerData.AccountNumber = value; } }
		public int? CreditCardID { get { LazyGet(); return InnerData.CreditCardID; } set { if (LazySet(Members.CreditCardID, InnerData.CreditCardID, value)) InnerData.CreditCardID = value; } }
		public string CreditCardApprovalCode { get { LazyGet(); return InnerData.CreditCardApprovalCode; } set { if (LazySet(Members.CreditCardApprovalCode, InnerData.CreditCardApprovalCode, value)) InnerData.CreditCardApprovalCode = value; } }
		public int? CurrencyRateID { get { LazyGet(); return InnerData.CurrencyRateID; } set { if (LazySet(Members.CurrencyRateID, InnerData.CurrencyRateID, value)) InnerData.CurrencyRateID = value; } }
		public string SubTotal { get { LazyGet(); return InnerData.SubTotal; } set { if (LazySet(Members.SubTotal, InnerData.SubTotal, value)) InnerData.SubTotal = value; } }
		public string TaxAmt { get { LazyGet(); return InnerData.TaxAmt; } set { if (LazySet(Members.TaxAmt, InnerData.TaxAmt, value)) InnerData.TaxAmt = value; } }
		public string Freight { get { LazyGet(); return InnerData.Freight; } set { if (LazySet(Members.Freight, InnerData.Freight, value)) InnerData.Freight = value; } }
		public string TotalDue { get { LazyGet(); return InnerData.TotalDue; } set { if (LazySet(Members.TotalDue, InnerData.TotalDue, value)) InnerData.TotalDue = value; } }
		public string Comment { get { LazyGet(); return InnerData.Comment; } set { if (LazySet(Members.Comment, InnerData.Comment, value)) InnerData.Comment = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }
		public CurrencyRate CurrencyRate
		{
			get { return ((ILookupHelper<CurrencyRate>)InnerData.CurrencyRate).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.CurrencyRate, ((ILookupHelper<CurrencyRate>)InnerData.CurrencyRate).GetItem(null), value))
					((ILookupHelper<CurrencyRate>)InnerData.CurrencyRate).SetItem(value, null); 
			}
		}
		public CreditCard CreditCard
		{
			get { return ((ILookupHelper<CreditCard>)InnerData.CreditCard).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.CreditCard, ((ILookupHelper<CreditCard>)InnerData.CreditCard).GetItem(null), value))
					((ILookupHelper<CreditCard>)InnerData.CreditCard).SetItem(value, null); 
			}
		}
		public Address Address
		{
			get { return ((ILookupHelper<Address>)InnerData.Address).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Address, ((ILookupHelper<Address>)InnerData.Address).GetItem(null), value))
					((ILookupHelper<Address>)InnerData.Address).SetItem(value, null); 
			}
		}
		public ShipMethod ShipMethod
		{
			get { return ((ILookupHelper<ShipMethod>)InnerData.ShipMethod).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.ShipMethod, ((ILookupHelper<ShipMethod>)InnerData.ShipMethod).GetItem(null), value))
					((ILookupHelper<ShipMethod>)InnerData.ShipMethod).SetItem(value, null); 
			}
		}
		public EntityCollection<SalesTerritory> SalesTerritories { get { return InnerData.SalesTerritories; } }
		public SalesReason SalesReason
		{
			get { return ((ILookupHelper<SalesReason>)InnerData.SalesReason).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.SalesReason, ((ILookupHelper<SalesReason>)InnerData.SalesReason).GetItem(null), value))
					((ILookupHelper<SalesReason>)InnerData.SalesReason).SetItem(value, null); 
			}
		}

		#endregion
		#region Members for interface ISchemaBase

		public System.DateTime ModifiedDate { get { LazyGet(); return InnerData.ModifiedDate; } set { if (LazySet(Members.ModifiedDate, InnerData.ModifiedDate, value)) InnerData.ModifiedDate = value; } }

		#endregion
		#region Members for interface INeo4jBase

		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }

		#endregion

		#region Virtual Node Type
		
		public string NodeType  { get { return InnerData.NodeType; } }
		
		#endregion

		#endregion

		#region Reflection

        private static SalesOrderHeaderMembers members = null;
        public static SalesOrderHeaderMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(SalesOrderHeader))
                    {
                        if (members == null)
                            members = new SalesOrderHeaderMembers();
                    }
                }
                return members;
            }
        }
        public class SalesOrderHeaderMembers
        {
            internal SalesOrderHeaderMembers() { }

			#region Members for interface ISalesOrderHeader

            public Property RevisionNumber { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["RevisionNumber"];
            public Property OrderDate { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["OrderDate"];
            public Property DueDate { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["DueDate"];
            public Property ShipDate { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["ShipDate"];
            public Property Status { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["Status"];
            public Property OnlineOrderFlag { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["OnlineOrderFlag"];
            public Property SalesOrderNumber { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["SalesOrderNumber"];
            public Property PurchaseOrderNumber { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["PurchaseOrderNumber"];
            public Property AccountNumber { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["AccountNumber"];
            public Property CreditCardID { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["CreditCardID"];
            public Property CreditCardApprovalCode { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["CreditCardApprovalCode"];
            public Property CurrencyRateID { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["CurrencyRateID"];
            public Property SubTotal { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["SubTotal"];
            public Property TaxAmt { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["TaxAmt"];
            public Property Freight { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["Freight"];
            public Property TotalDue { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["TotalDue"];
            public Property Comment { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["Comment"];
            public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["rowguid"];
            public Property CurrencyRate { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["CurrencyRate"];
            public Property CreditCard { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["CreditCard"];
            public Property Address { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["Address"];
            public Property ShipMethod { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["ShipMethod"];
            public Property SalesTerritories { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["SalesTerritories"];
            public Property SalesReason { get; } = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["SalesReason"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static SalesOrderHeaderFullTextMembers fullTextMembers = null;
        public static SalesOrderHeaderFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(SalesOrderHeader))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new SalesOrderHeaderFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class SalesOrderHeaderFullTextMembers
        {
            internal SalesOrderHeaderFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(SalesOrderHeader))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"];
                }
            }
            return entity;
        }

		private static SalesOrderHeaderEvents events = null;
        public static SalesOrderHeaderEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(SalesOrderHeader))
                    {
                        if (events == null)
                            events = new SalesOrderHeaderEvents();
                    }
                }
                return events;
            }
        }
        public class SalesOrderHeaderEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<SalesOrderHeader, EntityEventArgs> onNew;
            public event EventHandler<SalesOrderHeader, EntityEventArgs> OnNew
            {
                add
                {
                    lock (this)
                    {
                        if (!onNewIsRegistered)
                        {
                            Entity.Events.OnNew -= onNewProxy;
                            Entity.Events.OnNew += onNewProxy;
                            onNewIsRegistered = true;
                        }
                        onNew += value;
                    }
                }
                remove
                {
                    lock (this)
                    {
                        onNew -= value;
                        if (onNew == null && onNewIsRegistered)
                        {
                            Entity.Events.OnNew -= onNewProxy;
                            onNewIsRegistered = false;
                        }
                    }
                }
            }
            
			private void onNewProxy(object sender, EntityEventArgs args)
            {
                EventHandler<SalesOrderHeader, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((SalesOrderHeader)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<SalesOrderHeader, EntityEventArgs> onDelete;
            public event EventHandler<SalesOrderHeader, EntityEventArgs> OnDelete
            {
                add
                {
                    lock (this)
                    {
                        if (!onDeleteIsRegistered)
                        {
                            Entity.Events.OnDelete -= onDeleteProxy;
                            Entity.Events.OnDelete += onDeleteProxy;
                            onDeleteIsRegistered = true;
                        }
                        onDelete += value;
                    }
                }
                remove
                {
                    lock (this)
                    {
                        onDelete -= value;
                        if (onDelete == null && onDeleteIsRegistered)
                        {
                            Entity.Events.OnDelete -= onDeleteProxy;
                            onDeleteIsRegistered = false;
                        }
                    }
                }
            }
            
			private void onDeleteProxy(object sender, EntityEventArgs args)
            {
                EventHandler<SalesOrderHeader, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((SalesOrderHeader)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<SalesOrderHeader, EntityEventArgs> onSave;
            public event EventHandler<SalesOrderHeader, EntityEventArgs> OnSave
            {
                add
                {
                    lock (this)
                    {
                        if (!onSaveIsRegistered)
                        {
                            Entity.Events.OnSave -= onSaveProxy;
                            Entity.Events.OnSave += onSaveProxy;
                            onSaveIsRegistered = true;
                        }
                        onSave += value;
                    }
                }
                remove
                {
                    lock (this)
                    {
                        onSave -= value;
                        if (onSave == null && onSaveIsRegistered)
                        {
                            Entity.Events.OnSave -= onSaveProxy;
                            onSaveIsRegistered = false;
                        }
                    }
                }
            }
            
			private void onSaveProxy(object sender, EntityEventArgs args)
            {
                EventHandler<SalesOrderHeader, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((SalesOrderHeader)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnRevisionNumber

				private static bool onRevisionNumberIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onRevisionNumber;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnRevisionNumber
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onRevisionNumberIsRegistered)
							{
								Members.RevisionNumber.Events.OnChange -= onRevisionNumberProxy;
								Members.RevisionNumber.Events.OnChange += onRevisionNumberProxy;
								onRevisionNumberIsRegistered = true;
							}
							onRevisionNumber += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onRevisionNumber -= value;
							if (onRevisionNumber == null && onRevisionNumberIsRegistered)
							{
								Members.RevisionNumber.Events.OnChange -= onRevisionNumberProxy;
								onRevisionNumberIsRegistered = false;
							}
						}
					}
				}
            
				private static void onRevisionNumberProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onRevisionNumber;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnOrderDate

				private static bool onOrderDateIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onOrderDate;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnOrderDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onOrderDateIsRegistered)
							{
								Members.OrderDate.Events.OnChange -= onOrderDateProxy;
								Members.OrderDate.Events.OnChange += onOrderDateProxy;
								onOrderDateIsRegistered = true;
							}
							onOrderDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onOrderDate -= value;
							if (onOrderDate == null && onOrderDateIsRegistered)
							{
								Members.OrderDate.Events.OnChange -= onOrderDateProxy;
								onOrderDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onOrderDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onOrderDate;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnDueDate

				private static bool onDueDateIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onDueDate;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnDueDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDueDateIsRegistered)
							{
								Members.DueDate.Events.OnChange -= onDueDateProxy;
								Members.DueDate.Events.OnChange += onDueDateProxy;
								onDueDateIsRegistered = true;
							}
							onDueDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDueDate -= value;
							if (onDueDate == null && onDueDateIsRegistered)
							{
								Members.DueDate.Events.OnChange -= onDueDateProxy;
								onDueDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDueDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onDueDate;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnShipDate

				private static bool onShipDateIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onShipDate;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnShipDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onShipDateIsRegistered)
							{
								Members.ShipDate.Events.OnChange -= onShipDateProxy;
								Members.ShipDate.Events.OnChange += onShipDateProxy;
								onShipDateIsRegistered = true;
							}
							onShipDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onShipDate -= value;
							if (onShipDate == null && onShipDateIsRegistered)
							{
								Members.ShipDate.Events.OnChange -= onShipDateProxy;
								onShipDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onShipDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onShipDate;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnStatus

				private static bool onStatusIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onStatus;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnStatus
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onStatusIsRegistered)
							{
								Members.Status.Events.OnChange -= onStatusProxy;
								Members.Status.Events.OnChange += onStatusProxy;
								onStatusIsRegistered = true;
							}
							onStatus += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onStatus -= value;
							if (onStatus == null && onStatusIsRegistered)
							{
								Members.Status.Events.OnChange -= onStatusProxy;
								onStatusIsRegistered = false;
							}
						}
					}
				}
            
				private static void onStatusProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onStatus;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnOnlineOrderFlag

				private static bool onOnlineOrderFlagIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onOnlineOrderFlag;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnOnlineOrderFlag
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onOnlineOrderFlagIsRegistered)
							{
								Members.OnlineOrderFlag.Events.OnChange -= onOnlineOrderFlagProxy;
								Members.OnlineOrderFlag.Events.OnChange += onOnlineOrderFlagProxy;
								onOnlineOrderFlagIsRegistered = true;
							}
							onOnlineOrderFlag += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onOnlineOrderFlag -= value;
							if (onOnlineOrderFlag == null && onOnlineOrderFlagIsRegistered)
							{
								Members.OnlineOrderFlag.Events.OnChange -= onOnlineOrderFlagProxy;
								onOnlineOrderFlagIsRegistered = false;
							}
						}
					}
				}
            
				private static void onOnlineOrderFlagProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onOnlineOrderFlag;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnSalesOrderNumber

				private static bool onSalesOrderNumberIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onSalesOrderNumber;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnSalesOrderNumber
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalesOrderNumberIsRegistered)
							{
								Members.SalesOrderNumber.Events.OnChange -= onSalesOrderNumberProxy;
								Members.SalesOrderNumber.Events.OnChange += onSalesOrderNumberProxy;
								onSalesOrderNumberIsRegistered = true;
							}
							onSalesOrderNumber += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalesOrderNumber -= value;
							if (onSalesOrderNumber == null && onSalesOrderNumberIsRegistered)
							{
								Members.SalesOrderNumber.Events.OnChange -= onSalesOrderNumberProxy;
								onSalesOrderNumberIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSalesOrderNumberProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onSalesOrderNumber;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnPurchaseOrderNumber

				private static bool onPurchaseOrderNumberIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onPurchaseOrderNumber;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnPurchaseOrderNumber
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPurchaseOrderNumberIsRegistered)
							{
								Members.PurchaseOrderNumber.Events.OnChange -= onPurchaseOrderNumberProxy;
								Members.PurchaseOrderNumber.Events.OnChange += onPurchaseOrderNumberProxy;
								onPurchaseOrderNumberIsRegistered = true;
							}
							onPurchaseOrderNumber += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPurchaseOrderNumber -= value;
							if (onPurchaseOrderNumber == null && onPurchaseOrderNumberIsRegistered)
							{
								Members.PurchaseOrderNumber.Events.OnChange -= onPurchaseOrderNumberProxy;
								onPurchaseOrderNumberIsRegistered = false;
							}
						}
					}
				}
            
				private static void onPurchaseOrderNumberProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onPurchaseOrderNumber;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnAccountNumber

				private static bool onAccountNumberIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onAccountNumber;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnAccountNumber
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onAccountNumberIsRegistered)
							{
								Members.AccountNumber.Events.OnChange -= onAccountNumberProxy;
								Members.AccountNumber.Events.OnChange += onAccountNumberProxy;
								onAccountNumberIsRegistered = true;
							}
							onAccountNumber += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onAccountNumber -= value;
							if (onAccountNumber == null && onAccountNumberIsRegistered)
							{
								Members.AccountNumber.Events.OnChange -= onAccountNumberProxy;
								onAccountNumberIsRegistered = false;
							}
						}
					}
				}
            
				private static void onAccountNumberProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onAccountNumber;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnCreditCardID

				private static bool onCreditCardIDIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onCreditCardID;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnCreditCardID
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCreditCardIDIsRegistered)
							{
								Members.CreditCardID.Events.OnChange -= onCreditCardIDProxy;
								Members.CreditCardID.Events.OnChange += onCreditCardIDProxy;
								onCreditCardIDIsRegistered = true;
							}
							onCreditCardID += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCreditCardID -= value;
							if (onCreditCardID == null && onCreditCardIDIsRegistered)
							{
								Members.CreditCardID.Events.OnChange -= onCreditCardIDProxy;
								onCreditCardIDIsRegistered = false;
							}
						}
					}
				}
            
				private static void onCreditCardIDProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onCreditCardID;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnCreditCardApprovalCode

				private static bool onCreditCardApprovalCodeIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onCreditCardApprovalCode;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnCreditCardApprovalCode
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCreditCardApprovalCodeIsRegistered)
							{
								Members.CreditCardApprovalCode.Events.OnChange -= onCreditCardApprovalCodeProxy;
								Members.CreditCardApprovalCode.Events.OnChange += onCreditCardApprovalCodeProxy;
								onCreditCardApprovalCodeIsRegistered = true;
							}
							onCreditCardApprovalCode += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCreditCardApprovalCode -= value;
							if (onCreditCardApprovalCode == null && onCreditCardApprovalCodeIsRegistered)
							{
								Members.CreditCardApprovalCode.Events.OnChange -= onCreditCardApprovalCodeProxy;
								onCreditCardApprovalCodeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onCreditCardApprovalCodeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onCreditCardApprovalCode;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnCurrencyRateID

				private static bool onCurrencyRateIDIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onCurrencyRateID;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnCurrencyRateID
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCurrencyRateIDIsRegistered)
							{
								Members.CurrencyRateID.Events.OnChange -= onCurrencyRateIDProxy;
								Members.CurrencyRateID.Events.OnChange += onCurrencyRateIDProxy;
								onCurrencyRateIDIsRegistered = true;
							}
							onCurrencyRateID += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCurrencyRateID -= value;
							if (onCurrencyRateID == null && onCurrencyRateIDIsRegistered)
							{
								Members.CurrencyRateID.Events.OnChange -= onCurrencyRateIDProxy;
								onCurrencyRateIDIsRegistered = false;
							}
						}
					}
				}
            
				private static void onCurrencyRateIDProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onCurrencyRateID;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnSubTotal

				private static bool onSubTotalIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onSubTotal;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnSubTotal
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSubTotalIsRegistered)
							{
								Members.SubTotal.Events.OnChange -= onSubTotalProxy;
								Members.SubTotal.Events.OnChange += onSubTotalProxy;
								onSubTotalIsRegistered = true;
							}
							onSubTotal += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSubTotal -= value;
							if (onSubTotal == null && onSubTotalIsRegistered)
							{
								Members.SubTotal.Events.OnChange -= onSubTotalProxy;
								onSubTotalIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSubTotalProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onSubTotal;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnTaxAmt

				private static bool onTaxAmtIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onTaxAmt;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnTaxAmt
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onTaxAmtIsRegistered)
							{
								Members.TaxAmt.Events.OnChange -= onTaxAmtProxy;
								Members.TaxAmt.Events.OnChange += onTaxAmtProxy;
								onTaxAmtIsRegistered = true;
							}
							onTaxAmt += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onTaxAmt -= value;
							if (onTaxAmt == null && onTaxAmtIsRegistered)
							{
								Members.TaxAmt.Events.OnChange -= onTaxAmtProxy;
								onTaxAmtIsRegistered = false;
							}
						}
					}
				}
            
				private static void onTaxAmtProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onTaxAmt;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnFreight

				private static bool onFreightIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onFreight;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnFreight
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onFreightIsRegistered)
							{
								Members.Freight.Events.OnChange -= onFreightProxy;
								Members.Freight.Events.OnChange += onFreightProxy;
								onFreightIsRegistered = true;
							}
							onFreight += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onFreight -= value;
							if (onFreight == null && onFreightIsRegistered)
							{
								Members.Freight.Events.OnChange -= onFreightProxy;
								onFreightIsRegistered = false;
							}
						}
					}
				}
            
				private static void onFreightProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onFreight;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnTotalDue

				private static bool onTotalDueIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onTotalDue;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnTotalDue
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onTotalDueIsRegistered)
							{
								Members.TotalDue.Events.OnChange -= onTotalDueProxy;
								Members.TotalDue.Events.OnChange += onTotalDueProxy;
								onTotalDueIsRegistered = true;
							}
							onTotalDue += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onTotalDue -= value;
							if (onTotalDue == null && onTotalDueIsRegistered)
							{
								Members.TotalDue.Events.OnChange -= onTotalDueProxy;
								onTotalDueIsRegistered = false;
							}
						}
					}
				}
            
				private static void onTotalDueProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onTotalDue;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnComment

				private static bool onCommentIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onComment;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnComment
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCommentIsRegistered)
							{
								Members.Comment.Events.OnChange -= onCommentProxy;
								Members.Comment.Events.OnChange += onCommentProxy;
								onCommentIsRegistered = true;
							}
							onComment += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onComment -= value;
							if (onComment == null && onCommentIsRegistered)
							{
								Members.Comment.Events.OnChange -= onCommentProxy;
								onCommentIsRegistered = false;
							}
						}
					}
				}
            
				private static void onCommentProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onComment;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onrowguid;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> Onrowguid
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onrowguidIsRegistered)
							{
								Members.rowguid.Events.OnChange -= onrowguidProxy;
								Members.rowguid.Events.OnChange += onrowguidProxy;
								onrowguidIsRegistered = true;
							}
							onrowguid += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onrowguid -= value;
							if (onrowguid == null && onrowguidIsRegistered)
							{
								Members.rowguid.Events.OnChange -= onrowguidProxy;
								onrowguidIsRegistered = false;
							}
						}
					}
				}
            
				private static void onrowguidProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onrowguid;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnCurrencyRate

				private static bool onCurrencyRateIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onCurrencyRate;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnCurrencyRate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCurrencyRateIsRegistered)
							{
								Members.CurrencyRate.Events.OnChange -= onCurrencyRateProxy;
								Members.CurrencyRate.Events.OnChange += onCurrencyRateProxy;
								onCurrencyRateIsRegistered = true;
							}
							onCurrencyRate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCurrencyRate -= value;
							if (onCurrencyRate == null && onCurrencyRateIsRegistered)
							{
								Members.CurrencyRate.Events.OnChange -= onCurrencyRateProxy;
								onCurrencyRateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onCurrencyRateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onCurrencyRate;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnCreditCard

				private static bool onCreditCardIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onCreditCard;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnCreditCard
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCreditCardIsRegistered)
							{
								Members.CreditCard.Events.OnChange -= onCreditCardProxy;
								Members.CreditCard.Events.OnChange += onCreditCardProxy;
								onCreditCardIsRegistered = true;
							}
							onCreditCard += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCreditCard -= value;
							if (onCreditCard == null && onCreditCardIsRegistered)
							{
								Members.CreditCard.Events.OnChange -= onCreditCardProxy;
								onCreditCardIsRegistered = false;
							}
						}
					}
				}
            
				private static void onCreditCardProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onCreditCard;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnAddress

				private static bool onAddressIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onAddress;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnAddress
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onAddressIsRegistered)
							{
								Members.Address.Events.OnChange -= onAddressProxy;
								Members.Address.Events.OnChange += onAddressProxy;
								onAddressIsRegistered = true;
							}
							onAddress += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onAddress -= value;
							if (onAddress == null && onAddressIsRegistered)
							{
								Members.Address.Events.OnChange -= onAddressProxy;
								onAddressIsRegistered = false;
							}
						}
					}
				}
            
				private static void onAddressProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onAddress;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnShipMethod

				private static bool onShipMethodIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onShipMethod;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnShipMethod
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onShipMethodIsRegistered)
							{
								Members.ShipMethod.Events.OnChange -= onShipMethodProxy;
								Members.ShipMethod.Events.OnChange += onShipMethodProxy;
								onShipMethodIsRegistered = true;
							}
							onShipMethod += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onShipMethod -= value;
							if (onShipMethod == null && onShipMethodIsRegistered)
							{
								Members.ShipMethod.Events.OnChange -= onShipMethodProxy;
								onShipMethodIsRegistered = false;
							}
						}
					}
				}
            
				private static void onShipMethodProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onShipMethod;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnSalesTerritories

				private static bool onSalesTerritoriesIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onSalesTerritories;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnSalesTerritories
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalesTerritoriesIsRegistered)
							{
								Members.SalesTerritories.Events.OnChange -= onSalesTerritoriesProxy;
								Members.SalesTerritories.Events.OnChange += onSalesTerritoriesProxy;
								onSalesTerritoriesIsRegistered = true;
							}
							onSalesTerritories += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalesTerritories -= value;
							if (onSalesTerritories == null && onSalesTerritoriesIsRegistered)
							{
								Members.SalesTerritories.Events.OnChange -= onSalesTerritoriesProxy;
								onSalesTerritoriesIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSalesTerritoriesProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onSalesTerritories;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnSalesReason

				private static bool onSalesReasonIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onSalesReason;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnSalesReason
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalesReasonIsRegistered)
							{
								Members.SalesReason.Events.OnChange -= onSalesReasonProxy;
								Members.SalesReason.Events.OnChange += onSalesReasonProxy;
								onSalesReasonIsRegistered = true;
							}
							onSalesReason += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalesReason -= value;
							if (onSalesReason == null && onSalesReasonIsRegistered)
							{
								Members.SalesReason.Events.OnChange -= onSalesReasonProxy;
								onSalesReasonIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSalesReasonProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onSalesReason;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnModifiedDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onModifiedDateIsRegistered)
							{
								Members.ModifiedDate.Events.OnChange -= onModifiedDateProxy;
								Members.ModifiedDate.Events.OnChange += onModifiedDateProxy;
								onModifiedDateIsRegistered = true;
							}
							onModifiedDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onModifiedDate -= value;
							if (onModifiedDate == null && onModifiedDateIsRegistered)
							{
								Members.ModifiedDate.Events.OnChange -= onModifiedDateProxy;
								onModifiedDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onModifiedDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<SalesOrderHeader, PropertyEventArgs> onUid;
				public static event EventHandler<SalesOrderHeader, PropertyEventArgs> OnUid
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onUidIsRegistered)
							{
								Members.Uid.Events.OnChange -= onUidProxy;
								Members.Uid.Events.OnChange += onUidProxy;
								onUidIsRegistered = true;
							}
							onUid += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onUid -= value;
							if (onUid == null && onUidIsRegistered)
							{
								Members.Uid.Events.OnChange -= onUidProxy;
								onUidIsRegistered = false;
							}
						}
					}
				}
            
				private static void onUidProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesOrderHeader, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((SalesOrderHeader)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region ISalesOrderHeaderOriginalData

		public ISalesOrderHeaderOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ISalesOrderHeader

		string ISalesOrderHeaderOriginalData.RevisionNumber { get { return OriginalData.RevisionNumber; } }
		System.DateTime ISalesOrderHeaderOriginalData.OrderDate { get { return OriginalData.OrderDate; } }
		System.DateTime ISalesOrderHeaderOriginalData.DueDate { get { return OriginalData.DueDate; } }
		System.DateTime? ISalesOrderHeaderOriginalData.ShipDate { get { return OriginalData.ShipDate; } }
		string ISalesOrderHeaderOriginalData.Status { get { return OriginalData.Status; } }
		string ISalesOrderHeaderOriginalData.OnlineOrderFlag { get { return OriginalData.OnlineOrderFlag; } }
		string ISalesOrderHeaderOriginalData.SalesOrderNumber { get { return OriginalData.SalesOrderNumber; } }
		string ISalesOrderHeaderOriginalData.PurchaseOrderNumber { get { return OriginalData.PurchaseOrderNumber; } }
		string ISalesOrderHeaderOriginalData.AccountNumber { get { return OriginalData.AccountNumber; } }
		int? ISalesOrderHeaderOriginalData.CreditCardID { get { return OriginalData.CreditCardID; } }
		string ISalesOrderHeaderOriginalData.CreditCardApprovalCode { get { return OriginalData.CreditCardApprovalCode; } }
		int? ISalesOrderHeaderOriginalData.CurrencyRateID { get { return OriginalData.CurrencyRateID; } }
		string ISalesOrderHeaderOriginalData.SubTotal { get { return OriginalData.SubTotal; } }
		string ISalesOrderHeaderOriginalData.TaxAmt { get { return OriginalData.TaxAmt; } }
		string ISalesOrderHeaderOriginalData.Freight { get { return OriginalData.Freight; } }
		string ISalesOrderHeaderOriginalData.TotalDue { get { return OriginalData.TotalDue; } }
		string ISalesOrderHeaderOriginalData.Comment { get { return OriginalData.Comment; } }
		string ISalesOrderHeaderOriginalData.rowguid { get { return OriginalData.rowguid; } }
		CurrencyRate ISalesOrderHeaderOriginalData.CurrencyRate { get { return ((ILookupHelper<CurrencyRate>)OriginalData.CurrencyRate).GetOriginalItem(null); } }
		CreditCard ISalesOrderHeaderOriginalData.CreditCard { get { return ((ILookupHelper<CreditCard>)OriginalData.CreditCard).GetOriginalItem(null); } }
		Address ISalesOrderHeaderOriginalData.Address { get { return ((ILookupHelper<Address>)OriginalData.Address).GetOriginalItem(null); } }
		ShipMethod ISalesOrderHeaderOriginalData.ShipMethod { get { return ((ILookupHelper<ShipMethod>)OriginalData.ShipMethod).GetOriginalItem(null); } }
		IEnumerable<SalesTerritory> ISalesOrderHeaderOriginalData.SalesTerritories { get { return OriginalData.SalesTerritories.OriginalData; } }
		SalesReason ISalesOrderHeaderOriginalData.SalesReason { get { return ((ILookupHelper<SalesReason>)OriginalData.SalesReason).GetOriginalItem(null); } }

		#endregion
		#region Members for interface ISchemaBase

		ISchemaBaseOriginalData ISchemaBase.OriginalVersion { get { return this; } }

		System.DateTime ISchemaBaseOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		INeo4jBaseOriginalData INeo4jBase.OriginalVersion { get { return this; } }

		string INeo4jBaseOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}