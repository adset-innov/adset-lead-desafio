using AdSet.Lead.Domain.Entities;
using AdSet.Lead.Domain.Enums;
using AdSet.Lead.Domain.Filters;
using AdSet.Lead.Domain.Interfaces;
using AdSet.Lead.Domain.VOs;

namespace AdSet.Lead.Domain.Repositories;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<int> GetTotalCountAsync();
    Task<int> GetWithPhotosCountAsync();
    Task<int> GetWithoutPhotosCountAsync();
    Task<IEnumerable<string>> GetDistinctColorsAsync();
    Task<PagedResult<Vehicle>> SearchAsync(VehicleSearchFilter filter);
}