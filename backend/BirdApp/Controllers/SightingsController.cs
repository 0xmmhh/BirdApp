using BirdApp.Data;
using BirdApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirdApp.Controllers;

[ApiController]
[Route("api/sightings")]
public class SightingsController : ControllerBase
{
    private readonly BirdAppDbContext _context;

    public SightingsController(BirdAppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Sighting>>> GetSightings()
    {
        return await _context.Sightings.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Sighting>> GetSightingById([FromRoute] int id)
    {
        var result = await _context.Sightings.FindAsync(id);
        if (result is null) return NotFound();
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<Sighting>> AddSighting(Sighting sighting)
    {
        _context.Sightings.Add(sighting);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSightingById), new { id = sighting.Id });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSighting(int id, [FromBody] Sighting sighting)
    {
        if (id != sighting.Id) return BadRequest();
        var query = await _context.Sightings.FindAsync(id);

        if (query is null) return NotFound();

        query.Date = sighting.Date;
        query.Latitude = sighting.Latitude;
        query.Longitude = sighting.Longitude;
        query.Notes = sighting.Notes;
        query.SpeciesId = sighting.SpeciesId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSighting([FromRoute] int id)
    {
        var sighting = await _context.Sightings.FindAsync(id);

        if (sighting is null) return NotFound();

        _context.Sightings.Remove(sighting);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}