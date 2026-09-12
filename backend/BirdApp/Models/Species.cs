namespace BirdApp.Models;

public class Species
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Notes { get; set; }
    public List<Sighting> Sightings { get; set; }
}