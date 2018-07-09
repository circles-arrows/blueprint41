using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.UnitTest.DataStore
{
    public class MockModel : DatastoreModel<MockModel>
    {
        protected override void SubscribeEventHandlers()
        {

        }

        [Version(0, 0, 0)]
        public void Script_0_0_0()
        {
            FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

            Entities.New("BaseEntity")
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .Abstract(true)
                   .Virtual(true)
                   .SetKey("Uid", true)
                   .AddProperty("LastModifiedOn", typeof(DateTime))
                   .SetRowVersionField("LastModifiedOn");

            Entities.New("Person", Entities["BaseEntity"])
                   .AddProperty("Name", typeof(string));
        }
    }
}
