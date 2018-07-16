 
using System;
using System.Linq;
using System.Collections.Generic;


using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Datastore.Query;

namespace Datastore.Manipulation
{
	public interface IBaseEntityOriginalData
    {
		string Uid { get; }
		System.DateTime LastModifiedOn { get; }
    }

	public partial interface IBaseEntity : OGM
	{
		string NodeType { get; }
		string Uid { get; set; }
		System.DateTime LastModifiedOn { get; }

		IBaseEntityOriginalData OriginalVersion { get; }
	}

	public partial class BaseEntity : OGMAbstractImpl<BaseEntity, IBaseEntity, System.String>
	{
        #region Initialize

		static BaseEntity()
        {
            Register.Types();
        }

        protected override void RegisterGeneratedStoredQueries()
        {
			AdditionalGeneratedStoredQueries();
        }
		partial void AdditionalGeneratedStoredQueries();

		#endregion

        private static IBaseEntityMembers members = null;
        public static IBaseEntityMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(IBaseEntity))
                    {
                        if (members == null)
                            members = new IBaseEntityMembers();
                    }
                }
                return members;
            }
        }
        public class IBaseEntityMembers
        {
            internal IBaseEntityMembers() { }

			#region Members for interface IBaseEntity

            public Property Uid { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"];
            public Property LastModifiedOn { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"];
			#endregion

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(IBaseEntity))
                {
                    if (entity == null)
                        entity = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"];
                }
            }
            return entity;
        }
	}
}
