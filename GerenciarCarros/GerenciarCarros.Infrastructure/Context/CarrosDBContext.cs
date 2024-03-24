using GerenciarCarros.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciarCarros.Infrastructure.Context
{
    public class CarrosDBContext : DbContext
    {
        public CarrosDBContext(DbContextOptions<CarrosDBContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Carro> Carros { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
        public DbSet<Opcionais> Opcionais { get; set; }
        public DbSet<Pacote> Pacotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarrosDBContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
