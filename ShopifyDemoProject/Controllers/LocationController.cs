using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopifyDemoProject.Models;

namespace ShopifyDemoProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : Controller
    {
        private readonly AppDbContext _db;

        public LocationController(AppDbContext db)
        {
            _db = db;
        }

        //Produces a list of all locations
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var locations = _db.Locations.ToList();
            return new JsonResult(locations);
        }

        //Produces information about a specific location
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var location = await _db.Locations.FirstOrDefaultAsync(x => x.Id == id);
            if (location == null) return BadRequest("Location not found");
            return new JsonResult(location);
        }

        //Accepts data through post to create a new location record
        [HttpPost]
        public async Task<IActionResult> Post(Location location)
        {
            _db.Locations.Add(location);
            await _db.SaveChangesAsync();

            return new JsonResult(new { Id = location.Id });
        }

        //Accepts data through post to update a location record
        [HttpPut]
        public async Task<IActionResult> Update(Location location)
        {
            var existing = await _db.Locations.FirstOrDefaultAsync(x => x.Id.Equals(location.Id));
            if (existing == null) return BadRequest("Location not found");

            existing.Name = location.Name;
            existing.Address = location.Address;
            existing.Capacity = location.Capacity;

            _db.Locations.Update(existing);
            await _db.SaveChangesAsync();

            return new JsonResult(location.Id);
        }

        //Deletes a specific location given the location id
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var location = await _db.Locations.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (location == null) return BadRequest("Location not found");

            _db.Locations.Remove(location);
            await _db.SaveChangesAsync();

            return new OkObjectResult("Location deleted");
        }
    }
}
