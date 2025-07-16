using ADSET.DESAFIO.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;

namespace ADSET.DESAFIO.INFRASTRUCTURE.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Optional> Optionals { get; set; }
        public DbSet<CarOptional> CarOptionals { get; set; }
        public DbSet<CarPhoto> CarPhotos { get; set; }
        public DbSet<CarPortalPackage> CarPortalPackages { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<CarOptional>().HasKey(co => new { co.CarId, co.OptionalId });

            model.Entity<CarPortalPackage>().HasKey(pp => new { pp.CarId, pp.Portal });

            model.Entity<CarPhoto>().Property(p => p.Order);

            base.OnModelCreating(model);
        }
    }
}
