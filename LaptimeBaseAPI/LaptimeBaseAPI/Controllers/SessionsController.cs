using LaptimeBaseAPI.Data;
using LaptimeBaseAPI.Helper;
using LaptimeBaseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Session;

namespace LaptimeBaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SessionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/sessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionDto>>> GetSessions()
        {
            var result = (await _context.Sessions
                    .Include(s => s.Track)
                    .ToListAsync())
                .Select(x => x.ToSessionDto());

            return Ok(result);
        }

        // GET: api/sessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionDto>> GetSession(int id)
        {
            var session = await _context.Sessions
                .Include(x => x.Track)
                .Include(x => x.Laptimes)
                .ThenInclude(x => x.Team)
                .ThenInclude(x => x.Car)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (session is null)
            {
                return NotFound();
            }

            var result = session.ToSessionDto();

            return Ok(result);
        }

        // POST: api/sessions/old
        [HttpPost("old")]
        public async Task<ActionResult<SessionDto>> PostSession(NewSessionRequest request)
        {
            var track = await _context.Tracks.FindAsync(request.TrackId);
            
            if (track is null)
            {
                return BadRequest($"Track with ID {request.TrackId} not found.");
            }

            var laptimes = await _context.Laptimes
                .Include(x => x.Team)
                .ThenInclude(x => x.Car)
                .Where(x => request.LaptimeIds.Contains(x.Id))
                .ToListAsync();

            var notFoundLaptimes = request.LaptimeIds
                .Except(laptimes.Select(x => x.Id))
                .ToList();

            if (notFoundLaptimes.Any())
            {
                return BadRequest("Laptimes with IDs " + string.Join(", ", notFoundLaptimes) + " not found.");
            }

            var newSession = new Session
            {
                AmbientTemp = request.AmbientTemp,
                HeldAt = request.HeldAt,
                TrackTemp = request.TrackTemp,
                Laptimes = laptimes,
                Track = track,
            };

            _context.Sessions.Add(newSession);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSessions), newSession.ToSessionDto());
        }
        
        // POST: api/sessions
        // Valamiért bugos. Laptime-okat nem rendeli hozzá a session-höz
        [HttpPost]
        public async Task<ActionResult<SessionDto>> PostSession(NewSessionWithLaptimesRequest request)
        {
            var track = await _context.Tracks.FindAsync(request.TrackId);
            
            if (track is null)
            {
                return BadRequest($"Track with ID {request.TrackId} not found.");
            }

            var laptimes = new List<Laptime>();
            foreach (var laptimeRequest in request.NewLapRequests)
            {
                var team = await _context.Teams.FindAsync(laptimeRequest.TeamId);
                
                if (team is null)
                {
                    return BadRequest($"Team with ID {laptimeRequest.TeamId} not found.");
                }
                
                laptimes.Add(
                    new Laptime
                    {
                        CreatedAt = DateTime.Now,
                        Team = team,
                        Time = laptimeRequest.Laptime,
                    });
            }

            await _context.Laptimes.AddRangeAsync(laptimes);
            await _context.SaveChangesAsync();

            var newSession = new Session
            {
                AmbientTemp = request.AmbientTemp,
                HeldAt = request.HeldAt,
                TrackTemp = request.TrackTemp,
                Laptimes = laptimes,
                Track = track,
            };

            _context.Sessions.Add(newSession);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSessions), newSession.ToSessionDto());
        }

        // PUT: api/sessions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSession(int id, UpdateSessionRequest request)
        {
            var session = await _context.Sessions.FindAsync(id);

            if (session is null)
            {
                return NotFound();
            }

            var track = await _context.Tracks.FindAsync(request.TrackId);
            
            if (track is null)
            {
                return BadRequest($"Track with ID {request.TrackId} not found.");
            }

            session.HeldAt = request.HeldAt;
            session.AmbientTemp = request.AmbientTemp;
            session.TrackTemp = request.TrackTemp;
            session.Track = track;

            await _context.SaveChangesAsync(); 

            return NoContent();
        }

        // DELETE: api/sessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var session = await _context.Sessions.FindAsync(id);

            if (session is null)
            {
                return NotFound();
            }

            _context.Sessions.Remove(session);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
