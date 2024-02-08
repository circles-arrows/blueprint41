 
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
        public static IBaseEntity LoadByUid(string uid)
        {
            return FromQuery(nameof(LoadByUid), new Parameter(Param0, uid)).FirstOrDefault();
        }
        partial void AdditionalGeneratedStoredQueries();

        #endregion

        private static IBaseEntityMembers members = null;
        public static IBaseEntityMembers Members
        {
            get
            {
                if (members is null)
                {
                    lock (typeof(IBaseEntity))
                    {
                        if (members is null)
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

            public Property Uid { get; } = Blueprint41.UnitTest.Memgraph.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"];
            public Property LastModifiedOn { get; } = Blueprint41.UnitTest.Memgraph.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"];
            #endregion

        }

        sealed public override Entity GetEntity()
        {
            if (entity is null)
            {
                lock (typeof(IBaseEntity))
                {
                    if (entity is null)
                        entity = Blueprint41.UnitTest.Memgraph.DataStore.MockModel.Model.Entities["BaseEntity"];
                }
            }
            return entity;
        }
    }
}
