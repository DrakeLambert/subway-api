using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SubwayApi.Endpoints;

public class AddFrequentedStationEndpoint : EndpointBaseAsync
    .WithRequest<AddFrequentedStationRequest>
    .WithActionResult
{
    private readonly SubwayApiDbContext _db;

    public AddFrequentedStationEndpoint(SubwayApiDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Add a station to the current user's list of frequented stations. Requires authentication.
    /// </summary>
    [Authorize]
    [HttpPost("frequented-stations")]
    public override async Task<ActionResult> HandleAsync(AddFrequentedStationRequest request, CancellationToken cancellationToken = default)
    {
        var station = await _db.Stations
            .SingleOrDefaultAsync(station => station.Id == request.StationId, cancellationToken: cancellationToken);
        if (station is null)
        {
            ModelState.AddModelError(nameof(AddFrequentedStationRequest.StationId), "Station does not exist.");
            return ValidationProblem();
        }

        var newFrequentedStation = new FrequentedStation(
            request.StationId!.Value,
            User.Identity!.Name!
        );

        _db.FrequentedStations.Add(newFrequentedStation);
        await _db.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}
