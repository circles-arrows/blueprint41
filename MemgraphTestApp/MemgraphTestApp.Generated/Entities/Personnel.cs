 
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
	public interface IPersonnelOriginalData : INeo4jBaseOriginalData
	{
		string FirstName { get; }
		string LastName { get; }
		EmploymentStatus Status { get; }
	}

	public partial interface IPersonnel : OGM, INeo4jBase
	{
		string FirstName { get; set; }
		string LastName { get; set; }
		EmploymentStatus Status { get; set; }

		new IPersonnelOriginalData OriginalVersion { get; }
	}

	public partial class Personnel : OGMAbstractImpl<Personnel, IPersonnel, System.String>
	{
		#region Initialize

		static Personnel()
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
		
		public static Dictionary<System.String, IPersonnel> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.PersonnelAlias, IWhereQuery> query)
		{
			q.PersonnelAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Personnel.Alias(out alias));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		#endregion

		private static IPersonnelMembers members = null;
		public static IPersonnelMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(IPersonnel))
					{
						if (members is null)
							members = new IPersonnelMembers();
					}
				}
				return members;
			}
		}
		public class IPersonnelMembers
		{
			internal IPersonnelMembers() { }

			#region Members for interface IPersonnel

			public Property FirstName { get; } = MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["FirstName"];
			public Property LastName { get; } = MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["LastName"];
			public Property Status { get; } = MemgraphTestApp.HumanResources.Model.Entities["Personnel"].Properties["Status"];
			#endregion

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
				lock (typeof(IPersonnel))
				{
					if (entity is null)
						entity = MemgraphTestApp.HumanResources.Model.Entities["Personnel"];
				}
			}
			return entity;
		}
	}
}
