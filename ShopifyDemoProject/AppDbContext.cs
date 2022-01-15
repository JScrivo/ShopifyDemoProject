using ShopifyDemoProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ShopifyDemoProject
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Price> Prices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>().HasKey(c => new { c.ProductID, c.LocationID });
            modelBuilder.Entity<Price>().HasKey(c => new { c.ProductID, c.LocationID });

            //Seed Data

            //Product Table
            modelBuilder.Entity<Product>().HasData(new Product { Id = 1, Name = "Bananas", Description = "Bundle of Bananas", DefaultPrice = 4.56f, VolPerUnit = 1.2f });
            modelBuilder.Entity<Product>().HasData(new Product { Id = 2, Name = "Soup", Description = "Can of Soup", DefaultPrice = 0.95f, VolPerUnit = 0.5f });
            modelBuilder.Entity<Product>().HasData(new Product { Id = 3, Name = "Cereal", Description = "Box of Cereal", DefaultPrice = 5.00f, VolPerUnit = 3.5f });

            //Location Table
            modelBuilder.Entity<Location>().HasData(new Location { Id = 1, Name = "Storefront", Address = "124 Real Place Ave.", Capacity = 2500f });
            modelBuilder.Entity<Location>().HasData(new Location { Id = 2, Name = "Warehouse", Address = "471 Industrial Complex Rd.", Capacity = 10000f });

            //Prices Table
            modelBuilder.Entity<Price>().HasData(new Price { ProductID = 1, LocationID = 1, UnitPrice = 2.25f });
            modelBuilder.Entity<Price>().HasData(new Price { ProductID = 2, LocationID = 1, UnitPrice = 0.75f });

            //Inventory Table
            modelBuilder.Entity<Inventory>().HasData(new Inventory { ProductID = 1, LocationID = 1, Quantity = 20 });
            modelBuilder.Entity<Inventory>().HasData(new Inventory { ProductID = 2, LocationID = 1, Quantity = 50 });
            modelBuilder.Entity<Inventory>().HasData(new Inventory { ProductID = 3, LocationID = 1, Quantity = 15 });

            modelBuilder.Entity<Inventory>().HasData(new Inventory { ProductID = 1, LocationID = 2, Quantity = 50 });
            modelBuilder.Entity<Inventory>().HasData(new Inventory { ProductID = 2, LocationID = 2, Quantity = 500 });
            modelBuilder.Entity<Inventory>().HasData(new Inventory { ProductID = 3, LocationID = 2, Quantity = 100 });
        }
    }
}
