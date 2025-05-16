using LaptimeBaseAPI.Data;
using LaptimeBaseAPI.Helper;
using LaptimeBaseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Team;

namespace LaptimeBaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "admin")]
    public class TeamsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeamsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeams()
        {
            var result = (await _context.Teams
                    .Include(x => x.Car)
                    .ToListAsync())
                .Select(x => x.ToTeamDto());
            
            return Ok(result);
        }
        
        // POST: api/teams
        [HttpPost]
        public async Task<ActionResult<TeamDto>> PostTeam(NewTeamRequest request)
        {
            var car = await _context.Cars.FindAsync(request.CarId);

            if (car is null)
            {
                return BadRequest($"Car with ID {request.CarId} not found.");
            }

            var team = new Team
            {
                Name = request.Name,
                Car = car,
            };
            
            _context.Teams.Add(team);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeams), team.ToTeamDto());
        }
    }
}
