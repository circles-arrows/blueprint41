using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Blueprint41;
using Blueprint41.Core;

using neo4j = Blueprint41.Neo4j.Persistence.Driver.v5;

namespace OGMSampleCode
{
    class Program
    {
        static void Main(string[] args)
        {
            PersistenceProvider.CurrentPersistenceProvider = new neo4j.Neo4jPersistenceProvider($"bolt://localhost:7690", $"neo4j", $"passionite?01");

            // The definition of what you can store in the graph can be found in the "Datastore" project
            // The type-safe objects you can program against to do CRUD operations can be found in the "Datastore.Generated" project
            var model = new Datastore.HumanResources
            {
                LogToConsole = true,
                LogToDebugger = false,
            };
            model.Execute(true);
            //CreateEntities();
            //UpdateEntities();
        }

        //private static void UpdateEntities()
        //{
        //    using (Transaction.Begin(true))
        //    {
        //        // create 2 departments in the graph
        //        Department development = Department.Load("4");
        //        Department support = Department.Load("5");

        //        // and an employee
        //        Employee employee = Employee.Load("6");
        //        Employee employee2 = Employee.Load("7");

        //        //employee.Department = support;
        //        development.Employees.AddRange(new List<Employee>() { employee, employee2 });

        //        // save them, since we did it right...
        //        Transaction.Commit();
        //        //Transaction.Rollback();
        //    }
        //}
        //private static void CreateEntities()
        //{
        //    using (Transaction.Begin())
        //    {
        //        // create 2 departments in the graph
        //        Department development = new Department()
        //        {
        //            Name = "Software Development",
        //        };
        //        Department support = new Department()
        //        {
        //            Name = "Support",
        //        };

        //        // and an employee
        //        Employee employee1 = new Employee()
        //        {
        //            FirstName = "Juan",
        //            LastName = "Enrile"
        //        };
        //        development.Employees.Add(employee1);
        //        Employee employee2 = new Employee()
        //        {
        //            FirstName = "Juan2",
        //            LastName = "Enrile"
        //        };
        //        development.Employees.Add(employee2);
        //        Employee employee3 = new Employee()
        //        {
        //            FirstName = "Juan3",
        //            LastName = "Enrile"
        //        };
        //        development.Employees.Add(employee3);
        //        Employee employee4 = new Employee()
        //        {
        //            FirstName = "Juan4",
        //            LastName = "Enrile"
        //        };
        //        development.Employees.Add(employee4);

        //        // save them, since we did it right...
        //        Transaction.Commit();
        //        //Transaction.Rollback();
        //    }
        //}
    }
}
