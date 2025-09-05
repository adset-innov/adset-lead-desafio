using AdSet.Lead.Domain.Entities;
using AdSet.Lead.Infrastructure.Data.Configurations.Extensions;
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
        builder.OwnsOne(v => v.Options, o => o.ConfigureVehicleOptions());

        builder.HasMany(v => v.Photos)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsMany(v => v.PortalPackages, pp => pp.ConfigurePortalPackages());
    }
}