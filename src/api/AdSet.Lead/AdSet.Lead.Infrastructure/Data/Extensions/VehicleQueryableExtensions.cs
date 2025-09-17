using AdSet.Lead.Domain.Entities;
using AdSet.Lead.Domain.Filters;

namespace AdSet.Lead.Infrastructure.Data.Extensions;

public static class VehicleQueryableExtensions
{
    public static IQueryable<Vehicle> ApplyFilters(this IQueryable<Vehicle> query, VehicleSearchFilter filter)
    {
        query = query
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
            .WhereIf(filter.HasPhotos is true, v => v.Photos.Any())
            .WhereIf(filter.HasPhotos is false, v => !v.Photos.Any())
            .WhereIf(!string.IsNullOrWhiteSpace(filter.Color),
                v => v.Color.Value == filter.Color!)
            .WhereIf(filter.Options != null && filter.Options.Count != 0,
                v => filter.Options!.All(opt =>
                    v.Options.Any(o =>
                        o.Name.Contains(opt, StringComparison.CurrentCultureIgnoreCase))))
            .WhereIf(filter.Portal.HasValue,
                v => v.PortalPackages.Any(pp => pp.Portal == filter.Portal))
            .WhereIf(filter.Package.HasValue,
                v => v.PortalPackages.Any(pp => pp.Package == filter.Package));

        return query;
    }

    public static IQueryable<Vehicle> ApplyOrdering(this IQueryable<Vehicle> query, VehicleSearchFilter filter)
    {
        if (string.IsNullOrWhiteSpace(filter.SortField))
            return query.OrderBy(v => v.CreatedOn);

        return filter.SortField.ToLower() switch
        {
            "brand" => filter.SortDirection == "desc"
                ? query.OrderByDescending(v => v.Brand)
                : query.OrderBy(v => v.Brand),

            "model" => filter.SortDirection == "desc"
                ? query.OrderByDescending(v => v.Model)
                : query.OrderBy(v => v.Model),

            "year" => filter.SortDirection == "desc"
                ? query.OrderByDescending(v => v.Year)
                : query.OrderBy(v => v.Year),

            "price" => filter.SortDirection == "desc"
                ? query.OrderByDescending(v => v.Price)
                : query.OrderBy(v => v.Price),

            "photos" => filter.SortDirection == "desc"
                ? query.OrderByDescending(v => v.Photos.Count)
                : query.OrderBy(v => v.Photos.Count),

            _ => query.OrderBy(v => v.CreatedOn)
        };
    }
}