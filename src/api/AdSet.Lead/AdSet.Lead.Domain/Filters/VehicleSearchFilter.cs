using AdSet.Lead.Domain.Enums;
using AdSet.Lead.Domain.VOs;

namespace AdSet.Lead.Domain.Filters;

public sealed class VehicleSearchFilter
{
    public string? Plate { get; init; }
    public string? Brand { get; init; }
    public string? Model { get; init; }
    public int? YearMin { get; init; }
    public int? YearMax { get; init; }
    public decimal? PriceMin { get; init; }
    public decimal? PriceMax { get; init; }
    public bool? HasPhotos { get; init; }
    public string? Color { get; init; }
    public VehicleOptions? Options { get; init; }
    public Portal? Portal { get; init; }
    public Package? Package { get; init; }
    public PaginationFilter Pagination { get; init; } = new(1, 10);
}