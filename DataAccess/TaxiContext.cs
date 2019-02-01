using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class TaxiContext:DbContext
    {
        public TaxiContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TaxiDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var client = new Client
            {
                Id=1,
                FullName = "Anton"
            };

            var driver = new Driver
            {
                Id=1,
                FullName="Vasya",
                Car = "Toyota"
            };

            var order = new Order
            {
                Id=1,
                StartPoint="Pushkina",
                EndPoint = "Imanova",
                Price=500
            };

            modelBuilder.Entity<Client>().HasData(client);
            modelBuilder.Entity<Driver>().HasData(driver);
            modelBuilder.Entity<Order>().HasData(order);

            base.OnModelCreating(modelBuilder);
        }
    }
}
