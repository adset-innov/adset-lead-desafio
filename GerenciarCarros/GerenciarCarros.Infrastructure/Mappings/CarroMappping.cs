using GerenciarCarros.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciarCarros.Infrastructure.Mappings
{
    public class CarroMappping : IEntityTypeConfiguration<Carro>
    {
        public void Configure(EntityTypeBuilder<Carro> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnType("uniqueidentifier");

            builder.Property(p => p.Modelo)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Marca)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Preco)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.Ano)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(p => p.Cor)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Km)
               .IsRequired()
               .HasColumnType("decimal(10,2)");

            builder.HasMany(c => c.Opcionais)
                .WithOne(c => c.Carro)
                .HasForeignKey(c => c.IdCarro);

            builder.HasMany(c => c.Imagens)
                .WithOne(c => c.Carro)
                .HasForeignKey(c => c.IdCarro);

            builder.HasMany(c => c.Pacotes)
               .WithOne(c => c.Carro)
               .HasForeignKey(c => c.IdCarro);


            builder.ToTable("Carros");
        }
    }
}
