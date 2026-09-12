using Microsoft.EntityFrameworkCore;
using BirdApp.Models;

namespace BirdApp.Data;

public class BirdAppDbContext : DbContext
{
    public BirdAppDbContext(DbContextOptions<BirdAppDbContext> options) : base(options)
    {
    }

    public DbSet<Species> Species => Set<Species>();
    public DbSet<Sighting> Sightings => Set<Sighting>();
    public DbSet<Photo> Photos => Set<Photo>();
}