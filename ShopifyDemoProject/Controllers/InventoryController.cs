using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopifyDemoProject.Models;

namespace ShopifyDemoProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : Controller
    {
        private readonly AppDbContext _db;

        public InventoryController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var inventories = _db.Inventories.ToList();
            return new JsonResult(inventories);
        }

        /*[HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var inventory = await _db.Inventories.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) return BadRequest("Inventory not found");
            return new JsonResult(product);
        }*/

        [HttpPost]
        public async Task<IActionResult> Post(Inventory inventory)
        {
            var existing = await _db.Inventories.FirstOrDefaultAsync(x => x.ItemID.Equals(inventory.ItemID) && x.LocationID.Equals(inventory.LocationID));
            if (existing != null) return BadRequest("Inventory already exists for this item at this location.");

            _db.Inventories.Add(inventory);
            await _db.SaveChangesAsync();

            return new JsonResult(new { ItemID = inventory.ItemID, LocationID = inventory.LocationID });
        }

        [HttpPut]
        public async Task<IActionResult> Update(Inventory inventory)
        {
            var existing = await _db.Inventories.FirstOrDefaultAsync(x => x.ItemID.Equals(inventory.ItemID) && x.LocationID.Equals(inventory.LocationID));
            if (existing == null) return BadRequest("Inventory not found");

            existing.Quantity = inventory.Quantity;

            _db.Inventories.Update(existing);
            await _db.SaveChangesAsync();

            return new JsonResult(new { ItemID = inventory.ItemID, LocationID = inventory.LocationID });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int itemID, int locationID)
        {
            var inventory = await _db.Inventories.FirstOrDefaultAsync(x => x.ItemID.Equals(itemID) && x.LocationID.Equals(locationID));
            if (inventory == null) return BadRequest("Inventory not found");

            _db.Inventories.Remove(inventory);
            await _db.SaveChangesAsync();

            return new OkObjectResult("Inventory deleted");
        }

        public float CheckRemainingInventory(int locationID)
        {
            var inventory =  _db.Inventories.Where(x => x.LocationID == locationID);
            float capacity = _db.Locations.First(x => x.Id == locationID).Capacity;

            foreach (Inventory item in inventory)
            {
                Product product = _db.Products.First(x => x.Id == item.ItemID);
                capacity -= product.VolPerUnit * item.Quantity;
            }

            return capacity;
        }
    }
}
