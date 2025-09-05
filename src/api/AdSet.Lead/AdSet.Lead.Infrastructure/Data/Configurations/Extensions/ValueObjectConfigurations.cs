using AdSet.Lead.Domain.VOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdSet.Lead.Infrastructure.Data.Configurations.Extensions;

public static class ValueObjectConfigurations
{
    public static void ConfigureLicensePlate<T>(this OwnedNavigationBuilder<T, LicensePlate> builder)
        where T : class
    {
        builder.Property(lp => lp.Value)
            .HasColumnName("LicensePlate")
            .IsRequired()
            .HasMaxLength(10);
    }

    public static void ConfigureColor<T>(this OwnedNavigationBuilder<T, Color> builder)
        where T : class
    {
        builder.Property(c => c.Value)
            .HasColumnName("Color")
            .IsRequired()
            .HasMaxLength(50);
    }

    public static void ConfigureVehicleOptions<T>(this OwnedNavigationBuilder<T, VehicleOptions> builder)
        where T : class
    {
        builder.Property(o => o.AirConditioning).HasColumnName("HasAirConditioning");
        builder.Property(o => o.Alarm).HasColumnName("HasAlarm");
        builder.Property(o => o.Airbag).HasColumnName("HasAirbag");
        builder.Property(o => o.AbsBrakes).HasColumnName("HasAbsBrakes");
    }

    public static void ConfigurePortalPackages<T>(this OwnedNavigationBuilder<T, PortalPackage> builder)
        where T : class
    {
        builder.Property(pp => pp.Portal)
            .HasColumnName("Portal")
            .IsRequired();

        builder.Property(pp => pp.Package)
            .HasColumnName("Package")
            .IsRequired();

        builder.WithOwner().HasForeignKey("VehicleId");
        builder.HasKey("VehicleId", "Portal");
    }
}