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

        //Additional context: The thinking behind this controller, schema, etc. was that at certain locations you would probably want to set differant prices than the MSRP you would use at majority of locations

        //Produces a list of all alternative prices set
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var prices = _db.Prices.ToList();
            return new JsonResult(prices);
        }

        //Get a price for a specific product at a specific location, if there is no set price, the default price will be displayed
        [HttpGet("{productID}/{locationID}")]
        public async Task<IActionResult> Get(int productID, int locationID)
        {
            var pricing = await _db.Prices.FirstOrDefaultAsync(x => x.ProductID.Equals(productID) && x.LocationID.Equals(locationID));
            if (pricing == null)
            {
                var product = await _db.Products.FirstOrDefaultAsync(x => x.Id.Equals(productID));
                if (product == null) return BadRequest("Product not found");

                pricing = new Price();
                pricing.LocationID = locationID;
                pricing.UnitPrice = product.DefaultPrice;
                pricing.ProductID = productID;

                return new JsonResult(new { productPricing = pricing, defaultPricing = true });

            } else
            {
                return new JsonResult(new { productPricing = pricing, defaultPricing = false });
            }
               
        }

        //Create an alernative price by passing json through post
        [HttpPost]
        public async Task<IActionResult> Post(Price price)
        {
            var existing = await _db.Prices.FirstOrDefaultAsync(x => x.ProductID.Equals(price.ProductID) && x.LocationID.Equals(price.LocationID));
            if (existing != null) return BadRequest("Price for this item has already been set for this location.");

            _db.Prices.Add(price);
            await _db.SaveChangesAsync();

            return new JsonResult(new { ItemID = price.ProductID, LocationID = price.LocationID });
        }

        //Update an alernative price record by passing json
        [HttpPut]
        public async Task<IActionResult> Update(Price price)
        {
            var existing = await _db.Prices.FirstOrDefaultAsync(x => x.ProductID.Equals(price.ProductID) && x.LocationID.Equals(price.LocationID));
            if (existing == null) return BadRequest("Price for this item does not exist at this location.");

            existing.UnitPrice = price.UnitPrice;

            _db.Prices.Update(existing);
            await _db.SaveChangesAsync();

            return new JsonResult(new { ItemID = price.ProductID, LocationID = price.LocationID });
        }

        //Delete a specific price record by passing a product ID and location ID
        [HttpDelete]
        public async Task<IActionResult> Delete(int productID, int locationID)
        {
            var existing = await _db.Prices.FirstOrDefaultAsync(x => x.ProductID.Equals(productID) && x.LocationID.Equals(locationID));
            if (existing == null) return BadRequest("Price for this item not found at specified location.");

            _db.Prices.Remove(existing);
            await _db.SaveChangesAsync();

            return new OkObjectResult("Pricing information deleted");
        }
    }
}
