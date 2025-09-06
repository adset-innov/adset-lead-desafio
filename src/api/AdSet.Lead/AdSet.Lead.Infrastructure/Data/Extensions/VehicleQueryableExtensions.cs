using AdSet.Lead.Domain.Entities;
using AdSet.Lead.Domain.Filters;

namespace AdSet.Lead.Infrastructure.Data.Extensions;

public static class VehicleQueryableExtensions
{
    public static IQueryable<Vehicle> ApplyFilters(this IQueryable<Vehicle> query, VehicleSearchFilter filter)
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filter.Plate),
                v => v.LicensePlate.Value.Contains(filter.Plate!))
            .WhereIf(!string.IsNullOrWhiteSpace(filter.Brand),
                v => v.Brand.Contains(filter.Brand!))
            .WhereIf(!string.IsNullOrWhiteSpace(filter.Model),
                v => v.Model.Contains(filter.Model!))
            .WhereIf(filter.YearMin.HasValue,
                v => v.Year >= filter.YearMin!.Value)
            .WhereIf(filter.YearMax.HasValue,
                v => v.Year <= filter.YearMax!.Value)
            .WhereIf(filter.PriceMin.HasValue,
                v => v.Price >= filter.PriceMin!.Value)
            .WhereIf(filter.PriceMax.HasValue,
                v => v.Price <= filter.PriceMax!.Value)
            .WhereIf(filter.HasPhotos == true,
                v => v.Photos.Any())
            .WhereIf(filter.HasPhotos == false,
                v => !v.Photos.Any())
            .WhereIf(!string.IsNullOrWhiteSpace(filter.Color),
                v => v.Color.Value == filter.Color!)
            .WhereIf(filter.Options?.AirConditioning == true,
                v => v.Options.AirConditioning)
            .WhereIf(filter.Options?.Alarm == true,
                v => v.Options.Alarm)
            .WhereIf(filter.Options?.Airbag == true,
                v => v.Options.Airbag)
            .WhereIf(filter.Options?.AbsBrakes == true,
                v => v.Options.AbsBrakes)
            .WhereIf(filter.Portal.HasValue,
                v => v.PortalPackages.Any(pp => pp.Portal == filter.Portal))
            .WhereIf(filter.Package.HasValue,
                v => v.PortalPackages.Any(pp => pp.Package == filter.Package));
    }
}