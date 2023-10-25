using System;
using Blueprint41.Core;

namespace Domain.Data.Manipulation
{
	internal class Register
	{
		private static bool isInitialized = false;

		public static void Types()
		{
			if (MemgraphTestApp.HumanResources.Model.TypesRegistered)
				return;

			lock (typeof(Register))
			{
				if (MemgraphTestApp.HumanResources.Model.TypesRegistered)
					return;

				((ISetRuntimeType)MemgraphTestApp.HumanResources.Model.Entities["Branch"]).SetRuntimeTypes(typeof(Branch), typeof(Branch));
				((ISetRuntimeType)MemgraphTestApp.HumanResources.Model.Entities["Department"]).SetRuntimeTypes(typeof(Department), typeof(Department));
				((ISetRuntimeType)MemgraphTestApp.HumanResources.Model.Entities["Employee"]).SetRuntimeTypes(typeof(Employee), typeof(Employee));
				((ISetRuntimeType)MemgraphTestApp.HumanResources.Model.Entities["EmploymentStatus"]).SetRuntimeTypes(typeof(EmploymentStatus), typeof(EmploymentStatus));
				((ISetRuntimeType)MemgraphTestApp.HumanResources.Model.Entities["HeadEmployee"]).SetRuntimeTypes(typeof(HeadEmployee), typeof(HeadEmployee));
				((ISetRuntimeType)MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"]).SetRuntimeTypes(typeof(INeo4jBase), typeof(Neo4jBase));
				((ISetRuntimeType)MemgraphTestApp.HumanResources.Model.Entities["Personnel"]).SetRuntimeTypes(typeof(IPersonnel), typeof(Personnel));
			}
		}
	}
}
