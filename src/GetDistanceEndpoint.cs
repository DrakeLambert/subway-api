using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace SubwayApi;

public class GetDistanceEndpoint : EndpointBaseAsync
    .WithRequest<GetDistanceRequest>
    .WithActionResult<GetDistanceResponse>
{
    private readonly StationRepository _stationRepository;

    public GetDistanceEndpoint(StationRepository stationRepository)
    {
        _stationRepository = stationRepository;
    }

    [HttpGet("/distances")]
    public override async Task<ActionResult<GetDistanceResponse>> HandleAsync([FromQuery] GetDistanceRequest request, CancellationToken cancellationToken = default)
    {
        var station1 = await _stationRepository.GetByIdAsync(request.Station1Id);
        var station2 = await _stationRepository.GetByIdAsync(request.Station2Id);

        if (station1 is null || station2 is null)
        {
            return NotFound();
        }

        return new GetDistanceResponse(station1.DistanceTo(station2));
    }
}
