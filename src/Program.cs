using SubwayApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<StationRepository>();
builder.Services.AddSingleton(builder.Environment.ContentRootFileProvider);
builder.Services.AddHostedService<StationSeedService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
