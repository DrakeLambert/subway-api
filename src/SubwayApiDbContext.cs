using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SubwayApi;

public class SubwayApiDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Station> Stations { get; set; }

    public DbSet<FrequentedStation> FrequentedStations { get; set; }

    public SubwayApiDbContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<FrequentedStation>()
            .HasKey(frequentedStation => frequentedStation.Username);
    }
}