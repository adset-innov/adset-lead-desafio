using AdSet.Lead.Domain.Entities;
using AdSet.Lead.Domain.Interfaces;

namespace AdSet.Lead.Domain.Repositories;

public interface IVehicleOptionRepository : IRepository<VehicleOption>
{
    Task<VehicleOption?> GetByNameAsync(string name);
    Task<IEnumerable<VehicleOption>> SearchByNameAsync(string query);
};