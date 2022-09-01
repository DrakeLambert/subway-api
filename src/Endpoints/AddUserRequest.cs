using System.ComponentModel.DataAnnotations;

namespace SubwayApi.Endpoints;

public class AddUserRequest
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
}