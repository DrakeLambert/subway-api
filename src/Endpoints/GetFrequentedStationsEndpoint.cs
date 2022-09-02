using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SubwayApi.Endpoints;

public class GetFrequentedStationsEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<IEnumerable<Station>>
{
    private readonly SubwayApiDbContext _db;

    public GetFrequentedStationsEndpoint(SubwayApiDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Get the current user's list of frequented stations. Requires authentication.
    /// </summary>
    [Authorize]
    [HttpGet("frequented-stations")]
    public override async Task<ActionResult<IEnumerable<Station>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var currentUsername = User.Identity!.Name!;
        var frequentedStations = await _db.FrequentedStations
            .Where(frequentedStation => frequentedStation.Username == currentUsername)
            .Join(
                _db.Stations,
                frequentedStation => frequentedStation.StationId,
                station => station.Id,
                (_, station) => station
            ).ToListAsync(cancellationToken: cancellationToken);

        return Ok(frequentedStations);
    }
}