using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Application.Mappers;
using AdSet.Lead.Domain.Enums;
using AdSet.Lead.Domain.Filters;
using AdSet.Lead.Domain.Interfaces;
using AdSet.Lead.Domain.Repositories;
using AdSet.Lead.Domain.VOs;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class SearchVehicles(IVehicleRepository repository)
{
    public async Task<SearchVehiclesOutput> Execute(SearchVehiclesInput input)
    {
        var filter = new VehicleSearchFilter
        {
            Plate = input.Plate,
            Brand = input.Brand,
            Model = input.Model,
            YearMin = input.YearMin,
            YearMax = input.YearMax,
            PriceMin = input.PriceMin,
            PriceMax = input.PriceMax,
            HasPhotos = input.HasPhotos,
            Color = input.Color,
            Options = input.Options,
            Portal = input.Portal,
            Package = input.Package,
            Pagination = input.Pagination
        };

        var pagedResult = await repository.SearchAsync(filter);

        var dtos = pagedResult.Items.Select(VehicleMapper.ToDto).ToList();

        return new SearchVehiclesOutput(
            dtos,
            pagedResult.TotalCount,
            pagedResult.PageNumber,
            pagedResult.PageSize,
            pagedResult.TotalPages
        );
    }
}

public record SearchVehiclesInput(
    string? Plate,
    string? Brand,
    string? Model,
    int? YearMin,
    int? YearMax,
    decimal? PriceMin,
    decimal? PriceMax,
    bool? HasPhotos,
    string? Color,
    VehicleOptions? Options,
    Portal? Portal,
    Package? Package,
    IPaginationFilter Pagination
);

public record SearchVehiclesOutput(
    List<VehicleDto> Vehicles,
    int TotalCount,
    int PageNumber,
    int PageSize,
    int TotalPages
);