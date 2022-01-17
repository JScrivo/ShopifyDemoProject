using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopifyDemoProject.Models;

namespace ShopifyDemoProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;

        public ProductController(AppDbContext db)
        {
            _db = db;
        }

        //Produces a list of all products on record
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = _db.Products.ToList();
            return new JsonResult(products);
        }

        //Produces information about a specific product
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) return BadRequest("Product not found");
            return new JsonResult(product);
        }

        //Accepts JSON through Post to create a new product record
        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return new JsonResult(new { Id = product.Id });
        }

        //Accepts JSON through Post to update a product record
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            var existing = await _db.Products.FirstOrDefaultAsync(x => x.Id.Equals(product.Id));
            if(existing == null) return BadRequest("Product not found");

            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.DefaultPrice = product.DefaultPrice;
            existing.VolPerUnit = product.VolPerUnit;
            _db.Products.Update(existing);
            await _db.SaveChangesAsync();

            return new JsonResult(product.Id);
        }

        //Deletes a product when provided with its ID
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (product == null) return BadRequest("Product not found");

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return new OkObjectResult("Product deleted");
        }
    }
}
