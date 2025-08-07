using Backend_adset_lead.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_adset_lead.Contexts
{
    public class AdsetLeadContext : DbContext
    {
        public AdsetLeadContext(DbContextOptions<AdsetLeadContext> options) : base(options) { }

        public DbSet<Carro> Carros { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<PortalPacote> PortaisPacotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carro>()
                .HasMany(c => c.Fotos)
                .WithOne(f => f.Carro)
                .HasForeignKey(f => f.CarroId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PortalPacote>()
                .HasIndex(v => new { v.CarroId, v.Portal })
                .IsUnique();
        }
    }
}
