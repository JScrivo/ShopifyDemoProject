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
            modelBuilder.Entity<Inventory>().HasKey(c => new { c.ItemID, c.LocationID });
            modelBuilder.Entity<Price>().HasKey(c => new { c.ItemID, c.LocationID });
        }
    }
}
