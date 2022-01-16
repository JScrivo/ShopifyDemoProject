﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopifyDemoProject.Models;
using ShopifyDemoProject;

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

        [HttpGet("{productID}/{locationID}")]
        public async Task<IActionResult> Get(int productID, int locationID)
        {
            var inventory = await _db.Inventories.FirstOrDefaultAsync(x => x.ProductID.Equals(productID) && x.LocationID.Equals(locationID));
            if (inventory == null) return BadRequest("Inventory not found");
            return new JsonResult(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Inventory inventory)
        {
            var existing = await _db.Inventories.FirstOrDefaultAsync(x => x.ProductID.Equals(inventory.ProductID) && x.LocationID.Equals(inventory.LocationID));
            if (existing != null) return BadRequest("Inventory already exists for this item at this location.");

            FranchiseManager franchiseManager = new();

            if (!await franchiseManager.WillNewInventoryFit(inventory, _db)) return BadRequest("The seleted location does not have enough room to store the specified quantity of this product.");

            _db.Inventories.Add(inventory);
            await _db.SaveChangesAsync();

            return new JsonResult(new { ItemID = inventory.ProductID, LocationID = inventory.LocationID });
        }

        [HttpPut]
        public async Task<IActionResult> Update(Inventory inventory)
        {
            var existing = await _db.Inventories.FirstOrDefaultAsync(x => x.ProductID.Equals(inventory.ProductID) && x.LocationID.Equals(inventory.LocationID));
            if (existing == null) return BadRequest("Inventory not found");

            FranchiseManager franchiseManager = new();

            if (!await franchiseManager.WillUpdatedInventroyFit(inventory, _db)) return BadRequest("The seleted location does not have enough room to store the specified quantity of this product.");

            existing.Quantity = inventory.Quantity;

            _db.Inventories.Update(existing);
            await _db.SaveChangesAsync();

            return new JsonResult(new { ItemID = inventory.ProductID, LocationID = inventory.LocationID });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int itemID, int locationID)
        {
            var inventory = await _db.Inventories.FirstOrDefaultAsync(x => x.ProductID.Equals(itemID) && x.LocationID.Equals(locationID));
            if (inventory == null) return BadRequest("Inventory not found");

            _db.Inventories.Remove(inventory);
            await _db.SaveChangesAsync();

            return new OkObjectResult("Inventory deleted");
        }

        

        
    }
}