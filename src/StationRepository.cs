using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace SubwayApi;

public class StationRepository
{
    private readonly SubwayApiDbContext _dbContext;

    public StationRepository(SubwayApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRangeAsync(IEnumerable<Station> stations)
    {
        _dbContext.Stations.AddRange(stations);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Station>> ListAsync() => await _dbContext.Stations.ToListAsync();

    public async Task<Station?> GetByIdAsync(Guid stationId) => await _dbContext.Stations.SingleOrDefaultAsync(station => station.Id == stationId);
}
