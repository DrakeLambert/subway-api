using Microsoft.EntityFrameworkCore;

namespace SubwayApi;

public class SubwayApiDbContext : DbContext
{
    public SubwayApiDbContext(DbContextOptions options) : base(options)
    { }

    public DbSet<Station> Stations { get; set; }
}