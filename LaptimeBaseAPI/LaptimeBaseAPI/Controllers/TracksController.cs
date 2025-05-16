using LaptimeBaseAPI.Data;
using LaptimeBaseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Track;
using LaptimeBaseAPI.Helper;

namespace LaptimeBaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TracksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TracksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/tracks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackDto>>> GetTracks()
        {
            var result = (await _context.Tracks.ToListAsync())
                .Select(x => x.ToTrackDto());

            return Ok(result);
        }

        // POST: api/tracks
        [HttpPost]
        public async Task<ActionResult<TrackDto>> PostTrack(NewTrackRequest request)
        {
            var existingTrack = await _context.Tracks
                .FirstOrDefaultAsync(t => t.Name == request.Name);

            if (existingTrack is not null)
            {
                return BadRequest("Track already exists.");
            }
            
            var newTrack = new Track
            {
                Name = request.Name,
            };

            _context.Tracks.Add(newTrack);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTracks), newTrack.ToTrackDto());
        }
    }
}
