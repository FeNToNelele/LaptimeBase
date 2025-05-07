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
        public async Task<ActionResult<IEnumerable<Session>>> GetSessions()
        {
            return await _context.Sessions.Include(s => s.Track).ToListAsync();
        }

        // GET: api/sessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetSession(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null) return NotFound();
            return session;
        }

        // POST: api/sessions
        [HttpPost]
        public async Task<ActionResult<SessionDto>> PostSession(NewSessionRequest session)
        {
            var newSession = session.ToSessionModel();

            _context.Sessions.Add(newSession);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSessions), newSession.ToSessionDto());
        }

        // PUT: api/sessions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSession(int id, Session session)
        {
            if (id != session.Id) return BadRequest();
            _context.Entry(session).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        // DELETE: api/sessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null) return NotFound();
            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool SessionExists(int id) => _context.Sessions.Any(e => e.Id == id);

        //GET: api/sessions/5/laptimes
        [HttpGet("{id}/laptimes")]
        public async Task<ActionResult<IEnumerable<Laptime>>> GetLaptimesFromSession(int id)
        {
            var sessionExists = await _context.Sessions.AnyAsync(s => s.Id == id);
            if (!sessionExists)
            {
                return NotFound();
            }

            var laps = await _context.Laptimes
                .Where(lt => lt.SessionId == id)
                .Include(lt => lt.Team)
                .ThenInclude(lt => lt.Car)
                .Include(lt => lt.Time)
                .Include(lt => lt.CreatedAt)
                .ToListAsync();

            return laps;
        }

    }
}
