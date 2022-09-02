using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SubwayApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Name = "Authorization",
        Scheme = "Basic"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Basic"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<SubwayApiDbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(SubwayApiDbContext));
});
builder.Services.AddTransient<StationRepository>();
builder.Services.AddSingleton(builder.Environment.ContentRootFileProvider);
builder.Services.AddHostedService<StationSeedService>();

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<SubwayApiDbContext>();
var basicAuthenticationScheme = AuthenticationSchemes.Basic.ToString();
builder.Services.AddAuthentication(basicAuthenticationScheme)
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(basicAuthenticationScheme, null);

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
