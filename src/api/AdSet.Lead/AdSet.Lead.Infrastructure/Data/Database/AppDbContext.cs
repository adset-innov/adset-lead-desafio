using AdSet.Lead.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdSet.Lead.Infrastructure.Data.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Vehicle> Vehicles { get; init; }
    public DbSet<Photo> Photos { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}