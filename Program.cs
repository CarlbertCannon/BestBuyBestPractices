using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace BestBuyBestPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion

            IDbConnection conn = new MySqlConnection(connString);
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);

            var Repo = new DapperProductRepository(conn); 

            Console.WriteLine("Hello, here are the current departments:");
            Console.WriteLine("Please press enter");
            Console.ReadLine();

            var depos = repo.GetAllDepartments();
            

            Print(depos);

            Console.WriteLine("Do you want to add a department?");
            var userResponse = Console.ReadLine();
            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of your new department?");
                userResponse = Console.ReadLine();
                repo.InsertDepartment(userResponse);
                Print(repo.GetAllDepartments());
            }

            var products = Repo.GetAllProducts();

            foreach (var prod in products)
            {
                Console.WriteLine($"ProductID: {prod.ProductID}, Name: {prod.Name}, CategoryID: {prod.CategoryID}");
            }
            Repo.CreateProduct("newProd", 49, 5);


            Console.WriteLine("Have a great day!");
            Console.WriteLine("Here is the products section");
            Console.WriteLine("Press enter...");

            Console.ReadLine();
            

        }
        private static void Print(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"ID: {depo.DepartmentID} Name: {depo.Name}");
            }
        }

    }
}
