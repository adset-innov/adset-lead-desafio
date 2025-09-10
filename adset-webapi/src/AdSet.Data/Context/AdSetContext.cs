using System.Reflection;

namespace AdSet.Data.Context
{
    public class AdSetContext : DbContext
    {
        public AdSetContext(DbContextOptions<AdSetContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Optional> Optionals { get; set; }
        public DbSet<VehicleOptional> VehicleOptionals { get; set; }
        public DbSet<VehicleImage> Images { get; set; }
        public DbSet<VehiclePortalPackage> VehiclePortalPackages { get; set; }
    }
}