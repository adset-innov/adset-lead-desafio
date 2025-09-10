using System.Text.Json;
using AdSet.Lead.Domain.VOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdSet.Lead.Infrastructure.Data.Configurations;

public static class ValueObjectConfigurations
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

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
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        builder.Property(o => o.Options)
            .HasColumnName("VehicleOptions")
            .HasConversion(
                v => JsonSerializer.Serialize(v, options),
                v => JsonSerializer.Deserialize<Dictionary<string, bool>>(v, options)
                     ?? new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase))
            .Metadata.SetValueComparer(
                new ValueComparer<Dictionary<string, bool>>(
                    (d1, d2) => d1 != null && d2 != null && d1.Count == d2.Count && !d1.Except(d2).Any(),
                    d => d.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
                    d => d.ToDictionary(entry => entry.Key, entry => entry.Value, StringComparer.OrdinalIgnoreCase)
                )
            );

        builder.Property(o => o.Options).HasColumnType("nvarchar(max)");
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