namespace BirdApp.Models;

public class Photo
{
    public int Id { get; set; }
    public string Url { get; set; }
    public int SightingId { get; set; }
    public Sighting Sighting { get; set; }
}