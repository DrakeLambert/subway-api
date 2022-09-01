namespace SubwayApi;

public record GetDistanceResponse(
    double Distance,
    string UnitOfMeasure = "mile"
);