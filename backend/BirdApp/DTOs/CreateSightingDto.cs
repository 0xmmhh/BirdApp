namespace BirdApp.DTOs;

public record CreateSightingDto(
    double Latitude,
    double Longitude,
    DateTime Date,
    string? Notes,
    int? SpeciesId);