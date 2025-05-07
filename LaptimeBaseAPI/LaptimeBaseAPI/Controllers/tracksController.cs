using System.Formats.Asn1;
using LaptimeBaseAPI.Data;
using LaptimeBaseAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Track;
using LaptimeBaseAPI.Helper;

namespace LaptimeBaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class TracksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TracksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/tracks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Track>>> GetTracks()
        {
            return await _context.Tracks.ToListAsync();
        }

        // POST: api/tracks
        [HttpPost]
        public async Task<ActionResult<Team>> PostTrack(NewTrackRequest request)
        {
            var newTrack = request.ToTrackModel();

            _context.Tracks.Add(newTrack);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TrackExists(newTrack.Name))
                    return Conflict();
                else throw;
            }
            return CreatedAtAction(nameof(GetTracks), newTrack.ToTrackDto());
        }

        private bool TrackExists(string trackName)
        {
            return _context.Tracks.Any(t => t.Name == trackName);
        }
    }
}
