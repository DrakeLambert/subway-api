using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SubwayApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SubwayApiDbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(SubwayApiDbContext));
});
builder.Services.AddTransient<StationRepository>();
builder.Services.AddSingleton(builder.Environment.ContentRootFileProvider);
builder.Services.AddHostedService<StationSeedService>();

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<SubwayApiDbContext>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();

app.MapControllers();

app.Run();
