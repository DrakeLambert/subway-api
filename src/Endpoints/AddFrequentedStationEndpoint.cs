using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SubwayApi.Endpoints;

public class AddFrequentedStationEndpoint : EndpointBaseAsync
    .WithRequest<FrequentedStationRequest>
    .WithActionResult
{
    private readonly SubwayApiDbContext _db;

    public AddFrequentedStationEndpoint(SubwayApiDbContext db)
    {
        _db = db;
    }

    [Authorize]
    [HttpPost("frequented-stations")]
    public override async Task<ActionResult> HandleAsync(FrequentedStationRequest request, CancellationToken cancellationToken = default)
    {
        var station = await _db.Stations
            .SingleOrDefaultAsync(station => station.Id == request.StationId, cancellationToken: cancellationToken);
        if (station is null)
        {
            ModelState.AddModelError(nameof(FrequentedStationRequest.StationId), "Station does not exist.");
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
