using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SubwayApi;

public class GetDistanceRequest
{
    [BindRequired]
    public Guid Station1Id { get; set; }

    [BindRequired]
    public Guid Station2Id { get; set; }
}