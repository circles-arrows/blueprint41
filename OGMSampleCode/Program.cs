using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blueprint41;
using Blueprint41.Core;
using neo4j = Blueprint41.Neo4j.Persistence.Driver.v5;

using Domain.Data.Manipulation;
using Domain.Data.Query;

namespace OGMSampleCode
{
    class Program
    {
        static void Main(string[] args)
        {
            PersistenceProvider.CurrentPersistenceProvider = new neo4j.Neo4jPersistenceProvider($"bolt://localhost:7689", $"neo4j", $"neo");

            // The definition of what you can store in the graph can be found in the "Datastore" project
            // The type-safe objects you can program against to do CRUD operations can be found in the "Datastore.Generated" project
            var model = new Datastore.AdventureWorks
            {
                LogToConsole = true,
                LogToDebugger = false,
            };

            model.Execute(true);

            using (Transaction.Begin())
            {
                // create 2 departments in the graph
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

                // and an employee
                Employee employee = new Employee()
                {
                    NationalIDNumber = "123",
                    rowguid = Guid.NewGuid().ToString(),
                    JobTitle = "Developer",
                    BirthDate = new DateTime(1980, 1, 1),
                    MaritalStatus = "M",
                    Gender = "M",
                    SalariedFlag = true,
                    VacationHours = 0,
                    SickLeaveHours = 0,
                    Currentflag = true,
                    ModifiedDate = Transaction.Current.TransactionDate,
                };
                development.Employees.Add(employee);

                // save them, since we did it right...
                Transaction.Commit();
                //Transaction.Rollback();
            }

            using (Transaction.Begin())
            {
                // Reusable type-safe query
                var compiled = Transaction.CompiledQuery
                    .Match(Node.Department.Alias(out var d).In.DEPARTMENT_CONTAINS_EMPLOYEE.Out.Employee.Alias(out var e))
                    .Where(d.Name == Parameter.New<string>("DepartmentName"))
                    .Return(d.Name.As("Department"), e.SickLeaveHours.Sum().As("TotalSickleaveHours"), e.VacationHours.Sum().As("TotalVacationHours"))
                    .Compile();

                // Make an execution context, which will hold the parameter values to be used
                var context = compiled.GetExecutionContext();
                context.SetParameter("DepartmentName", "Software Development");

                // Execute query and display results
                var resultSet = context.Execute();
                foreach (var result in resultSet)
                {
                    Console.WriteLine($"Department: {result.Department}, Total outstanding sick leave hours: {result.TotalSickleaveHours}, Total outstanding vacation hours: {result.TotalVacationHours}.");
                }

                // No commit needed, since we're only reading anyway...
            }
        }
    }
}
