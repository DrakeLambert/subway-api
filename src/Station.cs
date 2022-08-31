namespace SubwayApi;

public record Station(
    string Notes,
    string Name,
    Guid Id,
    string Line,
    double Latitude,
    double Longitude
);
