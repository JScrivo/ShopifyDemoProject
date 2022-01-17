using Microsoft.EntityFrameworkCore;
using ShopifyDemoProject.Models;

namespace ShopifyDemoProject
{
    public class FranchiseManager
    {
        //Checks if new inventory will fit a location
        public async Task<bool> WillNewInventoryFit(Inventory inventory, AppDbContext _db)
        {
            float remainingCapacity = await CheckRemainingInventory(inventory.LocationID, _db);
            Product product = await _db.Products.AsNoTracking().FirstAsync(x => x.Id == inventory.ProductID);

            return remainingCapacity - (product.VolPerUnit * inventory.Quantity) >= 0;
        }

        //Checks if a change to existing inventory will exceed capacity
        public async Task<bool> WillUpdatedInventroyFit(Inventory inventory, AppDbContext _db)
        {
            float remainingCapacity = await CheckRemainingInventory(inventory.LocationID, _db);
            Product product = await _db.Products.AsNoTracking().FirstAsync(x => x.Id == inventory.ProductID);
            int quantityDifference = inventory.Quantity - (await _db.Inventories.FirstAsync(x => x.LocationID.Equals(inventory.LocationID) &&
                                                                                        x.ProductID.Equals(inventory.ProductID))).Quantity;
            return remainingCapacity - (quantityDifference * product.VolPerUnit) >= 0;
        }

        //Calculates the remaining capacity of a location
        public async Task<float> CheckRemainingInventory(int locationID, AppDbContext _db)
        {
            var inventory = await _db.Inventories.AsNoTracking().Where(x => x.LocationID == locationID).ToListAsync();
            Location location = await _db.Locations.AsNoTracking().FirstAsync(x => x.Id == locationID);
            float capacity = location.Capacity;

            foreach (Inventory item in inventory)
            {
                Product product = _db.Products.AsNoTracking().First(x => x.Id == item.ProductID);
                capacity -= product.VolPerUnit * item.Quantity;
            }

            return capacity;
        }
    }
}
