using AdSet.Lead.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdSet.Lead.Infrastructure.Data.Configurations;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CreatedOn).IsRequired();
        builder.Property(p => p.UpdatedOn).IsRequired();
        builder.Property(p => p.Url)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne<Vehicle>()
            .WithMany(v => v.Photos)
            .HasForeignKey("VehicleId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}