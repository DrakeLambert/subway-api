using System.ComponentModel.DataAnnotations;

namespace SubwayApi.Endpoints;

public class AddFrequentedStationRequest
{
    [Required]
    public Guid? StationId { get; set; }
}