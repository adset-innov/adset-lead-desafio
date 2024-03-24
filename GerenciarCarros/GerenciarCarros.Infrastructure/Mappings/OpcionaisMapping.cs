using GerenciarCarros.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciarCarros.Infrastructure.Mappings
{
    internal class OpcionaisMapping : IEntityTypeConfiguration<Opcionais>
    {
        public void Configure(EntityTypeBuilder<Opcionais> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnType("uniqueidentifier");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.ToTable("Opcionais");
        }
    }
}
