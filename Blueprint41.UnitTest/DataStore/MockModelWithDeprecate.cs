using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.UnitTest.DataStore
{
    public class MockModelWithDeprecate : DatastoreModel<MockModelWithDeprecate>
    {
        protected override void SubscribeEventHandlers() { }

        [Version(0, 0, 0)]
        public void Script_0_0_0()
        {
            FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

            Entities.New("BaseEntity")
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .Abstract(true)
                   .Virtual(true)
                   .SetKey("Uid", true);

            Entities.New("Person", Entities["BaseEntity"])
                    .AddProperty("Name", typeof(string));
        }

        [Version(0, 0, 1)]
        public void Script_0_0_1()
        {
            Entities["Person"].Refactor.Deprecate();
        }
    }
}
