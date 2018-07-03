using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.UnitTest.DataStore
{
    public class MockModel : DatastoreModel<MockModel>
    {
        public Entity Greeting { get; private set; }
        public Entity Person { get; private set; }
        public Entity PersonType { get; private set; }

        protected override void SubscribeEventHandlers()
        {
            
        }

        [Version(0, 0, 0)]
        public void Script_0_0_0()
        {
            FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

            PersonType = Entities.New("Test2")
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .HasStaticData(true)
                   .SetKey("Uid", true)
                   .AddProperty("Name", typeof(string))
                   .AddProperty("Label", typeof(string));

            PersonType.Refactor.CreateNode(new { Uid = "100", Name = "Account" });

            PersonType = Entities.New("Test3")
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .HasStaticData(true)
                   .SetKey("Uid", true)
                   .AddProperty("Name", typeof(string))
                   .AddProperty("Label", typeof(string));

            PersonType.Refactor.CreateNode(new { Uid = "100", Name = "Account" });
        }

        [Version(0, 0, 2)]
        public void Script_0_0_1()
        {
            Entities["Test2"].Refactor.Deprecate();
            Entities["Test3"].Refactor.Deprecate();
        }
    }
}
