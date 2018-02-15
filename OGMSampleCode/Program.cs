using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blueprint41;
using Domain.Data.Manipulation;
using Domain.Data.Query;

namespace OGMSampleCode
{
    class Program
    {
        static void Main(string[] args)
        {
            // The definition of what you can store in the graph can be found in the "Datastore" project
            // The type-safe objects you can program against to do CRUD operations can be found in the "Datastore.Generated" project

            using (Transaction.Begin())
            {
                Department development = new Department()
                {
                    Name = "Software Development",
                    GroupName = "IT",
                };
                Department support = new Department()
                {
                    Name = "Support",
                    GroupName = "IT",
                };

                development.Employees.Add(
                    new Employee()
                    {
                        NationalIDNumber = "123",
                        JobTitle = "Developer",
                        BirthDate = new DateTime(1980, 1, 1),
                        MaritalStatus = "M",
                        Gender = "M",
                        SalariedFlag = true,
                        VacationHours = 0,
                        SickLeaveHours = 0,
                        Currentflag = true,
                        ModifiedDate = Transaction.Current.TransactionDate,
                    });

                Transaction.Commit();
            }

            using (Transaction.Begin())
            {
                var compiled = Transaction.CompiledQuery
                    .Match(Node.Department.Alias(out var d).In.DEPARTMENT_CONTAINS_EMPLOYEE.Out.Employee.Alias(out var e))
                    .Where(d.Name == Parameter.New<string>("DepartmentName"))
                    .Return(d.Name.As("Department"), e.SickLeaveHours.Sum().As("TotalSickleaveHours"), e.VacationHours.Sum().As("TotalVacationHours"))
                    .Compile();

                var context = compiled.GetExecutionContext();
                context.SetParameter("DepartmentName", "Software Development");
                var resultSet = context.Execute();

                foreach (var result in resultSet)
                {
                    Console.WriteLine($"Department: {result.Department}, Total outstanding sick leave hours: {result.TotalSickleaveHours}, Total outstanding vacation hours: {result.TotalVacationHours}.");
                }
            }
        }
    }
}
