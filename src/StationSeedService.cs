using System.Text.Json;
using Microsoft.Extensions.FileProviders;

namespace SubwayApi;

public class StationSeedService : IHostedService
{
    private readonly IFileProvider _fileProvider;
    private readonly IServiceProvider _services;

    private const string STATION_FILE_NAME = "subway-stations.json";

    private static readonly JsonSerializerOptions _serializerOptions = new(JsonSerializerDefaults.Web);

    public StationSeedService(IFileProvider fileProvider, IServiceProvider services)
    {
        _fileProvider = fileProvider;
        _services = services;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var file = _fileProvider.GetFileInfo(STATION_FILE_NAME);
        if (!file.Exists)
        {
            throw new Exception($"Cannot locate {file.PhysicalPath}");
        }

        var stream = file.CreateReadStream();

        var stations = await JsonSerializer.DeserializeAsync<IEnumerable<Station>>(stream, _serializerOptions, cancellationToken);

        if (stations is null)
        {
            throw new Exception($"{file.PhysicalPath} must not contain a null value.");
        }

        using var serviceScope = _services.CreateScope();
        var repository = serviceScope.ServiceProvider.GetRequiredService<StationRepository>();
        await repository.AddRangeAsync(stations); 
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
