using Adsetdesafio.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adsetdesafio.Domain.Models.Mapping
{
    public class AnnouncementPortalMap : IEntityTypeConfiguration<AnnouncementPortal>
    {
        public void Configure(EntityTypeBuilder<AnnouncementPortal> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.NomePortal)
                  .IsRequired()
                  .HasMaxLength(100);

            builder.Property(e => e.Categoria)
                  .HasConversion<string>()
                  .IsRequired();
        }
    }
}
