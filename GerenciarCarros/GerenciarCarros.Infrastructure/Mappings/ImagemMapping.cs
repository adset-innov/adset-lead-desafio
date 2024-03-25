using GerenciarCarros.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciarCarros.Infrastructure.Mappings
{
    public class ImagemMapping : IEntityTypeConfiguration<Imagem>
    {
        public void Configure(EntityTypeBuilder<Imagem> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnType("uniqueidentifier");

            builder.Property(p => p.Bytes)
                // .HasColumnType("varbinary")
                .IsRequired();

            builder.Property(p => p.Tipo)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Nome)
               .IsRequired(false)
               .HasColumnType("varchar(200)");

            builder.ToTable("Imagens");
        }
    }
}
