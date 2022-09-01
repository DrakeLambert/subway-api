using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SubwayApi;

public class SubwayApiDbContext : IdentityDbContext<IdentityUser>
{
    public SubwayApiDbContext(DbContextOptions options) : base(options)
    { }

    public DbSet<Station> Stations { get; set; }
}