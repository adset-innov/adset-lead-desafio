using System.Text.Json;
using AdSet.Lead.Domain.VOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdSet.Lead.Infrastructure.Data.Configurations;

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