 
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
		System.DateTime? CreatedOn { get; }
		string CreatedBy { get; }
		System.DateTime LastModifiedOn { get; }
		string LastModifiedBy { get; }
		string Description { get; }
	}

	public partial interface INeo4jBase : OGM
	{
		string NodeType { get; }
		string Uid { get; set; }
		System.DateTime? CreatedOn { get; set; }
		string CreatedBy { get; set; }
		System.DateTime LastModifiedOn { get; }
		string LastModifiedBy { get; set; }
		string Description { get; set; }

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

			public Property Uid { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["Uid"];
			public Property CreatedOn { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["CreatedOn"];
			public Property CreatedBy { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["CreatedBy"];
			public Property LastModifiedOn { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["LastModifiedOn"];
			public Property LastModifiedBy { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["LastModifiedBy"];
			public Property Description { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["Description"];
			#endregion

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(INeo4jBase))
				{
					if (entity is null)
						entity = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"];
				}
			}
			return entity;
		}
	}
}
