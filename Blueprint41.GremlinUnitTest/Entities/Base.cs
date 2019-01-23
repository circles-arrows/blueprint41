 
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
	public interface IBaseOriginalData
    {
		string Uid { get; }
		System.DateTime LastModifiedOn { get; }
    }

	public partial interface IBase : OGM
	{
		string NodeType { get; }
		string Uid { get; set; }
		System.DateTime LastModifiedOn { get; }

		IBaseOriginalData OriginalVersion { get; }
	}

	public partial class Base : OGMAbstractImpl<Base, IBase, System.String>
	{
        #region Initialize

		static Base()
        {
            Register.Types();
        }

        protected override void RegisterGeneratedStoredQueries()
        {
			AdditionalGeneratedStoredQueries();
        }
		partial void AdditionalGeneratedStoredQueries();

		#endregion

        private static IBaseMembers members = null;
        public static IBaseMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(IBase))
                    {
                        if (members == null)
                            members = new IBaseMembers();
                    }
                }
                return members;
            }
        }
        public class IBaseMembers
        {
            internal IBaseMembers() { }

			#region Members for interface IBase

            public Property Uid { get; } = Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Base"].Properties["Uid"];
            public Property LastModifiedOn { get; } = Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Base"].Properties["LastModifiedOn"];
			#endregion

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(IBase))
                {
                    if (entity == null)
                        entity = Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Base"];
                }
            }
            return entity;
        }
	}
}
