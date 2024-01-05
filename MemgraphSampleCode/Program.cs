using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Blueprint41;
using Blueprint41.Core;

using Domain.Data.Manipulation;

using MemgraphTestApp;

using neo4j = Blueprint41.Neo4j.Persistence.Driver.Memgraph;

namespace MemgraphSampleCode
{
    class Program
    {
        static void Main(string[] args)
        {
            PersistenceProvider.CurrentPersistenceProvider = new neo4j.Neo4jPersistenceProvider($"bolt://localhost:7690", $"neo4j", $"passionite?01");

            // The definition of what you can store in the graph can be found in the "Datastore" project
            // The type-safe objects you can program against to do CRUD operations can be found in the "Datastore.Generated" project
            var model = new HumanResources
            {
                LogToConsole = true,
                LogToDebugger = false,
            };
            model.Execute(true);
            //CreateEntities();
            UpdateEntities();
        }

        private static void UpdateEntities()
        {
            using (Transaction.Begin(true))
            {
                // create 2 departments in the graph
                Department development = Department.Load("DEP_AAAAA");
                Department support = Department.Load("DEP_BBBBB");

                // and an employee
                Employee employee = Employee.Load("EMP_AAAAA");
                Employee employee2 = Employee.Load("EMP_BBBBB");

                //employee.LastModifiedOn = Transaction.Current.TransactionDate;
                employee.Department = support;
                //employee.LastModifiedOn = Transaction.Current.TransactionDate;
                employee2.Department = support;
                //employee.Department = support;
                //support.Employees.AddRange(new List<Employee>() { employee, employee2 });

                // save them, since we did it right...
                Transaction.Commit();
                //Transaction.Rollback();
            }
        }
        private static void CreateEntities()
        {
            using (Transaction.Begin())
            {
                // create 2 departments in the graph
                Department development = new Department()
                {
                    Uid = "DEP_AAAAA",
                    Name = "Software Development",
                    LastModifiedOn = Transaction.Current.TransactionDate
                };
                Department support = new Department()
                {
                    Uid = "DEP_BBBBB",
                    Name = "Support",
                    LastModifiedOn = Transaction.Current.TransactionDate
                };

                // and an employee
                Employee employee1 = new Employee()
                {
                    Uid = "EMP_AAAAA",
                    FirstName = "Bong",
                    LastName = "Revilla",
                    LastModifiedOn = Transaction.Current.TransactionDate
                };
                development.Employees.Add(employee1);
                Employee employee2 = new Employee()
                {
                    Uid = "EMP_BBBBB",
                    FirstName = "Juan",
                    LastName = "Enrile",
                    LastModifiedOn = Transaction.Current.TransactionDate
                };
                development.Employees.Add(employee2);
                Employee employee3 = new Employee()
                {
                    Uid = "EMP_CCCCC",
                    FirstName = "Bato",
                    LastName = "Stone",
                    LastModifiedOn = Transaction.Current.TransactionDate
                };
                development.Employees.Add(employee3);
                Employee employee4 = new Employee()
                {
                    Uid = "EMP_DDDDD",
                    FirstName = "Bong",
                    LastName = "Go",
                    LastModifiedOn = Transaction.Current.TransactionDate
                };
                development.Employees.Add(employee4);

                // save them, since we did it right...
                Transaction.Commit();
                //Transaction.Rollback();
            }
        }
    }
}
