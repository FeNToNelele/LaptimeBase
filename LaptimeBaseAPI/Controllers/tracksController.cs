using LaptimeBaseAPI.Data;
using LaptimeBaseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LaptimeBaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tracksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public tracksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<tracksController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Track>>> GetTracks()
        {
            return await _context.Tracks.ToListAsync();
        }

        // GET api/<tracksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return null;
        }

        // POST api/<tracksController>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<tracksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<tracksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
