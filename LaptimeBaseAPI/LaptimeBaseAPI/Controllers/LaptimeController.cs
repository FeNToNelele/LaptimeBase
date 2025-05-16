using LaptimeBaseAPI.Data;
using LaptimeBaseAPI.Helper;
using LaptimeBaseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Laptime;

namespace LaptimeBaseAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LaptimeController : ControllerBase
{
    private readonly AppDbContext _context;

    public LaptimeController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<LaptimeDto>>> GetLaptimes()
    {
        var result = (await _context.Laptimes
                .Include(x => x.Team)
                .ThenInclude(x => x.Car)
                .ToListAsync())
            .Select(x => x.ToLaptimeDto());
        
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult<LaptimeDto>> PostLaptime(NewLapRequest request)
    {
        var team = await _context.Teams
            .Include(x => x.Car)
            .FirstOrDefaultAsync(x => x.Id == request.TeamId);

        if (team is null)
        {
            return BadRequest($"Team with ID {request.TeamId} not found.");
        }

        var newLaptime = new Laptime
        {
            CreatedAt = DateTime.Now,
            Time = request.Laptime,
            Team = team,
        };

        _context.Laptimes.Add(newLaptime);
        
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLaptimes), newLaptime.ToLaptimeDto());
    }
}