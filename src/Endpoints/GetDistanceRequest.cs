using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SubwayApi.Endpoints;

public class GetDistanceRequest
{
    [BindRequired]
    public Guid Station1Id { get; set; }

    [BindRequired]
    public Guid Station2Id { get; set; }
}