using adset_api.Data;
using Microsoft.EntityFrameworkCore;

public class ContextAdSet : DbContext
{
    public DbSet<Veiculo> Veiculos { get; set; }
    public DbSet<Pacote> Pacotes { get; set; }
    public DbSet<FotoVeiculo> FotosVeiculo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Veiculo
        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.ToTable("Veiculo");
            entity.HasKey(e => e.IdVeiculo);

            entity.Property(e => e.Marca).IsRequired();
            entity.Property(e => e.Modelo).IsRequired();
            entity.Property(e => e.Ano).IsRequired();
            entity.Property(e => e.Placa).IsRequired();
            entity.Property(e => e.Quilometragem).IsRequired(false);
            entity.Property(e => e.Cor).IsRequired();
            entity.Property(e => e.Preco).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(e => e.ListaOpcionais).IsRequired(false);

            entity.HasMany(e => e.Pacote)
                .WithOne(p => p.Veiculo)
                .HasForeignKey(p => p.IdVeiculo)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.FotosVeiculo)
                .WithOne(f => f.Veiculo)
                .HasForeignKey(f => f.IdVeiculo)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Pacote
        modelBuilder.Entity<Pacote>(entity =>
        {
            entity.ToTable("Pacotes");
            entity.HasKey(e => e.IdPacote);

            entity.Property(e => e.IdVeiculo).IsRequired();
            entity.Property(e => e.TipoPacote).IsRequired();
            entity.Property(e => e.TipoPortal).IsRequired();

            entity.HasOne(p => p.Veiculo)
                .WithMany(v => v.Pacote)
                .HasForeignKey(p => p.IdVeiculo)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // FotoVeiculo
        modelBuilder.Entity<FotoVeiculo>(entity =>
        {
            entity.ToTable("FotosVeiculo");
            entity.HasKey(e => e.IdFoto);

            entity.Property(e => e.IdVeiculo).IsRequired();
            entity.Property(e => e.CaminhoUrl).IsRequired();

            entity.HasOne(f => f.Veiculo)
                .WithMany(v => v.FotosVeiculo)
                .HasForeignKey(f => f.IdVeiculo)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}