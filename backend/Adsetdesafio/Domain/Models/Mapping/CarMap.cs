using Adsetdesafio.Domain.Models.Entities;
using Adsetdesafio.Shared.Utils.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Adsetdesafio.Domain.Models.Mapping
{
    public class CarMap : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.Marca)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Modelo)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Ano)
                   .IsRequired();

            builder.Property(c => c.Placa)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(c => c.Km)
                   .IsRequired(false);

            builder.Property(c => c.Cor)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(c => c.Preco)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(c => c.OpcionaisVeiculo);

            builder.Property(c => c.Fotos);

            builder.HasMany(c => c.PortaisAnuncio)
              .WithOne()
              .HasForeignKey(x => x.CarId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
