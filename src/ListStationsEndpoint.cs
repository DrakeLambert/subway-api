using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace SubwayApi;

public class ListStationsEndpoint : EndpointBaseSync
    .WithoutRequest
    .WithResult<IEnumerable<Station>>
{
    private readonly StationRepository _stationRepository;

    public ListStationsEndpoint(StationRepository stationRepository)
    {
        _stationRepository = stationRepository;
    }

    [HttpGet("/stations")]
    public override IEnumerable<Station> Handle() => _stationRepository;
}
