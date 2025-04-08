// Controllers/LapTimeController.cs
using Microsoft.AspNetCore.Mvc;
using LaptimeBaseAPI.Data;
using LaptimeBaseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LaptimeBaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class laptimesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public laptimesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/laptimes
        [HttpGet]

        /// <summmary>
        /// Lists all laptimes from database.
        /// </summmary>
        public async Task<ActionResult<IEnumerable<Laptime>>> GetLaptimes()
        {
            return await _context.Laptimes.ToListAsync();
        }

        // GET: api/laptimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Laptime>> GetLapTime(int id)
        {
            var lapTime = await _context.Laptimes.FindAsync(id);
            if (lapTime == null)
            {
                return NotFound();
            }
            return lapTime;
        }
    }
}