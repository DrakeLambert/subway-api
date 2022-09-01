namespace SubwayApi.Endpoints;

public record GetDistanceResponse(
    double Distance,
    string UnitOfMeasure = "mile"
);