using System.ComponentModel.DataAnnotations;

namespace SubwayApi.Endpoints;

public class FrequentedStationRequest
{
    [Required]
    public Guid? StationId { get; set; }
}