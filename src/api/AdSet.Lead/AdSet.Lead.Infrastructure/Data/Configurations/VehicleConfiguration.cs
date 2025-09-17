using AdSet.Lead.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdSet.Lead.Infrastructure.Data.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.CreatedOn).IsRequired();
        builder.Property(v => v.UpdatedOn).IsRequired();
        builder.Property(v => v.Brand).IsRequired().HasMaxLength(100);
        builder.Property(v => v.Model).IsRequired().HasMaxLength(100);
        builder.Property(v => v.Year).IsRequired();
        builder.Property(v => v.Mileage).IsRequired();
        builder.Property(v => v.Price).HasColumnType("decimal(18,2)").IsRequired();

        builder.OwnsOne(v => v.LicensePlate, lp => lp.ConfigureLicensePlate());
        builder.OwnsOne(v => v.Color, c => c.ConfigureColor());

        builder.HasMany(v => v.Options)
            .WithMany(o => o.Vehicles)
            .UsingEntity<Dictionary<string, object>>(
                "VehicleOptionJoin",
                j => j.HasOne<VehicleOption>()
                    .WithMany()
                    .HasForeignKey("OptionId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Vehicle>()
                    .WithMany()
                    .HasForeignKey("VehicleId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("VehicleId", "OptionId");
                    j.ToTable("VehicleOptionsMap");
                });


        builder.HasMany(v => v.Photos)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsMany(v => v.PortalPackages, pp => pp.ConfigurePortalPackages());
    }
}