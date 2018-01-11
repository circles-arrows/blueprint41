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
	public interface IPurchaseOrderHeaderOriginalData
    {
		#region Outer Data

		#region Members for interface IPurchaseOrderHeader

		string RevisionNumber { get; }
		string Status { get; }
		System.DateTime OrderDate { get; }
		System.DateTime ShipDate { get; }
		double SubTotal { get; }
		double TaxAmt { get; }
		string Freight { get; }
		double TotalDue { get; }
		Vendor Vendor { get; }
		ShipMethod ShipMethod { get; }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ModifiedDate { get; }

		#endregion
		#region Members for interface INeo4jBase

		string Uid { get; }

		#endregion
		#endregion
    }

	public partial class PurchaseOrderHeader : OGM<PurchaseOrderHeader, PurchaseOrderHeader.PurchaseOrderHeaderData, System.String>, ISchemaBase, INeo4jBase, IPurchaseOrderHeaderOriginalData
	{
        #region Initialize

        static PurchaseOrderHeader()
        {
            Register.Types();
        }

        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadFromNaturalKey
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

			AdditionalGeneratedStoredQueries();
        }
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, PurchaseOrderHeader> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.PurchaseOrderHeaderAlias, IWhereQuery> query)
        {
            q.PurchaseOrderHeaderAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.PurchaseOrderHeader.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"PurchaseOrderHeader => RevisionNumber : {this.RevisionNumber}, Status : {this.Status}, OrderDate : {this.OrderDate}, ShipDate : {this.ShipDate}, SubTotal : {this.SubTotal}, TaxAmt : {this.TaxAmt}, Freight : {this.Freight}, TotalDue : {this.TotalDue}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new PurchaseOrderHeaderData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.RevisionNumber == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderHeader with key '{0}' because the RevisionNumber cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Status == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderHeader with key '{0}' because the Status cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.OrderDate == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderHeader with key '{0}' because the OrderDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ShipDate == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderHeader with key '{0}' because the ShipDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.SubTotal == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderHeader with key '{0}' because the SubTotal cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.TaxAmt == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderHeader with key '{0}' because the TaxAmt cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Freight == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderHeader with key '{0}' because the Freight cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.TotalDue == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderHeader with key '{0}' because the TotalDue cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save PurchaseOrderHeader with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class PurchaseOrderHeaderData : Data<System.String>
		{
			public PurchaseOrderHeaderData()
            {

            }

            public PurchaseOrderHeaderData(PurchaseOrderHeaderData data)
            {
				RevisionNumber = data.RevisionNumber;
				Status = data.Status;
				OrderDate = data.OrderDate;
				ShipDate = data.ShipDate;
				SubTotal = data.SubTotal;
				TaxAmt = data.TaxAmt;
				Freight = data.Freight;
				TotalDue = data.TotalDue;
				Vendor = data.Vendor;
				ShipMethod = data.ShipMethod;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "PurchaseOrderHeader";

				Vendor = new EntityCollection<Vendor>(Wrapper, Members.Vendor);
				ShipMethod = new EntityCollection<ShipMethod>(Wrapper, Members.ShipMethod);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("RevisionNumber",  RevisionNumber);
				dictionary.Add("Status",  Status);
				dictionary.Add("OrderDate",  Conversion<System.DateTime, long>.Convert(OrderDate));
				dictionary.Add("ShipDate",  Conversion<System.DateTime, long>.Convert(ShipDate));
				dictionary.Add("SubTotal",  SubTotal);
				dictionary.Add("TaxAmt",  TaxAmt);
				dictionary.Add("Freight",  Freight);
				dictionary.Add("TotalDue",  TotalDue);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("RevisionNumber", out value))
					RevisionNumber = (string)value;
				if (properties.TryGetValue("Status", out value))
					Status = (string)value;
				if (properties.TryGetValue("OrderDate", out value))
					OrderDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("ShipDate", out value))
					ShipDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("SubTotal", out value))
					SubTotal = (double)value;
				if (properties.TryGetValue("TaxAmt", out value))
					TaxAmt = (double)value;
				if (properties.TryGetValue("Freight", out value))
					Freight = (string)value;
				if (properties.TryGetValue("TotalDue", out value))
					TotalDue = (double)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IPurchaseOrderHeader

			public string RevisionNumber { get; set; }
			public string Status { get; set; }
			public System.DateTime OrderDate { get; set; }
			public System.DateTime ShipDate { get; set; }
			public double SubTotal { get; set; }
			public double TaxAmt { get; set; }
			public string Freight { get; set; }
			public double TotalDue { get; set; }
			public EntityCollection<Vendor> Vendor { get; private set; }
			public EntityCollection<ShipMethod> ShipMethod { get; private set; }

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

		#region Members for interface IPurchaseOrderHeader

		public string RevisionNumber { get { LazyGet(); return InnerData.RevisionNumber; } set { if (LazySet(Members.RevisionNumber, InnerData.RevisionNumber, value)) InnerData.RevisionNumber = value; } }
		public string Status { get { LazyGet(); return InnerData.Status; } set { if (LazySet(Members.Status, InnerData.Status, value)) InnerData.Status = value; } }
		public System.DateTime OrderDate { get { LazyGet(); return InnerData.OrderDate; } set { if (LazySet(Members.OrderDate, InnerData.OrderDate, value)) InnerData.OrderDate = value; } }
		public System.DateTime ShipDate { get { LazyGet(); return InnerData.ShipDate; } set { if (LazySet(Members.ShipDate, InnerData.ShipDate, value)) InnerData.ShipDate = value; } }
		public double SubTotal { get { LazyGet(); return InnerData.SubTotal; } set { if (LazySet(Members.SubTotal, InnerData.SubTotal, value)) InnerData.SubTotal = value; } }
		public double TaxAmt { get { LazyGet(); return InnerData.TaxAmt; } set { if (LazySet(Members.TaxAmt, InnerData.TaxAmt, value)) InnerData.TaxAmt = value; } }
		public string Freight { get { LazyGet(); return InnerData.Freight; } set { if (LazySet(Members.Freight, InnerData.Freight, value)) InnerData.Freight = value; } }
		public double TotalDue { get { LazyGet(); return InnerData.TotalDue; } set { if (LazySet(Members.TotalDue, InnerData.TotalDue, value)) InnerData.TotalDue = value; } }
		public Vendor Vendor
		{
			get { return ((ILookupHelper<Vendor>)InnerData.Vendor).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Vendor, ((ILookupHelper<Vendor>)InnerData.Vendor).GetItem(null), value))
					((ILookupHelper<Vendor>)InnerData.Vendor).SetItem(value, null); 
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

        private static PurchaseOrderHeaderMembers members = null;
        public static PurchaseOrderHeaderMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(PurchaseOrderHeader))
                    {
                        if (members == null)
                            members = new PurchaseOrderHeaderMembers();
                    }
                }
                return members;
            }
        }
        public class PurchaseOrderHeaderMembers
        {
            internal PurchaseOrderHeaderMembers() { }

			#region Members for interface IPurchaseOrderHeader

            public Property RevisionNumber { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["RevisionNumber"];
            public Property Status { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["Status"];
            public Property OrderDate { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["OrderDate"];
            public Property ShipDate { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["ShipDate"];
            public Property SubTotal { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["SubTotal"];
            public Property TaxAmt { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["TaxAmt"];
            public Property Freight { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["Freight"];
            public Property TotalDue { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["TotalDue"];
            public Property Vendor { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["Vendor"];
            public Property ShipMethod { get; } = Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["ShipMethod"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static PurchaseOrderHeaderFullTextMembers fullTextMembers = null;
        public static PurchaseOrderHeaderFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(PurchaseOrderHeader))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new PurchaseOrderHeaderFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class PurchaseOrderHeaderFullTextMembers
        {
            internal PurchaseOrderHeaderFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(PurchaseOrderHeader))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"];
                }
            }
            return entity;
        }

		private static PurchaseOrderHeaderEvents events = null;
        public static PurchaseOrderHeaderEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(PurchaseOrderHeader))
                    {
                        if (events == null)
                            events = new PurchaseOrderHeaderEvents();
                    }
                }
                return events;
            }
        }
        public class PurchaseOrderHeaderEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<PurchaseOrderHeader, EntityEventArgs> onNew;
            public event EventHandler<PurchaseOrderHeader, EntityEventArgs> OnNew
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
                EventHandler<PurchaseOrderHeader, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((PurchaseOrderHeader)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<PurchaseOrderHeader, EntityEventArgs> onDelete;
            public event EventHandler<PurchaseOrderHeader, EntityEventArgs> OnDelete
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
                EventHandler<PurchaseOrderHeader, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((PurchaseOrderHeader)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<PurchaseOrderHeader, EntityEventArgs> onSave;
            public event EventHandler<PurchaseOrderHeader, EntityEventArgs> OnSave
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
                EventHandler<PurchaseOrderHeader, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((PurchaseOrderHeader)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnRevisionNumber

				private static bool onRevisionNumberIsRegistered = false;

				private static EventHandler<PurchaseOrderHeader, PropertyEventArgs> onRevisionNumber;
				public static event EventHandler<PurchaseOrderHeader, PropertyEventArgs> OnRevisionNumber
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
					EventHandler<PurchaseOrderHeader, PropertyEventArgs> handler = onRevisionNumber;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderHeader)sender, args);
				}

				#endregion

				#region OnStatus

				private static bool onStatusIsRegistered = false;

				private static EventHandler<PurchaseOrderHeader, PropertyEventArgs> onStatus;
				public static event EventHandler<PurchaseOrderHeader, PropertyEventArgs> OnStatus
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
					EventHandler<PurchaseOrderHeader, PropertyEventArgs> handler = onStatus;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderHeader)sender, args);
				}

				#endregion

				#region OnOrderDate

				private static bool onOrderDateIsRegistered = false;

				private static EventHandler<PurchaseOrderHeader, PropertyEventArgs> onOrderDate;
				public static event EventHandler<PurchaseOrderHeader, PropertyEventArgs> OnOrderDate
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
					EventHandler<PurchaseOrderHeader, PropertyEventArgs> handler = onOrderDate;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderHeader)sender, args);
				}

				#endregion

				#region OnShipDate

				private static bool onShipDateIsRegistered = false;

				private static EventHandler<PurchaseOrderHeader, PropertyEventArgs> onShipDate;
				public static event EventHandler<PurchaseOrderHeader, PropertyEventArgs> OnShipDate
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
					EventHandler<PurchaseOrderHeader, PropertyEventArgs> handler = onShipDate;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderHeader)sender, args);
				}

				#endregion

				#region OnSubTotal

				private static bool onSubTotalIsRegistered = false;

				private static EventHandler<PurchaseOrderHeader, PropertyEventArgs> onSubTotal;
				public static event EventHandler<PurchaseOrderHeader, PropertyEventArgs> OnSubTotal
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
					EventHandler<PurchaseOrderHeader, PropertyEventArgs> handler = onSubTotal;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderHeader)sender, args);
				}

				#endregion

				#region OnTaxAmt

				private static bool onTaxAmtIsRegistered = false;

				private static EventHandler<PurchaseOrderHeader, PropertyEventArgs> onTaxAmt;
				public static event EventHandler<PurchaseOrderHeader, PropertyEventArgs> OnTaxAmt
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
					EventHandler<PurchaseOrderHeader, PropertyEventArgs> handler = onTaxAmt;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderHeader)sender, args);
				}

				#endregion

				#region OnFreight

				private static bool onFreightIsRegistered = false;

				private static EventHandler<PurchaseOrderHeader, PropertyEventArgs> onFreight;
				public static event EventHandler<PurchaseOrderHeader, PropertyEventArgs> OnFreight
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
					EventHandler<PurchaseOrderHeader, PropertyEventArgs> handler = onFreight;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderHeader)sender, args);
				}

				#endregion

				#region OnTotalDue

				private static bool onTotalDueIsRegistered = false;

				private static EventHandler<PurchaseOrderHeader, PropertyEventArgs> onTotalDue;
				public static event EventHandler<PurchaseOrderHeader, PropertyEventArgs> OnTotalDue
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
					EventHandler<PurchaseOrderHeader, PropertyEventArgs> handler = onTotalDue;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderHeader)sender, args);
				}

				#endregion

				#region OnVendor

				private static bool onVendorIsRegistered = false;

				private static EventHandler<PurchaseOrderHeader, PropertyEventArgs> onVendor;
				public static event EventHandler<PurchaseOrderHeader, PropertyEventArgs> OnVendor
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onVendorIsRegistered)
							{
								Members.Vendor.Events.OnChange -= onVendorProxy;
								Members.Vendor.Events.OnChange += onVendorProxy;
								onVendorIsRegistered = true;
							}
							onVendor += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onVendor -= value;
							if (onVendor == null && onVendorIsRegistered)
							{
								Members.Vendor.Events.OnChange -= onVendorProxy;
								onVendorIsRegistered = false;
							}
						}
					}
				}
            
				private static void onVendorProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<PurchaseOrderHeader, PropertyEventArgs> handler = onVendor;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderHeader)sender, args);
				}

				#endregion

				#region OnShipMethod

				private static bool onShipMethodIsRegistered = false;

				private static EventHandler<PurchaseOrderHeader, PropertyEventArgs> onShipMethod;
				public static event EventHandler<PurchaseOrderHeader, PropertyEventArgs> OnShipMethod
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
					EventHandler<PurchaseOrderHeader, PropertyEventArgs> handler = onShipMethod;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderHeader)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<PurchaseOrderHeader, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<PurchaseOrderHeader, PropertyEventArgs> OnModifiedDate
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
					EventHandler<PurchaseOrderHeader, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderHeader)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<PurchaseOrderHeader, PropertyEventArgs> onUid;
				public static event EventHandler<PurchaseOrderHeader, PropertyEventArgs> OnUid
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
					EventHandler<PurchaseOrderHeader, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((PurchaseOrderHeader)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IPurchaseOrderHeaderOriginalData

		public IPurchaseOrderHeaderOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IPurchaseOrderHeader

		string IPurchaseOrderHeaderOriginalData.RevisionNumber { get { return OriginalData.RevisionNumber; } }
		string IPurchaseOrderHeaderOriginalData.Status { get { return OriginalData.Status; } }
		System.DateTime IPurchaseOrderHeaderOriginalData.OrderDate { get { return OriginalData.OrderDate; } }
		System.DateTime IPurchaseOrderHeaderOriginalData.ShipDate { get { return OriginalData.ShipDate; } }
		double IPurchaseOrderHeaderOriginalData.SubTotal { get { return OriginalData.SubTotal; } }
		double IPurchaseOrderHeaderOriginalData.TaxAmt { get { return OriginalData.TaxAmt; } }
		string IPurchaseOrderHeaderOriginalData.Freight { get { return OriginalData.Freight; } }
		double IPurchaseOrderHeaderOriginalData.TotalDue { get { return OriginalData.TotalDue; } }
		Vendor IPurchaseOrderHeaderOriginalData.Vendor { get { return ((ILookupHelper<Vendor>)OriginalData.Vendor).GetOriginalItem(null); } }
		ShipMethod IPurchaseOrderHeaderOriginalData.ShipMethod { get { return ((ILookupHelper<ShipMethod>)OriginalData.ShipMethod).GetOriginalItem(null); } }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime IPurchaseOrderHeaderOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		string IPurchaseOrderHeaderOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}