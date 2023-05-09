using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class PERSON_HAS_CONTACTTYPE_REL : RELATIONSHIP, IFromIn_PERSON_HAS_CONTACTTYPE_REL, IFromOut_PERSON_HAS_CONTACTTYPE_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "HAS_CONTACTTYPE";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal PERSON_HAS_CONTACTTYPE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PERSON_HAS_CONTACTTYPE_REL Alias(out PERSON_HAS_CONTACTTYPE_ALIAS alias)
		{
			alias = new PERSON_HAS_CONTACTTYPE_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public PERSON_HAS_CONTACTTYPE_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new PERSON_HAS_CONTACTTYPE_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_PERSON_HAS_CONTACTTYPE_REL IFromIn_PERSON_HAS_CONTACTTYPE_REL.Alias(out PERSON_HAS_CONTACTTYPE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PERSON_HAS_CONTACTTYPE_REL IFromOut_PERSON_HAS_CONTACTTYPE_REL.Alias(out PERSON_HAS_CONTACTTYPE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PERSON_HAS_CONTACTTYPE_REL IFromIn_PERSON_HAS_CONTACTTYPE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PERSON_HAS_CONTACTTYPE_REL IFromIn_PERSON_HAS_CONTACTTYPE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PERSON_HAS_CONTACTTYPE_REL IFromOut_PERSON_HAS_CONTACTTYPE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PERSON_HAS_CONTACTTYPE_REL IFromOut_PERSON_HAS_CONTACTTYPE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PERSON_HAS_CONTACTTYPE_IN In { get { return new PERSON_HAS_CONTACTTYPE_IN(this); } }
		public class PERSON_HAS_CONTACTTYPE_IN
		{
			private PERSON_HAS_CONTACTTYPE_REL Parent;
			internal PERSON_HAS_CONTACTTYPE_IN(PERSON_HAS_CONTACTTYPE_REL parent)
			{
				Parent = parent;
			}

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
		}

		public PERSON_HAS_CONTACTTYPE_OUT Out { get { return new PERSON_HAS_CONTACTTYPE_OUT(this); } }
		public class PERSON_HAS_CONTACTTYPE_OUT
		{
			private PERSON_HAS_CONTACTTYPE_REL Parent;
			internal PERSON_HAS_CONTACTTYPE_OUT(PERSON_HAS_CONTACTTYPE_REL parent)
			{
				Parent = parent;
			}

			public ContactTypeNode ContactType { get { return new ContactTypeNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_PERSON_HAS_CONTACTTYPE_REL
	{
		IFromIn_PERSON_HAS_CONTACTTYPE_REL Alias(out PERSON_HAS_CONTACTTYPE_ALIAS alias);
		IFromIn_PERSON_HAS_CONTACTTYPE_REL Repeat(int maxHops);
		IFromIn_PERSON_HAS_CONTACTTYPE_REL Repeat(int minHops, int maxHops);

		PERSON_HAS_CONTACTTYPE_REL.PERSON_HAS_CONTACTTYPE_OUT Out { get; }
	}
	public interface IFromOut_PERSON_HAS_CONTACTTYPE_REL
	{
		IFromOut_PERSON_HAS_CONTACTTYPE_REL Alias(out PERSON_HAS_CONTACTTYPE_ALIAS alias);
		IFromOut_PERSON_HAS_CONTACTTYPE_REL Repeat(int maxHops);
		IFromOut_PERSON_HAS_CONTACTTYPE_REL Repeat(int minHops, int maxHops);

		PERSON_HAS_CONTACTTYPE_REL.PERSON_HAS_CONTACTTYPE_IN In { get; }
	}

	public class PERSON_HAS_CONTACTTYPE_ALIAS : AliasResult
	{
		private PERSON_HAS_CONTACTTYPE_REL Parent;

		internal PERSON_HAS_CONTACTTYPE_ALIAS(PERSON_HAS_CONTACTTYPE_REL parent)
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
