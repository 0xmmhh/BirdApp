using BirdApp.Data;
using BirdApp.DTOs;
using BirdApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirdApp.Controllers;

[ApiController]
[Route("api/species")]
public class SpeciesController : ControllerBase
{
    private readonly BirdAppDbContext _context;

    public SpeciesController(BirdAppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<SpeciesResponseDto>>> GetSpecies()
    {
        var species = await _context.Species.Select(s => new SpeciesResponseDto(s.Id, s.Name)).ToListAsync();
        return Ok(species);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SpeciesResponseDto>> GetSpeciesById([FromRoute] int id)
    {
        var species = await _context.Species.FindAsync(id);
        if (species is null) return NotFound();

        var response = new SpeciesResponseDto(
            species.Id,
            species.Name
        );

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<SpeciesResponseDto>> AddSpecies(CreateSpeciesDto dto)
    {
        var species = new Species
        {
            Name = dto.Name
        };

        _context.Species.Add(species);
        await _context.SaveChangesAsync();

        var response = new SpeciesResponseDto(
            species.Id,
            species.Name
        );

        return CreatedAtAction(nameof(GetSpeciesById), new { id = species.Id }, response);
    }
}