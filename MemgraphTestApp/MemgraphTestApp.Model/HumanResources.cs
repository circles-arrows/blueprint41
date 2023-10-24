using System;

using Blueprint41;



namespace MemgraphTestApp

{

    public class HumanResources : DatastoreModel<HumanResources>

    {

        [Version(1, 0, 0)]

        protected void Initial()

        {

            FunctionalIds.Default = FunctionalIds.New("Shared", "HR_", IdFormat.Numeric, 0);



            Entities.New("Branch")

                .AddProperty("Name", typeof(string))

                .AddProperty("Uid", typeof(string), false, IndexType.Unique)

                .SetKey("Uid", true);



            Entities.New("Department")

                .AddProperty("Name", typeof(string))

                .AddProperty("Uid", typeof(string), false, IndexType.Unique)

                .SetKey("Uid", true);



            var Personnel = Entities.New("Personnel")

                .Abstract(true)

                .AddProperty("Uid", typeof(string), false, IndexType.Unique)

                .SetKey("Uid", true)

                .AddProperty("FirstName", typeof(string))

                .AddProperty("LastName", typeof(string));





            Entities.New("Employee", Personnel);



            Entities.New("HeadEmployee", Personnel)

              .AddProperty("Title", typeof(string));



            Entities.New("EmploymentStatus")

                .HasStaticData(true)

                .AddProperty("Uid", typeof(string), false, IndexType.Unique)

                .SetKey("Uid", true)

                .AddProperty("Name", typeof(string), false);



            Relations.New(Personnel, Entities["EmploymentStatus"], "HAS_STATUS", "HAS_STATUS")

                     .SetInProperty("Status", PropertyType.Lookup);



            Relations.New(Entities["Branch"], Entities["Department"], "BELONGS_TO", "BELONGS_TO")

              .SetInProperty("Departments", PropertyType.Collection)

              .SetOutProperty("Branch", PropertyType.Lookup);



            Relations.New(Entities["Employee"], Entities["Department"], "WORKS_IN", "WORKS_IN")

                .SetInProperty("Department", PropertyType.Collection)

                .SetOutProperty("Employees", PropertyType.Collection);



            Relations.New(Entities["HeadEmployee"], Entities["Department"], "MANAGES_DEPARTMENT", "MANAGES_DEPARTMENT")

                .SetOutProperty("DepartmentHead", PropertyType.Lookup);





            Relations.New(Entities["HeadEmployee"], Entities["Branch"], "MANAGES_BRANCH", "MANAGES_BRANCH")

                .SetInProperty("Branch", PropertyType.Lookup)

                .SetOutProperty("BranchHead", PropertyType.Lookup);





            Entities["EmploymentStatus"].Refactor.CreateNode(new { Uid = "1", Name = "Regular" });

            Entities["EmploymentStatus"].Refactor.CreateNode(new { Uid = "2", Name = "Probationary" });

            Entities["EmploymentStatus"].Refactor.CreateNode(new { Uid = "3", Name = "Contractual" });

        }



        protected override void SubscribeEventHandlers()

        {



        }

    }

}