using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace SubwayApi;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly UserManager<IdentityUser> _userManager;

    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, UserManager<IdentityUser> userManager) : base(options, logger, encoder, clock)
    {
        _userManager = userManager;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        const string authorizationHeaderName = "Authorization";

        if (!Request.Headers.ContainsKey(authorizationHeaderName))
        {
            return AuthenticateResult.NoResult();
        }

        if (!AuthenticationHeaderValue.TryParse(Request.Headers[authorizationHeaderName], out var authorizationHeaderValue))
        {
            return AuthenticateResult.NoResult();
        }

        if (authorizationHeaderValue.Scheme != Scheme.Name)
        {
            return AuthenticateResult.NoResult();
        }

        var credentialsBase64 = authorizationHeaderValue.Parameter;

        string username, password;
        try
        {
            (username, password) = DecodeBasicCredential(credentialsBase64);
        }
        catch (Exception exception)
        {
            return AuthenticateResult.Fail($"Failed to decode basic credential. {exception.Message}");
        }

        var user = await _userManager.FindByNameAsync(username);
        var passwordValid = await _userManager.CheckPasswordAsync(user, password);

        if (!passwordValid)
        {
            return AuthenticateResult.Fail("Invalid username or password.");
        }

        var identity = new GenericIdentity(username);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }

    private static (string username, string password) DecodeBasicCredential(string? credential)
    {
        var credentialsBytes = Convert.FromBase64String(credential!);
        var credentials = Encoding.UTF8.GetString(credentialsBytes);
        var parts = credentials.Split(':');
        return (
            username: parts[0],
            password: parts[1]
        );
    }
}