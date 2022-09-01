using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace SubwayApi.Endpoints;

public class ListStationsEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithResult<IEnumerable<Station>>
{
    private readonly StationRepository _stationRepository;

    public ListStationsEndpoint(StationRepository stationRepository)
    {
        _stationRepository = stationRepository;
    }

    [HttpGet("/stations")]
    public override async Task<IEnumerable<Station>> HandleAsync(CancellationToken cancellationToken = default) => await _stationRepository.ListAsync();
}
