using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SubwayApi.Endpoints;

public class AddUserEndpoint : EndpointBaseAsync
    .WithRequest<AddUserRequest>
    .WithActionResult
{
    private readonly UserManager<IdentityUser> _userManager;

    public AddUserEndpoint(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("users")]
    public override async Task<ActionResult> HandleAsync([FromBody] AddUserRequest request, CancellationToken cancellationToken = default)
    {
        var newUser = new IdentityUser(request.Username);
        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (result.Succeeded)
        {
            return NoContent();
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return ValidationProblem();
    }
}
