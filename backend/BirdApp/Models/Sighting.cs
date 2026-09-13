namespace BirdApp.Models;

public class Sighting
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Notes { get; set; }
    public int? SpeciesId { get; set; }
    public Species? Species { get; set; } = null!;
    public List<Photo> Photos { get; set; } = new();
}