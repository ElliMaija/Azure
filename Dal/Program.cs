using Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(
            "Create new database (existing db will be deleted), press Y");
            var ki = Console.ReadKey();
            if (ki.Key.ToString().ToLower() != "y")
            {
                PrintData();
                return;
            }
            Console.WriteLine("\nCreating database ...");
            var dbc = new MySqlContext();
            dbc.Database.EnsureDeleted();
            dbc.Database.EnsureCreated();
            Console.WriteLine("\nCreating data ...");
            dbc.Customers.Add(new Customer { Name = "First Customer", Saldo = 123M });
            dbc.Customers.Add(new Customer { Name = "Second Customer", Saldo = 321M });
            dbc.SaveChanges();
            PrintData();
        }
        private static void PrintData()
        {
            using (var dbc = new MySqlContext())
            {
                Console.WriteLine(
               $"Conn.str='{dbc.Database.GetDbConnection().ConnectionString}'");
                Console.WriteLine("Customers");
                foreach (var item in dbc.Customers)
                {
                    Console.WriteLine(
                     $"Id:{item.Id} Name:'{item.Name}' Saldo:{item.Saldo}");
                }
            }
        }

    }
}
