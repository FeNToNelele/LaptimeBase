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
    public class TeamsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeamsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return await _context.Teams
                .Include(t => t.User) // owner of the team
                .Include(t => t.Car)
                .ToListAsync();
        }

        // POST: api/teams
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            _context.Teams.Add(team);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TeamExists(team.UserId, team.CarId))
                    return Conflict();
                else throw;
            }
            return CreatedAtAction(nameof(GetTeams), team);
        }

        private bool TeamExists(int userId, int carId) =>
            _context.Teams.Any(e => e.UserId == userId && e.CarId == carId);
    }
}
