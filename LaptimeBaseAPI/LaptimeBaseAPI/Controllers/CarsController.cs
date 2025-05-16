using LaptimeBaseAPI.Data;
using LaptimeBaseAPI.Helper;
using LaptimeBaseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Car;

namespace LaptimeBaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCars()
        {
            var result = (await _context.Cars.ToListAsync())
                .Select(x => x.ToCarDto());

            return Ok(result);
        }

        // POST: api/cars
        [HttpPost]
        public async Task<ActionResult<CarDto>> PostCar(NewCarRequest request)
        {
            var newCar = new Car
            {
                Class = request.Class,
            };

            _context.Cars.Add(newCar);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCars), newCar.ToCarDto());
        }

        private bool CarExists(string carClass)
        {
            return _context.Cars.Any(c => c.Class == carClass);
        }
    }
}
