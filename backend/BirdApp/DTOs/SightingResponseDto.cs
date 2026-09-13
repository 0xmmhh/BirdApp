namespace BirdApp.DTOs;

public record SightingResponseDto(
    int Id,
    double Latitude,
    double Longitude,
    DateTime Date,
    string? Notes,
    int? SpeciesId,
    string? SpeciesName
);