using AdSet.Lead.Domain.Entities;
using AdSet.Lead.Domain.Enums;
using AdSet.Lead.Domain.Interfaces;
using AdSet.Lead.Domain.VOs;

namespace AdSet.Lead.Domain.Repositories;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<int> GetTotalCountAsync();
    Task<int> GetWithPhotosCountAsync();
    Task<int> GetWithoutPhotosCountAsync();
    Task<IEnumerable<string>> GetDistinctColorsAsync();

    Task<PagedResult<Vehicle>> SearchAsync(
        string? plate = null,
        string? brand = null,
        string? model = null,
        int? yearMin = null,
        int? yearMax = null,
        decimal? priceMin = null,
        decimal? priceMax = null,
        bool? hasPhotos = null,
        string? color = null,
        VehicleOptions? options = null,
        Portal? portal = null,
        Package? package = null,
        IPaginationFilter? pagination = null
    );
}