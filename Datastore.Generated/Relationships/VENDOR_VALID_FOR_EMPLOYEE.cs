using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class VENDOR_VALID_FOR_EMPLOYEE_REL : RELATIONSHIP, IFromIn_VENDOR_VALID_FOR_EMPLOYEE_REL, IFromOut_VENDOR_VALID_FOR_EMPLOYEE_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "VALID_FOR_EMPLOYEE";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal VENDOR_VALID_FOR_EMPLOYEE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public VENDOR_VALID_FOR_EMPLOYEE_REL Alias(out VENDOR_VALID_FOR_EMPLOYEE_ALIAS alias)
		{
			alias = new VENDOR_VALID_FOR_EMPLOYEE_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public VENDOR_VALID_FOR_EMPLOYEE_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new VENDOR_VALID_FOR_EMPLOYEE_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_VENDOR_VALID_FOR_EMPLOYEE_REL IFromIn_VENDOR_VALID_FOR_EMPLOYEE_REL.Alias(out VENDOR_VALID_FOR_EMPLOYEE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_VENDOR_VALID_FOR_EMPLOYEE_REL IFromOut_VENDOR_VALID_FOR_EMPLOYEE_REL.Alias(out VENDOR_VALID_FOR_EMPLOYEE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_VENDOR_VALID_FOR_EMPLOYEE_REL IFromIn_VENDOR_VALID_FOR_EMPLOYEE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_VENDOR_VALID_FOR_EMPLOYEE_REL IFromIn_VENDOR_VALID_FOR_EMPLOYEE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_VENDOR_VALID_FOR_EMPLOYEE_REL IFromOut_VENDOR_VALID_FOR_EMPLOYEE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_VENDOR_VALID_FOR_EMPLOYEE_REL IFromOut_VENDOR_VALID_FOR_EMPLOYEE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public VENDOR_VALID_FOR_EMPLOYEE_IN In { get { return new VENDOR_VALID_FOR_EMPLOYEE_IN(this); } }
		public class VENDOR_VALID_FOR_EMPLOYEE_IN
		{
			private VENDOR_VALID_FOR_EMPLOYEE_REL Parent;
			internal VENDOR_VALID_FOR_EMPLOYEE_IN(VENDOR_VALID_FOR_EMPLOYEE_REL parent)
			{
				Parent = parent;
			}

			public VendorNode Vendor { get { return new VendorNode(Parent, DirectionEnum.In); } }
		}

		public VENDOR_VALID_FOR_EMPLOYEE_OUT Out { get { return new VENDOR_VALID_FOR_EMPLOYEE_OUT(this); } }
		public class VENDOR_VALID_FOR_EMPLOYEE_OUT
		{
			private VENDOR_VALID_FOR_EMPLOYEE_REL Parent;
			internal VENDOR_VALID_FOR_EMPLOYEE_OUT(VENDOR_VALID_FOR_EMPLOYEE_REL parent)
			{
				Parent = parent;
			}

			public EmployeeNode Employee { get { return new EmployeeNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_VENDOR_VALID_FOR_EMPLOYEE_REL
	{
		IFromIn_VENDOR_VALID_FOR_EMPLOYEE_REL Alias(out VENDOR_VALID_FOR_EMPLOYEE_ALIAS alias);
		IFromIn_VENDOR_VALID_FOR_EMPLOYEE_REL Repeat(int maxHops);
		IFromIn_VENDOR_VALID_FOR_EMPLOYEE_REL Repeat(int minHops, int maxHops);

		VENDOR_VALID_FOR_EMPLOYEE_REL.VENDOR_VALID_FOR_EMPLOYEE_OUT Out { get; }
	}
	public interface IFromOut_VENDOR_VALID_FOR_EMPLOYEE_REL
	{
		IFromOut_VENDOR_VALID_FOR_EMPLOYEE_REL Alias(out VENDOR_VALID_FOR_EMPLOYEE_ALIAS alias);
		IFromOut_VENDOR_VALID_FOR_EMPLOYEE_REL Repeat(int maxHops);
		IFromOut_VENDOR_VALID_FOR_EMPLOYEE_REL Repeat(int minHops, int maxHops);

		VENDOR_VALID_FOR_EMPLOYEE_REL.VENDOR_VALID_FOR_EMPLOYEE_IN In { get; }
	}

	public class VENDOR_VALID_FOR_EMPLOYEE_ALIAS : AliasResult
	{
		private VENDOR_VALID_FOR_EMPLOYEE_REL Parent;

		internal VENDOR_VALID_FOR_EMPLOYEE_ALIAS(VENDOR_VALID_FOR_EMPLOYEE_REL parent)
		{
			Parent = parent;

			CreationDate = new RelationFieldResult(this, "CreationDate");
		}

        public Assignment[] Assign(JsNotation<DateTime> CreationDate = default)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (CreationDate.HasValue) assignments.Add(new Assignment(this.CreationDate, CreationDate));

            return assignments.ToArray();
        }

		public RelationFieldResult CreationDate { get; private set; } 
	}
}
