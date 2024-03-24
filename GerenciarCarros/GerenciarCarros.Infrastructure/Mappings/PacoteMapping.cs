using GerenciarCarros.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciarCarros.Infrastructure.Mappings
{
    public sealed class PacoteMapping : IEntityTypeConfiguration<Pacote>
    {
        public void Configure(EntityTypeBuilder<Pacote> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnType("uniqueidentifier");

            builder.Property(p => p.TipoPacote)
                .IsRequired()
                .HasColumnType("int");

            builder.ToTable("Pacotes");
        }
    }
}
