 
using System;
using System.Collections.Generic;
using System.Linq;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Domain.Data.Query;

namespace Domain.Data.Manipulation
{
	public interface ISchemaBaseOriginalData : INeo4jBaseOriginalData
    {
		System.DateTime ModifiedDate { get; }
    }

	public partial interface ISchemaBase : OGM, INeo4jBase
	{
		System.DateTime ModifiedDate { get; set; }

		new ISchemaBaseOriginalData OriginalVersion { get; }
	}

	public partial class SchemaBase : OGMAbstractImpl<SchemaBase, ISchemaBase, System.String>
	{
        #region Initialize

		static SchemaBase()
        {
            Register.Types();
        }

        protected override void RegisterGeneratedStoredQueries()
        {
			AdditionalGeneratedStoredQueries();
        }
		partial void AdditionalGeneratedStoredQueries();

		#endregion

        private static ISchemaBaseMembers members = null;
        public static ISchemaBaseMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(ISchemaBase))
                    {
                        if (members == null)
                            members = new ISchemaBaseMembers();
                    }
                }
                return members;
            }
        }
        public class ISchemaBaseMembers
        {
            internal ISchemaBaseMembers() { }

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(ISchemaBase))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["SchemaBase"];
                }
            }
            return entity;
        }
	}
}
