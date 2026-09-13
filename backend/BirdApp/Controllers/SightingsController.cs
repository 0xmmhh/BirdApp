using BirdApp.Data;
using BirdApp.DTOs;
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
    public async Task<ActionResult<List<SightingResponseDto>>> GetSightings()
    {
        var sightings = await _context.Sightings.Select(s => new SightingResponseDto(s.Id, s.Latitude, s.Longitude,
            s.Date, s.Notes, s.SpeciesId, s.Species != null ? s.Species.Name : null)).ToListAsync();
        return Ok(sightings);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SightingResponseDto>> GetSightingById([FromRoute] int id)
    {
        var sighting = await _context.Sightings
            .Include(s => s.Species)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sighting is null) return NotFound();

        var response = new SightingResponseDto(
            sighting.Id,
            sighting.Latitude,
            sighting.Longitude,
            sighting.Date,
            sighting.Notes,
            sighting.SpeciesId,
            sighting.Species?.Name
        );

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<SightingResponseDto>> AddSighting(CreateSightingDto dto)
    {
        var sighting = new Sighting
        {
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Date = dto.Date,
            Notes = dto.Notes,
            SpeciesId = dto.SpeciesId
        };

        _context.Sightings.Add(sighting);
        await _context.SaveChangesAsync();

        var response = new SightingResponseDto(
            sighting.Id,
            sighting.Latitude,
            sighting.Longitude,
            sighting.Date,
            sighting.Notes,
            sighting.SpeciesId,
            null
        );

        return CreatedAtAction(nameof(GetSightingById), new { id = sighting.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSighting(int id, [FromBody] CreateSightingDto dto)
    {
        var sighting = await _context.Sightings.FindAsync(id);

        if (sighting is null) return NotFound();

        sighting.Date = dto.Date;
        sighting.Latitude = dto.Latitude;
        sighting.Longitude = dto.Longitude;
        sighting.Notes = dto.Notes;
        sighting.SpeciesId = dto.SpeciesId;

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