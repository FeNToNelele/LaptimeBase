using LaptimeBaseAPI.Data;
using LaptimeBaseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LaptimeBaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public usersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<usersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET api/<usersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return null;
        }

        // POST api/<usersController>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<usersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<usersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
