using System.Collections;

namespace SubwayApi;

public class StationRepository : IEnumerable<Station>
{
    private readonly List<Station> _stations = new();

    public void AddRange(IEnumerable<Station> stations) => _stations.AddRange(stations);

    public IEnumerator<Station> GetEnumerator() => _stations.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _stations.GetEnumerator();

    public Station? GetById(Guid stationId) => _stations.SingleOrDefault(station => station.Id == stationId);
}
