using Microsoft.EntityFrameworkCore;
using ShopifyDemoProject.Models;

namespace ShopifyDemoProject
{
    public class FranchiseManager
    {
        public async Task<bool> WillNewInventoryFit(Inventory inventory, AppDbContext _db)
        {
            float remainingCapacity = await CheckRemainingInventory(inventory.LocationID, _db);
            Product product = await _db.Products.AsNoTracking().FirstAsync(x => x.Id == inventory.ItemID);

            return remainingCapacity - (product.VolPerUnit * inventory.Quantity) >= 0;
        }

        public async Task<bool> WillUpdatedInventroyFit(Inventory inventory, AppDbContext _db)
        {
            float remainingCapacity = await CheckRemainingInventory(inventory.LocationID, _db);
            Product product = await _db.Products.AsNoTracking().FirstAsync(x => x.Id == inventory.ItemID);
            int quantityDifference = inventory.Quantity - (await _db.Inventories.FirstAsync(x => x.LocationID.Equals(inventory.LocationID) &&
                                                                                        x.ItemID.Equals(inventory.ItemID))).Quantity;
            return remainingCapacity - (quantityDifference * product.VolPerUnit) >= 0;
        }

        public async Task<float> CheckRemainingInventory(int locationID, AppDbContext _db)
        {
            var inventory = await _db.Inventories.AsNoTracking().Where(x => x.LocationID == locationID).ToListAsync();
            Location location = await _db.Locations.AsNoTracking().FirstAsync(x => x.Id == locationID);
            float capacity = location.Capacity;

            foreach (Inventory item in inventory)
            {
                Product product = _db.Products.AsNoTracking().First(x => x.Id == item.ItemID);
                capacity -= product.VolPerUnit * item.Quantity;
            }

            return capacity;
        }
    }
}
