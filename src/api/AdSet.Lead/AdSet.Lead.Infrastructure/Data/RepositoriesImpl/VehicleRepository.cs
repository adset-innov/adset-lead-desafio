using AdSet.Lead.Domain.Entities;
using AdSet.Lead.Domain.Enums;
using AdSet.Lead.Domain.Interfaces;
using AdSet.Lead.Domain.Repositories;
using AdSet.Lead.Domain.VOs;
using AdSet.Lead.Infrastructure.Data.Database;

namespace AdSet.Lead.Infrastructure.Data.RepositoriesImpl;

public class VehicleRepository(AppDbContext context) : IVehicleRepository
{
    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Vehicle> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Vehicle vehicle)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Vehicle entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetTotalCountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> GetWithPhotosCountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> GetWithoutPhotosCountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> GetDistinctColorsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PagedResult<Vehicle>> SearchAsync(string? plate = null, string? brand = null, string? model = null,
        int? yearMin = null,
        int? yearMax = null, decimal? priceMin = null, decimal? priceMax = null, bool? hasPhotos = null,
        string? color = null, VehicleOptions? options = null, Portal? portal = null, Package? package = null,
        IPaginationFilter? pagination = null)
    {
        throw new NotImplementedException();
    }
}