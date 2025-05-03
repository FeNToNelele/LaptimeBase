using LaptimeBaseAPI.Data;
using LaptimeBaseAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaptimeBaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        // POST: api/cars
        [HttpPost]
        public async Task<ActionResult<Team>> PostCar(Car newCar)
        {
            _context.Cars.Add(newCar);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CarExists(newCar.Class))
                    return Conflict();
                else throw;
            }
            return CreatedAtAction(nameof(GetCars), newCar);
        }

        private bool CarExists(string carClass)
        {
            return _context.Cars.Any(c => c.Class == carClass);
        }
    }
}
