 
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
	public interface INeo4jBaseOriginalData
	{
		string Uid { get; }
	}

	public partial interface INeo4jBase : OGM
	{
		string NodeType { get; }
		string Uid { get; set; }

		INeo4jBaseOriginalData OriginalVersion { get; }
	}

	public partial class Neo4jBase : OGMAbstractImpl<Neo4jBase, INeo4jBase, System.String>
	{
		#region Initialize

		static Neo4jBase()
		{
			Register.Types();
		}

		protected override void RegisterGeneratedStoredQueries()
		{
			AdditionalGeneratedStoredQueries();
		}
		public static INeo4jBase LoadByUid(string uid)
		{
			return FromQuery(nameof(LoadByUid), new Parameter(Param0, uid)).FirstOrDefault();
		}
		partial void AdditionalGeneratedStoredQueries();

		#endregion

		private static INeo4jBaseMembers members = null;
		public static INeo4jBaseMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(INeo4jBase))
					{
						if (members is null)
							members = new INeo4jBaseMembers();
					}
				}
				return members;
			}
		}
		public class INeo4jBaseMembers
		{
			internal INeo4jBaseMembers() { }

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(INeo4jBase))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["Neo4jBase"];
				}
			}
			return entity;
		}
	}
}
