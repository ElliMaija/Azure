using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class MySqlContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public
        MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        { }
        //protected override void OnConfiguring(
        //DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //    @"Server=(localdb)\mssqllocaldb;Database=MyDb;Trusted_Connection=True;")
        //    ;
        //}
    }
}
