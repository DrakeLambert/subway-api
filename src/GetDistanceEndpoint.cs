using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace SubwayApi;

public class GetDistanceEndpoint : EndpointBaseSync
    .WithRequest<GetDistanceRequest>
    .WithActionResult<double>
{
    private readonly StationRepository _stationRepository;

    public GetDistanceEndpoint(StationRepository stationRepository)
    {
        _stationRepository = stationRepository;
    }

    [HttpGet("/distances")]
    public override ActionResult<double> Handle([FromQuery] GetDistanceRequest request)
    {
        var station1 = _stationRepository.GetById(request.Station1Id);
        var station2 = _stationRepository.GetById(request.Station2Id);

        if (station1 is null || station2 is null)
        {
            return NotFound();
        }

        return station1.DistanceTo(station2);
    }
}
