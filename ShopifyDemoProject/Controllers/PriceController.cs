using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopifyDemoProject.Models;

namespace ShopifyDemoProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : Controller
    {
        private readonly AppDbContext _db;

        public PriceController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var prices = _db.Prices.ToList();
            return new JsonResult(prices);
        }

        [HttpGet("{productID}/{locationID}")]
        public async Task<IActionResult> Get(int productID, int locationID)
        {
            var pricing = await _db.Prices.FirstOrDefaultAsync(x => x.ItemID.Equals(productID) && x.LocationID.Equals(locationID));
            if (pricing == null)
            {
                var product = await _db.Products.FirstOrDefaultAsync(x => x.Id.Equals(productID));
                if (product == null) return BadRequest("Product not found");

                pricing = new Price();
                pricing.LocationID = locationID;
                pricing.UnitPrice = product.DefaultPrice;
                pricing.ItemID = productID;

                return new JsonResult(new { productPricing = pricing, defaultPricing = true });

            } else
            {
                return new JsonResult(new { productPricing = pricing, defaultPricing = false });
            }
               
        }

        [HttpPost]
        public async Task<IActionResult> Post(Price price)
        {
            var existing = await _db.Prices.FirstOrDefaultAsync(x => x.ItemID.Equals(price.ItemID) && x.LocationID.Equals(price.LocationID));
            if (existing != null) return BadRequest("Price for this item has already been set for this location.");

            _db.Prices.Add(price);
            await _db.SaveChangesAsync();

            return new JsonResult(new { ItemID = price.ItemID, LocationID = price.LocationID });
        }

        [HttpPut]
        public async Task<IActionResult> Update(Price price)
        {
            var existing = await _db.Prices.FirstOrDefaultAsync(x => x.ItemID.Equals(price.ItemID) && x.LocationID.Equals(price.LocationID));
            if (existing == null) return BadRequest("Price for this item does not exist at this location.");

            existing.UnitPrice = price.UnitPrice;

            _db.Prices.Update(existing);
            await _db.SaveChangesAsync();

            return new JsonResult(new { ItemID = price.ItemID, LocationID = price.LocationID });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int itemID, int locationID)
        {
            var existing = await _db.Prices.FirstOrDefaultAsync(x => x.ItemID.Equals(itemID) && x.LocationID.Equals(locationID));
            if (existing == null) return BadRequest("Price for this item not found at specified location.");

            _db.Prices.Remove(existing);
            await _db.SaveChangesAsync();

            return new OkObjectResult("Pricing information deleted");
        }
    }
}
